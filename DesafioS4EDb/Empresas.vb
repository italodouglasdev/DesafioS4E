
Imports System.Data.SqlClient

Public Class Empresas
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
    Property Retorno As Retorno


    Public Shared Function Selecionar(Id As Integer) As Empresas

        Dim empresa = New Empresas()

        Dim connection As New SqlConnection("Initial Catalog=DesafioS4EDb; User ID=caririfest; Password=Y5hAmR9cJNKmGeY; Data Source=localhost")
        Dim query As String = $"SELECT Id, Nome, Cnpj FROM Empresas WHERE Id = {Id}"
        Dim command As New SqlCommand(query, connection)

        Try

            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()
            reader.Read()

            empresa = New Empresas() With 
                {
                    .Id = Convert.ToInt32(reader("Id")),
                    .Nome = Convert.ToString(reader("Nome")),
                    .Cnpj = Convert.ToString(reader("Cnpj"))
                }

            empresa.Retorno = New Retorno(True, "Consulta realizada com sucesso!")

        Catch ex As Exception
            empresa.Retorno = New Retorno(False, $"Falha ao realizar Consulta! Detalhes: {ex.Message}")
        Finally
            connection.Close()
            connection.Dispose()
        End Try

        Return empresa

    End Function

    Public Shared Function ReadEmpresas() As List(Of Empresas)
        Dim empresas As New List(Of Empresas)()

        Using connection As New SqlConnection("Initial Catalog=DesafioS4EDb; User ID=caririfest; Password=Y5hAmR9cJNKmGeY; Data Source=localhost")
            Dim query As String = "SELECT Id, Nome, Cnpj FROM Empresas"
            Dim command As New SqlCommand(query, connection)

            connection.Open()
            Dim reader As SqlDataReader = command.ExecuteReader()

            While reader.Read()
                Dim empresa As New Empresas() With {
                    .Id = Convert.ToInt32(reader("Id")),
                    .Nome = reader("Nome").ToString(),
                    .Cnpj = reader("Cnpj").ToString()
                }
                empresas.Add(empresa)
            End While
        End Using

        Return empresas
    End Function


End Class
