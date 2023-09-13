<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_DepartmentalExamNew.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_DepartmentalExamNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <%--<br />--%>
            <div class="row">
                <div class="col-md-12">
                    <form role="form">
                        <div class="box-body">
                            <div class="col-md-12">
                                <%-- <div class="col-md-6">--%>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Department/Government Examination/Additional Certification Courses</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlFm" runat="server">
                                    <div class="panel panel-info">

                                        <%-- <div class="panel panel-heading">Department Examination</div>--%>
                                        <div class="panel panel-body">
                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <sup>*</sup>

                                                        <label>Exam :</label>

                                                        <asp:DropDownList ID="ddlexam" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="1" data-select2-enable="true"
                                                            CssClass="form-control" ToolTip="Select Exam">
                                                            <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="NET"></asp:ListItem>
                                                            <asp:ListItem Value="GATE"></asp:ListItem>
                                                            <asp:ListItem Value="SET"></asp:ListItem>
                                                            <asp:ListItem Value="OTHER"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlexam" InitialValue="0"
                                                            Display="None" ErrorMessage="Please Select Exam" ValidationGroup="ServiceBook"
                                                            SetFocusOnError="True">
                                               
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <sup>*</sup>
                                                        <label>Name Of Examination :</label>
                                                        <asp:TextBox ID="txtNameOfExam" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter Name Of Examination" MaxLength="400"
                                                            onkeyup="validateAlphabet(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNameOfExam" runat="server" ControlToValidate="txtNameOfExam"
                                                            Display="None" ErrorMessage="Please Enter Name Of Examination " ValidationGroup="ServiceBook"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <label>Registration Number  :</label>
                                                        <asp:TextBox ID="txtRegNo" runat="server" CssClass="form-control" TabIndex="2"
                                                            ToolTip="Enter Registration Number" MaxLength="25"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <sup>*</sup>
                                                        <label>Year of passing :</label>
                                                        <asp:TextBox ID="txtYearOfPassing" runat="server" CssClass="form-control" TabIndex="3" MaxLength="4" onkeyup="validateNumeric(this);"
                                                            ToolTip="Enter Year of Passing"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvYearOfPassing" runat="server" ControlToValidate="txtYearOfPassing"
                                                            Display="None" ErrorMessage="Please Enter Year of passing" ValidationGroup="ServiceBook" ForeColor="Red" SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator ID="revYearOfPassing" runat="server" ControlToValidate="txtYearOfPassing" ValidationExpression="^[0-9]{4}$"
                                                            ErrorMessage="Please Enter Valid Year" ValidationGroup="ServiceBook" SetFocusOnError="true" ForeColor="Red"></asp:RegularExpressionValidator>


                                                    </div>
                                                    <div class="form-group col-md-12" id="trOff" runat="server" visible="false">
                                                        <label>Attesting Officer :</label>
                                                        <asp:TextBox ID="txtAttestOfficer" runat="server" CssClass="form-control" TabIndex="4"
                                                            ToolTip="Enter Attesting Officer :"></asp:TextBox>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label>Upload Document :</label>
                                                                <asp:FileUpload ID="flupld" runat="server" TabIndex="5" ToolTip="Click here to Upload Document" />
                                                                <asp:Label ID="Label2" runat="server" Text=" Please Select valid Document file(e.g. .jpg,.png,.doc,.txt) upto 10MB" ForeColor="Red"></asp:Label>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                                                <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                                                <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                                                <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                                            </div>
                                                            <div class="form-group col-md-12">
                                                                <p class="text-center">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="6"
                                                                        OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />&nbsp;
                                                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="7"
                                                               OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                                </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvDeptExam" runat="server">
                                        <EmptyDataTemplate>
                                            <br />
                                            <p class="text-center text-bold">
                                                <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Dept Examination"></asp:Label>
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Dept Examination Details
                                                </h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <%-- <table class="table table-bordered table-hover">--%>
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Name Of Exam
                                                        </th>
                                                        <th>Reg.No
                                                        </th>
                                                        <th>YOP
                                                        </th>
                                                        <th>EXAM
                                                        </th>
                                                        <%--<th width="10%">Attest.Officer
                                                    </th>--%>
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
                                            <%--</div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DENO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("DENO") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("EXAM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PASSYEAR")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EXAMname")%>
                                                </td>
                                                <%-- <td>
                                            <%# Eval("OFFICER")%>
                                        </td>--%>
                                                <td id="tdFolder" runat="server">
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("DENO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
                </form>
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

                function validateNumeric(txt) {
                    if (isNaN(txt.value)) {
                        txt.value = '';
                        alert('Only Numeric Characters Allowed!');
                        txt.focus();
                        return;
                    }
                }

                function validateAlphabet(txt) {
                    var expAlphabet = /^[A-Za-z .]+$/;
                    if (txt.value.search(expAlphabet) == -1) {
                        txt.value = txt.value.substring(0, (txt.value.length) - 1);
                        txt.value = '';
                        txt.focus = true;
                        alert("Only Alphabets allowed!");
                        return false;
                    }
                    else
                        return true;
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

