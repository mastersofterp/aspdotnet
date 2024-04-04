<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BankBookReport.aspx.cs" Inherits="Account_BankBookReport" Title="Bank Book Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
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
    <asp:UpdatePanel runat="server" ID="UPDLedger">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BANK BOOK REPORT</h3>
                        </div>
                        <!--==== Page Main Body =====-->
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                            </div>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12 mt-3">
                                    <%-- <div class="panel-heading">Bank Book Report</div>--%>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Report Type</label>
                                            </div>
                                            <asp:RadioButton ID="rdbMonthWise" runat="server" Text="Month Wise" AutoPostBack="false"
                                                GroupName="DayMonth" />&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbDayWise1" runat="server" Text="Day Wise" AutoPostBack="false"
                                                GroupName="DayMonth" Checked="true" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divCalender" runat="server">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged"/>
                                                <AjaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                </AjaxToolKit:CalendarExtender>
                                                <AjaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                </AjaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged"/>
                                                <AjaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                    TargetControlID="txtUptoDate">
                                                </AjaxToolKit:CalendarExtender>
                                                <AjaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                </AjaxToolKit:MaskedEditExtender>
                                                <input id="hdnBal" runat="server" type="hidden" />
                                                <input id="hdnMode" runat="server" type="hidden" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Bank </label>
                                            </div>
                                            <asp:DropDownList ID="ddlbank" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" AppendDataBoundItems="true">
                                                <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                      
                                        </div>
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Report Type</label>
                                            </div>
                                            <asp:RadioButton ID="rdbWithNarration" runat="server" Text="With Narration" OnCheckedChanged="rdbWithNarration_CheckedChanged"
                                                GroupName="Report" />
                                            <asp:RadioButton ID="rdbWithoutNarration" runat="server" Text="Without Narration"
                                                Checked="True" OnCheckedChanged="rdbWithoutNarration_CheckedChanged" GroupName="Report" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:CheckBox ID="chkRunning" runat="server" Text="With Running Total" Checked="true" />

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnReport_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export in excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btncancel_Click" />
                                    </div>
                            </asp:Panel>
                            <div id="divMsg" runat="server">
                            </div>
                            <asp:Panel ID="pnlShow" runat="server">
                                <div class="col-12">
                                    <div class="table table-responsive">
                                    </div>
                                    <asp:GridView ID="gvTrialBalance" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" CellPadding="3" CellSpacing="2">
                                        <Columns>
                                            <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="DATE" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                <ItemStyle Wrap="False" Width="5%" HorizontalAlign="Left" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTY NAME" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CLUB_AMT" HeaderText="CLUB AMOUNT" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="20%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Vch_Type" HeaderText="VCH TYPE" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VOUCHER_NO" HeaderText="VCH NO" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Left" Width="3%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Left" Width="3%" Font-Size="Smaller" />
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
                                        <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:GridView>
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
