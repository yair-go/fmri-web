<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Results" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Results</title>
    <link href="fmri.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmResults" runat="server">
    <div>
        <table>
            <tr>
                <td>Excel Reference Number:</td>
                <td><asp:TextBox ID="txtExcelID" runat="server" size="45" /></td>
            </tr>
            <tr>
                <td>Image Reference Number:</td>
                <td><asp:TextBox ID="txtID" runat="server" size="45" /></td>
            </tr>
        </table>
        <asp:LinkButton ID="btnRefresh" runat="server" Text="View Results!" OnClick="btnRefresh_Click" class="large yellow awesome" />
        <br />
        <asp:Panel ID="pnlMessage" runat="server">
            <br />
            <asp:Label ID="lblMsg" runat="server" />
            <br />
        </asp:Panel>
        
        <asp:Panel ID="pnlExcelFile" runat="server">
            <br />
            Download correlation matrix as: &nbsp;&nbsp;
            <asp:HyperLink ID="lnkExcelFile" runat="server" class="small awesome">Excel</asp:HyperLink>&nbsp;&nbsp; 
            <asp:HyperLink ID="lnkZipFile" runat="server" class="small awesome">Zipped</asp:HyperLink><br />
            <br />
        </asp:Panel>
        
        <asp:Panel ID="pnlImage" runat="server">
            <br />
            <asp:Label runat="server" ForeColor="Green" Font-Bold="true">Your image is:</asp:Label><br />
            Black pixel represents "no correlation". White pixel represent a correlation above the given threshold.<br />
            Note that the image is duplicated (mirror axis from top-left to bottom-right is always white).<br />
            <asp:Image ID="imgResult" runat="server" BorderStyle="Solid" BorderWidth="1" />
            <br />
        </asp:Panel>
    </div>
    </form>
    <div class="footer">
        <a href="Default.aspx" class="footerlink">Home</a> |
        <a href="Upload.aspx" class="footerlink">Upload</a> |
        <a href="Analyze.aspx" class="footerlink">Analyze</a> |
        <b>View Results</b> |
        <a href="Control.aspx" class="footerlink">Control</a>
        <br /><br />Application Instance: <% =Application["ID"].ToString() %> | Hosting Process ID: <% =System.Diagnostics.Process.GetCurrentProcess().Id %>
    </div>
</body>
</html>
