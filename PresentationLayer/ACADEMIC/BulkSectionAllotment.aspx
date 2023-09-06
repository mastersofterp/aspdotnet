<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkSectionAllotment.aspx.cs" Inherits="ACADEMIC_BulkSectionAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <div>
       <%-- <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSection"
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
        </asp:UpdateProgress>--%>
    </div>

    <%--<asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">BULK SECTION ALLOTMENT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <%--<asp:HiddenField ID="ActiveTabIndexHiddenField" runat="server" />--%>
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Bulk Section Allotment</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">Upload Section Allotment</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">      <%--onclick="tabClickHandler(0);"--%>
                                        <div>
                                            <asp:UpdateProgress ID="UpdBulkSectionAllot" runat="server" AssociatedUpdatePanelID="updBulkSectionA"
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

                                        <asp:UpdatePanel ID="updBulkSectionA" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>School/Institute Name</label>--%>
                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlInsName" runat="server" AppendDataBoundItems="true" AutoPostBack="true" ValidationGroup="teacherallot" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlInsName_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvInsName" runat="server"
                                                                ControlToValidate="ddlInsName" Display="None"
                                                                ErrorMessage="Please Select School/Institute Name" InitialValue="0"
                                                                ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                              <%--  <sup>* </sup>--%>
                                                                <%--<label>Admission Batch</label>--%>
                                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" data-select2-enable="true"
                                                                ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server"
                                                                ControlToValidate="ddlAdmBatch" Display="None"
                                                                ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                                ValidationGroup="teacherallot"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Academic Year</label>                                               
                                                            </div>
                                                            <asp:DropDownList ID="ddlAcdYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAcdYear_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="show" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvAcademicYear" runat="server" ControlToValidate="ddlAcdYear"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="RegisterReport">
                                                                </asp:RequiredFieldValidator>
                                            
                                                            <asp:RequiredFieldValidator ID="rfcvacdyear" runat="server" ControlToValidate="ddlAcdYear"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Academic Year" ValidationGroup="admYear" Visible="false" >
                                                                </asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%-- <label>Degree</label>--%>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                                ValidationGroup="teacherallot">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="teacherallot">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Programme/Branch</label>--%>
                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                                ValidationGroup="teacherallot">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                Display="None" ErrorMessage="Please Select Programme/Branch" InitialValue="0" ValidationGroup="teacherallot">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Semester</label>--%>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="5" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--     <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                                            </asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Section</label>--%>
                                                                <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlClassSection" runat="server" AppendDataBoundItems="true" TabIndex="6" OnSelectedIndexChanged="ddlClassSection_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvClassSection" runat="server" ControlToValidate="ddlClassSection"
                                                                Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="sectionVal">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Range From</label>
                                                            </div>
                                                            <div class="form-inline">
                                                                <asp:TextBox ID="txtEnrollFrom" runat="server" CssClass="form-control" ValidationGroup="EnrollText" Enabled="false" TabIndex="7" />
                                                                <asp:RequiredFieldValidator ID="rfvEnrollFrom" runat="server" ControlToValidate="txtEnrollFrom"
                                                                    Display="None" ErrorMessage="Please Select Registration No From Range" ValidationGroup="EnrollText"></asp:RequiredFieldValidator>

                                                                &nbsp;&nbsp;
                                                                <label>To</label>
                                                                &nbsp;&nbsp;

                                                                <asp:TextBox ID="txtEnrollTo" runat="server" CssClass="form-control" ValidationGroup="EnrollText" Enabled="false" TabIndex="8" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollTo"
                                                                    Display="None" ErrorMessage="Please Select Registration No To Range" ValidationGroup="EnrollText"></asp:RequiredFieldValidator>&nbsp;&nbsp;

                                                                <asp:Button ID="btnConfirm" runat="server" TabIndex="9" CssClass="btn btn-primary" Text="Confirm Students" OnClick="btnConfirm_Click" ValidationGroup="EnrollText" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-8 col-md-12 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sort By</label>
                                                            </div>
                                                            <asp:RadioButton ID="rbenroll" runat="server" GroupName="sort" Text="Enrollment No." Checked="True" OnCheckedChanged="rbenroll_CheckedChanged" AutoPostBack="true" />
                                                            <asp:RadioButton ID="rbRegNo" runat="server" GroupName="sort" Text="PRN Number" OnCheckedChanged="rbRegNo_CheckedChanged" AutoPostBack="true" />
                                                            <asp:RadioButton ID="rbStudName" runat="server" GroupName="sort" Text="Student Name" OnCheckedChanged="rbStudName_CheckedChanged" AutoPostBack="true" />
                                                            <asp:RadioButton ID="rbAdmDate" runat="server" GroupName="sort" Text="Admission Date" OnCheckedChanged="rbAdmDate_CheckedChanged" AutoPostBack="true" />
                                                            <asp:RadioButton ID="rbmeritno" runat="server" GroupName="sort" Text="Merit No" OnCheckedChanged="rbmeritno_CheckedChanged" AutoPostBack="true" Visible="false" />
                                                            <asp:RadioButton ID="rbCGPA" runat="server" GroupName="sort" Text="CGPA" OnCheckedChanged="rbCGPA_CheckedChanged" AutoPostBack="true" Visible="false" />

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" Visible="false" />&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" Visible="false" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" ValidationGroup="teacherallot" OnClick="btnFilter_Click"
                                                        CssClass="btn btn-primary" TabIndex="10" />
                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="sectionVal" TabIndex="11" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnClear_Click" TabIndex="12" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    <asp:ValidationSummary ID="validationSection" runat="server" ValidationGroup="sectionVal"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                                                    <asp:ValidationSummary ID="validationEnrolltxt" runat="server" ValidationGroup="EnrollText"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" />
                                                    <asp:HiddenField ID="hdfIdnos" runat="server" />
                                                </div>

                                                <div class="col-12 d-none">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Total Selected Students</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" Enabled="false" Style="text-align: center" />
                                                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Section</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlStudent" runat="server">
                                                        <asp:ListView ID="lvStudents" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="demo-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                     <div class="row mb-1">
                                                                    <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                       <%-- <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>--%>
                                                                    </div>

                                                                    <div class="col-lg-3 col-md-6">
                                                                        <div class="input-group sea-rch">
                                                                            <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                            <div class="input-group-addon">
                                                                                <i class="fa fa-search"></i>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                                    <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudent">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Sr No.</th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th style="display: none">Roll No.</th>
                                                                                    <th>Student Name </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                    <th>Admission Date </th>
                                                                                    <th>Merit No. </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkrow" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("IDNO")%>' />
                                                                        <asp:HiddenField ID="hdfEnroll" runat="server" Value='<%# Eval("ENROLLNO") %>' />
                                                                        <asp:HiddenField ID="hdfAdm" runat="server" Value='<%# Eval("ADMBATCH") %>' />
                                                                        <asp:Label ID="lblRegno" runat="server" Visible="false" Text='<%# Eval("ROLLNO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ENROLLNO") %>
                                                                    </td>
                                                                    <td style="display: none">
                                                                        <%# Eval("ROLLNO") %>
                                                                        <asp:HiddenField ID="hdfRollNO" runat="server" Value='<%# Eval("ROLLNO") %>' />
                                                                    </td>
                                                                    <td><%# Eval("REGNO") %>
                                                                        <asp:HiddenField ID="lblprnno" runat="server" Value='<%# Eval("REGNO") %>' />
                                                                        <asp:Label ID="Label1" runat="server" Visible="false" Text='<%# Eval("REGNO") %>'></asp:Label></td>
                                                                    <td><%# Eval("STUDNAME")%></td>
                                                                    <td><%# Eval("SEMESTERNAME")%></td>
                                                                    <td><%# Eval("SECTIONNAME")%></td>
                                                                    <td><%# Eval("ADMDATE")%></td>
                                                                    <td><%# Eval("MERITNO")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab2">   <%--onclick="tabClickHandler(1);"--%>
                                        <div>
                                            <asp:UpdateProgress ID="updBulkSectionImport" runat="server" AssociatedUpdatePanelID="updBulkSectionI"
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

                                        <asp:UpdatePanel ID="updBulkSectionI" runat="server" UpdateMode="Conditional" >
                                            <ContentTemplate>
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-12 col-12 mt-2">
                                                            <div class=" note-div">
                                                                <h5 class="heading">Note</h5>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: green; font-weight: bold">Only Excel files are allowed to Upload For Section Allotment</span>  </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12 sub-heading">
                                                            <h5>Student Section Allotment Data Import</h5>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <%--<label>School/Institute</label>--%>
                                                                <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSchool" runat="server" AutoPostBack="false" ToolTip="Please Select" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RfvddlSchool" runat="server" ControlToValidate="ddlSchool"
                                                                Display="None" ValidationGroup="Submit" InitialValue="0" ErrorMessage="Please Select School/Institute!!!"></asp:RequiredFieldValidator>
                                                            <%--<asp:RequiredFieldValidator ID="RfvddlSchool1" runat="server" ControlToValidate="ddlSchool"
                                                                Display="None" ValidationGroup="Submit1" InitialValue="0" ErrorMessage="Please Select School!!!"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Attach Excel File</label>
                                                            </div>
                                                            <asp:FileUpload ID="fuSectionAllotUpload" runat="server" ToolTip="Please Attach Excel File" TabIndex="2" />
                                                            <asp:RequiredFieldValidator ID="RfvSectionAllotUpload" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Choose file for Section Allotment!!!" ControlToValidate="fuSectionAllotUpload"
                                                                Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnDownload" runat="server" Text="Download Blank Excel Format" ToolTip="Click To Download Blank Excel Format" OnClick="btnDownload_Click" CssClass="btn btn-primary"/>
                                                            <asp:Button ID="btnUpload" runat="server" Text="Upload Excel" ToolTip="Click To Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click"  ValidationGroup="Submit"/>
                                                            <asp:Button ID="btnCancel_SectionImport" runat="server" Text="Cancel" ToolTip="Click To Reset" CssClass="btn btn-warning" OnClick="btnCancel_SectionImport_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" ValidationGroup="Submit" />
                                                            <%--<asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" ValidationGroup="Submit1" />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>

                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnDownload" />
                                                <asp:PostBackTrigger ControlID="btnUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" lang="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

            if (txtTot == 0) {
                alert('Please Select atleast one student/batch from student/batch list');
                return false;
            }
            else
                return true;
        }

        function checkStudents() {
            try {
                if (document.getElementById('<%=txtEnrollFrom.ClientID %>').value != null &&
                    document.getElementById('<%=txtEnrollFrom.ClientID %>').value != '' &&
                    document.getElementById('<%=txtEnrollTo.ClientID %>').value != null &&
                    document.getElementById('<%=txtEnrollTo.ClientID %>').value != '') {
                    var fromEnroll = (document.getElementById('<%=txtEnrollFrom.ClientID %>').value);
                    var toEnroll = (document.getElementById('<%=txtEnrollTo.ClientID %>').value);

                    table = document.getElementById('#tblStudent');
                    for (var r = 0; r < table.rows.length - 1; r++) {
                        var enrollNo = (document.getElementById('ContentPlaceHolder1_lvStudent_hdfRow_' + r.toString()));
                        chk = document.getElementById('ContentPlaceHolder1_lvStudent_chkRow_' + r.toString());
                        if (enrollNo >= fromEnroll && enrollNo <= toEnroll)
                            chk.checked = true;
                        else
                            chk.checked = false;
                    }
                }
                return false;
            }
            catch (er) {
                alert(er.description);
                return false;
            }
        }
        function clearCheckStudents() {
            try {

                table = document.getElementById('tblStudent');
                for (var r = 0; r < table.rows.length - 1; r++) {
                    chk = document.getElementById('ContentPlaceHolder1_lvStudent_chkRow_' + r.toString());
                    chk.checked = false;
                }
                document.getElementById('<%=txtEnrollFrom.ClientID %>').value = '';
                document.getElementById('<%=txtEnrollTo.ClientID %>').value = '';

                return false;
            }
            catch (er) {
                alert(er.description);
                return false;
            }
        }
        function checkAllStudents(headchk) {
            //alert('A');
            try {
                table = document.getElementById('tblStudent');
                for (var r = 0; r < table.rows.length - 1; r++) {
                    chk = document.getElementById('ContentPlaceHolder1_lvStudent_chkRow_' + r.toString());
                    chk.checked = headchk.checked;
                }
            }
            catch (er) {
                alert(er.description);
            }
        }
        //function validateNumeric(txt)
        //{
        //    if (isNaN(txt.value))
        //    {
        //        txt.value = '';
        //        alert('Only Numeric Characters Allowed!');
        //        txt.focus();
        //        return;
        //    }
        //}

        function confirmEnroll() {
            if (document.getElementById('<%=txtEnrollFrom.ClientID %>').value == '' || document.getElementById('<%=txtEnrollTo.ClientID %>').value == '') {
                alert("Please Enter Range of Registration No. to be Filter..");
            }
        }

        function ConfirmSubmit() {
            if (document.getElementById('<%=hdfIdnos.ClientID %>').value != '') {
                var answer = confirm("The Students with the Registration No.s " + document.getElementById('<%=hdfIdnos.ClientID %>').value + "are already assigned to sections ...Do you still want to continue?")
                if (answer)
                    return true;
                else
                    return false;
            }
        }
    </script>

     <script>
         function toggleSearch(searchBar, table) {
             var tableBody = table.querySelector('tbody');
             var allRows = tableBody.querySelectorAll('tr');
             var val = searchBar.value.toLowerCase();
             allRows.forEach((row, index) => {
                 var insideSearch = row.innerHTML.trim().toLowerCase();
             //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
             if (insideSearch.includes(val)) {
                 row.style.display = 'table-row';
             }
             else {
                 row.style.display = 'none';
             }

         });
         }

         function test5() {
             var searchBar5 = document.querySelector('#FilterData');
             var table5 = document.querySelector('#tblStudent');

             //console.log(allRows);
             searchBar5.addEventListener('focusout', () => {
                 toggleSearch(searchBar5, table5);
         });

         $(".saveAsExcel").click(function () {
             var workbook = XLSX.utils.book_new();
             var allDataArray = [];
             allDataArray = makeTableArray(table5, allDataArray);
             var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
             workbook.SheetNames.push("Test");
             workbook.Sheets["Test"] = worksheet;
             XLSX.writeFile(workbook, "StudentList.xlsx");
         });
         }

         function makeTableArray(table, array) {
             var allTableRows = table.querySelectorAll('tr');
             allTableRows.forEach(row => {
                 var rowArray = [];
             if (row.querySelector('td')) {
                 var allTds = row.querySelectorAll('td');
                 allTds.forEach(td => {
                     if (td.querySelector('span')) {
                         rowArray.push(td.querySelector('span').textContent);
             }
             else if (td.querySelector('input')) {
                 rowArray.push(td.querySelector('input').value);
             }
             else if (td.querySelector('select')) {
                 rowArray.push(td.querySelector('select').value);
             }
             else if (td.innerText) {
                 rowArray.push(td.innerText);
             }
             else{
                 rowArray.push('');
             }

         });
         }
         if (row.querySelector('th')) {
             var allThs = row.querySelectorAll('th');
             allThs.forEach(th => {
                 if (th.textContent) {
                     rowArray.push(th.textContent);
         }
         else {
             rowArray.push('');
         }
         });
         }
         // console.log(allTds);

         array.push(rowArray);
         });
         return array;
         }

    </script>
</asp:Content>



