Imports System.Text
Imports DesafioS4EDb.SQL

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


    Public Shared Function [Select](Id As Integer) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectPorId(New Empresas, Id)
        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    Public Shared Function [Select](cnpj As String) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWherePorNomeEOuCNPJ(cnpj)
        Dim consulta = Script.GerarSelectAll(New Empresas(), Where)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    Public Shared Function SelectAll(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As (ListaEmpresasDb As List(Of Empresas), RetornoDb As RetornoDb)

        Dim Where = GerarClausulaWherePorNomeEOuCNPJ(FiltroCNPJ, FiltroNome)

        Dim consulta = Script.GerarSelectAll(New Empresas(), Where)

        Return Comando.ObtenhaLista(Of Empresas)(consulta)

    End Function

    Public Function Insert() As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarInsert(Me)
        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    Public Function Update() As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarUpdate(Me)
        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    Public Function Delete() As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarDelete(Me)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function



    Private Shared Function GerarClausulaWherePorNomeEOuCNPJ(Optional FiltroCNPJ As String = "", Optional FiltroNome As String = "") As String
        Dim Where = New StringBuilder()

        If String.IsNullOrEmpty(FiltroNome) = False Then

            FiltroNome.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"UPPER([Nome]) Like '%{FiltroNome.ToUpper()}%' ")
        End If

        If String.IsNullOrEmpty(FiltroCNPJ) = False Then

            FiltroCNPJ.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[Cnpj] Like '%{FiltroCNPJ}%'")
        End If

        Return Where.ToString()
    End Function



End Class



