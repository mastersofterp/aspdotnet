<%@ Page Language="C#" AutoEventWireup="true" CodeFile="commonreport.aspx.cs" Inherits="LegalMatters_Crystalreport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>  

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script language="javascript" for="window" event="onbeforeunload">
        alert("Trapping Closing event");
    </script>--%>
<%--
<script language="javascript">

    function confirmMe()
    {
        var posx = 0;
        var posy = 0;
        if (!e) var e = window.event;
        if (e.pageX || e.pageY) {
            posx = e.pageX;
            posy = e.pageY;
        }    
        else if (e.clientX || e.clientY) {   
            posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }
        alert(posx + ", " + posy);
    }
</script>--%>


   <%-- <script type="text/javascript">
    <!--
    function callAjax(webUrl, queryString)
    {
     var xmlHttpObject = null;

     try
     {
      // Firefox, Opera 8.0+, Safari...

      xmlHttpObject = new XMLHttpRequest();
     }
     catch(ex)
     {
      // Internet Explorer...

      try
      {
       xmlHttpObject = new ActiveXObject('Msxml2.XMLHTTP');
      }
      catch(ex)
      {
       xmlHttpObject = new ActiveXObject('Microsoft.XMLHTTP');
      }
     }

     if ( xmlHttpObject == null )
     {
      window.alert('AJAX is not available in this browser');
      return;
     }

     xmlHttpObject.open("GET", webUrl + queryString, false);
     xmlHttpObject.send();

     return xmlText;
    }
    // -->
    </script>

    <script type="text/javascript">
    <!--
    var g_isPostBack = false;

    window.onbeforeunload = function ()
    {
     if ( g_isPostBack == true )
      return;

     var closeMessage =
      'You are exiting this page.\n' +
      'If you have made changes without saving, your changes will be lost.\n' +
      'Are you sure that you want to exit?';

     if ( window.event )
     {
      // IE only...

      window.event.returnValue = closeMessage;
     }
     else
     {
      // Other browsers...

      return closeMessage;
     }

     g_isPostBack = false;
    }
    window.onunload = function ()
    {
     if ( g_isPostBack == true )
      return;

     var webUrl = 'LogoffPage.aspx';
     var queryString = '?LogoffDatabase=Y&UserID=' + '<%= Session["UserID"] %>';
     var returnCode = callAjax(webUrl, queryString);
     //alert(returnCode);
    }
    // -->
    </script>--%>

</head>
<body>  
<%--onload="window.print(); window.close();"--%>
    <form id="form1" runat="server">
    <div>
         <CR:CrystalReportViewer  ID="crViewer" runat="server" AutoDataBind="true" 
            HasZoomFactorList="False" 
            HasGotoPageButton="False" HasDrillUpButton="False" HasPrintButton="True" 
            HasSearchButton="False" HasToggleGroupTreeButton="False" HasCrystalLogo="False" 
            EnableParameterPrompt="false" BorderStyle="Double"
            onunload="crViewer_Unload" EnableDatabaseLogonPrompt="False" 
           HasExportButton="False"/>
    </div>
    </form>
</body>
</html>
