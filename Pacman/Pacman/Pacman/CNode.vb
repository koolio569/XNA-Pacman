Public Class CNode

    Private _identifier As Integer

    Private _position As Vector2

    Private _listOfNeighbours As New List(Of Integer)

    Private _visited As Boolean

    Sub New(ByVal newIdentifier As Integer, ByVal newPosition As Vector2, ByVal newListOfNeighbours As List(Of Integer))

        _identifier = newIdentifier

        _position = newPosition

        _listOfNeighbours = newListOfNeighbours

        _visited = False

    End Sub

    Function Getidentifier() As Integer

        Return _identifier

    End Function

    Function GetPosition() As Vector2

        Return _position

    End Function

    Function GetListOfNeighbours() As List(Of Integer)

        Return _listOfNeighbours

    End Function

    Function GetVisited() As Boolean

        Return _visited

    End Function

    Sub SetIdentifier(ByVal newIdentifier As Integer)

        _identifier = newIdentifier

    End Sub

    Sub SetPosition(ByVal newPosition As Vector2)

        _position = newPosition

    End Sub

    Sub SetListOfNeighbours(ByVal newListOfNeighbours As List(Of Integer))

        _listOfNeighbours = newListOfNeighbours

    End Sub

    Sub SetVisited(ByVal newState As Boolean)

        _visited = newState

    End Sub

End Class
