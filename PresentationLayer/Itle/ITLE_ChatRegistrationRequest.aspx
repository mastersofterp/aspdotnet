<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ITLE_ChatRegistrationRequest.aspx.cs" Inherits="Itle_ITLE_ChatRegistrationRequest"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
    <%--   <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript" charset="utf-8">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }
        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CHAT REGISTRATION AND FRIEND REQUEST</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlChat" runat="server">

                        <%--<div class="col-12">
                            <div class="text-center">
                                <asp:Image ID="imgSignIn" runat="server" ImageUrl="~/IMAGES/signup.png" />
                            </div>
                        </div>--%>
                        <div class=" col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Sign In" ValidationGroup="submit" CssClass="btn btn-primary"
                                OnClick="btnSubmit_Click" ToolTip="Click here to Sign In"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                OnClick="btnCancel_Click" ToolTip="Click here to Reset"></asp:Button>
                        </div>

                        <div class="col-12" runat="server" id="trButtons">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl" ForeColor="Green" Font-Bold="true"></asp:Label>

                        </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlAssignment" runat="server">
                                <asp:ListView ID="lvChatRequestList" runat="server">
                                    <LayoutTemplate>
                                        <div id="demo-grid">
                                            <div class="sub-heading">
                                                <h5>RequestList</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Name
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
                                                <asp:ImageButton ID="btnAcceptRequest" runat="server" ImageUrl="~/Images/edit1.png"
                                                    CommandArgument='<%# Eval("UA_NO") %>' ToolTip="Edit Record" AlternateText="Edit Record"
                                                    OnClick="btnAcceptRequest_Click" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png"
                                                                        CommandArgument='<%# Eval("UA_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:ListView ID="LVSendRequest" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>SendRequest</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Name
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
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/send request.jpeg"
                                                    CommandArgument='<%# Eval("UA_NO") %>' ToolTip="Edit Record" AlternateText="Edit Record"
                                                    OnClick="btnEdit_Click" />&nbsp;
                                            </td>
                                            <td>
                                                <%# Eval("UA_FULLNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <asp:Panel ID="PanelRepeter" runat="server">
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                    <asp:Repeater ID="RptrSendRequest" runat="server">
                                        <HeaderTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    Send Request
                                                </div>

                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Name
                                                        </th>
                                                    </tr>
                                                    <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/Request.jpeg"
                                                        CommandArgument='<%# Eval("UA_NO") %>'
                                                        Height="20px" Width="60px" ToolTip="Edit Record" AlternateText="Edit Record"
                                                        OnClick="btnEdit_Click" />&nbsp;<%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("AS_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("SUBJECT")%>--%><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </asp:Panel>
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" AlternateText="Warning" />
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

    <%--  Enable the button so it can be played again --%>

    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }


    </script>

    <%--  Shrink the info panel out of view --%>
</asp:Content>
