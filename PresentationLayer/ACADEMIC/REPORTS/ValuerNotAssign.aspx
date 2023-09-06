<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ValuerNotAssign.aspx.cs" Inherits="Academic_REPORTS_MarksEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                VALUER NOT ASSIGN
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
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
    <table cellpadding="2" cellspacing="2" style="width: 100%">
        <tr>
            <td style="padding-top: 5px;width:100%" >
                <fieldset class="fieldset">
                    <legend class="legend">Selection Criteria</legend>
                    <table style="width: 100%" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td style="width: 13%">
                                Session :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSession" runat="server" Width="20%" 
                                    AppendDataBoundItems="true" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 13%">
                                Degree :</td>
                            <td>
                                <asp:DropDownList ID="ddlDegree" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" 
                                    onselectedindexchanged="ddlDegree_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="width: 13%">
                                Branch :</td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="30%" 
                                    AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    
                            </td>
                        </tr>
                       
                        <tr >
                            <td style="width: 13%">
                               Semester :</td>
                            <td>
                            <asp:DropDownList ID="ddlSemester" runat="server" Width="20%" 
                                    AppendDataBoundItems="true" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                
                                
                               </td>
                        </tr>
                       
                        <tr style="display:none">
                            <td style="width: 13%">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr>
                       
                            <td style="width: 5%">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                    
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                            </td>
                            <td>
                                <asp:Button ID="btnReport1" runat="server" Text="Report" ValidationGroup="teacherreport"
                                 onclick="btnReport1_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" ValidationGroup="teacherreport"
                                    onclick="btnExcelReport_Click" />
&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" />
                            </td>
                        </tr>
                       <tr>
                       
                            <td style="width: 5%">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                       
                        <tr style="display:none">
                       
                            <td style="width: 5%">
                                &nbsp;</td>
                            <td style="color:RED">
                                &nbsp;</td>
                        </tr>
                        </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
