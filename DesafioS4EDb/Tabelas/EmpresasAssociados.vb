Imports System.Text
Imports DesafioS4EDb.Enumeradores
Imports DesafioS4EDb.SQL

''' <summary>
''' Classe responsável pelo CRUD da tabela EmpresasAssociados
''' </summary>
Public Class EmpresasAssociados

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto da classe EmpresasAssociados
    ''' </summary>
    ''' <param name="idEmpresa">Id da Empresa</param>
    ''' <param name="idAssociado">Id do Associado</param>
    ''' <param name="instrucao">Instrução para monstagem do script SQL</param>
    Public Sub New(idEmpresa As Integer, idAssociado As Integer, instrucao As EnumInstrucaoDb)
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
    Property Instrucao As EnumInstrucaoDb





    ''' <summary>
    ''' Obtém um objeto do tipo EmpresasAssociados pelo Id da Empresa e Id do Associado
    ''' </summary>
    ''' <param name="IdEmpresa">Id da Empresa</param>
    ''' <param name="IdAssociado">Id do Associado</param>
    ''' <returns>Tupla (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)</returns>
    Public Shared Function [Select](idEmpresa As Integer, idAssociado As Integer) As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim where = GerarClausulaWhereIdEmpresaIdAssociado(idEmpresa, idAssociado)

        Dim consulta = Script.GerarSelectAll(New EmpresasAssociados(), where)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    End Function

    ''' <summary>
    ''' Obtém uma lista de objetos do tipo EmpresasAssociados pelo Id da Empresa e Id do Associado
    ''' </summary>
    ''' <param name="IdEmpresa">Id da Empresa (Opcional)</param>
    ''' <param name="IdAssociado">Id do Associado (Opcional)</param>
    ''' <returns>Tupla (ListaEmpresaAssociadosDb As List(Of EmpresasAssociados), RetornoDb As RetornoDb)</returns>
    Public Shared Function SelectAll(Optional idEmpresa As Integer = 0, Optional idAssociado As Integer = 0) As (ListaEmpresaAssociadosDb As List(Of EmpresasAssociados), RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWhereIdEmpresaIdAssociado(idEmpresa, idAssociado)

        Dim consulta = Script.GerarSelectAll(New EmpresasAssociados(), Where)

        Return Comando.ObtenhaLista(Of EmpresasAssociados)(consulta)

    End Function



    ''' <summary>
    ''' Obtém consultas SQL de acordo com o tipo de Instrução
    ''' </summary>
    ''' <param name="ListaEmpresasAssociadosDb">Lista de EmpresasAssociados</param>
    ''' <returns>Retorna uma string com aos Scripts</returns>
    Public Shared Function ObtenhaListaDeConsultas(listaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As StringBuilder

        Dim scripts = New StringBuilder()

        If listaEmpresasAssociadosDb IsNot Nothing Then

            For Each empresaAssociadoDb In listaEmpresasAssociadosDb

                If empresaAssociadoDb.Instrucao = EnumInstrucaoDb.Incluir Then

                    scripts.AppendLine(Script.GerarInsert(empresaAssociadoDb, False))

                ElseIf empresaAssociadoDb.Instrucao = EnumInstrucaoDb.Remover Then

                    scripts.AppendLine(Script.GerarDelete(empresaAssociadoDb, $"WHERE [IdEmpresa] = {empresaAssociadoDb.IdEmpresa} AND [IdAssociado] = {empresaAssociadoDb.IdAssociado}"))

                End If

            Next

        End If

        Return scripts

    End Function

    ''' <summary>
    ''' Gerar a cláusula WHERE com base nos parâmetros informados
    ''' </summary>
    ''' <param name="IdEmpresa">Id da Empresa (Opcional)</param>
    '''  <param name="IdAssociado">Id do Associado (Opcional)</param>
    ''' <returns>Retorna uma string com a cláusula WHERE</returns>
    Private Shared Function GerarClausulaWhereIdEmpresaIdAssociado(Optional idEmpresa As Integer = 0, Optional idAssociado As Integer = 0) As String

        Dim Where = New StringBuilder()

        If idEmpresa > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"[IdEmpresa] = {idEmpresa} ")
        End If

        If idAssociado > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[IdAssociado] = {idAssociado}")
        End If

        Return Where.ToString()

    End Function

End Class
