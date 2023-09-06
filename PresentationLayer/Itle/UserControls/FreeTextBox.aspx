<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FreeTextBox.aspx.cs" Inherits="ITLE_User_Controls_FreeTextBox" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>A wrapper for the freetextbox in an atlas:updatepanel </title>
    <style type="text/css">
        body
        {
            background: #ffdaa0;
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <FTB:FreeTextBox ID="ftb" runat="server" SupportFolder="~/" ClientSideTextChanged="onFtbClientTextChanged">
        </FTB:FreeTextBox>
    </div>

    <script type="text/javascript"> //The lengthy constructor is there so that firefox also knows where to get the text. 
function onFtbClientTextChanged ()
{ 
 window.parent.document.getElementById('<%= MainPageField %>').value = document.getElementById('<%= ftb.ClientID %>_designEditor').contentWindow.document.body.innerHTML;
} ; 

if(navigator.userAgent.indexOf("Firefox")!=-1) document.getElementById("<%= ftb.ClientID%>_htmlEditorArea").addEventListener('change', onFtbClientTextChanged , true ); //for firefox script > 

    </script>

    </form>
</body>
</html>
