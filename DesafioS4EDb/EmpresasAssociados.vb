
Imports System.Text
Imports DesafioS4EDb.Enumeradores
Imports DesafioS4EDb.SQL

Public Class EmpresasAssociados

    Public Sub New()
    End Sub

    Public Sub New(idEmpresa As Integer, idAssociado As Integer, instrucao As EnumInstrucaoDb)
        Me.IdEmpresa = idEmpresa
        Me.IdAssociado = idAssociado
        Me.Instrucao = instrucao
    End Sub

    Property IdEmpresa As Integer
    Property IdAssociado As Integer


    Property Instrucao As EnumInstrucaoDb


    Public Shared Function [Select](IdEmpresa As Integer, IdAssociado As Integer) As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWhereIdEmpresaIdAssociado(IdEmpresa, IdAssociado)

        Dim consulta = Script.GerarSelectAll(New EmpresasAssociados(), Where)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    End Function

    Public Shared Function SelectAll(Optional IdEmpresa As Integer = 0, Optional IdAssociado As Integer = 0) As (ListaEmpresaAssociadosDb As List(Of EmpresasAssociados), RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWhereIdEmpresaIdAssociado(IdEmpresa, IdAssociado)

        Dim consulta = Script.GerarSelectAll(New EmpresasAssociados(), Where)

        Return Comando.ObtenhaLista(Of EmpresasAssociados)(consulta)

    End Function

    Public Function Insert() As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarInsert(Me)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    End Function

    Public Function Delete() As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarDelete(Me)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    End Function


    Public Shared Function ObtenhaListaDeConsultas(ListaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As StringBuilder

        Dim scripts = New StringBuilder()

        If ListaEmpresasAssociadosDb IsNot Nothing Then

            For Each empresaAssociadoDb In ListaEmpresasAssociadosDb

                If empresaAssociadoDb.Instrucao = EnumInstrucaoDb.Incluir Then

                    scripts.AppendLine(Script.GerarInsert(empresaAssociadoDb, False))

                ElseIf empresaAssociadoDb.Instrucao = EnumInstrucaoDb.Excluir Then

                    scripts.AppendLine(Script.GerarDelete(empresaAssociadoDb, $"WHERE [IdEmpresa] = {empresaAssociadoDb.IdEmpresa} AND [IdAssociado] = {empresaAssociadoDb.IdAssociado}"))

                End If

            Next

        End If

        Return scripts

    End Function


    Private Shared Function GerarClausulaWhereIdEmpresaIdAssociado(Optional IdEmpresa As Integer = 0, Optional IdAssociado As Integer = 0) As String

        Dim Where = New StringBuilder()

        If IdEmpresa > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"[IdEmpresa] = {IdEmpresa} ")
        End If

        If IdAssociado > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[IdAssociado] = {IdAssociado}")
        End If

        Return Where.ToString()

    End Function

End Class
