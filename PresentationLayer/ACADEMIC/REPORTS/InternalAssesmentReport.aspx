<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InternalAssesmentReport.aspx.cs"
    Inherits="ACADEMIC_REPORTS_InternalAssesmentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpaper"
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
    <asp:UpdatePanel ID="updpaper" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Internal Assessment Mark Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged"
                                            ValidationGroup="offered"  data-select2-enable="true" TabIndex="1">
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
                                        <asp:DropDownList ID="ddlSession" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" 
                                            AppendDataBoundItems="True" TabIndex="1" ToolTip="Please Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="Show"
                                            InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" TabIndex="1" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Show" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <span style="color: red;">*</span>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" TabIndex="1" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" ToolTip="Please Select Course" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" ValidationGroup="Show" InitialValue="0"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            
                           
                            <div class="col-12 mt-3 btn-footer">
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" TabIndex="1" Text="Internal Assessment Report" ToolTip="Click to save" OnClick="btnReport_Click" ValidationGroup="Show" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" TabIndex="1" Text="Cancel" ToolTip="Click to cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>


