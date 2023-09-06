<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Browser.aspx.cs" Inherits="ITLE_Browser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DOWNLOAD BROWSER</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlBrowser" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Browser</div>
                                <div class="panel panel-body">
                                    <div class="col-md-12">
                                        <div class="text-center">
                                            <p>
                                                <b>Your Browser Doe's not support Online Exam</b>
                                            </p>
                                            <p>
                                                <b>You have to Download Latest Internet Explorer, Below is the link given to Download</b>
                                            </p>
                                            <p>
                                                <a href="http://www.microsoft.com/windows/internet-explorer/default.aspx" target="_blank">Click Here</a>
                                            </p>
                                            <p>
                                            </p>
                                            <p>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--  Shrink the info panel out of view --%>
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit1.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <%--  Enable the button so it can be played again --%>
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>
                <script type="text/javascript">
                    function disableBackButton() {
                        window.history.forward();
                    }
                    setTimeout("disableBackButton()", 0);
                    window.onunload = function () {
                        null
                    };

                </script>
                <script type="text/javascript">
                    function open_win() {
                        window.open("DemoTest.aspx", "_blank", "toolbar=no,directories =yes, location=no, directories=no, status=no, menubar=no,fullscreen=yes,  scrollbars=yes, resizable=no, copyhistory=no, width=1000, height=600");
                    }
                </script>
            </td>
        </tr>
    </table>

    <%-- <p align="center">
        You Browser Does not support Online Exam
    </p>
    <p align="center">
        You have to Download Latest Internet Explorer, Below is the link given to Download
    </p>
    <p align="center">
        <a href="http://www.microsoft.com/windows/internet-explorer/default.aspx" target="_blank">Click Here</a>
    </p>
    <p>
    </p>
    <p>
    </p>--%>



    <%--    <table>
   <tr>
   <td class="form_left_label">
        <asp:HyperLink ID="hpLink" runat="server" Target="_blank" ></asp:HyperLink>
   </td>   
   </tr>
    </table>--%>
</asp:Content>

