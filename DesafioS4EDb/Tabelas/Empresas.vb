Imports System.Text
Imports DesafioS4EDb.SQL

''' <summary>
''' Classe responsável pelo CRUD da tabela Empresas
''' </summary>
Public Class Empresas

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto da classe Empresas
    ''' </summary>
    ''' <param name="id">Id da Empresa</param>
    ''' <param name="nome">Nome da Empresa</param>
    ''' <param name="cnpj">CNPJ da Empresa</param>
    Public Sub New(id As Integer, nome As String, cnpj As String)
        Me.Id = id
        Me.Nome = nome
        Me.Cnpj = cnpj
    End Sub

    ''' <summary>
    ''' Id do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Id As Integer

    ''' <summary>
    ''' Nome do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Nome As String

    ''' <summary>
    ''' CNPJ do Associado
    ''' </summary>
    ''' <returns></returns>
    Property Cnpj As String




    ''' <summary>
    ''' Obtém um objeto do tipo Empresa pelo Id da Empresa
    ''' </summary>
    ''' <param name="id">Id da Empresa</param>
    ''' <returns>Tupla (EmpresaDb As Empresas, RetornoDb As RetornoDb)</returns>
    Public Shared Function [Select](id As Integer) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectPorId(New Empresas, id)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Obtém um objeto do tipo Empresa pelo CNPJ da Empresa
    ''' </summary>
    ''' <param name="cnpj">CNPJ da Empresa</param>
    ''' <returns>Tupla (EmpresaDb As Empresas, RetornoDb As RetornoDb)</returns>
    Public Shared Function [Select](cnpj As String) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim where = GerarClausulaWherePorNomeEOuCNPJ(cnpj)

        Dim consulta = Script.GerarSelectAll(New Empresas(), where)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Obtém uma lista de objetos do tipo Empresa pelo CNPJ da Empresa
    ''' </summary>
    ''' <param name="FiltroCNPJ">Filtro de CNPJ (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <returns>Tupla (ListaEmpresasDb As List(Of Empresas), RetornoDb As RetornoDb)</returns>
    Public Shared Function SelectAll(Optional filtroCNPJ As String = "", Optional filtroNome As String = "") As (ListaEmpresasDb As List(Of Empresas), RetornoDb As RetornoDb)

        Dim where = GerarClausulaWherePorNomeEOuCNPJ(filtroCNPJ, filtroNome)

        Dim consulta = Script.GerarSelectAll(New Empresas(), where)

        Return Comando.ObtenhaLista(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Insert no banco de dados do objeto instanciado
    ''' </summary>
    ''' <returns>Tupla (EmpresaDb As Empresas, RetornoDb As RetornoDb)</returns>
    Public Function Insert() As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarInsert(Me)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Update no banco de dados do objeto instanciado
    ''' </summary>
    ''' <param name="ListaEmpresasAssociadosDb">Lista de EmpresasAssociados</param>
    ''' <returns>Tupla (EmpresaDb As Empresas, RetornoDb As RetornoDb)</returns>
    Public Function Update(listaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consultaEmpresasAssociados = EmpresasAssociados.ObtenhaListaDeConsultas(listaEmpresasAssociadosDb)

        consultaEmpresasAssociados.AppendLine(Script.GerarUpdate(Me))

        Dim consulta = Script.GerarTransaction(consultaEmpresasAssociados)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Delete no banco de dados do objeto instanciado
    ''' </summary>
    ''' <param name="ListaEmpresasAssociadosDb">Lista de EmpresasAssociados</param>
    ''' <returns>Tupla (EmpresaDb As Empresas, RetornoDb As RetornoDb)</returns>
    Public Function Delete(listaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As (EmpresaDb As Empresas, RetornoDb As RetornoDb)

        Dim consultaEmpresasAssociados = EmpresasAssociados.ObtenhaListaDeConsultas(listaEmpresasAssociadosDb)

        consultaEmpresasAssociados.AppendLine(Script.GerarDelete(Me))

        Dim consulta = Script.GerarTransaction(consultaEmpresasAssociados)

        Return Comando.Obtenha(Of Empresas)(consulta)

    End Function

    ''' <summary>
    ''' Gerar a cláusula WHERE com base nos parâmetros informados
    ''' </summary>
    ''' <param name="FiltroCNPJ">Filtro de CNPJ (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <returns>Retorna uma string com a cláusula WHERE</returns>
    Private Shared Function GerarClausulaWherePorNomeEOuCNPJ(Optional filtroCNPJ As String = "", Optional filtroNome As String = "") As String
        Dim Where = New StringBuilder()

        If String.IsNullOrEmpty(filtroNome) = False Then

            filtroNome.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"UPPER([Nome]) Like '%{filtroNome.ToUpper()}%' ")
        End If

        If String.IsNullOrEmpty(filtroCNPJ) = False Then

            filtroCNPJ.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[Cnpj] Like '%{filtroCNPJ}%'")
        End If

        Return Where.ToString()
    End Function


End Class



