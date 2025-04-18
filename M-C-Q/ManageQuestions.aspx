<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageQuestions.aspx.cs" Inherits="M_C_Q.ManageQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <script src="Script/Index.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="admin-menu-main">
        <div class="admin-menu-content">

            <asp:Panel runat="server" ID="pnlSubmit" CssClass="pnlSubmit">

                <div class="add-user-content">
                    <div class="lbl" style="margin-top: 15px;">
                        <asp:Label runat="server" ID="lblAddQuestions" Text="Add Question"></asp:Label>
                    </div>

                    <div class="txt">
                        <asp:TextBox runat="server" ID="txtQuestion" TextMode="MultiLine" CssClass="form-control txtQuestion" placeholder="Type your Question..."></asp:TextBox>
                    </div>
                </div>

                <div class="add-user-content">
                    <asp:Panel runat="server" ID="pnlAddAns" CssClass="pnlAddAns">
                        <div class="lbl" style="margin-top: 15px;">
                            <asp:Label runat="server" ID="Label1" Text="Add Options"></asp:Label>

                            <div class="txt pnlAddOptions" ID="pnlAddOptions1">
                                <label runat="server" id="lblOption1" Class="lblOption">1</label>
                                <input runat="server" type="text" id="txtOption1" Class="txtOption form-control" />
                                <select runat="server" id="ddlCorrect1" Class="ddlCorrect form-control-m">
                                    <option>Incorrect</option>
                                    <option>Correct</option>
                                </select>
                                <input runat="server" type="button" id="btnAddOptions" Class="btnAddOptions mx-2" value="+" />
                            </div>

                        </div>
                    </asp:Panel>
                </div>

            </asp:Panel>

        </div>
    </div>

    <script>
        sessionStorage.setItem("intRow", 1)
    </script>

</asp:Content>
