<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudViewSyllabus.aspx.cs" Inherits="ACADEMIC_StudViewSyllabus" %>

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
                <div class="col-md-12 col-sm-12 col-12" id="div2" runat="server">
                    <div class="box box-primary">
                        <div id="div" runat="server"></div>
                        <div class="box-header with-border" id="div1" runat="server">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                <div class="row">
                                    <div class="col-12" id="DivSyllabusList" runat="server" visible="true">
                                        <asp:Panel ID="pnlSyllabusList" runat="server">
                                            <asp:ListView ID="lvSyllabus" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Fil Name</th>
                                                                <th><%--Scheme Name--%>
                                                                    <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th><%--Semester Name--%>
                                                                    <asp:Label ID="lblDYtxtSemName" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>Created Date</th>
                                                                <th>Attachment</th>
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
                                                            <%# Eval("FILE_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SCHEMENAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SEMESTERNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Create_Date", "{0:dd-MMM-yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDownload" runat="server" Text="Download"
                                                                ToolTip='<%# Eval("ATTACHMENT")%>' CommandArgument='<%# Eval("SEMESTERNO")%>' OnClick="lnkDownload_Click">
                                                            </asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <%--<div class="col-md-12">
            <asp:Panel ID="pnlViewSyllabus" runat="server">
                <div class="col-md-12">
                    <asp:Panel ID="pnlStudView" runat="server">
                        <div class="panel panel-info">
                            <div class="panel panel-heading">View Syllabus</div>
                            <div class="panel panel-body">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>Session&nbsp;&nbsp;:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label>Course&nbsp;&nbsp;:</label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="showSyllabus" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label></label>
                                        </div>
                                        <div class="col-md-6" id="divDesc" runat="server">
                                            <div class="titlebar">Complete Syllabus Model Of Selected Course</div>
                                            <asp:TreeView ID="tvSyllabus" runat="server">
                                            </asp:TreeView>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <label></label>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Button ID="btnViewSyllabus" runat="server" Text="View Syllabus" CssClass="btn btn-primary"
                                                OnClick="btnViewSyllabus_Click" ToolTip="Click here to View Syllabus" />
                                            <asp:Button ID="btnViewSyllabus" runat="server" Text="View Syllabus" BackColor="#9ba7c6"
                                        OnClick="btnViewSyllabus_Click" BorderColor="#000066" ToolTip="Click here to View Syllabus" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

