<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="STR_ACCEPTANCE.aspx.cs" Inherits="STORES_Transactions_STR_ACCEPTANCE"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
    <%-- Flash the text/border red and fade in the "close" button --%>

    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />

    <%-- <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ITEM ACCEPTANCE</h3>

                </div>

                <form role="form">
                    <div class="box-body">
                        <div class="col-md-12">
                            <asp:Panel ID="pnlAcceptance" runat="server"
                                Visible="true">
                                <div class="panel panel-info">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Select Issue Slip No. For Acceptance</h5>
                                        </div>
                                    </div>

                                    <div class="panel-body">
                                        <asp:Label ID="Label1" runat="server" SkinID="Msglbl"></asp:Label>
                                        <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>
                                        <asp:HiddenField ID="hdnDsrRowCount" runat="server" Value="0" />

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="form-group col-md-10" id="divrdl" runat="server" visible="false">
                                                    <label></label>
                                                    <asp:RadioButtonList ID="radReq" runat="server" TabIndex="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="radReq_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                        <asp:ListItem Text="Requisition No." Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Issue Slip No." Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="form-group col-md-10" id="tdIssue" runat="server" visible="false">
                                                    <label>Issue No.:<span style="color: Red">*</span></label>
                                                    <asp:DropDownList ID="ddlIssueSlipNo" TabIndex="2" ToolTip="Select Issue No" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlIssueSlipNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvtxtIssueSlipNo" runat="server" ControlToValidate="ddlIssueSlipNo"
                                                        Display="None" ErrorMessage="Please Enter Issue Slip No." ValidationGroup="StoreReq"
                                                        InitialValue="0"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-md-10" id="tdReq" runat="server" visible="false">
                                                    <label>Requisition No.:<span style="color: Red">*</span></label>

                                                    <asp:DropDownList ID="ddlReq" runat="server" TabIndex="3" ToolTip="Select Requisition No" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlReq_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvReqno" runat="server" ControlToValidate="ddlReq"
                                                        Display="None" ErrorMessage="Please Enter Requisition No." ValidationGroup="StoreReq"
                                                        InitialValue="0"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Acceptance Date :</label>

                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtItemAcceptDate" TabIndex="4" ToolTip="Enter Acceptance Date" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />
                                                        </div>

                                                        <ajaxToolKit:CalendarExtender ID="cetxtIndentSlipDate" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtItemAcceptDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtItemIssueDate" runat="server" ControlToValidate="txtItemAcceptDate"
                                                            Display="None" ErrorMessage="Please Select Item Issue Date in (dd/MM/yyyy Format)"
                                                            SetFocusOnError="True" ValidationGroup="StoreReq">
                                                        </asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="meAcceptDate" runat="server" AcceptNegative="Left"
                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                            MessageValidatorTip="true" TargetControlID="txtItemAcceptDate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meAcceptDate"
                                                            ControlToValidate="txtItemAcceptDate" Display="None" EmptyValueBlurredText="Empty"
                                                            EmptyValueMessage="Please Issue Date" InvalidValueBlurredMessage="Invalid Date"
                                                            InvalidValueMessage="Issue Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                                            TooltipMessage="Please Enter Issue Date" ValidationGroup="StoreReq" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-10">
                                                    <label>Remark :</label>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    <asp:ValidationSummary ID="vasAcc" runat="server" ValidationGroup="StoreReq"
                                                        ShowMessageBox="True" ShowSummary="False" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <asp:Panel runat="server" ID="pnlreq" Visible="true">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Issued Item Details</h5>
                                        </div>
                                    </div>
                                    <div class="col-md-12 ">
                                        <div class="col-md-8">
                                            <asp:ListView ID="lvitemReq" runat="server">
                                                <EmptyDataTemplate>
                                                    <br />
                                                    <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                </EmptyDataTemplate>
                                                <LayoutTemplate>

                                                    <div id="demo-grid" class="vista-grid">


                                                        <table class="table table-bordered table-hover table-responsive" id="tbllvitemReq">
                                                            <tr class="bg-light-blue">
                                                                <th>Select
                                                                </th>
                                                                <th>Item Name
                                                                </th>
                                                                <th>Requested Quantity
                                                                </th>
                                                                <th>Issued Quantity
                                                                </th>
                                                                <th>Accepted Quantity
                                                                </th>
                                                                <%-- <th width="10%"></th>--%>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkItmSelect" runat="server" ToolTip='<%# Eval("MIGNO")%>' AutoPostBack="true" OnCheckedChanged="chkIssueSelect_CheckedChanged" />
                                                            <asp:HiddenField ID="hdnMigno" runat="server" Value='<%# Eval("MIGNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("REQ_QTY")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIssueQty" runat="server" Text='<%# Eval("ISSUED_QTY")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccQty" runat="server" Text='<%# Eval("ACC_QTY")%>' CssClass="form-control"
                                                                onkeyup="IsNumeric(this);" Enabled="true"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtAccQty"
                                                                Display="None" ErrorMessage="Please Enter No. of Accepted Quantity" SetFocusOnError="True"
                                                                ValidationGroup="StoreReq">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="10%" id="tdReject" runat="server" visible="false">
                                                            <asp:Button ID="btnReject" runat="server" CssClass="btn btn-warning" Text="Reject" OnClick="btnReject_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:ListView ID="lvDsr" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="lgv1">
                                                        <label>Items Serial Number List</label>

                                                        <table class="table table-bordered table-hover table-responsive" style="margin-bottom: 0px;" id="tbllvDsr">
                                                            <thead>
                                                                <tr class="bg-light-blue">

                                                                    <th style="width: 5%">Select
                                                                    </th>

                                                                    <th style="width: 70%">Item Serial Number
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 100px;">
                                                        <div id="demo-grid">
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 20%">
                                                            <asp:CheckBox ID="chkDsrselect" runat="server" ToolTip='<%# Eval("INVDINO")%>' />
                                                            <asp:HiddenField ID="hdDsrItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                                        </td>
                                                        <td style="width: 80%">
                                                            <asp:Label ID="lblDsrNumber" runat="server" Text='<%# Eval("DSR_NUMBER")%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnlReject" Visible="false">
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="panel panel-info">
                                                <div class="panel-heading">Rejected Item Details </div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-12">

                                                        <div class="form-group col-md-3">
                                                            <label>Item Name :</label>

                                                            <asp:Label ID="lblIName" runat="server" Text=""></asp:Label>

                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label>No. of Item Issued :</label>
                                                            <asp:Label ID="lblIssuedItem" runat="server" Text=""></asp:Label></td>
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label>No. of Item To Be Rejected :</label>
                                                            <asp:TextBox ID="txtRejectItems" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter No. of Item To Be Rejected" Text="" onkeyup="IsNumeric(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvRejQty" runat="server" ControlToValidate="txtRejectItems"
                                                                Display="None" ErrorMessage="Please Enter No. of Rejected Quantity" SetFocusOnError="True"
                                                                ValidationGroup="rejItemSubmit">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label>Reason :</label>
                                                            <asp:TextBox ID="txtRejReason" runat="server" TabIndex="7" ToolTip="Enter Reason" CssClass="form-control" Text="" TextMode="MultiLine"></asp:TextBox><asp:ValidationSummary ID="vsRejItems" runat="server" ValidationGroup="rejItemSubmit"
                                                                ShowMessageBox="True" ShowSummary="False" />
                                                        </div>


                                                    </div>



                                                </div>

                                            </div>
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                    CausesValidation="true" ValidationGroup="rejItemSubmit" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Back" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                            </div>
                                            <div class="table-responsive col-md-12">
                                                <asp:ListView ID="lvRejItems" runat="server">
                                                    <EmptyDataTemplate>
                                                        <br />
                                                        <center>
                                                                <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                    </center>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <div class="titlebar">
                                                                <h4>Rejected Item Details</h4>
                                                            </div>
                                                            <table class="table table-bordered table-hover table-responsive">
                                                                <tr class="bg-light-blue">
                                                                    <th width="15%">Action
                                                                    </th>
                                                                    <th>Item Name
                                                                    </th>
                                                                    <th width="15%">Issued Quantity
                                                                    </th>
                                                                    <th>Rejected Quantity
                                                                    </th>
                                                                    <th>Reason
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td width="15%">
                                                                <asp:ImageButton ID="btnRejEdit" runat="server" ImageUrl="~/IMAGES/edit.gif" CommandArgument='<%# Eval("REJTRNO")%>'
                                                                    OnClick="btnRejEdit_Click" />
                                                                <asp:ImageButton ID="btnRejDelete" runat="server" ImageUrl="~/IMAGES/delete.gif"
                                                                    CommandArgument='<%# Eval("REJTRNO")%>' OnClick="btnRejDeletet_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                            </td>
                                                            <td width="15%">
                                                                <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("ISSUED_QTY")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRejQty" runat="server" Text='<%# Eval("REJ_QTY")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblRejReason" runat="server" Text='<%# Eval("REASON")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>

                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnAccept" runat="server" Text="Accept Slip" CssClass="btn btn-primary" CausesValidation="true"
                                        ValidationGroup="StoreReq" OnClick="btnAccept_Click" OnClientClick="return ValidateSubmit(this);" />
                                    <%-- <asp:Button ID="btnRejectSlip" runat="server" Text="Reject Slip" CssClass="btn btn-warning" CausesValidation="true"
                                                OnClick="btnRejectSlip_Click" ValidationGroup="StoreReq" />--%>
                                    <asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false" CssClass="btn btn-info" Visible="false"
                                        OnClick="btnReport_Click" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlReport" runat="server">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <div class="panel panel-info">
                                            <div class="panel-heading">Issue Slip Report</div>
                                            <div class="panel-body">
                                                <div class="form-group col-md-12">
                                                    <div class="form-group col-md-5">
                                                        <label>Select Issue Slip No. :</label>
                                                        <asp:DropDownList ID="ddlIssueSlipNoRpt" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                            AutoPostBack="True" EnableTheming="False" TabIndex="1" ToolTip="Select Issue Slip No" Visible="true">
                                                            <asp:ListItem Selected="True" Text="Please select Issue Slip No." Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvIssueSlipNoRpt" runat="server" ControlToValidate="ddlIssueSlipNoRpt"
                                                            Display="None" ErrorMessage="Please Select Issue Slip No." ValidationGroup="report"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnPrintRpt" runat="server" CausesValidation="true"
                                                OnClick="btnPrintRpt_Click" Text="Print Report" ValidationGroup="report"
                                                CssClass="btn btn-info" TabIndex="2" />

                                            <asp:Button ID="btnCancelRpt" runat="server" CausesValidation="false"
                                                OnClick="btnCancelRpt_Click1" Text="Cancel" CssClass="btn btn-warning" TabIndex="3" />
                                            <asp:ValidationSummary ID="vsReport" runat="server" ValidationGroup="report"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </td>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>


                    </div>
                </form>
            </div>

        </div>

    </div>

    <div class="col-md-12">
    </div>

    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript" language="javascript">

        function ValidateSubmit(crl) {
            debugger;
            if (document.getElementById('<%=ddlIssueSlipNo.ClientID%>').value == 0) {
                alert('Please Select Issue No.');
                return;
            }
            var checkItemCount = 0;
            var Rowcount = Number($("#tbllvitemReq tr").length);

            for (var i = 0; i < Rowcount - 1; i++) {
                debugger;
                var chkItemSel = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_chkItmSelect');
                var Migno = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_hdnMigno').value;
                var IssueQty = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_lblIssueQty').innerText;
                var AccQty = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_txtAccQty').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_txtAccQty').value;
                var ItemNo = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_hdItemno').value;
                var ItemName = document.getElementById('ctl00_ContentPlaceHolder1_lvitemReq_ctrl' + i + '_lblItemName').innerText;

                if (chkItemSel.checked) {

                    if (AccQty < 1) {
                        alert('Please Enter Accepted Quantity For Item : ' + ItemName);
                        return false;
                    }


                    if (Number(AccQty) > Number(IssueQty)) {
                        alert('Accepted Quantity Should Not Be Greater Than Issued Quantity');
                        return false;
                    }
                    if (Migno == 1) {//Fixed Item
                        var DsrRowCount = document.getElementById('<%=hdnDsrRowCount.ClientID%>').value;
                        var DsrItemSelCount = 0;
                        for (var j = 0; j < DsrRowCount; j++) {
                            var Dsrselect = document.getElementById('ctl00_ContentPlaceHolder1_lvDsr_ctrl' + j + '_chkDsrselect');
                            var DsrItemno = document.getElementById('ctl00_ContentPlaceHolder1_lvDsr_ctrl' + j + '_hdDsrItemno').value;

                            if (Dsrselect.checked && ItemNo == DsrItemno) {
                                DsrItemSelCount++;
                            }
                        }
                        if (DsrItemSelCount == 0) {
                            alert('Please Select Item Serial Number\'s To Accept Item : ' + ItemName);
                            return false;
                        }
                        if (Number(AccQty) != Number(DsrItemSelCount)) {

                            alert('Number Of Selected Item Serial Number\'s Are Not Matching With Accepted Quantity For Item : ' + ItemName);
                            return false;
                        }
                    }
                    checkItemCount++;
                }
            }
            if (checkItemCount == 0) {
                alert('Please Select At Least One Item');
                return false;
            }

        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    alert("Please type in Numeric Format.");
                    document.getElementById(textbox.id).value = "0";
                }
            }
        }

        function check(me) {

            if (document.getElementById("" + me.id + "").value != "Y" && document.getElementById("" + me.id + "").value != "N") {
                alert("Please Enter Y Or N ");
                document.getElementById("" + me.id + "").value = "";
                document.getElementById("" + me.id + "").focus();
            }

        }
    </script>

    <div id="divMsg" runat="server">
    </div>


</asp:Content>
