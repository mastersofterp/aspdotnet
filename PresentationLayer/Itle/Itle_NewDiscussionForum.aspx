<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Itle_NewDiscussionForum.aspx.cs" Inherits="Itle_Itle_NewDiscussionForum" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        #ctl00_ContentPlaceHolder1_lblnewmessage a {
            color: #fff !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        if (top != self) top.location.href = location.href;
    </script>
    <script language="JavaScript" type="text/javascript">
        //onerror = report;
        var Selected = 1;


        function OnOffPost(e) {

            if (!e) e = window.event;
            var target = e.target ? e.target : e.srcElement;


            if (!target) return;

            while (target.id.indexOf('LinkTrigger') < 0) {
                //alert(target.id + target.id.indexOf('LinkTrigger')+target.parentNode);

                target = target.parentNode;
                if (target.id == null) return;
            }
            if (target.id.indexOf('LinkTrigger') < 0)
                return;


            if (Selected) {
                var body = document.getElementById(Selected + "ON");
                if (body)
                    body.style.display = 'none';
                var head = document.getElementById(Selected + "OFF");
                if (head)
                    head.bgColor = '#EDF8F4';
            }

            if (Selected == target.name) // just collapse
                Selected = "";
            else {
                Selected = target.name;
                var body = document.getElementById(Selected + "ON");
                if (body) {
                    if (body.style.display == 'none')
                        body.style.display = '';
                    else
                        body.style.display = 'none';
                }
                var head = document.getElementById(Selected + "OFF");
                if (head)
                    head.bgColor = '#B7DFD5';

                if (body && head && body.style.display != 'none') {
                    document.body.scrollTop = FindPosition(head, "Top") - document.body.clientHeight / 10;
                    OpenMessage(target.name, true);
                }
            }

            if (e.preventDefault)
                e.preventDefault();
            else
                e.returnValue = false;
            return false;
        }

        // does its best to make a message visible on-screen (vs. scrolled off somewhere).
        function OpenMessage(msgID, bShowTop) {
            var msgHeader = document.getElementById(msgID + "OFF");
            var msgBody = document.getElementById(msgID + "ON");

            // determine scroll position of top and bottom
            var MyBody = document.body;
            var top = FindPosition(msgHeader, 'Top');
            var bottom = FindPosition(msgBody, 'Top') + msgBody.offsetHeight;

            // if not already visible, scroll to make it so
            if (MyBody.scrollTop > top && !bShowTop)
                MyBody.scrollTop = top - document.body.clientHeight / 10;
            if (MyBody.scrollTop + MyBody.clientHeight < bottom)
                MyBody.scrollTop = bottom - MyBody.clientHeight;
            if (MyBody.scrollTop > top && bShowTop)
                MyBody.scrollTop = top - document.body.clientHeight / 10;
        }

        // utility
        function FindPosition(i, which) {
            iPos = 0
            while (i != null) {
                iPos += i["offset" + which];
                i = i.offsetParent;
            }
            return iPos
        }

        function report(message, url, line) {
            alert('Error : ' + message + ' at line ' + line + ' in ' + url);
        }

        // cause an <B style="COLOR: black; BACKGROUND-COLOR: #ffff66">error</B>:
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WELCOME TO NEW DISCUSSION FORUM  </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlNewDiscussionForum" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Td1" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Per page</label>
                                            </div>
                                            <asp:DropDownList ID="txtpagesize" runat="server" OnSelectedIndexChanged="txtpageSize_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="40">40</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnsetpaging" runat="server" Text="Set Pagesize" Visible="false" CssClass="btn btn-primary"
                                                OnClick="btnsetpaging_Click"></asp:Button>
                                        </div>
                                        <div class="col-12 btn-footer mb-3">
                                            <%-- <img height="16" alt="screen" src="../Images/Itle/forum_newmsg.png" width="16" align="top" border="0">
                                            --%>
                                            <asp:Label ID="lblnewmessage" CssClass="btn btn-primary" runat="server" Visible="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div onclick="OnOffPost(event)">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="ForumTable">
                                            <thead class="bg-light-blue" id="forum">
                                                <tr>
                                                    <td>Subject</td>
                                                    <td>User</td>
                                                    <td>Reply Time</td>
                                                </tr>
                                            </thead>
                                        </table>
                                        <div class="col-12 btn-footer">
                                            <img height="5" src="../images/t.png" width="1" border="0" alt="">
                                            <asp:Literal ID="ltlPost" runat="server" OnInit="ltlPost_Init"></asp:Literal>
                                            <img height="5" src="../images/t.png" width="1" border="0" alt=""></td>
                                        </div>
                                        <div class="col-12 ">
                                            <asp:Label ID="lbldate" runat="server" Font-Names="Arial" Font-Size="Smaller">Label</asp:Label>
                                            <div style="display: none;">
                                                <asp:Label ID="lblPaging" runat="server">Label</asp:Label>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

