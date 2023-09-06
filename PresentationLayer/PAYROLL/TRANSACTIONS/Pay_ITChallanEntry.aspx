<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ITChallanEntry.aspx.cs" Inherits="Pay_ITChallanEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">INCOME TAX CHALLAN ENTRY</h3>
                    <p class="text-center">
                    </p>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                    <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Employee Income Tax Challan Entry</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                                Visible="false">
                                                <table class="table table-bordered table-hover table-responsive">
                                                    <tr>
                                                        <td style="width: 3%; vertical-align: top">
                                                            <img src="../../../images/error.gif" align="absmiddle" alt="Error" />
                                                        </td>
                                                        <td>
                                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                                         </font>
                                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                Visible="false">
                                                <table class="table table-bordered table-hover table-responsive">
                                                    <tr>
                                                        <td style="width: 3%; vertical-align: top">
                                                            <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>--%>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Month</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalMonYear" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtMonYear" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Month"
                                                    OnTextChanged="txtMonYear_TextChanged" AutoPostBack="True" Style="z-index: 0" />
                                               
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="MMMyyyy" TargetControlID="txtMonYear"
                                                    PopupButtonID="imgCalMonYear" Enabled="true" EnableViewState="true" />
                                                <asp:RequiredFieldValidator ID="rfvMonYear" runat="server" ControlToValidate="txtMonYear"
                                                    ErrorMessage="Please Select Month" SetFocusOnError="True" ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Challan Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalChDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtChDate" runat="server" Text="" CssClass="form-control" TabIndex="2" ToolTip="Enter Challan Date" Style="z-index: 0" />
                                               
                                                <ajaxToolKit:CalendarExtender ID="ceChDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChDate"
                                                    PopupButtonID="imgCalChDate" Enabled="true" EnableViewState="true" />
                                                <asp:RequiredFieldValidator ID="rfvChDate" runat="server" ControlToValidate="txtChDate"
                                                    ErrorMessage="Please Select Challan Date" SetFocusOnError="True"
                                                    ValidationGroup="submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowSupplOrder" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Supl.Order No.</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSuplOrderNo" runat="server" TabIndex="3" ToolTip="Select Supl.Order No" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSuplOrderNo" runat="server" ControlToValidate="ddlSuplOrderNo"
                                                ErrorMessage="Please Select Supl.Order No." SetFocusOnError="True" InitialValue="0"
                                                ValidationGroup="submit" Enabled="false"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollegeNo" CssClass="form-control" TabIndex="4" ToolTip="Select College" runat="server" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollegeNo" runat="server" ControlToValidate="ddlCollegeNo"
                                                ErrorMessage="Please Select Supl.Order No." SetFocusOnError="True" InitialValue="0"
                                                ValidationGroup="submit" Enabled="false"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <%--<label>Staff Type</label>--%>
                                                <label>Scheme/Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" CssClass="form-control" TabIndex="5" ToolTip="Select Scheme/Staff" runat="server" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Challan No</label>
                                            </div>
                                            <asp:TextBox ID="txtChallanNo" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Enter Challan Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvChallanNo" runat="server" ControlToValidate="txtChallanNo"
                                                ErrorMessage="Please Enter Challan Number" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeGovOrdNumber" runat="server"
                                                TargetControlID="txtChallanNo"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Cheque/DD No</label>
                                            </div>
                                            <asp:TextBox ID="txtChequeDDNo" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Enter Cheque/DD Number"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                TargetControlID="txtChequeDDNo"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Deposited</label>
                                            </div>
                                            <asp:TextBox ID="txtTaxDeposited" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Tax Deposited"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                TargetControlID="txtTaxDeposited"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Surcharge</label>
                                            </div>
                                            <asp:TextBox ID="txtSurcharge" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Enter Surcharge"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FtbeOffOrdNo" runat="server"
                                                TargetControlID="txtSurcharge"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Education Cess</label>
                                            </div>
                                            <asp:TextBox ID="txtEducationCess" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Enter EducationCess"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                TargetControlID="txtEducationCess"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Interest</label>
                                            </div>
                                            <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control" TabIndex="11" ToolTip="Enter Interest"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                TargetControlID="txtInterest"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Others</label>
                                            </div>
                                            <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Enter Others"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                TargetControlID="txtOthers"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>B.S.R Code</label>
                                            </div>
                                            <asp:TextBox ID="txtBSRCode" runat="server" ToolTip="Enter BSR Code" CssClass="form-control" TabIndex="13"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                TargetControlID="txtBSRCode"
                                                FilterType="Custom,Numbers,LowerCaseLetters,UpperCaseLetters"
                                                FilterMode="ValidChars"
                                                ValidChars=".-_ /">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Deduction Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalTaxDedDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtTaxDedDate" ToolTip="Enter Tax Deduction Date " runat="server" CssClass="form-control" TabIndex="14" />
 
                                                <ajaxToolKit:CalendarExtender ID="ceTaxDedDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTaxDedDate" PopupButtonID="imgCalTaxDedDate" Enabled="true"
                                                    EnableViewState="true" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Tax Deposit Date</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalDepDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtDepDate" runat="server" ToolTip="Enter Tax Deposit Date " CssClass="form-control" TabIndex="15" />
                                               
                                                <ajaxToolKit:CalendarExtender ID="ceDepDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDepDate"
                                                    PopupButtonID="imgCalDepDate" Enabled="true" EnableViewState="true" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>&nbsp;</label>
                                            </div>
                                            <asp:CheckBox ID="chkSuppBill" runat="server" Checked="false" Text="Supplementary Bill"
                                                AutoPostBack="true" OnCheckedChanged="chkSuppBill_CheckedChanged" TabIndex="3" />
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="submit" TabIndex="16"
                                    ToolTip="Click To Add Challan Entry" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                 <asp:Button ID="btnPrint" runat="server" Text="FORM 24Q" ToolTip="Click To Show Form 24Q" TabIndex="17"
                                    CssClass="btn btn-info" OnClick="btnPrint_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="18" ToolTip="Click To Reset"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" />                               
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>

                            <div class="col-12">
                                <div id="pnlList" runat="server">

                                    <asp:ListView ID="lvChallan" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Challan Entry Exists" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                                <div class="titlebar">
                                                    <h4>CHALLAN ENTRIES</h4>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Challan Dt
                                                        </th>
                                                        <th>Cheque/D.D.
                                                        </th>
                                                        <th>Challan No
                                                        </th>
                                                        <th>BSR Code
                                                        </th>
                                                        <th>TDS
                                                        </th>
                                                        <th>Surcharge
                                                        </th>
                                                        <th>Edu.Cess
                                                        </th>
                                                        <th>Interest
                                                        </th>
                                                        <th>Others
                                                        </th>
                                                        <th>Supl.
                                                        </th>
                                                        <th>Order No
                                                        </th>
                                                        <th>Tax Ded.
                                                        </th>
                                                        <th>Tax Deposit
                                                        </th>
                                                        <th>Staff Type
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
                                                    <asp:CheckBox ID="chkPrint" runat="server" ToolTip='<%# Eval("CHIDNO") %>' />
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit1.gif" CommandArgument='<%# Eval("CHIDNO")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("CHIDNO") %>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="showConfirmDel(this); return false;" />
                                                </td>
                                                <td>
                                                    <%# Eval("CHALANDT", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHQDDNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHALANNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BSRNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHAMT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHSCHARGE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CHEDUCESS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("INTEREST")%>
                                                </td>
                                                <td>
                                                    <%# Eval("OTHERS")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUPL")%>
                                                </td>
                                                <td>
                                                    <%# Eval("ORDNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SALDT", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("DEPODT", "{0:dd/MM/yyyy}")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STAFF")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                   <%-- <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvChallan" PageSize="10"
                                            OnPreRender="dpPager_PreRender">
                                            <Fields>
                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>--%>

                                </div>
                            </div>

                    </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server"></div>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </td>
                    <td>Are you sure you want to delete this Challan Entry?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>--%>

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
    </script>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
