<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LedgerReport.aspx.cs" Inherits="LedgerReport" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
        }
    </script>
    <%--<script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

    <script language="javascript" type="text/javascript">
        function CheckFields() {
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').value == '') {
                alert('Please Enter From Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').value == '') {
                alert('Please Enter Upto Date.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').focus();
                return false;
            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value == '') {
                alert('Please Enter Ledger.');
                document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
                return false;
            }
        }
        function popUpToolTip(CAPTION) {
            var strText = CAPTION;
            overlib(strText, 'Tool Tip', 'CreateSubLinks');
            return true;
        }

        function ShowledgerReport(ledger, party_no, fromDate, Todate) {
            var popUrl = 'Acc_ledgerReportGrid.aspx?ledger=' + ledger + '&party_no=' + party_no + '&fromDate=' + fromDate + '&Todate=' + Todate;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=1,' +
                     'status=no,toolbar=no,titlebar=no,' +
                     'left=50,top=35,width=900px,height=650px,scrollbars=1';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>

    <%--   <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>
    <script language="javascript" type="text/javascript">
        function ShowLedger() {
            var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                             'status=no,toolbar=no,titlebar=no,' +
                             'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function AskSave() {
            if (confirm('Do You Want To Save The Transaction ? ') == true) {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value = 0;
                return false;
            }
        }

        function CheckTranChar(obj) {
            var k = (window.event) ? event.keyCode : event.which;
            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46) {
                obj.style.backgroundColor = "White";
                return true;
            }
            else {
                alert('Please Enter Either C OR D');
                obj.focus();
            }
            return false;
        }

        function ShowHelp() {
            var popUrl = 'PopUp.aspx?fn=' + 'LedgerHelp';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
                             'status=no,toolbar=no,titlebar=no,' +
                             'left=100,top=50,width=600px,height=300px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function SetFoc(obj) {
            obj.style.backgroundColor = SetTextBackColor();  // This function is created at Master page , register by javascript.
            var objRange = obj.createTextRange();
            objRange.moveStart("character", 0);
            objRange.moveEnd("character", obj.value.length);
            objRange.select();
            obj.focus();
        }

        function updateValues(popupValues) {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }
    </script>

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
                            <h3 class="box-title">LEDGER REPORT</h3>
                        </div>
                        <!--==== Page Main Body =====-->
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="col-12  mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divCalender" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="imgCal">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" TabIndex="4" />
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
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="imgCal1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control"
                                                        AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" TabIndex="5" />
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
                                            <div  class="form-group col-lg-6 col-md-6 col-12"  >   

                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Ledger Name</label>
                                                </div>
                                                <asp:TextBox ID="txtAcc" runat="server" CssClass="form-control" ToolTip="Please Enter Bank Name"
                                                    AutoPostBack="true" OnTextChanged="txtAcc_TextChanged"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtAcc"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1"
                                                    ServiceMethod="GetLedgerkName" OnClientShowing="clientShowing">
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtAcc" Display="None"
                                                    ErrorMessage="Please Enter Bank Name" SetFocusOnError="true"
                                                    ValidationGroup="AccMoney">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:TextBox ID="lblCurBal" runat="server" BorderColor="White"
                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                    Font-Size="Small" Font-Bold="true"></asp:TextBox>
                                                <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="21px" BorderColor="White"
                                                    BorderStyle="None"
                                                    Style="background-color: Transparent;" ReadOnly="True"
                                                    Font-Size="XX-Small"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-2 col-md-6 col-12" ></div>

                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Report Type</label>
                                                </div>
                                                <asp:RadioButton ID="rdbWithNarration" runat="server" Text="With Narration" GroupName="Report" />&nbsp;
                                                    <asp:RadioButton ID="rdbWithoutNarration" runat="server" Text="Without Narration"
                                                        Checked="True" GroupName="Report" />
                                            </div>

                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButtonList ID="rdbLedgerList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Text="Condense" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Detailed" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                             <%--  <div class="form-group col-lg-6 col-md-6 col-12"></div>--%>
                                        
                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                <div  class="label-dynamic" >    
                                                    <sup></sup>
                                                    <label></label>
                                                </div>                                            
                                                <asp:CheckBox ID="chkRunningTot" runat="server" Text="With Running Total" Checked="true" />

                                            </div>

                                             <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                     <asp:CheckBox ID="chkAdjustment" runat="server" Text="With Adjustment Voucher Report" Checked="false" />

                                                </div>
                                               
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <div class="col-md-2"></div>
                                        <asp:Button ID="bntShowGrid" runat="server" Text="Show in Grid" OnClick="btnShowGrid_Click"
                                            CssClass="btn btn-primary" Visible="false" />
                                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Ledger Report"
                                            CssClass="btn btn-info" ValidationGroup="LedReport" />
                                        <asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-primary"
                                            OnClick="btnExportExcel_Click" />
                                        <asp:Button ID="btnSummary" runat="server" Text="Summary Report" CssClass="btn btn-info"
                                            OnClick="btnSummary_Click" ValidationGroup="LedReport" Visible="False" />
                                        <asp:Button ID="btnLedgerList" runat="server" Text="Ledger List" CssClass="btn btn-primary"
                                            OnClick="btnLedgerList_Click" Visible="false" />
                                        <asp:Button ID="btnAllLedgerRpt" runat="server" Text="All Ledger Report " CssClass="btn btn-info"
                                            OnClick="btnAllLedgerRpt_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            OnClick="btnCancel_Click" TabIndex="2" />
                                        <asp:ValidationSummary ID="vsLedger" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="LedReport" />
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="GridPanel" runat="server">
                                    <div class="col-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridExcel" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" CellPadding="3" CellSpacing="2">
                                                <Columns>
                                                    <asp:BoundField DataField="TRANSACTION_DATE" HeaderText="DATE" HtmlEncode="false"
                                                        DataFormatString="{0:d}" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Mode" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTICULARS" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="30%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Vch_Type" HeaderText="VOUCHER TYPE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="VOUCHER NO" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="left" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="DEBIT" HtmlEncode="false" DataFormatString="{0:n}"
                                                        ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREDIT" HeaderText="CREDIT" HtmlEncode="false" DataFormatString="{0:n}"
                                                        ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PARTICULARS" HeaderText="NARRATION" HtmlEncode="false"
                                                        ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="20%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <%--<asp:BoundField DataField="curbal" HeaderText="RUNNING TOTAL" HtmlEncode="false"
                                                ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Right" Width="5%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Right" Width="5%" Font-Size="Smaller" />
                                            </asp:BoundField>--%>
                                                </Columns>
                                                <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" HorizontalAlign="Center" />

                                            </asp:GridView>
                                        </div>
                                    </div>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
