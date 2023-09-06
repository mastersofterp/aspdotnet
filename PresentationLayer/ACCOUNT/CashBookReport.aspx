<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CashBookReport.aspx.cs" Inherits="CashBookReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPDLedger"
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
                        <div class="box-header with-border">
                            <h3 class="box-title">CASH BOOK REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Report Type</label>
                                                </div>
                                                <asp:RadioButton ID="rdbMonthWise" runat="server" Text="Month Wise" Checked="true"
                                                    AutoPostBack="false" GroupName="DayMonth" />&nbsp;
                                            <asp:RadioButton ID="rdbDayWise1" runat="server" Text="Day Wise" AutoPostBack="false"
                                                GroupName="DayMonth" />
                                            </div>
                                            <div class="col-5" id="divCalender" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>From Date</label>
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
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>To Date</label>
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
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Report Type</label>
                                                </div>
                                                <asp:RadioButton ID="rdbWithNarration" runat="server" Text="With Narration" Checked="true"
                                                    AutoPostBack="true" OnCheckedChanged="rdbWithNarration_CheckedChanged" GroupName="Report" />&nbsp;
                                            <asp:RadioButton ID="rdbWithoutNarration" runat="server" Text="Without Narration"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="rdbWithoutNarration_CheckedChanged"
                                                GroupName="Report" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Ledger Name</label>
                                                </div>
                                                <asp:ListBox ID="lstCash" runat="server" CssClass="form-control" Style="height: 300px!important"></asp:ListBox>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:CheckBox ID="chkRunning" runat="server" Text="With Running Total" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />

                                        <%--<asp:Button ID="btnCondensed" runat="server" Text="Condensed Cashbook Report" Width="200px" OnClick="btnCondensed_Click" />--%>
                                        <asp:Button ID="btnOldFormat" runat="server" OnClick="btnOldFormat_Click" Text="OldFormat" CssClass="btn btn-primary"
                                            Visible="False" />

                                        <asp:Button ID="btnExport" runat="server" Text="Export in excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btncancel_Click" />
                                    </div>
                                </asp:Panel>
                                <div id="divMsg" runat="server">
                                </div>
                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gvTrialBalance" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover" CellPadding="3" CellSpacing="2">
                                            <Columns>
                                                <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="DATE" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                    <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTY NAME" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CLUB_AMT" HeaderText="CLUB AMOUNT" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Vch_Type" HeaderText="VOUCHER TYPE" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="VOUCHER_NO" HeaderText="VOUCHER NO" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTDEBIT" HeaderText="DEBIT" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TOTCREDIT" HeaderText="CREDIT" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CHQ_NO" HeaderText="CHEQUE NO" ControlStyle-Font-Size="Smaller">
                                                    <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" HorizontalAlign="Center" />

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
