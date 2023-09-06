<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaymentBillFormat.aspx.cs" Inherits="ACCOUNT_PaymentBillFormat" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="90%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    FINAL ACCOUNT GROUP
                    <!-- Button used to launch the help (animation) -->
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
            
            <%--<script type="text/javascript" language="javascript">
                function SetPage() {
                
                }
            </script>--%>
            
            <tr>
                <td style="padding: 10px">
                    <div id="divCompName" runat="server" class="account_compname">
                    </div>
                    <center>
                    <fieldset class="fieldset" style="width: 50%;padding: 5% 5% 5% 5%;">
                        <legend class="legend">Payment Bill Voucher<br />
                        </legend>
                        <span style="font-weight: normal">Note:</span> 
                        <span style="color: red;font-weight: normal">* Marked is mandatory !</span>
                        <br />
                        <br />
                        <asp:UpdatePanel ID="UPDMainGroup" runat="server">
                            <ContentTemplate>
                                <table cellpadding="3" cellspacing="3" >
                                <tr>
                                        <td class="form_left_text" style="width: 25%;">
                                            <span style="color: #FF0000">*</span>Select Type:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlVoucherType" runat="server" style="width: 102%;" 
                                                AutoPostBack="True" 
                                                onselectedindexchanged="ddlVoucherType_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0" Text="--Please Select--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Payment Voucher"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Advance Cum Payment Voucher"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Advance Ajustment Cum Payment Voucher"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Advance Voucher"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvType" runat="server" 
                                                ErrorMessage="Please Select Type" Display="None" 
                                                ControlToValidate="ddlVoucherType" ValidationGroup="Submit" 
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trGross" runat="server">
                                        <td class="form_left_text" style="width: 25%;">
                                            <span style="color: #FF0000">*</span>Gross Amount:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtGrossAmoount" runat="server" enableSelection="Yes" Text="" ToolTip="Please Enter Gross Amount"
                                                Width="100%" ValidationGroup="Submit"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvGrossAmount" runat="server" 
                                                ErrorMessage="Please Enter Gross Amount" Display="None" 
                                                ControlToValidate="txtGrossAmoount" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trTDS" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                             TDS @ 2%:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtTDS" runat="server" enableSelection="Yes" Text="" ToolTip="Please Enter TDS"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trDeduct" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                            (-):
                                        </td>
                                        <td class="form_left_text" style="width: 25%;">
                                            <asp:TextBox ID="txtDeduction" runat="server" enableSelection="Yes" Text="" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trBillAmount" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                            Bill Amount / Settle Amount:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtBillAmont" runat="server" enableSelection="Yes" Text="" ToolTip="Please Enter Bill Amount"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trAmountToPaid" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                            Amount to be Paid(+) / Deposited(-):
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAmountPaid" runat="server" enableSelection="Yes" Text="" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trAlreadyDeposite" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                            Already Deposited:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAllDeposite" runat="server" enableSelection="Yes" Text="" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trRecovered" runat="server">
                                        <td class="form_left_label" style="width: 25%;">
                                            To be Deposited / Recovered:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtRecovered" runat="server" enableSelection="Yes" Text="" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_text" colspan="2">
                                            <center>
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" Width="100px" TabIndex="2"
                                                    Style="height: 25px" ValidationGroup="Submit" />
                                                <asp:ValidationSummary ID="vsPaymentBill" runat="server" DisplayMode="List" 
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    </center>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
