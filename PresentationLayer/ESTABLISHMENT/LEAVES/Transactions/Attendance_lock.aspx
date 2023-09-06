<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Attendance_lock.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Attendance_lock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ATTENDANCE LOCK UNLOCK</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="PnlLockPermantely" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Lock Attendance Permanently</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Month Year</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="4"
                                                    onblur="return checkdate(this);" data-inputmask="'mask': '99/9999'" ToolTip="Enter Month & Year" Style="z-index: 0;">
                                                </asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MMMyyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please Select Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                    ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>                                              

                                            </div>
                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Month &amp; Year </label>
                                            </div>
                                            <div class='input-group date' id="myDatepicker1">
                                                <div class="input-group-addon" id="ImaCalStartDate" runat="server">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtMonthYear" runat="server"
                                                    ToolTip="Enter date in MM/yyyy format"
                                                    CssClass="form-control datepickerinput" data-inputmask="'mask': '99/9999'" onchange="return checkdate(this);" TabIndex="3"></asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please Enter Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                    ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>College Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                                ToolTip="Select College Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select College Name " ValidationGroup="payroll"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Staff Type :</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstaff" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Staff-Type" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlstaff"
                                                Display="None" ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdep" visible="false">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldeptLock" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select Department" TabIndex="5">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddldeptLock" runat="server" ControlToValidate="ddldeptLock"
                                                Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="payroll">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlLockUnlock" runat="server">
                                <div class="form-group col-lg-8 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note (Please Select)</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>1) To Unlock Attendance please enter 'N' in Lock textbox and click on Unlock Attendance button.</span></p>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>2) To Lock Attendance permanently click on Lock Attendance button.</span></p>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvLockUnlock" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found To Lock/Unlock Salary" />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Lock/Unlock Attendance List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Month & Year
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                        <th>Staff Type
                                                        </th>
                                                        <th>Attendance Processed
                                                        </th>
                                                        <th>Lock
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
                                                    <%#Eval("MONYEAR")%>
                                                </td>
                                                <td>
                                                    <%#Eval("COLLEGE_NAME")%>
                                                </td>
                                                <td>
                                                    <%#Eval("STAFFTYPE")%>
                                                </td>
                                                <td>
                                                    <%#Eval("REPROCESS")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtYesNo" runat="server" MaxLength="1" Text='<%#Eval("STATUS")%>'
                                                        ToolTip='<%#Eval("ATTENDNO")%>' Width="40px" TabIndex="1" onkeyup="return check(this);" />
                                                    <asp:RequiredFieldValidator ID="rfvYesNo" runat="server" ControlToValidate="txtYesNo"
                                                        Display="None" ErrorMessage="Please Enter Y Or N" ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnLockAttendance" runat="server" Text="Lock Attendance" CssClass="btn btn-primary" TabIndex="2"
                                    OnClick="btnLockAttendance_Click" ToolTip="Click here to Lock Attendance" />
                                <asp:Button ID="btnSave" runat="server" Text="Unlock Attendance" ValidationGroup="payroll" TabIndex="3"
                                    CssClass="btn btn-primary" ToolTip="Click here to Unlock Attendance" OnClick="btnSub_Click" />
                                <asp:Button ID="butLockAttendancePermanently" runat="server" Text="Lock Attendance Permanently"
                                    CssClass="btn btn-primary" ToolTip="Click here to Lock Attendance Permanently" TabIndex="6"
                                    OnClick="butLockAttendancePermanently_Click" ValidationGroup="payroll" />
                                <asp:Button ID="butBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="7"
                                    OnClick="butBack_Click" ToolTip="Click here to Go Back" />
                                <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                                    DisplayMode="List" ValidationGroup="payroll" />
                                <asp:HiddenField ID="hidMonYear" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>

            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function check(me) {

            if (document.getElementById("" + me.id + "").value != "Y" && document.getElementById("" + me.id + "").value != "N") {
                alert("Please Enter Y Or N ");
                document.getElementById("" + me.id + "").value = "";
                document.getElementById("" + me.id + "").focus();
            }

        }


        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Format")
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

