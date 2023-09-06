<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FinancialYearEnd_Test.aspx.cs" Inherits="FinancialYearEnd_Test" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="2">
                    Split Data Yearwise
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                        font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                                <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                <%--  Enable the button so it can be played again --%>
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                        </div>
                    </div>

                    <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
                    </script>

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>

                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                            <%-- Flash the text/border red and fade in the "close" button --%>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                            </Parallel>
                            <Parallel AnimationTarget="info" Duration=".5">
                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                            </Parallel>
                        </Sequence>
                    </OnClick>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                    <OnClick>
                        <Sequence AnimationTarget="info">
                            <%--  Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                            <%--  Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%--  Enable the button so it can be played again --%>
                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                        </Sequence>
                    </OnClick>
                    <OnMouseOver>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                    </OnMouseOver>
                    <OnMouseOut>
                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                    </OnMouseOut>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px" colspan="2">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; text-align: center;">
                    <b>Current Financial Year </b>
                    <input id="hdnBal" runat="server" type="hidden" />
                </td>
                <td align="center">
                    <b style="text-align: center">Actual Financial Year </b>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; text-align: center;">
                    Start Date :
                    <asp:Label ID="lblFinYrStartDate" runat="server" Style="font-weight: 700; color: #FF0000;"></asp:Label>
                    &nbsp;&nbsp;&nbsp; End Date :
                    <asp:Label ID="lblFinYrEndDate" runat="server" Style="font-weight: 700; color: #FF0000;"></asp:Label>
                </td>
                <td align="center">
                    Start Date :
                    <asp:TextBox ID="txtActStartfinYear" Width="80px" runat="server" 
                        AutoPostBack="True" ontextchanged="txtActStartfinYear_TextChanged"></asp:TextBox>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    &nbsp;&nbsp;&nbsp; End Date :
                    <asp:Label ID="lblActFinYrEndDate" runat="server" Style="font-weight: 700; color: #FF0000;"></asp:Label>
                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtActStartfinYear">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtActStartfinYear">
                    </ajaxToolKit:MaskedEditExtender>
                    <asp:Button ID="btnGo" runat="server" Width="25px" Text="Go" OnClick="btnGo_Click" />
                </td>
            </tr>
            <tr align="left">
                <asp:UpdatePanel ID="UPDLedger" runat="server">
                    <ContentTemplate>
                        <td style="padding: 10px; text-align: right">
                            <asp:Panel ID="pnl" BorderWidth="1px" ScrollBars="Vertical" runat="server" Style="width: 100%;
                                height: 350Px; text-align: left" BorderColor="#0066FF">
                                Ledger Head Names ( Closing Balances )
                                <asp:TreeView ID="tvLinks" NodeStyle-ForeColor="Blue" NodeWrap="true" runat="server">
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                        <td style="padding: 10px; text-align: right">
                            <asp:Panel ID="pnl1" BorderWidth="1px" ScrollBars="Vertical" runat="server" Style="width: 100%;
                                height: 350Px; text-align: left" BorderColor="#0066FF">
                                Ledger Head Names ( Closing Balances )
                                <asp:TreeView ID="tvLinks1" NodeStyle-ForeColor="Blue" NodeWrap="true" runat="server">
                                </asp:TreeView>
                            </asp:Panel>
                        </td>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            
            <tr align="left">
                <td style="text-align: left">
                    Total Credit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotCurrCr"  runat="server" style="font-weight: 700;text-align:right"></asp:Label>
                </td>
                <td style="text-align: left">
                    Total Credit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotActCr"  runat="server" style="font-weight: 700;text-align:right"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="text-align: left">
                    Total Debit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotCurrDr"  runat="server" style="font-weight: 700;text-align:right"></asp:Label>
                </td>
                <td style="text-align: left">
                    Total Debit:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotActDr" runat="server" style="font-weight: 700 ;text-align:right"></asp:Label>
                
                </td>
            </tr>
            <tr align="left">
                <td style="text-align: left">
                    Total Closing Diff.:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotCurrDiff"  runat="server" style="font-weight: 700 ;text-align:right"></asp:Label>
                </td>
                <td style="text-align: left">
                    Total Closing Diff.:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="lbltotActDiff"  runat="server" style="font-weight: 700; text-align:right"></asp:Label>
                </td>
            </tr>
            <tr align="left">
                <td style="text-align: center" colspan="2">
                <asp:Button ID="btnSplit" runat="server" Width="80Px" Text="Split Data" 
                        onclick="btnSplit_Click" />
                <asp:Button ID="btnCancel" runat="server" Width="80Px" Text="Cancel" />
                <asp:Button ID="btnEndFin"  runat="server" Width="120px" Text="End Financial Year" />
                </td>
                
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
