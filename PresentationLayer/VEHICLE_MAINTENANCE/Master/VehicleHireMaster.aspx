<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="VehicleHireMaster.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Master_VehicleHireMaster"
    Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">VEHICLE HIRE MASTER</h3>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Name</label>
                                    </div>
                                    <asp:TextBox ID="txtVehicleName" runat="server" MaxLength="60" CssClass="form-control"
                                        TabIndex="1" ToolTip="Enter Vehicle Name or Number" onkeypress="return CheckAlphaNumeric(event, this);">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvSuppilerName" runat="server" SetFocusOnError="true"
                                        Display="None" ErrorMessage="Please Enter Vehicle Name."
                                        ValidationGroup="Submit" ControlToValidate="txtVehicleName"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Type Name </label>
                                    </div>
                                    <asp:DropDownList ID="ddlVType" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="2"
                                        AppendDataBoundItems="true" ToolTip="Select Vehicle Type Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVType" runat="server" ErrorMessage="Please Select Vehicle Type Name."
                                        ControlToValidate="ddlVType" InitialValue="0"
                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Registration No.</label>
                                    </div>
                                    <asp:TextBox ID="txtRegNo" runat="server" MaxLength="60" CssClass="form-control" TabIndex="3"
                                        onkeypress="return CheckAlphaNumeric(event, this);" ToolTip="Enter Registration Number  "></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRegNo" runat="server" SetFocusOnError="true" Display="None"
                                        ErrorMessage="Please Enter Vehicle Registration Number."
                                        ValidationGroup="Submit" ControlToValidate="txtRegNo"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Supplier/Owner</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSuppiler" CssClass="form-control" data-select2-enable="true" ToolTip="Select Supplier/Owner" runat="server" TabIndex="4"
                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSuppiler_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddl" runat="server" ErrorMessage="Please Select Suppiler."
                                        ControlToValidate="ddlSuppiler" InitialValue="0"
                                        Display="None" ValidationGroup="Submit">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Hire Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlHireType" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="5"
                                        AppendDataBoundItems="true" ToolTip="Select Hire Type">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Short Term</asp:ListItem>
                                        <asp:ListItem Value="2">Long Term</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvHType" runat="server" ErrorMessage="Please Select Hire Type."
                                        ControlToValidate="ddlHireType" InitialValue="0"
                                        Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <%--  <asp:RequiredFieldValidator ID="rfvRepo" runat="server" ErrorMessage="Please Select Hire Type." ControlToValidate="ddlHireType" 
                                                                            InitialValue="0" Display="None" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                                    onkeypress="return CheckNumeric(event, this);"--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>Hire Rate</label>
                                    </div>
                                    <asp:TextBox ID="txtHRate" runat="server" MaxLength="8" CssClass="form-control"
                                        TabIndex="6" ToolTip="Enter Hire Rate"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvHRate" runat="server" SetFocusOnError="true" Display="None"
                                        ErrorMessage="Please Enter Hire Rate."
                                        ControlToValidate="txtHRate"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FTBE1" runat="server" FilterType="Custom, Numbers"
                                        TargetControlID="txtHRate" ValidChars=".">
                                    </ajaxToolKit:FilteredTextBoxExtender>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date </label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="img3">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                            ValidationGroup="Submit" TabIndex="7"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="medt" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                            ControlToValidate="txtFrmDt" IsValidEmpty="false" EmptyValueMessage="Please Enter From Date"
                                            ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            Display="None" Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
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
                                        <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ValidationGroup="Submit"
                                            TabIndex="8" ToolTip="Enter To Date"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image12" TargetControlID="txtToDt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDt" ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                            ControlExtender="MaskedEditExtender1" ControlToValidate="txtToDt" EmptyValueMessage="Please Enter To Date"
                                            IsValidEmpty="false" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                            InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format" Display="None"
                                            Text="*" ValidationGroup="Submit"></ajaxToolKit:MaskedEditValidator>
                                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFrmDt" ControlToValidate="txtToDt"
                                            ErrorMessage="To Date Should Greater Than From Date"
                                            runat="server" Operator="GreaterThan" SetFocusOnError="True" Type="Date" ValidationGroup="Submit"
                                            Display="None"></asp:CompareValidator>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Number</label>
                                    </div>
                                    <asp:TextBox ID="txtVehNumber" runat="server" MaxLength="10" CssClass="form-control"
                                        TabIndex="12" ToolTip="Enter Vehicle Number" onkeypress="return CheckAlphaNumeric(event, this);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVehNo" runat="server" SetFocusOnError="true" Display="None"
                                        ErrorMessage="Please Enter Vehicle Number." ControlToValidate="txtVehNumber" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbeVehNo" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtVehNumber" ValidChars="">
                                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Location</label>
                                    </div>
                                    <asp:TextBox ID="txtfromlocation" runat="server" MaxLength="30" CssClass="form-control"
                                        TabIndex="10" onkeypress="return CheckAlphabet(event, this);" ToolTip="Enter From Location"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDrvrCont" ValidationGroup="Submit" ControlToValidate="txtfromlocation"
                                        Display="None" ErrorMessage="Please Enter From Location." SetFocusOnError="true" runat="server">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Location</label>
                                    </div>
                                    <asp:TextBox ID="txtTolocation" runat="server" MaxLength="30" CssClass="form-control"
                                        TabIndex="11" onkeypress="return CheckAlphabet(event, this);" ToolTip="Enter To Location"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDrvrAdd1" Display="None" runat="server"
                                        SetFocusOnError="true" ValidationGroup="Submit" ErrorMessage="Please Enter To Location."
                                        ControlToValidate="txtTolocation"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Vehicle Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlVehicleTypeAC" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="12"
                                        AppendDataBoundItems="true" ToolTip="Select Vehicle Type ">
                                        <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">AC</asp:ListItem>
                                        <asp:ListItem Value="2">Non AC</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvVehicleTypeAC" runat="server" ErrorMessage="Please Select Vehicle Type "
                                        ControlToValidate="ddlVehicleTypeAC" InitialValue="0" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Status</label>
                                    </div>
                                    <asp:RadioButtonList ID="rdblistStatus" runat="server" RepeatDirection="Horizontal" TabIndex="13"
                                        ToolTip="Select Status">
                                        <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="2">In Active</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                            </div>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="14"
                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" UseSubmitBehavior="false" OnClientClick="handleButtonClick()"/>
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="16"
                            CssClass="btn btn-info" ToolTip="You can also select Hire Type." CausesValidation="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="15"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />

                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                    </div>

                    <div class="col-12 mt-3">
                        <%-- <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">--%>
                        <asp:ListView ID="lvHireVeh" runat="server">
                            <LayoutTemplate>
                                <div id="lgv1">
                                    <div class="sub-heading">
                                        <h5>Hire Vehicle Entry List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>EDIT
                                                </th>
                                                <th>SUPPLIER NAME
                                                </th>
                                                <th>VEHICLE NAME
                                                </th>
                                                <th>REG NO.
                                                </th>
                                                <th>FROM LOCATION
                                                </th>
                                                <th>TO LOCATION
                                                </th>
                                                <th>HIRE TYPE
                                                </th>
                                                <th>FROM DATE
                                                </th>
                                                <th>TO DATE
                                                </th>
                                                <th>HIRE RATE
                                                </th>
                                                <th>STATUS
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
                                        <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                            CommandArgument='<%# Eval("VEHICLE_ID") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                            OnClick="btnEdit_Click" />
                                    </td>
                                    <td>
                                        <%# Eval("SUPPILER_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("VEHICLE_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("REGNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("FROM_LOCATION")%>
                                    </td>
                                    <td>
                                        <%# Eval("TO_LOCATION")%>
                                    </td>
                                    <td>
                                        <%# Eval("HIRE_TYPE").ToString() == "1" ? "Short Term" : "Long Term"%>
                                    </td>
                                    <td>
                                        <%# Eval("FROM_DATE","{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("TO_DATE","{0:dd-MMM-yyyy}")%>
                                    </td>
                                    <td>
                                        <%# Eval("HIRE_RATE")%>                               
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("ACTIVESTATUS").ToString() == "1" ? "Active" : "In Active"  %>'></asp:Label>
                                        <%-- <%# Eval("VTNAME")%>      --%>                         
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                        <%-- </asp:Panel>--%>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" language="javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
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


