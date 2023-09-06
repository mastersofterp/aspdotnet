<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleTourEventDetails.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleTourEventDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE TOUR/ EVENT DETAILS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Vehicle Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdblistVehicleType" runat="server" RepeatDirection="Horizontal"
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
                                        <asp:DropDownList ID="ddlVehicle" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Vehicle" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvVehicle" runat="server" ErrorMessage="Please Select Vehicle"
                                            ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlVehicle" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Tour/ Event Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgPdOn">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtTourEventDate" runat="server" TabIndex="2" ValidationGroup="Submit"
                                                CssClass="form-control" ToolTip="Enter Tour/Event Date" Style="z-index: 0;"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="imgPdOn" TargetControlID="txtTourEventDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtTourEventDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                ControlToValidate="txtTourEventDate" IsValidEmpty="false"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                EmptyValueMessage="Tour/ Event Date is required"
                                                Display="None" Text="*" ValidationGroup="Submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>OUT Time </label>
                                        </div>
                                        <asp:TextBox ID="txtOUTTime" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Enter OUT Time"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meOutTime" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtOUTTime" AcceptAMPM="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevOutTime" runat="server" ControlExtender="meOutTime"
                                            ControlToValidate="txtOUTTime" Display="None" EmptyValueBlurredText="Empty"
                                            InvalidValueMessage="OUT Time is Invalid (Enter 12 Hour Format)" EmptyValueMessage="Please Enter OUT Time"
                                            SetFocusOnError="true" TooltipMessage="Please Enter OUT Time" ValidationGroup="Submit"
                                            IsValidEmpty="false"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>OUT KM</label>
                                        </div>
                                        <asp:TextBox ID="txtOUTkm" runat="server" TabIndex="4"
                                            CssClass="form-control" ToolTip="Enter OUT Km" MaxLength="20" onchange="TotalKm();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvOUTkm" runat="server" ErrorMessage="Please Enter OUT Km"
                                            ValidationGroup="Submit" ControlToValidate="txtOUTkm" Display="None"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeOUTKm" runat="server" FilterType="Custom, Numbers" TargetControlID="txtOUTkm" ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Driver</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDriver" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Driver Name" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDriver" runat="server" ErrorMessage="Please Select Driver."
                                            ValidationGroup="Submit" InitialValue="0" ControlToValidate="ddlDriver" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>IN Time</label>
                                        </div>
                                        <asp:TextBox ID="txtINTime" runat="server" TabIndex="6" CssClass="form-control"
                                            ToolTip="Enter Arrival Time"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meeINTime" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time" MessageValidatorTip="true"
                                            OnInvalidCssClass="errordate" TargetControlID="txtINTime" AcceptAMPM="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeINTime"
                                            ControlToValidate="txtINTime" Display="None" EmptyValueBlurredText="Empty"
                                            EmptyValueMessage="Please Enter IN Time" InvalidValueBlurredMessage="Invalid Time"
                                            InvalidValueMessage="IN Time is Invalid (Enter 12 Hour Format)"
                                            SetFocusOnError="true" TooltipMessage="Please Enter IN Time" ValidationGroup="Submit" IsValidEmpty="false">
                                        </ajaxToolKit:MaskedEditValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>IN KM </label>
                                        </div>
                                        <asp:TextBox ID="txtINkm" runat="server" TabIndex="7"
                                            CssClass="form-control" ToolTip="Enter IN Km" MaxLength="20" onchange="TotalKm();"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvIN" runat="server" ErrorMessage="Please Enter IN Km"
                                            ValidationGroup="Submit" ControlToValidate="txtINkm" Display="None">                                                            
                                        </asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeInKm" runat="server"
                                            FilterType="Custom, Numbers" TargetControlID="txtINkm" ValidChars=".">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Male</label>
                                        </div>
                                        <asp:TextBox ID="txtMale" runat="server" TabIndex="8" onkeypress="return CheckNumeric(event,this);"
                                            CssClass="form-control" ToolTip="Enter Male Patient Count" MaxLength="3" Text="0" onchange="PatientTotal();">
                                        </asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMale" runat="server" FilterType="Numbers" TargetControlID="txtMale" ValidChars="">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Female</label>
                                        </div>
                                        <asp:TextBox ID="txtFemale" runat="server" TabIndex="9" onkeypress="return CheckNumeric(event,this);"
                                            CssClass="form-control" ToolTip="Enter Female Patient Count" MaxLength="3" Text="0" onchange="PatientTotal();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeFemale" runat="server"
                                            FilterType="Numbers" TargetControlID="txtFemale" ValidChars="">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Children</label>
                                        </div>
                                        <asp:TextBox ID="txtChild" runat="server" TabIndex="10" onkeypress="return CheckNumeric(event,this);"
                                            CssClass="form-control" ToolTip="Enter Children Patient Count" MaxLength="3" Text="0" onchange="PatientTotal();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeChhild" runat="server"
                                            FilterType="Numbers" TargetControlID="txtChild" ValidChars="">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Infant</label>
                                        </div>
                                        <asp:TextBox ID="txtINCount" runat="server" TabIndex="11" onkeypress="return CheckNumeric(event,this);"
                                            CssClass="form-control" ToolTip="Enter In Patient Count" MaxLength="3" Text="0" onchange="PatientTotal();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbeInFant" runat="server"
                                            FilterType="Numbers" TargetControlID="txtINCount" ValidChars="">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Total Patient</label>
                                        </div>
                                        <asp:TextBox ID="txtTotalPatient" runat="server" TabIndex="12" CssClass="form-control" Enabled="false"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Total KM </label>
                                        </div>
                                        <asp:TextBox ID="txtTotKm" runat="server" TabIndex="13" CssClass="form-control" Enabled="false"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Place</label>
                                        </div>
                                        <asp:TextBox ID="txtPlace" runat="server" TextMode="MultiLine" TabIndex="14" CssClass="form-control" ToolTip="Enter Place"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPlace" runat="server" ErrorMessage="Please Enter Place"
                                            ValidationGroup="Submit" ControlToValidate="txtPlace" Display="None">                                                            
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Purpose</label>
                                        </div>
                                        <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" TabIndex="15" CssClass="form-control" ToolTip="Enter Purpose"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPurpose" runat="server" ErrorMessage="Please Enter Purpose"
                                            ValidationGroup="Submit" ControlToValidate="txtPurpose" Display="None">                                                            
                                        </asp:RequiredFieldValidator>
                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                    OnClick="btnSubmit_Click" TabIndex="16" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click"
                                    TabIndex="17" ToolTip="Click here to Reset" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" HeaderText="Following Fields are mandatory" />
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvTourEvent" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Tour/ Event Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT
                                                            </th>
                                                            <th>VEHICLE
                                                            </th>
                                                            <th>TOUR/ EVENT DATE
                                                            </th>
                                                            <th>OUT TIME
                                                            </th>
                                                            <th>IN TIME
                                                            </th>
                                                            <th>PLACE
                                                            </th>
                                                            <th>PURPOSE
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("TEID") %>'
                                                        ToolTip="Edit Record" AlternateText="Edit Record" OnClick="btnEdit_Click" />&nbsp;
                                            <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"
                                                CommandArgument='<%# Eval("TEID") %>' ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%# Eval("VEHICLENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TOUREVENTDATE", "{0:dd-MMM-yyyy}")%>
                                                </td>
                                                <td>
                                                    <%#Eval("OUTTIME","{0:hh:mm tt}")%>   
                                                </td>
                                                <td>
                                                    <%#Eval("INTIME","{0:hh:mm tt}")%>  
                                                </td>
                                                <td>
                                                    <%# Eval("PLACE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PURPOSE")%>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function TotalKm() {
            var OUTkm = parseFloat(document.getElementById('<%=txtOUTkm.ClientID %>').value);
            var INkm = parseFloat(document.getElementById('<%=txtINkm.ClientID %>').value);

            var totkm = 0;
            if (INkm > OUTkm) {
                totkm = (INkm - OUTkm);
            }
            else {
                alert('IN Km Should be greater than OUT Km.');
                document.getElementById('<%=txtINkm.ClientID %>').value = '';
                window.setTimeout(function () {
                    document.getElementById('<%=txtINkm.ClientID %>').focus();
                }, 0);
                return false;
            }
            document.getElementById('<%=txtTotKm.ClientID %>').value = totkm;
        }
    </script>

    <script type="text/javascript">
        function PatientTotal() {
            var maleCount = document.getElementById('<%=txtMale.ClientID %>').value;
            var femaleCount = document.getElementById('<%=txtFemale.ClientID %>').value;
            var childCount = document.getElementById('<%=txtChild.ClientID %>').value;
            var infantCount = document.getElementById('<%=txtINCount.ClientID %>').value;

            var totPatient = 0;
            totPatient = (parseInt(maleCount) + parseInt(femaleCount) + parseInt(childCount) + parseInt(infantCount));

            document.getElementById('<%=txtTotalPatient.ClientID %>').value = totPatient;

        }
    </script>
</asp:Content>


