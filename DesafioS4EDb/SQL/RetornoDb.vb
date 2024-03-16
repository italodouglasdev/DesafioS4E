Namespace SQL

    Public Class RetornoDb

        Public Sub New()
        End Sub

        Public Sub New(ByVal _SQLSucesso As Boolean, ByVal _SQLMensagem As String)
            Sucesso = _SQLSucesso
            Mensagem = _SQLMensagem
        End Sub


        Public Property Sucesso As Boolean

        Public Property Mensagem As String

    End Class

End Namespace