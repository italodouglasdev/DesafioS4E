Imports System.Web.Http

Namespace Controllers
    Public Class AssociadosController
        Inherits ApiController

        Public Function GetAssociados(id As Integer) As Object

            Dim Consulta = AssociadoModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return Consulta.Retorno.Mensagem
            End If



        End Function


        Public Function GetAssociados() As Object

            Dim Consulta = AssociadoModel.VerTodos()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.ListaAssociados
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function PostAssociados(Associado As AssociadoModel) As Object

            Dim Consulta = Associado.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function PutAssociados(Associado As AssociadoModel) As Object

            Dim Consulta = Associado.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function DeleteAssociados(Associado As AssociadoModel) As Object

            Dim Consulta = Associado.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Associado
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function

    End Class
End Namespace