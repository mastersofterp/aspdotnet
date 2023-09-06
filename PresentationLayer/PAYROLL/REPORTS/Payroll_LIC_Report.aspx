<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Payroll_LIC_Report.aspx.cs" Inherits="PayRoll_Payroll_LIC_Report" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">EMPLOYEE LIC REPORT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Select Month</label>
                                    </div>
                                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                        CssClass="form-control" ToolTip="Select Month" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMonth" runat="server" Display="None" ControlToValidate="ddlMonth"
                                        ErrorMessage="Please Select Month." ValidationGroup="ADD" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--  <sup>* </sup>--%>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select College Name" data-select2-enable="true"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>* </sup>--%>
                                        <%--<label>Staff</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" ToolTip="Select Staff Wise" data-select2-enable="true"
                                        AppendDataBoundItems="True" TabIndex="3">
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="ddlMonth"
                                        ErrorMessage="Please Select Staff." ValidationGroup="ADD" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                    --%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnLICReport" runat="server" Text="LIC"
                            CssClass="btn btn-info" TabIndex="4" OnClick="btnLICReport_Click" ValidationGroup="ADD" />

                        <asp:Button ID="btnLICExcel" runat="server" Text="LIC Excel" CssClass="btn btn-info" TabIndex="5" OnClick="btnLICExcel_Click" ValidationGroup="ADD" />

                        <asp:Button ID="btnPTReport" runat="server" Text="PT Report" ValidationGroup="ADD" TabIndex="5" CssClass="btn btn-info" OnClick="btnPTReport_Click" />
                        <asp:Button ID="btnEPFReport" runat="server" Text="EPF Report" ValidationGroup="ADD" CssClass="btn btn-info" TabIndex="6" OnClick="btnEPFReport_Click" />

                        <asp:Button ID="btnEPFCexcel" runat="server" Text="EPF Excel" CssClass="btn btn-info" TabIndex="8" ToolTip="Click to Show EPF Excel" ValidationGroup="Payroll" OnClick="btnEPFCexcel_Click" />
                        <asp:Button ID="btnEPFExcelNew" runat="server" Text="EPF Excel New" CssClass="btn btn-info" TabIndex="8" ToolTip="Click to Show EPF Excel" ValidationGroup="Payroll" OnClick="btnEPFExcelNew_Click" />
                        <asp:Button ID="btnEPFWord" runat="server" Text="EPF Text" CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Show EPF Excel" ValidationGroup="Payroll" OnClick="btnEPFWord_Click" />


                        <asp:Button ID="btnGrossTDSReport" runat="server" CssClass="btn btn-info" ValidationGroup="ADD" Text="Gross & TDS Report"
                            TabIndex="7" OnClick="btnGrossTDSReport_Click" />

                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="8" OnClick="btnCancel_Click" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ADD"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function DisabledEmpDropdown() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_rdoAllEMployee').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = true;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = false;
            }
        }
        function EnabledEmpDropdown() {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = false;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').disabled = true;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlStaffNo').selectedIndex = 0;
        }
    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>

