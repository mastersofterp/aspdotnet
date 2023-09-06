<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_DolDojRdt.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_DolDojRdt" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">DETAIL REPORT</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Employee Detail Report</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlAuthor" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalFromDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ToolTip="Enter From Date" CssClass="form-control" />
                                        <%--<div class="input-group-addon">
                                            <asp:Image ID="imgCalFromDate" runat="server"
                                                Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                                        </div>--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate1" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                            ValidationGroup="Payroll" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalToDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="2"
                                            OnTextChanged="txtToDate_TextChanged" ToolTip="Enter To Date" CssClass="form-control" />
                                        <%--<div class="input-group-addon">
                                            <asp:Image ID="imgCalToDate" runat="server"
                                                Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />
                                        </div>--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="imgCalToDate1" Enabled="true" EnableViewState="true">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                        <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                            Display="None" ErrorMessage="Please enter last date for report." SetFocusOnError="true"
                                            ValidationGroup="Payroll" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trStaff" runat="server">
                                    <div class="label-dynamic">
                                        <%--<label>Staff Type</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Scheme/Staff">
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server">
                                    <div class="label-dynamic">
                                        <label>Report</label>
                                    </div>
                                    <asp:DropDownList ID="ddlReport" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Report" data-select2-enable="true">
                                        <asp:ListItem Text="Date of Joining" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Date of Leaving" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Date of Retirement" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Date of Increment" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Expiry date of Extention" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                            OnClick="btnShowReport_Click" CssClass="btn btn-info" ToolTip="Click To Show Report" ValidationGroup="Payroll" TabIndex="5" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                            OnClick="btnCancel_Click" TabIndex="6" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server"
                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Payroll" Width="123px" />
                    </div>


                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function DisableDropDownListAllEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = false;

        }
        function DisableDropDownListParticularEmployee(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = disable;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = false;
        }

    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>

