<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Audit.aspx.cs" Inherits="DISPATCH_Transactions_Audit" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%--  Shrink the info panel out of view --%>
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">
                AUDIT ENTRY
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Reset the sample so it can be played again --%>
        <%--  Enable the button so it can be played again --%>
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
    <br />
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlAdd" runat="server">
                <fieldset class="fieldsetPay" style="width: 99%">
                    <legend class="legendPay">Inward Movement Entry </legend>
                    <table width="95%" cellpadding="2" cellspacing="2">
                    
                        <tr>
                            <td class="form_left_label" style="width: 20%;">
                                Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAmtDt" runat="server" MaxLength="10" Width="80px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReceivedDT" runat="server" ControlToValidate="txtAmtDt"
                                    Display="None" ErrorMessage="Please enter valid Amount  Received Date." SetFocusOnError="true"
                                    ValidationGroup="Submit" />
                                <asp:Image ID="imgReceivedDT" runat="server" src="../../images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true"
                                    Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT" TargetControlID="txtAmtDt">
                                </ajaxToolKit:CalendarExtender>
                                <ajaxToolKit:MaskedEditExtender ID="meeReceivedDT" runat="server" TargetControlID="txtAmtDt"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                <ajaxToolKit:MaskedEditValidator ID="mevReceivedDT" runat="server" ControlExtender="meeReceivedDT"
                                    ControlToValidate="txtAmtDt" EmptyValueMessage="Please Enter Movement Date"
                                    InvalidValueMessage="Received Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Amount Receive Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Movement Date"
                                    ValidationGroup="Submit" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Amount :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAmt" onkeyup="validateNumeric(this);"  runat="server" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmt"
                                    Display="None" ErrorMessage="Please enter Amount Received" SetFocusOnError="true"
                                    ValidationGroup="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_button" style="padding-top: 10px" colspan="4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" Width="80px" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div style="width: 99%; padding: 10px">
                                    <asp:Panel ID="pnlListView" Width="100%" runat="server" Style="margin-right: 0px">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:ListView ID="lvAudit" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="titlebar">
                                                                    AUDIT DETAILS
                                                                </div>
                                                                <table class="datatable" cellpadding="0" cellspacing="0">
                                                                    <thead>
                                                                        <tr class="header">
                                                                            <th>
                                                                                Action
                                                                            </th>
                                                                            <th>
                                                                                RECEIVE DATE
                                                                            </th>
                                                                            <th>
                                                                                AMOUNT
                                                                            </th>
                                                                            <%--<th>
                                                                                LOCK/UNLOCK
                                                                            </th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                        CommandArgument='<%# Eval("AU_NO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click"
                                                                        ToolTip="Edit Record" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DATE", "{0:dd-MMM-yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("AMT_ACC")%>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                            </td>
                        </tr>
                    
         
           </table> </fieldset> </asp:Panel>
            <br />

            <script language="javascript" type="text/javascript">
             function IsNumeric(textbox)
                {
                    if(textbox != null && textbox.value != "")
                    {
                        if (isNaN(textbox.value))
                        {
                            document.getElementById(textbox.id).value = 0;
                        }
                    }
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
    function totAllIDs(headchk)
	    {	
		    var frm = document.forms[0]
		    for (i=0; i < document.forms[0].elements.length; i++)  
		    {				
			    var e = frm.elements[i];
			    {
			    if (e.type=='checkbox' )
    			{    
    			
			        
			            if (headchk.checked == true)				  	    
			               e.checked = true;			       	
			            else
			               e.checked = false;			       
			 
		        }
		        }
		    }			
		 		
	    }  
	    function validateNumeric(txt)
    {
        if(isNaN(txt.value))
        {
        txt.value='';       
            alert('Only Numeric Characters Allowed!');
            txt.focus();
            return;            
         }        
    }
    function toggleExpansion(imageCtl, divId)
                {
                    if(document.getElementById(divId).style.display == "block")
                    {
                        document.getElementById(divId).style.display = "none";
                        imageCtl.src = "../../IMAGES/expand_blue.jpg";
                    }//../images/action_up.gif
                    else if(document.getElementById(divId).style.display == "none")
                    {
                        document.getElementById(divId).style.display = "block";
                        imageCtl.src ="../../IMAGES/collapse_blue.jpg";
                    }
                }  
    </script>

</asp:Content>
