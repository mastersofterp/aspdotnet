<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhdAnnexure_update.aspx.cs" Inherits="ACADEMIC_PHD_PhdAnnexure_update" %>

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
                                                        </ul>
                                                    </div>
                                                </div>

                                                <div class="row mt-3" id="DivDrops" runat="server" visible="false">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status Category</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlStatusCat" runat="server" AppendDataBoundItems="True" TabIndex="8"
                                                            ToolTip="Please Select Status Category" CssClass="form-control" data-select2-enable="true" Enabled="false">
                                                            <asp:ListItem Value="1" Text="Course Work Completed"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Course Work InCompleted"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--                                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Status Category"
                                                            ControlToValidate="ddlStatusCat" Display="None" SetFocusOnError="True" InitialValue="0"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Supervisor</label>
                                                        </div>

                                                        <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" TabIndex="13"
                                                            ToolTip="Please Select Supervisor" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSupervisor_SelectedIndexChanged" />
                                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="Is External" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSupervisor"
                                                            Display="None" ErrorMessage="Please Select Supervisor" InitialValue="0"
                                                            ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>



                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Area of Research </label>
                                                        </div>

                                                        <asp:TextBox ID="txtResearch" runat="server" CssClass="unwatermarked" Rows="1" class="form-control" TextMode="MultiLine" TabIndex="11"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtResearch"
                                                            Display="None" ErrorMessage="Please Enter Area of Research" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="nodgc" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>No.of DGC Member</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlNdgc" runat="server" TabIndex="12"
                                                            ToolTip="Please Select No.Of DGC" CssClass="form-control" data-select2-enable="true" Enabled="false" OnSelectedIndexChanged="ddlNdgc_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                            <asp:ListItem Selected="True" Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Remark for Cancellation </label>
                                                        </div>
                                                        <asp:TextBox ID="txtRemark" runat="server" Enabled="false"> </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRemark"
                                                            Display="None" ErrorMessage="Please Enter Remark for Cancellation" SetFocusOnError="True"
                                                            ValidationGroup="Reject"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <asp:Label ID="lblRejectStatus" runat="server" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="PnlSP" runat="server" Visible="false">
                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true" aria-controls="collapseTwo">
                                        <span class="title" id="trDGC" runat="server">Doctoral guidance committee (DGC)</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseTwo" class="collapse collapse show">
                                        <div class="card-body">
                                            <div id="Div1" class="divdoctrate" runat="server">
                                                <asp:Panel ID="pnlDoc" runat="server" Enabled="true">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Commitee Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCommittee" runat="server" TabIndex="15" ToolTip="Please Select Commitee Name" AutoPostBack="true"
                                                                    CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCommitee_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCommittee"
                                                                    Display="None" ErrorMessage="Please Select committee" InitialValue="0"
                                                                    ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                  <%--  <sup>* </sup>--%>
                                                                    <label>Supervisor Role</label>
                                                                </div>
                                                                 <%--patches updated--%>
                                                                <asp:DropDownList ID="ddlSupervisorrole" runat="server" AutoPostBack="true" AppendDataBoundItems="True"
                                                                    TabIndex="15" ToolTip="Please Select Supervisor role" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSupervisorrole_SelectedIndexChanged">
                                                                    <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="S">Singly</asp:ListItem>
                                                                    <asp:ListItem Value="J">Jointly</asp:ListItem>
                                                                    <asp:ListItem Value="T">Multiple</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSupervisorrole"
                                                                    Display="None" ErrorMessage="Please Select Supervisor Role" InitialValue="0"
                                                                    ValidationGroup="Academic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Supervisor </label>
                                                                        </div>

                                                                        <asp:DropDownList ID="ddlDGCSupervisor" runat="server" TabIndex="15" ToolTip="Please Select Supervisor"
                                                                            CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="Is External" OnCheckedChanged="CheckBox3_CheckedChanged" AutoPostBack="true" />
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Supervisor Designation </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlMember" runat="server" TabIndex="16" ToolTip="Please Select Member" AppendDataBoundItems="true"
                                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvDGCSupervisor" runat="server" ControlToValidate="ddlMember"
                                                                            Display="None" ErrorMessage="Please Select Supervisor Designation" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="expertrow" runat="server" visible="false">


                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Joint-Supervisor *(s)(if any) </label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlJointSupervisor' runat="server" AppendDataBoundItems="True"
                                                                            TabIndex="75" ToolTip="Please Select Joint Supervisor" CssClass="form-control" data-select2-enable="true"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlJointSupervisor_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="Is External" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlJointSupervisor"
                                                                            Display="None" ErrorMessage="Please Select Joint-Supervisor *(s)(if any) " InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Joint-Supervisor Designation </label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlMember1' runat="server" AutoPostBack="true" TabIndex="18" CssClass="form-control" data-select2-enable="true"
                                                                            ToolTip="Please Select Member" OnSelectedIndexChanged="ddlMember1_SelectedIndexChanged1" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlMember1"
                                                                            Display="None" ErrorMessage="Please Select Joint-Supervisor Designation" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <asp:Label ID="lblStatus1" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <%--ADDED FOR EXTRA SUPERVISOR--%>

                                                            <div class="form-group col-lg-6 col-md-6 col-12">


                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>One Institute faculty expert </label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlInstFac' runat="server" AppendDataBoundItems="True" TabIndex="20" AutoPostBack="true"
                                                                            ToolTip="Please Select One Institute faculty expert" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlInstFac_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="Is External" OnCheckedChanged="CheckBox4_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlInstFac"
                                                                            Display="None" ErrorMessage="Please Select One Institute faculty expert" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Supervisor Designation </label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlMember2' runat="server" TabIndex="15" ToolTip="Please Select Member" AutoPostBack="true" AppendDataBoundItems="true"
                                                                            CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlMember2"
                                                                            Display="None" ErrorMessage="Please Select One Supervisor Designation" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <asp:Label ID="lblStatus2" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12" id="secondsupervisor" runat="server">


                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Joint-Supervisor *(s)(if any)</label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlJointSupervisorSecond' runat="server" AppendDataBoundItems="True"
                                                                            OnSelectedIndexChanged="ddlJointSupervisorSecond_SelectedIndexChanged"
                                                                            AutoPostBack="true" TabIndex="15" ToolTip="Please Select Second Joint Supervisor" class="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="Is External" OnCheckedChanged="CheckBox6_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlJointSupervisorSecond"
                                                                            Display="None" ErrorMessage="Please Select Joint-Supervisor *(s)(if any)" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Joint-Supervisor Designation</label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlMember5' runat="server" AutoPostBack="true" TabIndex="15" AppendDataBoundItems="true"
                                                                            ToolTip="Please Select Member" class="form-control" OnSelectedIndexChanged="ddlMember5_SelectedIndexChanged" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlMember5"
                                                                            Display="None" ErrorMessage="Please Select Joint-Supervisor Designation" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label ID="lblStatus5" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-6 col-md-6 col-12">

                                                                <div class="row">
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>A DRC nominee </label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlDRC' runat="server" AppendDataBoundItems="True" TabIndex="22" AutoPostBack="true"
                                                                            ToolTip="Please Select A DRC nominee" class="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDRC_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CheckBox ID="CheckBox5" runat="server" Text="Is External" OnCheckedChanged="CheckBox5_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlDRC"
                                                                            Display="None" ErrorMessage="Please Select A DRC nominee" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>A DRC Nominee Designation</label>
                                                                        </div>
                                                                        <asp:DropDownList ID='ddlMember3' runat="server" TabIndex="15" ToolTip="Please Select Member" AppendDataBoundItems="true"
                                                                            OnSelectedIndexChanged="ddlMember3_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlMember3"
                                                                            Display="None" ErrorMessage="Please Select A DRC Nominee Designation" InitialValue="0" ValidationGroup="Academic"
                                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <asp:Label ID="lblStatus3" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card" id="divchairman" runat="server">
                <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="true">
                    <span class="title" id="trdrc" runat="server">Recommendation of the Departmental Research Committee(DRC)</span>
                    <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                </div>
                <div id="collapseThree" class="collapse collapse show">
                    <div class="card-body">
                        <div id="divdrc" runat="server" class="col-12">
                            The DRC recommends the registration of Mr./Mrs.<asp:Label ID="lblname" runat="server"
                                Text="name" Font-Bold="true"></asp:Label>&nbsp;<asp:Label ID="partfull" runat="server"></asp:Label>
                            student with effect from
                                                        <asp:Label ID="lbldate" runat="server" Font-Bold="true"></asp:Label>
                            and also recommends the appointment of supervisor (s) as he / she / they satisfy
                                                        rule R.7 of PhD ordinance (supervisors' Bio-data with list of publications and experience
                                                        be enclosed) and formation of DGC as indicated above.
                        </div>

                        <div id="trdrc1" runat="server" class="col-12 mt-3">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>A DRC Chairman </label>
                                    </div>

                                    <span class="input-group-addon">
                                        <asp:DropDownList ID='ddlDRCChairman' runat="server" TabIndex="24" ToolTip="Please Select A DRC nominee" OnSelectedIndexChanged="ddlDRCChairman_SelectedIndexChanged"
                                            CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlDRCChairman" 
                                            Display="None" ErrorMessage="Please Select A DRC Chairman" InitialValue="0" ValidationGroup="Academic"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </asp:Panel>

                            <div class="col-12">
                                <asp:Panel ID="pnlApprove" runat="server">
                                    <asp:ListView ID="lvApprove" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Alloted Supervisor List</h5>
                                                </div>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%--<th>Supervisor Role
                                                                </th>--%>
                                                                <th>Members
                                                                </th>
                                                                <th>Members Name
                                                                </th>
                                                                <th>Allotment Status
                                                                </th>
                                                                <th>Approved Status
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
                                                <%--<td><%#Eval("SUP_ROLE") %></td>--%>
                                                <td><%#Eval("ROLE") %></td>
                                                <td><%#Eval("FULLNAME") %></td>
                                                <td><%#Eval("AllotStatus") %></td>
                                                <td><%#Eval("STATUS") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

            <div class="col-12 btn-footer">

                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" TabIndex="26" Text="Submit" ValidationGroup="Academic" />

                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary" OnClick="btnApprove_Click" TabIndex="27" Text="Approve" ValidationGroup="Academic" Visible="false" />
                <asp:Button ID="btnReject" runat="server" CssClass="btn btn-warning" OnClick="btnReject_Click" TabIndex="28" Text="Reject" Visible="false" />
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="29" Text="Cancel" />
                <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
            </div>
        </div>
        </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <script type="text/javascript">
        jQuery(function ($) {
            $(document).ready(function () {
                bindDataTable();
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
            });
            function bindDataTable() {
                var myDT = $('#id1').DataTable({
                });
            }

        });
    </script>

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

</asp:Content>

