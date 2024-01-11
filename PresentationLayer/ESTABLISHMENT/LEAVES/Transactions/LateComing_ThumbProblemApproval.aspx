<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LateComing_ThumbProblemApproval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_LateComing__ThumbProblemApproval"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <style>
        #ctl00_ContentPlaceHolder1_pnlLateComersList .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlEmpEarlyList .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LATE COMING AND THUMB PROBLEM APPROVAL</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Approval</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Selection Type</label>
                                    </div>
                                    <asp:RadioButtonList ID="rblSlectionType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblSlectionType_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="&nbsp; All" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="&nbsp; Single Employee"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>

                        </div>


                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Employee Type</label>
                                    </div>
                                    <asp:RadioButtonList ID="rblEmpType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">
                                        <%--OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged"--%>
                                        <asp:ListItem Value="0" Text="&nbsp; General Employee &nbsp;&nbsp;" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="&nbsp; Shift Module Employee"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divcollege">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                        TabIndex="1" AutoPostBack="true" ToolTip="Select College Name"
                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Shift"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
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
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter From Date"
                                            TabIndex="2" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Shift"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Shift" SetFocusOnError="true">
                                        </ajaxToolKit:MaskedEditValidator>
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
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                            TabIndex="3" Style="z-index: 0;" AutoPostBack="true" OnTextChanged="txtToDt_TextChanged" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Holiday"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" PopupButtonID="imgCalToDt" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Holiday" SetFocusOnError="true">
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                            CultureInvariantValues="true" Display="None" SetFocusOnError="True" Type="Date"
                                            ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual"
                                            ValidationGroup="Shift" ControlToCompare="txtFromDt" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trdept" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" ToolTip="Select Department" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="4">
                                    </asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                           Display="None" ErrorMessage="Please Select Department" ValidationGroup="Shift"
                                                          SetFocusOnError="True" InitialValue="0">
                                                                        </asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divstaff">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffType" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaffType"
                                        Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Shift"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divEmployee" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12" id="trdesig" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Designation</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDesig" runat="server" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Designation">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="col-12 mb-3">
                            <div class="label-dynamic">
                                <label>Select</label>
                            </div>
                            <asp:RadioButtonList ID="rblcondn" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rblcondn_SelectedIndexChanged" TabIndex="7"
                                ToolTip="Select Forgot Punch/Late Comers/Early Going/Non Registered">
                                <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Forgot Punch&nbsp;&nbsp;" Value="0"></asp:ListItem>
                                <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Latecomers&nbsp;&nbsp;" Value="1"></asp:ListItem>
                                <%--&nbsp;&nbsp;--%>
                                <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Early Going&nbsp;&nbsp;" Value="2"></asp:ListItem>
                                <%--&nbsp;&nbsp;--%>
                                <asp:ListItem Enabled="true" Text="&nbsp;&nbsp;Non Registered" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-12 btn-footer">
                            <%--<div class="text-center">--%>
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click here to Show"
                                ValidationGroup="Shift" OnClick="btnShow_Click" TabIndex="8" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary"
                                ValidationGroup="Shift" ToolTip="Click here to Save" TabIndex="9" />
                            <asp:Button ID="btnModify" runat="server" Text="Modify" CssClass="btn btn-primary" OnClick="btnModify_Click"
                                ValidationGroup="Shift" ToolTip="Click here to Modify" TabIndex="10" />
                            <asp:Button ID="btnreport" runat="server" Text="Report" CausesValidation="false" TabIndex="11"
                                CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnreport_Click"
                                ValidationGroup="Shift" />
                            <asp:Button ID="btnStatusRpt" runat="server" Text="Status Report" CausesValidation="false" TabIndex="12"
                                CssClass="btn btn-info" ToolTip="Click here to Show Status Report" ValidationGroup="Shift"
                                OnClick="btnStatusRpt_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="13"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Shift"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <%-- </div>--%>
                        </div>
                        <%--<div class="col-12">
                                    <div class="text-center">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                Processing Please Wait.........................................
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>--%>
                        <div class="col-12 btn-footer">
                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" OnClick="btnShowReport_Click"
                                Visible="False" UseSubmitBehavior="False" ToolTip="Click here to Show Report" TabIndex="14" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlEmpList" runat="server">
                                <asp:ListView ID="lvEmpList" runat="server"
                                    OnItemDataBound="lvEmpList_ItemDataBound">
                                    <%--ViewStateMode="Disabled"--%>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                            <span style="white-space: nowrap"></span>Forgot to Punch
                                        </div>
                                        <div class="sub-heading">
                                            <h5>Select Employees</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                            runat="server" onclick="checkAllEmployees(this)" ToolTip="Select All Employee" TabIndex="15" />
                                                    </th>
                                                    <td>Designation
                                                    </td>
                                                    <th>Department
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Intime
                                                    </th>
                                                    <th>OutTime
                                                    </th>
                                                    <%--<th style="display: none">Status
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
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("EMPNAME")%>'
                                                    ToolTip='<%#Eval("Idno")%>' TabIndex="16" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("SUBDESIG")%>' TabIndex="17"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("SUBDEPT")%>' TabIndex="17"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("DATE", "{0:dd/MM/yyyy}")%>'
                                                    TabIndex="18"></asp:Label>
                                                <%--<%# Eval("DATE", "{0:dd/MM/yyyy}")%>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIntime" runat="server" Text='<%# Eval("INTIME")%>'
                                                    TabIndex="19"></asp:Label>
                                                <%--<%# Eval("INTIME")%>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME")%>'
                                                    TabIndex="20"></asp:Label>
                                                <%-- <%# Eval("OUTTIME")%>--%>
                                            </td>
                                            <%-- <td style="display: none">
                                                <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                    TabIndex="20" ToolTip="Select Work Type">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                            </td>--%>
                                            <td>
                                                <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                    TabIndex="21" ToolTip="Select Allow or Not Allow">
                                                    <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true"  Text="Allow" Value="A"></asp:ListItem>
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
                        <div class="col-12">
                            <asp:Panel ID="pnlLateComersList" runat="server">
                                <asp:ListView ID="lvLateComers" runat="server" OnItemDataBound="lvLateComers_ItemDataBound">
                                    <%--ViewStateMode="Disabled"--%>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No late Comers" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                            <span style="white-space: nowrap">Latecomers List</span>
                                        </div>
                                        <div class="sub-heading">
                                            <h5>Select Employees</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="22" ToolTip="Select All Employee" />
                                                    </th>
                                                    <td>Designation
                                                    </td>
                                                    <th>Department
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
                                                    <%-- <th style="display: none">Status
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
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("USERNAME")%>'
                                                    ToolTip='<%#Eval("IDNO")%>' TabIndex="23" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("SUBDESIG")%>' TabIndex="17"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DEPARTMENT")%>' CssClass="form-control" ToolTip="Department"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("ENTDATE", "{0:dd/MM/yyyy}")%>'
                                                    TabIndex="24" CssClass="form-control" ToolTip="Date"></asp:Label>
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
                                            <%-- <td style="display: none">
                                                <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" TabIndex="30" ToolTip="Select Work Type">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                            </td>--%>
                                            <td>
                                                <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" TabIndex="31" ToolTip="Select Allow/Not Allow">
                                                    <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true"  Text="Allow" Value="A"></asp:ListItem>
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
                        <div class="col-12">
                            <asp:Panel ID="pnlEmpEarlyList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvEmpEarly" runat="server"
                                    OnItemDataBound="lvEmpEarly_ItemDataBound">
                                    <%--ViewStateMode="Disabled"--%>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                            <span style="white-space: nowrap">Early Going Employees</span>
                                        </div>
                                        <div class="sub-heading">
                                            <h5>Select Employees</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="33" ToolTip="Select All Employee" />
                                                    </th>
                                                    <td>Designation
                                                    </td>
                                                    <th>Department
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>InTime
                                                    </th>
                                                    <th>OutTime
                                                    </th>
                                                    <th>Shift Outtime
                                                    </th>
                                                    <th style="display: none">Leave Type
                                                    </th>
                                                    <th style="display: none">Status
                                                    </th>
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
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("USERNAME")%>'
                                                    ToolTip='<%#Eval("Idno")%>' TabIndex="34" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("SUBDESIG")%>' TabIndex="17"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DEPT_NAME")%>' CssClass="form-control" ToolTip="Department"></asp:Label>
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
                                            <td style="display: none">
                                                <asp:Label ID="lblLeaveType" runat="server" Text='<%# Eval("LEAVETYPE")%>'
                                                    CssClass="form-control" ToolTip="Leave Type" TabIndex="39"></asp:Label>
                                                <%--<%# Eval("INTIME")%>--%>
                                            </td>
                                            <td style="display: none">
                                                <asp:DropDownList ID="ddlWorkType" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" ToolTip="Work Type" TabIndex="40">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hidWorkType" Value='<%# Eval("WTNO")%>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAllow" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" ToolTip="Select Allow/Not Allow" TabIndex="41">
                                                    <asp:ListItem Enabled="true" Text="Please Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true"  Text="Allow" Value="A"></asp:ListItem>
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
                        <div class="col-12">
                            <asp:Panel ID="pnlNREmppList" runat="server">
                                <asp:ListView ID="lvNREmpList" runat="server"
                                    OnItemDataBound="lvNREmpList_ItemDataBound">
                                    <%--ViewStateMode="Disabled"--%>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Employee Not Found" CssClass="d-block text-center mt-3" />
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div style="font-family: 'Times New Roman', Times, serif; font-size: medium; color: #FF0000; font-weight: bold;">
                                            <span style="white-space: nowrap">Non Registered Record</span>
                                        </div>
                                        <div class="sub-heading">
                                            <h5>Select Employees</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblNR">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="chkAllEmployees" Checked="false" Text="Employee Name" Enabled="true"
                                                            runat="server" onclick="checkAllEmployees(this)" TabIndex="43" ToolTip="Select All Employee" />
                                                    </th>
                                                    <td>Designation
                                                    </td>
                                                    <th>Department
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Intime
                                                    </th>
                                                    <th>OutTime
                                                    </th>
                                                   
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
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkID" runat="server" Checked="false" Tag='lvItem' Text='<%#Eval("EMPNAME")%>'
                                                    ToolTip='<%#Eval("Idno")%>' TabIndex="44" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldesign" runat="server" Text='<%# Eval("SUBDESIG")%>' TabIndex="17"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("SUBDEPT")%>' ToolTip="Department"></asp:Label>
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
        //function enabledisablePhotoCheckBox(chk) {
        //    if (chk.checked == true)
        //        document.getElementById("ctl00_ctp_chkPhoto").disabled = true;
        //    else
        //        document.getElementById("ctl00_ctp_chkPhoto").disabled = false;
        //}
    </script>

    <div id="divMsg" runat="server">
    </div>
    <%-- </ContentTemplate>--%>

    <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>--%>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
