<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="UploadDocs.aspx.cs" Inherits="DCMNTSCN_UploadDocs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">UPLOAD DOCUMENTS</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">

                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Title</label>
                                    </div>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" ToolTip="Enter Title" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                        Display="None" ErrorMessage="Please Enter Title" ValidationGroup="Submit" SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Category</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"
                                        data-select2-enable="true" TabIndex="2" ToolTip="Select Category"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server" ControlToValidate="ddlCategory"
                                        Display="None" ErrorMessage="Please Select Category" ValidationGroup="Submit"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-8 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:Label ID="lbl" runat="server" ToolTip="Path" CssClass="form-control" Enabled="false" />

                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Description</label>
                                    </div>
                                    <asp:TextBox ID="ftbDescription" TextMode="SingleLine" TabIndex="3" CssClass="form-control"
                                        runat="server" ToolTip="Enter Description"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-4 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Keywords</label>
                                    </div>
                                    <asp:TextBox ID="txtKeyword" TextMode="SingleLine" CssClass="form-control" TabIndex="4" runat="server"
                                        onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Keywords"></asp:TextBox>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Attachment</label>
                                    </div>
                                    <%--<asp:Label ID="lblUpld" runat="server" Text="Attachment"></asp:Label>--%>
                                    <asp:FileUpload ID="FileUpload10" runat="server" class="multi" TabIndex="5" ToolTip="Click here to Upload Document" />&nbsp;                                                   
                                       
                                      <asp:Button ID="btnAttachFile" runat="server" Text="Attach File" ToolTip="Click here to Upload Files"
                                            OnClick="btnAttachFile_Click" CssClass="btn btn-primary mt-1" TabIndex="15" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Share</label>
                                    </div>                                  
                                    <asp:CheckBox ID="chkId" runat="server" Text="Yes" ToolTip="Select Yes If You Want To Share" TabIndex="6" />

                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvAttachments" runat="server">
                                <LayoutTemplate>
                                    <table>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <%--<a onmouseOver="window.status='HTMLcenter';return true" onmouseOut="window.status='HTMLcenter';return true" target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%# Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME") %>">
                                                        <%# Eval("ORIGINAL_FILENAME")%></a>&nbsp;&nbsp;(<%# (Convert.ToInt32(Eval("SIZE")) / 1).ToString() %>&nbsp;KB)--%>
                                        <td>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="X" OnClick="lnkDelete_Click" CommandArgument='<%# Eval("ATTACH_ID") %>'
                                                ></asp:LinkButton>  <%--ToolTip='<%# Eval("IDNO") %>' CommandName='<%# Eval("UPLNO")%>'--%>

                                            <img alt="Attachment" src="../../../../Images/attachment.png" />
                                            <a onmouseover="window.status='HTMLcenter';return true" onmouseout="window.status='HTMLcenter';return true"
                                                target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%# Eval("FILE_PATH") %>&filename=<%# Eval("ORIGINAL_FILENAME") %>">
                                                <%# Eval("ORIGINAL_FILENAME")%></a>

                                            <%-- <asp:HiddenField ID="hdnIDNO" runat="server" Value='<%# Eval("IDNO")%>' />--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>


                      <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>

                    <div class=" col-12 btn-footer">
                        <asp:Panel ID="pnlButton" runat="server">
                            <asp:Button ID="Button3" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="7"
                                OnClick="jQueryUploadFiles" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"
                                TabIndex="9" ToolTip="Click here to Go Back" CssClass="btn btn-primary" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click1"
                                TabIndex="8" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <input id="hdnAskSave" runat="server" type="hidden" />

                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="Pancatgrid" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="LVCATDOC" runat="server" >
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Document Uploaded By User
                                            </h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Title
                                                    </th>
                                                    <th>Category
                                                    </th>
                                                    <th>Created Date
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

                                              <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("UPLNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            &nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("UPLNO") %>'
                                                        ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("TITLE")%>
                                        </td>
                                        <td>
                                            <%# Eval("DOCUMENTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <p class="text-center">
                                <asp:Button ID="lnkAdd" runat="server" Text="Add New" OnClick="btnAdd_Click" CssClass="btn btn-primary"
                                    ToolTip="Click here to Upload New Document" TabIndex="10" />
                            </p>
                            <asp:Panel ID="pnlDoc" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvDoc" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Document Uploaded By User  </h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Title
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Created Date
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                    CommandArgument='<%# Eval("UPLNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("UPLNO") %>'
                                                        ToolTip="Delete Record" OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("TITLE")%>
                                            </td>
                                            <td>
                                                <%# Eval("DOCUMENTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("CREATED_DATE", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>


            </div>
        </div>
    </div>

     <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <script src="../JAVASCRIPTS/jquery-1.4.min.js" type="text/javascript"></script>
    <script src="../JAVASCRIPTS/jquery.MultiFile.pack.js" type="text/javascript"></script>

            <tr>
            <%--<td class="vista_page_title_bar" style="height: 30px">UPLOAD DOCUMENTS              
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>--%>
        </tr>
        <%--PAGE HELP--%>
        <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
        <tr>
            <td>
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                    </div>
                    <div>
                        <p class="page_help_head">
                            <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                            <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                            Edit Record&nbsp;&nbsp;
                            <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                            Delete Record
                        </p>
                        <p class="page_help_text">
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                        </p>
                    </div>
                </div>

                <script type="text/javascript" language="javascript">
                    // Move an element directly on top of another element (and optionally
                    // make it the same size)
                    function Cover(bottom, top, ignoreSize) {
                        var location = Sys.UI.DomElement.getLocation(bottom);
                        top.style.position = 'absolute';
                        top.style.top = location.y + 'px';
                        top.style.left = location.x + 'px';
                        if (!ignoreSize) {
                            top.style.height = bottom.offsetHeight + 'px';
                            top.style.width = bottom.offsetWidth + 'px';
                        }
                    }
                </script>
            </td>
        </tr>
    </table>
  
    <table cellpadding="0" cellspacing="0" style="width: 70%">
        <tr>
            <td style="padding-left: 15px;">
                <asp:Button ID="btnAlelrt" runat="server" Text="GetDetails" OnClick="btnAlelrt_Click" />
                <%--<input id="hdnAskSave" runat="server" type="hidden" />--%>
                <%--<asp:Button ID="btnAskSave" runat="server" Text="" OnClick="btnAskSave_Click" Visible ="false"  />--%>
            </td>
        </tr>
    </table>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <div class="text-center">
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
    <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="Button4" PopupControlID="Panel1" OkControlID="Button1"
        CancelControlID="Button2" BackgroundCssClass="modalBackground" />--%>
    <%--<asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/warning.gif" AlternateText="Warning" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to Replace this file?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="Button1" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="Button2" runat="server" Text="No" Width="50px" />
                        <asp:Button ID="Button4" runat="server" Style="display: none;" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>--%>
    <div class="col-md-12">
        <div class="text-center">
            <asp:Panel ID="Panel1" runat="server" Style="display: none" CssClass="modalPopup">
                <div class="text-center">
                    <div class="modal-content">
                        <div class="modal-body">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/warning.png" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="Button1" runat="server" Text="Yes" CssClass="btn-primary" />
                                <asp:Button ID="Button2" runat="server" Text="No" CssClass="btn-primary" />
                                <asp:Button ID="Button4" runat="server" Style="display: none;" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%--ytt--%>
    <%--<asp:Button ID="button1" runat="server" Text="Server_Click" OnClick="button1_Click" />
<asp:Button ID="buttonNull" style="display:none;" runat="server" />
<asp:UpdatePanel ID="updPanel1" runat='server'></asp:UpdatePanel>

<asp:Panel runat="server" ID="Panel1" Width="500" Height="500" style="display:none;"
BackColor="#fafad2" BorderColor="black" BorderStyle="solid" BorderWidth="1px">

<asp:panel runat="server" ID="Panel3" Width="100%" Height="27" BackColor="red">
DragHandle
</asp:panel>

<asp:Button ID="OKButton" runat="server"/>
<asp:button ID="CancelButton" runat="server" />
</asp:Panel>

<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender5" runat="server"
BehaviorID="modalPopupExtender5"
TargetControlID="buttonNull"
PopupControlID="Panel1"
BackgroundCssClass="modalBackground"
OkControlID="OkButton"
OnOkScript="onOk()"
CancelControlID="CancelButton"
DropShadow="true"
PopupDragHandleControlID="Panel3" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="upd_ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
       DropShadow="true" PopupControlID="pnl" OkControlID="btnBack" TargetControlID="btnForPopUpModel2">
    </ajaxToolKit:ModalPopupExtender>
     <asp:Panel ID="pnl" runat="server" Style="width: 600px" BorderColor="#0066FF">
        <asp:Button ID="btnPrint" runat="server" Text="Yes" ValidationGroup="Validation"
              Width="20%" OnClick="btnPrint_Click" />
        &nbsp;<asp:Button ID="Button1" runat="server" Text="No" ValidationGroup="Validation"
                Width="20%" OnClick="btnBack1_Click" />                                 
              </asp:Panel>  
      <asp:Button ID="btnForPopUpModel2" Style="display: none" runat="server" Text="For PopUp Model Box" />--%>
    <%--ytt--%>

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

        function PopupModal() {
            var modal = $find('modalPopupExtender5');

            if (modal) {
                if (modal.show) {
                    modal.show();
                }
                else {
                    alert("nope!");
                }
            }
            else {
                throw modal;
            }
        }
        function onOk() {
        }


        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        //        ctl00_
        function AskSave() {
            if (confirm('The same file name already exist, Do u want to replace it? ') == true) {
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 0;
                return false;
            }
        }


        function ShowConfirmation() {

            if (confirm("The same file name already exist, Do u want to replace it? ") == true) {
                document.getElementById("<%= btnAlelrt.ClientID %>").click();
                __doPostBack(this._source.name, '');
                //Calling the server side code after confirmation from the user
                //                document.getElementById("btnAlelrt").click();

            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        // show more file upload
        function ShowHideFileUpload(id) {
            document.getElementById('divShowMore').style.display = '';
            document.getElementById(id).style.display = 'none';
        }
    </script>



   <%-- <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            debugger;
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it 
            debugger;
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

        function PopupModal() {
            var modal = $find('modalPopupExtender5');

            if (modal) {
                if (modal.show) {
                    modal.show();
                }
                else {
                    alert("nope!");
                }
            }
            else {
                throw modal;
            }
        }
        function onOk() {
        }


        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        //        ctl00_
        function AskSave() {
            if (confirm('The same file name already exist, Do u want to replace it? ') == true) {
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 1;
                return true;
            }
            else {
                document.getElementById('ContentPlaceHolder1_hdnAskSave').value = 0;
                return false;
            }
        }


        function ShowConfirmation() {

            if (confirm("The same file name already exist, Do u want to replace it? ") == true) {
                document.getElementById("<%= btnAlelrt.ClientID %>").click();
                __doPostBack(this._source.name, '');
                //Calling the server side code after confirmation from the user
                //                document.getElementById("btnAlelrt").click();

            }

        }
    </script>

    <script language="javascript" type="text/javascript">
        // show more file upload
        function ShowHideFileUpload(id) {
            document.getElementById('divShowMore').style.display = '';
            document.getElementById(id).style.display = 'none';
        }
    </script>--%>

</asp:Content>
