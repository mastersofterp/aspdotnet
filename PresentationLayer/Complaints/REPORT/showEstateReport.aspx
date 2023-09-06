<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showEstateReport.aspx.cs" Inherits="Estate_showEstateReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmReport" runat="server">
 <div>
            <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                 EnableDrillDown="False" HasCrystalLogo="False" 
                HasDrillUpButton="False" HasGotoPageButton="False" HasRefreshButton="True" 
                HasSearchButton="False" HasToggleGroupTreeButton="False" 
                EnableDatabaseLogonPrompt="False"  
                HasZoomFactorList="False" />
        </div>
        
        
    <%--   <div id="divMsg" runat="server" ></div> --%>
    </form>
    
</body>
</html>