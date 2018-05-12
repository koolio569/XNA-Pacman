Public Class CMenuOption

    Private _gameStates As List(Of Integer)

    Private _keys As List(Of Keys)

    Sub New()

        _keys = New List(Of Keys) From {Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0}

        _gameStates = New List(Of Integer)

        For i = 0 To 9

            _gameStates.Add(-1)

        Next

    End Sub

    Sub SetGameSates(ByVal newStates As List(Of Integer))

        _gameStates = newStates

    End Sub

    Function GetGameStateFromKey(ByVal newKey As Keys) As Integer

        For i = 0 To _gameStates.Count - 1

            If newKey = _keys(i) Then

                Return _gameStates(i)

            End If

        Next

        Return -1

    End Function

End Class
