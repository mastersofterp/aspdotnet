<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="IsolatedChequePrint.aspx.cs" Inherits="ACCOUNT_IsolatedChequePrint"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="cc2" Namespace="ASB" Assembly="AutoSuggestBox" %>
<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="Custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
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

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UPDLedger"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UPDLedger" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Isloated Cheque Printing</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12 mb-3">
                                <div id="divCompName" runat="server" style="text-align: center; font-size: x-large">
                                </div>
                            </div>
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Account Name</label>
                                            </div>
                                            <asp:DropDownList ID="txtAccount" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="txtAccount_TextChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvdept" runat="server" ControlToValidate="txtAccount"
                                                    ValidationGroup="submit" Display="None" InitialValue="0" ErrorMessage="Please Select Account Name">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Payee Name</label>
                                            </div>
                                            <asp:DropDownList ID="txtLedgerName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvtxtLedgerName" ControlToValidate="txtLedgerName" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select or Enter Payee Name" runat="server"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cheque Number</label>
                                            </div>
                                            <asp:TextBox ID="txtCHQNo" runat="server" AutoPostBack="false" TabIndex="3" Placeholder="Enter Cheque Number"
                                                ToolTip="Please Enter Cheque Number" ValidationGroup="submit" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator ID="rfvChqDt" runat="server" ControlToValidate="txtCHQNo"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCHQNo"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Cheque Number" ></asp:RequiredFieldValidator>
                                       
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                FilterMode="ValidChars" ValidChars="1234567890" TargetControlID="txtCHQNo">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Cheque Date</label>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="imgCal">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" Placeholder="Enter Cheque Date"
                                                            meta:resourcekey="txtDateResource1" TabIndex="4" />
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

                                                           <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate"
                                                          Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Cheque Date" ></asp:RequiredFieldValidator>
                                                        
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Amount</label>
                                            </div>
                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15"
                                                TabIndex="5" ToolTip="Please Enter Amount" Placeholder="Enter Amount" onchange="tAmountCheck(this)"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeContactNo" runat="server" FilterMode="ValidChars"
                                                TargetControlID="txtAmount" ValidChars="0123456789.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Amount" ></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:CheckBox runat="server" ID="IsAccountPayee" Text="&nbsp;&nbsp;Is Account Payee" />

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Add" ValidationGroup="submit"
                                        CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="7" Text="Cancel" CssClass="btn btn-warning"
                                        OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    <asp:HiddenField ID="hfHiddenFieldID" runat="server" />
                                    <br />
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>

                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:GridView ID="gvChqDetails" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                            BorderWidth="1px" CssClass="table table-striped table-bordered nowrap "
                                            GridLines="Vertical" OnRowCommand="gvChqDetails_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Account Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccName" runat="server" Text='<%# Eval("AccName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfAccNo" runat="server" Value='<%# Eval("AccId") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Party
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblParty" runat="server" Text='<%# Eval("PayeeName") %>'></asp:Label>
                                                        <asp:HiddenField ID="hfPartyNo" runat="server" Value='<%# Eval("PayeeId") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Cheque NO.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChqNo" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Cheque Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ChequeDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Ac Payee
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Amount
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="22px" />
                                                    <ItemStyle Width="22px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                    <HeaderTemplate>
                                                        Edit
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            runat="server" ImageUrl="~/images/edit.png" ToolTip="Edit record" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="15px" />
                                                    <ItemStyle Width="15px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="#000" />
                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="#000" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" Visible="false"
                                        OnClick="btnSave_Click" />
                                       <asp:Button ID="btncancel2" runat="server" TabIndex="7" Text="Cancel" CssClass="btn btn-warning"
                                        onClick="Cancel2_Click" Visible="false"/>
                                    <asp:Button ID="btnGenerate" runat="server" Text="Genrate" CssClass="btn btn-primary" Visible="false" />
                                    <input id="hdnIdEditParty" runat="server" type="hidden" />
                                </div>

                                <div class="form-group row" id="rowhelp" runat="server">
                                    <div id="Td35" class="form_left_label" style="width: 9%; height: 19px;" runat="server"></div>
                                    <div id="Td36" class="form_left_text" style="height: 19px" runat="server">
                                        <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                            DropShadow="True" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                            Enabled="True">
                                        </ajaxToolKit:ModalPopupExtender>
                                    </div>
                                </div>

                                <asp:Panel ID="pnl" runat="server" meta:resourcekey="pnlResource1">
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnBack" runat="server" Text="Close" ValidationGroup="Validation"
                                            CssClass="btn btn-primary" meta:resourcekey="btnBackResource1" />
                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                    </div>
                                    <div class="col-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GvPrintDetails" runat="server" AutoGenerateColumns="False"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                                CssClass="table table-striped table-bordered table-hover"
                                                GridLines="Vertical" OnRowCommand="GvPrintDetails_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Account Name
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccName" runat="server" Text='<%# Eval("AccName") %>' ForeColor="White"></asp:Label>
                                                            <asp:HiddenField ID="hfAccNo" runat="server" Value='<%# Eval("AccId") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Party
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParty" runat="server" Text='<%# Eval("PayeeName") %>' ForeColor="White"></asp:Label>
                                                            <asp:HiddenField ID="hfPartyNo" runat="server" Value='<%# Eval("PayeeId") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Cheque NO.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChqNo" runat="server" Text='<%# Eval("ChequeNo") %>' ForeColor="White"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Cheque Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("ChequeDate") %>' ForeColor="White"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Ac Payee
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' ForeColor="White"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Amount
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount") %>' ForeColor="White"></asp:Label>
                                                            <asp:HiddenField ID="hfAmtinwrd" runat="server" Value='<%# Eval("AmountinWrd") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount") %>' ForeColor="White"></asp:Label>
                                                            <%--added by vijay andoju 16-07-2020 for get the account--%>
                                                            <asp:HiddenField ID="hfAccnumber" runat="server" Value='<%# Eval("ACCNO") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="22px" />
                                                        <ItemStyle Width="22px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                                        <HeaderTemplate>
                                                            Print
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                Text="Print" runat="server" ToolTip="Print Cheque" CssClass="btn btn-info" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="45px" />
                                                        <ItemStyle Width="45px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle CssClass="bg-light-blue" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
