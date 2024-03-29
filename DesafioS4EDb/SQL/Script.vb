﻿Imports System.Reflection
Imports System.Text

Namespace SQL

    Friend Class Script

        ''' <summary>
        ''' Obtém o nome da Tabela com base no Objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para obter o nome da Tabela do banco de dados</param>
        ''' <returns>Nome da Tabela em formato de texto</returns>
        Private Shared Function ObtenhaNomeTabela(ByVal _Objeto As Object) As String
            Return _Objeto.GetType().Name.ToString()
        End Function

        ''' <summary>
        ''' Obtém a lista de nomes de colunas com base no Objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para obter os nomes das colunas da tabela do banco de dados</param>
        ''' <param name="AdicionarColunaId">Caso true, adiciona também a Coluna Id aos nomes das colunas</param>
        ''' <returns>Nomes das Colunas em formato de texto</returns>
        Private Shared Function ObtenhaNomesColunas(ByVal _Objeto As Object, Optional ByVal AdicionarColunaId As Boolean = False) As String
            Dim nomeColunas As New StringBuilder()

            Dim nomeColuna = ObtenhaNomeTabela(_Objeto)

            For Each Propriedade In ObtenhaPropriedadesDoObjeto(_Objeto)

                If (Propriedade.Name = "Id" AndAlso AdicionarColunaId = True) OrElse (Propriedade.Name <> "Id") Then
                    nomeColunas.Append($"[{nomeColuna}].[{Propriedade.Name}]")

                    If Not Propriedade.Equals(ObtenhaPropriedadesDoObjeto(_Objeto).Last()) Then
                        nomeColunas.Append($", ")
                    End If
                End If
            Next

            Return nomeColunas.ToString()
        End Function

        ''' <summary>
        ''' Obtém a lista de nomes de colunas para o OUTPUT com base no Objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para obter os nomes das colunas da tabela do banco de dados</param>
        ''' <returns>Nomes das Colunas em formato de texto</returns>
        Private Shared Function ObtenhaNomesColunasOutput(ByVal _Objeto As Object) As String
            Dim nomeColunas As New StringBuilder()

            For Each Propriedade In ObtenhaPropriedadesDoObjeto(_Objeto)
                nomeColunas.Append($"Inserted.[{Propriedade.Name}]")

                If Not Propriedade.Equals(ObtenhaPropriedadesDoObjeto(_Objeto).Last()) Then
                    nomeColunas.Append($", ")
                End If
            Next

            Return nomeColunas.ToString()
        End Function

        ''' <summary>
        ''' Obtém a lista de nomes de colunas para o VALUES com base no Objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para obter os nomes das colunas da tabela do banco de dados</param>
        ''' <returns>Nomes das Colunas em formato de texto</returns>
        Private Shared Function ObtenhaNomesColunasValues(ByVal _Objeto As Object) As String
            Dim nomeColunas As New StringBuilder()


            For Each Propriedade In ObtenhaPropriedadesDoObjeto(_Objeto)
                Dim Tipo = Propriedade.PropertyType.Name

                If Propriedade.Name <> "Id" Then
                    Dim valor = ObtenhaValorDaPropriedade(_Objeto, Propriedade)

                    If Tipo = "DateTime" Then
                        valor = Helpers.FormatacaoHelper.FormatarDataTime_yyyyMMddHHmm(valor)
                    ElseIf Tipo = "Decimal" Then
                        valor = Helpers.FormatacaoHelper.FormatarMoney(valor)
                    ElseIf Tipo = "Double" Then
                        valor = Helpers.FormatacaoHelper.FormatarDouble(valor)
                    End If

                    If Tipo = "Double" OrElse Tipo = "Decimal" Then
                        nomeColunas.Append($"{valor}")
                    Else
                        nomeColunas.Append($"'{valor}'")
                    End If

                    If Not Propriedade.Equals(ObtenhaPropriedadesDoObjeto(_Objeto).Last()) Then
                        nomeColunas.Append($", ")
                    End If
                End If
            Next

            Return nomeColunas.ToString()
        End Function

        ''' <summary>
        ''' Obtém a lista de nomes de colunas para o UPDATE com base no Objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para obter os nomes das colunas da tabela do banco de dados</param>
        ''' <returns>Nomes das Colunas em formato de texto</returns>
        Private Shared Function ObtenhaNomesColunasUpdate(ByVal _Objeto As Object) As String
            Dim nomeColunas As New StringBuilder()

            For Each Propriedade In ObtenhaPropriedadesDoObjeto(_Objeto)
                Dim Tipo = Propriedade.PropertyType.Name

                If Propriedade.Name <> "Id" Then
                    Dim valor = ObtenhaValorDaPropriedade(_Objeto, Propriedade)

                    If Tipo = "DateTime" Then
                        valor = Helpers.FormatacaoHelper.FormatarDataTime_yyyyMMddHHmm(valor)
                    ElseIf Tipo = "Decimal" Then
                        valor = Helpers.FormatacaoHelper.FormatarMoney(valor)
                    ElseIf Tipo = "Double" Then
                        valor = Helpers.FormatacaoHelper.FormatarDouble(valor)
                    End If

                    If Tipo = "Double" OrElse Tipo = "Decimal" Then
                        nomeColunas.Append($"[{Propriedade.Name}] = {valor}")
                    Else
                        nomeColunas.Append($"[{Propriedade.Name}] = '{valor}'")
                    End If

                    If Not Propriedade.Equals(ObtenhaPropriedadesDoObjeto(_Objeto).Last()) Then
                        nomeColunas.Append($", ")
                    End If
                End If
            Next

            Return nomeColunas.ToString()
        End Function

        ''' <summary>
        ''' Obtém o valor da propriedade do objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto que se deseja obter o valor</param>
        ''' <param name="Propriedade">Propriedade que se deseja obter o valor</param>
        ''' <returns>Valor contido na Propriedade informada</returns>
        Private Shared Function ObtenhaValorDaPropriedade(ByVal _Objeto As Object, ByVal Propriedade As PropertyInfo) As String
            Dim valor = Propriedade.GetValue(_Objeto, Nothing)

            If valor Is Nothing Then
                Return String.Empty
            End If

            Return valor.ToString().Replace("'", "´")
        End Function

        ''' <summary>
        ''' Obtém o valor do campo Id genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que se deseja obter o valor</param>
        ''' <returns>Valor contido na Propriedade informada</returns>
        Private Shared Function ObtenhaValorDoId(ByVal _Objeto As Object) As Integer
            Dim valor = 0

            For Each Propriedade In ObtenhaPropriedadesDoObjeto(_Objeto)
                If Propriedade.Name = "Id" Then
                    Integer.TryParse(ObtenhaValorDaPropriedade(_Objeto, Propriedade), valor)
                    Return valor
                End If
            Next

            Return valor
        End Function


        ''' <summary>
        ''' Gera comando Select Por Id genérico para qualquer tabela
        ''' </summary>
        ''' <param name="Objeto">Objeto que será usado como base para gerar o script desejado</param>
        ''' <param name="Id">Id</param>
        ''' <returns>Retorna um texto no formato: SELECT [Coluna1] ... [Coluna(n)] FROM [Tabela] WHERE [Id] = 0</returns>
        Friend Shared Function GerarSelectPorId(Objeto As Object, Id As Integer) As String

            Return $"SELECT {ObtenhaNomesColunas(Objeto, True)} FROM [{ObtenhaNomeTabela(Objeto)}] WHERE [Id] = '" & Id & "';"

        End Function

        ''' <summary>
        ''' Gera comando Select Todos genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para gerar o script desejado</param>
        ''' <returns>Retorna um texto no formato: SELECT [Coluna1] ... [Coluna(n)] FROM [Tabela]</returns>
        Friend Shared Function GerarSelectAll(ByVal _Objeto As Object, Optional ByVal Where As String = "") As String
            Return $"SELECT {ObtenhaNomesColunas(_Objeto, True)} FROM [{ObtenhaNomeTabela(_Objeto)}] {Where};"
        End Function

        ''' <summary>
        ''' Gera comando Select Todos genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para gerar o script desejado</param>
        ''' <returns>Retorna um texto no formato: SELECT [Coluna1] ... [Coluna(n)] FROM [Tabela]</returns>
        Friend Shared Function GerarSelectAllTop(ByVal _Objeto As Object, ByVal Top As Integer, Optional ByVal Where As String = "") As String
            Return $"SELECT TOP {Top} {ObtenhaNomesColunas(_Objeto, True)} FROM [{ObtenhaNomeTabela(_Objeto)}] {Where} ;"
        End Function

        ''' <summary>
        ''' Gera comando Insert genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para gerar o script desejado </param>
        ''' <param name="GerarOutPut"> Deve gerar ou não o OUTPUT para que o Obejeto seja retornado ou não</param>
        ''' <returns>Retorna um texto no formato: INSERT INTO [Tabela] ([Coluna1] ... [Coluna(n)]) OUTPUT (Inserted.[Coluna1] ... Inserted.[Coluna(n)]) VALUES ('Valor1' ... 'Valor(n)')</returns>
        Friend Shared Function GerarInsert(ByVal _Objeto As Object, Optional GerarOutPut As Boolean = True) As String

            Dim Script = New StringBuilder()

            Script.Append($"INSERT INTO {ObtenhaNomeTabela(_Objeto)} ({ObtenhaNomesColunas(_Objeto)})")

            If GerarOutPut = True Then
                Script.Append($"OUTPUT {ObtenhaNomesColunasOutput(_Objeto)}")
            End If

            Script.Append($" VALUES ({ObtenhaNomesColunasValues(_Objeto)});")

            Return Script.ToString()

        End Function

        ''' <summary>
        ''' Gera comando Update genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para gerar o script desejado </param>
        ''' <param name="GerarOutPut"> Deve gerar ou não o OUTPUT para que o Obejeto seja retornado ou não</param>
        ''' <returns>Retorna um texto no formato: UPDATE [Tabela] SET ([Coluna1] = 'Valor1', ... [Coluna(n)] = 'Valor(n)') WHERE [Id] = 0 </returns>
        Friend Shared Function GerarUpdate(ByVal _Objeto As Object, Optional GerarOutPut As Boolean = True) As String

            Dim Id = ObtenhaValorDoId(_Objeto)

            Dim Script = New StringBuilder()

            Script.Append($"UPDATE {ObtenhaNomeTabela(_Objeto)} SET {ObtenhaNomesColunasUpdate(_Objeto)} WHERE [Id] = '{Id}';")

            If GerarOutPut = True Then
                Script.Append(GerarSelectPorId(_Objeto, Id))
            End If

            Return Script.ToString()

        End Function

        ''' <summary>
        ''' Gera comando Delete genérico para qualquer tabela
        ''' </summary>
        ''' <param name="_Objeto">Objeto que será usado como base para gerar o script desejado </param>
        ''' <param name="Where">Cláusula Where para realizar o filtro no Script</param>
        ''' <returns>Retorna um texto no formato: DELETE FROM [Tabela] WHERE [Id] = 0 </returns>
        Friend Shared Function GerarDelete(ByVal _Objeto As Object, Optional Where As String = "") As String

            Dim Script = New StringBuilder()

            If String.IsNullOrEmpty(Where) Then
                Dim Id = ObtenhaValorDoId(_Objeto)
                Where = $"WHERE [Id] = '{ObtenhaValorDoId(_Objeto)}'"
            End If

            Script.Append($"DELETE FROM {ObtenhaNomeTabela(_Objeto)} {Where};")

            Return Script.ToString()

        End Function

        ''' <summary>
        ''' Método responsável por ecapsular os Scripts para que caso haja algum erro na excução, tudo seja desfeito
        ''' </summary>
        ''' <param name="listaDeScripts">Lista de scripts para encapsular </param>
        ''' <returns>Retorna uma string com a Transaction e os scripts encapsulados </returns>
        Friend Shared Function GerarTransaction(listaDeScripts As StringBuilder) As String

            Dim sqlTransaction = New StringBuilder()

            sqlTransaction.AppendLine("BEGIN TRANSACTION;")

            sqlTransaction.AppendLine(listaDeScripts.ToString())

            sqlTransaction.AppendLine("IF @@ERROR = 0")
            sqlTransaction.AppendLine("    COMMIT;")
            sqlTransaction.AppendLine("ELSE")
            sqlTransaction.AppendLine("    ROLLBACK;")

            Return sqlTransaction.ToString()

        End Function

        ''' <summary>
        ''' Obtém a lista de propriedades do objeto informado
        ''' </summary>
        ''' <param name="_Objeto">Objeto para obtenção das propriedades</param>
        ''' <returns>Lista de Propriedades do Objeto</returns>
        Private Shared Function ObtenhaPropriedadesDoObjeto(_Objeto As Object) As List(Of PropertyInfo)

            Dim PropriedadesDoObjeto = _Objeto.GetType().GetProperties().ToList()

            PropriedadesDoObjeto.RemoveAll(Function(x) x.PropertyType.Name = "EnumInstrucaoDb")

            Return PropriedadesDoObjeto

        End Function


    End Class

End Namespace
