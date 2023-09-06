<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="StudentWiseReconcileTransfer.aspx.cs" Inherits="ACCOUNT_StudentWiseReconcileTransfer" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 150px;
        }

        .auto-style1 {
            width: 161px;
        }

        .auto-style2 {
            width: 192px;
        }
    </style>

    <link href="../Css/UpdateProgress.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        function totAllSubjects(headchk) {
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            hdfTot.value = "0";
	    
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (e.name.endsWith('chkFeesTransfer')) {
                        if (headchk.checked == true) {
                            e.checked = true;
                            hdfTot.value = Number(hdfTot.value) + 1;
                        }
                        else {
                            e.checked = false;
                            label.innerHTML = "0";
                            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
                        }

                    }
                }
            }
            label.innerHTML = hdfTot.value;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            if (headchk.checked == false) {
                hdfTot.value = "0";
                var count = $("[type='checkbox']:checked").length;
                label.innerHTML = count;
                //label.innerHTML = "0";
                document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
            }
        }

        function validateAssign() {
            debugger;
            var label = document.getElementById("<%=lblChkCount.ClientID %>");
            var count = $("[type='checkbox']:checked").length;
            label.innerHTML = count;
            document.getElementById("<%=lblChkCount.ClientID %>").value = label.innerHTML;
        }

    </script>

    <script src="../jquery/jquery-1.10.2.js" type="text/javascript"></script>

    <div style="z-index: 1; position: fixed; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBank"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updBank" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">FEES TRANSFER STUDENTWISE
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" /></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div id="divCompName" runat="server" style="font-size: x-large; text-align: center">
                                </div>
                                <asp:Panel ID="pnl" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">Fees Transfer</div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <label>Note : </label>
                                                    <span style="color: red; font-weight: bold">Please select only 200 student for Single Voucher</span>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-3">
                                                    <label>From Date:</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>To Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtTodate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" PopupPosition="BottomLeft"
                                                            TargetControlID="txtTodate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtTodate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Degree :</label>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" ValidationGroup="Submit" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                                        Width="250px">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    <%--<asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" InitialValue="0" ControlToValidate="ddlDegree"
                                                               ErrorMessage="Please Select Degree" ValidationGroup="Submit" Display="None">
                                                        </asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>Receipt Type :</label>
                                                    <asp:DropDownList ID="ddlReceipt" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit" AutoPostBack="true" OnSelectedIndexChanged="ddlReceipt_SelectedIndexChanged"
                                                        CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlReceipt" runat="server" InitialValue="Please Select"
                                                        ControlToValidate="ddlReceipt" ErrorMessage="Please Select Receipt Type" ValidationGroup="Submit"
                                                        Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-3">
                                                    <label>Branch :</label>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="false">
                                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <label>College Code :</label>
                                                    <asp:DropDownList ID="ddlAided_NoAided" AppendDataBoundItems="true" runat="server" CssClass="form-control" AutoPostBack="false">
                                                        <asp:ListItem Value="0">Please select</asp:ListItem>
                                                        <asp:ListItem Value="1">E021</asp:ListItem>
                                                        <asp:ListItem Value="2">E057</asp:ListItem>
                                                        <asp:ListItem Value="3">B292</asp:ListItem>
                                                        <asp:ListItem Value="4">B292BC</asp:ListItem>
                                                        <asp:ListItem Value="5">B292BR</asp:ListItem>
                                                        <asp:ListItem Value="6">E721 </asp:ListItem>
                                                        <asp:ListItem Value="7">B292BD</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 form-group row">
                                        <div class="text-center">
                                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ValidationGroup="Submit"
                                                ShowMessageBox="true" ShowSummary="false" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row" id="trTotal" runat="server" visible="false">
                                    <div class="col-md-5"></div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblCount" runat="server" Text="Receipt Count :" Font-Bold="true" Font-Size="Medium">
                                        </asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="lblChkCount" runat="server" Font-Bold="true" Font-Size="Medium">
                                        </asp:Label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-md-12" id="Panel1" runat="server" visible="false">
                                        <b>
                                            <h4>Student Details :</h4>
                                        </b>
                                        <asp:Panel ID="pnlList" runat="server" Height="420px" ScrollBars="Vertical">
                                            <asp:ListView ID="lvFeeTransfer" runat="server" OnSelectedIndexChanged="lvFeeTransfer_SelectedIndexChanged">
                                                <LayoutTemplate>
                                                    <%-- <div class="ui-widget-header">
                                                                        Fees Transfer
                                                                    </div>--%>
                                                    <table id="tblHead" class="table table-responsive table-bordered">
                                                        <thead style="font-size: 13px">
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: left; padding-left: 3px">
                                                                    <asp:CheckBox ID="chkBoxFeesTransfer" runat="server" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th style="text-align: left;">NAME
                                                                </th>
                                                                <th style="text-align: left;">REC_NO
                                                                </th>
                                                                <th style="text-align: left;">REC_DATE
                                                                </th>
                                                                <th style="text-align: left;">DD/CHEQUE/NEFT NO.
                                                                </th>
                                                                <th style="text-align: left;">AMOUNT
                                                                </th>
                                                                <%--<th style="text-align: left; width: 10%">BANK AMOUNT</th>
                                                                <th style="text-align: left; width: 11%">CASH AMOUNT</th>--%>
                                                                <th style="text-align: left;">BRANCH
                                                                </th>
                                                                <th style="text-align: left; display: none">RECONCILE DATE
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <asp:CheckBox ID="chkFeesTransfer" runat="server" onclick="validateAssign()" ToolTip='<%#Eval("Id") %>' />
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblRecptCode" runat="server" Text='<%# Eval("REC_NO") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblRecptDate" runat="server" Text='<%# Eval("REC_DT", "{0:dd/MM/yyyy}")  %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DEGREENAME")  %>' Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblCheckDDNeftNo" runat="server" Text='<%# Eval("DD_Cheque_NEFT_No") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblDob" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("DD_AMT") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 10%">
                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("CASH_AMT") %>'></asp:Label>
                                                        </td>--%>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblPassword" runat="server" Text='<%# Eval("BRANCH") %>'></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; display: none">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal12" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtReconcileDate" Style="text-align: right; width: 100px" runat="server" ToolTip="Please Enter Reconcile Date"></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                                    Format="dd/MM/yyyy" PopupButtonID="imgCal12" PopupPosition="BottomRight" TargetControlID="txtReconcileDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtReconcileDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="form-group row">
                                    <div class="col-md-3">
                                        <label>Select Ledger :</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlLedger" runat="server" AppendDataBoundItems="true" ValidationGroup="Submit"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                        <asp:RequiredFieldValidator ID="rfvLedger" runat="server" InitialValue="0" ControlToValidate="ddlLedger"
                                            ErrorMessage="Please Select Ledger" ValidationGroup="Save" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group row" id="TrFees" runat="server" visible="false">
                                    <div id="divTransfer" runat="server">
                                        <div class="col-md-6">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    Fees Heads
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="ScrollPanel" runat="server" Width="100%">
                                                            <asp:ListView ID="lstFees" runat="server">
                                                                <LayoutTemplate>
                                                                    <%--<div class="vista-grid">--%>
                                                                    <table id="tblHead" class="table table-responsive table-bordered">
                                                                        <thead style="font-size: 13px">
                                                                            <tr class="bg-light-blue">
                                                                                <th style="text-align: left;">F.NO
                                                                                </th>
                                                                                <th style="text-align: left;">FEE HEAD
                                                                                </th>
                                                                                <th style="text-align: left;">AMOUNT
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                    <%--</div>--%>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblFeeHeadsNo" runat="server" Text='<%# Eval("FEE_HEAD") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblFeeHeads" runat="server" Text='<%# Eval("FEE_LONGNAME") %>'></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: left;">
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">
                                                    Cash/Bank
                                                </div>
                                                <div class="panel-body">
                                                    <%--<div class="form-group row">
                                                        <div class="col-md-3">
                                                            <label>Voucher Date :</label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>
                                                                <asp:TextBox ID="txtVoucherDate" Style="text-align: right" runat="server" CssClass="form-control" />
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                                                    TargetControlID="txtVoucherDate">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtVoucherDate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="form-group row">
                                                        <div class="col-md-3">
                                                            <label>Total Amount</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblTotal" runat="server" CssClass="form-control" ForeColor="Red" Text="0" Font-Bold="true"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="text-center">
                                        <asp:Button ID="btnTransfer" runat="server" CssClass="btn btn-primary" Text="Transfer Fees" ValidationGroup="Save"
                                            OnClick="btnTransfer_Click" Visible="false" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
