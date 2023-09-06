<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeActiveCompOff.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_DeActiveCompOff" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">De Active Comp-Off</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <%--<div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Comp-Off Cancel</h5>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <asp:Panel ID="pnlLeaveCard" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Comp-Off List</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvCompoff" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Employee Name
                                                    </th>
                                                    <th>Working Date
                                                    </th>
                                                    <th>Expiry Date
                                                    </th>
                                                    <th>No. of Days
                                                    </th>
                                                    <th>Deactive Comp-Off
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
                                                <%# Eval("EMPNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("WORKING_DATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("EXPIRY_DATE") %>
                                            </td>
                                            <td>
                                                <%# Eval("LEAVES") %>
                                            </td>
                                            <td>
                                                <%--  <asp:Button ID="btnCancel" runat="server" Text="DeActive" CommandName='<%# Eval("ENO") %>'
                                                    CommandArgument='<%# Eval("IDNO") %>' CssClass="btn btn-primary" TabIndex="1"
                                                    />    --%>
                                                <asp:Button ID="btnLeaveCancel" runat="server" Text="DeActive" CommandName='<%# Eval("ENO") %>'
                                                    CommandArgument='<%# Eval("IDNO") %>' CssClass="btn btn-primary" TabIndex="1" OnClick="btnLeaveCancel_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server"></div>

</asp:Content>




