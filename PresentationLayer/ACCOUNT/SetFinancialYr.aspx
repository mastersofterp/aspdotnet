<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SetFinancialYr.aspx.cs" Inherits="Account_SetFinancialYr" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>
    <%--<script src="../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>--%>
    
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="90%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    SELECT COMPANY
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
                                <%--<asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                        </div>
                    </div>
                   <%--<asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>--%>
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

                        function onCalendarShown() {
                            var cal = $find("calendar1");
                            cal._switchMode("years", true);
                            if (cal._yearsBody) {
                                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                                    var row = cal._yearsBody.rows[i];
                                    for (var j = 0; j < row.cells.length; j++) {
                                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                                    }
                                }
                            }
                        }

                        function onCalendarHidden() {
                            var cal = $find("calendar1");
                            if (cal._yearsBody) {
                                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                                    var row = cal._yearsBody.rows[i];
                                    for (var j = 0; j < row.cells.length; j++) {
                                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                                    }
                                }
                            }
                        }

                        function call(eventElement) {
                            var target = eventElement.target;
                            switch (target.mode) {
                                case "year":
                                    var cal = $find("calendar1");
                                    cal.set_selectedDate(target.date);
                                    cal._blur.post(true);
                                    cal.raiseDateSelectionChanged(); break;
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
                <td style="width: 100%; text-align: center">
                    <div style="width: 100%; text-align: center">
                        <fieldset class="vista-grid" style="padding: 10px; width: 60%">
                            <legend class="titlebar">Company Selection</legend>
                            <%--<asp:ListBox ID="lstCompany" runat="server" Rows="20" Width="100%" SelectionMode="Single">
                            </asp:ListBox>--%>
                            <table width="90%">
                                <tr>
                                    <td colspan="2" style="width: 50%">
                                        FromDate:
                                        <asp:TextBox ID="txtFromDate" runat="server" ToolTip="Please Enter Account Name"
                                            Width="40%" Visible="true" ontextchanged="txtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:Image ID="Image1" runat="server"
                                                ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="yyyy" PopupButtonID="Image1" TargetControlID="txtFromDate" BehaviorID="calendar1">
                                        </ajaxToolKit:CalendarExtender>
                                        <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="9999" MaskType="Date"
                                            OnInvalidCssClass="errordate" TargetControlID="txtFromDate" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True">
                                        </ajaxToolKit:MaskedEditExtender>--%>
                                    </td>
                                    <td style="width: 50%">
                                        ToDate:
                                        <asp:TextBox ID="txtToDate" runat="server" ToolTip="Please Enter Account Name" Width="40%"
                                            Visible="true" Enabled="false"></asp:TextBox>&nbsp;<asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png"
                                                Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" 
                                            Format="yyyy" PopupButtonID="Image2" Enabled="false" TargetControlID="txtToDate">
                                        </ajaxToolKit:CalendarExtender>
                                       <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="9999" MaskType="Date"
                                            OnInvalidCssClass="errordate" TargetControlID="txtToDate" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True">
                                        </ajaxToolKit:MaskedEditExtender>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="width: 100%; text-align: center; padding: 5px">
                                            <asp:Button ID="btnProceed" runat="server" Text="Proceed >>" OnClick="btnProceed_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
