Imports System.Web.Http

Namespace Controllers
    Public Class EmpresasController
        Inherits ApiController


        <HttpGet>
        Public Function VerTodas() As List(Of DesafioS4EDb.Empresas)

            Dim Empresas = DesafioS4EDb.Empresas.ReadEmpresas()

            Return Empresas

        End Function


        <HttpGet>
        Public Function Ver(ByVal id As Integer) As DesafioS4EDb.Empresas

            Dim Empresa = DesafioS4EDb.Empresas.Selecionar(id)
            Return Empresa

        End Function


        <HttpPost>
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub


        <HttpPut>
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub


        <HttpDelete>
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub

    End Class
End Namespace