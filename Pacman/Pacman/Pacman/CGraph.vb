Public Class CGraph

    Private _name As String

    Private _listOfNodes As List(Of CNode)

    Private _Ppath As New List(Of Vector2)

    Protected _PstackOfNodes As New Stack(Of CNode)

    Protected _Pfound As Boolean

    Sub New(ByVal newName As String, ByVal newListOfNodes As List(Of CNode))

        _name = newName

        _listOfNodes = newListOfNodes

    End Sub

    Function GetName() As String

        Return _name

    End Function

    Function GetListOfNodes() As List(Of CNode)

        Return _listOfNodes

    End Function

    Function GetPath(ByVal startPoint As Integer, ByVal endPoint As Integer) As List(Of Vector2)

        _Pfound = False

        _Ppath.Clear()

        GetListOfNodes(startPoint).SetVisited(True)

        DepthFirstSearch(GetListOfNodes(startPoint), GetListOfNodes(endPoint))

        _Ppath.Reverse()

        Return _Ppath

    End Function

    Private Sub DepthFirstSearch(ByVal startPoint As CNode, ByVal endPoint As CNode)

        Dim neighbourNode As CNode = GetNeighbourgh(startPoint)

        If startPoint.Getidentifier = endPoint.Getidentifier Then

            _Pfound = True

        End If

        If neighbourNode.Getidentifier = -1 Or _Pfound = True Then

            If _Pfound = True Then

                _Ppath.Add(startPoint.GetPosition)

            End If

            If _PstackOfNodes.Count > 0 Then

                DepthFirstSearch(_PstackOfNodes.Pop(), endPoint)

            End If

        Else

            GetListOfNodes(neighbourNode.Getidentifier).SetVisited(True)

            _PstackOfNodes.Push(startPoint)

            DepthFirstSearch(neighbourNode, endPoint)

        End If

    End Sub

    Private Function GetNeighbourgh(ByVal currentNode As CNode) As CNode

        Dim count As Integer = -1

        For Each CNode In currentNode.GetListOfNeighbours

            count = count + 1

            If GetListOfNodes(currentNode.GetListOfNeighbours(count)).GetVisited = False Then

                Return GetListOfNodes(currentNode.GetListOfNeighbours(count))

            End If

        Next

        Return New CNode(-1, New Vector2(-1, -1), New List(Of Integer))

    End Function

End Class
