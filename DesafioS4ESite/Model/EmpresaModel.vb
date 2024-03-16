Imports DesafioS4EDb.SQL

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
    Property ListaAssociados As List(Of RelacaoEmpresaAssociadoModel)


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

        Dim ResultadoValidacao = ValidarVer(ConsultaDb.EmpresaDb.Id)
        If ResultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(ConsultaDb.EmpresaDb), ResultadoValidacao)
        End If

        Return ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

    End Function

    Public Shared Function VerTodos(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As (ListaEmpresas As List(Of EmpresaModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Empresas.SelectAll(FiltroCNPJ, FiltroNome)

        Dim listaEmpresasModel = New List(Of EmpresaModel)

        Dim ResultadoValidacao = ValidarTodos(ConsultaDb.ListaEmpresasDb.Count())
        If ResultadoValidacao.Sucesso = False Then
            Return (listaEmpresasModel, ResultadoValidacao)
        End If

        For Each empresaDb In ConsultaDb.ListaEmpresasDb
            listaEmpresasModel.Add(ConverterParaModelo(empresaDb))
        Next

        Return (listaEmpresasModel, New RetornoModel(ConsultaDb.RetornoDb.Sucesso, ConsultaDb.RetornoDb.Mensagem))

    End Function

    Public Function Cadastrar() As (Empresa As EmpresaModel, Retorno As RetornoModel)


        Dim ResultadoValidacao = ValidarCadastro()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If


        Dim empresaDb = ConverterParaBanco(Me)

        Dim ConsultaDb = empresaDb.Insert()

        Return ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

    End Function

    Public Function Atualizar() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarAtualizar()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim ConsultaDb = empresaDb.Update()
        Return ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)


    End Function


    Public Function Excluir() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        'If Not Me.ValidarExcluir() Then
        '    Return Me
        'End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim ConsultaDb = empresaDb.Delete()

        Return ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

    End Function




    Private Shared Function ValidarVer(Id As String) As RetornoModel

        If Id = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar a Empresa!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Shared Function ValidarTodos(QuantidadeRegistros As Integer) As RetornoModel

        If QuantidadeRegistros = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar as Empresas!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Function ValidarCadastro() As RetornoModel

        If Me.Id > 0 Then
            Return New RetornoModel(False, "O Id da Empresa é gerado automaticamente em novos cadastros, por favor informar o valor 0 (zero)!")
        End If

        If String.IsNullOrEmpty(Me.Nome) OrElse Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        If ValidadorHelper.ValidarCNPJ(Me.Cnpj) = False Then
            Return New RetornoModel(False, "O CNPJ informado é inválido!")
        End If

        If Me.ListaAssociados IsNot Nothing Then


        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Function ValidarAtualizar() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Empresa deve ser informado!")
        End If

        If String.IsNullOrEmpty(Me.Nome) OrElse Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        If ValidadorHelper.ValidarCNPJ(Me.Cnpj) = False Then
            Return New RetornoModel(False, "O CNPJ informado é inválido!")
        End If

        If Me.ListaAssociados IsNot Nothing Then


        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function


End Class
