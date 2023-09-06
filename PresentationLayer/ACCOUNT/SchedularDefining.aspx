<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SchedularDefining.aspx.cs" Inherits="SchedularDefining" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    SCHEDULE DEFINING
                    <!-- Button used to launch the help (animation) -->
                   <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                         <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;"></div>
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
                <td style="padding: 10px">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: center; padding: 10px">
        <asp:UpdatePanel ID="upd" runat="server">
            <ContentTemplate>
                <fieldset class="fieldset">
                    <legend class="legend">Schedule Defining</legend>
                    <table cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td style="text-align: center">
                                <asp:RadioButton ID="rbDetail" runat="server" Text="Detail" AutoPostBack="True" Checked="True" GroupName="sch"
                                    OnCheckedChanged="rbDetail_CheckedChanged" />&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rbSummary" runat="server" Text="Summary" AutoPostBack="True" GroupName="sch"
                                    OnCheckedChanged="rbSummary_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            <asp:Panel ID="pnlgrd" Height="400px" ScrollBars="Vertical" runat="server">
                                <asp:GridView ID="rptSchDef" runat="server" AutoGenerateColumns="False" BorderColor="White"
                                    BorderStyle="Ridge" BorderWidth="2px" Width="672px" CellPadding="3" 
                                    Height="180px" OnPageIndexChanging="rptSchDef_PageIndexChanging" 
                                    BackColor="White" CellSpacing="1" GridLines="None" 
                                   >
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <RowStyle Wrap="True" BackColor="#DEDFDE" ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundField DataField="FA_NAME" HeaderText="GROUP NAME">
                                            <ControlStyle Width="500px" />
                                            <HeaderStyle BackColor="#3399FF" BorderStyle="None" Width="500px" ForeColor="White"
                                                HorizontalAlign="Left" />
                                            <ItemStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" Width="500px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MGRP_NAME1">
                                            <ControlStyle BorderStyle="None" Width="300px" />
                                            <HeaderStyle BackColor="#3399FF" BorderStyle="None" />
                                            <ItemStyle BorderStyle="None" ForeColor="Blue" Width="300px" 
                                                HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MGRP_NAME">
                                            <ControlStyle BorderStyle="None" Width="600px" />
                                            <HeaderStyle BackColor="#3399FF" BorderStyle="None" Width="600px" />
                                            <ItemStyle BorderStyle="None" Width="600px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SCH" HeaderText="SCH">
                                            <ControlStyle BorderStyle="None" Width="30px" />
                                            <HeaderStyle BackColor="#3399FF" BorderStyle="None" ForeColor="White" />
                                            <ItemStyle BorderStyle="None" HorizontalAlign="Center" Width="30px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                    <AlternatingRowStyle Wrap="True" />
                                </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
