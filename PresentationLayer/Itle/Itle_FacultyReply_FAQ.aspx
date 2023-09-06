<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Itle_FacultyReply_FAQ.aspx.cs" Inherits="Itle_Itle_FacultyReply_FAQ" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    --%>
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FAQ REPLY</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlFAQ" runat="server">
                        <asp:Panel ID="pnlAssignDetail" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblSession0" runat="server" Font-Bold="true">Session :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true">Reply Date:</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblReplyDate" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblStudId0" runat="server" Font-Bold="true">Student Id :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudId1" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblStudName0" runat="server" Font-Bold="true">Student Name :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudName1" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-4 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item" runat="server" visible="false"><b>
                                                <asp:Label ID="lblRecieveDate0" runat="server" Font-Bold="true">Received Date :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRecieveDate1" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Subject :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSubject" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-12 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li id="Li1" class="list-group-item" runat="server" visible="false"><b>Question : </b>
                                                <a class="sub-label">
                                                    <span runat="server" id="tdDescription" font-bold="True"></span></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <div class="row">
                                    <span runat="server" id="tdSubmitDate"></span>
                                </div>

                                <div class="row mt-3">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblReply" runat="server" Font-Bold="true">Reply </asp:Label>
                                        </div>
                                        <CKEditor:CKEditorControl ID="txtReplyDescription" runat="server" Height="200" BasePath="~/ckeditor">		                        
                                        </CKEditor:CKEditorControl>
                                        <%--<FTB:FreeTextBox ID="txtReplyDescription" runat="server" Height="250px" Width="100%" Text="&nbsp;">
                                                    </FTB:FreeTextBox>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtReplyDescription" Display="None"
                                            ErrorMessage="Reply Field Must Not Be Blank." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblFlUpld" runat="server" Visible="False" Font-Bold="true">Upload File :</asp:Label>
                                        </div>
                                        <asp:FileUpload ID="fuAssign" runat="server" Visible="False" />
                                        <asp:Label ID="lblPreAttach" runat="server" Text=""></asp:Label>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblAssignFile" runat="server" Visible="False" Font-Bold="true">Assignment File :</asp:Label>
                                        </div>
                                        <asp:HyperLink ID="linkAssingFile" runat="server">HyperLink</asp:HyperLink>
                                    </div>

                                    <div class="col-12">
                                        <%# Eval("SUBJECT")%>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                    <div id="trMessage" runat="server" visible="true">
                                        <asp:Label ID="lblMessage" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Reply"
                                        ValidationGroup="submit" CssClass="btn btn-primary" ToolTip="Click here to Reply" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                        CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                </div>
                                <div class="col-12">
                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-12 btn-footer">
                            <asp:UpdatePanel ID="updViewFAQ" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnViewFAQ" runat="server" Text="Download FAQ Answers" CssClass="btn btn-primary"
                                        OnClick="btnViewFAQ_Click" ToolTip="Click here to Download FAQ Answers" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnViewFAQ" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </asp:Panel>

                    <div class="col-12">
                        <div class="sub-heading">
                            <h5>Question List</h5>
                        </div>
                        <asp:Panel ID="pnlFAQList" runat="server">
                            <asp:ListView ID="lvFAQ" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Action</th>
                                                <th>Subject</th>
                                                <th>Received Date</th>
                                                <th>Faculty Reply(Outbox)</th>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.png" CommandArgument='<%# Eval("QUES_NO") %>'
                                                ToolTip="Reply This Assignment" AlternateText="Edit Record" OnClick="btnEdit_Click" AutoPostBack="true" />
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("QUES_NO") %>'
                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("SUBJECT")%>
                                        </td>
                                        <td>
                                            <%# Eval("CREATED_DATE","{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="btnReplyAnswer" runat="server" Enabled='<%# checkeEnable(Eval("FQUES_NO"))%>'
                                                OnClick="btnReplyAnswer_Click" Text='<%# Eval("FQUES_NO") %>' CommandArgument='<%# Eval("QUES_NO") %>'></asp:LinkButton>
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

                    <div class="col-12">
                        <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                            runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                            OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                            BackgroundCssClass="modalBackground" />
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
                    </div>
                </div>
            </div>
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

