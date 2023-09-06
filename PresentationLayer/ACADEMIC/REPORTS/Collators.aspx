<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Collators.aspx.cs" Inherits="Academic_REPORTS_Collators" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                Collators Page
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
                                <asp:DropDownList ID="ddlSession" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSession_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <tr>
                            <td style="width: 13%">
                                Degree :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDegree" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" 
                                    onselectedindexchanged="ddlDegree_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13%">
                                Branch :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" 
                                    onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13%">
                                Scheme :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlScheme" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13%">
                                Semester :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSemester" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" 
                                    onselectedindexchanged="ddlSemester_SelectedIndexChanged1" 
                                    AutoPostBack="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 13%">
                                Collator Name :</td>
                            <td>
                                <asp:DropDownList ID="ddlCollators1" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" >
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCollator1" runat="server" ControlToValidate="ddlCollators1"
                                    Display="None" ErrorMessage="Please Select First Collator" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                &nbsp
                                <asp:DropDownList ID="ddlCollators2" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" >
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="rfvCollator2" runat="server" ControlToValidate="ddlCollators2"
                                    Display="None" ErrorMessage="Please Select Second Collator" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                            </td>
                        </tr>                       
                        <tr>
                            <td style="width: 13%">
                                Checker Name :</td>
                            <td>
                            <asp:DropDownList ID="ddlCheckers1" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" >
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCheckers1" runat="server" ControlToValidate="ddlCheckers1"
                                    Display="None" ErrorMessage="Please Select First Checker" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                                &nbsp
                                <asp:DropDownList ID="ddlCheckers2" runat="server" Width="30%" 
                                    AppendDataBoundItems="true" TabIndex="1" >
                                     <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCheckers2" runat="server" ControlToValidate="ddlCheckers2"
                                    Display="None" ErrorMessage="Please Select Second Checker" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="report"></asp:RequiredFieldValidator>
                              </td>
                        </tr>
                        <tr>                       
                            <td style="width: 5%; margin-left: 160px;">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                            </td>
                            <td>
                                &nbsp;<asp:Button ID="btnReport2" runat="server" Text="Show Report" 
                                ValidationGroup="report" onclick="btnReport2_Click"  />
                                &nbsp
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    onclick="btnCancel_Click" />
                                
                                </td>
                        </tr>            
                        <tr>
                            <td style="width: 5%; margin-left: 160px;">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="margin-left: 160px;">
                              <asp:ListView ID="lvChecker" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                       Checkers Details
                                    </div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr class="header">
                                                <th style="width: 15%; text-align: center;">
                                                    Degree
                                                </th>
                                                <th style="width: 30%; text-align: center;">
                                                    Branch
                                                </th>
                                                <th style="width: 15%; text-align: center;">
                                                    Semester
                                                </th>
                                                
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                           
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("DEGREENO")%>
                                    </td>
                                    <td style="width: 30%; text-align: center;">
                                        <%# Eval("BRANCHNO")%>
                                    </td>
                                    <td style="width: 15%; text-align: center;">
                                        <%# Eval("SEMESTERNO")%>
                                    </td>
                                   
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%; margin-left: 160px;">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        </table>    
                </fieldset>
            </td>
        </tr>
    </table>
    
   <div id="divMsg" runat="server">
    </div>
</ContentTemplate>
</asp:UpdatePanel>
    
</asp:Content>
