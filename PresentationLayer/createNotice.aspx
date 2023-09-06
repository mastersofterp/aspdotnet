<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="createNotice.aspx.cs" Inherits="notice" Title="" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updIntake"
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

    <asp:UpdatePanel ID="updIntake" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">NOTICE MANAGEMENT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <%--<legend class="legendPay">Create Notice</legend>--%>

                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Notice Title</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="99" CssClass="form-control" AutoComplete="off" TabIndex="1" />
                                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                                        Display="None" ErrorMessage="Please Enter Notice Title." ValidationGroup="News">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTitle" runat="server" TargetControlID="txtTitle"
                                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Link Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtLinkName" runat="server" MaxLength="60" CssClass="form-control" AutoComplete="off" TabIndex="1" />
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Expiry Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div id="icon" class="input-group-addon">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtExpiryDate" runat="server" onchange="CheckDate(this);" CssClass="form-control" AutoComplete="off" TabIndex="1" />
                                                        <%--&nbsp;<asp:Image ID="imgExpDate" runat="server" ImageUrl="~/images/calendar.png"
                                        Style="cursor: pointer" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="ceExpDate" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtExpiryDate" PopupButtonID="icon">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                                            Display="None" ErrorMessage="Please Select/Enter Expiry Date." ValidationGroup="News">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>User Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlUserType" runat="server" AppendDataBoundItems="true"
                                                        OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"
                                                        AutoPostBack="true" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvUsertype" runat="server" ControlToValidate="ddlUserType"
                                                        Display="None" ErrorMessage="Please Select User Type." ValidationGroup="News" InitialValue="0" Visible="false">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Upload File</label>
                                                    </div>
                                                    <asp:FileUpload ID="fuFile" runat="server" TabIndex="1" />
                                                    <%-- <asp:HiddenField ID="hdnFile" runat="server" />--%>
                                                    <asp:Label ID="lblfile" runat="server" />
                                                </div>


                                                <div class="form-group col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>User Type</label>
                                                            </div>
                                                            <div class="form-group col-md-12 checkbox-list-box">
                                                                <%--<asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" onclick="SelectAll(this);" TabIndex="1" CssClass="checkbox-list-style" />--%>
                                                                <asp:CheckBoxList ID="chklUserType" runat="server" CellPadding="1" CellSpacing="8" RepeatColumns="1"
                                                                    AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12 d-none">
                                                            <asp:CheckBox ID="chkAllUsers" runat="server" Text="All Users" TabIndex="1"
                                                                OnCheckedChanged="chkAllUsers_CheckedChanged" AutoPostBack="true" CssClass="form-control" />
                                                            <asp:CustomValidator runat="server" ID="cvmodulelist" ClientValidationFunction="ValidateModuleList" Display="Dynamic" ErrorMessage="Please select state">
                                                            </asp:CustomValidator>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divDept" runat="server" style="display: none" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department</label>
                                                            </div>

                                                            <asp:DropDownList ID="ddlDept" runat="server" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" onchange="hidefaultyshow(this);">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDept"
                                                                Display="None" ErrorMessage="Please Select Department." ValidationGroup="News" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12" runat="server" style="display: none" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Department</label>
                                                            </div>
                                                            <div class="form-group col-md-12 checkbox-list-box">
                                                                <%-- <asp:CheckBox ID="chkselectDept" runat="server" Text="Select All" onclick="SelectAllDepts(this)" TabIndex="1" CssClass="checkbox-list-style" />--%>
                                                                <asp:CheckBoxList ID="chklDepartment" runat="server" CellPadding="1" CellSpacing="8" RepeatColumns="1" TabIndex="1"
                                                                    RepeatDirection="Horizontal">
                                                                </asp:CheckBoxList>
                                                                <asp:HiddenField ID="hdfDepartment" runat="server" Value="0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group col-12 d-none">
                                                            <asp:CheckBox ID="chkAll" runat="server" Text="Select All" CssClass="form-control" onclick="SelectAllDepts(this)" ToolTip="Select All" TabIndex="1" Visible="false" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" EnableVie />
                                                        </div>
                                                        <div class="form-group col-12 mt-4">
                                                            <asp:RadioButtonList ID="rblFilter" runat="server" AppendDataBoundItems="true"
                                                                AutoPostBack="true" OnSelectedIndexChanged="rblFilter_SelectedIndexChanged" RepeatColumns="3" TabIndex="1">
                                                                <asp:ListItem Value="2">Filter Students &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="3">Filter Faculty</asp:ListItem>
                                                                <asp:ListItem Value="8">Filter HOD</asp:ListItem>
                                                                <asp:ListItem Value="5">Filter Non-Teaching</asp:ListItem>
                                                                <%--<asp:ListItem Value="9" >Filter Affiliated College</asp:ListItem>--%>
                                                                <asp:ListItem Value="14">Filter Parent</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12" id="divNote" runat="server" visible="false">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Click On 'Show Student List' For Sending Notice To Selected Student.</span>  </p>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                        <div class="col-lg-6 col-md-12 col-sm-12">
                                            <div class="row">
                                                <div class="form-group col-lg-12 col-md-12 col-12 table-responsive">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Description</label>
                                                    </div>
                                                    <FTB:FreeTextBox ID="ftbDesc" runat="Server" Height="200px" Width="520px" TabIndex="1" />
                                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="ftbDesc"
                                                        Display="None" ErrorMessage="Please Enter Description." ValidationGroup="News"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divdepartment" runat="server" visible="false">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:ListBox ID="lstbxdept" runat="server" CssClass="form-control multi-select-demo"
                                            AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvdepart" runat="server" ControlToValidate="lstbxdept"
                                            Display="None" ErrorMessage="Please Select Department." ValidationGroup="News">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="lstbxdept"
                                            Display="None" ErrorMessage="Please Select Department." ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div id="divStudent" runat="server" visible="false">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College." ValidationGroup="News" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College." ValidationGroup="Show" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="true" InitialValue="0" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree." ValidationGroup="News" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree." ValidationGroup="Show" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Programme/Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="1"
                                                CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Programme/Branch." ValidationGroup="News">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBranch" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Programme/Branch." ValidationGroup="Show">
                                            </asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" TabIndex="1"
                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester." ValidationGroup="News" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester." ValidationGroup="Show" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                            <%--<asp:CustomValidator ID="CustomValidator1" ControlToValidate="ddlSemester" OnServerValidate="CustomValidator1_ServerValidate" ErrorMessage="Please Select Semester" runat="server" Display="None" ValidationGroup="News"/>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" Text="Show Student List" OnClick="btnShow_Click" ValidationGroup="Show" TabIndex="1"
                                        CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnShowFaculty" runat="server" Text="Show Faculty List" OnClick="btnShowFaculty_Click" ValidationGroup="Show" TabIndex="1"
                                        CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnParentList" runat="server" Text="Show Parent List" OnClick="btnParentList_Click" ValidationGroup="Show"
                                        CssClass="btn btn-primary" Visible="false" />

                                    <asp:HiddenField ID="hdffacultycount" runat="server" />
                                    <%--<p class="text-center">--%>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="1"
                                        ValidationGroup="News" CssClass="btn btn-primary" OnClientClick="return validate();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="1"
                                        CausesValidation="False" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="News" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                    <%-- </p>--%>
                                    <div>
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                    </div>
                                </div>
                                <div class="box-footer col-md-12">
                                    <div class="form-group col-12 ">
                                        <asp:Panel ID="pnlStudent" runat="server" Visible="false">
                                            <asp:ListView ID="lvStudent" runat="server" Visible="true">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>
                                                                <asp:Label ID="lblstudent" runat="server" Text="Students List"></asp:Label>
                                                            </h5>
                                                        </div>

                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblData">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Select
                                                        <asp:CheckBox ID="chkAll" runat="server" onChange="CheckAll(this)" />
                                                                        </th>
                                                                        <th>Registration No.
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="Label1" runat="server" Text="Student Name"></asp:Label></th>
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

                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Convert.ToInt32(Eval("Flag"))==2 ? false : true %>' />
                                                            <%--<asp:CheckBox ID="chkSelect" runat="server" TabIndex="6" Value='<%# Eval("SPECIAL_STUD") %>' />--%>
                                                            <asp:HiddenField ID="hdnIdno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("REGNO") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STUDNAME") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-12">
                                        <asp:Panel ID="pnlFaculty" runat="server" Visible="false">
                                            <asp:ListView ID="lvFacultyList" runat="server" Visible="true">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Users List</h5>
                                                        </div>
                                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblData">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Select
                                                                         <asp:CheckBox ID="chkAll" runat="server" onChange="CheckAll(this)" />
                                                                            <%-- <asp:CheckBox ID="chkAll" runat="server" onclick="SelectAllfaculty(this);" />--%>
                                                                        </th>
                                                                        <th>User Name
                                                                        </th>
                                                                        <th>Faculty Name</th>
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
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Convert.ToInt32(Eval("STATUS"))==0 ? false : true %>' TabIndex="6" />
                                                            <asp:HiddenField ID="hdnuano" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("UA_NAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("UA_FULLNAME") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div>
                                    <asp:HiddenField ID="hdnCount" runat="server" />
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvNotice" runat="server">
                                            <LayoutTemplate>

                                                <div class="sub-heading">
                                                    <h5>Notice List</h5>
                                                </div>
                                                <table class="table table-hover table-bordered display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Notice Title
                                                            </th>
                                                            <%--<th>Document
                                                            </th>--%>
                                                            <th>Document Preview
                                                            </th>
                                                            <th>Expiry Date
                                                            </th>
                                                            <th>Status
                                                            </th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("newsid") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("newsid") %>'
                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                        OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("Title")%>
                                                    </td>
                                                    <%-- <td>
                                                        <%# Eval("FILENAME") %>
                                                       <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILENAME"))%>'><%# GetFileName(Eval("FILENAME"))%></asp:HyperLink>
                                                    </td>--%>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                            Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("FILENAME") %>' data-toggle="modal" data-target="#preview"
                                                            CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("Expiry_Date","{0:dd-MMM-yyyy}") %>
                                                    </td>
                                                    <td>
                                                        <%# GetStatus(Eval("status")) %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <%--<div class="vista-grid_datapager">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvNotice" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>--%>
                                <div id="divMsg" runat="server">
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
    <%--   <script>

        function SelectAll(chk) {
            //debugger;
            var hdnVal = document.getElementById('<%= hdnCount.ClientID %>');
            for (i = 0; i < hdnVal.value; i++) {
                //alert('aa');
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
        function SelectAllStudent(chk) {
            var hdnVal = document.getElementById('<%= hdnCount.ClientID %>');
            for (i = 0; i < hdnVal.value; i++) {
                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_chkSelect');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }
        }
        function SelectAllfaculty(chk) {
            //debugger;
            var hdnVal = document.getElementById('<%= hdffacultycount.ClientID %>');
                //alert(hdnVal.value);


                for (i = 0; i < hdnVal.value; i++) {
                    // alert('aa');
                    var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvFacultyList_ctrl' + i + '_chkSelect');
                    if (lst.type == 'checkbox') {
                        if (chk.checked == true) {
                            if (!lst.disabled)
                                lst.checked = true;
                        }
                        else {
                            lst.checked = false;
                        }
                    }
                }
            }


    </script>--%>



    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="background: border-box; background-color: gray" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <img align="middle" src="../Images/warning.png" alt="" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <%--<script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js" type="text/javascript"></script>--%>

    <!-- The Modal -->
    <div class="modal fade" id="preview" role="dialog">
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Document</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body text-center">
                            <asp:Literal ID="ltEmbed" runat="server" />
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal" id="btnclose">Close</button>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        function hidefaultyshow(ddl) {
            var ddlvalue = ddl.value;
            if (ddlvalue > 0)
                document.getElementById('<%=btnShowFaculty.ClientID%>').style.display = "block";
            else
                document.getElementById('<%=btnShowFaculty.ClientID%>').style.display = "none";
        }

        $("#fuFile").change(function () {
            //submit the form here
        });


        function uploadFile() {
            var formData = new FormData();
            formData.append("file", fileupload.files[0]);
            fetch('/upload.php', {
                method: "POST",
                body: formData
            });
            alert('The file has been uploaded successfully.');
        }


        function SelectAll(chk) {

            for (var i = 0 ; i <= 4; i++) {
                if (chk.checked) {
                    document.getElementById('ctl00_ContentPlaceHolder1_chklUserType_' + i).checked = true;
                }
                else {
                    document.getElementById('ctl00_ContentPlaceHolder1_chklUserType_' + i).checked = false;
                }
            }
        }

        function SelectAllDepts(chk) {
            var count = document.getElementById('<%= hdfDepartment.ClientID%>').value;
            for (var i = 0; i < count; i++) {
                if (chk.checked) {
                    document.getElementById('ctl00_ContentPlaceHolder1_chklDepartment_' + i).checked = true;
                }
                else {
                    document.getElementById('ctl00_ContentPlaceHolder1_chklDepartment_' + i).checked = false;
                }

            }
        }

        function validate() {
            var chk = false;
            if (Page_ClientValidate('News')) {
                if (document.getElementById('<%=txtTitle.ClientID%>').value == '') {
                    alert('Please Enter Notice Title.');
                    return false;
                }
                else if (document.getElementById('<%=txtExpiryDate.ClientID%>').value == '') {
                    alert('Please Enter Expiry Date.');
                    return false;
                }
                else if (document.getElementById('<%=ftbDesc.ClientID%>').value == '') {
                    alert('Please Enter Description.');
                    return false;
                }
                else {
                    for (var i = 0; i < 5; i++) {
                        var check = document.getElementById('ctl00_ContentPlaceHolder1_chklUserType_' + i);
                        if (check.checked)
                            chk = true;
                    }

                    if (chk == false) {
                        alert('Please Select atleast one User Type.');
                        return false;
                    }
                    else
                        return true;
                }
        }
    }



    </script>
    <script type="text/javascript">
        function CheckAll(checkid) {
            var updateButtons = $('#tblData input[type=checkbox]');

            if ($(checkid).children().is(':checked')) {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", true);
                    }
                });
            }
            else {
                updateButtons.each(function () {
                    if ($(this).attr("id") != $(checkid).children().attr("id")) {
                        $(this).prop("checked", false);
                    }
                });
            }
        }
    </script>
    <script>
        function OpenPreview() {

            $('#preview').modal('hide');
        }

    </script>
    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });
    </script>

</asp:Content>



