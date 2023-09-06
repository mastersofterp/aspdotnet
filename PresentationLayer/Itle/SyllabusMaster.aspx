<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SyllabusMaster.aspx.cs" Inherits="Itle_SyllabusMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>

    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>

    <script type="text/javascript">

        //    window.onload = function() {

        //        document.getElementById('<%=txtTopic.ClientID%>').focus();
        //    }
        function validate() {
            document.getElementById('txtDescription').focus();

        }
    </script>
    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>    
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
</style>   

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SYLLABUS CREATION</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSyllabusMaster" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlSyllabus" runat="server">
                                <div class="row">

                                    <div class="col-lg-5 col-md-6 col-12">
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
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label>

                                                </a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Subject Name</label>
                                        </div>
                                        <asp:TextBox ID="txtSyllabus" runat="server" CssClass="form-control" TabIndex="1"
                                            ToolTip="Subject Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSyllabus"
                                            ErrorMessage="Enter the Syllabus Name" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Unit Name</label>
                                        </div>
                                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" TabIndex="2"
                                            ToolTip="Enter Unit Name"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                    ControlToValidate="txtUnit" ErrorMessage="Enter the Unit Name" 
                                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Topic Name</label>
                                        </div>
                                        <asp:TextBox ID="txtTopic" runat="server" CssClass="form-control" TabIndex="3"
                                            ToolTip="Enter Topic Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtTopic" ErrorMessage="Please Enter Topic Name"
                                            ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Attachment <small style="color: red;">(Max.Size<asp:Label ID="lblFileSize" runat="server" Font-Bold="true"></asp:Label>)</small></label>
                                        </div>
                                        <asp:FileUpload ID="fuSyllabus" runat="server" EnableViewState="true" TabIndex="5"
                                            ToolTip="Click here to Attach File" />

                                        <asp:Label ID="lblPreAttach" runat="server" Text="Label" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hdnFile" runat="server" />
                                    </div>
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Description</label>
                                        </div>
                                        <div class="table table-responsive">
                                            <%-- <ajaxToolkit:CascadingDropDown ID="cddBranch" runat="server" TargetControlID="ddlSemester"
                                          <script src="../plugins/ckeditor/ckeditor.js"></script>                              Category="City" PromptText="Please Select" LoadingText="[Loading...]" ServicePath="~/WebService.asmx"
                                                                        ServiceMethod="GetSemester" ParentControlID="ddlBranch" />--%>
                                            <CKEditor:CKEditorControl ID="ftbDescription" runat="server" Height="150" TabIndex="4"
                                                BasePath="~/plugins/ckeditor" ToolTopic="Enter Description">		                        
                                            </CKEditor:CKEditorControl>
                                            <%--<FTB:FreeTextBox ID="ftbDescription" runat="server" Height="250px" Width="100%"   Focus ="true" Text="&nbsp;" >
                                                                </FTB:FreeTextBox>--%>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="UpdSyllabus" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="6"
                                                OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnViewSyllabus" runat="server" Text="View Syllabus" CssClass="btn btn-info"
                                                OnClick="btnViewSyllabus_Click" ToolTip="Click here to View Syllabus" TabIndex="8" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                                OnClick="btnCancel_Click" ToolTip="Click here to Reset" TabIndex="7" />

                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                            <asp:PostBackTrigger ControlID="btnViewSyllabus" />
                                           
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>

                            </asp:Panel>
                        </div>

                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Syllabus List</h5>
                            </div>
                            <div class="table-responsive">
                                <asp:Panel ID="SyllabusMasterList" runat="server">
                                    <asp:ListView ID="lvSyllabus" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                             <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>ACTION</th>
                                                        <th>SYLLABUS_NAME</th>
                                                        <th>UNIT NAME</th>
                                                        <th>TOPIC NAME</th>
                                                        <th>PLAN DATE</th>
                                                        <th>ATTACHMENT</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("SUB_NO") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("SUB_NO") %>'
                                                                        ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("SYLLABUS_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("UNIT_NAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOPIC_NAME")%>                                                                 
                                                </td>
                                                <td>
                                                    <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>                                                   
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>'
                                                        ToolTip="Click here to download" OnClick="lnkDownload_Click" CommandArgument='<%#Eval("SUB_NO")%>'>
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
                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


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
