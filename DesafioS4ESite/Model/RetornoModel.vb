Public Class RetornoModel
    Public Sub New()
    End Sub

    Public Sub New(ByVal Sucesso As Boolean, ByVal Mensagem As String)
        Me.Sucesso = Sucesso
        Me.Mensagem = Mensagem
    End Sub


    Public Property Sucesso As Boolean

    Public Property Mensagem As String

End Class
