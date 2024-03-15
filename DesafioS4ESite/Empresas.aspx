<%@ Page Title="Empresas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.vb" Inherits="DesafioS4ESite.Empresas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">

        <h2 id="title">Cadastro de Empresas</h2>

        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
            <p class="text-danger">
                <asp:Literal runat="server" ID="FailureText" />
            </p>
        </asp:PlaceHolder>

        <div class="row">

            <div class="col-2">
                <asp:Label runat="server" CssClass="col-form-label">ID</asp:Label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" TextMode="Number" />
            </div>

            <div class="col-4">
                <asp:Label runat="server" CssClass=" col-form-label">CNPJ</asp:Label>
                <asp:TextBox runat="server" ID="txtCNPJ" CssClass="form-control" />
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <asp:Label runat="server" CssClass="col-form-label">Nome</asp:Label>
                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control" />
            </div>
        </div>

        <div class="row mt-3">
            <div class="offset-md-2">
                <asp:Button runat="server" OnClick="Salvar" Text="Salvar" CssClass="btn btn-primary" ID="btnSalvar" />
            </div>
        </div>

        <div class="row mt-3">

            <asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            OnPageIndexChanging="OnPageIndexChanging" PageSize="10">

                <Columns>
                    <asp:BoundField ItemStyle-Width="150px" DataField="CustomerID" HeaderText="Customer ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="ContactName" HeaderText="Contact Name" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="City" HeaderText="City" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="Country" HeaderText="Country" />
                </Columns>


            </asp:GridView>

        </div>

    </main>
</asp:Content>
