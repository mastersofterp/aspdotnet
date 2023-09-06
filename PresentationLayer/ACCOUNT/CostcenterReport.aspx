<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CostcenterReport.aspx.cs" Inherits="ACCOUNT_CostcenterReport" Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                        <div id="divMsg" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">COST CENTER REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div id="divCompName" runat="server" style="font-size: x-large; text-align: center"></div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>session</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control" />
                                                <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                    TargetControlID="txtUptoDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <input id="hdnBal" runat="server" type="hidden" />
                                                <input id="hdnMode" runat="server" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Cost Centre </label>
                                            </div>
                                            <asp:DropDownList ID="ddlCostCentre" runat="server" CssClass="form-control " data-select2-enable="true" AppendDataBoundItems="true">
                                           
                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="Select Cost Centre" ></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btndb" runat="server" Text="REPORT" CssClass="btn btn-info" OnClick="btndb_Click" />
                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-info" OnClick="btncancel_Click" />
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
