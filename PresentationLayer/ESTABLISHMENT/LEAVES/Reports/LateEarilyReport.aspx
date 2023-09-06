<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LateEarilyReport.aspx.cs" MasterPageFile="~/SiteMasterPage.master"
    Inherits="ESTABLISHMENT_LEAVES_Reports_LateEarilyReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LATE COMING AND EARLY GOING REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="panel-body">
                                <asp:Label ID="Label1" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="Label2" runat="server" SkinID="lblmsg"></asp:Label>

                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>College Name :</label>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                    TabIndex="1" AutoPostBack="true" ToolTip="Select College Name" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                                    SetFocusOnError="True" InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: #FF0000">*</span>From Date :</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter From Date"
                                                        TabIndex="2" Style="z-index: 0;" />
                                                    <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                        ControlToValidate="txtFromDt" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Shift" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <sup>* </sup>
                                                <label>To Date :</label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgCalToDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                                        TabIndex="3" Style="z-index: 0;" OnTextChanged="txtToDt_TextChanged" AutoPostBack="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtToDt" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtToDt" InvalidValueMessage="To Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Shift" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                    <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                                        CultureInvariantValues="true" Display="None" SetFocusOnError="True" Type="Date"
                                                        ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual"
                                                        ValidationGroup="Shift" ControlToCompare="txtFromDt" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">

                                                <label>Department :</label>
                                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" ToolTip="Select Department"
                                                    AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="Shift"
                                                                            SetFocusOnError="True" InitialValue="0">
                                                                        </asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label>Staff Type :</label>
                                                <%--<span style="color: #FF0000">*</span>--%>
                                                <asp:DropDownList ID="ddlStaffType" runat="server" CssClass="form-control" TabIndex="5"
                                                    AppendDataBoundItems="true" ToolTip="Select Staff Type" data-select2-enable="true">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaffType"
                                                            Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Shift"
                                                            SetFocusOnError="True" InitialValue="0">
                                                        </asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trdesig" runat="server" visible="false">
                                                <div class="form-group col-md-8">
                                                    <label>Designation :</label>
                                                    <asp:DropDownList ID="ddlDesig" runat="server" CssClass="form-control" TabIndex="6"
                                                        AppendDataBoundItems="true" ToolTip="Select Designation" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <sup>* </sup>
                                                <label>Select : </label>
                                                <asp:RadioButtonList ID="rblcondn" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblcondn_SelectedIndexChanged" TabIndex="7"
                                                    ToolTip="Select Forgot Punch/Late Comers/Early Going/Non Registered">
                                                    <%--  <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Forgot Punch&nbsp;&nbsp;" Value="0"></asp:ListItem>--%>
                                                    <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Late Comers&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                                    <%--&nbsp;&nbsp;--%>
                                                    <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Early Going&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                                    <%--&nbsp;&nbsp;--%>
                                                    <%-- <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Non Registered" Value="3"></asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="rfvSelect" runat="server" ControlToValidate="rblcondn"
                                                        Display="None" ErrorMessage="Please Select Latecomers Or Early Going" ValidationGroup="Shift"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnreport" runat="server" Text="Report" TabIndex="12"
                                                    CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnreport_Click"
                                                    ValidationGroup="Shift" />&nbsp;
                                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="11"
                                                     OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />&nbsp;
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            </div>
                                            <%-- <div class="form-group col-md-12">
                                                <div class="text-center">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                        <ProgressTemplate>
                                                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                            Processing Please Wait.........................................
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </div>--%>
                                            <div class="form-group col-md-12">
                                                <div class="text-center">
                                                    <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlList" runat="server">
                                    <div class="form-group col-md-12">
                                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" OnClick="btnShowReport_Click"
                                            Visible="False" UseSubmitBehavior="False" ToolTip="Click here to Show Report" TabIndex="14" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlEmpList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvEmpList" runat="server"
                                                OnItemDataBound="lvEmpList_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <%--<asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" />--%>
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                                            Forgot to Punch
                                                        </div>
                                                        <h4 class="box-title">Select Employees
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                                            runat="server" onclick="checkAllEmployees(this)" ToolTip="Select All Employee" TabIndex="15" />
                                                                    </th>
                                                                    <th>Date
                                                                    </th>
                                                                    <th>Intime
                                                                    </th>
                                                                    <th>OutTime
                                                                    </th>
                                                                    <%--<th>Status
                                                                            </th>--%>
                                                                    <th>Allow/NotAllow
                                                                    </th>
                                                                    <th>Reason
                                                                    </th>
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
                                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("EMPNAME")%>'
                                                                ToolTip='<%#Eval("Idno")%>' TabIndex="16" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("DATE", "{0:dd/MM/yyyy}")%>'
                                                                TabIndex="17"></asp:Label>
                                                            <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("INTIME")%>'
                                                                TabIndex="18"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>'
                                                                TabIndex="19"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <%-- <td>
                                                                    <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                                        TabIndex="20" ToolTip="Select Work Type">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                                                </td>--%>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                                TabIndex="21" ToolTip="Select Allow or Not Allow">
                                                                <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Allow" Value="A"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Not Allow" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="true"
                                                                TabIndex="21" ToolTip="Select Reason">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidReason" Value='<%# Eval("REASON_NO")%>' runat="server" />

                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlLateComersList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvLateComers" runat="server" OnItemDataBound="lvLateComers_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No late Comers" />
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                                            Latecomers List
                                                        </div>
                                                        <h4 class="box-title">Select Employees
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="22" ToolTip="Select All Employee" />
                                                                    </th>
                                                                    <th>Date
                                                                    </th>
                                                                    <th>Shift Intime
                                                                    </th>
                                                                    <th>Intime
                                                                    </th>
                                                                    <th>OutTime
                                                                    </th>
                                                                    <th>Hours
                                                                    </th>
                                                                    <th>Late By
                                                                    </th>
                                                                    <%--<th>Status
                                                                            </th>--%>
                                                                    <th>Allow/NotAllow
                                                                    </th>
                                                                    <th>Reason
                                                                    </th>
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
                                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("USERNAME")%>'
                                                                ToolTip='<%#Eval("IDNO")%>' TabIndex="23" />
                                                        </td>
                                                        <td>
                                                            <%--<asp:Label ID="lbldate" runat="server" Text='<%# Eval("DATE", "{0:dd/MM/yyyy}")%>'
                                                                        TabIndex="24" CssClass="form-control" ToolTip="Date"></asp:Label>--%>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("ENTDATE", "{0:dd/MM/yyyy}")%>' Width="80px"></asp:Label>
                                                            <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShiftIntime" runat="server" Text='<%# Eval("SHIFT_INTIME")%>'
                                                                CssClass="form-control" TabIndex="25" ToolTip="Shift In Time"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("INTIME")%>'
                                                                CssClass="form-control" TabIndex="26" ToolTip="In Time"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>'
                                                                CssClass="form-control" TabIndex="27" ToolTip="Out Time"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblhrs" runat="server" Text='<%# Eval("HOURS")%>'
                                                                CssClass="form-control" TabIndex="28" ToolTip="Hours"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLateby" runat="server" Text='<%# Eval("LATEBY")%>'
                                                                CssClass="form-control" TabIndex="29" ToolTip="Late By"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <%-- <td>
                                                                    <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                                        CssClass="form-control" TabIndex="30" ToolTip="Select Work Type">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                                                </td>--%>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" TabIndex="31" ToolTip="Select Allow/Not Allow">
                                                                <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Allow" Value="A"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Not Allow" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="true" ToolTip="Select Reason"
                                                                CssClass="form-control" TabIndex="32" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidReason" Value='<%# Eval("REASON_NO")%>' runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlEmpEarlyList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvEmpEarly" runat="server"
                                                OnItemDataBound="lvEmpEarly_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" />
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                                            Early Going Employees
                                                        </div>
                                                        <h4 class="box-title">Select Employees
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="33" ToolTip="Select All Employee" />
                                                                    </th>
                                                                    <th>Date
                                                                    </th>
                                                                    <th>InTime
                                                                    </th>
                                                                    <th>OutTime
                                                                    </th>
                                                                    <th>Shift Outtime
                                                                    </th>
                                                                    <th>Leave Type
                                                                    </th>
                                                                    <%--<th>Status
                                                                            </th>--%>
                                                                    <th>Allow/Not Allow
                                                                    </th>
                                                                    <th>Reason
                                                                    </th>
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
                                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("USERNAME")%>'
                                                                ToolTip='<%#Eval("Idno")%>' TabIndex="34" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("ENTDATE", "{0:dd/MM/yyyy}")%>'
                                                                CssClass="form-control" ToolTip="Date" TabIndex="35"></asp:Label>
                                                            <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("INTIME")%>'
                                                                CssClass="form-control" ToolTip="In Time" TabIndex="36"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>'
                                                                CssClass="form-control" ToolTip="Out Time" TabIndex="37"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShiftOuttime" runat="server" Text='<%# Eval("SHIFTOUTTIME")%>'
                                                                CssClass="form-control" ToolTip="Shift Out Time" TabIndex="38"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("LEAVETYPE")%>'
                                                                CssClass="form-control" ToolTip="Leave Type" TabIndex="39"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>

                                                        <%--<td>
                                                                    <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                                        CssClass="form-control" ToolTip="Work Type" TabIndex="40">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                                                </td>--%>

                                                        <td>
                                                            <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" ToolTip="Select Allow/Not Allow" TabIndex="41">
                                                                <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Allow" Value="A"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Not Allow" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" ToolTip="Select Reason" TabIndex="42">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidReason" Value='<%# Eval("REASON_NO")%>' runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="pnlNREmppList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvNREmpList" runat="server"
                                                OnItemDataBound="lvNREmpList_ItemDataBound">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" />
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <h4 id="lgv1">
                                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                                            Non Registered Record
                                                        </div>
                                                        <h4 class="box-title">Select Employees
                                                        </h4>
                                                        </div>
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                                                runat="server" onclick="checkAllEmployees(this)" TabIndex="43" ToolTip="Select All Employee" />
                                                                        </th>
                                                                        <th>Date
                                                                        </th>
                                                                        <th>Intime
                                                                        </th>
                                                                        <th>OutTime
                                                                        </th>
                                                                        <%-- <th>Status
                                                                        </th>--%>
                                                                        <th>Allow/NotAllow
                                                                        </th>
                                                                        <th>Reason
                                                                        </th>
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
                                                            <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("EMPNAME")%>'
                                                                ToolTip='<%#Eval("Idno")%>' TabIndex="44" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("DATE", "{0:dd/MM/yyyy}")%>'
                                                                TabIndex="45" ToolTip="Date"></asp:Label>
                                                            <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("INTIME")%>'
                                                                TabIndex="46" ToolTip="In Time"></asp:Label>
                                                            <%--<%# Eval("INTIME")%>--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>'
                                                                TabIndex="47" ToolTip="Out Time"></asp:Label>
                                                            <%-- <%# Eval("OUTTIME")%>--%>
                                                        </td>
                                                        <%-- <td>
                                                                    <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                                        CssClass="form-control" ToolTip="Select Work Type" TabIndex="48">
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                                                </td>--%>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" ToolTip="Select Allow or Not Allow" TabIndex="49">
                                                                <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Text="Allow" Value="A"></asp:ListItem>
                                                                <asp:ListItem Enabled="true" Selected="True" Text="Not Allow" Value="N"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidStatus" Value='<%# Eval("STATUS")%>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control" ToolTip="Select Reason" TabIndex="50">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hidReason" Value='<%# Eval("REASON_NO")%>' runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnreport" />
        </Triggers>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <%--<td class="vista_page_title_bar" valign="top" style="height: 30px">LATE COMING AND THUMB PROBLEM APPROVAL &nbsp;                       
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record
                                    <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
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
    </table>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <%--<td colspan="2" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                            Processing Please Wait.........................................
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>--%>
            </tr>
        </table>
    </center>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
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
    <%-- </ContentTemplate>--%>

    <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>


