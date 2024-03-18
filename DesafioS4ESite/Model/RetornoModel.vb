''' <summary>
''' Classe reponsável retornar o Status das validações
''' </summary>
Public Class RetornoModel

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instacia um novo objeto
    ''' </summary>
    ''' <param name="Sucesso"></param>
    ''' <param name="Mensagem"></param>
    Public Sub New(ByVal Sucesso As Boolean, ByVal Mensagem As String)
        Me.Sucesso = Sucesso
        Me.Mensagem = Mensagem
    End Sub

    ''' <summary>
    ''' Propriedade responsável por armazenar o Status das validações
    ''' </summary>
    ''' <returns>[True] Executado com Sucesso | [False] Erro na Execução</returns>
    Public Property Sucesso As Boolean

    ''' <summary>
    ''' Propriedade responsável por armazenar os detalhes do Staus das validações
    ''' </summary>
    ''' <returns> Mensagem de retorno da excução da Consulta SQL </returns>
    Public Property Mensagem As String

End Class
