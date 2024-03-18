
Imports DesafioS4EDb.SQL
Imports DesafioS4ESite.Enumeradores

''' <summary>
''' Classe responsável por 
''' </summary>
Public Class RelacaoEmpresaAssociadoModel

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instacia um novo objeto
    ''' </summary>
    ''' <param name="id">Id Principal</param>
    ''' <param name="instrucao"></param>
    Public Sub New(id As Integer, instrucao As EnumInstrucao)
        Me.Id = id
        Me.Instrucao = instrucao
    End Sub

    ''' <summary>
    ''' Id Principal
    ''' </summary>
    ''' <returns></returns>
    Property Id As Integer

    ''' <summary>
    ''' Instrucao [Consultar], [Incluir] ou [Remover]
    ''' </summary>
    ''' <returns></returns>
    Property Instrucao As EnumInstrucao


    ''' <summary>
    ''' Obtém a lista de Empresas do Associado ou a lista de Associdos da Empresa com base no Tipo de Relação
    ''' TipoRelacao = EnumTipoRelacao.EmpresasDoAssociado => Informar Id da Empresa como Id Principal
    ''' TipoRelacao = EnumTipoRelacao.AssociadosDaEmpresa => Informar Id do Associado como Id Principal
    ''' </summary>
    ''' <param name="Id">Id Principal</param>
    ''' <param name="TipoRelacao">Tipo de Relação</param>
    ''' <returns>List(Of RelacaoEmpresaAssociadoModel)</returns>
    Public Shared Function VerTodos(id As Integer, tipoRelacao As EnumTipoRelacao) As List(Of RelacaoEmpresaAssociadoModel)

        Dim listaEmpresasAssociados = New List(Of DesafioS4EDb.EmpresasAssociados)
        Dim listaRelacoesEmpresasAssociados = New List(Of RelacaoEmpresaAssociadoModel)

        If tipoRelacao = EnumTipoRelacao.EmpresasDoAssociado Then

            Dim consultaDb = DesafioS4EDb.EmpresasAssociados.SelectAll(0, id)

            For Each empresaAssociadoDb In consultaDb.ListaEmpresaAssociadosDb
                listaRelacoesEmpresasAssociados.Add(New RelacaoEmpresaAssociadoModel(empresaAssociadoDb.IdEmpresa, EnumInstrucao.Consultar))
            Next

        ElseIf tipoRelacao = EnumTipoRelacao.AssociadosDaEmpresa Then

            Dim consultaDb = DesafioS4EDb.EmpresasAssociados.SelectAll(id, 0)

            For Each empresaAssociadoDb In consultaDb.ListaEmpresaAssociadosDb
                listaRelacoesEmpresasAssociados.Add(New RelacaoEmpresaAssociadoModel(empresaAssociadoDb.IdAssociado, EnumInstrucao.Consultar))
            Next

        End If

        Return listaRelacoesEmpresasAssociados

    End Function


End Class
