<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_PFESIReport.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_PFESIReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ESIC Report</h3>
                        </div>

                        <form role="form">
                            <div class="box-body">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month / Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlMonthYear" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="1" ToolTip="Select Month/Year">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonthYear" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlMonthYear" Display="None" ErrorMessage="Please Select Month/Year"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                  <%--  <sup>* </sup>--%>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    TabIndex="2" ToolTip="Select College" >
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                   <%-- <sup>* </sup>--%>
                                                   <%-- <label>Staff</label>--%>
                                                    <label>Scheme/Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaffNo" runat="server" ToolTip="Select Staff" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                    TabIndex="3">
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                                    ControlToValidate="ddlStaffNo" Display="None" ErrorMessage="Please Select Scheme"
                                                    ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                               --%>             </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display:none;">
                                                <div class="label-dynamic">
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmployeeType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="True" TabIndex="4" ToolTip="Select Staff">
                                                </asp:DropDownList>
                                            </div>
                                            <%--<div class="form-group col-md-10" id="trrbl" runat="server" >

                                                <asp:RadioButton ID="rdoSelectEmployee" runat="server" Checked="true" Text="Select Employee"
                                                    GroupName="A" TabIndex="4" onclick="DisableDropDownList(false);" />
                                                <asp:RadioButton ID="rdoAllEmployee" runat="server" Text="All Employee" GroupName="A"
                                                    onclick="DisableDropDownList(true);" TabIndex="5" />

                                            </div>--%>
                                            <%--<div class="form-group col-md-10">
                                                <label>Employee:</label>

                                                <asp:DropDownList ID="ddlEmployeeNo" runat="server" AppendDataBoundItems="True" TabIndex="6"
                                                    CssClass="form-control" ToolTip="Select Employee">
                                                </asp:DropDownList>

                                            </div>--%>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Salary Certificate Issued to</label>
                                                </div>
                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ToolTip="Enter Salary Certificate Issued to " TabIndex="5"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="False" />
                                    <asp:Button ID="btnPFReport" runat="server" Text="PF Contribution" ToolTip="Click to Show PF Report" CssClass="btn btn-info"
                                        TabIndex="6" ValidationGroup="Payroll" Visible="false" />
                                    <asp:Button ID="btnESIReport" runat="server" ToolTip="Click to Show ESI Report" Text="ESIC Report"
                                        TabIndex="7" ValidationGroup="Payroll" CssClass="btn btn-info" OnClick="btnESIReport_Click" />

                                    <asp:Button ID="btnESICExcelReport" runat="server" ToolTip="Click to Show ESI Excel Report" Text="ESIC Excel"
                                        TabIndex="7" ValidationGroup="Payroll" CssClass="btn btn-info" OnClick="btnESICExcelReport_Click" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click To Reset" OnClick="btnCancel_Click"
                                        TabIndex="8" />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPFReport" />
            <asp:PostBackTrigger ControlID="btnESIReport" />
            <asp:PostBackTrigger ControlID="btnESICExcelReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function DisableDropDownList(disable) {
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').selectedIndex = 0;
            document.getElementById('ctl00_ContentPlaceHolder1_ddlEmployeeNo').disabled = disable;
        }
    </script>

</asp:Content>

