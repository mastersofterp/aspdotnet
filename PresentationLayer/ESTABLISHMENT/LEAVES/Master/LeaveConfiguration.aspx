<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeaveConfiguration.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Master_LeaveConfiguration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../../jquery/jquery-3.2.1.min.js"></script>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script type="text/javascript">
        //------------05/05/2022--start--------------
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                maxWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    maxWidth: '100%',
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });


        //------------05/05/2022--start--------------

    </script>

    <script type="text/javascript">
        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                //   InitAutoCompl();
            }

        });

        function GetLeave() {

            var LVNOarray = []
            var LVNO;

            var checkboxes = document.querySelectorAll("#Leaveno input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (checkboxes[i].value != 'multiselect-all') {
                    if (LVNO == undefined) {
                        LVNO = checkboxes[i].value + '$';
                    }
                    else {
                        LVNO += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnLeaveno.ClientID %>').val(Leaveno);
            alert(LVNO);
        }


    </script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE CONFIGURATION</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Leave And OD Configuration</h5>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divSMS" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Send Leave SMS :</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsSMS" runat="server" TabIndex="1" CssClass="form-control" ToolTip="IsLCT" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Send Leave MAIL :</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsMAIL" runat="server" TabIndex="2" CssClass="form-control" ToolTip="IsMail" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Send Charge handOver Mail :</label>
                                    </div>
                                    <asp:CheckBox ID="chkChargeMail" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Charge Handover Mail" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>OD Slip Capping Days :</label>
                                    </div>
                                    <asp:TextBox ID="txtODDays" runat="server" TabIndex="4" MaxLength="2" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" ToolTip="Enter OD Slip Capping Days" />
                                    <asp:RequiredFieldValidator ID="rfvoddays" runat="server" ControlToValidate="txtODDays"
                                        Display="None" ErrorMessage="Please Enter OD Slip Capping Days " ValidationGroup="Leave"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>OD Application  Capping Days:</label>
                                    </div>
                                    <asp:TextBox ID="txtODAppDays" runat="server" TabIndex="5" MaxLength="2" onkeypress="return CheckNumeric(event,this);" CssClass="form-control" ToolTip="Enter OD Application Capping Days" />
                                    <asp:RequiredFieldValidator ID="rfvodappdays" runat="server" ControlToValidate="txtODAppDays"
                                        Display="None" ErrorMessage="Please Enter OD Application  Capping Days " ValidationGroup="Leave"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Com off Valid Days:<%--<span style="color: #FF0000"> </span>--%></label>
                                    </div>
                                    <asp:TextBox ID="txtcomoffvalidmonth" runat="server" CssClass="form-control" ToolTip="Com off Valid Month" TabIndex="6"></asp:TextBox>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtcomoffvalidmonth"
                                        ValidChars="0123456789." Enabled="True">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Minimum Hours Full Day Com-off:<span style="color: #FF0000"> </span></label>
                                    </div>
                                    <asp:TextBox ID="txtminhoursfull" CssClass="form-control" runat="server" TabIndex="7"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="txtminhoursfull"
                                        Mask="99:99:99" MaskType="Time" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="Mevminhoursfull" runat="server" EmptyValueMessage="Please Enter Minimum Hours Full Day Com-off" ControlExtender="MaskedEditExtender8"
                                        ControlToValidate="txtminhoursfull" IsValidEmpty="false" ErrorMessage="Please  Enter Minimum Hours Full Day Com-off" SetFocusOnError="True"
                                        InvalidValueMessage="Time for Full Day Com-off Invalid" Display="None" TooltipMessage="Input a time" InvalidValueBlurredMessage="*"
                                        ValidationGroup="Leave" />
                                    <%--<asp:RequiredFieldValidator ID="rfvOut" runat="server" ControlToValidate="txthqouttime"
                                                        Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Leaveapp"
                                                        SetFocusOnError="True"> AcceptAMPM="True"
                                                    </asp:RequiredFieldValidator>--%>
                                </div>
                                <%--        <div class="form-group col-md-4">
                                                    <label>Minimum Hours Half Day Com-off:<span style="color: #FF0000">*</span> </label>
                                                    <asp:TextBox ID="txthourshalfday" CssClass="form-control" runat="server" ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="txthourshalfday"
                                                        Mask="99:99:99" MaskType="Time"  ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvIn" runat="server" ControlToValidate="txthqintime"
                                                        Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Leaveapp"
                                                        SetFocusOnError="True"> AcceptAMPM="True"
                                                    </asp:RequiredFieldValidator>--%>
                                <%--</div>--%>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Is Leave Wise Passing Path :</label>
                                    </div>
                                    <asp:CheckBox ID="chkLeavePath" runat="server" TabIndex="8" CssClass="form-control" ToolTip="Is Leave Wise Passing Path" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Director MailId:</label>
                                    </div>
                                    <asp:TextBox ID="txtmailid" runat="server" TabIndex="9" MaxLength="2" CssClass="form-control" ToolTip="Enter Director MailId" />
                                    <asp:RegularExpressionValidator ID="RegEmail" runat="server" ControlToValidate="txtmailid"
                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAutoApprove" runat="server">
                                    <div class="label-dynamic">
                                        <label>Is Leave Auto Approve</label>
                                    </div>
                                    <asp:CheckBox ID="chkAuto" runat="server" TabIndex="10" CssClass="form-control" ToolTip="Is Auto Approve" AutoPostBack="true" OnCheckedChanged="chkAuto_CheckedChanged" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divLeaveValid" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Leave Approval Valid Days:</label>
                                    </div>
                                    <asp:TextBox ID="txtLeaveValid" runat="server" TabIndex="11" MaxLength="2" CssClass="form-control" ToolTip="Enter Leave Approval Valid Days" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="divNotification" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Notification days for Leave Approval:</label>
                                    </div>
                                    <asp:TextBox ID="txtnotify" runat="server" TabIndex="12" MaxLength="2" CssClass="form-control" ToolTip="Enter Notification days for Leave Approval" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Required Approval on Direct Leave Application</label>
                                    </div>
                                    <asp:CheckBox ID="chkDirect" runat="server" TabIndex="13" CssClass="form-control" ToolTip="Is Approval on Direct Leave Application Required" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Holiday Other Than Sunday</label>
                                    </div>
                                    <asp:DropDownList ID="ddlworkingday" runat="server" TabIndex="14" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <%-- <asp:ListItem Value="1">MONDAY</asp:ListItem>
                                        <asp:ListItem Value="2">TUESDAY</asp:ListItem>
                                        <asp:ListItem Value="3">WEDNESDAY</asp:ListItem>
                                        <asp:ListItem Value="4">THURSDAY</asp:ListItem>
                                        <asp:ListItem Value="5">FRIDAY</asp:ListItem>--%>
                                        <asp:ListItem Value="6">SATURDAY</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Required Employee Wise Saturday Working</label>
                                    </div>
                                    <asp:CheckBox ID="chkEmployeewiseSaturday" runat="server" TabIndex="15" CssClass="form-control" ToolTip="Configration Check Mark for Employeewise Saturday Working" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Required Document compulsory for OD</label>
                                    </div>
                                    <asp:CheckBox ID="CkhOdDocument" runat="server" TabIndex="16" CssClass="form-control" ToolTip="Configration for Document Mandatory for Od Application" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Allow Late :</label>
                                    </div>
                                    <asp:TextBox ID="txtAllowLate" runat="server" TabIndex="17" CssClass="form-control" ToolTip="Enter Allow Late Time" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeAllowLate" runat="server" TargetControlID="txtAllowLate"
                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                    <ajaxToolKit:MaskedEditValidator ID="MevAllowLate" runat="server" EmptyValueMessage="Please Enter Time Allow Late" ControlExtender="meeAllowLate"
                                        ControlToValidate="txtAllowLate" IsValidEmpty="false" ErrorMessage="Please  Enter Time Allow Late" SetFocusOnError="True"
                                        InvalidValueMessage="Time for Allow Late Invalid" Display="None" TooltipMessage="Input a time" InvalidValueBlurredMessage="*"
                                        ValidationGroup="Leave" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Allow Early :</label>
                                    </div>
                                    <asp:TextBox ID="txtAllowEarly" runat="server" TabIndex="18" CssClass="form-control" ToolTip="Enter Allow Early Time" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeAllowEarly" runat="server" TargetControlID="txtAllowEarly"
                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevAllowEarly" runat="server" EmptyValueMessage="Please Enter Time Allow Early" ControlExtender="meeAllowEarly"
                                        ControlToValidate="txtAllowEarly" IsValidEmpty="false" ErrorMessage="Please  Enter Time Allow Early" SetFocusOnError="True"
                                        InvalidValueMessage="Time for Allow Early Invalid" Display="None" TooltipMessage="Input a time" InvalidValueBlurredMessage="*"
                                        ValidationGroup="Leave" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Permission Entry/Short Leave Valid in Month</label>
                                    </div>
                                    <asp:TextBox ID="txtpermissionvalid" runat="server" TabIndex="19" CssClass="form-control" MaxLength="2" ToolTip="Permission Entry/Short Leave Valid in Month" onkeypress="return CheckNumeric(event,this);" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Validate Leave Combination</label>
                                    </div>
                                    <asp:CheckBox ID="ChkValidateLeaveComb" runat="server" TabIndex="20" CssClass="form-control" ToolTip="Check Mark to Validate Leave Combination" />
                                </div>
<<<<<<< HEAD
=======
                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Not Allow any Leave in Continuation</label>
                                    </div>
                                    <asp:CheckBox ID="chkLeaveincont" runat="server" TabIndex="21" CssClass="form-control" ToolTip="Check Mark to Not Allow any Leave in Continution" />
                                </div>
>>>>>>> 1b3fb3d ([FEATURE] [48677] Changes on Leave application,Leave Configration)
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Show LWP/Loss Of Pay Leave on Leave Card</label>
                                    </div>
                                    <asp:CheckBox ID="ChkshowLwpLeave" runat="server" TabIndex="21" CssClass="form-control" ToolTip="Check Mark to Show LWP leave On Leave Card" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Lwp Apply  Configration</h5>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Not Allow to Apply LWP if Balance is Available selected Leaves</label>
                                    </div>
                                    <asp:CheckBox ID="chkLwpstop" runat="server" TabIndex="22" CssClass="form-control" ToolTip="Check Mark to Not Allow to Apply LWP If other leave Balance available" AutoPostBack="true" OnCheckedChanged="chkLwpstop_CheckedChanged" />
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12" id="divleavename" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Leave Name</label>
                                    </div>
                                    <asp:ListBox ID="ddlLeave" runat="server" AppendDataBoundItems="true" TabIndex="23" CssClass="form-control multi-select-demo"
                                        SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="Button1" runat="server" Text="Submit" ValidationGroup="Leave" OnClick="btnSave_Click"
                            CssClass="btn btn-primary" ToolTip="Click here to Save" TabIndex="24" />
                        &nbsp;     
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leave"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>

                    <asp:HiddenField ID="hdnLeaveno" runat="server" />


                </div>
            </div>
        </div>
    </div>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%-- <td class="vista_page_title_bar" valign="top" style="height: 30px">ATTENDANCE CONFIGURATION&nbsp;               
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
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
            </td>
        </tr>
    </table>

    <%--  Enable the button so it can be played again --%>            <%# Eval("Leave_Name")%>
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
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }

        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }

        //------------05/05/2022--start--------------
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                maxWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    maxWidth: '100%',
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });


        //------------05/05/2022--start--------------

    </script>
</asp:Content>

