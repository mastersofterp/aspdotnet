<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="WorkPriorityMaster.aspx.cs" Inherits="Complaints_MASTER_WorkPriorityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">WORK PRIORITY</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Add/Edit Work Priority
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Priority Work Name <span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtWorkPriority" runat="server" CssClass="form-control" MaxLength="60" TabIndex="1" ValidationGroup="Work"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTye" runat="server" ErrorMessage="Please Enter Priority Work Name."
                                                        ControlToValidate="txtWorkPriority" Display="None" ValidationGroup="Work"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeType" runat="server" TargetControlID="txtWorkPriority" FilterType="Custom, LowercaseLetters,UppercaseLetters"
                                                        ValidChars=" ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Work" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="2" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="3" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Work" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlList" runat="server">
                                                        <asp:ListView ID="lvPriorityWork" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <h4 class="box-title">PRIORITY WORK ENTRY LIST</h4>
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Action</th>
                                                                                <th>Priority Work Name</th>
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
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" TabIndex="6"
                                                                            CommandArgument='<%# Eval("PWID") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                    </td>
                                                                    <td><%# Eval("PWNAME")%></td>
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

