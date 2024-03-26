<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LeaveDocumentDown.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_LeaveDocumentDown" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE APPLICATION DOCUMENTS</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Leave Application List</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">

                            <%--<asp:Panel ID="pnlLeaveList" runat="server">--%>
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
                                            Style="z-index: 0;" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdt"
                                            Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Leave">
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
                                            ValidationGroup="Leave" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
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
                                        <asp:TextBox ID="txtTodt" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter To Date"
                                            Style="z-index: 0;" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                            Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leave">
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
                                            ValidationGroup="Leave" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click here To Show" ValidationGroup="Leave" OnClick="btnshow_Click" TabIndex="3"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset" CssClass="btn btn-warning" TabIndex="4"  OnClick="btnCancel_Click"/>
                                    <asp:ValidationSummary ID="ValShow" runat="server" ValidationGroup="Leave"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                
                            </div>
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
                                                <h5>Leave Allication</h5>
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
                                                        <th>Download
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
                                                <td style="width: 15%">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"),Eval("LETRNO"),Eval("EMPNO"))%>'><%# Eval("FILENAME")%></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

