<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DayBookReport.aspx.cs" Inherits="DayBookReport" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>

    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

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
                            <h3 class="box-title">DAY BOOK REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 ">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center"></div>
                                <asp:Panel ID="pnlDayBookReport" runat="server">
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Date Type</label>
                                                </div>
                                                <asp:RadioButton ID="radBetTwoDates" runat="server" Text="Between Two Dates" Checked="true"
                                                    AutoPostBack="true" OnCheckedChanged="radBetTwoDates_CheckedChanged" GroupName="Report" />&nbsp;
                                            <asp:RadioButton ID="radDay" runat="server" Text="Daywise" Checked="false" AutoPostBack="true"
                                                OnCheckedChanged="radDay_CheckedChanged" GroupName="Report" />

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Report Type</label>
                                                </div>
                                                <asp:RadioButton ID="rdbWithNarration" runat="server" Text="With Narration" GroupName="Report1" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbWithoutNarration" runat="server" Text="Without Narration"
                                                Checked="true" GroupName="Report1" />
                                            </div>
                                            <div class="col-5" id="pnlBetTwoDates" runat="server">
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
                                                            <label>Upto Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="imgCal1">
                                                                <i class="fa fa-calendar text-blue"></i>
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
                                        </div>

                                           <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:CheckBox ID="chkdept" runat="server" Text="Department Wise Day Book" Checked="false" AutoPostBack="true"  OnCheckedChanged="chkdept_CheckedChanged"/>

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false" id="divDept">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Department</label>
                                                </div>
                                                  <asp:DropDownList ID="ddldepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                              OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                         
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer" id="trBtn" runat="server">
                                        <asp:Button ID="btndb" runat="server" Text="Day Book" CssClass="btn btn-primary" OnClick="btndb_Click" />
                                        <asp:Button ID="btnExportExcel" runat="server" Text="Export To Excel" CssClass="btn btn-primary"
                                            OnClick="btnExportExcel_Click" />
                                    </div>
                                    <div id="pnlDatewise" runat="server">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft" TargetControlID="txtDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" Display="None"
                                                            ErrorMessage="Please Enter Date" SetFocusOnError="true" ValidationGroup="Daywise"
                                                            Width="10%" ID="rfvFromDate" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer" id="trDatewise" runat="server">
                                            <asp:Button ID="btndbOther" runat="server" Text="Day Book" CssClass="btn btn-primary" OnClick="btndbOther_Click"
                                                ValidationGroup="Daywise" />
                                            <asp:Button ID="btnDaywiseExport" runat="server" Text="Export To Excel" CssClass="btn btn-primary"
                                                ValidationGroup="Daywise" OnClick="btnDaywiseExport_Click" />
                                             <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btncancel_Click"
                                                ValidationGroup="Daywise" />
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div id="divMsg" runat="server">
                                </div>
                                <div class="col-12">
                                    <asp:Panel ID="GridPanel" runat="server">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridExcel" runat="server" Width="100%"
                                                AutoGenerateColumns="False" CssClass="table table-striped table-bordered nowrap">
                                                <Columns>
                                                    <asp:BoundField DataField="TranDate" HeaderText="DATE" HtmlEncode="false" DataFormatString="{0:d}" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle Wrap="False" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Particulars" HeaderText="PARTICULARS" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="100%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="100%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="party_no" HeaderText="PARTYNO" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="2%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VchType" HeaderText="VOUCHER TYPE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VchNo" HeaderText="VOUCHER NO" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="DEBIT" HtmlEncode="false" DataFormatString="{0:n}" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREDIT" HeaderText="CREDIT" HtmlEncode="false" DataFormatString="{0:n}" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="20%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Narration" HeaderText="NARRATION" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="30%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="30%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Bold" HeaderText="BOLD" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TRAN" HeaderText="TRAN" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Right" Width="2%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDaywiseExport" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
