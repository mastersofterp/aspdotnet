<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OD_Approval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_OD_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">OD APPROVAL</h3>
                </div>
                <div>

                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Panel ID="pnllist" runat="server">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>OD Leave Approval List</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-12">--%>
                                        <div class="col-12 table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                        <asp:Panel ID="pnlPendingList" runat="server">
                                            <asp:ListView ID="lvPendingList" runat="server">
                                                <EmptyDataTemplate>
                                                    <p class="text-center text-bold">
                                                        <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of OD for Approval" Font-Bold="true">
                                                        </asp:Label>
                                                    </p>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Pending List of OD for Approval</h5>
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
                                                                <th>From DATE
                                                                </th>
                                                                <th>To DATE
                                                                </th>
                                                                <th>Place
                                                                </th>
                                                                <%--<th>
                                                                    Instructed By
                                                                 </th>--%>
                                                                <th>OD TYPE
                                                                </th>
                                                                <%-- <th>
                                                                    In_Time
                                                                    </th>--%>
                                                                <th>Approve/Reject
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
                                                            <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                            </asp:Panel>
                                                        </td>
                                                        <td>
                                                            <%# Eval("sno")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("EmpName")%>
                                                            <asp:HiddenField ID="hidEmpno" Value='<%# Eval("EMPNO")%>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("FROM_DATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TO_DATE", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLACE")%>
                                                        </td>
                                                        <%--  <td>
                                                                <%# Eval("INSTRUCTED_BY") %>
                                                             </td>--%>
                                                        <td>
                                                            <%# Eval("ODTYPE")%>
                                                        </td>
                                                        <%--<td>
                                                                <%# Eval("IN_TIME") %>
                                                            </td>--%>
                                                        <td>
                                                            <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("ODTRNO")%>'
                                                                ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" TabIndex="1"
                                                                CssClass="btn btn-primary" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                            <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                                <table class="table table-bordered table-hover">
                                                                    <tr class="bg-light-blue">
                                                                        <th>Purpose Of Visit
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%# Eval("PURPOSE") %>
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
                                        </asp:Panel>
                                       <%-- <div class="vista-grid_datapager d-none">
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
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="lnkbut" runat="server" OnClick="lnkbut_Click" Text="OD Approval Status" TabIndex="2"
                                    CssClass="btn btn-primary" ToolTip="Click here for OD Approval Status"></asp:LinkButton>
                            </div>
                            <asp:Panel ID="pnlENtry" runat="server">
                                <div id="trfrmto" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Form Date"
                                                        Style="z-index: 0;" TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFromDt" runat="server" ControlToValidate="txtFromdt"
                                                        Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="show">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceFromDt" runat="server" Enabled="true"
                                                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeFromDt" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDt" runat="server" ControlExtender="meeFromDt"
                                                        ControlToValidate="txtFromdt"
                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="show" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                                        Style="z-index: 0;" TabIndex="4" OnTextChanged="txtTodt_TextChanged"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvToDt" runat="server" ControlToValidate="txtTodt"
                                                        Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="show">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CeToDt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeToDt" runat="server" AcceptNegative="Left"
                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                        MessageValidatorTip="true" TargetControlID="txtTodt" />

                                                    <ajaxToolKit:MaskedEditValidator ID="mevToDt" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtTodt"
                                                        InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="show" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>

                                                    <%--  <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtTodt"
                                                    CultureInvariantValues="true" Display="Static" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date."
                                                    Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                    ValidationGroup="show" ControlToCompare="txtFromdt" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary"
                                            ValidationGroup="show" ToolTip="Click here to Show" TabIndex="5" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlODStatus" runat="server">
                                <%-- <legend class="legendPay">Approval Status</legend>--%>
                                <div class="col-12">
                                    <asp:Panel ID="pnlStatusList" runat="server">
                                        <asp:ListView ID="lvApprStatus" runat="server">
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication" Font-Bold="true"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>OD Approval Status</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Employee name
                                                            </th>
                                                            <th>From Date
                                                            </th>
                                                            <th>To Date
                                                            </th>
                                                            <th>Place Of Visit
                                                            </th>
                                                            <th>Purpose
                                                            </th>

                                                            <th>OD Type
                                                            </th>
                                                            <th>Approval Date
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
                                                        <%# Eval("EMPNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FROM_DATE", "{0:dd-MM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TO_DATE", "{0:dd-MM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Place")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PURPOSE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ODTYPE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("APR_DATE", "{0:dd-MM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Status") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnHidePanel" runat="server" Text="Back" OnClick="btnHidePanel_Click" CssClass="btn btn-primary"
                                        ToolTip="Click here to Go Back" TabIndex="6" />
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>OD Records (Approve/Reject)</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Employee Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEmpName" runat="server" TabIndex="7" ToolTip="Employee Name"></asp:Label>
                                                        <asp:HiddenField ID="hidIdno" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>From Date :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFromDate" runat="server" TabIndex="8" ToolTip="From Date"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>To Date :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblToDate" runat="server" TabIndex="9" ToolTip="To Date"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12" id="trTime" runat="server">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Out Time :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblOutTime" runat="server" TabIndex="10" ToolTip="Out Time"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>In Time :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblInTime" runat="server" TabIndex="11" ToolTip="In Time"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Place of Visit :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPlace" runat="server" TabIndex="12" ToolTip="Place Of Visit"></asp:Label></a>
                                                </li>
                                                <%--<li class="list-group-item"><b>Purpose Of Visit :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" /></a>
                                                    </li>--%>
                                                <li class="list-group-item"><b>Purpose of Visit :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblPurpose" runat="server" TabIndex="13" ToolTip="Purpose Of Visit"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12" id="divevent" runat="server">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Event Type :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEvent" runat="server" TabIndex="10" ToolTip="Event Type"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Topic :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTopic" runat="server" TabIndex="11" ToolTip="Topic"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item" id="trInst" runat="server" visible="false"><b>Instructed By :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblInstr" runat="server" TabIndex="14" ToolTip="Instructed By"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item" id="trRegAmt" runat="server" visible="false"><b>Reg. Amount :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRegAmt" runat="server" Font-Bold="true" TabIndex="15" ToolTip="Registration Amount"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trModReg" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Modified Reg.Amt</label>
                                            </div>
                                            <asp:TextBox ID="txtRegAmt" runat="server" TabIndex="16" ToolTip="Modified Registration Amount"
                                                onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trtada" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>TA/DA Amount</label>
                                            </div>
                                            <asp:Label ID="lblTADAAmt" runat="server" CssClass="form-control" Visible="false"
                                                Font-Bold="true"></asp:Label>
                                            <asp:TextBox ID="txtTADAamt" runat="server" TabIndex="17" ToolTip="TA/DA Amount"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" TabIndex="18"
                                                AppendDataBoundItems="true" ToolTip="Select Approve/Reject" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="A">Approve</asp:ListItem>
                                                <asp:ListItem Value="R">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvselect" runat="server" ControlToValidate="ddlSelect"
                                                Display="None" ErrorMessage="Please Select Approve/Reject" InitialValue="0" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" TabIndex="19"
                                                CssClass="form-control" ToolTip="Enter Remarks" />
                                        </div>

                                        <div class="col-12" id="divDocument" runat="server">
                                            <div class="sub-heading">
                                                <h5>Download Document</h5>
                                            </div>
                                            <%--<div class="form-group col-md-8">--%>
                                            <asp:ListView ID="lvDownload" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"),Eval("ODTRNO"),Eval("EMPNO"))%>'><%# Eval("FILENAME")%></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%-- </div>--%>
                                        </div>

                                        <div class="form-group col-md-12">
                                            <asp:Panel ID="pnlODStatusList" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvStatus" runat="server">
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                                        </p>
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
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="20"
                                        CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="21"
                                        OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="22"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        //Calculating the total amount
        function totalamount(val) {
            if (ValidateNumeric(val)) {
                var txtMonthlyDedAmt = document.getElementById("ctl00_ctp_txtMonthlyDedAmt");
                var txtTotalAmount = document.getElementById("ctl00_ctp_txtTotalAmount");
                var txtOutStandingAmt = document.getElementById("ctl00_ctp_txtOutStandingAmt");
                var txtNoofInstPaid = document.getElementById("ctl00_ctp_txtNoofInstPaid");
                txtTotalAmount.value = Number(val.value) * Number(txtMonthlyDedAmt.value);
                txtNoofInstPaid.value = 0;
                txtOutStandingAmt.value = txtTotalAmount.value;
            }
        }




        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters alloewd");
                return false;
            }
            else {
                return true;
            }

        }



    </script>
</asp:Content>
