<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentFeedBackOnTeacher.aspx.cs" Inherits="ACADEMIC_StudentFeedBackReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeed"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader"></div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT FEEDBACK ON TEACHER</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <asp:UpdatePanel ID="updFeed" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-md-3">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control"
                                    ToolTip="Please Select Session" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label><span style="color: red;">*</span> College</label>
                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                    TabIndex="2" ToolTip="Please Select College" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label><span style="color: red;">*</span> Department</label>
                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                    TabIndex="3" ToolTip="Please Select Department" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">

                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                    Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <label><span style="color: red;">*</span> Faculty</label>
                                <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                    TabIndex="5" ToolTip="Please Select Faculty">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvfac" runat="server" ControlToValidate="ddlFaculty"
                                    Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Report"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnFeedbackReport" runat="server" Text="Feedback Report"
                                    ValidationGroup="Report" OnClick="btnFeedbackReport_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancelReport" runat="server" Text="Cancel"
                                    OnClick="btnCancelReport_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                            </p>
                        </div>
                        <div id="divMsg" runat="server">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
