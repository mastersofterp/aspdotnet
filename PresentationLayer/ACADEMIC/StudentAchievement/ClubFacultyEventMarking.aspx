<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ClubFacultyEventMarking.aspx.cs" Inherits="ACADEMIC_StudentAchievement_ClubFacultyStudentEventMarking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updclubfact"
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

    <asp:UpdatePanel ID="updclubfact" runat="server">
        <ContentTemplate>
            <div class="row" id="div1" runat="server">
                <div class="col-md-12 col-sm-12 col-12" id="div3" runat="server">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--<asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>--%>
                        CLUB ACTIVITY STUDENT EVENT MARKING
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class=" note-div">
                                <h5 class="heading">Note</h5>
                                <p><i class="fa fa-star" aria-hidden="true"></i><b><span>Record Having End Date Less than Today's Date Is Only Shown In List. </span></b></p>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Club Type</label>
                                        </div>

                                        <asp:DropDownList ID="ddlclubtype" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlclubtype_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAcademicYear" runat="server" ControlToValidate="ddlclubtype"
                                            Display="None" ErrorMessage="Please Select Club Type" SetFocusOnError="True" ValidationGroup="submit"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Title</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEventCategory" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEventCategory_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlEventCategory" runat="server" ControlToValidate="ddlEventCategory"
                                            Display="None" ErrorMessage="Please Select Event Title" SetFocusOnError="True" ValidationGroup="submit"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShowActivityDetails" runat="server" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnShowActivityDetails_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnCancelActivityDetails" runat="server" CssClass="btn btn-warning" OnClick="btnCancelActivityDetails_Click"> Cancel</asp:LinkButton>
                                <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" TabIndex="3" OnClick="btnExport_Click"
                                    Text="Download Excel Sheet" ToolTip="Download Excel Sheet" Enabled="true"> Club Student List Report</asp:LinkButton>

                                <asp:LinkButton ID="btnExportR" runat="server" CssClass="btn btn-primary" TabIndex="4" OnClick="btnExportR_Click"
                                    Text="Download Excel Sheet" ToolTip="Download Excel Sheet" Enabled="true"> Club Activity Registration Report</asp:LinkButton>
                                <asp:ValidationSummary ID="ActiveStudentDetails" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="submit" />
                            </div>

                            <div class="col-12" id="ActiveRegisteredList" runat="server">
                                <div class="sub-heading">
                                    <h5>Student Event Marking List</h5>
                                </div>
                                <asp:Panel ID="pnlStudActDetails" runat="server">
                                    <asp:ListView ID="lvcStudentActiveDetails" runat="server" OnDataBound="lvcStudentActiveDetails_DataBound">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Student Registered No
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Degree Name
                                                        </th>
                                                        <th>Branch Name
                                                        </th>
                                                        <th>Participation Type
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
                                                    <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdfIdno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    <asp:HiddenField ID="hdnRegno" runat="server" Value='<%# Eval("REGNO") %>' />
                                                    <asp:HiddenField ID="hdnCreateeventId" runat="server" Value='<%# Eval("CREATE_EVENT_ID") %>' />
                                                    <%--<asp:HiddenField ID="hdnEventMarkingId" runat="server" Value='<%# Eval("EVENT_MARKING_ID") %>' />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Eval("STUDENT_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDegreeName" runat="server" Text='<%# Eval("DEGREE_NAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEvent" runat="server" Text='<%# Eval("EVENT_ID") %>' AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                        <%--Text='<%# Eval("EVENT_ID") %>'--%>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Participated</asp:ListItem>
                                                        <asp:ListItem Value="2">Not Participated</asp:ListItem>
                                                        <asp:ListItem Value="3">Winner</asp:ListItem>
                                                        <asp:ListItem Value="4">Student Activity Cordinator</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfcevent" runat="server" ControlToValidate="ddlEvent"
                                                        Display="None" ErrorMessage="Please Select Participation Type" SetFocusOnError="True" ValidationGroup="submitfac"
                                                        InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="col-12 btn-footer" id="btndiv">

                                        <asp:UpdatePanel ID="updexcel" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="btnSaveStudentActivitydetails" runat="server" CssClass="btn btn-primary" ValidationGroup="submitfac" OnClick="btnSaveStudentActivitydetails_Click">Save</asp:LinkButton>
                                                <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-info" OnClick="btnReport_Click">Excel Report</asp:LinkButton>

                                                <asp:LinkButton ID="btnCancelStudentActivitydetails" runat="server" CssClass="btn btn-warning" OnClick="btnCancelStudentActivitydetails_Click"> Cancel</asp:LinkButton>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnReport" />
                                                <asp:PostBackTrigger ControlID="btnExport" />
                                                <asp:PostBackTrigger ControlID="btnExportR" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="submitfac" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
