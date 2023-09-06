<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LectureMaster.aspx.cs" Inherits="Itle_LectureMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>

    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <script type="text/javascript">

        window.onload = function () {

            document.getElementById('<%=txtSubject.ClientID%>').focus();
        }
        function validate() {
            document.getElementById('ftbDescription').focus();

        }
    </script>
    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }

        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">LECTURE NOTES CREATION</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlLecture" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlCreateNotes" runat="server">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12 mb-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12 mb-2">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Current Date :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCurrdate" runat="server" Font-Bold="True"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-12 mb-3">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" Font-Bold="True" runat="server"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Topic Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTopic" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                            AppendDataBoundItems="true" ToolTip="Select Topic Name">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlTopic" ErrorMessage="Select Topic Name" InitialValue="0"
                                            ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Sub Topic</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TabIndex="2"
                                            OnBlur="validate();" ToolTip="Enter Sub Topic"></asp:TextBox>
                                        <%--  Enable the button so it can be played again --%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtSubject" ErrorMessage="Enter the Subject name"
                                            ValidationGroup="submit">*</asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attachment Files <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                        </div>
                                        <asp:FileUpload ID="fileUploader" runat="server" TabIndex="4" ToolTip="Click here to Select Files" />

                                        <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" TabIndex="5" CssClass="btn btn-primary"
                                            OnClick="btnAttachFile_Click" ToolTip="Click here to Attach Files" />

                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Description</label>
                                        </div>
                                        <div class="table table-responsive">
                                            <CKEditor:CKEditorControl ID="ftbDescription" runat="server" Height="150" TabIndex="3"
                                                BasePath="~/plugins/ckeditor" ToolTip="Enter Description">		                        
                                            </CKEditor:CKEditorControl>
                                            <%-- <FTB:FreeTextBox ID="ftbDescription" runat="server" Height="250px" Width="100%"  Focus ="true" 
                                                                     Text="&nbsp;"></FTB:FreeTextBox>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <div id="divAttch" runat="server" style="display: none">

                                            <asp:Panel ID="pnlCompAttach" runat="server">
                                                <asp:ListView ID="lvCompAttach" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                    <th>Action
                                                                    </th>
                                                                    <th id="divattach" runat="server">
                                                                    Attachments  
                                                                        </th>
                                                                    <th id="divattachblob" runat="server" visible="false">
                                                                        Attachments
                                                                    </th>
                                                                    <th id="divDownload" runat="server" visible="false">Download
                                                        </th>
                                                        <th id="divBlobDownload" runat="server" visible="false">Download
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
                                                                <asp:LinkButton ID="lnkRemoveAttach" runat="server" CommandArgument='<%# Eval("ATTACH_ID")%>'
                                                                    OnClick="lnkRemoveAttach_Click" CssClass="mail_pg">Remove</asp:LinkButton>

                                                                <ajaxToolKit:ConfirmButtonExtender ID="CnfDrop" runat="server"
                                                                    ConfirmText="Are you Sure, Want to Remove.?" TargetControlID="lnkRemoveAttach">
                                                                </ajaxToolKit:ConfirmButtonExtender>
                                                            </td>
                                                            <td id="attachfile" runat="server" >
                                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                            --%>            
                                                              
                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                    <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                                </td>
                                                              <td id="attachblob" runat="server" visible="false" >
                                                                        <%--<asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("FILE_PATH"))%>'><%# Eval("FILE_NAME")%></asp:HyperLink>
                            --%>            
                                                              
                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                               <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                              --%>      <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                                </td>


                                                             <td id="tdDownloadLink" runat="server" visible="false">
                                                  
                                                              
                                                                <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                               <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                              --%>      <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1000).ToString() %>&nbsp;KB)
                                                            
                                                                    </td>

                                                            <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILE_NAME") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                               
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:UpdatePanel ID="updLectureNotes" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="7"
                                                    ValidationGroup="submit" OnClick="btnSubmit_Click" ToolTip="Click here to Submit" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8"
                                                    OnClick="btnCancel_Click" ToolTip="Click here to Reset" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnCancel" />
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                        <div class="form-group col-12">
                            <div class="text-center">
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
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Lecture Notes List</h5>
                            </div>
                            <div class="DocumentList">
                                <asp:Panel ID="pnlNoteList" runat="server">
                                    <asp:ListView ID="lvNotes" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Unit Name</th>
                                                            <th>Topic Name</th>
                                                            <th>Sub Topic</th>
                                                            <th>Created Date</th>
                                                            <th>Attachment</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("NOTE_NO") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("NOTE_NO") %>'
                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                     <%# Eval("UNITNAME_LEC")%>
                                                </td>
                                                 <td>
                                                    <%# Eval("TOPIC_NAMELEC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOPIC_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <asp:Image ID="img1" runat="server" ImageUrl="~/Images/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                  <%--  <img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                             --%>   </td>
                                                <%--<asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>'  
                                                                     ToolTip='<%# Eval("ATTACHMENT")%>' OnClick="lnkDownload_Click" CommandArgument='<%#Eval("NOTE_NO")%>'>
                                                                </asp:LinkButton>--%>
                                                <%--<asp:HyperLink ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT")%>'  NavigateUrl='<%# GetFileName(Eval("ATTACHMENT"), Eval("NOTE_NO"))%>' Target="_blank">
                                                                </asp:HyperLink></td>--%>
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
                        </div>
                    </asp:Panel>
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
    <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content" style="width: 700px;">
                        <div class="modal-header">
                            <%--   <button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            <h4 class="modal-title">Document</h4>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">

                                <asp:Literal ID="ltEmbed" runat="server" />

                                <%--<iframe runat="server" style="width: 100; height: 100px" id="iframe2"></iframe>--%>

                                <%--<asp:Image ID="imgpreview" runat="server" Height="530px" Width="600px"  />--%>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

