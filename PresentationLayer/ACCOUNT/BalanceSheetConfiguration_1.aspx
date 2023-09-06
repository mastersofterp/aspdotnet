<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BalanceSheetConfiguration_1.aspx.cs" Inherits="BalanceSheetConfiguration" Title="" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <fieldset class="fieldset">
                    <legend class="legend">Balance Sheet Configuration</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td>
                                <b>Liability </b>
                                <asp:Panel ID="pnlgrd" Height="400px" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="500px" CellPadding="3" Height="180px"
                                        OnPageIndexChanging="rptSchDef_PageIndexChanging" BackColor="White" CellSpacing="1"
                                        GridLines="None">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Groups" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="350px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Balance">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="300px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txt_position_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sch">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_sch" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txt_sch_TextChanged"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_mgrpno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_prno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_partyno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_headno" runat="server" />
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
                                        GridLines="None">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Groups" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="350px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="cl_balance" HeaderText="Balance">
                                                <ControlStyle BorderStyle="None" Width="50px" />
                                                <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                                <ItemStyle BorderStyle="None" ForeColor="Blue" Width="300px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Position">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_position" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txt_position_TextChanged1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sch">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_sch" runat="server" Width="50px" AutoPostBack="True" OnTextChanged="txt_sch_TextChanged1"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_mgrpno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_prno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_partyno" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hid_headno" runat="server" />
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
                            <td colspan="2">
                                <asp:Button ID="btnSet" runat="server" Text="Set Balance Sheet" OnClick="btnSet_Click" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Refresh" OnClick="btnCancel_Click" />
                                &nbsp;<asp:Button ID="btnshow" runat="server" Text="Show" OnClick="btnshow_Click" />
                                &nbsp;<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" />
                                &nbsp;Schedule No.<asp:TextBox ID="txtschedule" runat="server" Width="67px" MaxLength="3"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftcSchedule" runat="server" TargetControlID="txtschedule" FilterType="Numbers">
                            </ajaxToolKit:FilteredTextBoxExtender>
                                &nbsp;<asp:Button ID="btnSchedule" runat="server" OnClick="btnSchedule_Click" Text="Show Schedule" />
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
