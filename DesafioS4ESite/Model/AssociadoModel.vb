Imports DesafioS4ESite.Enumeradores

''' <summary>
''' Classe responsável pelo Negócio da tabela Associados
''' </summary>
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
    Property ListaEmpresas As List(Of RelacaoEmpresaAssociadoModel)



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

        Dim ResultadoValidacao = ValidarVer(ConsultaDb.AssociadoDb.Id)
        If ResultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(ConsultaDb.AssociadoDb), ResultadoValidacao)
        End If

        Dim Model = ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

        Model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return Model

    End Function

    Public Shared Function Ver(cpf As String) As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Associados.Select(cpf)

        Dim ResultadoValidacao = ValidarVer(ConsultaDb.AssociadoDb.Id)
        If ResultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(ConsultaDb.AssociadoDb), ResultadoValidacao)
        End If

        Dim Model = ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

        Model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return Model

    End Function

    Public Shared Function VerTodos(Optional FiltroCPF As String = "", Optional FiltroNome As String = "", Optional FiltroDataNascimentoInicio As Date = Nothing, Optional FiltroDataNascimentoFim As Date = Nothing) As (ListaAssociados As List(Of AssociadoModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Associados.SelectAll(FiltroCPF, FiltroNome, FiltroDataNascimentoInicio, FiltroDataNascimentoFim)

        Dim listaAssociadosModel = New List(Of AssociadoModel)

        Dim ResultadoValidacao = ValidarTodos(ConsultaDb.ListaAssociadosDb.Count())
        If ResultadoValidacao.Sucesso = False Then
            Return (listaAssociadosModel, ResultadoValidacao)
        End If

        For Each AssociadoDb In ConsultaDb.ListaAssociadosDb

            Dim Associado = ConverterParaModelo(AssociadoDb)

            Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

            listaAssociadosModel.Add(Associado)
        Next

        Return (listaAssociadosModel, New RetornoModel(ConsultaDb.RetornoDb.Sucesso, ConsultaDb.RetornoDb.Mensagem))

    End Function

    Public Function Cadastrar() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarCadastro()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim AssociadoDb = ConverterParaBanco(Me)

        Dim ConsultaDb = AssociadoDb.Insert()

        If ConsultaDb.RetornoDb.Sucesso And ConsultaDb.AssociadoDb.Id > 0 And Me.ListaEmpresas.Any = True Then
            Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, ConsultaDb.AssociadoDb.Id, Me.ListaEmpresas)
            ConsultaDb = ConsultaDb.AssociadoDb.Update(listaEmpresasAssociadosDb)
        End If

        Dim Model = ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

        Model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return Model

    End Function

    Public Function Atualizar() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarAtualizar()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim AssociadoDb = ConverterParaBanco(Me)

        Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, Me.Id, Me.ListaEmpresas)

        Dim ConsultaDb = AssociadoDb.Update(listaEmpresasAssociadosDb)

        Dim Model = ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

        Model.Associado.ListaEmpresas = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.AssociadoDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return Model

    End Function

    Public Function Excluir() As (Associado As AssociadoModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarExcluir()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim AssociadoDb = ConverterParaBanco(Me)

        Dim ConsultaEmpresasAssociadosDb = DesafioS4EDb.EmpresasAssociados.SelectAll(0, Me.Id)

        Dim ConsultaDb = AssociadoDb.Delete(ConsultaEmpresasAssociadosDb.ListaEmpresaAssociadosDb)

        Return ConverterParaModelo(ConsultaDb.AssociadoDb, ConsultaDb.RetornoDb)

    End Function




    Private Shared Function ValidarVer(Id As String) As RetornoModel

        If Id = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar o Associado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Shared Function ValidarTodos(QuantidadeRegistros As Integer) As RetornoModel

        If QuantidadeRegistros = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar os Associados!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

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

        Dim ConsultaAssociado = AssociadoModel.Ver(Me.Cpf)
        If ConsultaAssociado.Retorno.Sucesso = True Then
            Return New RetornoModel(False, "O CPF informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim ValidacaoEmpresasDoAssociado = Me.ValidarListaDeEmpresasNoCadastro()
        If ValidacaoEmpresasDoAssociado.Sucesso = False Then
            Return ValidacaoEmpresasDoAssociado
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

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

        Dim ConsultaAssociado = AssociadoModel.Ver(Me.Cpf)
        If ConsultaAssociado.Retorno.Sucesso = True And ConsultaAssociado.Associado.Id <> Me.Id Then
            Return New RetornoModel(False, "O CPF informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim ValidacaoEmpresasDoAssociado = Me.ValidarListaDeEmpresasNaAtualizacao()
        If ValidacaoEmpresasDoAssociado.Sucesso = False Then
            Return ValidacaoEmpresasDoAssociado
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Public Function ValidarExcluir() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Associado deve ser informado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Function ValidarListaDeEmpresasNoCadastro() As RetornoModel

        If Me.ListaEmpresas IsNot Nothing Then

            For Each Relacao In Me.ListaEmpresas

                If Relacao.Instrucao = EnumInstrucao.Incluir Then

                    Dim ConsultaEmpresa = EmpresaModel.Ver(Relacao.Id)
                    If ConsultaEmpresa.Retorno.Sucesso = False Then
                        Return ConsultaEmpresa.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na inclusão de Associados só é possível [Adicionar] novas Empresas!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Function ValidarListaDeEmpresasNaAtualizacao() As RetornoModel

        If Me.ListaEmpresas IsNot Nothing Then

            For Each Relacao In Me.ListaEmpresas

                If Relacao.Instrucao = EnumInstrucao.Incluir Or Relacao.Instrucao = EnumInstrucao.Remover Then

                    Dim ConsultaEmpresa = EmpresaModel.Ver(Relacao.Id)
                    If ConsultaEmpresa.Retorno.Sucesso = False Then
                        Return ConsultaEmpresa.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na alteração de Associados só é possível [Adicionar] ou [Remover] Empresas!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function



End Class
