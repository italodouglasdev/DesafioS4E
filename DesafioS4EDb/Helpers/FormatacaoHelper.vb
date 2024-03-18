
Namespace Helpers

    Public Class FormatacaoHelper


        Public Shared Function FormatarDataTime_yyyyMMddHHmm(ByVal Valor As String) As String
            Dim ValorConvertido As DateTime
            DateTime.TryParse(Valor, ValorConvertido)

            If ValorConvertido.ToString("yyyy") = "0001" Then
                ValorConvertido = ValorConvertido.AddYears(1999)
            End If

            Return ValorConvertido.ToString("yyyy-MM-dd HH:mm:ss")
        End Function


        Public Shared Function FormatarMoney(ByVal Valor As String) As String
            Dim retorno As String = Valor.ToString()

            retorno = retorno.Replace(".", "").Replace(",", ".")

            Return retorno
        End Function


        Public Shared Function FormatarDouble(ByVal Valor As String) As String
            Dim retorno As String = Valor.ToString()

            retorno = retorno.Replace(".", "").Replace(",", ".")

            Return retorno
        End Function


    End Class

End Namespace


