<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChequePrintingTransaction.aspx.cs" Inherits="ChequePrintingTransaction"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>

    <style type="text/css">
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 500px;
            height: 500px;
            overflow-y: auto;
        }

        .ledgermodalBackground {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup {
            background-color: #e5ecf9;
            border-width: 3px;
            border-style: double;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 80%;
            height: 600px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function tAmountCheck(txt) {
            var vCode = document.getElementById('<%= hfHiddenFieldID.ClientID%>');
            var at = txt.value;
            if (parseFloat(vCode.value) != 0) {
                if (parseFloat(vCode.value) < parseFloat(at)) {
                    txt.value = '';
                    alert('Please Enter Amount less then Voucher Amount');
                }
            }
        }
    </script>

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div style="width: 100%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div1" runat="server">
                                <div id="div2" runat="server"></div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">CHEQUE PRINTING</h3>
                                </div>
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                                <div class="box-body">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <%-- <div class="col-md-8">--%>
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Print Cheque</div>
                                            <div class="panel-body">
                                                Note<span style="font-size: small">:</span><span style="font-weight: bold; font-size: x-small; color: red">* Marked is mandatory !</span><br />
                                                <br />
                                                <div id="dvSign" class="col-md-10" runat="server">
                                                    <div class="form-group row">
                                                        <div class="col-md-3">
                                                            <label>Select Signature<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:RadioButton ID="rdbRegistrar" Text="Registrar" runat="server" Checked="true" GroupName="Sign" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:RadioButton ID="rdbPrincipal" Text="Principal" runat="server" GroupName="Sign" />
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:RadioButton ID="rdbDean" Text="Dean" runat="server" GroupName="Sign" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <label>Bank Allocation For : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Label ID="lblBankName" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <label>Voucher No.<span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtVCHNO" runat="server" TabIndex="1" TextMode="SingleLine" ToolTip="Please Enter Ledger Name"
                                                                CssClass="form-control" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <label>Party Name <span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtLedgerName" runat="server" TabIndex="1" TextMode="SingleLine"
                                                                ToolTip="Please Enter Party Name" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ft" runat="server" FilterMode="InvalidChars"
                                                                InvalidChars=".!@#$%^&*=+';:/?'" TargetControlID="txtLedgerName">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvLedgerName" runat="server" ControlToValidate="txtLedgerName"
                                                                Display="None" ErrorMessage="Please Enter Party Name" SetFocusOnError="True"
                                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <label>Cheque Number <span style="color: red">*</span> : </label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtCHQNo" runat="server" AutoPostBack="false" TabIndex="2" ToolTip="Please Enter Cheque Number"
                                                                ValidationGroup="submit" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvChqDt" runat="server" ControlToValidate="txtCHQNo"
                                                                ValidationGroup="submit" ErrorMessage="Please Enter Cheque Number"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="ValidChars"
                                                                ValidChars="1234567890" TargetControlID="txtCHQNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Amount:</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <asp:CheckBox ID="chkStamp" runat="server" Text="A/C Payee" Checked="true" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Cheque Date:</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Style="text-align: right" AutoPostBack="true"
                                                                    meta:resourcekey="txtDateResource1" TabIndex="3" />
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="imgCal" TargetControlID="txtDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtDate" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <label>Amount:</label>
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Style="text-align: right"
                                                                TabIndex="4" ToolTip="Please Enter Amount" onchange="tAmountCheck(this)" CssClass="form-control"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeContactNo" runat="server" FilterMode="ValidChars"
                                                                TargetControlID="txtAmount" ValidChars="0123456789.">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                                Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div id="Div4" class="row" runat="server">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="5"
                                                                Text="Add" ValidationGroup="submit" CssClass="btn btn-info" />
                                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                                            <asp:HiddenField ID="hfHiddenFieldID" runat="server" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:GridView ID="gvChqDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                        GridLines="Vertical" Width="100%" OnRowCommand="gvChqDetails_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="30px" HeaderStyle-Width="30px"
                                                                ItemStyle-Width="30px">
                                                                <HeaderTemplate>
                                                                    VchNo.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVchNo" runat="server" Text='<%# Eval("VOUCHER_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" />
                                                                <ItemStyle Width="7%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="100%" FooterStyle-Width="100%" HeaderStyle-Width="100%"
                                                                ItemStyle-Width="100%">
                                                                <HeaderTemplate>
                                                                    Party
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblParty" runat="server" Text='<%# Eval("PARTY_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="30%" />
                                                                <ItemStyle Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="30px" HeaderStyle-Width="30px"
                                                                ItemStyle-Width="130px">
                                                                <HeaderTemplate>
                                                                    Cheque NO.
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChqNo" runat="server" Text='<%# Eval("CHQ_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                ItemStyle-Width="130px">
                                                                <HeaderTemplate>
                                                                    Cheque Date
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CHQ_DATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                ItemStyle-Width="130px">
                                                                <HeaderTemplate>
                                                                    Ac Payee
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                ItemStyle-Width="130px">
                                                                <HeaderTemplate>
                                                                    Amount
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ControlStyle-Width="15px" FooterStyle-Width="15px" HeaderStyle-Width="15px"
                                                                ItemStyle-Width="15px">
                                                                <HeaderTemplate>
                                                                    Edit
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit record" />

                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" />
                                                                <ItemStyle Width="7%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" />
                                                        <PagerStyle BackColor="#F7F7DE" ForeColor="white" HorizontalAlign="Right" />
                                                        <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="White" Height="30px" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                                <br />
                                                <div class="form-group col-md-12">
                                                    <div id="Div3" class="row" runat="server">
                                                        <div class="col-md-6">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-info" />
                                                            <asp:Button ID="btnGenerate" runat="server" Text="Genrate" OnClick="btnGenrate_Click"
                                                                Width="55px" Visible="false" />
                                                        </div>
                                                    </div>
                                                    <div id="Td36" class="row" runat="server">
                                                        <div class="col-md-3">
                                                        </div>
                                                        <div class="col-md-5">
                                                            <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                            <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                        DropShadow="True" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                                        Enabled="True">
                                                    </ajaxToolKit:ModalPopupExtender>
                                                    <asp:Panel ID="pnl" runat="server" Width="800px" BorderColor="#0066FF" meta:resourcekey="pnlResource1">
                                                        <div class="form-group col-md-12" style="text-align: center">
                                                            <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="Close" ValidationGroup="Validation"
                                                                Width="20%" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
                                                        </div>
                                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                                        <br />
                                                        <br />
                                                        <asp:GridView ID="GvPrintDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                            GridLines="Vertical" Width="100%" OnRowCommand="GvPrintDetails_RowCommand" OnSelectedIndexChanged="GvPrintDetails_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="30px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="10%">
                                                                    <HeaderTemplate>
                                                                        VchNo.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVchNo" runat="server" Text='<%# Eval("VNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="30px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="150px">
                                                                    <HeaderTemplate>
                                                                        Party
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblParty" runat="server" Text='<%# Eval("PARTYNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="180px" />
                                                                    <ItemStyle Width="180px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="30px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="130px">
                                                                    <HeaderTemplate>
                                                                        Cheque NO.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblChqNo" runat="server" Text='<%# Eval("CHECKNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="22px" />
                                                                    <ItemStyle Width="22px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="130px">
                                                                    <HeaderTemplate>
                                                                        Cheque Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("CHECKDT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="22px" />
                                                                    <ItemStyle Width="22px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="130px">
                                                                    <HeaderTemplate>
                                                                        Ac Payee
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STAMP") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="22px" />
                                                                    <ItemStyle Width="22px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="30px" FooterStyle-Width="130px" HeaderStyle-Width="30px"
                                                                    ItemStyle-Width="130px">
                                                                    <HeaderTemplate>
                                                                        Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="22px" />
                                                                    <ItemStyle Width="22px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ControlStyle-Width="80px" FooterStyle-Width="80px" HeaderStyle-Width="80px"
                                                                    ItemStyle-Width="80px">
                                                                    <HeaderTemplate>
                                                                        Print
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnPrint" CommandArgument='<%# Eval("CTRNO") %>' Text="Print" runat="server"
                                                                            ToolTip="Print Cheque" CssClass="btn btn-info" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="80px" />
                                                                    <ItemStyle Width="80px" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#3C8DBC" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </ContentTemplate>
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>--%>
        </asp:UpdatePanel>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
