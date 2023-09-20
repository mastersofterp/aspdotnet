<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkScholarshipAdjustment.aspx.cs" Inherits="ACADEMIC_BulkScholarshipAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime1"
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

    <asp:UpdatePanel ID="updtime1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Bulk Scholarship Adjustment </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlBulkStud" runat="server" Visible="true">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <%-- <sup>* </sup>--%>
                                                <%-- <label>Admission Batch</label>--%>
                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                ToolTip="Please Select Admission Batch">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Academic Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" AppendDataBoundItems="true" TabIndex="2" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select  Academic Year" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Institute Name</label>--%>
                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                            </div>

                                            <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                AutoPostBack="True" ToolTip="Please Select Institute">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvColg" runat="server" ControlToValidate="ddlColg"
                                                Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Degree</label>--%>
                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="3"
                                                ToolTip="Please Select Degree" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <%--<label>Branch</label>--%>
                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" TabIndex="4"
                                                ToolTip="Please Select Branch" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblYearMandatory" runat="server" Style="color: red" Visible="false">*</asp:Label>
                                                <%--lblDYddlYear--%>
                                                <asp:Label ID="lblDYddlYear" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Year" CssClass="form-control" data-select2-enable="true" TabIndex="5" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--  <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divSchltype" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Scholarship Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScholarShipsType" runat="server" AutoPostBack="true" AppendDataBoundItems="True" CssClass="form-control" OnSelectedIndexChanged="ddlScholarShipsType_SelectedIndexChanged" data-select2-enable="true"
                                                ToolTip="Please Select ScholarShip Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlScholarShipsType" runat="server" ControlToValidate="ddlScholarShipsType"
                                                Display="None" ErrorMessage="Please Select Scholarship Type " InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <%--<label>Semester</label>--%>
                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Semester" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="6" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReceipt" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" ToolTip="Please Select Receipt Type" TabIndex="7" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReceipt" runat="server" ControlToValidate="ddlReceipt"
                                                Display="None" ErrorMessage="Please Select Receipt Type " InitialValue="0" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">

                                                <label>Bank Name</label>
                                            </div>
                                            <%--<asp:TextBox ID="txtBankName" runat="server" TabIndex="8" MaxLength="64"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlBankName" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Bank" TabIndex="8" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server">
                                            <div class="label-dynamic">
                                                <label>Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="dvcal2" runat="server" class="fa fa-calendar text-green"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="Show" TabIndex="9"
                                                    ToolTip="Please Enter To Date" CssClass="form-control" Style="z-index: 0;" />
                                                <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                <ajaxToolkit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="dvcal2" OnClientDateSelectionChanged="checkDate" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" SetFocusOnError="True"
                                                    ErrorMessage="Please Enter To Date" ControlToValidate="txtToDate" Display="None"
                                                    ValidationGroup="Report" />
                                                <ajaxToolkit:MaskedEditExtender ID="meeToDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolkit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeToDate"
                                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Report" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Date of Issue</label>
                                            </div>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtDateofissue" TabIndex="10" CssClass="form-control" ToolTip="Please Enter Date"></asp:TextBox>
                                                <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                                <ajaxToolkit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateofissue" PopupButtonID="imgDate" />
                                                <ajaxToolkit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtDateofissue"
                                                    Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                    MaskType="Date" />
                                                <ajaxToolkit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Date of Issue"
                                                    ControlExtender="meExamDate" ControlToValidate="txtDateofissue" IsValidEmpty="false"
                                                    InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Date of Issue"
                                                    InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateofissue"
                                                Display="None" ErrorMessage="Please Select/Enter Date" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rbRegEx" runat="server" RepeatDirection="Horizontal" TabIndex="11" AutoPostBack="true">
                                                <asp:ListItem Value="0" Selected="True">&nbsp;Regular Student &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="1">&nbsp;Ex Student</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show Student" TabIndex="12"
                                        ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" OnClick="btnShow_Click" CssClass="btn btn-primary" />

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="13"
                                        ValidationGroup="show" CssClass="btn btn-primary" OnClick="btnSubmit_Click" Enabled="false" OnClientClick="return validateAssign();" CausesValidation="false" />

                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="14" CssClass="btn btn-primary"
                                        ToolTip="Send Card By Email" ValidationGroup="show" Visible="false" />

                                    <asp:Button ID="btnPrintReport" runat="server" Text="Admit Card" TabIndex="999" CssClass="btn btn-info"
                                        ToolTip="Print Card under Selected Criteria." ValidationGroup="show" Visible="false" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="15" OnClick="btnCancel_Click"
                                        ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-warning" />

                                    <asp:HiddenField ID="hftot" runat="server" />
                                    <asp:HiddenField ID="txtTotStud" runat="server" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvStudentRecords" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudent">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this)" ToolTip="Select or Deselect All Records" Visible="true" />
                                                            </th>
                                                            <th>
                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>
                                                            <%--   <th>Roll No.</th>--%>
                                                            <th>Student Name</th>
                                                            <th>
                                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                            </th>

                                                            <th>Scholarship Applied Amount</th>
                                                            <th>Scholarship Due Amount</th>
                                                            <th>Scholarship Paid Amount</th>
                                                            <th>Scholarship Excess Amount</th>
                                                            <th>Adjusted Amount</th>
                                                            <th>DD Number/TransactionID</th>
                                                            <th>Category</th>
                                                            <th>Batch</th>
                                                            <%-- <th style="display: none">Student Email </th>--%>
                                                            <th>Scholarship Type</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>

                                            <ItemTemplate>
                                                <%--  <asp:UpdatePanel runat="server" ID="updList">
                                                <ContentTemplate>--%>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" onclick="totStudents(this);" ToolTip='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <%--  <td><%# Eval("ROLLNO")%></td>--%>

                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                     
                                                    </td>

                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>                                                    
                                                    </td>

                                                    <td>
                                                        <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>' ToolTip='<%# Eval("SEMESTERNO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--<%# Eval("SCHL_AMOUNT") %>--%>
                                                        <%-- <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                        <asp:Label ID="lblschamt" runat="server" Text='<%# Eval("SCHL_AMOUNT") %>' ToolTip='<%# Eval("SEMESTERNO") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblAdjSchAmt" runat="server" Text='<%# Eval("DUEAMOUNT") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSchAmt" runat="server" Value='<%# Eval("DUEAMOUNT") %>' />
                                                    </td>

                                                    <td>
                                                        <asp:TextBox ID="txtAdjAmount" runat="server" onkeyup="return IsNumeric(this);" MaxLength="9"></asp:TextBox>
                                                        <%-- onchange="return CalculateValue(this);"--%>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblRemainingAmt" runat="server" Text='<%#Eval("SCHL_EXCESS_AMOUNT") %> ' />
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblschajmt" runat="server" Text='<%#Eval("SCH_ADJ_AMT") %> '></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtDDNumber" runat="server"></asp:TextBox></td>
                                                    <%-- Text='<%#Eval("TRANSACTIONID") %>'--%>
                                                    <%--<td style="display: none">--%>
                                                    <%--<%# Eval("EMAILID_INS")%>
                                                        <asp:HiddenField ID="Hdfemail" runat="server" Value='<%# Eval("EMAILID_INS") %>' />--%>
                                                    <%--</td>--%>

                                                    <td><%# Eval("CATEGORY") %></td>
                                                    <td><%# Eval("BATCHNAME") %></td>


                                                    <td>
                                                        <asp:Label ID="lblDDLData" runat="server" Text='<%# Eval("SCHOLARSHIP_ID") %>' ToolTip='<%# Eval("LONGNAME") %>' Visible="false"></asp:Label>
                                                        <%#Eval("SCHOLORSHIPNAME") %>
                                                    </td>

                                                </tr>

                                                <asp:HiddenField ID="hdfBranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("STUDNAME") %>' ToolTip='<%# Eval("ENROLLNO") %>' Visible="false"></asp:Label>
                                                <%--<asp:HiddenField ID="hiddenBranch" runat="server" Value='<%#Eval ("BRANCHNO") %>' />--%>
                                                <asp:HiddenField ID="hfdyearname" runat="server" Value='<%# Eval("YEAR") %>' />
                                                <%--</ContentTemplate>
                                                <Triggers>
                                                        <asp:PostBackTrigger ControlID="rdoYes" />
                                                        <asp:PostBackTrigger ControlID="rdoNo" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>



                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                                <div id="divMsg" runat="server">
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lvStudentRecords" />
            <asp:PostBackTrigger ControlID="btnSubmit" />

        </Triggers>
        <%--<Triggers>   
            <asp:AsyncPostBackTrigger ControlID="btnPrintReport" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
               <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <%--<script language="javascript" type="text/javascript">
        function check()
        {
            var a = document.getElementsByName("chkIdentityCard")
            var j=0
            for(i=0;i<=a.length;i++)
            {
                if(a[i].checked==true)
                {
                    j=j+1;
                }
            }
            if (j==0)
            {
                alert("please select checkbox")
            }
        } 
    </script>
    --%>

    <script type="text/javascript">
        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;

            }
            else
                if (confirm("Are you sure to adjust scholarship for selected students?"))
                    return true;
            return false;

        }


        function confirmSearch() {
            if (confirm("Are you sure to adjust scholarship for selected students?"))
                return true;
            return false;

        }
        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }


        function IsNumeric(txt) {
            var ValidChars = ".0123456789";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
            }
            return num;
        }



        function CalculateValue(txt) {
            //var tr = obj.parentNode.parentNode //get the entered row object.
            //var txt1 = tr.cells[2].getElementByTagName("SPAN")[0]  //Schlorship Amount
            //var txt2 = tr.cells[3].getElementByTagName("INPUT")[0]  //Adjustment Amount textbox
            //var lbl  = tr.cells[4].getElementByTagName("INPUT")[0]
            //lbl.value = parseInt(txt2.value) - parseInt(txt3.value); //total value calculation.       
            //alert("cal");
            //var isValid1 = $("#lvStudentRecords input[type=label]:value")
            //var dd = $('#lblschamt').val;
            //var hv = $('[id$="hdfSchAmt"]').val();
            //alert(hv);
            try {
                var id = "" + txt.id + "";
                var myarray = new Array();
                myarray = id.split("_");
                var index = myarray[3];
                var valuelbl = document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_" + index + "_hdfSchAmt").value;
                var txtvalue = document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_" + index + "_txtAdjAmount").value;


                document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_" + index + "_lblRemainingAmt").innerText = txtvalue != "" ? (parseFloat(valuelbl) - parseFloat(txtvalue)) : "0";
                txt.focus();

                //if (parseFloat(txtvalue) > parseFloat(valuelbl))
                //{
                //    alert("Adjustment Amount should be less than or equal to Scholarship Amount Enter Valid Amount")
                //    txt.value = '';
                //    txt.focus();
                //}
                //else
                //{
                //    document.getElementById("ctl00_ContentPlaceHolder1_lvStudentRecords_" + index + "_lblRemainingAmt").innerText = txtvalue != "" ? (parseFloat(valuelbl) - parseFloat(txtvalue)) : "0";
                //    txt.focus();
                //}

            }
            catch (err) {
                alert(err);
            }


        }


    </script>

    <script type="text/javascript">
        function checkDate(sender, args) {
            // I change the < operator to >
            if (sender._selectedDate > new Date()) {
                alert("Unable to select future date !!!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value('')
            }

        }
    </script>
</asp:Content>
