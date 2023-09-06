<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExecutedTeachingPlan.aspx.cs" Inherits="Itle_TeachingPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">EXECUTED TEACHING PLAN ENTRY</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlExecuted" runat="server">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlExecutedTeahingPlan" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Select Teaching Plan</div>
                                        <div class="panel panel-body">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <label>Current Session&nbsp;&nbsp;:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="true" ForeColor="Green" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <label>Course Name&nbsp;&nbsp;:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lblCourseName" runat="server" Font-Bold="true" ForeColor="Green" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="form-group col-md-12">
                                                    <div class="col-md-2">
                                                        <label>Select Teaching Plan&nbsp;&nbsp;:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:DropDownList ID="ddlTeachingPlan" runat="server" AppendDataBoundItems="true"
                                                            AutoPostBack="true" CssClass="form-control" ToolTip="Select Teaching Type"
                                                            OnSelectedIndexChanged="ddlTeachingPlan_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <label>Get Report for Session&nbsp;&nbsp;:</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-primary"
                                                            OnClick="btnShowReport_Click" ToolTip="Click here to Show report" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12" id="divDetails" runat="server" visible="false">
                                <div class="panel panel-info">
                                    <div class="panel panel-heading">Add or Edit</div>
                                    <div class="panel panel-body">
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>Start Date&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblStartDate" runat="server" Font-Bold="true" ForeColor="Green" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>End Date&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblEndDate" runat="server" Font-Bold="true" ForeColor="Green" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>Subject&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblSubject" runat="server" Font-Bold="true" ForeColor="Green" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-group col-md-12">
                                                <div class="col-md-2">
                                                    <label>Description&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <div id="divDesc" runat="server" style="height: 100px; overflow: scroll; border: solid 1px olive; background-color: #E6F3F8;">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-group col-md-12">
                                                <div class="col-md-2">
                                                    <label>Actual Topics Covered&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtTopicsCovered" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="form-group col-md-12">
                                                <div class="col-md-2">
                                                    <label>Deviation (if any)&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDeviation" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>Reason for deviation&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtDevtReason" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label>Learning Resourses Used&nbsp;&nbsp;:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtResources" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <div class="col-md-2">
                                                    <label></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                                        OnClick="btnSubmit_Click" ToolTip="Click here to Submit" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                        OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                                    &nbsp;&nbsp;
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlExecTPlanList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvExecTPlan" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4 class="box-title">Executed Teaching Plan List
                                                        </h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Action
                                                                    </th>
                                                                    <th>Covered Topic
                                                                    </th>
                                                                    <th>Deviation
                                                                    </th>
                                                                    <th>Resources Used
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("EXEC_TPLAN_NO") %>'
                                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("COVERED_TOPIC")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DEVIATION")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("RESOURCES")%>
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
    </div>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
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
        <tr>
            <td style="padding: 10px 10px 10px 10px"></td>
        </tr>
    </table>
</asp:Content>
