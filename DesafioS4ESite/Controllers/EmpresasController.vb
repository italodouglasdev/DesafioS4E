Imports System.Web.Http

Namespace Controllers

    ''' <summary>
    ''' Cadastro de Empresas
    ''' </summary>
    Public Class EmpresasController
        Inherits ApiController

        ''' <summary>
        ''' Obtém uma lista de Empresas
        ''' </summary>
        ''' <param name="FiltroCNPJ">Filtro de CNPJ (Opcional)</param>
        ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
        ''' <returns>Lista de Empresas</returns>
        Public Function GetEmpresas(Optional filtroCNPJ As String = "", Optional filtroNome As String = "") As Object

            Dim consulta = EmpresaModel.VerTodos(filtroCNPJ, filtroNome)

            If consulta.Retorno.Sucesso = True Then
                Return consulta.ListaEmpresas
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Obtém uma Empresa
        ''' </summary>
        ''' <param name="id">Id da Empresa</param>
        ''' <returns>Empresa</returns>
        Public Function GetEmpresa(id As Integer)

            Dim consulta = EmpresaModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Cadastra uma nova Empresa e suas relações com os Associados
        ''' </summary>
        ''' <param name="empresa">Objeto do tipo Empresa</param>
        ''' <returns>Empresa</returns>
        Public Function PostEmpresa(empresa As EmpresaModel) As Object

            If empresa Is Nothing Then
                Return BadRequest("Objeto inválido no body da requisição!")
            End If

            Dim consulta = empresa.Cadastrar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Atualiza uma nova Empresa e suas relações com os Associados
        ''' </summary>
        ''' <param name="empresa">Objeto do tipo Empresa</param>
        ''' <returns>Empresa</returns>
        Public Function PutEmpresa(empresa As EmpresaModel) As Object

            If empresa Is Nothing Then
                Return BadRequest("Objeto inválido no body da requisição!")
            End If

            Dim consulta = EmpresaModel.Ver(empresa.Id)

            If consulta.Retorno.Sucesso = False Then
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

            consulta = consulta.Empresa.Atualizar()

            If consulta.Retorno.Sucesso = True Then
                Return consulta.Empresa
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Deleta uma nova Empresa e suas relações com os Associados
        ''' </summary>
        ''' <param name="id">Id da Empresa</param>
        ''' <returns>Empresa</returns>
        Public Function DeleteEmpresa(id As Integer) As Object

            Dim consulta = EmpresaModel.Ver(id)

            If Consulta.Retorno.Sucesso = False Then
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

            Consulta = Consulta.Empresa.Excluir()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Retorno.Mensagem
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

    End Class

End Namespace