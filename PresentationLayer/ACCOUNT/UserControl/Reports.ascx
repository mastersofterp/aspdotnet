<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reports.ascx.cs" Inherits="ACCOUNT_User_Control_Reports" %>
<table width="30%">
    <tr>
        <td>
            <asp:ImageButton ID="imdPdf" runat="server" ImageUrl="../../IMAGES/pdf.png" Width="25%"
                Height="100%"  ToolTip="Export to PDF" OnClientClick="return Export('pdf')"  />
            <asp:ImageButton ID="imgWord" runat="server" ImageUrl="../../IMAGES/word.png" Width="25%"
                Height="100%"  ToolTip="Export to Word" OnClientClick="return Export('doc')"/>
            <asp:ImageButton ID="imgExcel" runat="server" Width="25%" Height="100%" ImageUrl="../../IMAGES/excel.png"
                 ToolTip="Export to Excel"  OnClientClick="return Export('xls')"/>
        </td>
    </tr>
</table>
<%--
OnClick="imdPdf_Click"
OnClick="imgWord_Click"
OnClick="imgExcel_Click"--%>