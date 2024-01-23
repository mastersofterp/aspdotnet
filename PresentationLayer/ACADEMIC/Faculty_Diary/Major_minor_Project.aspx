<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Major_minor_Project.aspx.cs" Inherits="ACADEMIC_Major_minor_Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server"
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

    <%--  
    <asp:UpdatePanel ID="UpdProject" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Major / Minor Project</h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Project Master</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Assign</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Edit Assign Project Title</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">

                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdProjectMs"
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

                                <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                                <asp:UpdatePanel ID="UpdProjectMs" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYProject" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtProject" runat="server" MaxLength="50" TextMode="MultiLine" ViewStateMode="Enabled" AppendDataBoundItems="True"
                                                            CssClass="form-control" ToolTip="Name Of Manjor/Minor Project" AutoPostBack="true" TabIndex="1" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProject"
                                                            Display="None" ErrorMessage="Please Enter Name Of Major/Minor Project" SetFocusOnError="True"
                                                            ValidationGroup="submit4" />
                                                    </div>
                                                    <div class="form-group col-6">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActive" name="switch" />
                                                            <%-- <input type="checkbox" id="Checkbox1" name="switch" checked />--%>
                                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmit_Click"
                                                    CssClass="btn btn-primary" TabIndex="2" OnClientClick="return validate2();" ValidationGroup="submit4" />
                                                <asp:Button ID="btnCancell" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" TabIndex="3" OnClick="btnCancell_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit4"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>


                                            <div class="col-12">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:ListView ID="lvProject" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>

                                                                        <th style="width: 5%; text-align: center;">Edit
                                                                        </th>
                                                                        <th>Project Title
                                                                        </th>
                                                                        <th>Activity Status
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
                                                                    <asp:ImageButton ID="btn_editt" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        TabIndex="14" OnClick="btn_editt_Click" />
                                                                </td>

                                                                <td><%# Eval("PROJECT_TITLE")%></td>
                                                                <td>
                                                                    <%--  <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("IS_ACTIVE")%>'></asp:Label>--%>
                                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                </td>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCancell" />

                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>



                            <div class="tab-pane fade" id="tab_2">

                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updStudent"
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

                                <asp:UpdatePanel ID="updStudent" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Session" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSession1" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit1">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlCollege" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select College" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                        Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select degree" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDegree"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Session</label>--%>
                                                        <asp:Label ID="lblDYtxtBranchName" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Branch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divProject" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Project </label>
                                                        <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProject" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select project" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProject"
                                                        Display="None" ErrorMessage="Please Select Project" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShow" runat="server" Text="Show" ToolTip="Submit"
                                                    CssClass="btn btn-primary" TabIndex="6" ValidationGroup="submit1" CausesValidation="true" OnClick="btnSubmitt_Click" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" TabIndex="7" OnClick="btnCancel_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit1"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>


                                            <div class="col-12">
                                                <asp:Panel ID="pnlSession" runat="server">
                                                    <asp:ListView ID="lvStudent" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Check</th>
                                                                            <%--<center><%# Container.ItemIndex + 1 %></center>--%>
                                                                            <%--<th style="width: 5%; text-align: center;">Edit </th>--%>
                                                                            <th>Student Name</th>
                                                                            <th>Degree Name</th>
                                                                            <th>Branch Name</th>
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
                                                                    <asp:CheckBox ID="ChkBox" runat="server" Style="text-align: center" />

                                                                </td>
                                                                <%--  <td>
                                                                            <asp:ImageButton ID="btn_editt" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                CommandArgument='<%# Eval("idno")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                TabIndex="14" OnClick="btn_editt_Click" />
                                                                        </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                                    <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSIONNO")%>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblIDno" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME")%>' ToolTip='<%# Eval("DEGREENO")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("LONGNAME")%>' ToolTip='<%# Eval("BRANCHNO")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnShow" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>


                            <div class="tab-pane fade" id="tab_3">

                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updEditAssignStudent"
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

                                <asp:UpdatePanel ID="updEditAssignStudent" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlsessionNew" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Session" AutoPostBack="true" OnSelectedIndexChanged="ddlsessionNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlsessionNew"
                                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit2">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlCollegeNew" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCollegeNew" runat="server" AppendDataBoundItems="True" TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select College" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollegeNew"
                                                        Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYddlDegreename" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegreeNew" runat="server" AppendDataBoundItems="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select degree" AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDegreeNew"
                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Session</label>--%>
                                                        <asp:Label ID="lblDYBranchNew" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranchNew" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select Branch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranchNew_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlBranchNew"
                                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divNewProject" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Project </label>
                                                        <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProejctNew" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                                        ToolTip="Please Select project" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlProejctNew"
                                                        Display="None" ErrorMessage="Please Select Project" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>


                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmitNew" runat="server" Text="Show" ToolTip="Submit"
                                                    CssClass="btn btn-primary" TabIndex="6" ValidationGroup="submit2" CausesValidation="true" OnClick="btnSubmitNew_Click" />
                                                 <asp:Button ID="btnReport" runat="server" Text="Report" ToolTip="Report"
                                                    CssClass="btn btn-primary" TabIndex="7"  OnClick="btnReport_Click"  />
                                                <asp:Button ID="btnCancelNew" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancelNew_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="submit2"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>


                                            <div class="col-12">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:ListView ID="lvStudentAssign" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th style="width: 5%; text-align: center;">Edit </th>
                                                                            <th>Student Name</th>
                                                                            <th>Project Name</th>
                                                                            <th>Degree Name</th>
                                                                            <th>Branch Name</th>
                                                                            <th>Session Name</th>
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
                                                                    <asp:ImageButton ID="btn_EditAssignStudent" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("idno")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        TabIndex="14" OnClick="btn_EditAssignStudent_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                                    <%-- <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSIONNO")%>' Visible="false"></asp:Label>
                                                                             <asp:Label ID="lblIDno" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>--%>

                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblProject" runat="server" Text='<%# Eval("PROJECT_NAME")%>'></asp:Label>
                                                             
                                                                <td>

                                                                    <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREENAME")%>'></asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("LONGNAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSession" runat="server" Text='<%# Eval("SESSION_NAME")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSubmitNew" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancelNew" />
                                            <asp:AsyncPostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>








                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="TabName" runat="server" />
    <%--     </ContentTemplate>
      
    </asp:UpdatePanel>--%>
    <script>

        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function validate2() {

            $('#hfdActive').val($('#rdActive').prop('checked'));

            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //prm.add_endRequest(function () {
            //    $(function () {
            //        $('#btnSubmit').click(function () {
            //            validate2();
            //        });
            //    });
            //});
        }

    </script>

    <script>

        function TabShow(tabName) {
            //alert('hii' + tabName);
            //var tabName = "tab_2";
            $('#Tabs button[href="#' + tabName + '"]').tab('show');
            $("#Tabs button").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
    <script>

        $("#ctl00_ContentPlaceHolder1_Panel2").click(function () {
            var tab2 = $("[id*=TabName]").val("tab_2");//document.getElementById('<%= TabName.ClientID%>').value;
            //$('#Tabs a[href="' + tab1 + '"]').tab('show');
            //alert(tab2.val());

        });
        $("#ctl00_ContentPlaceHolder1_PanelCategory").click(function () {
            var tab3 = $("[id*=TabName]").val("tab_3");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab3.val());
            //$('#Tabs a[href="' + tab2 + '"]').tab('show');

        });
        $("#ctl00_ContentPlaceHolder1_PanelActivity").click(function () {
            var tab4 = $("[id*=TabName]").val("tab_4");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab4.val());
            //$('#Tabs a[href="' + tab3 + '"]').tab('show');

        });

        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>

    <script>
        function validateCheckBox() {
            var checkBox = document.getElementById('ChkBox');
            return checkBox.checked;
        }

        document.addEventListener('DOMContentLoaded', function () {
            var submitButton = document.getElementById('btnShow');
            submitButton.addEventListener('click', function (event) {
                if (!validateCheckBox()) {
                    alert('Please check the checkbox.');
                    event.preventDefault(); // Prevent form submission
                }
            });
        });
    </script>
</asp:Content>

