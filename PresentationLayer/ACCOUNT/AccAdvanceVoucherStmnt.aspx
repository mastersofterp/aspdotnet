<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccAdvanceVoucherStmnt.aspx.cs" Inherits="ACCOUNT_AccAdvanceVoucherStmnt" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>




    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADVANCE VOUCHER STATEMENT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" visible="false" style="font-size: x-large; text-align: center"></div>
                                <asp:Panel ID="pnlDayBookReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Advance Voucher Statement</div>
                                        <div class="panel-body">

                                            <div class="row" id="divSelectUser" runat="server">
                                                <div class="col-md-2">
                                                    <label>Select User :</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlEmpType" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Employee</asp:ListItem>
                                                        <asp:ListItem Value="2">Payee</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-md-2" id="divEmployee1" runat="server" visible="false">
                                                    <label>Employee<span style="color: red">*</span></label>
                                                </div>
                                                <div class="col-md-2" id="divEmployee2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2" id="divPayeeNature1" runat="server" visible="false">
                                                    <label>Payee Nature<span style="color: red">*</span></label>
                                                </div>
                                                <div class="col-md-2" id="divPayeeNature2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlPayeeNature" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPayeeNature_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2" id="divPayee1" runat="server" visible="false">
                                                    <label>Payee Name<span style="color: red">*</span></label>
                                                </div>
                                                <div class="col-md-2" id="divPayee2" runat="server" visible="false">
                                                    <asp:DropDownList ID="ddlPayee" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <br />
                                            <div id="pnlBetTwoDates" runat="server">
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <label>From Date :</label>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
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
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <label>Upto Date :</label>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtUptoDate" CssClass="form-control" runat="server" />
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
                                            <div class="form-group col-md-12" id="trBtn" runat="server">
                                                <div class="col-md-2"></div>
                                                <asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-primary" OnClick="btnExportExcel_Click"/>

                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="divMsg" runat="server">
                                </div>

                                <asp:Panel ID="GridPanel" runat="server">
                                    <asp:GridView ID="GridExcel" runat="server" Width="100%"
                                        AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="SlNo" HeaderText="Sl.No." HtmlEncode="false" DataFormatString="{0:d}" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle Wrap="False" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="100%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="100%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AmountPaid" HeaderText="Amount Paid" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentVoucherNo" HeaderText="Payment VoucherNo. TYPE" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ChequeUTRNo" HeaderText="Cheque/UTRNo." HtmlEncode="false" DataFormatString="{0:n}" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AmountAdjusted" HeaderText="Amount Adjusted" HtmlEncode="false" DataFormatString="{0:n}" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AdjustedVoucherNo" HeaderText="Adjusted Voucher No." ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="30%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="30%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AdjustedDate" HeaderText="Adjusted Date" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AmountRefunded" HeaderText="Amount Refunded" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>

                                             <asp:BoundField DataField="RefundedVoucherNo" HeaderText="Refunded Voucher No." ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="RefundedDate" HeaderText="Refunded Date" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Balance" HeaderText="Balance" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Remark" HeaderText="Remark" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

