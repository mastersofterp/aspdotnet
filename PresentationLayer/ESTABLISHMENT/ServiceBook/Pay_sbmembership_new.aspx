<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ServiceBookMaster.master" CodeFile="Pay_sbmembership_new.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_sbmembership_new" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <%-- <from>--%>

                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Membership of Professional Body</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlFm" runat="server">

                        <div class="panel panel-info">
                            <%--<div class="panel panel-heading">Membership in Professional body</div>--%>
                            <div class="panel panel-body">
                                <%-- Modified by Saahil Trivedi 25/01/2022--%>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <label>Name of the professional body : <span style="color: #FF0000">*</span></label>
                                        </div>
                                        <asp:TextBox ID="txtNameProfessional" runat="server" TabIndex="1" MaxLength="200" CssClass="form-control" TextMode="MultiLine"
                                            ToolTip="Enter Name of the professional body" onkeypress="return CheckAlphabet(event,this);"
                                            onkeyDown="checkTextAreaMaxLength(this,event,'200');" onkeyup="textCounter(this, this.form.remLen, 200);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAwardName" runat="server" ControlToValidate="txtNameProfessional"
                                            Display="None" ErrorMessage="Please Enter Name Of professional body" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Membership Number : <span style="color: #FF0000">*</span></label>
                                        </div>
                                        <asp:TextBox ID="txtmemberno" runat="server" TabIndex="2" MaxLength="30" CssClass="form-control"
                                            ToolTip="Enter Membership Number"></asp:TextBox>
                                        <%--onkeypress="return CheckNumeric(event,this);"--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtmemberno"
                                            Display="None" ErrorMessage="Please Enter Membership number" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbememno" runat="server" TargetControlID="txtmemberno" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Member Type :<span style="color: #FF0000">*</span></label>
                                        </div>
                                        <asp:TextBox ID="txtmembertype" runat="server" TabIndex="3" MaxLength="200" CssClass="form-control"
                                            ToolTip="Enter Member Type "></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmembertype"
                                            Display="None" ErrorMessage="Please Enter Member Type" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Other Member Type :</label>
                                        </div>
                                        <asp:DropDownList ID="ddlmembertype" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            CssClass="form-control" ToolTip="Select Award Level" TabIndex="4">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Lifetime</asp:ListItem>
                                            <asp:ListItem Value="2">Annual</asp:ListItem>
                                            <asp:ListItem Value="3">Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Year : <span style="color: #FF0000">*</span></label>
                                        </div>
                                        <asp:TextBox ID="txtyear" runat="server" TabIndex="5" MaxLength="4" CssClass="form-control"
                                            ToolTip="Enter year" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtyear"
                                            Display="None" ErrorMessage="Please Enter Year" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revyear" runat="server" ControlToValidate="txtyear" ValidationExpression="^[0-9]{4}$"
                                            ErrorMessage="Please Enter Valid Year" ValidationGroup="ServiceBook" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>


                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtyear" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                        <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                        <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                    </div>
                                    <%--     
                                                    <div class="form-group col-md-12">
                                                     <label>Upload Document :</label>
                                                         <asp:FileUpload ID="flupld" runat="server" TabIndex="10" ToolTip="Click to  Upload Document"/>
                                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <div class="col-md-12">
                    <asp:UpdatePanel ID="pnlnew" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server">
                                <%--<div class="panel panel-info">
                                        <div class="panel-heading">--%>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Upload Files</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%-- Upload Files--%>
                                <%--</div>--%>
                                <div class="panel-body">
                                    <div class="form-group row" id="divUploadFiles" style="display: block;">
                                        <div class="col-md-6">
                                            <label>Multiple Files Can Be Attached :</label>
                                            <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="6" ToolTip="Upload Multiple Files Here" /><br />
                                            <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg,.doc) upto 10MB" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <br />
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="7" class="btn btn-primary" OnClick="btnAdd_Click"
                                                ToolTip="Click here to uplaod multiple files" />
                                            <%----%>
                                        </div>
                                        <div id="divAttch" runat="server" style="display: none">
                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlfiles" runat="server">
                                                    <asp:ListView ID="LVFiles" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Attached Files</h5>
                                                            </div>

                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <%--<th>Delete</th>                                                                        
                                                                        <th>File Name</th>--%>
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
                                                            <%--</div>--%>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <%--<td>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                    CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDelFile_Click" />
                                                            <td>
                                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                            </td>--%>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                        CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record"
                                                                        OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="btnDelFile_Click" />
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
                                                                    <%-- <a target="_blank" class="mail_pg" href="DownloadAttachment.aspx?file=<%#Eval("FILE_PATH") %>&filename=<%# Eval("FILE_NAME")%>">
                                                                    --%>      <%# Eval("DisplayFileName")%></a>&nbsp;&nbsp;
                                                            
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
                                </div>
                                <%--</div>--%>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnAdd" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>


                <div class="form-group col-md-12">
                    <p class="text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="8"
                            CssClass="btn btn-primary" OnClick="btnSubmit_Click" ToolTip="Click here to Submit" />&nbsp;
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="9"
                                                                CssClass="btn btn-warning" OnClick="btnCancel_Click1" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </p>
                </div>

                <div class="col-md-12">
                    <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                        <asp:ListView ID="lvAchiveInfo" runat="server">
                            <EmptyDataTemplate>
                                <br />
                                <p class="text-center text-bold">
                                    <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Membership in Professional Details"></asp:Label>
                                </p>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Membership in Professional Details
                                    </h5>
                                </div>
                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <%-- <table class="table table-bordered table-hover">--%>
                                    <thead>
                                        <tr class="bg-light-blue">
                                            <th>Action
                                            </th>
                                            <th>Name of the professional body
                                            </th>
                                            <th>Membership No.
                                            </th>
                                            <th>Memebrship Type
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
                                    <%-- Modified by Saahil Trivedi 25/01/2022--%>
                                    <td>
                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("MPNO")%>'
                                            AlternateText="Edit Record" OnClick="btnEdit_Click" ToolTip="Edit Record" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("MPNO") %>'
                                            AlternateText="Delete Record" OnClick="btnDelete_Click" ToolTip="Delete Record"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td>
                                        <%# Eval("NAME_PROF_BODY")%>
                                    </td>
                                    <td>
                                        <%# Eval("MemberShipNo")%>
                                    </td>
                                    <td>
                                        <%# Eval("MemberShipType")%>
                                    </td>

                                    <%--   <td>
                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ACNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                        </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </asp:Panel>
                </div>


            </div>

            </from>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
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

        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only.');
                obj.focus();
            }
            return false;
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

    <script type="text/javascript">
        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }

        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }

    </script>

</asp:Content>

