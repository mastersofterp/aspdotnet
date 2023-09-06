<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IGradeEntry.aspx.cs" Inherits="ACADEMIC_EXAMINATION_IGradeEntry" Title="" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../newbootstrap/js/jquery-3.3.1.min.js"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updGrade">
            <ProgressTemplate>
                <progresstemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </progresstemplate>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updGrade" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">I Grade Entry</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:RadioButton ID="rdoSingle" runat="server" Text="Single I Grade Entry" GroupName="Grade" OnCheckedChanged="rdoSingle_CheckedChanged1" AutoPostBack="true" TabIndex="1" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoMultiple" runat="server" Text="Bulk I Grade Entry" GroupName="Grade" OnCheckedChanged="rdoMultiple_CheckedChanged" AutoPostBack="true" TabIndex="2" />
                                    </div>
                                </div>
                                <div id="divMultiple" runat="server" visible="false" class="row mt-3">




                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Scheme</label>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="Show" data-select2-enable="true" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3" ValidationGroup="Show" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlSession" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSession" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4" ValidationGroup="" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege" ErrorMessage="Please Select School/Institute Name" Display="None" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5" ValidationGroup="" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree" ErrorMessage="Please Select Degree" Display="None" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ValidationGroup="" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch" ErrorMessage="Please Select Programme/Branch" Display="None" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="7" ValidationGroup="" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlScheme" ErrorMessage="Please Select Scheme" Display="None" InitialValue="0" ValidationGroup=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="8" ValidationGroup="Show" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester" ErrorMessage="Please Select Semester" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="9" ValidationGroup="Show" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSubject" ErrorMessage="Please Select Subject" Display="None" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="10" ValidationGroup="Show" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="11" ValidationGroup="Show" OnClick="btnSubmit_Click" Enabled="false" />
                                        <asp:Button ID="btnExcel" runat="server" Text="I Grade Entry Report(Excel)" TabIndex="19" ValidationGroup="Report" OnClick="btnExcel_Click" CssClass="btn btn-info" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="False" TabIndex="12" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="validSummary" runat="server" ValidationGroup="Show" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Report" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />

                                    </div>
                                </div>
                                <div id="divSingle" runat="server" visible="false" class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSessionSingle" runat="server" CssClass="form-control" TabIndex="13" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSessionSingle_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvalidator7" runat="server" ControlToValidate="ddlSessionSingle" ErrorMessage="Please Select Session" Display="None" InitialValue="0" ValidationGroup="ShowSingle"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemesterSingle" runat="server" CssClass="form-control" TabIndex="14" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemesterSingle_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSemesterSingle" ErrorMessage="Please Select Semester" Display="None" InitialValue="0" ValidationGroup="ShowSingle"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Registration No</label>
                                        </div>
                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" TabIndex="15"></asp:TextBox><br />
                                        <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" InvalidChars="~`!@#$%^&*()_-+={[}]:;'<,>.?/*"
                                                    FilterMode="ValidChars" TargetControlID="txtRegNo">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRegNo" ErrorMessage="Please Enter Registration No" Display="None" ValidationGroup="ShowSingle"></asp:RequiredFieldValidator>
                                    </div>


                                    <%--added by prafull--%>

                                    <div class="col-12" id="divstudDetails" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>Student details</h5>
                                        </div>
                                        <div class="row">

                                            <div class="col-lg-6 col-md-6 col-12">

                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Student Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="true" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>RRN No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Admission Batch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Degree / Branch :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Scheme :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="true" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                   
                                    <div class="col-12 btn-footer mt-3">
                                        <asp:Button ID="btnShowSingle" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="ShowSingle" TabIndex="16" OnClick="btnShowSingle_Click" />
                                        <asp:Button ID="btnSubmitSingle" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="ShowSingle" TabIndex="17" OnClick="btnSubmitSingle_Click" Enabled="false" />
                                        <asp:Button ID="btnCancelSingle" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="18" OnClick="btnCancelSingle_Click" />
                                        <asp:ValidationSummary ID="validSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowSingle" />
                                    </div>

                                </div>



                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudents" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbllist">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Select</th>
                                                        <th>Sr.No.</th>
                                                        <th>Registration No</th>
                                                        <th>Student Name</th>
                                                        <th>Grade</th>
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
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%#Eval("IDNO")%>' OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME") %>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrade" runat="server" CssClass="form-control" TabIndex="11" Text='<%# Eval("GRADE") %>' MaxLength="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="I"
                                                        FilterMode="ValidChars" TargetControlID="txtGrade">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 mt-3" id="divSubjectsList" runat="server" visible="false">
                                <asp:Panel ID="pnlSubjects" runat="server" Visible="false">
                                    <asp:ListView ID="lvSubjects" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tbllist">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Select</th>
                                                        <th>Sr.No.</th>
                                                        <th>Subject Code</th>
                                                        <th>Subject Name</th>
                                                        <th>Grade</th>
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
                                                    <asp:CheckBox ID="chkSubject" runat="server" ToolTip='<%#Eval("COURSENO")%>' OnCheckedChanged="chkSubject_CheckedChanged" AutoPostBack="true" />
                                                </td>
                                                <td>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </td>
                                                <td>
                                                    <%# Eval("CCODE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("COURSENAME") %>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGrade_Sub" runat="server" CssClass="form-control" TabIndex="19" Text='<%# Eval("GRADE") %>' MaxLength="1" Enabled="false"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="I"
                                                        FilterMode="ValidChars" TargetControlID="txtGrade_Sub">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStatusSub" runat="server"></asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    
</asp:Content>

