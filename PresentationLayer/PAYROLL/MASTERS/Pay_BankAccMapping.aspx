<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_BankAccMapping.aspx.cs"
    Inherits="PAYROLL_MASTERS_Pay_BankAccMapping" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div>
       <%-- <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
        </asp:UpdateProgress>--%>
    </div>
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK ACCOUNT MAPPING</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Bank account mapping</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" ToolTip="Select College" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                ValidationGroup="payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Scheme</label>
                                            </div>
                                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Select Scheme" AppendDataBoundItems="true" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="ddlScheme"
                                                Display="None" ErrorMessage="Please Select Scheme" ValidationGroup="payroll" SetFocusOnError="True"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlfrom" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select staff" ValidationGroup="payroll" SetFocusOnError="True"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Bank</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBank"
                                                Display="None" ErrorMessage="Please Select bank" ValidationGroup="payroll" SetFocusOnError="True"
                                                InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Bank Account No.</label>
                                            </div>
                                            <asp:TextBox ID="txtBankAccNo" runat="server" CssClass="form-control" TabIndex="5" MaxLength="16" ToolTip="Enter Bank Account Number" />
                                            <asp:RequiredFieldValidator ID="rfvtxtminlimit" runat="server" ControlToValidate="txtBankAccNo"
                                                Display="None" ErrorMessage="Please Enter Bank Account Number" ValidationGroup="payroll"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFirstName" runat="server"
                                                TargetControlID="txtBankAccNo"
                                                FilterType="Custom"
                                                FilterMode="ValidChars"
                                                ValidChars="0123456789">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </asp:Panel>
                            <div runat="server" id="pnlList">
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" TabIndex="6" ToolTip="Click to Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7" ToolTip="Click to Reset"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div id="List1" runat="server">
                                <div class="col-12">
                                    <asp:ListView ID="lv_Bank" runat="server">
                                        <EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                                <div class="sub-heading">
	                                                <h5>Bank Account Mapping</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action
                                                            </th>
                                                            <th>Scheme
                                                            </th>
                                                            <th>Staff
                                                            </th>
                                                            <th>Bank
                                                            </th>
                                                            <th>Bank A/c Number
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("BankAccMappingId")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="7" />&nbsp;                                                     
                                                </td>
                                                <td>
                                                    <%# Eval("SCHEME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STAFF")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BANKNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BANK_ACCNO")%> 
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

