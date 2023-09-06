<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OthReport.aspx.cs" Inherits="PAYROLL_REPORTS_OthReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">OTHER REPORTS</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="sub-heading">
                            <h5>EMPLOYEES OTHER REPORTS</h5>
                        </div>
                    </div>
                    <asp:Panel ID="pnlOther" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Month</label>
                                    </div>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="1" ToolTip="Select Month">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMonth" runat="server" Display="None" ControlToValidate="ddlMonth"
                                        ErrorMessage="Please Select Month." ValidationGroup="Payroll" SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" TabIndex="2" ToolTip="Select College">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaffNo" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Staff"
                                        AppendDataBoundItems="true" TabIndex="3">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Show In Report</label>
                                    </div>
                                    <asp:DropDownList ID="ddlShowInReport" runat="server" TabIndex="4" ToolTip="Select Show In Report" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Bank Account No.</asp:ListItem>
                                        <asp:ListItem Value="2">PF No.</asp:ListItem>
                                        <asp:ListItem Value="3">PAN No.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Pay Heads</label>
                                    </div>
                                    <asp:ListBox ID="lstParticularColumn" runat="server" SelectionMode="Multiple" ToolTip="Select Pay Heads"
                                        CssClass="form-control" TabIndex="5" AppendDataBoundItems="True" Style="height: 150px!important"></asp:ListBox>
                                    <asp:RequiredFieldValidator ID="rfvLstParticularColumn" runat="server" ControlToValidate="lstParticularColumn"
                                        Display="None" ErrorMessage="Please Select Pay Head" SetFocusOnError="true" InitialValue="0"
                                        ValidationGroup="Payroll" />
                                    <asp:HiddenField ID="hidIdNo" runat="server" />
                                </div>


                            </div>

                        </div>
                    </asp:Panel>

                    <div class="col-12 btn-footer">
                        <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                            OnClick="btnShowReport_Click" ValidationGroup="Payroll" TabIndex="6" CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                            OnClick="btnCancel_Click" TabIndex="7" />
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
        function DisableListboxOnParticularColumn(disable) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_chkPartiularColumn').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstParticularColumn').disabled = disable;
            }
            if (!document.getElementById('ctl00_ContentPlaceHolder1_chkPartiularColumn').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstParticularColumn').disabled = false;
            }
        }
        function DisabledListboxForMonth(disable) {
            if (document.getElementById('ctl00_ContentPlaceHolder1_rdoAllMonth').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').selectedIndex = 0;
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disabled = disable;
            }
            if (!document.getElementById('ctl00_ContentPlaceHolder1_rdoAllMonth').checked) {
                document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disable = false;
            }
        }
        function EnabledListboxForMonth() {
            document.getElementById('ctl00_ContentPlaceHolder1_lstMonth').disabled = false;
        }
        function DateDiff() {

        }

    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>
