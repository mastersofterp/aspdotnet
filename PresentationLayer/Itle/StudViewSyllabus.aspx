<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudViewSyllabus.aspx.cs" Inherits="Itle_StudViewSyllabus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    <asp:Panel ID="UpdatePanel1" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">SYLLABUS DESCRIPTION</h3>
                    </div>
                    <div class="box-body">
                        <asp:Panel ID="pnlViewSyllabus" runat="server">
                            <div class="col-12">
                                <asp:Panel ID="pnlStudView" runat="server">
                                    <div class="row">
                                        <%--  <div class="sub-heading">View Syllabus</div>--%>
                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Session  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSession" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>

                                        </div>
                                        <div class="col-lg-7 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Course  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCourseName" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="showSyllabus" runat="server" visible="false">
                                            <div class="label-dynamic">
                                            </div>
                                            <div id="divDesc" runat="server">
                                                <div class="titlebar">Complete Syllabus Model Of Selected Course</div>
                                                <asp:TreeView ID="tvSyllabus" runat="server">
                                                </asp:TreeView>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnViewSyllabus" runat="server" Text="View Syllabus" CssClass="btn btn-primary"
                                            OnClick="btnViewSyllabus_Click" ToolTip="Click here to View Syllabus" />
                                        <%--<asp:Button ID="btnViewSyllabus" runat="server" Text="View Syllabus" BackColor="#9ba7c6"
                                                            OnClick="btnViewSyllabus_Click" BorderColor="#000066" ToolTip="Click here to View Syllabus" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                    </div>

                                </asp:Panel>
                            </div>

                            <div class="col-12">
                                <div id="DivSyllabusList" runat="server" visible="false">
                                    <div class="sub-heading">
                                        <h5>Syllabus List</h5>
                                    </div>


                                    <asp:Panel ID="pnlSyllabusList" runat="server">
                                        <asp:ListView ID="lvSyllabus" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Syllabus Name</th>
                                                            <th>Unit Name</th>
                                                            <th>Topic Name</th>
                                                            <th>Plan Date</th>
                                                            <th id="divDownload" runat="server" visible="false">Attachment
                                                            </th>
                                                            <th id="divBlobDownload" runat="server" visible="false">Attachment
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
                                                    <td id="tdDownloadLink" runat="server" visible="false">
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text='<%# Eval("ATTACHMENT") %>'
                                                            ToolTip='<%# Eval("ATTACHMENT")%>' OnClick="lnkDownload_Click" CommandArgument='<%#Eval("SUB_NO")%>'>
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("File_Path") %>'
                                                                    data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("File_Path") %>' Visible='<%# Convert.ToString(Eval("File_Path"))==string.Empty?false:true %>'></asp:ImageButton>
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
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function CloseModal() {
                $("#preview").modal("hide");
            }
            function ShowModal() {
                $("#preview").modal("show");
            }
</script>

    </asp:Panel>
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
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>

                            <asp:HiddenField ID="hdnfilename" runat="server" />
                            <asp:Button ID="BTNCLOSE" runat="server" Text="CLOSE" OnClick="BTNCLOSE_Click" OnClientClick="CloseModal();return true;" CssClass="btn btn-outline-danger" />
                        </div>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


</asp:Content>

