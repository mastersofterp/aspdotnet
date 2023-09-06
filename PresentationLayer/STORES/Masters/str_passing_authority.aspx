<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="str_passing_authority.aspx.cs" Inherits="STORES_Masters_str_passing_authority" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:UpdatePanel ID="pnlMessage" runat="server">
        <ContentTemplate></ContentTemplate>
    </asp:UpdatePanel>
     <div class="row">
        <div class="col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">PASSING AUTHORITY</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Add/Edit</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12" id="spanPAuth" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Passing Authority</label>
                                    </div>
                                    <asp:TextBox ID="txtPAuthority" runat="server" MaxLength="50" CssClass="form-control" TabIndex="1" ToolTip="Enter Passing Authority" onKeyUp="LowercaseToUppercase(this);" />
                                    <asp:RequiredFieldValidator ID="rfvPAuthority" runat="server" ControlToValidate="txtPAuthority"
                                        Display="None" ErrorMessage="Please Enter Passing Authority" ValidationGroup="PAuthority"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="spanUser" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>User</label>
                                    </div>
                                    <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Select User" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                        Display="None" ErrorMessage="Please Select User" ValidationGroup="PAuthority"
                                        SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Is Special Authority</label>
                                    </div>
                                    <asp:CheckBox ID="chkSpeAuthority" runat="server" AutoPostBack="true" OnCheckedChanged="chkSpeAuthority_CheckedChanged" />

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="DivAmtFr" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Sanctioning Amount From Range</label>
                                    </div>
                                    <asp:TextBox ID="txtFValue" runat="server" MaxLength="9" CssClass="form-control" TabIndex="3" ToolTip="Enter From Value Range" />
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFV" runat="server" FilterType="Custom, Numbers" TargetControlID="txtFValue" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="Div3AmtTo" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Sanctioning Amount To Range</label>
                                    </div>
                                    <asp:TextBox ID="txtTValue" runat="server" MaxLength="9" CssClass="form-control" TabIndex="4" ToolTip="Enter To Value Range" />
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeTV" runat="server" FilterType="Custom, Numbers" TargetControlID="txtTValue" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>

                                </div>
                            </div>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="PAuthority"
                                OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="5" ToolTip="Click To Submit" />
                             <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click" CssClass="btn btn-info" TabIndex="7" ToolTip="Click to go Back" />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-warning" TabIndex="6" ToolTip="Click To Reset"
                                OnClick="btnCancel_Click" />
                          
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="PAuthority"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                     </div>
                    </asp:Panel>
                

                <asp:Panel ID="pnlList" runat="server">

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" ToolTip="Click Add New To Enter Passing Authority" CssClass="btn btn-primary" TabIndex="1" OnClick="btnAdd_Click" Text="Add New">
                        </asp:LinkButton>
                    </div>
                    <div class="col-12">
                        <asp:ListView ID="lvPAuthority" runat="server">
                            <EmptyDataTemplate>
                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div id="demp_grid">
                                    <div class="sub-heading">
                                        <h5>Passing Authority List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action
                                                </th>
                                                <th>Passing Authority
                                                </th>
                                                <th>User
                                                </th>
                                                <th>Authority Type
                                                </th>
                                                <th>Sanctioning Amt. From Range
                                                </th>
                                                <th>Sanctioning Amt. To Range
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
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("PANO") %>'
                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("PANO") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td>
                                        <%# Eval("PANAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("UA_FULLNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("IS_SPECIAL")%>
                                    </td>
                                    <td>
                                        <%# Eval("AMOUNT_FROM")%>
                                    </td>
                                    <td>
                                        <%# Eval("AMOUNT_TO")%>
                                    </td>
                                </tr>
                            </ItemTemplate>

                        </asp:ListView>
                       <%-- <div class="vista-grid_datapager text-center d-none">
                            <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvPAuthority" PageSize="10"
                                OnPreRender="dpPager_PreRender">
                                <Fields>
                                    <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                        ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                    <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                        RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowLastPageButton="true" ShowNextPageButton="true" />
                                </Fields>
                            </asp:DataPager>
                        </div>--%>

                    </div>
                </asp:Panel>
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


        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

    </script>

</asp:Content>


