Public Class CAi

    Private _alive As Boolean

    Private _textureInfo As CTextureInfo

    Private _position As Vector2

    Sub New(ByVal newPosition As Vector2)

        _alive = True

        _position = newPosition

    End Sub

    Sub FollowPath(ByVal path As List(Of Integer), ByVal graph As CGraph)



    End Sub

    Sub Update()



    End Sub

    Sub Draw()



    End Sub

End Class
