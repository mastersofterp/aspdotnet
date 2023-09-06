﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="GraphCalculation.aspx.cs" Inherits="ACADEMIC_REPORTS_GraphCalculation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" colspan="2" style="height: 30px">
                CONSOLIDATE REPORT
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
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
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
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
    </table>
    <div style="padding-left: 10px; width: 99%">
        <fieldset class="fieldset">
            <legend class="legend">Calculation Report</legend>
            <asp:UpdatePanel ID="updUpdate" runat="server">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="form_left_label" style="width: 10%">
                                Session :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" AutoPostBack="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Course :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    Width="50%">
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                    Display="None" ErrorMessage="Please Select Course" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <table width="100%">
                <tr>
                    <td class="form_left_text" align="center" >
                         <asp:Button ID="btnGraph" runat="server" Width="150px"  
                             Text="Course Avg. Calculation" onclick="btnGraph_Click"
                            />&nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear"
                            Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
