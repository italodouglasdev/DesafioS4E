﻿<%@ Page Title="CadastroAssociado" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroAssociado.aspx.vb" Inherits="DesafioS4ESite.CadastroAssociado" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <section id="AssociadoForm" class="row formulario-centralizado">
            <div class="col-md-8 col-12">
                <div class="card">
                    <h5 class="card-header">Cadastro de Associado</h5>
                    <div class="card-body">

                        <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                            <p class="text-danger">
                                <asp:Literal runat="server" ID="FailureText" />
                            </p>
                        </asp:PlaceHolder>

                        <div class="row">
                            <div class="col-2">
                                <asp:Label runat="server" AssociatedControlID="txtId" CssClass="col-form-label">ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                            </div>

                            <div class="col-4">
                                <asp:Label runat="server" AssociatedControlID="txtCPF" CssClass=" col-form-label">CNPJ</asp:Label>
                                <asp:TextBox runat="server" ID="txtCPF" CssClass="form-control" MaxLength="14" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-8 col-12">
                                <asp:Label runat="server" AssociatedControlID="txtNome" CssClass="col-form-label">Nome</asp:Label>
                                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control" MaxLength="200" />
                            </div>
                            <div class="col-md-4 col-12">
                                <asp:Label runat="server" AssociatedControlID="txtDataNascimento" CssClass="col-form-label">Data de Nascimento</asp:Label>
                                <asp:TextBox runat="server" ID="txtDataNascimento" CssClass="form-control" TextMode="Date" />
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <asp:Button runat="server" OnClick="LimparFormulario" Text="Nova" CssClass="btn btn-success" />
                                <asp:Button runat="server" OnClick="ExcluirAssociado" Text="Excluir" CssClass="btn btn-danger" />
                                <asp:Button runat="server" OnClick="SalvarAssociado" Text="Salvar" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="card mt-5">
                    <h5 class="card-header">Lista de Associados</h5>
                    <div class="card-body">


                        <table class="table" id="ListaAssociados">
                            <thead>
                                <tr>
                                    <th scope="col">Id</th>
                                    <th scope="col">CPF</th>
                                    <th scope="col">Nome</th>
                                    <th scope="col">Data de Nascimento</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>

                            <tbody runat="server" id="ListaAssociadosItens">
                            </tbody>
                        </table>

                    </div>
                </div>


            </div>
        </section>
    </div>

</asp:Content>