<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AddForum.aspx.cs" Inherits="Itle_AddForum" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <script type="text/javascript">

        window.onload = function () {

            document.getElementById('<%=txtForum.ClientID%>').focus();
        }
        function validate() {
            document.getElementById('txtDescription').focus();
        }
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdForum"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdForum" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE FORUM</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlForum" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlForumDetail" runat="server">
                                        <div class="row">
                                            <div class="col-lg-5 col-md-6 col-12 mb-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Session :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>


                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-6 col-12 mb-3">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Forum</label>
                                                    <asp:TextBox ID="txtForum" runat="server" MaxLength="100" CssClass="form-control"
                                                        ToolTip="Enter Forum Name" TabIndex="1"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="reqSubmitDate" runat="server" ControlToValidate="txtForum" Display="None"
                                                        ErrorMessage="Please Enter Forum Topic"
                                                        ValidationGroup="grpForum"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Description</label>
                                                </div>
                                                <div class="table table-responsive">
                                                    <CKEditor:CKEditorControl ID="txtDescription" runat="server" Height="250" BasePath="~/plugins/ckeditor">		                        
                                                    </CKEditor:CKEditorControl>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnAdd" runat="server" Text="Submit" OnClick="btnAdd_Click"
                                                meta:resourcekey="btnAddResource1" CssClass="btn btn-primary" TabIndex="3"
                                                ValidationGroup="grpForum" ToolTip="Click here to Submit"></asp:Button>
                                            <asp:Button ID="btnViewForum" runat="server" Text="Forum Report" CssClass="btn btn-info"
                                                OnClick="btnViewForum_Click" TabIndex="5" ToolTip="Click here for Forum Report" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                                            <asp:ValidationSummary ID="grpForum" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="grpForum" />
                                        </div>
                                        <div class="col-12">
                                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label ID="lblError" runat="server" Visible="False"
                                                EnableViewState="False" SkinID="Msglbl">Error: a forum was not created. Please fill all the fields.</asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Forum List</h5>
                                    </div>

                                    <asp:Panel ID="pnlForumGrid" runat="server">
                                        <asp:ListView ID="lvAssignment" runat="server">
                                            <LayoutTemplate>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Created Date</th>
                                                            <th>Forum </th>
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
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("FORUM_NO") %>'
                                                            ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("FORUM_NO") %>'
                                                            ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("CREATEDDATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="btnlnkSelect" runat="server" Text='<%# Eval("FORUM")%>' CommandName='<%# Eval("FORUM_NO") %>'
                                                            CommandArgument='<%# Eval("FORUM_NO") %>' ToolTip="Discussion Forum" OnClick="btnlnkSelect_Click">
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <p class="text-center text-bold">
                                                    <asp:Label ID="lblEmptyMsg" runat="server" Text="No Records Found"></asp:Label>
                                                </p>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </asp:Panel>

                                </div>
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
                <div class="text-center">
                    <div class="modal-content">
                        <div class="modal-body">
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.png" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
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
