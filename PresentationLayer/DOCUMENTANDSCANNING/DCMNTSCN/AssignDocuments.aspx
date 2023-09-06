<%@ Page Title="" Language="C#" MasterPageFile="~/BlankMasterPage.master" AutoEventWireup="true" CodeFile="AssignDocuments.aspx.cs"
    Inherits="DOCUMENTANDSCANNING_DCMNTSCN_AssignDocuments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">AUTHORIZE DOCUMENTS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnl" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Select Department"
                                        AppendDataBoundItems="True" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div class=" col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="show"
                            OnClick="btnShow_Click" ToolTip="Click here to Show Report" TabIndex="2" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" CausesValidation="false"
                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" TabIndex="3" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" Visible="false"
                            OnClick="btnReport_Click" ToolTip="Click here to Show Report" TabIndex="4" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="show" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlListMain" runat="server" Visible="false" ScrollBars="Auto">
                            <asp:ListView ID="lvUsers" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>User List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Employee Code
                                                    </th>
                                                    <th>Employee Name
                                                    </th>
                                                    <th>Designation
                                                    </th>
                                                    <th>Assigned Category
                                                    </th>
                                                    <th>Shared Documents
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("PFILENO") %>
                                        </td>
                                        <td>
                                            <%# Eval("NAME") %>
                                        </td>
                                        <td>
                                            <%# Eval("SUBDESIG") %>
                                        </td>
                                        <td>
                                            <%# Eval("CATEGORY") %>
                                        </td>
                                        <td>
                                            <%# Eval("DOCUMENTS") %>
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
   
    <div id="divMsg" runat="server"></div>
</asp:Content>
