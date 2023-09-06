﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPStudInfoList_1.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_TPStudInfoList"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">
                STUDENT INFORMATION LIST &nbsp;
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
                        <fieldset class="fieldsetPay" style="width: 662px">
                            <legend class="legendPay">Selection Criteria</legend>
                            <table>
                                <tr style="display:none;">
                                    <td class="form_left_label">
                                        Student Type :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlStudType" runat="server" Width="100px" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Both</asp:ListItem>
                                            <asp:ListItem Value="R">Regular</asp:ListItem>
                                            <asp:ListItem Value="P">Pass-out</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="form_left_label">
                                        Session :
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ValidationGroup="show"
                                            Width="50%" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Summary"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="form_left_label">
                                        Degree :
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" Width="50%" TabIndex="1">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%" class="form_left_label">
                                        Branch :
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="50%" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%" class="form_left_label">
                                        Scheme :
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" ValidationGroup="show"
                                            Width="50%" TabIndex="3">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%" class="form_left_label">
                                        Semester :
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" Width="50%"
                                            TabIndex="4" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="save" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 12%" class="form_left_label">
                                        Tp_Registered
                                    </td>
                                    <td style="width: 60%" class="form_left_text">
                                        <asp:CheckBox ID="chkTpReg" runat="server" Checked="true" />
                                    </td>
                                </tr>
                               <%-- <tr>--%>
                                <%--<tr>
                                    <td class="form_left_label">
                                        Student Name :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlStudName" runat="server" Width="250px" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="form_button" colspan="2" align="center">
                                        <%--<asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Select" OnClick="btnShow_Click"
                                            Width="80px" />--%>
                                            <asp:Button ID="btnExcel" runat="server" Text="Excel Report" ValidationGroup="Select" OnClick="btnExcel_Click"
                                            Width="120px" />
                                            
                                            <asp:Button ID="btnCan" runat="server" Text="Cancel" OnClick="btnCan_Click"
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
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
