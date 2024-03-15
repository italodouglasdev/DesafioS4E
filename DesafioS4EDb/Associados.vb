
Public Class Associados
    Inherits Script

    Property Id As Integer
    Property Nome As String
    Property Cpf As String
    Property DataNascimento As DateTime


    Public Function Tabela() As String

        Return "[Associados]"

    End Function

    Public Function Colunas() As String

        Return "[Id], [Nome], [Cpf], [DataNascimento]"

    End Function

End Class
