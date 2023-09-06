<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Acc_IncExpBalGrid.aspx.cs"
    Inherits="Acc_IncExpBalGrid" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Configuration For Voucher Print</title>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/tabs.css" rel="stylesheet" type="text/css" />
    <link href="Css/linkedin/blue/linkedin-blue.css" rel="stylesheet" type="text/css" />
    <link href="includes/modalbox.css" rel="stylesheet" type="text/css" />
    <link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="../dist/css/AdminLTE.css" rel="stylesheet" />

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
            //            var appearence = 'dependent=yes,menubar=no,resizable=no,' +
            //         'status=no,toolbar=no,titlebar=no,' +
            //         'left=50,top=35,width=900px,height=650px';
            var appearence = 'center:yes; dialogWidth:1150px; dialogHeight:900px; edge:raised; ' +
            'help:no; resizable:no; scroll:no; status:no;';
            var openWindow = window.open(popUrl, name, appearence);
            //var openWindow = window.showModalDialog(popUrl, name, appearence);
            openWindow.focus();
            return false;
        }
    </script>

</head>
<body class="body">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <%--<div style="z-index: 1; position: fixed; top: 40%; left: 40%; text-align: center;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDMainGroup"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
    </ProgressTemplate> </asp:UpdateProgress> </div>--%>
        <div style="z-index: 1; position: fixed; left: 600px;">
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updDiv"
                DynamicLayout="true" DisplayAfter="0">
                <ProgressTemplate>
                    <div style="width: 120px; padding-left: 5px">
                        <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                        <p class="text-success"><b>Loading..</b></p>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="updDiv" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Main Group Report</h3>
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                    </div>
                                    <asp:Panel ID="pnlIncExpReport" runat="server">
                                        <div class="panel panel-primary">
                                            <%--<div class="panel-heading">Main Group Report</div>--%>
                                            <div class="panel-body">
                                                <div class="container">
                                                  <%--  <div class="form-group row">
                                                        <h4><span style="color: Red">Press Backspace to get back</span></h4>
                                                    </div>--%>
                                                    <div class="form-group row">
                                                        <div class="col-md-8">
                                                            <b>Group Name&nbsp;:&nbsp;</b>
                                                       
                                                            <asp:Label ID="lblLedger" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <b>From&nbsp;:&nbsp;</b>
                                                            <asp:Label ID="lblFrm" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <b>To&nbsp;:&nbsp;</b>
                                                            <asp:Label ID="lblTo" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <div class="col-md-12">
                                                        <div class="row" style="border: solid 1px #CCCCCC">
                                                            <div style="font-weight: bold; background-color: #72A9D3; color: white" class="panel-heading">Report</div>
                                                            <div class="table-responsive">
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th style="width: 2%;">&nbsp;
                                                                            </th>
                                                                            <th style="width: 20%">Particulars
                                                                            </th>
                                                                            <th style="text-align: right; width: 4%">Closing Balance
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                </table>
                                                            </div>
                                                            <asp:Panel ID="pnl" runat="server" BorderColor="#0066FF">
                                                                <asp:Repeater ID="RptExpense" runat="server">
                                                                    <ItemTemplate>
                                                                        <table class="customers table-bordered" width="100%">
                                                                            <tr style="background-color: ThreeDFace; height: 2Px">
                                                                                <td style="width: 7%">
                                                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("PARTY_NO")%>'
                                                                                        ToolTip='<%# Eval("PARTYNAME")%>' ImageUrl="~/images/action_down.gif" Visible="false" />
                                                                                </td>
                                                                                <td id="trPartyName" runat="server" style="width: 60%" align="left">
                                                                                    <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                                </td>
                                                                                <td style="font-weight: bold; width: 16%;" align="right">
                                                                                    <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE")%>'> </asp:Label>
                                                                                    &nbsp;<%#Eval("clBalMode")%>
                                                                                </td>
                                                                            </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <table width="100%">
                                                                            <tr class="header" style="height: 2Px">
                                                                                <th style="width: 7%">&nbsp;
                                                                                </th>
                                                                                <th style="width: 33%; text-align: left; font-weight: bold"></th>
                                                                                <th style="text-align: right; width: 16.6%;">&nbsp;
                                                                                </th>
                                                                                <th style="text-align: right; width: 13.4%;">
                                                                                    <asp:Label runat="server" ID="lblTotalDebit"></asp:Label>
                                                                                </th>
                                                                                <th style="text-align: right; width: 13.4%;">
                                                                                    <asp:Label runat="server" ID="lblTotalCredit"></asp:Label>
                                                                                </th>
                                                                                <th style="text-align: right">&nbsp;
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
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
