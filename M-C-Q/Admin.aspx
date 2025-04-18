<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="M_C_Q.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Admin</h1>
        </div>
        <div>
            <asp:Label runat="server" ID="lblMessage" Text="lblMessage"></asp:Label>
        </div>
        <div>
            <asp:Button runat="server" ID="btnlogout" Text="Logout" OnClick="btnlogout_Click" />
        </div>
    </form>
</body>
</html>
