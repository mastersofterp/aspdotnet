<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceSheetConfiguration.aspx.cs" Inherits="BalanceSheetConfiguration" Title="" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
<script language="javascript" type="text/javascript">

        function add(ctl, id1) {
            
            var value1 = ctl.value;
            
           var rptData = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef').rows.length;
           for (var i = 2; i <= rptData ; i++) {
             var Position = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0'+i+'_txt_position').value;
             if (Position == value1) {
                 //document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position').value = "";
                 
                 if (ctl.id != 'ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position') {
                     ctl.value = "";

//                     setTimeout(function() { ctl.focus(); }, 1);
                     alert('Position Must Be Different');
                     return false;
                 }
             
            }
          }
        }

        function Position(ctl, id1) {

            var value1 = ctl.value;

            var rptData = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef1').rows.length;
            for (var i = 2; i <= rptData; i++) {
                var Position = document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef1_ctl0' + i + '_txt_position').value;
                if (Position == value1) {
                    //document.getElementById('ctl00_ContentPlaceHolder1_rptSchDef_ctl0' + i + '_txt_position').value = "";
                    if (ctl.id != 'ctl00_ContentPlaceHolder1_rptSchDef1_ctl0' + i + '_txt_position') {
                        ctl.value = "";
                        alert('Position Must Be Different');
                        return false;
                    }
                    
                }
            }
        }
     
    </script>
    
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    BALANCE SHEET CONFIGURATION
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 10px">
        <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
                <fieldset class="vista-grid">
                                <legend class="titlebar">Balance Sheet Configuration</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td>
                                <b>Liability </b>
                                <asp:Panel ID="pnlgrd" Height="400px" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="90%" CellPadding="3" Height="180px"
                                        OnPageIndexChanging="rptSchDef_PageIndexChanging" BackColor="White" CellSpacing="1"
                                        GridLines="None" onrowcommand="rptSchDef_RowCommand">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Schedule Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="250px" AutoPostBack="False" Enabled="True" ReadOnly="True"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Amount">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="50px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" AutoPostBack="False" 
                                                    onblur="return add(this,'<%= txt_position.ClientID %>')"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position" 
                                                       ValidChars="0123456789" FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                <asp:Button ID="btnPrint" Text="Print" runat="server" 
                                                        CommandArgument='<%# Eval("Sch_ID") %>' CommandName="Print"/>
                                                    <asp:HiddenField ID="hdnSchIdEx" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnAmount" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <AlternatingRowStyle Wrap="True" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                            <td>
                                <b>Asset</b>
                                <asp:Panel ID="pnlgrd1" Height="400px" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="rptSchDef1" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="500px" CellPadding="3" Height="180px"
                                        OnPageIndexChanging="rptSchDef1_PageIndexChanging" BackColor="White" CellSpacing="1"
                                        GridLines="None" onrowcommand="rptSchDef1_RowCommand">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Schedule Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="250px" ReadOnly="True"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Amount">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="50px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" AutoPostBack="False"
                                                    onblur="return Position(this,'<%= txt_position.ClientID %>')"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_position" 
                                                       ValidChars="0123456789" FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                <asp:Button ID="btnPrint" Text="Print" runat="server" 
                                                        CommandArgument='<%# Eval("Sch_ID") %>' CommandName="Print"/>
                                                    <asp:HiddenField ID="hdnSchIdIncome" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnAmount" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                        <AlternatingRowStyle Wrap="True" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSet" runat="server" Text="Set Balance Sheet" OnClick="btnSet_Click" />
                                
                                &nbsp;<asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
                                &nbsp;<asp:Button ID="btnShowWithLedger" runat="server" Text="Show With Ledger" 
                                    onclick="btnShowWithLedger_Click" />
                                &nbsp;<asp:Button ID="btnExport" runat="server" Text="Export To Word" 
                                    onclick="btnExport_Click" Enabled="False" 
                                     />
                                
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
