<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CommitteeCellUser.aspx.cs" Inherits="Complaints_MASTER_CommitteeCellUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE SERVICE REQUEST CELL USERS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Service Request Cell Users
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Department <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlDept" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDept" Display="None" InitialValue="0" ErrorMessage="Please Select Department" SetFocusOnError="true"
                                                        ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Employee <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmployee" Display="None" ErrorMessage="Please Select Employee." ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Submit" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <asp:ListView ID="lvCreateUser" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">SERVICE REQUEST CELL USER LIST</h4>
                                                                    <table class="table table-responsive table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Edit</th>
                                                                                <th>Employee Name</th>
                                                                                <th>Department</th>
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("USERNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;                                                    
                                                                    </td>
                                                                    <td><%# Eval("EMPNAME")%></td>
                                                                    <td><%# Eval("DEPTNAME")%></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

