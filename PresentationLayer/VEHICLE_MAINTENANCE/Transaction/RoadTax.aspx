<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RoadTax.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_RoadTax"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ROAD TAX</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-12">
                            <div class="sub-heading">
                                <h5>Vehicle Road Tax</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal" TabIndex="1"
                                        OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Value="1">College Vehicles</asp:ListItem>
                                        <asp:ListItem Value="2">Contract Vehicles</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle</label>
                                    </div>
                                    <asp:DropDownList ID="ddl" CssClass="form-control" data-select2-enable="true" ToolTip="Select Vehicle" runat="server"
                                        AppendDataBoundItems="true" TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server"
                                        ErrorMessage="Please Select Vehicle" ControlToValidate="ddl" Display="None" ValidationGroup="submit"
                                        InitialValue="0"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Valid For (yrs)</label>
                                    </div>
                                    <asp:TextBox ID="txtValid" MaxLength="3" ToolTip="Enter Valid For (yrs)" CssClass="form-control"
                                        onkeypress="return CheckNumeric(event,this);" runat="server" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" SetFocusOnError="true"
                                        runat="server" ControlToValidate="txtValid"
                                        ErrorMessage="Please Enter valid for years" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtValid">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="img3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                            TabIndex="4" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="ceFromDt" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <%--  <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtFrmDt"
                                                        IsValidEmpty="false" Display="None" Text="*" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="submit">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>--%>
                                        <asp:RequiredFieldValidator ID="rfvFrDate" Display="None" SetFocusOnError="true"
                                            runat="server" ControlToValidate="txtFrmDt"
                                            ErrorMessage="Please Select From Date" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image12">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                            TabIndex="5" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image12" TargetControlID="txtToDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDt"
                                            ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                        ControlToValidate="txtToDt" Display="None" Text="*" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        IsValidEmpty="false" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                        ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>--%>
                                        <%--<asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFrmDt" ControlToValidate="txtToDt"
                                                        ErrorMessage="To Date Should Greater Than From Date" runat="server" Operator="GreaterThan"
                                                        SetFocusOnError="True" Type="Date" ValidationGroup="submit" Display="None">
                                                    </asp:CompareValidator>--%>
                                        <asp:RequiredFieldValidator ID="rfvToDate" Display="None" SetFocusOnError="true"
                                            runat="server" ControlToValidate="txtToDt"
                                            ErrorMessage="Please Select To Date" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Amount</label>
                                    </div>
                                    <asp:TextBox ID="txtAmt" onkeypress="return CheckNumeric(event,this);" runat="server"
                                        CssClass="form-control" TabIndex="6" ToolTip="Enter Amount" MaxLength="10"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtAmt" runat="server"
                                        ErrorMessage="Please Enter Amount" Display="None" ControlToValidate="txtAmt" ValidationGroup="submit">
                                    </asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtAmt">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Receipt No </label>
                                    </div>
                                    <asp:TextBox ID="txtReceiptNo" onkeypress="return CheckAlphaNumeric(event,this);" runat="server"
                                        CssClass="form-control" TabIndex="7" ToolTip="Enter Receipt Number" MaxLength="15"></asp:TextBox>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Paid Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1121">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtPdDt" runat="server" CssClass="form-control" ToolTip="Enter Paid Date"
                                            TabIndex="8" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1121" TargetControlID="txtPdDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                            TargetControlID="txtPdDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                            ControlToValidate="txtPdDt" EmptyValueMessage="Please Enter Paid Date" IsValidEmpty="false"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="submit">
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                            OnClick="btnSubmit_Click" CausesValidation="true" ToolTip="Click here to Submit" TabIndex="9" />
                      <asp:Button ID="btnRoadTax" runat="server" Text="RoadTax Expiry Report" TabIndex="11"
                            OnClick="btnRoadTax_Click" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                            OnClientClick="return UserDeleteConfirmation();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                            CausesValidation="false" ToolTip="Click here to Reset" TabIndex="10" />
                        <asp:HiddenField ID="hdnexpiryinput" runat="server" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                    </div>

                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12">
                            <asp:ListView ID="lvRoadTaxExpire" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List Of Vehicle Road Tax Expire In Next 15 Days</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>RECEIPT NO.
                                                    </th>
                                                    <th>AMOUNT
                                                    </th>
                                                    <th>ROADTAX EXPIRY DATE
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
                                            <%# Eval("MODEL")%>
                                        </td>
                                        <td>
                                            <%# Eval("RECNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("AMT")%>
                                        </td>
                                        <td>
                                            <%# Eval("TDAT", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="col-12">
                            <asp:ListView ID="lvRoadTax" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle Road Tax Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>RECEIPT NO.
                                                    </th>
                                                    <th>FROM DATE
                                                    </th>
                                                    <th>TO DATE
                                                    </th>
                                                    <th>AMOUNT
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("RTAXID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("RTAXID") %>'
                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("RECNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("FDATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("TDAT", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("AMT")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>

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
                            <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                            <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                            <div class="text-center">
                                <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                                <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-primary" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
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
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
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
        function UserDeleteConfirmation() {
            var num = prompt("Insert Number of Days", "Type no. of days here");

            n = parseInt(num, 10);

            if (isNaN(n)) {
                alert("Enter valid number of days or click on cancel");
                return false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_hdnexpiryinput').value = n;
                return true;
            }

        }
    </script>
</asp:Content>
