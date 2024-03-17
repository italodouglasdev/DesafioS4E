Imports System.Web.Http

Namespace Controllers
    Public Class AssociadosController
        Inherits ApiController

        Public Function GetAssociados(Optional FiltroCPF As String = "", Optional FiltroNome As String = "", Optional FiltroDataNascimentoInicio As Date = Nothing, Optional FiltroDataNascimentoFim As Date = Nothing) As Object

            Dim Consulta = AssociadoModel.VerTodos(FiltroCPF, FiltroNome, FiltroDataNascimentoInicio, FiltroDataNascimentoFim)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.ListaAssociados
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function GetAssociado(id As Integer) As Object

            Dim Consulta = AssociadoModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function


        Public Function PostAssociado(Associado As AssociadoModel) As Object

            Dim Consulta = Associado.Cadastrar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function PutAssociado(Associado As AssociadoModel) As Object

            Dim Consulta = Associado.Atualizar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function DeleteAssociado(id As Integer) As Object

            Dim Consulta = AssociadoModel.Ver(id)

            If Consulta.Retorno.Sucesso = False Then
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If


            Consulta = Consulta.Associado.Excluir()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Retorno.Mensagem
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

    End Class
End Namespace