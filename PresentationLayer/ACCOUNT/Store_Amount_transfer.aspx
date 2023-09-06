<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Store_Amount_transfer.aspx.cs" Inherits="ACCOUNT_Store_Amount_transfer" Title="Untitled Page" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    STORE - AMOUNT TRANSFER
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                        border: solid 1px #D0D0D0;">
                    </div>
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
                    <fieldset class="vista-grid">
                                <legend class="titlebar">&nbsp;STORE - AMOUNT TRANSFER</legend>
                        <asp:UpdatePanel ID="UPDLedger" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                               
                                    <tr>
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                           From Date :
                                        </td>
                                        <td class="form_left_text" style="height: 19px;" colspan="2">
                                            <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" Width="8%"/>
                                            &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            &nbsp;&nbsp;&nbsp;To Date :
                                            <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" Width="8%" />
                                            &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                                Visible="False" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                TargetControlID="txtTodate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="row18" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            <span style="color: #FF0000"><b>*</b></span> Vendor Name
                                        </td>
                                        <td class="form_left_text" style="height: 19px; width: 502px;">
                                            <asp:DropDownList ID="ddlVendor" runat="server" Width="30%" 
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVendor" runat="server" 
                                                ErrorMessage="Please Select Vendor Name" Display="None" InitialValue="0" 
                                                ValidationGroup="Show" ControlToValidate="ddlVendor"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="vertical-align: top; text-align: center; padding: 5px; width: 11%; height: 19px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr id="Tr1" runat="server">
                                        <td>
                                        &nbsp;
                                        </td>
                                        <td class="form_left_text" colspan="2" style="height: 19px">
                                            <asp:Button ID="btnShowData" runat="server" Text="Show" 
                                                 Width="10%" onclick="btnShowData_Click" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="vsShow" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                        </td>
                                    </tr>
                                    <tr id="rowgrid" runat="server">
                                        
                                        <td class="form_left_text" colspan="2" style="height: 19px">
                                            <asp:Panel ID="ScrollPanel" Height="200px" runat="server" ScrollBars="Both">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    AutoGenerateColumns="False" Width="95%" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="None" BorderWidth="1px" >
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="Invoice No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceNo" runat="server" Text='<%#Bind("INVNO") %>'></asp:Label>
                                                                <asp:HiddenField ID="hdnINVTRNO" runat="server" Value='<%#Bind("INVTRNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Ledger Head Name">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLedgerheadName" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Expence/Purchase">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpence" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField> 
                                                        
                                                        <asp:TemplateField HeaderText="Net Amount">
                                                            <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                             <ItemStyle HorizontalAlign="Right"  Width="10%"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNetAmt" runat="server" Style="text-align: right" Text='<%#Bind("NETAMT")%>' ></asp:Label>
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
                                    
                                    
                                    
                                    
                                    
                                    <tr id="Row20" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            &nbsp;
                                        </td>
                                        <td class="form_left_text" style="height: 19px; width: 502px;">
                                            &nbsp;
                                            <asp:Button ID="btnTrans" runat="server" Enabled="false"  
                                                Style="height: 26px" Text="Transfer" ValidationGroup="Validation" 
                                                Width="15%" onclick="btnTrans_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnReset" runat="server" CausesValidation="False" 
                                                 Text="Cancel" Width="15%" Height="27px" onclick="btnReset_Click"  />
                                        </td>
                                        
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    
                </td>
            </tr>
        </table>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

