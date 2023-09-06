<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Permissionapproval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Permissionapproval"
    MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PERMISSION APPROVAL</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Permission Approval List</h5>
                            </div>
                            <asp:Panel ID="pnlPending" runat="server">
                                <asp:ListView ID="lvPendingList" runat="server">
                                    <EmptyDataTemplate>
                                        <div class="text-center">
                                            <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of Leaves for Approval">
                                            </asp:Label>
                                        </div>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <%-- <div class="sub-heading">
                                                <h5>Pending List of PERMISSION for Approval</h5>
                                            </div>--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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
                                                    <asp:Image ID="imgExp" runat="server" ImageUrl="~/images/action_down.png" TabIndex="1" />
                                                </asp:Panel>
                                            </td>
                                            <td>
                                                <%# Eval("sno")%>
                                            </td>
                                            <td>
                                                <%# Eval("EmpName")%>
                                            </td>
                                            <td>
                                                <%# Eval("Date")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("PERTNO")%>' TabIndex="2"
                                                    ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                                <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel">
                                                    <table class="table table-bordered table-hover">
                                                        <tr class="bg-light-blue">
                                                            <th>Reason
                                                            </th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%# Eval("REMARK") %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                            TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                            ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/images/action_down.png"
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
                    </asp:Panel>

                    <div class="col-12  btn-footer mt-3">
                        <asp:LinkButton ID="lnkbut" runat="server" OnClick="lnkbut_Click" Text="Permission Approval Status" CssClass="btn btn-primary"
                            ToolTip="Click here for Leave Approval Status" TabIndex="4"></asp:LinkButton>
                    </div>

                    <asp:Panel ID="pnlODStatus" runat="server">

                        <div id="trfrmto" class="col-12" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>From Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalFromdt">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Form Date"
                                            Style="z-index: 0;" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                            Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>To Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCalTodt">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                            Style="z-index: 0;" TabIndex="6"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                            Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtTodt" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer" id="trbutshow" runat="server">
                            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btn btn-primary"
                                ToolTip="Click here To Show Status" TabIndex="7" />
                        </div>
                        <div class="col-12 mt-3">
                            <asp:Panel ID="pnlStatusList" runat="server">
                                <asp:ListView ID="lvApprStatus" runat="server">
                                    <EmptyDataTemplate>
                                        <p class="text-center text-bold">
                                            <asp:Label ID="ibler" runat="server" Text="No more Leave aaplication"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Permission Approval Status</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Employee Name
                                                        </th>
                                                        <th>Department
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Reason
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
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("EmpName")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBDEPT")%>
                                            </td>
                                            <td>
                                                <%# Eval("DATE", "{0:dd-MM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("REMARK")%>
                                            </td>
                                            <td>
                                                <%# Eval("APR_DATE" , "{0:dd-MM-yyyy}")%>
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
                            <asp:Button ID="btnHidePanel" runat="server" Text="Back" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                OnClick="btnHidePanel_Click" TabIndex="8" />
                        </div>

                    </asp:Panel>

                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-md-12 col-lg-12 col-12" id="pnlAdd" runat="server">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Employee Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmpName" runat="server" TabIndex="17" ToolTip="Employee Name"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblFromdt" runat="server" TabIndex="19" ToolTip="From Date"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Reason :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblReason" runat="server" TabIndex="24" ToolTip="Reason"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Permission Type :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblcat" runat="server" TabIndex="25" ToolTip="Permission Type"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Approve/Reject"
                                            AppendDataBoundItems="true" TabIndex="28">
                                            <%--<asp:ListItem Value="A">Approve</asp:ListItem>
                                                <asp:ListItem Value="R">Reject</asp:ListItem>--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="F">Approve & Forward To Next Authority(Recommended)</asp:ListItem>
                                            <asp:ListItem Value="A">Approve & Final Submit</asp:ListItem>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvselectmodify" runat="server" ControlToValidate="ddlSelect"
                                            Display="None" ErrorMessage="Please Select Status" InitialValue="0" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remarks</label>
                                        </div>
                                        <asp:textbox id="txtRemarks" runat="server" textmode="MultiLine" cssclass="form-control" tabindex="29"
                                            tooltip="Enter Remarks" onkeypress="if (this.value.length > 110) { return false; }" xmlns:asp="#unknown" />
                                    </div>


                                </div>
                            </div>

                            <div class="col-lg-12 col-12" id="divAuthorityList" runat="server" visible="false">
                                <asp:Panel ID="pnlSelectList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStatus" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="ibler" runat="server" Text=""></asp:Label>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Approval Status</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
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
                                            </div>
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
                    <asp:Panel ID="pnlButton" runat="server" Visible="false">
                        <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="30"
                                CssClass="btn btn-primary" ToolTip="Click here To Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary"
                                OnClick="btnBack_Click" ToolTip="Click here to Go Back" TabIndex="32" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="31"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

