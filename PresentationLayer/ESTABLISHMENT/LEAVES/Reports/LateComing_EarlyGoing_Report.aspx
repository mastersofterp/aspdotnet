<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LateComing_EarlyGoing_Report.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_LateComing_EarlyGoing_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        LATE COMING AND THUMB PROBLEM APPROVAL &nbsp;
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
                <tr>
                    <td>
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
                                    Edit Record
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
                            function Cover(bottom, top, ignoreSize) 
                            {
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

                        <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
                <tr>
                    <td>
                        <br />
                        <asp:Panel ID="pnlAdd" runat="server" Width="613px">
                            <div style="text-align: left; width: 87%; padding-left: 10px;">
                                <fieldset class="fieldsetPay">
                                    <legend class="legendPay">Approval Report</legend>
                                    <table>
                                        <tr>
                                            <td style="width: 699px">
                                                <br />
                                                <table cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td class="form_left_label" style="width: 25%">
                                                        </td>
                                                        <td class="form_left_text">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="form_left_label">
                                                            From Date :
                                                        </td>
                                                        <td class="form_left_text" width="70%">
                                                            <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" Width="100px" />
                                                            <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                                ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                            &nbsp;&nbsp; To Date :
                                                            <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" Width="100px" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Holiday"
                                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                                ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trdept" runat="server">
                                                        <td class="form_left_label" style="width: 25%">
                                                            Department :
                                                        </td>
                                                        <td class="form_left_text">
                                                            <asp:DropDownList ID="ddlDept" runat="server" Width="300px" AppendDataBoundItems="true"
                                                                AutoPostBack="false" >
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="Shift"
                                                                SetFocusOnError="True" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trdesig" runat="server">
                                                        <td class="form_left_label" style="width: 25%">
                                                            Work Type :
                                                        </td>
                                                        <td class="form_left_text">
                                                            <asp:DropDownList ID="ddlWorkType" runat="server" Width="300px" AppendDataBoundItems="true">
                                                            </asp:DropDownList>
                                                             <asp:RequiredFieldValidator ID="rfvWorkType" runat="server" ControlToValidate="ddlWorkType"
                                                        Display="None" ErrorMessage="Please Select Work Type" ValidationGroup="Shift"
                                                        SetFocusOnError="True" InitialValue ="0"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="form_left_label" style="width: 25%">
                                                            <%--Select:--%>
                                                        </td>
                                                        <td class="form_left_text">
                                                            <%--<asp:RadioButtonList ID="rblcondn" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rblcondn_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="Forgot to Punch" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Late Comers" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text ="Early Going" Value ="2"></asp:ListItem> 
                                                            </asp:RadioButtonList>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_button" align="center" style="width: 499px">
                                                 <asp:Button ID="btnreport" runat="server" Text="Report" CausesValidation="false"
                                                    Width="80px" ValidationGroup="Shift" onclick="btnreport_Click" />&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 499px">
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 499px">
                                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left: 10px">
                        <asp:Panel ID="pnlList" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 90%; text-align: center">
                                <tr>
                                    <td style="text-align: left; padding-left: 10px; padding-top: 10px;">
                                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" Width="90px" 
                                            Visible="False" UseSubmitBehavior="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" width="100%">
                                        
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <%--<asp:RadioButtonList ID="rblcondn" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                OnSelectedIndexChanged="rblcondn_SelectedIndexChanged">
                                                                <asp:ListItem Enabled="true" Text="Forgot to Punch" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Late Comers" Value="1"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text ="Early Going" Value ="2"></asp:ListItem> 
                                                            </asp:RadioButtonList>--%>
            </tr>
        </table>
    </center>
                <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>    <%--<%# Eval("INTIME")%>--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>
                        &nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;
    
    function showConfirmDel(source){
        this._source = source;
        this._popup = $find('mdlPopupDel');
        
        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }
    
    function okDelClick(){
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }
    
    function cancelDelClick(){
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }


    function checkAllEmployees(chkcomplaint) {
        var frm = document.forms[0];
        for (i = 0; i < document.forms[0].elements.length; i++) {
            var e = frm.elements[i];
            if (e.type == 'checkbox') {
                if (chkcomplaint.checked == true)
                    e.checked = true;
                else
                    e.checked = false;
            }
        }
    }
    function enabledisablePhotoCheckBox(chk) {
        if (chk.checked == true)
            document.getElementById("ctl00_ctp_chkPhoto").disabled = true;
        else
            document.getElementById("ctl00_ctp_chkPhoto").disabled = false;
    }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
