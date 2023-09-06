<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="UpdatePassword"
    StylesheetTheme="Theme1" Theme="Theme1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="imgnew1/favicon.ico" type="image/x-icon" />
    <title></title>
    <link href="Css/master.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-image: url('images/body_bg.gif'); background-repeat: repeat">
    <form id="frmUpdatePassword" runat="server">
    <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="server" ID="Script_water" />
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="3">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="100%" valign="top">
                            <table width="100%" cellspacing="0" class="login_head_bg">
                                <tr>
                                    <td width="44%" valign="top">
                                        <img src="images/head_01.jpg" width="427" height="67" />
                                    </td>
                                    <td width="27%">
                                        &nbsp;
                                    </td>
                                    <td width="29%" valign="top">
                                        <img src="images/head_033.jpg" width="281" height="67" border="0" usemap="#Map" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; vertical-align: top; width: 30%; padding: 5px">
                &nbsp;
            </td>
            <td style="text-align: left; vertical-align: top; width: 35%; padding: 5px">
                <%--GET REGISTERED--%>
                <table cellpadding="0" cellspacing="0" style="background-color: White; width: 330px">
                    <tr>
                        <td colspan="3" class="def_blk_head">
                            Change Password
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                    <tr>
                        <td class="def_blk_left_right" width="11px">
                            &nbsp;
                        </td>
                        <td width="302px">
                            <%--PAGE HELP--%>
                            <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                            <!-- "Wire frame" div used to transition from the button to the info panel -->
                            <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                                border: solid 1px #D0D0D0;">
                            </div>
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
                                        <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                        Edit Record&nbsp;&nbsp;
                                        <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                        Delete Record
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

                            <ajaxToolkit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                                <Animations>
                                        <OnClick>
                                            <Sequence>
                                                <%-- Disable the button so it can't be clicked again --%>
                                                <EnableAction Enabled="false" />
                                                
                                                <%-- Position the wire frame on top of the button and show it --%>
                                                <ScriptAction Script="Cover($get('btnHelp'), $get('flyout'));" />
                                                <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                                
                                                <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                                                <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                                    <Move Horizontal="150" Vertical="-50" />
                                                    <Resize Width="260" Height="280" />
                                                    <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                                </Parallel>
                                                
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
                            </ajaxToolkit:AnimationExtender>
                            <ajaxToolkit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
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
                            </ajaxToolkit:AnimationExtender>
                        </td>
                        <td class="def_blk_left_right" width="11px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="def_blk_left_right" width="11px">
                            &nbsp;
                        </td>
                        <td width="302px">
                            <asp:UpdatePanel ID="upRegister" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2" class="form_button">
                                                <span style="font-size: 8pt; font-weight: bold;">As you have logged in for first time,<br />
                                                    it is mandatory to change your password.</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text">
                                                <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Old Password :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="20" TextMode="Password"
                                                    Wrap="False" />
                                                <asp:RequiredFieldValidator ID="rfvOldPass" runat="server" ControlToValidate="txtOldPassword"
                                                    Display="None" ErrorMessage="Old Password Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                New Password :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20"
                                                    Wrap="False" />
                                                <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="txtNewPassword"
                                                    Display="None" ErrorMessage="New Password Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                Confirm Password :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20"
                                                    Wrap="False" />
                                                <asp:RequiredFieldValidator ID="rfvConfirmPass" runat="server" ControlToValidate="txtConfirmPassword"
                                                    Display="None" ErrorMessage="Confirm Password Required" ValidationGroup="changePassword"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="New &amp; Confirm Password Not Matching"
                                                    ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                                    ValidationGroup="changePassword"></asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label">
                                                &nbsp;
                                            </td>
                                            <td class="form_left_text">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                    ValidationGroup="changePassword" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CausesValidation="False" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="changePassword"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMessage" runat="server" SkinID="Errorlbl" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="def_blk_left_right" width="11px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="def_blk_bot">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: center; vertical-align: top; width: 35%; padding: 5px">
                &nbsp;
            </td>
        </tr>
    </table>
    <map name="Map" id="map">
        <area shape="rect" coords="24,41,75,62" href="default.aspx" alt="Home" />
        <area shape="rect" coords="109,43,182,65" alt="Feedback" onclick="showConfirm(this); return false;"
            style="cursor: hand" />
        <area shape="rect" coords="214,42,253,65" href="faq.aspx" alt="FAQs" />
    </map>
    <%--FEEDBACK MODALPOPUP--%>
    <ajaxToolkit:ModalPopupExtender ID="modalFeedBack" BehaviorID="mdlPopup" runat="server"
        TargetControlID="div" PopupControlID="div" OkControlID="fb$btnOk" OnOkScript="okClick();"
        CancelControlID="fb$btnCancel" OnCancelScript="cancelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <%--<uc1:feedback ID="fb" runat="server" /> --%>
        </div>
    </asp:Panel>

    <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirm(source){    
        this._source = source;
        this._popup = $find('mdlPopup');
        
        //  find the confirm ModalPopup and show it
        this._popup.show();
    }
    
    function okClick(){
        //  find the confirm ModalPopup and hide it
        //this._popup.hide();
        //  use the cached button as the postback source
        
       var btnsrc = document.getElementById('fb$btnOk');
        __doPostBack(btnsrc.name, '');
    }
    
    function cancelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }
    </script>

    </form>
</body>
</html>
