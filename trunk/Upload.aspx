<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Upload</title>
</head>
<body>
    <form id="UploadForm" runat="server">
    <div>
        <table>
            <tr>
                <td>Filename on server:</td>
                <td>
                    <asp:TextBox ID="txtServerFileName" runat="server" OnTextChanged="txtServerFileName_TextChanged" AutoPostBack="True" />
                    <asp:Label ID="lblSFNValidationMessage" runat="server" Text="" ForeColor="Red" Visible="False" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Field!" ControlToValidate="txtServerFileName" />
                </td>
            </tr>
            <tr>
                <td>Local File:</td>
                <td>
                    <asp:FileUpload ID="uploadCtrl" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Field!" ControlToValidate="uploadCtrl" />
                </td>
            </tr>
        </table>
        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="Upload!" />
        
        <asp:Panel ID="pnlDebugArea" runat="server">
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
        <a href="Default.aspx">Back to home...</a>
    </div>
    </form>
</body>
</html>
