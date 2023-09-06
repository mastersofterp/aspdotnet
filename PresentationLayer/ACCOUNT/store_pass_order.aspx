<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="store_pass_order.aspx.cs" Inherits="ACCOUNT_store_pass_order" Title="Untitled Page" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkVoucherNoValid(ctrl, txtAmount,status) {
            var voucherNo = ctrl.value;
            var Amount = txtAmount.innerHTML;
            var compcode = '<%= HttpContext.Current.Session["comp_code"] %>';
            $.ajax({
                type: "POST",
                url: "store_pass_order.aspx/IsValid",
                data: '{VoucherNo: "' + voucherNo + '",amount:"' + Amount + '",compcode:"' + compcode + '",state:"' + status + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {


                    var IsValid = response.d;
                    //alert(IsValid);
                    if (IsValid == '0') {
                        //ctl00_ContentPlaceHolder1_btnSave.disabled = false;
                        return false;
                    }
                    else if (IsValid == 'Valid') {
                        //ctl00_ContentPlaceHolder1_btnSave.disabled = false;
                        return false;

                    }
                    else {
                        alert(IsValid);
                        //ctl00_ContentPlaceHolder1_btnSave.disabled = true;
                        ctrl.value = '';
                    }
                   
                },
                failure: function(response) {
                    alert(response.d);
                }
            });
        }
       
    </script>

    <script type="text/javascript" language="javascript">
        function atLeastOnePass() {
            var rptData = document.getElementById('ctl00_ContentPlaceHolder1_GridData').rows.length;
            for (var i = 2; i <= rptData; i++) {
                if (i < 10) {
                    var sel = document.getElementById('ctl00_ContentPlaceHolder1_GridData_ctl0' + i + '_txtVoucherNo').value;
                }
                else {
                    var sel = document.getElementById('ctl00_ContentPlaceHolder1_GridData_ctl' + i + '_txtVoucherNo').value;
                }

                if (sel != '') {

                    return true;
                }

            }
            alert('Please enter voucher no for at least one pass order');
            return false;
        }


        function EnableTextbox(ctrl) {

            ctrl.disabled = false;
            return false;
        }
    </script>

    <div style="width: 100%">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">
                    PASS ORDER PAY
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
                                <legend class="titlebar">PASS OREDER PAY</legend>
                        <asp:UpdatePanel ID="UPDLedger" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr id="rowgrid" runat="server">
                                        <td class="form_left_label" style="width: 18%; height: 19px;">
                                            From Date :
                                        </td>
                                        <td class="form_left_text" style="height: 19px;">
                                            <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" Width="8%" />
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
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:RadioButton ID="rdbUnPaid" runat="server" Text="Unpaid Passorder" GroupName="PassOrder"
                                                Checked="true" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rdbPaid" runat="server" Text="Paid Passorder" GroupName="PassOrder" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Button ID="btnShow" runat="server" Text="Show" Width="100px" OnClick="btnShow_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trUnpaidPO" runat="server">
                                        <td class="form_left_text" colspan="2">
                                            <asp:Panel ID="ScrollPanel" Height="350px" runat="server" ScrollBars="Vertical">
                                                <asp:GridView ID="GridData" runat="server" CellPadding="4" ForeColor="Black" GridLines="Both"
                                                    AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="None" BorderWidth="1px" OnRowCommand="GridData_RowCommand">
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <br />
                                                                <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                                    CommandArgument='<%# Eval("PASSORD_TRNO")%>' CommandName="Print" ImageUrl="~/images/print.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PASS ORDER NO/PASS ORDER DATE" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <br />
                                                                &nbsp;&nbsp;<b>No:-</b>&nbsp;&nbsp;
                                                                <asp:Label ID="lblPassNo" runat="server" Text='<%#Bind("PASSORDNO") %>'></asp:Label><br />
                                                                &nbsp;&nbsp;<b>Date:-</b>&nbsp;<asp:Label ID="lblPassDate" runat="server" Text='<%#Bind("PASSORD_DATE") %>'></asp:Label><asp:HiddenField
                                                                    ID="hdnPASSORD_TRNO" runat="server" Value='<%#Bind("PASSORD_TRNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No/Invoice Date" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <br />
                                                                &nbsp;&nbsp;<b>No:-</b>&nbsp;&nbsp;
                                                                <asp:Label ID="lblInvNo" runat="server" Text='<%#Bind("INVNO") %>'></asp:Label><br />
                                                                &nbsp;&nbsp;<b>Date:-</b>&nbsp;<asp:Label ID="lblInvDate" runat="server" Text='<%#Bind("INVDT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-Width="25%">
                                                            <ItemTemplate>
                                                                &nbsp;&nbsp;<asp:Label ID="lblPartyName" runat="server" Text='<%#Bind("PNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayAmount" runat="server" Text='<%#Bind("PAYMENT_AMOUNT") %>'></asp:Label><br />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVoucherNo" runat="server"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtVoucherNo" runat="server" ValidChars="0123456789,"
                                                                    TargetControlID="txtVoucherNo">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
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
                                    <tr id="trPaid" runat="server">
                                        <td class="form_left_text" colspan="2">
                                            <asp:Panel ID="Panel1" Height="350px" runat="server" ScrollBars="Vertical">
                                                <asp:GridView ID="gridDataPaid" runat="server" CellPadding="4" ForeColor="Black"
                                                    GridLines="Both" AutoGenerateColumns="False" Width="100%" BackColor="White" BorderColor="#DEDFDE"
                                                    BorderStyle="None" BorderWidth="1px" OnRowCommand="GridData_RowCommand">
                                                    <RowStyle BackColor="#F7F7DE" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <br />
                                                                <asp:ImageButton ID="btnVchPrint" ToolTip="Click For Voucher Printing. " runat="server"
                                                                    CommandArgument='<%# Eval("PASSORD_TRNO")%>' CommandName="Print" ImageUrl="~/images/print.gif" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PASS ORDER NO/PASS ORDER DATE" HeaderStyle-Width="30%">
                                                            <ItemTemplate>
                                                                <br />
                                                                &nbsp;&nbsp;<b>No:-</b>&nbsp;&nbsp;
                                                                <asp:Label ID="lblPassNo" runat="server" Text='<%#Bind("PASSORDNO") %>'></asp:Label><br />
                                                                &nbsp;&nbsp;<b>Date:-</b>&nbsp;<asp:Label ID="lblPassDate" runat="server" Text='<%#Bind("PASSORD_DATE") %>'></asp:Label><asp:HiddenField
                                                                    ID="hdnPASSORD_TRNO" runat="server" Value='<%#Bind("PASSORD_TRNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No/Invoice Date" HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <br />
                                                                &nbsp;&nbsp;<b>No:-</b>&nbsp;&nbsp;
                                                                <asp:Label ID="lblInvNo" runat="server" Text='<%#Bind("INVNO") %>'></asp:Label><br />
                                                                &nbsp;&nbsp;<b>Date:-</b>&nbsp;<asp:Label ID="lblInvDate" runat="server" Text='<%#Bind("INVDT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name" HeaderStyle-Width="25%">
                                                            <ItemTemplate>
                                                                &nbsp;&nbsp;<asp:Label ID="lblPartyName" runat="server" Text='<%#Bind("PNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayAmount" runat="server" Text='<%#Bind("PAYMENT_AMOUNT") %>'></asp:Label><br />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher No">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtVoucherNo" runat="server" Text='<%#Bind("VCHNO") %>'></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtVoucherNo" runat="server" ValidChars="0123456789,"
                                                                    TargetControlID="txtVoucherNo">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" ToolTip="Edit record" />
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
                                        <td class="form_left_text" style="height: 19px; width: 502px;">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="30%" ValidationGroup="Validation"
                                                Style="height: 26px" Height="24px" OnClick="btnSave_Click" OnClientClick="return atLeastOnePass()" />
                                            &nbsp;<asp:Button ID="btnReset" runat="server" Text="Cancel" Width="30%" Height="25px"
                                                OnClick="btnReset_Click" />
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
