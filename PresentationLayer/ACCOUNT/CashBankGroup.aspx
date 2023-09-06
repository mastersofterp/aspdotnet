<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CashBankGroup.aspx.cs" Inherits="CashBankGroup" Title="CASH/BANK GROUP GENERATION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
function totAllSubjects(headchk)
	{				    				
	    
		
		var frm = document.forms[0]
		for (i=0; i < document.forms[0].elements.length; i++)  
		{				
			var e = frm.elements[i];
			if (e.type == 'checkbox')
			{
			   if (headchk.checked == true)				  	    
			       e.checked = true;			       	
			   else
			       e.checked = false;			       
			}
		}
				
		
	}



</script>

    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="90%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    CASH/BANK GROUP
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
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <br />
            <table style="width: 100%">
            <tr>
            <td colspan="2">
                &nbsp;&nbsp; Note : <span style="color: #FF0000">* Marked is mandatory ! </span>
            </td>
            
            </tr>
                <tr id="myrow" runat="server">
                    <td class="form_left_label" style="width: 92px">
                        &nbsp; Modify
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlHeads" runat="server" AutoPostBack="True" 
                            Width="60%" onselectedindexchanged="ddlHeads_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label" style="width: 92px">
                        <span style="color: #FF0000"><b>*</b></span>Name
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtname" runat="server" Width="60%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtname"
                            Display="None" ErrorMessage="Head/Group Name Required" ValidationGroup="grpName"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label" style="height: 194px; width: 92px;">
                        &nbsp;
                    </td>
                    <td class="form_left_text" style="height: 194px">
                        <asp:Panel ID="pnl" runat="server" Style="height: 300px; width: 60%" 
                            BorderColor="#0066FF">
                            <asp:ListView ID="lvGrp" runat="server" >
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                            <tr class="header">
                                                <th>
                                                    <asp:CheckBox ID="chkHead" onClick="totAllSubjects(this)" runat="server" />
                                                </th>
                                                <th>
                                                    Cash/Bank Head Details
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <asp:CheckBox ID="chkDet" runat="server" />
                                            
                                            <%--<input id="hdnPcd" runat="server" value= '<%# Eval("PARTY_NO") %>' type="hidden" />--%>
                                        </td>
                                        <td>
                                            <%# Eval("PARTY_NAME") %>
                                            <asp:HiddenField id="hdnPcd" runat="server" value= '<%# Eval("PARTY_NO") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <asp:CheckBox ID="chkDet" runat="server" />
                                            
                                        </td>
                                        <td>
                                            <%# Eval("PARTY_NAME")%>
                                             <asp:HiddenField id="hdnPcd" runat="server" value= '<%# Eval("PARTY_NO") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="form_left_label" style="width: 92px">
                        &nbsp;
                    </td>
                    <td class="form_left_text">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="grpName"
                            Width="80px" onclick="btnAdd_Click" />
                        &nbsp;<asp:Button ID="btnModify" runat="server" Text="Modify" 
                            Width="80px" onclick="btnModify_Click" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                            CausesValidation="false" Width="80px" onclick="btnCancel_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="grpName" 
                            Height="16px" />
                    </td>
                </tr>
                    <tr>
                        <td class="form_left_label" style="width: 92px">
                            &nbsp;</td>
                        <td class="form_left_text">
                            <asp:Label ID="lblStatus" runat="server" SkinID="lblmsg"></asp:Label>
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
