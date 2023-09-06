<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="homenew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title ></title>
<script language="javascript" type="text/javascript">
    function setIframeHeight(iframe) {
        
        if (iframe) {
            var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
            if (iframeWin.document.body) {
                iframe.height = 17 + iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            }
        }
    };
 </script>
</head>


<body >
   
    <form id="form1" runat="server"> 
    
    <iframe name="frame1" src="home.aspx"   
        frameborder="0" scrolling="auto"  
        style="left:0px; margin-left:0px; height: 620px; width: 100%;"  onload ="setIframeHeight(this)">  
  </iframe> 
        
  
    </form>
</body>
</html>
