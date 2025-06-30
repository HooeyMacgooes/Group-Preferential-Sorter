
Imports System.IO
Imports System.Windows.Forms

Public Class Form1

    Private lstPeople As List(Of Person)
    Private sorter As New GroupPreferentialSorter()

    ' Handles the Import button click event
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Using dlgOpen As New OpenFileDialog()
            dlgOpen.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlgOpen.Title = "Select People CSV File"
            If dlgOpen.ShowDialog() = DialogResult.OK Then
                lstPeople = sorter.LoadPeopleFromCsv(dlgOpen.FileName)
                MessageBox.Show("Loaded " & lstPeople.Count & " people from file.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Now you can use lstPeople and update your UI as needed
            End If
        End Using
    End Sub

End Class




' This class handles sorting people into groups based on their preferences, inclusion, and exclusion constraints.
Public Class GroupPreferentialSorter



    ' Assigns people to groups based on inclusion, exclusion, and preferences.
    ' lstPeople: List of people to assign.
    ' dictGroups: Dictionary of group IDs to lists of assigned people.
    ' intMaxSize: Maximum allowed size for each group.
    ' Returns: Updated dictionary with people assigned to groups.
    Public Function PreferentialSort(lstPeople As List(Of Person), dictGroups As Dictionary(Of String, List(Of Person)), intMaxSize As Integer) As Dictionary(Of String, List(Of Person))
        ' First, assign people to their inclusion group if specified and space is available.
        For Each objPerson As Person In lstPeople
            If Not String.IsNullOrEmpty(objPerson.strInclusion) Then
                Dim strGroupId As String = objPerson.strInclusion
                If dictGroups.ContainsKey(strGroupId) AndAlso dictGroups(strGroupId).Count < intMaxSize Then
                    dictGroups(strGroupId).Add(objPerson)
                    objPerson.strAssignedGroup = strGroupId
                Else
                    ' Could handle overflow or conflicts here if needed.
                End If
            End If
        Next

        ' Add exclusion group to each person's exclusion set if specified.
        For Each objPerson As Person In lstPeople
            If Not String.IsNullOrEmpty(objPerson.strExclusion) Then
                objPerson.hshExcludedGroups.Add(objPerson.strExclusion)
            End If
        Next

        ' Assign people to their preferred groups, skipping excluded groups and full groups.
        For Each objPerson As Person In lstPeople
            If String.IsNullOrEmpty(objPerson.strAssignedGroup) Then
                For Each strPreference As String In objPerson.lstPreferences
                    If Not objPerson.hshExcludedGroups.Contains(strPreference) AndAlso dictGroups.ContainsKey(strPreference) AndAlso dictGroups(strPreference).Count < intMaxSize Then
                        dictGroups(strPreference).Add(objPerson)
                        objPerson.strAssignedGroup = strPreference
                        Exit For
                    End If
                Next
            End If
        Next

        ' Assign any remaining unassigned people to any available group that is not excluded and not full.
        For Each objPerson As Person In lstPeople
            If String.IsNullOrEmpty(objPerson.strAssignedGroup) Then
                For Each kvpGroup As KeyValuePair(Of String, List(Of Person)) In dictGroups
                    Dim strGroupId As String = kvpGroup.Key
                    Dim lstMemberList As List(Of Person) = kvpGroup.Value

                    If lstMemberList.Count < intMaxSize AndAlso Not objPerson.hshExcludedGroups.Contains(strGroupId) Then
                        lstMemberList.Add(objPerson)
                        objPerson.strAssignedGroup = strGroupId
                        Exit For
                    End If
                Next
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







