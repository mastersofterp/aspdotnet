<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="pay_lock_salary.aspx.cs" Inherits="PayRoll_pay_lock_salary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LOCK/UNLOCK SALARY</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-md-12">

                                <div id="PnlLockPermantely" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Lock Salary Permanently</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Month &amp; Year</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMonthYear" runat="server" AutoPostBack="true" TabIndex="1" ToolTip="Enter Month & Year" CssClass="form-control" onblur="return checkdate(this);"></asp:TextBox>

                                                    <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                        Display="None" ErrorMessage="Please Select Month &amp; Year in (MM/YYYY Format)"
                                                        SetFocusOnError="True" ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                   <%-- <sup>* </sup>--%>
                                                    <label>College</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="2" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                               <%-- <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                    ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                    InitialValue="0" Display="None"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                    TabIndex="3" ToolTip="Select Staff" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlStaff" runat="server" ControlToValidate="ddlStaff"
                                                    Display="None" ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="payroll"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Salary Given/Deposided Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDepDate" runat="server" Style="z-index: 0" TabIndex="4" ToolTip="Enter Salary Given/Deposided Date" CssClass="form-control" />

                                                    <asp:RequiredFieldValidator ID="rfvtxtDepDate" runat="server" ControlToValidate="txtDepDate"
                                                        Display="None" ErrorMessage="Please Select Salary Given/Deposided Date in (mm/dd/yyyy Format)"
                                                        ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDepDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                        PopupPosition="BottomLeft">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" TargetControlID="txtDepDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevtxtDepDate" runat="server" ControlExtender="metxtDepDate"
                                                        ControlToValidate="txtDepDate" EmptyValueMessage="Please Enter Inc.Date" InvalidValueMessage="Salary Given/Deposided Date is Invalid (Enter mm/dd/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Salary Given/Deposided Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="payroll" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="butLockSalaryPermanently" TabIndex="5" runat="server" Text="Lock Salary Permanently"
                                            CssClass="btn btn-primary" ToolTip="Click to Lock Salary Permanently" OnClick="butLockSalaryPermanently_Click" ValidationGroup="payroll" />
                                        <asp:Button ID="butBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="6" OnClick="butBack_Click" CausesValidation="false" ToolTip="Click to go to Previous" />
                                        <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                                            DisplayMode="List" ValidationGroup="payroll" />
                                        <asp:HiddenField ID="hidMonYear" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlLockUnlock" runat="server">
                                <div class="form-group col-lg-8 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>1) To Unlock salary please enter 'N' in Lock textbox and click on UnlockSalary button.</span></p>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>2) To Lock Salary permanently click on LockSalary button.</span></p>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Select College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollegeFilter" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                TabIndex="1" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCollegeFilter_SelectedIndexChanged1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvLockUnlock" runat="server" >
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found To Lock/Unlock Salary" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Lock/Unlock Salary</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Month & Year
                                                        </th>
                                                        <th>Staff
                                                        </th>
                                                        <th>Salary Processed
                                                        </th>
                                                        <th>Lock
                                                          <%-- <asp:TextBox ID="txtYesNo1" runat="server" MaxLength="1"  OnTextChanged="txtYesNo1_TextChanged" AutoPostBack="true"
                                                       CssClass="form-control" TabIndex="2" onkeyup="return check(this);" />--%>
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("MONYEAR")%>
                                                </td>
                                                <td>
                                                    <%#Eval("STAFF")%>
                                                </td>
                                                <td>
                                                    <%#Eval("REPROCESS")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtYesNo" runat="server" MaxLength="1" Text='<%#Eval("STATUS")%>'
                                                        ToolTip='<%#Eval("SALNO")%>' CssClass="form-control" TabIndex="2" onkeyup="return check(this);" />
                                                    <asp:RequiredFieldValidator ID="rfvYesNo" runat="server" ControlToValidate="txtYesNo"
                                                        Display="None" ErrorMessage="Please Enter Y Or N" ValidationGroup="payroll1" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnLockSalary" runat="server" Text="Lock Salary" TabIndex="2" CssClass="btn btn-primary" OnClick="btnLockSalary_Click" />
                                    <asp:Button ID="btnSave" runat="server" Text="Unlock Salary" ValidationGroup="payroll1"
                                        CssClass="btn btn-primary" TabIndex="3" OnClick="btnSub_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll1"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </div>

                    </div>

                </div>
            </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="butLockSalaryPermanently" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
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
                //alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
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
