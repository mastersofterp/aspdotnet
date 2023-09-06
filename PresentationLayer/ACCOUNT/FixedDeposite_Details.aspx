﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FixedDeposite_Details.aspx.cs" Inherits="FixedDeposite_Details"
    Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    function CheckFields()
    {

     if(document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').value=='')
     {
      alert('Please Enter From Date.');
      document.getElementById('ctl00_ContentPlaceHolder1_txtFrmDate').focus();
      return false;
     
     }

     if(document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').value=='')
     {
      alert('Please Enter Upto Date.');
      document.getElementById('ctl00_ContentPlaceHolder1_txtUptoDate').focus();
      return false;
     
     }
    
    
    
     if(document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').value=='')
     {
      alert('Please Enter Ledger.');
      document.getElementById('ctl00_ContentPlaceHolder1_txtAcc').focus();
      return false;
     
     }
    
    }
    
 function popUpToolTip(CAPTION)
		{	 
     		
   			 var strText=CAPTION;
	        overlib(strText,'Tool Tip','CreateSubLinks');
	        return true;
		} 
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
    function ShowLedger()
    {
    
    var popUrl = 'ledgerhead.aspx?obj=' + 'AccountingVouchers';
         var name = 'popUp';
         var appearence ='dependent=yes,menubar=no,resizable=no,'+
         'status=no,toolbar=no,titlebar=no,' +
         'left=50,top=35,width=900px,height=650px';
         var openWindow = window.open(popUrl, name, appearence);
         openWindow.focus();
      return false;
      
    }
    
    function AskSave()
    {
    if(confirm('Do You Want To Save The Transaction ? ')==true)
    {
      document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value=1;
      return true;
    }
    else
    {
       document.getElementById('ctl00_ContentPlaceHolder1_hdnAskSave').value=0;
       return false;
    }
    }
   
    function CheckTranChar(obj)
    {
    
      var k =(window.event)? event.keyCode : event.which;
      
      
      
     if (k==68 || k==67 || k==8 || k==9 || k==36 || k==37 || k==38 || k==39 || k==40 || k==46 )
     {
     obj.style.backgroundColor ="White";
     return true;
      
     }
     else
     {
      alert('Please Enter Either C OR D');
      obj.focus();
     }
     return false;
    }
    
    function ShowHelp()
    {
    
         var popUrl = 'PopUp.aspx?fn=' + 'LedgerHelp';
         var name = 'popUp';
         var appearence ='dependent=yes,menubar=no,resizable=no,'+
         'status=no,toolbar=no,titlebar=no,' +
         'left=100,top=50,width=600px,height=300px';
         var openWindow = window.open(popUrl, name, appearence);
         openWindow.focus();
      return false;
    
    
    }
    
    function SetFoc(obj)
    {
     obj.style.backgroundColor =SetTextBackColor();  // This function is created at Master page , register by javascript.
   var objRange = obj.createTextRange();
   objRange.moveStart("character", 0);
   objRange.moveEnd("character", obj.value.length);
   objRange.select();
   obj.focus();
    }
    
    function updateValues(popupValues)
    {
      document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value=popupValues[0];
      document.forms(0).submit();
    }
    
    
    </script>

    <div style="width: 100%; height: 591px;">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    FIXED DEPOSITE ENTRY
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
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
                                <%--  Shrink the info panel out of view --%>
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
                <td style="padding: 10px" colspan="6">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left; height: 52px;">
                    Funding Ledger</td>
                <td colspan="5" style="height: 52px">
                    <asp:DropDownList ID="ddlFundingLedger" runat="server" Width="304px" 
                        TabIndex="1">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    Scheme</td>
                <td colspan="5">
                    <asp:TextBox ID="txtScheme" runat="server" Width="304px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    FDR. No.</td>
                <td colspan="5">
                    <asp:TextBox ID="txtFdrNo" runat="server" Style="text-align: right" TabIndex="3" Width="215px"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="txtFdrNo" ErrorMessage="FDR Noumder Should Be Numeric" 
                        Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    Rate Of Interest</td>
                <td colspan="5">
                    <asp:TextBox ID="txtRoi" runat="server" Style="text-align: right" TabIndex="4"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="txtRoi" ErrorMessage="Rate Of Interest Should Be Numeric" 
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="qw"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    Date Of Deposit
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtFrmDate" runat="server" Width="12%" 
                        Style="text-align: right" 
                        ontextchanged="txtFrmDate_TextChanged" TabIndex="5" />
                    <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                        Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                    </ajaxToolKit:MaskedEditExtender>
                    <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                    &nbsp;&nbsp;&nbsp;&nbsp; Date Of Maturity&nbsp;
                    <asp:TextBox ID="txtUptoDate" Style="text-align: right" runat="server" 
                        Width="12%" TabIndex="6"/>
                    <ajaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                        EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                        TargetControlID="txtUptoDate">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                        MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                    </ajaxToolKit:MaskedEditExtender>
                    <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    Deposit Amount</td>
                <td colspan="5">
                    <asp:TextBox ID="txtamt" runat="server" Style="text-align: right" TabIndex="7"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="txtamt" ErrorMessage="Amount Should Be Numeric" 
                        Type="Double" ValidationGroup="qw"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    &nbsp;</td>
                <td colspan="5">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<input id="hdnBal" runat="server" type="hidden" /><input id="hdnMode" runat="server" type="hidden" />&nbsp;&nbsp;<asp:Button 
                        ID="btnRP" runat="server" 
                        Text="Save"  Width="65px" onclick="btnRP_Click" TabIndex="8"    />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                        Text="Cancel" onclick="btnCancel_Click" TabIndex="9" />
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    &nbsp;</td>
                <td colspan="5">
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl="~/Account/View_FixedDeposite_Details.aspx">To View Details Of Fixed Deposites 
                    Cleck Here</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 17%; text-align: left">
                    &nbsp;</td>
                <td style="width: 40%;">
                    &nbsp;</td>
                <td style="width: 10%; text-align: center">
                    &nbsp;</td>
                <td style="width: 237px;">
                    &nbsp;
                    </td>
                <td style="width: 10%;">
                    &nbsp;</td>
                <td style="width: 300px;">
                    <asp:TextBox ID="lblCurBal" runat="server" Height="23px" Width="80px" 
                        BorderColor="White" BorderStyle="None"  
                        style="background-color:Transparent; margin-left: 6px;" ReadOnly="True" 
                        Font-Size="XX-Small"></asp:TextBox>
                        <asp:TextBox ID="txtmd" runat="server" Height="23px" Width="12px" 
                        BorderColor="White" BorderStyle="None"  
                        style="background-color:Transparent; margin-left: 6px;" ReadOnly="True" 
                        Font-Size="XX-Small"></asp:TextBox>
                    </td>
            </tr>
            <tr align="left">
                <td style="padding: 10px; text-align: right" colspan="6">
                    <asp:UpdatePanel ID="UPDLedger" runat="server">
                    </asp:UpdatePanel>
                  </td>
            </tr>
            <tr>
            <td colspan="5" align="Left" style="font-size:medium">
                &nbsp;</td>
            </tr>
                        
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
