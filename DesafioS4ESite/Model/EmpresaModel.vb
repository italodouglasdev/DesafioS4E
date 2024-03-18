Imports DesafioS4EDb.SQL
Imports DesafioS4ESite.Enumeradores


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

        Dim Model = ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

        Model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.EmpresaDb.Id, EnumTipoRelacao.EmpresasDoAssociado)

        Return Model

    End Function


    Public Shared Function Ver(cnpj As String) As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Empresas.Select(cnpj)

        Dim ResultadoValidacao = ValidarVer(ConsultaDb.EmpresaDb.Id)
        If ResultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(ConsultaDb.EmpresaDb), ResultadoValidacao)
        End If

        Dim Model = ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

        Model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.EmpresaDb.Id, EnumTipoRelacao.EmpresasDoAssociado)

        Return Model

    End Function

    Public Shared Function VerTodos(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As (ListaEmpresas As List(Of EmpresaModel), Retorno As RetornoModel)

        Dim ConsultaDb = DesafioS4EDb.Empresas.SelectAll(FiltroCNPJ, FiltroNome)

        Dim listaEmpresasModel = New List(Of EmpresaModel)

        Dim ResultadoValidacao = ValidarTodos(ConsultaDb.ListaEmpresasDb.Count())
        If ResultadoValidacao.Sucesso = False Then
            Return (listaEmpresasModel, ResultadoValidacao)
        End If

        For Each empresaDb In ConsultaDb.ListaEmpresasDb

            Dim Empresa = ConverterParaModelo(empresaDb)

            Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(empresaDb.Id, EnumTipoRelacao.EmpresasDoAssociado)

            listaEmpresasModel.Add(Empresa)

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

        If ConsultaDb.RetornoDb.Sucesso And ConsultaDb.EmpresaDb.Id > 0 And Me.ListaAssociados.Any = True Then
            Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, ConsultaDb.EmpresaDb.Id, Me.ListaAssociados)
            ConsultaDb = ConsultaDb.EmpresaDb.Update(listaEmpresasAssociadosDb)
        End If

        Dim Model = ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

        Model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.EmpresaDb.Id, EnumTipoRelacao.EmpresasDoAssociado)

        Return Model

    End Function

    Public Function Atualizar() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarAtualizar()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, Me.Id, Me.ListaAssociados)

        Dim ConsultaDb = empresaDb.Update(listaEmpresasAssociadosDb)

        Dim Model = ConverterParaModelo(ConsultaDb.EmpresaDb, ConsultaDb.RetornoDb)

        Model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(ConsultaDb.EmpresaDb.Id, EnumTipoRelacao.EmpresasDoAssociado)

        Return Model

    End Function


    Public Function Excluir() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim ResultadoValidacao = ValidarExcluir()

        If ResultadoValidacao.Sucesso = False Then
            Return (Me, ResultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim ConsultaEmpresasAssociadosDb = DesafioS4EDb.EmpresasAssociados.SelectAll(Me.Id, 0)

        Dim ConsultaDb = empresaDb.Delete(ConsultaEmpresasAssociadosDb.ListaEmpresaAssociadosDb)

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

    Public Function ValidarCadastro() As RetornoModel

        If Me.Id > 0 Then
            Return New RetornoModel(False, "O Id da Empresa é gerado automaticamente em novos cadastros, por favor informar o valor 0 (zero)!")
        End If

        If Me.Cnpj.Length <> 14 Then
            Return New RetornoModel(False, "O CNPJ deve conter 14 caracteres!")
        End If

        If String.IsNullOrEmpty(Me.Cnpj) Or Me.Cnpj.Length = 0 Then
            Return New RetornoModel(False, "O CNPJ deve ser informado!")
        End If

        If ValidadorHelper.ValidarCNPJ(Me.Cnpj) = False Then
            Return New RetornoModel(False, "O CNPJ informado é inválido!")
        End If

        Dim ConsultaEmpresa = EmpresaModel.Ver(Me.Cnpj)
        If ConsultaEmpresa.Retorno.Sucesso = True Then
            Return New RetornoModel(False, "O CNPJ informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim ValidacaoAssociadosDaEmpresa = Me.ValidarListaDeAssociadosNoCadastro()
        If ValidacaoAssociadosDaEmpresa.Sucesso = False Then
            Return ValidacaoAssociadosDaEmpresa
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Public Function ValidarAtualizar() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Empresa deve ser informado!")
        End If

        If Me.Cnpj.Length <> 14 Then
            Return New RetornoModel(False, "O CNPJ deve conter 14 caracteres!")
        End If

        If String.IsNullOrEmpty(Me.Cnpj) Or Me.Cnpj.Length = 0 Then
            Return New RetornoModel(False, "O CNPJ deve ser informado!")
        End If

        If ValidadorHelper.ValidarCNPJ(Me.Cnpj) = False Then
            Return New RetornoModel(False, "O CNPJ informado é inválido!")
        End If

        Dim ConsultaEmpresa = EmpresaModel.Ver(Me.Cnpj)
        If ConsultaEmpresa.Retorno.Sucesso = True And ConsultaEmpresa.Empresa.Id <> Me.Id Then
            Return New RetornoModel(False, "O CNPJ informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim ValidacaoAssociadosDaEmpresa = Me.ValidarListaDeAssociadosNaAtualizacao()
        If ValidacaoAssociadosDaEmpresa.Sucesso = False Then
            Return ValidacaoAssociadosDaEmpresa
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Public Function ValidarExcluir() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Empresa deve ser informado!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function



    Private Function ValidarListaDeAssociadosNoCadastro() As RetornoModel

        If Me.ListaAssociados IsNot Nothing Then

            For Each Relacao In Me.ListaAssociados

                If Relacao.Instrucao = EnumInstrucao.Incluir Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(Relacao.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, Relacao.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = True Then
                        ConsultaEmpresaAssociado.Retorno.Sucesso = False
                        ConsultaEmpresaAssociado.Retorno.Mensagem = $"Já existe uma relação entro a Empresa e o Associado informados! | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaEmpresaAssociado.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na inclusão de Emrpesas só é possível [Adicionar] novos Associados!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    Private Function ValidarListaDeAssociadosNaAtualizacao() As RetornoModel

        If Me.ListaAssociados IsNot Nothing Then

            For Each Relacao In Me.ListaAssociados

                If Relacao.Instrucao = EnumInstrucao.Incluir Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(Relacao.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, Relacao.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = True Then
                        ConsultaEmpresaAssociado.Retorno.Sucesso = False
                        ConsultaEmpresaAssociado.Retorno.Mensagem = $"Já existe uma relação entro a Empresa e o Associado informados! | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaEmpresaAssociado.Retorno
                    End If

                ElseIf Relacao.Instrucao = EnumInstrucao.Remover Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(Relacao.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, Relacao.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = False Then
                        ConsultaEmpresaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {Relacao.Id}"
                        Return ConsultaEmpresaAssociado.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na alteração de Empresas só é possível [Adicionar] ou [Remover] Associados!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function


End Class
