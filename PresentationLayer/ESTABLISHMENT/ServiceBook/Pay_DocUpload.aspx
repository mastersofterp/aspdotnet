<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ServiceBookMaster.master" CodeFile="Pay_DocUpload.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_DocUpload" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%--    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div id="div2" runat="server"></div>
            <form role="form">
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="panel panel-info">
                            <%--<div class="panel-heading">Miscellaneous Information </div>--%>
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <div class="box box-primary">
                                        <div class="box-header with-border">
                                            <h1 class="box-title"><b>Document Upload</b></h1>
                                        </div>
                                        <div class="box-tools pull-right">
                                            <div class="pull-right">
                                                <%--<div style="color: Red; font-weight: bold">
                                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                            </div>--%>
                                                <div class="col-sm-12" style="color: Red; font-weight: bold; padding-top: 15px">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            Note :1. Upload the Documents only with following formats: .jpg, .jpeg, .pdf
                                                        </div>
                                                        <div class="row" style="padding-left: 8%">
                                                            2. Upload Documents File Size Should be maximum 512 KB.
                                                        </div>
                                                        <div class="row" style="padding-left: 8%">
                                                            3. * Marked Documents are mandatory.
                                                        </div>
                                                        <%--add--%>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>



                                        <div class="box-body" style="height: 500px;">
                                            <div class="col-md-12" id="divDocumentUpload" style="display: block;">
                                                <%--  <asp:UpdatePanel ID="upDocumentUpload" runat="server">
                                            <ContentTemplate>--%>





                                                <div class="row">
                                                    <%--   <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                        <strong>
                                                            <h4><b>Permenant Address</b></h4>
                                                        </strong>
                                                    </div>--%>

                                                    <%--  Added By Prafull Muke--%>

                                                    <div class="col-12" style="margin-top: 10px;">
                                                        <asp:UpdatePanel ID="updDocument" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnlBind" runat="server">
                                                                    <asp:Label ID="lblmessageShow" runat="server"></asp:Label>
                                                                    <asp:ListView ID="lvBinddata" runat="server" OnItemDataBound="lvBinddata_ItemDataBound">
                                                                        <%--OnItemDataBound="lvBinddata_ItemDataBound1"--%>
                                                                        <LayoutTemplate>
                                                                            <div id="demo-grid">
                                                                                <div class="sub-heading">
                                                                                    <h5>Document List</h5>
                                                                                </div>
                                                                                <div style="width: 100%; height: 370px; overflow: auto">
                                                                                    <table class="table table-striped table-bordered nowrap">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th>Sr.NO</th>
                                                                                                <%-- <th>Document No.</th>--%>

                                                                                                <th>Document Name</th>
                                                                                                <th>Upload Document</th>
                                                                                                <th>File</th>
                                                                                                <th>Preview</th>
                                                                                                <th>Upload</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td><%# Container.DataItemIndex +1 %></td>
                                                                                <%-- <td style="width:5%"><%# Eval("DOCUMENTNO") %></td>--%>

                                                                                <td>
                                                                                    <asp:Label ID="lblStar" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblDocument" Width="150px" ToolTip='<%# Eval("PHOTO_TYPEID") %>' runat="server" Text='<%# Eval("PHOTO_TYPE") %>'></asp:Label>
                                                                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("PHOTO_TYPEID") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:FileUpload ID="fuFile" runat="server" ToolTip='<%# Eval("PHOTO_TYPEID") %>' />
                                                                                    <asp:HiddenField ID="hdnFile" runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblFile" runat="server" ToolTip="" Text='<%# Eval("UPLOADED")%>'></asp:Label>
                                                                                </td>
                                                                                <td style="text-align: center">
                                                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                                                        <ContentTemplate>
                                                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click"
                                                                                                Text="Preview" ImageUrl="~/Images/search-svg.png" ToolTip='<%# Eval("DOCUMENT_NAME") %>' data-toggle="modal" data-target="#preview"
                                                                                                CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                                            <%--   <asp:ImageButton ID="imgbtnPreviewdoc" runat="server" OnClick="imgbtnPrevDoc_Click1" data-toggle="modal" data-target="#PassModel"
                                                                        Text="Preview" ImageUrl="~/Images/search-svg.png" Height="32px"
                                                                        Width="30px" CommandArgument='<%# Eval("DOCUMENT_NAME") %>' Visible='<%# Convert.ToString(Eval("DOCUMENT_NAME"))==string.Empty?false:true %>'></asp:ImageButton>--%>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td>
                                                                                    <%--<asp:UpdatePanel ID="updSubmit" runat="server">--%>
                                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Upload" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                                                                        CssClass="btn btn-info" CommandArgument='<%# Eval("PHOTO_TYPEID") %>' ToolTip='<%# Eval("PHOTO_TYPEID") %>' />
                                                                                    <%--</asp:UpdatePanel>--%>
                                                                                </td>

                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="lvBinddata" />
                                                            </Triggers>

                                                        </asp:UpdatePanel>

                                                    </div>



                                                    <%-- <asp:ListView ID="lvDocumentList" runat="server" OnItemDataBound="lvDocumentList_ItemDataBound">--%>
                                                </div>



                                                <%--  </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </div>
                                        </div>

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


                                        <div class="box box-footer text-center">
                                            <asp:Button ID="btnSave" runat="server" TabIndex="29" Text="Save & Continue >>" ToolTip="Click to Report"
                                                class="btn btn-success" ValidationGroup="Academic" />
                                            <%--OnClick="btnSave_Click"--%>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="Academic" />
                                            &nbsp
                                            <button runat="server" id="btnGohome" visible="false" tabindex="30" class="btn btn-warning btnGohome" tooltip="Click to Go Back Home">
                                                <%--onserverclick="btnGohome_Click"--%>
                                                <i class="fa fa-home"></i>Go Back Home
                                            </button>
                                        </div>
                                        <%--DigiLocker--%>
                                        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                        <ajaxToolKit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlModal" TargetControlID="lnkDummy"
                                            CancelControlID="btnCloseModal" BackgroundCssClass="modalBackground">
                                        </ajaxToolKit:ModalPopupExtender>



                                        <%--  Digilocker--%>

                                        <div class="modal fade" id="PassModel" role="dialog" style="display: none;">
                                            <div class="modal-dialog text-center">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <!-- Modal content-->
                                                        <div class="modal-content" style="width: 900px;">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                <h4 class="modal-title">Document</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="col-md-12">
                                                                    <iframe runat="server" style="width: 800px; height: 1100px" id="iframeView"></iframe>
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
                                    </div>
                                </div>

                            </div>
                        </div>
            </form>
        </div>
    </div>
    <%--</ContentTemplate>
        <Triggers>
             
        </Triggers>
    </asp:UpdatePanel>--%>

    <script>

        document.getElementById('btnSubmit').onclick = changeColor;

        function changeColor() {
            document.body.style.color = "green";
            return false;
        }

    </script>

</asp:Content>

