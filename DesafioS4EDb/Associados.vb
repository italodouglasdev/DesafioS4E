
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

    Public Shared Function SelectAll() As (ListaAssociadosDb As List(Of Associados), RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectAll(New Associados())
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


End Class
