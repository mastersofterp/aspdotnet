<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_LoansAndAdvance.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_LoansAndAdvance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Loans & Advance</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updImage" runat="server">
                                <ContentTemplate>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Loan Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlLoanName" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    ToolTip="Select Loan Name" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLoanName" runat="server" ControlToValidate="ddlLoanName"
                                                    Display="None" SetFocusOnError="True" ErrorMessage="Please Select Loan Name "
                                                    ValidationGroup="ServiceBook" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Order No.</label>
                                                </div>
                                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" ToolTip="Enter Order No" MaxLength="35" TabIndex="2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtOrderNo" runat="server" ControlToValidate="txtOrderNo"
                                                    Display="None" ErrorMessage="Please Enter Order No." ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" ToolTip="Enter Amount" TabIndex="3"
                                                    onkeyup="return validateNumeric(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rvfAmount" runat="server" ControlToValidate="txtAmount"
                                                    Display="None" ErrorMessage="Please Enter Amount " ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Rate Of Interest</label>
                                                </div>
                                                <asp:TextBox ID="txtRateOfInterest" runat="server" CssClass="form-control" ToolTip="Enter Rate Of Interest" MaxLength="2"
                                                    onkeyup="return validateNumeric(this);" TabIndex="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRateOfInterest" runat="server" ControlToValidate="txtRateOfInterest"
                                                    Display="None" ErrorMessage="Please Enter Rate Of Interest " ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>No.Of Installment</label>
                                                </div>
                                                <asp:TextBox ID="txtNoOfInstallMent" runat="server" CssClass="form-control" ToolTip="Enter No.Of InstallMent"
                                                    onkeyup="return validateNumeric(this);" TabIndex="5" MaxLength="4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtNoOfInstallMent" runat="server" ControlToValidate="txtNoOfInstallMent"
                                                    Display="None" ErrorMessage="Please Enter No.Of Installment " ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Loan Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtLoanDate" runat="server" CssClass="form-control" ToolTip="Enter Loan Date"
                                                        TabIndex="6" Style="z-index: 0;"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceLoanDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtLoanDate" PopupButtonID="dvcal2" Enabled="true" EnableViewState="true"
                                                        PopupPosition="BottomLeft">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvLoanDate" runat="server" ControlToValidate="txtLoanDate"
                                                        Display="None" ErrorMessage="Please Enter Loan Date" ValidationGroup="ServiceBook"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>
                                                    <ajaxToolKit:MaskedEditExtender ID="meLoanDate" runat="server" TargetControlID="txtLoanDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevLoanDate" runat="server" ControlExtender="meLoanDate"
                                                        ControlToValidate="txtLoanDate" InvalidValueMessage="Loan Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Loan Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Remarks</label>
                                                </div>
                                                <asp:TextBox ID="txtReMarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks If Any"
                                                    TextMode="MultiLine" TabIndex="7" MaxLength="110"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvReMarks" runat="server" ControlToValidate="txtReMarks"
                                                    Display="None" ErrorMessage="Please Enter Remarks " ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Undertaking Document</label>
                                                </div>
                                                <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="8" />
                                                <span style="color: red">Upload Document Maximum Size 10 Mb</span>
                                            </div>    
                                            
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Affidavit Document</label>
                                                </div>
                                                <asp:FileUpload ID="flupAFF" runat="server" ToolTip="Click here to Upload Document" TabIndex="9" />
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="10"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="11"
                                                OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvLoanAndAdvance" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Loan And Advance of Employee"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Loan & Advance Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>Lon.Name
                                                    </th>
                                                    <th>Ord. No.
                                                    </th>
                                                    <th>Amt.
                                                    </th>
                                                    <th>ROI
                                                    </th>
                                                    <th>No.of inst.
                                                    </th>
                                                    <th>Loan Date
                                                    </th>
                                                    <th id="divFolder" runat="server">Undertaking Document
                                                    </th>
                                                    <th id="divBlob" runat="server">Undertaking Document
                                                    </th>
                                                    <th id="divFolder1" runat="server">Affidavit Document
                                                    </th>
                                                    <th id="divBlob1" runat="server">Affidavit Document
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("lno")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("lno") %>'
                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("LOANNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("orderno")%>
                                            </td>
                                            <td>
                                                <%# Eval("amount")%>
                                            </td>
                                            <td>
                                                <%# Eval("interest")%>
                                            </td>
                                            <td>
                                                <%# Eval("instal")%>
                                            </td>
                                            <td>
                                                <%# Eval("loandt", "{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td id="tdFolder" runat="server">
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
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
                                            <td id="tdFolder1" runat="server">
                                                 <asp:HyperLink ID="lnkDownload1" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("AFFIDAVITATTACH"),Eval("LNO"),Eval("IDNO"))%>'><%# Eval("AFFIDAVITATTACH")%></asp:HyperLink>
                                            </td>
                                            <td style="text-align: center" id="tdBlob1" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview1" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("AFFIDAVITATTACH") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("AFFIDAVITATTACH") %>' Visible='<%# Convert.ToString(Eval("AFFIDAVITATTACH"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview1" EventName="Click" />
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

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
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



</asp:Content>

