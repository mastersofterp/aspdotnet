<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Insurance.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_Insurance"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">INSURANCE OF VEHICLE</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="pnlInsurance" runat="server">
                            <div class="col-12">
                            <%--    <div class=" sub-heading">Insurance of Vehicle</div>--%>
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
                                        <asp:DropDownList ID="ddl" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                            ToolTip="Select Vehicle">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                            ControlToValidate="ddl" ErrorMessage="Please Select Vehicle" SetFocusOnError="true"
                                            ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divInsuranceCmp" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Insurance Company </label>
                                        </div>
                                        <asp:TextBox ID="txtInsComp" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Insurance Company Name"
                                            MaxLength="50" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="rfvInsC" Display="None" runat="server" ControlToValidate="txtInsComp"
                                                        ErrorMessage="Please Enter Insurance Company Name" SetFocusOnError="true" ValidationGroup="submit">
                                                    </asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Policy No</label>
                                        </div>
                                        <asp:TextBox ID="txtPolicyNo" runat="server"
                                            onkeypress="return CheckNumeric(event,this);" CssClass="form-control" TabIndex="4" ToolTip="Enter Policy Number"
                                            MaxLength="20"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPloyNo" Display="None" runat="server" ControlToValidate="txtPolicyNo"
                                            ErrorMessage="Please Enter Policy No." SetFocusOnError="true" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Insurance From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="img3">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtInsDt" runat="server" CssClass="form-control" TabIndex="5"
                                                ToolTip="Enter Insurance From Date" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtInsDt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtInsDt" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <%-- <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                            ControlToValidate="txtInsDt" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            IsValidEmpty="false" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                            Display="None" Text="*" ValidationGroup="submit"></ajaxToolKit:MaskedEditValidator>--%>

                                            <asp:RequiredFieldValidator ID="rfvInsDt" Display="None" runat="server"
                                                ControlToValidate="txtInsDt" ErrorMessage="Please Enter Insurance From Date" SetFocusOnError="true"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Insurance To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image12">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtInsEndDt" runat="server" CssClass="form-control" TabIndex="6"
                                                ToolTip="Enter Insurance To Date" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image12" TargetControlID="txtInsEndDt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtInsEndDt"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                            ControlToValidate="txtInsEndDt"  IsValidEmpty="false" Display="None" Text="*"
                                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="submit"
                                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format">
                                                        </ajaxToolKit:MaskedEditValidator>--%>
                                            <asp:RequiredFieldValidator ID="rfvInsEndDt" Display="None" runat="server"
                                                ControlToValidate="txtInsEndDt" ErrorMessage="Please Enter Insurance To Date" SetFocusOnError="true"
                                                ValidationGroup="submit"></asp:RequiredFieldValidator>

                                            <%--<asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtInsDt" ControlToValidate="txtInsEndDt"
                                                         ErrorMessage="Insurance To Date Should Greater Than From Date" runat="server"
                                                        Operator="GreaterThan" SetFocusOnError="True" Type="Date" ValidationGroup="submit" Display="None">                                                        
                                                    </asp:CompareValidator>--%>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Premium</label>
                                        </div>
                                        <asp:TextBox ID="txtPrem" MaxLength="10" onkeypress="return CheckNumeric(event,this);" runat="server"
                                            CssClass="form-control" TabIndex="7" ToolTip="Enter Premium"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtPrem" Display="None" runat="server" ControlToValidate="txtPrem"
                                            ErrorMessage="Please Enter Premium Amount for Insurance" SetFocusOnError="true" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtPrem" runat="server" ValidChars=".0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtPrem">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAgentName" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Agent Name</label>
                                        </div>
                                        <asp:TextBox ID="txtAgntName" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Agent Name"
                                            onkeypress="return CheckAlphabet(event,this);" MaxLength="60"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Agent No </label>
                                        </div>
                                        <asp:TextBox ID="txtAgntNo" onkeypress="return CheckNumeric(event,this);" runat="server"
                                            CssClass="form-control" TabIndex="9" ToolTip="Enter Agent Number" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAgntNo" runat="server"
                                            ErrorMessage="Please Enter Agent No...!!" Display="None" ValidationGroup="submit" ControlToValidate="txtAgntNo">
                                        </asp:RequiredFieldValidator>

                                          <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" ValidChars=".0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtAgntNo">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Agent Phone </label>
                                        </div>
                                        <asp:TextBox ID="txtAgntPh" runat="server" MaxLength="15" CssClass="form-control" TabIndex="10"
                                            ToolTip="Enter Agent Phone Number">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="+- " TargetControlID="txtAgntPh">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvAgnPh" runat="server"
                                            ErrorMessage="Please Enter Agent Phone No...!!" Display="None"
                                            ValidationGroup="submit" ControlToValidate="txtAgntPh"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Agents Secondary Phone</label>
                                        </div>
                                        <asp:TextBox ID="txtSecPh" runat="server" MaxLength="15" CssClass="form-control" TabIndex="11"
                                            ToolTip="Enter Agents Secondary Phone Number"> </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                            ValidChars="+- " TargetControlID="txtSecPh">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>No Claim Bonus (NCB)</label>
                                        </div>
                                        <asp:TextBox ID="txtNCB" MaxLength="6" onkeypress="return CheckNumeric(event,this);" runat="server"
                                            CssClass="form-control" TabIndex="12" ToolTip="Enter No Claim Bonus (NCB)"></asp:TextBox>

                                          <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars=".0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtNCB">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Claim If any </label>
                                        </div>
                                        <asp:TextBox ID="txtClaim" MaxLength="6" onkeypress="return CheckNumeric(event,this);" runat="server"
                                            CssClass="form-control" TabIndex="13" ToolTip="Enter Claim If any"></asp:TextBox>

                                           <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" ValidChars=".0123456789"
                                                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtClaim">
                                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Claim Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1785">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtClmDt" runat="server" CssClass="form-control" TabIndex="14"
                                                ToolTip="Enter Claim Date" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1785" TargetControlID="txtClmDt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtClmDt"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                ControlToValidate="txtClmDt" EmptyValueMessage="Please Enter Claim Date" IsValidEmpty="true"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                            <asp:HiddenField ID="hdnDate" runat="server" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                OnClick="btnSubmit_Click" ToolTip="Click here to Submit" TabIndex="15"  UseSubmitBehavior="false" OnClientClick="handleButtonClick()"/>
                            <asp:Button ID="btnInsuExpiry" runat="server" Text="Insurance Expiry Report" CssClass="btn btn-info" TabIndex="17"
                                OnClick="btnInsuExpiry_Click" OnClientClick="return UserDeleteConfirmation();" ToolTip="Click here to Show Report" />
                            <asp:HiddenField ID="hdnexpiryinput" runat="server" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="16"
                                OnClick="btnCancel_Click" ToolTip="Click here to reset" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvIsuranceExpire" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>List Of Vehicle Insurance Expire In Next 15 Days</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>VEHICLE NAME
                                                        </th>

                                                        <th>POLICY NO.
                                                        </th>
                                                        <th>INSURANCE EXPIRY DATE
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
                                                <%# Eval("POLICYNO")%>
                                            </td>
                                            <td>
                                                <%#Eval("INSENDDT", "{0:dd-MMM-yyyy}")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lvInsurance" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Vehicle Insurance Entry List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>EDIT
                                                        </th>
                                                        <th>VEHICLE NAME
                                                        </th>
                                                        <%--<th>INSURANCE COMPANY
                                                    </th>--%>
                                                        <th>INSURANCE DATE
                                                        </th>
                                                        <th>INSURANCE END DATE
                                                        </th>
                                                        <%-- <th>AGENT NAME
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("INSSIDNO") %>'
                                                    ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png"
                                                CommandArgument='<%# Eval("INSSIDNO") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("VEHICLENAME")%>
                                            </td>
                                            <%--<td>
                                            <%# Eval("INSCOMPANY")%>
                                        </td>--%>
                                            <td>
                                                <%# Eval("INSDT","{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("INSENDDT","{0:dd-MMM-yyyy}")%>
                                            </td>
                                            <%--<td>
                                            <%# Eval("AGENTNAME")%>
                                        </td>--%>
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
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
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
      <script>
          function handleButtonClick() {
              var button = document.getElementById('<%= btnSubmit.ClientID %>');

            // Disable the button and update text
            button.disabled = true;
            button.value = "Please Wait...";

            // Enable the button after 10 seconds
            setTimeout(function () {
                button.disabled = false;
                button.value = "Submit";
            }, 10000); // 10000 milliseconds = 10 seconds
        }
</script>
</asp:Content>
