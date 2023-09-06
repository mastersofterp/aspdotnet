<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Access_Link_Payroll.aspx.cs" Inherits="PayRoll_Access_Link_Payroll"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADD MODULE AND LINK NAME IN PAY REF</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Module and Link Name</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Module Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlModuleName" AppendDataBoundItems="true" ToolTip="Select Module Name" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvModuleName" runat="server" ControlToValidate="ddlModuleName"
                                                Display="None" ErrorMessage="Select Module Name" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Link Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlLinkName" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select Link Name" runat="server" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rvfLinkName" runat="server" ControlToValidate="ddlLinkName"
                                                Display="None" ErrorMessage="Select Link Name" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" TabIndex="3" ToolTip="Click to Reset" CssClass="btn btn-primary"
                                    OnClick="btnSub_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" ToolTip="Click to Reset" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvPayRef" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Link Name</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Module Name
                                                    </th>
                                                    <th>Link Name
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
                                                <%# Eval("ModuleName")%>
                                            </td>
                                            <td>
                                                <%# Eval("LinkName")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
