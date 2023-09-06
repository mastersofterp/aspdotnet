<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportType.ascx.cs" Inherits="UC_ReportType" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<fieldset class="fieldset" style="width:170px">
    <legend class="legend" style="font-size:8pt">Report Type</legend>
    <table cellpadding="2" cellspacing="2" style="width:100%">
        <tr>
            <td>
                <asp:RadioButton ID="rbPDF" runat="server" Text="PDF" GroupName="report" Checked="true" Font-Size="8pt" />&nbsp;
                <asp:RadioButton ID="rbExcel" runat="server" Text="Excel" GroupName="report" Font-Size="8pt"/>&nbsp;
                <asp:RadioButton ID="rbWord" runat="server" Text="Word" GroupName="report" Font-Size="8pt"/>
            </td>
        </tr>
    </table>
</fieldset>