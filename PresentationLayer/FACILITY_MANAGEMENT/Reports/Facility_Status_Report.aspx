<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Facility_Status_Report.aspx.cs" Inherits="Facility_Status_Report" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Facility Status Report</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="panel panel-info">
                                <div class="panel-heading" style="font-weight: 600; font-size: 13px">Facility Application Pending/Approved Status Report</div>
                                <div class="panel-body">
                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>* Marked Is Mandatory !</span></p>
                                        </div>
                                    </div>


                                    <asp:UpdatePanel ID="updAdd" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlAdd" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-md-3">
                                                        <label>From Date :<span style="color: Red">*</span> </label>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <%-- <asp:Image ID="imgCalJoindt" runat="server" ImageUrl="~/images/calendar.png"
                                                                                            Style="cursor: pointer" />--%>
                                                                <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue" style="cursor: pointer"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtFromdt" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                                CssClass="form-control" Style="z-index: 0;" TabIndex="1" ToolTip="Enter From Date"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtFromdt">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="mefrmdt" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                                            <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromdt"
                                                                Display="None" ErrorMessage="Please Select / Enter From Date" SetFocusOnError="True"
                                                                ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditValidator ID="mevFacilityDt" runat="server" ControlExtender="mefrmdt"
                                                                ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                                InvalidValueMessage=" From Date is Invalid (Enter dd/mm/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <label>To Date :<span style="color: Red">*</span> </label>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <%-- <asp:Image ID="imgCalJoindt" runat="server" ImageUrl="~/images/calendar.png"
                                                                                            Style="cursor: pointer" />--%>
                                                                <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue" style="cursor: pointer"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10"
                                                                CssClass="form-control" Style="z-index: 0;" TabIndex="2" ToolTip="Enter To Date" />

                                                            <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                                Display="None" ErrorMessage="Please Select / Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>

                                                            <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtTodt" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeTodt"
                                                                ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                                InvalidValueMessage=" To Date is Invalid (Enter dd/mm/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-3" id="trddldept" runat="server">
                                                        <label>Department :</label>

                                                        <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true" ToolTip="Select Department"
                                                            CssClass="form-control">
                                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group col-md-3" id="Div2" runat="server">
                                                        <label>Centralize Facility Name :</label>

                                                        <asp:DropDownList ID="ddlFacility" runat="server" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true" ToolTip="Select Centralize Facility Name"
                                                            CssClass="form-control">
                                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="form-group col-md-4" id="tr1" runat="server">
                                                        <label>Application Status :</label>

                                                        <asp:RadioButtonList ID="rdbleavestatus" runat="server" TabIndex="9" ToolTip="Select Application Status"
                                                            RepeatDirection="Horizontal"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Enabled="true" Selected="True" Text="Approved" Value="A"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="Pending" Value="P"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="Rejected" Value="R"></asp:ListItem>
                                                            <asp:ListItem Enabled="true" Text="Cancelled" Value="C"></asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </div>
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <div class="box-footer">
                                                        <p class="text-center">

                                                            <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp" TabIndex="10" ToolTip="Click To Show the Report"
                                                                CssClass="btn btn-info" OnClick="btnReport_Click" />

                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" ToolTip="Click To Reset"
                                                                CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                        </p>
                                                        <div class="col-md-12">
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                    </div>
                </form>
            </div>

        </div>

    </div>




    <table cellpadding="0" cellspacing="0" width="100%">
        <%--<tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">Leave Approval/Pending Status &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>--%>
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
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
        <tr>
            <td style="height: 16px">&nbsp;
            </td>
        </tr>
        <tr>
            <td class="form_left_label">
                <%-- <span style="font-size: 9pt"><b>Note : <span style="color: Red">*</span></b> denotes mandatory fields</span>--%>
            </td>
        </tr>
        <tr>
            <td width="100%">
                <br />
                <%--<asp:UpdatePanel ID="updAdd" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAdd" runat="server" Width="900px">--%>
                <div style="text-align: left; padding-left: 10px; width: 92%">
                    <%--<fieldset class="fieldset">
                        <legend class="legend">Leave Pending/Approved Status Report</legend>--%>
                    <table>
                        <tr>
                            <td style="width: 850px">
                                <br />
                                <table cellpadding="0" cellspacing="0" width="100%">


                                    <tr id="tr2" runat="server" visible="true">
                                        <td style="padding-left: 30px" width="15%">
                                            <%--Month &amp; Year :--%>
                                            <%--From Date :--%>
                                        </td>




                                    </tr>
                                    <tr>
                                        <td style="padding-left: 30px" width="15%">
                                            <%--Month &amp; Year :--%>
                                            <%--From Date :--%>
                                        </td>
                                        <td class="form_left_text"><%--From Date <span style="color: Red">*</span> :
                                            </td>
                                            <td class="form_left_text">


                                                <asp:TextBox ID="txtFromdt" runat="server" AutoPostBack="true" onblur="return checkdate(this);"
                                                    Width="100px"></asp:TextBox>
                                                &nbsp;<asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtFromdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="mefrmdt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                                <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromdt"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>--%>
                                            <%-- To Date <span style="color: Red">*</span> :
                                                &nbsp;
                                                <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10"
                                                    Width="100px" />
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtTodt" />--%>

                                        </td>


                                    </tr>


                                    <%--<tr id="trchkdept" runat="server">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text">
                                                <asp:CheckBox ID="chkDept" Text="Department wise " runat="server"
                                                    OnCheckedChanged="chkDept_CheckedChanged" AutoPostBack="True" />
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>

                                    <%--<tr id="trddldept" runat="server" visible="false">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label">Select Department :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true"
                                                    Width="80%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>
                                    <%--<tr id="trchkstaff" runat="server">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text">
                                                <asp:CheckBox ID="chkstaff" Text="Staff wise " runat="server"
                                                    AutoPostBack="True" OnCheckedChanged="chkstaff_CheckedChanged" />
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>

                                    <%-- <tr id="trddlstaff" runat="server" visible="false">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label">Select Staff:
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlstafftype" runat="server" AppendDataBoundItems="true"
                                                    Width="80%" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlstafftype_SelectedIndexChanged">
                                                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>



                                    <%--<tr>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text">
                                                <asp:RadioButtonList ID="rblAllParticular" runat="server"
                                                    RepeatDirection="Horizontal" Width="100%"
                                                    OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="All Employees" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Particular Employee" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>

                                    <%-- <tr id="tremp" runat="server">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label">Select Employee :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" Width="80%">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                    Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>
                                    <%--<tr id="tr1" runat="server">
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label">Leave Status :
                                            </td>
                                            <td class="form_left_text">
                                                <asp:RadioButtonList ID="rdbleavestatus" runat="server"
                                                    RepeatDirection="Horizontal" Width="83%"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="Approved" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Pending" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_label"></td>
                                            <td class="form_left_text" style="width: 110px"></td>
                                        </tr>--%>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 599px">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <%--<td class="form_button" align="center" style="width: 599px">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp"
                                        Width="80px" OnClick="btnReport_Click" />
                                    &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                            Width="80px" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </td>--%>
                        </tr>
                    </table>
                    </fieldset>
                </div>
                </asp:Panel>
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
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
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
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

