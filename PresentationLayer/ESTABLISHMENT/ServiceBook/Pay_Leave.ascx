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

