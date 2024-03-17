
Imports System.Text
Imports DesafioS4EDb.SQL

Public Class Associados
    Public Sub New()
    End Sub

    Public Sub New(id As Integer, nome As String, cpf As String, dataNascimento As Date)
        Me.Id = id
        Me.Nome = nome
        Me.Cpf = cpf
        Me.DataNascimento = dataNascimento
    End Sub

    Property Id As Integer
    Property Nome As String
    Property Cpf As String
    Property DataNascimento As DateTime



    Public Shared Function [Select](Id As Integer) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectPorId(New Associados, Id)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    Public Shared Function [Select](cpf As String) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWherePorCPFNomeDataNascimento(cpf)

        Dim consulta = Script.GerarSelectAll(New Associados(), Where)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    Public Shared Function SelectAll(Optional FiltroCPF As String = "", Optional FiltroNome As String = "", Optional FiltroDataNascimentoInicio As Date = Nothing, Optional FiltroDataNascimentoFim As Date = Nothing) As (ListaAssociadosDb As List(Of Associados), RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWherePorCPFNomeDataNascimento(FiltroCPF, FiltroNome, FiltroDataNascimentoInicio, FiltroDataNascimentoFim)

        Dim consulta = Script.GerarSelectAll(New Associados(), Where)

        Return Comando.ObtenhaLista(Of Associados)(consulta)

    End Function

    Public Function Insert() As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarInsert(Me)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    Public Function Update() As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarUpdate(Me)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    Public Function Delete() As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarDelete(Me)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function



    Private Shared Function GerarClausulaWherePorCPFNomeDataNascimento(Optional FiltroCPF As String = "", Optional FiltroNome As String = "", Optional FiltroDataNascimentoInicio As Date = Nothing, Optional FiltroDataNascimentoFim As Date = Nothing) As String

        Dim Where = New StringBuilder()

        If String.IsNullOrEmpty(FiltroCPF) = False Then

            FiltroCPF.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"[Cpf] Like '%{FiltroCPF}%' ")

        End If

        If String.IsNullOrEmpty(FiltroNome) = False Then

            FiltroNome.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"UPPER([Nome]) Like '%{FiltroNome.ToUpper()}%' ")
        End If

        If FiltroDataNascimentoInicio <> Nothing Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[DataNascimento] >= '{Util.FormatDataTime_yyyyMMddHHmm(FiltroDataNascimentoInicio)}' ")
        End If

        If FiltroDataNascimentoFim <> Nothing Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[DataNascimento] <= '{Util.FormatDataTime_yyyyMMddHHmm(FiltroDataNascimentoFim)}' ")
        End If

        Return Where.ToString()

    End Function


End Class
