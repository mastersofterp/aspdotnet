<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Course_Equivalance.aspx.cs" Inherits="ACADEMIC_Course_Equivalance"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COURSE EQUIVALANCE</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="submit" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree"
                                            ControlToValidate="ddlDegree" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree"
                                            ControlToValidate="ddlDegree" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please Select Scheme"
                                            ControlToValidate="ddlScheme" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Scheme"
                                            ControlToValidate="ddlScheme" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ErrorMessage="Please Select Semester"
                                            ControlToValidate="ddlSemester" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>CSVTU Course</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse_auto" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCourse_auto_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse_auto" runat="server" ControlToValidate="ddlCourse_auto"
                                            Display="None" ErrorMessage="Please Select CSVTU Course" InitialValue="0"
                                            ValidationGroup="submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:CheckBox ID="chkNonequi" runat="server" Text="Non Equivalence"
                                            onclick="toggle_panel(this);" AutoPostBack="True"
                                            OnCheckedChanged="chkNonequi_CheckedChanged" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlRtmCourse" runat="server" Visible="false">
                                    <asp:ListView ID="lvNITCourse" runat="server" Enabled="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>NIT Courses</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th>Semester
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
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("COURSENO" )%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourse" Text='<%# Eval("COURSE_NAME")%>' ToolTip='<%# Eval("COURSENO" )%>'
                                                        runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO" )%>'
                                                        runat="server"></asp:Label>
                                                    <asp:Label ID="lblScheme" Text='<%# Eval("SCHEMENO") %>' runat="server" Visible="false" ToolTip='<%# Eval("CCODE" )%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-12  d-none">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Exempted</label>
                                        </div>
                                        <asp:RadioButtonList ID="rblExemted" runat="server" AutoPostBack="True"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                        OnClick="btnSubmit_Click" />
                                   <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info"
                                        ValidationGroup="report" OnClick="btnReport_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                            OnClick="btnCancel_Click" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-warning" OnClick="btnDelete_Click" />
                                    
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="report" />

                                    </div>

                            <div class="col-12">
                                <asp:ListView ID="lvEquivalanceCourse" runat="server" Enabled="true">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>EQUIVALANCE COURSE LIST</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Select
                                                    </th>
                                                    <th>New Course
                                                    </th>
                                                    <th>New Scheme
                                                    </th>
                                                    <th>Old Course
                                                    </th>
                                                    <th>Old Scheme
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
                                                <asp:CheckBox ID="chkCourse" runat="server" ToolTip='<%# Eval("OLD_SCHEMENO" )%>' />
                                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("OLD_COURSENO" )%>' Visible="false" />

                                            </td>
                                            <td>
                                                <%# Eval("NEW_CCODE")%> - <%# Eval("NEW_COURSENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("NEW_SCHEME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OLD_CCODE")%> - <%# Eval("OLD_COURSENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("OLD_SCHEME")%>                                                        
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
