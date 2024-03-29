﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Control.aspx.cs" Inherits="Control" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Control</title>
    <link href="fmri.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <b>FMRI request status: (newer to older)</b><br /><br />
        <asp:Table ID="tblReq" runat="server" BorderStyle="Solid" BorderWidth="1" CellPadding="5" GridLines="Both">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Status</asp:TableHeaderCell>
                <asp:TableHeaderCell>Filename</asp:TableHeaderCell>
                <asp:TableHeaderCell>X</asp:TableHeaderCell>
                <asp:TableHeaderCell>Y</asp:TableHeaderCell>
                <asp:TableHeaderCell>Z</asp:TableHeaderCell>
                <asp:TableHeaderCell>Threshold</asp:TableHeaderCell>
                <asp:TableHeaderCell>Time</asp:TableHeaderCell>
                <asp:TableHeaderCell>Clique</asp:TableHeaderCell>
                <asp:TableHeaderCell>IP</asp:TableHeaderCell>
                <asp:TableHeaderCell>Submitted</asp:TableHeaderCell>
                <asp:TableHeaderCell>Executed</asp:TableHeaderCell>
                <asp:TableHeaderCell>Result</asp:TableHeaderCell>
                <asp:TableHeaderCell>Link</asp:TableHeaderCell>
                <asp:TableHeaderCell>Excel</asp:TableHeaderCell>
                <asp:TableHeaderCell>Cliques</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
        
        <br />
        <a class="large magenta awesome" href="Control.aspx">Refresh!</a><br />
        <br />
        <b>Note: </b> the "history" feature does not work properly. It might "forget" some requests. The bug will be fixed soon. Sorry.
        <br /><br />
    </div>
    <div class="footer">
        <a href="Default.aspx" class="footerlink">Home</a> |
        <a href="Upload.aspx" class="footerlink">Upload</a> |
        <a href="Analyze.aspx" class="footerlink">Analyze</a> |
        <a href="Results.aspx" class="footerlink">View Results</a> |
        <b>Control</b>
        <br /><br />Application Instance: <% =Application["ID"].ToString() %> | Hosting Process ID: <% =System.Diagnostics.Process.GetCurrentProcess().Id %>
        <br /><br />
        <asp:LinkButton ID="lnkSvnUpdate" runat="server" onclick="lnkSvnUpdate_Click" class="footerlink">Update application from SVN</asp:LinkButton> (will restart the application instance)<br />
    </div>
    </form>
</body>
</html>
