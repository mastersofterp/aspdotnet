<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Leave_Joining_List_Report.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_Leave_Joining_List_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }
    
    </script>
    
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
               Joining List Report &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
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
            <td style="height: 16px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width ="100%">
                <br />
                <asp:Panel ID="pnlAdd" runat="server" Width="900px">
                    <div style="text-align: left; padding-left: 10px; width: 92%">
                        <fieldset class="fieldsetPay">
                            <table>
                                <tr>
                                    <td style="width: 850px">
                                        <br />
                                        <table cellpadding="0" cellspacing="0" width ="100%">
                                                                             
                                            <tr>
                                                <td class="form_left_label">
                                                    Select Date
                                                </td>
                                                <td class="form_left_label">
                                                    :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                                        Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                                    <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
                                                    ControlExtender="meeFromdt" ControlToValidate="txtFromdt" display="None" 
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date" 
                                                    InvalidValueBlurredMessage="Invalid Date" 
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" 
                                                    SetFocusOnError="true" TooltipMessage="Please Enter From Date" 
                                                    ValidationGroup="Leaveapp">
                                                </ajaxToolKit:MaskedEditValidator>--%>
                                               
                                               
                                               
                                              
                                            </tr>
                                            
                                            
                                            
                                            
                                            <tr id="trchkdept" runat ="server">
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:CheckBox ID="chkDept" Text ="Department wise " runat="server"  AutoPostBack ="true" 
                                                        oncheckedchanged="chkDept_CheckedChanged" />
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text" style="width: 110px">
                                                   
                                                </td>
                                            </tr>
                                            
                                            <tr id="trddldept" runat ="server" visible ="false">
                                                <td class="form_left_label">
                                                    Select Department
                                                </td>
                                                <td class="form_left_label">
                                                    :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddldept" runat="server"  AppendDataBoundItems ="true" Width ="80%" AutoPostBack ="true">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text" style="width: 110px">
                                                   
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:RadioButtonList ID="rblAllParticular" runat="server" 
                                                        RepeatDirection ="Horizontal" Width ="100%" 
                                                        onselectedindexchanged="rblAllParticular_SelectedIndexChanged" AutoPostBack ="true">
                                                    <asp:ListItem Enabled ="true" Selected ="True" Text ="All Employees" Value ="0"></asp:ListItem>
                                                    <asp:ListItem Enabled ="true" Text ="Particular Employee" Value ="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text" style="width: 110px">
                                                   
                                                </td>
                                            </tr>
                                            
                                            <tr id="tremp" runat ="server">
                                                <td class="form_left_label">
                                                    Select Employee
                                                </td>
                                                <td class="form_left_label">
                                                    :
                                                </td>
                                                <td class="form_left_text" >
                                                    <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems ="true" Width ="80%">
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                        Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue ="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_label">
                                                    
                                                </td>
                                                <td class="form_left_text" style="width: 110px">
                                                   
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 599px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_button" align="center" style="width: 599px">
                                        <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp"
                                            Width="80px" onclick="btnReport_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                            Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
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
    
//    function showLvstatus(source)
//    {   
//        this._source=source;
//        
//        //__doPostBack(this._source.name, '');
//        __doPostBack(this._source.name,'');
//        this._popup=$find('mdlPopupView');
//        
//        this._popup.show();
//    }    
//    function backClick()
//    {
//       this._popup.hide();        
//       this._source = null;
//       this._popup = null; 
//    }
//    
    </script>
    
     <div id="divMsg" runat="server">
    </div>
</asp:Content>

