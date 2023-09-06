<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BookEntry.aspx.cs" Inherits="BookEntry" Title="BOOK ENTRY" %>

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
                <td class="vista_page_title_bar" id="tdlabel" runat="server" style="height: 30px">
                BOOK ENTRY TRANSACTION
                  
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
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
            <table style="width: 100%; text-align:center" Id="tblGrid">
                <tr>
                    <td class="form_left_text" id="tdhdname" runat="server" align="center" style="text-transform:uppercase" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="form_left_text" style="height: 177px">
                        <asp:Panel ID="pnl" runat="server" BorderColor="#0066FF" Height="256px" 
                            Style="height: 300px; width: 50%">
                            <asp:ListView ID="lvGrp" runat="server">
                                <LayoutTemplate>
                                    <div ID="demo-grid" class="vista-grid">
                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                            <tr class="header">
                                                <th>
                                                    Head Details
                                                </th>
                                            </tr>
                                            <tr ID="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" 
                                            onmouseover="this.style.backgroundColor='#FFFFAA'">
                                            <td>
                                                <asp:LinkButton ID="lnkHead" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdnPcd" runat="server" value='<%# Eval("PARTY_NO") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" 
                                            onmouseover="this.style.backgroundColor='#FFFFAA'">
                                            <td>
                                                <asp:LinkButton ID="lnkHead0" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdnPcd0" runat="server" value='<%# Eval("PARTY_NO") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </td>
                    </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    </asp:Content>
