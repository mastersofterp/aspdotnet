<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EventApproval.aspx.cs" Inherits="Sports_Transaction_EventApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">EVENT APPROVAL</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12" id="divAdd" runat="server" visible="false">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Institute :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCollege" runat="server"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>From Date  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFromdt" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Event Name:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEvent" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>To Date  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTodt" runat="server"></asp:Label>
                                                    </a>
                                                </li>

                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Organizing Team :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblOrganizingTeam" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row mt-3">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Status" AppendDataBoundItems="true">
                                                <asp:ListItem Value="A">Approve</asp:ListItem>
                                                <asp:ListItem Value="R">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Remarks</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Remark" Height="35px" />
                                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks" Display="None"
                                                ErrorMessage="Please Enter Remarks" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12" id="divEdit" runat="server" visible="false">
                                <asp:Panel ID="pnlEdit" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Date</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtfrmdt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Select From Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtfrmdt" Display="None"
                                                    ErrorMessage="Please Enter From Date" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromdt" TargetControlID="txtfrmdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" TargetControlID="txtfrmdt" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txttodate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control" ToolTip="Select To Date" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txttodate" Display="None" ErrorMessage="Please Enter To Date"
                                                    ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txttodate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" TargetControlID="txttodate" />
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnllist" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvEventPendingList" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Event List for Approval</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Event Name
                                                            </th>
                                                            <th>From Date
                                                            </th>
                                                            <th>To Date
                                                            </th>
                                                            <th>Organizing Team 
                                                            </th>
                                                            <th>Institute
                                                            </th>
                                                            <th>Approve/Reject
                                                            </th>
                                                            <th>Modify
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
                                                    <%# Eval("EVENTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FROM_DATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TO_DATE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME") %>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("PSID")%>'
                                                        ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnModify" runat="server" Text="Modify" CommandArgument='<%# Eval("PSID")%>'
                                                        ToolTip="Select to Modify and Approve" OnClick="btnModify_Click" CssClass="btn btn-primary" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12" id="divAuthority" runat="server" visible="false">
                                <asp:Panel ID="PanelAuthority" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStatus" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <%--<div class="sub-heading">
                                                    Approval Status
                                                </div>--%>
                                                <div class="sub-heading"><h5>Approval Status</h5>
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
                                                    <%# Eval("SNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PANAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PAUSERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class=" col-12 btn-footer" id="divButtons" runat="server" visible="false">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" CssClass="btn btn-primary" ToolTip="Click here to Back" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click"/>
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Leaveapp" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

