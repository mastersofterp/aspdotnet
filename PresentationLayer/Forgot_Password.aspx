<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgot_Password.aspx.cs" Inherits="_default" 
    StylesheetTheme="Theme1" Theme="Theme1" %>

  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register Src="~/UC/feed_back.ascx" TagName="feedback" TagPrefix="uc1" %>--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head >
<link rel="shortcut icon" href="IMAGES/indoreuniversity.png" type="image/x-icon" />

    <title></title>
   <link href="Css/master.css" rel="stylesheet" type="text/css" />
    <link href="Css/Theme1.css" rel="stylesheet" type="text/css" />
    
    <style>
          .bottomRight
        {
            bottom: 0px;
            right: 0px;
            background-color: #4a4a4a;
            color: #fff;
            font-size: 10px;
            padding: 10px;
            bottom: 0;
            position: fixed;
            width: 100%;
            text-align: center;
        }
    </style>
      <style type="text/css">
        .style2
        {
            font-size: x-large;
            font-family: Arial;
            font-weight: bold;
        }
        .style3
        {
            font-size: small;
            font-family: Arial;
            font-weight: bold;
            font-style: italic;
        }
        .style4
        {
            width: 41%;
        }
        .style5
        {
            color: #993333;
        }
        .style6
        {
            color: #FF0066;
        }
    </style>
</head>
<body>
    <form id="frmDefault" runat="server">  <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <%--<ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="server" ID="Script_water" />--%>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td class="header" style="padding-top: 5px; padding-left: 0px">
                <div>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="100%" valign="top" ">
                            <table width="100%" cellspacing="0" class="login_head_bg">
                                
                                <tr>
            <td style="width: 75%; font-family: 'Times New Roman'; font-size: 18px; padding:15px 0; " >
               <%-- <asp:Label ID="lblColName" runat="server" />--%>
                <img id="Img1" runat="server" src="~/IMAGES/indorebanner.png" />
            </td> 
          
            <td style="text-align: right; vertical-align: middle; padding-right: 15px; width: 25%">
                
                
            </td>
        </tr>
                            </table>
                        </td>
                    </tr>
                </table>
        </div>
                <hr style="background-color:#1c8a99; border:2px solid #1c8a99; width:100%;">
            </td>
        </tr>
        <tr >        
                               <td><center>
    
              <span style="color: Red; font-size:larger; ">Enter your registered email and we will send your reset password in your email to get you back to your account.</span>
                               </center></td>
                                </tr>
        <tr>
            <td height="368" align="center" style="padding: 20px;">
                <!--main table for body part-->
                <table width="85%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="28%" align="right" valign="top">
                        </td>
                        <td align="center" valign="top" class="style4">
                            <!--  login tabel-->
                            <table cellpadding="2" cellspacing="0" class="login_bg" style="width:100%; text-align:center;">
                    <tr>
                        <td colspan="2" style="text-align: left; padding: 10px 10px 10px 20px; background-color: #1B7A87;  border-bottom: #9AE1EA 3px solid;">
                            <span style="font-family: 'Times New Roman'Arial; font-size: 14pt; font-weight: bold; color:white; ">Forgot the Password?</span>
                            <span class="style3">Not a problem ...just reset it...
                                        </span>
                        </td>
                    </tr>
                    <tr>
                        <%--<td width="40%" align="right" class="name">
                            <asp:Label ID="lblUsername" runat="server" Text="Username :" SkinID="lblHead" />
                        </td>--%>
                     <td width="100%" colspan="2" style="padding:30px 10px 10px; ">

                          <asp:TextBox ID="txt_username" runat="server" Width="202px" TabIndex="1" Height="21px"  CssClass="unwatermarked"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_username"
                                                        ErrorMessage="Username Required !" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                        TargetControlID="txt_username" WatermarkText="Enter Username" />
                        </td>
                    </tr>
                    <tr>
                       <%-- <td width="40%" align="right" class="name">
                            <asp:Label ID="lblPassword" runat="server" Text="Password :" SkinID="lblHead" />
                        </td>--%>
                        <td  width="100%" colspan="2" style="padding:10px;">
                           <asp:TextBox ID="txt_emailid" runat="server" TabIndex="2" Width="200" CssClass="unwatermarked"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Email Id Required !"
                                                        ControlToValidate="txt_emailid" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_emailid"
                                                        Display="None" ErrorMessage="Enter Valid Email ID" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="login"></asp:RegularExpressionValidator>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                        TargetControlID="txt_emailid" WatermarkText="Enter Registered Email Id" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 5px 0;">
                             <asp:TextBox ID="txt_captcha" runat="server" TabIndex="2" Width="200px" Height="20px"  CssClass="unwatermarked"></asp:TextBox>
                                                    <ajaxToolkit:TextBoxWatermarkExtender ID="txt_captcha_TextBoxWatermarkExtender" runat="server"
                                                        TargetControlID="txt_captcha" WatermarkText="Enter following captcha Character" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter following Charactor"
                                                        ControlToValidate="txt_captcha" Display="None" ValidationGroup="login"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <br />
                                                    <img src="GenerateCaptcha.ashx" style="width: 150px; height: 50px;" />
                                                    <asp:ImageButton ID="imgrefresh" runat="server" ImageUrl="~/IMAGES/refresh.gif" Height="20px"
                                                        OnClick="imgrefresh_Click" Width="20px" />
                        </td>
                    </tr>
                      <tr>
                        <td colspan="2" style="padding-bottom: 5px; padding-top: 5px; padding-right:10px;" class="btnTd">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                                        OnClick="btnSubmit_Click" ValidationGroup="login"  CssClass="loginbtn" /> &nbsp&nbsp
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                                         onclick="btnCancel_Click"  CssClass="loginbtn"
                                                         />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:ValidationSummary ID="val_summary" runat="server" Height="38px" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="login" DisplayMode="List" />
                                                    <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                        </td>
                    </tr>
                     
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                </table>
                            
                            <!-- end login table-->
                        </td>
                        <td width="28%" valign="top">
                            <!--news & event table-->
                            <!--  end table-->
                        </td>
                    </tr>
                </table>
                <!--    end main table-->
            </td>
        </tr>
        <tr>
            <%--<td class="footer" align="center" style="padding-top: 5px">
                <a href="http://iitms.co.in/" target="_blank" title="Click Here">
                    <img src="IMAGES/footer.png" width="146" height="29" border="0" alt="footer" /></a>
            </td>--%>
        </tr>
                   
    </table>
         <div class="bottomRight">
                                                  COPYRIGHT © 2015. ALL RIGHTS RESERVED.
  

        </div>
    <asp:HiddenField ID="hdfHeight" runat="server" />

    <script type="text/javascript">
        var screenHeight = screen.availHeight;

        function setRes() {
            document.getElementById('<%=hdfHeight.ClientID %>').value = screenHeight;

        }
    </script>

    <script type="text/javascript">
        function MM_swapImgRestore() { //v3.0
            var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
        }
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
            }
        }

        function MM_findObj(n, d) { //v4.01
            var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
                d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
            }
            if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
            for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
            if (!x && d.getElementById) x = d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
            var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2) ; i += 3)
                if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
        }
    </script>

    </form>
</body>
</html>

