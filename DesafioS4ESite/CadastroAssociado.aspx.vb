Public Class CadastroAssociado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AtualizarTabelaAssociados()
    End Sub

    Protected Sub SalvarAssociado(sender As Object, e As EventArgs)

        Dim associado = Me.ObtenhaDadosFormulario()

        If Associado.Id = 0 Then

            Dim consulta = Associado.Cadastrar()

            If consulta.Retorno.Sucesso Then
                Me.PreencheDadosFormulario(consulta.Associado)
                ErrorMessage.Visible = False
            Else
                FailureText.Text = consulta.Retorno.Mensagem
                ErrorMessage.Visible = True
            End If

        Else

            Dim consulta = Associado.Atualizar()

            If consulta.Retorno.Sucesso Then
                Me.PreencheDadosFormulario(consulta.Associado)
                ErrorMessage.Visible = False
            Else
                Me.PreencheDadosFormulario(consulta.Associado)
                FailureText.Text = consulta.Retorno.Mensagem
                ErrorMessage.Visible = True
            End If

        End If

        Me.AtualizarTabelaAssociados()

    End Sub

    Private Sub EditarAssociado(sender As Object, e As EventArgs)

        Dim button As Button = DirectCast(sender, Button)
        Dim row As HtmlTableRow = DirectCast(button.Parent.Parent, HtmlTableRow)
        Dim id As Integer = ConversorHelper.ConverterStringParaInteiro(row.Cells(0).InnerText)

        Dim consultaAssociado = AssociadoModel.Ver(id)
        Me.PreencheDadosFormulario(consultaAssociado.Associado)

    End Sub

    Protected Sub ExcluirAssociado(sender As Object, e As EventArgs)

        Dim associado = Me.ObtenhaDadosFormulario()

        Dim consulta = Associado.Excluir()

        If consulta.Retorno.Sucesso Then
            Me.PreencheDadosFormulario(consulta.Associado)
            ErrorMessage.Visible = False
        Else
            Me.PreencheDadosFormulario(consulta.Associado)
            FailureText.Text = consulta.Retorno.Mensagem
            ErrorMessage.Visible = True
        End If

        Me.AtualizarTabelaAssociados()

    End Sub

    Protected Sub LimparFormulario()
        txtId.Text = $"{0}"
        txtCPF.Text = ""
        txtNome.Text = ""
        txtDataNascimento.Text = "2024/01/01"
    End Sub

    Private Function ObtenhaDadosFormulario() As AssociadoModel
        Dim Associado = New AssociadoModel
        Associado.Id = ConversorHelper.ConverterStringParaInteiro(txtId.Text)
        Associado.Cpf = txtCPF.Text
        Associado.Nome = txtNome.Text
        Associado.DataNascimento = txtDataNascimento.Text
        Return Associado
    End Function
    Private Sub PreencheDadosFormulario(associado As AssociadoModel)
        txtId.Text = associado.Id.ToString
        txtCPF.Text = associado.Cpf
        txtNome.Text = associado.Nome
        txtDataNascimento.Text = associado.DataNascimento.ToString("yyyy-MM-dd")
    End Sub

    Private Sub AtualizarTabelaAssociados()

        Dim consultaAssociados = AssociadoModel.VerTodos()

        If ListaAssociadosItens.Controls.Count > 0 Then
            ListaAssociadosItens.Controls.Clear()
        End If

        For Each Associado In consultaAssociados.ListaAssociados

            Dim novaLinha As New HtmlTableRow()

            Dim celulaId As New HtmlTableCell()
            Dim celulaCpf As New HtmlTableCell()
            Dim celulaNome As New HtmlTableCell()
            Dim celulaDataNacimento As New HtmlTableCell()
            Dim celularBotaoEditar As New HtmlTableCell()

            celulaId.InnerText = $"{Associado.Id}"
            celulaCpf.InnerText = $"{Associado.Cpf}"
            celulaNome.InnerText = $"{Associado.Nome}"
            celulaDataNacimento.InnerText = $"{Associado.DataNascimento.ToString("dd/MM/yyyy")}"

            Dim btnEditar As New Button()
            btnEditar.Text = "Editar"
            btnEditar.CssClass = "btn btn-warning"
            AddHandler btnEditar.Click, AddressOf EditarAssociado
            celularBotaoEditar.Controls.Add(btnEditar)

            novaLinha.Cells.Add(celulaId)
            novaLinha.Cells.Add(celulaCpf)
            novaLinha.Cells.Add(celulaNome)
            novaLinha.Cells.Add(celulaDataNacimento)
            novaLinha.Cells.Add(celularBotaoEditar)

            ListaAssociadosItens.Controls.Add(novaLinha)

        Next

    End Sub

End Class