<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Re_ExamRegistration.aspx.cs" 
    Inherits="ACADEMIC_EXAMINATION_Re_ExamRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <style>
        .panel-info > .panel-heading {
            color: #31708f;
            background-color: #c8d8e3;
            border-color: #c8d8e3;
        }

        .panel-info {
            border: 2px solid #c8d8e3;
            height: 125px;
            width: 475px;
            margin-left: 20px;
        }
        .text-center {
            margin-left: 120px;
        }
    </style>

    <asp:Panel ID="pnlStart" runat="server">

        <div>
            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
                DynamicLayout="true" DisplayAfter="0">
                <ProgressTemplate>
                    <div id="preloader">
                        <div id="loader-img">
                            <div id="loader"></div>
                            <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <asp:UpdatePanel ID="updLists" runat="server">
            <ContentTemplate>

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">RE EXAM REGISTRATION</h3>
                            </div>

                            <div class="box-body">
                                <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                                    <b>Note : </b>Steps To Follow For Re Exam Registration.
                                     <div style="padding-left: 20px; padding-right: 20px">
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             1. Please click Proceed to Re Exam Registration button.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             2. Please verify the Re Exam Subjects Listed.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             3. Re Exam fees per subject is Rs.
                                             <asp:Label runat="server" ID="lblreExamfees"></asp:Label>/-.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             4. Finally Click the Submit Button and fill the Transaction Details.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             5. You will get your Registration Receipt After Successfully Transaction Details.
                                         </p>
                                          <p style="padding-top: 5px; padding-bottom: 5px;">
                                             6. Student can apply for RE EXAM Registration only once, it can not modify once the registration is submitted.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                             <asp:Button ID="btnProceed" runat="server" Text="Proceed to Re Exam Registration" OnClick="btnProceed_Click" CssClass="btn btn-success" />
                                         </p>
                                     </div>
                                </div>

                                <div id="divStud" class="col-12" runat="server" visible="false">

                                    <fieldset class="fieldset">
                                        <%--  <legend class="legend">Revaluation Registration</legend>--%>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <label><sup>* </sup>Session Name :</label>
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="true" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" SetFocusOnError="True" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="report"></asp:RequiredFieldValidator>


                                                </div>
                                                <div class="col-md-3" style="display:none;">
                                                    <label><sup>* </sup>PRN No. :</label>
                                                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" placeholder="Enter PRN No." />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter PRN No."
                                                        ControlToValidate="txttranid" Display="None" SetFocusOnError="true" ValidationGroup="Show" />
                                                </div>

                                                 <div class="col-md-6" style="margin-top: 19px;">                                          
                                                     <asp:Button ID="btnreport" runat="server" Text="Registration Status Report" 
                                                        Font-Bold="true" ValidationGroup="report" CssClass="btn btn-success"
                                                        OnClick="btnreport_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="report" />
                                                     </div>


                                                <div class="col-md-6" style="margin-top: 19px; display:none;">
                                                    <asp:Button ID="btnShowstud" runat="server" Text="Show"
                                                        Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-primary"
                                                        OnClick="btnShowstud_Click" />
                                                   
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                        Font-Bold="true" OnClick="btnCancel_Click" />
                                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                                        Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Show" />
                                                 
                                                </div>
                                            </div>
                                    </fieldset>
                                </div>
                            </div>

                            <div class="col-12" id="divCourses" runat="server" visible="false">
                                <div class="row">
                                    <%--  Personal Details--%>
                                    <div class="col-12" id="Divpersonaldetails" runat="server" visible="true">

                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-5">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <b>Student Name :</b><a class="pull-right">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                                            <asp:HiddenField ID="hfidno" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Reg/PRN No. :</b><a class="pull-right">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>

                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Current Semester :</b><a class="pull-right">
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                        <asp:HiddenField ID="hfsemno" runat="server" />
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Admission Batch :</b><a class="pull-right">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>

                                                    <li class="list-group-item">
                                                        <asp:Label>
                                                            <sup style="color:red;font-weight:bold">*</sup> <b>Semester</b>:
                                                        </asp:Label>
                                                          <asp:DropDownList runat="server" ID="ddlsemester"  Width="100%" AppendDataBoundItems="true"
                                                            ToolTip="Please Select Semester" CssClass="form-control" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged" AutoPostBack="true">
                                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                              
                                                          </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Semester."
                                                            ControlToValidate="ddlsemester" Display="None" InitialValue="0" SetFocusOnError="true"
                                                            ValidationGroup="ShowCourse" />                                               

                                                    </li>


                                                </ul>
                                            </div>

                                            <div class="col-md-7">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item">
                                                        <a class="pull-right">
                                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                                                                  <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />

                                                        </a>

                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Degree/Branch :</b><a class="pull-right">
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item">
                                                        <b>Scheme :</b><a class="pull-right">
                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                                        </a>
                                                    </li>

                                                    <li class="list-group-item">
                                                        <b>Email/Mobile No. :</b><a class="pull-right">
                                                            <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                                                            <asp:HiddenField ID="hdmobileno" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3" runat="server" id="Totalamt" visible="false" style="display:none;">
                                                <div class="label-dynamic">
                                                    <label>Total Amount :</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txttotalamt" CssClass="form-control" Text="0" Enabled="false"></asp:TextBox>
                                            </div>


                                            <div class="col-md-5">
                                                <div class=" note-div" style="margin-top: 18px">
                                                     <h5 class="heading">Note </h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Re Exam fees per subject is Rs. 500.00/-.</span>  </p>
                                                </div>
                                            </div>
                                             <div class="col-md-7">
                                                <div class=" note-div" style="margin-top: 18px">
                                                    <h5 class="heading">Bank Details </h5>
                                                    <p>
                                                        <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Account Number : 012001100000643 </span> &nbsp;<i class="fa fa-star" aria-hidden="true">&nbsp; </i><span style="color: red; font-weight: bold">IFSC Code : HCBL0000112</span>

                                                    <p>
                                                        <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Bank Name : Hasti Bank</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-star" aria-hidden="true">&nbsp; </i><span style="color: red; font-weight: bold">Account Name : R. C. Patel Institute of Technology</span>
                                                    </p>

                                                </div>
                                            </div>




                                        </div>

                                    <div class="row" runat="server" id="TransactionDiv" visible="false">
                                        <div class="col-12" runat="server" id="Transdetails" visible="true">
                                            <div class="sub-heading">
                                                <h5>Transaction Details </h5>
                                            </div>
                                            <div class="box-tools pull-left">
                                                <div style="color: Red; font-weight: bold">
                                                    Note : jpg,png,jpeg,pdf file required and file size upto 512 Kb only.
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>
                                                            <sup>* </sup>Transaction ID(UTR/UPI) :
                                                        </label>
                                                    </div>

                                                    <%--added by prafull on dt 07102022--%>
                                                    <asp:TextBox runat="server" ID="txttranid" TabIndex="2" Width="100%" ToolTip="Please Enter Transaction ID" CssClass="form-control" oncopy="return false" onpaste="return false"
                                                        Placeholder="Please Enter Transaction ID" MaxLength="13" onkeyup="ToUpper(this)" OnTextChanged="txttranid_TextChanged" onkeypress="return isNumber(event)"></asp:TextBox>
                                                    <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txttranid"
                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />--%>

                                                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txttranid"
                                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                                   <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txttranid"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Transaction ID."
                                                        ControlToValidate="txttranid" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label><sup>* </sup>Transaction Date :</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon" id="imgDOB" runat="server">
                                                            <i class="fa fa-calendar" id="imgStartDate" runat="server" style="z-index: 10"></i>
                                                        </div>
                                                        <asp:TextBox ID="txttransdate" runat="server" ValidationGroup="submit" CssClass="form-control" 
                                                            TabIndex="3" ToolTip="Please Enter Transaction Date" placeholder="DD/MM/YYYY" Enable="false" OnTextChanged="txttransdate_TextChanged" AutoPostBack="true" />
                                                        <ajaxToolKit:CalendarExtender ID="StartDate" runat="server" Format="dd/MM/yyyy" Enabled="false"
                                                            TargetControlID="txttransdate" PopupButtonID="imgStartDate"  />
                                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txttransdate"
                                                            Display="None" ErrorMessage="Please Enter Transaction Date" SetFocusOnError="True" 
                                                            ValidationGroup="regsubmit" />
                                                        <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                            TargetControlID="txttransdate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" Enabled="false"  />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                            ControlToValidate="txttransdate" EmptyValueMessage="Please Enter Date"
                                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="regsubmit" SetFocusOnError="True" Enabled="false" />

                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label><sup>* </sup>Transaction Amount :</label>
                                                    </div>
                                                    <asp:TextBox runat="server" ID="txttransamount" TabIndex="4" Width="100%" ToolTip="Please Enter Transaction Amount."
                                                        CssClass="form-control" placeholder="Please Enter Transaction Amount" MaxLength="10" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Transaction Amount."
                                                        ControlToValidate="txttransamount" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DocUploadDiv">
                                                    <div class="label-dynamic">
                                                        <label><sup>* </sup>Transaction Receipt :</label>
                                                    </div>

                                                    <asp:FileUpload ID="fuUpload" runat="server" CssClass="form-control" ToolTip="Select file to upload" accept=".png,.jpg,.jpeg,.png,.pdf"
                                                        Width="100%" TabIndex="5" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDocument" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>Download Receipt :</label>
                                                    </div>

                                                    <asp:UpdatePanel runat="server" ID="updpreview">
                                                        <ContentTemplate>
                                                            <asp:Button ID="lnkTransDoc" runat="server" OnClick="lnkTransDoc_Click" Text="Preview" CssClass="btn btn-success" data-toggle="modal" data-target="#preview" TabIndex="6"></asp:Button>
                                                            <asp:HiddenField runat="server" ID="hftransdoc" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnkTransDoc" EventName="click" />
                                                            <%--  <asp:PostBackTrigger ControlID="lnkTransDoc" />--%>
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                    <asp:LinkButton runat="server" TabIndex="6" ID="btnDownloadFile" OnClick="btnDownloadFile_Click" Style="display: none;"></asp:LinkButton>
                                                </div>
                                            </div>



                                        </div>
                                    </div>


                                    <%-- Admin Approval Details--%>

                                    <div class="row" runat="server" id="DivAdminapproval" visible="false">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Approval Details </h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Divstatus">
                                            <div class="label-dynamic">
                                                <label>
                                                    <sup>* </sup>Approval Status :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlstatus" TabIndex="7" Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Approval Status" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Approve</asp:ListItem>
                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Approval Status."
                                                ControlToValidate="ddlstatus" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="regsubmit" />
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    <sup>* </sup>Remark :
                                                </label>
                                            </div>

                                            <asp:TextBox runat="server" ID="txtremark" TabIndex="8" Width="100%" ToolTip="Please Enter Remark" CssClass="form-control"
                                                Placeholder="Please Enter Remark" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Remark."
                                                ControlToValidate="txtremark" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                        </div>




                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label ID="lblErrorMsg" runat="server" Style="color: red; font-size: medium; font-weight: bold;" Text="">
                                        </asp:Label>
                                    </div>
                                    </div>
                                </div>
                            </div>

                            <div class="box-footer" runat="server" id="DivSubmit" visible="false">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" ToolTip="Submit" ValidationGroup="ShowCourse"
                                        TabIndex="9" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="bntCancel1" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="10" OnClick="bntCancel1_Click" CssClass="btn btn-danger" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="11" Text="Submit" ToolTip="Submit" ValidationGroup="regsubmit" />
                                    <asp:Button ID="btntransactiondetails" runat="server" CssClass="btn btn-primary" OnClick="btntransactiondetails_Click" TabIndex="12" Text="Upload Transaction Details" ToolTip="Transaction Details" ValidationGroup="regsubmit" Visible="false" />
                                    <asp:Button ID="btnprintpayslip" runat="server" CssClass="btn btn-info" OnClick="btnprintpayslip_Click" Text="Print" Visible="false" TabIndex="13" />
                                    <asp:Button ID="btnPrintRegSlip" runat="server" CssClass="btn btn-info" OnClick="btnPrintRegSlip_Click" Text="Registration Slip" Visible="false" TabIndex="14" />
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-danger" OnClick="btnBack_Click" Text="Back" Visible="false" TabIndex="13" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="regsubmit" />

                                    <p>
                                        &nbsp;<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowCourse" />
                                    </p>


                            </div>

                            <%--added by prafull--%>
                            <%--  <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Divsem" visible="true">
                                            <div class="label-dynamic">
                                                <label>
                                                    <sup>* </sup>Semester :
                                                </label>
                                            </div>
                                            <asp:DropDownList runat="server" ID="ddlsemester"  Width="100%" AppendDataBoundItems="true"
                                                ToolTip="Please Select Semester" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                              
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Semester."
                                                ControlToValidate="ddlsemester" Display="None" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="regsubmit" />
                                        </div>--%>

                            <div id="DivRegCourse" runat="server" class="col-12" visible="false">
                                <%--  <div class="sub-heading">
                                            <h5>Revaluation & photocopy Courses List </h5>
                                        </div>--%>
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <asp:ListView ID="lvFailCourse" runat="server">
                                        <LayoutTemplate>
                                            <%-- <div id="listViewGrid">--%>
                                            <div class="sub-heading">
                                                <h5>Re Exam Course List</h5>
                                            </div>
                                            <table id="tblCurrentSubjects" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />
                                                         Select </th>
                                                        <th>Subject/s</th>
                                                        <th>Course Code</th>
                                                        <th>Subject Type</th>
                                                        <th>Semester</th>
                                                        <th>Credit</th>
                                                        <th>CA Mark</th>
                                                        <th>ESE Mark</th>
                                                        <th>Total</th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            <%-- </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td>
                                                    <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' Checked='<%# Eval("STATUS").ToString()=="1" ? true:false %>' />
                                                      <asp:HiddenField runat="server" ID="hdfretest" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsubid" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("INTERMARK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("EXTERMARK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("MARKTOT") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>



                            <div id="DivReExamReglist" runat="server" class="col-12" visible="false">
                            
                                <asp:Panel ID="pnlreglist" runat="server">
                                    <asp:ListView ID="lvlstudlist" runat="server">
                                        <LayoutTemplate>
                                            <%-- <div id="listViewGrid">--%>
                                            <div class="sub-heading">
                                                <h5>Re Exam Registerd Student List</h5>
                                            </div>
                                            <table id="tblCurrentSubjects" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                         <th style="width: 10%">PRN No.
                                                        </th>
                                                        <th style="width: 20%">Name
                                                        </th>
                                                        <th style="width: 5%">Gender
                                                        </th>
                                                        <th style="width: 5%">Sem
                                                        </th>
                                                        <th style="width: 10%">Degree
                                                        </th>
                                                        <th style="width: 10%">Branch
                                                        </th>
                                                        <th style="width: 10%">Trans. ID
                                                        </th>
                                                        <th style="width: 10%">Amount
                                                        </th>
                                                        <th style="width: 10%">Trans. Date 
                                                        </th>
                                                        <th style="width: 10%">Status
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                            <%-- </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                
                                                <td style="width: 10%">
                                                    <u style="color: #3c8dbc">
                                                    <asp:LinkButton runat="server" ID="lnkrollno" Text='<%# Eval("REGNO") %>' CommandName='<%#Eval("IDNO") %>'
                                                        OnClick="lnkrollno_Click" ToolTip="Click Here"></asp:LinkButton></u>
                                                </td>
                                                <td style="width: 20%">
                                                <%# Eval("STUDNAME") %>
                                            </td>
                                            <td style="width: 5%">
                                                <%# Eval("GENDER") %>
                                            </td>
                                            <td style="width: 5%">
                                                <%# Eval("SEMESTERNAME") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("DEGREENAME") %>
                                            </td>
                                                 <td style="width: 10%">
                                                <%# Eval("LONGNAME") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANSACTION_NO") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANSACTION_AMT") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("TRANS_DATE") %>
                                            </td>
                                            <td style="width: 10%">
                                                <%# Eval("STATUS") %>
                                                <asp:HiddenField ID="HdnStatus" runat="server" Value='<%#Eval("APPROVAL_STATUS") %>' />
                                            </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
                </div>
               
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
                <asp:PostBackTrigger ControlID="btnprintpayslip" />
                <asp:PostBackTrigger ControlID="btntransactiondetails" />
                <asp:PostBackTrigger ControlID="btnreport" />
            </Triggers>
        </asp:UpdatePanel>


    </asp:Panel>

    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content" style="width: 700px;">
                        <div class="modal-header">
                            <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Document</h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">

                                <asp:Literal ID="ltEmbed" runat="server" />

                                <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        var count = e.checked.count;
                        e.checked = true;
                    }
                    else
                        e.checked = false;
                }
            }
        }


        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }

    </script>

</asp:Content>
