<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_PersonalDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #sidebar {
            display: none;
        }

        .page-wrapper.toggled .page-content {
            padding-left: 15px;
        }

        .panel-info > .panel-heading b {
            padding: 8px;
            font-size: 12px;
        }

        .sidebar-menu {
            padding: 0;
            list-style: none;
        }

            .sidebar-menu .treeview {
                padding: 0px 0px;
                color: #255282;
            }

        .treeview i {
            padding-left: 10px;
        }

        .treeview span a {
            color: #255282 !important;
            font-weight: 600;
            padding-left: 3px;
        }

            .treeview span a:hover {
                color: #0d70fd !important;
            }

        .treeview.active i, .treeview.active span a {
            color: #0d70fd !important;
        }

        hr {
            margin: 12px 0px;
            border-top: 1px solid #ccc;
        }

        #ctl00_ContentPlaceHolder1_divtabs {
            box-shadow: rgb(0 0 0 / 20%) 0px 5px 10px;
            padding: 15px 5px;
            margin: 5px 0px 15px 0px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpersonalinformation"
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
    <asp:UpdatePanel ID="updpersonalinformation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT INFORMATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-lg-2 col-md-4 col-12" id="divtabs" runat="server">
                                        <aside class="sidebar">

                                            <!-- sidebar: style can be found in sidebar.less -->
                                            <section class="sidebar" style="background-color: #ffffff">
                                                <ul class="sidebar-menu">
                                                    <!-- Optionally, you can add icons to the links -->
                                                    <li class="treeview" id="divhome" runat="server">
                                                        <i class="fa fa-search"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkGoHome"
                                                                ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Text="Search New Student"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview active">
                                                        <i class="fa fa-user"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                                ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-map-marker"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                                ToolTip="Please select Address Details" OnClick="lnkAddressDetail_Click" Text="Address Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divadmissiondetails" runat="server">
                                                        <i class="fa fa-university"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                                ToolTip="Please select Admission Details" OnClick="lnkAdmissionDetail_Click" Text="Admission Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" style="display: none">
                                                        <i class="fa fa-info-circle"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                                ToolTip="Please select DASA Student Information" Text="Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-file"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkUploadDocument"
                                                                ToolTip="Please Upload Documents" OnClick="lnkUploadDocument_Click" Text="Document Upload"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">
                                                        <i class="fa fa-graduation-cap"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                                ToolTip="Please select Qualification Details" OnClick="lnkQualificationDetail_Click" Text="Qualification Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-stethoscope"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkCovid" Visible="true"
                                                                ToolTip="Covid Vaccination Details" OnClick="lnkCovid_Click" Text="Covid Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">
                                                        <i class="fa fa-link"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                                ToolTip="Please select Other Information." OnClick="lnkotherinfo_Click" Text="Other Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divAdmissionApprove" runat="server">
                                                        <i class="fa fa-check-circle"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkApproveAdm"
                                                                ToolTip="Verify Information" OnClick="lnkApproveAdm_Click" Text="Verify Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divPrintReport" runat="server" visible="false">
                                                        <i class="fas fa-print"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Text="Print"></asp:LinkButton>
                                                        </span>
                                                    </li>
                                                </ul>
                                            </section>
                                        </aside>
                                    </div>

                                    <div class="col-lg-10 col-md-8 col-12" id="divGeneralInfo" style="display: block;">
                                        <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="sub-heading">
                                                        <h5>Student Personal Details</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divtxtidno" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>ID No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" Enabled="False" Visible="false" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="lblDYtxtEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtEnrollno" runat="server" ToolTip="Please Enter Enrollment No." CssClass="form-control"
                                                        Enabled="false" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Admission Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" ToolTip="Please Select Admission Type"
                                                        TabIndex="14" Enabled="false">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server" ToolTip="Please Enter Registration No."
                                                        Enabled="false" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Roll No.</label>
                                                    </div>

                                                    <asp:TextBox ID="txtsrno" CssClass="form-control" runat="server" ToolTip="Please Enter SR No."
                                                        Enabled="false" />
                                                    <asp:TextBox ID="TextBox2" runat="server" ToolTip="Please Enter Roll No."
                                                        Enabled="false" Visible="false" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Full Name </label>
                                                    </div>

                                                    <asp:TextBox ID="txtStudFullname" CssClass="form-control" runat="server" Enabled="false" TabIndex="1" ToolTip="Please Enter Student Full Name" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudFullname" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student First Name </label>
                                                    </div>
                                                    <%--                                                    <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="2" ToolTip="Please Enter Student First name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Enter First Name" />--%>
                                                    <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="2" ToolTip="Please Enter Student First name" Style="text-transform: uppercase;" onkeypress="return alphaOnly(event);" placeholder="Enter First Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudentName" />
                                                    <%--  <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                                    Display="None" ErrorMessage="Please Enter Student First Name" SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Middle Name</label>
                                                    </div>

                                                    <%--<asp:TextBox ID="txtStudMiddleName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="3" ToolTip="Please Enter Student Middle name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Enter Middle Name" />--%>
                                                    <asp:TextBox ID="txtStudMiddleName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="3" ToolTip="Please Enter Student Middle name" Style="text-transform: uppercase;" onkeypress="return alphaOnly(event);" placeholder="Enter Middle Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudMiddleName" />
                                                    <%--                                        <asp:RequiredFieldValidator ID="rfvStudMiddleName" runat="server" ControlToValidate="txtStudMiddleName"
                                            Display="None" ErrorMessage="Please Enter Middle Name" SetFocusOnError="True" TabIndex="8"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Last Name</label>
                                                    </div>

                                                    <%--<asp:TextBox ID="txtStudLastName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="4" ToolTip="Please Enter Student Last name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Enter Last Name" />--%>
                                                    <asp:TextBox ID="txtStudLastName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="4" ToolTip="Please Enter Student Last name" Style="text-transform: uppercase;" onkeypress="return alphaOnly(event);" placeholder="Enter Last Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudLastName" />
                                                    <%--     <asp:RequiredFieldValidator ID="rfvStudLastName" runat="server" ControlToValidate="txtStudLastName"
                                            Display="None" ErrorMessage="Please Enter Student Last Name" SetFocusOnError="True" TabIndex="8"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Mobile No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudMobile" CssClass="form-control" runat="server" MaxLength="10" TabIndex="5"
                                                        onkeyup="validateNumeric(this);" ToolTip="Please Enter Student Mobile No"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStudMobile" runat="server" ControlToValidate="txtStudMobile"
                                                        Display="None" ErrorMessage="Please Enter Student Mobile No " SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtStudMobile" ErrorMessage="Student's Mobile No. is Invalid" ForeColor="red" ValidationGroup="Academic"
                                                        ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
<<<<<<< HEAD
                                                        <%-- <sup>* </sup>--%>
=======
                                                        <sup id="supAlternateNoStud" runat="server">* </sup>
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        <label>Alternate Mobile No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtAlternateNoStud" CssClass="form-control" runat="server" MaxLength="10" TabIndex="5"
                                                        onkeyup="validateNumeric(this);" ToolTip="Please Enter Student Alternate Mobile No"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" Display="Dynamic"
                                                        ControlToValidate="txtAlternateNoStud" ErrorMessage="Student's Alternate Mobile No. is Invalid" ForeColor="red" ValidationGroup="Academic"
                                                        ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student Email ID </label>
                                                    </div>
                                                    <asp:TextBox ID="txtStudentEmail" CssClass="form-control" runat="server" TabIndex="6" ToolTip="Please Enter Student Email Id"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="rfvStudentEmail" runat="server" ControlToValidate="txtStudentEmail"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid Student Email Id" ValidationGroup="Academic">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvtxtStudentEmail" runat="server" ControlToValidate="txtStudentEmail"
                                                        Display="None" ErrorMessage="Please Enter Student Email-Id" SetFocusOnError="True"
                                                        TabIndex="20" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Alternate Email ID </label>
                                                    </div>
                                                    <asp:TextBox ID="txtInstituteEmail" CssClass="form-control" runat="server" TabIndex="200" ToolTip="Please Enter Alternate Email Id"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="rfvIndusEmail" runat="server" ControlToValidate="txtInstituteEmail"
                                                        Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid Alternate Email Id" ValidationGroup="Academic">
                                                    </asp:RegularExpressionValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date of Birth </label>
                                                    </div>
                                                    <div class="input-group">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfBirth" CssClass="form-control" runat="server" TabIndex="7" ToolTip="Please Enter Date Of Birth" />
                                                        <%-- <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                                    Height="16px" />--%>
                                                        <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                                                            Display="None" ErrorMessage="Please Enter Date Of Birth" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDateOfBirth" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                            Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server"
                                                            ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="False"
                                                            InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                            ErrorMessage="Please Enter Valid Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                            ValidationGroup="Academic" SetFocusOnError="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Birth Place </label>
                                                    </div>
                                                    <asp:TextBox ID="txtBirthPlace" runat="server" ToolTip="Please Enter Birth Place" MaxLength="300"
                                                        TabIndex="8" CssClass="form-control" placeholder="Enter Birth Place" onkeypress="return alphaOnly(event);" />
                                                    <%--   <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                    TargetControlID="txtBirthPlace" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>
<<<<<<< HEAD
                                                    <asp:RequiredFieldValidator ID="rfvbirth" runat="server" ControlToValidate="txtBirthPlace"
=======
                                                                                                       <%--<asp:RequiredFieldValidator ID="rfvbirth" runat="server" ControlToValidate="txtBirthPlace"
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        Display="None" ErrorMessage="Please Enter Birth Place" SetFocusOnError="True"
                                                        TabIndex="1" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
<<<<<<< HEAD
=======
                                                        <sup id="supGender" runat="server">* </sup>
                                                        <%--<asp:Label ID="supGender" runat="server" ForeColor="OrangeRed" Font-Size="Small">* </asp:Label>--%>
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        <label>Gender </label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdobtn_Gender" runat="server" TabIndex="9" RepeatDirection="Horizontal" ToolTip="Please Select Gender">
                                                        <asp:ListItem Text="&nbsp;Male" Value="M"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Female" Value="F"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Others" Value="T"></asp:ListItem>

                                                    </asp:RadioButtonList>
                                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Gender"
                                                    ControlToValidate="rdobtn_Gender" Display="None" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Marital Status </label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdobtn_marital" runat="server" TabIndex="11" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="&nbsp;Single" Value="N" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Married" Value="Y"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Marital Status"
                                                        ControlToValidate="rdobtn_marital" Display="None" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Nationality </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlNationality" CssClass="form-control" runat="server" TabIndex="9" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Nationality" data-select2-enable="true" />
                                                    <asp:RequiredFieldValidator ID="rfvddlNationality" runat="server" ControlToValidate="ddlNationality"
                                                        Display="None" ErrorMessage="Please Select Nationality" SetFocusOnError="True"
                                                        ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
<<<<<<< HEAD
                                                        <%-- <sup>* </sup>--%>
=======
                                                        <sup id="supBloodGroup" runat="server">* </sup>
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        <label>Blood Group </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBloodGroupNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        TabIndex="10" ToolTip="Please Select Blood group" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvddlBloodGroupNo" runat="server" ControlToValidate="ddlBloodGroupNo"
                                                    Display="None" ErrorMessage="Please Select Blood Group" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Religion </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReligion" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Religion" TabIndex="12" data-select2-enable="true" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlReligion"
                                                        Display="None" ErrorMessage="Please Select Religion" SetFocusOnError="True"
                                                        ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <%--Added by sachin 26-07-2022 RequiredFieldValidator--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Category </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlClaimedcategory" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select category" TabIndex="13" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlClaimedcategory" runat="server" ControlToValidate="ddlClaimedcategory"
                                                        Display="None" ErrorMessage="Please Select category" SetFocusOnError="True"
                                                        ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCaste" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supCaste" runat="server"></sup>
                                                        <label>Caste</label>
                                                    </div>

<<<<<<< HEAD
<<<<<<< HEAD
=======
                                                    <asp:TextBox ID="txtCaste" CssClass="form-control" runat="server" TabIndex="14" ToolTip="Please Enter Caste"
                                                        placeholder="Enter Caste" onkeypress="return alphabetWithSpace(event);"
                                                        MaxLength="50" />
                                                    
                                                </div>
>>>>>>> 343751a ([BUGFIX] [168347] Added validation To The Caste Textbox)
=======
                                                    <asp:TextBox ID="txtCaste" CssClass="form-control" runat="server" TabIndex="14" ToolTip="Please Enter Caste"
                                                        placeholder="Enter Caste" onkeypress="return alphaOnly(event);"
                                                        MaxLength="50" />
                                                </div>
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Category </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCasteCategory" runat="server" CssClass="form-control"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Category" TabIndex="10" />
                                                    <%--  <asp:RequiredFieldValidator ID="rfvddlCasteCategory" runat="server" ControlToValidate="ddlCasteCategory"
                                                    Display="None" ErrorMessage="Please select Caste Category" SetFocusOnError="True"
                                                     ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Caste </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCaste" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select  Caste" TabIndex="11">
                                                    </asp:DropDownList>
                                                    <%--     <asp:RequiredFieldValidator ID="rfvddlCaste" runat="server" ControlToValidate="ddlCaste"
                                                    Display="None" ErrorMessage="Please Enter Caste" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Sub Caste</label>
                                                    </div>

                                                    <asp:TextBox ID="txtSubCaste" CssClass="form-control" runat="server" TabIndex="14" ToolTip="Please Enter Sub Caste"
                                                        placeholder="Enter Sub Caste" onkeypress="return alphaOnly(event);"
                                                        MaxLength="100" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Physically Disabled</label>
                                                    </div>

                                                    <%--<label> Physically Handicapped</label>--%>
                                                    <asp:DropDownList ID="ddlHandicap" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true" ToolTip="Please Select Physical Handicap Status" TabIndex="15">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rfv_ddlHandicap" runat="server" ControlToValidate="ddlHandicap"
                                                    Display="None" ErrorMessage="Please Select Handicap Status" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Aadhar No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtAddharCardNo" CssClass="form-control" runat="server" ToolTip="Please Enter Aadhar Card No."
                                                        TabIndex="16" MaxLength="12" placeholder="Enter Aadhar No."></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                                        FilterMode="ValidChars" FilterType="Custom" ValidChars="0123456789" TargetControlID="txtAddharCardNo" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddharCardNo"
                                                        Display="None" ErrorMessage="Please Enter Aadhar Card No." SetFocusOnError="True" TabIndex="8"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Aadhar No. is Invalid" ID="RegularExpressionValidator6" ControlToValidate="txtAddharCardNo" ValidationExpression=".{12}.*"
                                                        Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Passport No.</label>
                                                    </div>

                                                    <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control" ToolTip="Please Enter Passport No." MaxLength="24"
                                                        TabIndex="17" placeholder="Enter Passport No."></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <label>Citizenship No.</label>
                                                    </div>

                                                    <asp:TextBox ID="txtCitizenshipNo" runat="server" CssClass="form-control" ToolTip="Please Enter Citizenship No." MaxLength="24"
                                                        TabIndex="19" placeholder="Enter Citizenship No."></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>College Rank</label>
                                                    </div>

                                                    <asp:TextBox ID="txtClgRank" runat="server" CssClass="form-control" ToolTip="Please Enter College Rank" MaxLength="24"
                                                        TabIndex="19" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <label>Payment Type </label>
                                                    </div>
                                                    <%--<asp:TextBox ID="txtPaymentType" CssClass="form-control" runat="server" Enabled="false" TabIndex="23" />--%>
                                                    <asp:DropDownList ID="ddlPayType" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Payment Type" TabIndex="18" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Scholarship </label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoscholarship" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" TabIndex="29">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <%-- <asp:DropDownList ID="ddladmthrough" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Admission Type" TabIndex="14">
                                            </asp:DropDownList>--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Admission Through </label>
                                                    </div>
                                                    <%--<asp:TextBox ID="txtPaymentType" CssClass="form-control" runat="server" Enabled="false" TabIndex="23" />--%>
                                                    <asp:DropDownList ID="ddladmthrough" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Admission Type" TabIndex="30" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="form-group col-lg-1 col-md-4 col-6 pr-md-0">
                                                    <div class="label-dynamic">
                                                        <label>Hosteller </label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoHosteler" runat="server" RepeatDirection="Horizontal" TabIndex="19">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-4 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Transportation</label>
                                                    </div>

                                                    <asp:RadioButtonList ID="rdbTransport" runat="server" RepeatDirection="Horizontal" TabIndex="20">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-4 col-12">
                                                    <div class="label-dynamic">
                                                        <label>NRI/OCI/International Student/ PIO</label>
                                                    </div>

                                                    <asp:RadioButtonList ID="rdoInternationalStu" runat="server" RepeatDirection="Horizontal" TabIndex="21">
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-4 col-6">
                                                    <div class="label-dynamic">
                                                        <label>Specify Parents Details </label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdofatheralive" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdofatheralive_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="21">
                                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                                <div class="form-group col-md-8 col-lg-4 col-12" id="rdoParents" visible="true" runat="server">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-6">
                                                            <div class="label-dynamic">
                                                                <label>Specify Father Details </label>
                                                            </div>

                                                            <asp:RadioButtonList ID="rdoFather" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoFather_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="21">
                                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-6">
                                                            <div class="label-dynamic">
                                                                <label>Specify Mother Details </label>
                                                            </div>
                                                            <asp:RadioButtonList ID="rdoMother" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rfoMother_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="21">
                                                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                                <asp:ListItem Value="0">No</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>


                                                    </div>
                                                </div>
<<<<<<< HEAD
                                            </div>
                                            <asp:Panel ID="pnlApplicationId" runat="server" Visible="false">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-6">
=======
                                                </div>
                                            
                                            <%--<asp:Panel ID="pnlApplicationId" runat="server" Visible="false">--%>
                                                <div class="row" id="divApplicationId" runat="server" visible="false">
                                                    <div class="form-group col-lg-3 col-md-6 col-6" id="divABCCId" runat="server">
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label id="lblAbccId">ABCC Id</label>
                                                        </div>

                                                        <asp:TextBox ID="txtABCCId" runat="server" CssClass="form-control" ToolTip="Please Enter ABCC Id" MaxLength="20"
                                                            TabIndex="19" placeholder="Enter ABCC Id" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtABCCId" runat="server" ControlToValidate="txtABCCId"
                                                            Display="None" ErrorMessage="Please Enter ABCC Id" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-6">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label id="lblDteAppId">DTE Application Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDTEAppId" runat="server" CssClass="form-control" ToolTip="Please Enter DTE Application Id" MaxLength="20"
                                                            TabIndex="19" placeholder="Enter DTE Application Id" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDTEAppId" runat="server" ControlToValidate="txtDTEAppId"
                                                            Display="None" ErrorMessage="Please Enter DTE Application Id" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                               <%--</asp:Panel>--%>



                                            <div class="row" runat="server" id="FatherSection" visible="true">
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Father Details</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
<<<<<<< HEAD
=======
                                                        <sup id="supFatherFullName" runat="server">* </sup>
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                        <label>Father's Full Name </label>
                                                    </div>

                                                    <asp:TextBox ID="txtFatherFullName" CssClass="form-control" runat="server" TabIndex="1" ToolTip="Please Enter Father's Full Name" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherFullName" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's First Name </label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherName" CssClass="form-control" runat="server" TabIndex="22" ToolTip="Please Enter Father's First Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" Enabled="true" placeholder="Enter First Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherName" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ControlToValidate="txtFatherName"
                                                    Display="None" ErrorMessage="Please Enter Father First Name" SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father Middle Name</label>
                                                    </div>

                                                    <asp:TextBox ID="txtFatherMiddleName" CssClass="form-control" runat="server" TabIndex="23" ToolTip="Please Enter Father Middle Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Enter Middle Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherMiddleName" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvFatherMiddleName" runat="server" ControlToValidate="txtFatherMiddleName"
                                        Display="None" ErrorMessage="Please Enter Father Name" SetFocusOnError="True"
                                        ValidationGroup="Academic" Visible="False"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Last Name</label>
                                                    </div>

                                                    <asp:TextBox ID="txtFatherLastName" CssClass="form-control" runat="server" TabIndex="24" ToolTip="Please Enter Father Last Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" placeholder="Enter Last Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherLastName" />
                                                    <%--  <asp:RequiredFieldValidator ID="rfvFatherLastName" runat="server" ControlToValidate="txtFatherLastName"
                                            Display="None" ErrorMessage="Please Enter Father Last Name" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                    <%--  </div>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Mobile No. </label>
                                                    </div>
                                                    <%--<label>Father's Mobile No.</label>--%>
                                                    <asp:TextBox ID="txtFatherMobile" CssClass="form-control" runat="server" TabIndex="25" ToolTip="Please Enter Father's Mobile No"
                                                        MaxLength="10" placeholder="Enter Mobile No." />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteFatherMobile" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtFatherMobile">
                                                    </ajaxToolKit:FilteredTextBoxExtender>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                                                        ControlToValidate="txtFatherMobile" ErrorMessage="Father's Mobile No. is Invalid" ForeColor="red" ValidationGroup="Academic"
                                                        ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Alternate Mobile No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherAlterateNo" CssClass="form-control" runat="server" TabIndex="25" ToolTip="Please Enter Father's Alternate Mobile No"
                                                        MaxLength="10" placeholder="Enter Mobile No." />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtFatherAlterateNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>

                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ControlToValidate="txtFatherAlterateNo" ErrorMessage="Father's Alternate Mobile No. is Invalid" ForeColor="red" ValidationGroup="Academic"
                                                        ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Office Phone No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtFathersOfficeNo" CssClass="form-control" runat="server" TabIndex="26" MaxLength="10" ToolTip="Please Enter Father's Office Phone No" placeholder="Enter Office Phone No."></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtFathersOfficeNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Father's Office Phone No. is Invalid" ID="revMobile" ControlToValidate="txtFathersOfficeNo" ValidationExpression=".{10}.*"
                                                        Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Qualification </label>
                                                    </div>
                                                    <asp:TextBox ID="txtFatherDesignation" CssClass="form-control" runat="server" ToolTip="Please Enter Father's Qualification" onkeypress="return alphaOnly(event);" placeholder="Enter Father Qualification"
                                                        TabIndex="27"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                        TargetControlID="txtFatherDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Occupation </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlOccupationNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Father's Occupation" TabIndex="28" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Father's Email </label>
                                                    </div>
                                                    <asp:TextBox ID="txtfatheremailid" CssClass="form-control" runat="server" TabIndex="29" ToolTip="Please Enter Father's Email"
                                                        Enabled="true" placeholder="Enter Father's Email" onchange="return isEmail(this);" />
                                                    <asp:RegularExpressionValidator ID="rfvfathEmail" runat="server" ControlToValidate="txtfatheremailid"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Date of Birth"></asp:RegularExpressionValidator>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtfatheremailid"
                                    Display="None" ErrorMessage="Please Enter Father's Email " ValidationGroup="academic"
                                    SetFocusOnError="true">
                                </asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Annual Income </label>
                                                    </div>
                                                    <asp:TextBox ID="txtAnnualIncome" CssClass="form-control" runat="server" TabIndex="30" MaxLength="12" ToolTip="Please Enter Annual Income" placeholder="Enter Annual Income"></asp:TextBox>
                                                    <%--               <asp:RequiredFieldValidator ID="rfvAnnualincome" runat="server" ControlToValidate="txtAnnualIncome"
                                                    Display="None" ErrorMessage="Please Enter Annual Income" SetFocusOnError="True"
                                                    ValidationGroup="Academic" ></asp:RequiredFieldValidator>--%>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteAnnualIncome" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtAnnualIncome">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="row" id="MotherSection" runat="server" visible="true">
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Mother Details</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother's Name </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherName" CssClass="form-control" runat="server" TabIndex="31" ToolTip="Please Enter Mother's Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" Enabled="true" placeholder="Enter Mother's Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvtxtMotherName" runat="server" ControlToValidate="txtMotherName"
                                            Display="None" ErrorMessage="Please Enter Mother Name" SetFocusOnError="True"
                                            TabIndex="7" ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother's Mobile No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherMobile" CssClass="form-control" runat="server" TabIndex="32" ToolTip="Please Enter Mother's Mobile No"
                                                        MaxLength="10" placeholder="Enter Mobile No." />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteMotherMobile" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMotherMobile">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Mother's Mobile No. is Invalid" ID="RegularExpressionValidator2" ControlToValidate="txtMotherMobile" ValidationExpression=".{10}.*"
                                                        Display="Dynamic" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Alternate Mobile No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherAlternateNo" CssClass="form-control" runat="server" TabIndex="32" ToolTip="Please Enter Mother's Alternate Mobile No"
                                                        MaxLength="10" placeholder="Enter Mobile No." />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMotherAlternateNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Mother's Alternate Mobile No. is Invalid" ID="RegularExpressionValidator5" ControlToValidate="txtMotherAlternateNo" ValidationExpression=".{10}.*"
                                                        Display="Dynamic" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother's Email </label>
                                                    </div>
                                                    <asp:TextBox ID="txtmotheremailid" CssClass="form-control" runat="server" TabIndex="33" ToolTip="Please Enter Mother's Email" Enabled="true"
                                                        placeholder="Enter Mother's Email" onchange="return isEmail(this);" />
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtmotheremailid"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Email Id" ValidationGroup="Academic">
                                                </asp:RegularExpressionValidator>--%>
                                                    <%-- </div>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother's Qualification </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMotherDesignation" runat="server" TabIndex="34" CssClass="form-control" ToolTip="Please Enter Mother's Qualification" onkeypress="return alphaOnly(event);" placeholder="Enter Mother's Qualification"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                        TargetControlID="txtMotherDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                </div>
<<<<<<< HEAD
                                                <div class="form-group col-lg-3 col-md-6 col-12">
=======
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divMOcc" runat="server">
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
                                                    <div class="label-dynamic">
                                                        <label>Mother's Occupation </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlMotherOccupation" CssClass="form-control" runat="server" TabIndex="35" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Mother's Occupation" data-select2-enable="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mother's Office Phone No. </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMothersOfficeNo" CssClass="form-control" runat="server" TabIndex="36" MaxLength="10" ToolTip="Mother's Office Phone No" placeholder="Enter Office Phone No."></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteMotherOfficeNum" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMothersOfficeNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Mother's Office Phone No is Invalid" ID="RegularExpressionValidator3" ControlToValidate="txtMothersOfficeNo" ValidationExpression=".{10}.*"
                                                        Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divMAnnualIncome" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup id="supMAnnualIncome" runat="server"></sup>
                                                        <label>Annual Income </label>
                                                    </div>
                                                    <asp:TextBox ID="txtMAnnualIncome" CssClass="form-control" runat="server" TabIndex="30" MaxLength="8" ToolTip="Please Enter Annual Income" placeholder="Enter Annual Income"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMAnnualIncome">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5><b>Photo & Signature Details</b></h5>
                                                    </div>
                                                </div>


                                                <div class="form-group col-md-12" style="color: Red; font-weight: bold">
                                                    <span style="color: black">Note :</span>  Only JPG,JPEG,PNG files are allowed upto 150 KB size For Photo and Signature, (Only Passport Size Photo Allowed).
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">

                                                                <label>Photo</label>
                                                            </div>
                                                            <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" /><br />
                                                            <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="37" onchange="previewFilePhoto()" />
                                                            <asp:Button ID="btnPhotoUpload" runat="server" CssClass="btn btn-primary" Text="Upload" TabIndex="37" OnClick="btnPhotoUpload_Click" />

                                                            <%--  <asp:RequiredFieldValidator ID="rfvfuPhotoUpload" runat="server" ControlToValidate="fuPhotoUpload"
                                                        Display="None" ErrorMessage="Please Upload Photo" SetFocusOnError="True"
                                                        ValidationGroup="Academic" ></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">

                                                                <label>Signature</label>
                                                            </div>
                                                            <asp:Image ID="ImgSign" runat="server" Width="150px" Height="40px" /><br />
                                                            <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()" TabIndex="38" />
                                                            <asp:Button ID="btnSignUpload" CssClass="btn btn-primary" runat="server" Text="Upload" TabIndex="38" OnClick="btnSignUpload_Click" />
                                                            <%--  <asp:RequiredFieldValidator ID="rfvfuSignUpload" runat="server" ControlToValidate="fuSignUpload"
                                                        Display="None" ErrorMessage="Please Upload Signature" SetFocusOnError="True"
                                                        ValidationGroup="Academic" ></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" TabIndex="39" Text="Save & Continue >>" ToolTip="Click to Submit"
<<<<<<< HEAD
                                                    CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
=======
                                                    CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)

                                                <button runat="server" id="btnGohome" visible="false" tabindex="40" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                                    Go Back Home
                                                </button>

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Academic" />

                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnPhotoUpload" />
            <asp:PostBackTrigger ControlID="btnSignUpload" />


        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }


        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script type="text/javascript">

        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
        function previewFilePhoto() {
            debugger;
            var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        function previewFilePhoto2() {
            var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
<<<<<<< HEAD
=======

    <script type="text/javascript">
        function alertmessage(commaSeperatedString) {
            var parts = commaSeperatedString.split(',');
            var errorMessage = parts.join('\n');
            alert(errorMessage);
        }
    </script>


>>>>>>> e63510d ([ENHANCMENT] [49053] Added Mother Annual Income Textbox)
    <script type="text/javascript">
        function pageLoad() {

            function previewFilePhoto() {
                var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }

            function previewFilePhoto2() {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }


        }
        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (regex.test(email.value)) {
                return true;
            }
            else {
                alert('Please Enter Valid Email ID');
                email.value = '';
                email.focus();
                return false;
            }
        }



    </script>


    <script>
        function validateInput(inputElement) {
            var inputValue = inputElement.value;
            var alphabeticRegex = /^[A-Za-z]+$/;

            if (!alphabeticRegex.test(inputValue)) {
                document.getElementById('errorText').textContent = 'Only alphabetic characters are allowed.';
                inputElement.value = inputValue.replace(/[^A-Za-z]/g, ''); // Remove non-alphabetic characters
            } else {
                document.getElementById('errorText').textContent = '';
            }
        }
    </script>

    <script type="text/javascript">
        function alphabetWithSpace(e) {
            var code;

            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if (!(code >= 65 && code <= 90) || (code >= 97 && code <= 122) || code === 32) {
                return false;
            } else {
                //e.preventDefault(); 
                return true;
            }
        }
    </script>


    <style id="cssStyle" type="text/css" media="all">
        .CS {
            background-color: #abcdef;
            color: Yellow;
            border: 1px solid #AB00CC;
            font: Verdana 0px;
            padding: px 4px;
            f ly: Palatino Linotype, Arial, Helvetica, sans-serif;
        }
    </style>
    <script>
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
                //    alert("Not Allowed Special Character..!");
                return true;
            }

            else

                return false;

        }

    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
