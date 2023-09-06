<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="BillCheckRequest.aspx.cs" Inherits="ACCOUNT_BillCheckRequest" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../Javascripts/overlib.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <style type="text/css">
        .account_compname
        {
            font-weight: bold;
            text-align: center;
        }
    </style>

    <script type="text/javascript">

        function totAllSubjects(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkBillid')) {
                        if (headchk.checked == true) {
                            e.checked = true;

                        }
                        else {
                            e.checked = false;

                        }

                    }
                }
            }
        }

    </script>

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
                                <h3 class="box-title">CHEQUE PRINTING REQUEST</h3>
                            </div>
                            <div id="divCompName" runat="server" style="text-align: center; font-size: x-large"></div>
                            <div class="box-body">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Cheque Printing Request</div>
                                        <div class="panel-body">
                                            <div class="panel panel-primary">
                                                <div class="panel-body">
                                                    <%--<div class="form-group row">
                                                        <div class="text-center">
                                                            <label style="font-size: 16px; padding: 1px;">JSS MahavidyaPeetha</label>
                                                        </div>
                                                        <div class="text-center">
                                                            <label style="font-size: 16px; font-style: italic; color: gray; padding: 1px;">JSS SCIENCE & TECHNOLOGY UNIVERSITY</label>
                                                        </div>
                                                        <div class="text-center">
                                                            <label style="font-size: 16px; padding: 1px;">JSS Technical Institututions Campus, MYSURU - 570006</label>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <div class="col-md-12">
                                                        <b>The Director (TED),<br />
                                                            JSS Mahavidyapeetha, JSS TI Campus<br />
                                                            Mysore :: 570 006</b>
                                                        <br />
                                                        <br />
                                                        Sir,<br />
                                                        <center>SUB : Request for issue a Cheques for Payment Reg. A/C <asp:Label ID="lblAccountno" runat="server" Font-Size="20px" Font-Bold="true" Text="3950002100000019"></asp:Label></center>
                                                    </div>
                                                    <center>----</center>
                                                    <br />--%>
                                                    <%--<div class="col-md-12">
                                                        With refernce to the above, we are enclosing the following bills,for your kind approval and to issue of a cheque
                                                from SJCE Recurring State, for payments to the concerned, at an early date.
                                                    </div>--%>
                                                    <div class="form-group">
                                                        <div class="col-md-2">
                                                            <label>Cheque Request No. :- </label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label ID="lblBillCheckNo" Font-Bold="true" runat="server" CssClass="form-control" Style="background-color: whitesmoke" Text="JSS S&TU/FB/32/CHQ/173/2018-19"></asp:Label>
                                                        </div>
                                                        <div class="col-md-2"></div>
                                                        <div class="col-md-2" style="text-align: right">
                                                            <label>Date :</label>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                    PopupButtonID="Image3" TargetControlID="txtApprovalDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtApprovalDate" CultureAMPMPlaceholder=""
                                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                                    Enabled="True">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="pnlChkBillList" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvBillCheck" runat="server" class="scroll" OnDataBound="lvBillCheck_DataBound">
                                                                <LayoutTemplate>
                                                                    <div class="vista-grid">
                                                                        <%--<div class="ui-widget-header">
                                                                    Bill Check List
                                                                </div>--%>
                                                                        <table id="tblHead" class="table table-responsive table-bordered">
                                                                            <thead style="font-size: 13px">
                                                                                <tr class="bg-light-blue">
                                                                                    <th style="text-align: center">Select All
                                                                                <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this);" />
                                                                                    </th>
                                                                                    <th style="text-align: center">Sl. No.
                                                                                    </th>
                                                                                    <th style="text-align: center">Voucher No.
                                                                                    </th>
                                                                                    <th style="text-align: center;display:none">Name to which Cheque is to be issued in favour of
                                                                                    </th>
                                                                                    <th style="text-align: center;">
                                                                                        Cheque to be written in the name of or cheque in favour of
                                                                                    </th>
                                                                                    <th style="text-align: center">Nature of service
                                                                                    </th>
                                                                                    <th style="text-align: center">Amount 
                                                                                        (Rs.)
                                                                                    </th>
                                                                                    <th style="text-align: center">Department
                                                                                    </th>
                                                                                    <th style="text-align: center">Approval Letter Details.
                                                                                    </th>
                                                                                    <th style="text-align: center; display: none">Cheque Number
                                                                                    </th>
                                                                                    <%--<th style="text-align: left; width: 5%">PAY TYPE
                                                                        </th>
                                                                        <th style="text-align: left; width: 10%">BRANCH
                                                                        </th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                            <tfoot>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <label>Total</label>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        <asp:Label ID="lblTotalAmt" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </td>
                                                                                    <td></td>
                                                                                    <td></td>
                                                                                    <td style="display: none"></td>
                                                                                </tr>
                                                                            </tfoot>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center">
                                                                            <asp:CheckBox ID="chkBillid" runat="server" onclick="validateAssign();" ToolTip='<%#Eval("ID") %>' />
                                                                            <asp:HiddenField ID="hdnBillNo" runat="server" Value='<%# Eval("BILL_NO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSlno" runat="server" Text='<%# Eval("SLNO") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnCompCode" runat="server" Value='<%# Eval("COMP_CODE") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VOUCHER_NO") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblNatureService" runat="server" Text='<%# Eval("NATURE_SERVICE")  %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: right">
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblDept" runat="server" Text='<%# Eval("DEPARTMENT") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblAprLetter" runat="server" Text='<%# Eval("APR_LETTER") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <asp:TextBox ID="txtCheckNo" runat="server" CssClass="form-control" ToolTip="Please enter check number"
                                                                                TabIndex="1" MaxLength="6"></asp:TextBox>
                                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtCheckNo"
                                                                                FilterType="Custom, Numbers" ValidChars="." Enabled="True" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                    <%--<div class="col-md-3">
                                                        Thanking You,
                                                    </div>
                                                    <div class="col-md-7">
                                                    </div>
                                                    <div class="col-md-2" style="text-align: right">
                                                        Yours faithfully,
                                                    </div>--%>
                                                </div>
                                                <%--<div class="panel-footer">
                                                    <div class="col-md-10">
                                                        Encl : As Above
                                                    </div>
                                                    <div class="Col-md-2" style="text-align: right">
                                                        FINANCE OFFICER
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <div class="form-group row">
                                                <div class="text-center">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="2" ToolTip="Save Record" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Reset" TabIndex="3" ToolTip="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlBank" runat="server">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            Cheque Request
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>Account :</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlCompAccount" AutoPostBack="true" runat="server" CssClass="form-control" ToolTip="Please Select Account"
                                                        OnSelectedIndexChanged="ddlCompAccount_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select Account</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <label>Select Bank :</label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" ToolTip="Please slect Bank">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnShow" Text="Show" runat="server" CssClass="btn btn-info" ToolTip="Click to show" OnClick="btnShow_Click" />
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
