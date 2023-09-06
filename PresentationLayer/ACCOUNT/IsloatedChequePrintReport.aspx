<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IsloatedChequePrintReport.aspx.cs" Inherits="ACCOUNT_IsloatedChequePrintReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>
    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    ISOLATED CHEQUE PRINT
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
                    <tr>
                        <td>
                            <fieldset class="vista-grid" style="width: 75%;">
                                <legend class="titlebar">Isolated Cheque Print Report</legend>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                            <div runat="server" id="divCalender">
                                                <tr>
                                                    <td style="padding: 10px; width: 15%; text-align: left">
                                                        From Date
                                                    </td>
                                                    <td style="width: 1%">
                                                        <b>:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFrmDate" runat="server" Width="25%" Style="text-align: right"
                                                            AutoPostBack="True" />
                                                        &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        <AjaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                                        </AjaxToolKit:CalendarExtender>
                                                        <AjaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                                        </AjaxToolKit:MaskedEditExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp; To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" Width="25%"
                                                            AutoPostBack="True" />
                                                        &nbsp;<asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        <AjaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtUptoDate">
                                                        </AjaxToolKit:CalendarExtender>
                                                        <AjaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                                        </AjaxToolKit:MaskedEditExtender>
                                                        <input id="hdnBal" runat="server" type="hidden" />
                                                        <input id="hdnMode" runat="server" type="hidden" />
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%; text-align: left">
                                           Select Bank
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlbank" runat="server" Width="70%" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0" Selected="True"> Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvbank" runat="server" ErrorMessage="Please Select Bank."
                                                InitialValue="0" ControlToValidate="ddlbank"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 40%;">
                                        </td>
                                        <td style="width: 12px; text-align: center">
                                        </td>
                                        <td style="width: 237px;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 300px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px; width: 15%; text-align: left">
                                        </td>
                                        <td style="width: 1%">
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Button ID="btnReport" runat="server" Text="Report" Width="120px" OnClick="btnReport_Click" />
                                        </td>
                                        <td style="width: 10%; text-align: center">
                                        </td>
                                        <td style="width: 12px;">
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
        </fieldset> </td> </tr> </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
