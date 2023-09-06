<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CashBookEntry.aspx.cs" Inherits="CashBookEntry" Title="CASH BOOK ENTRY" %>

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
            <div id="Divcntl" runat="server" style="width: 100%; text-align: center;padding:10px">
            <table style="width: 400px;background-color:#DFDFDF" id="tblGrid">
                <tr>
                    <td style="text-align:left">
                        Search Criteria
                        <asp:TextBox ID="txtname" runat="server" Width="35%" AutoPostBack="True" 
                            OnTextChanged="txtname_TextChanged"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="btnclose" runat="server" Text="X" Height="19px" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Panel ID="pnl" runat="server" Style="width: 100%" BorderColor="#0066FF"
                            Height="300px">
                            <asp:ListView ID="lvGrp" runat="server" OnItemCommand="lvGrp_ItemCommand">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                            <tr class="header">
                                                <th>
                                                    Head Details
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <asp:LinkButton ID="lnkHead" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:LinkButton>
                                            <asp:HiddenField ID="hdnPcd" runat="server" Value='<%# Eval("PARTY_NO") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem" onmouseout="this.style.backgroundColor='#FFFFD2'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <asp:LinkButton ID="lnkHead" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:LinkButton>
                                            <asp:HiddenField ID="hdnPcd" runat="server" Value='<%# Eval("PARTY_NO") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                            <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="False" PopupControlID="tblGrid" RepositionMode="RepositionOnWindowScroll"
                                TargetControlID="txtname">
                            </ajaxToolKit:ModalPopupExtender>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
