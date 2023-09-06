<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TeamDetails.aspx.cs"
    Inherits="Sports_Masters_TeamDetails" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>   --%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TEAM DETAILS</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlDesig" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Team Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlTeamName" CssClass="form-control" data-select2-enable="true" ToolTip="Select Team" runat="server"
                                                AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlTeamName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Team Name." ControlToValidate="ddlTeamName" InitialValue="0"
                                                Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdblistPlayerType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rdblistPlayerType_SelectedIndexChanged" ToolTip="Select Team Members" TabIndex="2">
                                                <asp:ListItem Selected="True" Value="1">Staff Members</asp:ListItem>
                                                <asp:ListItem Value="2">Players</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Post" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Post</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPost" CssClass="form-control" data-select2-enable="true" ToolTip="Select Post" runat="server" AppendDataBoundItems="true" TabIndex="3">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPost" runat="server" ErrorMessage="Please Select Post." ControlToValidate="ddlPost" InitialValue="0"
                                                Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Player" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Players</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPlayer" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Players" AppendDataBoundItems="True" TabIndex="4">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPlayer" ValidationGroup="Submit" ControlToValidate="ddlPlayer"
                                                Display="None" ErrorMessage="Please Select Player." SetFocusOnError="true" runat="server" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Role" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Role</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Role" AppendDataBoundItems="True" TabIndex="5"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRole" Display="None" runat="server" SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please Select Players Role."
                                                ControlToValidate="ddlRole" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                            <div class=" col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="6" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Report" CssClass="btn btn-info" ToolTip="Click to get Report" OnClick="btnShowReport_Click" TabIndex="9" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Cancel" CausesValidation="false" TabIndex="7" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvTeamPlayer" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>TEAM DETAILS ENTRY</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit
                                                            </th>
                                                            <th>Sport 
                                                            </th>
                                                            <th>Team 
                                                            </th>
                                                            <th>Player 
                                                            </th>
                                                            <th>Role
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
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("TDID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PLAYERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ROLENAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvTeamPost" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <h4 class="sub-heading">
                                                   <%-- <h5>TEAM DETAILS ENTRY</h5>--%>
                                                     <div class="sub-heading">
                                                    <h5>TEAM DETAILS ENTRY</h5>
                                                </div>
                                                </h4>
                                                <table class="table table-striped table-bordered nowrap display">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Edit
                                                            </th>
                                                            <th>Sport
                                                            </th>
                                                            <th>Team
                                                            </th>
                                                            <th>Post
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
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%# Eval("TDID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                </td>
                                                <td>
                                                    <%# Eval("SNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TEAMNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("POSTNAME")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


