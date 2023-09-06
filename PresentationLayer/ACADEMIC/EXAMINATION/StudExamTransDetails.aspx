<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudExamTransDetails.aspx.cs" Inherits="ACADEMIC_EXAMINATION_StudExamTransDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
     <script type="text/javascript" language="javascript">
         function IsNumeric(textbox) {
             if (textbox != null && textbox.value != "") {
                 if (isNaN(textbox.value)) {
                     document.getElementById(textbox.id).value = 0;
                 }
             }
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

     <script type="text/javascript">
         function ToUpper(ctrl) {

             var t = ctrl.value;

             ctrl.value = t.toUpperCase();

         }
    </script>
    
     <%-- <div>
        <asp:UpdateProgress ID="updDepart" runat="server" AssociatedUpdatePanelID="updexamtrans"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        &nbsp;<div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updexamtrans" runat="server">
        <ContentTemplate>--%>
            <div class="col-sm-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title"><b>STUDENT EXAM TRANSACTION DETAILS</b></h3>

                    </div>

                    <div class="box-tools pull-right">
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                    </div>


                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <%--  Personal Details--%>
                                <div class="col-sm-12" id="Divpersonaldetails" runat="server" visible="true">

                                    <div class="sub-heading">
                                        <h5>Personal Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Student Name :</b><a class="pull-right">
                                                        <asp:Label ID="lblstudname" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:HiddenField ID="hfidno" runat="server" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>PRN No. :</b><a class="pull-right">
                                                        <asp:Label ID="lblregistrationnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Degree :</b><a class="pull-right">
                                                        <asp:Label ID="lbldegrees" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>

                                                <li class="list-group-item">
                                                    <b>Email ID :</b><a class="pull-right">
                                                        <asp:Label ID="lblemail" runat="server" Font-Bold="True"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Enrollment No. :</b><a class="pull-right">
                                                        <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item">
                                                    <b>Semester :</b><a class="pull-right">
                                                        <asp:Label ID="lblsemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                    <asp:HiddenField ID="hfsemno" runat="server" />
                                                </li>


                                                <li class="list-group-item">
                                                    <b>Branch :</b><a class="pull-right">
                                                        <asp:Label ID="lblbranchs" runat="server" Font-Bold="true"></asp:Label>
                                                        <asp:HiddenField ID="hfbranchnos" runat="server" />
                                                    </a>
                                                </li>

                                                <li class="list-group-item">
                                                    <b>Mobile No. :</b><a class="pull-right">
                                                        <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                                                        <asp:HiddenField ID="hdmobileno" runat="server" />
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>


                    <%-- Transaction Details--%>
                    <div class="box-body">
                        <div class="col-12" runat="server" id="Transdetails" visible="true">
                            <div class="sub-heading">
                                <h5>Transaction Details </h5>
                            </div>
                             <div class="box-tools pull-right">
                        <div style="color: Red; font-weight: bold">
                            Note : jpg,png,jpeg,pdf file required and file size upto 512 Kb only.
                        </div>
                    </div>

                            <div class="col-md-12">
                                <div class="row">


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>
                                                <sup>* </sup>Session :
                                            </label>
                                        </div>
                                        <asp:DropDownList runat="server" ID="ddlSession" TabIndex="1" Width="100%" AppendDataBoundItems="true"
                                            ToolTip="Please Select Session." CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Session."
                                            ControlToValidate="ddlsession" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="regsubmit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>
                                                <sup>* </sup>Transaction ID :
                                            </label>
                                        </div>

                                        <asp:TextBox runat="server" ID="txttranid" TabIndex="2" Width="100%" ToolTip="Please Enter Transaction ID" CssClass="form-control"
                                            Placeholder="Please Enter Transaction ID" MaxLength="13" onkeyup="ToUpper(this)" onkeypress="return isNumber(event)" OnTextChanged="txttranid_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                                TabIndex="3" ToolTip="Please Enter Transaction Date" placeholder="DD/MM/YYYY" Enabled="false" OnTextChanged="txttransdate_TextChanged" AutoPostBack="true" />
                                            <ajaxToolKit:CalendarExtender ID="StartDate" runat="server" Format="dd/MM/yyyy" Enabled="false"
                                                TargetControlID="txttransdate" PopupButtonID="imgStartDate" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txttransdate"
                                                Display="None" ErrorMessage="Please Enter Transaction Date" SetFocusOnError="True"
                                                ValidationGroup="regsubmit" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txttransdate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" Enabled="false" />
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
                                       <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txttransamount"
                                            ValidChars="1234567890" />--%>
                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txttransamount"
                                            InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Transaction Amount."
                                            ControlToValidate="txttransamount" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label><sup>* </sup>Document :</label>
                                        </div>

                                        <asp:FileUpload ID="fuUpload" runat="server" CssClass="form-control" ToolTip="Select file to upload" accept=".png,.jpg,.jpeg,.png,.pdf"
                                            Width="100%" TabIndex="5"/>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="DivDocument" visible="false">
                                        <div class="label-dynamic">
                                            <label>Preview Document :</label>
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

                                        <asp:LinkButton runat="server" TabIndex="6" ID="btnDownloadFile" OnClick="btnDownloadFile_Click" style="display:none;"></asp:LinkButton>
                                    </div>

                                    <%--<div class="form-group col-lg-1 col-md-6 col-12"></div>--%>
                                    <div class="form-group col-lg-5 col-md-6 col-12" runat="server" id="DivNote" visible="true">

                                        <div class="panel panel-info">
                                            <div class="panel-heading p-2">Bank Details</div>
                                            <div class="panel-body" style="margin-left:10px; margin-top:5px;">
                                                <span style="color: red; font-size: small"><b> Account Number : 012001100000643</b></span>
                                                <br />
                                                <span style="color: red; font-size: small"><b> IFSC Code : HCBL0000112</b></span>
                                                <br />
                                                <span style="color: red; font-size: small"><b> Bank Name : Hasti Bank</b></span>
                                                <br />
                                                <span style="color: red; font-size: small"><b> Account Name : R. C. Patel Institute of Technology</b></span>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                              <div class="col-12" runat="server" id="DivRegCourse" visible="true">
                            <div class="sub-heading">
                                <h5>Registered Courses </h5>
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvCurrentSubjects" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblCurrentSubjects">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Course Code
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Subject Type
                                                        </th>
                                                        <th>Credits
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
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>


                        <br />

                        <%-- Admin Approval Details--%>
                        <div class="col-12" runat="server" id="DivAdminapproval" visible="true">
                            <div class="sub-heading">
                                <h5>Approval Details </h5>
                            </div>

                            <div class="col-md-12">
                                <div class="row">


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
                            </div>
                        </div>
                        <br />
                        <div class="box-footer" runat="server" id="DivSubmit">
                            <p class="text-center">

                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="regsubmit"
                                    TabIndex="9" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="10" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="regsubmit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                        </div>
                    </div>
                </div>

            </div>


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

       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="lnkTransDoc" />
            <asp:PostBackTrigger ControlID="btnDownloadFile" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>


