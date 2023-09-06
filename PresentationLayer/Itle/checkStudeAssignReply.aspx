<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="checkStudeAssignReply.aspx.cs" Inherits="ITLE_checkStudeAssignReply" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <%--<script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">CHECK ASSIGNMENT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlCheckStudentAssignment" runat="server">
                        <div class="col-12">
                            <div class="row">

                                <div class="col-12 mb-1">
                                    <div class="text-right">
                                        <asp:HyperLink ID="HyperLink1" runat="server"
                                            NavigateUrl="~/ITLE/checkStudeAssignReply.aspx?pageno=1631"><b>Home</b></asp:HyperLink>
                                    </div>
                                </div>

                                <div class="col-lg-5 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-3 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Current Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-12 col-md-12 col-12 mt-2 mb-2">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Course Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCourseName" runat="server" Font-Bold="true"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12" id="trAssignTopic" runat="server" visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Assignment Topic :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAssignmentTopic" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12" id="trStudentName" runat="server" visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="form-group col-lg-12 col-md-12 col-12 mt-3" runat="server" id="trDesc" visible="false">
                                    <div class="label-dynamic">
                                        <label>Replied Answer</label>
                                    </div>
                                    <div class="table table-responsive">
                                            <CKEditor:CKEditorControl ID="divRepDesc" runat="server" Height="150"    ToolbarStartupExpanded="false" ToolbarCanCollapse="false" Enabled="false"
                                                BasePath="~/plugins/ckeditor" ToolTip="Enter Description" TabIndex="2">		                        
                                            </CKEditor:CKEditorControl>
                                        </div>
                                  <%--  <asp:Panel ID="pnlReplyDesc" runat="server" BorderColor="Navy" BorderWidth="3px" Heigh="200px">
                                        <div style="height: 300px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                            <div id="divRepDesc" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                            </div>
                                        </div>
                                    </asp:Panel>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trfile" visible="false">
                                    <div class="label-dynamic">
                                        <label>Attachment Files :</label>
                                    </div>
                                    <asp:ListView ID="lvAttachments" runat="server">
                                        <LayoutTemplate>
                                            <table>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td id="tdblob" runat="server" visible="false">
                                                    <asp:Image ID="img1"  runat="server" ImageUrl="~/Images/attachment.png" />
                                                   <%-- <img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                    <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                        <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                </td>

                                                <td id="tdattachblob" runat="server" visible="false" >
                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/attachment.png" />
                                                                          <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                           <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                           --%>     <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                  
                                                                         </td>

                                                                      <td style="text-align: center" id="tdbtndownload" runat="server"  visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgdow" runat="server"  OnClick="imgdow_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILE_NAME") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILE_NAME") %>' Visible='<%# Convert.ToString(Eval("FILE_NAME"))==string.Empty?false:true %>'></asp:ImageButton>

                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgdow" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                               
                                                            </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%-- <asp:HyperLink ID="linkAssingReplyFile" runat="server">HyperLink</asp:HyperLink>--%>
                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trReplyDate" visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Session  :</b>
                                            <a class="sub-label">
                                                <%--<asp:Label ID="Label1" runat="server"></asp:Label>--%>
                                                <span runat="server" id="tdDate"></span>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-3 col-md-6 col-12" runat="server" id="trTotalMarks" visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Total Marks  :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTotalMarks" runat="server"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="trMarksObtained" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Marks Obtained </label>
                                    </div>
                                    <asp:TextBox ID="txtMarks" onkeyup="validateNumeric(this);" onBlur="checkMark();" CssClass="form-control"
                                        runat="server" ToolTip="Enter Marks Obtained"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtMarks" ErrorMessage="Enter Marks"
                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row" runat="server" id="trRemark" visible="false">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Remark</label>
                                    </div>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="100" CssClass="form-control"
                                        ToolTip="Enter Remark If Any" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkChecked" runat="server" Text="Checked" Height="20px"></asp:CheckBox>
                                    <asp:CheckBox ID="chkDisplayMarks" runat="server" Text="Display Marks To Student" Height="20px"></asp:CheckBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer" runat="server" id="trButtons" visible="false">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit"
                                ValidationGroup="submit" OnClick="btnSubmit_Click"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to reset"
                                OnClick="btnCancel_Click"></asp:Button>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                                DisplayMode="List" ValidationGroup="submit" />

                        </div>
                        <div class="col-12">
                            <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                        </div>
                        <div class="col-12 mt-3" id="divStudList" runat="server" visible="false">
                            <div class="sub-heading">
                                <h5>Student List</h5>
                            </div>

                            <asp:Panel ID="pnlAssignment" runat="server">
                                <asp:ListView ID="lvAssignmentReply" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action</th>
                                                    <th>RRN</th>
                                                    <th>Student</th>
                                                    <th>Reply Date</th>
                                                   <%-- <th>Attachment</th>--%>
                                                    <th>Check Status</th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.png" CommandArgument='<%# Eval("sa_no") %>'
                                                    ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("sa_no") %>'
                                                    ToolTip="Delete Record"
                                                    OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" Visible="false" />
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("reply_date", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                           <%-- <td>
                                                <img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                            </td>--%>
                                            <td>
                                                <asp:Image ID="imgcheck" runat="server" ImageUrl="~/Images/check1.jpg" class='<%# (Convert.ToInt32(Eval("STATUS")) > 0)? "show_img": "hide_img" %>' width="15px" height="15px" />
                                              <%--  <img alt="checked" src="../IMAGES/check1.jpg" class='<%# (Convert.ToInt32(Eval("STATUS")) > 0)? "show_img": "hide_img" %>' width="15px" height="15px" />
                                            --%></td>
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
                        <div class="col-12 mt-3" id="divAssinList" runat="server">
                            <div class="sub-heading">
                                <h5>Assignment List</h5>
                            </div>
                            <asp:Panel ID="pnlAssignmentList" runat="server">
                                <asp:ListView ID="lvAssignment" runat="server">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Subject</th>
                                                    <th>Created Date</th>
                                                    <th>Submission Date Time </th>
                                                    <%--<th>Attachment </th>--%>
                                                    <th>Status</th>
                                                    <th>Student Reply</th>
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
                                                <%# Eval("SUBJECT")%>
                                            </td>
                                            <td>
                                                <%# Eval("ASSIGNDATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBMITDATE","{0:dd-MMM-yyyy}")%>
                                                            -
                                                            <%# Eval("SUBMITDATE", "{0:hh:mm:ss tt}")%>
                                            </td>
                                          <%--  <td>
                                                <img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                            </td>--%>
                                            <td>
                                                <%# GetStatus(Eval("SUBMITDATE"))%>                                                        
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnStudeReply" runat="server" Text='<%# Eval("studreply")%>'
                                                    Enabled='<%# checkeEnable(Eval("studreply"))%>' ToolTip="Click here"
                                                    OnClick="btnStudeReply_Click" CommandArgument='<%#Eval("AS_NO")%>'> </asp:LinkButton>
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

                         <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
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
    <%--  Enable the button so it can be played again --%>
    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>


    <script type="text/javascript">

        function checkMark() {


            var totalMark = document.getElementById('<%=lblTotalMarks.ClientID%>').innerHTML;

            var obtainedMark = document.getElementById('<%=txtMarks.ClientID%>').value;

            if (obtainedMark != '') {

                if (parseInt(obtainedMark) > parseInt(totalMark)) {

                    alert("Please enter Obtained mark less than Total marks");
                    document.getElementById('<%=txtMarks.ClientID%>').value = '';
                    document.getElementById('<%=txtMarks.ClientID%>').focus();
                }

            }

        }

    </script>


     <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
        <div class="modal-dialog text-center">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
    <%--  </ContentTemplate></asp:UpdatePanel>--%>
</asp:Content>

