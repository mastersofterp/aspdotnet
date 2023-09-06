<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Financial_Year.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Financial_Year" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FINANCIAL YEAR</h3>
                </div>

                <div>
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">

                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Financial Year</div>
                                        <div class="panel panel-body">
                                            <div class="col-md-12">
                                                <div class="form-group col-md-3">
                                                    <label>Start Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalAdvdt" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFromdate" runat="server" MaxLength="10" Style="z-index: 0;" TabIndex="12"
                                                            CssClass="form-control" ToolTip="Enter Advance Date" />
                                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server"
                                                            ControlToValidate="txtFromdate" Display="None" ErrorMessage="Enter Advance Date"
                                                            SetFocusOnError="true" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtFromdate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtFromdate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt" ControlToValidate="txtFromdate"
                                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                            TooltipMessage="Enter Advance Date" ValidationGroup="payroll">                                                    
                                                    &#160;&#160;
                                                        </ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label>End Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtToYear" runat="server" MaxLength="10" Style="z-index: 0;" TabIndex="12"
                                                            CssClass="form-control" ToolTip="Enter To Date" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtToYear" Display="None" ErrorMessage="Enter To Date"
                                                            SetFocusOnError="true" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtToYear">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            TargetControlID="txtToYear" />
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeTodt" ControlToValidate="txtToYear"
                                                            Display="None" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="true"
                                                            TooltipMessage="Enter Advance Date" ValidationGroup="payroll">                                                    
                                                    &#160;&#160;
                                                        </ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label>Financial Year :</label>
                                                    <asp:TextBox ID="txtFinanYear" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-md-3">
                                                    <label>Short Name :</label>
                                                    <asp:TextBox ID="txtshortName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-md-12">

                                                <p class="text-center">
                                                    <asp:Button ID="Button1" TabIndex="3" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-success  "
                                                        OnClick="butSubmit_Click" ToolTip="Click here to Save" />
                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" TabIndex="4" OnClick="Button2_Click"
                                                        CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </p>

                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                            <div class="form-group col-md-12">
                                <asp:Panel ID="pnlFinanYearList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvFinancialYear" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="ibler" runat="server" Text="No more Financial Year"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="box-title">Financial Year
                                                </h4>
                                                <table class="table table-bordered table-hover">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>Start Date 
                                                            </th>
                                                            <th>End Date
                                                            </th>
                                                            <th>Financial Year
                                                            </th>
                                                            <th>Short Name
                                                            </th>
                                                            <th>Edit
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
                                                    <%# Eval("FROMDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TODATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FINANCIAL_YEAR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SHORT_NAME")%>
                                                </td>

                                                <td>
                                                    <%-- <%# Eval("FINYEARID")%>--%>
                                                    <asp:Button ID="btnRPT" runat="server" Text="Edit" CommandArgument='<%# Eval("FINYEARID")%>'
                                                    OnClick="btnRPT_Click"  CssClass="btn btn-info"  ToolTip="Edit Record" />
                                                    
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>
                            </div>

                        </div>
                    </form>
                </div>
                <div>
                    <p class="page_help_text">
                        <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                    </p>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server">
        </div>
    </div>
</asp:Content>

