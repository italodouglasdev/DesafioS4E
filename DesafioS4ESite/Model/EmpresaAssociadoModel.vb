
Public Class EmpresaAssociadoModel

    Public Sub New()
    End Sub

    Public Sub New(IdEmpresa As Integer, IdAssociado As Integer)
        Me.IdEmpresa = IdEmpresa
        Me.IdAssociado = IdAssociado
    End Sub

    Property IdEmpresa As String
    Property IdAssociado As String



    Private Shared Function ConverterParaModelo(EmpresaAssociadoDb As DesafioS4EDb.EmpresasAssociados) As EmpresaAssociadoModel
        Return New EmpresaAssociadoModel(EmpresaAssociadoDb.IdEmpresa, EmpresaAssociadoDb.IdAssociado)
    End Function

    Private Shared Function ConverterParaModelo(EmpresaAssociadoDb As DesafioS4EDb.EmpresasAssociados, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (EmpresaAssociado As EmpresaAssociadoModel, Retorno As RetornoModel)
        Return (New EmpresaAssociadoModel(EmpresaAssociadoDb.IdEmpresa, EmpresaAssociadoDb.IdAssociado), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function

    Public Shared Function ConverterParaBanco(model As EmpresaAssociadoModel) As DesafioS4EDb.EmpresasAssociados
        Return New DesafioS4EDb.EmpresasAssociados(model.IdEmpresa, model.IdAssociado)
    End Function




    Public Shared Function Ver(idEmpresa As Integer, idAssociado As Integer) As (EmpresaAssociado As EmpresaAssociadoModel, Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.EmpresasAssociados.Select(idEmpresa, idAssociado)

        Dim ResultadoValidacao = ValidarVer(ConsultaDb.EmpresaAssociadoDb.IdEmpresa, ConsultaDb.EmpresaAssociadoDb.IdAssociado)
        If ResultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(ConsultaDb.EmpresaAssociadoDb), ResultadoValidacao)
        End If

        Return ConverterParaModelo(ConsultaDb.EmpresaAssociadoDb, ConsultaDb.RetornoDb)

    End Function


    Public Shared Function VerTodos(Optional idEmpresa As Integer = 0, Optional idAssociado As Integer = 0) As (ListaEmpresasEmpresaAssociados As List(Of EmpresaAssociadoModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.EmpresasAssociados.SelectAll(idEmpresa, idAssociado)

        Dim listaEmpresasEmpresaAssociadosModel = New List(Of EmpresaAssociadoModel)

        Dim ResultadoValidacao = ValidarTodos(ConsultaDb.ListaEmpresaAssociadosDb.Count())
        If ResultadoValidacao.Sucesso = False Then
            Return (listaEmpresasEmpresaAssociadosModel, ResultadoValidacao)
        End If

        For Each EmpresaAssociadoDb In ConsultaDb.ListaEmpresaAssociadosDb
            listaEmpresasEmpresaAssociadosModel.Add(ConverterParaModelo(EmpresaAssociadoDb))
        Next

        Return (listaEmpresasEmpresaAssociadosModel, New RetornoModel(ConsultaDb.RetornoDb.Sucesso, ConsultaDb.RetornoDb.Mensagem))

    End Function

    Private Shared Function ValidarVer(idEmpresa As Integer, idAssociado As Integer) As RetornoModel

        If idEmpresa = 0 Or idAssociado = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar a relação entre a Empresa e Associado informados!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Shared Function ValidarTodos(QuantidadeRegistros As Integer) As RetornoModel

        If QuantidadeRegistros = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar nenhuma relação com a Empresa e/ou Associado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function



End Class
