<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cheque_Vertical.aspx.cs" Inherits="Reports_Cheque_Vertical" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>  

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <STYLE>
        .classname
        {
            letter-spacing:500px;
        }
  </STYLE>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 148px">
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
            RepeatDirection="Horizontal" 
           >
            <asp:ListItem Value="P">Want To Print</asp:ListItem>
            <asp:ListItem Value="N">Dont Want To Print</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" HasPrintButton="False"  />
    </div> 
    </form>
</body>
</html>
