<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudNotes.aspx.cs" Inherits="ITLE_StudNotes" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <script src="ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="ckeditor/ckeditor_basic.js" type="text/javascript"></script>--%>
    <script src="../plugins/ckeditor/ckeditor.js"></script>
    <script src="../plugins/ckeditor/ckeditor_basic.js"></script>


    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>




    <%--<style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>--%>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT NOTES</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlStudentNotes" runat="server">
                        <div class="col-12">
                            <asp:Panel ID="pnlNotes" runat="server">
                                <div class="row">
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Session  :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-7 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Course Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlAssignDetail" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblCrtdDt" runat="server" Font-Bold="true">Created Date :</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCurrdate" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-7 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>
                                                    <asp:Label ID="lblSubj" runat="server" Font-Bold="true">Sub Topic :</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSubject" runat="server">Subject :</asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-8 col-md-12 col-12 mt-3">
                                            <div class="label-dynamic">
                                                <label>Description</label>
                                            </div>
                                            <asp:Panel ID="pnlDescription" runat="server" BorderColor="Navy" BorderWidth="3px"
                                                Heigh="150px">
                                                <div style="height: 150px; background-color: #D1EDD1; padding-top: 25px; padding-left: 5px; padding-left: 5px;">
                                                    <div id="divDescription" runat="server" style="height: 95%; padding-left: 10px; padding-top: 5px; padding-right: 5px; padding-bottom: 5px; background-color: White; border-radius: 25px; overflow: auto">
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12 mt-3" id="trh" runat="server">
                                            <div class="label-dynamic">
                                                <asp:Label ID="lblAssignFile" runat="server" Visible="true" Font-Bold="true">Lecture Notes File </asp:Label>
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
                                            <%# Eval("SUBJECT")%>
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" ValidationGroup="submit"
                                            CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                    </div>
                                    <div id="trMessage" runat="server" visible="true">
                                        <div class="col-12">
                                            <asp:Label ID="lblMessage" runat="server" SkinID="Msglbl"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <asp:HiddenField ID="hdnFile" runat="server" />
                                    </div>

                                </asp:Panel>
                            </asp:Panel>
                        </div>
                        <div class="col-12 mt-4">
                            <div id="DivLectureNotesList" runat="server" visible="false">
                                <div class="sub-heading">
                                    <h5>Lecture Notes List</h5>
                                </div>

                                <asp:Panel ID="pnlNotesList" runat="server" ScrollBars="Vertical" Height="250px" BackColor="#FFFFFF">
                                    <asp:ListView ID="lvNotes" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead>
                                                        <tr>
                                                            <th class="text-center">View</th>
                                                            <th>Unit Name</th>
                                                            <th>Topic Name</th>
                                                            <th>Sub Topic</th>
                                                            <th>Created Date</th>
                                                            <%--<th>Attachment</th>--%>
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
                                                <td class="text-center" style="width: 1%; padding-left: 8px;">
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("NOTE_NO") %>' OnClick="btnEdit_Click"></asp:LinkButton></td>

                                                <%-- <td style="width: 1%; padding-left: 8px;">
                                                    <asp:ImageButton ID="btnEdit" runat="server"  ImageUrl="~/Images/view.png" CommandArgument='<%# Eval("NOTE_NO") %>'
                                                        ToolTip="To View Notes" AlternateText="Show Record" OnClick="btnEdit_Click" />
                                                </td>--%>
                                                <td style="width: 14%; text-align: left">
                                                    <%# Eval("UNIT_NAME")%>
                                                </td>
                                                <td style="width: 11%; text-align: left">
                                                    <%# Eval("TOPIC_NAME")%>
                                                </td>
                                                <td style="width: 11%; text-align: left">
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td style="width: 5%; text-align: left">
                                                    <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <%--  <td style="width: 5%; text-align: left">
                                                    <%--<img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
                                                     <asp:ImageButton ID="btnimgdow" runat="server"  ImageUrl="~/Images/attachment.png" CommandArgument='<%# Eval("NOTE_NO") %>' ToolTip="DownLoad Assignment" OnClick="btnimgdow_Click" />
                       
                                                </td>--%>
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
                                        <h5>Download attached Lexture  Notes</h5>
                                    </div>


                                    <asp:ListView ID="lvLDoc" runat="server">
                                        <LayoutTemplate>
                                            <table>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Image ID="img2" runat="server" ImageUrl="~/Images/attachment.png" />
                                                    <%--<img alt="Attachment" src="../Images/attachment.png" />--%>
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
                            </asp:Panel>
                        </div>


                        <div class="modal fade" id="preview" role="dialog" style="display: none; margin-left: -100px;">
                            <div class="modal-dialog text-center">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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


                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
