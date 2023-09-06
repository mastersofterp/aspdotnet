<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IrregularityApproval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_IrregularityApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">IRREGULARITY APPROVAL</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <%--<div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Irregular Approval List</h5>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <asp:Panel ID="pnlPending" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvPendingList" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of Leaves for Approval" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Pending List for Approval</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Irregularity Status
                                                    </th>
                                                    <th>Approve/Reject
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
                                                <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" TabIndex="1" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <%# Eval("sno")%>
                                            </td>
                                            <td>
                                                <%# Eval("EmpName")%>
                                            </td>
                                            <td>
                                                <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("IRREGULARITYSTATUS")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("IRRTRNO")%>' TabIndex="2"
                                                    ToolTip="Select to Approve/Reject" CssClass="btn btn-primary" OnClick="btnApproval_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <tr class="bg-light-blue">
                                                            <th>Reason
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("REASON") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                            TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                            ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/Images/action_down.png"
                                            ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                        </ajaxToolKit:CollapsiblePanelExtender>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>

                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="lnkbut" runat="server" Text="Approval Status" CssClass="btn btn-primary"
                            ToolTip="Click here for Leave Approval Status" TabIndex="4" OnClick="lnkbut_Click"></asp:LinkButton>
                    </div>
                    <asp:Panel ID="pnlODStatus" runat="server">
                        <%--<div class="panel panel-info">
                        <div class="panel panel-heading">Approval Status</div>
                        <div class="panel panel-body">--%>
                        <div id="trfrmto" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Form Date"
                                            Style="z-index: 0;" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                            Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="odapr">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="MaskedEditExtender5"
                                            ControlToValidate="txtFromdt"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="odapr" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                            Style="z-index: 0;" TabIndex="6" OnTextChanged="txtTodt_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                            Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtTodt" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeTodt"
                                            ControlToValidate="txtTodt"
                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="odapr" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer" id="trbutshow" runat="server">
                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary"
                                ValidationGroup="odapr" ToolTip="Click here To Show Status" TabIndex="7" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="odapr"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                        <div class="form-group col-md-12">
                            <asp:Panel ID="pnlStatusList" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lvApprStatus" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication" CssClass="d-block text-center mt-3"></asp:Label>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Leave Approval Status</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Irregularity Status
                                                        </th>
                                                        <th>Date of Approval
                                                        </th>
                                                        <th>Status
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
                                                    <%# Eval("EmpName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IRREGULARITYSTATUS")%>
                                                </td>
                                                <td>
                                                    <%#Eval("APR_DATE","{0:dd/MM/yyyy}") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Status") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnHidePanel" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                OnClick="btnHidePanel_Click" TabIndex="8" />
                        </div>
                        <%-- </div>
                        </div>--%>
                    </asp:Panel>

                    <div class="col-12" id="pnlAdd" runat="server">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">

                                    <li class="list-group-item"><b>Employee Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblEmpName" runat="server" TabIndex="18" ToolTip="Employee Name"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Department :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDept" runat="server" TabIndex="19" ToolTip="Department Name"></asp:Label>
                                        </a>
                                    </li>

                                    <li class="list-group-item"><b>Designation :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDesignation" runat="server" TabIndex="21" ToolTip="Designation"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDate" runat="server" TabIndex="22" ToolTip="Date"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>In Time :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblInTime" runat="server" TabIndex="24" ToolTip="In Time"></asp:Label></a>
                                    </li>

                                </ul>
                            </div>

                            <div class="col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">

                                    <li class="list-group-item"><b>Out Time :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblOutTime" runat="server" TabIndex="25" ToolTip="Out Time"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Shift In Time :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblShiftIn" runat="server" TabIndex="24" ToolTip="In Time"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Shift Out Time :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblShiftOut" runat="server" TabIndex="25" ToolTip="Out Time"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Working Hours :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblWorking" runat="server" TabIndex="26" ToolTip="Working Hours"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Status :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblIrregularstatus" runat="server" TabIndex="26" ToolTip="Status"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Reason :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblReason" runat="server" TabIndex="27" ToolTip="Reason"></asp:Label></a>
                                    </li>

                                </ul>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" ToolTip="Select Approve/Reject" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="29">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="F">Forward To Next Authority(Recommended)</asp:ListItem>
                                        <asp:ListItem Value="A">Approve & Final Submit</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvselect" runat="server" ControlToValidate="ddlSelect"
                                        Display="None" ErrorMessage="Please Select Status" InitialValue="0" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remarks</label>
                                    </div>
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="30"
                                        ToolTip="Enter Remarks" />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3" id="divAuthorityList" runat="server" visible="false">
                            <asp:Panel ID="pnlSelectList" runat="server">
                                <asp:ListView ID="lvStatus" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="ibler" runat="server" Text="" CssClass="d-block text-center mt-3"></asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Approval Status</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Authority Name
                                                    </th>
                                                    <th>User Name
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                    <th>Remarks
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
                                                <%# Eval("sno")%>
                                            </td>
                                            <td>
                                                <%# Eval("PANAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAusername")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATUS")%>
                                            </td>
                                            <td>
                                                <%# Eval("APR_REMARKS")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>


                </div>

                <asp:Panel ID="pnlButton" runat="server" Visible="false">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="31"
                            CssClass="btn btn-primary" ToolTip="Click here To Submit" OnClick="btnSave_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                            ToolTip="Click here to Go Back" TabIndex="32" OnClick="btnBack_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="33"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

