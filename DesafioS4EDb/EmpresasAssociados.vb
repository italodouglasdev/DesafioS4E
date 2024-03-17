
Imports System.Text
Imports DesafioS4EDb.SQL

Public Class EmpresasAssociados

    Public Sub New()
    End Sub

    Public Sub New(IdEmpresa As Integer, IdAssociado As Integer)
        Me.IdEmpresa = IdEmpresa
        Me.IdAssociado = IdAssociado
    End Sub

    Property IdEmpresa As String
    Property IdAssociado As String


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

    'Public Function Update() As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

    '    Dim consulta = Script.GerarUpdate(Me)
    '    Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    'End Function

    Public Function Delete() As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarDelete(Me)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

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
