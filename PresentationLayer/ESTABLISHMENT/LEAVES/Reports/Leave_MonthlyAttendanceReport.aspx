<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Leave_MonthlyAttendanceReport.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_Leave_MonthlyAttendanceReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">MONTHLY ATTENDANCE REPORT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Employee Monthly Attendance Report</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updAdd" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Select Department" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFVdept" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select Staff Type" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaffType"
                                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trddldept" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Department" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="RFVdept" runat="server" ControlToValidate="ddldept"
                                                        Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trchkdept" runat="server" style="padding-top: 12px" visible="false">
                                            <div class="label-dynamic">
                                                <label>
                                                    <asp:CheckBox ID="chkDept" Text="Department wise " runat="server"
                                                        OnCheckedChanged="chkDept_CheckedChanged" AutoPostBack="True" Visible="true" TabIndex="4" ToolTip="Select Department wise" /></label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <asp:RadioButtonList ID="rblAllParticular" runat="server" TabIndex="5" ToolTip="Employees"
                                                    RepeatDirection="Horizontal"
                                                    OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="All Employees" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Particular Employee" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Select Employees" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Month/Year</label>
                                            </div>
                                            <div class="input-group date">
                                                <asp:TextBox ID="txtMonthYear" runat="server" onblur="return checkdate(this);" TabIndex="7"
                                                    CssClass="form-control"></asp:TextBox>
                                               <div class="input-group-addon">
                                                <i id="imgMonthYear" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>

                                                <ajaxToolKit:CalendarExtender ID="ceMonthYear" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MM/yyyy" PopupButtonID="imgMonthYear" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please Enter Month & Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                    ValidationGroup="Leaveapp">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="form-group col-md-4">
                                                    <label>From Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtMonthYear" runat="server" TabIndex="7" ToolTip="Enter From Date" Style="z-index: 0;" AutoPostBack="true" onblur="return checkdate(this);"
                                                            CssClass="form-control"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />
                                                        </div>

                                                        <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="mefrmdt" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" TargetControlID="txtMonthYear" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                            Display="None" ErrorMessage="Please Date in (dd/MM/yyyy Format)" SetFocusOnError="True"
                                                            ValidationGroup="payroll">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>--%>
                                <%-- <div class="form-group col-md-4">
                                                    <label>To Date :<span style="color: Red">*</span></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" TabIndex="8" ToolTip="Enter To Date" Style="z-index: 0;"
                                                            CssClass="form-control" />
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                            Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>

                                                        <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" TargetControlID="txtTodt" />
                                                    </div>
                                                </div>--%>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp" TabIndex="9" ToolTip="Click to show the Report"
                                        CssClass="btn btn-info" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnCombinreRpt" runat="server" Text="Combine Report" ValidationGroup="Leaveapp" TabIndex="10" ToolTip="Click to Combine Report"
                                        CssClass="btn btn-info" OnClick="btnCombinreRpt_Click" Visible="true" />
                                    <asp:Button ID="btnReport_Format2" runat="server" Text="Format2 Report" ValidationGroup="Leaveapp"
                                        CssClass="btn btn-info" OnClick="btnReport_Format2_Click" TabIndex="11" ToolTip="Click to Monthly Attendance Format2 Report" />
                                    <%-- <asp:Button ID="btnExport" runat="server" Text="Export" ValidationGroup="Leaveapp"
                                                                CssClass="btn btn-info" TabIndex="9" ToolTip="Click to Monthly Attendance Format2 in excel " OnClick="btnExport_Click" />--%>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="12" ToolTip="Click To Reset"
                                        CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <div class="col-md-12">
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnReport" />
                            <asp:PostBackTrigger ControlID="btnCombinreRpt" />
                            <asp:PostBackTrigger ControlID="btnReport_Format2" />
                            <asp:PostBackTrigger ControlID="btnCancel" />
                            <%--<asp:PostBackTrigger ControlID="btnExport" />--%>
                        </Triggers>
                    </asp:UpdatePanel>


                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>

