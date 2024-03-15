
Public Class Retorno
    Public Sub New()
    End Sub

    Public Sub New(sucesso As Boolean, mensagem As String)
        Me.Sucesso = sucesso
        Me.Mensagem = mensagem
    End Sub

    Property Sucesso As Boolean
    Property Mensagem As String

End Class
