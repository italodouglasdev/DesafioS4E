Public Class CadastroAssociado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AtualizarTabelaAssociados()
        Me.AtualizarTabelaEmpresasDoAssociado()
    End Sub

    Protected Sub SalvarAssociado(sender As Object, e As EventArgs)

        Dim associado = Me.ObtenhaDadosFormulario()

        If associado.Id = 0 Then

            Dim consulta = associado.Cadastrar()

            If consulta.Retorno.Sucesso Then
                Me.PreencheDadosFormulario(consulta.Associado)
                ErrorMessage.Visible = False
            Else
                FailureText.Text = consulta.Retorno.Mensagem
                ErrorMessage.Visible = True
            End If

        Else

            Dim consulta = associado.Atualizar()

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

        Me.AtualizarTabelaEmpresasDoAssociado()

    End Sub

    Protected Sub IncluirAssociado(sender As Object, e As EventArgs)

        Dim button As Button = DirectCast(sender, Button)
        Dim row As HtmlTableRow = DirectCast(button.Parent.Parent, HtmlTableRow)
        Dim id As Integer = ConversorHelper.ConverterStringParaInteiro(row.Cells(0).InnerText)

        Dim asssociado = ObtenhaDadosFormulario()

        asssociado.ListaEmpresas = New List(Of RelacaoEmpresaAssociadoModel)
        asssociado.ListaEmpresas.Add(New RelacaoEmpresaAssociadoModel(id, Enumeradores.EnumInstrucao.Incluir))


        Dim consulta = asssociado.Atualizar()

        If consulta.Retorno.Sucesso Then
            Me.PreencheDadosFormulario(consulta.Associado)
            ErrorMessage.Visible = False
        Else
            Me.PreencheDadosFormulario(consulta.Associado)
            FailureText.Text = consulta.Retorno.Mensagem
            ErrorMessage.Visible = True
        End If

        Me.AtualizarTabelaEmpresasDoAssociado()

    End Sub

    Protected Sub RemoverAssociado(sender As Object, e As EventArgs)

        Dim button As Button = DirectCast(sender, Button)
        Dim row As HtmlTableRow = DirectCast(button.Parent.Parent, HtmlTableRow)
        Dim id As Integer = ConversorHelper.ConverterStringParaInteiro(row.Cells(0).InnerText)

        Dim associado = ObtenhaDadosFormulario()

        associado.ListaEmpresas = New List(Of RelacaoEmpresaAssociadoModel)
        associado.ListaEmpresas.Add(New RelacaoEmpresaAssociadoModel(id, Enumeradores.EnumInstrucao.Remover))

        Dim consulta = associado.Atualizar()

        If consulta.Retorno.Sucesso Then
            Me.PreencheDadosFormulario(consulta.Associado)
            ErrorMessage.Visible = False
        Else
            Me.PreencheDadosFormulario(consulta.Associado)
            FailureText.Text = consulta.Retorno.Mensagem
            ErrorMessage.Visible = True
        End If

        Me.AtualizarTabelaEmpresasDoAssociado()

    End Sub

    Protected Sub ExcluirAssociado(sender As Object, e As EventArgs)

        Dim associado = Me.ObtenhaDadosFormulario()

        Dim consulta = associado.Excluir()

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
        txtDataNascimento.Text = "0001-01-01"

        ErrorMessage.Visible = False
        Me.AtualizarTabelaEmpresasDoAssociado()

    End Sub

    Private Function ObtenhaDadosFormulario() As AssociadoModel
        Dim Associado = New AssociadoModel
        Associado.Id = ConversorHelper.ConverterStringParaInteiro(txtId.Text)
        Associado.Cpf = txtCPF.Text
        Associado.Nome = txtNome.Text
        Associado.DataNascimento = ConversorHelper.ConverterStringParaDateTime(txtDataNascimento.Text).ToString("yyyy-MM-dd")
        Return Associado
    End Function

    Private Sub PreencheDadosFormulario(associado As AssociadoModel)

        ErrorMessage.Visible = False

        txtId.Text = associado.Id.ToString
        txtCPF.Text = associado.Cpf
        txtNome.Text = associado.Nome
        txtDataNascimento.Text = associado.DataNascimento.ToString("yyyy-MM-dd")

    End Sub

    Protected Sub AtualizarTabelaAssociados()

        Dim consultaAssociados = AssociadoModel.VerTodos(txtBuscarCPF.Text, txtBuscarNome.Text, ConversorHelper.ConverterStringParaDateTime(txtBuscarDataNascimentoInicio.Text), ConversorHelper.ConverterStringParaDateTime(txtBuscarDataNascimentoFim.Text))

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

    Protected Sub AtualizarTabelaEmpresasDoAssociado()

        Dim associado = ObtenhaDadosFormulario()

        If associado.Id = 0 Then
            If ListaEmpresasDoAssociadoItens.Controls.Count > 0 Then
                ListaEmpresasDoAssociadoItens.Controls.Clear()
                Return
            End If
        End If

        Dim consultaEmpresasAssociados = RelacaoEmpresaAssociadoModel.VerTodos(associado.Id, Enumeradores.EnumTipoRelacao.EmpresasDoAssociado)

        Dim consultaEmpresas = EmpresaModel.VerTodos()

        If ListaEmpresasDoAssociadoItens.Controls.Count > 0 Then
            ListaEmpresasDoAssociadoItens.Controls.Clear()
        End If

        For Each empresa In consultaEmpresas.ListaEmpresas

            Dim associadoRelacionado = consultaEmpresasAssociados.Exists(Function(x) x.Id = empresa.Id)

            Dim novaLinha As New HtmlTableRow()

            Dim celulaId As New HtmlTableCell()
            Dim celulaCpf As New HtmlTableCell()
            Dim celulaNome As New HtmlTableCell()
            Dim celulaDataNacimento As New HtmlTableCell()
            Dim celularBtnIncluirRemover As New HtmlTableCell()

            celulaId.InnerText = $"{empresa.Id}"
            celulaCpf.InnerText = $"{empresa.Cnpj}"
            celulaNome.InnerText = $"{empresa.Nome}"

            Dim btnIncluirRemover As New Button()

            If associadoRelacionado = True Then
                btnIncluirRemover.Text = "Remover"
                btnIncluirRemover.CssClass = "btn btn-danger"
                AddHandler btnIncluirRemover.Click, AddressOf RemoverAssociado
            Else
                btnIncluirRemover.Text = "Incluir"
                btnIncluirRemover.CssClass = "btn btn-success"
                AddHandler btnIncluirRemover.Click, AddressOf IncluirAssociado
            End If

            celularBtnIncluirRemover.Controls.Add(btnIncluirRemover)

            novaLinha.Cells.Add(celulaId)
            novaLinha.Cells.Add(celulaCpf)
            novaLinha.Cells.Add(celulaNome)
            novaLinha.Cells.Add(celulaDataNacimento)
            novaLinha.Cells.Add(celularBtnIncluirRemover)

            ListaEmpresasDoAssociadoItens.Controls.Add(novaLinha)

        Next

    End Sub


End Class