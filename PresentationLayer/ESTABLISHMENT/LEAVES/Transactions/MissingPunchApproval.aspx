<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="MissingPunchApproval.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_MissingPunchApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">MISSING PUNCH APPROVAL</h3>
                </div>
                <div class="box-body">
                        <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Missing Punch Approval</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Employee Name</label>
                                        </div>
                                        <asp:TextBox ID="lblEmpName" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Working Date</label>
                                        </div>
                                        <asp:TextBox ID="lblWDate" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>IN Time </label>
                                        </div>
                                        <asp:TextBox ID="lblInTime" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>OUT Time</label>
                                        </div>
                                        <asp:TextBox ID="lblOutTime" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Reason</label>
                                        </div>
                                        <asp:TextBox ID="lblReason" runat="server" Enabled="false" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Approve/Reject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSelect" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="A">Approve</asp:ListItem>
                                            <asp:ListItem Value="R">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer" id="Div2" runat="server">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false"
                                    CssClass="btn btn-primary" ToolTip="Click here to Reset" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </asp:Panel>                
                    <div class="col-12">
                        <asp:Panel ID="pnllist" runat="server" Visible="true">
                            <asp:Repeater ID="lvPendingList" runat="server">
                                <HeaderTemplate>
                                    <div class="sub-heading">
	                                    <h5>Miss Punch Requests List</h5>
                                    </div>
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width:100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No.
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Working Date
                                                </th>
                                                <th>Reason
                                                </th>
                                                <th>Approve/Reject
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("sno")%>
                                        </td>
                                        <td>
                                            <%# Eval("EmpName")%>
                                        </td>
                                        <td>
                                            <%# Eval("WRKDATE")%>
                                        </td>
                                        <td>
                                            <%# Eval("REASON")%>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("MPNO")%>'
                                                ToolTip="Select to Approve/Reject" CssClass="btn btn-primary" OnClick="btnApproval_Click" />
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody></table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <div id="DivNote" runat="server">
                                <div class="form-group col-12">
                                    <div class="text-center">
                                        <p style="color: Red; font-weight: bold">
                                            No Record Found..!!                                                                
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
