Public Class CGraph

    Private _name As String

    Private _listOfNodes As List(Of CNode)

    Private _Ppath As New List(Of Vector2)

    Protected _PstackOfNodes As New Stack(Of CNode)



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

                Dim pParents As New List(Of Integer)

                For i = 0 To _listOfNodes.Count - 1

                    pParents.Add(New Integer)

                Next

                GetListOfNodes(startPoint).SetVisited(True)

                BreathFirstSearch(GetListOfNodes(startPoint), pParents)

                Dim parentPosition As Integer = endPoint

                While parentPosition <> endPoint

                    _Ppath.Add(_listOfNodes(pParents(parentPosition)).GetPosition)

                    parentPosition = pParents(parentPosition)

                End While

                _Ppath.Reverse()

            Case Else


        End Select

        Return _Ppath

    End Function

    Private Sub DepthFirstSearch(ByVal startPoint As CNode, ByVal endPoint As CNode)

        Dim neighbourNode As CNode = GetNeighbourgh(startPoint)

        If startPoint.GetIdentifier = endPoint.GetIdentifier Then

            _Pfound = True

        End If

        If neighbourNode.GetIdentifier = -1 Or _Pfound = True Then

            If _Pfound = True Then

                _Ppath.Add(startPoint.GetPosition)

            End If

            If _PstackOfNodes.Count > 0 Then

                DepthFirstSearch(_PstackOfNodes.Pop(), endPoint)

            End If

        Else

            GetListOfNodes(neighbourNode.GetIdentifier).SetVisited(True)

            _PstackOfNodes.Push(startPoint)

            DepthFirstSearch(neighbourNode, endPoint)

        End If

    End Sub

    Private Sub BreathFirstSearch(ByRef startPoint As CNode, ByRef pParents As List(Of Integer))

        Dim _PqueueOfNodes As New Queue(Of CNode)

        _PqueueOfNodes.Enqueue(startPoint)

        Dim currentNode As CNode

        While _PqueueOfNodes.Count > 0

            currentNode = _PqueueOfNodes.Dequeue

            For i = 0 To currentNode.GetListOfNeighbours.Count - 1

                If _listOfNodes(currentNode.GetListOfNeighbours(i)).GetVisited = False Then

                    _PqueueOfNodes.Enqueue(_listOfNodes(currentNode.GetListOfNeighbours(i)))

                    _listOfNodes(currentNode.GetListOfNeighbours(i)).SetVisited(True)

                    pParents(i) = currentNode.GetIdentifier

                End If

            Next

        End While

    End Sub

    Sub DijsktraSearch(ByRef startPoint As CNode, ByRef endPoint As CNode)

        Dim _PqueueOfNodes As New Queue(Of CNode)

        For i = 0 To _listOfNodes.Count - 1

            _listOfNodes(i).SetDistance(CInt(2 ^ 30))

            _listOfNodes(i).SetPreviousNodeIdentifier(-1)

            _PqueueOfNodes.Enqueue(_listOfNodes(i))

        Next

        Dim currentNode As CNode

        Dim listOfNodes As List(Of CNode) = _listOfNodes

        listOfNodes(startPoint.GetIdentifier).SetDistance(0)

        While _PqueueOfNodes.Count > 0

            _PqueueOfNodes.Clear()

            listOfNodes = listOfNodes.OrderBy(Function(x) x.GetDistance).ToList()

            For i = 0 To listOfNodes.Count - 1

                _PqueueOfNodes.Enqueue(listOfNodes(i))

            Next

            Dim alt As Integer

            currentNode = _PqueueOfNodes.Dequeue

            If currentNode.GetIdentifier = endPoint.GetIdentifier Then

                Dim currentPathNode As CNode = endPoint

                While currentPathNode.GetIdentifier <> -1

                    _Ppath.Add(currentPathNode.GetPosition)

                    currentPathNode = _listOfNodes(currentPathNode.GetPreviousNodeIdentifier)

                End While

                _Ppath.Add(currentPathNode.GetPosition)

            Else

                For i = 0 To currentNode.GetListOfNeighbours.Count - 1

                    alt = currentNode.GetDistance + DistanceBetween(currentNode, listOfNodes(currentNode.GetListOfNeighbours(i)))

                    If alt < listOfNodes(currentNode.GetListOfNeighbours(i)).GetDistance Then

                        listOfNodes(currentNode.GetListOfNeighbours(i)).SetDistance(alt)

                        listOfNodes(currentNode.GetListOfNeighbours(i)).SetPreviousNodeIdentifier(currentNode.GetIdentifier)

                    End If

                Next

            End If

        End While

    End Sub

    Function DistanceBetween(ByVal node1 As CNode, ByVal node2 As CNode) As Integer

        Return CInt(Math.Sqrt((node1.GetPosition.X - node2.GetPosition.X) ^ 2 + (node1.GetPosition.Y - node2.GetPosition.Y) ^ 2))

    End Function

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
