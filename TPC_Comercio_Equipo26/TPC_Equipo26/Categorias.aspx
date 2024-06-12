﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="TPC_Equipo26.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CATEGORÍAS</h1>
    <div class="container">
    <div class="row">
        <asp:Label Text="Buscar:" runat="server" CssClass="form-label" />
        <div class="col-2">
            <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
        </div>
        <div class="row">
            <div class="col-2">
                <asp:CheckBox runat="server" ID="chkIncluirInactivos" Text="Incluir inactivos"  AutoPostBack="true" />
            </div>
        </div>
        <div class="row">
            <div class="col-2">
                <asp:Button runat="server" ID="btnLimpiarFiltros" Text="Limpiar filtros" OnClick="btnLimpiarFiltros_Click" CssClass="btn btn-light mt-3" Style="margin: 15px" />
            </div>
        </div>
    </div>
</div>

    <asp:GridView ID="gvCategorias" runat="server" DataKeyNames="ID" CssClass="table table-light table-bordered"
        Style="text-align: center" AutoGenerateColumns="false"
        OnSelectedIndexChanged ="gvCategorias_SelectedIndexChanged">
                <Columns>
            <asp:BoundField HeaderText="Id" DataField="ID" />
            <asp:BoundField HeaderText="Categoría" DataField="Descripcion" />
            <asp:BoundField HeaderText="Activo" DataField="Activo" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href='<%# "AltaCategoria.aspx?ID=" + Eval("ID") %>' class="icono" title="Gestionar">
                        <i class="fa-solid fa-pen" style="color: dimgrey; margin: 10px"></i>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <a href="AltaCategoria.aspx" class="btn btn-success">Agregar una Categoria</a>
</asp:Content>
