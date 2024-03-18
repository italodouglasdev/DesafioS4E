
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

    Public Shared Function ConverterStringParaDateTime(ByVal data As String) As DateTime

        Dim dataValida As New DateTime

        Try
            If String.IsNullOrEmpty(data) Then
                Return New DateTime(1, 1, 1)
            End If

            If Not String.IsNullOrEmpty(data) Then
                Date.TryParse(data, dataValida)
            End If

            Return dataValida.ToString("yyyy-MM-dd")

        Catch ex As Exception

            Return New DateTime(1, 1, 1)
        End Try

    End Function

End Class
