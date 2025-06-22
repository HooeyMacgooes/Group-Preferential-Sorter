Public Class Form1

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

End Class

Public Class Person

    Public Preferences As List(Of String)
    Public Inclusion As String
    Public Exclusion As String
    Public AssignedGroup As String
    Public ExcludedGroups As HashSet(Of String)

    Public Sub New()
        Preferences = New List(Of String)()
        AssignedGroup = Nothing
    End Sub
End Class



