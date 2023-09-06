<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_SalaryConsolidateStatement.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_SalaryConsolidateStatement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CONSOLIDATED SALARY STATEMENT</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Month / Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" ToolTip="Select Month/Year" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="1">
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
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="2">
                                                <%--OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true"--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Scheme" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="3">
                                                <%--OnSelectedIndexChanged="ddlStaffNo_SelectedIndexChanged" AutoPostBack="true"--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaff" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Staff"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployeeType" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlEmployeeType" Display="None" ErrorMessage="Please Select Employee Type"
                                                ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <%--<div class="form-group col-md-10">
                                            <label>Order By :<span style="color: Red">*</span></label>
                                            <asp:DropDownList ID="ddlOrderBy" runat="server" CssClass="form-control"
                                                AppendDataBoundItems="True" TabIndex="5" ToolTip="Select Order by">
                                                <asp:ListItem Value="1" Text="IDNO"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="SEQ NO."></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>
                                        <%--<div class="form-group col-md-10" id="trCertificate" runat="server">
                                            <label>Salary Certificate Issued to :</label>

                                            <asp:TextBox ID="txtBankName" ToolTip="Enter Salary Certificate Issued to" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>
                                        </div>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Consolidated Salary Statement"
                                    OnClick="btnShow_Click" CssClass="btn btn-primary" TabIndex="5" ToolTip="Click to Show Bank Statement"
                                    ValidationGroup="Payroll" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                    OnClick="btnCancel_Click" TabIndex="6" ToolTip="Click to Reset" />
                                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                    DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server"></div>
    <script type="text/javascript">
        function DisableDropDownList(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
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
    </script>
</asp:Content>
