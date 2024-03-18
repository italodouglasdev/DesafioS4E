Imports DesafioS4ESite.Enumeradores

''' <summary>
''' Classe responsável pelo Negócio da tabela Associados
''' </summary>
Public Class AssociadoModel

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto da classe AssiciadosModel
    ''' </summary>
    ''' <param name="id">Id do Associado</param>
    ''' <param name="nome">Nome do Associado</param>
    ''' <param name="cpf">CPF do Associado</param>
    ''' <param name="dataNascimento">Data de Nascimento do Associado</param>
    Public Sub New(id As Integer, nome As String, cpf As String, dataNascimento As Date)
        Me.Id = id
        Me.Nome = nome
        Me.Cpf = cpf
        Me.DataNascimento = dataNascimento
    End Sub

    ''' <summary>
    ''' Id do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Id As Integer

    ''' <summary>
    ''' Nome do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Nome As String

    ''' <summary>
    ''' CPF do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Cpf As String

    ''' <summary>
    ''' Data de Nascimento do Associado
    ''' </summary>
    ''' <returns></returns>
    Property DataNascimento As DateTime

    ''' <summary>
    ''' Lista de Empresas relacionadas ao Associado
    ''' </summary>
    ''' <returns></returns>
    Property ListaEmpresas As List(Of RelacaoEmpresaAssociadoModel)





    ''' <summary>
    ''' Realiza a conversão do objeto Associado da camada de banco de dados para um objeto da camada de negócio
    ''' </summary>
    ''' <param name="AssociadoDb">Objeto Associado do CRUD</param>
    ''' <returns>Empresa</returns>
    Private Shared Function ConverterParaModelo(associadoDb As DesafioS4EDb.Associados) As AssociadoModel
        Return New AssociadoModel(associadoDb.Id, associadoDb.Nome, associadoDb.Cpf, associadoDb.DataNascimento)
    End Function

    ''' <summary>
    ''' Realiza a conversão do objeto Associado da camada de banco de dados para um objeto da camada de negócio, com retorno do banco da consulta SQL
    ''' </summary>
    ''' <param name="AssociadoDb">Objeto Associado do CRUD</param>
    ''' <param name="RetornoDb">Objeto com o retorno da excução da consulta no banco de dados</param>
    ''' <returns></returns>
    Private Shared Function ConverterParaModelo(associadoDb As DesafioS4EDb.Associados, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (Associado As AssociadoModel, Retorno As RetornoModel)
        Return (New AssociadoModel(associadoDb.Id, associadoDb.Nome, associadoDb.Cpf, associadoDb.DataNascimento), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function

    ''' <summary>
    ''' Realiza a conversão do objeto Associado da camada de negócio para um objeto da camada de banco de dados
    ''' </summary>
    ''' <param name="Model">Objeto Associado do Negócio</param>
    ''' <returns></returns>
    Private Shared Function ConverterParaBanco(model As AssociadoModel) As DesafioS4EDb.Associados
        Return New DesafioS4EDb.Associados(model.Id, model.Nome, model.Cpf, model.DataNascimento)
    End Function





    ''' <summary>
    ''' Obtém um Associado pelo Id
    ''' </summary>
    ''' <param name="id">Id do Associado</param>
    ''' <returns>Tupla (Associado As AssociadoModel, Retorno As RetornoModel)</returns>
    Public Shared Function Ver(id As Integer) As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Associados.Select(id)

        Dim resultadoValidacao = ValidarVer(consultaDb.AssociadoDb.Id)
        If resultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(consultaDb.AssociadoDb), resultadoValidacao)
        End If

        Dim model = ConverterParaModelo(consultaDb.AssociadoDb, consultaDb.RetornoDb)

        model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Obtém um Associado pelo CPF
    ''' </summary>
    ''' <param name="cpf">CPF do Associado</param>
    ''' <returns>Tupla (Associado As AssociadoModel, Retorno As RetornoModel)</returns>
    Public Shared Function Ver(cpf As String) As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Associados.Select(cpf)

        Dim resultadoValidacao = ValidarVer(consultaDb.AssociadoDb.Id)
        If resultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(consultaDb.AssociadoDb), resultadoValidacao)
        End If

        Dim model = ConverterParaModelo(consultaDb.AssociadoDb, consultaDb.RetornoDb)

        model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Obtém uma lista de Associados
    ''' </summary>
    ''' <param name="FiltroCPF">Filtro de CPF (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <param name="FiltroDataNascimentoInicio">Filtro de Data de Nascimento Incício (Opcional)</param>
    ''' <param name="FiltroDataNascimentoFim">Filtro de Data de Nascimento Fim (Opcional)</param>
    ''' <returns>Tupla (ListaAssociados As List(Of AssociadoModel), Retorno As RetornoModel)</returns>
    Public Shared Function VerTodos(Optional filtroCPF As String = "", Optional filtroNome As String = "", Optional filtroDataNascimentoInicio As Date = Nothing, Optional filtroDataNascimentoFim As Date = Nothing) As (ListaAssociados As List(Of AssociadoModel), Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Associados.SelectAll(filtroCPF, filtroNome, filtroDataNascimentoInicio, filtroDataNascimentoFim)

        Dim listaAssociadosModel = New List(Of AssociadoModel)

        Dim resultadoValidacao = ValidarTodos(consultaDb.ListaAssociadosDb.Count())
        If resultadoValidacao.Sucesso = False Then
            Return (listaAssociadosModel, resultadoValidacao)
        End If

        For Each associadoDb In consultaDb.ListaAssociadosDb

            Dim associado = ConverterParaModelo(associadoDb)

            associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(associadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

            listaAssociadosModel.Add(associado)
        Next

        Return (listaAssociadosModel, New RetornoModel(consultaDb.RetornoDb.Sucesso, consultaDb.RetornoDb.Mensagem))

    End Function

    ''' <summary>
    ''' Cadastra um novo Associado
    ''' </summary>
    ''' <returns>Tupla (Associado As AssociadoModel, Retorno As RetornoModel)</returns>
    Public Function Cadastrar() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarCadastro()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim associadoDb = ConverterParaBanco(Me)

        Dim consultaDb = associadoDb.Insert()

        If consultaDb.RetornoDb.Sucesso And consultaDb.AssociadoDb.Id > 0 And Me.ListaEmpresas.Any = True Then
            Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, consultaDb.AssociadoDb.Id, Me.ListaEmpresas)
            consultaDb = consultaDb.AssociadoDb.Update(listaEmpresasAssociadosDb)
        End If

        Dim model = ConverterParaModelo(consultaDb.AssociadoDb, consultaDb.RetornoDb)

        model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Atualiza um Associado
    ''' </summary>
    ''' <returns>Tupla (Associado As AssociadoModel, Retorno As RetornoModel)</returns>
    Public Function Atualizar() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarAtualizar()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim associadoDb = ConverterParaBanco(Me)

        Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, Me.Id, Me.ListaEmpresas)

        Dim consultaDb = associadoDb.Update(listaEmpresasAssociadosDb)

        Dim model = ConverterParaModelo(consultaDb.AssociadoDb, consultaDb.RetornoDb)

        model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Exclui um novo Associado
    ''' </summary>
    ''' <returns>Tupla (Associado As AssociadoModel, Retorno As RetornoModel)</returns>
    Public Function Excluir() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarExcluir()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim associadoDb = ConverterParaBanco(Me)

        Dim consultaEmpresasAssociadosDb = DesafioS4EDb.EmpresasAssociados.SelectAll(0, Me.Id)

        Dim consultaDb = associadoDb.Delete(consultaEmpresasAssociadosDb.ListaEmpresaAssociadosDb)

        Return ConverterParaModelo(consultaDb.AssociadoDb, consultaDb.RetornoDb)

    End Function






    ''' <summary>
    ''' Realiza a validação do Associado ao ver
    ''' </summary>
    ''' <param name="Id">Id do Associado</param>
    ''' <returns>RetornoModel</returns>
    Private Shared Function ValidarVer(id As String) As RetornoModel

        If id = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar o Associado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação do Associado ao ver todos
    ''' </summary>
    ''' <param name="QuantidadeRegistros">Quantidade de registros na lista</param>
    ''' <returns>RetornoModel</returns>
    Private Shared Function ValidarTodos(quantidadeRegistros As Integer) As RetornoModel

        If quantidadeRegistros = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar os Associados!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação do Associado ao Cadastrar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Public Function ValidarCadastro() As RetornoModel

        If Me.Id > 0 Then
            Return New RetornoModel(False, "O Id do Associado é gerado automaticamente em novos cadastros, por favor informar o valor 0 (zero)!")
        End If

        If Me.Cpf.Length <> 11 Then
            Return New RetornoModel(False, "O CPF deve conter 11 caracteres!")
        End If

        If String.IsNullOrEmpty(Me.Cpf) Or Me.Cpf.Length = 0 Then
            Return New RetornoModel(False, "O CPF deve ser informado!")
        End If

        If ValidadorHelper.ValidarCPF(Me.Cpf) = False Then
            Return New RetornoModel(False, "O CPF informado é inválido!")
        End If

        Dim consultaAssociado = AssociadoModel.Ver(Me.Cpf)
        If consultaAssociado.Retorno.Sucesso = True Then
            Return New RetornoModel(False, "O CPF informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim validacaoEmpresasDoAssociado = Me.ValidarListaDeEmpresasNoCadastro()
        If validacaoEmpresasDoAssociado.Sucesso = False Then
            Return validacaoEmpresasDoAssociado
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação do Associado ao Atualizar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Public Function ValidarAtualizar() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Associado deve ser informado!")
        End If

        If Me.Cpf.Length <> 11 Then
            Return New RetornoModel(False, "O CPF deve conter 11 caracteres!")
        End If

        If String.IsNullOrEmpty(Me.Cpf) Or Me.Cpf.Length = 0 Then
            Return New RetornoModel(False, "O CPF deve ser informado!")
        End If

        If ValidadorHelper.ValidarCPF(Me.Cpf) = False Then
            Return New RetornoModel(False, "O CPF informado é inválido!")
        End If

        Dim consultaAssociado = AssociadoModel.Ver(Me.Cpf)
        If consultaAssociado.Retorno.Sucesso = True And consultaAssociado.Associado.Id <> Me.Id Then
            Return New RetornoModel(False, "O CPF informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim validacaoEmpresasDoAssociado = Me.ValidarListaDeEmpresasNaAtualizacao()
        If validacaoEmpresasDoAssociado.Sucesso = False Then
            Return validacaoEmpresasDoAssociado
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação do Associado ao Excluir
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Public Function ValidarExcluir() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Associado deve ser informado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação lista de Empresas relacionadas com o Associado ao Cadastrar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Private Function ValidarListaDeEmpresasNoCadastro() As RetornoModel

        If Me.ListaEmpresas IsNot Nothing Then

            For Each relacaoEmpresaAssociado In Me.ListaEmpresas

                If relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Incluir Then

                    Dim consultaEmpresa = EmpresaModel.Ver(relacaoEmpresaAssociado.Id)
                    If consultaEmpresa.Retorno.Sucesso = False Then
                        Return consultaEmpresa.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na inclusão de Associados só é possível [Adicionar] novas Empresas!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação lista de Empresas relacionadas com o Associado ao Atualizar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Private Function ValidarListaDeEmpresasNaAtualizacao() As RetornoModel

        If Me.ListaEmpresas IsNot Nothing Then

            For Each relacaoEmpresaAssociado In Me.ListaEmpresas

                If relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Incluir Or relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Remover Then

                    Dim consultaEmpresa = EmpresaModel.Ver(relacaoEmpresaAssociado.Id)
                    If consultaEmpresa.Retorno.Sucesso = False Then
                        Return consultaEmpresa.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na alteração de Associados só é possível [Adicionar] ou [Remover] Empresas!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function




End Class
