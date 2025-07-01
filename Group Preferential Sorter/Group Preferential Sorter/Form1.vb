Imports System.IO
Imports System.Windows.Forms

Public Class Form1
    Private lstPeople As List(Of Person)
    Private sorter As New GroupPreferentialSorter()
    Private isSorted As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSort.Enabled = False
        btnExport.Enabled = False
        AddHandler numUpDown.ValueChanged, AddressOf numUpDown_ValueChanged
    End Sub

    Private Sub numUpDown_ValueChanged(sender As Object, e As EventArgs)
        ValidateSortButton()
        ValidateExportButton()
    End Sub

    Private Sub ValidateSortButton()
        btnSort.Enabled = lstPeople IsNot Nothing AndAlso
                        lstPeople.Count > 0 AndAlso
                        numUpDown.Value > 0
    End Sub

    Private Sub ValidateExportButton()
        btnExport.Enabled = isSorted AndAlso numUpDown.Value > 0
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Using dlgOpen As New OpenFileDialog()
            dlgOpen.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlgOpen.Title = "Select People CSV File"
            If dlgOpen.ShowDialog() = DialogResult.OK Then
                lstPeople = sorter.LoadPeopleFromCsv(dlgOpen.FileName)
                MessageBox.Show($"Loaded {lstPeople.Count} people from file.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isSorted = False
                numUpDown.Maximum = If(lstPeople IsNot Nothing, lstPeople.Count, 1)
                numUpDown.Value = If(lstPeople IsNot Nothing AndAlso lstPeople.Count > 0, 1, 0)
                ValidateSortButton()
                ValidateExportButton()
            End If
        End Using
    End Sub

    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        If lstPeople Is Nothing OrElse lstPeople.Count = 0 Then
            MessageBox.Show("No people loaded to sort.", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim groupCount As Integer
        If Not Integer.TryParse(numUpDown.Value, groupCount) OrElse groupCount <= 0 Then
            MessageBox.Show("Invalid group amount.", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim dictGroups As New Dictionary(Of String, List(Of Person))()
        For i As Integer = 1 To groupCount
            dictGroups(i.ToString()) = New List(Of Person)()
        Next

        For Each p In lstPeople
            p.strAssignedGroup = Nothing
            p.hshExcludedGroups.Clear()
        Next

        sorter.PreferentialSort(lstPeople, dictGroups, Integer.MaxValue)

        isSorted = True
        ValidateExportButton()
        MessageBox.Show("Sorting complete. Groups have been assigned.", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If lstPeople Is Nothing OrElse lstPeople.Count = 0 Then
            MessageBox.Show("No people loaded to export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If Not isSorted Then
            MessageBox.Show("Please sort the groups before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim groupCount As Integer
        If Not Integer.TryParse(numUpDown.Value, groupCount) OrElse groupCount <= 0 Then
            MessageBox.Show("Invalid group amount.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim dictGroups As New Dictionary(Of String, List(Of Person))()
        For i As Integer = 1 To groupCount
            dictGroups(i.ToString()) = New List(Of Person)()
        Next

        For Each p In lstPeople
            p.strAssignedGroup = Nothing
            p.hshExcludedGroups.Clear()
        Next

        sorter.PreferentialSort(lstPeople, dictGroups, Integer.MaxValue)

        Dim maxGroupSize As Integer = dictGroups.Values.Max(Function(g) g.Count)

        Using dlgSave As New SaveFileDialog()
            dlgSave.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlgSave.Title = "Select File Path To Send"
            dlgSave.FileName = "SortedPeople.csv"
            If dlgSave.ShowDialog() = DialogResult.OK Then
                Try
                    Using sw As New StreamWriter(dlgSave.FileName, False)
                        Dim header As String = "Group Number"
                        For i As Integer = 1 To maxGroupSize
                            header &= $",Person {i}"
                        Next
                        sw.WriteLine(header)

                        For i As Integer = 1 To groupCount
                            Dim groupId = i.ToString()
                            Dim members = dictGroups(groupId).Select(Function(p) p.strName).ToList()
                            While members.Count < maxGroupSize
                                members.Add("")
                            End While
                            Dim line = String.Join(","c, New String() {groupId}.Concat(members))
                            sw.WriteLine(line)
                        Next
                    End Using
                    MessageBox.Show("Export successful.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Error exporting file: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End
                End Try
            End If
        End Using
    End Sub

End Class


' This class handles sorting people into groups based on their preferences, inclusion, and exclusion constraints.
Public Class GroupPreferentialSorter

    ' Assigns people to groups based on inclusion, exclusion, and preferences,
    ' but only using the numeric group IDs provided in dictGroups.
    Public Function PreferentialSort(lstPeople As List(Of Person), dictGroups As Dictionary(Of String, List(Of Person)), intMaxSize As Integer) As Dictionary(Of String, List(Of Person))
        ' Only allow group IDs that exist in dictGroups (i.e., "1", "2", ..., groupCount)
        Dim validGroupIds = New HashSet(Of String)(dictGroups.Keys)

        ' First, assign people to their inclusion group if specified and valid
        For Each objPerson As Person In lstPeople
            If Not String.IsNullOrEmpty(objPerson.strInclusion) AndAlso validGroupIds.Contains(objPerson.strInclusion) Then
                Dim strGroupId As String = objPerson.strInclusion
                If dictGroups(strGroupId).Count < intMaxSize Then
                    dictGroups(strGroupId).Add(objPerson)
                    objPerson.strAssignedGroup = strGroupId
                End If
            End If
        Next

        ' Add exclusion group to each person's exclusion set if specified and valid
        For Each objPerson As Person In lstPeople
            If Not String.IsNullOrEmpty(objPerson.strExclusion) AndAlso validGroupIds.Contains(objPerson.strExclusion) Then
                objPerson.hshExcludedGroups.Add(objPerson.strExclusion)
            End If
        Next

        ' Assign people to their preferred groups, skipping excluded groups and full groups, and only if valid
        For Each objPerson As Person In lstPeople
            If String.IsNullOrEmpty(objPerson.strAssignedGroup) Then
                For Each strPreference As String In objPerson.lstPreferences
                    If validGroupIds.Contains(strPreference) AndAlso
                       Not objPerson.hshExcludedGroups.Contains(strPreference) AndAlso
                       dictGroups(strPreference).Count < intMaxSize Then
                        dictGroups(strPreference).Add(objPerson)
                        objPerson.strAssignedGroup = strPreference
                        Exit For
                    End If
                Next
            End If
        Next

        ' Assign any remaining unassigned people to any available group that is not excluded and not full, round-robin
        Dim groupIndex As Integer = 1
        For Each objPerson As Person In lstPeople
            If String.IsNullOrEmpty(objPerson.strAssignedGroup) Then
                Dim assigned As Boolean = False
                For i As Integer = 1 To dictGroups.Count
                    Dim strGroupId = ((groupIndex - 1 + i - 1) Mod dictGroups.Count + 1).ToString()
                    If dictGroups(strGroupId).Count < intMaxSize AndAlso Not objPerson.hshExcludedGroups.Contains(strGroupId) Then
                        dictGroups(strGroupId).Add(objPerson)
                        objPerson.strAssignedGroup = strGroupId
                        groupIndex = (groupIndex Mod dictGroups.Count) + 1
                        assigned = True
                        Exit For
                    End If
                Next
                If Not assigned Then
                    ' If all groups are full or excluded, assign to the first available group
                    For Each strGroupId In validGroupIds
                        If dictGroups(strGroupId).Count < intMaxSize Then
                            dictGroups(strGroupId).Add(objPerson)
                            objPerson.strAssignedGroup = strGroupId
                            Exit For
                        End If
                    Next
                End If
            End If
        Next

        Return dictGroups
    End Function

    ' Loads people and their preferences from a CSV file.
    ' strFilePath: Path to the CSV file.
    ' Returns: List of Person objects populated from the file.
    Public Function LoadPeopleFromCsv(strFilePath As String) As List(Of Person)
        Dim lstPeople As New List(Of Person)()

        For Each strLine As String In File.ReadLines(strFilePath)
            If String.IsNullOrWhiteSpace(strLine) Then Continue For
            Dim arrFields = strLine.Split(","c)

            Dim objPerson As New Person()
            objPerson.strName = arrFields(0).Trim()

            ' Add up to 5 preferences from the CSV fields.
            For intI As Integer = 1 To 5
                If arrFields.Length > intI AndAlso Not String.IsNullOrWhiteSpace(arrFields(intI)) Then
                    objPerson.lstPreferences.Add(arrFields(intI).Trim())
                End If
            Next

            ' Optional inclusion group.
            If arrFields.Length > 6 AndAlso Not String.IsNullOrWhiteSpace(arrFields(6)) Then
                objPerson.strInclusion = arrFields(6).Trim()
            End If

            ' Optional exclusion group.
            If arrFields.Length > 7 AndAlso Not String.IsNullOrWhiteSpace(arrFields(7)) Then
                objPerson.strExclusion = arrFields(7).Trim()
            End If

            lstPeople.Add(objPerson)
        Next

        Return lstPeople
    End Function
End Class

' Represents a person with preferences, inclusion/exclusion constraints, and group assignment.
Public Class Person

    Public strName As String ' Person's name
    Public lstPreferences As List(Of String) ' List of preferred group IDs
    Public strInclusion As String ' Group ID the person must be included in (if any)
    Public strExclusion As String ' Group ID the person must be excluded from (if any)
    Public strAssignedGroup As String ' The group ID the person was assigned to
    Public hshExcludedGroups As HashSet(Of String) ' Set of group IDs the person cannot be assigned to

    ' Initializes a new person with empty preferences and exclusions.
    Public Sub New()
        lstPreferences = New List(Of String)()
        hshExcludedGroups = New HashSet(Of String)()
        strAssignedGroup = Nothing
    End Sub
End Class









