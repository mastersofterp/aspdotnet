<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievanceRedressalCell.aspx.cs" Inherits="GrievanceRedressal_Transaction_GrievanceRedressalCell" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upCommitteeMember"
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
    <asp:UpdatePanel ID="upCommitteeMember" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE REDRESSAL CELL</h3>
                        </div>
                        <div class="box-body">

                            <div class="col-12" id="divButtons" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdbGriv" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="2" OnSelectedIndexChanged="rdbGriv_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">Committee Type Member &nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Grievance Type Member &nbsp;&nbsp;</asp:ListItem>

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlCommitteTypeMember" runat="server">
                                <asp:Panel ID="pnlGrivRedressalCell" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Committee type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCommitteeType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlCommitteeType_SelectedIndexChanged"
                                                    ValidationGroup="show" AutoPostBack="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCommittee" runat="server"
                                                    ErrorMessage="Please Select Grievance Redressal Committee Type" ControlToValidate="ddlCommitteeType" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divDept" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Department</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDepartment" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True"
                                                    ValidationGroup="show" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ValidationGroup="Add"
                                                    ErrorMessage="Please Select Department" ControlToValidate="ddlDepartment" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12">
                                    <asp:Panel ID="pnlCDetails" runat="server">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Grievance Redressal Cell </h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Employee Department </label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmpDepartment" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEmpDepartment_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Committee Members</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCommitteeMembers" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    ValidationGroup="show" AutoPostBack="false" OnSelectedIndexChanged="ddlCommitteeMembers_SelectedIndexChanged1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCommitteeMember" runat="server" ValidationGroup="Add"
                                                    ErrorMessage="Please Select Committee Member" ControlToValidate="ddlCommitteeMembers" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Designation</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDesignation" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    ValidationGroup="show">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvdesignation" runat="server" ValidationGroup="Add"
                                                    ErrorMessage="Please Select Designation" ControlToValidate="ddlDesignation" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Status</label>
                                                </div>
                                                <asp:RadioButtonList ID="rdbStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1">
                                                    <asp:ListItem Value="A" Selected="True">Active&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="D">DeActive</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="Add"
                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnAdd_Click" CausesValidation="true" TabIndex="1" />
                                                <asp:ValidationSummary ID="VSAdd" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />

                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlRedressalCell" runat="server">
                                                    <asp:ListView ID="lvshowRedressalCell" runat="server" Visible="false">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>GRIEVANCE REDRESSAL CELL</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>EDIT
                                                                            </th>
                                                                            <th>COMMITTEE MEMBERS
                                                                            </th>
                                                                            <th>DESIGNATION
                                                                            </th>
                                                                            <th>STATUS
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
                                                                    <asp:ImageButton ID="btnEditRecord" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                        CommandArgument='<%#Eval("EMP_DEPARTMENTID") + ";" +Eval("COMMITTEE_MEMBERS")+ ";" +Eval("GR_DESIGID")+";"+Eval("STATUS_VALUE")%>'
                                                                        ImageUrl="~/Images/edit.png" TabIndex="1" ToolTip='<%# Eval("SRNO")%>' OnClick="btnEdit_Click" />
                                                                    <%--//CommandArgument='<%#Eval("DATE", "{0:yyyy/MM/dd}")+ ";" +Eval("TIME")+ ";" +Eval("YEAR_SEM")+ ";" +Eval("SUBJECT")+";"+Eval("FACULTY_NAME")%>'
                                                                        //AlternateText="Edit Record" ToolTip='<%# Eval("SEQNO")%>' Enabled="true" OnClick="btnEditEng_Click" />&nbsp;--%>
                                                                    <asp:HiddenField ID="hdnsrno" runat="server" Value='<%# Eval("SRNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMMITTEE_MEMBERS")%>
                                                                    <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UANO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DESIGNATION")%>
                                                                    <asp:HiddenField ID="hdndesignId" runat="server" Value='<%# Eval("GR_DESIGID") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STATUS")%>
                                                                    <asp:HiddenField ID="hdnEmpDepartment" runat="server" Value='<%# Eval("EMP_DEPARTMENTID") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class=" col-12 btn-footer">

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" CausesValidation="true" TabIndex="1" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click" CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="1" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />

                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlCommittee" runat="server">
                                        <asp:ListView ID="lvCommittee" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>GRIEVANCE REDRESSAL CELL</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Grievance Redressal Committee type
                                                                </th>
                                                                <th>Department
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
                                                        <asp:ImageButton ID="btnEditRecord" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                            CommandArgument='<%# Eval("GRC_ID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditRecord_Click"
                                                            ToolTip="Edit Record" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("GR_COMMITTEE")%>
                                                        <asp:HiddenField ID="hdnGrCommittee" runat="server" Value='<%# Eval("GRCT_ID") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTNAME ")%>
                                                        <asp:HiddenField ID="hdnsubdept" runat="server" Value='<%# Eval("DEPT_ID") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                            <%--Panel For Grievance Type Committee--%>
                            <asp:Panel ID="pnlSubGType" runat="server" Visible="false">
                                <asp:Panel ID="pnlSub" runat="server">
                                    <div class="col-12">
                                        <div class="row">

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Grievance type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlGrivType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                    ValidationGroup="show" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlGrivType_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvGrivType" runat="server" ErrorMessage="Please Select Grievance Type"
                                                    ControlToValidate="ddlGrivType" Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="AddSub"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Sub-Grievance type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubGriv" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                    ValidationGroup="show" AutoPostBack="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12">
                                    <asp:Panel ID="pnlSubCom" runat="server">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Grievance Redressal Cell </h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Employee Department </label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmp" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlEmp" runat="server" ValidationGroup="AddSub"
                                                    ErrorMessage="Please Select Employee Department" ControlToValidate="ddlEmp" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Committee Members</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSubMem" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    ValidationGroup="show" AutoPostBack="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlSubMem" runat="server" ValidationGroup="AddSub"
                                                    ErrorMessage="Please Select Committee Member" ControlToValidate="ddlSubMem" Display="None"
                                                    InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Designation</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDes" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="True" TabIndex="1"
                                                    ValidationGroup="show">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDes" runat="server" ValidationGroup="AddSub"
                                                    ErrorMessage="Please Select Designation" ControlToValidate="ddlDes" Display="None"
                                                    InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Status</label>
                                                </div>
                                                <asp:RadioButtonList ID="rdSubStatus" runat="server" RepeatDirection="Horizontal" TabIndex="1">
                                                    <asp:ListItem Value="A" Selected="True">Active&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="D">DeActive</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAddSub" runat="server" Text="Add" ValidationGroup="AddSub"
                                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnAddSub_Click" CausesValidation="true" TabIndex="1" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddSub" />

                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlSubList" runat="server">
                                                    <asp:ListView ID="lvAddSub" runat="server" Visible="false">
                                                        <LayoutTemplate>
                                                            <div id="lgv1">
                                                                <div class="sub-heading">
                                                                    <h5>GRIEVANCE REDRESSAL CELL</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>EDIT
                                                                            </th>
                                                                            <th>COMMITTEE MEMBERS
                                                                            </th>
                                                                            <th>DESIGNATION
                                                                            </th>
                                                                            <th>STATUS
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
                                                                    <asp:ImageButton ID="btnEditSub" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                        CommandArgument='<%#Eval("SUBEMP_DEPARTMENTID") + ";" +Eval("SUBUANO")+ ";" +Eval("SUBDESIGID")+";"+Eval("SUBSTATUS_VALUE")%>'
                                                                        ImageUrl="~/Images/edit.png" TabIndex="1" ToolTip='<%# Eval("SUBSRNO")%>' OnClick="btnEditSub_Click" />
                                                                    <%--//CommandArgument='<%#Eval("DATE", "{0:yyyy/MM/dd}")+ ";" +Eval("TIME")+ ";" +Eval("YEAR_SEM")+ ";" +Eval("SUBJECT")+";"+Eval("FACULTY_NAME")%>'
                                                                        //AlternateText="Edit Record" ToolTip='<%# Eval("SEQNO")%>' Enabled="true" OnClick="btnEditEng_Click" />&nbsp;--%>
                                                                    <%--                                                              <asp:ImageButton ID="btnDeletesub" runat="server" AlternateText="Delete Record"CommandArgument='<%#Eval("SUBEMP_DEPARTMENTID") + ";" +Eval("SUB_GRIV_MEM")+ ";" +Eval("SUBDESIGID")+";"+Eval("SUBSTATUS_VALUE")%>'
                                                                       ImageUrl="~/images/delete.png" TabIndex="1" ToolTip='<%# Eval("SUBSRNO")%>' OnClick="btnDeletesub_Click" />--%>
                                                                    <asp:HiddenField ID="hdnSUBSRNO" runat="server" Value='<%# Eval("SUBSRNO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUB_GRIV_MEM")%>
                                                                    <asp:HiddenField ID="hdnSubuano" runat="server" Value='<%# Eval("SUBUANO") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUBDESIGNATION")%>
                                                                    <asp:HiddenField ID="hdnSubDes" runat="server" Value='<%# Eval("SUBDESIGID") %>' />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUBSTATUS_VALUE")%>
                                                                    <asp:HiddenField ID="hdnSubDeptId" runat="server" Value='<%# Eval("SUBEMP_DEPARTMENT") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class=" col-12 btn-footer">

                                    <asp:Button ID="btnSubmitSub" runat="server" Text="Submit" ValidationGroup="SaveSub" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="1" OnClick="btnSubmitSub_Click" />
                                    <%--<asp:Button ID="Button3" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click" CssClass="btn btn-info" CausesValidation="false" ToolTip="Click here to Show Report" />--%>
                                    <asp:Button ID="btnCancelSub" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="1" OnClick="btnCancelSub_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SaveSub" />

                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="pnlSubTypeList" runat="server">
                                        <asp:ListView ID="lvSubMem" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>GRIEVANCE REDRESSAL CELL</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit
                                                                </th>
                                                                <th>Grievance type
                                                                </th>
                                                                <th>Grievance Sub type
                                                                </th>
                                                                <th>Department
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
                                                        <asp:ImageButton ID="btnEditSubRec" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                            CommandArgument='<%# Eval("SUB_GR_ID") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditSubRec_Click"
                                                            ToolTip="Edit Record" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("GT_NAME")%>
                                                        <asp:HiddenField ID="hgnGTNAME" runat="server" Value='<%# Eval("GRIV_ID") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBGTNAME")%>
                                                        <asp:HiddenField ID="hdnSubGT" runat="server" Value='<%# Eval("GRIV_ID") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEPTNAME ")%>
                                                        <asp:HiddenField ID="hdnsubdept" runat="server" Value='<%# Eval("DEPT_ID") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


