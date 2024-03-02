<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ServiceBookMaster.master" CodeFile="Pay_Sb_RevenueGenerated.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_RevenueGenerated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Revenue Generated</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" TabIndex="1" runat="server" CssClass="form-control" ToolTip="Select Year" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" ValidationGroup="ServiceBook"
                                            SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                          <div class="label-dynamic">
                                          <sup>* </sup>
                                          <label>Revenue Generated Through VAC (INR) :</label>
                                          </div>
                                          <asp:TextBox ID="txtRGTVAC" runat="server" CssClass="form-control" ToolTip="Enter Revenue Generated Through VAC (INR)" TabIndex="2" MaxLength="20" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvVAC" runat="server" ControlToValidate="txtRGTVAC"
                                            Display="None" ErrorMessage="Please Enter Revenue Generated Through VAC (INR)" ValidationGroup="ServiceBook"
                                            SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                          <div class="label-dynamic">
                                          <sup>*</sup>
                                          <label>Revenue Generated Through Conducting Events / Training</label>
                                          </div>
                                          <asp:TextBox ID="txtRGTEvents" runat="server" CssClass="form-control" ToolTip="Enter Revenue Generated Through Conducting Events/Training" TabIndex="3" MaxLength="20" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvEvents" runat="server" ControlToValidate="txtRGTEvents"
                                            Display="None" ErrorMessage="Please Enter Revenue Generated Through Conducting Events/Training" ValidationGroup="ServiceBook"
                                            SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                          <div class="label-dynamic">
                                          <sup>* </sup>
                                          <label>Revenue Generated Through Sponsorship</label>
                                          </div>
                                          <asp:TextBox ID="txtRGTSponsor" runat="server" CssClass="form-control" ToolTip="Enter Revenue Generated Through Sponsorship" TabIndex="4" MaxLength="20" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvSponsor" runat="server" ControlToValidate="txtRGTSponsor"
                                            Display="None" ErrorMessage="Please Enter Revenue Generated Through Sponsorship" ValidationGroup="ServiceBook"
                                            SetFocusOnError="true" ></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Web Link</label>
                                            </div>
                                            <asp:TextBox ID="txtWebLink" runat="server" CssClass="form-control" ToolTip="Enter Web Link" TabIndex="5" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvWebLink" runat="server" ControlToValidate="txtWebLink"
                                            Display="None" ErrorMessage="Please Enter Web Link" ValidationGroup="ServiceBook" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revWebLink" runat="server" ControlToValidate="txtWebLink"
                                                Display="None" ErrorMessage="Invalid Web Link Format" ValidationGroup="ServiceBook"
                                                ValidationExpression="^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$"></asp:RegularExpressionValidator>
                                        </div>


                                         <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Upload Files</label>
                                                    <label>Multiple Files Can Be Attached</label>
                                                </div>
                                                <asp:FileUpload ID="flupld" runat="server" TabIndex="21" ToolTip="Upload Multiple Files Here"/><br />
                                                <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="22" class="btn btn-primary" OnClick="btnAdd_Click"
                                                    ToolTip="Click here to uplaod multiple files" />
                                        </div>

                                         <div class="form-group col-lg-4 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divAttch" runat="server" style="display: none">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvCompAttach" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Delete</th>
                                                                <th id="divattach" runat="server">Attachments  
                                                                </th>
                                                                <th id="divattachblob" runat="server" visible="false">Attachments
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
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnDeleteFile" runat="server" ImageUrl="~/Images/delete.png"
                                                                CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDeleteFile_Click" />
                                                        </td>
                                                        <td id="attachfile" runat="server">
                                                            <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("DisplayFileName") %>&filename=<%# Eval("DisplayFileName")%>">
                                                                <%# Eval("DisplayFileName")%></a>
                                                        </td>
                                                        <td id="attachblob" runat="server" visible="false">
                                                            <%# Eval("DisplayFileName")%></a>
                                                        </td>

                                                        <td id="tdDownloadLink" runat="server" visible="false">
                                                            <img alt="Attachment" src="../IMAGES/attachment.png" />
                                                            <%# Eval("DisplayFileName")%></a>&nbsp;&nbsp;
                                                
                                                        </td>
                                                        <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                            <asp:UpdatePanel ID="updPreview" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                        data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>

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
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="5"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="6"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlRevenue" runat="server">
                                    <asp:ListView ID="lvRevenue" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Revenue Generated"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Revenue Generated</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>RGT VAC(INR)
                                                        </th>
                                                        <th>RGT Events
                                                        </th>
                                                        <th>RGT Sponsorship
                                                        </th>
                                                        <th>Web Link
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RGNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("RGNO") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("YEAR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RGT_VAC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RGT_EVENT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("RGT_SPONSOR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("WEBLINK")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <div class="col-md-12">
                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                    <div class="text-center">
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
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

                function CheckNumeric(event, obj) {
                    var k = (window.event) ? event.keyCode : event.which;
                    //alert(k);
                    if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                        obj.style.backgroundColor = "White";
                        return true;
                    }
                    if (k > 45 && k < 58) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter numeric Value');
                        obj.focus();
                    }
                    return false;
                }
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
