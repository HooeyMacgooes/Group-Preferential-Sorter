Imports System.IO

Public Class GroupPreferentialSorter

    Public Function PreferentialSort(people As List(Of Person), groups As Dictionary(Of String, List(Of Person)), maxSize As Integer) As Dictionary(Of String, List(Of Person))
        For Each person As Person In people
            If Not String.IsNullOrEmpty(person.Inclusion) Then
                Dim groupId As String = person.Inclusion
                If groups.ContainsKey(groupId) AndAlso groups(groupId).Count < maxSize Then
                    groups(groupId).Add(person)
                    person.AssignedGroup = groupId
                Else

                End If
            End If
        Next

        For Each person As Person In people
            If Not String.IsNullOrEmpty(person.Exclusion) Then
                person.ExcludedGroups.Add(person.Exclusion)
            End If
        Next

        For Each person As Person In people
            If String.IsNullOrEmpty(person.AssignedGroup) Then
                For Each Preference As String In person.Preferences
                    If Not person.ExcludedGroups.Contains(Preference) AndAlso groups.ContainsKey(Preference) AndAlso groups(Preference).Count < maxSize Then
                        groups(Preference).Add(person)
                        person.AssignedGroup = Preference
                        Exit For
                    End If
                Next
            End If
        Next

        For Each person As Person In people
            If String.IsNullOrEmpty(person.AssignedGroup) Then
                For Each kvp As KeyValuePair(Of String, List(Of Person)) In groups
                    Dim groupId As String = kvp.Key
                    Dim memberList As List(Of Person) = kvp.Value

                    If memberList.Count < maxSize AndAlso Not person.ExcludedGroups.Contains(groupId) Then
                        memberList.Add(person)
                        person.AssignedGroup = groupId
                        Exit For
                    End If
                Next
            End If
        Next

        Return groups
    End Function

    Public Function LoadPeopleFromCsv(filePath As String) As List(Of Person)
        Dim people As New List(Of Person)()

        For Each line As String In File.ReadLines(filePath)
            If String.IsNullOrWhiteSpace(line) Then Continue For
            Dim fields = line.Split(","c)

            If fields.Length < 6 Then Continue For

            Dim person As New Person()
            person.Name = fields(0).Trim()

            For i As Integer = 1 To 5
                If fields.Length > i AndAlso Not String.IsNullOrWhiteSpace(fields(i)) Then
                    person.Preferences.Add(fields(i).Trim())
                End If
            Next

            If fields.Length > 6 AndAlso Not String.IsNullOrWhiteSpace(fields(6)) Then
                person.Inclusion = fields(6).Trim()
            End If

            If fields.Length > 7 AndAlso Not String.IsNullOrWhiteSpace(fields(7)) Then
                person.Exclusion = fields(7).Trim()
            End If

            people.Add(person)
        Next

        Return people
    End Function

End Class

Public Class Person

    Public Name As String
    Public Preferences As List(Of String)
    Public Inclusion As String
    Public Exclusion As String
    Public AssignedGroup As String
    Public ExcludedGroups As HashSet(Of String)

    Public Sub New()
        Preferences = New List(Of String)()
        ExcludedGroups = New HashSet(Of String)()
        AssignedGroup = Nothing
    End Sub
End Class



