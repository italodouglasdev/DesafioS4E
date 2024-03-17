
Imports DesafioS4EDb.SQL
Imports DesafioS4ESite.Enumeradores

Public Class RelacaoEmpresaAssociadoModel

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, instrucao As EnumInstrucao)
        Me.Id = id
        Me.Instrucao = instrucao
    End Sub

    Property Id As Integer
    Property Instrucao As EnumInstrucao


    Public Shared Function VerTodos(Id As Integer, TipoRelacao As EnumTipoRelacao) As List(Of RelacaoEmpresaAssociadoModel)

        Dim listaEmpresasAssociados = New List(Of DesafioS4EDb.EmpresasAssociados)

        Dim listaRelacoesEmpresasAssociados = New List(Of RelacaoEmpresaAssociadoModel)


        If TipoRelacao = EnumTipoRelacao.EmpresasDoAssociado Then

            Dim ConsultaDb = DesafioS4EDb.EmpresasAssociados.SelectAll(Id, 0)

            For Each EmpresaAssociadoDb In ConsultaDb.ListaEmpresaAssociadosDb
                listaRelacoesEmpresasAssociados.Add(New RelacaoEmpresaAssociadoModel(EmpresaAssociadoDb.IdAssociado, EnumInstrucao.Consultar))
            Next

        ElseIf TipoRelacao = EnumTipoRelacao.AssociadosDaEmpresa Then

            Dim ConsultaDb = DesafioS4EDb.EmpresasAssociados.SelectAll(0, Id)

            For Each EmpresaAssociadoDb In ConsultaDb.ListaEmpresaAssociadosDb
                listaRelacoesEmpresasAssociados.Add(New RelacaoEmpresaAssociadoModel(EmpresaAssociadoDb.IdEmpresa, EnumInstrucao.Consultar))
            Next

        End If




        Return listaRelacoesEmpresasAssociados

    End Function


End Class
