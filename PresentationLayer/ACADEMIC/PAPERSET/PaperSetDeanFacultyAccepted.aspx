<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperSetDeanFacultyAccepted.aspx.cs" Inherits="ACADEMIC_PaperSetDeanFacultyAccepted" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFacAllot"
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
    <%--<asp:Panel ID="pnlShow" runat="server">--%>
    <asp:UpdatePanel runat="server" ID="updFacAllot">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">PAPER SETTER APPROVAL</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <%--<div class="sub-heading">
                                        <h5></h5>
                                    </div>--%>
                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-12 form-group">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                             <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select Session" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged"
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select ddlScheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDepartment" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" ToolTip="Select Semester" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnShow" runat="server" ToolTip="Click To Show" Text="Show" OnClick="btnShow_Click"
                                    ValidationGroup="Show" CssClass="btn btn-primary" />
                                <asp:Button ID="btnReport" runat="server" ToolTip="Click for report" Text="Report"
                                    OnClick="btnReport_Click" ValidationGroup="Show" CssClass="btn btn-info" />
                                <asp:Button ID="btnClear" runat="server" ToolTip="Click to Clear" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                            </div>
                            <div class="col-12 mt-4">
                                <asp:Panel ID="pnlList" runat="server">

                                    <asp:ListView ID="lvCourse" OnItemDataBound="lvCourse_ItemDataBound" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Courses</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <%--  <th>
                                                                Scheme
                                                            </th>--%>
                                                        <th>Sem
                                                        </th>
                                                        <th>Code
                                                        </th>
                                                        <th>Course
                                                        </th>
                                                        <th>
                                                            Approve
                                                            <%-- <asp:CheckBox ID="chkhead1" AutoPostBack="true" runat="server"  onclick="totAll(this)"  />Approve--%>
                                                            <%--<asp:CheckBox ID="chkhead1" AutoPostBack="true" runat="server" OnCheckedChanged="cbhead1_OnCheckedChanged" />Approve <%--Checked='<%# Eval("FLAG").ToString() == "F2" ? true : false %>'--%>
                                                        </th>
                                                        <th>Paper Setter 1
                                                        </th>
                                                        <th style="display: none;">Quantity1
                                                        </th>
                                                        <th style="display: none;">MOI1
                                                        </th>
                                                        <th>
                                                            Approve
                                                            <%--<asp:CheckBox ID="chkhead2" AutoPostBack="true" runat="server" OnCheckedChanged="cbhead2_OnCheckedChanged" />Approve <%--Checked='<%# Eval("FLAG").ToString() == "F2" ? true : false %>' --%>
                                                        </th>
                                                        <th>Paper Setter 2
                                                        </th>
                                                        <th style="display: none;">Quantity2
                                                        </th>
                                                        <th style="display: none;">MOI2
                                                        </th>
                                                        <th>
                                                            Approve
                                                            <%--<asp:CheckBox ID="chkhead3" AutoPostBack="true" runat="server" OnCheckedChanged="cbhead3_OnCheckedChanged" Checked='<%# Eval("FLAG").ToString() == "F3" ? true : false %>' />Approve--%>
                                                        </th>
                                                        <th>Paper Setter 3
                                                        </th>
                                                        <th style="display: none;">Quantity3
                                                        </th>
                                                        <th style="display: none;">MOI3
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
                                                <%-- <td>
                                                    <asp:Label ID="lblSchemeType" runat="server" Text='<%# Eval("SCHEMETYPE") %>' />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty1" Value='<%# Eval("FACULTY1") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty2" Value='<%# Eval("FACULTY2") %>' />
                                                    <asp:HiddenField runat="server" ID="hffaculty3" Value='<%# Eval("FACULTY3") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkAppFac1" runat="server" AutoPostBack="true"
                                                        Enabled='<%# Eval("DEAN_LOCK").ToString().ToLower() == "true" ? false : true %>'
                                                    Checked='<%# Eval("FACULTY1").ToString() == Eval("APPROVED").ToString()&& Eval("FACULTY1").ToString() != "0"? true : false %>'
                                                    OnCheckedChanged="chkAppFac1_OnCheckedChanged" />
                                                        &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty1" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlFaculty1_SelectedIndexChanged" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField runat="server" ID="hfDeanLock" Value='<%# Eval("DEAN_LOCK") %>' />
                                                    <asp:HiddenField runat="server" ID="hfApproved1" Value='<%# Eval("APPROVED") %>' />
                                                    <asp:HiddenField runat="server" ID="hfBosLock" Value='<%# Eval("BOS_LOCK") %>' />
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txQt1" ToolTip="Paperset quantity for paper setter 1" MaxLength="1"
                                                        runat="server" Text='<%# Eval("QT1") %>' />
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI1" ToolTip="Paperset MOI for paper setter 1" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI1") %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkAppFac2" runat="server" Enabled='<%# Eval("DEAN_LOCK").ToString().ToLower() == "true" ? false : true %>'
                                                        AutoPostBack="true" OnCheckedChanged="chkAppFac2_OnCheckedChanged" />
                                                    <%--Checked='<%# Eval("FACULTY2").ToString() == Eval("APPROVED").ToString() && Eval("FACULTY2").ToString() != "0"? true : false %>' --%>
                                                        &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty2" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlFaculty2_SelectedIndexChanged" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txQt2" ToolTip="Paperset quantity for paper setter 2" MaxLength="1"
                                                        runat="server" Text='<%# Eval("QT2") %>' />
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI2" ToolTip="Paperset MOI for paper setter 2" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI2") %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chkAppFac3" runat="server" AutoPostBack="true" OnCheckedChanged="chkAppFac3_OnCheckedChanged"
                                                        Enabled='<%# Eval("DEAN_LOCK").ToString().ToLower() == "true" ? false : true %>' />
                                                    <%--Checked='<%# Eval("FACULTY3").ToString() == Eval("APPROVED").ToString()&& Eval("FACULTY3").ToString() != "0"? true : false %>' --%>
                                                        &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFaculty3" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlFaculty3_SelectedIndexChanged" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txQt3" ToolTip="Paperset quantity for paper setter 3" MaxLength="1"
                                                        runat="server" Text='<%# Eval("QT3") %>' />
                                                </td>
                                                <td style="display: none;">
                                                    <asp:TextBox ID="txtMOI3" ToolTip="Paperset MOI for paper setter 3" MaxLength="1"
                                                        runat="server" Text='<%# Eval("MOI3") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%-- <asp:Button ID="btnSave" Visible="false" runat="server" Text="Save" OnClick="btnSave_Click"
                                        ValidationGroup="Show" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnLock" Visible="false" runat="server" Text="Lock" OnClick="btnLock_Click"
                                        ValidationGroup="Show" CssClass="btn btn-primary" />
                                    <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                    <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />--%>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSave" Visible="false" runat="server" Text="Save" OnClick="btnSave_Click"
                                            ValidationGroup="Show" CssClass="btn btn-primary" />
                                        <asp:Button ID="btnLock" Visible="false" runat="server" Text="Lock" OnClick="btnLock_Click"
                                            ValidationGroup="Show" CssClass="btn btn-primary" />
                                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" />
                                        <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                    </div>
                                </asp:Panel>
                            </div>


                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <%--</asp:Panel>--%>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function totAll(headchk) {
            //alert('ck');
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }
    </script>
</asp:Content>
