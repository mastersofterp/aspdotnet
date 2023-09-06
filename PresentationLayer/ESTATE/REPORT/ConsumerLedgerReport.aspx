<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ConsumerLedgerReport.aspx.cs" Inherits="ESTATE_Report_ConsumerLedgerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updQuarterReport" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CONSUMER LEDGER REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Consumer Ledger Report
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Select Reading Month<span style="color: red;">*</span>:</label>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtFromdate" runat="server" TabIndex="12" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server"
                                                            Enabled="True" Format="MM/yyyy" PopupButtonID="imgCal"
                                                            TargetControlID="txtFromdate" />
                                                        <%--  <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" 
                    AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                    TargetControlID="txtFromdate" />--%>
                                                        <div class="input-group-addon">
                                                           <%-- <asp:Image ID="imgCal" runat="server" Height="18px"
                                                                ImageUrl="~/images/calendar.png" Style="cursor: pointer" Width="16px" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgCal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<td style="width: 102px" align="right">
                To Date</td>
            <td style="width: 50px">
                <center style="width: 50px">
                    <b>:</b></center>
            </td>
            <td>
                <asp:TextBox ID="txtTodate" runat="server" style="margin-left: 0px" 
                    TabIndex="13" Width="90px"></asp:TextBox>
                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    CssClass="cal_Theme1" Enabled="True" Format="dd/MM/yyyy" 
                    PopupButtonID="imgCal1" TargetControlID="txtTodate" />
                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date" 
                    TargetControlID="txtTodate" />
                <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" 
                    Style="cursor:pointer" />
            </td>--%>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="13" 
                                                        Text="Report" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnreset" runat="server" OnClick="btnreset_Click" Text="Reset" TabIndex="14" 
                                                        CssClass="btn btn-warning" />
                                                    <%--<asp:Button ID="btnWaterReading" runat="server" onclick="btnWaterReading_Click" 
                                                           Text="Watere Reading" Width="113px" />--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

