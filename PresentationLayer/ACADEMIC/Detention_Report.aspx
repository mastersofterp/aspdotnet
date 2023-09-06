<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Detention_Report.aspx.cs" Inherits="ACADEMIC_Detention_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upd1"
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

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">DETENTION  REPORT</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClgname"
                                            Display="None" ErrorMessage="Please Select College & Regulation" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Select Session">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvSession2" runat="server" ControlToValidate="ddlSession" Display="None"
                                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                            AppendDataBoundItems="true" TabIndex="5" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" ToolTip="Select Semester">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester" Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>
                                            <label>Course</label>--%>
                                            <label></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" Visible="false" CssClass="form-control" data-select2-enable="true"
                                            AppendDataBoundItems="true" TabIndex="1" ToolTip="Select Course">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rvfCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnProvisionalReport" runat="server" Text="Provisional Detention Report (PDF)" Visible="false"
                                    ValidationGroup="submit" CssClass="btn btn-primary" OnClick="btnProvisionalReport_Click" ToolTip="Provisional Detention Report" TabIndex="6" />

                                <asp:Button ID="btnNillReport" runat="server" Text="Provisional Detention Report (Excel)"
                                    ValidationGroup="report" CssClass="btn btn-info" OnClick="btnNillReport_Click" ToolTip="Provisional Detention Report (Excel)" TabIndex="7" />

                                <asp:Button ID="btnReport" runat="server" Text="Final Detention Report (PDF)" Visible="false"
                                    ValidationGroup="submit" OnClick="btnReport_Click" CssClass="btn btn-info" ToolTip="Final Detention Report " TabIndex="8" />

                                <asp:Button ID="btnCoursewisereport" runat="server" Text="Final Detention Report (Excel)"
                                    ValidationGroup="report" CssClass="btn btn-info" OnClick="btnCoursewisereport_Click" ToolTip="Final Detention Report (Excel)" TabIndex="9" />

                                <asp:Button ID="btnCancelDetention" runat="server" Text="Cancel Detention Report (Excel)"
                                    ValidationGroup="report" CssClass="btn btn-info" OnClick="btnCancelDetention_Click" ToolTip="Detention Cancel Report (Excel)" TabIndex="10" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Cancel" TabIndex="10" />

                                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                <asp:ValidationSummary ID="vsReportsss" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnProvisionalReport" />--%>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnCoursewisereport" />
            <asp:PostBackTrigger ControlID="btnNillReport" />
            <asp:PostBackTrigger ControlID="btnCancelDetention" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

