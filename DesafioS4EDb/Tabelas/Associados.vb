Imports System.Text
Imports DesafioS4EDb.SQL

''' <summary>
''' Classe responsável pelo CRUD da tabela Associados
''' </summary>
Public Class Associados

    Public Sub New()
    End Sub

    ''' <summary>
    ''' Instancia um novo objeto da classe Assiciados
    ''' </summary>
    ''' <param name="id">Id do Associado</param>
    ''' <param name="nome">Nome do Associado</param>
    ''' <param name="cpf">CPF do Associado</param>
    ''' <param name="dataNascimento">Data de Nascimento do Associado</param>
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


    ''' <summary>
    ''' Obtém um objeto do tipo Associado pelo Id do Associado
    ''' </summary>
    ''' <param name="id">Id do Associado</param>
    ''' <returns>Retorna uma tupla com o objeto e o retorno da Consulta SQL</returns>
    Public Shared Function [Select](id As Integer) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarSelectPorId(New Associados, id)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Obtém um objeto do tipo Associado pelo CPF do Associado
    ''' </summary>
    ''' <param name="cpf">CPF do Associado</param>
    ''' <returns>Retorna uma tupla com o objeto e o retorno da Consulta SQL</returns>
    Public Shared Function [Select](cpf As String) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim where = GerarClausulaWherePorCPFNomeDataNascimento(cpf)

        Dim consulta = Script.GerarSelectAll(New Associados(), where)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Obtém uma lista de objetos do tipo Associado pelo CPF do Associado
    ''' </summary>
    ''' <param name="FiltroCPF">Filtro de CPF (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <param name="FiltroDataNascimentoInicio">Filtro de Data de Nascimento Incício (Opcional)</param>
    ''' <param name="FiltroDataNascimentoFim">Filtro de Data de Nascimento Fim (Opcional)</param>
    ''' <returns>Retorna uma tupla com uma lista de objetos e o retorno da Consulta SQL</returns>
    Public Shared Function SelectAll(Optional filtroCPF As String = "", Optional filtroNome As String = "", Optional filtroDataNascimentoInicio As Date = Nothing, Optional filtroDataNascimentoFim As Date = Nothing) As (ListaAssociadosDb As List(Of Associados), RetornoDb As RetornoDb)

        Dim where = GerarClausulaWherePorCPFNomeDataNascimento(filtroCPF, filtroNome, filtroDataNascimentoInicio, filtroDataNascimentoFim)

        Dim consulta = Script.GerarSelectAll(New Associados(), where)

        Return Comando.ObtenhaLista(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Insert no banco de dados do objeto instanciado
    ''' </summary>
    ''' <returns>Retorna uma tupla com o objeto e o retorno da Consulta SQL</returns>
    Public Function Insert() As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consulta = Script.GerarInsert(Me)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Update no banco de dados do objeto instanciado
    ''' </summary>
    ''' <param name="ListaEmpresasAssociadosDb">Lista de Empresas Associados</param>
    ''' <returns>Retorna uma tupla com o objeto e o retorno da Consulta SQL</returns>
    Public Function Update(listaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consultaEmpresasAssociados = EmpresasAssociados.ObtenhaListaDeConsultas(listaEmpresasAssociadosDb)

        consultaEmpresasAssociados.AppendLine(Script.GerarUpdate(Me))

        Dim consulta = Script.GerarTransaction(consultaEmpresasAssociados)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Realiza o Delete no banco de dados do objeto instanciado
    ''' </summary>
    ''' <param name="ListaEmpresasAssociadosDb">Lista de Empresas Associados</param>
    ''' <returns>Retorna uma tupla com o objeto e o retorno da Consulta SQL</returns>
    Public Function Delete(listaEmpresasAssociadosDb As List(Of EmpresasAssociados)) As (AssociadoDb As Associados, RetornoDb As RetornoDb)

        Dim consultaEmpresasAssociados = EmpresasAssociados.ObtenhaListaDeConsultas(listaEmpresasAssociadosDb)

        consultaEmpresasAssociados.AppendLine(Script.GerarDelete(Me))

        Dim consulta = Script.GerarTransaction(consultaEmpresasAssociados)

        Return Comando.Obtenha(Of Associados)(consulta)

    End Function

    ''' <summary>
    ''' Gerar a cláusula WHERE com base nos parâmetros informados
    ''' </summary>
    ''' <param name="FiltroCPF">Filtro de CPF (Opcional)</param>
    ''' <param name="FiltroNome">Filtro de Nome (Opcional)</param>
    ''' <param name="FiltroDataNascimentoInicio">Filtro de Data de Nascimento Incício (Opcional)</param>
    ''' <param name="FiltroDataNascimentoFim">Filtro de Data de Nascimento Fim (Opcional)</param>
    ''' <returns>Retorna uma string com a cláusula WHERE</returns>
    Private Shared Function GerarClausulaWherePorCPFNomeDataNascimento(Optional filtroCPF As String = "", Optional filtroNome As String = "", Optional filtroDataNascimentoInicio As Date = Nothing, Optional filtroDataNascimentoFim As Date = Nothing) As String

        Dim Where = New StringBuilder()

        If String.IsNullOrEmpty(filtroCPF) = False Then

            filtroCPF.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            End If

            Where.Append($"[Cpf] Like '%{filtroCPF}%' ")

        End If

        If String.IsNullOrEmpty(filtroNome) = False Then

            filtroNome.Replace("'", "´")

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"UPPER([Nome]) Like '%{filtroNome.ToUpper()}%' ")
        End If

        If filtroDataNascimentoInicio <> Nothing Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[DataNascimento] >= '{Helpers.FormatacaoHelper.FormatarDataTime_yyyyMMddHHmm(filtroDataNascimentoInicio)}' ")
        End If

        If filtroDataNascimentoFim <> Nothing Then

            If Where.Length = 0 Then
                Where.Append($" WHERE ")
            Else
                Where.Append($"AND ")
            End If

            Where.Append($"[DataNascimento] <= '{Helpers.FormatacaoHelper.FormatarDataTime_yyyyMMddHHmm(filtroDataNascimentoFim)}' ")
        End If

        Return Where.ToString()

    End Function


End Class
