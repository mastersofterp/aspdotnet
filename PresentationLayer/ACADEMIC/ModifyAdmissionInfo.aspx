<<<<<<< HEAD
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ModifyAdmissionInfo.aspx.cs" Inherits="ACADEMIC_ModifyAdmissionInfo" %>
=======
﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true" CodeFile="ModifyAdmissionInfo.aspx.cs" Inherits="ACADEMIC_ModifyAdmissionInfo" %>
>>>>>>> UAT_TO_MAIN_2023-10-30/06-30PM

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="dvMain" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div id="pnlSearch" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Criteria</label>
                                        </div>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                        <asp:Panel ID="pnltextbox" runat="server">
                                            <div id="divtxt" runat="server" style="display: block">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Search String</label>
                                                </div>
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()"></asp:TextBox>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlDropdown" runat="server">
                                            <div id="divDropDown" runat="server" style="display: block">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                </div>
                                                <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-lg-3 col-md-12 col-12 btn-footer mt-3">
                                        <%-- OnClientClick="return submitPopup(this.name);"--%>
                                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlLV" runat="server">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Sr. No.
                                                                    </th>
                                                                    <th>Name
                                                                    </th>
                                                                    <th>Adm. Status
                                                                    </th>
                                                                    <th style="display: none">IdNo
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Father Name
                                                                    </th>
                                                                    <th>Mother Name
                                                                    </th>
                                                                    <th>Mobile No.
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                        OnClick="lnkId_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAdmcan" Font-Bold="true" runat="server" ForeColor='<%# Eval("ADMCANCEL").ToString().Equals("ADMITTED")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ADMCANCEL")%>'></asp:Label>
                                                </td>
                                                <td style="display: none">
                                                    <%# Eval("idno")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FATHERNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MOTHERNAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDENTMOBILE") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="col-12" id="AdmDetails" runat="server">
                            <div class="sub-heading" id="trAdmission" runat="server">
                                <h5>Admission Details</h5>
                            </div>
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Father's Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Mother's Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>

                                    </ul>
                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>
                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                            :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Gender :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblGender" runat="server" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>

                                    </ul>

                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Application ID :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblApplicationId" runat="server" Font-Bold="True"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Enrollment No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>


                                    </ul>

                                </div>

                            </div>

                            <div id="divAdmissionDetails" class="mt-4" runat="server">
                                <div class="row" id="tbladmission">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Admission Batch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="True"
                                            ToolTip="Please Select Batch" TabIndex="5" ValidationGroup="Academic"
                                            CssClass="form-control" data-select2-enable="true" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" SetFocusOnError="True" ErrorMessage="Please Select Admission Batch" ValidationGroup="Academic"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                            ToolTip="Please select Semester" TabIndex="7" ValidationGroup="Academic" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" SetFocusOnError="True" ErrorMessage="Please Select Semester" ValidationGroup="Academic"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Admission Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlclaim" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Enter Caste Category"
                                            ValidationGroup="Academic">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlclaim"
                                            Display="None" SetFocusOnError="True" ErrorMessage="Please Select Admission Type" ValidationGroup="Academic"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                             <sup>*</sup>
                                            <label>Academic Year </label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcademicYear" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="8" ToolTip="Please Select Academic Year"
                                            ValidationGroup="Academic">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAcademicYear"
                                            Display="None" SetFocusOnError="True" ErrorMessage="Please Select Academic Year" ValidationGroup="Academic"
                                            InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="11" Text="Submit" ToolTip="Click to Submit" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary" ValidationGroup="Academic" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Academic" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

