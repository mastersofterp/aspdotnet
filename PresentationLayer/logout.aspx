<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/UC/feed_back.ascx" TagName="feedback" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="images/nitlogo.ico" type="image/x-icon" />

    <title></title>
    <link href="Css/master.css" rel="stylesheet" type="text/css" />
    <link href="Css/Theme1.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #E3E3E3">
    <form id="frmLogout" runat="server">
    <ajaxToolkit:ToolkitScriptManager EnablePartialRendering="true" runat="server" ID="Script_water" />
    <table cellpadding="0" cellspacing="0" width="100%" align="center">
        <tr>
            <td colspan="3" align="center">
                <table width="100%" cellspacing="0" class="login_head_bg">
                    <tr>
                        <td width="78px" valign="top">
                            <asp:ImageButton ID="imgLogo" runat="server" PostBackUrl="~/default.aspx" Height="64px"
                                Width="64px" />
                        </td>
                        <td colspan="2">
                            <table width="100%" cellspacing="0" class="login_head_bg">
                                <tr>
                                    <td style="width: 80%; text-align: center; font-size: 16pt; font-family: Verdana;
                                        font-weight: bold;">
                                        <span id="spnHead" runat="server" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="width: 80%; text-align: center; font-size: 12pt; font-family: Verdana;
                                        font-weight: lighter">
                                        <span id="SpnAddress" runat="server" />
                                            
                                    </td>
                                    
                                </tr>
                            </table>
                        </td>
                        <td width="10%" valign="top">
                        <img src="IMAGES/HomeButton.jpg" width="94" height="67" border="0" usemap="#Map" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 35%; padding: 5px" align="center">
                <table cellpadding="0" cellspacing="0" style="background-color: White; width: 330px">
                    <tr>
                        <td colspan="3" class="def_blk_head">
                            Logging Out
                        </td>
                    </tr>
                    <tr>
                        <td class="def_blk_left_right" width="11px">
                            &nbsp;
                        </td>
                        <td width="302px">
                            <br />
                            You have successfully logged out!!
                            <br />
                            <br />
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
        </tr>
    </table>
    <map name="Map" id="map">
        <area shape="rect" coords="14, 7, 79, 61" href="default.aspx" alt="Home" />
    </map>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR FEEDBACK --%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolkit:ModalPopupExtender ID="modalFeedBack" BehaviorID="mdlPopup" runat="server"
        TargetControlID="div" PopupControlID="div" OkControlID="fb$btnOk" OnOkScript="okClick();"
        CancelControlID="fb$btnCancel" OnCancelScript="cancelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <uc1:feedback ID="fb" runat="server" />
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

    <%--END : FEEDBACK--%>
    </form>
</body>
</html>
