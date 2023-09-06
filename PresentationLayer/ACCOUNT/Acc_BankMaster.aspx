<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acc_BankMaster.aspx.cs" Inherits="Acc_BankMaster"
    Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 150px;
        }
    </style>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBank"
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
    <asp:UpdatePanel ID="updBank" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK MASTER</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Bank Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Enter Bank Name</label>
                                            </div>
                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" ValidationGroup="submit" MaxLength="70" ToolTip="Enter Bank Name"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender FilterType="Custom" ID="flt1" FilterMode="InvalidChars" InvalidChars="!@#$%^&*_=+';:/?1234567890"
                                                runat="server" TargetControlID="txtBankName" />
                                            <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankName"
                                                Display="None" ErrorMessage="Please Enter bank name..!" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                        ValidationGroup="submit" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" OnClick="btnCancel_Click" Text="Cancel" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="submit" />
                                </div>

                            </asp:Panel>

                            <div class="col-12 mb-4">
                                <asp:Panel ID="pnlShow" runat="server">
                                    <asp:ListView ID="lvBank" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Bank List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                           
                                                            <th>
                                                                <%--Id. No.--%>
                                                            </th>
                                                            <th>Bank Name
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
                                            <tr class="item">
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonEdit" runat="server" ImageUrl="../Images/edit.png"
                                                        CommandArgument='<%#Eval("BANKNO") %>' ToolTip="Edit Record" OnClick="ImageButtonEdit_Click" />
                                                </td>
                                                <%--<td style="text-align: left; width: 0%; visibility:hidden">
                                                                <asp:Label ID="lblBankNo" runat="server" Text='<%# Eval("BANKNO") %>' Visible="false"></asp:Label>
                                                            </td>--%>
                                                <td>
                                                    <asp:Label ID="lblBnakName" runat="server" Text='<%# Eval("BANKNAME") %>'></asp:Label>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
