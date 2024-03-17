Imports System.Data.SqlClient

Namespace SQL
    Public Class Comando

        ''' <summary>
        ''' Obtém um único objeto que for gerado após a execução do SQL
        ''' </summary>
        ''' <typeparam name="T">Tipo do objeto que sera retronado.</typeparam>
        ''' <param name="Comando">Comando SQL ex: SELECT * FROM [Tabela] WHERE [Codigo] = '0'</param>
        ''' <returns>Objeto do Tipo T</returns>
        Friend Shared Function Obtenha(Of T As New)(Comando As String) As (Objeto As T, Retorno As RetornoDb)
            Dim objeto As New T()

            Dim ResultadoExecucao = Executar(Of T)(Comando)

            If ResultadoExecucao.Retorno.Sucesso AndAlso ResultadoExecucao.Objetos.Count > 0 Then
                objeto = CType(ResultadoExecucao.Objetos(0), T)
            End If

            Return (objeto, ResultadoExecucao.Retorno)

        End Function

        ''' <summary>
        ''' Obtém uma lista de objetos que forem gerados após a execução do SQL
        ''' </summary>
        ''' <typeparam name="T">Tipo do objeto que sera retronado na lista</typeparam>
        ''' <param name="Comando">Comando SQL ex: SELECT * FROM [Tabela]</param>
        ''' <returns>Lista de objetos do Tipo T</returns>
        Friend Shared Function ObtenhaLista(Of T As New)(Comando As String) As (ListaObjetos As List(Of T), Retorno As RetornoDb)
            Dim lista As New List(Of T)

            Dim ResultadoExecucao = Executar(Of T)(Comando)

            If ResultadoExecucao.Retorno.Sucesso Then
                For Each obj As Object In ResultadoExecucao.Objetos
                    lista.Add(CType(obj, T))
                Next
            End If

            Return (lista, ResultadoExecucao.Retorno)
        End Function

        ''' <summary>
        ''' Realiza a comunicação com o banco de dados e executa o comando SQL desejado
        ''' </summary>
        ''' <param name="Comando">Comando SQL ex: SELECT * FROM [Tabela]</param>
        ''' <returns>Retorna um objeto do tipo SQLRetorno</returns>
        Private Shared Function Executar(Of T As New)(Comando As String) As (Objetos As Object, Retorno As RetornoDb)

            Dim retorno As New RetornoDb()

            Dim ListaObjetos As New List(Of Object)()
            Dim conexaoMSDE As New SqlConnection()
            Dim comandoSQL As New SqlCommand()
            conexaoMSDE = New SqlConnection(Configuracao.STRING_CONEXAO)
            comandoSQL.Connection = conexaoMSDE
            comandoSQL.CommandText = Comando

            Try
                Dim dr As SqlDataReader
                conexaoMSDE.Open()
                dr = comandoSQL.ExecuteReader()

                While dr.Read()
                    ListaObjetos.Add(DataReaderParaObjeto(Of T)(dr))
                End While

                Return (ListaObjetos, New RetornoDb(True, "Operação realizada com sucesso!"))
            Catch ex As Exception

                Return (ListaObjetos, New RetornoDb(False, TratarMensagemExcecao(ex.Message)))

            Finally
                conexaoMSDE.Close()
                conexaoMSDE.Dispose()
            End Try

        End Function

        ''' <summary>
        ''' Realiza o preenchimento dos dados do Objeto com base nos dados existentes no DataReader.
        ''' </summary>
        ''' <param name="dr">SQL Data Reader, onde estão os dados que vem do SQL Server</param>
        ''' <returns></returns>
        Private Shared Function DataReaderParaObjeto(Of T As New)(dr As SqlDataReader) As T
            Dim objeto As New T()

            For Each Propriedade In objeto.GetType().GetProperties()
                Dim Tipo = Propriedade.PropertyType.Name
                Dim Info = objeto.GetType().GetProperty(Propriedade.Name)

                If Tipo.ToLower() = "string" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), String), "")
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                ElseIf Tipo.ToLower() = "int32" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), Integer), 0)
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                ElseIf Tipo.ToLower() = "boolean" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), Boolean), False)
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                ElseIf Tipo.ToLower() = "datetime" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), DateTime), New DateTime())
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                ElseIf Tipo.ToLower() = "decimal" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), Decimal), 0)
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                ElseIf Tipo.ToLower() = "double" Then
                    Dim Valor = If(Not String.IsNullOrEmpty(dr(Propriedade.Name).ToString()), DirectCast(dr(Propriedade.Name), Double), 0)
                    Info.SetValue(objeto, Convert.ChangeType(Valor, Info.PropertyType), Nothing)
                End If
            Next

            Return objeto
        End Function


        Private Shared Function TratarMensagemExcecao(mensagem As String)

            If mensagem.Contains("PK_EmpresasAssociados") Then
                Return "Já existe uma relação entro a Empresa e o Associado informados!"
            End If

            If mensagem.Contains("FK_EmpresasAssociados_Empresas") Then
                Return "Não foi possível realizar a exclusão da Empresa, pois ela possui vínculo com um ou mais EmpresaAssociados!"
            End If

            If mensagem.Contains("FK_EmpresasAssociados_Associados") Then
                Return "Não foi possível realizar a exclusão do Assiciado, pois ela possui vínculo com uma ou mais Empresas!"
            End If

            If mensagem.Contains("UK_Empresas_Cnpj") Then
                Return "O CNPJ informado já possui um cadastro!"

            End If
            If mensagem.Contains("UK_Associados_Cpf") Then
                Return "O CPF informado já possui um cadastro!"
            End If

            Return mensagem

        End Function




    End Class
End Namespace
