<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Analyze.aspx.cs" Inherits="Analyze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FMRI :: Analyze</title>
    <link href="fmri.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="AnalyzeForm" runat="server">
    <div>
        <table>
            <tr>
                <td>Image name:</td>
                <td><asp:DropDownList ID="lstImageName" runat="server" /></td>
            </tr>
            <tr>
                <td>X range:</td>
                <td>
                    <asp:TextBox ID="txtX1" runat="server" Columns="4" /> to 
                    <asp:TextBox ID="txtX2" runat="server" Columns="4" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br />X1 is required!" ControlToValidate="txtX1" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<br />X2 is required!" ControlToValidate="txtX2" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />X1 should be a number!" ControlToValidate="txtX1" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="<br />X2 should be a number!" ControlToValidate="txtX2" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="<br />X1 range is: [1,95]" ControlToValidate="txtX1" MinimumValue="1" MaximumValue="95" Type="Integer" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="<br />X2 range is: [1,95]" ControlToValidate="txtX2" MinimumValue="1" MaximumValue="950" Type="Integer" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />X2 should be greater than X1!" ControlToValidate="txtX2" ControlToCompare="txtX1" Type="Integer" Operator="GreaterThan" Display="Dynamic" />

                </td>
            </tr>
            <tr>
                <td>Y range:</td>
                <td>
                    <asp:TextBox ID="txtY1" runat="server" Columns="4" /> to 
                    <asp:TextBox ID="txtY2" runat="server" Columns="4" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<br />Y1 is required!" ControlToValidate="txtY1" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<br />Y2 is required!" ControlToValidate="txtY2" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br />Y1 should be a number!" ControlToValidate="txtY1" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="<br />Y2 should be a number!" ControlToValidate="txtY2" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="<br />Y1 range is: [1,57]" ControlToValidate="txtY1" MinimumValue="1" MaximumValue="57" Type="Integer" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="<br />Y2 range is: [1,57]" ControlToValidate="txtY2" MinimumValue="1" MaximumValue="57" Type="Integer" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Y2 should be greater than Y1!" ControlToValidate="txtY2" ControlToCompare="txtY1" Type="Integer" Operator="GreaterThan" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>Z range:</td>
                <td>
                    <asp:TextBox ID="txtZ1" runat="server" Columns="4" /> to 
                    <asp:TextBox ID="txtZ2" runat="server" Columns="4" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="<br />Z1 is required!" ControlToValidate="txtZ1" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="<br />Z2 is required!" ControlToValidate="txtZ2" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="<br />Z1 should be a number!" ControlToValidate="txtZ1" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="<br />Z2 should be a number!" ControlToValidate="txtZ2" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="<br />Z1 range is: [1,79]" ControlToValidate="txtZ1" MinimumValue="1" MaximumValue="79" Type="Integer" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="<br />Z2 range is: [1,79]" ControlToValidate="txtZ2" MinimumValue="1" MaximumValue="79" Type="Integer" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Z2 should be greater than Z1!" ControlToValidate="txtZ2" ControlToCompare="txtZ1" Type="Integer" Operator="GreaterThan" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>Threshold:</td>
                <td>
                    <asp:TextBox ID="txtThreshold" runat="server" Columns="4" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="<br />Threshold is required!" ControlToValidate="txtThreshold" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Threshold should be Double!" ControlToValidate="txtThreshold" Type="Double" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator7" runat="server" ErrorMessage="<br />Threshold range is: [0,1]" ControlToValidate="txtThreshold" MinimumValue="0" MaximumValue="1" Type="Double" Display="Dynamic">
                    </asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>Time range:</td>
                <td>
                    <asp:TextBox ID="txtT1" runat="server" Columns="4" Text="1" /> to 
                    <asp:TextBox ID="txtT2" runat="server" Columns="4" Text="132" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="<br />T1 is required!" ControlToValidate="txtT1" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="<br />T2 is required!" ControlToValidate="txtT2" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="<br />T1 should be a number!" ControlToValidate="txtT1" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="<br />T2 should be a number!" ControlToValidate="txtT2" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator8" runat="server" ErrorMessage="<br />T1 range is: [1,131]" ControlToValidate="txtT1" MinimumValue="1" MaximumValue="131" Type="Integer" Display="Dynamic" />
                    <asp:RangeValidator ID="RangeValidator9" runat="server" ErrorMessage="<br />T2 range is: [2,132]" ControlToValidate="txtT2" MinimumValue="2" MaximumValue="132" Type="Integer" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="<br />T2 should be greater than T1!" ControlToValidate="txtT2" ControlToCompare="txtT1" Type="Integer" Operator="GreaterThan" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>Clique size</td>
                <td>
                    <asp:TextBox ID="txtCS1" runat="server" Columns="4" /> to 
                    <asp:TextBox ID="txtCS2" runat="server" Columns="4" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="<br />CS1 is required!" ControlToValidate="txtCS1" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="<br />CS2 is required!" ControlToValidate="txtCS2" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="<br />CS1 should be a number!" ControlToValidate="txtCS1" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="<br />CS2 should be a number!" ControlToValidate="txtCS2" Type="Integer" Operator="DataTypeCheck" Display="Dynamic" />
                    <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="<br />CS2 should be greater than CS1!" ControlToValidate="txtCS2" ControlToCompare="txtCS1" Type="Integer" Operator="GreaterThanEqual" Display="Dynamic" />
                </td>
            </tr>
        </table>
        <asp:LinkButton ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="Analyze!" class="large green awesome" />
        
        <asp:Panel ID="pnlDebugArea" runat="server" Visible="false">
            <br /><hr /><br />
            Debug messages: <br />
            <asp:Label ID="lblDebugReport" runat="server" BorderStyle="Solid" 
                BackColor="#eeeeee" BorderWidth="1px" Width="100%" />
        </asp:Panel>
        
        <asp:Panel ID="pnlRefArea" runat="server" Visible="false">
            <br /><hr /><br />
            <asp:HiddenField ID="refStr" runat="server" />
            <asp:HiddenField ID="refNoThreshold" runat="server" />
            <span style="font-weight:bold">
                <asp:Label ID="lblRef" runat="server" Text="" ForeColor="Blue" />
            </span><br />
            <a href="Control.aspx" class="large magenta awesome">View Status</a><br /><br />
            <asp:LinkButton ID="btnGoToResults" runat="server" onclick="btnGoToResults_Click" Text="View Results" class="large yellow awesome" />
            
        </asp:Panel>
        <br />
    </div>
    </form>
    <div class="footer">
        <a href="Default.aspx" class="footerlink">Home</a> |
        <a href="Upload.aspx" class="footerlink">Upload</a> |
        <b>Analyze</b> |
        <a href="Results.aspx" class="footerlink">View Results</a> |
        <a href="Control.aspx" class="footerlink">Control</a>
        <br /><br />Application Instance: <% =Application["ID"].ToString() %> | Hosting Process ID: <% =System.Diagnostics.Process.GetCurrentProcess().Id %>
    </div>
</body>
</html>
