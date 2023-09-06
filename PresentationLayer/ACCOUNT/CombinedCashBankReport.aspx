<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CombinedCashBankReport.aspx.cs" Inherits="Acc_combinedCashBank" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            text-align:center;
        }
    </style>
    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    COMBINED CASH BANK BOOK REPORT
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset class="vista-grid" style="width: 75%;">
                        <legend class="titlebar">Combined Cash & Bank Book Report</legend>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>
            <tr>
                    <td style="padding: 10px; width: 13%; text-align: left">
                    From Date   
                    </td>
                    <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                    <td>
                    <asp:TextBox ID="txtFrmDate" runat="server" Width="14%" 
                        Style="text-align: right" AutoPostBack="True" 
                            ontextchanged="txtFrmDate_TextChanged" />
                    &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                    </ajaxToolKit:MaskedEditExtender>
                    
                    &nbsp;&nbsp;&nbsp;&nbsp; To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" 
                        Width="14%" AutoPostBack="True" ReadOnly="True" />
                    &nbsp;<asp:Image ID="imgCal1" runat="server" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"/>
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
                        &nbsp;&nbsp;
                    </td>
                </tr>
           
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    Ledger Cash
                </td>
                <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                <td style="width: 70%;">
                    <asp:ListBox ID="lstCash" runat="server" Width="50%" ></asp:ListBox>
                    
                </td>
                <td style="width: 10%; text-align: center">
                    
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td class="style2">
                </td>
                <td style="width: 300px;">
                </td>
            </tr>
            
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                 </td>
                 <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                <td style="width: 40%;">
                </td>
                <td style="width: 10%; text-align: center">
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td class="style2">
                 </td>
                <td style="width: 300px;">
                </td>
            </tr>
            
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    Select Bank
                </td>
                <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                <td style="width: 70%;">
                    <asp:ListBox ID="lstBank" runat="server" Width="50%" SelectionMode="Multiple"></asp:ListBox>
                    
                </td>
                <td style="width: 10%; text-align: center">
                    
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td class="style2">
                </td>
                <td style="width: 300px;">
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                 </td>
                 <td style="width: 1%">
                                            
                                        </td>
                <td style="width: 40%;">
                </td>
                <td style="width: 10%; text-align: center">
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td class="style2">
                 </td>
                <td style="width: 300px;">
                </td>
            </tr>
            
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    
                </td>
                <td style="width: 1%">
                                            
                                        </td>
                <td style="width: 40%;">
                  <asp:Button ID="btnReport" runat="server" Text="Report" Width="120px" OnClick="btnReport_Click" />  
                </td>
                <td style="width: 10%; text-align: center">
                    
                </td>
                <td style="width: 237px;">
                    &nbsp;
                </td>
                <td class="style2">
                </td>
                <td style="width: 300px;">
                </td>
            </tr>
            
            <tr align="left">
                <td style="padding: 10px; text-align: right" colspan="6">
                    <asp:UpdatePanel ID="UPDLedger" runat="server">
                        <ContentTemplate>
                            
                            <%--<asp:ListBox ID="lstBank1" runat="server"></asp:ListBox>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </td>
            </tr>
            </table>
            </fieldset>
            </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

