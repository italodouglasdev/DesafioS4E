Imports DesafioS4ESite.Enumeradores


''' <summary>
''' Classe responsável pelo gerenciamento da relação entre Associados e Empresas
''' </summary>
Public Class EmpresaAssociadoModel

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto
    ''' </summary>
    ''' <param name="idEmpresa">Id da Empresa</param>
    ''' <param name="idAssociado">Id do Associado</param>
    ''' <param name="instrucao">Instrução para monstagem do script SQL</param>
    Public Sub New(idEmpresa As String, idAssociado As String, instrucao As EnumInstrucao)
        Me.IdEmpresa = idEmpresa
        Me.IdAssociado = idAssociado
        Me.Instrucao = instrucao
    End Sub


    ''' <summary>
    ''' Id da Empresa
    ''' </summary>
    ''' <returns></returns>
    Property IdEmpresa As Integer

    ''' <summary>
    ''' Id do Associado
    ''' </summary>
    ''' <returns></returns>
    Property IdAssociado As Integer

    ''' <summary>
    ''' Essa propriedade não faz parte da tabela, posteriormente ela é desconsiderada no momento de gerar os Scripts
    ''' </summary>
    ''' <returns></returns>
    Property Instrucao As EnumInstrucao


    ''' <summary>
    ''' Realiza a conversão do objeto de relação entre Associados e Empresas da camada de banco de dados para um objeto da camada de negócio
    ''' </summary>
    ''' <param name="empresaAssociadoDb">Objeto EmpresaAssociado do CRUD</param>
    ''' <returns>EmpresaAssociadoModel</returns>
    Private Shared Function ConverterParaModelo(empresaAssociadoDb As DesafioS4EDb.EmpresasAssociados) As EmpresaAssociadoModel
        Return New EmpresaAssociadoModel(empresaAssociadoDb.IdEmpresa, empresaAssociadoDb.IdAssociado, empresaAssociadoDb.Instrucao)
    End Function

    ''' <summary>
    ''' Realiza a conversão do objeto de relação entre Associados e Empresas da camada de banco de dados para um objeto da camada de negócio, com retorno do banco da consulta SQL
    ''' </summary>
    ''' <param name="empresaAssociadoDb">Objeto EmpresaAssociado do CRUD</param>
    ''' <param name="RetornoDb">Objeto com o retorno da excução da consulta no banco de dados</param>
    ''' <returns></returns>
    Private Shared Function ConverterParaModelo(empresaAssociadoDb As DesafioS4EDb.EmpresasAssociados, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (EmpresaAssociado As EmpresaAssociadoModel, Retorno As RetornoModel)
        Return (New EmpresaAssociadoModel(empresaAssociadoDb.IdEmpresa, empresaAssociadoDb.IdAssociado, empresaAssociadoDb.Instrucao), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function


    ''' <summary>
    ''' Realiza a conversão de uma lista objetos de relação entre Associados e Empresas do negócio para um objeto da camada de camada de banco de dados
    ''' </summary>
    ''' <param name="TipoRelacao">Enum tipo de relação</param>
    ''' <param name="idPrincipal">Id Principal</param>
    ''' <param name="ListaRelacaoEmpresasAssociados">Lista de RelacaoEmpresaAssociadoModel</param>
    ''' <returns>List(Of DesafioS4EDb.EmpresasAssociados)</returns>
    Public Shared Function ConverterParaListaBanco(tipoRelacao As EnumTipoRelacao, idPrincipal As Integer, listaRelacaoEmpresasAssociados As List(Of RelacaoEmpresaAssociadoModel)) As List(Of DesafioS4EDb.EmpresasAssociados)

        Dim listaEmpresasAssociadosDb = New List(Of DesafioS4EDb.EmpresasAssociados)

        If listaRelacaoEmpresasAssociados IsNot Nothing Then
            If tipoRelacao = EnumTipoRelacao.EmpresasDoAssociado Then

                For Each relacao In listaRelacaoEmpresasAssociados
                    listaEmpresasAssociadosDb.Add(New DesafioS4EDb.EmpresasAssociados(relacao.Id, idPrincipal, relacao.Instrucao))
                Next

            ElseIf tipoRelacao = EnumTipoRelacao.AssociadosDaEmpresa Then

                For Each relacao In listaRelacaoEmpresasAssociados
                    listaEmpresasAssociadosDb.Add(New DesafioS4EDb.EmpresasAssociados(idPrincipal, relacao.Id, relacao.Instrucao))
                Next

            End If
        End If

        Return listaEmpresasAssociadosDb

    End Function


    ''' <summary>
    ''' Obtém um objeto de relação entre Associados e Empresas pelo Id da Empresa e Id do Associado
    ''' </summary>
    ''' <param name="idEmpresa">Id da Empresa</param>
    ''' <param name="idAssociado">Id do Associado</param>
    ''' <returns>Tupla (EmpresaAssociado As EmpresaAssociadoModel, Retorno As RetornoModel)</returns>
    Public Shared Function Ver(idEmpresa As Integer, idAssociado As Integer) As (EmpresaAssociado As EmpresaAssociadoModel, Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.EmpresasAssociados.Select(idEmpresa, idAssociado)

        Dim resultadoValidacao = ValidarVer(consultaDb.EmpresaAssociadoDb.IdEmpresa, consultaDb.EmpresaAssociadoDb.IdAssociado)

        If resultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(consultaDb.EmpresaAssociadoDb), resultadoValidacao)
        End If

        Return ConverterParaModelo(consultaDb.EmpresaAssociadoDb, consultaDb.RetornoDb)

    End Function



    ''' <summary>
    ''' Realiza a validação de relação entre Associados e Empresas ao ver
    ''' </summary>
    ''' <param name="idEmpresa">Id da Empresa</param>
    ''' <param name="idAssociado">Id do Associado</param>
    ''' <returns>RetornoModel</returns>
    Private Shared Function ValidarVer(idEmpresa As Integer, idAssociado As Integer) As RetornoModel

        If idEmpresa = 0 Or idAssociado = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar a relação entre a Empresa e Associado informados!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

End Class
