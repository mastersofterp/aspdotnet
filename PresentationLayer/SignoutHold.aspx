<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignoutHold.aspx.cs" Inherits="SignoutHold" Title="Wait" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            font-size: large;
            text-align: center;
        }
        .style2
        {
            width: 410px;
        }
        .style3
        {
            width: 450px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  <div>
<asp:ScriptManager ID= "SM1" runat="server"></asp:ScriptManager>
<asp:Timer ID="timer1" runat="server" 
Interval="1000" OnTick="timer1_tick"></asp:Timer>
</div>

<div>
<asp:UpdatePanel id="updPnl" 
runat="server" UpdateMode="Conditional">
<ContentTemplate>
 <table style="width:100%;">
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
              
                <table style="width:53%;">
                    <tr>
                        <td class="style3" align ="center" >
                            <b><span class="style1">You did not logout successfully from your last session
                           </span></b></td>
                    </tr>
                    <tr>
                        <td class="style3" align ="center">
                            <b><span class="style1">Please wait for
                            <asp:Label ID="lblTimer" runat="server"></asp:Label>
                            Seconds...while we process your request.</span></b></td>
                    </tr>
                    <tr>
                        <td class="style3" align="center" >
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                ImageUrl="~/IMAGES/anim_loading_75x75.gif" />
                        </td>
                    </tr>
                </table>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="timer1" EventName ="tick" />
</Triggers>
</asp:UpdatePanel>
</div>
<style type="text/css">
.divN 
{
	width: 400px;
height: 127px;

border: 4px #1e458a dashed;

background-color: #e5edfa;
}
.hr{
border:0;
border-top:1px solid #ccc;
margin-bottom:-10px
}
.divY
{
width: 190px;
height: 190px;

-ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=70)";
filter: alpha(opacity=70);
-moz-opacity: 0.7;
-khtml-opacity: 0.7;
opacity: 0.7;
border: 20px #ff1a1a inset;

background-color: #cc1e1e;

padding: 0px;

-webkit-border-radius: 190px;
-moz-border-radius: 190px;
border-radius: 190px;

-webkit-border-top-right-radius: 0px;
-moz-border-radius-topright: 0px;
border-top-right-radius: 0px;

-moz-box-shadow: -15px 29px 5px 0px #c46e6e;
-webkit-box-shadow: -15px 29px 5px 0px #c46e6e;
box-shadow: -15px 29px 5px 0px #c46e6e;
}
.span {
  display: inline-block;
  vertical-align: middle;
  line-height: normal;      
}
</style>
<div class="divN" style=" position:absolute;bottom:0px; width:98%; height:10%; font-size:xx-large; color:Navy; text-align:center; vertical-align:middle; line-height:normal" ><span>Do Not Forget To Logout</span></div>
    </form>
   
</body>
</html>
