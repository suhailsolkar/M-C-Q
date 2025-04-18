<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageTeachers.aspx.cs" Inherits="M_C_Q.ManageTeachers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center">
        <div class="left">
            <asp:Panel ID="pnlManageTeachers" runat="server">
                <h1>Manage Teachers</h1>
                <div>
                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnAdd" Text="Add" runat="server" OnClick="btnAdd_Click" />
                    <br />
                    <div class="auto-style1">
                        <asp:GridView ID="gvTeachers" runat="server" 
                            AutoGenerateColumns="False" CssClass="grid" 
                            OnRowEditing="gvTeachers_RowEditing" OnRowUpdating="gvTeachers_RowUpdating" 
                            OnRowCancelingEdit="gvTeachers_RowCancelingEdit" OnRowDeleting="gvTeachers_RowDeleting"
                            >
                            <Columns>
                                <asp:CommandField HeaderText="Add" InsertText="Add" NewText="Add" ShowHeader="True" ShowInsertButton="True" ButtonType="Button" />
                                <asp:CommandField ButtonType="Button" HeaderText="Edit" ShowEditButton="True" />
                                <asp:TemplateField HeaderText="User_ID">
                                    <EditItemTemplate>
                                        <asp:label ID="lblUserID2" runat="server" Text='<%# Eval("User_ID") %>'></asp:label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("User_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email_ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmailID" runat="server" Text='<%# Eval("Email_ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("Email_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Username">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUsername" runat="server" Text='<%# Eval("Username") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role_ID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRoleID" runat="server" Text='<%# Eval("Role_ID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Eval("Role_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" />
                            </Columns>
                            <EmptyDataTemplate>
                            <label style="color:Red;font-weight:bold; font-family:'Lucida Sans';">No records found !</label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
