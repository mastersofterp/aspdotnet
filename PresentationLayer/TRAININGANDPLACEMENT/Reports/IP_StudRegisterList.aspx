<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IP_StudRegisterList.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_IP_StudRegisterList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                STUDENTS LIST FOR INPLANT TRAINING &nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
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
                            Edit Record--%>
                            <%--<asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record--%>
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                            // Move an element directly on top of another element (and optionally
                            // make it the same size)
                            function Cover(bottom, top, ignoreSize) 
                            {
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            <td>
                <br />
                <asp:Panel ID="pnlSelect" runat="server">
                    <div style="text-align: left; width: 87%; padding-left: 10px;">
                        <fieldset class="fieldsetPay">
                            <legend class="legendPay">Report Selection Criteria</legend>
                            <table>
                                <tr>
                                    <td class="form_left_text" colspan="2">
                                        <asp:RadioButtonList ID="radlTransfer" runat="server" 
                                            RepeatDirection="Horizontal" AutoPostBack="true"
                                            onselectedindexchanged="radlTransfer_SelectedIndexChanged">
                                            <asp:ListItem Value="R" Selected="True">Registered</asp:ListItem>
                                            <asp:ListItem Value="P">Preference List </asp:ListItem>
                                            <asp:ListItem Value="S">Sorted Student List   </asp:ListItem>
                                            <asp:ListItem Value="C">Final List</asp:ListItem>
                                            <asp:ListItem Value="A">Summary Report</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlSelection" runat="server">
                                            <table>
                                                <tr>
                                                    <td class="form_left_label">
                                                        Company :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:DropDownList ID="ddlCompany" runat="server" Width="350px" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                        <asp:Panel ID="pnlchoice" runat="server">
                                            <table>
                                                <tr>
                                                    <td class="form_left_label">
                                                         Select :
                                                    </td>
                                                    <td class="form_left_text">
                                                        <asp:RadioButtonList ID="radlSelect" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="R" Selected="True">Report</asp:ListItem>
                                                        <asp:ListItem Value="E">Export </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        
                                       <%-- <asp:Panel ID="pnlBiodata" runat="server">
                                            <fieldset class="fieldset" style="width: 362px">
                                                <legend class="legend">Select Student Type</legend>
                                                <table>
                                                    <tr>
                                                        <td class="form_left_text" colspan="2">
                                                            <asp:RadioButtonList ID="radlStudentType" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="R" Selected="True">Regular</asp:ListItem>
                                                                <asp:ListItem Value="P">Pass out</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlschedule" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:RadioButtonList ID="radlschedule" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="F1" Selected="True">Format1</asp:ListItem>
                                                            <asp:ListItem Value="F2">Notice</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_button" colspan="2" align="center">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Select" OnClick="btnShow_Click"
                                            Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Select"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            <asp:Panel ID="pnllist" runat="server">
                <table>
                    <tr>
                        <td>
                            <div style="width: 97%; padding: 10px">
                                <asp:ListView ID="lvPinfo" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid" class="vista-grid" style="height: 350px; overflow: auto;">
                                            <div class="titlebar">
                                                Student List
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <thead>
                                                    <tr class="header">
                                                        <th>
                                                            Enroll No.
                                                        </th>
                                                        <th>
                                                            Name
                                                        </th>
                                                        <th>
                                                            %S.S.C.
                                                        </th>
                                                        <th>
                                                            %H.S.C.
                                                        </th>
                                                        <th>
                                                            % of Vth Sem
                                                        </th>
                                                        <th>
                                                            Total No. of Backlog
                                                        </th>
                                                        <th>
                                                            Contact No
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("TENTH_PER")%>
                                            </td>
                                            <td>
                                                <%# Eval("TWELVETH_PER")%>
                                            </td>
                                            <td>
                                                <%# Eval("FIFTH_SEM_PER")%>
                                            </td>
                                            <td>
                                                <%# Eval("NO_OF_BACKLOG_SUBJECTS")%>
                                            </td>
                                            <td>
                                                <%# Eval("TEMPCONTACTNO")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="form_button">
                            <asp:Button ID="btnExport" runat="server" Text="Export" ValidationGroup="Report"
                                OnClick="btnExport_Click" Width="80px" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
