<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_SupplementaryWise_Report.aspx.cs" Inherits="PAYROLL_REPORT_Pay_SupplementaryWise_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">SUPPLIMENTARY WISE REPORT</h3>
                </div>
                <div class="box-body">
                        <div class="panel panel-info">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                 <sup>* </sup>
                                                <label>Month & Year</label>
                                            </div>
                                             <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true" ToolTip="Select Month" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>     
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                TabIndex="2">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trstaff" runat="server">
                                            <div class="label-dynamic">
                                                <%--<label>Scheme</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme" AppendDataBoundItems="True" TabIndex="3"  data-select2-enable="true">
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Supplimentary Head</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSuppliHead" runat="server" CssClass="form-control" AutoPostBack="true" data-select2-enable="true"
                                                AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSuppliHead_SelectedIndexChanged" TabIndex="4" ToolTip="Select Supplimentry Head">
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlSuppliHead" Display="None" ErrorMessage="Please Select Supplimentry Head"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Order No</label>
                                            </div>
                                            <asp:DropDownList ID="ddlOrderBy" runat="server"
                                                CssClass="form-control" ToolTip="Select Order No" AppendDataBoundItems="True" TabIndex="5" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvOrderBy" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlOrderBy" Display="None" ErrorMessage="Please Select Order No"
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" ToolTip="Select Department" AppendDataBoundItems="True" TabIndex="6" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="rfvDept" runat="server" SetFocusOnError="true"
                                                        ControlToValidate="ddlDept" Display="None" ErrorMessage="Please Select Department "
                                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>
                                   <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSuppWiseRpt" runat="server" Text="Report"
                                ToolTip="Click to Show Supplimentry wise Report"   CssClass="btn btn-outline-info"
                                 ValidationGroup="Payroll" TabIndex="6"  OnClick="btnSuppWiseRpt_Click"/>
                                <asp:Button ID="btnexporttoexcel" runat="server" Text=" Export To Excel Format "
                                 CssClass="btn btn-outline-info" TabIndex="12"    ValidationGroup="Payroll" OnClick="btnexporttoexcel_Click" />

                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                            </p>
                            <div class="col-md-12">
                            </div>
                        </div>
                            </asp:Panel>
                        </div>

                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

