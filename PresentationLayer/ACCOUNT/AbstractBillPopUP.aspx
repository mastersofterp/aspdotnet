<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AbstractBillPopUP.aspx.cs" Inherits="ACCOUNT_AbstractBillPopUP" Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            margin-left:250px;
        }
    </style>
    <div style="width: 100%; height: 591px;">
    <asp:UpdatePanel ID="upd" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px" colspan="6">
                    ABSTRACT BILL APPROVAL
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
                <td>
                   
                        <fieldset class="vista-grid" style="width: 50%;">
                            <legend class="titlebar">Abstract Bill Approval</legend>
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td class="form_left_label" style="width: 5%">
                                        Bill Type
                                    </td>
                                    <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:DropDownList ID="ddlAbstractBillType" runat="server" AppendDataBoundItems="true"
                                            Width="75%" AutoPostBack="true" 
                                            onselectedindexchanged="ddlAbstractBillType_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Text="--Please Select--"></asp:ListItem>
                                            <asp:ListItem Value="PV" Text="PAYMENT VOUCHER"></asp:ListItem>
                                            <asp:ListItem Value="APV" Text="ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                                            <asp:ListItem Value="AAPV" Text="ADVANCE ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                                            <asp:ListItem Value="AV" Text="ADVANCE VOUCHER"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 5%">
                                      Select Bill No.
                                    </td>
                                    <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:DropDownList ID="ddlAbtractBillNo" runat="server" AppendDataBoundItems="true"
                                            Width="75%" OnSelectedIndexChanged="ddlAbtractBillNo_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="-1" Text="--Please Select--"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 5%">
                                        Bill No
                                    </td>
                                     <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:Label runat="server" ID="lblBillNo" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="form_left_label" style="width: 5%">
                                        Bank
                                    </td>
                                     <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="true" Width="75%">
                                            <asp:ListItem Value="0" Selected="True">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBank" runat="server" ErrorMessage="Please Select Bank"
                                            InitialValue="0" ControlToValidate="ddlBank" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="divledger" runat="server">
                                    <td class="form_left_label" style="width: 5%">
                                        PARTY
                                    </td>
                                     <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:DropDownList ID="ddlleager" runat="server" AppendDataBoundItems="true" Width="75%">
                                            <asp:ListItem Value="0" Selected="True">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvLeager" runat="server" ErrorMessage="Please Select JV Party"
                                            InitialValue="0" ControlToValidate="ddlleager" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 5%">
                                        Cheque No.
                                    </td>
                                     <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:TextBox ID="txtChequeNo" runat="server" MaxLength="6"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftvChequeNo" runat="server" TargetControlID="txtChequeNo"
                                            ValidChars="1234567890" FilterMode="ValidChars">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr id="trDate" runat="server">
                                    <td class="form_left_label" style="width: 5%">
                                        Cheque Date
                                    </td>
                                     <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                    <td class="form_left_text" style="width: 30%">
                                        <asp:TextBox ID="txtChequeDate" runat="server"></asp:TextBox>
                                        <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer"
                                            meta:resourcekey="imgCalResource1" />
                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            PopupButtonID="imgCal" TargetControlID="txtChequeDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                            OnInvalidCssClass="errordate" TargetControlID="txtChequeDate" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                            CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                            Enabled="True">
                                        </ajaxToolKit:MaskedEditExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                <td class="form_left_label" style="width: 5%">
                                       
                                    </td>
                                     <td style="width: 1%">
                                            
                                        </td>
                                    <td colspan="2">
                                        <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                            AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                            BorderStyle="None" BorderWidth="1px">
                                            <RowStyle BackColor="#F7F7DE" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Deductin Head">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeductFeeHead" runat="server" Text='<%#Bind("DeductHead") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="DeductHeadNo" Value='<%#Bind("DeductHeadNo") %>' />
                                                        <asp:HiddenField runat="server" ID="hdnTRANSACTION_NO" Value='<%#Bind("TRANSACTION_NO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Head">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlleager" runat="server" AppendDataBoundItems="true" Width="100%">
                                                            <asp:ListItem Value="0" Selected="True">--Please Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_text" colspan="3" align="center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                            Width="80px" OnClick="btnSubmit_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                            Width="80px" />
                                        <asp:ValidationSummary ID="vs" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                   
                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
