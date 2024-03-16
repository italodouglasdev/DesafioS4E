Imports System.Web.Http

Namespace Controllers
    Public Class EmpresasController
        Inherits ApiController

        Public Function GetEmpresas(id As Integer) As Object

            Dim Consulta = EmpresaModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function


        Public Function GetEmpresas() As Object

            Dim Consulta = EmpresaModel.VerTodos()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.ListaEmpresas
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function PostEmpresas(empresa As EmpresaModel) As Object

            Dim Consulta = empresa.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function PutEmpresas(empresa As EmpresaModel) As Object

            Dim Consulta = empresa.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function



        Public Function DeleteEmpresas(empresa As EmpresaModel) As Object

            Dim Consulta = empresa.Salvar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return Consulta.Retorno.Mensagem
            End If

        End Function

    End Class

End Namespace