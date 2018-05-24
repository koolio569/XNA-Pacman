Public Class CAi

    Private _alive As Boolean

    Private _textureInfo As CTextureInfo

    Private _position As Vector2

    Private _comptence As Integer

    Public Enum comptence

        easy = 0

        medium = 1

        hard = 2

    End Enum

    Sub New(ByVal newPosition As Vector2, ByVal newComptence As Integer)

        _alive = True

        _position = newPosition

    End Sub

    Sub FollowPath(ByVal path As List(Of Integer), ByVal graph As CGraph)

        If path.Count >= 2 Then

            _position.X = graph.GetListOfNodes(path(1)).GetPosition.X

            _position.Y = graph.GetListOfNodes(path(1)).GetPosition.Y

        End If

    End Sub

    Sub Update(ByVal path As List(Of Integer), ByVal graph As CGraph)


        FollowPath(path, graph)



    End Sub

    Sub Draw()



    End Sub

End Class
