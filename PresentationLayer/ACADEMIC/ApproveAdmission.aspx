<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ApproveAdmission.aspx.cs" Inherits="ACADEMIC_ApproveAdmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updApproveAdmission"
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

    <asp:UpdatePanel ID="updApproveAdmission" runat="server">
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
                                                                ToolTip="Please select Other Information" OnClick="lnkotherinfo_Click" Text="Other Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview active" id="divAdmissionApproval" runat="server">
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
                                                <div class="col-md-12" id="Div1" runat="server">
                                                    <div class="sub-heading">
                                                        <h5>Other Personal Information</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Information Verified </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmStatus" CssClass="form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmStatus_SelectedIndexChanged" AutoPostBack="true"
                                                        ToolTip="Please Select  Information Verification Status" TabIndex="1" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Approve</asp:ListItem>
                                                        <asp:ListItem Value="2">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlAdmStatus" runat="server" ControlToValidate="ddlAdmStatus"
                                                        Display="None" ErrorMessage="Please Select Information Verification Status" SetFocusOnError="True"
                                                        ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divReason" visible="false" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Reason </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReason" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Reason" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Personal Information Related</asp:ListItem>
                                                        <asp:ListItem Value="2">Address Details Related</asp:ListItem>
                                                        <asp:ListItem Value="3">Upload Documents Related</asp:ListItem>
                                                        <asp:ListItem Value="4">Photo and Signature Related</asp:ListItem>
                                                        <asp:ListItem Value="5">Nationality Related</asp:ListItem>
                                                        <asp:ListItem Value="6">Qualification Details Related</asp:ListItem>
                                                        <asp:ListItem Value="7">10th Qualification Related</asp:ListItem>
                                                        <asp:ListItem Value="8">12th Qualification Related</asp:ListItem>
                                                        <asp:ListItem Value="9">Diploma Qualification Related</asp:ListItem>
                                                        <asp:ListItem Value="10">Course Related</asp:ListItem>
                                                        <asp:ListItem Value="11">Eligibility Criteria Related</asp:ListItem>
                                                        <asp:ListItem Value="12"> Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReason"
                                                        Display="None" ErrorMessage="Please Select Reason" SetFocusOnError="True"
                                                        ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRemark" visible="false" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Remark </label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="240" TabIndex="5" ToolTip="Please Enter Remark" />
                                                    <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark"
                                                        Display="None" ErrorMessage="Please Enter Remark" SetFocusOnError="True"
                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <span id="divStatus" runat="server" visible="false"></span>

                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit Status" ToolTip="Click to Submit"
                                                    class="btn btn-primary" OnClick="btnSubmit_Click" ValidationGroup="Submit" />

                                                <asp:Button ID="btnreport" runat="server" TabIndex="7" Text="Print Application" ToolTip="Click to Print Application"
                                                    class="btn btn-info" OnClick="btnreport_Click" Visible="true" />

                                                <button runat="server" id="btnGohome" visible="false" tabindex="8" onserverclick="btnGohome_Click" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                                    Go Back Home
                                                </button>

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Submit" />

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
                                                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
    </asp:UpdatePanel>
</asp:Content>

