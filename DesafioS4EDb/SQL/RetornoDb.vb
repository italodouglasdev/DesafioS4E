
Namespace SQL

    ''' <summary>
    ''' Classe reponsável retornar o status das Consultas SQL
    ''' </summary>
    Public Class RetornoDb

        Public Sub New()
        End Sub

        Public Sub New(ByVal _SQLSucesso As Boolean, ByVal _SQLMensagem As String)
            Sucesso = _SQLSucesso
            Mensagem = _SQLMensagem
        End Sub


        ''' <summary>
        ''' Propriedade responsável por armazenar o Status da Consulta SQL
        ''' </summary>
        ''' <returns>[True] Executado com Sucesso | [Falas] Erro na Execução</returns>
        Public Property Sucesso As Boolean

        ''' <summary>
        ''' Propriedade responsável por armazenar os detalhes do Staus da Consulta SQL
        ''' </summary>
        ''' <returns> Mensagem de retorno da excução da Consulta SQL </returns>
        Public Property Mensagem As String

    End Class

End Namespace