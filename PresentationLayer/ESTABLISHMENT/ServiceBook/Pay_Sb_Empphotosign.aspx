<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Empphotosign.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_Empphotosign" %>

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
                                                <h5>Employee Photo & Sign Upload</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Photo</label>
                                            </div>
                                            <asp:FileUpload ID="fuplEmpPhoto" runat="server" TabIndex="1" />
                                            <span style="color: red">Upload Photo Maximum Size 100Kb.Only .jpg, .jpeg format allowed</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Height="128px"
                                                Width="128px" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="Button1" runat="server" TabIndex="6" CssClass="btn-primary"
                                                Text="Upload Photo" ValidationGroup="ServiceBook" ToolTip="Click here to Upload Photo" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Sign</label>
                                            </div>
                                            <asp:FileUpload ID="fuplEmpSign" runat="server" TabIndex="2" />
                                            <span style="color: red">Upload Sign Maximum Size 100Kb.Only .jpg, .jpeg format allowed</span>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/IMAGES/sign11.jpg" Height="58px"
                                                Width="128px" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="Button2" runat="server" TabIndex="6" CssClass="btn-primary"
                                                Text="Upload Sign" ValidationGroup="ServiceBook" ToolTip="Click here to Upload Sign" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnUpload" runat="server" TabIndex="6" CssClass="btn-primary"
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
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text=" No Rows In Emp Image"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Employee Photo & Sign</h5>
                                            </div>

                                            <table class="table table-bordered table-hover">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <%--  <th width="10%">
                                                        Image Id
                                                     </th>--%>
                                                        <th>Photo
                                                        </th>
                                                        <th>Sign 
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("IDNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;                                        
                                                </td>
                                                <td>
                                                    <asp:Image ID="imgPhoto" Height="50px" Width="80px"
                                                        runat="server" ImageUrl="~/Images/nophoto.jpg" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="imgsign" Height="50px" Width="80px"
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
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>


