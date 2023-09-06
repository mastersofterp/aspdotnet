<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudForum.aspx.cs" Inherits="Itle_StudForum" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>

    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DISCUSSION FORUM</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlForum" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlSession" runat="server">
                                <div class="row">
                                    <%-- <div class="col-lg-12 col-md-12 col-12">
                                        <div class="sub-heading">
                                            <h5>Session & Course</h5>
                                        </div>
                                    </div>--%>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-7 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>

                        <div class="col-12 mt-3">
                            <asp:Panel runat="server" ID="tdForumList">
                                <div class="sub-heading">
                                    <h5>Available Forums List</h5>
                                </div>

                                <asp:Panel ID="pnlVwForum" runat="server">
                                    <asp:ListView ID="lstVwForum" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Forum</th>
                                                        <th>Messages</th>
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
                                                    <asp:LinkButton ID="btnlnkSelect" runat="server" Text='<%# Eval("FORUM")%>'
                                                        CommandName='<%# Eval("FORUM_NO") %>'
                                                        CommandArgument='<%# Eval("FORUM_NO") %>' ToolTip="Click here to Open Messages"
                                                        OnClick="btnlnkSelect_Click">
                                                    </asp:LinkButton>
                                                    <%# Eval("DESCRIPTION")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MESSAGE_COUNT")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="tdMessages" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <div class="sub-heading">
                                            <h5>Messages</h5>
                                        </div>
                                    </div>
                                    <div class="col-12 col-md-6 d-none">
                                        <div class="float-md-right">
                                            <asp:LinkButton ID="btnlnkGoBack" runat="server" Text="Back" CssClass="btn btn-warning" ToolTip="Go Back" OnClick="btnlnkGoBack_Click">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <asp:ImageButton ID="lnkPostMessage2" runat="server" ImageUrl="~/Images/Compose.JPG"
                                            Width="110px" Height="50px" AlternateText="Compose Message"
                                            OnClick="lnkPostMessage_Click" ToolTip="Click here to Compose Message"></asp:ImageButton>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <asp:LinkButton ID="lnkBackForum2" runat="server" OnClick="lnkBackForum_Click" CssClass="btn btn-primary mt-3"
                                            ToolTip="Click here to Return Back">FORUM</asp:LinkButton>
                                    </div>
                                </div>

                                <%-- </div>--%>
                                <div class="col-12 table-responsive">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlVwMessageList" runat="server">
                                                <asp:ListView ID="lstVwMessage" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Date</th>
                                                                    <th>Image</th>
                                                                    <th>Name</th>
                                                                    <th>Message</th>
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
                                                                <%# Eval("POSTDATE", "{0:dd-MMM-yyyy}")%>
                                                            </td>
                                                            <td>
                                                                <asp:Image ID="imgPhoto" runat="server" Width="50px" Height="50px"
                                                                    ImageUrl='<%# Eval("imgUrl")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDNAME")%>                                                                  
                                                            </td>
                                                            <td>
                                                                <%# Eval("MESSAGE")%>                                                        
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <p class="text-center text-bold">

                                                            <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                        </p>
                                                    </EmptyDataTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lstVwMessage" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <asp:ImageButton ID="lnkPostMessage1" runat="server" OnClick="lnkPostMessage_Click" Visible="false"
                                            ImageUrl="~/images/Compose.JPG" Width="110px" Height="50px" AlternateText="Compose Message"></asp:ImageButton>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <asp:LinkButton ID="lnkBackForum1" runat="server" CssClass="btn btn-primary mt-3" Visible="false" OnClick="lnkBackForum_Click">FORUM</asp:LinkButton>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="tdAddPost" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Create New Message</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Message</label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtMessage" runat="server" Height="200"
                                            BasePath="~/ckeditor" ToolbarStartupExpanded="false">		                        
                                        </CKEditor:CKEditorControl>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="updSendMessage" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btmSubmit" runat="server" Text="Send" OnClick="btmSubmit_Click"
                                                CssClass="btn btn-primary" ToolTip="Click here to Send message" />
                                            <%-- <asp:ImageButton ID="btmSubmit" runat="server" Text="Submit" OnClick="btmSubmit_Click"
                                                                            ImageAlign="AbsBottom" ImageUrl="~/images/send.jpeg" CssClass="btn btn-primary"
                                                                             AlternateText="Send Message" ToolTip="Click here to Send message"></asp:ImageButton>--%>
                                            <%--Width="60px" Height="25px"--%>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btmSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" Style="text-decoration: blink;"></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
