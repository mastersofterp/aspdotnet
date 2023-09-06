<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="ACC_Account_Master.aspx.cs" Inherits="ACCOUNT_ACC_Account_Master" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
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

    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COMPANY ACCOUNT MASTER</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Account Name </label>
                                            </div>
                                            <asp:TextBox ID="txtAccountName" runat="server" ToolTip="Please Enter Account Name" CssClass="form-control" autocomplete="off" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfAccName" runat="server" ControlToValidate="txtAccountName" Display="None"
                                                ErrorMessage="Please Enter Account Name" ValidationGroup="Submit"
                                                SetFocusOnError="True" Text="*"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" ValidationGroup="Submit" CssClass="btn btn-primary" TabIndex="2" Text="Submit" ToolTip="Click to Save" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-warning" TabIndex="3" Text="Cancel" ToolTip="Click to Cancel" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="vsSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Search </label>
                                            </div>
                                            <asp:TextBox ID="txtSearch" runat="server" AutoPostBack="True" Style="text-transform: uppercase"
                                                Text="" ToolTip="Please Enter Group Name" CssClass="form-control" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                            <div class="mt-2">
                                                <asp:ListBox ID="lstAccountName" runat="server" AutoPostBack="True" Rows="20" CssClass="form-control" Style="height: 300px!important;" OnSelectedIndexChanged="lstAccountName_SelectedIndexChanged"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
