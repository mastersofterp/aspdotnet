<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_Desig.aspx.cs" Inherits="PayRoll_Pay_Designation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" valign="top" style="height: 30px">
                        DESIGNATION
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
                                    <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                    Edit Record
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
                                if (!ignoreSize)
                                {
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
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 30px">
                        <asp:Panel ID="pnlSelect" runat="server" Width="90%">
                            <fieldset class="fieldsetPay">
                                <legend class="legendPay">Add Designation</legend>
                                <br />
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td class="form_left_label" width="15%">
                                            Staff Type :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlStaffType" AppendDataBoundItems="true" runat="server" Width="300px"
                                                AutoPostBack="true" 
                                                onselectedindexchanged="ddlStaffType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStaffType" runat="server" ControlToValidate="ddlStaffType"
                                                Display="None" ErrorMessage="Select Staff Type" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="15%">
                                            Designation :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtDesig" runat="server" Width="300px">
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDesig" runat="server" ControlToValidate="txtDesig"
                                                Display="None" ErrorMessage="Enter Designation" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="15%">
                                            Short Name:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtDesigShort" runat="server" Width="300px">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" width="15%">
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                                ValidationGroup="submit" onclick="btnSubmit_Click" />
                                            <asp:ValidationSummary runat="server" ID="vsSubmit" ShowMessageBox="true" ShowSummary="true"
                                                ValidationGroup="submit" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 30px">
                        <asp:Panel ID="pnlList" runat="server">
                            <table cellpadding="0" cellspacing="0" style="width: 90%;">
                                <tr>
                                    <td align="center">
                                        <asp:ListView ID="lvDesignation" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                No Records Found</EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Designation</div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr class="header">
                                                                <th width="10%">
                                                                    Action
                                                                </th>
                                                                <th width="30%">
                                                                    Designation
                                                                </th>
                                                                <th width="30%">
                                                                    Short Name 
                                                                </th>
                                                                <th width="30%">
                                                                    Staff Type
                                                                </th>
                                                            </tr>
                                                            <thead>
                                                    </table>
                                                </div>
                                                <div class="listview-container">
                                                    <div id="Div1" class="vista-grid">
                                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td width="10%">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("NUDESIGNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblNUDesig" runat="server" Text=' <%# Eval("NUDESIG")%>'></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblNUDesigShort" runat="server" Text=' <%# Eval("NUDESIGSHORT")%>'></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblStaffType" runat="server" Text=' <%# Eval("STAFF")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td width="10%">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("NUDESIGNO")%>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblNUDesig" runat="server" Text=' <%# Eval("NUDESIG")%>'></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblNUDesigShort" runat="server" Text=' <%# Eval("NUDESIGSHORT")%>'></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                       <asp:Label ID="lblStaffType" runat="server" Text=' <%# Eval("STAFF")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
