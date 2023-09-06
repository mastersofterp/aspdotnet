<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Acc_TDS_Report.aspx.cs" Inherits="ACCOUNT_Acc_TDS_Report" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            text-align: center;
        }
    </style>
    <%-- <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>--%>
    <script type="text/javascript">
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 100000;
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
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">TDS REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <asp:Panel ID="Panel1" runat="server">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divQuarter" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Select Quarter</label>
                                            </div>
                                            <asp:DropDownList ID="ddlQuarter" runat="server" AutoPostBack="false" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Text="Please Select" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="1st Quarter" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2nd Quarter" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="3rd Quarter" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="4th Quarter" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter From Date" />
                                                <ajaxToolKit:CalendarExtender ID="ceQuotDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFrmDt"
                                                    PopupButtonID="imgCal" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtQuotDate" runat="server" ControlToValidate="txtFrmDt"
                                                    Display="None" ErrorMessage="Please Select From Date" ValidationGroup="submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meQuotDate" runat="server" TargetControlID="txtFrmDt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meQuotDate"
                                                    ControlToValidate="txtFrmDt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodt" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter To Date" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTodt" PopupButtonID="Image1" Enabled="true" EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="submit"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtTodt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                            </div>
                                        </div>
                                        <div class=" d-none">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Select Ledger</label>
                                                </div>
                                                <asp:TextBox ID="txtLedger" runat="server" CssClass="form-control" ToolTip="Please Enter Ledger Name"
                                                    AutoPostBack="true" OnTextChanged="txtLedger_TextChanged"></asp:TextBox>
                                                <ajaxToolKit:AutoCompleteExtender ID="autLedger" runat="server" TargetControlID="txtLedger"
                                                    MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="1000"
                                                    ServiceMethod="GetMergeLedger" OnClientShowing="clientShowing">
                                                    <%--OnClientShowing="clientShowing"--%>
                                                </ajaxToolKit:AutoCompleteExtender>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLedger"
                                                            Display="None" ErrorMessage="Please Select Ledger" SetFocusOnError="true" ValidationGroup="AccMoney">

                                                        </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label></label>
                                                </div>
                                                <asp:TextBox ID="lblCurBal" runat="server" BorderColor="White"
                                                    BorderStyle="None" Style="background-color: Transparent; margin-left: 0px;" ReadOnly="True"
                                                    Font-Size="Small" Font-Bold="true"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" ValidationGroup="AccMoney" Text="Show"
                                        OnClick="btnShow_Click" />
                                    <asp:Button ID="btnShowPDF" runat="server" CssClass="btn btn-info" Text="Show Report" ValidationGroup="AccMoney"
                                        OnClick="btnShowPDF_Click" />
                                    <asp:Button ID="btnShowExcel" runat="server" CssClass="btn btn-primary" Text="Show Excel" ValidationGroup="AccMoney"
                                        OnClick="btnShowExcel_Click" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="vsSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AccMoney" />
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlTDSGrid" runat="server" Visible="false">
                                <div class="form-group row">

                                    <div class="col-md-8">
                                        <asp:ImageButton ID="btnbeforeExcel" runat="server" ImageUrl="~/Images/excel.jpeg" ToolTip="Export Record" Width="60px" OnClick="btnbeforeExcel_Click" />

                                        <asp:Label ID="lblInstruction" Text="" Font-Bold="true" runat="server"
                                            CssClass="btn btn-primary"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnltds" runat="server">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <asp:Repeater ID="rptTDSGrid" runat="server">
                                                <HeaderTemplate>
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <td>
                                                                <label>Party Reference No.</label>
                                                            </td>
                                                            <td>
                                                                <label>PAN</label>
                                                            </td>
                                                            <td>
                                                                <label>Party Name</label>
                                                            </td>
                                                            <td>
                                                                <label>Date of payment</label>
                                                            </td>
                                                            <td>
                                                                <label>Taxable Amount</label>
                                                            </td>
                                                            <td>
                                                                <label>TDS</label>
                                                            </td>
                                                            <td>
                                                                <label>Surcharge</label>
                                                            </td>
                                                            <td>
                                                                <label>ED CESS</label>
                                                            </td>
                                                            <td>
                                                                <label>Total Tax Deposit ED</label>
                                                            </td>
                                                            <td>
                                                                <label>Date Of Deduction</label>
                                                            </td>
                                                            <td>
                                                                <label>Section Code</label>
                                                            </td>
                                                            <td>
                                                                <label>Challan No</label>
                                                            </td>
                                                            <td>
                                                                <label>Challan Amount</label>
                                                            </td>
                                                            <td>
                                                                <label>Challan Date</label>
                                                            </td>
                                                            <%-- <td>
                                                                        <label>Rate at which deducted</label>
                                                                    </td>
                                                                    <td>
                                                                        <label>Nature of Work</label>
                                                                    </td>
                                                                    <td>
                                                                        <label>Cheque No</label>
                                                                    </td>

                                                                    <td>
                                                                        <label>Name of the Bank</label>
                                                                    </td>--%>
                                                            <td>
                                                                <label>BSR Code</label>
                                                            </td>

                                                            <td>
                                                                <label>Receipt No.</label>
                                                            </td>
                                                        </tr>
                                                        <tr class="text-center" style="background-color: GrayText">
                                                            <td>
                                                                <label>1</label></td>
                                                            <td>
                                                                <label>2</label></td>
                                                            <td>
                                                                <label>3</label></td>
                                                            <td>
                                                                <label>4</label></td>
                                                            <td>
                                                                <label>5</label></td>
                                                            <td>
                                                                <label>6</label></td>
                                                            <td>
                                                                <label>7</label></td>
                                                            <td>
                                                                <label>8</label></td>
                                                            <td>
                                                                <label>9</label></td>
                                                            <td>
                                                                <label>10</label></td>

                                                            <td>
                                                                <label>11</label></td>
                                                            <td>
                                                                <label>12</label></td>
                                                            <td>
                                                                <label>13</label></td>
                                                            <td>
                                                                <label>14</label></td>
                                                            <td>
                                                                <label>15</label></td>
                                                            <td>
                                                                <label>16</label></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="placeholder" runat="server"></tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%#Eval("srno")%>
                                                            <asp:HiddenField ID="hdnVchSQN" runat="server" Value='<%# Eval("VOUCHER_SQN") %>' />
                                                            <asp:HiddenField ID="hdnVCH_No" runat="server" Value='<%# Eval("VOUCHER_NO") %>' />
                                                        </td>
                                                        <td><%#Eval("PANNO")%>
                                                            <asp:HiddenField ID="hdnPanno" runat="server" Value='<%#Eval("PANNO")%>' />
                                                        </td>
                                                        <td><%#Eval("Party_Name_Address")%>
                                                            <asp:HiddenField ID="hdnPartyno" runat="server" Value='<%# Eval("PARTY_NO") %>' />
                                                            <asp:HiddenField ID="hdnPartyName_Address" runat="server" Value='<%#Eval("Party_Name_Address")%>' />
                                                        </td>
                                                        <td><%#Eval("Pay_Date")%>
                                                            <asp:HiddenField ID="hdnPayDate" runat="server" Value='<%#Eval("Pay_Date")%>' />
                                                        </td>
                                                        <td><%#Eval("Bill_Amt")%>
                                                            <asp:HiddenField ID="hdnBillDate" runat="server" Value='<%#Eval("Bill_Amt")%>' />
                                                        </td>
                                                        <td><%#Eval("TDS")%>
                                                            <asp:HiddenField ID="hdnTDS" runat="server" Value='<%#Eval("TDS")%>' />
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtSurcharge" Style="width: 120px" runat="server" Text='<%#Eval("SURCHARGE")%>'></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtSurcharge"
                                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                        </td>
                                                             <td>
                                                                 <asp:TextBox ID="txtEdcess" Style="width: 120px" runat="server" Text='<%#Eval("EDCESS")%>'></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEdcess"
                                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                        </td>
                                                             <td>
                                                                 <asp:TextBox ID="txtTotalTaxDeposit" Style="width: 120px; text-transform: uppercase" runat="server" Text='<%#Eval("TOTAL_TAX_DEPOSIT")%>'></asp:TextBox></td>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTotalTaxDeposit"
                                                            FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                        </td>

                                                             <td><%#Eval("TDS_Date")%>
                                                                 <asp:HiddenField ID="hdnTDSDate" runat="server" Value='<%#Eval("TDS_Date")%>' />
                                                             </td>

                                                        <td><%#Eval("SECTION_NAME")%>
                                                            <asp:HiddenField ID="hdnSectionName" runat="server" Value='<%#Eval("SECTION_NAME")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtChallanNo" Text='<%#Eval("CHALLAN_NO")%>' Style="width: 120px; text-transform: uppercase" runat="server"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtChallanAmt" runat="server" Style="width: 120px" Text='<%#Eval("CHALLAN_AMT")%>'></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtChallanAmt"
                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />

                                                        </td>
                                                        <td>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon" id="imgCalholidayDt">
                                                                    <i class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtChallanDate" runat="server" MaxLength="10" Style="width: 120px" CssClass="form-control" TabIndex="5" Text='<%# Eval("CHALLAN_DATE")%>'
                                                                    ToolTip="Enter  Date" />
                                                                <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtChallanDate" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                                    EnableViewState="true">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="medepdt" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtChallanDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="medepdt" ControlToValidate="txtChallanDate"
                                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Challan Date In [dd/MM/yyyy] format"
                                                                    InvalidValueMessage="Challan Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                                    Text="*" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>
                                                            </div>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBSRCode" Text='<%#Eval("BSRCODE")%>' Style="width: 120px; text-transform: uppercase" runat="server"></asp:TextBox></td>

                                                        <td>
                                                            <asp:TextBox ID="txtReceiptNo" runat="server" Style="width: 120px" Text='<%# Eval("RECEIPT_NO")%>'></asp:TextBox></td>
                                                        <td id="tdRAte" runat="server" visible="false"><%#Eval("Rate")%>
                                                            <asp:HiddenField ID="hdnRate" runat="server" Value='<%#Eval("Rate")%>' />
                                                        </td>
                                                        <td id="tdWorkNature" runat="server" visible="false"><%#Eval("Work_Nature")%>
                                                            <asp:HiddenField ID="hdnWorkNature" runat="server" Value='<%#Eval("Work_Nature")%>' />
                                                        </td>
                                                        <td id="tdChqNo" runat="server" visible="false"><%#Eval("CHQ_NO")%>
                                                            <asp:HiddenField ID="hdnChqNo" runat="server" Value='<%#Eval("CHQ_NO")%>' />
                                                        </td>

                                                        <td id="tdBankName" runat="server" visible="false">
                                                            <asp:TextBox ID="txtBankName" Text='<%#Eval("BANK_NAME")%>' Style="width: 200px; text-transform: uppercase" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                                            </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </asp:Panel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" />
                                    <asp:Button ID="btnClearGrid" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnClearGrid_Click" />
                                    <asp:ValidationSummary ID="ValidationSummury" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />

                                </div>

                            </asp:Panel>

                            <div class="col-12">
                                <div class="table- table-responsive">
                                    <asp:GridView ID="grdTDSExcel" runat="server" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered table-hover" CellPadding="3" CellSpacing="2">
                                        <Columns>
                                            <asp:BoundField DataField="srno" HeaderText="Party Reference No." ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle Wrap="False" Width="10%" HorizontalAlign="Center" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VOUCHER_NO" HeaderText="VOUCHER NO" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Pay_Date" HeaderText="Voucher Date" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PANNO" HeaderText="PAN NO" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="20%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Party_Name_Address" HeaderText="Party Name" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="30%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="30%" Font-Size="Smaller" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Bill_Amt" HeaderText="Tds On Amount" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TDS" HeaderText="TDS" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SURCHARGE" HeaderText="Surcharge" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EDCESS" HeaderText="ED CESS" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TOTAL_TAX_DEPOSIT" HeaderText="Total Tax Deposited ED" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="TDS_Date" HeaderText="Date of TDS deposited" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SECTION_NAME" HeaderText="Section Code" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PARTICULARS" HeaderText="Narration" ControlStyle-Font-Size="Smaller">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="Rate" HeaderText="Rate at which deducted" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Left" Width="5%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Work_Nature" HeaderText="Nature of Work" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CHQ_NO" HeaderText="Cheque No" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>--%>


                                            <asp:BoundField DataField="CHALLAN_NO" HeaderText="Challan No" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHALLAN_AMT" HeaderText="Challan Amount" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CHALLAN_DATE" HeaderText="Challan Date" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <%--  <asp:BoundField DataField="BANK_NAME" HeaderText="Name of the Bank" ControlStyle-Font-Size="Smaller">
                                            <HeaderStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                            <ItemStyle HorizontalAlign="Right" Width="10%" Font-Size="Smaller" />
                                        </asp:BoundField>--%>
                                            <asp:BoundField DataField="BSRCODE" HeaderText="BSRCODE" ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RECEIPT_NO" HeaderText="Receipt No." ControlStyle-Font-Size="Smaller" Visible="false">
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                                <ItemStyle HorizontalAlign="Center" Width="10%" Font-Size="Smaller" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="bg-light-blue" Width="100%" ForeColor="#000" HorizontalAlign="Center" />

                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShowExcel" />
            <asp:PostBackTrigger ControlID="btnShowPDF" />
            <asp:PostBackTrigger ControlID="btnbeforeExcel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
