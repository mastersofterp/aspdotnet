<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="createUpload.aspx.cs" Inherits="createUpload" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Create Upload</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Upload Title</label>
                                    </div>
                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" CssClass="form-control" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                        Display="None" ErrorMessage="Please Enter Upload Title." ValidationGroup="createUpload">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTitle" runat="server" TargetControlID="txtTitle"
                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Link Name</label>
                                    </div>
                                    <asp:TextBox ID="txtLinkName" runat="server" MaxLength="60" CssClass="form-control" TabIndex="1" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Expiry Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtExpiryDate" runat="server" MaxLength="50" onchange="CheckDate(this);" CssClass="form-control" TabIndex="1" />
                                        <%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtExpiryDate" PopupButtonID="Image1">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvExpiryDate" runat="server" ControlToValidate="txtExpiryDate"
                                            Display="None" ErrorMessage="Please Enter Expiry Date." ValidationGroup="createUpload">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Upload File</label>
                                    </div>
                                    <asp:FileUpload ID="fuFile" runat="server" />
                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                </div>
                                <%--<div class="form-group col-lg-3 col-md-6 col-12"></div>--%>
                                <div class="form-group col-lg-6 col-md-12 col-12 table-responsive">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Description</label>
                                    </div>
                                    <FTB:FreeTextBox ID="ftbDesc" runat="Server" Height="200px" Width="520px" TabIndex="1" />
                                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="ftbDesc"
                                        Display="None" ErrorMessage="Please Enter Description." ValidationGroup="createUpload">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                ValidationGroup="createUpload" CssClass="btn btn-primary" TabIndex="1" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                CausesValidation="False" CssClass="btn btn-warning" TabIndex="1" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="createUpload" />
                            <div>
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 btn-footer">
                            <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvUpload" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Upload List</h5>
                                    </div>
                                    <table class="table table-hover table-bordered display" style="width: 100%;">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Upload Title
                                                </th>
                                                <th>Link
                                                </th>
                                                <th>Status
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("newsid") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("newsid") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("title") %>
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("filename"))%>'><%#  GetFileName(Eval("filename"))%></asp:HyperLink>
                                        </td>
                                        <td>
                                            <%# GetStatus(Eval("status")) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="vista-grid_datapager">
                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvUpload" PageSize="10">
                                <Fields>
                                    <asp:NumericPagerField ButtonCount="5" ButtonType="Link" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </asp:Panel>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                         <asp:Image ID="imgss" runat="server" ImageUrl="~/Images/warning.png"/></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-danger" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-danger" />
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


