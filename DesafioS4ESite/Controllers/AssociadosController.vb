Imports System.Web.Http

Namespace Controllers

    ''' <summary>
    ''' Cadastro de Associados
    ''' </summary>
    Public Class AssociadosController
        Inherits ApiController

        ''' <summary>
        ''' Obtém uma lista de Associados
        ''' </summary>
        ''' <param name="FiltroCPF">Filtro de CPF (Opcional)</param>
        ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
        ''' <param name="FiltroDataNascimentoInicio">Filtro de Data de Nascimento Incício (Opcional)</param>
        ''' <param name="FiltroDataNascimentoFim">Filtro de Data de Nascimento Fim (Opcional)</param>
        ''' <returns>Lista de Associados</returns>
        Public Function GetAssociados(Optional filtroCPF As String = "", Optional filtroNome As String = "", Optional filtroDataNascimentoInicio As Date = Nothing, Optional filtroDataNascimentoFim As Date = Nothing) As Object

            Dim consulta = AssociadoModel.VerTodos(filtroCPF, filtroNome, filtroDataNascimentoInicio, filtroDataNascimentoFim)

            If consulta.Retorno.Sucesso = True Then
                Return consulta.ListaAssociados
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Obtém um Associado
        ''' </summary>
        ''' <param name="id">Id do Associado</param>
        ''' <returns>Associado</returns>
        Public Function GetAssociado(id As Integer) As Object

            Dim consulta = AssociadoModel.Ver(id)

            If consulta.Retorno.Sucesso = True Then
                Return consulta.Associado
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Cadastra um novo Associado e suas relações com as Empresas
        ''' </summary>
        ''' <param name="Associado">Objeto do tipo Associado</param>
        ''' <returns>Associado</returns>
        Public Function PostAssociado(associado As AssociadoModel) As Object

            If associado Is Nothing Then
                Return BadRequest("Objeto inválido no body da requisição!")
            End If

            Dim consulta = associado.Cadastrar()

            If consulta.Retorno.Sucesso = True Then
                Return consulta.Associado
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Atualiza o cadastro do Associado e suas relações com as Empresas
        ''' </summary>
        ''' <param name="Associado">Objeto do tipo Associado</param>
        ''' <returns>Associado</returns>
        Public Function PutAssociado(associado As AssociadoModel) As Object

            If associado Is Nothing Then
                Return BadRequest("Objeto inválido no body da requisição!")
            End If

            Dim consulta = AssociadoModel.Ver(associado.Id)

            If consulta.Retorno.Sucesso = False Then
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

            consulta = consulta.Associado.Atualizar()

            If consulta.Retorno.Sucesso = True Then
                Return consulta.Associado
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

        ''' <summary>
        ''' Deleta o cadastro do Associado e suas relações com as Empresas
        ''' </summary>
        ''' <param name="id">Id do Associado</param>
        ''' <returns>Associado</returns>
        Public Function DeleteAssociado(id As Integer) As Object

            Dim consulta = AssociadoModel.Ver(id)

            If consulta.Retorno.Sucesso = False Then
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

            consulta = consulta.Associado.Excluir()

            If consulta.Retorno.Sucesso = True Then
                Return consulta.Retorno.Mensagem
            Else
                Return BadRequest(consulta.Retorno.Mensagem)
            End If

        End Function

    End Class

End Namespace