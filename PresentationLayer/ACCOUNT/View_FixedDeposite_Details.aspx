<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="View_FixedDeposite_Details.aspx.cs" Inherits="View_FixedDeposite_Details"
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
                    VIEW FIXED DEPOSITE&nbsp;
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
                <td style="padding: 10px" colspan="6">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; text-align: left" colspan="6">
                <asp:Panel ID ="P1" runat="server" ScrollBars="Both" Width="800px" Height="300px">
                    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" 
                        DataKeyNames="FDR_No" onrowediting="GridView1_RowEditing" 
                        onrowcancelingedit="GridView1_RowCancelingEdit" 
                        onrowupdating="GridView1_RowUpdating">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                        <asp:TemplateField HeaderText ="FUNDING LEDGER">
                        <ItemTemplate>
                       <%-- Text='<%#Bind("PARTY_NAME") %>'--%>
                       <asp:Label ID="pname" runat="server" Text='<%#Bind("PARTY_NAME") %>'></asp:Label>
                       <%--<asp:DropDownList ID="ddlParty" runat="server" ></asp:DropDownList>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:DropDownList ID="ddlParty" runat="server" ></asp:DropDownList>
                       <%--<asp:Label ID="l1" runat="server"></asp:Label>--%>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText ="SCHEME">
                        <ItemTemplate>
                       <asp:Label ID="lblFDScheme" runat="server" Text='<%#Bind("FDScheme") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:TextBox ID="txtFDScheme" runat="server" Text='<%#Bind("FDScheme") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" 
                           ControlToValidate="txtFDScheme" ErrorMessage="Enter Scheme Name"></asp:RequiredFieldValidator>
          <asp:CompareValidator ID="CompareValidator100" runat="server" ControlToValidate="txtFDScheme" ErrorMessage="Enter Valid Scheme Name" 
            Operator="DataTypeCheck" Type="String"></asp:CompareValidator>
            
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText ="FDR No">
                       <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblFDR_No" runat="server" Text='<%#Bind("FDR_No") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtFDR_No" runat="server" Text='<%#Bind("FDR_No") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFdrNo" runat="server" 
            ControlToValidate="txtFDR_No" ErrorMessage="Enter FDR Noumber"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="txtFDR_No" ErrorMessage="Enter Numeric Value" 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                       <asp:TemplateField HeaderText ="RATE OF INTEREST">
                       <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblROI" runat="server" Text='<%#Bind("ROI") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtROI" runat="server" Text='<%#Bind("ROI") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRoi" runat="server" 
            ControlToValidate="txtROI" ErrorMessage="Enter Rate Of Interest"></asp:RequiredFieldValidator>
                        
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ControlToValidate="txtROI" ErrorMessage="Enter Numeric Value " 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText ="DEPOSITE DATE">
                        <ItemTemplate>
                       <asp:Label ID="lblDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:Label>
                       
                    
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvDDate" runat="server" 
            ControlToValidate="txtDeposit_Date" ErrorMessage="Enter Deposit Date"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator22" runat="server" 
            ControlToValidate="txtDeposit_Date" ErrorMessage="Enter Date Only " 
            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
            
                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                             Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtDeposit_Date">
                         </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDeposit_Date">
                          </ajaxToolKit:MaskedEditExtender>
                        
                        </EditItemTemplate>
                        
                        
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText ="MATURITY DATE">
                        <ItemTemplate>
                       <asp:Label ID="lblMaturity_Date" runat="server" Text='<%#Bind("Maturity_Date") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtMaturity_Date" runat="server" Text='<%#Bind("Maturity_Date") %>'></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvMdate" runat="server" 
            ControlToValidate="txtMaturity_Date" ErrorMessage="Enter Maturity Date"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator12" runat="server" 
            ControlToValidate="txtMaturity_Date" ErrorMessage="Enter Date Only " 
            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        
                        <%--<asp:TextBox ID="txtDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:TextBox>--%>
                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate1" runat="server" Enabled="true" EnableViewState="true"
                             Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtMaturity_Date">
                         </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate1" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtMaturity_Date">
                          </ajaxToolKit:MaskedEditExtender>
                        
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText ="DEPOSITED AMOUNT">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblAmt" runat="server" Text='<%#Bind("Deposit_Amount") %>'></asp:Label>
                        </ItemTemplate>
                        
                         <EditItemTemplate>
                        <asp:TextBox ID="txtamt" runat="server" Text='<%#Bind("Deposit_Amount") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtamt" ErrorMessage="Enter Amount"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ControlToValidate="txtamt" ErrorMessage="Enter Numeric Value " 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        <%--<asp:TemplateField HeaderText="LEDGER NAME">
                       <ItemTemplate>
                       <asp:DropDownList ID="ddlLedger" runat="server"></asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </asp:Panel>
                     
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; text-align: left" colspan="6">
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl="~/Account/FixedDeposite_Details.aspx">BACK</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; text-align: left" colspan="6">
                    <b>MATURIED FIXED DEPOSITE SCHEMS</b>&nbsp;
                    <asp:ImageButton ID="btnHelp0" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />

                    <ajaxToolKit:AnimationExtender ID="btnHelp0_AnimationExtender" runat="server" 
                        TargetControlID="btnHelp0">
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
                    <div id="flyout0" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; text-align: left" colspan="6">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<input id="hdnBal" runat="server" type="hidden" /><input id="hdnMode" runat="server" type="hidden" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView2" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" 
                        DataKeyNames="FDR_No" onrowdeleting="GridView2_RowDeleting">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                        <asp:TemplateField HeaderText ="FUNDING LEDGER">
                        <ItemTemplate>
                       <%-- Text='<%#Bind("PARTY_NAME") %>'--%>
                       <asp:Label ID="pname" runat="server" Text='<%#Bind("PARTY_NAME") %>'></asp:Label>
                       <%--<asp:DropDownList ID="ddlParty" runat="server" ></asp:DropDownList>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:DropDownList ID="ddlParty" runat="server" ></asp:DropDownList>
                       <%--<asp:Label ID="l1" runat="server"></asp:Label>--%>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText ="SCHEME">
                        <ItemTemplate>
                       <asp:Label ID="lblFDScheme" runat="server" Text='<%#Bind("FDScheme") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:TextBox ID="txtFDScheme" runat="server" Text='<%#Bind("FDScheme") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" 
                           ControlToValidate="txtFDScheme" ErrorMessage="Enter Scheme Name"></asp:RequiredFieldValidator>
          <asp:CompareValidator ID="CompareValidator100" runat="server" ControlToValidate="txtFDScheme" ErrorMessage="Enter Valid Scheme Name" 
            Operator="DataTypeCheck" Type="String"></asp:CompareValidator>
            
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText ="FDR No">
                       <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblFDR_No" runat="server" Text='<%#Bind("FDR_No") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtFDR_No" runat="server" Text='<%#Bind("FDR_No") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFdrNo" runat="server" 
            ControlToValidate="txtFDR_No" ErrorMessage="Enter FDR Noumber"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToValidate="txtFDR_No" ErrorMessage="Enter Numeric Value" 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                       <asp:TemplateField HeaderText ="RATE OF INTEREST">
                       <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblROI" runat="server" Text='<%#Bind("ROI") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtROI" runat="server" Text='<%#Bind("ROI") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRoi" runat="server" 
            ControlToValidate="txtROI" ErrorMessage="Enter Rate Of Interest"></asp:RequiredFieldValidator>
                        
                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
            ControlToValidate="txtROI" ErrorMessage="Enter Numeric Value " 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText ="DEPOSITE DATE">
                        <ItemTemplate>
                       <asp:Label ID="lblDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:Label>
                       
                    
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvDDate" runat="server" 
            ControlToValidate="txtDeposit_Date" ErrorMessage="Enter Deposit Date"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator22" runat="server" 
            ControlToValidate="txtDeposit_Date" ErrorMessage="Enter Date Only " 
            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
            
                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                             Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtDeposit_Date">
                         </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDeposit_Date">
                          </ajaxToolKit:MaskedEditExtender>
                        
                        </EditItemTemplate>
                        
                        
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText ="MATURITY DATE">
                        <ItemTemplate>
                       <asp:Label ID="lblMaturity_Date" runat="server" Text='<%#Bind("Maturity_Date") %>'></asp:Label>
                        </ItemTemplate>
                        
                        <EditItemTemplate>
                        <asp:TextBox ID="txtMaturity_Date" runat="server" Text='<%#Bind("Maturity_Date") %>'></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="rfvMdate" runat="server" 
            ControlToValidate="txtMaturity_Date" ErrorMessage="Enter Maturity Date"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator12" runat="server" 
            ControlToValidate="txtMaturity_Date" ErrorMessage="Enter Date Only " 
            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        
                        <%--<asp:TextBox ID="txtDeposit_Date" runat="server" Text='<%#Bind("Deposit_Date") %>'></asp:TextBox>--%>
                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate1" runat="server" Enabled="true" EnableViewState="true"
                             Format="dd/MM/yyyy" PopupButtonID="imgCal10" PopupPosition="BottomLeft" TargetControlID="txtMaturity_Date">
                         </ajaxToolKit:CalendarExtender>
                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate1" runat="server" AcceptNegative="Left"
                                                                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtMaturity_Date">
                          </ajaxToolKit:MaskedEditExtender>
                        
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText ="DEPOSITED AMOUNT">
                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                        <ItemTemplate>
                       <asp:Label ID="lblAmt" runat="server" Text='<%#Bind("Deposit_Amount") %>'></asp:Label>
                        </ItemTemplate>
                        
                         <EditItemTemplate>
                        <asp:TextBox ID="txtamt" runat="server" Text='<%#Bind("Deposit_Amount") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="txtamt" ErrorMessage="Enter Amount"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ControlToValidate="txtamt" ErrorMessage="Enter Numeric Value " 
            Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                        </EditItemTemplate>
                        </asp:TemplateField>
                        
                        
                        
                        
                        
                        
                        
                        
                        
                        <%--<asp:TemplateField HeaderText="LEDGER NAME">
                       <ItemTemplate>
                       <asp:DropDownList ID="ddlLedger" runat="server"></asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
                    &nbsp;</td>
                <td colspan="5">
                &nbsp;</td>
            </tr>
            <tr>
                <td style="padding: 10px; width: 13%; text-align: left">
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
