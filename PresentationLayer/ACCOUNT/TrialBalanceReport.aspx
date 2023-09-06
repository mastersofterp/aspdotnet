<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TrialBalanceReport.aspx.cs" Inherits="TrialBalanceReportClass" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
        function CheckFields() {

            if (document.getElementById('ContentPlaceHolder1_txtFrmDate').value == '') {
                alert('Please Enter From Date.');
                document.getElementById('ContentPlaceHolder1_txtFrmDate').focus();
                return false;

            }

            if (document.getElementById('ContentPlaceHolder1_txtUptoDate').value == '') {
                alert('Please Enter Upto Date.');
                document.getElementById('ContentPlaceHolder1_txtUptoDate').focus();
                return false;

            }



            if (document.getElementById('ContentPlaceHolder1_txtAcc').value == '') {
                alert('Please Enter Ledger.');
                document.getElementById('ContentPlaceHolder1_txtAcc').focus();
                return false;

            }

        }

        function popUpToolTip(CAPTION) {

            var strText = CAPTION;
            overlib(strText, 'Tool Tip', 'CreateSubLinks');
            return true;
        }
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

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
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 0;
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
            document.getElementById('ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }


    </script>

    
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
            <div class="form-group col-md-12">
                <div class="col-md-12">

                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">TRIAL BALANCE REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" class="account_compname" style="text-align: center; font-size: x-large">
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">TRIAL BALANCE REPORT</div>
                                        <div class="panel-body">

                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Report Type : </label>
                                                </div>
                                                <div class="col-md-5">
                                                    <asp:RadioButton runat="server" ID="rdbGeneral" Text="General Trial Balance" GroupName="ReportType"
                                                        Checked="true" AutoPostBack="True" OnCheckedChanged="rdbGeneral_CheckedChanged" />&nbsp;
                                                <asp:RadioButton runat="server" ID="rdbGroup" Text="Group Trial Balance" GroupName="ReportType"
                                                    OnCheckedChanged="rdbGroup_CheckedChanged" AutoPostBack="True" />
                                                </div>
                                            </div>

                                            <div class="form-group row" id="trFAGroup" runat="server" visible="false">
                                                <div class="col-md-2">
                                                    <label>Select Final Account Group : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="ddlFAGroup" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Selected="True" Text="--Please Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFAGroup" runat="server" ErrorMessage="Please Select Final Account Group"
                                                        ControlToValidate="ddlFAGroup" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>

                                            <div id="trFromDate" runat="server" class="form-group row">

                                                <div class="col-md-2">
                                                    <label>From Date : </label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" Style="text-align: right" CssClass="form-control"
                                                            AutoPostBack="True" OnTextChanged="txtFrmDate_TextChanged" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>

                                                <input id="hdnBal" runat="server" type="hidden" />
                                                <input id="hdnMode" runat="server" type="hidden" />
                                            </div>

                                            <div id="trToDate" runat="server" class="form-group row">
                                                <div class="col-md-2">
                                                    <label>To Date : </label>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server"
                                                            AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="trReportType" runat="server" class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Select Report Type : </label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:RadioButton ID="rdbDetail" runat="server" Text="Detailed Trial Balance" Checked="true"
                                                        AutoPostBack="true" OnCheckedChanged="rdbDetail_CheckedChanged" GroupName="Report" />&nbsp;
                                                <asp:RadioButton ID="rdbShortTrailBalanec" runat="server" Text="Summary Trial Balance"
                                                    Checked="false" AutoPostBack="true" OnCheckedChanged="rdbShortTrailBalanec_CheckedChanged"
                                                    GroupName="Report" />&nbsp;
                                                <asp:RadioButton ID="rdbConsolidateTrialBalance" runat="server" Text="Consolidate Trial Balance"
                                                    GroupName="Report" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnShowTrialBalance" runat="server" Text="Show Reports"
                                                        OnClick="btnShowTrialBalance_Click" Width="110px" ValidationGroup="Report"
                                                        CssClass="btn btn-success" />
                                                </div>
                                            </div>

                                        </div>
                                        
                                        <div style="font-size: medium">
                                            <asp:ValidationSummary ID="vsTrialBalanceReport" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="Report" />
                                        </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>
