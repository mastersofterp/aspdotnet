<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChequeEntryModifications.aspx.cs" Inherits="ChequeEntryModifications" Title="" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
<script language="javascript" type="text/javascript" >
</script>
    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    <script language="javascript" type="text/javascript">
    
    
    
  function UpdateParentWindow(fName,lName)
 {
     alert(fName);
     alert(lName);
     
      var arrayValues= new Array(fName,lName);
      window.opener.updateValues(arrayValues);       
      window.close(); 
  }
    
    
    
    function CheckNumeric(obj)
    {
  
    
      var k =(window.event)? event.keyCode : event.which;
      
    // alert(k);
      
     if (k==68 || k==67 || k==8 || k==9 || k==36 || k==35 || k==16 || k==37 || k==38 || k==39 || k==40 || k==46 || k==13 || k==110)
     {
     if(obj.value=='')
     {
      alert('Field Cannot Be Blank');
      obj.focus();
      return false;
     }
      obj.style.backgroundColor ="White";
      return true;
      
     }
     if(k > 45 && k < 58 || k > 95 && k < 106)
     {
      obj.style.backgroundColor ="White";
      return true;
     
     }
     else
     {
    
      alert('Please Enter numeric Value');
      obj.focus();
     }
     return false;
    }

    
    
    function updateValues(popupValues)
    {
      document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value=popupValues[0];
      document.forms(0).submit();
    }
    
    
    </script>

    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    CHEQUE ENTRY MODIFICATIONS
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
                            <%--  Enable the button so it can be played again --%>
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
                        <legend class="legend">Select For Cheque Entry Modifications</legend>
                     
                        <asp:UpdatePanel ID="UPDLedger" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                      <tr>
                                      <td>From Date:
                                       <asp:TextBox ID="txtfrmDate" runat="server" style="text-align:right" ToolTip="Please Enter From Date"
                                                 Width="12%" TabIndex="7" />
                                                 <asp:RequiredFieldValidator ID="rfvchqissuedate" runat="server" ErrorMessage="Please Enter From Date"
                                                ControlToValidate="txtfrmDate" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                 <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" 
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal" 
                                                PopupPosition="BottomLeft" TargetControlID="txtfrmDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" 
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" 
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" 
                                                OnInvalidCssClass="errordate" TargetControlID="txtfrmDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                      
                                          &nbsp;Upto Date:
                                          <asp:TextBox ID="txtUptoDate" runat="server" style="text-align:right" 
                                              TabIndex="7" ToolTip="Please Enter From Date" Width="12%" />
                                          <ajaxToolKit:CalendarExtender ID="txtfrmDate0_CalendarExtender" runat="server" 
                                              Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" 
                                              PopupButtonID="imgCal0" PopupPosition="BottomLeft" TargetControlID="txtUptoDate">
                                          </ajaxToolKit:CalendarExtender>
                                          <ajaxToolKit:MaskedEditExtender ID="txtfrmDate0_MaskedEditExtender" 
                                              runat="server" AcceptNegative="Left" DisplayMoney="Left" 
                                              ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" 
                                              MessageValidatorTip="true" OnInvalidCssClass="errordate" 
                                              TargetControlID="txtUptoDate">
                                          </ajaxToolKit:MaskedEditExtender>
                                          <asp:RequiredFieldValidator ID="rfvchqissuedate0" runat="server" 
                                              ControlToValidate="txtUptoDate" Display="None" 
                                              ErrorMessage="Please Enter Upto Date" SetFocusOnError="True" 
                                              ValidationGroup="submit"></asp:RequiredFieldValidator>
                                          <asp:Image ID="imgCal0" runat="server" ImageUrl="~/images/calendar.png" 
                                              Style="cursor: pointer" />
                                          &nbsp;<asp:Button ID="btnGet" runat="server" onclick="btnGet_Click" Text="Get" 
                                              Width="48px" />
                                          </td>
                                      </tr>                                 
                                    <tr id="rowgrid" runat="server">
                                        <td class="form_left_text" style="height: 19px">
                                            <asp:Panel ID="ScrollPanel" Height="513px" runat="server" ScrollBars="Both">
                                               <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    AutoGenerateColumns="False" Width="95%" BackColor="White" 
                                                    BorderColor="#DEDFDE" BorderStyle="None" 
                                                    BorderWidth="1px" Height="160px" onselectedindexchanging="GridData_SelectedIndexChanging"  
                                                    >
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                        <asp:CommandField ShowSelectButton="True" />
                                                        <asp:BoundField DataField="PARTYACCOUNTNO" HeaderText="A/C No.">
                                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                                            <ItemStyle Wrap="False" Width="15%" />
                                                        </asp:BoundField>
                                                        <%--<asp:BoundField DataField="PARTYNAME" HeaderText="A/C Name">
                                                            <HeaderStyle HorizontalAlign="Left" Width="40%" />
                                                            <ItemStyle Wrap="False" Width="40%" />
                                                        </asp:BoundField>--%>
                                                                                                               
                                                        <asp:TemplateField HeaderText="Account Name">
                                                        <ItemTemplate>
                                                        <asp:LinkButton ID="lnkparty" runat="server" text='<%# Eval("PARTYNAME") %>' 
                                                               ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                                                                              
                                                        
                                                        <asp:BoundField DataField="CHECKNO" HeaderText="Cheque No.">
                                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            <ItemStyle Wrap="False" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CHECKDT" HeaderText="Cheque Date" DataFormatString="{0:dd/MM/yyyy}" >
                                                            <HeaderStyle HorizontalAlign="right" Width="10%" />
                                                            <ItemStyle Wrap="False" Width="10%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="15%" />
                                                            <ItemStyle Wrap="False" Width="15%" HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                        <ItemTemplate>
                                                        <asp:HiddenField ID="hdntrno" runat="server" Value='<%# Eval("CTRNO") %>'/>
                                                        <asp:HiddenField ID="hdnPartyNo1" runat="server" Value='<%# Eval("PARTYNO") %>'/>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                      
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                
                            
                       
                         
                            </ContentTemplate>
                        </asp:UpdatePanel>
                      </fieldset>
                        <input id="hdnbal2" runat="server" type="hidden" /> 
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
