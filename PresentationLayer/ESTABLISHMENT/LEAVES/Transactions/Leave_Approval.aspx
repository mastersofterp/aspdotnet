<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Leave_Approval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Leave_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE APPROVAL</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Leave Approval List</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlPending" runat="server">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Pending List of Leaves for Approval</h5>
                                </div>
                            </div>
                            <div class="col-12 table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                <asp:ListView ID="lvPendingList" runat="server">
                                    <LayoutTemplate>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Sr.No.
                                                    </th>
                                                    <th>Name
                                                    </th>
                                                    <th>Leave Name
                                                    </th>
                                                    <th>Apply Date
                                                    </th>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
                                                    </th>
                                                    <th>No of days
                                                    </th>
                                                    <th>Joining date
                                                    </th>
                                                    <th>Approve/Reject
                                                    </th>
                                                    <th style="display: none">Modify
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of Leaves for Approval" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
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
                                                <%# Eval("LName")%>
                                            </td>
                                            <td>
                                                <%# Eval("ApplyDate")%>
                                            </td>
                                            <td>
                                                <%# Eval("From_date")%>
                                            </td>
                                            <td>
                                                <%# Eval("TO_DATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("NO_OF_DAYS") %>
                                            </td>
                                            <td>
                                                <%# Eval("JOINDT") %>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("LETRNO")%>' TabIndex="2"
                                                    ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" />
                                            </td>
                                            <td style="display: none">
                                                <asp:Button ID="btnModify" runat="server" Text="Modify" CommandArgument='<%# Eval("LETRNO")%>' TabIndex="3"
                                                    ToolTip="Select to Modify and Approve Leave" OnClick="btnModify_Click" CssClass="btn btn-primary" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%">
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
                        <%--<div class="vista-grid_datapager d-none">
                            <div class="text-center">
                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPendingList" PageSize="10"
                                    OnPreRender="dpPager_PreRender">
                                    <Fields>
                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </div>--%>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="lnkbut" runat="server" OnClick="lnkbut_Click" Text="Leave Approval Status" CssClass="btn btn-primary"
                            ToolTip="Click here for Leave Approval Status" TabIndex="4"></asp:LinkButton>
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
                                                        <th>Apply Date
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th style="display: none">Reason
                                                        </th>
                                                        <th>Leave name
                                                        </th>
                                                        <th>Date of Approval
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th style="display: none">Report
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
                                                    <%# Eval("ApplyDate", "{0:dd-MM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FROM_DATE", "{0:dd-MM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TO_DATE", "{0:dd-MM-yyyy}")%>
                                                </td>
                                                <td style="display: none">
                                                    <%# Eval("REASON")%>
                                                </td>
                                                <td>
                                                    <%# Eval("LName")%>
                                                </td>
                                                <td>
                                                    <%# Eval("APR_DATE" , "{0:dd-MM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Status") %>
                                                </td>
                                                <td style="display: none">
                                                    <asp:Button ID="btnRPT" runat="server" Text="Report" CommandArgument='<%# Eval("LETRNO")%>'
                                                        ToolTip='<%# Eval("LETRNO")%>' OnClick="btnRPT_Click" CssClass="btn btn-info" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%-- <div class="vista-grid_datapager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvStatus" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>--%>
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
                    <div id="pnlvedit" runat="server">
                        <div id="trType" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Leave Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rblleavetype" runat="server" AutoPostBack="true"
                                            RepeatDirection="Horizontal" TabIndex="9" ToolTip="Select Full Day or Half Day"
                                            OnSelectedIndexChanged="rblleavetype_SelectedIndexChanged">
                                            <asp:ListItem Enabled="true" Selected="True" Text="Full Day" Value="0"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Half Day" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divHalfCriteria" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Half Day Criteria</label>
                                        </div>
                                        <asp:DropDownList ID="ddlLeaveFNAN" runat="server" AppendDataBoundItems="true" TabIndex="10"
                                            CssClass="form-control" ToolTip="Select Half Day Criteria" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlLeaveFNAN_SelectedIndexChanged">
                                            <asp:ListItem Value="0">AN</asp:ListItem>
                                            <asp:ListItem Value="1">FN</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divml" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Medical Leave</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbml" runat="server" AutoPostBack="true"
                                            RepeatDirection="Horizontal">
                                            <%--onselectedindexchanged="rdbml_SelectedIndexChanged"--%>
                                            <asp:ListItem Enabled="true" Selected="True" Text="Full Pay" Value="0"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Text="Half Pay" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Leave Name</label>
                                        </div>
                                        <asp:TextBox ID="txtLeavename" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="11"
                                            ToolTip="Leave Name">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvleavemodify" runat="server" ControlToValidate="txtLeavename"
                                            Display="None" ErrorMessage="Please Enter Leave" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:Label ID="lblmodifyDepartment" runat="server" ToolTip="Department"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Designation</label>
                                        </div>
                                        <asp:Label ID="lblmodifyDesignation" runat="server" ToolTip="Designation"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bal.Leave</label>
                                        </div>
                                        <asp:TextBox ID="txtLeavebal" runat="server" ReadOnly="true" CssClass="form-control" TabIndex="12"
                                            ToolTip="Balance Leave" />
                                        <asp:RequiredFieldValidator ID="rfvBalmodify" runat="server" ControlToValidate="txtLeavebal"
                                            Display="None" ErrorMessage="Please Enter Balance" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtfrmdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Leave From Date"
                                                Style="z-index: 0;" TabIndex="13" OnTextChanged="txtfrmdt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                ControlToValidate="txtfrmdt" Display="None"
                                                ErrorMessage="Please Enter From Date" SetFocusOnError="true"
                                                ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromdt"
                                                TargetControlID="txtfrmdt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                TargetControlID="txtfrmdt" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                                ControlExtender="meeFromdt" ControlToValidate="txtfrmdt" Display="None"
                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                SetFocusOnError="true" TooltipMessage="Please Enter From Date"
                                                ValidationGroup="Leaveapp">
                                            &nbsp;&nbsp;
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
                                                <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" MaxLength="10" Style="z-index: 0;" TabIndex="14"
                                                CssClass="form-control" ToolTip="Enter Leave To Date" OnTextChanged="txttodate_TextChanged" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To Date"
                                                SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt"
                                                TargetControlID="txttodate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                TargetControlID="txttodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server"
                                                ControlExtender="meeTodt" ControlToValidate="txttodate" Display="None"
                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                                InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                SetFocusOnError="true" TooltipMessage="Please Enter To Date"
                                                ValidationGroup="Leaveapp">
                                                &nbsp;&nbsp;
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>No of Days</label>
                                        </div>
                                        <asp:TextBox ID="txtNodays" runat="server" AutoPostBack="true" MaxLength="5" TabIndex="15"
                                            CssClass="form-control" ToolTip="Enter Number Of Days" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Joining Date</label>
                                        </div>
                                        <%--<div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgCalJoindt" runat="server" ImageUrl="~/images/calendar.png"
                                                        Style="cursor: pointer" />
                                                </div>--%>
                                        <asp:TextBox ID="txtJoindt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Joining Date"
                                            Style="z-index: 0;" TabIndex="16" ReadOnly="true" />
                                        <%--<ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalJoindt"
                                                    TargetControlID="txtJoindt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtJoindt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server"
                                                    ControlExtender="meeJoindt" ControlToValidate="txtJoindt" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Joining Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Enter Joining Date"
                                                    ValidationGroup="Leaveapp">
                                            &nbsp;&nbsp;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSelectModify" runat="server" CssClass="form-control" ToolTip="Select Approve/Reject" data-select2-enable="true"
                                            AppendDataBoundItems="true" TabIndex="16">
                                            <%--<asp:ListItem Value="A">Approve</asp:ListItem>--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="F">Forward To Next Authority(Recommended)</asp:ListItem>
                                            <asp:ListItem Value="A">Approve & Final Submit</asp:ListItem>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvselectmodify" runat="server" ControlToValidate="ddlSelectModify"
                                            Display="None" ErrorMessage="Please Enter Status" InitialValue="0" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remarks</label>
                                        </div>
                                        <asp:TextBox ID="txtRemarkModify" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="17"
                                            ToolTip="Enter Remarks" />
                                        <%--<asp:RequiredFieldValidator ID="rfvmodifyremark" runat="server" ControlToValidate="txtRemarkModify"
                                            Display="None" ErrorMessage="Please Enter Remarks" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12" id="pnlAdd" runat="server">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">

                                    <li class="list-group-item"><b>Employee Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblEmpName" runat="server" TabIndex="18" ToolTip="Employee Name"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Leave Name :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblLeaveName" runat="server" TabIndex="19" ToolTip="Leave Name"></asp:Label>
                                            <%--<asp:RequiredFieldValidator ID="rfvleave" runat="server" ControlToValidate="lblLeaveName"
                                                    Display="None" ErrorMessage="Please Enter Leave" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                </asp:RequiredFieldValidator>--%></a>
                                    </li>

                                    <li class="list-group-item"><b>Department :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDepartment" runat="server" TabIndex="20" ToolTip="Department"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Designation :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblDesignation" runat="server" TabIndex="21" ToolTip="Designation"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Apply Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblApplydt" runat="server" TabIndex="22" ToolTip="Apply Date"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>From Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblFromdt" runat="server" TabIndex="23" ToolTip="From Date"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item"><b>To Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblTodt" runat="server" TabIndex="24" ToolTip="To Date"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>

                            <div class="col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">

                                    <li class="list-group-item"><b>No.of Days :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblNodays" runat="server" TabIndex="25" ToolTip="Number Of Days"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Joining Date :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblJoindt" runat="server" TabIndex="26" ToolTip="Joining Date"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item" id="divNoon" runat="server"><b>FN/AN :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblNoon" runat="server" TabIndex="27" ToolTip="FN/AN"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Reason :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblReason" runat="server" TabIndex="28" ToolTip="Reason"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item"><b>Balance Leaves :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblbal" runat="server" TabIndex="29" ToolTip="Balance Leaves"
                                                Font-Bold="True">0</asp:Label>
                                        </a>
                                    </li>
                                    <li class="list-group-item"><b>Alternate Arrangement :</b>
                                        <a class="sub-label">
                                            <asp:Label ID="lblAlternateArrang" runat="server" TabIndex="30" ToolTip="Alternate Arrangement"
                                                Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>


                                    <%-- <div class="form-group col-md-12">
                                            <label>Total No.of Leaves :</label>
                                            <asp:Label ID="lbltot" runat="server" TabIndex="25" ToolTip="Total Number Of Leaves"></asp:Label>
                                        </div>--%>


                                    <%-- <div class="form-group col-md-12">
                                <div class="form-group col-md-4">
                                    <label>Balance Leaves :</label>
                                </div>
                                <div class="form-group col-md-8">
                                    
                                </div>
                            </div>--%>
                                </ul>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <div class="row">
                                <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>
                                <div class="col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" ToolTip="Select Approve/Reject" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="30">
                                        <%--<asp:ListItem Value="A">Approve</asp:ListItem>--%>
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="F">Approve & Forward To Next Authority(Recommended)</asp:ListItem>
                                        <asp:ListItem Value="A">Approve & Final Submit</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvselect" runat="server" ControlToValidate="ddlSelect"
                                        Display="None" ErrorMessage="Please Enter Status" InitialValue="0" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>
                                <div class="col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remarks</label>
                                    </div>
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="31"
                                        ToolTip="Enter Remarks" MaxLength="180" onkeyDown="checkTextAreaMaxLength(this,event,'180');" onkeyup="textCounter(this, this.form.remLen, 180);" />
                                    <%--<asp:RequiredFieldValidator ID="rfvremark" runat="server" ControlToValidate="txtRemarks"
                                        Display="None" ErrorMessage="Please Enter Remarks" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                    </asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="divDocument" runat="server">
                            <div class="sub-heading">
                                <h5>Download Document</h5>
                            </div>

                            <div class="form-group col-md-8">
                                <asp:ListView ID="lvDownload" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="ibler" runat="server" Text="" CssClass="d-block text-center mt-3"></asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="datatable" cellpadding="0" cellspacing="0">
                                            <thead class="bg-light-blue">
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"),Eval("LETRNO"),Eval("EMPNO"))%>'><%# Eval("FILENAME")%></asp:HyperLink>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
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

                        <asp:Panel ID="panelArrangement" runat="server" Visible="false">
                            <asp:ListView ID="lvCLSArrangment" runat="server">
                                <%--<EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            NO RECORD FOUND
                                                        </p>
                                                    </EmptyDataTemplate>--%>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Class Arrangements
                                            </h5>
                                        </div>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Date</th>
                                                    <th>Time</th>
                                                    <th>Year & Semester</th>
                                                    <th>Subject</th>
                                                    <th>Engaged By</th>
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
                                            <%# Eval("DATE","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("TIME")%>
                                        </td>
                                        <td>
                                            <%# Eval("YEAR_SEM")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBJECT")%>
                                        </td>
                                        <td>
                                            <%# Eval("FACULTY_NAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>


                </div>

                <asp:Panel ID="pnlButton" runat="server" Visible="false">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="31"
                            CssClass="btn btn-primary" ToolTip="Click here To Submit" OnClick="btnSave_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                            OnClick="btnBack_Click" ToolTip="Click here to Go Back" TabIndex="32" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="33"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                </asp:Panel>
            </div>

            <%--  </div>--%>
            <%--   </asp:Panel>--%>
        </div>
    </div>
    </div>
    <script>
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }
        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }
    </script>
</asp:Content>
