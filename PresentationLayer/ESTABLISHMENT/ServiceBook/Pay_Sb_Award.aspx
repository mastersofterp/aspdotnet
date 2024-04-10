<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_Award.aspx.cs"
    Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_Award" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">


                    <div class="col-md-12">

                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Award</h5>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="pnlaward" runat="server">

                            <div class="panel panel-info">

                                <div class="panel panel-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Award Name : <span style="color: #FF0000">*</span></label>
                                                </div>
                                                <asp:TextBox ID="txtAward" runat="server" TabIndex="1" MaxLength="200" CssClass="form-control"
                                                    ToolTip="Enter Award Name" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAward" runat="server" ControlToValidate="txtAward"
                                                    Display="None" ErrorMessage="Please Enter Award Name" SetFocusOnError="True" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Award Level:</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAward" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                    CssClass="form-control" ToolTip="Select Award Level" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">National</asp:ListItem>
                                                    <asp:ListItem Value="2">International</asp:ListItem>

                                                </asp:DropDownList>
                                                <%--  <asp:RequiredFieldValidator ID="rfvawardtype" runat="server" ControlToValidate="ddlAwardlevel"
                                                        Display="None" ErrorMessage="Please SelectAward Level" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Organization Address : <span style="color: #FF0000">*</span></label>
                                                </div>
                                                <asp:TextBox ID="txtOrganizationAdd" runat="server" TabIndex="3" MaxLength="300" CssClass="form-control"
                                                    ToolTip="Enter Organization Address" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvOragAdd" runat="server" ControlToValidate="txtOrganizationAdd"
                                                    Display="None" ErrorMessage="Please Enter Organization Address" SetFocusOnError="true" ValidationGroup="ServiceBook"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Date Received : <span style="color: #FF0000">*</span></label>
                                                </div>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgcal" runat="server" class="fa fa-calendar text-blue" />
                                                    </div>
                                                    <asp:TextBox ID="txtDateOftalk" runat="server" CssClass="form-control" ToolTip="Enter Date Received"
                                                        TabIndex="4" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDateOftalk"
                                                        PopupButtonID="imgcal" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtDateOftalk"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                        ControlToValidate="txtDateOftalk" EmptyValueMessage="Please Enter Date of talk"
                                                        InvalidValueMessage="Date of Received is Invalid (Enter dd/mm/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Date of talk" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                    <asp:RequiredFieldValidator ID="rfvPublicationDate" runat="server" ControlToValidate="txtDateOftalk"
                                                        Display="None" ErrorMessage="Please Select Date of Received (dd/MM/yyyy Format)"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Amount Received :</label>
                                                </div>
                                                <asp:TextBox ID="txtAmount" runat="server" TabIndex="5" CssClass="form-control"
                                                    ToolTip="Enter Amount Received" onkeypress="return CheckNumeric(event, this);" MaxLength="10"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                                             Display="None" ErrorMessage="Please Enter Amount Received" ValidationGroup="ServiceBook" ></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeamount" runat="server" TargetControlID="txtAmount"
                                                    ValidChars="0123456789." Enabled="True">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Description: </label>
                                                </div>
                                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="300" TabIndex="6" CssClass="form-control"
                                                    ToolTip="Please Enter Description"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Issuing Organization :</label>
                                                </div>
                                                <asp:TextBox ID="txtIssueOrg" runat="server" MaxLength="300" TabIndex="7" CssClass="form-control"
                                                    ToolTip="Please Enter Issuing Organization"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Upload Files</label>
                                                    <label>Multiple Files Can Be Attached :</label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="8" ToolTip="Upload Multiple Files Here" />
                                                    <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .pdf,.jpg) upto 5MB" ForeColor="Red"></asp:Label>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" TabIndex="9" class="btn btn-primary" OnClick="btnAdd_Click"
                                                        ToolTip="Click here to uplaod multiple files" />
                                                    <%--<div class="form-group col-md-4">
                                        <label>Upload Document :</label>
                                        <asp:FileUpload ID="flupld" runat="server" TabIndex="10" ToolTip="Click to  Upload Document" />
                                    </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                        </asp:Panel>
                    </div>


                    <div class="col-md-12">
                        <asp:UpdatePanel ID="pnlnew" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel2" runat="server">
                                    <div class="panel panel-info">
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Panel ID="pnlfiles" runat="server">
                                            <asp:ListView ID="LVFiles" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <h4>Attached Files</h4>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Delete</th>
                                                                    <%-- <th>File Name</th>--%>
                                                                    <th>File Name</th>
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
                                                            <asp:ImageButton ID="imgbtnfiledelete" runat="server" ImageUrl="~/Images/delete.png"
                                                                CommandArgument=' <%#Eval("GETFILE") %>' AlternateText=' <%#Eval("APPID") %>' ToolTip="Delete Record" OnClick="imgbtnfiledelete_Click"
                                                                OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                            <%-- --%>
                                                        </td>
                                                        <%--<td>
                                                                                        <%#Eval("GETFILE") %>
                                                                                    </td>--%>
                                                        <td>
                                                            <%--<asp:ImageButton ID="imgFile" runat="Server" ImageUrl="~/images/action_down.gif" CommandArgument='<%#Eval("GETFILE") %>'
                                                                                            AlternateText='<%#Eval("FILEPATH") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                                            OnClick="imgdownload_Click" />--%>
                                                            <%-- OnClick="imgdownload_Click"--%>
                                                            <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    </div>
                                        </div>
                                    </div>
                                    
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnAdd" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="form-group col-md-12">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="10"
                                CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />&nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="11"
                                   CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
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
                                        <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Accomplishment Details"></asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <h4 class="box-title">Award Details
                                        </h4>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <%--  <table class="table table-bordered table-hover">--%>
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action
                                                    </th>
                                                    <th>Award Name
                                                    </th>
                                                    <th>Organization Address
                                                    </th>
                                                    <th>Date Received
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <%--<th>Attachment
                                                    </th>--%>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("AWDNO")%>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("AWDNO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("AWARDNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ORG_ADDRESS")%>
                                        </td>
                                        <td>
                                            <%# Eval("DOA", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("AMOUNT_REC")%>
                                        </td>
                                        <%-- <td>
                                            
                                             <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("GETFILE"),Eval("FUID"),Eval("IDNO"),Eval("FOLDER"),Eval("APPID"))%>'><%# Eval("DisplayFileName")%></asp:HyperLink>
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>


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
                alert('Please Enter Alphabets Only!');
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

        function numericdotOnly(eventRef, elementRef) {
            var keyCodeEntered = (eventRef) ? eventRef.keyCode : (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;

            if (keyCodeEntered == 46) {
                // Allow only 1 decimal point ('.')...
                if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                    return false;
                else
                    return true;
            }
        }
    </script>
</asp:Content>

