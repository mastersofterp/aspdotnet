<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhdStudProgress.aspx.cs" Inherits="ACADEMIC_PHD_PhdStudProgress" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="myModal2" role="dialog" runat="server">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updEdit"
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

            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:Panel ID="pnDisplay" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                                </h3>
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </div>

                            <div class="box-body">
                                <asp:UpdatePanel ID="updEdit" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divCriteria" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Search Criteria</label>
                                                    </div>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmBatch" runat="server" visible="false">
                                                    <span style="color: red;">*</span><label>Admission Batch</label>
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Admission Batch" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                        Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="submit" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">

                                                    <asp:Panel ID="pnltextbox" runat="server">
                                                        <div id="divtxt" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Search String</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
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
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return submitPopup(this.name);" />

                                                <asp:Button ID="btnClose" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panellistview" runat="server">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5>Student List</h5>
                                                            </div>
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Name
                                                                            </th>
                                                                            <th style="display: none">IdNo
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Branch--%>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Semester--%>
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
                                                            </asp:Panel>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkId_Click"></asp:LinkButton>
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
                                                                <%# Eval("SEMESTERNAME")%>
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
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvStudent" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div id="divmain" runat="server" visible="false">
                                    <div class="accordion" id="accordionExample">
                                        <div class="card" runat="server" id="DivSutLog">
                                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                                <span class="title">General Information</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseOne" class="collapse show">
                                                <div class="card-body">
                                                    <div class="col-12" id="DivGenInfo" runat="server" visible="true">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>ID No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblidno" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Enrollment No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblenrollmentnos" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Date of Joining :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbljoiningdate" runat="server" Font-Bold="True"></asp:Label>
                                                                            <asp:HiddenField ID="hfdegreenos" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Status :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblstatussup" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Supervisor Role :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblSuperRole" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>No.of DGC Member :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblNDM" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblnames" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Father Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblfathername" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Department :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblDepartment" runat="server" Font-Bold="true"></asp:Label>
                                                                            <asp:HiddenField ID="hfDepartment" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Admission Batch :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbladmbatch" runat="server" Font-Bold="True"></asp:Label>
                                                                            <asp:HiddenField ID="hfadmbatch" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Scheme :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Area of Research :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblAoR" runat="server" Font-Bold="True"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="PnlSP" runat="server" Visible="true">
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                                <span class="title" id="trDGC" runat="server"><span style="color: red;">* </span>PHD Research Topic</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseTwo" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div id="Div1" class="divdoctrate" runat="server">
                                                        <asp:Panel ID="pnlDoc" runat="server" Enabled="true">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>SEMESTER</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlsem" runat="server" TabIndex="1" ToolTip="Please Select Commitee Name" AutoPostBack="true"
                                                                            CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlsem_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlsem"
                                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                                            ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-8 col-md-6 col-12">
                                                                        <label><span style="color: red;">*</span><b> Research Topic</b></label>
                                                                        <asp:TextBox ID="txtReserchTopic" runat="server" CssClass="form-control" Height="50px" TabIndex="2" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvTopic" runat="server"
                                                                            ErrorMessage="Please Enter Research Topic" SetFocusOnError="True" Display="None"
                                                                            ControlToValidate="txtReserchTopic" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlWd" runat="server" Visible="true">
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseone" aria-expanded="true" aria-controls="collapseone">
                                                <span class="title" id="trDGC1" runat="server"><span style="color: red;">* </span>DESCRIPTION OF WORK DONE BY STUDENT DURING THE PERIOD</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseone" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div id="Div3" class="divdoctrate" runat="server">
                                                        <asp:Panel ID="Panel3" runat="server" Enabled="true">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                                        <asp:TextBox ID="txtWorkDone" runat="server" CssClass="form-control" Height="100px" TabIndex="3" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ErrorMessage="Please Enter Description Of Work Done" SetFocusOnError="True" Display="None"
                                                                            ControlToValidate="txtWorkDone" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="txtRemain" runat="server" Text="" ForeColor="Red" Enabled="False" Visible="false" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlRemark" runat="server" Visible="true">
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true" aria-controls="collapseThree">
                                                <span class="title" id="Span1" runat="server"><span style="color: red;">* </span>REMARKS OF ON THE WORK DONE BY THE CANDIDATE</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseThree" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div id="Div5" class="divdoctrate" runat="server">
                                                        <asp:Panel ID="Panel4" runat="server" Enabled="true">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" Height="50px" TabIndex="4" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                            ErrorMessage="Please Enter Remark" SetFocusOnError="True" Display="None"
                                                                            ControlToValidate="txtRemark" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Grade Awarded</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGreade" runat="server" TabIndex="5" ToolTip="Please Select Grade Awarded" AutoPostBack="true"
                                                                            CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Satisfactory</asp:ListItem>
                                                                            <asp:ListItem Value="2">UnSatisfactory</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGreade"
                                                                            Display="None" ErrorMessage="Please Select Grade Awarded" InitialValue="0"
                                                                            ValidationGroup="Approve" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="Panel1" runat="server" Visible="true">
                                        <div class="card">
                                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="true" aria-controls="collapseFour">
                                                <span class="title" id="Span2" runat="server"><span style="color: red;">* </span>COMMENTS</span>
                                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                            </div>
                                            <div id="collapseFour" class="collapse collapse show">
                                                <div class="card-body">
                                                    <div id="Div4" class="divdoctrate" runat="server">
                                                        <asp:Panel ID="Panel5" runat="server" Enabled="true">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-8 col-md-12 col-12">
                                                                        <asp:TextBox ID="txtComment" runat="server" TabIndex="6" CssClass="form-control" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                            ErrorMessage="Please Enter Comment" SetFocusOnError="True" Display="None"
                                                                            ControlToValidate="txtComment" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <%--                                <asp:Panel ID="PnlAppRemark" runat="server" Enabled="true">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>ApproveRemark</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAppRemark" runat="server" TabIndex="6" CssClass="form-control" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ErrorMessage="Please Enter Comment" SetFocusOnError="True" Display="None"
                                                        ControlToValidate="txtAppRemark" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>--%>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" Visible="false" OnClick="btnSave_Click" ToolTip="Click to Submit." TabIndex="7" ValidationGroup="Academic" />

                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-primary" Visible="false" OnClick="btnApprove_Click" ToolTip="Click to Approve" TabIndex="8" ValidationGroup="Approve" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" Visible="false" OnClick="btnCancel_Click" ToolTip="Click to cancel." TabIndex="9" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Academic" Style="text-align: center" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Approve" Style="text-align: center" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlApprove" runat="server">
                                            <asp:ListView ID="lvApprove" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Current Session Progress Report Status</h5>
                                                        </div>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Members
                                                                        </th>
                                                                        <th>Members Name
                                                                        </th>
                                                                        <th>Status
                                                                        </th>
                                                                        <%-- <th>Remark
                                                                        </th>--%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("ROLE") %></td>
                                                        <td><%#Eval("FULLNAME") %></td>
                                                        <td><%#Eval("APPROVAL") %></td>
                                                        <%--<td></td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {
            debugger
            $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });
        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                    var searchtxt = skillsSelect;
                    if (searchtxt == "0") {
                        alert('Please Select ' + ddlname + '..!');
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        return true;
                        //$("#<%= divpanel.ClientID %>").hide();
                        $("#<%= pnltextbox.ClientID %>").hide();

                    }
                }
                else if (rbText == "BRANCH") {

                    if (searchtxt == "Please Select") {
                        alert('Please Select Branch..!');

                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);

                        return true;
                    }

                }
                else {
                    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                    if (searchtxt == "" || searchtxt == null) {
                        alert('Please Enter Data to Search.');
                        //$(searchtxt).focus();
                        document.getElementById('<%=txtSearch.ClientID %>').focus();
                        return false;
                    }
                    else {
                        __doPostBack(btnsearch, rbText + ',' + searchtxt);
                        //$("#<%= divpanel.ClientID %>").hide();
                        //$("#<%= pnltextbox.ClientID %>").show();

                        return true;
                    }
                }
        }
    }

    function ClearSearchBox(btncancelmodal) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btncancelmodal, '');
        return true;
    }
    function CloseSearchBox(btnClose) {
        document.getElementById('<%=txtSearch.ClientID %>').value = '';
        __doPostBack(btnClose, '');
        return true;
    }




    function Validate() {

        debugger

        var rbText;

        var e = document.getElementById("<%=ddlSearch.ClientID%>");
        var rbText = e.options[e.selectedIndex].text;

        if (rbText == "IDNO" || rbText == "Mobile") {

            var char = (event.which) ? event.which : event.keyCode;
            if (char >= 48 && char <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        else if (rbText == "NAME") {

            var char = (event.which) ? event.which : event.keyCode;

            if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                return true;
            }
            else {
                return false;
            }

        }
    }
    </script>
    <script type="text/javascript">

        function CountCharacters() {
            var maxSize = 700;

            if (document.getElementById('<%= txtWorkDone.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtWorkDone.ClientID %>');
                var len = document.getElementById('<%= txtWorkDone.ClientID %>').value.length;
                if (len <= maxSize) {
                    var diff = parseInt(maxSize) - parseInt(len);

                    if (document.getElementById('<%= txtRemain.ClientID %>')) {
                        document.getElementById('<%= txtRemain.ClientID %>').value = diff;
                    }
                }
                else {
                    ctrl.value = ctrl.value.substring(0, maxSize);
                }
            }

            return false;
        }
    </script>

</asp:Content>

