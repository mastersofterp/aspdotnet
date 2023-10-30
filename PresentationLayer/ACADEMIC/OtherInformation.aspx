<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="OtherInformation.aspx.cs" Inherits="ACADEMIC_OtherInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updotherinformation"
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

        .del-cont-text span {
            margin-left: 15px;
        }
    </style>

    <asp:HiddenField ID="hfdParamValue" runat="server" ClientIDMode="Static" />

    <asp:UpdatePanel ID="updotherinformation" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div4" runat="server"></div>
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
                                                    <li class="treeview">
                                                        <i class="fa fa-user"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                                ToolTip="Please select Personal Details" OnClick="lnkPersonalDetail_Click" Text="Personal Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview ">
                                                        <i class="fa fa-map-marker"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                                ToolTip="Please select Address Details" OnClick="lnkAddressDetail_Click" Text="Address Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview" id="divadmissiondetailstreeview" runat="server">
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

                                                    <li class="treeview active">
                                                        <i class="fa fa-link"></i>
                                                        <span>
                                                            <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                                ToolTip="Please select Other Information" OnClick="lnkotherinfo_Click" Text="Other Information"> 
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

                                    <div class="col-lg-10 col-md-8 col-12">
                                        <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                            <div class="row">
                                                <div class="col-md-12" id="Div1" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Other Personal Information</h5>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divAboutStudent" style="display: block;">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Birth Place</label>
                                                        </div>

                                                        <asp:TextBox ID="txtBirthPlace" runat="server" ToolTip="Please Enter Birth Place" MaxLength="24"
                                                            TabIndex="80" CssClass="form-control" onkeypress="return alphaOnly(event);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                            TargetControlID="txtBirthPlace" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Birth State </label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="86"
                                                            ToolTip="Please Select State" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>PIN Code</label>
                                                        </div>

                                                        <asp:TextBox ID="txtBirthPinCode" runat="server" CssClass="form-control" TabIndex="87" ToolTip="Please Enter PIN code of birth place"
                                                            MaxLength="6" onkeyup="validateNumeric(this);" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Urban</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdobtn_urban" runat="server" TabIndex="90" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Yes&nbsp;&nbsp;" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;No" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mother Tongue</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlMotherToungeNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Select Mother Tounge" TabIndex="81" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Other Language</label>
                                                        </div>

                                                        <asp:TextBox ID="txtOtherLangauge" CssClass="form-control" runat="server" ToolTip="Please Enter Other Language" onkeypress="return alphaOnly(event);"
                                                            TabIndex="82" MaxLength="16"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                            TargetControlID="txtOtherLangauge" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Identification Mark</label>
                                                        </div>

                                                        <asp:TextBox ID="txtIdentiMark" runat="server" CssClass="form-control" ToolTip="Please Enter Identy Mark" onkeypress="return alphaOnly(event);"
                                                            TabIndex="91" MaxLength="32" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server"
                                                            TargetControlID="txtIdentiMark" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Height (In inch)</label>
                                                        </div>

                                                        <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                            ToolTip="Please Enter Height" MaxLength="3" TabIndex="88" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Weight (In Kg)</label>
                                                        </div>

                                                        <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                            ToolTip="Please Enter Weight (In Kg)" MaxLength="3" TabIndex="89" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Passport No.</label>
                                                        </div>

                                                        <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control" ToolTip="Please Enter Passport No." MaxLength="24"
                                                            TabIndex="92"></asp:TextBox>
                                                    </div>

                                                    <div class="form-group col-md-3" style="display: none">
                                                        <label>Birth Village</label>
                                                        <asp:TextBox ID="txtBirthVillage" runat="server" CssClass="form-control" ToolTip="Please Enter Birth Village" onkeypress="return alphaOnly(event);"
                                                            TabIndex="83" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server"
                                                            TargetControlID="txtBirthVillage" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-3" style="display: none">
                                                        <label>Birth Tehsil </label>
                                                        <asp:TextBox ID="txtBirthTaluka" runat="server" CssClass="form-control" ToolTip="Please Enter Birth Taluka" onkeypress="return alphaOnly(event);"
                                                            TabIndex="84" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server"
                                                            TargetControlID="txtBirthTaluka" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-3" style="display: none">
                                                        <label>Birth District </label>
                                                        <asp:TextBox ID="txtBirthDistrict" runat="server" CssClass="form-control" ToolTip="Please Enter Birth District" onkeypress="return alphaOnly(event);"
                                                            TabIndex="85" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server"
                                                            TargetControlID="txtBirthDistrict" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-md-3" style="display: none;">
                                                        <label>Country Domicile</label>
                                                        <asp:TextBox ID="txtCountryDomicile" runat="server" CssClass="form-control" ToolTip="Please Enter Country Domicile"
                                                            TabIndex="92" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                            TargetControlID="txtCountryDomicile" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="1234567890" />

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Goa/Non Goa</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlGoaNonGoa" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Select Goa/Non Goa" Visible="false">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Goa</asp:ListItem>
                                                            <asp:ListItem Value="2">Non Goa</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Td1" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Years of Study in Goa</label>
                                                        </div>

                                                        <asp:TextBox ID="txtyears_study" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                            Visible="false"></asp:TextBox>
                                                    </div>


                                                    <asp:Panel ID="pnlvisa" runat="server" Visible="false">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Visa No.</label>
                                                            </div>
                                                            <asp:TextBox ID="txtVisaNo" runat="server" CssClass="form-control" ToolTip="Please Enter Visa No."></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>


                                                    <div class="form-group col-md-8" style="display: none;">
                                                        <label>Remark</label>
                                                        <asp:TextBox ID="txtRemark" runat="server" Rows="3" CssClass="form-control" TextMode="MultiLine"
                                                            Height="51px" ToolTip="Please Enter Remark" ValidationGroup="Academic"></asp:TextBox>
                                                    </div>

                                                    <asp:Panel ID="pnlspecialization" runat="server" Visible="false">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Specialization</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" ToolTip="Please Enter Specialization" />
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <div class="row mt-3">
                                                    <div class="col-md-12">
                                                        <div class="sub-heading">
                                                            <h5><b>Bank Details</b></h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                         <%--   <sup>* </sup>--%>
                                                            <label>Bank Name</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="True" data-select2-enable="true"
                                                            ToolTip="Please Select Bank" TabIndex="93">
                                                        </asp:DropDownList>
                                                       <%-- <asp:RequiredFieldValidator ID="rfvddlBank" runat="server" ControlToValidate="ddlBank"
                                                            Display="None" ErrorMessage="Please select Bank" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                          <%--  <sup>* </sup>--%>
                                                            <label>Bank Account No</label>
                                                        </div>

                                                        <%--   <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" ToolTip="Please Enter Account No."
                                                TabIndex="94" MaxLength="20"></asp:TextBox>--%>
                                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" ToolTip="Please Enter Account No."
                                                            TabIndex="94" MaxLength="24"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                                            TargetControlID="txtAccNo" FilterType="Numbers" />

<%--                                                        <asp:RequiredFieldValidator ID="rfvtxtAccNo" runat="server" ControlToValidate="txtAccNo"
                                                            Display="None" ErrorMessage="Please Enter Bank Account Number" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                         <%--   <sup>* </sup>--%>
                                                            <label>IFSC Code</label>
                                                        </div>

                                                        <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" ToolTip="Please Enter IFSC Code"
                                                            TabIndex="95" MaxLength="11" Style="text-transform: uppercase"></asp:TextBox>
                                                        <%-- //Added by sachin chaange in maxlength and validation alpanumric on 29-07-2022--%>
                                                       <%-- <asp:RequiredFieldValidator ID="rfvtxtIFSC" runat="server" ControlToValidate="txtIFSC"
                                                            Display="None" ErrorMessage="Please Enter IFSC Code" SetFocusOnError="True" 
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>

                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtIFSC" />--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                          <%--  <sup>* </sup>--%>
                                                            <label>Bank Address</label>
                                                        </div>

                                                        <asp:TextBox ID="txtBankAddress" runat="server" CssClass="form-control" ToolTip="Please Enter Bank Address"
                                                            TabIndex="96" TextMode="MultiLine" MaxLength="80" Rows="1"></asp:TextBox>
                                                       <%-- <asp:RequiredFieldValidator ID="rfvtxtBankAddress" runat="server" ControlToValidate="txtBankAddress"
                                                            Display="None" ErrorMessage="Please Enter Bank Address" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>

                                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtAccNo" FilterType="UppercaseLetters" />--%>
                                                    </div>

                                                </div>
                                                <asp:Panel ID="pnlAntiRagging" runat="server">
                                                    <div class="row mt-3">
                                                        <div class="col-md-12">
                                                            <div class="sub-heading">
                                                                <h5><b>Anti Ragging</b></h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                            </div>
                                                            <asp:CheckBox ID="chkAntiRagging" runat="server" />
                                                            I Agree

                                                        <button type="button" class="btn btn-primary ml-3" data-toggle="modal" data-target="#myModal_Declaration">
                                                            View Declaration
                                                        </button>
                                                        </div>

                                                    </div>
                                                </asp:Panel>
                                                <div class="row mt-3">
                                                    <div class="col-md-12">
                                                        <div class="sub-heading">
                                                            <h5><b>Sport Achievement Information</b></h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Sports Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSportName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSportName"
                                                            Display="None" ErrorMessage="Please Enter Sports Name" SetFocusOnError="True"
                                                            ValidationGroup="Sport"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Sports Level</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtSportLevel" runat="server" CssClass="form-control" ToolTip="Please Enter Sports Level"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlSportLevel" runat="server" CssClass="form-control" ToolTip="Please Select Sports Level" AppendDataBoundItems="true" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">District</asp:ListItem>
                                                            <asp:ListItem Value="2">State</asp:ListItem>
                                                            <asp:ListItem Value="3">National</asp:ListItem>
                                                            <asp:ListItem Value="4">International</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSportLevel"
                                                            Display="None" ErrorMessage="Please Enter Sports Level" SetFocusOnError="True"
                                                            ValidationGroup="Sport"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Sports Achievement Details</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSportAchieve" runat="server" TextMode="MultiLine" TabIndex="98" Rows="1" CssClass="form-control" ToolTip="Please Enter Designation."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSportAchieve"
                                                            Display="None" ErrorMessage="Please Enter Sports Achievement Details" SetFocusOnError="True"
                                                            ValidationGroup="Sport"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddSport" runat="server" Text="Add" TabIndex="99" CssClass="btn btn-primary" OnClick="btnAddSport_Click" ValidationGroup="Sport" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="Sport" />
                                                    </div>

                                                </div>


                                                <div class="col-md-12">
                                                    <asp:ListView ID="lvSport" runat="server">
                                                        <LayoutTemplate>
                                                            <%-- <div class="vista-grid">--%>
                                                            <div id="demo-grid">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit
                                                                            </th>
                                                                            <th>Delete
                                                                            </th>
                                                                            <th>Sport Name
                                                                            </th>
                                                                            <th>Sport Level
                                                                            </th>
                                                                            <th>Sport Achievement Details
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
                                                            <tr class="item">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditSportDetail" runat="server" OnClick="btnEditSportDetail_Click"
                                                                        CommandArgument='<%# Eval("SPORT_SRNO") %>' ImageUrl="~/images/edit1.gif" />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:ImageButton ID="btnDeleteSportDetail" runat="server" OnClick="btnDeleteSportDetail_Click"
                                                                        CommandArgument='<%# Eval("SPORT_SRNO") %>' ImageUrl="~/images/delete.png" ToolTip='<%# Eval("IDNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SPORT_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SPORT_LEVEL")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ACHIEVEMENT_DETAILS")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>

                                            <div class="row" style="display: none;">
                                                <div class="col-md-12">
                                                    <div class="sub-heading">
                                                        <h5><b>Work Information</b></h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label>Work Experience</label>
                                                    <asp:TextBox ID="txtworkexp" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvworkexp" runat="server" ControlToValidate="txtworkexp"
                                                        Display="None" ErrorMessage="Please Enter Work Experience" SetFocusOnError="True"
                                                        ValidationGroup="Work"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>Organization Last Worked For</label>
                                                    <asp:TextBox ID="txtorgwork" runat="server" CssClass="form-control" ToolTip="Please Enter Organization Last Worked For"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvOrg" runat="server" ControlToValidate="txtorgwork"
                                                        Display="None" ErrorMessage="Please Enter Organization" SetFocusOnError="True"
                                                        ValidationGroup="Work"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3">
                                                    <label>Designation</label>
                                                    <asp:TextBox ID="txtdesignation" runat="server" TabIndex="98" CssClass="form-control" ToolTip="Please Enter Designation."></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDesg" runat="server" ControlToValidate="txtdesignation"
                                                        Display="None" ErrorMessage="Please Enter Designation" SetFocusOnError="True"
                                                        ValidationGroup="Work"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-3" style="display: none">
                                                    <label>Total Work Experience</label>
                                                    <asp:TextBox ID="txttotalexp" runat="server" CssClass="form-control" ToolTip="Please Enter Total Work Experience."></asp:TextBox>

                                                </div>
                                                <div class="form-group col-md-2">
                                                    <label>EPF Number</label>
                                                    <asp:TextBox ID="txtepfno" runat="server" CssClass="form-control" ToolTip="Please Enter EPF Number." MaxLength="20"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer" style="display: none;">
                                                <asp:Button ID="btnadd" runat="server" Text="Add" TabIndex="99" CssClass="btn btn-primary" OnClick="btnadd_Click" ValidationGroup="Work" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Work" />
                                            </div>
                                            <%--   <br />--%>
                                            <div class="row" style="display: none;">
                                                <div class="container-fluid">
                                                    <div class="col-md-12">
                                                        <asp:ListView ID="lvExperience" runat="server">
                                                            <LayoutTemplate>
                                                                <%-- <div class="vista-grid">--%>
                                                                <div id="demo-grid">
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Delete
                                                                                </th>
                                                                                <th>Work Experience
                                                                                </th>
                                                                                <th>Organization Last Worked For
                                                                                </th>
                                                                                <th>Designation
                                                                                </th>
                                                                                <th>EPF Number
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
                                                                <tr class="item">
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEditexpDetail" runat="server" OnClick="btnEditexpDetail_Click"
                                                                            CommandArgument='<%# Eval("EXP_INC") %>' ImageUrl="~/images/edit1.gif" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnDeleteworkDetail" runat="server" OnClick="btnDeleteworkDetail_Click"
                                                                            CommandArgument='<%# Eval("EXP_INC") %>' ImageUrl="~/images/delete.png" ToolTip='<%# Eval("IDNO") %>' />
                                                                    </td>
                                                                    <td id="qualifyno" runat="server">
                                                                        <%# Eval("WORK_EXP")%>
                                                                    </td>
                                                                    <td id="year_of_exam" runat="server">
                                                                        <%# Eval("ORG_LAST_WORK")%>
                                                                    </td>
                                                                    <td id="school_college_name" runat="server">
                                                                        <%# Eval("DESIGNATION")%>
                                                                    </td>
                                                                    <td id="Td2" runat="server">
                                                                        <%# Eval("EPFNO")%>
                                                                    </td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSave" runat="server" class="btn btn-primary" Text="Save" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnSubmit" runat="server" TabIndex="100" Text="Final Submit" ToolTip="Click to Report" OnClientClick=" return Confirmation();"
                                                    class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Academic" />

                                                <button runat="server" id="btnGohome" visible="false" tabindex="101" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                                    Go Back Home
                                                </button>
                                            </div>
                                        </div>

                                        <!-- The Modal -->
                                        <div class="modal fade" id="myModal_Declaration">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">

                                                    <!-- Modal Header -->
                                                    <div class="modal-header">
                                                        <h4 class="modal-title">Undertaking By The Student (Anti-Ragging)</h4>
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    </div>

                                                    <!-- Modal body -->
                                                    <div class="modal-body">
                                                        <div class="col-12">
                                                            <div class="del-content">
                                                                <p class="del-cont-text">
                                                                    <%--1) I, having Been admitted to  MIT Academy of Engineering Alandi  Have received a copy of the UGC regulations--%>
                                                                    1) I, having Been admitted to<asp:Label ID="lblCollegeName" runat="server" Font-Bold="true"></asp:Label>
                                                                    Have received a copy of the UGC regulations
                                                                    on curbing the menace of Ragging in Higher Educational Institution, 2009,
                                                                    (hereinafter called the “Regulations”) carefully read and fully understood the provisions 
                                                                   contained in the said regulations.  
                                                                </p>

                                                                <p class="del-cont-text">
                                                                    2) I have, in particular, perused clause 3 of the Regulations and am aware as to what constitutes ragging.
                                                                </p>

                                                                <p class="del-cont-text">
                                                                    3) I have also, in particular, perused clause7 and clause9.1 of the Regulations and am fully aware of the penal 
                                                                    and administrative action that is liable to be taken against me in case I am found guilty of or  abetting ragging,
                                                                     actively or passively, or being part of a conspiracy to promote ragging.
                                                                </p>

                                                                <p class="del-cont-text">
                                                                    4) I hereby solemnly aver and undertake that
                                                                    <br />
                                                                    <span>a)I will not indulge in any behavior or act that may be constituted as ragging under clause 3 of the Regulations.
                                                                    </span>
                                                                    <br />
                                                                    <span>b)I will not participate in or abet or propagate through any act of commission or omission that may be constituted
                                                                         as ragging under clause 3 of the Regulations.
                                                                    </span>
                                                                </p>

                                                                <p class="del-cont-text">
                                                                    5)   I hereby affirm that, if found guilty of ragging, I am liable for punishment according to clause 9.1 of the regulations, 
                                                                    without prejudice to any other criminal action that may be taken against me under any penal law or any law for the time being in force. 
                                                                </p>

                                                                <p class="del-cont-text">
                                                                    6)   I hereby declare that I have not been expelled or debarred from admission in any institution in the country on account of being 
                                                                    found guilty of abetting or being part of a conspiracy to promote ragging and further affirm that in case the declaration is found to be untrue, 
                                                                    I am aware that my admission is liable to be cancelled.
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <!-- Modal footer -->
                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                    </div>

                                                </div>
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
        </Triggers>
    </asp:UpdatePanel>
    <script>
        function myFunction() {
            if (confirm("Are you sure!you want to delete !!!")) {
                return true;
            }
            else {
                return false;
            }

        }
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

        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }

        function Confirmation() {                                               //Added by sachin on 22-07-2022
            var isPageValid = Page_ClientValidate('Academic');
            if (isPageValid) {


               

                if (document.getElementById("hfdParamValue").value == 1) {
                    if ($("[id$='chkAntiRagging']").prop('checked') == false) {
                        alert('Please Submit Anti Ragging Declaration !');
                        return false;
                    }
                }


               

                if (confirm("Are you sure, do you really want to submit information?\nOnce submitted it cannot be modified again.") == true)
                    return true;
                else
                    return false;

            }
        }

    
        


        $("#txtIFSC").keydown(function (e) {
            var k = e.keyCode || e.which;
            var ok = k >= 65 && k <= 90 || // A-Z
                k >= 96 && k <= 105 || // a-z
                k >= 35 && k <= 40 || // arrows
                k == 9 || //tab
                k == 46 || //del
                k == 8 || // backspaces
                (!e.shiftKey && k >= 48 && k <= 57); // only 0-9 (ignore SHIFT options)

            if (!ok || (e.ctrlKey && e.altKey)) {
                e.preventDefault();
            }
        });


        //Added by sachin validation IFCCODE BY 29-07-2022
        $(function () {                                                 
            $('#<%=txtIFSC.ClientID %>').keypress(function (e) {
                var keyCode = e.keyCode || e.which;

                $("#lblError").html("");

                //Regex for Valid Characters i.e. Alphabets and Numbers.
                var regex = /^[A-Za-z0-9]+$/;

                //Validate TextBox value against the Regex.
                var isValid = regex.test(String.fromCharCode(keyCode));
                if (!isValid) {
                    $("#lblError").html("Only Alphabets and Numbers allowed.");
                }

                return isValid;
            });
        });
 





        //function Confirmation() {
        //    var isPageValid = Page_ClientValidate('Academic');
        //    if (isPageValid) {
        //        if (confirm("Are you sure, do you really want to submit information?\nOnce submitted it cannot be modified again.") == true)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
    </script>
    <div id="divMsg" runat="server"></div>

</asp:Content>
