<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_LoansAndAdvance.ascx.cs"
    Inherits="PayRoll_Pay_LoansAndAdvance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="panel panel-info">
                                <div class="panel panel-heading">Loans & Advance</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-6">
                                        <label>Loan Name :</label>
                                        <asp:DropDownList ID="ddlLoanName" AppendDataBoundItems="true" runat="server" CssClass="form-control"
                                            ToolTip="Select Loan Name" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLoanName" runat="server" ControlToValidate="ddlLoanName"
                                            Display="None" SetFocusOnError="True" ErrorMessage="Please Select Loan Name "
                                            ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Order No :</label>
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" ToolTip="Enter Order No" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                            Display="None" ErrorMessage="Please Order No" ValidationGroup="ServiceBook" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Amount :</label>
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" ToolTip="Enter Amount" TabIndex="6"
                                            onkeyup="return validateNumeric(this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvfAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ErrorMessage="Please Enter Amount " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Rate Of Interest :</label>
                                        <asp:TextBox ID="txtRateOfInterest" runat="server" CssClass="form-control" ToolTip="Enter Rate Of Interest"
                                            onkeyup="return validateNumeric(this);" TabIndex="7"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRateOfInterest" runat="server" ControlToValidate="txtRateOfInterest"
                                            Display="None" ErrorMessage="Please Rate Of Interest " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>No.Of InstallMent :</label>
                                        <asp:TextBox ID="txtNoOfInstallMent" runat="server" CssClass="form-control" ToolTip="Enter No.Of InstallMent"
                                            onkeyup="return validateNumeric(this);" TabIndex="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtNoOfInstallMent" runat="server" ControlToValidate="txtNoOfInstallMent"
                                            Display="None" ErrorMessage="Please Enter No.Of InstallMent " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Loan Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgLoanDate" runat="server" ImageUrl="~/images/calendar.png"
                                                    Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtLoanDate" runat="server" CssClass="form-control" ToolTip="Enter Loan Date"
                                                TabIndex="9" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceLoanDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtLoanDate" PopupButtonID="imgLoanDate" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvLoanDate" runat="server" ControlToValidate="txtLoanDate"
                                                Display="None" ErrorMessage="Please Select LoanDate in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meLoanDate" runat="server" TargetControlID="txtLoanDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevLoanDate" runat="server" ControlExtender="meLoanDate"
                                                ControlToValidate="txtLoanDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="Loan Date is Invalid (Enter mm/dd/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Loan Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Remarks :</label>
                                        <asp:TextBox ID="txtReMarks" runat="server" CssClass="fomr-control" ToolTip="Enter Remarks If Any"
                                            TextMode="MultiLine" TabIndex="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                            Display="None" ErrorMessage="Please Enter Remarks " ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Upload Document :</label>
                                        <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="11" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="12"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-success" ToolTip="Click here to Submit" />&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="13"
                                                OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvLoanAndAdvance" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Loan And Advance of Employee"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Loan & Advance Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Lon.Name
                                                    </th>
                                                    <th>Ord. No
                                                    </th>
                                                    <th>Amt.
                                                    </th>
                                                    <th>ROI
                                                    </th>
                                                    <th>No.of inst.
                                                    </th>
                                                    <th>Loan Date
                                                    </th>
                                                    <th>Attachment
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("lno")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("lno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("LOANNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("orderno")%>
                                        </td>
                                        <td>
                                            <%# Eval("amount")%>
                                        </td>
                                        <td>
                                            <%# Eval("interest")%>
                                        </td>
                                        <td>
                                            <%# Eval("instal")%>
                                        </td>
                                        <td>
                                            <%# Eval("loandt", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td valign="top">
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Loans & Advance</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Loan Name :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlLoanName" AppendDataBoundItems="true" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvLoanName" runat="server" ControlToValidate="ddlLoanName"
                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Loan Name "
                                    ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Order No :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrderNo" runat="server" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                    Display="None" ErrorMessage="Please Order No" ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Amount :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAmount" runat="server" Width="100px" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfAmount" runat="server" ControlToValidate="txtAmount"
                                    Display="None" ErrorMessage="Please Enter Amount " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Rate Of Interest :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRateOfInterest" runat="server" Width="100px" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRateOfInterest" runat="server" ControlToValidate="txtRateOfInterest"
                                    Display="None" ErrorMessage="Please Rate Of Interest " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">No.Of InstallMent :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtNoOfInstallMent" runat="server" Width="100px" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtNoOfInstallMent" runat="server" ControlToValidate="txtNoOfInstallMent"
                                    Display="None" ErrorMessage="Please Enter No.Of InstallMent " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Loan Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtLoanDate" runat="server" Width="100px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgLoanDate" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceLoanDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtLoanDate" PopupButtonID="imgLoanDate" Enabled="true" EnableViewState="true"
                                    PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvLoanDate" runat="server" ControlToValidate="txtLoanDate"
                                    Display="None" ErrorMessage="Please Select LoanDate in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meLoanDate" runat="server" TargetControlID="txtLoanDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevLoanDate" runat="server" ControlExtender="meLoanDate"
                                    ControlToValidate="txtLoanDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="Loan Date is Invalid (Enter mm/dd/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter Loan Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Remarks :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtReMarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                    Display="None" ErrorMessage="Please Enter Remarks " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook"
                                    OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" Width="80px" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
        <td colspan="2" align="center" valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <%--<asp:ListView ID="lvLoanAndAdvance" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Loan And Advance of Employee"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Loan & Advance
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Lon.Name
                                                </th>
                                                <th width="10%">Ord. No
                                                </th>
                                                <th width="10%">Amt.
                                                </th>
                                                <th width="10%">ROI
                                                </th>
                                                <th width="10%">No.of inst.
                                                </th>
                                                <th width="10%">Loan Date
                                                </th>
                                                <th width="15%" align="left">Attachment
                                                </th>
                                            </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td width="10%" align="left">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("lno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("lno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("LOANNAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("orderno")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("amount")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("interest")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("instal")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("loandt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("lno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("lno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("LOANNAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("orderno")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("amount")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("interest")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("instal")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("loandt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>--%>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
<%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
<%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
    BackgroundCssClass="modalBackground" />
<div class="col-md-12">
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div class="text-center">
            <div class="modal-content">
                <div class="modal-body">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                    <div class="text-center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>

<script type="text/javascript">
    //  keeps track of the delete button for the row
    //  that is going to be removed
    var _source;
    // keep track of the popup div
    var _popup;

    function showConfirmDel(source) {
        this._source = source;
        this._popup = $find('mdlPopupDel');

        //  find the confirm ModalPopup and show it    
        this._popup.show();
    }

    function okDelClick() {
        //  find the confirm ModalPopup and hide it    
        this._popup.hide();
        //  use the cached button as the postback source
        __doPostBack(this._source.name, '');
    }

    function cancelDelClick() {
        //  find the confirm ModalPopup and hide it 
        this._popup.hide();
        //  clear the event source
        this._source = null;
        this._popup = null;
    }

    function validateNumeric(txt) {
        if (isNaN(txt.value)) {
            txt.value = txt.value.substring(0, (txt.value.length) - 1);
            txt.value = '';
            txt.focus = true;
            alert("Only Numeric Characters allowed !");
            return false;
        }
        else
            return true;
    }

    function validateAlphabet(txt) {
        var expAlphabet = /^[A-Za-z]+$/;
        if (txt.value.search(expAlphabet) == -1) {
            txt.value = txt.value.substring(0, (txt.value.length) - 1);
            txt.value = '';
            txt.focus = true;
            alert("Only Alphabets allowed!");
            return false;
        }
        else
            return true;
    }

</script>

