<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IncomeExpenditureConfiguration_Pre.aspx.cs" Inherits="IncomeExpenditureConfiguration_Pre"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <%--<script type="text/javascript">
        var value = document.getElementById('GridViewId_ColumnID_' + RowIndex).value;
    
    </script>--%>

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

        function ShowledgerReport(mgrpno, fromDate, Todate) {
            debugger;
            var popUrl = 'Acc_IncExpBalGrid.aspx?MainGrpNo=' + mgrpno + '&fromDate=' + fromDate + '&Todate=' + Todate;
            var name = 'popUp';
            var appearence = 'dependent=yes,menubar=no,resizable=1,' + 'status=no,toolbar=no,titlebar=no,' + 'left=50,top=35,width=1500px,height=650px,scrollbars=1';

            var openWindow = window.open(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>

    <%--  <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>--%>

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

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">INCOME EXPENDITURE REPORT CONFIGURATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                            </div>
                            <asp:Panel ID="upd" runat="server">
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Income Expenditure Report Configuration</h5>
                                    </div>
                                    <div class="row">
                                            <div class="col-md-6">
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h4 class="box-title">Expenditure</h4>
                                                    </div>
                                                    <div class="box-body">
                                                        <asp:Panel ID="pnlgrd" Height="100%" runat="server">
                                                            <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" BorderColor="#000"
                                                                BorderStyle="None" CssClass="table table-striped table-bordered table-hover"
                                                                OnPageIndexChanging="rptSchDef_PageIndexChanging" Width="95%"
                                                                GridLines="None" OnRowCommand="rptSchDef_RowCommand">
                                                                <FooterStyle ForeColor="Black" />
                                                                <RowStyle Wrap="True" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Groups" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblparty" CssClass="label-default" BorderStyle="None" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="cl_balance" HeaderText="Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemStyle BorderStyle="None" ForeColor="Blue" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Position" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_position" runat="server" CssClass="form-control text-center" AutoPostBack="True" OnTextChanged="txt_position_TextChanged"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hid_mgrpno" runat="server" />
                                                                            <asp:Button ID="btnGropRpt" runat="server" Text="Report" CssClass="btn btn-info text-center" CommandArgument='<%# Eval("MGRP_NO") %>'
                                                                                CommandName="GroupReport" />
                                                                            <asp:HiddenField ID="hid_prno" runat="server" />
                                                                            <asp:HiddenField ID="hid_partyno" runat="server" />
                                                                            <asp:HiddenField ID="hid_headno" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="#000" />
                                                                <HeaderStyle CssClass="bg-light-blue text-center" Font-Bold="True"  ForeColor="#000"   />
                                                                <AlternatingRowStyle Wrap="True" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h4 class="box-title">Income</h4>
                                                    </div>
                                                    <div class="box-body">
                                                        <asp:Panel ID="pnlgrd1" Height="95%" runat="server">
                                                            <asp:GridView ID="rptSchDef1" runat="server" AutoGenerateColumns="False"
                                                                BorderStyle="Ridge" BorderWidth="2px" Width="95%" CssClass="table table-striped table-bordered table-hover"
                                                                OnPageIndexChanging="rptSchDef1_PageIndexChanging"
                                                                GridLines="None" OnRowCommand="rptSchDef1_RowCommand">
                                                                <FooterStyle ForeColor="Black" />
                                                                <RowStyle Wrap="True" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Groups" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblparty" CssClass="label-default" BorderStyle="None" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="cl_balance" HeaderText="Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemStyle BorderStyle="None" ForeColor="Blue" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Position" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" Visible="false">
                                                                        <ItemTemplate>
                                                                            <div>
                                                                                <asp:TextBox ID="txt_position" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_position_TextChanged1"></asp:TextBox>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hid_mgrpno" runat="server" />
                                                                            <asp:Button ID="btnGropRpt" runat="server" Text="Report" CssClass="btn btn-info" CommandArgument='<%# Eval("MGRP_NO") %>'
                                                                                CommandName="GroupReport" />
                                                                            <asp:HiddenField ID="hid_prno" runat="server" />
                                                                            <asp:HiddenField ID="hid_partyno" runat="server" />
                                                                            <asp:HiddenField ID="hid_headno" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle CssClass="bg-light-blue  text-center" Font-Bold="True" ForeColor="#000" />
                                                                <AlternatingRowStyle Wrap="True" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                       
                                        <div class="col-12 mt-3 btn-footer">
                                            <asp:Button ID="btnSet" runat="server" Text="Set Report" CssClass="btn btn-info" OnClick="btnSet_Click"  Visible="false"/>
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Refresh" OnClick="btnCancel_Click" Visible="false" />
                                            <asp:Button ID="btnshow" runat="server" CssClass="btn btn-primary" Text="Show" OnClick="btnshow_Click" />
                                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" OnClick="btnBack_Click" Text="Back" />
                                            <asp:Button ID="btnShowConsolidateIncome" CssClass="btn btn-primary" runat="server" OnClick="btnShowConsolidateIncome_Click"
                                                Text="Consolidate Income Expense" />
                                            <asp:TextBox ID="txtschedule" runat="server" CssClass="form-control" Width="67px" MaxLength="3" Visible="false"></asp:TextBox><ajaxToolKit:FilteredTextBoxExtender
                                                ID="ftcSchedule" runat="server" TargetControlID="txtschedule" FilterType="Numbers">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:Button ID="btnSchedule" runat="server" OnClick="btnSchedule_Click" Visible="false"
                                                Text="Show Schedule" />
                                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" OnClick="btnExport_Click" />
                                            <%--<asp:Button ID="btnShowGrid" runat="server" Text="Show Details" Width="102px" OnClick="btnShowGrid_Click" />--%>
                                        </div>
                                        <div class="form-group row">
                                            <asp:GridView ID="gvExpense" runat="server" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="PARTYNAME" HeaderText="Party Name" HtmlEncode="false"
                                                        ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="50%" Font-Size="Smaller" />
                                                        <ItemStyle Wrap="False" Width="50%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PARTYBALANCE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FINALBALANCE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </asp:GridView>
                                        </div>
                                        <div class="form-group row">
                                            <asp:GridView ID="gvIncome" runat="server" Width="100%" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="INCOMEPARTY" HeaderText="Party Name" HtmlEncode="false"
                                                        ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="50%" Font-Size="Smaller" />
                                                        <ItemStyle Wrap="False" Width="50%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PARTYBALANCE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FINALBALANCE" ControlStyle-Font-Size="Smaller">
                                                        <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </asp:GridView>
                                        </div>
                                        <div class="row">
                                            <asp:Table ID="tblIncExp" runat="server">
                                            </asp:Table>
                                        </div>
                                        <div class="form-group row" id="trPanel" runat="server" visible="false">
                                            <div class="col-md-6">
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h4 class="box-title">Expenditure</h4>
                                                    </div>
                                                    <div class="box-body">
                                                        <asp:Panel ID="pnlExpense" runat="server">
                                                            <asp:Repeater ID="RptExpense" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="datatable" width="100%">
                                                                        <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                                                            <th>
                                                                            </th>
                                                                            <th>Particulars
                                                                            </th>
                                                                            <th>Closing Balance
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                    <table width="100%" style="height: 90%">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr >
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                                ToolTip='<%# Eval("Party_name")%>' ImageUrl="~/Images/action_down.png" Visible="false" />
                                                                        </td>
                                                                        <td id="trPartyName" runat="server" style="font-weight: bold; width: 33%" align="left">
                                                                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                                                           <%#Eval("clBalMode")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <table>
                                                                        <tr>
                                                                            <th>
                                                                            </th>
                                                                            <th></th>
                                                                            <th>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblTotalDebit"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblTotalCredit"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                            </th>
                                                                        </tr>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="box box-primary">
                                                    <div class="box-header with-border">
                                                        <h4 class="box-title">Income</h4>
                                                    </div>
                                                    <div class="box-body">
                                                        <asp:Panel ID="PnlIncome" runat="server">
                                                            <asp:Repeater ID="RptIncome" runat="server">
                                                                <HeaderTemplate>
                                                                    <table class="datatable" width="100%">
                                                                        <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                                                            <th>
                                                                            </th>
                                                                            <th>Particulars
                                                                            </th>
                                                                            <th>Closing Balance
                                                                            </th>
                                                                        </tr>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </table>
                                                                    <table width="100%" style="height: 90%">
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr style="background-color: ThreeDFace; height: 2Px">
                                                                        <td style="width: 7%">
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                                ToolTip='<%# Eval("Party_name")%>' ImageUrl="~/Images/action_down.png" Visible="false" />
                                                                        </td>
                                                                        <td id="trPartyName" runat="server">
                                                                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                                                            &nbsp;<%#Eval("clBalMode")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <th>
                                                                            </th>
                                                                            <th></th>
                                                                            <th>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblTotalDebit"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label runat="server" ID="lblTotalCredit"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                            </th>
                                                                        </tr>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <p id="P1">
                                                <a href="#top"><span></span>Back to Top</a>
                                            </p>
                                        </div>
                                        <div class="form-group row" id="rowhelp" runat="server">
                                            <div id="Td35" runat="server" class="col-md-2">
                                                <input id="hdnIdEditParty" runat="server" type="hidden" />
                                            </div>
                                            <div id="Td36" runat="server" class="col-md-2">
                                                <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                            </div>
                                        </div>
                                        <div class="form-group row" id="Row1" runat="server">
                                            <td id="TD1" runat="server"></td>
                                        </div>
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
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
