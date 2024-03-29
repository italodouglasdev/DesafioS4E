﻿<%@ Page Title="CadastroEmpresa" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroEmpresa.aspx.vb" Inherits="DesafioS4ESite.CadastroEmpresa" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">

        <section id="empresaForm" class="row formulario-centralizado">
            <div class="col-md-8 col-12">
                <div class="card">
                    <h5 class="card-header">Cadastro de Empresas</h5>
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
                                <asp:Label runat="server" AssociatedControlID="txtCNPJ" CssClass=" col-form-label">CNPJ</asp:Label>
                                <asp:TextBox runat="server" ID="txtCNPJ" CssClass="form-control" MaxLength="14" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-12">
                                <asp:Label runat="server" AssociatedControlID="txtNome" CssClass="col-form-label">Nome</asp:Label>
                                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control" MaxLength="200" />
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="accordion accordion-flush" id="accordionEmpresaAssociados">
                                <div class="accordion-item">
                                    <h2 class="accordion-header">
                                        <button class="accordion-button collapsed bg-light" type="button" data-bs-toggle="collapse" data-bs-target="#flush-EmpresaAssociados" aria-expanded="false" aria-controls="flush-EmpresaAssociados">
                                            Associados da Empresa
                                        </button>
                                    </h2>

                                    <div id="flush-EmpresaAssociados" class="accordion-collapse collapse" data-bs-parent="#accordionEmpresaAssociados">

                                        <div>
                                            <table class="table" id="ListaAssociadosDaEmpresas">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">Id</th>
                                                        <th scope="col">CPF</th>
                                                        <th scope="col">Nome</th>
                                                        <th scope="col">Nascimento</th>
                                                        <th scope="col"></th>
                                                    </tr>
                                                </thead>

                                                <tbody runat="server" id="ListaAssociadosDaEmpresasItens">
                                                </tbody>

                                            </table>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-12">
                                <asp:Button runat="server" OnClick="LimparFormulario" Text="Nova" CssClass="btn btn-success" />
                                <asp:Button runat="server" OnClick="ExcluirEmpresa" Text="Excluir" CssClass="btn btn-danger" />
                                <asp:Button runat="server" OnClick="SalvarEmpresa" Text="Salvar" CssClass="btn btn-primary" />
                            </div>
                        </div>

                    </div>
                </div>

                <div class="card mt-5">
                    <h5 class="card-header">Lista de Empresas</h5>
                    <div class="card-body">

                        <div>
                            <div class="row">

                                <div class="col-4">
                                    <asp:Label runat="server" AssociatedControlID="txtBuscarCNPJ" CssClass=" col-form-label">CNPJ</asp:Label>
                                    <asp:TextBox runat="server" ID="txtBuscarCNPJ" CssClass="form-control" MaxLength="14" />
                                </div>
                                <div class="col-6">
                                    <asp:Label runat="server" AssociatedControlID="txtBuscarNome" CssClass="col-form-label">Nome</asp:Label>
                                    <asp:TextBox runat="server" ID="txtBuscarNome" CssClass="form-control" MaxLength="200" />
                                </div>

                            </div>

                            <div class="row mt-3 mb-5">
                                <div class="col-12">
                                    <asp:Button runat="server" OnClick="AtualizarTabelaEmpresas" Text="Buscar" CssClass="btn btn-primary btn-block" />
                                </div>
                            </div>

                            <hr />

                        </div>


                        <table class="table" id="ListaEmpresas">
                            <thead>
                                <tr>
                                    <th scope="col">Id</th>
                                    <th scope="col">CNPJ</th>
                                    <th scope="col">Nome</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>

                            <tbody runat="server" id="ListaEmpresasItens">
                            </tbody>

                        </table>

                    </div>
                </div>


            </div>
        </section>
    </div>

</asp:Content>
