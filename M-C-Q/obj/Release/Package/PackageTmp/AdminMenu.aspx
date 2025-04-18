<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminMenu.aspx.cs" Inherits="M_C_Q.AdminMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-menu-main">
        <div class="admin-menu-content">
            <header>
                <h1>Dashboard</h1>
            </header>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnView" />
</asp:Content>
