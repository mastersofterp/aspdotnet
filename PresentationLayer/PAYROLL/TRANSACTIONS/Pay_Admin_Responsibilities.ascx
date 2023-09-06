<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_Admin_Responsibilities.ascx.cs" Inherits="PAYROLL_TRANSACTIONS_Pay_Admin_Responsibilities" %>
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
                                <div class="panel panel-heading">Admiministrative Responsibilities</div>
                                <div class="panel panel-body">
                                    <div class="form-group col-md-12">
                                        <label>Responsibility :</label>
                                        <asp:TextBox ID="txtResponsibility" runat="server" CssClass="form-control"
                                            ToolTip="Enter Responsibility" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvResponsibility" runat="server" ControlToValidate="txtResponsibility"
                                            Display="None" ErrorMessage="Please Enter Responsibility" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Organization :</label>
                                        <asp:TextBox ID="txtOrganization" runat="server" CssClass="form-control"
                                            ToolTip="Enter Organization" TabIndex="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" ControlToValidate="txtOrganization"
                                            Display="None" ErrorMessage="Please Enter Organization" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>From Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                TabIndex="6" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select From Date in (dd/MM/yyyy Format)"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter mm/dd/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>To Date :</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                TabIndex="7" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Select To Date in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                            <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="Traning To Date  Should be Greater than  or equal to From Date"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Remarks If Any :</label>
                                        <asp:TextBox ID="txtReMarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any"
                                            TabIndex="8" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                            Display="None" ErrorMessage="Please Enter ReMarks If Any" ValidationGroup="ServiceBook"
                                            SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Upload Document :</label>
                                        <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="9" />
                                    </div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="10"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="11"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
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
                            <asp:ListView ID="lvAdminResponsibilities" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows Admiministrative Responsibilities"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Admiministrative Responsibilities Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Responsibility
                                                    </th>
                                                    <th>Organization
                                                    </th>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ADMINTRXNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ADMINTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("Responsibility")%>
                                        </td>
                                        <td>
                                            <%# Eval("ORGANIZATION")%>
                                        </td>
                                        <td>
                                            <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ADMINTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
        <td valign="top" width="50%">
            <%--<asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 15px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Admiministrative Responsibilities</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Responsibility :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtResponsibility" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvResponsibility" runat="server" ControlToValidate="txtResponsibility"
                                    Display="None" ErrorMessage="Please Enter Responsibility" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Organization :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrganization" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" ControlToValidate="txtOrganization"
                                    Display="None" ErrorMessage="Please Enter Organization" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">From Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtFromDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgCal" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                    PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                    Display="None" ErrorMessage="Please Select From Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter mm/dd/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">To Date :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtToDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                    Display="None" ErrorMessage="Please Select To Date in (dd/MM/yyyy Format)" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                    ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="Traning To Date  Should be Greater than  or equal to From Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Remarks If Any :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtReMarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                    Display="None" ErrorMessage="Please Enter ReMarks If Any" ValidationGroup="ServiceBook"
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
                        <%--<asp:ListView ID="lvAdminResponsibilities" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows Admiministrative Responsibilities"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Admiministrative Responsibilities
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">Responsibility
                                                </th>
                                                <th width="10%">Organization
                                                </th>
                                                <th width="10%">From Date
                                                </th>
                                                <th width="10%">To Date
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ADMINTRXNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ADMINTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("Responsibility")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ORGANIZATION")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ADMINTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ADMINTRXNO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ADMINTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("Responsibility")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ORGANIZATION")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ADMINTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
</script>
