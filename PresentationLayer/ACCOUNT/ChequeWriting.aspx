<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="ChequeWriting.aspx.cs" Inherits="ACCOUNT_ChequeWriting" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <style type="text/css">
        .modalBackground
        {
            background-color: black;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .modalPopup
        {
            background-color: white;
            padding-top: 10px;
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 20px;
            width: 500px;
            height: 500px;
            overflow-y: auto;
        }

        .ledgermodalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
        }

        .ledgermodalPopup
        {
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

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updChkBill"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="updChkBill" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div id="div2" runat="server"></div>
                            <div class="box-header with-border">
                                <h3 class="box-title">CHEQUE WRITING</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Cheque Writing</div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-4">
                                                    <label>Account :</label>
                                                    <asp:DropDownList ID="ddlCompAccount" AutoPostBack="true" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Please Select Account"
                                                        OnSelectedIndexChanged="ddlCompAccount_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <label>Select Bank:</label>
                                                    <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Please Select Bank"
                                                        OnSelectedIndexChanged="ddlBank_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4" style="display: none">
                                                    <label>Request No.</label>
                                                    <asp:DropDownList ID="ddlRequestNo" runat="server" CssClass="form-control" ToolTip="Please Select RequestNo" OnSelectedIndexChanged="ddlRequestNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--<div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                </div>
                                            </div>--%>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Repeater ID="rptRequestno" runat="server">
                                                        <HeaderTemplate>
                                                            <h4>Request Number</h4>
                                                            <table id="table1" class="table table-bordered table-striped dt-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Action</th>
                                                                        <th>Request Number</th>
                                                                        <th>Date</th>
                                                                        <th>Bank Name</th>
                                                                        <th>Account Name</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" CommandName='<%# Eval("CHKBILL_APPRNO") %>' CommandArgument='<%# Eval("SEQ_NO") %>'
                                                                        ToolTip="Click to Show Report" Text="Show" OnClick="btnShow_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("CHKBILL_APPRNO") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblReqdate" runat="server" Text='<%# Eval("CHECKDATE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BANKNAME") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAccountName" runat="server" Text='<%# Eval("ACCOUNTNAME") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <asp:Panel ID="pnlChqList" runat="server" Visible="false">
                                                    <div class="col-md-12">
                                                        <h4 class="box-title">Cheque Writing List for Request No. : -
                                                            <asp:Label ID="lblReqNo" runat="server" Font-Bold="true"></asp:Label></h4>
                                                        <asp:Repeater ID="rptChqPrintList" runat="server" OnItemDataBound="rptChqPrintList_ItemDataBound" OnItemCommand="rptChqPrintList_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table id="table2" class="table table-bordered table-striped dt-responsive nowrap">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Sl. NO
                                                                            </th>
                                                                            <th>Voucher No
                                                                            </th>
                                                                            <th>Cheque to be written in the name of or in favour of
                                                                            </th>
                                                                            <th>Nature of Service
                                                                            </th>
                                                                            <th style="text-align: right">Amount (Rs.)
                                                                            </th>
                                                                            <th>Department
                                                                            </th>
                                                                            <th>Cheque Number
                                                                            </th>
                                                                            <th style="text-align: center; width: 15%">Save and Print
                                                                            </th>
                                                                            <th style="text-align: center">Return Cheque
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SrNO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VOUCHER_NO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPayeeName" runat="server" Text='<%# Eval("PAYEE_NAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNatureService" runat="server" Text='<%# Eval("NATURE_SERVICE")%>'></asp:Label>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("NET_AMT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DEPARTMENT") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnBillNo" runat="server" Value='<%# Eval("BILL_NO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtChqNo" runat="server" CssClass="form-control" MaxLength="6" Text='<%# Eval("CHEQUE_NO") %>' TabIndex="1"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtChqNo"
                                                                            FilterType="Custom, Numbers" ValidChars="1234567890" Enabled="True" />
                                                                    </td>
                                                                    <td style="width: 15%; text-align: center">
                                                                        <asp:Button ID="btnSavePrint" CommandName="Save" runat="server" Text="Save" CssClass="btn btn-primary"
                                                                            ToolTip="Click to Save Cheque and Print" CommandArgument='<%# Eval("CHKID") %>' OnClick="btnSavePrint_Click" />
                                                                        <asp:Button ID="btnPrint" runat="server" CommandName='<%# Eval("CHKID") %>' Text="Print" CommandArgument='<%# Eval("CTRNO") %>' CssClass="btn btn-primary"
                                                                            ToolTip="Click to Print Cheque" OnClick="btnPrint_Click" Enabled='<%# Eval("CTRNO").ToString() == "0" ? false : true %>' />
                                                                    </td>
                                                                    <%--Style="display:none"--%>
                                                                    <td style="text-align: center">
                                                                        <asp:Button ID="btnReturnChq" runat="server" Text="Return" CssClass="btn btn-warning" CommandName='<%# Eval("CHKID") %>' 
                                                                            CommandArgument='<%# Eval("BILL_NO") %>' ToolTip='<%# Eval("CTRNO") %>' OnClick="btnReturnChq_Click" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                </tbody></table>
                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click to Reset" OnClick="btnReset_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Button ID="btnforPopUpModel1" Style="display: none" runat="server" Text="For Popup Model Box" />
                                <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="True" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                    Enabled="True">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnl" runat="server" Width="800px" BorderColor="#0066FF" meta:resourcekey="pnlResource1">
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    Cheque Printing
                                                </div>
                                                <div class="panel-body">
                                                    <asp:Repeater ID="rptPrint" runat="server" OnItemCommand="rptPrint_ItemCommand">
                                                        <HeaderTemplate>
                                                            <h4 class="box-title">Cheque Writing List</h4>
                                                            <table id="table2" class="table table-bordered table-striped dt-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>VchNo.
                                                                        </th>
                                                                        <th>Party
                                                                        </th>
                                                                        <th>Cheque NO.
                                                                        </th>
                                                                        <th>Cheque Date
                                                                        </th>
                                                                        <th>Ac Payee
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                        <th>Print
                                                                        </th>
                                                                        <th>Cancel
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("VNO") %>
                                                                </td>
                                                                <td><%# Eval("PARTYNAME") %>
                                                                </td>
                                                                <td><%# Eval("CHECKNO") %>
                                                                </td>
                                                                <td><%# Eval("CHECKDT") %>
                                                                </td>
                                                                <td><%# Eval("STAMP") %>
                                                                </td>
                                                                <td><%# Eval("AMOUNT") %>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnPrint" CommandName="Print" CommandArgument='<%# Eval("CTRNO") %>' Text="Print" runat="server"
                                                                        ToolTip="Print Cheque" CssClass="btn btn-info" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnCancel" CommandName="Cancel" Text="Cancel" runat="server"
                                                                        ToolTip="Print Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody></table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>



                                <ajaxToolKit:ModalPopupExtender ID="upd_ModelPopupReturn" runat="server" BackgroundCssClass="modalBackground"
                                    DropShadow="false" PopupControlID="pnlReturn" TargetControlID="btnforPopUpModel1" DynamicServicePath="" Enabled="true">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnlReturn" runat="server" Width="600px" BorderColor="#0066FF" meta:resourcekey="pnlResource1">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            Return Remarks for Bill No :-
                                            <asp:Label ID="lblBillNoPopup" runat="server"></asp:Label>
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-9">
                                                    <label>Request No :</label>
                                                    <asp:Label ID="lblReqNoPopup" runat="server" Style="background-color: whitesmoke" CssClass="form-control"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Voucher No :</label>
                                                    <asp:Label ID="lblVoucherNoPopup" runat="server" Style="background-color: whitesmoke" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <label>Payee Name :</label>
                                                    <asp:Label ID="lblPayeeNamePopup" runat="server" Style="background-color: whitesmoke" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <label>Remarks For Return<span style="color:red">*</span> :</label>
                                                    <asp:TextBox ID="txtReturnRemarks" runat="server" TextMode="MultiLine" ToolTip="Please Enter Return Remarks" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvVoucherNo" runat="server" ControlToValidate="txtReturnRemarks" Display="None" ErrorMessage="Please Enter Return Remark" 
                                                    SetFocusOnError="True" ValidationGroup="AccMoney1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="AccMoney1" CssClass="btn btn-primary" Text="Submit" ToolTip="Click to Submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" ToolTip="Click to Cancel" OnClick="btnCancel_Click1" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="AccMoney1" DisplayMode="List" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
