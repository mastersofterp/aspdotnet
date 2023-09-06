<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceSheetConfiguration_Pre.aspx.cs" Inherits="BalanceSheetConfiguration_Pre"
    Title="" %>

<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>

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

    <script type="text/javascript">
        function CloseUpdatePanel() {
            document.getElementById('Row1').style.display = 'none';
            //            panelctrl.style.visibility = 'hidden';
            //            panelctrl.style.display = 'none';
            // return false;
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
                            <h3 class="box-title">BALANCE SHEET CONFIGURATION</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                                <asp:Panel ID="upd" runat="server">
                                    <div class="panel panel-info">
                                      <%--  <div class="panel-heading">Balance Sheet Configuration</div>--%>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-6">
                                                    <div class="box box-primary">
                                                        <div class="box-header with-border">
                                                            <h4 class="box-title">Liability</h4>
                                                        </div>
                                                        <div class="box-body">
                                                            <asp:Panel ID="pnlgrd" Height="100%" runat="server">
                                                                <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-bordered table-hover"
                                                                    OnPageIndexChanging="rptSchDef_PageIndexChanging"
                                                                    GridLines="None" OnRowCommand="rptSchDef_RowCommand">
                                                                    <FooterStyle ForeColor="Black" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Groups" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblparty" CssClass="label-default" BorderStyle="None" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="bg-light-blue" ForeColor="#000" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="cl_balance" HeaderText="Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemStyle BorderStyle="None" ForeColor="Blue" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Position" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_position" runat="server" CssClass="form-control text-center" AutoPostBack="True" OnTextChanged="txt_position_TextChanged"></asp:TextBox>
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
                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                                    <AlternatingRowStyle Wrap="True" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="box box-primary">
                                                        <div class="box-header with-border">
                                                            <h4 class="box-title">Asset</h4>
                                                        </div>
                                                        <div class="box-body">
                                                            <asp:Panel ID="pnlgrd1" Height="95%" runat="server">
                                                                <asp:GridView ID="rptSchDef1" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-bordered table-hover"
                                                                    OnPageIndexChanging="rptSchDef1_PageIndexChanging" CellSpacing="1"
                                                                    GridLines="None" OnRowCommand="rptSchDef1_RowCommand">
                                                                    <FooterStyle ForeColor="Black" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Groups" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblparty" CssClass="label-default" BorderStyle="None" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle CssClass="bg-light-blue" ForeColor="#000" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="cl_balance" HeaderText="Balance" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemStyle BorderStyle="None" ForeColor="Blue" HorizontalAlign="Right" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Position" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txt_position" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txt_position_TextChanged1"></asp:TextBox>
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
                                                                    <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                                                    <AlternatingRowStyle Wrap="True" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSet" runat="server" Text="Set Balance Sheet" CssClass="btn btn-primary" OnClick="btnSet_Click" Style="display:none"/>
                                                  <asp:Button ID="btnCancel" runat="server" Text="Refresh" CssClass="btn btn-warning" OnClick="btnCancel_Click" Visible="false" />
                                                   <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="btnshow_Click" />
                                                   <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" OnClick="btnBack_Click" Text="Back" />
                                                    <asp:Button ID="btnShowConsolidateBalance" runat="server" CssClass="btn btn-primary" OnClick="btnShowConsolidateBalance_Click"
                                                        Text="Consolidate Balance Sheet" />
                                                   <asp:TextBox ID="txtschedule" Visible="false" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox><ajaxToolKit:FilteredTextBoxExtender
                                                        ID="ftcSchedule" runat="server" TargetControlID="txtschedule" FilterType="Numbers">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                   <asp:Button ID="btnSchedule" runat="server" CssClass="btn btn-primary" OnClick="btnSchedule_Click" Text="Configure"
                                                        ToolTip="Press to Configure Balance Sheet" Style="display:none" />
                                                  <asp:Button ID="btnExport" runat="server" CssClass="btn btn-primary" Text="Export to Excel" OnClick="btnExport_Click" />
                                                    <%--<asp:Button ID="btnShowGrid" runat="server" Text="Show Details" Width="102px" OnClick="btnShowGrid_Click" />&nbsp;&nbsp;&nbsp;--%>
                                                </div>
                                          
                                            <div class="form-group row" id="Row1" runat="server">
                                                <div id="TD1" runat="server">
                                                    <ajaxToolKit:ModalPopupExtender ID="updp_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                        DropShadow="True" PopupControlID="Panel1" TargetControlID="btnForPopUpModel2"
                                                        DynamicServicePath="" Enabled="True">
                                                    </ajaxToolKit:ModalPopupExtender>
                                                    <asp:Panel ID="Panel1" Height="400px" runat="server">
                                                        <div class="container">
                                                            <div class="row">
                                                                <asp:Label ID="lblHead" runat="server" Font-Bold="true" Text="Select Heads to Compress the Ledgers"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Note: Enter 1 to Compress Ledgers of respective Head."></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <asp:Panel ID="pnlGrid" Height="400px" runat="server">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                                                        BorderStyle="Ridge" BorderWidth="2px" Width="500px" CellPadding="3" Height="70px"
                                                                        BackColor="White" CellSpacing="1" GridLines="None">
                                                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Head Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                                                HeaderStyle-ForeColor="#000">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                                                        Width="250px"></asp:Label>
                                                                                    <asp:HiddenField ID="hdnSchIdEx" runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Enter 0/1">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txt_position1" runat="server" Width="50px" MaxLength="1" DataType="NumberType"
                                                                                        ToolTip="Enter 1 to Compress Ledgers" onblur="return add(this,'<%= txt_position1.ClientID%>');"></asp:TextBox>
                                                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position1"
                                                                                        ValidChars="01" FilterMode="ValidChars">
                                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#000" />
                                                                        <AlternatingRowStyle Wrap="True" />
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </div>
                                                            
                                                                <div class="col-12 btn-footer">
                                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Validation"
                                                                        CssClass="btn btn-primary" meta:resourcekey="btnSaveResource1" OnClick="btnSave_Click" />
                                                                  <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" Text="Close" ValidationGroup="Validation"
                                                                        OnClientClick="CloseUpdatePanel();"  meta:resourcekey="btnBackResource1" />
                                                                 <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary" Text="Reset" ValidationGroup="Validation"
                                                                        meta:resourcekey="btnBackResource1" OnClick="btnReset_Click" />
                                                                </div>
                                                           
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="trPanel" runat="server" visible="false">
                                                <div class="panel panel-primary">
                                                    <div class="panel-heading">Add/Modify - Ledgers</div>
                                                    <div class="panel-body">
                                                        <div class="form-group row">
                                                            <div class="col-md-6">
                                                                <div class="box box-primary">
                                                                    <div class="box-header with-border">
                                                                        <h3 class="box-title">Liability</h3>
                                                                    </div>
                                                                    <div class="box-body">
                                                                        <asp:Panel ID="pnlLiability" runat="server" Style="width: 98%; height: auto; text-align: left;"
                                                                            BorderColor="#0066FF">
                                                                            <asp:Repeater ID="RptData" runat="server">
                                                                                <HeaderTemplate>
                                                                                    <table class="datatable" width="100%">
                                                                                        <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                                                                            <th style="width: 7%;">&nbsp;
                                                                                            </th>
                                                                                            <th style="width: 33%; text-align: center">Particulars
                                                                                            </th>
                                                                                            <th style="text-align: right; width: 13.4%;">Closing Balance
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
                                                                                                ToolTip='<%# Eval("Party_name")%>' ImageUrl="~/images/action_down.gif" Visible="false" />
                                                                                        </td>
                                                                                        <td id="trPartyName" runat="server" style="font-weight: bold; width: 33%" align="left">
                                                                                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                                        </td>
                                                                                        <td style="font-weight: bold; width: 13.4%;" align="right">
                                                                                            <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                                                                            &nbsp;<%#Eval("clBalMode")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <table width="100%">
                                                                                        <tr class="header" style="height: 2Px">
                                                                                            <th style="width: 7%;">&nbsp;
                                                                                            </th>
                                                                                            <th style="width: 33%; text-align: left; font-weight: bold">
                                                                                                <%--Grand Total--%>
                                                                                            </th>
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
                                                            <div class="col-md-6">
                                                                <div class="box box-primary">
                                                                    <div class="box-header with-border">
                                                                        <h3 class="box-title">Assets</h3>
                                                                    </div>
                                                                    <div class="box-body">
                                                                        <asp:Panel ID="pnlAssets" runat="server" Style="width: 98%; height: auto; text-align: left;"
                                                                            BorderColor="#0066FF">
                                                                            <asp:Repeater ID="RptAssets" runat="server">
                                                                                <HeaderTemplate>
                                                                                    <table class="datatable" width="100%">
                                                                                        <tr class="header" style="background-color: ThreeDShadow; height: 2Px">
                                                                                            <th style="width: 7%;">&nbsp;
                                                                                            </th>
                                                                                            <th style="width: 33%; text-align: center">Particulars
                                                                                            </th>
                                                                                            <th style="text-align: right; width: 13.4%;">Closing Balance
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
                                                                                                ToolTip='<%# Eval("Party_name")%>' ImageUrl="~/images/action_down.gif" Visible="false" />
                                                                                        </td>
                                                                                        <td id="trPartyName" runat="server" style="font-weight: bold; width: 33%" align="left">
                                                                                            <asp:Label ID="lblParty" runat="server" Text='<%#Eval("PARTYNAME")%>' Width="100%"></asp:Label>
                                                                                        </td>
                                                                                        <td style="font-weight: bold; width: 13.4%;" align="right">
                                                                                            <asp:Label ID="lblClBalance" runat="server" Text='<%#Eval("CL_BALANCE1")%>'> </asp:Label>
                                                                                            &nbsp;<%#Eval("clBalMode")%>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <table width="100%">
                                                                                        <tr class="header" style="height: 2Px">
                                                                                            <th style="width: 7%;">&nbsp;
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
                                                        <div class="form-group row">
                                                            <p id="back-top">
                                                                <a href="#top"><span></span>Back to Top</a>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row" id="rowhelp" runat="server">
                                                <div id="Td35" runat="server">
                                                    <input id="hdnIdEditParty" runat="server" type="hidden" />
                                                </div>
                                                <div id="Td36" runat="server">
                                                    <asp:Button ID="btnForPopUpModel" Style="display: none" CssClass="btn btn-primary" runat="server" Text="For PopUp Model Box" />
                                                    <asp:Button ID="btnForPopUpModel2" Style="display: none" CssClass="btn btn-primary" runat="server" Text="For PopUp Model Box" />
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <asp:GridView ID="gvLiability" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="PARTYNAME" HeaderText="Party Name" HtmlEncode="false"
                                                            ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="50%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="50%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PARTYBAL" HeaderText="Party Balance" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FINALBAL"  HeaderText="Final Balance" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:GridView>
                                            </div>
                                            <div class="form-group row">
                                                <asp:GridView ID="gvAssets" runat="server" Width="100%" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:BoundField DataField="ASSETPARTYNAME" HeaderText="Party Name" HtmlEncode="false"
                                                            ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="50%" Font-Size="Smaller" />
                                                            <ItemStyle Wrap="False" Width="50%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PARTYBAL" HeaderText="Party Balance" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FINALBAL" HeaderText="Final Balance" ControlStyle-Font-Size="Smaller">
                                                            <HeaderStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                            <ItemStyle HorizontalAlign="Left" Width="25%" Font-Size="Smaller" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:GridView>
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
