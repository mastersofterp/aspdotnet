<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_OverRule.aspx.cs" Inherits="PayRoll_Transactions_Pay_OverRule"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">OVERRULE</h3>
                        </div>
                        <div class="box-body">
                                <asp:Panel ID="pnlSupplimentaryBillDetails" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Fees Add/Edit OverRule Details</h5>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Emp.Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmpName" runat="server" TabIndex="1" ToolTip="Select Emp Name" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlEmpName" runat="server" ControlToValidate="ddlEmpName"
                                                    Display="None" ErrorMessage="Please Select Employee Name" ValidationGroup="payroll"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Designation</label>
                                                </div>
                                                <asp:Label ID="lblDesignation" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Appointment</label>
                                                </div>
                                                <asp:Label ID="lblAppointMent" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Active</label>
                                                </div>
                                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" TabIndex="2" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="ImaCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter From Date" onchange="CompareDOJ1()"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Enabled="true" EnableViewState="true" 
                                                        Format="MM/yyyy" PopupButtonID="ImaCalFromDate" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        Display="None" ErrorMessage="Please Select  From Date in(dd/mm/yyyy Format)"
                                                        SetFocusOnError="True" ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                    <%--  <asp:CompareValidator ID="cmpVal1" ControlToCompare="txtFromDate"
                                                            ControlToValidate="txtToDate" Operator="GreaterThanEqual"
                                                            ErrorMessage="From Date Should be Less than  To Date." ValidationGroup="payroll" SetFocusOnError="true" Display="None" runat="server"></asp:CompareValidator>--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Enter To Date"  onchange="CompareDOJ2()"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtToDate" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="MM/yyyy" PopupButtonID="imgToDate" TargetControlID="txtToDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                                        Display="None" ErrorMessage="Please Select  To Date(dd/mm/yyyy Format)" SetFocusOnError="True"
                                                        ValidationGroup="payroll">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12 mt-2">
                                    <asp:Panel ID="pnlEarningHeads" runat="server">
                                        <div class="sub-heading">
                                            <h5>Earning Heads</h5>
                                        </div>
                                        <asp:ListView ID="lvEarningHeads" runat="server">
                                            <EmptyDataTemplate>
                                                <label class="text-center mt-3">No Rows In Earning Heads</label>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>PayHead
                                                            </th>
                                                            <th>Calculation
                                                            </th>
                                                            <th>Days
                                                            </th>
                                                            <th>Amount  </th>
                                                        </tr>
                                                        <thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkEarningHead" TabIndex="4" onchange="checkboxChecked();" onclick="return EarningHeadsChecked(this);" runat="server" Checked="true"
                                                            ToolTip='<%# Eval("PAYHEAD")%>' />
                                                        <%# Eval("PAYSHORT")%>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEarnings" CssClass="form-control" TabIndex="5" runat="server" Enabled="true" onchange="return ddlearningsonchange(this);" data-select2-enable="true">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                            <asp:ListItem Value="1" Selected="True">Actual</asp:ListItem>
                                                            <asp:ListItem Value="2">Double</asp:ListItem>
                                                            <asp:ListItem Value="3">Half</asp:ListItem>
                                                            <asp:ListItem Value="4">1/3</asp:ListItem>
                                                            <asp:ListItem Value="5">Zero</asp:ListItem>
                                                            <asp:ListItem Value="6">Days</asp:ListItem>
                                                            <asp:ListItem Value="7">Amount</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txEHDays" runat="server" MaxLength="4" CssClass="form-control" Enabled="false"
                                                            onkeyup="return ValidateNumeric(this);" TabIndex="6" ToolTip='<%# Eval("PAYHEAD")%>' Text="0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEAmount" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"
                                                            onkeyup="return ValidateNumeric(this);" ToolTip='<%# Eval("PAYHEAD")%>' Text="0"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 mt-4">
                                    <asp:Panel ID="PnlDeductionHeads" runat="server">
                                        <div class="sub-heading">
                                            <h5>Deduction Heads</h5>
                                        </div>
                                        <asp:ListView ID="lvDeductionHeads" runat="server">
                                            <EmptyDataTemplate>
                                                <label>No Rows In Deduction Heads</label>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>PayHead
                                                            </th>
                                                            <th>Calculation
                                                            </th>
                                                            <th>Days
                                                            </th>
                                                            <th>Amount  </th>
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
                                                        <asp:CheckBox ID="chkDeductionHead" runat="server" TabIndex="7" ToolTip='<%# Eval("PAYHEAD")%>'
                                                            onclick="return DeductionHeadsChecked(this);" Checked="true" />
                                                        <%# Eval("PAYSHORT")%>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDeduction" TabIndex="8" CssClass="form-control" onchange="return ddlDeductiononchange(this);" data-select2-enable="true"
                                                            Enabled="true" runat="server">
                                                            <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                            <asp:ListItem Value="1" Selected="True">Actual</asp:ListItem>
                                                            <asp:ListItem Value="2">Double</asp:ListItem>
                                                            <asp:ListItem Value="3">Half</asp:ListItem>
                                                            <asp:ListItem Value="4">1/3</asp:ListItem>
                                                            <asp:ListItem Value="5">Zero</asp:ListItem>
                                                            <asp:ListItem Value="6">Days</asp:ListItem>
                                                            <asp:ListItem Value="7">Amount</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txDHDays" runat="server" TabIndex="9" MaxLength="4" Enabled="false"
                                                            ToolTip='<%# Eval("PAYHEAD")%>' Text="0" onkeyup="return ValidateNumeric(this);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDAmount" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"
                                                            ToolTip='<%# Eval("PAYHEAD")%>' Text="0"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="form-group col-12 mt-3">
                                    <div class="label-dynamic">
									    <sup>* </sup>
									    <label>Remark</label>
								    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="250" TabIndex="10" ToolTip="Enter Remark" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                        Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="payroll" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="butSubmit" runat="server" Text="Submit" ValidationGroup="payroll"
                                        OnClick="butSubmit_Click" CssClass="btn btn-success" TabIndex="11" ToolTip="Click To submit" />
                                    <asp:Button ID="butCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="butCancel_Click" TabIndex="12" ToolTip="Click To Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" id="hidTotalDeductionRecordsCount" runat="server" />
            <input type="hidden" id="hidEarningRecordsCount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function EarningHeadsChecked(me) {
            var checkboxid = me.id;
            var checkboxid_array = checkboxid.split("_");

            var ddlEarnings = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_" + checkboxid_array[3] + "_ddlEarnings");
            var txEHDays = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_" + checkboxid_array[3] + "_txEHDays");
            var txEHAmount = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + checkboxid_array[3] + "_txtEAmount");

            if (me.checked == true) {
                ddlEarnings.disabled = false;
                ddlEarnings.value = 1;
            }
            else {
                ddlEarnings.disabled = true;
                txEHDays.disabled = true;
                txEHDays.value = 0;
                txEHAmount.disabled = true;
                txEHAmount.value = 0;
                ddlEarnings.value = 0;
            }
        }

        function ddlearningsonchange(me) {
            var ddlEarnings = me.id;
            var ddlEarnings_array = ddlEarnings.split("_");

            var txEHDays = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_" + ddlEarnings_array[3] + "_txEHDays");
            var txEHAmount = document.getElementById("ctl00_ContentPlaceHolder1_lvEarningHeads_" + ddlEarnings_array[3] + "_txtEAmount");

            if (me.value == 6) {
                txEHDays.disabled = false;
                txEHDays.value = 0;
            }
            else {
                txEHDays.disabled = true;
                txEHDays.value = 0;
            }

            if (me.value == 7) {
                txEHAmount.disabled = false;
                txEHAmount.value = 0;
            }
            else {
                txEHAmount.disabled = true;
                txEHAmount.value = 0;
            }
        }



        function DeductionHeadsChecked(me) {
            var checkboxid = me.id;
            var checkboxid_array = checkboxid.split("_");

            var ddlDeduction = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + checkboxid_array[3] + "_ddlDeduction");
            var txDHDays = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + checkboxid_array[3] + "_txDHDays");
            var txDHAmount = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + checkboxid_array[3] + "_txtDAmount");
            if (me.checked == true) {
                ddlDeduction.disabled = false;
                ddlDeduction.value = 1;
            }
            else {

                ddlDeduction.disabled = true;
                ddlDeduction.value = 0;
                txDHDays.disabled = true;
                txDHDays.value = 0;
                txDHAmount.disabled = true;
                txDHAmount.value = 0;
            }
        }

        function ddlDeductiononchange(me) {
            var ddlDeduction = me.id;
            var ddlDeduction_array = ddlDeduction.split("_");

            var txDHDays = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + ddlDeduction_array[3] + "_txDHDays");
            var txDHAmount = document.getElementById("ctl00_ContentPlaceHolder1_lvDeductionHeads_" + ddlDeduction_array[3] + "_txtDAmount");

            if (me.value == 6) {
                txDHDays.disabled = false;
                txDHDays.value = 0;
            }
            else {
                txDHDays.disabled = true;
                txDHDays.value = 0;
            }

            if (me.value == 7) {
                txDHAmount.disabled = false;
                txDHAmount.value = 0;
            }
            else {
                txDHAmount.disabled = true;
                txDHAmount.value = 0;
            }
        }

        //function checkboxChecked(txt) {
        //    if (chkDeductionHead.checked == false) {
        //        ddlEarnings.txt = "Please Select";
        //    }
        //    else {

        //    }
        //}


        function ValidateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = "";
                txt.focus();
                alert("Only Numeric Characters allowed");
                return false;
            }
            else {
                return true;
            }
        }

        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("" + input.id + "").value = "";
                document.getElementById("" + input.id + "").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greater than 0 and less than 13");
                    document.getElementById("" + input.id + "").value = "";
                    document.getElementById("" + input.id + "").focus();
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        function totalHeads(chkcomplaint) {
            var frm = document.forms[0];
            //            var lv = chkcomplaint.parentNode.parentNode;
            //            var ListView = lv.parentNode;
            //            var inputList = ListView.getElementsByTagName("input");
            //            alert(ListView)
            //            for (i = 0; i < inputList.length; i++) {
            //                var e = inputList[i];
            ////                alert(inputList.length)
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true) {
                        e.checked = true;
                        //                        EarningHeadsChecked(e);
                    }
                    else {
                        e.checked = false;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function CompareDOJ1() {
            debugger
           
            var fdate = document.getElementById('<%=txtFromDate.ClientID%>');
            var edate = document.getElementById('<%=txtToDate.ClientID%>');

            var fdate1 = document.getElementById('<%=txtFromDate.ClientID%>').value;
            var edate1 = document.getElementById('<%=txtToDate.ClientID%>').value;
            if (document.getElementById('<%=txtToDate.ClientID%>').value != "") {
                if (fdate1 > edate1) {
                    alert('From Date should be less than To Date');
                    document.getElementById('<%=txtFromDate.ClientID%>').value = ""
                    return false;
                }
            }
            
           
        }
    </script>
     <script type="text/javascript" language="javascript">
         function CompareDOJ2() {
                debugger
                var fdate = document.getElementById('<%=txtFromDate.ClientID%>');
                var edate = document.getElementById('<%=txtToDate.ClientID%>');
             if (document.getElementById('<%=txtFromDate.ClientID%>').value == "") {
                 alert('Enter From date Value');
                 document.getElementById('<%=txtToDate.ClientID%>').value = "";
                 return false;
             }
             else {
                 var fdate1 = document.getElementById('<%=txtFromDate.ClientID%>').value;
                 var edate1 = document.getElementById('<%=txtToDate.ClientID%>').value;
                 if (edate1 < fdate1) {
                     alert('To date should be grater than From date');

                     document.getElementById('<%=txtToDate.ClientID%>').value = "";
                     return false;
                 }
             }
        }
    </script>
</asp:Content>
