<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceSheetReport.aspx.cs" Inherits="BalanceSheetReport" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
  <%--  <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

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
    </script>

   <%-- <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

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
                            <h3 class="box-title">BALANCE SHEET REPORT</h3>
                        </div>
                        <!--==== Page Main Body =====-->
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="dvRbnRow" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButton runat="server" ID="rdbDefaultGroup" Text="Print With Deafult Heads"
                                                    GroupName="Print" Checked="true" />
                                                <asp:RadioButton runat="server" ID="rdbDefinedGroup" Text="Print With Created Heads"
                                                    GroupName="Print" />
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="imgCal">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control"
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
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Upto Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="imgCal1">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtUptoDate" CssClass="form-control" runat="server"
                                                        AutoPostBack="True" OnTextChanged="txtUptoDate_TextChanged" />
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
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowBalanceSheet" runat="server" Text="Show Balance Sheet" OnClick="btnShowBalanceSheet_Click"
                                            CssClass="btn btn-primary" />
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
