Imports System.Web.Http

Namespace Controllers
    Public Class EmpresasController
        Inherits ApiController

        Public Function GetEmpresas(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As Object

            Dim Consulta = EmpresaModel.VerTodos(FiltroCNPJ, FiltroNome)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.ListaEmpresas
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function GetEmpresa(id As Integer) As Object

            Dim Consulta = EmpresaModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function


        Public Function PostEmpresa(empresa As EmpresaModel) As Object

            Dim Consulta = empresa.Cadastrar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function PutEmpresa(empresa As EmpresaModel) As Object

            Dim Consulta = empresa.Atualizar()

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return BadRequest(Consulta.Retorno.Mensagem)
            End If

        End Function

        Public Function DeleteEmpresa(id As Integer) As Object

            Dim Consulta = EmpresaModel.Ver(id)

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