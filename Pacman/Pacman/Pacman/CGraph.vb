Public Class CGraph

    Private _name As String

    Private _listOfNodes As List(Of CNode)

    Private _Ppath As New List(Of Vector2)

    Protected _PstackOfNodes As New Stack(Of CNode)

    Protected _PqueueOfNodes As New Queue(Of CNode)

    Protected _Pparents As New List(Of Integer)

    Protected _Pfound As Boolean

    Public Enum PathType

        DepthFirst = 0

        BreadthFirst = 1

        Dijkstra = 2

    End Enum

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

    Function GetPath(ByVal startPoint As Integer, ByVal endPoint As Integer, ByVal type As Integer) As List(Of Vector2)

        _Pfound = False

        _Ppath.Clear()

        SetAllNodesVisited(False)

        Select Case type

            Case PathType.DepthFirst

                GetListOfNodes(startPoint).SetVisited(True)

                _PstackOfNodes.Clear()

                DepthFirstSearch(GetListOfNodes(startPoint), GetListOfNodes(endPoint))

                _Ppath.Reverse()

            Case PathType.BreadthFirst

                GetListOfNodes(startPoint).SetVisited(True)

                _PqueueOfNodes.Clear()

                _Pparents.Clear()

                For i = 0 To _listOfNodes.Count - 1

                    _Pparents(i) = -1

                Next

                BreathFirstSearch(GetListOfNodes(startPoint))

                Dim parentPosition As Integer = endPoint

                While parentPosition <> endPoint

                    _Ppath.Add(_listOfNodes(_Pparents(parentPosition)).GetPosition)

                    parentPosition = _Pparents(parentPosition)

                End While

                _Ppath.Reverse()

            Case Else


        End Select

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

    Private Sub BreathFirstSearch(ByRef startPoint As CNode)

        _PqueueOfNodes.Enqueue(startPoint)

        Dim currentNode As CNode

        While _PqueueOfNodes.Count > 0

            currentNode = _PqueueOfNodes.Dequeue

            For i = 0 To currentNode.GetListOfNeighbours.Count - 1

                If _listOfNodes(currentNode.GetListOfNeighbours(i)).GetVisited = False Then

                    _PqueueOfNodes.Enqueue(_listOfNodes(currentNode.GetListOfNeighbours(i)))

                    _listOfNodes(currentNode.GetListOfNeighbours(i)).SetVisited(True)

                    _Pparents(i) = currentNode.Getidentifier

                End If

            Next

        End While

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

    Private Sub SetAllNodesVisited(ByVal newState As Boolean)

        For i = 0 To _listOfNodes.Count - 1

            _listOfNodes(i).SetVisited(newState)

        Next

    End Sub

End Class
