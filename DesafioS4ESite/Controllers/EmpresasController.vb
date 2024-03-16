Imports System.Web.Http

Namespace Controllers
    Public Class EmpresasController
        Inherits ApiController

        '<HttpGet>
        Public Function GetEmpresas(id As Integer) As Object

            Dim Consulta = EmpresaModel.Ver(id)

            If Consulta.Retorno.Sucesso = True Then
                Return Consulta.Empresa
            Else
                Return Consulta.Retorno.Mensagem
            End If



        End Function

        '<HttpGet>
        Public Function GetEmpresas() As List(Of EmpresaModel)

            Dim Consulta = EmpresaModel.VerTodos()

            Return Consulta.ListaEmpresas

        End Function


        '<HttpPost>
        Public Function PostEmpresas(empresa As EmpresaModel) As EmpresaModel

            Dim Consulta = empresa.Salvar()

            Return Consulta.Empresa

        End Function


        '<HttpPut>
        Public Function PutEmpresas(empresa As EmpresaModel) As EmpresaModel

            Dim Consulta = empresa.Salvar()

            Return Consulta.Empresa

        End Function


        '<HttpDelete>
        Public Function DeleteEmpresas(empresa As EmpresaModel) As EmpresaModel

            Dim Consulta = empresa.Salvar()

            Return Consulta.Empresa

        End Function

    End Class
End Namespace