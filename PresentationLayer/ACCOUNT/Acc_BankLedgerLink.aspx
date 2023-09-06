<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Acc_BankLedgerLink.aspx.cs" Inherits="ACCOUNT_Acc_BankLedgerLink" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 220px;
        }
    </style>

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
                            <h3 class="box-title">LEDGER AND BANK LINKNG</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12 mt-3">
                                    <%--    <div class="panel-heading">LEDGER AND BANK LINKING</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Ledger Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlLedgerName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlLedgerName_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Selected="True">--Please Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvLedgerName" runat="server" ErrorMessage="Please Select Ledger Name" Display="None"
                                                ControlToValidate="ddlLedgerName" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Account Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAccountName" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Selected="True">--Please Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAccountName" runat="server" ErrorMessage="Please Select Account Name" Display="None"
                                                ControlToValidate="ddlAccountName" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                        ValidationGroup="submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="submit" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" />
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
