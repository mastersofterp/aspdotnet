<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Configuration.aspx.cs" Inherits="Configuration" Title="Configuration Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="90%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    CONFIGURATION CREATION
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
                    <fieldset class="fieldset">
                        <legend class="legend">Add/Modify - Configuration</legend>
                        <asp:UpdatePanel ID="UPDMainGroup" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td class="form_left_label" colspan="2">
                                            &nbsp; Note : <span style="color: #FF0000">* Marked is mandatory !</span>
                                        </td>
                                        <td rowspan="6" 
                                            style="vertical-align: top; text-align: center; padding: 10px; width: 40%">
                                            Search Criteria....<br>
                                            <asp:TextBox ID="txtSearch" runat="server" 
                                                 Style="text-transform: uppercase" Text="" ToolTip="Please Enter Group Name"
                                                Type="Varchar" Width="90%" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged">
                                                </asp:TextBox>
                                            <br />
                                            <asp:ListBox ID="lstGroup" runat="server" Rows="10" Width="100%" AutoPostBack="True"
                                                OnSelectedIndexChanged="lstGroup_SelectedIndexChanged"></asp:ListBox>
                                            <%--<ajaxToolKit:ListSearchExtender ID="lstGroup_ListSearchExtender" runat="server" 
                                                Enabled="True" TargetControlID="lstGroup">
                                            </ajaxToolKit:ListSearchExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 24%">
                                            <span style="color: #FF0000">*</span>Configuration Description:
                                        </td>
                                        <td class="form_left_text" style="width: 40%">
                                            <asp:TextBox ID="txtconfigdesc" runat="server" Style="text-transform: uppercase"
                                                Text="" ToolTip="Please Enter Configuration Description" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGroupName" runat="server" ControlToValidate="txtconfigdesc"
                                                Display="None" ErrorMessage="Please Enter Configuration Description" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 24%">
                                            <span style="color: #FF0000">*</span>Configuration Value
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtconfigvalue" runat="server" Style="text-transform: uppercase"
                                                Text="" ToolTip="Please Enter Configuration Value" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGroupName0" runat="server" ControlToValidate="txtconfigvalue"
                                                Display="None" ErrorMessage="Please Enter Configuration Value" SetFocusOnError="True"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 24%">
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                Width="60px" OnClick="btnSubmit_Click" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                Width="60px" OnClick="btnCancel_Click" />
                                            &nbsp;<asp:Button ID="btnBack" runat="server" CausesValidation="false" 
                                                 Text="Back" Width="60px" onclick="btnBack_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 24%">
                                            &nbsp;</td>
                                        <td class="form_left_text">
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                                                ValidationGroup="submit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 24%">
                                            &nbsp;
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
