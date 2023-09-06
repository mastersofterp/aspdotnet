<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LoanApplicableStudent.aspx.cs" Inherits="LoanApplicableStudentList" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlStud .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLoan"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updLoan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">Loan Applicable Student List</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Year</label>
                                            <asp:Label ID="lblDYtxtAcdemicYear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlacademicYear" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Academic Year" AutoPostBack="True" OnSelectedIndexChanged="ddlacademicYear_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="rfvacdYear" runat="server" ControlToValidate="ddlacademicYear"
                                            Display="None" SetFocusOnError="true" InitialValue="0" ErrorMessage="Please Select Academic Year"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Please Select Degree" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlyear" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlyear" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" data-select2-enable="true"
                                            ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlyear" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </div>



                                </div>
                            </div>

                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnShow" runat="server" ValidationGroup="show" Text="Show"
                                    CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8"
                                    CssClass="btn btn-primary" CausesValidation="false" Visible="false" OnClick="btnSubmit_Click" ValidationGroup="submit" />

                                <asp:Button ID="btnPrintReport" runat="server" Text="Report(Excel)" TabIndex="9" CssClass="btn btn-info"
                                    ValidationGroup="show" Visible="false" OnClick="btnPrintReport_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false"
                                    DisplayMode="List" />

                            </div>


                            <div class="col-12">
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                <asp:Panel ID="pnlStud" runat="server">

                                    <asp:ListView ID="lvStudents" runat="server">

                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>List of Students</h5>
                                            </div>
                                            <%--  <table id="tblStudent" class="table table-hover table-bordered table-responsive">--%>
                                            <table class="table table-striped table-bordered nowrap display" id="divadmissionlist">
                                                <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="lstTable">--%>
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: left; padding-left: 3px">
                                                            <%-- <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this)" />--%>
                                                             Select
                                                        </th>
                                                        <th>PRNNO
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Approve Loan Amount
                                                            
                                                        </th>
                                                        <th>DD No/Transaction No
                                                           
                                                        </th>
                                                        <th>Date
                                                        
                                                        </th>
                                                        <th>Bank Details
                                                          
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
                                                    <asp:CheckBox ID="chkloan" runat="server" ToolTip='<%#Eval("IDNO") %>' onclick="CheckSelectionCount(this)" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblregno" runat="server" Text='<%# Eval("PRNNO")%>' ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                    <%--  <%# Eval("PRNNO")%>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Lblname" runat="server" Text='<%# Eval("STUDENTNAME")%>'></asp:Label>
                                                    <%--<%# Eval("YEARNAME")%>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("BRANCH")%>'></asp:Label>
                                                    <%--<%# Eval("YEARNAME")%>--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblyear" runat="server" Text='<%# Eval("YEARNAME")%>'></asp:Label>
                                                    <%--<%# Eval("YEARNAME")%>--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtapproveloan" runat="server" onkeyup="validateNumeric(this);" Text='<%# Eval("APPROVELOANAMOUNT")%>' Enabled="false" MaxLength="9"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvapproveloan" runat="server" SetFocusOnError="True"
                                                        ErrorMessage="Please Enter Approve Loan Amount" ControlToValidate="txtapproveloan"
                                                        Display="None" ValidationGroup="submit" InitialValue="" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtddno" runat="server" Text='<%# Eval("DDNO")%>' Enabled="false" MaxLength="30"></asp:TextBox>

                                                    <%--   onkeyup="validateNumeric(this); commented as per ticket-43459"--%>

                                                    <asp:RequiredFieldValidator ID="rfvddno" runat="server" SetFocusOnError="True"
                                                        ErrorMessage="Please Enter Ddno/Transaction No " ControlToValidate="txtddno"
                                                        Display="None" ValidationGroup="submit" InitialValue="" />
                                                </td>
                                                <td>
                                                    
                                                    <asp:TextBox ID="txtDate" runat="server" type="date" TabIndex="2" CssClass="form-control" Text='<%# Eval("SDATE")%>' Enabled="false"  onblur="validateDate(this);" />
                                                    <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                       TargetControlID="txtDate"   Enabled="true" EnableViewState="true"  >
                                                   </ajaxToolKit:CalendarExtender>--%>
                                                    
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbankdetails" runat="server" Text='<%# Eval("BANKDETAILS")%>' Enabled="false" MaxLength="64" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>



                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintReport" />
        </Triggers>
    </asp:UpdatePanel>


    <script>
        function validateDate(textBox) {
            var currentDate = new Date();
            var selectedDate = new Date(textBox.value);

            if (selectedDate > currentDate) {
                alert("Future Date Is Not Acceptable.");
                textBox.value = ""; // Clear the invalid value
            }
        }
  </script>

    <script>
        function CheckSelectionCount(chk) {

            var str = chk.id;
            var start = str.indexOf("_ctrl") + 5;
            var end = str.indexOf("_chkloan");
            var eindex = str.substring(start, end);
            // alert(eindex);

            var txtapproveloan = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_txtapproveloan");
            var ddno = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_txtddno");
            var bankdetails = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_txtbankdetails");
            var date = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_txtDate");






            if (chk.checked == true) {

                txtapproveloan.disabled = false;
                ddno.disabled = false;
                bankdetails.disabled = false;
                date.disabled = false;

            }
            else {
                txtapproveloan.disabled = true;
                ddno.disabled = true;
                bankdetails.disabled = true;
                date.disabled = true;
            }
        }
    </script>

    <script>
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8)

                return true;
            else
                return false;
        }

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }


        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
            }
        }

        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            var j = 0;
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                debugger;
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (j != 0) {
                            e.checked = true;
                        }
                        j++;
                    }
                    else
                        e.checked = false;
                }
            }

        }
        function Checkedfalse(headchk) {


        }



    </script>


</asp:Content>
