<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FileMovementStatus.aspx.cs"
    Inherits="FileMovementTracking_Transaction_FileMovementStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FILE MOVEMENT STATUS</h3>
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        Note <b>:</b> <span style="color: #FF0000">* Marked Fields Are Mandatory.</span>
                                        <asp:Panel ID="pnlFile" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">File Movement Status</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="1" OnClick="btnReport_Click"
                                                                CssClass="btn btn-info" ToolTip="Click here to Show Report" CausesValidation="false" />
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlMovement" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvFileMovement" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <h4 class="box-title">Movement Status Of Files Which Are Moved
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>FILE CODE
                                                                    </th>
                                                                    <th>FILE NAME
                                                                    </th>
                                                                    <th>SECTION NAME
                                                                    </th>
                                                                    <th>FILE PATH
                                                                    </th>
                                                                    <th>MOVEMENT DATE
                                                                    </th>
                                                                    <th>RECEIVER NAME
                                                                    </th>
                                                                    <th>STATUS
                                                                    </th>
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
                                                            <%# Eval("FILE_CODE")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FILE_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SECTION_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FILEPATH")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MOVEMENT_DATE", "{0:dd-MMM-yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FNAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("STATUS") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>                        
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--  Shrink the info panel out of view --%>
        <tr>
            <%--<td class="vista_page_title_bar" style="height: 30px">FILE MOVEMENT STATUS              
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--  Reset the sample so it can be played again --%>
        <%--  Enable the button so it can be played again --%>
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
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
            </td>
        </tr>
    </table>
    <br />

</asp:Content>

