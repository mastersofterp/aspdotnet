<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_Leave.ascx.cs" Inherits="PayRoll_Pay_Leave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                   
                    <div class="col-md-12">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvLeave" runat="server">
                                <EmptyDataTemplate>
                                    <br />
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Leave Of Employee"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Leave Details
                                        </h4>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>LName
                                                    </th>
                                                    <th>FDate
                                                    </th>
                                                    <th>TDate
                                                    </th>
                                                    <%-- <th width="10%">
                                                        No.of Days
                                                        </th>--%>
                                                    <th>Ord.No
                                                    </th>
                                                    <th>Join. Date
                                                    </th>
                                                    <%-- <th width="10%">
                                                    Remark/Reason
                                                </th>--%>
                                                    <%--  <th width="10%">
                                                    Session
                                                </th>
                                                <th width="10%">
                                                    Leave Tran. Type
                                                </th>--%>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ENO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ENO")%>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("LEAVENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("fdt", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("tdt", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <%-- <td width="10%">
                                        <%# Eval("Leaves")%>
                                    </td>--%>
                                        <td>
                                            <%# Eval("Ordno")%>
                                        </td>
                                        <td>
                                            <%# Eval("JoinDt", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <%--  <td width="10%">
                                        <%# Eval("Remark")%>
                                    </td>--%>
                                        <%--  <td width="10%">
                                        <%# Eval("PERIOD_DESC")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ST_Desc")%>
                                    </td>--%>
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
                    <legend class="legendPay">Leave</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">Leave Name :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlLeaveName" AppendDataBoundItems="true" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfLeaveName" runat="server" ControlToValidate="ddlLeaveName"
                                    Display="None" ErrorMessage="Please Select  Leave Name" ValidationGroup="ServiceBook"
                                    InitialValue="0"></asp:RequiredFieldValidator>
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
                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than  or equal to Leave from Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">No.of Days :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtNoOfDays" runat="server" Width="50px" MaxLength="3" onkeyup="return validateNumeric(this);"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNoOfDays" runat="server" ControlToValidate="txtNoOfDays"
                                    Display="None" ErrorMessage="Please Enter  No.of Days" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Order No. :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtOrderNo" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                    Display="None" ErrorMessage="Please Enter Order No." ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Joining Date
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtJoiningDate" runat="server" Width="80px"></asp:TextBox>
                                &nbsp;<asp:Image ID="imgJoiningDate" runat="server" ImageUrl="~/images/calendar.png"
                                    Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceJoiningDate" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtJoiningDate" PopupButtonID="imgJoiningDate" Enabled="true"
                                    EnableViewState="true" PopupPosition="BottomLeft">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvJoiningDate" runat="server" ControlToValidate="txtJoiningDate"
                                    Display="None" ErrorMessage="Please Select Joining Date in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meJoiningDate" runat="server" TargetControlID="txtJoiningDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                </ajaxToolKit:MaskedEditExtender>
                                <ajaxToolKit:MaskedEditValidator ID="mevJoiningDate" runat="server" ControlExtender="meJoiningDate"
                                    ControlToValidate="txtJoiningDate" EmptyValueMessage="Please Enter Joining Date"
                                    InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                    TooltipMessage="Please Enter Joining Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                <asp:CompareValidator ID="cvJoiningDate" runat="server" Display="None" ErrorMessage="Joining Date Should be Greater than  or equal to Leave to Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtToDate"
                                    ControlToValidate="txtJoiningDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Session :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" AppendDataBoundItems="true" runat="server" Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">Jan-June</asp:ListItem>
                                    <asp:ListItem Value="2">July-Dec</asp:ListItem>
                                    <asp:ListItem Value="3">Yearly</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="ServiceBook"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Leave Tran. type :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlLeaveTrantype" AppendDataBoundItems="true" runat="server"
                                    Width="200px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">O.B. Leave</asp:ListItem>
                                    <asp:ListItem Value="2">Leave Credit</asp:ListItem>
                                    <asp:ListItem Value="3">Leave Taken</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rvfLeaveTrantype" runat="server" ControlToValidate="ddlLeaveTrantype"
                                    Display="None" ErrorMessage="Please Select  Leave Tran type" ValidationGroup="ServiceBook"
                                    InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Remarks/Reason
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
                        <%--<asp:ListView ID="lvLeave" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Leave Of Employee"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Leave
                                    </div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">Action
                                                </th>
                                                <th width="10%">LName
                                                </th>
                                                <th width="10%">FDate
                                                </th>
                                                <th width="10%">TDate
                                                </th>--%>
                        <%-- <th width="10%">
                                                    No.of Days
                                                </th>--%>
                        <%-- <th width="10%">Ord.No
                                                </th>
                                                <th width="10%">Join. Date
                                                </th>--%>
                        <%--Already Committed<th width="10%">
                                                    Remark/Reason
                                                </th>--%>
                        <%--Already Committed  <th width="10%">
                                                    Session
                                                </th>
                                                <th width="10%">
                                                    Leave Tran. Type
                                                </th>--%>
                        <%-- </tr>
                                            <thead>
                                    </table>
                                </div>
                                <div class="listview-container-servicebook">
                                    <div id="Div1" class="vista-gridServiceBook">
                                        <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0">
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ENO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ENO")%>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("LEAVENAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt", "{0:dd/MM/yyyy}")%>
                                    </td>--%>
                        <%--Already Committed<td width="10%">
                                        <%# Eval("Leaves")%>
                                    </td>--%>
                        <%--    <td width="10%">
                                        <%# Eval("Ordno")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("JoinDt", "{0:dd/MM/yyyy}")%>
                                    </td>--%>
                        <%--Already Committed <td width="10%">
                                        <%# Eval("Remark")%>
                                    </td>--%>
                        <%--Already Committed <td width="10%">
                                        <%# Eval("PERIOD_DESC")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ST_Desc")%>
                                    </td>--%>
                        <%-- </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ENO")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("ENO")%>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("LEAVENAME")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt", "{0:dd/MM/yyyy}")%>
                                    </td>--%>
                        <%--Already Committed<td width="10%">
                                        <%# Eval("Leaves")%>
                                    </td>--%>
                        <%--  <td width="10%">
                                        <%# Eval("Ordno")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("JoinDt", "{0:dd/MM/yyyy}")%>
                                    </td>--%>
                        <%--     <td width="10%">
                                        <%# Eval("Remark")%>
                                    </td>--%>
                        <%--Already Committed <td width="10%">
                                        <%# Eval("PERIOD_DESC")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("ST_Desc")%>
                                    </td>--%>
                        <%--  </tr>
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

