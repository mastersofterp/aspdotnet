<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_ImageUpload.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_ImageUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            width: 443px;
        }
    </style>
    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Document Upload</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Document Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlImageType" runat="server" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlImageType_SelectedIndexChanged" CssClass="form-control" ToolTip="Select Image Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvImageType" runat="server"
                                                ControlToValidate="ddlImageType" Display="None"
                                                ErrorMessage="Please select Image Type" InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="ServiceBook">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                            </div>
                                            <asp:Image ID="Image1" runat="server" BorderColor="White"
                                                ImageUrl="~/IMAGES/nophoto.jpg" Width="60%" Height="95%" />
                                            <%--<asp:Image ID="Image1" runat="server" BorderColor="White" Height="100"
                                            ImageUrl="~/IMAGES/nophoto.jpg" Width="100" />--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="fuUploadImage" runat="server" TabIndex="5" />
                                            <span style="color: red">Only .Jpeg/.Jpg/.Png Format is Allowed of Maximum Size 10 Mb</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                            </div>
                                            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" TabIndex="6" CssClass="btn btn-primary"
                                                Text="Upload" ValidationGroup="ServiceBook" ToolTip="Click here to Upload Image" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="6"
                                        Text="Submit" ValidationGroup="ServiceBook" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="false" TabIndex="7"
                                        OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                        ValidationGroup="ServiceBook" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlImage" runat="server">
                                    <asp:ListView ID="lvEmpImage" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text=" No Rows In Emp Document"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee Document</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <%--  <th width="10%">
                                                        Image Id
                                                     </th>--%>
                                                        <th>Document Type
                                                        </th>
                                                        <th>Document 
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("imageTrxId")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("imageTrxId") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <%-- <td width="10%">
                                        <%# Eval("imageid")%>
                                    </td>--%>
                                                <td>
                                                    <%# Eval("imagetype")%>
                                                </td>
                                                <td>
                                                    <asp:Image ID="imgPhoto" Height="50px" Width="80px"
                                                        runat="server" ImageUrl="~/Images/nophoto.jpg" />
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
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
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
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

