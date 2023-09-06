<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_NoduesAuthorityDetail.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_NoduesAuthorityDetail" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">NO DUES AUTHORTIY DETAIL</h3>
                           
                        </div>
                        <div>
                            <form role="form">
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnluser" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Select Authority Type</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <div class="form-group col-md-6">
                                                            <label>Authority Type :</label>
                                                            <asp:DropDownList ID="ddlAuthoName" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="1"
                                                                ToolTip="Select Autho Name" AutoPostBack="true" OnSelectedIndexChanged="ddlAuthoName_SelectedIndexChanged">
                                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="ddlAuthoName"
                                                                Display="None" ErrorMessage="Please Select Autho Name" ValidationGroup="payroll"
                                                                InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <p class="text-center text-bold">
                                                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Panel ID="pnlButton" runat="server">
                                    <p class="text-center">
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="payroll" CssClass="btn btn-primary" TabIndex="2"
                                            OnClick="btnSave_Click" ToolTip="Click here to Save" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="payroll" TabIndex="3" Visible="false" ToolTip="Click here to Delete"
                                            CssClass="btn btn-danger" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" TabIndex="4"
                                            CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </p>
                                </asp:Panel>
                            </p>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlPayhead" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAutho" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Data Found" />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="vista-grid">
                                                <h4 class="box-title">No Dues Authority</h4>
                                                <table class="table table-bordered table-hover" tabindex="5">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="totalAppointment(this)" />Select
                                                            </th>
                                                            <th>Authority Name
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                               
                                                <td>
                                                    <asp:CheckBox ID="ChkPayHead" runat="server" AlternateText="Check to allocate Payhead"
                                                        ToolTip='<%# Eval("AUTHO_ID")%>' />
                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("AUTHORITY_NAME")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
          
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <%--<asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                            </td>
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record?
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
            </asp:Panel>--%>
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

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function totalAppointment(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>


