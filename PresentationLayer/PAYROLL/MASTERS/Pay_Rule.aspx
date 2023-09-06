<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Rule.aspx.cs" Inherits="PAYROLL_MASTERS_Pay_Rule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#example').DataTable({

            });

        }

    </script>--%>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PAY RULE</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Pay Rule</label>
                                    </div>
                                    <asp:TextBox ID="txtRule" CssClass="form-control" runat="server" MaxLength="30" />
                                    <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="txtRule"
                                        Display="None" ErrorMessage="Please Enter Pay Rule" ValidationGroup="Rule"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Rule Name</label>
                                    </div>
                                    <asp:TextBox ID="txtRuleName" CssClass="form-control" runat="server" MaxLength="30" />
                                    <asp:RequiredFieldValidator ID="rfvRuleName" runat="server" ControlToValidate="txtRuleName"
                                        Display="None" ErrorMessage="Please Enter Rule Name" ValidationGroup="Rule"></asp:RequiredFieldValidator>

                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>7 CPC Rule</label>
                                    </div>
                                    <asp:CheckBox ID="cb7cprule" runat="server" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Active</label>
                                    </div>
                                    <asp:CheckBox ID="ChkIsActive" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Rule" OnClick="btnSave_Click" CssClass="btn btn-primary progress-button" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Rule"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            <div class="text-center">
                                <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>
                            </div>

                            <asp:Panel ID="pnlList" runat="server">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" Visible="false" CssClass="btn btn-info" />
                            </asp:Panel>
                        </div>

                    </asp:Panel>

                    <div class="col-12">
                        <asp:ListView ID="lvPayRule" runat="server">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Rule" />
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div id="demp_grid" class="vista-grid">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Pay Rule
                                                </th>
                                                <th>Rule Name 
                                                </th>

                                                <th>Active Status
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <div id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("RULENO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("PAYRULE")%>
                                    </td>
                                    <td>
                                        <%# Eval("RULENAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ACTIVESTATUS")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("RULENO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("PAYRULE")%>
                                    </td>
                                    <td>
                                        <%# Eval("RULENAME")%>
                                    </td>

                                    <td>
                                        <%# Eval("ACTIVESTATUS")%>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>

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
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-warning" />
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
</asp:Content>
