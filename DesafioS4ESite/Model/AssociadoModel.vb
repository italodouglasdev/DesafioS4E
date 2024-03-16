
Public Class AssociadoModel

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, nome As String, cpf As String, dataNascimento As Date)
        Me.Id = id
        Me.Nome = nome
        Me.Cpf = cpf
        Me.DataNascimento = dataNascimento
    End Sub

    Property Id As Integer
    Property Nome As String
    Property Cpf As String
    Property DataNascimento As DateTime
    Property ListaEmpresas As List(Of RelacaoAssociadoEmpresaModel)



    Private Shared Function ConverterParaModelo(AssociadoDb As DesafioS4EDb.Associados) As AssociadoModel
        Return New AssociadoModel(AssociadoDb.Id, AssociadoDb.Nome, AssociadoDb.Cpf, AssociadoDb.DataNascimento)
    End Function

    Private Shared Function ConverterParaModelo(AssociadoDb As DesafioS4EDb.Associados, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (Associado As AssociadoModel, Retorno As RetornoModel)
        Return (New AssociadoModel(AssociadoDb.Id, AssociadoDb.Nome, AssociadoDb.Cpf, AssociadoDb.DataNascimento), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function

    Private Shared Function ConverterParaBanco(model As AssociadoModel) As DesafioS4EDb.Associados
        Return New DesafioS4EDb.Associados(model.Id, model.Nome, model.Cpf, model.DataNascimento)
    End Function



    Public Shared Function Ver(id As Integer) As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Associados.Select(id)

        Return ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

    End Function

    Public Shared Function VerTodos() As (ListaAssociados As List(Of AssociadoModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Associados.SelectAll()

        Dim listaAssociadosModel = New List(Of AssociadoModel)

        For Each AssociadoDb In ConsultaDb.ListaAssociadosDb
            listaAssociadosModel.Add(ConverterParaModelo(AssociadoDb))
        Next

        Return (listaAssociadosModel, New RetornoModel(ConsultaDb.RetornoDb.Sucesso, ConsultaDb.RetornoDb.Mensagem))

    End Function

    Public Function Salvar() As (Associado As AssociadoModel, Retorno As RetornoModel)

        'If Not Me.ValidarSalvar() Then
        '    Return Me
        'End If


        Dim AssociadoDb = ConverterParaBanco(Me)

        If AssociadoDb.Id = 0 Then

            Dim ConsultaDb = AssociadoDb.Insert()
            Return ConverterParaModelo(AssociadoDb, ConsultaDb.RetornoDb)

        Else

            Dim ConsultaDb = AssociadoDb.Update()
            Return ConverterParaModelo(AssociadoDb, ConsultaDb.RetornoDb)

        End If
    End Function


    Public Function Excluir() As (Associado As AssociadoModel, Retorno As RetornoModel)

        'If Not Me.ValidarExcluir() Then
        '    Return Me
        'End If

        Dim AssociadoDb = ConverterParaBanco(Me)

        Dim ExecucaoDb = AssociadoDb.Delete()

        Return ConverterParaModelo(AssociadoDb, ExecucaoDb.RetornoDb)

    End Function


End Class
