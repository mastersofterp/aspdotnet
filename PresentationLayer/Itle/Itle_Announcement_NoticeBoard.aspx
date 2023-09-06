<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Itle_Announcement_NoticeBoard.aspx.cs" Inherits="Itle_Itle_Announcement_NoticeBoard" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="JQuery/JquerySlide.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VIRTUAL NOTICE BOARD</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlCourse" runat="server">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-10">
                                                <div style="background: url('../images/noticeBoard2.jpg'); background-repeat: no-repeat; height: 450px; width: 650px">
                                                    <div style="height: 32px; width: 550px"></div>
                                                    <div style="height: 380px; width: 550px; overflow: auto">
                                                        <div class="text-center">
                                                            <table width="95%">
                                                                <tr style="height: 15px">
                                                                    <td></td>
                                                                </tr>
                                                                <tr style="height: 350px; overflow: auto">
                                                                    <td></td>
                                                                    <td>

                                                                        <asp:Label ID="lblNotice" runat="server"> <%=fMarquee%></asp:Label>

                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <%--<asp:Label ID="lblFacultyAnnounce" runat="server" ></asp:Label>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <%--<script type="text/javascript">
                 $(document).ready(function() {
                 var imgbtn = document.getElementById("<%= imgPage1%>").value);
                 alert(imgbtn);
                     $("imgbtn").click(function() {
                         
                         $("imgbtn").animate({
                             left: '100px',
                             height: '150px',
                             width: '150px'
                         });
                     });
                 });
          </script>--%>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

