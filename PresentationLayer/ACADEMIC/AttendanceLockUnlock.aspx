<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendanceLockUnlock.aspx.cs" Inherits="AttendanceLockUnlock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_pnlattendancelockunlock .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress4" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updattendancelock" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Attendance Lock Unlock</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgScheme" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            TabIndex="1" OnSelectedIndexChanged="ddlClgScheme_SelectedIndexChanged2" data-select2-enable="true">

                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select College Scheme" InitialValue="0" ControlToValidate="ddlClgScheme"
                                            Display="None" ValidationGroup="show" />
                                        <%--  <asp:RequiredFieldValidator ID="RfvddlClgScheme" runat="server" ControlToValidate="ddlClgScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="show" >
                                        </asp:RequiredFieldValidator>--%>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Faculty </label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="3" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Course </label>
                                        </div>
                                        <%--   <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>

                                        <asp:DropDownList ID="ddlCourse" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="4" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Section </label>
                                        </div>
                                        <%--  <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                                        <asp:DropDownList ID="ddlSection" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"
                                            TabIndex="5" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem"
                                            TabIndex="6" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:DropDownList ID="ddlSem" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Attendance Start - End Date</label>
                                        </div>
                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                        <div id="picker" class="form-control" tabindex="7">

                                            <i class="fa fa-calendar"></i>&nbsp;
                                                        <span id="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                    </div>
                                    <%-- <asp:UpdatePanel ID="upd" runat="server">
                                        <ContentTemplate>--%>
                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Start End Date</label>
                                                </div>
                                                <div id="picker" class="form-control" tabindex="7">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>

                                                </div>
                                            </div>--%>
                                    <%--   </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnShow" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark </label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" SelectionMode="Multiple" Rows="1" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                        <%--<asp:HiddenField ID="hdnDate" runat="server" />--%>
                                        <%--<asp:ListBox ID="lstRemark" runat="server" SelectionMode="Multiple" Rows="1" CssClass="form-control"></asp:ListBox>--%>
                                    </div>
                                    <asp:Panel ID="PnlDate" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start Date : </label>
                                                    </div>
                                                    <div class='input-group date' id="myDatepicker">
                                                        <div class="input-group-addon" id="Div2" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" autocomplete="off" ToolTip="Please Enter Start Date" CssClass="form-control datepickerinput" TabIndex="9" data-inputmask="'mask': '99/99/9999'" />

                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please select Start Date" SetFocusOnError="true"
                                            ValidationGroup="submit" InitialValue="0" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Div2" TargetControlID="txtStartDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2"
                                                            runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999"
                                                            MaskType="Date"
                                                            MessageValidatorTip="true"
                                                            OnInvalidCssClass="errordate"
                                                            TargetControlID="txtStartDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                            ControlExtender="MaskedEditExtender2"
                                                            ControlToValidate="txtStartDate"
                                                            IsValidEmpty="False"
                                                            EmptyValueMessage="Please Enter Start Date"
                                                            InvalidValueMessage="Start Date is invalid(Enter dd/mm/yyyy format)"
                                                            Display="None"
                                                            TooltipMessage="Input a date"
                                                            EmptyValueBlurredText="*"
                                                            InvalidValueBlurredMessage="*"
                                                            ValidationGroup="Lock" />

                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="stfilter" TargetControlID="txtStartDate" runat="server" FilterType="Custom,Numbers" ValidChars="0123456789 /"  ></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>End Date : </label>
                                                    </div>
                                                    <div class="input-group date" id="myDatepicker3">
                                                        <div class="input-group-addon" id="Div3" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="10" ToolTip="Please Enter End Date" CssClass="form-control datepickerinput" data-inputmask="'mask': '99/99/9999'" />

                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Div3" TargetControlID="txtEndDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1"
                                                            runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true"
                                                            Mask="99/99/9999"
                                                            MaskType="Date"
                                                            MessageValidatorTip="true"
                                                            OnInvalidCssClass="errordate"
                                                            TargetControlID="txtEndDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                            ControlExtender="MaskedEditExtender2"
                                                            ControlToValidate="txtEndDate"
                                                            IsValidEmpty="False"
                                                            EmptyValueMessage="Please Enter End Date"
                                                            InvalidValueMessage="End Date is invalid(Enter dd/mm/yyyy format)"
                                                            Display="None"
                                                            TooltipMessage="Input a date"
                                                            EmptyValueBlurredText="*"
                                                            InvalidValueBlurredMessage="*"
                                                            ValidationGroup="Lock" />
                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtEndDate" runat="server" FilterType="Custom,Numbers" ValidChars="0123456789 /"  ></ajaxToolKit:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="PnlDateTime" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <sup>* </sup>
                                                    <label><span style="color: red"></span>Start Time</label>

                                                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="11" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtStartTime"
                                                        Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                        MaskType="Time" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Start Time"
                                                        ControlToValidate="txtStartTime" Display="NONE" SetFocusOnError="true" ValidationGroup="show"
                                                        ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                    <%--<asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Start Time."></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <sup>* </sup>
                                                    <label><span style="color: red"></span>End Time</label>

                                                    <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" ToolTip="Please Enter Time" TabIndex="12" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meEndtime" runat="server" TargetControlID="txtEndTime"
                                                        Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                        MaskType="Time" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid End Time"
                                                        ControlToValidate="txtEndTime" Display="NONE" SetFocusOnError="true" ValidationGroup="show"
                                                        ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndTime"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter End Time."></asp:RequiredFieldValidator>--%>
                                                </div>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attendance Marked </label>
                                        </div>
                                        <asp:DropDownList ID="ddlattstatus"
                                            TabIndex="6" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlattstatus_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" ValidationGroup="show" TabIndex="13" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Lock" CssClass="btn btn-danger" OnClick="btnSubmit_Click" TabIndex="14" ValidationGroup="Lock" />
                                <asp:Button ID="btnUnlock" runat="server" Text="Unlock" CssClass="btn btn-success" OnClick="btnUnlock_Click" TabIndex="15" ValidationGroup="Lock" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="16" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="show" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Lock" />
                            </div>

                            <%--                    <div class="col-12">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Lock/Unlock</th>
                                    <th>Faculty Name</th>
                                    <th>Course </th>
                                    <th>Section</th>
                                    <th>Semester</th>
                                    <th>Date</th>
                                    <th>Time Slot</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" /></td>
                                    <td>Rahul patil</td>
                                    <td>RFC01234 - BE</td>
                                    <td>Section</td>
                                    <td>SEMIII</td>
                                    <td>21-11-2022</td>
                                    <td>10am - 1pm</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CheckBox2" runat="server" /></td>
                                    <td>Pankaj Thakare</td>
                                    <td>RFC01234 - BE</td>
                                    <td>Section</td>
                                    <td>SEMIII</td>
                                    <td>21-11-2022</td>
                                    <td>10am - 1pm</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                            <%--<asp:UpdatePanel ID="upd" runat="server">
                                        <ContentTemplate>--%>
                            <div class="col-12">
                                <asp:Panel ID="pnlattendancelockunlock" runat="server">
                                    <asp:ListView ID="lvattendancelockunlock" runat="server">
                                        <LayoutTemplate>
                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divsessionlist">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="selectAll(this);" />
                                                            </th>
                                                            <th>Faculty Name</th>
                                                            <th>Course </th>
                                                            <th>Section</th>
                                                            <th>Semester</th>
                                                            <th>Time Table Date</th>
                                                            <th>Time Slot</th>
                                                            <th>Attendance Marked</th>
                                                            <th>Start Date
                                                            </th>
                                                            <th>End Date
                                                            </th>
                                                            <th>Start Time
                                                            </th>
                                                            <th>End Time
                                                            </th>
                                                            <th>Lock Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="Chkfaculty" runat="server" ToolTip='<%# Eval("TTNO")%>' />
                                                    <asp:HiddenField ID="hdttno" runat="server" Value='<%# Eval("TTNO")%>' />
                                                    <%-- <asp:HiddenField ID="hdnStatus" runat="server" Value='<%# Eval("STATUS")%>' />--%>
                                                </td>
                                                <td><%# Eval("Faculty Name")  %>
                                                    <asp:HiddenField ID="hdnUaNo" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                </td>
                                                <td><%# Eval("Course") %>
                                                    <asp:HiddenField ID="hdnCourseNo" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td><%# Eval("Section") %>
                                                    <asp:HiddenField ID="hdnsectionno" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                </td>
                                                <td><%# Eval("Semester") %>
                                                    <asp:HiddenField ID="hdnsemesterno" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td><%# Eval("Date","{0:dd-MMM-yyyy}") %>
                                                    <asp:HiddenField ID="hddate" runat="server" Value='<%# Eval("Date")%>' />

                                                </td>
                                                <td><%# Eval("TimeSlot") %>
                                                    <asp:HiddenField ID="hdnslotno" runat="server" Value='<%# Eval("SLOTNO")%>' />
                                                </td>

                                                <td><%# Eval("Attendance_Marked") %>
                                                    
                                                </td>
                                                <td><%# Eval("START_DATE","{0:dd-MMM-yyyy}") %>
                                                    <asp:HiddenField ID="hdstdate" runat="server" Value='<%# Eval("START_DATE")%>' />

                                                </td>
                                                <td><%# Eval("END_DATE","{0:dd-MMM-yyyy}") %>
                                                    <asp:HiddenField ID="hdendt" runat="server" Value='<%# Eval("END_DATE")%>' />

                                                </td>
                                                <td><%# Eval("START_TIME") %>
                                                    <asp:HiddenField ID="hdsttime" runat="server" Value='<%# Eval("START_TIME")%>' />

                                                </td>
                                                <td><%# Eval("END_TIME") %>
                                                    <asp:HiddenField ID="hdentime" runat="server" Value='<%# Eval("END_TIME")%>' />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLockStatus" runat="server" Text='<%# Eval("LOCKSTATUS")%>'
                                                        ForeColor='<%# Eval("LOCKSTATUS").ToString()=="Lock"?System.Drawing.Color.Red : System.Drawing.Color.Green %>'
                                                        Font-Bold="true"></asp:Label>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <%--                                             </ContentTemplate>
                                      <%--  <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnShow" />
                                        </Triggers>--%>
                            <%--</asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- <asp:PostBackTrigger ControlID="btnShow" />--%>
            <%--<asp:PostBackTrigger ControlID="lvattendancelockunlock" />--%>
            <%--<asp:PostBackTrigger ControlID="btnSubmit" />--%>
        </Triggers>

    </asp:UpdatePanel>

    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },

                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },

        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },




                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                    var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                });
            });
};
    </script>

    <script>
        function selectAll(chk) {
            var totalCheckboxes = $('[id*=divsessionlist] td input:checkbox').length;
            for (var i = 0; i < totalCheckboxes; i++) {
                if (chk.checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvattendancelockunlock_ctrl" + i + "_Chkfaculty").checked = true;
                }
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_lvattendancelockunlock_ctrl" + i + "_Chkfaculty").checked = false;
            }
        }
    </script>

    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            debugger
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>--%>
</asp:Content>

