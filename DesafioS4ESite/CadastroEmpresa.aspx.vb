Imports DesafioS4EDb

Public Class CadastroEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.LimparFormulario()
        Me.AtualizarTabelaEmpresas()
    End Sub

    Protected Sub SalvarEmpresa(sender As Object, e As EventArgs)

        Dim empresa = Me.ObtenhaDadosFormulario()

        If empresa.Id = 0 Then

            Dim consulta = empresa.Cadastrar()

            If consulta.Retorno.Sucesso Then
                Me.PreencheDadosFormulario(consulta.Empresa)
                ErrorMessage.Visible = False
            Else
                FailureText.Text = consulta.Retorno.Mensagem
                ErrorMessage.Visible = True
            End If

        Else

            Dim consulta = empresa.Atualizar()

            If consulta.Retorno.Sucesso Then
                Me.PreencheDadosFormulario(consulta.Empresa)
                ErrorMessage.Visible = False
            Else
                Me.PreencheDadosFormulario(consulta.Empresa)
                FailureText.Text = consulta.Retorno.Mensagem
                ErrorMessage.Visible = True
            End If

        End If

        Me.AtualizarTabelaEmpresas()

    End Sub

    Private Sub EditarEmpresa(sender As Object, e As EventArgs)

        Dim button As Button = DirectCast(sender, Button)
        Dim row As HtmlTableRow = DirectCast(button.Parent.Parent, HtmlTableRow)
        Dim id As Integer = ConversorHelper.ConverterStringParaInteiro(row.Cells(0).InnerText)

        Dim consultaEmpresa = EmpresaModel.Ver(id)
        Me.PreencheDadosFormulario(consultaEmpresa.Empresa)

    End Sub

    Protected Sub ExcluirEmpresa(sender As Object, e As EventArgs)

        Dim empresa = Me.ObtenhaDadosFormulario()

        Dim consulta = empresa.Excluir()

        If consulta.Retorno.Sucesso Then
            Me.PreencheDadosFormulario(consulta.Empresa)
            ErrorMessage.Visible = False
        Else
            Me.PreencheDadosFormulario(consulta.Empresa)
            FailureText.Text = consulta.Retorno.Mensagem
            ErrorMessage.Visible = True
        End If

        Me.AtualizarTabelaEmpresas()

    End Sub

    Protected Sub LimparFormulario()
        txtId.Text = $"{0}"
        txtCNPJ.Text = ""
        txtNome.Text = ""
    End Sub

    Private Function ObtenhaDadosFormulario() As EmpresaModel
        Dim empresa = New EmpresaModel
        empresa.Id = ConversorHelper.ConverterStringParaInteiro(txtId.Text)
        empresa.Cnpj = txtCNPJ.Text
        empresa.Nome = txtNome.Text
        Return empresa
    End Function
    Private Sub PreencheDadosFormulario(empresa As EmpresaModel)
        txtId.Text = empresa.Id.ToString
        txtCNPJ.Text = empresa.Cnpj
        txtNome.Text = empresa.Nome
    End Sub

    Private Sub AtualizarTabelaEmpresas()

        Dim consultaEmpresas = EmpresaModel.VerTodos()

        If ListaEmpresasItens.Controls.Count > 0 Then
            ListaEmpresasItens.Controls.Clear()
        End If

        For Each empresa In consultaEmpresas.ListaEmpresas

            Dim novaLinha As New HtmlTableRow()

            Dim celulaId As New HtmlTableCell()
            Dim celulaCnpj As New HtmlTableCell()
            Dim celulaNome As New HtmlTableCell()
            Dim celularBotaoEditar As New HtmlTableCell()

            celulaId.InnerText = $"{empresa.Id}"
            celulaCnpj.InnerText = $"{empresa.Cnpj}"
            celulaNome.InnerText = $"{empresa.Nome}"

            Dim btnEditar As New Button()
            btnEditar.Text = "Editar"
            btnEditar.CssClass = "btn btn-warning"
            AddHandler btnEditar.Click, AddressOf EditarEmpresa
            celularBotaoEditar.Controls.Add(btnEditar)

            novaLinha.Cells.Add(celulaId)
            novaLinha.Cells.Add(celulaCnpj)
            novaLinha.Cells.Add(celulaNome)
            novaLinha.Cells.Add(celularBotaoEditar)

            ListaEmpresasItens.Controls.Add(novaLinha)

        Next

    End Sub




End Class