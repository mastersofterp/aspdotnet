<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pay_PersonalMemoranda.ascx.cs"
    Inherits="PayRoll_Pay_PersonalMemoranda" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<link href="../Css/master.css" rel="stylesheet" type="text/css" />
<link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />

<br />
<div class="row">
    <div class="col-md-12">
        <form role="form">
            <div class="box-body">
                <div class="col-md-12">
                    <asp:Panel ID="PnlPersonalMemorandam" runat="server">
                        <div class="panel panel-info">
                            <div class="panel panel-heading">Personal Memoranda</div>
                            <div class="panel panel-body">
                                <p class="text-center">
                                    <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>
                                </p>
                                <div class="form-group col-md-6">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Personal Details</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-12">
                                                    <label>Title :</label>
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"
                                                        ToolTip="Title" Enabled="false" TabIndex="1" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>First Name :</label>
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"
                                                        ToolTip="Employee First Name" Enabled="false" TabIndex="2" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Middle Name :</label>
                                                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"
                                                        ToolTip="Employee Middle Name" Enabled="false" TabIndex="3" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Last Name :</label>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                                                        ToolTip="Employee Last Name" Enabled="false" TabIndex="4" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Fathers Name :</label>
                                                    <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" TabIndex="5" Enabled="false"
                                                        ToolTip="Enter Fathers Name" onkeyup="validateAlphabet(this);" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Mothers Name :</label>
                                                    <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control" TabIndex="6" Enabled="false"
                                                        ToolTip="Enter Mother Name" onkeyup="validateAlphabet(this);" />
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <label>Date of Birth :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgCalDateOfBirth" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtBirthDate" runat="server" Enabled="true" CssClass="form-control" TabIndex="6"
                                                            ToolTip="Enter Date of Birth" ValidationGroup="PersonalMemoranda" Style="z-index: 0;" />
                                                        <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtBirthDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ControlToValidate="txtBirthDate"
                                                            Display="None" ErrorMessage="Please Select Date of Birth  in (dd/MM/yyyy Format)"
                                                            ValidationGroup="PersonalMemorandaty" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtBirthDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                                            ControlToValidate="txtBirthDate" PersonalMemorandatyValueMessage="Please Enter Birth Date"
                                                            InvalidValueMessage="BirthDate is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                            TooltipMessage="Please Enter Birth Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <label>Marks of Identification :</label>
                                                    1)&nbsp<asp:TextBox ID="txtMarksofIdentification1" runat="server" Enabled="true" TabIndex="7"
                                                        CssClass="form-control" ToolTip="Identification Mark 1" onkeyup="validateAlphabet(this);" />
                                                </div>

                                                <div class="form-group col-md-12">
                                                    2)&nbsp<asp:TextBox ID="txtMarksofIdentification2" runat="server" CssClass="form-control"
                                                        ToolTip="Identification Mark 2" onkeyup="validateAlphabet(this);" TabIndex="8" />
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <label>FN/AN :</label>
                                                    <asp:TextBox ID="txtFnAn" runat="server" CssClass="form-control" ToolTip="Enter FN/AN" TabIndex="9" />
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <label>Height :</label>
                                                    <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" TabIndex="10"
                                                        ToolTip="Enter Height" MaxLength="10" />
                                                </div>

                                                <div class="form-group col-md-12">
                                                    <label>Phone Number :</label>
                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" TabIndex="11"
                                                        ToolTip="Enter Phone Number" MaxLength="15" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Present Address :</label>
                                                    <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" TabIndex="12"
                                                        ToolTip="Enter Present Address" TextMode="MultiLine" />
                                                </div>
                                                <div class="form-group col-md-12">
                                                    <label>Permanent Address :</label>
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control"
                                                        ToolTip="Enter Permanent Address" TextMode="MultiLine" TabIndex="13" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="form-group col-md-6">
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="ServiceDetails" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Service Type Details</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <label>Present Designation :</label>
                                                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"
                                                            ToolTip="Present Designation" Enabled="false" TabIndex="14" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Present Departement :</label>
                                                        <asp:TextBox ID="txtDeparteMent" runat="server" CssClass="form-control"
                                                            ToolTip="Present Departement" Enabled="false" TabIndex="15" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Staff Type :</label>
                                                        <asp:DropDownList ID="ddlStaffType" runat="server" CssClass="form-control"
                                                            ToolTip="Select Staff Type" AppendDataBoundItems="true" TabIndex="16">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Appointment Category :</label>
                                                        <asp:DropDownList ID="ddlAppointmentCategory" runat="server" CssClass="form-control"
                                                            ToolTip="Select Appointment Category" AppendDataBoundItems="true" TabIndex="17">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Date of Appointment :</label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                            </div>
                                                            <asp:TextBox ID="txtDateofAppointment" runat="server" Enabled="true" CssClass="form-control"
                                                                ToolTip="Enter Date of Appointment" Style="z-index: 0;" TabIndex="18"
                                                                ValidationGroup="ServiceBook" />
                                                            <ajaxToolKit:CalendarExtender ID="ceDateofAppointment" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtDateofAppointment" PopupButtonID="Image2" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="rfvDateofAppointment" runat="server" ControlToValidate="txtDateofAppointment"
                                                                Display="None" ErrorMessage="Please Select Date of Appointment in (dd/MM/yyyy Format)"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                            <ajaxToolKit:MaskedEditExtender ID="meDateofAppointment" runat="server" TargetControlID="txtDateofAppointment"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevDateofAppointment" runat="server" ControlExtender="meDateofAppointment"
                                                                ControlToValidate="txtDateofAppointment" ServiceBooktyValueMessage="Please Enter Date of Appointment"
                                                                InvalidValueMessage="Date of Appointment is Invalid (Enter dd/MM/yyyy Format)"
                                                                Display="None" TooltipMessage="Please Enter Date of Appointment" PersonalMemorandatyValueBlurredText="ServiceBook"
                                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Authenticated with reference to :</label>
                                                        <asp:TextBox ID="txtAuthenticated" runat="server" CssClass="form-control"
                                                            ToolTip="Enter Authenticated with reference to" TabIndex="19" />
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Email :</label>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Email ID" />
                                                    </div>

                                                    <div class="form-group col-md-12">
                                                        <label>Status Type :</label>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                            TabIndex="21" Visible="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtpayStatus" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Status Type" />--%>
                                                    </div>

                                                    <div class="form-group col-md-12">
                                                        <label>Date :</label>
                                                        <%--<asp:TextBox ID="txtSTdate" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Date" />--%>
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtStatusDT" CssClass="form-control" runat="server" Enabled="true" TabIndex="29" />
                                                            <div class="input-group-addon">
                                                                <asp:Image ID="imgCalDateStatus" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Style="cursor: pointer;" />
                                                            </div>
                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtStatusDT" PopupButtonID="imgCalDateStatus" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStatusDT"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                                ControlToValidate="txtStatusDT" EmptyValueMessage="Please Enter Increment Date"
                                                                InvalidValueMessage="Status Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                TooltipMessage="Please Enter Increment Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                ValidationGroup="emp" SetFocusOnError="True" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Panel ID="PnlNationalityAndCast" runat="server">
                                            <div class="panel panel-info">
                                                <div class="panel panel-heading">Nationality And Cast Details</div>
                                                <div class="panel panel-body">
                                                    <div class="form-group col-md-12">
                                                        <label>Nationality :</label>
                                                        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" ToolTip="Select Nationality" TabIndex="21">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Religion :</label>
                                                        <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" ToolTip="Select Religion" TabIndex="22">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Caste :</label>
                                                        <asp:DropDownList ID="ddlCaste" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" ToolTip="Select Caste" TabIndex="23">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-md-12">
                                                        <label>Category :</label>
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"
                                                            AppendDataBoundItems="true" ToolTip="Select Category" TabIndex="24">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <br />
                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="25"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-success" ToolTip="Click here to Submit" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="26"
                                    OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </p>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </form>
    </div>
</div>

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr>
        <td>
            <%--<asp:Panel ID="PnlPersonalMemorandam" runat="server" Style="text-align: left; width: 99%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Personal Memoranda</legend>
                    <center>
                        <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label></center>
                    <br />
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="pnlAdd" runat="server" Style="text-align: left; width: 100%; padding-left: 5px">
                                    <fieldset class="fieldsetPay">
                                        <legend class="legendPay">Personal Details</legend>
                                        <br />
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="form_left_label" style="width: 38%">Title :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtTitle" runat="server" Width="50px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">First Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFirstName" runat="server" Width="200px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Middle Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtMiddleName" runat="server" Width="200px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Last Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtLastName" runat="server" Width="200px" Enabled="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Fathers Name :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFatherName" runat="server" Width="200px" onkeyup="validateAlphabet(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Date of Birth :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtBirthDate" runat="server" Enabled="true" Width="80px" ValidationGroup="PersonalMemoranda" />
                                                    <asp:Image ID="imgCalDateOfBirth" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />

                                                    <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtBirthDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ControlToValidate="txtBirthDate"
                                                        Display="None" ErrorMessage="Please Select Date of Birth  in (dd/MM/yyyy Format)"
                                                        ValidationGroup="PersonalMemorandaty" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeBirthDate" runat="server" TargetControlID="txtBirthDate"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeBirthDate"
                                                        ControlToValidate="txtBirthDate" PersonalMemorandatyValueMessage="Please Enter Birth Date"
                                                        InvalidValueMessage="BirthDate is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Birth Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Marks of Identification :
                                                </td>
                                                <td class="form_left_text">1)&nbsp<asp:TextBox ID="txtMarksofIdentification1" runat="server" Enabled="true" Width="200px" onkeyup="validateAlphabet(this);" /><br />
                                                    2)&nbsp<asp:TextBox ID="txtMarksofIdentification2" runat="server" Width="200px" onkeyup="validateAlphabet(this);" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">FN/AN :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtFnAn" runat="server" Width="10%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Height :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtHeight" runat="server" Width="100px" MaxLength="10" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Phone Number :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" Width="200px" MaxLength="15" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Present Address :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtPresentAddress" runat="server" Width="215px" TextMode="MultiLine" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label">Permanent Address :
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" Width="215px" TextMode="MultiLine" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </asp:Panel>--%>
        </td>
        <td valign="top">
            <%--<asp:Panel ID="ServiceDetails" runat="server" Style="text-align: left; width: 98%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Service Type Details</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form_left_label" style="width: 40%">Present Designation :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtDesignation" runat="server" Width="200px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Present Departement :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtDeparteMent" runat="server" Width="200px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Staff Type :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlStaffType" runat="server" Width="205px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Appointment Category :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlAppointmentCategory" runat="server" Width="205px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Date of Appointment :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtDateofAppointment" runat="server" Enabled="true" Width="80px"
                                    ValidationGroup="ServiceBook" />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                <ajaxToolKit:CalendarExtender ID="ceDateofAppointment" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtDateofAppointment" PopupButtonID="Image2" Enabled="true"
                                    EnableViewState="true">
                                </ajaxToolKit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="rfvDateofAppointment" runat="server" ControlToValidate="txtDateofAppointment"
                                    Display="None" ErrorMessage="Please Select Date of Appointment in (dd/MM/yyyy Format)"
                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <ajaxToolKit:MaskedEditExtender ID="meDateofAppointment" runat="server" TargetControlID="txtDateofAppointment"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                <ajaxToolKit:MaskedEditValidator ID="mevDateofAppointment" runat="server" ControlExtender="meDateofAppointment"
                                    ControlToValidate="txtDateofAppointment" ServiceBooktyValueMessage="Please Enter Date of Appointment"
                                    InvalidValueMessage="Date of Appointment is Invalid (Enter dd/MM/yyyy Format)"
                                    Display="None" TooltipMessage="Please Enter Date of Appointment" PersonalMemorandatyValueBlurredText="ServiceBook"
                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Authenticated with reference to :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtAuthenticated" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Email :
                            </td>
                            <td class="form_left_text">
                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
            <br />
            <%--<asp:Panel ID="PnlNationalityAndCast" runat="server" Style="text-align: left; width: 98%; padding-left: 5px;">
                <fieldset class="fieldsetPay">
                    <legend class="legendPay">Nationality And Cast Details</legend>
                    <br />
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="form_left_label" style="width: 25%">Nationality :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlNationality" runat="server" Width="200px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Religion :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlReligion" runat="server" Width="200px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Caste :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlCaste" runat="server" Width="200px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Category :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>--%>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook"
                OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" Width="80px" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
</fieldset>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>&nbsp
        </td>
    </tr>
</table>
<script type="text/javascript" language="javascript">

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
        var expAlphabet = /^[A-Za-z ]+$/;
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

