Public Class Empresas
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub


    Protected Sub Salvar()
        FailureText.Text = "Tentativa de logon inválida"
        ErrorMessage.Visible = True
    End Sub




End Class