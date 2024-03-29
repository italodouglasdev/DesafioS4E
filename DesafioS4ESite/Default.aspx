﻿<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DesafioS4ESite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row mb-5" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Desafio S4E</h1>
            <p class="lead">Projeto desenvolvido em WebForms, utilizando VB.Net como linguagem e Banco de Dados em SQL Server.</p>
            <p class="lead">Também foi integrado ao projeto o Swagger para facilitar a utilização da API.</p>
            <p><a href="https://github.com/italodouglasdev/DesafioS4E" target="_blank" class="btn btn-dark btn-md">Repositório GitHub &raquo;</a></p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="menuEmpresas">
                <h2 id="menuEmpresas">Empresas</h2>
                <p>
                    Clique para ter acesso ao cadastro de Empresas, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/CadastroEmpresa" target="_blank">Abrir Cadastro</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="menuAssociados">
                <h2 id="menuAssociados">Associados</h2>
                <p>
                    Clique para ter acesso ao cadastro de Associados, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/CadastroAssociado" target="_blank">Abrir Cadastro</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="menuAPI">
                <h2 id="menuAPI">API</h2>
                <p>
                    Clique para ter acesso ao cadastro de Empresas e Associados via API, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/Swagger" target="_blank">Abrir Swagger</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
