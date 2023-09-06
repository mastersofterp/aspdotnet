<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_publication_Details1.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_publication_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />


    <%-- <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>--%>
    <br />
    <div class="row">
        <div class="col-md-12">
            <form role="form">
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="panel panel-info">
                                    <div class="panel panel-heading">Publication Details</div>
                                    <div class="panel panel-body">
                                        <asp:UpdatePanel ID="updImage" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group col-md-6">
                                                    <label><span style="color: #FF0000">*</span> Publication Category :</label>
                                                    <asp:DropDownList ID="ddlPublication" runat="server" CssClass="form-control"
                                                        TabIndex="4" ToolTip="Select Publication Category">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="Conference">Conference</asp:ListItem>
                                                        <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                                        <asp:ListItem Value="Book">Book Chapter</asp:ListItem>
                                                        <%--<asp:ListItem Value="Paper">Paper</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPublication"
                                                        Display="None" ErrorMessage="Please Select Publication Category" ValidationGroup="ServiceBook" InitialValue="0"
                                                        SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label><span style="color: #FF0000">*</span>Publication Type :</label>
                                                    <asp:DropDownList ID="ddlPublicationType" runat="server" CssClass="form-control"
                                                        ToolTip="Select Publication Type" TabIndex="5">
                                                          <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="National">National</asp:ListItem>
                                                        <asp:ListItem Value="InterNational">Inter National</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RfvPublicationType" runat="server" ControlToValidate="ddlPublicationType"
                                                        Display="None" ErrorMessage="Please Select Publication Type" ValidationGroup="ServiceBook" InitialValue="0"
                                                        SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Name Of The Authors :</label>
                                                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" Enabled="false" onkeypress="return CheckAlphabet(event,this);"
                                                        ToolTip="Name Of The Authors 1" TabIndex="6"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:TextBox ID="txtAuthor2" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Name Of The Authors 2" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:TextBox ID="txtAuthor3" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Name Of The Authors 3" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <asp:TextBox ID="txtAuthor4" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Name Of The Authors 4" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label><span style="color: #FF0000">*</span> Title of Paper :</label>
                                                    <asp:TextBox ID="txttitle" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Title" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtitle" runat="server" ControlToValidate="txttitle"
                                                        Display="None" ErrorMessage="Please Enter Title" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-12" id="trnamejou" runat="server" visible="false">
                                                    <label>Name of Journal/Conference :</label>
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ToolTip="Enter Name of Journal/Conference" MaxLength="100" TabIndex="10" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12" id="trOrg" runat="server" visible="false">
                                                    <label>Organised By :</label>
                                                    <asp:TextBox ID="txtOrg" runat="server" CssClass="form-control" ToolTip="Enter Organised By" MaxLength="100" TabIndex="11" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label><span style="color: #FF0000">*</span> Title of Journal :</label>
                                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" ToolTip="Enter Subject" TabIndex="12" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                                        Display="None" ErrorMessage="Please Enter  Title of Journal" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True"> </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-12" id="trPageno" runat="server" visible="false">
                                                    <label>Page NO :</label>
                                                    <asp:TextBox ID="txtPage" runat="server" CssClass="form-control" ToolTip="Enter Page Number" TabIndex="13" MaxLength="4" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12" id="Div1">
                                                    <label>ISBN :</label>
                                                    <asp:TextBox ID="txtIsbn" runat="server" CssClass="form-control" ToolTip="Enter Page ISBN" TabIndex="13"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Publication Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtPublicationDate" runat="server" CssClass="form-control" ToolTip="Enter Publication Date"
                                                            TabIndex="14" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPublicationDate"
                                                            PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <%--<asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtPublicationDate"
                                                        Display="None" ErrorMessage="Please Select Publication Date in (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True"> </asp:RequiredFieldValidator>--%>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtPublicationDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtPublicationDate" EmptyValueMessage="Please Enter Publication Date"
                                                            InvalidValueMessage="Publication Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Publication Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Abstract :</label>
                                                    <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" ToolTip="Enter Details" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"
                                                        TabIndex="15" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Attachments :</label>
                                                    <asp:FileUpload ID="flPUB" runat="server" />
                                                    <span style="color:red">Upload File Maximum Size 10 Mb</span>
                                                    <%--<asp:FileUpload ID="flPUB" runat="server" ToolTip="Click here to Upload Attachments" TabIndex="16"/>--%>
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="17"
                                                            OnClick="btnSubmit_Click" CssClass="btn btn-success" ToolTip="Click here to Submit" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="18"
                                                    OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </p>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-md-6">
                            <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvPublicationDetails" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Publication Details"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <h4 class="box-title">Publication Details
                                            </h4>
                                            <table class="table table-bordered table-hover">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Publication Type
                                                        </th>
                                                        <th>Title
                                                        </th>
                                                        <th>Subject
                                                        </th>
                                                        <th>Publication Date
                                                        </th>
                                                        <th>Uploaded File
                                                        </th>
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
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("PUBTRXNO")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("PUBTRXNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("PUBLICATION_TYPE")%>
                                            </td>
                                            <td>
                                                <%# Eval("TITLE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SUBJECT")%>
                                            </td>
                                            <td>
                                                <%# Eval("PUBLICATIONDATE", "{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PUBTRXNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
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
        onkeypress = "return CheckAlphabet(event,this);"
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
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>





</asp:Content>

