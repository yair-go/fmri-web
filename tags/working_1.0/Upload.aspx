<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Upload</title>
    <link href="fmri.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="UploadForm" runat="server">
    <div>
        <table>
            <tr>
                <td>Filename on server:</td>
                <td>
                    <asp:TextBox ID="txtServerFileName" runat="server" OnTextChanged="txtServerFileName_TextChanged" AutoPostBack="True" />
                </td>
                <td>
                    <asp:Label ID="lblSFNValidationMessage" runat="server" Text="" ForeColor="Red" Visible="False" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field!" ControlToValidate="txtServerFileName" />
                </td>
            </tr>
            <tr>
                <td>Local File:</td>
                <td>
                    <asp:FileUpload ID="uploadCtrl" runat="server" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field!" ControlToValidate="uploadCtrl" />
                </td>
            </tr>
        </table>
        <asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="Upload!" class="large orange awesome" />
        
        <asp:Panel ID="pnlDebugArea" runat="server" Visible="false">
            <br /><hr /><br />
            Debug messages: <br />
            <asp:Label ID="lblDebugReport" runat="server" BorderStyle="Solid" 
                BackColor="#eeeeee" BorderWidth="1px" Width="100%" />
        </asp:Panel>
        
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <br /><hr /><br />
            <asp:Label ID="lblResult" runat="server" Text="" />
        </asp:Panel>
        
        <br />
    </div>
    </form>
    <div class="footer">
        <a href="Default.aspx" class="footerlink">Home</a> |
        <b>Upload</b> |
        <a href="Analyze.aspx" class="footerlink">Analyze</a> |
        <a href="Results.aspx" class="footerlink">View Results</a> |
        <a href="Control.aspx" class="footerlink">Control</a>
    </div>
</body>
</html>
