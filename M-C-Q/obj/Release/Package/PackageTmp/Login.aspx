<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M_C_Q.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet" href="Css/Login.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>

    <script language='javascript' type='text/javascript'>
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
    </script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <script src="Script/Index.js"></script>
    <script src="Script/Swal.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlLoginForm">
            <div class="main">
                <div class="left">
                    <div class="form">
                        <div class="heading">
                            <h1>Login</h1>
                        </div>
                        <div class="formbody">
                            <div class="lbl">
                                <asp:Label runat="server" CssClass="lblUsername">Email ID</asp:Label>
                                <%--<span class="mendatory-sign">*</span>--%>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" CssClass="txtUsername" ID="txtEmail">
                                </asp:TextBox>
                            </div>
                            <div class="lbl">
                                <label for="" class="lblPassword">Password</label>
                                <%--<span class="mendatory-sign">*</span>--%>
                            </div>
                            <div class="txt">
                                <asp:TextBox runat="server" CssClass="txtPassword" ID="txtPassword" TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="submit">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="otherlinks">
                                <a href="#">Forget Password?</a>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div>
            <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>
        </div>


        <asp:Panel runat="server" ID="pnlLoginAsForm" Visible="false">
            <div class="main">
                <div class="left">
                    <div class="form">
                        <div class="heading">
                            <h2>Login as</h2>
                        </div>
                        <asp:Panel runat="server" class="formbodyLoginAs" ID="pnlLoginAs">
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </asp:Panel>

    </form>
</body>
</html>
