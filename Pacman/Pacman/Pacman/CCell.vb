Public Class CCell

    Private _position As Vector2

    Private _walls(3) As Boolean

    Private _symbol As Integer

    Private _visited As Boolean

    Private _item As String

    Sub New(ByVal newPosition As Vector2)

        _position = newPosition

        For w = 0 To 3

            _walls(w) = True

        Next

        _symbol = 0

        _visited = False

        _item = ""

    End Sub

    Function GetPositionX() As Integer

        Return CInt(_position.X)

    End Function

    Function GetPositionY() As Integer

        Return CInt(_position.Y)

    End Function

    Function GetWalls() As Boolean()

        Return _walls

    End Function

    Function GetSymbol() As Integer

        Return _symbol

    End Function

    Function GetVisited() As Boolean

        Return _visited

    End Function

    Function GetItem() As String

        Return _item

    End Function

    Sub SetWall(ByVal wallNumber As Integer, ByVal newState As Boolean)

        If (wallNumber < 4 And wallNumber > -1) Then

            _walls(wallNumber) = newState

        End If

    End Sub

    Sub SetVisited(ByVal newState As Boolean)

        _visited = newState

    End Sub

    Sub SetItem(ByVal newItem As String)

        _item = newItem

    End Sub

    Sub SetSymbol()

        Dim total As Byte = 0

        If _walls(0) = True Then

            total = CByte(total + 1)

        End If

        If _walls(1) = True Then

            total = CByte(total + 2)

        End If

        If _walls(2) = True Then

            total = CByte(total + 4)

        End If

        If _walls(3) = True Then

            total = CByte(total + 8)

        End If

        _symbol = total

    End Sub

    Sub Empty()

        For w = 0 To 3

            _walls(w) = False

        Next

    End Sub

End Class
