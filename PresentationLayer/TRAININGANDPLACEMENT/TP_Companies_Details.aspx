<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TP_Companies_Details.aspx.cs" Inherits="TRAININGANDPLACEMENT_TP_Companies_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hfDiscipline" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfCurriculum" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfVisFac" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfIndVisit" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfGuestlect" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hffacultylinkind" runat="server" ClientIDMode="Static" />
      <asp:HiddenField ID="hfFPTI" runat="server" ClientIDMode="Static" />
       <asp:HiddenField ID="hfFOOI" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hfEPAI" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hfFTI" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfFPP" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfPAIF" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfSev" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hfSSE" runat="server" ClientIDMode="Static" />

    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 5px 0 0;
        }

        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        .company-logo img {
            width: 26px;
        }

        .MyLRMar {
            margin-left: 5px;
            margin-right: 5px;
        }

        #datepicker, #pickerServices, #pickerCurriculam, #pickerExecutive, #pickerIndustry {
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: 1px solid #ccc;
            height: 30px !important;
        }
    </style>

    <style>
        .nav-tabs-custom .nav.nav-tabs {
            display: block;
            box-shadow: rgba(0, 0, 0, 0.2) 0px 5px 10px;
            padding: 8px;
        }

        .nav-tabs-custom .nav-tabs .nav-link {
            border: 0px solid transparent;
            border-left: 2px solid #e8eaed;
            color: #255282;
        }

            .nav-tabs-custom .nav-tabs .nav-item.show .nav-link, .nav-tabs-custom .nav-tabs .nav-link.active {
                color: #0d70fd;
                background-color: #fff;
                border: 0px solid transparent;
                border-left: 2px solid #255282;
                border-color: #ffffff #fff #fff #0d70fd;
                border-top-left-radius: 0rem;
                border-top-right-radius: 0rem;
                padding: .4rem 1rem;
            }

            .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
                color: #0d70fd;
                border: 0px solid transparent;
                border-left: 2px solid #0d70fd;
                border-color: #ffffff #fff #fff #0d70fd;
                border-top-left-radius: 0rem;
                border-top-right-radius: 0rem;
            }

        @media (max-width: 767px) {
            .nav-tabs-custom .nav-tabs .nav-link {
                padding: .2rem .6rem;
            }
        }

        input[type=checkbox], input[type=radio] {
            margin: 0 4px 0 0;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Company Details</span></h3>
                </div>

                 <div id="Tabs" role="tabpanel">
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-3 col-md-4 col-12">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_0">Companies </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_1">Discipline </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2">Curriculum Input </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_3">Visiting Faculties From Industries/ Expert Lecture </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_4">Industrial Visits </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_5">Guest lecture / Alumni Talk </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_6">Faculty Linked To Industry </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_7">Faculty Providing Training To industry </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_8">Faculty Onboard Of industry </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_9">Executive program Attend By Industry </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_10">Faculty Trained By Industry </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_11">Faculty Patents Leading To Industry Products </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_12">Papers Authored To Industry By Faculty </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_13">Services </a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_14">Student Self-Employment </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-9 col-md-8 col-12 pl-0 pr-0">
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="tab_0">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Company</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="ddlCompanyName" ValidationGroup="Company"
                                                            ErrorMessage="Please select Company Name " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="rfvSector" runat="server" ControlToValidate="ddlSector" ValidationGroup="Company"
                                                            ErrorMessage="Please select Sector Name " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIncorpStatus" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobno" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtManager" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" />
                                                         <asp:RegularExpressionValidator ID="rfvEmailId" runat="server" ControlToValidate="txtEmailid"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Company"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                <asp:LinkButton ID="btnSubmitCompanies" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitCompanies_Click" OnClientClick="return validate1()"  ValidationGroup="Company">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelCompanies" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelCompanies_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummaryCompany" runat="server" ValidationGroup="Company"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                          </div>

                                              <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvTPCompanies" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector
                                                                        </th>
                                                                        <th>Website
                                                                        </th>
                                                                        <th>Mobile No
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPCompaniesEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="1" OnClick="btnTPCompaniesEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("WEBSITE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("MOBILE_NO")%>'></asp:Label></td>
                                                                
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_1">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Discipline</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDiscipline" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtDiscipline" ValidationGroup="Discipline"
                                                            ErrorMessage="Please Enter Discipline " SetFocusOnError="true"  Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                      <%--  <asp:TextBox ID="txtLevel" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddllevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddllevel" ValidationGroup="Discipline"
                                                            ErrorMessage="Please select Level " SetFocusOnError="true" InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Year Of Inception</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtYearOfInception" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="4" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Number Of Substream</label>
                                                        </div>
                                                        <asp:TextBox ID="txtNumberOfSubstream" runat="server" CssClass="form-control" MaxLength="8" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Number Of Faculties</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFaculties" runat="server" CssClass="form-control"  MaxLength="8" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Total Number Of Final Year Students Eligible For Placement</label><%--  (2020 Batch)--%>
                                                        </div>
                                                        <asp:TextBox ID="txtStudenteligible" runat="server" CssClass="form-control"  MaxLength="8" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Total Number Of Final Year Students Eligible For Placement </label><%--(2021 Batch)--%>
                                                        </div>
                                                        <asp:TextBox ID="txtDiscStudBatch" runat="server" CssClass="form-control"  MaxLength="8" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdDiscipline" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdDiscipline"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
<%--                                                <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                 <asp:LinkButton ID="btnSubmitDiscipline" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitDiscipline_Click" OnClientClick="return validate2()" ValidationGroup="Discipline">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelDiscipline" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelDiscipline_Click">Cancel</asp:LinkButton>

                                                <asp:ValidationSummary ID="ValidationSummaryDiscipline" runat="server" ValidationGroup="Discipline" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvDiscipline" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Discipline</th>
                                                                        <th>Level
                                                                        </th>
                                                                        <th>Year Of Inception
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPDisciplineEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("ID") %>' TabIndex="1" OnClick="btnTPDisciplineEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("DISCRIPONLINE")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("LEVELS")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("YEAR_OF_INCEPTION")%>'></asp:Label></td>
                                                              
                                                                
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_2">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Curriculum Input</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlcomp2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server"
                                                            ControlToValidate="ddlcomp2" InitialValue="0" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Select Company Name." SetFocusOnError="true" ValidationGroup="CurIP">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlsector2" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                            ControlToValidate="ddlsector2" InitialValue="0" EnableClientScript="true" Display="None"
                                                            ErrorMessage="Please Select Company Sector." SetFocusOnError="true" ValidationGroup="CurIP">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Incorporation Sector</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIncSector" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtCurriculamAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtCurriculamWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtManger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailId1" runat="server" CssClass="form-control" />
                                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId1"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="CurIP"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtCurriculamDiscipline" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlCurrDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Level</label>
                                                        </div>
                                                      <%--  <asp:TextBox ID="txtlevel1" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlCurrlevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                 <%--   <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date From To</label>
                                                        </div>
                                                        <div id="pickerCurriculam" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="dateCurriculam"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>--%>
                                                      <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>From Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon" id="Div2">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" ToolTip="Enter Event From Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div1" TargetControlID="txtFromDate" />--%>
                                                                  <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div2" TargetControlID="txtFromDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFromDate" />
                                                           <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidatorCur" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender4" ControlToValidate="txtFromDate" IsValidEmpty="true"
                                                               InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="CurIP" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="CurIP"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>To Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Div4">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtToDate" runat="server" TabIndex="8" ToolTip="Enter Event To Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div4" TargetControlID="txtToDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtToDate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidatorToCur" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender5" ControlToValidate="txtFromDate" IsValidEmpty="true"
                                                               InvalidValueMessage="To Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="CurIP" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtToDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="CurIP"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>No. Of Students</label>
                                                        </div>
                                                        <asp:TextBox ID="txtNoofStudent" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"/>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator98" runat="server" ControlToValidate="txtNoofStudent"
                                                                    Display="None" ErrorMessage="Please Enter No. Of Students." ValidationGroup="CurIP"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdCurriculum" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdCurriculum"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="btnCurrSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnCurrSubmit_Click"/>
                                                <asp:Button ID="btnCurrCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCurrCancel_Click" />--%>
                                                 <asp:LinkButton ID="btnCurrSubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnCurrSubmit_Click" OnClientClick="return validate3()" ValidationGroup="CurIP">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCurrCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCurrCancel_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummaryCurriculumInput" runat="server" ValidationGroup="CurIP" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>

                                               <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvCurriculum" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Company Sector
                                                                        </th>
                                                                        <th>From Date
                                                                        </th>
                                                                        <th>To Date
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPCurreculumEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("CURR_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("CURR_ID") %>' TabIndex="1" OnClick="btnTPCurreculumEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("FROM_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                              
                                                                  <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("TO_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="tab-pane fade" id="tab_3">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Visiting Faculties From Industries/ Expert Lecture</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlVISCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="rfvCollaborationYear" runat="server" ControlToValidate="ddlVISCompany"
                                                            Display="None" ErrorMessage="Please Select Company Name" InitialValue="0" SetFocusOnError="true" ValidationGroup="VFFIEL"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlVISSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlVISSector"
                                                            Display="None" ErrorMessage="Please Select Company Sector" InitialValue="0" SetFocusOnError="true" ValidationGroup="VFFIEL"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Incorporation Sector</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIncorpSector" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Designation</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>First Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Last Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVisitingAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVisitingWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobileNo2" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10" />
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVisitingManger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVisitingEmailId" runat="server" CssClass="form-control" />
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtVisitingEmailId"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="VFFIEL"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtVisitingDiscipline" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlVISDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtVisitinglevel" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlVISLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                <%--    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date Of Lecture</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVisitingDateOfLecture" Type="date" runat="server" CssClass="form-control" />
                                                    </div>--%>

                                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Date Of Lecture</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divlect">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtVisitingDateOfLecture" runat="server" TabIndex="8" ToolTip="Enter Date Of Lecture"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divlect" TargetControlID="txtVisitingDateOfLecture" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtVisitingDateOfLecture" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Valid Date Of Lecture"
                                                               ControlExtender="MaskedEditExtender1" ControlToValidate="txtVisitingDateOfLecture" IsValidEmpty="true"
                                                               InvalidValueMessage="Date Of Lecture is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date Of Lecture" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="VFFIEL" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator112" runat="server" ControlToValidate="txtVisitingDateOfLecture"
                                                                    Display="None" ErrorMessage="Please Enter Date Of Lecture." ValidationGroup="VFFIEL"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>No. Of Students</label>
                                                        </div>
                                                        <asp:TextBox ID="txtNoOFStud" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"/>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator97" runat="server" ControlToValidate="txtNoOFStud"
                                                                    Display="None" ErrorMessage="Please Enter No. Of Students." ValidationGroup="VFFIEL"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdVisFac" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdVisFac"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button7" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button8" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                
                                                <asp:LinkButton ID="btnSubmitVisVisitingFaculties" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitVisVisitingFaculties_Click" OnClientClick="return validate4()"  ValidationGroup="VFFIEL">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelVisitingFaculties" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelVisitingFaculties_Click">Cancel</asp:LinkButton>

                                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="VFFIEL"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                              <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvVisiting" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Company Sector
                                                                        </th>
                                                                        <th>Date Of Lecture
                                                                        </th>
                                                                        <th>No. Of Students
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPVisitingEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("VIS_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("VIS_ID") %>' TabIndex="1" OnClick="btnTPVisitingEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LECTURE_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                              
                                                                  <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_4">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Industrial Visits</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlIndcompny" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlIndcompny"
                                                            Display="None" ErrorMessage="Please Select Company Name" InitialValue="0" SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlInvSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlInvSector"
                                                            Display="None" ErrorMessage="Please Select Company Sector" InitialValue="0" SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Sector</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustrialIncorpSector" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtIndustrialIncorpSector"
                                                            Display="None" ErrorMessage="Please Enter Incorporation Sector" SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddress3" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustrialWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobNo3" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustrialManger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustrialEmailId" runat="server" CssClass="form-control" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtIndustrialEmailId"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="INDVIS"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtDiscipline33" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlIndDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlIndDiscipline"
                                                            Display="None" ErrorMessage="Please Select Discipline" InitialValue="0" SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                     <%--   <asp:TextBox ID="txtIndustriallevel" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlIndLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlIndLevel"
                                                            Display="None" ErrorMessage="Please Select Level" InitialValue="0" SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                  <%--  <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date From To</label>
                                                        </div>
                                                        <div id="pickerIndustry" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="dateIndustry"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>--%>
                                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>From Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon" id="DivInvf">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtIndfromdate" runat="server" TabIndex="8" ToolTip="Enter Event From Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div1" TargetControlID="txtFromDate" />--%>
                                                                  <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivInvf" TargetControlID="txtIndfromdate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtIndfromdate" />
                                                           <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender2" ControlToValidate="txtIndfromdate" IsValidEmpty="true"
                                                               InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="INDVIS" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIndfromdate"
                                                                    Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="INDVIS"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>To Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divto">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtIndtodate" runat="server" TabIndex="8" ToolTip="Enter Event To Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divto" TargetControlID="txtIndtodate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtIndtodate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender3" ControlToValidate="txtIndtodate" IsValidEmpty="true"
                                                               InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="INDVIS" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtIndtodate"
                                                                    Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="INDVIS"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>No. Of Students</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustrialNoOfStud" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"/>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtIndustrialNoOfStud"
                                                            Display="None" ErrorMessage="Please Enter No Of Students"  SetFocusOnError="true" ValidationGroup="INDVIS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdIndVisit" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdIndVisit"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                            <%--    <asp:Button ID="Button9" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button10" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                   <asp:LinkButton ID="btnSubmitIndVisit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitIndVisit_Click" OnClientClick="return validate5()" ValidationGroup="INDVIS">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnSubmitCancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnSubmitCancle_Click">Cancel</asp:LinkButton>

                                                     <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="INDVIS" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                            <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvIndVisit" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Company Sector
                                                                        </th>
                                                                        <th>No. Of Students
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPIndVisitEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("IV_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("IV_ID") %>' TabIndex="1" OnClick="btnTPIndVisitEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_5">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Guest lecture / Alumni Talk</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlGuestlectCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ControlToValidate="ddlGuestlectCompany"
                                                            ValidationGroup="GLAT" ErrorMessage="Please select Company Name. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlGuestlectsector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="ddlGuestlectsector"
                                                            ValidationGroup="GLAT" ErrorMessage="Please select Company Sector. " SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Sector</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestIncorpSector" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Designation</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestDesignation" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtGuestDesignation"
                                                            ValidationGroup="GLAT" ErrorMessage="Please select Designation. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>First Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestFirstName" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtGuestFirstName"
                                                            ValidationGroup="GLAT" ErrorMessage="Please Enter First Name. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Last Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestLastName" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtGuestLastName"
                                                            ValidationGroup="GLAT" ErrorMessage="Please Enter Last Name. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddress4" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestMobNo" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestManager" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmailId4" runat="server" CssClass="form-control" />
                                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtEmailId4"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="GLAT"></asp:RegularExpressionValidator>
                                                    </div>
                                                   <%-- <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date of lecture</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestDateofLecture" Type="date" runat="server" CssClass="form-control" />
                                                    </div>--%>
                                                             <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Date of lecture</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divgul">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtGuestDateofLecture" runat="server" TabIndex="8" ToolTip="Enter lecture Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divgul" TargetControlID="txtGuestDateofLecture" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtGuestDateofLecture" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" EmptyValueMessage="Please Enter Valid Lecture Date"
                                                               ControlExtender="MaskedEditExtender6" ControlToValidate="txtGuestDateofLecture" IsValidEmpty="true"
                                                               InvalidValueMessage="Lecture Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Lecture Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="GLAT" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtGuestDateofLecture"
                                                                    Display="None" ErrorMessage="Please Enter Lecture Date." ValidationGroup="GLAT"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Students Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtStudDiscipline" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlGuestlectDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlGuestlectDiscipline"
                                                            ValidationGroup="GLAT" ErrorMessage="Please Select Students Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtGusetLevel" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlGuestlectlevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlGuestlectlevel"
                                                            ValidationGroup="GLAT" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>No. Of Students Attended</label>
                                                        </div>
                                                        <asp:TextBox ID="txtGuestNoOfStudent" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"/>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtGuestNoOfStudent"
                                                            ValidationGroup="GLAT" ErrorMessage="Please Enter No Of Students Attended. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfGuestlect" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfGuestlect"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button11" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button12" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                 <asp:LinkButton ID="btnSubmitGuestlect" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitGuestlect_Click" OnClientClick="return validate6()" ValidationGroup="GLAT">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnClearguestlect" runat="server" CssClass="btn btn-outline-danger" OnClick="btnClearguestlect_Click">Cancel</asp:LinkButton>

                                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="GLAT" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" />
                                            </div>
                                                <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvguestlect" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Company Sector
                                                                        </th>
                                                                        <th>No. Of Students
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPGuestLectEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("GL_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("GL_ID") %>' TabIndex="1" OnClick="btnTPGuestLectEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("NUMBER_OF_STUDENT")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_6">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Faculty Linked To Industry</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlFaculty"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>External Faculty </label>
                                                        </div>
                                                        <asp:TextBox ID="txtExFaculty" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtExFaculty"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select External Faculty. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtlinkIndustryID" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtlinkIndustryID"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select Faculty Id. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Name Of  Company</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtFacultynameofComp" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlfacultyCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="ddlfacultyCompany"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select Name Of  Company. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustryAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtWebsite5" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustryMobNo" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustryManger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndustryEmaiId" runat="server" CssClass="form-control" />
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtIndustryEmaiId"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FLTI"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtIndustryDiscipline" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlFacultyDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="ddlFacultyDiscipline"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtIndustryLevel" runat="server" CssClass="form-control" />--%>
                                                           <asp:DropDownList ID="ddlFacultyLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlFacultyLevel"
                                                            ValidationGroup="FLTI" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rffacultylinkind" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rffacultylinkind"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="Button13" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button14" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>

                                                 <asp:LinkButton ID="btnSubmitfacultylinkindustry" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSubmitfacultylinkindustry_Click" OnClientClick="return validate7()" ValidationGroup="FLTI">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btncancelfacultylinkindustry" runat="server" CssClass="btn btn-outline-danger" OnClick="btncancelfacultylinkindustry_Click">Cancel</asp:LinkButton>

                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="FLTI"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                                 <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvfacultylink" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Faculty
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPfacultyEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FLI_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FLI_ID") %>' TabIndex="1" OnClick="btnTPfacultyEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("EXTERNAL_FACULTY")%>'></asp:Label></td>
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_7">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                     <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Faculty Providing Training To Industry</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPTICompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlFPTICompany"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPTISector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="ddlFPTISector"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacltyIncorpStatus" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="txtFacltyIncorpStatus"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPTIFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFPTIFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />--%>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="ddlFPTIFaculty"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyId" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtFacultyId"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Enter Faculty Id. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyMobNo" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultymanger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyEmailid" runat="server" CssClass="form-control" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtFacultyEmailid"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FPTI"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtfacultyDiscpline" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlFPTIDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="ddlFPTIDiscipline"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtFacultyLevel" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlFPTILevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="ddlFPTIFaculty"
                                                            ValidationGroup="FPTI" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                 <%--   <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date Of Lecture</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFacultyDateofLecture" Type="date" runat="server" CssClass="form-control" />
                                                    </div>--%>
                                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Date of lecture</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divfpti">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtFacultyDateofLecture" runat="server" TabIndex="8" ToolTip="Enter lecture Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divfpti" TargetControlID="txtFacultyDateofLecture" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFacultyDateofLecture" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" EmptyValueMessage="Please Enter Valid Lecture Date"
                                                               ControlExtender="MaskedEditExtender6" ControlToValidate="txtFacultyDateofLecture" IsValidEmpty="true"
                                                               InvalidValueMessage="Lecture Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Lecture Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="FPTI" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFacultyDateofLecture"
                                                                    Display="None" ErrorMessage="Please Enter Lecture Date." ValidationGroup="FPTI"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfFPTI" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfFPTI"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button15" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button16" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                <asp:LinkButton ID="btnsubmitFPTI" runat="server" CssClass="btn btn-outline-info"  OnClick="btnsubmitFPTI_Click" OnClientClick="return validate8()"  ValidationGroup="FPTI">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancleFPTI" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancleFPTI_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="FPTI"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                               <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvFPTI" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>Date Of Lecture
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPFPTIEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FPTI_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FPTI_ID") %>' TabIndex="1" OnClick="btnTPFPTIEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("DATE_OF_LECTURE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_8">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Faculty Onboard Of Industry</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFOOICompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="ddlFOOICompany"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFOOFSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="ddlFOOFSector"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardIncorpStatus" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ControlToValidate="txtOnboardIncorpStatus"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Type Of Board /Company</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardCompany" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ControlToValidate="txtOnboardCompany"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Enter Type Of Board / Company. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFOOIFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFOOIFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ControlToValidate="ddlFOOIFaculty"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardFacultyID" runat="server" CssClass="form-control" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ControlToValidate="txtOnboardFacultyID"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Enter Faculty Id. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnaboardWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardMobNo" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Manager/Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardManger" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardEmailId" runat="server" CssClass="form-control" />
                                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtOnboardEmailId"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FOOI"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtOnboardDisipline" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlFOOIDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="ddlFOOIDiscipline"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                      <%--  <asp:TextBox ID="txtONbboardLevel" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlFOOILevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ControlToValidate="ddlFOOILevel"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Member</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnboardLevel" runat="server" CssClass="form-control" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ControlToValidate="txtOnboardLevel"
                                                            ValidationGroup="FOOI" ErrorMessage="Please Enter Member. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfFOOI" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfFOOI"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="Button17" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button18" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                 <asp:LinkButton ID="btnFOOISubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnFOOISubmit_Click" OnClientClick="return validate9()" ValidationGroup="FOOI">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnFOOICancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnFOOICancle_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="FOOI"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                                <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="LvFOOI" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>Members
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPFOOIEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FOOI_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FOOI_ID") %>' TabIndex="1" OnClick="btnTPFOOIEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("MEMBER")%>'></asp:Label></td>
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_9">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Executive program Attend By Industry</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlEPAICompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ControlToValidate="ddlEPAICompany"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlEPAISector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlEPAISector"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtProgIncorpStatus" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="txtProgIncorpStatus"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Incorporation Status. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlEPAIFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEPAIFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="ddlEPAIFaculty"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtProgFacId" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="TxtProgFacId"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Enter Faculty Id. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtProgDiscipline" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlEPAIDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="ddlEPAIDiscipline"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                      <%--  <asp:TextBox ID="txtprogLevel" runat="server" CssClass="form-control" />--%>
                                                           <asp:DropDownList ID="ddlEPAILevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="ddlEPAILevel"
                                                            ValidationGroup="EPAI" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Program Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtProgramName" runat="server" CssClass="form-control" />
                                                    </div>
                                              <%--      <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date From - To</label>
                                                        </div>
                                                        <div id="pickerExecutive" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="dateExecutive"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>--%>
                                                       <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>From Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon" id="DivFEPAI">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtEPAIFromDate" runat="server" TabIndex="8" ToolTip="Enter Event From Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div1" TargetControlID="txtFromDate" />--%>
                                                                  <ajaxToolKit:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivFEPAI" TargetControlID="txtEPAIFromDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEPAIFromDate" />
                                                           <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender8" ControlToValidate="txtEPAIFromDate" IsValidEmpty="true"
                                                               InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="EPAI" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEPAIFromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="EPAI"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>To Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="DivTEPAI">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtEPAITODate" runat="server" TabIndex="8" ToolTip="Enter Event To Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivTEPAI" TargetControlID="txtEPAITODate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEPAITODate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" EmptyValueMessage="Please Enter Valid Event To Date"
                                                               ControlExtender="MaskedEditExtender9" ControlToValidate="txtEPAITODate" IsValidEmpty="true"
                                                               InvalidValueMessage="To Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="EPAI" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEPAITODate"
                                                                    Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="EPAI"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>No. Of Executives attending courses</label>
                                                        </div>
                                                        <asp:TextBox ID="txtProgExecutive" runat="server" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfEPAI" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfEPAI"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button19" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button20" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>

                                                 <asp:LinkButton ID="btnEPAISubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnEPAISubmit_Click" OnClientClick="return validate10()" ValidationGroup="EPAI">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnEPAICancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnEPAICancle_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="EPAI"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                             <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvEPAI" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>Program Name
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPEPAIEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("EPAI_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("EPAI_ID") %>' TabIndex="1" OnClick="btnTPEPAIEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PROGRAM_NAME")%>'></asp:Label></td>
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_10">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Faculty Trained By Industry</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFTICompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="ddlFTICompany"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFTISector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="ddlFTISector"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndCompIncorp" runat="server" CssClass="form-control" />
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="txtIndCompIncorp"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFTIFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFTIFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ControlToValidate="ddlFTIFaculty"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIndFacultyId" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="txtIndFacultyId"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Faculty Id. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtIndDiscipline" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlFTIDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ControlToValidate="ddlFTIDiscipline"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtIndLevel" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlFTILevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ControlToValidate="ddlFTILevel"
                                                            ValidationGroup="FTBI" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                               <%--     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Start Date End Date</label>--%>
                                                            <%--<asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>--%>
                                                       <%-- </div>
                                                        <div id="Div3" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                                 <span id="Span1"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>--%>
                                                              <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>From Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon" id="Divffti">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtETIfromDate" runat="server" TabIndex="8" ToolTip="Enter Event From Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <%--<ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Div1" TargetControlID="txtFromDate" />--%>
                                                                  <ajaxToolKit:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divffti" TargetControlID="txtETIfromDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtETIfromDate" />
                                                           <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator8" runat="server" EmptyValueMessage="Please Enter Valid Event From Date"
                                                               ControlExtender="MaskedEditExtender10" ControlToValidate="txtETIfromDate" IsValidEmpty="true"
                                                               InvalidValueMessage="From Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="FTBI" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtETIfromDate"
                                                                    Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="FTBI"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>To Date </label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divtfti">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtFTIToDate" runat="server" TabIndex="8" ToolTip="Enter Event To Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender11" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divtfti" TargetControlID="txtFTIToDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFTIToDate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator9" runat="server" EmptyValueMessage="Please Enter Valid Event To Date"
                                                               ControlExtender="MaskedEditExtender9" ControlToValidate="txtFTIToDate" IsValidEmpty="true"
                                                               InvalidValueMessage="To Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="FTBI" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtFTIToDate"
                                                                    Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="FTBI"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfFTI" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfFTI"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="Button21" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button22" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>

                                                 <asp:LinkButton ID="btnFTISubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnFTISubmit_Click" OnClientClick="return validate11()"  ValidationGroup="FTBI">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnFTICancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnFTICancle_Click">Cancel</asp:LinkButton>

                                                  <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="FTBI"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                             <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvFTI" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>From Date
                                                                        </th>
                                                                          <th>To Date
                                                                        </th>
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPFTIEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FTI_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FTI_ID") %>' TabIndex="1" OnClick="btnTPFTIEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("FROM_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                                  <td>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("TO_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_11">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Faculty Patents Leading To Industry Products</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPPCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" ControlToValidate="ddlFPPCompany"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPPSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ControlToValidate="ddlFPPSector"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLeadIncStaus" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" ControlToValidate="txtLeadIncStaus"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFPPFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFPPFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" ControlToValidate="ddlFPPFaculty"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLeadFacultyId" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ControlToValidate="txtLeadFacultyId"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Enter Faculty Id. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtleadDisicpline" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlFPPDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ControlToValidate="ddlFPPDiscipline"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="ddlFPPLevel" runat="server" CssClass="form-control" />--%>
                                                        <asp:DropDownList ID="ddlFPPLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" ControlToValidate="ddlFPPLevel"
                                                            ValidationGroup="FPLIP" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                <%--    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date Of Patent Adoption</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLeadDateadp" runat="server" CssClass="form-control" />
                                                    </div>--%>

                                                       <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Date Of Patent Adoption</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="DivFPPPatent">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtFPPPatentDate" runat="server" TabIndex="8" ToolTip="Enter Event Date Of Patent Adoption"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender12" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivFPPPatent" TargetControlID="txtFPPPatentDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtFPPPatentDate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator10" runat="server" EmptyValueMessage="Please Enter Valid Date Of Patent Adoption"
                                                               ControlExtender="MaskedEditExtender9" ControlToValidate="txtFPPPatentDate" IsValidEmpty="true"
                                                               InvalidValueMessage="Date Of Patent Adoption is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="FPLIP" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFPPPatentDate"
                                                                    Display="None" ErrorMessage="Please Enter Date Of Patent Adoption." ValidationGroup="FPLIP"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Patent No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtleadPatentno" runat="server" CssClass="form-control" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Granted / Filled</label>
                                                        </div>
                                                        <asp:TextBox ID="txtleadgranted" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Patent Owner</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLeadPetOwner" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Year</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLeadYear" runat="server" CssClass="form-control" MaxLength="4" onKeyUp="return validateNumeric(this)" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfFPP" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfFPP"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="Button23" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button24" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                  <asp:LinkButton ID="btnFPPSubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnFPPSubmit_Click" OnClientClick="return validate12()" ValidationGroup="FPLIP">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnFPPCancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnFPPCancle_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="FPLIP"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                                <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvFPP" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>Patent Owner
                                                                        </th>
                                                                      
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPFPPEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FPP_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("FPP_ID") %>' TabIndex="1" OnClick="btnTPFPPEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PATENT_OWNER")%>'></asp:Label></td>
                                                                  
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_12">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Papers Authored To Industry By Faculty</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPAIFCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ControlToValidate="ddlPAIFCompany"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPAIFSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ControlToValidate="ddlPAIFSector"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthIncStatus" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ControlToValidate="txtAuthIncStatus"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPAIFFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPAIFFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" ControlToValidate="ddlPAIFFaculty"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthFacultyId" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="txtAuthFacultyId"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Faculty Id. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                        <%--<asp:TextBox ID="txtAuthDscipline" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlPAIFDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" ControlToValidate="ddlPAIFDiscipline"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                      <%--  <asp:TextBox ID="txtAuthlevel23" runat="server" CssClass="form-control" />--%>
                                                          <asp:DropDownList ID="ddlPAIFLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" ControlToValidate="ddlPAIFLevel"
                                                            ValidationGroup="PATIBF" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                  <%--  <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date Sent Or Presented</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthDate" runat="server" CssClass="form-control" />
                                                    </div>--%>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Date Sent Or Presented</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="Divpresented">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtAuthDate" runat="server" TabIndex="8" ToolTip="Enter Event Date Sent Or Presented"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender13" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="Divpresented" TargetControlID="txtAuthDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender13" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtAuthDate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator11" runat="server" EmptyValueMessage="Please Enter Valid Date Sent Or Presented"
                                                               ControlExtender="MaskedEditExtender13" ControlToValidate="txtAuthDate" IsValidEmpty="true"
                                                               InvalidValueMessage="Date Sent Or Presented is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="PATIBF" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAuthDate"
                                                                    Display="None" ErrorMessage="Please Enter Date Sent Or Presented." ValidationGroup="PATIBF"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Paper Title </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthPapertitle" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Assignment Type</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthAssignment" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfPAIF" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfPAIF"></label>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                               <%-- <asp:Button ID="Button25" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button26" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                  <asp:LinkButton ID="btnPAIFSubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnPAIFSubmit_Click" OnClientClick="return validate13()" ValidationGroup="PATIBF">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnPAIFCancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnPAIFCancle_Click">Cancel</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="PATIBF"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                             <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvPAIF" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                        <th>Presented Date
                                                                        </th>
                                                                      
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPPAIFEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PAIF_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("PAIF_ID") %>' TabIndex="1" OnClick="btnTPPAIFEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("PRESENTED_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label></td>
                                                                  
                                                              
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_13">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Services</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSevCompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" ControlToValidate="ddlSevCompany"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Sector</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSevSector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ControlToValidate="ddlSevSector"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Company Sector. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Incorporation Status</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSerIncstaus" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" ControlToValidate="txtSerIncstaus"
                                                            ValidationGroup="SERV" ErrorMessage="Please Enter Incorporation Status. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Type Of Services</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSertype" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Title Of Services</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSerTitleser" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Year</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSerYear" runat="server" CssClass="form-control" MaxLength="4" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSevFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSevFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" ControlToValidate="ddlSevFaculty"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Faculty. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Faculty ID</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSerFacultyId" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" ControlToValidate="txtSerFacultyId"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Faculty ID. " SetFocusOnError="true"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline </label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtSerDiscipline" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlSevDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" ControlToValidate="ddlSevDiscipline"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtASerLevel" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlSevLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator90" runat="server" ControlToValidate="ddlSevLevel"
                                                            ValidationGroup="SERV" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <%--<div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Start Date Finish Date</label>
                                                        </div>
                                                        <div id="pickerServices" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="dateServices"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>--%>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Start Date</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="DivSevS">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtSevStartdate" runat="server" TabIndex="8" ToolTip="Enter Start Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender14" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivSevS" TargetControlID="txtSevStartdate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender14" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSevStartdate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator12" runat="server" EmptyValueMessage="Please Enter Valid Start Date"
                                                               ControlExtender="MaskedEditExtender14" ControlToValidate="txtSevStartdate" IsValidEmpty="true"
                                                               InvalidValueMessage="Start Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="SERV" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtSevStartdate"
                                                                    Display="None" ErrorMessage="Please Enter Start Date." ValidationGroup="SERV"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                     <div class="form-group col-lg-4 col-md-6 col-12">
                                                       <div class="label-dynamic">
                                                           <sup>*</sup>
                                                           <label>Finish Date</label>
                                                       </div>
                                                       <div class="input-group date">
                                                           <div class="input-group-addon"  runat="server" id="DivSevF">
                                                               <i class="fa fa-calendar text-blue"></i>
                                                           </div>
                                                           <asp:TextBox ID="txtSevFinishDate" runat="server" TabIndex="8" ToolTip="Enter Finish Date"
                                                               CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender15" runat="server" Enabled="true" EnableViewState="true"
                                                               Format="dd/MM/yyyy" PopupButtonID="DivSevF" TargetControlID="txtSevFinishDate" />
                                                           <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender15" runat="server" Mask="99/99/9999" MaskType="Date"
                                                               OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSevFinishDate" />
                                                          <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator13" runat="server" EmptyValueMessage="Please Enter Valid Finish Date"
                                                               ControlExtender="MaskedEditExtender15" ControlToValidate="txtSevFinishDate" IsValidEmpty="true"
                                                               InvalidValueMessage="Finish Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                               ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                               ValidationGroup="SERV" SetFocusOnError="true" />
                                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSevFinishDate"
                                                                    Display="None" ErrorMessage="Please Enter Finish Date." ValidationGroup="SERV"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                         
                                                       </div>
                                                        </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Fee received From Industry</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAuthfeereceive" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfSev" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfSev"></label>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                                <%--<asp:Button ID="Button27" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button28" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>
                                                <asp:LinkButton ID="btnSevSubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSevSubmit_Click" OnClientClick="return validate14()" ValidationGroup="SERV">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnSevCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnSevCancel_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="SERV"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                               <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="LVSEV" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Sector Name
                                                                        </th>
                                                                      
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPSEVEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SEV_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("SEV_ID") %>' TabIndex="1" OnClick="btnTPSEVEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("JOBSECTOR")%>'></asp:Label></td>
                                                             
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="tab-pane fade" id="tab_14">
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Student Self-Employment</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Student First Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtstudFName" runat="server" CssClass="form-control" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator91" runat="server" ControlToValidate="txtstudFName"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Enter Student First Name. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Student Last Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtstudLName" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator92" runat="server" ControlToValidate="txtstudLName"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Enter Student Last Name. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Type Of Self Employment</label>
                                                        </div>
                                                        <asp:TextBox ID="txtstudSelfEmp" runat="server" CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator93" runat="server" ControlToValidate="txtstudSelfEmp"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Enter Type Of Self Employment. " SetFocusOnError="true" 
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Discipline</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtStudadiscipline" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlSSEDiscipline" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator94" runat="server" ControlToValidate="ddlSSEDiscipline"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Select Discipline. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Level</label>
                                                        </div>
                                                       <%-- <asp:TextBox ID="txtStudlevel" runat="server" CssClass="form-control" />--%>
                                                         <asp:DropDownList ID="ddlSSELevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" ControlToValidate="ddlSSELevel"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Select Level. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Year</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudYear" runat="server" CssClass="form-control" MaxLength="4" onKeyUp="return validateNumeric(this)"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Company Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSSECompany" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator96" runat="server" ControlToValidate="ddlSSECompany"
                                                            ValidationGroup="STDSELFEMP" ErrorMessage="Please Select Company Name. " SetFocusOnError="true" InitialValue="0"
                                                             Display="None"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudAddress" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudemailid" runat="server" CssClass="form-control" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtStudemailid"
                                                            Display="None" ErrorMessage="Enter Correct Email Id " SetFocusOnError="True"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="STDSELFEMP"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No. </label>
                                                        </div>
                                                        <asp:TextBox ID="txStudtMonNo" runat="server" CssClass="form-control"  onKeyUp="return validateNumeric(this)" MaxLength="10"/>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Website</label>
                                                        </div>
                                                        <asp:TextBox ID="txtStudWebsite" runat="server" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rfSSE" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rfSSE"></label>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-12 btn-footer">
                                             <%--   <asp:Button ID="Button29" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button30" runat="server" Text="Cancel" CssClass="btn btn-warning" />--%>

                                                 <asp:LinkButton ID="btnSSESubmit" runat="server" CssClass="btn btn-outline-info"  OnClick="btnSSESubmit_Click" OnClientClick="return validate15()" ValidationGroup="STDSELFEMP">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnSSECancle" runat="server" CssClass="btn btn-outline-danger" OnClick="btnSSECancle_Click">Cancel</asp:LinkButton>
                                                  <asp:ValidationSummary ID="ValidationSummary12" runat="server" ValidationGroup="STDSELFEMP"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>
                                                <div class="col-12">
                                                <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">

                                                    <asp:ListView ID="lvSSE" runat="server">
                                                        <LayoutTemplate>

                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Company Name</th>
                                                                        <th>Student First Name
                                                                        </th>
                                                                        <th>Student Last Name
                                                                        </th>
                                                                      
                                                                        <th>Status</th>
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
                                                                    <asp:ImageButton ID="btnTPSSEEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SSE_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="View & Edit Record" CommandName='<%# Eval("SSE_ID") %>' TabIndex="1" OnClick="btnTPSSEEdit_Click" />
                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("COMPNAME")%>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("STUDENT_FIRST_NAME")%>'></asp:Label></td>
                                                                  
                                                               <td>
                                                                    <asp:Label ID="Label9" runat="server" Text='<%# Eval("STUDENT_LAST_NAME")%>'></asp:Label></td>
                                                                <td class="text-center">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="badge" Text='<%# Eval("ISACTIVE")%>'></asp:Label></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
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
        </div>
    </div>
    <script>
        function TabShow(tabid) {
            var tabName = tabid;
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
        </script>
   
     <script type="text/javascript" language="javascript">

       
         //TAB0
         function validate1() {

             $('#hfdActive').val($('#rdActive').prop('checked'));
         }

         function SetStat(val) {
             $('#rdActive').prop('checked', val);
         }

         //TAB1
         function validate2() {

             $('#hfDiscipline').val($('#rdDiscipline').prop('checked'));
         }

         function SetStat2(val) {
             $('#rdDiscipline').prop('checked', val);
         }

         //TAB2
         function validate3() {

             $('#hfCurriculum').val($('#rdCurriculum').prop('checked'));
         }

         function SetStat3(val) {
             $('#rdCurriculum').prop('checked', val);
         }

         //TAB3
         function validate4() {

             $('#hfVisFac').val($('#rdVisFac').prop('checked'));
         }

         function SetStat4(val) {
             $('#rdVisFac').prop('checked', val);
         }
         
         //TAB4
         function validate5() {

             $('#hfIndVisit').val($('#rdIndVisit').prop('checked'));
         }

         function SetStat5(val) {
             $('#rdIndVisit').prop('checked', val);
         }

         //TAB5
         function validate6() {

             $('#hfGuestlect').val($('#rfGuestlect').prop('checked'));
         }

         function SetStat6(val) {
             $('#rfGuestlect').prop('checked', val);
         }
         //TAB6
         function validate7() {

             $('#hffacultylinkind').val($('#rffacultylinkind').prop('checked'));
         }

         function SetStat7(val) {
             $('#rffacultylinkind').prop('checked', val);
         }
         //TAB7
         function validate8() {

             $('#hfFPTI').val($('#rfFPTI').prop('checked'));
         }

         function SetStat8(val) {
             $('#rfFPTI').prop('checked', val);
         }
         //TAB 8
         function validate9() {

             $('#hfFOOI').val($('#rfFOOI').prop('checked'));
         }

         function SetStat9(val) {
             $('#rfFOOI').prop('checked', val);
         }
         //TAB 9
         function validate10() {

             $('#hfEPAI').val($('#rfEPAI').prop('checked'));
         }

         function SetStat10(val) {
             $('#rfEPAI').prop('checked', val);
         }
         //TAB 10
         function validate11() {

             $('#hfFTI').val($('#rfFTI').prop('checked'));
         }

         function SetStat11(val) {
             $('#rfFTI').prop('checked', val);
         }
         //TAB 11
         function validate12() {

             $('#hfFPP').val($('#rfFPP').prop('checked'));
         }

         function SetStat12(val) {
             $('#rfFPP').prop('checked', val);
         }
         //TAB 12
         function validate13() {

             $('#hfPAIF').val($('#rfPAIF').prop('checked'));
         }

         function SetStat13(val) {
             $('#rfPAIF').prop('checked', val);
         }
         //TAB 13
         function validate14() {

             $('#hfSev').val($('#rfSev').prop('checked'));
         }

         function SetStat14(val) {
             $('#rfSev').prop('checked', val);
         }

         //TAB 14
         function validate15() {

             $('#hfSSE').val($('#rfSSE').prop('checked'));
         }

         function SetStat15(val) {
             $('#rfSSE').prop('checked', val);
         }

         </script>

    <script>
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }
    </script>
    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

    <!-- ========= Daterange curriculam ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerCurriculam').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#dateCurriculam').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))

        });

            $('#dateCurriculam').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerCurriculam').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#dateCurriculam').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))

            });

                $('#dateCurriculam').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))

            });
        });

    </script>

    <!-- ========= Daterange  Executive program Attend by industry ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerExecutive').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#dateExecutive').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#dateExecutive').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerExecutive').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#dateExecutive').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#dateExecutive').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

    <!-- =========Date industry visits ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerIndustry').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#dateIndustry').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#dateIndustry').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerIndustry').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#dateIndustry').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#dateIndustry').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

    <!-- =========Date industry visits ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerServices').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#dateServices').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#dateServices').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerServices').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#dateServices').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#dateServices').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

</asp:Content>


<%--<div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">COMPANY DETAILS</h3>
                </div>
                <div class="box-body">


                    <div class="accordion" id="accordionExample">
                        <div class="card">
                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                <span class="title">Companies </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>

                            <div id="collapseOne" class="collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList25" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList26" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button31" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button32" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                <span class="title">Discipline</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseTwo" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Year Of Inception</label>
                                                </div>
                                                <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Number Of Substream</label>
                                                </div>
                                                <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Number Of Faculties</label>
                                                </div>
                                                <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-5 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Total Number Of Final Year Students Eligible For Placement (2020 Batch)</label>
                                                </div>
                                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-5 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Total Number Of Final Year Students Eligible For Placement (2021 Batch)</label>
                                                </div>
                                                <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button33" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button34" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true">
                                <span class="title">Curriculum Input</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseThree" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList27" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList28" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Sector</label>
                                                </div>
                                                <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox15" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date From To</label>
                                                </div>
                                                <div id="Div1" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="Span2"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>No Of Students</label>
                                                </div>
                                                <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button35" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button36" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="true">
                                <span class="title">Visiting Faculties From Industries/ Expert Lecture  </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseFour" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList29" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList30" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Sector</label>
                                                </div>
                                                <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Designation</label>
                                                </div>
                                                <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox27" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox29" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox30" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox31" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox32" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox33" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date Of Lecture</label>
                                                </div>
                                                <asp:TextBox ID="TextBox34" Type="date" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>No Of Students</label>
                                                </div>
                                                <asp:TextBox ID="TextBox35" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button37" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button38" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="true">
                                <span class="title">Industrial Visits </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseFive" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList31" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList32" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Sector</label>
                                                </div>
                                                <asp:TextBox ID="TextBox36" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox37" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox38" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox39" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox40" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox41" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox42" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox43" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date From To</label>
                                                </div>
                                                <div id="Div4" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="Span3"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>No Of Students</label>
                                                </div>
                                                <asp:TextBox ID="TextBox44" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button39" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button40" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="true">
                                <span class="title">Guest lecture / Alumni Talk </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseSix" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList33" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList34" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Sector</label>
                                                </div>
                                                <asp:TextBox ID="TextBox45" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Designation</label>
                                                </div>
                                                <asp:TextBox ID="TextBox46" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox47" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox48" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox49" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox50" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox51" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox52" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox53" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date of lecture</label>
                                                </div>
                                                <asp:TextBox ID="TextBox54" Type="date" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Students Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox55" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox56" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>No Of Students Attended</label>
                                                </div>
                                                <asp:TextBox ID="TextBox57" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button41" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button42" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSeven" aria-expanded="true">
                                <span class="title">Faculty Linked To Industry </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseSeven" class="collapse collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox58" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox59" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox60" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Name Of  Company</label>
                                                </div>
                                                <asp:TextBox ID="TextBox61" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox62" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox63" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox64" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox65" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox66" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox67" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox68" runat="server" CssClass="form-control" />
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button43" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button44" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--------faculty providing training to industry--->
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="true">
                                <span class="title">Faculty Providing Training To industry</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseEight" class="collapse collapse  show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList35" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList36" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox69" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox70" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox71" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox72" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox73" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox74" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox75" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox76" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox77" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox78" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox79" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date Of Lecture</label>
                                                </div>
                                                <asp:TextBox ID="TextBox80" Type="date" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button45" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button46" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--------faculty onboard of industry--->
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="true">
                                <span class="title">Faculty Onboard Of industry</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseNine" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList37" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList38" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox81" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Type Of Board /Company</label>
                                                </div>
                                                <asp:TextBox ID="TextBox82" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox83" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox84" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox85" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox86" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox87" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No.</label>
                                                </div>
                                                <asp:TextBox ID="TextBox88" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Manager/Contact Person Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox89" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox90" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox91" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox92" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Member</label>
                                                </div>
                                                <asp:TextBox ID="TextBox93" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button47" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button48" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--------Executive program attend by industry--->
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTen" aria-expanded="true">
                                <span class="title">Executive program Attend By Industry</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseTen" class="collapse collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList39" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList40" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox94" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox95" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox96" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox97" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox98" runat="server" CssClass="form-control" />
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox99" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Program Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox100" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date From - To</label>
                                                </div>
                                                <div id="Div5" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="Span4"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>No. Of Executives attending courses</label>
                                                </div>
                                                <asp:TextBox ID="TextBox101" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button49" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button50" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--------Faculty Trained By Industry--->
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseEleven" aria-expanded="true">
                                <span class="title">Faculty Trained By Industry</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseEleven" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList41" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList42" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox102" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox103" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox104" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox105" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox106" runat="server" CssClass="form-control" />
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox107" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Start Date End Date</label>
                                                </div>
                                                <div id="Div6" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                                 <span id="Span5"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button51" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button52" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwelve" aria-expanded="true">
                                <span class="title">Faculty Patents Leading To Industry Products</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseTwelve" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList43" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList44" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox108" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox109" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox110" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox111" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox112" runat="server" CssClass="form-control" />
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox113" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date Of Patent Adoption</label>
                                                </div>
                                                <asp:TextBox ID="TextBox114" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Patent No</label>
                                                </div>
                                                <asp:TextBox ID="TextBox115" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Granted / Filled</label>
                                                </div>
                                                <asp:TextBox ID="TextBox116" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Patent Owner</label>
                                                </div>
                                                <asp:TextBox ID="TextBox117" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Year</label>
                                                </div>
                                                <asp:TextBox ID="TextBox118" runat="server" CssClass="form-control" />
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button53" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button54" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThirteen" aria-expanded="true">
                                <span class="title">Papers Authored To Industry By Faculty</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseThirteen" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList45" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList46" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox119" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox120" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox121" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox122" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox123" runat="server" CssClass="form-control" />
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox124" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Date Sent Or Presented</label>
                                                </div>
                                                <asp:TextBox ID="TextBox125" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Paper Title </label>
                                                </div>
                                                <asp:TextBox ID="TextBox126" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Assignment Type</label>
                                                </div>
                                                <asp:TextBox ID="TextBox127" runat="server" CssClass="form-control" />
                                            </div>


                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button55" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button56" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                       
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFourteen" aria-expanded="true">
                                <span class="title">Services</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseFourteen" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList47" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Sector</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList48" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Incorporation Status</label>
                                                </div>
                                                <asp:TextBox ID="TextBox128" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Type Of Services</label>
                                                </div>
                                                <asp:TextBox ID="TextBox129" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Title Of Services</label>
                                                </div>
                                                <asp:TextBox ID="TextBox130" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Year</label>
                                                </div>
                                                <asp:TextBox ID="TextBox131" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty First Name </label>
                                                </div>
                                                <asp:TextBox ID="TextBox132" runat="server" CssClass="form-control" />
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox133" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Faculty ID</label>
                                                </div>
                                                <asp:TextBox ID="TextBox134" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline </label>
                                                </div>
                                                <asp:TextBox ID="TextBox135" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox136" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Start Date Finish Date</label>
                                                </div>
                                                <div id="Div7" class="form-control" tabindex="3">
                                                    <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="Span6"></span>
                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Fee received From Industry</label>
                                                </div>
                                                <asp:TextBox ID="TextBox137" runat="server" CssClass="form-control" />
                                            </div>


                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button57" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button58" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFifteen" aria-expanded="true">
                                <span class="title">Student Self-Employment</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseFifteen" class="collapse collapse show ">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Student First Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox138" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Student Last Name</label>
                                                </div>
                                                <asp:TextBox ID="TextBox139" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Type Of Self Employment</label>
                                                </div>
                                                <asp:TextBox ID="TextBox140" runat="server" CssClass="form-control" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Discipline</label>
                                                </div>
                                                <asp:TextBox ID="TextBox141" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Level</label>
                                                </div>
                                                <asp:TextBox ID="TextBox142" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Year</label>
                                                </div>
                                                <asp:TextBox ID="TextBox143" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Company Name</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList49" runat="server" CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Address</label>
                                                </div>
                                                <asp:TextBox ID="TextBox144" runat="server" TextMode="MultiLine" Rows="1" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Email Id</label>
                                                </div>
                                                <asp:TextBox ID="TextBox145" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Mobile No. </label>
                                                </div>
                                                <asp:TextBox ID="TextBox146" runat="server" CssClass="form-control" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Website</label>
                                                </div>
                                                <asp:TextBox ID="TextBox147" runat="server" CssClass="form-control" />
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="Button59" runat="server" Text="Submit" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button60" runat="server" Text="Cancel" CssClass="btn btn-warning" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

