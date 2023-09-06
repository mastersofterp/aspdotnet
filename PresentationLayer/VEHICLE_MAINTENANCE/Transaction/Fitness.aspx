<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Fitness.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_Fitness" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FITNESS REGISTER</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlFit" runat="server">
                        <div class="col-12">
                            <div class=" sub-heading">
                                <h5>Vehicle Fitness</h5>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal" TabIndex="1"
                                        OnSelectedIndexChanged="rdblistVehicleType_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Vehicle Type">
                                        <asp:ListItem Selected="True" Value="1">College Vehicles</asp:ListItem>
                                        <asp:ListItem Value="2">Contract Vehicles</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle</label>
                                    </div>
                                    <asp:DropDownList ID="ddl" CssClass="form-control" data-select2-enable="true" runat="server" ToolTip="Select Vehicle"
                                        AppendDataBoundItems="true" TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server"
                                        ErrorMessage="Please Select Vehicle" ControlToValidate="ddl" InitialValue="0"
                                        Display="None" ValidationGroup="ScheduleDtl">
                                    </asp:RequiredFieldValidator>
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
                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" TabIndex="3"
                                            ValidationGroup="ScheduleDtl" ToolTip="Enter From Date"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt" ControlToValidate="txtFrmDt"
                                            EmptyValueMessage="Please Enter From Date" IsValidEmpty="false" Display="None"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="ScheduleDtl">
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image12">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Enter To Date" TabIndex="4"
                                            ValidationGroup="ScheduleDtl"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image12" TargetControlID="txtToDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDt"
                                            ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Text="*" ValidationGroup="ScheduleDtl">
                                        </ajaxToolKit:MaskedEditValidator>
                                        <%--<asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFrmDt" ControlToValidate="txtToDt"
                                                        ErrorMessage="To Date Should Greater Than From Date" runat="server" Operator="GreaterThan"
                                                        SetFocusOnError="True" Type="Date" ValidationGroup="ScheduleDtl" Display="None">
                                                    </asp:CompareValidator>--%>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Fitness Test Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1123">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFtDt" runat="server" CssClass="form-control" ToolTip="Enter Fitness Test Date"
                                            TabIndex="5"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1123" TargetControlID="txtFtDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFtDt"
                                            ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                            ControlToValidate="txtFtDt" EmptyValueMessage="Please Enter Fitness Test Date" IsValidEmpty="false"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" ValidationGroup="ScheduleDtl">
                                        </ajaxToolKit:MaskedEditValidator>
                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Fitness No.</label>
                                    </div>
                                    <asp:TextBox ID="txtFitNo" runat="server" CssClass="form-control" ToolTip="Enter Fitness Number" MaxLength="25"
                                        onkeypress="return CheckAlphaNumeric(event, this);" TabIndex="6"></asp:TextBox>
                                </div>
                                <div class="col-12">
                                    <p class="text-center">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12">
                        <asp:Panel ID="pnlFitExpire" runat="server">
                            <asp:ListView ID="lvFitnessExpire" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>List Of Vehicle Fitness Expire In Next 15 Days</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>FITNESS NO.
                                                    </th>
                                                    <th>FITNESS EXPIRY DATE
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
                                            <%# Eval("FITNO")%>  
                                        </td>
                                        <td>
                                            <%# Eval("TDAT", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="ScheduleDtl"
                            OnClick="btnSubmit_Click" ToolTip="Click here to Submit" TabIndex="7" UseSubmitBehavior="false" OnClientClick="handleButtonClick()"/>
                        <asp:Button ID="Button1" runat="server" Text="Vehicle Fitness Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                            OnClick="Button1_Click" OnClientClick="return UserDeleteConfirmation();" TabIndex="9" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset"
                            OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:HiddenField ID="hdnexpiryinput" runat="server" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="List" ValidationGroup="ScheduleDtl" />
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="pnlList" runat="server">
                            <asp:ListView ID="lvFitness" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Vehicle Fitness Entry List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>EDIT
                                                    </th>
                                                    <th>VEHICLE NAME
                                                    </th>
                                                    <th>FITNESS NO.
                                                    </th>
                                                    <th>FROM DATE
                                                    </th>
                                                    <th>TO DATE
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FID") %>'
                                                ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("FID") %>'
                                                ToolTip="Delete Record" OnClick="btnDelete_Click" OnClientClick="showConfirmDel(this); return false;" />
                                        </td>
                                        <td>
                                            <%# Eval("VEHICLENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("FITNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("FDATE", "{0:dd-MMM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("TDAT", "{0:dd-MMM-yyyy}")%>
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
    </script>

    <script type="text/javascript" language="javascript">

        function UserDeleteConfirmation() {
            var num = prompt("Insert Number of Days", "Type no. of days here");

            n = parseInt(num, 10);

            if (isNaN(n)) {
                //alert("The input cannot be parsed to an integer");
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

