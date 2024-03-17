
Imports System.Text
Imports DesafioS4EDb.SQL

Public Class EmpresasAssociados

    Public Sub New()
    End Sub

    Public Sub New(CnpjEmpresa As String, CpfAssociado As String)
        Me.CnpjEmpresa = CnpjEmpresa
        Me.CpfAssociado = CpfAssociado
    End Sub

    Property CnpjEmpresa As String
    Property CpfAssociado As String


    Public Shared Function [Select](CnpjEmpresa As String, CpfAssociado As String) As (EmpresaAssociadoDb As EmpresasAssociados, RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWhereIdEmpresaIdAssociado(CnpjEmpresa, CpfAssociado)

        Dim consulta = Script.GerarSelectAll(New EmpresasAssociados(), Where)

        Return Comando.Obtenha(Of EmpresasAssociados)(consulta)

    End Function

    Public Shared Function SelectAll(Optional CnpjEmpresa As String = "", Optional CpfAssociado As String = "") As (ListaEmpresaAssociadosDb As List(Of EmpresasAssociados), RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWhereIdEmpresaIdAssociado(CnpjEmpresa, CpfAssociado)

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




    Private Shared Function GerarClausulaWhereIdEmpresaIdAssociado(Optional CnpjEmpresa As String = "", Optional CpfAssociado As String = "") As String
        Dim Where = New StringBuilder()

        If CnpjEmpresa > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"[CnpjEmpresa] = '{CnpjEmpresa}' ")
        End If

        If CpfAssociado > 0 Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[CpfAssociado] = '{CpfAssociado}'")
        End If

        Return Where.ToString()
    End Function

End Class
