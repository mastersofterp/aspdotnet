<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleRequisition.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_VehicleRequisition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../jquery/jquery-3.2.1.min.js"></script>
    <link href="../../jquery/jquery.multiselect.css" rel="stylesheet" />
    <script src="../../jquery/jquery.multiselect.js"></script>--%>
    <script type="text/javascript">
        function EnableDisable(ctrl) {
            if (ctrl.checked) {

                document.getElementById('<%=divGuest.ClientID%>').style.display = 'block';
                document.getElementById('<%=divStaff.ClientID%>').style.display = 'none';
            }
            else {
                document.getElementById('<%=divGuest.ClientID%>').style.display = 'none';
                document.getElementById('<%=divStaff.ClientID%>').style.display = 'block';
            }
            document.getElementById('<%=ddlStaff.ClientID%>').value = 0;
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel"
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
    <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Vehicle Requisition Form</h3>
                        </div>
                        <div class="box-body">
                            <div class="mb-4" id="divVehReq" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Vehicle Requisition Entry</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Name Of The Institution</label>
                                            </div>
                                            <asp:DropDownList ID="ddlInstitute" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="ddlInstitute" Display="None"
                                                ErrorMessage="Please Select Name Of The Institution" InitialValue="0" ValidationGroup="Submit" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Date Of Journey </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgFromDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateOfJourney" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtDateOfJourney">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2"
                                                    runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999"
                                                    MaskType="Date"
                                                    MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate"
                                                    TargetControlID="txtDateOfJourney"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtDateOfJourney"
                                                    IsValidEmpty="False"
                                                    EmptyValueMessage="Please Select Date Of Journey"
                                                    InvalidValueMessage="Date Of Journey is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None"
                                                    TooltipMessage="Input a date"
                                                    EmptyValueBlurredText="*"
                                                    InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Submit" />

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rdlOneWay" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">One-Way &nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Two-Way</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Details Of Guest/Staff</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Is Guest</label>
                                            </div>
                                            <asp:CheckBox ID="chkGuest" runat="server"  onclick="EnableDisable(this);" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGuest" runat="server" style="display: none">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Guest Name</label>
                                            </div>
                                            <asp:TextBox ID="txtGuestName" runat="server" CssClass="form-control" TabIndex="5"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divStaff" runat="server" style="display: block">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Staff Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Pick-Up Location </label>
                                            </div>
                                            <asp:TextBox ID="txtPickupLoc" runat="server" CssClass="form-control" TabIndex="5" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPickupLoc" Display="None"
                                                ErrorMessage="Please Enter Pick-Up Location" ValidationGroup="Guest" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Pick-Up Time </label>
                                            </div>
                                            <asp:TextBox ID="txtPickupTime" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtPickupTime"
                                                Mask="99:99" MaskType="Time"
                                                AcceptAMPM="true" ErrorTooltipEnabled="true" MessageValidatorTip="true" AcceptNegative="Left"
                                                DisplayMoney="Left" OnInvalidCssClass="errordate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtPickupTime" Display="None" EmptyValueBlurredText="Empty"
                                                InvalidValueMessage="Pick-Up Time is Invalid (Enter 12 Hour Format)" ValidationGroup="Guest"
                                                SetFocusOnError="true" TooltipMessage="Please Enter Pick-Up Time" EmptyValueMessage="Please Enter Pick-Up Time"
                                                IsValidEmpty="false" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Dropping Location </label>
                                            </div>
                                            <asp:TextBox ID="txtDropLoc" runat="server" CssClass="form-control" MaxLength="100" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtDropLoc" Display="None"
                                                ErrorMessage="Please Enter Dropping Location" ValidationGroup="Guest" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Contact Number </label>
                                            </div>
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" TabIndex="3" MaxLength="12" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" Display="None"
                                                ErrorMessage="Please Enter Contact Number" ValidationGroup="Guest" />

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnAddGuestStaff" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="5" ValidationGroup="Guest" OnClick="btnAddGuestStaff_Click" />
                                    <asp:ValidationSummary ID="vsdate" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Guest" />
                                    <asp:Button ID="btnCancelGuestStaff" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancelGuestStaff_Click" />
                                </div>

                                <div class="col-12 mt-3">
                                    <asp:ListView ID="lvGuestStaff" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Guest/Staff Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Guest/Staff Name</th>
                                                            <th>Pick Up Location </th>
                                                            <th>Pick Up Time</th>
                                                            <th>Dropping Location</th>
                                                            <th>Contact Number</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            <%-- <div class="listwokingday">
                                                                            <div id="demo-grid" class="vista-grid">
                                                                                <table class="table table-bordered table-hover">
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>

                                                    <asp:ImageButton ID="btnEditGuestStaff" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%#Eval("SRNO_EMP")%>' AlternateText="Edit Record" OnClick="btnEditGuestStaff_Click" />

                                                <td>
                                                    <%#Eval("ISGUEST").ToString() == "Y" ? Eval("GUEST_NAME") : Eval("EMP_NAME") %>                                                                                                                                             
                                                </td>

                                                <td>
                                                    <%#Eval("PIKUP_LOC")%>                                                        
                                                </td>

                                                <td>
                                                    <%#Eval("PIKUP_TIME","{0:hh:mm tt}")%>          
                                                </td>

                                                <td>
                                                    <%#Eval("DROP_LOCATION") %> 
                                                </td>
                                                <td>
                                                    <%#Eval("PHONE") %> 
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                                <div class="col-12 mt-3">
                                    <div class="sub-heading">
                                        <h5>Vehicle Required</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vehicle Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ddlVehicle" Display="None"
                                                ErrorMessage="Please Select Vehicle Name" ValidationGroup="Vehicle" InitialValue="0" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vehicle Type </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVehicleTypeAC" CssClass="form-control" data-select2-enable="true" runat="server" TabIndex="17"
                                                AppendDataBoundItems="true" ToolTip="Select Vehicle Type ">
                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">AC</asp:ListItem>
                                                <asp:ListItem Value="2">Non AC</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvVehicleTypeAC" runat="server" ErrorMessage="Please Select Vehicle Type "
                                                ControlToValidate="ddlVehicleTypeAC" InitialValue="0"
                                                Display="None" ValidationGroup="Vehicle"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mt-3">
                                    <asp:Button ID="btnAddVeh" runat="server" Text="Add" CssClass="btn btn-primary" TabIndex="5" ValidationGroup="Vehicle" OnClick="btnAddVeh_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Vehicle" />
                                    <asp:Button ID="btnCancelVeh" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="6" OnClick="btnCancelVeh_Click" />
                                </div>
                                <div class="col-12 mt-3 mb-3">
                                    <asp:ListView ID="lvVehicle" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Vehicle List </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Vehicle Name</th>
                                                            <th>Vehicle Type</th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                            <%--   <div class="listwokingday">
                                                <div id="demo-grid" class="vista-grid">
                                                    <table class="table table-bordered table-hover">
                                                     
                                                    </table>
                                                </div>
                                            </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>

                                                    <asp:ImageButton ID="btnVehEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                        CommandArgument='<%#Eval("SRNO_VEH")%>' AlternateText="Edit Record" OnClick="btnVehEdit_Click" />

                                                <td>

                                                    <%#Eval("VNAME")%>                                                              
                                                </td>

                                                <td>
                                                    <%#Eval("VEHICLE_AC_NONAC")%>                                                        
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="11" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" TabIndex="11" ValidationGroup="Report" OnClick="btnReport_Click" />
                                    <asp:ValidationSummary ID="vsbuss" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>


                                <div class="col-12" id="divList" runat="server">
                                    <asp:ListView ID="lvVehicleReq" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Vehicle Requisition Entry List </h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Action</th>
                                                            <th>Name Of The Institution</th>
                                                            <th>Date Of Journey</th>
                                                            <th>One-Way/Two-Way</th>
                                                            <th>Approval Status</th>
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

                                                    <asp:ImageButton ID="btnEditReq" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                        CommandArgument='<%#Eval("VEH_REQ_ID")%>' AlternateText="Edit Record" OnClick="btnEditReq_Click" />

                                                <td>
                                                    <%#Eval("COLLEGE_NAME")%>                                                              
                                                </td>
                                                <td>
                                                    <%#Eval("DATE_OF_JOURNEY","{0:dd/MM/yyyy}")%>                                                        
                                                </td>
                                                <td style="width: 15%">
                                                    <%#Eval("ONE_WAY")%>                                                        
                                                </td>
                                                <td>
                                                    <%#Eval("APPROVAL_STATUS")%>                                                        
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                
                            </div>
                            <div class="col-12" id="divReport" runat="server" visible="false">
                                    <div class="sub-heading" id="divheading" runat="server" >
                                        <h5>Vehicle Requisition Report </h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Name Of The Institution</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRInstitute" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlRInstitute" Display="None"
                                                ErrorMessage="Please Select Name Of The Institution" ValidationGroup="Report" InitialValue="0" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" >
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Date Of Journey </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgFromDate2">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate2" TargetControlID="txtDOJ">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3"
                                                    runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999"
                                                    MaskType="Date"
                                                    MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate"
                                                    TargetControlID="txtDOJ"
                                                    ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                    ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtDOJ"
                                                    IsValidEmpty="False"
                                                    EmptyValueMessage="Please Select Date Of Journey"
                                                    InvalidValueMessage="Date Of Journey is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None"
                                                    TooltipMessage="Input a date"
                                                    EmptyValueBlurredText="*"
                                                    InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Report" />

                                            </div>
                                        </div>


                                    </div>
                                    <div class="col-12 btn-footer ">
                                        <asp:Button ID="btnRpt" runat="server" Text="Show Report" CssClass="btn btn-info" TabIndex="11" ValidationGroup="Report" OnClick="btnRpt_Click" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" TabIndex="11" OnClick="btnBack_Click"  />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                    </div>
                                </div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
        
    </asp:UpdatePanel>

    <script type="text/javascript">
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
    </script>

</asp:Content>

