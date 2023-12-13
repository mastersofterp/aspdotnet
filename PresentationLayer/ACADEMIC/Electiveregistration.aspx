<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="Electiveregistration.aspx.cs" Inherits="ACADEMIC_Electiveregistration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="Label1" runat="server">Elective Course Registration</asp:Label></h3>
                </div>
                <div class="box-body">
                    <%-- <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs mt-2" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Elective Course Registration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link " data-toggle="tab" href="#tab_2" tabindex="2">Elective Course Cancellation</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                               
                            </div>
                            <div class="tab-pane " id="tab_2">
                               
                            </div>
                        </div>
                    </div>--%>



                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#Div3" tabindex="1">Elective Course Registration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#Div4" tabindex="1" style="display: none">Elective Course Cancellation</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="Div1">
                            <div class="tab-pane active" id="Div3">
                                <div class="box-body">
                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                                    <asp:UpdatePanel ID="updBulkReg" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Institute Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" ToolTip="Please Select Institute" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>

                                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Scheme Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchemeType" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Scheme</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="7" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--   <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Scheme"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="submit" SetFocusOnError="true"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="8" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Student Status</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="8" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="9" CssClass="form-control" data-select2-enable="true"
                                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSection" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-6" id="divcourse" runat="server" visible="false">
                                                        <sup>* </sup>
                                                        <label>Elective Courses</label>
                                                        <asp:DropDownList ID="ddlcourselist" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlcourselist_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                        <asp:HiddenField ID="hftot" runat="server" />
                                                        <div class="label-dynamic">
                                                            <label>Total Students Selected</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                                            ForeColor="#000066"></asp:TextBox>
                                                        <%--meta:resourcekey="txtTotStudResource1"--%>
                                                        <%--<ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Total Credit Limit :</label>
                                                        </div>
                                                        <asp:TextBox ID="lblTotalCredit" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                                            ForeColor="#000066"></asp:TextBox>
                                                        <%--meta:resourcekey="txtTotStudResource1"--%>
                                                        <%--<ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                                    OnClick="btnSubmit_Click" OnClientClick="return validateAssign();" TabIndex="10"
                                                    Enabled="false" />
                                                <asp:Button ID="btnReport" runat="server" Text="Registration Slip Report"
                                                    CssClass="btn btn-info" OnClick="btnReport_Click" TabIndex="11"
                                                    ValidationGroup="submit" />
                                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel" TabIndex="12"
                                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ValidationGroup="submit" ShowSummary="false" />
                                            </div>
                                            <div class="form-group col-lg-7 col-md-12 col-12">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note</h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Check for Elective Course Registration and Uncheck for Deapprove Elective Course Registration.</span></p>
                                                </div>
                                            </div>

                                            <div class="col-12" id="divpanels" runat="server">

                                                <div class="col-md-6">
                                                    <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                                        <asp:ListView ID="lvStudent" runat="server">
                                                            <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="sub-heading">
                                                                        <h5>Student List</h5>
                                                                    </div>
                                                                    <div class="row mb-1">
                                                                        <div class="col-lg-3 col-md-6 offset-lg-5">
                                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                        </div>


                                                                        <div class="col-lg-4 col-md-6">
                                                                            <div class="input-group sea-rch">
                                                                                <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-search"></i>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>

                                                                    <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>--%>
                                                                    <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblHead">
                                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" OnCheckedChanged="cbRow_CheckedChanged" ToolTip="Select/Select all" />
                                                                                    </th>
                                                                                    <th>USN No.
                                                                                    </th>
                                                                                    <%--<th style="text-align: left; width: 15%">
                                                                            Enroll. No.
                                                                        </th>--%>
                                                                                    <th>Student Name
                                                                                    </th>
                                                                                    <th style="display: none">Elective Courses
                                                                                    </th>
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
                                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" Enabled='<%# (Eval("REGISTERED").ToString() == "0" ?true:false)%>'
                                                                            Checked='<%# (Eval("REGISTERED").ToString() == "1" ?true:false)%>' OnCheckedChanged="cbRow_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                                    </td>
                                                                    <%--<td style="text-align: left; width: 25%">
                                                                <asp:LinkButton ID="lblRegNo" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip="Click here to Display Registered Courses"
                                                                    CommandArgument='<%# Eval("IDNO") %>' OnClick="btnPrint_Click" ValidationGroup="submit"
                                                                    Font-Underline="false" ForeColor="Black" />
                                                            </td>--%>
                                                                    <td>
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td style="display: none"><%# Eval("NAME") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <%--<AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td>
                                    <asp:CheckBox ID="cbRow" runat="server" onClick="totStudents(this);" GroupName="BoxChk" />
                                </td>
                                <td >
                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                </td>
                              
                                <td>
                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                        Visible="false" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>--%>
                                                        </asp:ListView>

                                                    </asp:Panel>
                                                </div>
                                                <div class="col-md-6" style="display: none">
                                                    <asp:Panel ID="pnlCourses" runat="server" Visible="true" ScrollBars="Auto" Height="400px">
                                                        <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div id="divlvPaidReceipts">

                                                                    <h4>Offered Courses</h4>

                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Select
                                                                                </th>
                                                                                <th>Course Code - Course Name
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
                                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                                        &nbsp;
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                                    <td>
                                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                                    </td>
                                                                </tr>
                                                            </AlternatingItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlStudentsReamin" runat="server" Visible="true" ScrollBars="Auto">
                                                        <asp:ListView ID="lvStudentsRemain" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="table table-responsive">

                                                                    <h4>Student List (Demand Not Found)</h4>

                                                                    <table id="tblHead" class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue" id="trRow">
                                                                                <th>HT No.
                                                                                </th>
                                                                                <th>Student Name
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
                                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                            Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <%--<AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td >
                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' />
                                </td>
                                <td >
                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                        Visible="false" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>--%>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="tab-pane fade" id="Div4">
                                <div class="box-body">
                                    <div>
                                        <asp:UpdateProgress ID="updProg2" runat="server" AssociatedUpdatePanelID="updBulkCancel"
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
                                    <asp:UpdatePanel ID="updBulkCancel" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: red;">*</span> Admission Batch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatchCancel" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvAdmbatchcancel" runat="server" ControlToValidate="ddlAdmBatchCancel" ValidationGroup="submit" SetFocusOnError="true"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: red;">*</span> School & Scheme</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClgnameCancel" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgnameCancel_SelectedIndexChanged" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvClgnameCancel" runat="server" ControlToValidate="ddlClgnameCancel" SetFocusOnError="true"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="submit">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: red;">*</span>Session</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionCancel" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSessionCancel_SelectedIndexChanged" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvSessionCancel" runat="server" ControlToValidate="ddlSessionCancel" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: red;">*</span>Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemesterCancel" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            TabIndex="8" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSemesterCancel_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvSemesterCancel" runat="server" ControlToValidate="ddlSemesterCancel" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label><span style="color: red;">*</span> Section</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSectionCancel" runat="server" AppendDataBoundItems="true" TabIndex="9" CssClass="form-control" data-select2-enable="true"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSectionCancel_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvSectionCancel" runat="server" ControlToValidate="ddlSectionCancel" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <label><span style="color: red;">*</span> Elective Courses</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlElective" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlElective_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvElective" runat="server" ControlToValidate="ddlElective" SetFocusOnError="true"
                                                            Display="None" ErrorMessage="Please Select Elective Courses" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnCancelElective" runat="server" Text="Cancel Elective Courses" CssClass="btn btn-primary"
                                                    OnClientClick="return validateAssign();" TabIndex="10" OnClick="btnCancelElective_Click" />
                                                <asp:Button ID="btnRemove" runat="server" CausesValidation="False" Text="Cancel" TabIndex="12"
                                                    CssClass="btn btn-warning" OnClick="btnRemove_Click" />
                                            </div>
                                            <div class="col-md-12" id="div2" runat="server">
                                                <asp:Panel ID="PanelCancel" runat="server" Visible="false" ScrollBars="Auto" Height="400px">
                                                    <asp:ListView ID="lvCancelCourse" runat="server">
                                                        <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <table id="tblHead" class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue" id="trRow">
                                                                            <th>
                                                                                <asp:CheckBox ID="cbHead1" runat="server" onClick="totAll(this);" ToolTip="Select/Select all" />
                                                                            </th>
                                                                            <th>Sr.no.
                                                                            </th>
                                                                            <th>Session
                                                                            </th>
                                                                            <th>Student Name
                                                                            </th>
                                                                            <th>Course-Code
                                                                            </th>
                                                                            <th>Semester
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
                                                                    <asp:CheckBox ID="cbRowCancel" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" />
                                                                </td>
                                                                <td>
                                                                    <%# Container.DataItemIndex + 1%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSION_PNAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStudNameCancel" runat="server" Text='<%# Eval("STUDNAME") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSesmester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
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
    </div>


    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
            }
            //		var frm = document.forms[0]
            //		for (i=0; i < document.forms[0].elements.length; i++) {
            //			var e = frm.elements[i];
            //			if (e.type == 'checkbox') {
            //			    if (headchk.checked == true) {
            //			        if (e.disabled == false) { e.checked = true; }
            //			    }
            //			    else
            //			        e.checked = false;
            //			}
            //        }
            //        if (headchk.checked == true) {
            //            txtTot.value = hftot.value;
            //            }
            //        else {
            //            txtTot.value = 0;
            //        }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else if (document.getElementById('<%= ddlcourselist.ClientID %>').value == 0) {
                alert('Please Select Elective Course ');
                return false;
            }
            else
                return true;
        }


        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

    </script>
    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
            var table5 = document.querySelector('#tblHead');



            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });



        $(".saveAsExcel").click(function () {

            //if (confirm('Do You Want To Apply for New Program?') == true) {
            // return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
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
