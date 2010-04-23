<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Main</title>
    <link href="fmri.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" width="100%">
    Recommanded browsers are Google Chrome or Mozilla Firefox.<br /><br />
    <table border="0"><tr>
    <td width="300" align="center" valign="middle">
        <img src="images/logo.png" alt="Logo" width="300" />
    </td>
    <td width="300" align="center" valign="middle">
	    <a class="large orange awesome" href="Upload.aspx">Upload</a><br /><br />
	    <a class="large green awesome" href="Analyze.aspx">Analyze</a><br /><br />
	    <a class="large yellow awesome" href="Results.aspx">View Results</a><br /><br />
	    <a class="large magenta awesome" href="Control.aspx">Control</a><br /><br />
    </td>
    </tr></table>
    </div>
    </form>
    <div class="footer">
        <b>Home</b> |
        <a href="Upload.aspx" class="footerlink">Upload</a> |
        <a href="Analyze.aspx" class="footerlink">Analyze</a> |
        <a href="Results.aspx" class="footerlink">View Results</a> |
        <a href="Control.aspx" class="footerlink">Control</a>
    </div>
</body>
</html>
