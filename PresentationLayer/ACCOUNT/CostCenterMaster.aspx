<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CostCenterMaster.aspx.cs" Inherits="ACCOUNT_CostCenterMaster" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="cc2" Namespace="ASB" Assembly="AutoSuggestBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 250px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>

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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COST CENTER MASTER</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large;">
                                </div>
                            </div>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cost Category</label>
                                            </div>
                                            <asp:TextBox ID="txtCostCategory" runat="server" CssClass="form-control" ToolTip="Please Enter Cost Category"
                                                AutoPostBack="true" OnTextChanged="txtCostCategory_TextChanged"></asp:TextBox>
                                            <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtCostCategory"
                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                ServiceMethod="GetCostCategoryData" OnClientShowing="clientShowing">
                                            </ajaxToolKit:AutoCompleteExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtCostCategory" Display="None"
                                                ErrorMessage="Please Enter Cost Category" SetFocusOnError="true"
                                                ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cost Center Head</label>
                                            </div>
                                            <asp:TextBox ID="txtCCHead" ToolTip="Please Enter Cost Center Head" CssClass="form-control"
                                                runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCCHead" ControlToValidate="txtCCHead"
                                                Display="None" ErrorMessage="Please Enter cost center head" runat="server"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                        OnClick="btnSubmit_Click" ValidationGroup="submit" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                    <asp:ValidationSummary ID="vsCostcenter" runat="server" ValidationGroup="submit" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>COST CENTER HEADS</label>
                                            </div>
                                            <asp:ListBox ID="lstCostCenter" runat="server" Rows="12" CssClass="form-control"
                                                OnSelectedIndexChanged="lstCostCenter_SelectedIndexChanged"
                                                AutoPostBack="True" style="height:300px!important;"></asp:ListBox>
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

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
</asp:Content>
