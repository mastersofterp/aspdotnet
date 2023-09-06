<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AccBulkVoucherPrint.aspx.cs" Inherits="ACCOUNT_AccBulkVoucherPrint" %>


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

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
       
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
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">BULK VOUCHER PRINT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center"></div>
                                <asp:Panel ID="pnlDayBookReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Bulk Voucher Print</div>
                                        <div class="panel-body">                                           
                                           
                                            <div id="pnlBetTwoDates" runat="server">
                                                 <div class="row">
                                                <div class="col-md-2">
                                                    <label>Select Voucher Type :</label>
                                                </div>
                                                <div class="form-group col-md-8">
                                                    <asp:RadioButtonList ID="rdlVoucherType" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="1">Payment</asp:ListItem>
                                                        <asp:ListItem Value="2">Receipt</asp:ListItem>
                                                        <asp:ListItem Value="3" >Journal</asp:ListItem>
                                                        <asp:ListItem Value="4">Contra</asp:ListItem>
                                                    </asp:RadioButtonList>                                            
                                             
                                                </div>
                                            </div>
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <label>From Date :</label>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control" AutoPostBack="true"  OnTextChanged="txtFrmDate_TextChanged"/>
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
                                                            <asp:TextBox ID="txtUptoDate" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtUptoDate_TextChanged" />
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
                                            </div>
                                            <div class="form-group col-md-12" id="trBtn" runat="server">
                                                <div class="col-md-2"></div>
                                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" OnClick="btnReport_Click"/>
                                               
                                            </div>                                          
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="divMsg" runat="server">
                                </div>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
       <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnDaywiseExport" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
