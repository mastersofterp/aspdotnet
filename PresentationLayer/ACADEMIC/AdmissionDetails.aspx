<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmissionDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_AdmissionDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAdmissionDetails"
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
    </style>

    <asp:UpdatePanel ID="updAdmissionDetails" runat="server">
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

                                                    <li class="treeview active" id="divadmissiondetailstreeview" runat="server">
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


                                    <div class="col-lg-10 col-md-8 col-12" id="AdmDetails" runat="server">
                                        <div class="col-12 pl-0 pr-0 pl-lg-2 pr-lg-2">
                                            <div class="row" id="trAdmission" runat="server">

                                                <div class="col-md-12">
                                                    <div class="sub-heading">
                                                        <h5>Admission Details</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-8 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note </h5>
                                                        <p>
                                                            <i class="fa fa-star" aria-hidden="true"></i><span>This Information is very critical. A seperate permission
                                    is required to change this critical data.</span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divAdmissionDetails" runat="server" style="display: block;">
                                                <div class="row" id="tbladmission">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date of Admission</label>
                                                        </div>

                                                        <div class="input-group">
                                                            <%--<div class="input-group-addon"style="display:none;">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>--%>
                                                            <asp:TextBox ID="txtDateOfAdmission" runat="server" ReadOnly="true" ToolTip="Please Enter Date Of Addmission"
                                                                TabIndex="1" CssClass="noteditable form-control" />
                                                            <%-- <asp:Image ID="imgAdmDate" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                                        Height="16px" />--%>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateOfAdmission" PopupButtonID="imgAdmDate" Enabled="false">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeAdmDate" runat="server" TargetControlID="txtDateOfAdmission"
                                                                Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                                CultureTimePlaceholder="" Enabled="false" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>School/Institute Admitted </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchoolCollege" runat="server" ValidationGroup="Academic" AutoPostBack="true"
                                                            CssClass="noteditable form-control" AppendDataBoundItems="true" ToolTip="Please Select School/Institute Admitted"
                                                            TabIndex="2" Enabled="False" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" data-select2-enable="true">
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlSchoolCollege"
                                                    InitialValue="0" Display="None" SetFocusOnError="True" ErrorMessage="Please Select School Admitted"
                                                    ValidationGroup="Academic">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Degree </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                            ToolTip="Please Select Degree" TabIndex="3" ValidationGroup="Academic" Enabled="false"
                                                            CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true" />
                                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                    InitialValue="0" Display="None" SetFocusOnError="True" ErrorMessage="Please Select Degree"
                                                    ValidationGroup="Academic">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Programme/Branch </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" ValidationGroup="Academic"
                                                            CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please Select Programme/Branch"
                                                            TabIndex="4" Enabled="False" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Please Select Branch" Display="None" ValidationGroup="Academic"
                                                    SetFocusOnError="true" InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Admission Batch </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Select Batch" TabIndex="5" ValidationGroup="Academic" Enabled="false"
                                                            CssClass="form-control" data-select2-enable="true" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Batch" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Enter Year" TabIndex="6" ValidationGroup="Academic" CssClass="form-control"
                                                            Enabled="False" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                    Display="None" ErrorMessage="Please Select Year" SetFocusOnError="True" ValidationGroup="Academic"
                                                    InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Semester </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                            ToolTip="Please select Semester" TabIndex="7" ValidationGroup="Academic" CssClass="form-control"
                                                            Enabled="False" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Semester" ValidationGroup="Academic"
                                                    InitialValue="0">
                                                </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Admission Category </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlclaim" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Enter Caste Category"
                                                            ValidationGroup="Academic" Enabled="False">
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlclaim"
                                        Display="None" SetFocusOnError="True" ErrorMessage="Please Select Admission Category" ValidationGroup="Academic"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Academic Year </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Select Academic Year"
                                                            ValidationGroup="Academic" Enabled="False">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Alloted Category</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="True" TabIndex="9" ToolTip="Please Enter Alloted Category"
                                                            ValidationGroup="Academic" Enabled="False">
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvPayType" runat="server" ControlToValidate="ddlPaymentType"
                                        Display="None" SetFocusOnError="True" ErrorMessage="Please Select Alloted Category" ValidationGroup="Academic"
                                        InitialValue="0">
                                    </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>State of Eligibility</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlStateOfEligibility" runat="server" TabIndex="10" AppendDataBoundItems="True"
                                                            ToolTip="Please Select State Of Eligibility" ValidationGroup="Academic"
                                                            Enabled="False" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Hosteler</label>
                                                        </div>

                                                        <div class="radio">
                                                            <label>
                                                                <asp:RadioButton ID="rdoHostelerYes" runat="server" Text="Yes" GroupName="Hosteler"
                                                                    Enabled="False" />
                                                            </label>

                                                            <label>
                                                                <asp:RadioButton ID="rdoHostelerNo" runat="server" Text="No" GroupName="Hosteler"
                                                                    Checked="True" Enabled="False" />
                                                            </label>

                                                        </div>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Document List</label>
                                                        </div>
                                                        <asp:CheckBoxList ID="chkDoc" runat="server" BorderColor="#FF9900" BorderStyle="Solid"
                                                            BorderWidth="1px" CellPadding="2" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal"
                                                            Font-Size="8pt" CssClass="form-control">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" TabIndex="11" Text="Save & Continue >>" ToolTip="Click to Submit"
                                                CssClass="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="Academic" />

                                            <button runat="server" id="btnGohome" tabindex="12" visible="false" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                                Go Back Home
                                            </button>
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
    <div id="divMsg" runat="server"></div>
</asp:Content>
