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

    Public Shared Function SelectAll() As (ListaEmpresasDb As List(Of Empresas), RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectAll(New Empresas())
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


End Class



