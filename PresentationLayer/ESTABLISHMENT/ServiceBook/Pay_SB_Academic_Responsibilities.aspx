<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_SB_Academic_Responsibilities.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_SB_Academic_Responsibilities" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
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
                                                <h5>Academic Responsibilities</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Responsibility</label>
                                            </div>
                                            <asp:TextBox ID="txtResponsibility" runat="server" CssClass="form-control"
                                                ToolTip="Enter Responsibility" TabIndex="1" MaxLength="300" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvResponsibility" runat="server" ControlToValidate="txtResponsibility"
                                                Display="None" ErrorMessage="Please Enter Responsibility" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department Level / Institute Level</label>
                                            </div>
                                            <asp:TextBox ID="txtDeptLevel" runat="server" CssClass="form-control" MaxLength="500"
                                                ToolTip="Enter Department Level / Institute Level" TabIndex="2" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvOrganization" runat="server" ControlToValidate="txtDeptLevel"
                                                Display="None" ErrorMessage="Please Enter Department Level / Institute Level" ValidationGroup="ServiceBook"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Is Current :</label>
                                            </div>
                                            <asp:CheckBox ID="chkIsCurrent" CssClass="form-control" Text=" Yes" runat="server" TabIndex="3"
                                                ToolTip="Check for Is Current" OnCheckedChanged="chkIsCurrent_CheckedChanged" AutoPostBack="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    TabIndex="4" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date"
                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ToolTip="Enter To Date" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"
                                                    TabIndex="5" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks If Any</label>
                                            </div>
                                            <asp:TextBox ID="txtReMarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any" MaxLength="200"
                                                TabIndex="6" TextMode="MultiLine"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="7" />
                                            <span style="color: red">Upload Document Maximum Size 10 Mb</span>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="8"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="9"
                                        OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvAcademicResponsibilities" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows Academic Responsibilities"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Academic Responsibilities Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Responsibility
                                                        </th>
                                                        <th>Department / Institute Level
                                                        </th>
                                                        <th>IsCurrent
                                                        </th>
                                                        <th>From Date
                                                        </th>
                                                        <th>To Date
                                                        </th>
                                                        <th id="divFolder" runat="server">Attachment
                                                        </th>
                                                        <th id="divBlob" runat="server">Attachment
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ACDNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("ACDNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("Responsibility")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPTLEVEL")%>
                                                </td>
                                                <td>
                                                    <%# Eval("Iscurrent")%>
                                                </td>
                                                <td>
                                                    <%# Eval("FROMDATE","{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TODATE", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("ACDNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                </td>
                                                <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
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

        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


