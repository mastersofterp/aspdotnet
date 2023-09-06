<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_ForeginService.ascx.cs"
    Inherits="PayRoll_Pay_ForeginService" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td class="form_left_label" colspan="2">
            <b><font color="red">Note :- </font></b>
        </td>
    </tr>
    <tr>
        <td class="form_left_label" colspan="2">
            <b>PANOE :</b> Post And Name Of The Employer
        </td>
    </tr>
    <tr>
        <td class="form_left_label" colspan="2">
            <b>LSCAPCP :</b>Leave Salary Contribution And Pension Contribution Payable
        </td>
    </tr>
    <tr>
        <td class="form_left_label" colspan="2">
            <b>LSCAPCPARWCP : </b>Leave Salary Contribution And Pension Contribution Payable
            Actually Remitted With Credit Particulars
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
    </tr>
    <tr>
        <td valign="top" width="50%">
            <asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 95%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Foreign Service</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td class="form_left_label">
                                From Date :
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
                            <td class="form_left_label">
                                To Date :
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
                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date  Should be Greater than  or equal to From Date"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                PANOE :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPostAndNameOfTheEmployer" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPostAndNameOfTheEmployer" runat="server" ControlToValidate="txtPostAndNameOfTheEmployer"
                                    Display="None" ErrorMessage="Please Enter Post And Name Of The Employer" ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                LSCAPCP :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPensionContributionPayable" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPensionContributionPayable" runat="server" ControlToValidate="txtPensionContributionPayable"
                                    Display="None" ErrorMessage="Please Enter Leave Salary Contribution & Pension Contribution Payable"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                LSCAPCPARWCP :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtPensionContributionPayableCreditparticulars" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPensionContributionPayableCreditparticulars" runat="server"
                                    ControlToValidate="txtPensionContributionPayableCreditparticulars" Display="None"
                                    ErrorMessage="Please Enter Leave Salary Contribution & Pension Contribution Payable Actually remitted with  credit particulars"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Remarks :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="200px" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks"
                                    Display="None" ErrorMessage="Please Enter Remarks " ValidationGroup="ServiceBook"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                Upload Document :
                            </td>
                            <td class="form_left_text">
                                <asp:FileUpload ID="flupld" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
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
                            <td>
                                &nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </td>
        <td valign="top">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td align="center">
                        <asp:ListView ID="lvForeginService" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Foreign Service Of Employee"></asp:Label>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="vista-gridServiceBook">
                                    <div class="titlebar-ServiceBook">
                                        Foregin Service</div>
                                    <table class="datatable-ServiceBook" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <thead>
                                            <tr class="header-ServiceBook">
                                                <th width="10%">
                                                    Action
                                                </th>
                                                <th width="10%">
                                                    FDate
                                                </th>
                                                <th width="10%">
                                                    TDate
                                                </th>
                                                <th width="10%">
                                                    PANOE
                                                </th>
                                                <th width="20%">
                                                    LSCAPCP
                                                </th>
                                                <th width="20%">
                                                    LSCAPCPARWCP
                                                </th>
                                                <th width="15%" align="left">
                                                    Attachment
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("fsno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("fsno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("postname")%>
                                    </td>
                                    <td width="20%">
                                        <%# Eval("lsc")%>
                                    </td>
                                    <td width="20%">
                                        <%# Eval("lscr")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("FSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem-ServiceBook" onmouseover="this.style.backgroundColor='#FFFFAA'"
                                    onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td width="10%">
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("fsno")%>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("fsno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td width="10%">
                                        <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td width="10%">
                                        <%# Eval("postname")%>
                                    </td>
                                    <td width="20%">
                                        <%# Eval("lsc")%>
                                    </td>
                                    <td width="20%">
                                        <%# Eval("lscr")%>
                                    </td>
                                    <td width="15%" align="left">
                                        <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("FSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
    </tr>
</table>
<%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
<%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
    runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
    OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
    BackgroundCssClass="modalBackground" />
<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
    <div style="text-align: center">
        <table>
            <tr>
                <td align="center">
                    <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                </td>
                <td>
                    &nbsp;&nbsp;Are you sure you want to delete this record?
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                    <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>

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

