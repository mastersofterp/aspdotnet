<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Attendance_Process.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Attendance_Process" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTENDANCE PROCESS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Criteria for Attendance Process</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-lg-6 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note (Please Select)</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>1)Please Don't close the browser when Attendance is processed</span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>2)Please click Attendance process button only once</span></p>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="payroll"
                                            SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Staff Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                            AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                            Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStafftype"
                                            Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="payroll"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Month/Year</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgMonthYear" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtMonthYear" runat="server" onblur="return checkdate(this);" AutoPostBack="true" OnTextChanged="txtMonthYear_TextChanged"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceMonthYear" runat="server" Enabled="true" EnableViewState="true"
                                                Format="MM/yyyy" PopupButtonID="imgMonthYear" TargetControlID="txtMonthYear">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMonthYear"
                                                Display="None" ErrorMessage="Please Enter Month & Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <%-- <div class="form-group col-md-4">
                                                        <label>From Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" onblur="return checkdate(this);" TabIndex="3"
                                                                OnTextChanged="txtMonthYear_TextChanged" CssClass="form-control" ToolTip="Enter From Date"></asp:TextBox>
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
                                    <%--<div class="form-group col-md-4">
                                                        <label>To Date :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtTodt" runat="server" AutoPostBack="true" MaxLength="10" TabIndex="4"
                                                                CssClass="form-control" ToolTip="Enter To Date" OnTextChanged="txtTodt_TextChanged" />
                                                            <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                                Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                                Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                MessageValidatorTip="true" TargetControlID="txtTodt" />
                                                        </div>
                                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="5" data-select2-enable="true"
                                            CssClass="form-control" ToolTip="Select Department" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%--<asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"

                                                        <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"

                                                      <%--  <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"

                                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="payroll">
                                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-info">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Attendance Reprocess For..?</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12" id="trselection" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <asp:RadioButton ID="radAllEmployees" GroupName="radpay" runat="server" Text="All Employees" TabIndex="6"
                                                Checked="true" AutoPostBack="true" OnCheckedChanged="radAllEmployees_CheckedChanged" />
                                            &nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="radSelectedEmployees" GroupName="radpay" runat="server" Text="Selected Employees"
                                                            AutoPostBack="true" OnCheckedChanged="radSelectedEmployees_CheckedChanged" TabIndex="7" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremployee" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Employee</label>
                                            </div>
                                            <asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true"
                                                CssClass="form-control" TabIndex="8" style="height:170px!important; width:300px"></asp:ListBox>
                                            <%--<asp:ListBox ID="lstEmployee" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true"
                                                            Width="400px" Height="170px"></asp:ListBox>--%>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="lstEmployee"
                                                Display="None" ErrorMessage="Please Select Employee" InitialValue="" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl"></asp:Label>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="butAttendanceProcess" runat="server" Text="Process Attendance" ValidationGroup="payroll" TabIndex="9"
                                    CssClass="btn btn-primary" ToolTip="Click here to Process Attendance" OnClick="butAttendanceProcess_Click" />
                                <asp:Label Visible="false" ID="lblProcess" Text="Processing....." runat="server" Font-Bold="true"></asp:Label>
                                <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="payroll" />
                                <asp:Label ID="lbltemp" runat="server"></asp:Label>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="butAttendanceProcess" />
            <asp:PostBackTrigger ControlID="ddlDept" />
            <asp:PostBackTrigger ControlID="txtMonthYear" />

        </Triggers>
    </asp:UpdatePanel>
    <%-- <center>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" align="center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                            Processing Attendance .........................................
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        </table>
    </center>--%>
    <div class="col-md-12">
        <div class="text-center text-bold">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="imgmoney" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                    Processing Attendance ...............
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greate than 0 and less than 13");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                }
            }
        }
    </script>

</asp:Content>
