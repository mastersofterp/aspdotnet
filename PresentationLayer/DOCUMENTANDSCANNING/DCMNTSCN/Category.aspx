<%@ Page Title=" " Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Category.aspx.cs" Inherits="DCMNTSCN_Category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">CATEGORY MASTER </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Category</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="t1" runat="server">
                                    <div class="label-dynamic">
                                        <%--<sup>* </sup>--%>
                                        <label>Parent Category </label>
                                    </div>
                                    <asp:DropDownList ID="ddlCategory" runat="server" data-select2-enable="true" AppendDataBoundItems="true"
                                        CssClass="form-control" ToolTip="Select Parent Category" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="t11" runat="server">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Category </label>
                                    </div>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" ToolTip="Select Category" TabIndex="2"
                                        onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server"
                                        ControlToValidate="txtTitle" Display="None"
                                        ErrorMessage="Please enter category" SetFocusOnError="true"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-12 btn-footer">

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="3"
                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click1" CssClass="btn btn-warning"
                            ToolTip="Click here to Reset" TabIndex="4" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvDoc" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>CATEGORIES</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sub Category
                                                    </th>
                                                    <th>Category
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
                                            <%# Eval("DOCUMENTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PARENTNAME")%>
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
    <div class="col-12">
        <div class="text-center">
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
</asp:Content>