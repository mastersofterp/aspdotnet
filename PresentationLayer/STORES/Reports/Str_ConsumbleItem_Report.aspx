<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ConsumbleItem_Report.aspx.cs" Inherits="STORES_Reports_Str_ConsumbleItem_Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }   

    </script>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div3" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Consumable Item Report</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                   <%-- <div class="sub-heading">
                                        <h5>Consumable Item Report</h5>
                                    </div>--%>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                            Visible="false">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12" id="divDelivered" runat="server">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Stock In</label>
                                                </div>
                                                <asp:RadioButtonList ID="rblAssestType" runat="server" TabIndex="1" RepeatDirection="Horizontal"
                                                    AutoPostBack="true">

                                                    <asp:ListItem Enabled="true" Selected="True" Text="Consumable &nbsp;" Value="1"></asp:ListItem>
                                                    <%--<asp:ListItem Enabled="true" Text="Near Exhausting Consumable(10%)  &nbsp;" Value="2"></asp:ListItem>--%>
                                                     <asp:ListItem Enabled="true" Text="Non Consumable  &nbsp;" Value="3"></asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <div class="form-group col-md-4">
                                                <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnReport_Click"
                                                    CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />
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

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>



