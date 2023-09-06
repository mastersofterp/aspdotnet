<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Invigilator.aspx.cs" Inherits="ACADEMIC_MASTERS_Invigilator"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px" colspan="2">
                INVIGILATOR MANAGEMENT
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
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
                        
                        <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                        <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                            <Move Horizontal="150" Vertical="-50" />
                            <Resize Width="260" Height="280" />
                            <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                        </Parallel>
                        
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
    <fieldset class="fieldset" style="width: 99%;">
        <legend class="legend">Invigilator List</legend>
        <table cellpadding="2" cellspacing="2" style="width: 90%;">
            <tr>
                <td class="form_left_label" style="width: 15%">
                    Total Selected :
                </td>
                <td class="form_left_text" style="width: 85%">
                    <asp:TextBox ID="txtTotFaculty" runat="server" Enabled="False" Width="30px" />
                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                </td>
                <td class="form_left_text">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Panel ID="pnlApplicants" runat="server" Width="50%">
                        <asp:ListView ID="lvFaculty" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Invigilator Selection</div>
                                    <table cellpadding="0" cellspacing="0" class="datatable">
                                        <thead>
                                            <tr class="header">
                                                <th style="width: 80%;" align="center">
                                                    Invigilators
                                                </th>
                                                <th style="width: 20%;">
                                                    <asp:CheckBox ID="chkhead" runat="server" OnClick="return totAll(this);" />Select
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div class="listview-container">
                                    <div id="demo-grid" class="vista-grid">
                                        <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td style="width:80%">
                                        <asp:Label ID="lblFaculty" runat="server" Text='<%# Eval("ua_fullname")%>' ToolTip='<%# Eval("ua_no")%>' />
                                    </td>
                                    <td style="width: 20%">
                                        <asp:CheckBox ID="chkUa_no" OnClick="return totchkRows(this);" runat="server" Checked='<%# Convert.ToInt16(Eval ("STATUS")) == 1 ? true:false %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Font-Bold="true" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text ="Cancel" Font-Bold="true" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </fieldset>

    <script type="text/javascript" language="javascript">
      function totchkRows(chk)
	{				    				
	    var txtTot = document.getElementById('<%= txtTotFaculty.ClientID %>');		
		if (chk.checked == true)					
			txtTot.value = Number(txtTot.value) + 1 ;		 
		else		
		if (chk.checked == false)	
			txtTot.value = Number(txtTot.value) - 1 ;	
		return;		
	}	

	function totAll(headchk)
	{				    				 
		var frm = document.forms[0]
		var txtTot = document.getElementById('<%= txtTotFaculty.ClientID %>');
	    txtTot.value = 0;
		for (i=3; i < document.forms[0].elements.length-2 ; i++)  
		{				
		var e = frm.elements[i];
			if (e.type == 'checkbox' && e != headchk  ){
			   if (headchk.checked == true) {			  	    
			       e.checked = true;
			       totchkRows(e);
			    }
			   else{
			       e.checked = false;	
			    }
			}
		}
		
	}	    
       
    </script>

</asp:Content>
