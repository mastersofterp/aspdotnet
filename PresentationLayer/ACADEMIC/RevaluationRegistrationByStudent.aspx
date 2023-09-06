<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RevaluationRegistrationByStudent.aspx.cs" Inherits="ACADEMIC_RevaluationRegistrationByStudent" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
    </script>
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
                                <h3 class="box-title">REVALUATION & PHOTOCOPY REGISTRATION</h3>
                            </div>

                            <div class="box-body">
                                <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                                    <b>Note : </b>Steps to follow for Revaluation & Photocopy Registration.
                                     <div style="padding-left: 20px; padding-right: 20px">
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             1. Please click Proceed to revaluation Registration button.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             2. Revaluation & Photocopy process student can select subjects for Photocopy, Revaluation & Both..
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             3. Only theory paper will be consider in revaluation & Photocopy process.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             4. Revaluation fees per subject is Rs.
                                             <asp:Label runat="server" ID="lblrevalfees"></asp:Label>/- & for Photocopy is Rs.
                                             <asp:Label runat="server" ID="lblphotofees"></asp:Label>/-.
                                     
                                     <%--</p>
                                            <p style="padding-top: 5px; padding-bottom: 5px;">
                                         5. Photocopy fees per subject is Rs. 200/-
                                     </p>--%>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             5. Student can simultaneously apply the revaluation & photocopy both at a time by paying fees Rs.
                                             <asp:Label runat="server" ID="totalfees"></asp:Label>/-.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             6. Finally Click the Submit Button.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             7. After declaration of result Revaluation & Photocopy Registration activity open till next 3 days only.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px;">
                                             8. Student can apply for Revaluation & Photocopy Registration only once, it can not modify once the registration is submitted.
                                         </p>
                                         <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                                             <asp:Button ID="btnProceed" runat="server" Text="Proceed to Revaluation & Photocopy Registration" OnClick="btnProceed_Click" CssClass="btn btn-success" />
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
                                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Font-Bold="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" SetFocusOnError="True" InitialValue="0" ErrorMessage="Please Select Session Name" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3">
                                                    <label><sup>* </sup>PRN No. :</label>
                                                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" placeholder="Enter PRN No." />

                                                </div>

                                                <div class="col-md-6" style="margin-top: 19px;">
                                                    <asp:Button ID="btnShowstud" runat="server" Text="Show"
                                                        Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-primary"
                                                        OnClick="btnShowstud_Click" />
                                                    <asp:Button ID="btnreport" runat="server" Text="Registration Status Report"
                                                        Font-Bold="true" ValidationGroup="report" CssClass="btn btn-success"
                                                        OnClick="btnreport_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"
                                                        Font-Bold="true" OnClick="btnCancel_Click" />
                                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                                        Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="report" />
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
                                                        <b>Semester :</b><a class="pull-right">
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                        <asp:HiddenField ID="hfsemno" runat="server" />
                                                    </li>
                                                    <li class="list-group-item">
                                                        <b>Admission Batch :</b><a class="pull-right">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
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
                                            <div class="col-md-5" style="margin-top: 15px">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="label-dynamic">
                                                            <label><sup>* </sup>Apply For Semester :</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBackLogSem" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="True" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                            Display="None" SetFocusOnError="True" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="ShowCourse"></asp:RequiredFieldValidator>

                                                        <asp:HiddenField ID="hdfCategory" runat="server" />

                                                        <asp:HiddenField ID="hdfDegreeno" runat="server" />

                                                    </div>

                                                    <div class="col-md-6" runat="server" id="Totalamt" visible="false">
                                                        <div class="label-dynamic">
                                                            <label>Total Amount :</label>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txttotalamt" CssClass="form-control" Text="0" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-7">
                                                <div class=" note-div" style="margin-top: 18px">
                                                    <h5 class="heading">Note </h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Form is provisionally submitted, confirmation will be done after paying Fees & submitting fee paid details.</span>  </p>
                                                    <h5 class="heading">Bank Details </h5>
                                                    <p>
                                                        <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Account Number : 012001100000643</span> <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">IFSC Code : HCBL0000112</span>

                                                    <p>
                                                        <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Bank Name : Hasti Bank</span> <i class="fa fa-star" aria-hidden="true"></i><span style="color: red; font-weight: bold">Account Name : R. C. Patel Institute of Technology</span>
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
                                                                <sup>* </sup>Transaction ID :
                                                            </label>
                                                        </div>

                                                        <asp:TextBox runat="server" ID="txttranid" TabIndex="2" Width="100%" ToolTip="Please Enter Transaction ID" CssClass="form-control"
                                                            Placeholder="Please Enter Transaction ID" MaxLength="20" onkeyup="ToUpper(this)"></asp:TextBox>
                                                        <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txttranid"
                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890" />--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txttranid"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
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
                                                                TabIndex="3" ToolTip="Please Enter Transaction Date" placeholder="DD/MM/YYYY" OnTextChanged="txttransdate_TextChanged" AutoPostBack="true" />
                                                            <ajaxToolKit:CalendarExtender ID="StartDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txttransdate" PopupButtonID="imgStartDate" />
                                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txttransdate"
                                                                Display="None" ErrorMessage="Please Enter Transaction Date" SetFocusOnError="True"
                                                                ValidationGroup="regsubmit" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                                TargetControlID="txttransdate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                                ControlToValidate="txttransdate" EmptyValueMessage="Please Enter Date"
                                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="regsubmit" SetFocusOnError="True" />

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
                                        TabIndex="9" OnClick="btnShow_Click1" CssClass="btn btn-primary" />
                                    <asp:Button ID="bntCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="10" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="9" Text="Submit" ToolTip="Submit" ValidationGroup="regsubmit" OnClientClick="return showLockConfirm(this,'val');" />
                                    <asp:Button ID="btntransactiondetails" runat="server" CssClass="btn btn-primary" OnClick="btntransactiondetails_Click" TabIndex="10" Text="Upload Transaction Details" ToolTip="Transaction Details" ValidationGroup="regsubmit" Visible="false" />
                                    <asp:Button ID="btnprintpayslip" runat="server" CssClass="btn btn-info" OnClick="btnprintpayslip_Click" Text="Print" Visible="false" TabIndex="11" />
                                    <asp:Button ID="btnPrintRegSlip" runat="server" CssClass="btn btn-info" OnClick="btnPrintRegSlip_Click" Text="Registration Slip" Visible="false" TabIndex="12" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="regsubmit" />

                                    <p>
                                        &nbsp;<asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowCourse" />
                                    </p>


                            </div>



                            <div id="DivRegCourse" runat="server" class="col-12" visible="false">
                                <%--  <div class="sub-heading">
                                            <h5>Revaluation & photocopy Courses List </h5>
                                        </div>--%>
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <asp:ListView ID="lvFailCourse" runat="server">
                                        <LayoutTemplate>
                                            <%-- <div id="listViewGrid">--%>
                                            <div class="sub-heading">
                                                <h5>Revaluation &amp; photocopy Courses List</h5>
                                            </div>
                                            <h4></h4>
                                            <%-- <table id="tblCurrentSubjects" class="table table-striped table-bordered nowrap display" style="width: 100%;">--%>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%--<th style="width: 5%">
                                                            <asp:CheckBox ID="ckhall" runat="server" onclick="totAll(this)" />Select All
                                                        </th>--%>
                                                        <th>Subject/s </th>
                                                        <th>Course Code </th>
                                                        <th>Credit </th>
                                                        <%-- <th>CA Max Mark </th>--%>
                                                        <th>CA Mark (35) </th>
                                                        <%-- <th>ESE Max Mark </th>--%>
                                                        <th>ESE Mark (65) </th>
                                                        <th>Total (100) </th>
                                                        <th>Grade </th>
                                                        <th>GP </th>
                                                        <th>Revaluation </th>
                                                        <th>Photocopy </th>
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
                                                <%--<td style="width: 5%">
                                                    <asp:CheckBox ID="chkstatus" runat="server" />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                                <%--   <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("INTER_OBT") %>' />
                                                            </td>--%>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("INTERMARK") %>' />
                                                </td>
                                                <%--   <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("EXTER_OBT") %>' />
                                                            </td>--%>
                                                <td>
                                                    <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("EXTERMARK") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("MARKTOT") %>' />
                                                </td>

                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("GRADE") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("GDPOINT") %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_reval" runat="server" Checked='<%# Eval("REVAL_FLAG").ToString()=="1" ? true:false %>' class="chk_ab" ToolTip='<%# Eval("COURSENO")%>'
                                                        OnCheckedChanged="chk_reval_CheckedChanged" AutoPostBack="true" />
                                                    <asp:HiddenField runat="server" ID="hdfreval" />
                                                    <%-- <asp:Label ID="lblreval" runat="server" Text="500"></asp:Label>--%>

                                                            </center>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chk_photocopy" runat="server" Checked='<%# Eval("PHOTO_FLAG").ToString()=="1" ? true:false %>' class="chk_uf" ToolTip='<%# Eval("COURSENO")%>'
                                                        OnCheckedChanged="chk_photocopy_CheckedChanged" AutoPostBack="true" />
                                                    <asp:HiddenField runat="server" ID="hdfphotocopy" />
                                                    </center>
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






        function showLockConfirm(btn, group) {
            debugger;
            var validate = false;
            if (Page_ClientValidate(group)) {

                validate = ValidatorOnSubmit();
            }
            if (validate)    //show div only if page is valid and ready to submit
            {
                //var ret = confirm('Do you really want to lock marks for selected exam?\n\nOnce Locked it cannot be modified or changed.');
                var ret = true;
                if (ret == true) {
                    var ret2 = confirm('You are about to submit Revalution/Photocopy Details, be sure before locking.\n\nOnce Submitted it cannot be modified or changed. \n\nClick OK to Continue....');
                    if (ret2 == true) {
                        validate = true;
                    }
                    else
                        validate = false;
                }
                else
                    validate = false;
            }
            var counter = 60;
            myVar = setInterval(function () {
                if (counter >= 0) {
                    document.getElementById("keep").innerHTML = "Your Popup will be close in " + counter + " Sec";
                }
                if (counter == 0) {
                    $("#myModal33").hide();
                    $(".modal-backdrop").removeClass("in");
                    $(".modal-backdrop").hide();
                }
                counter--;
                return false;
            }, 1000)

            return validate;

        }






        function SelectAll(headchk) {
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

    <%--<script type="text/javascript" language="javascript">

         //============================ CODE FOR LIVE SERVER ENVIRONMENT =======================//
         function check(chk) {
             //debugger;
             //var chkboxid = chk.id;

             //var last2 = chkboxid.slice(-2);

             //if (last2 >= 10) {
             //    last2 = '_' + last2;
             //}

             //alert(last2);
             var TotalAmount = document.getElementById('ContentPlaceHolder1_txttotalamt');

             var chkReval = document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl0_chk_reval');
             alert(chkReval);
             var hdfrevalamt = document.getElementById('ctl00$ContentPlaceHolder1$lvFailCourse$ctrl0$hdfreval').valueOf;
             alert(hdfrevalamt);
             var chkphoto = id = ocument.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl0_chk_photocopy');
             var hdfphotoamt =  document.getElementById('ctl00$ContentPlaceHolder1$lvFailCourse$ctrl0$hdfphotocopy');
             var siblnk = chk.nextSibling;

             if (chk.checked == true) {
                 if (document.getElementById('' + chkReval + '').checked == true) {
                      alert('hiiii');
                     // alert(document.getElementById('' + chkAccept + ''));
                     TotalAmount.value = Number(hdfrevalamt.value);
                 }
                 else {
                     hdnttotal.value = Number(hdfrevalamt.value) + 1;
                 }
                 document.getElementById('' + chkReval + '').checked = "true";
                 TotalAmount.value = Number(hdfrevalamt.value) + Number(hdfphotoamt.value);
             }
             //else {
             //    if (document.getElementById('' + chkTheory + '').checked || document.getElementById('' + chkPractical + '').checked) {
             //        document.getElementById('' + chkAccept + '').checked = "true";
             //        document.getElementById('' + hfdExmReg + '').value = "1";
             //        TotalSubjectAmount.value = Number(TotalSubjectAmount.value) - Number(siblnk.innerHTML);
             //        TotalAmount.value = Number(TotalSubjectAmount.value) + Number(TotalExamRegistationAmount.value);
             //    }
             //    else {
             //        document.getElementById('' + chkAccept + '').checked = false;
             //        document.getElementById('' + hfdExmReg + '').value = "0";
             //        hdnttotal.value = Number(hdnttotal.value) - 1;
             //        TotalSubjectAmount.value = Number(TotalSubjectAmount.value) - Number(siblnk.innerHTML);
             //        TotalAmount.value = Number(TotalSubjectAmount.value) + Number(TotalExamRegistationAmount.value);
             //    }
             //}
             //totAddSubjects(chk);
         }

         </script>--%>
</asp:Content>

