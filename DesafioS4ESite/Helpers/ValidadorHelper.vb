Public Class ValidadorHelper

    Public Shared Function ValidarCPF(cpf As String) As Boolean
        If String.IsNullOrEmpty(cpf) Then
            Return False
        End If

        Dim multiplicador1 As Integer() = New Integer(8) {10, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim multiplicador2 As Integer() = New Integer(9) {11, 10, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim tempCpf As String
        Dim digito As String
        Dim soma As Integer
        Dim resto As Integer
        cpf = cpf.Trim()
        cpf = cpf.Replace(".", "").Replace("-", "")
        If cpf.Length <> 11 Then
            Return False
        End If
        tempCpf = cpf.Substring(0, 9)
        soma = 0

        For i As Integer = 0 To 8
            soma += Integer.Parse(tempCpf(i).ToString()) * multiplicador1(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = resto.ToString()
        tempCpf = tempCpf & digito
        soma = 0

        For i As Integer = 0 To 9
            soma += Integer.Parse(tempCpf(i).ToString()) * multiplicador2(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = digito & resto.ToString()
        Return cpf.EndsWith(digito)
    End Function

    Public Shared Function ValidarCNPJ(cnpj As String) As Boolean
        Dim multiplicador1 As Integer() = New Integer(11) {5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim multiplicador2 As Integer() = New Integer(12) {6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2}
        Dim soma As Integer
        Dim resto As Integer
        Dim digito As String
        Dim tempCnpj As String
        cnpj = cnpj.Trim()
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "")
        If cnpj.Length <> 14 Then
            Return False
        End If
        tempCnpj = cnpj.Substring(0, 12)
        soma = 0

        For i As Integer = 0 To 11
            soma += Integer.Parse(tempCnpj(i).ToString()) * multiplicador1(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = resto.ToString()
        tempCnpj = tempCnpj & digito
        soma = 0

        For i As Integer = 0 To 12
            soma += Integer.Parse(tempCnpj(i).ToString()) * multiplicador2(i)
        Next

        resto = soma Mod 11
        If resto < 2 Then
            resto = 0
        Else
            resto = 11 - resto
        End If

        digito = digito & resto.ToString()
        Return cnpj.EndsWith(digito)
    End Function

End Class
