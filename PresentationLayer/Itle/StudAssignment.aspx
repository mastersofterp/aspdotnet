<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudAssignment.aspx.cs" Inherits="Itle_StudAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style type="text/css">
        .hide_img {
            display: none;
        }

        .show_img {
            display: block;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>
    <script>
        /* To Disable Inspect Element */
        $(document).bind("contextmenu", function (e) {
            e.preventDefault();
        });

        $(document).keydown(function (e) {
            if (e.which === 123) {
                return false;
            }
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            window.history.replaceState('', '', window.location.href) // it prevent page refresh to firing the event again
        })
    </script>
    <%-- <script>
        function hideToolBarDiv(event) {
            // Select the correct toolbar DIV and hide it.
            //'event.editor.name' returns the name of the DIV receiving focus.
           // $('#' + event.editor.name + 'divDescription').hide();
            $('#' + event.editor.divDescription + 'divDescription').hide();
        }
    </script>--%>
    <script>
        document.onkeydown = function (e) {
            if (event.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'E'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'S'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'H'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'A'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'F'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'E'.charCodeAt(0)) {
                return false;
            }
        }
    </script>



    <script>
        function disableCtrlKeyCombination(e) {
            if (event.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'E'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'S'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'H'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'A'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'F'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'E'.charCodeAt(0)) {
                return false;
            }
        }
    </script>

    <%--<script type="text/javascript">
$(document).ready(function(){
    $(document).bind("contextmenu",function(e){
   alert('Context Menu event has fired!');
   return false;
}); 
});
</script>--%>
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>
    <%--   <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>--%>


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
                    <h3 class="box-title">ASSIGNMENT REPLY</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlStudentAssignment" runat="server">

                        <asp:Panel ID="pnlAssignment" runat="server">
                            <div class="col-12 ">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="lblSession0" runat="server" Font-Bold="true">Session  :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-7 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true">Course :</asp:Label></b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlAssignDetail" runat="server" Visible="false">
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSession1" runat="server" Font-Bold="true">Assignment Date</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCurrdate" runat="server" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-3 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSubmtDate" runat="server" Font-Bold="true">Last Date  :</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSubmitDate" runat="server" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSession2" runat="server" Font-Bold="true">Assignment Topic :</asp:Label></b>
                                                    <a class="sub-label" id="tdTopic" runat="server"></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group col-lg-8 col-md-6 col-12 mt-3">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblSession3" runat="server" Font-Bold="true">Assignment Questions :</asp:Label>
                                            </div>
                                            <div runat="server" id="tdDescription">

                                                <div class="table table-responsive">
                                                    <CKEditor:CKEditorControl ID="divDescription" runat="server" Height="200" ToolbarStartupExpanded="false" ToolbarCanCollapse="false" Enabled="false"
                                                        BasePath="~/plugins/ckeditor" ToolTip="Enter Description" TabIndex="2">		                        
                                                    </CKEditor:CKEditorControl>
                                                    <%--<FTB:FreeTextBox ID="txtDescription" runat="server" Height="250px" Width="100%" Focus="true"
                                                    Text="&nbsp;">
                                                </FTB:FreeTextBox>--%>
                                                    <%--<ajaxToolkit:CascadingDropDown ID="cddDept" runat="server" TargetControlID="ddlBranch"
                                                Category="City" PromptText="Please Select" LoadingText="[Loading...]" 
                                                ServicePath="~/WebService.asmx"
                                                ServiceMethod="GetBranch" ParentControlID="ddlDept" />--%>
                                                </div>
                                                <%--<asp:Panel ID="pnlReplyDesc" runat="server" BorderColor="Navy" BorderWidth="3px"
                                                    Heigh="150px">
                                                    <div style="height: 150px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                                        <div id="divDescription" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                                            <%# Eval("STATUS")%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12 mt-3">
                                            <div class="label-dynamic">
                                                <label>Attachment</label>
                                            </div>
                                            <asp:ListView ID="lvAttachments" runat="server" Style="overflow: auto">
                                                <LayoutTemplate>
                                                    <table>
                                                        <%-- class="table table-bordered table-hover"--%>
                                                        <thead>
                                                            <tr>
                                                                <th id="divattach" runat="server" visible="false">Attachments  
                                                                </th>
                                                                <th id="divDownload" runat="server" visible="false">Preview
                                                                </th>
                                                                <th id="divBlobDownload" runat="server" visible="false">Download
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
                                                            <td id="tdDownload" runat="server" visible="false">
                                                                <asp:Image ID="img" runat="server" ImageUrl="~/Images/attachment.png" />
                                                                <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                                <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                                    <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                            </td>
                                                        </td>
                                                        <td id="tdDownloadLink" runat="server" visible="false">
                                                            <asp:Image ID="Img2" runat="server" ImageUrl="~/Images/attachment.png" />
                                                            <%-- <img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                            <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                            --%>     <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                        </td>

                                                        <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-lg-12 col-md-12 col-12 mt-2" id="divreply" runat="server">
                                    <div class="label-dynamic">
                                        <asp:Label ID="lblReply" runat="server" Visible="true" Font-Bold="true">Reply :</asp:Label>
                                    </div>
                                    <CKEditor:CKEditorControl ID="txtReplyDescription" runat="server" Height="150" Width="1000" Visible="false"
                                        BasePath="~/plugins/ckeditor">		                        
                                    </CKEditor:CKEditorControl>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReplyDescription"
                                        ErrorMessage="Reply Field Must Not Be Blank." ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                </div>


                                <div id="tdUploadFiles" visible="false" runat="server">
                                    <div class="col-12 ">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-12 col-12">
                                                <div class="label-dynamic">
                                                    <label>
                                                        Attachment &nbsp;&nbsp; (Upload File<asp:Label ID="lblFiletype" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                        )</label>
                                                </div>
                                                <asp:FileUpload ID="fuAssign" runat="server" />

                                                <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" CssClass="btn btn-primary"
                                                    OnClick="btnAttachFile_Click" ToolTip="Click here to Attach File" />&nbsp;&nbsp;(Max.Size
                                                                <asp:Label ID="lblFileSize" runat="server" Font-Bold="True"></asp:Label>

                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Math Equation</label>
                                                </div>
                                                <asp:Button ID="btnMathEditor" runat="server" Text="Math Equation" OnClientClick="MathEditor();"
                                                    CssClass="btn btn-primary" ToolTip="Click here for Math Equation" />
                                                <div></div>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div id="divAttch" runat="server" style="display: none">
                                                    <asp:Panel ID="pnlattachment" runat="server" ScrollBars="Auto">
                                                        <asp:ListView ID="lvCompAttach" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-bordered table-hover">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Action
                                                                            </th>
                                                                            <th id="divattach" runat="server" visible="false">Attachment
                                                                            </th>
                                                                            <th id="divattachblob" runat="server" visible="false">Attachments
                                                                            </th>
                                                                            <th id="divpreview" runat="server" visible="false">Preview</th>
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
                                                                        <asp:LinkButton ID="lnkRemoveAttach" runat="server" CommandArgument='<%# Eval("ATTACH_ID")%>'
                                                                            OnClick="lnkRemoveAttach_Click" CssClass="mail_pg">Remove</asp:LinkButton>
                                                                    </td>
                                                                    <td id="tdattach" runat="server" visible="false">
                                                                        <asp:Image ID="img3" runat="server" ImageUrl="~/Images/attachment.png" />
                                                                        <%-- <img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                                        <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                                            <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                  
                                                                    </td>
                                                                    <td id="tdattachblob" runat="server" visible="false">

                                                                        <asp:Image ID="Img4" runat="server" ImageUrl="~/Images/attachment.png" />
                                                                        <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                                        <%--  <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                                        --%>      <%# Eval("FILE_PATH")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                  
                                                                    </td>

                                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                                        <asp:UpdatePanel ID="updPreview" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:ImageButton ID="imgdow" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILE_NAME") %>'
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
                                                    </asp:Panel>
                                                </div>
                                                <div id="tdFileSize" runat="server" visible="false">
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:UpdatePanel ID="updReplyAssign" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Reply" CssClass="btn btn-primary"
                                                ValidationGroup="submit" ToolTip="Click here to Reply" />
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-primary"
                                                ValidationGroup="submit" ToolTip="Click here to Save" Visible="false" />
                                            <asp:Button ID="btnBackPage" runat="server" OnClick="btnCancel_Click" Text="Back" CssClass="btn btn-primary"
                                                ToolTip="Click here to Reset" />

                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning"
                                                ToolTip="Click here to Reset" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                            <asp:PostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-12">
                                    <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                </div>
                                <div id="trMessage" runat="server" visible="true">
                                    <div class="col-12">
                                        <asp:Label ID="lblMessage" runat="server" SkinID="Msglbl" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlDisplayMarks" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Subject :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSubject" runat="server"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>

                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Marks Obtained :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMarks" runat="server"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Remark:</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblRemark" runat="server"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="form-group col-lg-12 col-md-6 col-12 mt-3">
                                        <div class="label-dynamic">
                                            <asp:Label ID="lblreplyans" runat="server" Font-Bold="true">Replied Answer :</asp:Label>

                                        </div>
                                        <asp:Panel ID="pnlRepliedAnswer" runat="server" BorderColor="Navy" BorderWidth="3px"
                                            Heigh="300px">
                                            <div style="height: 300px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                                <div id="divCheckedAssign" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                        ToolTip="Click here to Go Back" />
                                </div>

                            </asp:Panel>

                        </asp:Panel>

                        <div class="col-12 mt-4">
                            <div id="DivAssignmentList" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Assignment List</h5>
                                </div>
                                <asp:Panel ID="pnlAssignmentList" runat="server">
                                    <asp:ListView ID="lvAssignment" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Subject</th>
                                                            <th>Created Date</th>
                                                            <th>Due Date Time</th>
                                                            <th>Cut-Off Date Time</th>
                                                            <%--<th>Attachment</th>--%>
                                                            <th>Status</th>
                                                            <th>Display Result</th>
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

                                                <td class="text-center">
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("AS_No") %>' OnClick="btnEdit_Click"></asp:LinkButton></td>

                                                <%--<td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.png" CommandArgument='<%# Eval("AS_No") %>'
                                                        ToolTip="Reply This Assignment" AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ASSIGNDATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DUEDATE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBMITDATE","{0:dd-MMM-yyyy}")%>
                                                                -
                                                                <%# Eval("SUBMITDATE", "{0:hh:mm:ss tt}")%>
                                                </td>
                                                <%--  <td>
                                                    <asp:ImageButton ID="btnimgdow" runat="server" ImageUrl="~/Images/attachment.png" CommandArgument='<%# Eval("AS_No") %>' ToolTip="DownLoad Assignment" OnClick="btnimgdow_Click" />

                                                    <%-- <img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                </td>--%>
                                                <td>

                                                    <%# GetStatus(Eval("SUBMITDATE"))%>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="btnDesplayMarks" runat="server" Enabled='<%# checkeEnable(Eval("DISPLAY_MARKS"))%>'
                                                        OnClick="btnDesplayMarks_Click" Text='<%# Eval("DISPLAY_MARKS") %>' CommandArgument='<%# Eval("AS_NO") %>'></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                        </div>


                        <div class="col-12 mt-3">
                            <asp:Button ID="btnForPopUpModel" Style="display: none" runat="server" Text="For PopUp Model Box" />

                            <ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                DropShadow="false" PopupControlID="pnl" TargetControlID="btnForPopUpModel2" DynamicServicePath=""
                                Enabled="True">
                            </ajaxToolKit:ModalPopupExtender>
                            <asp:Panel ID="pnl" runat="server" Width="400px" BackColor="#FFFFFF" BorderColor="black" meta:resourcekey="pnlResource1" Style="box-shadow: rgb(119 116 116) 0px 0px 15px; padding: 15px;">
                                <div>
                                    <div class="sub-heading">
                                        <h5>Download attached File  with E-Book</h5>
                                    </div>


                                    <asp:ListView ID="lvDoc" runat="server">
                                        <LayoutTemplate>
                                            <table>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>

                                                    <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                    <asp:Image ID="img5" runat="server" ImageUrl="~/Images/attachment.png" />
                                                    <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
                                                        <%# Eval("FILE_NAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE"))).ToString() %>&nbsp;KB)
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <div class="btn-footer mt-2">
                                        <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />
                                        <asp:Button ID="btnclose" runat="server" Text="Close" ValidationGroup="Validation"
                                            CssClass="btn btn-danger" OnClick="btnBack_Click" meta:resourcekey="btnBackResource1" />
                                        <asp:HiddenField ID="hdnBack" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                    <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                    <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //For Server 
        function MathEditor() {
            window.open("../ITLE_MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        }

        //For Local Uncomment This
        //function MathEditor() {
        //    window.open("ITLE_MathEditor.aspx", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, top=500, left=500, width=800, height=400");

        //}
    </script>
    <script type="text/javascript">
        function CloseModal() {
            $("#preview").modal("hide");
        }
        function ShowModal() {
            $("#preview").modal("show");
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
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                            <asp:HiddenField ID="hdnfilename" runat="server" />
                            <asp:Button ID="btnclosedoc" runat="server" Text="CLOSE" OnClick="btnclosedoc_Click" OnClientClick="CloseModal();return true;" CssClass="btn btn-outline-danger" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
