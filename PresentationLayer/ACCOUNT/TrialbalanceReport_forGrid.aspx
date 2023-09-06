<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TrialbalanceReport_forGrid.aspx.cs" Inherits="ACCOUNT_TrialbalanceReport_forGrid"
    Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
        #ctl00_ContentPlaceHolder1_pnlgrd{
            background-color:#fff!importnat;
        }
    </style>

    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>

    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js"></script>--%>

    <script>
        $(document).ready(function () {

            // hide #back-top first
            $("#back-top").hide();

            // fade in #back-top
            $(function () {
                $(window).scroll(function () {
                    debugger;
                    if ($(this).scrollTop() < 600) {
                        $('#back-top').fadeOut();
                    } else {
                        $('#back-top').fadeIn();
                    }
                });

                // scroll body to 0px on click
                $('#back-top a').click(function () {
                    $('body,html').animate({
                        scrollTop: 0
                    }, 800);
                    return false;
                });
            });

        });
    </script>

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
            debugger;
            var popUrl = 'Acc_ledgerReportGrid.aspx?ledger=' + ledger + '&party_no=' + party_no + '&fromDate=' + fromDate + '&Todate=' + Todate;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=1,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px,scrollbars=1';
            //          var appearence = 'center:yes; dialogWidth:1150px; dialogHeight:900px; edge:raised; ' +
            //'help:no; resizable:no; scrollbars=1; status:no;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }

        function ShowLedger() {

            var popUrl = 'AccountingVouchers.aspx?obj=' + 'AccountingVouchers';
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;

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
        debugger
        function CloseUpdatePanel() {
            document.getElementById('Row1').style.display = 'none';
            //            panelctrl.style.visibility = 'hidden';
            //            panelctrl.style.display = 'none';
            // return false;
        }

    </script>
    <style>
        .table-bordered > thead > tr > th {
            border-top: 1px solid #ccc;
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
                        <p >Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel runat="server" ID="UPDLedger">
        <ContentTemplate>
            <div class="row" id="upd" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">TRIAL BALANCE REPORT</h3>
                        </div>
                        <!--==== Page Main Body =====-->
                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Report Type</label>
                                                </div>
                                                <asp:RadioButton runat="server" ID="rdbGeneral" Text="General Trial Balance" GroupName="ReportType"
                                                    Checked="true" AutoPostBack="True" OnCheckedChanged="rdbGeneral_CheckedChanged" />&nbsp;
                                            <asp:RadioButton runat="server" ID="rdbGroup" Text="Group Consolidate Trial Balance"
                                                GroupName="ReportType" OnCheckedChanged="rdbGroup_CheckedChanged" AutoPostBack="True" />
                                                <asp:RadioButton runat="server" ID="rdbDetailedGroup" Text="Group Detailed Trial Balance"
                                                    GroupName="ReportType" OnCheckedChanged="rdbDetailedGroup_CheckedChanged" AutoPostBack="True" />
                                                <asp:RadioButton runat="server" ID="rdbHeadDeatils" Text="Head Detailed Trial Balance"
                                                    GroupName="ReportType" AutoPostBack="True" OnCheckedChanged="rdbHeadDeatils_CheckedChanged" Visible="false" /><br />
                                                <asp:RadioButton runat="server" ID="rdbGroupwise" Text="Group Wise"
                                                    GroupName="ReportType" AutoPostBack="True" OnCheckedChanged="rdbGroupwise_CheckedChanged" Visible="false" />

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trFAGroup" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Final Account Group</label>
                                                </div>
                                                <asp:DropDownList ID="ddlFAGroup" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Text="--Please Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFAGroup" runat="server" ErrorMessage="Please Select Final Account Group"
                                                    ControlToValidate="ddlFAGroup" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divLevel" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Select Level</label>
                                                </div>
                                                <asp:DropDownList ID="ddllevel" CssClass="form-control" runat="server" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Text="--Please Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select level"
                                                    ControlToValidate="ddllevel" Display="None" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12" id="trDate" runat="server">
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

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Upto Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon" id="imgCal1">
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtUptoDate" runat="server" CssClass="form-control"
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
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:RadioButton ID="rdbWithZero" runat="server" Text="With Zero Closing Balance"
                                                    Checked="true" GroupName="Config" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbWithoutZero" runat="server" Text="Without Zero Closing Balance"
                                                Checked="false" GroupName="Config" />
                                            </div>
                                            <div class="form-group col-lg-7 col-md-6 col-12" id="trReportType" runat="server">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label> Report Type</label>
                                                </div>
                                                <asp:RadioButton ID="rdbDetail" runat="server" Text="Detailed Trial Balance" Checked="true"
                                                    AutoPostBack="true" OnCheckedChanged="rdbDetail_CheckedChanged" GroupName="Report" />&nbsp;
                                            <asp:RadioButton ID="rdbShortTrailBalanec" runat="server" Text="Summary Trial Balance"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="rdbShortTrailBalanec_CheckedChanged"
                                                GroupName="Report"  Visible="false"/>&nbsp;
                                            <asp:RadioButton ID="rdbConsolidateTrialBalance" runat="server" Text="Consolidate Trial Balance"
                                                GroupName="Report" Visible="false" />&nbsp;
                                            <asp:RadioButton ID="rdbSetConfiguration" runat="server" Text="Set position" OnCheckedChanged="rdbSetConfiguration_CheckedChanged"
                                                GroupName="Report" AutoPostBack="true" Visible="false" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divonlyledger" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:CheckBox ID="rdbOnlyLedger" runat="server" Text="Only Ledgers" Checked="false" />

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show In Grid" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                        <asp:Button ID="btnShowTrialBalance" runat="server" Text="Show Reports" OnClick="btnShowTrialBalance_Click"
                                            CssClass="btn btn-info" />
                                        <asp:Button ID="btnExcel" runat="server" Text="Export in Excel" OnClick="btnExcel_Click"
                                            CssClass="btn btn-info" />
                                         <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click1"
                                            CssClass="btn btn-info" />
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="trPanel" runat="server" Visible="false">

                                    <div class="">
                                        <div class="col-12 btn-footer " runat="server" id="trLinkNewVoucher" visible="false">
                                             <asp:Button ID="lnkNewVoucher" runat="server" Text="Create New Voucher" OnClick="lnkNewVoucher_Click1"
                                            CssClass="btn btn-primary" />
                                            <%--<asp:LinkButton ID="lnkNewVoucher" Text="Create New Voucher" runat="server" CssClass="btn btn-primary" PostBackUrl="~/ACCOUNT/AccountingVouchers.aspx" OnClick="lnkNewVoucher_Click"></asp:LinkButton>--%>       

                                        </div>


                                        <asp:Panel ID="pnl" runat="server">
                                            <div class="col-12">
                                                <div class="table table-responsive">
                                                    <div class="sub-heading">
                                                        <h5>Add/Modify - Ledgers</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="divsessionlist">
                                                        <asp:Repeater ID="RptData" runat="server">
                                                            <HeaderTemplate>
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>&nbsp;
                                                                        </th>
                                                                        <th>Particulars
                                                                        </th>
                                                                        <th>Opening Balance
                                                                        </th>
                                                                        <th>Debit
                                                                        </th>
                                                                        <th>Credit
                                                                        </th>
                                                                        <th>Closing Balance
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                            ToolTip='<%# Eval("Party_name")%>' ImageUrl="~/Images/action_down.png" Visible="false" />
                                                                    </td>
                                                                    <td id="trPartyName" runat="server" style="font-weight: bold; width: 33%" align="left">
                                                                        <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblOpbal" runat="server" Text='<%#Eval("OP_BALANCE1")%>'> </asp:Label>
                                                                        &nbsp;<%#Eval("OpbalMode")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDebit" runat="server" Text='<%#Eval("DEBIT")%>'> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCredit" runat="server" Text='<%#Eval("CREDIT")%>'> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                                                        &nbsp;<%#Eval("clBalMode")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>

                                                                <tr>
                                                                    <th>&nbsp;
                                                                    </th>
                                                                    <th>Grand Total
                                                                    </th>
                                                                    <th>&nbsp;
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblTotalDebit"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label runat="server" ID="lblTotalCredit"></asp:Label>
                                                                    </th>
                                                                    <th>&nbsp;
                                                                    </th>
                                                                </tr>

                                                            </FooterTemplate>


                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <div class="form-group row d-none">
                                            <p id="P1">
                                                <a href="#top"><span></span>Back to Top</a>
                                            </p>
                                        </div>
                                    </div>

                                    <div id="back-top" class="d-none">
                                        <a href="#top"><span></span>Back to Top</a>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnl1" runat="server">
                                    <div class="form-group row" id="rowhelp" runat="server">
                                        <div class="col-md-2" id="Td35" runat="server">
                                            <input id="hdnIdEditParty" runat="server" type="hidden" />
                                        </div>
                                        <div class="col-md-10" id="Td36" runat="server">
                                            <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                            <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                            <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="True" PopupControlID="pnlgrd" TargetControlID="btnForPopUpModel"
                                                Enabled="True">
                                            </ajaxToolKit:ModalPopupExtender>
                                        </div>
                                    </div>
                                    <div id="Row1" runat="server">
                                        <div id="TD1" runat="server"></div>
                                    </div>
                                </asp:Panel>
                               
                                <asp:Panel ID="pnlgrd" runat="server">
                                   
                                        <div class="col-12 card " style="background-color:#fff!important;">
                                            <div class="table table-responsive mt-3"  style="height:400px; overflow:scroll;">
                                                <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" CellPadding="3" CellSpacing="2"  >
                                                    <FooterStyle BackColor="#fff"  CssClass="table table-bordered table-hover" />
                                                    <RowStyle Wrap="True" ForeColor="#fff" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Head Name"  HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblparty"  runat="server"
                                                                   CssClass="form-control" Enabled="True" ReadOnly="True"></asp:TextBox>
                                                                <asp:HiddenField ID="hdnSchIdEx" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="white" ForeColor="#000" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Position">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txt_position" runat="server"  CssClass="form-control"  onblur="return add(this,'<%= txt_position.ClientID%>');"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position"
                                                                    ValidChars="0123456789" FilterMode="ValidChars">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <SelectedRowStyle  ForeColor="#fff" />
                                                  <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" />
                                                    <AlternatingRowStyle Wrap="True" />
                                                </asp:GridView>

                                            </div>
                                      

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="Validation"
                                                OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                                            <asp:Button ID="btnBack" runat="server" Text="Close" ValidationGroup="Validation"
                                                OnClientClick="CloseUpdatePanel();" meta:resourcekey="btnBackResource1" CssClass="btn btn-warning" />
                                        </div>

                                    </div>
                                </asp:Panel>
                                    </div>
                                <asp:Panel ID="pnlgrid1" runat="server">
                                    <div class="col-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvTrialBalance" runat="server" CellPadding="4" ForeColor="Black"
                                                GridLines="Vertical" CssClass="table table-striped table-bordered nowrap" AutoGenerateColumns="False" Width="100%" BorderStyle="None" BorderWidth="1px">
                                                <RowStyle BackColor="#F7F7DE" />
                                                <Columns>
                                                    <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTY NAME">
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        <ItemStyle Wrap="False" Width="20%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="OPENING_BALANCE" HeaderText="OPENING BALANCE">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CrDr" HeaderText="CrDr">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="DEBIT">
                                                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                        <ItemStyle Wrap="True" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREDIT" HeaderText="CREDIT">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CLOSING_BALANCE" HeaderText="CLOSING BALANCE">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CCrDr" HeaderText="CrDr">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnIsParty" runat="server" Value='<%# Eval("IS_PARTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gv" runat="server" CellPadding="4" ForeColor="Black"
                                                GridLines="Vertical" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover">
                                                <RowStyle BackColor="#F7F7DE" />
                                                <Columns>
                                                    <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTY NAME">
                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                        <ItemStyle Wrap="False" Width="20%" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="OPENING_BALANCE" HeaderText="OPENING BALANCE">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CrDr" HeaderText="CrDr">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DEBIT" HeaderText="DEBIT">
                                                        <HeaderStyle HorizontalAlign="Left" Width="50%" />
                                                        <ItemStyle Wrap="True" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CREDIT" HeaderText="CREDIT">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CLOSING_BALANCE" HeaderText="CLOSING BALANCE">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CCrDr" HeaderText="CrDr">
                                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnIsParty" runat="server" Value='<%# Eval("IS_PARTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <HeaderStyle CssClass="bg-light-blue" ForeColor="#000" />
                                                <AlternatingRowStyle BackColor="White" />
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
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
