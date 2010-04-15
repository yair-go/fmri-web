<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Results</title>
</head>
<body>
    <form id="frmResults" runat="server">
    <div>
        <asp:Panel ID="pnlMessage" runat="server">
            <asp:Label ID="lblMsg" runat="server" />
            
            Reference Number: <asp:TextBox ID="txtID" runat="server" /><br />
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh!" OnClick="btnRefresh_Click" />
        </asp:Panel><br />
        <asp:Panel ID="pnlImage" runat="server">
            Your image is:<br />
            <asp:Image ID="imgResult" runat="server" />
        </asp:Panel><br />
        <a href="Default.aspx">Back to home...</a>
    </div>
    </form>
</body>
</html>
