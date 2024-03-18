Imports DesafioS4EDb.SQL
Imports DesafioS4ESite.Enumeradores

''' <summary>
''' Classe responsável pelo gerenciamento das Empresas e seus Associados
''' </summary>
Public Class EmpresaModel

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto
    ''' </summary>
    ''' <param name="id">Id da Empresa</param>
    ''' <param name="nome">Nome da Empresa</param>
    ''' <param name="cnpj">CNPJ da Empresa</param>
    Public Sub New(id As Integer, nome As String, cnpj As String)
        Me.Id = id
        Me.Nome = nome
        Me.Cnpj = cnpj
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
    ''' CNPJ do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Cnpj As String

    ''' <summary>
    ''' Lista de Associados relacionadas a Empresa 
    ''' </summary>
    ''' <returns></returns>
    Property ListaAssociados As List(Of RelacaoEmpresaAssociadoModel)



    ''' <summary>
    ''' Realiza a conversão do objeto Empresa da camada de banco de dados para um objeto da camada de negócio
    ''' </summary>
    ''' <param name="EmpresaDb">Objeto Empresa do CRUD</param>
    ''' <returns>EmpresaModel</returns>
    Private Shared Function ConverterParaModelo(empresaDb As DesafioS4EDb.Empresas) As EmpresaModel
        Return New EmpresaModel(empresaDb.Id, empresaDb.Nome, empresaDb.Cnpj)
    End Function

    ''' <summary>
    ''' Realiza a conversão do objeto Empresa da camada de banco de dados para um objeto da camada de negócio, com retorno do banco da consulta SQL
    ''' </summary>
    ''' <param name="empresaDb">Objeto Empresa do CRUD</param>
    ''' <param name="RetornoDb">Objeto com o retorno da excução da consulta no banco de dados</param>
    ''' <returns>EmpresaModel</returns>
    Private Shared Function ConverterParaModelo(empresaDb As DesafioS4EDb.Empresas, retornoDb As DesafioS4EDb.SQL.RetornoDb) As (Empresa As EmpresaModel, Retorno As RetornoModel)
        Return (New EmpresaModel(empresaDb.Id, empresaDb.Nome, empresaDb.Cnpj), New RetornoModel(retornoDb.Sucesso, retornoDb.Mensagem))
    End Function

    ''' <summary>
    ''' Realiza a conversão do objeto Empresa da camada de negócio para um objeto da camada de banco de dados
    ''' </summary>
    ''' <param name="Model">Objeto Empresa do Negócio</param>
    ''' <returns>DesafioS4EDb.Empresas</returns>
    Private Shared Function ConverterParaBanco(model As EmpresaModel) As DesafioS4EDb.Empresas
        Return New DesafioS4EDb.Empresas(model.Id, model.Nome, model.Cnpj)
    End Function



    ''' <summary>
    ''' Obtém uma Empresa pelo Id
    ''' </summary>
    ''' <param name="id">Id da Empresa</param>
    ''' <returns>Tupla (Empresa As EmpresaModel, Retorno As RetornoModel)</returns>
    Public Shared Function Ver(id As Integer) As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Empresas.Select(id)

        Dim resultadoValidacao = ValidarVer(consultaDb.EmpresaDb.Id)
        If resultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(consultaDb.EmpresaDb), resultadoValidacao)
        End If

        Dim model = ConverterParaModelo(consultaDb.EmpresaDb, consultaDb.RetornoDb)

        model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.EmpresaDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Obtém uma Empresa pelo CNPJ
    ''' </summary>
    ''' <param name="cnpj">CNPJ da Empresa</param>
    ''' <returns>Tupla (Empresa As EmpresaModel, Retorno As RetornoModel)</returns>
    Public Shared Function Ver(cnpj As String) As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Empresas.Select(cnpj)

        Dim resultadoValidacao = ValidarVer(consultaDb.EmpresaDb.Id)
        If resultadoValidacao.Sucesso = False Then
            Return (ConverterParaModelo(consultaDb.EmpresaDb), resultadoValidacao)
        End If

        Dim model = ConverterParaModelo(consultaDb.EmpresaDb, consultaDb.RetornoDb)

        model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.EmpresaDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Obtém uma lista de Empresas
    ''' </summary>
    ''' <param name="FiltroCNPJ">Filtro de CNPJ (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <returns>Tupla (ListaEmpresas As List(Of EmpresaModel), Retorno As RetornoModel)</returns>
    Public Shared Function VerTodos(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As (ListaEmpresas As List(Of EmpresaModel), Retorno As RetornoModel)

        Dim consultaDb = DesafioS4EDb.Empresas.SelectAll(FiltroCNPJ, FiltroNome)

        Dim listaEmpresasModel = New List(Of EmpresaModel)

        Dim resultadoValidacao = ValidarTodos(consultaDb.ListaEmpresasDb.Count())
        If resultadoValidacao.Sucesso = False Then
            Return (listaEmpresasModel, resultadoValidacao)
        End If

        For Each empresaDb In consultaDb.ListaEmpresasDb

            Dim empresa = ConverterParaModelo(empresaDb)

            empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(empresaDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

            listaEmpresasModel.Add(empresa)

        Next

        Return (listaEmpresasModel, New RetornoModel(consultaDb.RetornoDb.Sucesso, consultaDb.RetornoDb.Mensagem))

    End Function

    ''' <summary>
    ''' Cadastra uma nova Empresa
    ''' </summary>
    ''' <returns>Tupla (Empresa As EmpresaModel, Retorno As RetornoModel)</returns>
    Public Function Cadastrar() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarCadastro()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim consultaDb = empresaDb.Insert()

        If consultaDb.RetornoDb.Sucesso And consultaDb.EmpresaDb.Id > 0 And Me.ListaAssociados IsNot Nothing Then
            Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, consultaDb.EmpresaDb.Id, Me.ListaAssociados)
            consultaDb = consultaDb.EmpresaDb.Update(listaEmpresasAssociadosDb)
        End If

        Dim model = ConverterParaModelo(consultaDb.EmpresaDb, consultaDb.RetornoDb)

        model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.EmpresaDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Atualiza uma Empresa
    ''' </summary>
    ''' <returns>Tupla (Empresa As EmpresaModel, Retorno As RetornoModel)</returns>
    Public Function Atualizar() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarAtualizar()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim listaEmpresasAssociadosDb = EmpresaAssociadoModel.ConverterParaListaBanco(EnumTipoRelacao.AssociadosDaEmpresa, Me.Id, Me.ListaAssociados)

        Dim consultaDb = empresaDb.Update(listaEmpresasAssociadosDb)

        Dim model = ConverterParaModelo(consultaDb.EmpresaDb, consultaDb.RetornoDb)

        model.Empresa.ListaAssociados = RelacaoEmpresaAssociadoModel.VerTodos(consultaDb.EmpresaDb.Id, EnumTipoRelacao.AssociadosDaEmpresa)

        Return model

    End Function

    ''' <summary>
    ''' Exclui uma Empresa
    ''' </summary>
    ''' <returns>Tupla (Empresa As EmpresaModel, Retorno As RetornoModel)</returns>
    Public Function Excluir() As (Empresa As EmpresaModel, Retorno As RetornoModel)

        Dim resultadoValidacao = ValidarExcluir()

        If resultadoValidacao.Sucesso = False Then
            Return (Me, resultadoValidacao)
        End If

        Dim empresaDb = ConverterParaBanco(Me)

        Dim consultaEmpresasAssociadosDb = DesafioS4EDb.EmpresasAssociados.SelectAll(Me.Id, 0)

        Dim consultaDb = empresaDb.Delete(consultaEmpresasAssociadosDb.ListaEmpresaAssociadosDb)

        Return ConverterParaModelo(consultaDb.EmpresaDb, consultaDb.RetornoDb)

    End Function



    ''' <summary>
    ''' Realiza a validação da Empresa ao ver
    ''' </summary>
    ''' <param name="Id">Id da Empresa</param>
    ''' <returns>RetornoModel</returns>
    Private Shared Function ValidarVer(Id As String) As RetornoModel

        If Id = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar a Empresa!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação da Empresa ao ver todos
    ''' </summary>
    ''' <param name="QuantidadeRegistros">Quantidade de registros na lista</param>
    ''' <returns>RetornoModel</returns>
    Private Shared Function ValidarTodos(quantidadeRegistros As Integer) As RetornoModel

        If quantidadeRegistros = 0 Then
            Return New RetornoModel(False, $"Não foi possível localizar as Empresas!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação da Empresa ao Cadastrar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
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

        Dim consultaEmpresa = EmpresaModel.Ver(Me.Cnpj)
        If consultaEmpresa.Retorno.Sucesso = True Then
            Return New RetornoModel(False, "O CNPJ informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim validacaoAssociadosDaEmpresa = Me.ValidarListaDeAssociadosNoCadastro()
        If validacaoAssociadosDaEmpresa.Sucesso = False Then
            Return validacaoAssociadosDaEmpresa
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação da Empresa ao Atualizar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
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

        Dim consultaEmpresa = EmpresaModel.Ver(Me.Cnpj)
        If consultaEmpresa.Retorno.Sucesso = True And consultaEmpresa.Empresa.Id <> Me.Id Then
            Return New RetornoModel(False, "O CNPJ informado já possui um cadastro!")
        End If

        If String.IsNullOrEmpty(Me.Nome) Or Me.Nome.Length = 0 Then
            Return New RetornoModel(False, "O Nome deve ser informado!")
        End If

        If Me.Nome.Length > 200 Then
            Return New RetornoModel(False, "O Nome deve conter no máximo 200 caracteres!")
        End If

        Dim validacaoAssociadosDaEmpresa = Me.ValidarListaDeAssociadosNaAtualizacao()
        If validacaoAssociadosDaEmpresa.Sucesso = False Then
            Return validacaoAssociadosDaEmpresa
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação da Empresa ao Excluir
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Public Function ValidarExcluir() As RetornoModel

        If Me.Id = 0 Then
            Return New RetornoModel(False, "O Id da Empresa deve ser informado!")
        End If

        Dim consultaRelacao = EmpresaAssociadoModel.Ver(Me.Id, 0)
        If consultaRelacao.Retorno.Sucesso = True And consultaRelacao.EmpresaAssociado.IdEmpresa = Me.Id Then
            Return New RetornoModel(False, "Não foi possível realizar a exclusão da Empresa, pois ela possui vínculo com um ou mais Associados!")
        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação lista de Associados relacionadas com a Empresa ao Cadastrar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Private Function ValidarListaDeAssociadosNoCadastro() As RetornoModel

        If Me.ListaAssociados IsNot Nothing Then

            For Each relacaoEmpresaAssociado In Me.ListaAssociados

                If relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Incluir Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(relacaoEmpresaAssociado.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, relacaoEmpresaAssociado.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = True Then
                        ConsultaEmpresaAssociado.Retorno.Sucesso = False
                        ConsultaEmpresaAssociado.Retorno.Mensagem = $"Já existe uma relação entro a Empresa e o Associado informados! | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
                        Return ConsultaEmpresaAssociado.Retorno
                    End If

                Else

                    Return New RetornoModel(False, "Na inclusão de Emrpesas só é possível [Adicionar] novos Associados!")

                End If

            Next

        End If

        Return New RetornoModel(True, "Operação realizada com Sucesso!")

    End Function

    ''' <summary>
    ''' Realiza a validação lista de Associados relacionadas com a Empresa ao Atualizar
    ''' </summary>
    ''' <returns>RetornoModel</returns>
    Private Function ValidarListaDeAssociadosNaAtualizacao() As RetornoModel

        If Me.ListaAssociados IsNot Nothing Then

            For Each relacaoEmpresaAssociado In Me.ListaAssociados

                If relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Incluir Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(relacaoEmpresaAssociado.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, relacaoEmpresaAssociado.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = True Then
                        ConsultaEmpresaAssociado.Retorno.Sucesso = False
                        ConsultaEmpresaAssociado.Retorno.Mensagem = $"Já existe uma relação entro a Empresa e o Associado informados! | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
                        Return ConsultaEmpresaAssociado.Retorno
                    End If

                ElseIf relacaoEmpresaAssociado.Instrucao = EnumInstrucao.Remover Then

                    Dim ConsultaAssociado = AssociadoModel.Ver(relacaoEmpresaAssociado.Id)
                    If ConsultaAssociado.Retorno.Sucesso = False Then
                        ConsultaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
                        Return ConsultaAssociado.Retorno
                    End If

                    Dim ConsultaEmpresaAssociado = EmpresaAssociadoModel.Ver(Me.Id, relacaoEmpresaAssociado.Id)
                    If ConsultaEmpresaAssociado.Retorno.Sucesso = False Then
                        ConsultaEmpresaAssociado.Retorno.Mensagem += $" | Detalhes do Associado: Id {relacaoEmpresaAssociado.Id}"
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
