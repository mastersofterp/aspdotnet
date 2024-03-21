<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_PersonalMemoranda.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_PersonalMemoranda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    <%--    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Personal Memorandum</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="PnlPersonalMemorandam" runat="server">
                        <p class="text-center">
                            <asp:Label ID="lblmsg" runat="server" SkinID="Msglbl"></asp:Label>
                        </p>
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Personal Details</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Title</label>
                                        </div>
                                        <%--<asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"
                                            ToolTip="Title" Enabled="true" TabIndex="1" />--%>
                                        <asp:DropDownList ID="ddlTitle" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ToolTip="Select Title" TabIndex="1" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>First Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"
                                            ToolTip="Employee First Name" Enabled="true" TabIndex="2" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Middle Name</label>
                                        </div>
                                        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control"
                                            ToolTip="Employee Middle Name" Enabled="true" TabIndex="3" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Last Name</label>
                                        </div>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                                            ToolTip="Employee Last Name" Enabled="true" TabIndex="4" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Fathers Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" TabIndex="5" Enabled="true"
                                            ToolTip="Enter Fathers Name" onkeyup="validateAlphabet(this);" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Mothers Name</label>
                                        </div>
                                        <asp:TextBox ID="txtMotherName" runat="server" CssClass="form-control" TabIndex="6" Enabled="true"
                                            ToolTip="Enter Mother Name" onkeyup="validateAlphabet(this);" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Date of Birth</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalDateOfBirth" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtBirthDate" runat="server" Enabled="true" CssClass="form-control" TabIndex="7"
                                                ToolTip="Enter Date of Birth" ValidationGroup="PersonalMemoranda" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="ceBirthDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtBirthDate" PopupButtonID="imgCalDateOfBirth" Enabled="true"
                                                EnableViewState="true" OnClientDateSelectionChanged="CheckDateEalier">
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

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Marks of Identification</label>
                                        </div>
                                        <span class="font-weight-bold">1) </span>
                                        <asp:TextBox ID="txtMarksofIdentification1" runat="server" Enabled="true" TabIndex="8"
                                            CssClass="form-control d-inline-block" ToolTip="Identification Mark 1" MaxLength="50" Style="width: calc(100% - 15px);" /><%--onkeyup="validateAlphabet(this);"--%>

                                        <br />
                                        <span class="font-weight-bold">2) </span>
                                        <asp:TextBox ID="txtMarksofIdentification2" runat="server" CssClass="form-control d-inline-block" Style="width: calc(100% - 15px);"
                                            ToolTip="Identification Mark 2" TabIndex="9" MaxLength="50" /><%--onkeyup="validateAlphabet(this);"--%>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Height (in cm)</label>
                                        </div>
                                        <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" TabIndex="10"
                                            ToolTip="Enter Height" MaxLength="8" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Phone Number</label>
                                        </div>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" TabIndex="11"
                                            ToolTip="Enter Phone Number" MaxLength="10" onkeyup="validateNumeric(this);" />
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divCountry" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Country :</label>
                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" TabIndex="12" MaxLength="80"
                                                ToolTip="Enter Country Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Country :</label>
                                            <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                ToolTip="Select Country" TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divState" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>State :</label>
                                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" TabIndex="13" MaxLength="80"
                                                ToolTip="Enter State Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>State :</label>
                                            <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                ToolTip="Select State" TabIndex="1" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>City/Town :</label>
                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" TabIndex="14" MaxLength="100"
                                                ToolTip="Enter City Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div id="taluka" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Taluka :</label>
                                            <asp:TextBox ID="txtTaluka" runat="server" CssClass="form-control" TabIndex="15" MaxLength="80"
                                                ToolTip="Enter Taluka Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>District :</label>
                                            <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control" TabIndex="16" MaxLength="80"
                                                ToolTip="Enter District Name" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Present Address</label>
                                        </div>
                                        <asp:TextBox ID="txtPresentAddress" runat="server" CssClass="form-control" TabIndex="17"
                                            ToolTip="Enter Present Address" TextMode="MultiLine" MaxLength="1024" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Permanent Address</label>
                                        </div>
                                        <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control"
                                            ToolTip="Enter Permanent Address" TextMode="MultiLine" TabIndex="18" MaxLength="1024" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pincode :</label>
                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" TabIndex="19" MaxLength="6"
                                                ToolTip="Enter Pincode" onkeyup="validateNumeric(this);"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Blood Group </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBloodGroup" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ToolTip="Select Blood group" TabIndex="20" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Pan No. </label>
                                        </div>
                                        <asp:TextBox ID="txtPan" runat="server" CssClass="form-control"
                                            ToolTip="Enter Pan Number" TabIndex="21" MaxLength="10"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Aadhar No. </label>
                                        </div>
                                        <asp:TextBox ID="txtAdhar" runat="server" CssClass="form-control" ToolTip="Enter Adhar Number"
                                            TabIndex="22" MaxLength="12" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Whats App No. </label>
                                        </div>
                                        <asp:TextBox ID="txtWhats" runat="server" CssClass="form-control" ToolTip="Enter Whats App Number"
                                            TabIndex="23" MaxLength="10" onkeyup="validateNumeric(this);"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Passport No. </label>
                                        </div>
                                        <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" ToolTip="Enter Passport App Number"
                                            TabIndex="24" MaxLength="12"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>AICTE No. </label>
                                        </div>
                                        <asp:TextBox ID="txtaicte" runat="server" CssClass="form-control" ToolTip="Enter Aicte Number"
                                            TabIndex="25" MaxLength="12"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="ServiceDetails" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Service Type Details</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Present Designation</label>
                                        </div>
                                        <%--<asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"
                                            ToolTip="Present Designation" Enabled="true" TabIndex="14" />--%>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ToolTip="Select Designation" TabIndex="26" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Present Department</label>
                                        </div>
                                        <%--<asp:TextBox ID="txtDeparteMent" runat="server" CssClass="form-control"
                                            ToolTip="Present Departement" Enabled="true" TabIndex="26" />--%>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlDept" runat="server" CssClass="form-control"
                                            ToolTip="Select Department" AppendDataBoundItems="true" TabIndex="27">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Staff Type</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlStaffType" runat="server" CssClass="form-control"
                                            ToolTip="Select Staff Type" AppendDataBoundItems="true" TabIndex="28">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Appointment Category</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlAppointmentCategory" runat="server" CssClass="form-control"
                                            ToolTip="Select Appointment Category" AppendDataBoundItems="true" TabIndex="29">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Date of Joining</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtDateofAppointment" runat="server" Enabled="true" CssClass="form-control"
                                                ToolTip="Enter Date of Appointment" Style="z-index: 0;" TabIndex="30"
                                                ValidationGroup="ServiceBook" />
                                            <ajaxToolKit:CalendarExtender ID="ceDateofAppointment" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtDateofAppointment" PopupButtonID="Image2" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <%-- <asp:RequiredFieldValidator ID="rfvDateofAppointment" runat="server" ControlToValidate="txtDateofAppointment"
                                                                    Display="None" ErrorMessage="Please Select Date of Joining in (dd/MM/yyyy Format)"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                                </asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:MaskedEditExtender ID="meDateofAppointment" runat="server" TargetControlID="txtDateofAppointment"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDateofAppointment" runat="server" ControlExtender="meDateofAppointment"
                                                ControlToValidate="txtDateofAppointment" ServiceBooktyValueMessage="Please Enter Date of Joining"
                                                InvalidValueMessage="Date of Joining is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Date of Joining" PersonalMemorandatyValueBlurredText="ServiceBook"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>FN/AN</label>
                                        </div>
                                        <asp:TextBox ID="txtFnAn" runat="server" CssClass="form-control" ToolTip="Enter FN/AN" TabIndex="31" Enabled="true" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Personal Mail ID</label>
                                        </div>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="32" ToolTip="Enter Email ID" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid email address" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                            ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Official/Alternate Email ID</label>
                                        </div>
                                        <asp:TextBox ID="txtPersonalEmail" runat="server" CssClass="form-control" TabIndex="33" ToolTip="Enter Email ID" />
                                        <asp:RegularExpressionValidator ID="revPersEm" runat="server" ControlToValidate="txtPersonalEmail"
                                            ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                            Display="Dynamic" ErrorMessage="Invalid email address" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status Type</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlStatus" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            TabIndex="34" Visible="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="txtpayStatus" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Status Type" />--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status Date</label>
                                        </div>
                                        <%--<asp:TextBox ID="txtSTdate" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Date" />--%>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalDateStatus" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtStatusDT" CssClass="form-control" runat="server" Enabled="true" TabIndex="35" ToolTip="Select Status Date" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStatusDT" PopupButtonID="imgCalDateStatus" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStatusDT"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                ControlToValidate="txtStatusDT" EmptyValueMessage="Please Enter Increment Date"
                                                InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Authenticated with reference to</label>
                                        </div>
                                        <asp:TextBox ID="txtAuthenticated" runat="server" CssClass="form-control"
                                            ToolTip="Enter Authenticated with reference to" TabIndex="36" MaxLength="200" />
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PnlNationalityAndCast" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Nationality And Cast Details</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Nationality</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlNationality" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" ToolTip="Select Nationality" TabIndex="37">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Religion</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlReligion" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" ToolTip="Select Religion" TabIndex="38">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Category</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlCategory" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" ToolTip="Select Category" TabIndex="39">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Caste</label>
                                        </div>
                                        <asp:DropDownList data-select2-enable="true" ID="ddlCaste" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" ToolTip="Select Caste" TabIndex="40">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>


                                </div>
                            </div>
                        </asp:Panel>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="41"
                                OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" />
                            <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="26"
                                OnClick="btnCancel_Click" CssClass="btn btn-danger" ToolTip="Click here to Reset" />--%>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript" language="javascript">

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
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122 || k <= 20) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
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

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

    </script>
    <script type="text/javascript">
        function CheckDateEalier(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("Future Date Not Accepted for Date Of Birth.");
                sender._selectedDate = new Date();
                sender._textbox.set_Value("");
            }
        }
    </script>

    <script type="text/javascript">
        function ValidateRegForm() {
            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {
                alert('Please Enter a valid email address');
                email.focus;
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

