<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="SectionHead.aspx.cs"
    UICulture="auto" Inherits="ACCOUNT_SectionHead" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
  <%--  <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdSection"
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

    <asp:UpdatePanel ID="UpdSection" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Section Head</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large;">
                                </div>
                            </div>
                            <asp:Panel ID="pnl1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Section Name</label>
                                            </div>
                                            <asp:TextBox ID="txtSection" runat="server" TabIndex="1" CssClass="form-control" ValidationGroup="submit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvbank" ValidationGroup="submit" runat="server" ErrorMessage="Please Enter Section Head."
                                                ControlToValidate="txtSection" Display="None"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Section Per(%)</label>
                                            </div>
                                            <asp:TextBox ID="txtSectionPercent" runat="server" CssClass="form-control" TabIndex="2" ValidationGroup="submit"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtSectionPercent"
                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" UseSubmitBehavior="false" Text="Submit" CssClass="btn btn-primary" TabIndex="3" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" UseSubmitBehavior="false" TabIndex="4" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="vs1" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="submit" />
                                </div>

                                <div class="col-12 col-md-6 col-lg-6 mt-3 mb-5">
                                    <asp:ListBox ID="lstSectionName" runat="server" AutoPostBack="True" Rows="20" CssClass="form-control" OnSelectedIndexChanged="lstSectionName_SelectedIndexChanged" style="height:300px!important;"></asp:ListBox>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
   <%-- <script type="text/javascript">
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

    </script>--%>
</asp:Content>

