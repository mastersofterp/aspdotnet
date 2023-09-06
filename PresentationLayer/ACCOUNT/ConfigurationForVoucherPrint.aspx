<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigurationForVoucherPrint.aspx.cs"
    Inherits="ACCOUNT_ConfigurationForVoucherPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Configuration For Voucher Print</title>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
     
   

    <link href="Css/linkedin/tabs.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/blue/linkedin-blue.css" rel="stylesheet" type="text/css" />
    <link href="includes/modalbox.css" rel="stylesheet" type="text/css" />
</head>
<body class="body">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="3">
                    Configuration For Voucher Print
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                        font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                                <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                        </div>
                    </div>

                    <script type="text/javascript" language="javascript">
                        // Move an element directly on top of another element (and optionally
                        // make it the same size)
                        function Cover(bottom, top, ignoreSize) {
                            var location = Sys.UI.DomElement.getLocation(bottom);
                            top.style.position = 'absolute';
                            top.style.top = location.y + 'px';
                            top.style.left = location.x + 'px';
                            if (!ignoreSize) {
                                top.style.height = bottom.offsetHeight + 'px';
                                top.style.width = bottom.offsetWidth + 'px';
                            }
                        }
                    </script>

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>

                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <%--  Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <%--  Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%--  Enable the button so it can be played again --%>
                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                        </Sequence>
                    </OnClick>
                    <OnMouseOver>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                    </OnMouseOver>
                    <OnMouseOut>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                    </OnMouseOut>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                </td>
            </tr>
            <tr >
                <td style="padding: 10px " >
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    <center>
                    <fieldset class="fieldset" style="width:100%;">
                        <legend class="legend">Add/Modify - Signature<br />
                        </legend><span style="font-weight: normal"></span> <span style="color: red;
                            font-weight: normal"></span>
                        <br />
                        <br />
                        <asp:UpdatePanel ID="UPDMainGroup" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                        <td class="form_left_label" style="width: 12%">
                                            First Signature:
                                        </td>
                                        <td class="form_left_text" style="width: 30%">
                                            <asp:TextBox ID="txtFirst" runat="server" enableSelection="Yes" 
                                                Text="" ToolTip="Please Enter First Signature" Width="80%"></asp:TextBox>
                                        
                                            
                                            <%--<ajaxToolKit:ListSearchExtender ID="lstGroup_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="lstGroup">
                                            </ajaxToolKit:ListSearchExtender>--%> 
                                            
                                           
                                            
                                        </td>
                                        <td class="form_left_label" style="width: 12%">
                                            Second Signature :
                                        </td>
                                        <td class="form_left_text" style="width:30%">
                                            <asp:TextBox ID="txtSecond" runat="server" enableSelection="Yes" 
                                                Text="" ToolTip="Please Enter Second Signature" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 12%">
                                            Third Signature :
                                        </td>
                                        <td class="form_left_text" style="width:30%">
                                            <asp:TextBox ID="txtThird" runat="server" enableSelection="Yes" 
                                                Text="" ToolTip="Please Enter Third Signature" Width="80%"></asp:TextBox>
                                        </td>
                                        <td class="form_left_label" style="width: 12%">
                                            Fourth Signature :
                                        </td>
                                        <td class="form_left_text" style="width:30%">
                                            <asp:TextBox ID="txtFourth" runat="server" enableSelection="Yes" 
                                                Text="" ToolTip="Please Enter Third Signature" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td class="form_left_label" style="width: 12%">
                                            Fifth Signature :
                                        </td>
                                        <td class="form_left_text" style="width: 30%">
                                            <asp:TextBox ID="txtFifth" runat="server" enableSelection="Yes" 
                                                Text="" ToolTip="Please Enter Third Signature" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                    <td>&nbsp;
                                    </td>
                                    </tr>
                                    <tr>
                                    
                                     
                                        <td class="form_left_text" colspan="5" align="center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                Width="80px" onclick="btnSubmit_Click"/>&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                Width="80px"  onclick="btnCancel_Click"/>
                                        </td>
                                  
                                    </tr>
                                </table>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                    </fieldset>
                    </center>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
