<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="MailPage.aspx.cs" Inherits="Itle_MailPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <%-- <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>

    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }
    </style>


    <style type="text/css">
        .ajax__tab_outer {
            margin-right: 20px !important;
        }

        .ajax__tab_body {
            border: grover !important;
        }

        .ajax__tab_header {
            background: none !important;
        }

        .ajax__tab_tab {
            direction: ltr;
            width: 150px;
        }

        .tabCompose {
            background-color: #D74937;
            /*border: solid 1px black;*/
            font-size: small;
            cursor: pointer;
            padding: 5px 5px 5px 5px;
        }

        .tab {
            background-color: #F2F2F2;
            /*border: solid 1px black;*/
            cursor: hand;
            padding: 5px 5px 5px 5px;
        }

        .active_tab {
            /*background-color: #FFFFE0;*/
            /*border: solid 1px black;*/
            /*border-bottom-style: none;*/
            cursor: hand;
            font-weight: bold;
            padding: 2px 2px 2px 2px;
        }

        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }

        a.mail_pg:link {
            color: #556B2F;
            background-color: transparent;
        }

        a.mail_pg:visited {
            color: #556B2F;
            background-color: transparent;
        }

        a.mail_pg:hover {
            color: #FF0000;
            background-color: transparent;
        }

        a.mail_pg:active {
            color: #FF0000;
            background-color: transparent;
        }

        .current_pg {
            color: Maroon;
            font-weight: bolder;
            font-size: large;
            background-color: transparent;
        }
    </style>

    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }
    </style>
    <style>
        .new {
            box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;
            border: 1px solid #c7c7c7;
            border-radius: 8px;
        }

        .new1 {
            background: #dddbdb;
            padding: 4px 3px 3px 8px;
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;
        }

        .input-group .input-group-addon {
            padding: 6px 12px;
        }
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
    <script type="text/javascript">

        function SearchMyMail(source, eventArgs) {

            document.getElementById('ctl00_ContentPlaceHolder1_txtSearch').value = Name[0];

        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Mail Message</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlMail" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlSearch" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Messages</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                                                TabIndex="1" ToolTip="Enter Search Messages"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server"
                                                Enabled="True" ServicePath="~/Autocomplete.asmx" TargetControlID="txtSearch"
                                                CompletionSetCount="6" ServiceMethod="SearchMail" MinimumPrefixLength="1" CompletionInterval="0"
                                                CompletionListCssClass="autocomplete_completionListElement" OnClientItemSelected="SearchMyMail"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionListItemCssClass="autocomplete_listItem">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <div class="input-group-addon">
                                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/search-svg.png" Text="Search"
                                                    AlternateText="Search" ImageAlign="AbsMiddle" AutoPostBack="true"
                                                    TabIndex="2" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <a href="CreateEmailGroup.aspx" class="mail_pg" onclick="javascript:OpenGroupWindow(this.href); return false;"
                                            title="Please click to create Group of students">
                                            <asp:Label ID="lblGroupLink" runat="server" Visible="false" Font-Bold="true" ForeColor="Blue"
                                                Text="Create New Student Group"></asp:Label>
                                        </a>
                                    </div>
                                    <div class="col-12">
                                        <div class="text-center">
                                            <asp:Label runat="server" ID="lblError" Font-Bold="true" ForeColor="Green"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-12 mt-4">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Mail Message</h5>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-3 col-12">
                                            <span id="spnCompose" runat="server" class="tabCompose">&nbsp;&nbsp;<asp:LinkButton ID="btnComposeMsg"
                                                runat="server" OnClick="btnComposeMsg_Click" Text="Compose Message"
                                                Font-Underline="False" /></span> &nbsp;&nbsp;
                                                    <br />
                                            <hr />

                                            <span id="spnInbox" runat="server" class="active_tab">&nbsp;&nbsp;<asp:LinkButton
                                                ID="btnShowInbox" runat="server" OnClick="btnShowInbox_Click" Text="Inbox"
                                                Font-Underline="False" /></span>&nbsp;&nbsp;
                                                    <br />
                                            <hr />

                                            <span id="spnSentItems" runat="server">&nbsp;&nbsp;<asp:LinkButton ID="btnShowOutbox"
                                                runat="server" OnClick="btnShowOutbox_Click" Text="Outbox"
                                                Font-Underline="False" CssClass="mail_pg" /></span>&nbsp;&nbsp;
                                                    <br />
                                            <hr />

                                            <span id="spTrash" runat="server">&nbsp;&nbsp;<asp:LinkButton ID="btnTrash"
                                                runat="server" Text="Trash"
                                                Font-Underline="False" OnClick="btnTrash_Click" CssClass="mail_pg" /></span>&nbsp;&nbsp;
                                                    <br />
                                            <hr />
                                        </div>

                                        <div class="col-lg-9 col-md-9 col-12">
                                            <div id="divInMails" runat="server" visible="true" class="col-12 btn-footer mt-4">
                                                <div class="col-12 newid">
                                                    <div class="row">
                                                        <div class="col-md-8 mt-4">
                                                            <span id="Span2" runat="server">
                                                                <asp:LinkButton ID="btnMarkAsRead" runat="server" Text="Mark as Read" OnClick="btnMarkAsRead_Click"
                                                                    CssClass="btn btn-info" />
                                                            </span>
                                                            <span id="Span3" runat="server">
                                                                <asp:LinkButton ID="btnMarkUnread" runat="server" Text="Mark as Unread" OnClick="btnMarkAsUnread_Click"
                                                                    CssClass="btn btn-primary" />
                                                            </span>
                                                            <span id="Span1" runat="server">
                                                                <asp:LinkButton ID="btnDeleteInboxMail" runat="server" Text="Delete"
                                                                    OnClick="btnDeleteInboxMail_Click" CssClass=" btn btn-warning" />
                                                            </span>
                                                        </div>
                                                        <div class="col-4">
                                                            <div class="text-center">
                                                                <span id="Span4" runat="server">
                                                                    <asp:DataPager ID="dpInMails" runat="server" PageSize="20" PagedControlID="lvInMails"
                                                                        OnPreRender="dpInMails_OnPreRender">
                                                                        <Fields>
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowNextPageButton="false"
                                                                                ShowPreviousPageButton="true" FirstPageText="<<" ButtonCssClass="mail_pg" />
                                                                            <asp:NumericPagerField ButtonCount="10" CurrentPageLabelCssClass="current_pg" NextPreviousButtonCssClass="mail_pg" />
                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="true" ShowNextPageButton="true"
                                                                                ShowPreviousPageButton="false" LastPageText=">>" ButtonCssClass="mail_pg" />
                                                                        </Fields>
                                                                    </asp:DataPager>

                                                                </span>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 table-responsive">
                                                            <asp:Panel ID="pnlInMails" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvInMails" runat="server" AutoGenerateColumns="true">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id=" tblInboxMailList">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>
                                                                                        <input id="chkSelectAll" type="checkbox" onclick="javascript: SelectAllInMail(this);" />
                                                                                    </th>
                                                                                    <th></th>
                                                                                    <th>Sender
                                                                                    </th>
                                                                                    <th>Subject
                                                                                    </th>
                                                                                    <th>Attachment
                                                                                    </th>
                                                                                    <th>Date
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
                                                                                <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                                                <asp:HiddenField ID="hidMailId" runat="server" Value='<%# Eval("MAIL_ID")%>' />
                                                                            </td>
                                                                            <td>
                                                                                <img alt="Unread" src="../IMAGES/email.gif"
                                                                                    class='<%# (Eval("STATUS").ToString() == "U")? "show_img": "hide_img" %>' />
                                                                                <img alt="Read" src="../IMAGES/mail.gif"
                                                                                    class='<%# (Eval("STATUS").ToString() == "R")? "show_img": "hide_img" %>' />
                                                                            </td>
                                                                            <td>
                                                                                <a id="lnkSender" runat="server" class="mail_pg"
                                                                                    href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=in&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                                    <%# Eval("SENDER")%></a>
                                                                            </td>
                                                                            <td>
                                                                                <a id="lnkSubject" runat="server" class="mail_pg"
                                                                                    href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=in&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                                    <%# Eval("SUBJECT")%></a>
                                                                            </td>
                                                                            <td>
                                                                                <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                                    class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                            </td>
                                                                            <td>
                                                                                <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <p class="text-center text-bold">
                                                                            You don't have any message in inbox.<br />
                                                                        </p>
                                                                    </EmptyDataTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divOutMails" runat="server" visible="false" style="padding: 8px 4px 4px 4px;">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <span id="Span5" runat="server">
                                                                <asp:LinkButton ID="btnDeleteOnboxMail"
                                                                    runat="server" Text="Delete" CssClass="btn btn-warning"
                                                                    OnClick="btnDeleteOnboxMail_Click" />
                                                            </span>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="text-right">
                                                                <asp:DataPager ID="dpOutMails" runat="server" PageSize="20" PagedControlID="lvOutMails"
                                                                    OnPreRender="dpOutMails_OnPreRender">
                                                                    <Fields>
                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowNextPageButton="false"
                                                                            ShowPreviousPageButton="true" FirstPageText="<<" ButtonCssClass="mail_pg" />
                                                                        <asp:NumericPagerField ButtonCount="10" CurrentPageLabelCssClass="current_pg" NextPreviousButtonCssClass="mail_pg" />
                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="true" ShowNextPageButton="true"
                                                                            ShowPreviousPageButton="false" LastPageText=">>" ButtonCssClass="mail_pg" />
                                                                    </Fields>
                                                                </asp:DataPager>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 table-responsive mt-3">
                                                    <asp:Panel ID="pnlOutMails" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvOutMails" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblOutboxMailList">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <input id="chkSelectAll" type="checkbox" onclick="javascript: SelectAllOutMail(this);" />
                                                                            </th>
                                                                            <th>Sent To
                                                                            </th>
                                                                            <th>Subject
                                                                            </th>
                                                                            <th>Attachment
                                                                            </th>
                                                                            <th>Date
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
                                                                        <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                                        <asp:HiddenField ID="hidMailId" runat="server" Value='<%# Eval("MAIL_ID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSender" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("RECEIVER")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSubject" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SUBJECT")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                            class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    You don't have any message in outbox.<br />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div id="dvTrash" runat="server" visible="false" style="padding: 8px 4px 4px 4px;">
                                                <div class="col-md-12">
                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <span id="Span6" runat="server">
                                                                <asp:LinkButton ID="btnRestore" runat="server"
                                                                    Text="Restore" CssClass="btn btn-warning"
                                                                    OnClick="btnRestore_Click" />
                                                            </span>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="text-right">
                                                                <asp:DataPager ID="dpTrash" runat="server" PageSize="20" PagedControlID="lvTrash"
                                                                    OnPreRender="dpTrash_OnPreRender">
                                                                    <Fields>
                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowNextPageButton="false"
                                                                            ShowPreviousPageButton="true" FirstPageText="<<" ButtonCssClass="mail_pg" />
                                                                        <asp:NumericPagerField ButtonCount="10" CurrentPageLabelCssClass="current_pg" NextPreviousButtonCssClass="mail_pg" />
                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="true" ShowNextPageButton="true"
                                                                            ShowPreviousPageButton="false" LastPageText=">>" ButtonCssClass="mail_pg" />
                                                                    </Fields>
                                                                </asp:DataPager>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 table-responsive">
                                                    <asp:Panel ID="pnlTrash" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvTrash" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id=" tblTrash">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <input id="chkSelectAll" type="checkbox" onclick="javascript: SelectAllDeletedMail(this);" />
                                                                            </th>
                                                                            <th>Sender
                                                                            </th>
                                                                            <th>Subject
                                                                            </th>
                                                                            <th>Attachment
                                                                            </th>
                                                                            <th>Date
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
                                                                        <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                                        <asp:HiddenField ID="hidMailId" runat="server" Value='<%# Eval("MAIL_ID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSender" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SENDER")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSubject" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SUBJECT")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                            class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    You don't have any message in outbox.<br />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div id="divSearchMail" runat="server" visible="false" style="padding: 8px 4px 4px 4px;">

                                                <div class="col-12">
                                                    <span id="Span7" runat="server">
                                                        <asp:LinkButton ID="btnSearchRestore" runat="server"
                                                            Text="Restore" CssClass="btn btn-warning" OnClick="btnSearchRestore_Click" />
                                                    </span>
                                                </div>

                                                <div class="col-12 table-responsive">
                                                    <asp:Panel ID="pnlSearchMail" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvSearchMail" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblTrash">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <input id="chkSelectAll" type="checkbox" onclick="javascript: SelectAllDeletedMail(this);" />
                                                                            </th>
                                                                            <th>Sender
                                                                            </th>
                                                                            <th>Subject
                                                                            </th>
                                                                            <th>&nbsp;
                                                                            </th>
                                                                            <th></th>
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
                                                                        <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                                        <asp:HiddenField ID="hidMailId" runat="server" Value='<%# Eval("MAIL_ID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSender" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SENDER")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSubject" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SUBJECT")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                            class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    <br />
                                                                    You don't have any message in outbox.<br />
                                                                    <br />
                                                                </p>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <div id="divSearchOutBoxMail" runat="server" visible="false" style="padding: 8px 4px 4px 4px;">
                                                <div class="col-12 table-responsive">
                                                    <asp:Panel ID="pnlSearchOutBoxMail" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvSearchOutBoxMail" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblTrash">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>
                                                                                <input id="chkSelectAll" type="checkbox" onclick="javascript: SelectAllDeletedMail(this);" />
                                                                            </th>
                                                                            <th>Sender
                                                                            </th>
                                                                            <th>Subject
                                                                            </th>
                                                                            <th>Attachment
                                                                            </th>
                                                                            <th>Date
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
                                                                        <asp:CheckBox ID="chkSelectMail" runat="server" />
                                                                        <asp:HiddenField ID="hidMailId" runat="server" Value='<%# Eval("MAIL_ID")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSender" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("RECEIVER")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkSubject" runat="server" class="mail_pg"
                                                                            href='<%# (Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString()) + "&type=out&mid=" + Eval("MAIL_ID").ToString() %>'>
                                                                            <%# Eval("SUBJECT")%></a>
                                                                    </td>
                                                                    <td>
                                                                        <img alt="Attachment" src="../IMAGES/attachment.png"
                                                                            class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <p class="text-center text-bold">
                                                                    You don't have any message in outbox.<br />

                                                                </p>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div class="col-12 new">
                                                <div id="divCompose" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="col-12 mb-4 pt-2 new1">
                                                            <div class="sub-heading">
                                                                <h5>New Message</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <a href="AddContacts.aspx" class="mail_pg" onclick="javascript:openwindow(this.href); return false;"
                                                                    title="Please click to get Receiver name"><b>Select Recipient:
                                                                            
                                                                    (Click here to get recipient)</b></a>
                                                            </div>
                                                            <asp:TextBox ID="txtMailTo" runat="server" TextMode="MultiLine" CssClass="form-control"
                                                                ReadOnly="true" />
                                                            <asp:HiddenField ID="hdntxtMailto" runat="server" />
                                                            <asp:HiddenField ID="hidMailTo" runat="server" />
                                                            <asp:RequiredFieldValidator ID="valCode" runat="server" ControlToValidate="txtMailTo"
                                                                ValidationGroup="SendMail" Display="None" ErrorMessage="Please enter mail receiver" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Subject</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtSubject" ErrorMessage="Subject can not be blank"
                                                                ValidationGroup="SendMail" Display="None" />
                                                        </div>

                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                            </div>
                                                            <%# Eval("SUBJECT")%>
                                                            <CKEditor:CKEditorControl ID="ftbMailBody" runat="server" Height="150"
                                                                BasePath="~/ckeditor" ToolbarStartupExpanded="false">
                                                            </CKEditor:CKEditorControl>
                                                            <%--<FTB:FreeTextBox ID="ftbMailBody" runat="server" Height="300">
                                                                                    </FTB:FreeTextBox>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                            <div class="label-dynamic">
                                                                <label>Attachment</label>
                                                            </div>
                                                            <asp:FileUpload ID="fileUploader" runat="server" ToolTip="Click here to Select File" />

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" CssClass="btn btn-primary mt-1"
                                                                OnClick="btnAttachFile_Click" ToolTip="Click here to Attach File" />&nbsp;&nbsp;
                                                                (Max.Size
                                                            <asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)
                                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List"
                                                                    ValidationGroup="SendMail"
                                                                    ShowMessageBox="true" ShowSummary="false" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12" id="divAttch" runat="server" style="display: none">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-9">
                                                            <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvCompAttach" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <table class="table table-bordered table-hover">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Action
                                                                                        </th>
                                                                                        <th>
                                                                                        Attachment                                                                                                               
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
                                                                                <asp:LinkButton ID="lnkRemoveAttach" runat="server"
                                                                                    CommandArgument='<%# Eval("ATTACH_ID")%>'
                                                                                    OnClick="lnkRemoveAttach_Click" CssClass="mail_pg">
                                                                                                            Remove</asp:LinkButton>
                                                                            </td>
                                                                            <td>
                                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                                <a target="_blank" class="mail_pg"
                                                                                    href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                                    <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;
                                                                                                        (<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                            <%# Eval("SENDER")%>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSendMail" runat="server" Text="Send Message" CssClass="btn btn-primary"
                                                            OnClick="btnSendMail_Click" ValidationGroup="SendMail"
                                                            ToolTip="Click here to Send Message" />
                                                        <asp:Button ID="btnDiscard" runat="server" Text="Discard Message" CssClass="btn btn-warning"
                                                            OnClick="btnDiscard_Click" ToolTip="Click here to Discard the Message" />

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div id="divReadMail" runat="server" visible="false" style="padding: 4px 4px 4px 4px;">
                                                    <span id="Span8" runat="server">
                                                        <asp:LinkButton ID="btnReply" runat="server" CssClass="btn btn-primary"
                                                            OnClick="btnReply_Click">Reply</asp:LinkButton>
                                                    </span>&nbsp;&nbsp;
                                                    <span id="Span9" runat="server">
                                                        <asp:LinkButton ID="btnForward" runat="server" CssClass="btn btn-primary"
                                                            OnClick="btnForward_Click">Forward</asp:LinkButton>
                                                    </span>&nbsp;&nbsp;
                                                    <span id="Span10" runat="server">
                                                        <asp:LinkButton ID="btnDeleteMail" runat="server" CssClass="btn btn-warning"
                                                            OnClick="btnDeleteMail_Click">Delete</asp:LinkButton>
                                                    </span>&nbsp;&nbsp;
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div id="MailHeader">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>From :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblMailFrom" runat="server" Font-Bold="true" />
                                                                        <asp:HiddenField ID="hidSenderUsername" runat="server" />
                                                                        <asp:HiddenField ID="hidSenderid" runat="server" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>To :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblMailTo" runat="server" Font-Bold="true" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Date :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblMailDate" runat="server" Font-Bold="true" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Subject :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblSubject" runat="server" Font-Bold="true" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-8">
                                                            <asp:Panel ID="pnlAttachment" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvAttachments" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                                                    <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString()%>&nbsp;KB)
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divMailBody" runat="server">
                                            </div>
                                            <%--<div id="divMailBody" runat="server" style="padding: 4px 4px 4px 4px; background-color: White;">
                                                                                            </div>--%>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <script type="text/javascript" language="javascript">

                            function SelectAllInMail(chkAll) {
                                try {
                                    var tbl = document.getElementById('tblInboxMailList');

                                    if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                                        for (i = 1; i < tbl.rows.length; i++) {
                                            var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvInMails_ctrl' + (i - 1).toString() + '_chkSelectMail');
                                            if (chk != null) {
                                                chk.checked = chkAll.checked;
                                            }
                                        }
                                    }
                                }
                                catch (ex) {
                                }
                            }

                            function SelectAllOutMail(chkAll) {
                                try {
                                    var tbl = document.getElementById('tblOutboxMailList');

                                    if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                                        for (i = 1; i < tbl.rows.length; i++) {
                                            var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvOutMails_ctrl' + (i - 1).toString() + '_chkSelectMail');
                                            if (chk != null) {
                                                chk.checked = chkAll.checked;
                                            }
                                        }
                                    }
                                }
                                catch (ex) {
                                }
                            }



                            function SelectAllDeletedMail(chkAll) {
                                try {
                                    var tbl = document.getElementById('tblTrash');

                                    if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                                        for (i = 1; i < tbl.rows.length; i++) {
                                            var chk = document.getElementById('ctl00_ContentPlaceHolder1_lvTrash_ctrl' + (i - 1).toString() + '_chkSelectMail');
                                            if (chk != null) {
                                                chk.checked = chkAll.checked;
                                            }
                                        }
                                    }
                                }
                                catch (ex) {
                                }
                            }


                            function openwindow(url) {


                                window.open(url, 'Add_Contact', 'addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');
                            }



                            function OpenGroupWindow(url) {


                                window.open(url, 'Add_Group', 'addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');
                            }
                        </script>

                        <%# (Eval("SENT_DATE").ToString() != "" ? Convert.ToDateTime(Eval("SENT_DATE")).ToString("ddd, dd MMM") : "--")%>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
