<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudAnnouncement.aspx.cs" Inherits="Itle_StudAnnouncement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }

        p {
            margin-top: 0;
            margin-bottom: 0rem;
        }

         td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VIEW ANNOUNCEMENT</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlStudnet" runat="server">
                        <div class="col-12 mb-3">
                            <asp:Panel ID="pnlText" runat="server">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-7 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>


                                <div id="divAnnounce" runat="server" class="row mt-2">
                                   <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Last Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblLastdate" runat="server"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-7 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Announcement Title :</b>
                                                <a class="sub-label">
                                                    <span runat="server" id="tdSubject"></span>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                   
                                    <div class="row">
                                    <div class="col-lg-12 col-md-12 col-12" id="div2" runat="server">
                                        <ul class="list-group list-group-unbordered" id="trDesc" runat="server">
                                            <li class="list-group-item"><b>Description :</b>
                                                <a class="sub-label float-right">
                                                    <span id="tdDescription" runat="server">
                                                        <asp:Panel ID="pnlAnnouncement" runat="server">
                                                            <span id="divAnnouncement" runat="server"></span>
                                                        </asp:Panel>
                                                    </span>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                        </div>

                                    <div class="col-10" id="trFileAttach" runat="server" visible="false">
                                        <asp:HiddenField ID="hdnFile" runat="server" />
                                        <asp:HyperLink ID="hylFile" runat="server" Target="_blank">Download Attachment</asp:HyperLink>
                                    </div>

                                    <div class="col-12 btn-footer mt-3" id="trBack" runat="server">
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" ToolTip="Click here to Go Back"
                                            OnClick="btnBack_Click" Text="Back" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <div id="DivAnnouncementList" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Announcement List</h5>
                                </div>
                                <div class="DocumentList">
                                    <asp:Panel ID="pnlAnnounce" runat="server">
                                        <asp:ListView ID="lvAnnouncement" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th class="text-center">Action</th>
                                                            <th>Subject</th>
                                                            <th>Last Date</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                   <%-- <td class="text-center">
                                                       <asp:ImageButton ID="btnEdit" runat="server"  CssClass="fa fa-eye" CommandArgument='<%# Eval("AN_NO") %>'
                                                            ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                    --%> <td class="text-center"><asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-eye"
                                                         CommandArgument='<%# Eval("AN_NO") %>' OnClick="btnEdit_Click"></asp:LinkButton>
                                  
                                                         </td>
                                                    <td>
                                                        <%# Eval("SUBJECT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("EXPDATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# GetStatus(Eval("STATUS"))%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

