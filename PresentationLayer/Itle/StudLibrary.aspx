<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudLibrary.aspx.cs" Inherits="Itle_StudLibrary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <style>
        .list-group .list-group-item .sub-label {
            float: initial;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">E-BOOK LIST FOR ITLE SESSION</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlStudLibrary" runat="server">
                                <div class="col-12">
                                    <asp:Panel ID="pnlStudentLibrary" runat="server">
                                        <div class="row">
                                            <%--  <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>E-Book Details</h5>
                                                </div>
                                            </div>--%>
                                            <div class="col-lg-5 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Session Term :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-7 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Course  :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div id="tblBookInfo" runat="server" visible="false">
                                            <div class="row mt-3">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>E-Book Name:</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblBookName" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Author Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAuthorName" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Publisher Name :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblPublisher" runat="server" Font-Bold="true"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                    <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                    <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                </div>
                                                <div class="form-group col-lg-4 col-md-6 col-12 mt-3">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>File attached with E-Book</label>
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

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-primary"
                                                        ToolTip="Click here to Go Back" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 mt-3">
                                    <div id="DivLibraryList" runat="server" visible="false">
                                        <div class="sub-heading">
                                            <h5>E-Book List</h5>
                                        </div>
                                        <asp:Panel ID="pnlLibraryList" runat="server" ScrollBars="Vertical" Height="250px" BackColor="#FFFFFF">
                                            <asp:ListView ID="lvLibrary" runat="server" DataKeyNames="Book_No">
                                                <LayoutTemplate>

                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th class="text-center">View</th>
                                                                <th>Book Name</th>
                                                                <th>Author Name</th>
                                                                <th>WebLink</th>
                                                                <%-- <th>Attachment</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td class="text-center">
                                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-eye" CommandArgument='<%# Eval("BOOK_NO") %>' OnClick="btnEdit_Click"></asp:LinkButton></td>

                                                        <%--<asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/view.png"  CommandArgument='<%# Eval("BOOK_NO") %>'
                                                                ToolTip="To View E-BOOK" AlternateText="Show Record" OnClick="btnEdit_Click"/>
                                                        --%>
                                                        <td>
                                                            <%# Eval("BOOK_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("AUTHOR_NAME")%>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# Eval("WEBSITE_LINK")%>'
                                                                Text='<%# Eval("WEBSITE_LINK")%>'>
                                                            </asp:HyperLink>
                                                        </td>
                                                        <%-- <td>

                                                           <asp:ImageButton ID="imgbtndow" runat="server" ImageUrl="~/Images/attachment.png" CommandArgument='<%# Eval("BOOK_NO") %>'  OnClick="imgbtndow_Click" />
                                                            <%--<img alt="Attachment" src="../IMAGES/attachment.png" class='<%# (Convert.ToInt32(Eval("ATTACHMENT")) > 0)? "show_img": "hide_img" %>' />
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
                                                            <asp:Image ID="img2" runat="server" ImageUrl="~/Images/attachment.png" />
                                                            <%--<img alt="Attachment" src="../IMAGES/attachment.png" />--%>
                                                            <a class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH")%>&filename=<%# Eval("FILE_NAME") %>">
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

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
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                            <asp:HiddenField ID="hdnfilename" runat="server" />
                            <asp:Button ID="BTNCLOSEDOC" runat="server" Text="CLOSE" OnClick="BTNCLOSEDOC_Click" OnClientClick="CloseModal();return true;" CssClass="btn btn-outline-danger" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
