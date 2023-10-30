<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_IT_Transfer.aspx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_IT_Transfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">IT Transfer</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnl" runat="server">
                    <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>College</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" data-select2-enable="true"
                                        AppendDataBoundItems="true" AutoPostBack="true"
                                        TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                        ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                        InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trstaff" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%--<label>Scheme</label>--%>
                                        <label>Scheme/Staff</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" ToolTip="Select Scheme" AutoPostBack="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged"
                                        AppendDataBoundItems="True" TabIndex="2">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaffNo" runat="server" SetFocusOnError="true"
                                        ControlToValidate="ddlStaff" Display="None" ErrorMessage="Please Select Scheme"
                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Order By</label>
                                    </div>
                                    <asp:DropDownList ID="ddlorderby" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true"
                                        ToolTip="Select Order By" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="IDNO">IDNO</asp:ListItem>
                                        <asp:ListItem Value="SEQ_NO">SEQUENCE NO</asp:ListItem>
                                        <asp:ListItem Value="PFILENO">Employee Code</asp:ListItem>
                                        <asp:ListItem Value="EMPNAME">Name</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvorderby" runat="server" SetFocusOnError="true"
                                        ControlToValidate="ddlorderby" Display="None" ErrorMessage="Please Select Order By"
                                        ValidationGroup="Payroll" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Transfer IT Amount"
                            CssClass="btn btn-primary" OnClick="btnSave_Click"
                            ValidationGroup="Payroll" TabIndex="4" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            ToolTip="Click to Reset" CssClass="btn btn-warning" TabIndex="5" />
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Payroll"
                          ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12" id="div_ExportToExcel" runat="server">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lv_ITtransfer" runat="server">
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="" />
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No.
                                                </th>
                                                <%--  <th width="5%">Idno
                                                                        </th>--%>
                                                <th>Employee code
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Designation
                                                </th>
                                                <th>Department
                                                </th>
                                                <th>Tax Remaining Month
                                                </th>

                                                <th>IT Amount
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
                                            <%# Container.DataItemIndex + 1%>
                                        </td>
                                        <%--  <td width="5%">
                                                                <%#Eval("IDNO")%>
                                                            </td>--%>
                                        <td>
                                            <%#Eval("RFIDNO")%>
                                        </td>
                                        <td>
                                            <%#Eval("EMPNAME")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDESIG")%>
                                        </td>
                                        <td>
                                            <%#Eval("SUBDEPT")%>
                                        </td>

                                        <td>
                                            <%#Eval("Month")%>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Text='<%#Eval("TAX_REMAINING_MONTH")%>'
                                                ToolTip='<%#Eval("IDNO")%>' onkeyup="return check(this);" />
                                            <asp:RequiredFieldValidator ID="rfvDays" runat="server" ControlToValidate="txtAmount"
                                                Display="None" ErrorMessage="Please Enter Tax" ValidationGroup="payroll" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDays" runat="server" ControlToValidate="txtAmount" Display="None"
                                                ErrorMessage="Please Enter Numeric Value" SetFocusOnError="true" ValidationGroup="payroll"
                                                Operator="DataTypeCheck" Type="Double">  
                                            </asp:CompareValidator>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
