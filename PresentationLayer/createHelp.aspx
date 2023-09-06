<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="createHelp.aspx.cs" Inherits="createHelp" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register TagPrefix="rte" Namespace="dbAutoTrack.RichTextEditor" Assembly="dbAutoTrack.RichTextEditor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title"><b>HELP CREATION</b></h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <label>Select Page </label>
                            <asp:DropDownList ID="ddlPage" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvPage" runat="server"
                                ControlToValidate="ddlPage" Display="None" ErrorMessage="Help Page Required"
                                ValidationGroup="Help" InitialValue="-1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <label>Help Description </label>
                            <rte:Editor ID="txtHelpDesc" runat="server" Width="100%" Height="350px"></rte:Editor>
                            <%--<asp:TextBox ID="" runat="server" MaxLength="350" Width="400px" Height="150px" TextMode="MultiLine"/>--%>
                            <%--<asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                                        ControlToValidate="txtHelpDesc" Display="None" 
                                        ErrorMessage="Help Description Required" ValidationGroup="Help">
                                    </asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="Help" CssClass="btn btn-success" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" CssClass="btn btn-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Help" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                            </p>
                        </div>
                        <div>
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                        </div>

                    </asp:Panel>

                    <asp:Panel ID="pnlList" runat="server">
                        <div class="box-footer col-md-12">
                            <p class="text-center">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" CssClass="btn btn-primary">Add New</asp:LinkButton>
                            </p>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvHelp" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Help List</h4>
                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Action</th>
                                                            <th>Description</th>
                                                            <th>Page</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("helpid") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"
                                                        CommandArgument='<%# Eval("helpid") %>' AlternateText="Delete Record" ToolTip="Delete Record"
                                                        OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td><%# Eval("HelpDesc")%></td>
                                                <td><%# Eval("AL_LINK")%></td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>
                                <div class="vista-grid_datapager">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvHelp" PageSize="18"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField
                                                FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                            <asp:NumericPagerField ButtonType="Link"
                                                ButtonCount="7" CurrentPageLabelCssClass="current" />
                                            <asp:NextPreviousPagerField
                                                LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                RenderDisabledButtonsAsLabels="true"
                                                ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>

                </div>

            </div>
        </div>
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgss" runat="server" ImageUrl="~/Images/warning.png"/>
                    </td>
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
    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>
