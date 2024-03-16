Public Class EmpresaModel

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, nome As String, cnpj As String)
        Me.Id = id
        Me.Nome = nome
        Me.Cnpj = cnpj
    End Sub

    Property Id As Integer
    Property Nome As String
    Property Cnpj As String



    Private Shared Function ConverterParaModelo(empresaDb As DesafioS4EDb.Empresas) As EmpresaModel
        Return New EmpresaModel(empresaDb.Id, empresaDb.Nome, empresaDb.Cnpj)
    End Function

    Private Shared Function ConverterParaModelo(empresaDb As DesafioS4EDb.Empresas, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (Empresa As EmpresaModel, Retorno As RetornoModel)
        Return (New EmpresaModel(empresaDb.Id, empresaDb.Nome, empresaDb.Cnpj), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function

    Private Shared Function ConverterParaBanco(model As EmpresaModel) As DesafioS4EDb.Empresas
        Return New DesafioS4EDb.Empresas(model.Id, model.Nome, model.Cnpj)
    End Function



    Public Shared Function Ver(id As Integer) As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Empresas.Select(id)

        Return ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

    End Function

    Public Shared Function VerTodos() As (ListaEmpresas As List(Of EmpresaModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Empresas.SelectAll()

        Dim listaEmpresasModel = New List(Of EmpresaModel)

        For Each empresaDb In ConsultaDb.ListaEmpresasDb
            listaEmpresasModel.Add(ConverterParaModelo(empresaDb))
        Next

        Return (listaEmpresasModel, New RetornoModel(ConsultaDb.RetornoDb.Sucesso, ConsultaDb.RetornoDb.Mensagem))

    End Function

    Public Function Salvar() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        'If Not Me.ValidarSalvar() Then
        '    Return Me
        'End If


        Dim empresaDb = ConverterParaBanco(Me)

        If empresaDb.Id = 0 Then

            Dim ConsultaDb = empresaDb.Insert()
            Return ConverterParaModelo(empresaDb, ConsultaDb.RetornoDb)

        Else

            Dim ConsultaDb = empresaDb.Update()
            Return ConverterParaModelo(empresaDb, ConsultaDb.RetornoDb)

        End If
    End Function


    Public Function Excluir() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        'If Not Me.ValidarExcluir() Then
        '    Return Me
        'End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim ExecucaoDb = empresaDb.Delete()

        Return ConverterParaModelo(empresaDb, ExecucaoDb.RetornoDb)

    End Function


End Class
