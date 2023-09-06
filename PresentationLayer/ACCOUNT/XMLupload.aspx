<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="XMLupload.aspx.cs" Inherits="ACCOUNT_XMLupload" Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left: 150px;
        }
    </style>
    <%--<asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBank">
        <ProgressTemplate>
            <div id="IMGDIV" class="outerDivs" align="center" valign="middle" runat="server"
                style="position: absolute; left: 0%; top: 0%; visibility: visible; border-style: none;
                background-color: Transparent Black; z-index: 40; height: 100%; width: 100%;">
                <table style="width: 100%; height: 100%">
                    <tr>
                        <td valign="middle" align="center">
                            <img src="images/BarRotation.gif" style="height: 30px" /><br />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="99%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    XML data migration
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                <asp:UpdatePanel ID="updPnl" runat="server"></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px;">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    <%--<asp:UpdatePanel ID="updBank" runat="server">
                        <ContentTemplate>--%>
                    <fieldset class="vista-grid" style="width: 90%">
                        <legend class="titlebar">XML data migration</legend>
                        <table width="100%" cellpadding="3" cellspacing="0">
                            <tr>
                                <td style="text-align: left; width: 12%">
                                    XML file to upload :<span style="color: #FF0000; font-weight: bold">*</span>
                                </td>
                                <td style="width: 30%">
                                    <asp:FileUpload ID="fulUpload" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" ValidationGroup="submit"
                                        Text="Upload" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                    &nbsp;
                                    <asp:Button ID="btnMap" runat="server" ValidationGroup="submit" OnClick="btnMap_Click"
                                        Text="Map Ledger and Transfer" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Panel ID="ScrollPanel" Height="50%" runat="server" ScrollBars="Both">
                                        <asp:GridView ID="GridData" CssClass="datatable" runat="server" CellPadding="4" ForeColor="Black"
                                            GridLines="Vertical" AutoGenerateColumns="False" Width="100%" BackColor="White"
                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                                            onrowdatabound="GridData_RowDataBound">
                                            <RowStyle BackColor="#F7F7DE" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Tally Ledger Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPayHeadsNo" runat="server" Text='<%#Bind("LEDGERNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Head">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtLedgerHead" runat="server" MaxLength="30" placeholder="Enter Ledger Name"
                                                            Text='<%# Eval("PARTY_NAME") %>' ToolTip="Enter Ledger Name" AutoPostBack="False"
                                                            Width="95%"></asp:TextBox>
                                                        <ajaxToolKit:AutoCompleteExtender ID="autLedgerHead" runat="server" TargetControlID="txtLedgerHead"
                                                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="50" CompletionInterval="1000"
                                                            ServiceMethod="GetLedger">
                                                        </ajaxToolKit:AutoCompleteExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
