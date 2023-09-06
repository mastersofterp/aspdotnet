<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Depreciation.aspx.cs" Inherits="Depreciation" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    Depreciation Calculation
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
                <fieldset class="vista-grid" style="width: 70%;">
                        <legend class="titlebar">Depreciation Calculation</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td>
                                <b>Fixed Assets</b>
                                <asp:Panel ID="pnlgrd1" ScrollBars="Vertical" runat="server">
                                    <asp:GridView ID="grdDepriciation" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                        BorderStyle="Ridge" BorderWidth="2px" Width="100%" CellPadding="3" Height="180px"
                                        BackColor="White" CellSpacing="1"
                                        GridLines="None">
                                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                        <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ledger Name" HeaderStyle-BackColor="#3399FF" HeaderStyle-HorizontalAlign="Left"
                                                HeaderStyle-ForeColor="White">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblparty" BackColor="Gainsboro" BorderStyle="None" runat="server"
                                                        Width="250px" ReadOnly="True" Text='<%# Eval("PARTY_NAME")%>'></asp:TextBox>
                                                        <asp:HiddenField ID="hdnPartyNo" runat="server" Value='<%# Eval("PARTY_NO")%>'/>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#3399FF" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_rate" runat="server" Width="50px" AutoPostBack="False"></asp:TextBox>%
                                                    <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbePosition" TargetControlID="txt_rate"
                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
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
                        <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" />
                        &nbsp;
                        <asp:Button ID="btnReport" runat="server" Text="Report" onclick="btnReport_Click" />
                        </td>
                        </tr>
                    </table>
                </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
