﻿<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="DesafioS4ESite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row mb-5" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Desafio S4E</h1>
            <p class="lead">Projeto desenvolvido em WebForms, utilizando VB.Net como lingauagem e Banco de Dados em SQL Server. Também foi integrado ao projeto o Swagger para facilitar a utilização da API, inclusive toda sua documentação.</p>             
            <p><a href="https://github.com/italodouglasdev/DesafioS4E" target="_blank" class="btn btn-dark btn-md">Repositório GitHub &raquo;</a></p>
        </section>

        <div class="row">
            <section class="col-md-4" aria-labelledby="gettingStartedTitle">
                <h2 id="gettingStartedTitle">Empresas</h2>
                <p>
                    Clique para ter acesso ao cadastro de Empresas, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/Empresas">Abrir Cadastro</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="librariesTitle">
                <h2 id="librariesTitle">EmpresaAssociados</h2>
                <p>
                    Clique para ter acesso ao cadastro de EmpresaAssociados, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/EmpresaAssociados">Abrir Cadastro</a>
                </p>
            </section>
            <section class="col-md-4" aria-labelledby="hostingTitle">
                <h2 id="hostingTitle">API</h2>
                <p>
                    Clique para ter acesso ao cadastro de Empresas e EmpresaAssociados via API, nele é possível realizar consultas, inclusões e alterações.
                </p>
                <p>
                    <a class="btn btn-primary" href="/Swagger">Abrir Swagger</a>
                </p>
            </section>
        </div>
    </main>

</asp:Content>
