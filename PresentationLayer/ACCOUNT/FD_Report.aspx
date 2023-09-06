<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="FD_Report.aspx.cs" Inherits="ACCOUNT_FD_Report" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <div style="width: 100%">
        <asp:UpdatePanel ID="UPDLedger" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">FIXED DEPOSIT REPORT</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <b>Note : <span style="color: red">* mark is mandatory</span></b>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">
                                                Fixed Deposit Report
                                            </div>
                                            <div class="panel-body">
                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>Investment From Date<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgInvestFrmDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtInvestmentFrmDate" Width="200px" runat="server" TabIndex="6" CssClass="form-control"
                                                                Style="text-align: right"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                PopupButtonID="imgInvestFrmDate" TargetControlID="txtInvestmentFrmDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                OnInvalidCssClass="errordate" TargetControlID="txtInvestmentFrmDate" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtInvestmentDate" runat="server"
                                                                ControlToValidate="txtInvestmentFrmDate" Display="None"
                                                                ErrorMessage="Please Enter Investment Date" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <label>Investment UpTo Date<span style="color: red">*</span> :</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtInvestmentToDate" Width="200px" runat="server" TabIndex="6" CssClass="form-control"
                                                                Style="text-align: right"></asp:TextBox>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                PopupButtonID="Image1" TargetControlID="txtInvestmentToDate">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                                DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                OnInvalidCssClass="errordate" TargetControlID="txtInvestmentToDate" CultureAMPMPlaceholder=""
                                                                CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                Enabled="True">
                                                            </ajaxToolKit:MaskedEditExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                ControlToValidate="txtInvestmentToDate" Display="None"
                                                                ErrorMessage="Please Enter Investment Date" SetFocusOnError="true"
                                                                ValidationGroup="AccMoney">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-12 col-md-offset-3">
                                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" Text="Show Report" ToolTip="Click to show report" OnClick="btnReport_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" ToolTip="Click to cancel" OnClick="btnCancel_Click" />
                                                    </div>
                                                </div>
                                                <div id="divMsg" runat="server">
                                                </div>
                                                <asp:GridView ID="GridExcel" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="ROWID" HeaderText="ID" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SR_NO" HeaderText="Sr. No." ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FDRNUMBER" DataFormatString="&nbsp;{0}"  HeaderText="FDR NUMBER" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="LEDGERNAME" HeaderText="PARTY NAME" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BANKNAME" HeaderText="BANK NAME" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVESTMENTDATE" HeaderText="INVESTMENT DATE" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MATURITY_DATE" HeaderText="MATURITY DATE" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVESTED_AMT" HeaderText="INVESTED AMOUNT" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MATURITY_AMT" HeaderText="MATURITY AMOUNT" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ROI" HeaderText="ROI" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Left" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ACCOUNT_HOLDER" HeaderText="ACCOUNT HOLDER" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAN_NO" HeaderText="PAN NO" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOMINATION" HeaderText="NOMINATION" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="STATUS" HeaderText="STATUS" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
