<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModuleConfig.aspx.cs" Inherits="ADMINISTRATION_ModuleConfig" MasterPageFile="~/SiteMasterPage.master"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl_details"
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

    <style>
        #divStudentConfig {
            height: 400px;
            overflow: scroll;
        }

        .bg-light-blue {
            position: sticky;
            top: -1px;
            z-index: 1;
            background-color: #fff!important;
            border: 1px solid #ccc !important;
        }

        #divStudentConfig .switch label {
            width: 80px;
            height: 28px;
        }

        #divStudentConfig .switch input:checked + label:before {
            font-size: 14px;
            padding: 5px 11px;
        }

        #divStudentConfig .switch label:after {
            height: 24.5px;
            width: 8.2px;
            left: 2.5px;
        }

        #divStudentConfig .switch label:before {
            font-size: 14px;
            padding: 5px 11px;
        }
    </style>

    <asp:UpdatePanel ID="updpnl_details" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Module Configuration</a>
                                    </li>
                                    <%-- <li class="nav-item" style="visibility:hidden;">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Faculty Configuration</a>
                                    </li>--%>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Student Related</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="4">Course and Exam Registration Related</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_5" tabindex="5">Attendance Trigger Mail Related</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_6" tabindex="6">Semester Adm. Payment Related</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Registration No</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblRegno" runat="server" Font-Bold="true">Registration Number</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdRegno" name="rdRegno" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="1" data-off="No" for="rdRegno"></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblRollNo" runat="server" Font-Bold="true">Roll Number</asp:Label>
                                                        </div>









                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdRollNo" name="rdRollNo" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="2" data-off="No" for="rdRollNo"></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblEnroll" runat="server" Font-Bold="true">Enrollment Number</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdenroll" name="rdenroll" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="3" data-off="No" for="rdenroll"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related To Semester Admission</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblStudMandate" runat="server" Font-Bold="true">Yes,If Student Information is Mandatory For Semester Admission</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdStudMandate" name="rdStudMandate" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="5" data-off="No" for="rdStudMandate"></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblSemAdmWithPayment" runat="server" Font-Bold="true">No,If Semester Admission without Payment (Only Demand Creation)</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkSemAdmWithPayment" name="chkSemAdmWithPayment" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="6" data-off="No" for="chkSemAdmWithPayment"></label>
                                                        </div>
                                                    </div>
                                                    <div id="dvOnlbtnSemAdm" runat="server" class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblOnlbtnsemadm" runat="server" Font-Bold="true">Yes,If Semester Admission Online Payment Button is Visible.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdonlinepaymentbtn" disabled="disabled" name="rdonlinepaymentbtn" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="6" data-off="No" for="rdonlinepaymentbtn"></label>
                                                        </div>
                                                    </div>

                                                    <div id="dvOffbtnSemAdm" runat="server" class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="true">Yes,If Semester Admission Offline Payment Button is Visible.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdofflinepaymentbtn" disabled="disabled" name="rdofflinepaymentbtn" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="6" data-off="No" for="rdofflinepaymentbtn"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label10" runat="server" Font-Bold="true">Yes,If Allow Semester Admission Before Semester Promotion.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkbeforesempromotion" name="chkbeforesempromotion" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="6" data-off="No" for="chkbeforesempromotion"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label12" runat="server" Font-Bold="true">Yes,If Allow Semester Admission After Semester Promotion.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkafteresempromotion" name="chkafteresempromotion" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="6" data-off="No" for="chkafteresempromotion"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label7" runat="server" Font-Bold="true">Check for Sem Promotion without Criteria Demand Creation.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkdemandcreationsempromo" name="chkdemandcreationsempromo" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkdemandcreationsempromo"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Email</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-3 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true">Select Email Type</asp:Label>
                                                            <asp:DropDownList ID="ddlEmailType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Gmail</asp:ListItem>
                                                                <asp:ListItem Value="2">Send Grid</asp:ListItem>
                                                                <asp:ListItem Value="3">Outlook</asp:ListItem>
                                                                <asp:ListItem Value="4">Amazon</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <%--<div class="switch form-inline">
                                                            <input type="checkbox" id="Checkbox1" name="rdStudMandate" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="5" data-off="No" for="rdStudMandate"></label>
                                                        </div>--%>
                                                    </div>



                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Student</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblstudemail" runat="server" Font-Bold="true">Yes,If Allow to Send Email on New Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chknewstudentemail" name="chknewstudentemail" />
                                                            <label data-on="Yes" tabindex="8" data-off="No" for="chknewstudentemail"></label>
                                                        </div>

                                                        <%--<div class="switch form-inline">
                                        <input type="checkbox" id="rdEmailYes" name="switch" checked />
                                        <label data-on="Yes" data-off="No" for="rdEmailYes"></label>
                                    </div>--%>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblstuduser" runat="server" Font-Bold="true">Yes,If Allow to Create Student User on New Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkcreateusernewstudentry" name="chkcreateusernewstudentry" />
                                                            <label data-on="Yes" tabindex="8" data-off="No" for="chkcreateusernewstudentry"></label>
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label19" runat="server" Font-Bold="true">Yes,If Allow to Check Branch Wise Seat Capacity on New Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkseatcapacitynewstudentry" name="chkseatcapacitynewstudentry" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkseatcapacitynewstudentry"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblprntuser" runat="server" Font-Bold="true">Yes,If Allow to Create Parent User on New Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkcreateusernewprntentry" name="chkcreateusernewprntentry" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkcreateusernewprntentry"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label28" runat="server" Font-Bold="true">Yes,If Allow to Create Registration No. on New Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkCreateRegno" name="chkCreateRegno" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkCreateRegno"></label>
                                                        </div>
                                                    </div>

                                                    <%-- New code Fee Head Groups added by -Gopal M 01112023--%>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label31" runat="server" Font-Bold="true">Yes,If Allow to show Receipt Head Group on Fees Receipt.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkFessHeadGroup" name="chkFessHeadGroup" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkFessHeadGroup"></label>
                                                        </div>
                                                    </div>

                                                    <%-- New code added by -Gopal M 02102023--%>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Outstanding Fee Collection</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblOutstandingFeeCollection" runat="server" Font-Bold="true">Yes,If Allow to Outstanding Fee Collection on Student Entry Page.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkOutstandingFeeCollection" name="chkOutstandingFeeCollection" />
                                                            <label data-on="Yes" tabindex="8" data-off="No" for="chkOutstandingFeeCollection" onclick="ClickOutstandingFeeCollection(this.id);"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server" id="OutstandingMessageDiv" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblOutstandingMessage" runat="server" Font-Bold="true">Outstanding Message</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <asp:TextBox ID="txtOutstandingMessage" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                            <%-- <input type="text" id="txtOutstandingMessage" class="form-control" placeholder="Enter Outstanding Message"  />--%>
                                                        </div>
                                                    </div>


                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Fee Collection</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblRegnocreation" runat="server" Font-Bold="true">Yes,If Allow to Create Registration Number After Fees Collection.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkRegnocreation" name="chkRegnocreation" />
                                                            <label data-on="Yes" tabindex="8" data-off="No" for="chkRegnocreation"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="true">Yes,If Allow to Create User After Fees Collection.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkUserCreationonFee" name="chkUserCreationonFee" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkUserCreationonFee"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true">Check Previous Sem Oustanding Fees.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkoutstandingfees" name="chkoutstandingfees" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkoutstandingfees"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="true">Yes, If Deactive The Student After Reactivation Late Fine</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkStdReactivationfee" name="chkStdReactivationfee" />
                                                            <label data-on="Yes" tabindex="10" data-off="No" for="chkStdReactivationfee"></label>
                                                        </div>
                                                    </div>

                                                    <%-- Show Single, Duplicate Triplicate receipt added by -Gopal M 01112023--%>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label35" runat="server" Font-Bold="true">Yes,If Allow to show Single, Double And Triple Fee Receipt on Fee Collection.</asp:Label>
                                                        </div>
                                                        <div class="form-inline">
                                                            <asp:TextBox ID="txtFeeReceiptCopies" MaxLength="1" runat="server" CssClass="form-control"> </asp:TextBox>
                                                            <label style="color: red">Note: Allows only 1, 2 and 3 numbers</label>
                                                            <asp:RangeValidator ID="rvtxtFeeReceiptCopies" runat="server" ErrorMessage="Enter only in 1, 2 and 3 numbers" ForeColor="Red" ControlToValidate="txtFeeReceiptCopies" ValidationGroup="Submit" MinimumValue="1" MaximumValue="3" Type="Integer" SetFocusOnError="True"></asp:RangeValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftext" runat="server" TargetControlID="txtFeeReceiptCopies" FilterType="Numbers" ValidChars="123" FilterMode="ValidChars">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label32" runat="server" Font-Bold="true">Yes,If Allow to show Scholarship/ Concession Adjustment fees for Student Login.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkScholarshipConAdj" name="chkScholarshipConAdj" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkScholarshipConAdj"></label>
                                                        </div>
                                                    </div>

                                                    <%--  <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label32" runat="server" Font-Bold="true">Yes,If Allow to show Single Fee Receipt on Fee Collection.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkFeeReceiptSingle" name="chkFeeReceiptSingle" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkFeeReceiptSingle"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label33" runat="server" Font-Bold="true">Yes,If Allow to show Duplicate Fee Receipt on Fee Collection.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkFeeReceiptDouble" name="chkFeeReceiptDouble" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkFeeReceiptDouble"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label34" runat="server" Font-Bold="true">Yes,If Allow to show Triplicate Fee Receipt on Fee Collection.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkFeeReceiptTriple" name="chkFeeReceiptTriple" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkFeeReceiptTriple"></label>
                                                        </div>
                                                    </div>--%>





                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Faculty Advisor Approval</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblFacultyAdvisor" runat="server" Font-Bold="true">Yes,If Approved Faculty Advisor After Course Registration.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkFaculyAdvisorApp" name="chkFaculyAdvisorApp" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkFaculyAdvisorApp"></label>
                                                        </div>
                                                    </div>



                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to 3rd Party Integration</h5>
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true">Yes,If Allow Document Verification For Third Party Students.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkAllowDocumentVerification" name="chkAllowDocumentVerification" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkAllowDocumentVerification"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Choice For Elective Course Registration</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblElectiveChoiceFor" runat="server" Font-Bold="true">No,If Elective Choice not as per credit limit.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkElectChoiceFor" name="chkElectChoiceFor" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkElectChoiceFor"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Academic Related</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="true">Yes,Is Trimester.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkallowtrisemester" name="chkallowtrisemester" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chkallowtrisemester"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="true">Yes,If Allow Payment Mail Send on New Student Entry.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chksendpaymentmailstudentry" name="switch" />
                                                            <label data-on="Yes" tabindex="9" data-off="No" for="chksendpaymentmailstudentry"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label17" runat="server" Font-Bold="true">Add BCC for Mail Send on New Student Entry.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <asp:TextBox ID="txtaddBCC" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>


                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label14" runat="server" Font-Bold="true">Yes, If Department Elective Intake Capacity to Check during Course Registration by Student</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkIntakeCapacity" name="chkIntakeCapacity" />
                                                            <label data-on="Yes" tabindex="21" data-off="No" for="chkIntakeCapacity"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label15" runat="server" Font-Bold="true"> Yes, If Course Shortname Display in Time Table Report</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chktimeReport" name="chktimeReport" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chktimeReport"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label16" runat="server" Font-Bold="true"> Yes, If Global Elective Course Teacher Allotment Required</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkGlobalCTAllotment" name="chkGlobalCTAllotment" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkGlobalCTAllotment"></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label27" runat="server" Font-Bold="true"> Yes, If Value Added Course Teacher Allotment Required</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkValueAddedCTAllotment" name="chkValueAddedCTAllotment" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkValueAddedCTAllotment"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to online payment</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label18" runat="server" Font-Bold="true"> Yes, If Allow to Select Hostel Type on Online Payment</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkhosteltypeop" name="chkhosteltypeop" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkhosteltypeop"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Head to Head Adjustment Page</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label20" runat="server" Font-Bold="true">Users Allowed to view Head to Head Adjustment Page.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">

                                                            <asp:ListBox ID="ddluser" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                            <%-- <input type="checkbox" id="Checkbox1" name="chkhosteltypeop" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkhosteltypeop"></label>--%>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Student Dashboard</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label21" runat="server" Font-Bold="true">Allowed Students to view Outsatnding Fees on Dashboard.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkoutstandingdashorad" name="chkoutstandingdashorad" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkoutstandingdashorad"></label>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label25" runat="server" Font-Bold="true">Yes, If disable Student Login Dashboard.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkAllowToDisplayStudLoginDashboard" name="chkAllowToDisplayStudLoginDashboard" />
                                                            <label data-on="Yes" data-off="No" for="chkAllowToDisplayStudLoginDashboard"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Attendance</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label22" runat="server" Font-Bold="true">Select User to Mark Attendace.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">

                                                            <asp:ListBox ID="ddlAttendanceuser" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                                        </div>


                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label23" runat="server" Font-Bold="true">Yes,If Time Slots are Mandatory for Teaching Plan.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkslotmand" name="chkslotmand" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkslotmand"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label33" runat="server" Font-Bold="true">Select to show Attendance in Student's Login</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <asp:Label ID="Label34" runat="server" Font-Bold="true">Session</asp:Label>
                                                            </div>
                                                            <asp:ListBox ID="lstSession" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <asp:Label ID="Label36" runat="server" Font-Bold="true">School/College</asp:Label>
                                                            </div>
                                                            <asp:ListBox ID="lstCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label29" runat="server" Font-Bold="true">Yes,If allow Value added on Attendance / Teaching Plan.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkAttTeaching" name="chkAttTeaching" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkAttTeaching"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Go To User Login Page</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label24" runat="server" Font-Bold="true">Users Allowed to view Go To User Login Page.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <asp:ListBox ID="ddlUserLogin" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to display Receipt</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label26" runat="server" Font-Bold="true">Users Allowed to view Receipt in HTML Format.</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkDisplayReceiptInHTML_Format" name="chkDisplayReceiptInHTML_Format" />
                                                            <label data-on="Yes" tabindex="22" data-off="No" for="chkDisplayReceiptInHTML_Format"></label>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Course Creation</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblCourseCreate" runat="server" Font-Bold="true">Select User Type to View Course Creation Page.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <asp:ListBox ID="ddlCourseUser" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblCourseLock" runat="server" Font-Bold="true">Select User to View Course Lock/Unlock Page.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <asp:ListBox ID="ddlCourseLock" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Redo / Backlog / Improvement Course Registraion</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lbl" runat="server" Font-Bold="true">If, Yes Allow Current Semester for Course Registraion</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="chkRedoImprovementCourseRegFlag" name="chkRedoImprovementCourseRegFlag" />
                                                                <label data-on="Yes" data-off="No" for="chkRedoImprovementCourseRegFlag"></label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related to Modify Admission Info</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label30" runat="server" Font-Bold="true">Select User Type to View Modify Admission Page.</asp:Label>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <asp:ListBox ID="lboModAdmInfo" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                        </div>
                                                    </div>


                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return validate();" TabIndex="6" Text="Submit"
                                                        ValidationGroup="Show" CssClass="btn btn-primary" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="false" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%-- <div class="tab-pane" id="tab_2" style="visibility:hidden;">
                                        Faculty
                                    </div>--%>
                                    <div class="tab-pane" id="tab_3">
                                        <div class="col-12">
                                            <div id="demo-grid">
                                                <div class="sub-heading mt-4">
                                                    <h5>Student Configuration</h5>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Page Name </label>
                                                    </div>
                                                    <asp:UpdatePanel ID="updStudentC" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlPageName" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AutoPostBack="false" onchange="handleDropDownChange();">
                                                                <%--                                                            <asp:DropDownList ID="ddlPageName" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlPageName_SelectedIndexChanged">--%>
                                                                <asp:ListItem Value="0">Add Student</asp:ListItem>
                                                                <asp:ListItem Value="1">Personal Details</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlPageName" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>

                                                <div id="divStudentConfig" class="mt-3">
                                                </div>
                                                <%-- <table class="table table-striped table-bordered nowrap display" id="StudentConfig">
                                                    <thead>
                                                        <tr>
                                                            <th hidden>STUDCONFIG_ID
                                                            </th>
                                                            <th>Caption Name
                                                            </th>
                                                            <th>Field Name
                                                            </th>
                                                            <th style="text-align:center">Is    Active
                                                            </th>
                                                            <th>Is Mandatory
                                                            </th>
                                                            <th hidden>Organization ID
                                                            </th>
                                                            <th hidden>Page No
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>--%>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <input type="button" value="Submit" id="btnStudentSubmit" class="btn btn-primary" runat="server" />
                                            <input type="button" value="Reset" id="btnReset" class="btn btn-warning" />
                                        </div>

                                    </div>



                                    <div class="tab-pane" id="tab_4">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Related To Course & Exam Registration</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <%-- <div class="form-group col-md-12">
                                                            <asp:CheckBox ID="chkSelectCollege" runat="server" TextAlign="Left" OnCheckedChanged="ShowCollege(this)" /> <%-- OnCheckedChanged="ShowCollege(this);" />
                                                              <input type="checkbox" id="Checkbox1" name="rdRegSame" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="4" data-off="No" for="rdRegSame"></label>
                                                            <label>
                                                                Select to Configure on College Level
                                                            </label>
                                                        </div>--%>

                                                        <div class="label-dynamic">
                                                            <asp:Label ID="Label8" runat="server" Font-Bold="true">Do you want to Configure on College Level</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <%--   <asp:checkbox ID="chkSelectCollege" runat="server"  onclick="ShowCollege();" />--%>
                                                            <input type="checkbox" id="chkSelectCollege" name="chkSelectCollege" onclick="ShowCollege();" />
                                                            <label data-on="Yes" class="newAddNew Tab" data-off="No" for="chkSelectCollege"></label>
                                                        </div>

                                                        <div id="dvCollege" runat="server" style="visibility: hidden;" class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="form-group col-md-12">
                                                                <label><span style="color: red;">*</span>College</label>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                                    ToolTip="Please Select College" class="form-control" placeholder="Enter College">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="Label11" runat="server" Font-Bold="true">Yes,If Course and Exam Registration Activity is Same</asp:Label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdRegSame" name="rdRegSame" onclick="return SetStat(this);" />
                                                            <label data-on="Yes" class="newAddNew Tab" tabindex="4" data-off="No" for="rdRegSame"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnCourseExamReg" runat="server" OnClick="btnCourseExamReg_Click" OnClientClick="return validate1();" Text="Submit"
                                                        ValidationGroup="Show" CssClass="btn btn-primary" />
                                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="false" ValidationGroup="Submit" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab_5">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>To Students and Parents</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label><sup>*</sup>CC Email</label>
                                                        <asp:TextBox ID="txtStudCC" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Student and Parents CC Email"
                                                            AutoComplete="off" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Student and Parents CC Email"
                                                            ControlToValidate="txtStudCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />
                                                        <br />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label>BCC Email</label>
                                                        <asp:TextBox ID="txtStudBCC" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Please Enter Student and Parents BCC Email"
                                                            AutoComplete="off" TextMode="MultiLine" />
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Student and Parents BCC Email"
                                                            ControlToValidate="txtStudBCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />--%>
                                                        <br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Daily Faculty Attendance Status</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <label><sup>*</sup>To</label>
                                                        <asp:TextBox ID="txtFacMail" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Please Enter Daily Faculty Attendance Email"
                                                            MaxLength="256" AutoComplete="off" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Daily Faculty Attendance Email"
                                                            ControlToValidate="txtFacMail" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />
                                                        <br />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label><sup>*</sup>CC Email</label>
                                                        <asp:TextBox ID="txtFacCC" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Daily Faculty Attendance CC Email"
                                                            AutoComplete="off" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Daily Faculty Attendance CC Email"
                                                            ControlToValidate="txtFacCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />
                                                        <br />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label>BCC Email</label>
                                                        <asp:TextBox ID="txtFacBCC" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Daily Faculty Attendance BCC Email"
                                                            AutoComplete="off" TextMode="MultiLine" />
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Daily Faculty Attendance BCC Email"
                                                            ControlToValidate="txtFacBCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />--%>
                                                        <br />
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Absent Student Weekly</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-12 col-md-6 col-12">
                                                        <label><sup>*</sup>To</label>
                                                        <asp:TextBox ID="txtAbMail" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Absent Student Email"
                                                            MaxLength="256" AutoComplete="off" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Absent Student Email"
                                                            ControlToValidate="txtAbMail" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />
                                                        <br />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label><sup>*</sup>CC Email</label>
                                                        <asp:TextBox ID="txtAbCC" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Absent Student CC Email"
                                                            AutoComplete="off" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Absent Student CC Email"
                                                            ControlToValidate="txtAbCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />
                                                        <br />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <label>BCC Email</label>
                                                        <asp:TextBox ID="txtAbBCC" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Please Enter Absent Student BCC Email"
                                                            MaxLength="25" AutoComplete="off" TextMode="MultiLine" />
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Absent Student BCC Email"
                                                            ControlToValidate="txtAbBCC" Display="None" SetFocusOnError="true" ValidationGroup="SubmitMail" />--%>
                                                        <br />
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitMail" runat="server" Text="Submit" ValidationGroup="SubmitMail" CssClass="btn btn-primary" TabIndex="9" OnClick="btnSubmitMail_Click" />
                                                    <asp:Button ID="bntCancel" TabIndex="10" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="bntCancel_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="false" ValidationGroup="SubmitMail" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tab_6">
                                        <asp:UpdatePanel ID="updSlot" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Semester Admission Payment Related</h5>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Payment Mode</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPaymentMode" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter Payment Mode" placeholder="Please Enter Payment Mode" MaxLength="60" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                                    TargetControlID="txtPaymentMode" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|&&quot;'" />
                                                                <asp:RequiredFieldValidator ID="rfvPaymentMode" runat="server" ControlToValidate="txtPaymentMode"
                                                                    Display="None" ErrorMessage="Please Enter Payment Mode" ValidationGroup="Academic"
                                                                    SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Acc. Holder Name/ Cheque  & DD should be drawn in favour of </label>
                                                                </div>

                                                                <asp:TextBox ID="txtAccHolderName" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter cc. Holder Name/ Cheque  & DD should be drawn in favour of :" MaxLength="120" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    TargetControlID="txtAccHolderName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|&&quot1234567890;'" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAccHolderName"
                                                                    Display="None" ErrorMessage="Please Enter cc. Holder Name/ Cheque  & DD should be drawn in favour of :" ValidationGroup="Academic"
                                                                    SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Bank Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtBankName" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter Bank Name" MaxLength="60" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                                    TargetControlID="txtBankName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|-&&quot;'" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBankName"
                                                            Display="None" ErrorMessage="Please Enter Bank Name" ValidationGroup="Academic"
                                                            SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Account No.</label>
                                                                </div>
                                                                <asp:TextBox ID="txtAccountNo" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter Account No." MaxLength="60" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                                    TargetControlID="txtAccountNo" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|-&&quot;'" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAccountNo"
                                                            Display="None" ErrorMessage="Please Enter Account No." ValidationGroup="Academic"
                                                            SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>IFSC Code</label>
                                                                </div>
                                                                <asp:TextBox ID="txtifsccode" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter IFSC Code" MaxLength="60" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                                    TargetControlID="txtifsccode" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|-&&quot;'" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtifsccode"
                                                            Display="None" ErrorMessage="Please Enter IFSC Code" ValidationGroup="Academic"
                                                            SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Branch Name/DD payable at </label>
                                                                </div>
                                                                <asp:TextBox ID="txtBranchName" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter Branch /DD payable at :" MaxLength="60" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                                    TargetControlID="txtBranchName" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,.:;<>?'{}[]\|-&&quot;'" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBranchName"
                                                            Display="None" ErrorMessage="Please Enter Branch /DD payable at :" ValidationGroup="Academic"
                                                            SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Cheque Bounce Charges</label>
                                                                </div>
                                                                <asp:TextBox ID="txtBounceCharge" runat="server" TabIndex="0" CssClass="form-control" ToolTip="Please Enter Cheque Bounce Charge" MaxLength="12" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                                    TargetControlID="txtBounceCharge" FilterType="Custom" FilterMode="InvalidChars"
                                                                    InvalidChars="~`!@#$%^*()_+=,:;<>?'{}[]\|-&&quot;'" />
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBounceCharge"
                                                            Display="None" ErrorMessage="Please Enter  Cheque Bounce Charge" ValidationGroup="Academic"
                                                            SetFocusOnError="true">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Upload Bank Challan</label>
                                                                </div>
                                                                <asp:FileUpload ID="Fuslip" runat="server" TabIndex="0" />
                                                                <asp:Label ID="lblC" runat="server" Visible="false"></asp:Label>
                                                                <%--<asp:RequiredFieldValidator ID="rfvpayment" runat="server" ControlToValidate="Fuslip"
                                                            ErrorMessage="Please Upload Payment Slip" Display="None"
                                                            ValidationGroup="Academic">
                                                        </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblActiveStatus" runat="server" Font-Bold="true">Active Status</asp:Label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveStatus" name="rdActiveStatus" checked />
                                                                    <label data-on="Active" data-off="Deactive" for="rdActiveStatus"></label>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmitPaymentMode" runat="server" Text="Submit" TabIndex="0" CssClass="btn btn-primary"
                                                                OnClick="btnSubmitPaymentMode_Click" OnClientClick="return validateSemesterAdm(); return getactivestatus();" />
                                                            <asp:Button ID="btnCancelMode" runat="server" Text="Cancel" TabIndex="0" CssClass="btn btn-warning" OnClick="btnCancelMode_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Academic" ShowMessageBox="true" ShowSummary="false"
                                                                DisplayMode="List" />
                                                            <asp:HiddenField ID="hfdChkActiveStatus" runat="server" ClientIDMode="Static" />
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvPaymentDetails" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Payment Details</h5>
                                                                    </div>
                                                                    <asp:Panel ID="Panel2" runat="server">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Action
                                                                                    </th>
                                                                                    <th>Payment Mode
                                                                                    </th>
                                                                                    <th>Acc. Holder Name
                                                                                    </th>
                                                                                    <th>Bank Name
                                                                                    </th>
                                                                                    <th>Account No
                                                                                    </th>
                                                                                    <th>IFSC Code
                                                                                    </th>
                                                                                    <th>Branch Name
                                                                                    </th>
                                                                                    <th>Bounce Charges
                                                                                    </th>
                                                                                    <th>Active Status 
                                                                                    </th>
                                                                                    <th>Download
                                                                                    </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" OnClick="btnEdit_Click" runat="server" ImageUrl="~/Images/edit.png" CausesValidation="false" CommandArgument='<%# Eval("PAYMODENO") %>'
                                                                                AlternateText="Edit Record" ToolTip="Edit Record" />
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("PAYMENTMODE")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("ACC_HOLDER_NAME")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("BANKNAME")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("ACCOUNT_NO")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("IFSC_CODE")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("BRANCH_NAME") %>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("CHK_BOUNCE_CHARGE") %>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblActive1" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                        </td>

                                                                        <td id="tdPrintRecipet" runat="server">
                                                                            <asp:ImageButton ID="btnPrintReceipt" runat="server"
                                                                                ImageUrl="~/Images/print.png" ToolTip='<%# Eval("CHALLAN_FILE_NAME")%>' OnClick="btnPrintReceipt_Click" CausesValidation="False" />
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmitPaymentMode" />
                                                <asp:PostBackTrigger ControlID="lvPaymentDetails" />
                                                <asp:PostBackTrigger ControlID="btnCancelMode" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="TabName" runat="server" />
                <asp:HiddenField ID="hfdregno" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfRollNo" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfenroll" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfStudMandate" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfOnlinePaymentbtn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfRegSame" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfRegnocreation" runat="server" ClientIDMode="Static" />
                <%-- <asp:HiddenField ID="hfchkFaculyAdvisor" runat="server" ClientIDMode="Static" />--%>
                <asp:HiddenField ID="hfchknewstudentemail" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchksendemailonstudentry" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkRegnocreation" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkUserCreationonFee" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkFaculyAdvisorApp" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchksendpaymentmailstudentry" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkAllowDocumentVerification" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkAllowTrisemester" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkoutstandingfees" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkdemandcreationsempromo" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfCourseExamRegData" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfSemadmOfflinebtn" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkbeforesempromotion" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkafteresempromotion" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkcreateusernewstudentry" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkStdReactivationfee" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdSemAdmWithPayment" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkIntakeCapacity" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchktimeReport" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkGlobalCTAllotment" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkValueAddedCTAllotment" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkhosteltypeop" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfSelectCollege" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfElectChoiceFor" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfSeatcapacitynewstud" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfoutstandingdashorad" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkAllowToDisplayStudLoginDashboard" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkslotmand" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfReceiptDisplayInHTML_Format" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkAttTeaching" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkCreateRegno" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdchkcreateusernewprntentry" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfdRedoImprovementCourseRegFlag" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkOutstandingFeeCollection" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkFeeHeadGroup" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hftxtFeeReceiptCopies" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hfchkScholarshipConAdj" runat="server" ClientIDMode="Static" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <%--       <asp:PostBackTrigger ControlID="btnSubmitPaymentMode" />--%>
            <asp:PostBackTrigger ControlID="ddlCollege" />
            <asp:PostBackTrigger ControlID="btnCourseExamReg" />

        </Triggers>
    </asp:UpdatePanel>

    <div id="popup" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="modal" id="myModalPopUp" data-backdrop="static">
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-body pl-0 pr-0 pl-lg-2 pr-lg-2">
                                <div class="col-12 mt-3">
                                    <h5 class="heading">Please enter password to access this page.</h5>
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <%--  <label>PASSWORD</label>--%>
                                            <asp:Label ID="lblPass" runat="server" Text="ybc@123" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtPass" TextMode="Password" runat="server" TabIndex="1" ToolTip="Please Enter Password" AutoComplete="new-password"
                                                MaxLength="50" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="Password Required !" ControlToValidate="txtPass"
                                                Display="None" ValidationGroup="password"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                        </div>
                                        <div class="btn form-group col-lg-12 col-md-12 col-12">
                                            <asp:Button ID="btnConnect" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="1" CssClass="btn btn-outline-primary"
                                                runat="server" Text="Submit" OnClick="btnConnect_Click" ValidationGroup="password" />
                                            <asp:Button ID="btnCancel1" data-dismiss="myModalPopUp" data-keyboard="false" TabIndex="2" CssClass="btn btn-danger"
                                                runat="server" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="false" ValidationGroup="password" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnConnect" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        $("#ctl00_ContentPlaceHolder1_btnStudentSubmit").click(function(){
            //function submit(){
            debugger;
            var arrItems = [];
            $('#StudentConfig').find('tr').each(function () {
                var objArray= {};
                var row = $(this);
                var _studconfig_id,_caption_name,_field_name,_isactive,_ismandatory,_organization_id,_page_no,_page_name;

                _studconfig_id = row.find('td').eq(0).text();
                _caption_name = row.find('td').eq(1).text();
                _field_name = row.find('td').eq(2).text();
                _isactive = row.find("#rdISACTIVE" + row.find('td').eq(0).text()).is(":checked")
                _ismandatory = row.find("#rdISMANDATORY" + row.find('td').eq(0).text()).is(":checked")
                _organization_id = row.find('td').eq(4).text();
                _page_no = row.find('td').eq(5).text();
                _page_name = row.find('td').eq(6).text();

                if (_studconfig_id != '') {
                    objArray["studconfig_id"] = _studconfig_id;
                    objArray["caption_name"] = _caption_name;
                    objArray["isactive"] = _isactive;
                    objArray["ismandatory"] = _ismandatory;
                    objArray["organization_id"] = _organization_id;
                    objArray["page_no"] = _page_no;
                    objArray["pagename"] = _page_name;
                    //var item =  row.find("#rdISMANDATORY" + row.find('td').eq(0).text()).is(":checked")
                    arrItems.push(objArray);
                }
            });
            SaveUpdateStudentConfig(arrItems);
            //}
        });

        function SaveUpdateStudentConfig(_studentConfig)
        {
            debugger;
            var JData = '{StudentConfig: ' + JSON.stringify(_studentConfig) +'}'
            //var JData = '{StudentConfig: ' + JSON.stringify(_studentConfig) +'}'
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("ModuleConfig.aspx/SaveUpdateStudentconfig") %>',
                data: JData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    debugger;
                    var Jdata = data.d;
                    alert(Jdata);
                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    //debugger
                    alert("error");
                    alert(response.responseText);
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {

            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //$("[id*=TabName]").val();
            });
        });

    </script>
    <script>
        $(document).ready(function () {

            var hfdregno = document.getElementById('<%= hfdregno.ClientID %>');
            if (hfdregno.value == 1) {
                var tabName = "tab_2";
                $('#Tabs a[href="#' + tabName + '"]').tab('show');
                $('#Tabs a:first').hide()
                //$('#tabs li > a[data_id=3]').parent().removeClass('active').css('display', 'none');
            }
        });

    </script>

    <script>
        $(function () {
            var hdnroll = $('#hfRollNo').val();
            var hdnreg = $('#hfdregno').val();
            var hdnenroll = $('#hfenroll').val();
            var hfStudMandate = $('#hfStudMandate').val();
            var hfRegSame = $('#hfRegSame').val();
            // var hfOnlinePayment= $('#hfOnlinePaymentbtn').val();
            //  var hdnchkRegnocreation=$('#hfchkRegnocreation').val();
            //var hdnchknewstudentemail=$('#hfchknewstudentemail').val();
            // var hdnchksendemailonstudentry=$('#hfchksendemailonstudentry').val();
            //var hdnchkRegnocreation =$('#hfchkRegnocreation').val();
            var hhdnchkFaculyAdvisorApp=$('#hfchkFaculyAdvisorApp').val();
            var hdnchksendpaymentmailstudentry =$('#hfchksendpaymentmailstudentry').val();
            var hdnchkAllowDocumentVerification =$('#hfchkAllowDocumentVerification').val();

            if (hdnroll === 'true') 
            {
                $('input:checkbox[name=rdRollNo]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=rdRollNo]').prop('checked', false); 
            }

            if (hdnreg === 'true') 
            {
                $('input:checkbox[name=rdRegno]').prop('checked', true);
            }
            else
            { 
                $('input:checkbox[name=rdRegno]').prop('checked', false); 
            }

            if (hdnenroll === 'true') 
            {
                $('input:checkbox[name=rdenroll]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=rdenroll]').prop('checked', false);
            }

            if (hfStudMandate === 'true') 
            {
                $('input:checkbox[name=rdStudMandate]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=rdStudMandate]').prop('checked', false); 
            }

            if (hfRegSame === 'true') 
            {
                $('input:checkbox[name=rdRegSame]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=rdRegSame]').prop('checked', false); 
            }

            if (hfOnlinePayment === 'true') 
            {
                $('input:checkbox[name=rdonlinepaymentbtn]').prop('checked', true);
            }
            else 
            {
                $('input:checkbox[name=rdonlinepaymentbtn]').prop('checked', false); 
            }


            //if (hdnchkRegnocreation === 'true') 
            //{
            //    $('input:checkbox[name=chkRegnocreation]').prop('checked', true);
            //}
            //else 
            //{ 
            //    $('input:checkbox[name=chkRegnocreation]').prop('checked', false); 
            //}

            //if (hdnchknewstudentemail === 'true') 
            //{
            //    $('input:checkbox[name=chksendemailonstudentry]').prop('checked', true);
            //}
            //else
            //{ 
            //    $('input:checkbox[name=chksendemailonstudentry]').prop('checked', false); 
            //}

            //if (hdnchksendemailonstudentry === 'true') 
            //{
            //    $('input:checkbox[name=chksendemailonstudentry]').prop('checked', true);
            //}
            //else 
            //{ 
            //    $('input:checkbox[name=chksendemailonstudentry]').prop('checked', false); 
            //}

            //if (hdnchksendemailonstudentry === 'true') 
            //{
            //    $('input:checkbox[name=chkUserCreationonFee]').prop('checked', true);
            //}
            //else 
            //{ 
            //    $('input:checkbox[name=chkUserCreationonFee]').prop('checked', false);
            //}

            if (hdnchkFaculyAdvisorApp === 'true') 
            {
                $('input:checkbox[name=chkFaculyAdvisorApp]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=chkFaculyAdvisorApp]').prop('checked', false); 
            }

            if (hdnchksendpaymentmailstudentry === 'true') 
            {
                $('input:checkbox[name=chksendpaymentmailstudentry]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=chksendpaymentmailstudentry]').prop('checked', false); 
            }

            if (hdnchkAllowDocumentVerification === 'true') 
            {
                $('input:checkbox[name=chkAllowDocumentVerification]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name= ]').prop('checked',false);
            }
            
            if (hfdSemAdmWithPayment === 'true') 
            {
                $('input:checkbox[name=chkSemAdmWithPayment]').prop('checked', true);
            }
            else 
            { 
                $('input:checkbox[name=chkSemAdmWithPayment ]').prop('checked',false);
            }   
            
            var hfElectChoiceFor =$('#hfElectChoiceFor').val();
            if (hfElectChoiceFor === 'true') 
                $('input:checkbox[name=chkElectChoiceFor]').prop('checked', true);
            else 
                $('input:checkbox[name=chkElectChoiceFor ]').prop('checked',false);

            var hfdchkcreateusernewprntentry =$('#hfdchkcreateusernewprntentry').val();
            if (hfdchkcreateusernewprntentry === 'true') 
                $('input:checkbox[name=chkcreateusernewprntentry]').prop('checked', true);
            else 
                $('input:checkbox[name=chkcreateusernewprntentry ]').prop('checked',false);
        });
    </script>
    <script>
        function SetActive(val, chkValue) {
            var chk = val.id;
            var select = chkValue;
            //alert(chk+" and "+select);
            if (chk == "rdRollNo") {
                //alert("in")
                if (chkValue == "true") {
                    //alert("inn")
                    $('#rdRollNo').prop('checked', true);
                    $('#hfRollNo').val($('#rdRollNo').prop('checked'));
                }
                else {
                    //alert("out")
                    $('#rdRollNo').prop('checked', false);
                    $('#hfRollNo').val(false);
                }

                //$('#rdRegno').prop('checked', false);
                //$('#rdenroll').prop('checked', false);
            }
            if (chk == "rdRegno") {
                if (chkValue == "true") {
                    $('#rdRegno').prop('checked', true);
                    $('#hfdregno').val(true);
                }
                else {
                    $('#rdRegno').prop('checked', false);
                    $('#hfdregno').val($('#rdRegno').prop('checked'));
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdenroll').prop('checked', false);

            }
            if (chk == "rdenroll") {
                if (chkValue == "true") {
                    $('#rdenroll').prop('checked', true);
                    $('#hfenroll').val($('#rdenroll').prop('checked'));
                } else {
                    $('#rdenroll').prop('checked', false);
                    $('#hfenroll').val(false);
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdRegno').prop('checked', false);

            }

            if (chk == "rdStudMandate") {
                if (chkValue == "true") {
                    $('#rdStudMandate').prop('checked', true);
                    $('#hfStudMandate').val($('#rdStudMandate').prop('checked'));
                } else {
                    $('#rdStudMandate').prop('checked', false);
                    $('#hfStudMandate').val(false);
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdRegno').prop('checked', false);

            }

            if (chk == "rdRegSame") {
                if (chkValue == "true") {
                    $('#rdRegSame').prop('checked', true);
                    $('#hfRegSame').val($('#rdRegSame').prop('checked'));
                } else {
                    $('#rdRegSame').prop('checked', false);
                    $('#hfRegSame').val(false);
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdRegno').prop('checked', false);

            }

            if (chk == "rdonlinepaymentbtn") {
                if (chkValue == "true") {
                    $('#rdonlinepaymentbtn').prop('checked', true);
                    $('#hfOnlinePaymentbtn').val($('#rdonlinepaymentbtn').prop('checked'));
                } else {
                    $('#rdonlinepaymentbtn').prop('checked', false);
                    $('#hfOnlinePaymentbtn').val(false);
                }
                //$('#rdRollNo').prop('checked', false);
                //$('#rdRegno').prop('checked', false);

            }

            //if (chk == "chkRegnocreation") {
            //    if (chkValue == "true") {
            //        $('#chkRegnocreation').prop('checked', true);
            //        $('#hfRegnocreation').val($('#chkRegnocreation').prop('checked'));
            //    } else {
            //        $('#chkRegnocreation').prop('checked', false);
            //        $('#hfRegnocreation').val(false);
            //    }
            //}

            //if (chk == "chkFaculyAdvisorApp") {
            //    if (chkValue == "true") {
            //        $('#chkFaculyAdvisorApp').prop('checked', true);
            //        $('#hfchkFaculyAdvisor').val($('#chkFaculyAdvisorApp').prop('checked'));
            //    } else {
            //        $('#chkFaculyAdvisorApp').prop('checked', false);
            //        $('#hfchkFaculyAdvisor').val(false);
            //    }
            //}

            //if (chk == "chknewstudentemail") {
            //    if (chkValue == "true") {
            //        $('#chknewstudentemail').prop('checked', true);
            //        $('#hfchknewstudentemail').val($('#chknewstudentemail').prop('checked'));
            //    } else {
            //        $('#chknewstudentemail').prop('checked', false);
            //        $('#hfchknewstudentemail').val(false);
            //    }
            //}

            //if (chk == "chksendemailonstudentry") {
            //    if (chkValue == "true") {
            //        $('#chksendemailonstudentry').prop('checked', true);
            //        $('#hfchksendemailonstudentry').val($('#chksendemailonstudentry').prop('checked'));
            //    } else {
            //        $('#chksendemailonstudentry').prop('checked', false);
            //        $('#hfchksendemailonstudentry').val(false);
            //    }
            //}

            //if (chk == "chkRegnocreation") {
            //    if (chkValue == "true") {
            //        $('#chkRegnocreation').prop('checked', true);
            //        $('#hfchkRegnocreation').val($('#chkRegnocreation').prop('checked'));
            //    } else {
            //        $('#chkRegnocreation').prop('checked', false);
            //        $('#hfchkRegnocreation').val(false);
            //    }
            //}


            if (chk == "chkUserCreationonFee") {
                if (chkValue == "true") {
                    $('#chkUserCreationonFee').prop('checked', true);
                    $('#hfchkUserCreationonFee').val($('#chkUserCreationonFee').prop('checked'));
                } else {
                    $('#chkUserCreationonFee').prop('checked', false);
                    $('#hfchkUserCreationonFee').val(false);
                }
            }

            if (chk == "chkFaculyAdvisorApp") {
                if (chkValue == "true") {
                    $('#chkFaculyAdvisorApp').prop('checked', true);
                    $('#hfchkFaculyAdvisorApp').val($('#chkFaculyAdvisorApp').prop('checked'));
                } else {
                    $('#chkFaculyAdvisorApp').prop('checked', false);
                    $('#hfchkFaculyAdvisorApp').val(false);
                }
            }

            if (chk == "chksendpaymentmailstudentry") {
                if (chkValue == "true") {
                    $('#chksendpaymentmailstudentry').prop('checked', true);
                    $('#hfchksendpaymentmailstudentry').val($('#chksendpaymentmailstudentry').prop('checked'));
                } else {
                    $('#chksendpaymentmailstudentry').prop('checked', false);
                    $('#hfchksendpaymentmailstudentry').val(false);
                }
            }

            if (chk == "chkAllowDocumentVerification") {
                if (chkValue == "true") {
                    $('#chkAllowDocumentVerification').prop('checked', true);
                    $('#hfchksendpaymentmailstudentry').val($('#chkAllowDocumentVerification').prop('checked'));
                } else {
                    $('#chkAllowDocumentVerification').prop('checked', false);
                    $('#hfchksendpaymentmailstudentry').val(false);
                }
            }

            if (chk == "chkElectChoiceFor") {
                if (chkValue == "true") {
                    $('#chkElectChoiceFor').prop('checked', true);
                    $('#hfElectChoiceFor').val($('#chkElectChoiceFor').prop('checked'));
                } else {
                    $('#chkElectChoiceFor').prop('checked', false);
                    $('#hfElectChoiceFor').val(false);
                }
            }   
            
            if (chk == "chkcreateusernewprntentry") {
                if (chkValue == "true") {
                    $('#chkcreateusernewprntentry').prop('checked', true);
                    $('#hfdchkcreateusernewprntentry').val($('#chkcreateusernewprntentry').prop('checked'));
                } else {
                    $('#chkcreateusernewprntentry').prop('checked', false);
                    $('#hfdchkcreateusernewprntentry').val(false);
                }
            } 
        }    

    </script>
    <script>
        function SetStat(val) {

            var chk = val.id;

            if (chk == "rdRollNo") {
                if (val.checked) {
                    $('#hfRollNo').val($('#rdRollNo').prop('checked'));
                    //$('#rdRegno').prop('checked', false);
                    //$('#rdenroll').prop('checked', false);
                }
                else {
                    $('#hfRollNo').val(false);
                }
            }

            if (chk == "rdRegno") {

                if (val.checked) {

                    $('#hfdregno').val($('#rdRegno').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdenroll').prop('checked', false);
                }
                else {
                    $('#hfdregno').val(false);
                }
            }

            if (chk == "rdenroll") {

                if (val.checked) {
                    $('#hfenroll').val($('#rdenroll').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfenroll').val(false);
                }
            }

            if (chk == "rdStudMandate") {

                if (val.checked) {
                    $('#hfStudMandate').val($('#rdStudMandate').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfStudMandate').val(false);
                }
            }

            if (chk == "rdRegSame") {

                if (val.checked) {
                    $('#hfRegSame').val($('#rdRegSame').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfRegSame').val(false);
                }
            }

            if (chk == "rdonlinepaymentbtn") {

                if (val.checked) {
                    $('#hfOnlinePaymentbtn').val($('#rdonlinepaymentbtn').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfOnlinePaymentbtn').val(false);
                }
            }

            if (chk == "rdonlinepaymentbtn") {

                if (val.checked) {
                    $('#hfOnlinePaymentbtn').val($('#rdonlinepaymentbtn').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfOnlinePaymentbtn').val(false);
                }
            }

            if (chk == "chkSemAdmWithPayment") {
                var dvOnlbtnSemAdm = $("#dvOnlbtnSemAdm").find(":input");
                if (val.checked) {
                    $('#hfdSemAdmWithPayment').val($('#chkSemAdmWithPayment').prop('checked'));  
                    
                    $('#rdonlinepaymentbtn').prop('disabled', false);
                    $('#hfOnlinePaymentbtn').val($('#rdonlinepaymentbtn').prop('checked'));
                   
                    
                    $('#rdofflinepaymentbtn').prop('disabled', false);
                    $('#hfSemadmOfflinebtn').val($('#rdofflinepaymentbtn').prop('checked'));
                }
                else {
                    $('#hfdSemAdmWithPayment').val(false);

                    $('#rdonlinepaymentbtn').prop('disabled', true);
                    $('#hfOnlinePaymentbtn').val(false);
                    $('#rdonlinepaymentbtn').prop('checked',false);

                    $('#rdofflinepaymentbtn').prop('disabled', true);
                    $('#rdofflinepaymentbtn').prop('checked',false);
                    $('#hfSemadmOfflinebtn').val(false);
                }
            }

            //if (chk == "chkRegnocreation") {

            //    if (val.checked) {
            //        $('#hfRegnocreation').val($('#chkRegnocreation').prop('checked'));
            //        //$('#rdRollNo').prop('checked', false);
            //        //$('#rdRegno').prop('checked', false);
            //    }
            //    else {
            //        $('#hfRegnocreation').val(false);
            //    }
            //}

            //if (chk == "chkFaculyAdvisorApp") {

            //    if (val.checked) {
            //        $('#hfchkFaculyAdvisor').val($('#chkFaculyAdvisorApp').prop('checked'));
            //        //$('#rdRollNo').prop('checked', false);
            //        //$('#rdRegno').prop('checked', false);
            //    }
            //    else {
            //        $('#hfchkFaculyAdvisor').val(false);
            //    }
            //}

            //if (chk == "chknewstudentemail") {

            //    if (val.checked) {
            //        $('#hfchknewstudentemail').val($('#chknewstudentemail').prop('checked'));
            //        //$('#rdRollNo').prop('checked', false);
            //        //$('#rdRegno').prop('checked', false);
            //    }
            //    else {
            //        $('#hfchknewstudentemail').val(false);
            //    }
            //}

            //if (chk == "chksendemailonstudentry") {

            //    if (val.checked) {
            //        $('#hfchksendemailonstudentry').val($('#chksendemailonstudentry').prop('checked'));
            //        //$('#rdRollNo').prop('checked', false);
            //        //$('#rdRegno').prop('checked', false);
            //    }
            //    else {
            //        $('#hfchksendemailonstudentry').val(false);
            //    }
            //}


            //if (chk == "chkRegnocreation") {

            //    if (val.checked) {
            //        $('#hfchkRegnocreation').val($('#chkRegnocreation').prop('checked'));
            //        //$('#rdRollNo').prop('checked', false);
            //        //$('#rdRegno').prop('checked', false);
            //    }
            //    else {
            //        $('#hfchkRegnocreation').val(false);
            //    }
            //}

            if (chk == "chkUserCreationonFee") {

                if (val.checked) {
                    $('#hfchkUserCreationonFee').val($('#chkUserCreationonFee').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfchkUserCreationonFee').val(false);
                }
            }

            if (chk == "chkFaculyAdvisorApp") {

                if (val.checked) {
                    $('#hfchkFaculyAdvisorApp').val($('#chkFaculyAdvisorApp').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfchkFaculyAdvisorApp').val(false);
                }
            }

            if (chk == "chksendpaymentmailstudentry") {

                if (val.checked) {
                    $('#hfchksendpaymentmailstudentry').val($('#chksendpaymentmailstudentry').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfchksendpaymentmailstudentry').val(false);
                }
            }

            if (chk == "chkAllowDocumentVerification") {

                if (val.checked) {
                    $('#hfchkAllowDocumentVerification').val($('#chkAllowDocumentVerification').prop('checked'));
                    //$('#rdRollNo').prop('checked', false);
                    //$('#rdRegno').prop('checked', false);
                }
                else {
                    $('#hfchkAllowDocumentVerification').val(false);
                }
            }

            if (chk == "chkElectChoiceFor") {
                if (val.checked)
                    $('#hfElectChoiceFor').val($('#chkElectChoiceFor').prop('checked'));
                else
                    $('#hfElectChoiceFor').val(false);
            }

            if (chk == "chkcreateusernewprntentry") {
                if (val.checked)
                    $('#hfdchkcreateusernewprntentry').val($('#chkcreateusernewprntentry').prop('checked'));
                else
                    $('#hfdchkcreateusernewprntentry').val(false);
            }
        }      

        function ShowCollege(){
            //if ($('#ctl00_ContentPlaceHolder1_chkSelectCollege').is(":checked"))
            if ($('#chkSelectCollege').is(":checked"))
            {
                $("#ctl00_ContentPlaceHolder1_dvCollege").css({ visibility: 'visible' });
            }
            else
                $("#ctl00_ContentPlaceHolder1_dvCollege").css({ visibility: 'hidden' });
            // $('#rdRegSame').val()='off';
            $('#rdRegSame').prop('checked', false);
            $("#ctl00_ContentPlaceHolder1_ddlCollege").prop('selectedIndex', 0);           
            //SetStat(val);
        }

        $(document).ready (function () {  
            $("#ctl00_ContentPlaceHolder1_ddlCollege").change (function () {  
                var selectedClgID = $(this).children("#ctl00_ContentPlaceHolder1_ddlCollege :selected").val(); 
                //=============================================================================
                var getCourseExamData = $('#hfCourseExamRegData').val();
                xmlDOc=$.parseXML( getCourseExamData );
                var clgList = xmlDOc.getElementsByTagName('Table');
                for (var i = 0; i < clgList.length; i++) 
                {                    
                    var tbl=clgList[i].innerHTML;
                    var clgid = clgList[i].childNodes[7].innerHTML;

                    if(clgid !="0" && selectedClgID===clgid)
                    { 
                        var CourseExamRegBoth = clgList[i].childNodes[9].innerHTML;                       
                       
                        if (CourseExamRegBoth == "true") {
                            $('#rdRegSame').prop('checked', true);
                            $('#hfRegSame').val($('#rdRegSame').prop('checked'));
                        } else {
                            $('#rdRegSame').prop('checked', false);
                            $('#hfRegSame').val(false);
                        }
                        return;
                    }else {
                        $('#rdRegSame').prop('checked', false);
                        $('#hfRegSame').val(false);
                    }  
                }
                //=====================================================================================================
               
            }); 
        }); 
    </script>
    <script>
        $(document).ready(function () {
            var sessionvalue = "<%=Session["OrgId"]%>";
            BindStudentconfig(sessionvalue,"73","NULL");
        });
        function BindStudentconfig(OrgID_,PageNo_,PageName_)
        {
            //var OrgID_="";
            //var PageNo_="";
            //var PageName_="";

            //if(name=="a")
            //{
            //    OrgID_=2;
            //    PageNo_="73";
            //    PageName_="NULL";
            //}
            //else if(name=="p")
            //{
            //    OrgID_=2;
            //    PageNo_="";
            //    PageName_="PersonalDetails.aspx";
            //}
          
            debugger;
            $.ajax({
                type: "POST",

                url: '<%= ResolveUrl("ModuleConfig.aspx/GetStudentConfigData") %>',               
                data: JSON.stringify({ OrgID:OrgID_, PageNo:PageNo_, PageName:PageName_}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    debugger;
                    var Jdata = JSON.parse(data.d);                  
                    var htmlpage = "<table class='table table-striped table-bordered nowrap ' id='StudentConfig'>" ;
                    htmlpage += "<thead class='bg-light-blue'><tr>";
                    htmlpage += "<th hidden>STUDCONFIG_ID</th>";
                    htmlpage += "<th>Caption Name</th>";
                    htmlpage += "<th>Is Active</th>";
                    htmlpage += "<th>Is Mandatory</th>";
                    htmlpage += "<th hidden>Organization ID</th>";
                    htmlpage += "<th hidden>Page No</th>";
                    htmlpage += "<th hidden>Page Name</th>";
                    htmlpage += "</tr></thead><tbody>";
                        
                   
                    var output = Jdata.map(i =>
                    "<tr>"+
                    "<td hidden>" + i.STUDCONFIG_ID +  "</td>" +
                    "<td>" + i.CAPTION_NAME + "</td>" + 
                    "<td class='text-center; vertical-align:middle'><div class='switch form-inline'>" +
                    "<input type='checkbox' id='rdISACTIVE"+ i.STUDCONFIG_ID +"' name='switch' onclick='return SetStudent("+ i.STUDCONFIG_ID +");' "+ i.ISACTIVE +"/>" +
                    "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISACTIVE"+ i.STUDCONFIG_ID +"' ></label></td>"+
                    "<td><div class='switch form-inline'>" +
                    "<input type='checkbox' id='rdISMANDATORY"+ i.STUDCONFIG_ID +"' name='switch' onclick='return SetStudentCheckbox(this);' "+ i.ISMANDATORY +"/>" +
                    "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISMANDATORY"+ i.STUDCONFIG_ID +"' ></label></td>"+
                    "<td style='text-align:center; vertical-align:middle' hidden>" + i.ORGANIZATION_ID + "</td>"+
                    "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NO + "</td>"+
                    "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NAME + "</td></tr>");
                    debugger;
                     
                    for (var i = 0; i < output.length; i++) {
                        htmlpage =  htmlpage + output[i];
                       
                    }
                    debugger;
                    htmlpage = (htmlpage + '</tbody></table>');

                    //$("#StudentConfig").html(output);
                    $('#divStudentConfig').html('');
                    $('#divStudentConfig').append(htmlpage);
                    debugger;
                    for (var i = 0; i < Jdata.length; i++) {
                        if (Jdata[i].ISMANDATORY != 'checked' && Jdata[i].ISACTIVE != 'checked' ) {
                            $('#rdISMANDATORY' + Jdata[i].STUDCONFIG_ID).prop('checked', false);
                            $("#rdISMANDATORY" + Jdata[i].STUDCONFIG_ID).attr("disabled", true);
                        }
                    }

                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    //debugger
                    alert("error");
                    alert(response.responseText);
                }
            });
        }
        function SetStudent(val) {
            //var chk = val.id;
            if ($('#rdISACTIVE' + val)[0].checked == false) {
                $('#rdISMANDATORY' + val).prop('checked', false);
                $("#rdISMANDATORY" + val).attr("disabled", true);
            }
            else
            {
                $('#rdISMANDATORY' + val).prop('checked', false);
                $("#rdISMANDATORY" + val).attr("disabled", false);
            }
           
        }
        function SetStudentCheckbox(val) {
            var chk = val.id;
            
        }
        $('#btnReset').click(function(){
            BindStudentconfig(OrgID_,PageNo_,PageName_);
        });

    </script>

    <script>     
        function NewstudEmailSend(val) {
            $('[id*=chknewstudentemail]').prop('checked', val);
        }

        function Semadmbtn(val) {
            $('[id*=rdonlinepaymentbtn]').prop('checked', val);
        }

        function feescollregnocreation(val) {
            $('[id*=chkRegnocreation]').prop('checked', val);
        }
        function Feescollusercreation(val) {
            $('[id*=chkUserCreationonFee]').prop('checked', val);
        }

        function facultadvisorallot(val) {
            $('[id*=chkFaculyAdvisorApp]').prop('checked', val);
        }

        function newstuduser(val) {
            $('[id*=chksendemailonstudentry]').prop('checked', val);
        }

        function paymentmailsend(val) {
            $('[id*=chksendpaymentmailstudentry]').prop('checked', val);
        }

        function docverification(val) {
            $('[id*=chkAllowDocumentVerification]').prop('checked', val);
        }

        function trisemester(val) {
            $('[id*=chkallowtrisemester]').prop('checked', val);
        }
        function outstanding(val) {
            $('[id*=chkoutstandingfees]').prop('checked', val);
        }
        function demandcreationsempromo(val) {
            $('[id*=chkdemandcreationsempromo]').prop('checked', val);
        }

        function semadmofflinebtn(val) {
            $('[id*=rdofflinepaymentbtn]').prop('checked', val);
        }
        function semadmbeforepromotion(val) {
            $('[id*=chkbeforesempromotion]').prop('checked', val);
        }
        function semadmafterpromotion(val) {
            $('[id*=chkafteresempromotion]').prop('checked', val);
        }

        function newstudentryusercreation(val) {
            $('[id*=chkcreateusernewstudentry]').prop('checked', val);
        }

       

        function studentReactivationfee(val) {
            $('[id*=chkStdReactivationfee]').prop('checked', val);
        }

        function SemAdmWithPayment(val) {
            $('[id*=chkSemAdmWithPayment]').prop('checked', val);
        }

        function IntakeCapacity(val) {
            $('[id*=chkIntakeCapacity]').prop('checked', val);
        }

        function TimeTableReport(val) {
            $('[id*=chktimeReport]').prop('checked', val);
        }
        function GlobalElectiveCTAllotment(val) {
            $('[id*=chkGlobalCTAllotment]').prop('checked', val);
        }
        function ValueAddedCTAllotment(val) {
            $('[id*=chkValueAddedCTAllotment]').prop('checked', val);
        }
        function HostelStatusOnPayment(val) {
            $('[id*=chkhosteltypeop]').prop('checked', val);
        }

        function ElectiveChoiceFor(val) {
            $('[id*=chkElectChoiceFor]').prop('checked', val);
        }

        function SeatcapacityNewStud(val) {
            $('[id*=chkseatcapacitynewstudentry]').prop('checked', val);
        }

        function dashboardoutstanding(val)
        {
            $('[id*=chkoutstandingdashorad]').prop('checked', val);       
        }
        function DisplayStudLoginDashboard(val)
        {
            $('[id*=chkAllowToDisplayStudLoginDashboard]').prop('checked', val);       
        }
        
        function newchkCreateRegno(val) {
            $('[id*=chkCreateRegno]').prop('checked', val);
        }

        function newchkcreateusernewprntentry(val) {
            $('[id*=chkcreateusernewprntentry]').prop('checked', val);
        }

        function newschkAttTeaching(val) {
            $('[id*=chkAttTeaching]').prop('checked', val);
        }

        function slotmandatory(val)
        {
            $('[id*=chkslotmand]').prop('checked', val);       
        }

        function CheckReceiptDisplayInHTMLFormat(val)
        {
            $('[id*=chkDisplayReceiptInHTML_Format]').prop('checked', val);       
        }

        function CheckAllowCurrentSemForRedoImprovementCrsReg(val)
        {
            $('[id*=chkRedoImprovementCourseRegFlag]').prop('checked', val);       
        }

        //Added by Gopal M
        function CheckOnstandingFeeCollection(val) {
            $('[id*=chkOutstandingFeeCollection]').prop('checked', val);
        }
      
        function CheckFeeHeadGroup(val) {
            $('[id*=chkFessHeadGroup]').prop('checked', val);
        }
        
        function CheckScholarshipConAdj(val) {
            $('[id*=chkScholarshipConAdj]').prop('checked', val);
        }
       
        function validate() {
            $('#hfchknewstudentemail').val($('#chknewstudentemail').prop('checked'));
            $('#hfOnlinePaymentbtn').val($('#rdonlinepaymentbtn').prop('checked'));
            $('#hfchkRegnocreation').val($('#chkRegnocreation').prop('checked'));
            $('#hfchkUserCreationonFee').val($('#chkUserCreationonFee').prop('checked'));
            $('#hfchkFaculyAdvisorApp').val($('#chkFaculyAdvisorApp').prop('checked'));   
            $('#hfchksendpaymentmailstudentry').val($('#chksendpaymentmailstudentry').prop('checked'));
            $('#hfchkAllowDocumentVerification').val($('#chkAllowDocumentVerification').prop('checked'));
            $('#hfchksendemailonstudentry').val($('#chksendemailonstudentry').prop('checked'));
            $('#hfchkAllowTrisemester').val($('#chkallowtrisemester').prop('checked'));
            $('#hfchkoutstandingfees').val($('#chkoutstandingfees').prop('checked'));
            $('#hfchkdemandcreationsempromo').val($('#chkdemandcreationsempromo').prop('checked'));
            $('#hfSemadmOfflinebtn').val($('#rdofflinepaymentbtn').prop('checked'));
            $('#hfchkbeforesempromotion').val($('#chkbeforesempromotion').prop('checked'));
            $('#hfchkafteresempromotion').val($('#chkafteresempromotion').prop('checked')); 
            $('#hfchkcreateusernewstudentry').val($('#chkcreateusernewstudentry').prop('checked'));
         
            $('#hfdchkStdReactivationfee').val($('#chkStdReactivationfee').prop('checked'));
            $('#hfdSemAdmWithPayment').val($('#chkSemAdmWithPayment').prop('checked')); 
            $('#hfdchkIntakeCapacity').val($('#chkIntakeCapacity').prop('checked'));
            $('#hfdchktimeReport').val($('#chktimeReport').prop('checked'));
            $('#hfdchkGlobalCTAllotment').val($('#chkGlobalCTAllotment').prop('checked'));
            $('#hfdchkhosteltypeop').val($('#chkhosteltypeop').prop('checked'));
            $('#hfElectChoiceFor').val($('#chkElectChoiceFor').prop('checked'));
            $('#hfSeatcapacitynewstud').val($('#chkseatcapacitynewstudentry').prop('checked'));
            $('#hfoutstandingdashorad').val($('#chkoutstandingdashorad').prop('checked'));
            $('#hfchkAllowToDisplayStudLoginDashboard').val($('#chkAllowToDisplayStudLoginDashboard').prop('checked'));
            $('#hfReceiptDisplayInHTML_Format').val($('#chkDisplayReceiptInHTML_Format').prop('checked'));
            $('#hfdchkValueAddedCTAllotment').val($('#chkValueAddedCTAllotment').prop('checked'));
            $('#hfchkslotmand').val($('#chkslotmand').prop('checked'));
            $('#hfdchkCreateRegno').val($('#chkCreateRegno').prop('checked'));
            $('#hfdchkAttTeaching').val($('#chkAttTeaching').prop('checked'));
            $('#hfdchkcreateusernewprntentry').val($('#chkcreateusernewprntentry').prop('checked'));
            $('#hfdRedoImprovementCourseRegFlag').val($('#chkRedoImprovementCourseRegFlag').prop('checked'));
            // Added by Gopal M.
            $('#hfchkOutstandingFeeCollection').val($('#chkOutstandingFeeCollection').prop('checked'));
            $('#hfchkFeeHeadGroup').val($('#chkFessHeadGroup').prop('checked'));
            $('#hfchkScholarshipConAdj').val($('#chkScholarshipConAdj').prop('checked'));
            var numCopies =document.getElementById("<%=txtFeeReceiptCopies.ClientID %>").value;
            if(numCopies > 3 || numCopies == 0)
            {
                alert("Allow only 1,2 and 3 numbers");
                return false;
            }
          
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });

        function validate1(){
            $('#hfchknewstudentemail').val();
            var chkSelectClg= $('#chkSelectCollege').prop('checked');
            $('#hfSelectCollege').val(chkSelectClg);
            var ddlcolg =$('#ctl00_ContentPlaceHolder1_ddlCollege').val();
            if(chkSelectClg===true && ddlcolg==='0')
            { 
                alert('Please select College.');
                return false;
            }else
                return true;
        }
    </script>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <script type="text/javascript">
        $(window).on('load', function () {
            $('#myModalPopUp').modal('show');
        });
    </script>

    <script>
     
        function validateSemesterAdm() {
            var payMode="";
            var holderName="";
            var alertMsg="";
            payMode=document.getElementById('<%=txtPaymentMode.ClientID%>').value;
            holderName=document.getElementById('<%=txtAccHolderName.ClientID%>').value;
            //var activeStatus=document.getElementById("rdActiveStatus").checked;
            //alert(xxx);
            //return false;
            //if(
            //{
                
            //}
            //else
            //{
            //    alert("out");
            //}
            if(payMode=="" || holderName=="")
            {
                if(payMode=="")
                {
                    alertMsg+="Please enter Payment Mode.\n";
                }
                if(holderName=="")
                {
                    alertMsg+="Please enter Acc. Holder Name/ Cheque & DD should be drawn in favour of";
                }
                alert(alertMsg);
                return false;
            }
            else
            {
                getactivestatus();
                return true;
            }
    
        }
    </script>

    <script>
        function TabShow(tabName) {
            //alert('hii')
            //var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>

    <input type="hidden" id="orgId" value="<%= Session["OrgId"] %>" />

    <script type="text/javascript">
        function handleDropDownChange() {
            debugger;
            var selectedText = document.getElementById('<%= ddlPageName.ClientID %>').options[document.getElementById('<%= ddlPageName.ClientID %>').selectedIndex].text.trim();

            var orgID = '<%= Session["OrgId"] %>';
            var pageNo = "";
            var pageName = "";

            if (selectedText === "Add Student") {
                pageNo = "73";
            } else if (selectedText === "Personal Details") {
                pageName = "PersonalDetails.aspx";
            }
            else if(selectedText === "Admission Details"){
                pageName = "AdmissionDetails.aspx";
            }
            // Perform an AJAX request
            $.ajax({
                type: "POST",
                url: '<%= ResolveUrl("ModuleConfig.aspx/GetStudentConfigData") %>',
                data: JSON.stringify({ OrgID: orgID, PageNo: pageNo, PageName: pageName }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    debugger;
                    var Jdata = JSON.parse(data.d);

                    var htmlpage = "<table class='table table-striped table-bordered nowrap ' id='StudentConfig'>";
                    htmlpage += "<thead class='bg-light-blue'><tr>";
                    htmlpage += "<th hidden>STUDCONFIG_ID</th>";
                    htmlpage += "<th>Caption Name</th>";
                    htmlpage += "<th>Is Active</th>";
                    htmlpage += "<th>Is Mandatory</th>";
                    htmlpage += "<th hidden>Organization ID</th>";
                    htmlpage += "<th hidden>Page No</th>";
                    htmlpage += "<th hidden>Page Name</th>";
                    htmlpage += "</tr></thead><tbody>";

                    var output = Jdata.map(function (i) {
                        return "<tr>" +
                            "<td hidden>" + i.STUDCONFIG_ID + "</td>" +
                            "<td>" + i.CAPTION_NAME + "</td>" +
                            "<td class='text-center; vertical-align:middle'><div class='switch form-inline'>" +
                            "<input type='checkbox' id='rdISACTIVE" + i.STUDCONFIG_ID + "' name='switch' onclick='return SetStudent(" + i.STUDCONFIG_ID + ");' " + i.ISACTIVE + "/>" +
                            "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISACTIVE" + i.STUDCONFIG_ID + "' ></label></td>" +
                            "<td><div class='switch form-inline'>" +
                            "<input type='checkbox' id='rdISMANDATORY" + i.STUDCONFIG_ID + "' name='switch' onclick='return SetStudentCheckbox(this);' " + i.ISMANDATORY + "/>" +
                            "<label data-on='Yes' class='newAddNew Tab'  data-off='No' for='rdISMANDATORY" + i.STUDCONFIG_ID + "' ></label></td>" +
                            "<td style='text-align:center; vertical-align:middle' hidden>" + i.ORGANIZATION_ID + "</td>" +
                            "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NO + "</td>" +
                            "<td style='text-align:center; vertical-align:middle' hidden>" + i.PAGE_NAME + "</td></tr>";
                    });

                    for (var i = 0; i < output.length; i++) {
                        htmlpage += output[i];
                    }

                    htmlpage += '</tbody></table>';

                    $('#divStudentConfig').html('');
                    $('#divStudentConfig').append(htmlpage);

                    for (var i = 0; i < Jdata.length; i++) {
                        if (Jdata[i].ISMANDATORY !== 'checked' && Jdata[i].ISACTIVE !== 'checked') {
                            $('#rdISMANDATORY' + Jdata[i].STUDCONFIG_ID).prop('checked', false);
                            $("#rdISMANDATORY" + Jdata[i].STUDCONFIG_ID).attr("disabled", true);
                        }
                    }
                },
                failure: function (response) {
                    alert("failure");
                },
                error: function (response) {
                    alert("error");
                    alert(response.responseText);
                }
            });

            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=-]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>

    <script>

        function getactivestatus() {
            
            $('#hfdChkActiveStatus').val($('#rdActiveStatus').prop('checked'));
            return true;
        }
      
        function SetActiveStatus(val) {
            $('[id*=rdActiveStatus]').prop('checked', val);
        }
        
    </script>

    <script>
        function ClickOutstandingFeeCollection(val)
        {
            var isChecked = $("#chkOutstandingFeeCollection").is(":checked");
            if (isChecked) {
                //$("#OutstandingMessageDiv").css("display", "none");
                $("#<%=(OutstandingMessageDiv.ClientID)%>").hide();
            } 
            else {
                $('#<%=OutstandingMessageDiv.ClientID %>').show();                
            }
        }
    </script>
</asp:Content>

