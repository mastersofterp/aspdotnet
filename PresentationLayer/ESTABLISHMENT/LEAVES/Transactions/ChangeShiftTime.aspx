<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ChangeShiftTime.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_ChangeShiftTime" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">CHANGE SHIFT TIME</h3>
                </div>
                <div class="box-body">
                  
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria to Change Shift Time</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--  <div class="form-group col-md-9">--%>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Report"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Staff Type" AutoPostBack="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Shift"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStafftype"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Report"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                    <div class="label-dynamic">
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" ToolTip="Select Department">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control"
                                            TabIndex="4" ToolTip="Enter From Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Report"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged"
                                            TabIndex="5" ToolTip="Enter To Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Shift"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Report"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                            EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date"
                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>In Time</label>
                                    </div>
                                   <asp:TextBox ID="txtInTime" CssClass="form-control" runat="server" ToolTip="Press A or P to switch between AM and PM "
                                        TabIndex="6"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtInTime"
                                        Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInTime"
                                        Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Shift"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                        ControlToValidate="txtInTime" IsValidEmpty="False" EmptyValueMessage=" "
                                        InvalidValueMessage="In Time is invalid" Display="None" TooltipMessage="Input a time"
                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" InitialValue="12:00"
                                        ValidationGroup="Shift" ErrorMessage="mevEnterTimeTo" />

                                    <%--InvalidValueMessage="In Time is invalid"--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Out Time</label>
                                    </div>
                                   <asp:TextBox ID="txtOutTime" runat="server" ToolTip="Press A or P to switch between AM and PM "
                                        CssClass="form-control" TabIndex="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOutTime"
                                        Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Shift"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtOutTime"
                                        Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                        ControlToValidate="txtOutTime" IsValidEmpty="False" EmptyValueMessage=" "
                                        InvalidValueMessage="Out Time is invalid" Display="None" TooltipMessage="Input a time"
                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" InitialValue="12:00"
                                        ValidationGroup="Shift" ErrorMessage="mevEnterTimeTo" />
                                </div>
                                  <div class="form-group col-lg-8 col-md-12 col-12">
                        <div class=" note-div">
                            <h5 class="heading">Note</h5>
                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Marked Is Mandatory & Press A or P to switch between AM and PM..!</span></p>
                        </div>
                    </div>
                            </div>
                        </div>
                        <%--<div class="form-group col-md-4" id="trdept" runat="server" style="width: 250px">
                                <label>Department :</label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control"
                                    AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3"
                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" ToolTip="Select Department">
                                </asp:DropDownList>
                            </div>--%>

                        <div class="col-12 btn-footer">
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Shift" OnClick="btnSave_Click"
                                CssClass="btn btn-primary" TabIndex="8" ToolTip="Click here to Submit" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="9" ValidationGroup="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="10" ToolTip="Click here to Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvEmpList" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Select Employees</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No
                                                    </th>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Select All" Enabled="true" runat="server"
                                                            onclick="checkAllEmployees(this)" />
                                                    </th>
                                                    <th>Employee Code
                                                    </th>
                                                    <th>Employee Name
                                                    </th>
                                                    <%-- <th>Shift Name
                                                    </th>--%>
                                                    <%--<th>Date
                                                                            </th>
                                                                            <th>INTIME
                                                                            </th>
                                                                            <th>OUTTIME
                                                                            </th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%#Container.DataItemIndex+1 %>
                                            </td>
                                            <td>
                                                <%-- <asp:CheckBox ID="chkSelect" runat="server" TabIndex="9" ToolTip='<%#Eval("Idno")%>' />--%>
                                                <asp:CheckBox ID="chkID" runat="server" TabIndex="9" ToolTip='<%#Eval("Idno")%>' />
                                            </td>
                                            <td>
                                                <%#Eval("PFILENO")%>
                                            </td>
                                            <td>
                                                <%#Eval("NAME")%>
                                                <%--<asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>' ToolTip='<%#Eval("Idno")%>' />--%>
                                            </td>
                                            <%--<td>
                                            <%#Eval("SHIFTNAME")%>
                                        </td>--%>
                                            <%--<td>
                                                                    <%#Eval("DT")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("INTIME")%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("OUTTIME")%>
                                                                </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlDays" runat="server">
                                <asp:ListView ID="lvDays" runat="server">
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Select Days</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th></th>
                                                    <th>
                                                        <asp:Label ID="lbldays" runat="server" Text="Days"></asp:Label>
                                                        <%--<asp:CheckBox ID="chkAllDays" Checked="false" Text="Days" Enabled="true" runat="server" onclick="checkAllDays(this)"/>--%>
                                                        <%--onclick="checkAllEmployees(this)" --%>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:CheckBox ID="chkDay" runat="server" Checked="false" Tag='lvDay' Text='<%#Eval("DAYNAME")%>' ToolTip='<%#Eval("SRNO")%>' />
                                                <%--<asp:CheckBox Id="chkDay" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("DAYNAME")%>' ToolTip='<%#Eval("SRNO")%>'  />--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem">
                                            <td>
                                                <asp:CheckBox ID="chkDay" runat="server" Checked="false" Tag='lvDays' Text='<%#Eval("DAYNAME")%>' ToolTip='<%#Eval("SRNO")%>' />
                                                <%--<asp:CheckBox Id="chkDay" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("DAYNAME")%>' ToolTip='<%#Eval("SRNO")%>' />--%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>

    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%-- <td class="vista_page_title_bar" valign="top" style="height: 30px">CHANGE SHIFT TIME &nbsp;              
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/edit.png" AlternateText="Edit Record" />
                            Edit Record
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Images/delete.png" AlternateText="Delete Record" />
                            Delete Record
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

            <%--  <td colspan="4" style="padding-left: 20px">Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span><br />
            </td>--%>
        </tr>
        <tr>
            <td>
                <br />
                <%--<asp:Panel ID="pnlAdd" runat="server" Width="613px">
                    <div style="text-align: left; width: 87%; padding-left: 10px;">
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Change Shift Time</legend>
                            <table>
                                <tr>
                                    <td style="width: 699px">
                                        <br />
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="form_left_label" style="width: 25%">College Name <span style="color: #FF0000">*</span>
                                                </td>
                                                <td><b>:</b></td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlCollege" runat="server" Width="350px" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" style="width: 25%">Staff Type <span style="color: #FF0000">*</span>
                                                </td>
                                                <td style="width: 1%"><b>:</b></td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlStafftype" runat="server" Width="350px" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Shift"
                                                        SetFocusOnError="true" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr id="trdept" runat="server">
                                                <td class="form_left_label" style="width: 25%">Department <span style="color: #FF0000">*</span>
                                                </td>
                                                <td><b>:</b></td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlDept" runat="server" Width="350px"
                                                        AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Select Department" ValidationGroup="Shift"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td class="form_left_label">From Date <span style="color: #FF0000">*</span>
                                                </td>
                                                <td><b>:</b></td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" Width="80px" />
                                                    <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter Holiday Date"
                                                        InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Shift" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>

                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        To Date <span style="color: #FF0000">*</span><b>:</b>
                                                    <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" Width="80px" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtToDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtToDt" EmptyValueMessage="Please Enter Holiday Date"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Holiday" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>

                                                </td>



                                            </tr>

                                            <tr>
                                                <td class="form_left_label">In Time<span style="color: #FF0000">*</span>
                                                </td>
                                                <td><b>:</b></td>
                                                <td class="form_left_text">

                                                    <asp:TextBox ID="txtInTime" Width="80px" runat="server" ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtInTime"
                                                        Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtInTime"
                                                        Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    Out Time <span style="color: #FF0000">*</span><b>:</b>

                                                    <asp:TextBox ID="txtOutTime" Width="80px" runat="server" ToolTip="Press A or P to switch between AM and PM "></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOutTime"
                                                        Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtOutTime"
                                                        Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />


                                                </td>



                                            </tr>

                                        </table>




                                        <table cellpadding="0" cellspacing="0" width="99%">
                                            <tr id="trinout" runat="server">
                                                <td class="form_left_label" style="width: 94px"></td>
                                                <td class="form_left_text" style="width: 102px"></td>
                                                <td class="form_left_label"></td>
                                                <td class="form_left_text"></td>
                                            </tr>


                                        </table>
                                       
                                    </td>
                                </tr>--%>
                <%--Already Committed<tr>
                                
                                    <td align="center" style="width: 499px">
                                    <p>
                                        <asp:Calendar id="Calendar1" runat="server"  OnDayRender="Calendar1_DayRender" 
                                            onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
                                        </p>
                                        <p>
                                        <asp:Button id="Button1" onclick="Button1_Click" runat="server" Text="Button"></asp:Button>
                                        </p>
                                        <p> 
                                        <asp:Label id="labelOutput" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>--%>
                <%--<tr>
                                    <td class="form_button" align="center" style="width: 499px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Shift" OnClick="btnSave_Click"
                                            Width="80px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            OnClick="btnCancel_Click" Width="80px" />&nbsp;
                                        
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 499px">&nbsp
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
                </asp:Panel>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left: 10px">
                <%--<asp:Panel ID="pnlList" runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 90%; text-align: center">
                        <tr>
                            <td style="text-align: left; padding-left: 10px; padding-top: 10px;"></td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:ListView ID="lvEmpList" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <center>
                                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Employees" /></center>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">
                                                Select Employees
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr class="header">
                                                        <th width="5%">
                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true" runat="server" onclick="checkAllEmployees(this)" />
                                                        </th>
                                                    </tr>
                                                    <thead>
                                            </table>
                                        </div>
                                        <div class="listview-container">
                                            <div id="demo-grid" class="vista-grid">
                                                <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td width="50%">
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>' ToolTip='<%#Eval("Idno")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                            <td width="50%">
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("NAME")%>' ToolTip='<%#Eval("Idno")%>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
            </td>
        </tr>
    </table>

    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />


    <div class="col-md-12">
        <div class="text-center">
            <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div class="text-center">
                    <div class="modal-content">
                        <div class="modal-body">
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-btn-primary" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

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

        ; debugger
        function checkAllEmployees(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkID')) {

                        if (chkcomplaint.checked == true) {

                            e.checked = true
                        }
                        else
                            e.checked = false;
                        //e.checked = true;
                    }
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

