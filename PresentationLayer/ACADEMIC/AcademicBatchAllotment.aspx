<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AcademicBatchAllotment.aspx.cs" Inherits="ACADEMIC_AcademicBatchAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">DEFINE COUNTER a</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">Academic Batch Allotment</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Academic Year Allotment</a>
                            </li>

                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">

                                <div>
                                    <asp:UpdateProgress ID="UdpAcdBatch" runat="server" AssociatedUpdatePanelID="UdpAcademicBatch"
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
                                <asp:UpdatePanel ID="UdpAcademicBatch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Admission Batch</label>--%>
                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                            ToolTip="Please Select Admission Batch" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server" ControlToValidate="ddlAdmBatch" Display="None"
                                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmBatch" Display="None"
                                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>

                                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                                            ToolTip="Please Select School/Institute Name" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" Display="None"
                                                            ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege" Display="None"
                                                            ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Submit">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>

                                                        <asp:ListBox ID="ddlBranch" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                            ToolTip="Please Select Programme/Branch" TabIndex="4"></asp:ListBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYlvAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="6" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlAdmType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="7" ToolTip="Show" ValidationGroup="Show" OnClick="btnShow_Click" />
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="8" ToolTip="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Cancel" TabIndex="9" OnClick="btnCancel_Click" />
                                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Submit" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Show" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Academic Batch</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAcdBatch" runat="server" AppendDataBoundItems="true" TabIndex="10" data-select2-enable="true"
                                                        ToolTip="Please Select Academic Batch">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAcdBatch" runat="server" ControlToValidate="ddlAcdBatch" Display="None"
                                                        ErrorMessage="Please Select Academic Batch" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-12">
                                                            <asp:Panel ID="pnlStudents" runat="server" Visible="true">
                                                                <asp:ListView ID="lvAcdBatchAllotment" runat="server">
                                                                    <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Academic Batch Allotment List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblAcdBatchAllotment">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="SelectAll(this,1,'chkRegister');" />
                                                                                    </th>
                                                                                    <th>  <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th> <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th> <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th><asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th> <asp:Label ID="lblDYlvAdmType" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th>Academic Batch</th>
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
                                                                                <asp:CheckBox ID="chkRegister" runat="server" ToolTip='<%# Eval("IDNO") %>' onclick="ChkHeader(1,'cbHead','chkRegister');" />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("REGNO") %> 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("DEGREENAME") %> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("LONGNAME") %> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SEMESTERNAME") %> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("IDTYPEDESCRIPTION") %> 
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("ACADEMICBATCH") %> 
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ASTATUS") %>' />
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>

                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_2">

                                <div>
                                    <asp:UpdateProgress ID="UpdAcdYear" runat="server" AssociatedUpdatePanelID="UdpAcademicYear"
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
                                <asp:UpdatePanel ID="UdpAcademicYear" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Admission Batch</label>

                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatchAcdYear" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                            ToolTip="Please Select Admission Batch" OnSelectedIndexChanged="ddlAdmBatchAcdYear_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlAdmissionBatch" runat="server" ControlToValidate="ddlAdmBatchAcdYear" Display="None"
                                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="AcdYearSubmit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmBatchAcdYear" Display="None"
                                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="AcdYearShow"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>

                                                            <Label> School/Institute</Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                                            ToolTip="Please Select School/Institute Name"  OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSchool" Display="None"
                                                            ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="AcdYearSubmit"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSchool" Display="None"
                                                            ErrorMessage="Please Select School/Institute Name" InitialValue="0" ValidationGroup="AcdYearShow"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           
                                                            <label>Degree</label>
                                                            <%--<asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreeAcdYear" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Degree" OnSelectedIndexChanged="ddlDegreeAcdYear_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                       <%-- <asp:RequiredFieldValidator ID="rfvddlDegreeAcdYear" runat="server" ControlToValidate="ddlDegreeAcdYear"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="AcdYearSubmit">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDegreeAcdYear"
                                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="AcdYearShow">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Program/Branch</label>
                                                        </div>
                                                        <asp:ListBox ID="lstbBranch" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                            ToolTip="Please Select Programme/Branch" TabIndex="4"></asp:ListBox>
                                                    </div>
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <Label>Semester</Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAcdYearSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                            ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlAcdYearSemester_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>


                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShowAcdYear" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="6" ToolTip="Show" ValidationGroup="AcdYearShow" OnClick="btnShowAcdYear_Click" />
                                                <asp:Button ID="btnSubmitAcdYear" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="7" ToolTip="Submit" ValidationGroup="AcdYearSubmit" OnClick="btnSubmitAcdYear_Click" />
                                                <asp:Button ID="btnCancelAcdYear" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Cancel" TabIndex="8" OnClick="btnCancelAcdYear_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="AcdYearSubmit" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="AcdYearShow" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Academic Year</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAcdYear" runat="server" AppendDataBoundItems="true" TabIndex="9" data-select2-enable="true"
                                                    ToolTip="Please Select Academic Year">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAcdYear" Display="None"
                                                    ErrorMessage="Please Select Academic Year" InitialValue="0" ValidationGroup="AcdYearSubmit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-12">
                                                        <asp:Panel ID="PnlvAcdYear" runat="server" Visible="true">
                                                            <asp:ListView ID="lvAcdYearAllotment" runat="server">
                                                                <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Academic Year Allotment List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblAcdYearAllotment">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="trRow">
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHeadAcdYear" runat="server" onclick="SelectAll(this,2,'chkRegisterAcdYear');" ToolTip="Select/Select all" />
                                                                                </th>
                                                                                <th>Reg No
                                                                                </th>
                                                                                <th>Student Name
                                                                                </th>
                                                                                <th>Degree</th>
                                                                                <th>Branch</th>
                                                                                <th>Semester</th>                                                                               
                                                                                <th>Academic Year</th>
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
                                                                            <asp:CheckBox ID="chkRegisterAcdYear" runat="server" ToolTip='<%# Eval("IDNO") %>' onclick="ChkHeader(2,'cbHeadAcdYear','chkRegisterAcdYear');" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REGNO") %> 
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DEGREENAME") %> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("LONGNAME") %> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME") %> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACADEMIC_YEAR_NAME") %> 
                                                                        </td>                                                                   
                                                                        <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ASTATUS") %>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });


    </script>
    <%--   <script type="text/javascript">
        function Validate() {
            var ddlFruits = document.getElementById("<%=ddlAcdBatch.ClientID %>").value;
            if (ddlFruits.value == "") {
                //If the "Please Select" option is selected display error.
                alert("Please select an option!");
                return false;
            }
            return true;
        }
    </script>--%>
    <script>
        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblAcdBatchAllotment');
                list = 'lvAcdBatchAllotment';
            }
            else
                if (headid == 2) {
                    tbl = document.getElementById('tblAcdYearAllotment');
                    list = 'lvAcdYearAllotment';
                }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var headid = '';
                var tbl = '';
                var list = '';
                var chkcnt = 0;
                if (chklst == 1) {
                    tbl = document.getElementById('tblAcdBatchAllotment');
                    headid = 'ctl00_ContentPlaceHolder1_lvAcdBatchAllotment_' + head;
                    list = 'lvAcdBatchAllotment';
                }
                else if (chklst == 2) {
                    tbl = document.getElementById('tblAcdYearAllotment');
                    headid = 'ctl00_ContentPlaceHolder1_lvAcdYearAllotment_' + head;
                    list = 'lvAcdYearAllotment';
                }
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                //if (chkcnt > 0)
                //    document.getElementById(headid).checked = true;
                //else
                //    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }
    </script>
    <%-- <script>
        function confirmCheck(frm) {
            for (i = 0; i < frm.length; i++) {
                if (frm.elements[i].name.indexOf('chkRegister') != -1) {
                    if (frm.elements[i].checked) {
                        return 
                    }
                }
            }
            alert('Please select at least one Student for Academic Batch Allotment')
            return false
        }
    </script>--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblAcdYearAllotment').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblAcdYearAllotment').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tblAcdYearAllotment').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
</asp:Content>

