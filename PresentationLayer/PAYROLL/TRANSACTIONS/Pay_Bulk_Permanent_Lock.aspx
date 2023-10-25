<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Bulk_Permanent_Lock.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Bulk_Permanent_Lock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK PERMENANT LOCK SALARY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">

                                <div id="PnlLockPermantely" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Bulk Permanent Lock salary</h5>
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
                                                    <%--     <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtMonthYear"
                                                        Mask="99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtMonthYear"  InvalidValueMessage="Salary month Date is Invalid (Enter mm/dd/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Salary Month Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="payroll" SetFocusOnError="True" />--%>
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
                                                   <%-- <sup>* </sup>--%>
                                                    <label>Salary Given/Deposited Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDepDate" runat="server" Style="z-index: 0" TabIndex="4" ToolTip="Enter Salary Given/Deposited Date" CssClass="form-control" />

                                                    <%--   <asp:RequiredFieldValidator ID="rfvtxtDepDate" runat="server" ControlToValidate="txtDepDate"
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
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="payroll" SetFocusOnError="True" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" style="display: none">

                                        <asp:Button ID="butLockSalaryPermanently" TabIndex="5" runat="server" Text="Lock Salary Permanently"
                                            CssClass="btn btn-primary" ToolTip="Click to Lock Salary Permanently" OnClick="butLockSalaryPermanently_Click" />
                                        <asp:Button ID="butBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="6" OnClick="butBack_Click" CausesValidation="false" ToolTip="Click to go to Previous" />
                                        <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                                            DisplayMode="List" ValidationGroup="payroll" />
                                        <asp:HiddenField ID="hidMonYear" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlLockUnlock" runat="server">
                                <%-- <div class="form-group col-lg-8 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>1) To Unlock salary please enter 'N' in Lock textbox and click on UnlockSalary button.</span></p>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>2) To Lock Salary permanently click on LockSalary button.</span></p>
                                    </div>
                                </div>--%>
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
                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                     <sup>* </sup>
                                                    <label>Salary Given/Deposited Date</label>
                                                </div>
                                                <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtEditFieldDT" runat="server" MaxLength="100" CssClass="form-control" 
                                                             OnTextChanged="txtEditFieldDT_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtEditFieldDT" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtEditFieldDT"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                                            ControlToValidate="txtEditFieldDT" EmptyValueMessage="Please Enter Birth Date"
                                                            InvalidValueMessage="BirthDate is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Birth Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="emp" SetFocusOnError="True" />
                                                    </div>
                                            </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mt-4" style="display: none">
                                    <asp:Button ID="btnLockSalary" runat="server" Text="Lock Salary" TabIndex="2" CssClass="btn btn-primary" OnClick="btnLockSalary_Click" />

                                    <asp:Button ID="btnSave" runat="server" Text="Unlock Salary" ValidationGroup="payroll1"
                                        CssClass="btn btn-primary" TabIndex="3" OnClick="btnSub_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll1"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12 btn-footer mt-4">
                                    <asp:Button ID="btnPermnentLock" runat="server" Text="Lock Salary" TabIndex="2" CssClass="btn btn-primary" OnClick="butLockSalaryPermanentlynew_Click" ValidationGroup="payroll"/>

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ValidationGroup="payroll1"
                                        CssClass="btn btn-primary" TabIndex="3" OnClick="butBacknew_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="payroll"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <asp:ListView ID="lvLockUnlock" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" CssClass="text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Salary list</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap displaysd" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Check All &nbsp;&nbsp;&nbsp;
                                                            <asp:CheckBox runat="server" onclick="totAllSubjects(this)" Checked="false" ID="chkall" />
                                                        </th>

                                                        <th>Month & Year
                                                        </th>
                                                        <th>Staff
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                        <%-- <th>Salary Processed
                                                        </th>--%>
                                                        <th>Salary Given/Deposited Date
                                                          
                                                 
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
                                                    <center><asp:CheckBox runat="server" ID="Chkitem" /> </center>
                                                </td>

                                                <td>
                                                    <%#Eval("MONYEAR")%>
                                                </td>
                                                <td>
                                                    <%#Eval("STAFF")%>
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>
                                                </td>
                                                <%--  <td>
                                                    <%#Eval("REPROCESS")%>
                                                </td>--%>
                                                <td>

                                                    <asp:HiddenField runat="server" ID="hdnSalNo" Value='<%#Eval("SALNO")%>' />
                                                    <asp:HiddenField runat="server" ID="hdnStaffno" Value='<%#Eval("STAFFNO")%>' />
                                                    <asp:HiddenField runat="server" ID="hdnCollegeNo" Value='<%#Eval("COLLEGE_ID")%>' />
                                                    <asp:HiddenField runat="server" ID="hdnMonthYear" Value='<%#Eval("MONYEAR")%>' />
                                                    <asp:TextBox ID="txtEditFieldDT" runat="server" CssClass="form-control ui"> </asp:TextBox>


<%--                                                    <asp:RequiredFieldValidator ID="rfvtxtDepDate1" runat="server" ControlToValidate="txtEditFieldDT"
                                                        Display="None" ErrorMessage="Please Select Salary Given/Deposited Date in (mm/dd/yyyy Format)"
                                                        ValidationGroup="payroll" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtEditFieldDT" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                        PopupPosition="BottomLeft">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate1" runat="server" TargetControlID="txtEditFieldDT"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevtxtDepDate1" runat="server" ControlExtender="metxtDepDate1"
                                                        ControlToValidate="txtEditFieldDT" EmptyValueMessage="Please Enter Inc.Date" InvalidValueMessage="Salary Given/Deposited Date is Invalid (Enter mm/dd/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Salary Given/Deposited Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="payroll" SetFocusOnError="True" />
                                                </td>

                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
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
        function check(text1) {
            //  alert('gu');
            // $(".ui").val($("#ctl00_ContentPlaceHolder1_lvLockUnlock_txtEditFieldDT1").val());
            //for (i = 0; i < document.forms[0].elements.length; i++) {
            //    var s1 = document.getElementById("txtEditFieldDT1").innerHTML;
            //   // var inputValue1 = document.getElementById("txtEditFieldDT1").value;
            //    alert(s1);
            //    var inputValue =  document.getElementById("ctl00_ContentPlaceHolder1_lvLockUnlock_ctrl" + i + "_txtEditFieldDT").value;
            //    alert(inputValue);
            //    }

            //document.getElementById("ctl00_ContentPlaceHolder1_txtAmount").value = count;

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'text') {
                    //if (headchk.in == true)
                    //    e.checked = true;
                    //else
                    //    e.checked = false;
               

                    e.innerHTML = text1.innerHTML;

                }
            }

        }


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">
        //function check(me) {

        //    if (document.getElementById("" + me.id + "").value != "Y" && document.getElementById("" + me.id + "").value != "N") {
        //        alert("Please Enter Y Or N ");
        //        document.getElementById("" + me.id + "").value = "";
        //        document.getElementById("" + me.id + "").focus();
        //    }

        //}


        //function checkdate(input) {
        //    var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
        //    var returnval = false
        //    if (!validformat.test(input.value)) {
        //        alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
        //        document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
        //        document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
        //    }
        //    else {
        //        var monthfield = input.value.split("/")[0]

        //        if (monthfield > 12 || monthfield <= 0) {
        //            alert("Month Should be greate than 0 and less than 13");
        //            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
        //            document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
        //        }
        //    }
        //}



    </script>

</asp:Content>


