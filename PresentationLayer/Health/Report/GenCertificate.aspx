<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GenCertificate.aspx.cs" Inherits="Health_Report_GenCertificate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <asp:UpdatePanel ID="updOpdTransaction" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GENERATE CERTIFICATES</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="panelSelectCerti" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Certificates</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Certificate</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSelectCertificate" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                TabIndex="1" CssClass="form-control" data-select2-enable="true" ToolTip="Select Certificate"
                                                OnSelectedIndexChanged="ddlSelectCertificate_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">CERTIFICATE OF RECOMMENDED,EXTENSION OR COMMUTATION OF LEAVE</asp:ListItem>
                                                <asp:ListItem Value="2">CERTIFICATE OF APPOINTMENT OF GAZETTED/NON GAZETTED CANDIDATE</asp:ListItem>
                                                <asp:ListItem Value="3">CERTIFICATE OF FITNESS TO RETURN TO DUTY</asp:ListItem>
                                                <asp:ListItem Value="4">CERTIFICATE OF FITNESS</asp:ListItem>
                                                <asp:ListItem Value="5">MEDICAL CERTIFICATE REFER 1</asp:ListItem>
                                                <asp:ListItem Value="6">MEDICAL CERTIFICATE REFER 2</asp:ListItem>
                                                <asp:ListItem Value="7">MEDICAL CERTIFICATE REFER 3</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                                ControlToValidate="ddlSelectCertificate" Display="None" ErrorMessage="Please select certificate name"
                                                ValidationGroup="Submit" InitialValue="0" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnlCreateCertificate" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Create Certificate</h5>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trSignature" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Signature of Government Servant</label>
                                            </div>
                                            <asp:TextBox ID="txtSignature" runat="server" MaxLength="100" TabIndex="2"
                                                ToolTip="Enter Signature of Government Servant"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeSignature" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" InvalidChars="0123456789"
                                                TargetControlID="txtSignature" ValidChars="()-.,  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvSignature" runat="server" ControlToValidate="txtSignature"
                                                Display="None" ErrorMessage="Please enter signature of government servant." SetFocusOnError="true"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trDrName" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Doctor Name </label>
                                            </div>
                                            <asp:TextBox ID="txtDrName" runat="server" ToolTip="Enter Doctor Name" TabIndex="3"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDrName" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtDrName"
                                                InvalidChars="0123456789" ValidChars="()-.  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvDrName" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter doctor name."
                                                ValidationGroup="Submit" ControlToValidate="txtDrName"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trPatientName">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Patient Name</label>
                                            </div>
                                            <asp:TextBox ID="txtPatientName" runat="server" ToolTip="Enter Patient Name" TabIndex="4"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePatientName" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtPatientName"
                                                InvalidChars="0123456789" ValidChars="()-.  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter patient name."
                                                ValidationGroup="Submit" ControlToValidate="txtPatientName"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trDepartment" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:TextBox ID="txtDepartment" runat="server" ToolTip="Enter department name" TabIndex="5"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeDepartment" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtDepartment"
                                                InvalidChars="0123456789" ValidChars="()-.  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter department name."
                                                ValidationGroup="Submit" ControlToValidate="txtDepartment"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trSufferingFrom" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Suffering From</label>
                                            </div>
                                            <asp:TextBox ID="txtSufferingFrom" runat="server" ToolTip="Enter diseases name" TabIndex="6"
                                                MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbSufferingFrom" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtSufferingFrom"
                                                InvalidChars="0123456789" ValidChars="()-.,  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvSufferingFrom" runat="server" SetFocusOnError="true" Display="None"
                                                ErrorMessage="Please enter disease name."
                                                ValidationGroup="Submit" ControlToValidate="txtSufferingFrom"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAbsenceday" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Absence Days</label>
                                            </div>
                                            <asp:TextBox ID="txtAbsenceDays" runat="server" MaxLength="100" TabIndex="7"
                                                ToolTip="Enter Number of Absence Days" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAbsenceDays" runat="server" InvalidChars=""
                                                TargetControlID="txtAbsenceDays" ValidChars="(0123456789)-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAbsenceDays" runat="server" ControlToValidate="txtAbsenceDays"
                                                Display="None" ErrorMessage="Please enter number of absence days." SetFocusOnError="true"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trFromDate" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgFCal">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" ToolTip="Enter the From date"
                                                    CssClass="form-control" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgFCal" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFromDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter Date"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trPostOf" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Post of</label>
                                            </div>
                                            <asp:TextBox ID="txtPostOf" runat="server" MaxLength="100" TabIndex="9" ToolTip="Enter Post of "
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePostOf" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" InvalidChars="LowerCaseLetters,UpperCaseLetters"
                                                TargetControlID="txtPostOf" ValidChars="(0123456789)-.  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvPostOf" runat="server" ControlToValidate="txtPostOf" Display="None"
                                                ErrorMessage="Please enter post of." SetFocusOnError="true" ValidationGroup="Submit">s
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAgeAccor" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Age According to Own Statement</label>
                                            </div>
                                            <asp:TextBox ID="txtAge" runat="server" MaxLength="100" TabIndex="10"
                                                ToolTip="Enter Age According to Own Statement" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAge" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters"
                                                InvalidChars="LowerCaseLetters,UpperCaseLetters"
                                                TargetControlID="txtAge" ValidChars="(0123456789)-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                                Display="None" ErrorMessage="Please enter age according to own statement."
                                                SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAgeAppearence" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Age by Appearance</label>
                                            </div>
                                            <asp:TextBox ID="txtAgeAppearance" runat="server" MaxLength="100" TabIndex="11"
                                                ToolTip="Enter age by appearance" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAgeAppearance" runat="server"
                                                InvalidChars="LowerCaseLetters,UpperCaseLetters" TargetControlID="txtAgeAppearance"
                                                ValidChars="(0123456789)-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvAgeAppearance" runat="server" ControlToValidate="txtAgeAppearance"
                                                Display="None" ErrorMessage="Please enter Age by Appearance." SetFocusOnError="true"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trReferTo" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Refer To</label>
                                            </div>
                                            <asp:TextBox ID="txtReferTO" runat="server" MaxLength="100" TabIndex="12"
                                                ToolTip="Enter Refer TO Doctor Name" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeReferTO" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" InvalidChars="0123456789"
                                                TargetControlID="txtReferTO" ValidChars="()-.,  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvReferTO" runat="server" ControlToValidate="txtReferTO"
                                                Display="None" ErrorMessage="Please enter refer to doctor name." SetFocusOnError="true"
                                                sValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trExpenditure" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Expenditure</label>
                                            </div>
                                            <asp:TextBox ID="txtExpenditure" runat="server" MaxLength="13" TabIndex="13"
                                                ToolTip="Enter the Expenditure in Rupees" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeExpenditure" runat="server" FilterType="Numbers"
                                                TargetControlID="txtExpenditure">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvExpenditure" runat="server" ControlToValidate="txtExpenditure"
                                                Display="None" ErrorMessage="Please enter the expenditure." SetFocusOnError="true"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trIssuedDate">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Issued Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtIssuedDate" runat="server" CssClass="form-control" ToolTip="Enter the issued date"
                                                    Style="z-index: 0;" TabIndex="14"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtIssuedDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtIssuedDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtIssuedDate" EmptyValueMessage="Please Enter Date"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit">
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                    ControlToValidate="txtIssuedDate" Display="None"
                                                    ErrorMessage="Please Enter Issued Date" SetFocusOnError="true"
                                                    ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAuthorizedMed">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Authorized Medical Attendant </label>
                                            </div>
                                            <asp:TextBox ID="txtAMAttentant" runat="server" ToolTip="Enter authorized medical attendant name"
                                                TabIndex="15" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAMAttentant" runat="server"
                                                FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtAMAttentant"
                                                InvalidChars="0123456789" ValidChars="()-.,  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="rfvtxtAMAttentant" runat="server" SetFocusOnError="true"
                                                Display="None" ErrorMessage="Please enter authorized medical attendant name."
                                                ValidationGroup="Submit" ControlToValidate="txtDepartment"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" TabIndex="16"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-outline-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="17"
                                        CssClass="btn btn-outline-danger" ToolTip="Click here to Reset" CausesValidation="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />
                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlGeneratCertiList" runat="server">
                                <asp:ListView ID="lvGenRateCer" runat="server">
                                    <LayoutTemplate>
                                        <div id="lgv1">
                                            <div class="sub-heading">
                                                <h5>Generate Certificate</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit
                                                        </th>
                                                        <th>Certificate Name
                                                        </th>
                                                        <th>Dr Name
                                                        </th>
                                                        <th>Patient Name
                                                        </th>
                                                        <th>Suffering From
                                                        </th>
                                                        <th>Issued Date
                                                        </th>
                                                        <th>Print
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("CER_ID") %>'
                                                    ImageUrl="~/images/edit.png" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:Label ID="LBL_CER_NO" runat="server" Text='<%# Eval("CER_NO") %>' Visible="false" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCerificateName" runat="server" Text='<%# Eval("CERTIFICATE_NAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDrName" runat="server" Text='<%# Eval("DrName") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName") %>' />
                                            </td>

                                            <td>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%# Eval("SufferingFrom") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNorValue" runat="server" Text='<%# Eval("IssuedDate", "{0:dd-MMM-yyyy}") %>' />
                                            </td>

                                            <td>
                                                <asp:Button ID="BtnPrint" runat="server" CommandArgument='<%# Eval("CER_ID") %>'
                                                    ImageUrl="~/images/edit.png" ToolTip="Click here to Show Record" Text="Print"
                                                    CommandName='<%# Eval("CER_NO") %>' OnClick="BtnPrint_Click" CssClass="btn btn-info" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

