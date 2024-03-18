
Public Class ConversorHelper

    Public Shared Function ConverterStringParaInteiro(ByVal valor As String) As Integer
        Dim CodigoInteiro As Integer = 0

        Try
            If String.IsNullOrEmpty(valor) Then
                Return CodigoInteiro
            End If

            If Not String.IsNullOrEmpty(valor) Then
                Integer.TryParse(valor.ToString(), CodigoInteiro)
            End If

            Return CodigoInteiro
        Catch ex As Exception
            Return CodigoInteiro
        End Try
    End Function

End Class
