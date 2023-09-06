<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AbstractBillVouchers.aspx.cs" Inherits="AbstractBillVouchers" Title="Untitled Page" %>

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
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>

    <script language="javascript" type="text/javascript">
    </script>

    <script language="javascript" type="text/javascript" src="../IITMSTextBox.js"></script>

    
    
    <script language="javascript" type="text/javascript">

        function CheckNumeric(obj) {


            var k = (window.event) ? event.keyCode : event.which;

            // alert(k);

            if (k == 68 || k == 67 || k == 8 || k == 9 || k == 36 || k == 35 || k == 16 || k == 37 || k == 38 || k == 39 || k == 40 || k == 46 || k == 13 || k == 110) {
                if (obj.value == '') {
                    alert('Field Cannot Be Blank');
                    obj.focus();
                    return false;
                }
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k > 45 && k < 58 || k > 95 && k < 106) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {

                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }



        function updateValues(popupValues) {
            document.getElementById('ctl00_ContentPlaceHolder1_hdnPartyNo').value = popupValues[0];
            document.forms(0).submit();
        }

        function displayForm() {
            var a = document.getElementById('<%= ddlVoucherType.ClientID%>').value;
            alert(a);
            alert('dasdasdas');

            if (a == 'AAPV') {
                document.getElementsByName('<%= TrAdvance.ClientID%>').style.display = '';
                alert('sdasdasd');
            }
        }
        
        function calPer(ctrl) {

            var amount = ctrl.value;
            var GrossAmount = document.getElementById('<%=txtGAmount.ClientID %>').value;
            var per = (GrossAmount * amount) / 100;
            document.getElementById('<%=txtAmount.ClientID %>').value = per;
        }

//        $(document).ready(function() {
//            //Dropdownlist Selectedchange event
//            $('#ddlDeductionHead').change(function() {
//                // Get Dropdownlist seleted item text
//                var ValueDDl = $("#ddlCountry option:selected").text();
//                if (ValueDDl != "Please Select") {
//                    alert('dfsdf');
//                }
//                return false;
//            })
//        });
    </script>

    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    Abstract Bill Voucher
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
                    <fieldset class="vista-grid" style="width:90%">
                                <legend class="titlebar">Abstract Bill Voucher</legend>
                         <b>Note: <span style="font-weight: normal; color: red">All Fields are mandatory !</span></legend></b>
                        <asp:UpdatePanel ID="UPDLedger" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td>
                                        &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="row18" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            Voucher Type
                                        </td>
                                       <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlVoucherType" runat="server" 
                                                AppendDataBoundItems="True" Width="69%" 
                                                onselectedindexchanged="ddlVoucherType_SelectedIndexChanged" 
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0" Text="--Please Select--"></asp:ListItem>
                                                <asp:ListItem Value="APV" Text="ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                                                <asp:ListItem Value="AAPV" Text="ADVANCE ADJUSTMENT CUM PAYMENT VOUCHER"></asp:ListItem>
                                                <asp:ListItem Value="AV" Text="ADVANCE VOUCHER"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDdlVoucher" runat="server" ControlToValidate="ddlVoucherType"
                                                Display="None" ErrorMessage="Please Select Voucher Type" SetFocusOnError="True"
                                                ValidationGroup="AccMoney" InitialValue="0"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="row4" runat="server">
                                        <td class="form_left_label" style="width: 18%;">
                                            Head of Account&nbsp;<br />
                                            
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <cc2:AutoSuggestBox ID="txtAcc" ToolTip="Please Enter Account Name" DataType="All"
                                                Width="68%" runat="server" ResourcesDir="AutoSuggestBox" AutoPostBack="True"
                                                OnTextChanged="txtAcc_TextChanged"></cc2:AutoSuggestBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAcc"
                                                Display="None" ErrorMessage="Please Enter Account Ledger" SetFocusOnError="True"
                                                ValidationGroup="AccMoney"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                TargetControlID="txtAcc" WatermarkText="Press space to get ledgers." WatermarkCssClass="watermarked" />
                                            &nbsp;
                                           
                                            
                                        </td>
                                    </tr>
                                    <tr id="Tr3" runat="server">
                                        <td class="form_left_label" style="width: 18%;">
                                            Name to be Display&nbsp;<br />
                                            
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txDisplayName" runat="server" Width="67.5%">
                                            </asp:TextBox>
                                            
                                            &nbsp;
                                            
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
                                        <td class="form_left_label" style="width: 18%;">
                                            Gross Amount/Bill Amount&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtGAmount" runat="server" Width="68%" onblur="return CheckGross(this)" ></asp:TextBox>
                                            &nbsp;<ajaxToolKit:TextBoxWatermarkExtender ID="tbweGrossAmount" runat="server" TargetControlID="txtGAmount"
                                                WatermarkText="Gross Amount" WatermarkCssClass="watermarked" />
                                                <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbeGross" TargetControlID="txtGAmount" ValidChars="0123456789." FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                           <asp:RequiredFieldValidator ID="rfvGrossAmount" runat="server" ControlToValidate="txtGAmount"
                                                Display="None" ErrorMessage="Please Enter Gross Amount" SetFocusOnError="True"
                                                ValidationGroup="AccMoney" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="TrAdvance" runat="server" visible="False">
                                        <td class="form_left_label" style="width: 18%;">
                                            Advance Taken Amount&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdvanceTaken" runat="server" Width="68%" Text="0.00"></asp:TextBox>
                                            &nbsp;<ajaxToolKit:TextBoxWatermarkExtender ID="tbweAdvanceTaken" runat="server" TargetControlID="txtAdvanceTaken"
                                                WatermarkText="Advance Taken Amount" WatermarkCssClass="watermarked" />
                                                <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbeAdvanceTaken" TargetControlID="txtAdvanceTaken" ValidChars="0123456789." FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                           <asp:RequiredFieldValidator ID="rfvAdvanceTaken" runat="server" ControlToValidate="txtAdvanceTaken"
                                                Display="None" ErrorMessage="Please Enter Advance Taken Amount" SetFocusOnError="True"
                                                ValidationGroup="AccMoney" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td class="form_left_label" style="width: 18%;">
                                            Deduction Head&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text" colspan="2">
                                            <asp:DropDownList runat="server" AppendDataBoundItems="True" ID="ddlDeductionHead"
                                                AutoPostBack="True" Width="42%" 
                                                onselectedindexchanged="ddlDeductionHead_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="--Please Select--"></asp:ListItem>
                                            </asp:DropDownList>
                                            
                                            &nbsp;
                                            
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="form_left_label" style="width: 18%;">
                                            Tax in (%)&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text" colspan="2">
                                               <asp:TextBox runat="server" ID="txtPer" onblur="return calPer(this)"  Width="13.3%"></asp:TextBox>
                                            &nbsp;
                                            
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td class="form_left_label" style="width: 18%;">
                                            Deduction Amount&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text">
                                     
                                            <asp:TextBox ID="txtAmount" runat="server" Width="22%"></asp:TextBox>
                                            &nbsp;
                                      <ajaxToolKit:FilteredTextBoxExtender runat="server" ID="ftbeAmount" TargetControlID="txtAmount" ValidChars="0123456789." FilterMode="ValidChars"></ajaxToolKit:FilteredTextBoxExtender>

                                            <%--<ajaxToolKit:TextBoxWatermarkExtender ID="tbweDeductionAmount" runat="server" TargetControlID="txtAmount"
                                                WatermarkText="Deduction Amount" WatermarkCssClass="watermarked" />--%>
                                            <asp:Button ID="btnSaveDeduction" runat="server" Text="Add" Width="9%" ValidationGroup="AccMoney"
                                                Style="height: 26px" Height="24px" OnClick="btnSaveDeduction_Click" />
                                            <asp:ValidationSummary ID="vsAbsractBill" runat="server" DisplayMode="List" 
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="AccMoney" />
                                        </td>
                                    </tr>
                                    <tr id="rowgrid" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            <input id="hdnAgParty" runat="server" type="hidden" />
                                            <input id="hdnOparty" runat="server" type="hidden" />
                                        </td>
                                        <td style="width: 1%">
                                            <b></b>
                                        </td>
                                        <td class="form_left_text" style="height: 19px; width: 50%;">
                                            <asp:Panel ID="ScrollPanel" runat="server" ScrollBars="Both">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                                    AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="None" BorderWidth="1px">
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label runat="server" ID="lblDeductionHead" Text="Deduction Head"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("DeductHeadName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblPayble" runat="server" Text="Payble"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblDeductAmount" runat="server" Text="Deduction Amount"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("DeductAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblPaybleAmount" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblTran" runat="server" Text="Transaction Type"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:DropDownList runat="server" ID="ddlTran" Width="50%" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                                                        <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </center>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblPaybleAmount" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnPartyNo" runat="server" Value='<%# Eval("DeductHeadId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnTaxPercent" runat="server" Value='<%# Eval("DeductPercent") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" />
                                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                                <b>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 30%;">
                                                       
                                                            <asp:Label runat="server" ID="lblPayble" Text="Amount Payble"></asp:Label>
                                                            
                                                        </td>
                                                        <td>
                                                        &nbsp;<asp:Label runat="server" ID="lblAmountPayble"></asp:Label></td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                        <td>
                                                        &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                </b>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 18%; height: 20px;">
                                            Narration&nbsp;
                                        </td>
                                        <td style="width: 1%">
                                            <b>:</b>
                                        </td>
                                        <td class="form_left_text" colspan="3" style="height: 100%; width: 99%;">
                                            &nbsp;
                                            <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Width="59.5%"
                                                Height="100%"></asp:TextBox>
                                            &nbsp;
                                            <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                                                TargetControlID="txtNarration" WatermarkText="Narration" WatermarkCssClass="watermarked" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="Row20" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            <input id="hdnAskSave" runat="server" type="hidden" />
                                            <input id="hdnVchId" runat="server" type="hidden" />
                                        </td>
                                        <td style="width: 1%">
                                          
                                        </td>
                                        <td class="form_left_text" style="height: 19px; width: 502px;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="10%" ValidationGroup="Validation"
                                                OnClick="btnSave_Click" Style="height: 26px" Height="24px" />
                                            &nbsp;<asp:Button ID="btnReset" runat="server" Text="Cancel" Width="10%" Height="25px"
                                                OnClick="btnReset_Click" />
                                                <asp:Button ID="btnPrint" runat="server" Text="Print Voucher" Width="20%" 
                                                Height="25px" onclick="btnPrint_Click"
                                                 />
                                        </td>
                                        <td class="form_left_text" style="height: 19px">
                                            &nbsp;
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
