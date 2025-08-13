Imports System.IO
Imports System.Windows.Forms

' Main form for the group sorting application.
' Handles importing, sorting, exporting, UI scaling, and fun/secret features.
Public Class Form1
    ' List of all people loaded from the CSV file.
    Private lstPeople As List(Of Person)
    ' Sorter instance to handle group assignment logic.
    Private sorter As New GroupPreferentialSorter()
    ' Flag to indicate if the current list has been sorted.
    Private isSorted As Boolean = False

    ' For scaling controls when resizing the form
    Private originalFormSize As Size
    Private originalControlBounds As New Dictionary(Of Control, Rectangle)
    Private originalFontSizes As New Dictionary(Of Control, Single)

    ' Form load event: disables sort/export buttons, sets up event handlers, and stores original control sizes for scaling.
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSort.Enabled = False
        btnExport.Enabled = False

        ' Store original form and control sizes for scaling
        originalFormSize = Me.ClientSize
        StoreOriginalControlBounds(Me.Controls)
    End Sub

    ' Event handler for group count value change.
    Private Sub numUpDown_ValueChanged(sender As Object, e As EventArgs)
        ValidateSortButton()
        ValidateExportButton()
    End Sub

    ' Enables or disables the Sort button based on whether people are loaded and group count is valid.
    Private Sub ValidateSortButton()
        btnSort.Enabled = lstPeople IsNot Nothing AndAlso
                          lstPeople.Count > 0 AndAlso
                          numUpDown.Value > 0
    End Sub

    ' Enables or disables the Export button based on whether sorting has occurred and group count is valid.
    Private Sub ValidateExportButton()
        btnExport.Enabled = isSorted AndAlso numUpDown.Value > 0
    End Sub

    ' Handles importing people from a CSV file.
    ' Populates lstPeople and updates the UI with a preview of names and preferences.
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Using dlgOpen As New OpenFileDialog()
            ' Set filter and title for the file dialog.
            dlgOpen.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlgOpen.Title = "Select People CSV File"
            If dlgOpen.ShowDialog() = DialogResult.OK Then
                ' Load people from CSV file.
                lstPeople = sorter.LoadPeopleFromCsv(dlgOpen.FileName)
                MessageBox.Show($"Loaded {lstPeople.Count} people from file.", "Import Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                isSorted = False
                numUpDown.Maximum = If(lstPeople IsNot Nothing, lstPeople.Count, 1)
                numUpDown.Value = If(lstPeople IsNot Nothing AndAlso lstPeople.Count > 0, 1, 0)
                ValidateSortButton()
                ValidateExportButton()

                ' Display a preview of each person's name and their preferences.
                Dim sbPreview As New System.Text.StringBuilder()
                For Each p In lstPeople
                    sbPreview.AppendLine(p.strName)
                    If p.lstPreferences.Count > 0 Then
                        For Each pref In p.lstPreferences
                            sbPreview.AppendLine("    Preference: " & pref)
                        Next
                    End If
                    sbPreview.AppendLine()
                Next
                txtbxDisplay.Text = sbPreview.ToString().TrimEnd()
            End If
        End Using
    End Sub


    ' Handles sorting people into groups based on their preferences.
    ' Updates the display with the sorted groups.
    Private Sub btnSort_Click(sender As Object, e As EventArgs) Handles btnSort.Click
        If lstPeople Is Nothing OrElse lstPeople.Count = 0 Then
            MessageBox.Show("No people loaded to sort.", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Assure the group count is valid.
        Dim groupCount As Integer
        If Not Integer.TryParse(numUpDown.Value, groupCount) OrElse groupCount <= 0 Then
            MessageBox.Show("Invalid group amount.", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Initialize group dictionary with empty lists for each group.
        Dim dictGroups As New Dictionary(Of String, List(Of Person))()
        For i As Integer = 1 To groupCount
            dictGroups(i.ToString()) = New List(Of Person)()
        Next

        ' Clear previous group assignments.
        For Each p In lstPeople
            p.strAssignedGroup = Nothing
        Next

        ' Perform the sorting.
        sorter.PreferentialSort(lstPeople, dictGroups, Integer.MaxValue)

        ' Build and display the sorted groups in the display textbox.
        Dim sb As New System.Text.StringBuilder()
        For i As Integer = 1 To groupCount
            Dim groupId = i.ToString()
            sb.AppendLine($"Group {groupId}")
            Dim members = dictGroups(groupId).Select(Function(p) p.strName).ToList()
            If members.Count > 0 Then
                sb.AppendLine(String.Join(Environment.NewLine, members))
            Else
                sb.AppendLine("(No members)")
            End If
            sb.AppendLine()
        Next
        txtbxDisplay.Text = sb.ToString().TrimEnd()

        isSorted = True
        ValidateExportButton()
        MessageBox.Show("Sorting complete. Groups have been assigned.", "Sort", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' Handles exporting the sorted groups to a CSV file.
    ' Each row contains the group number and the names of its members.
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If lstPeople Is Nothing OrElse lstPeople.Count = 0 Then
            MessageBox.Show("No people loaded to export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Check if the groups are sorted before exporting.
        If Not isSorted Then
            MessageBox.Show("Please sort the groups before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' Assure the group count is valid.
        Dim groupCount As Integer
        If Not Integer.TryParse(numUpDown.Value, groupCount) OrElse groupCount <= 0 Then
            MessageBox.Show("Invalid group amount.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Initialize group dictionary for export.
        Dim dictGroups As New Dictionary(Of String, List(Of Person))()
        For i As Integer = 1 To groupCount
            dictGroups(i.ToString()) = New List(Of Person)()
        Next

        ' Clear previous group assignments.
        For Each p In lstPeople
            p.strAssignedGroup = Nothing
        Next

        ' Perform the sorting again to ensure export matches the display.
        sorter.PreferentialSort(lstPeople, dictGroups, Integer.MaxValue)

        ' Determine the maximum group size for CSV formatting.
        Dim maxGroupSize As Integer = dictGroups.Values.Max(Function(g) g.Count)

        Using dlgSave As New SaveFileDialog()
            ' Set filter and title for the save file dialog.
            dlgSave.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
            dlgSave.Title = "Select File Path To Send"
            dlgSave.FileName = "SortedPeople.csv"
            If dlgSave.ShowDialog() = DialogResult.OK Then
                Try
                    Using sw As New StreamWriter(dlgSave.FileName, False)
                        ' Write CSV header.
                        Dim header As String = "Group Number"
                        For i As Integer = 1 To maxGroupSize
                            header &= $",Person {i}"
                        Next
                        sw.WriteLine(header)

                        ' Write each group and its members.
                        For i As Integer = 1 To groupCount
                            Dim groupId = i.ToString()
                            Dim members = dictGroups(groupId).Select(Function(p) p.strName).ToList()
                            ' Pad with empty strings to ensure all rows have the same number of columns.
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

    ' Shows a help message box with instructions for using the application.
    Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        MessageBox.Show(
        "Instructions:" & Environment.NewLine &
        "- Set the number of groups using the up/down arrows on the left." & Environment.NewLine &
        "- Import your CSV file using the 'Import' button. The file should follow the required format." & Environment.NewLine &
        "- Click 'Sort' to assign people to groups based on their preferences." & Environment.NewLine &
        "- Click 'Export' to save the sorted groups to a CSV file." & Environment.NewLine &
        Environment.NewLine &
        "Ensure all names and preferences in your CSV are consistent." & Environment.NewLine &
        "For more assistance, questions or requests please comment under the github page at (https://github.com/HooeyMacgooes/Group-Preferential-Sorter)",
        "Help",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    )
    End Sub

    ' Shrinks all controls and the form itself for a "fun" effect when the Fun button is clicked.
    Private Sub btnFun_Click(sender As Object, e As EventArgs) Handles btnFun.Click
        Dim shrinkFactor As Double = 0.01
        ShrinkControls(Me.Controls, shrinkFactor)
        Me.Width = CInt(Me.Width * shrinkFactor)
        Me.Height = CInt(Me.Height * shrinkFactor)
    End Sub

    ' Recursively shrinks all controls in a control collection by a given factor.
    Private Sub ShrinkControls(ctrls As Control.ControlCollection, factor As Double)
        For Each ctrl As Control In ctrls
            ctrl.Width = Math.Max(10, CInt(ctrl.Width * factor))
            ctrl.Height = Math.Max(10, CInt(ctrl.Height * factor))
            ctrl.Left = CInt(ctrl.Left * factor)
            ctrl.Top = CInt(ctrl.Top * factor)
            If ctrl.Font IsNot Nothing Then
                Dim newSize As Single = Math.Max(6, ctrl.Font.Size * CSng(factor))
                ctrl.Font = New Font(ctrl.Font.FontFamily, newSize, ctrl.Font.Style)
            End If
            If ctrl.HasChildren Then
                ShrinkControls(ctrl.Controls, factor)
            End If
        Next
    End Sub

    ' Stores the original size, position, and font size of all controls for scaling.
    Private Sub StoreOriginalControlBounds(ctrls As Control.ControlCollection)
        For Each ctrl As Control In ctrls
            originalControlBounds(ctrl) = ctrl.Bounds
            originalFontSizes(ctrl) = ctrl.Font.Size
            If ctrl.HasChildren Then
                StoreOriginalControlBounds(ctrl.Controls)
            End If
        Next
    End Sub

    ' Scales all controls and fonts based on the new form size.
    Private Sub ScaleControls(ctrls As Control.ControlCollection, scaleX As Double, scaleY As Double)
        For Each ctrl As Control In ctrls
            If originalControlBounds.ContainsKey(ctrl) Then
                Dim orig = originalControlBounds(ctrl)
                ctrl.Left = CInt(orig.Left * scaleX)
                ctrl.Top = CInt(orig.Top * scaleY)
                ctrl.Width = Math.Max(10, CInt(orig.Width * scaleX))
                ctrl.Height = Math.Max(10, CInt(orig.Height * scaleY))
            End If
            If originalFontSizes.ContainsKey(ctrl) Then
                Dim newFontSize As Single = Math.Max(6, CSng(originalFontSizes(ctrl) * Math.Min(scaleX, scaleY)))
                ctrl.Font = New Font(ctrl.Font.FontFamily, newFontSize, ctrl.Font.Style)
            End If
            If ctrl.HasChildren Then
                ScaleControls(ctrl.Controls, scaleX, scaleY)
            End If
        Next
    End Sub

    ' Handles the Resize event to scale controls and fonts proportionally.
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If originalFormSize.Width = 0 OrElse originalFormSize.Height = 0 Then Return
        Dim scaleX As Double = Me.ClientSize.Width / originalFormSize.Width
        Dim scaleY As Double = Me.ClientSize.Height / originalFormSize.Height
        ScaleControls(Me.Controls, scaleX, scaleY)
    End Sub

End Class

' Handles sorting logic for assigning people to groups based on their preferences.
Public Class GroupPreferentialSorter

    ' Assigns people to groups based on their preferences.
    ' 1. Tries to assign each person to their preferred group (if not full).
    ' 2. Assigns any unassigned people to groups in round-robin fashion.
    ' lstPeople: List of people to assign.
    ' dictGroups: Dictionary of group ID to list of people.
    ' intMaxSize: Maximum allowed size for each group.
    Public Function PreferentialSort(lstPeople As List(Of Person), dictGroups As Dictionary(Of String, List(Of Person)), intMaxSize As Integer) As Dictionary(Of String, List(Of Person))
        Dim validGroupIds = New HashSet(Of String)(dictGroups.Keys)

        ' First, try to assign each person to their preferred group.
        For Each objPerson As Person In lstPeople
            objPerson.strAssignedGroup = Nothing
            For Each strPreference As String In objPerson.lstPreferences
                If validGroupIds.Contains(strPreference) AndAlso dictGroups(strPreference).Count < intMaxSize Then
                    dictGroups(strPreference).Add(objPerson)
                    objPerson.strAssignedGroup = strPreference
                    Exit For
                End If
            Next
        Next

        ' Next, assign any unassigned people to any available group (round-robin).
        Dim groupIds = dictGroups.Keys.ToList()
        Dim groupIndex As Integer = 0
        For Each objPerson As Person In lstPeople
            If String.IsNullOrEmpty(objPerson.strAssignedGroup) Then
                For i As Integer = 0 To groupIds.Count - 1
                    Dim idx = (groupIndex + i) Mod groupIds.Count
                    Dim strGroupId = groupIds(idx)
                    If dictGroups(strGroupId).Count < intMaxSize Then
                        dictGroups(strGroupId).Add(objPerson)
                        objPerson.strAssignedGroup = strGroupId
                        groupIndex = (idx + 1) Mod groupIds.Count
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
        ' Check if the file exists before reading.
        For Each strLine As String In File.ReadLines(strFilePath)
            If String.IsNullOrWhiteSpace(strLine) Then Continue For
            Dim arrFields = strLine.Split(","c)

            Dim objPerson As New Person()
            objPerson.strName = arrFields(0).Trim()

            ' Add up to 5 preferences from the CSV fields (columns 1-5).
            For intI As Integer = 1 To 5
                If arrFields.Length > intI AndAlso Not String.IsNullOrWhiteSpace(arrFields(intI)) Then
                    objPerson.lstPreferences.Add(arrFields(intI).Trim())
                End If
            Next

            lstPeople.Add(objPerson)
        Next

        Return lstPeople
    End Function
End Class

' Represents a person with a name, a list of group preferences, and their assigned group.
Public Class Person
    Public strName As String ' Person's name
    Public lstPreferences As List(Of String) ' List of preferred group IDs
    Public strAssignedGroup As String ' The group ID the person was assigned to

    ' Initializes a new person with empty preferences.
    Public Sub New()
        lstPreferences = New List(Of String)()
        strAssignedGroup = Nothing
    End Sub
End Class