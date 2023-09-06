<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PatientDetails.aspx.cs" Inherits="Health_Transaction_PatientDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--        <script src="../../includes/prototype.js" type="text/javascript"></script>
    <script src="../../includes/scriptaculous.js" type="text/javascript"></script>
    <script src="../../includes/modalbox.js" type="text/javascript"></script>--%>
    <%--<script src="../../jquery/jquery-1.9.1.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>
    <script type="text/javascript">
        var jq = $.noConflict();

    </script>

    <%--    <script type="text/javascript">
        ; debugger
        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }

        var jq = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    jq('#ContentPlaceHolder1_imgEmpPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>--%>
    <script type="text/javascript">

        function clientShowing(source, args) {
            source._popupBehavior._element.style.zIndex = 9999999999;
        }

        var $ = $.noConflict();

        function ShowpImagePreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#ContentPlaceHolder1_imgEmpPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>


    <%--    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    </div>--%>

    <%--    <asp:UpdatePanel ID="updOpdTransaction" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PATIENT DETAILS</h3>
                </div>
                <div class="box-body">

                    <div id="trAdd" runat="server">
                        <asp:Panel ID="pnlAdd" runat="server">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Patient Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImaCalStartDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtEnterDate" runat="server" CssClass="form-control" ToolTip="Enter Date"
                                                Style="z-index: 0;" TabIndex="1"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server"
                                                Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate"
                                                TargetControlID="txtEnterDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meReqdate" runat="server" TargetControlID="txtEnterDate"
                                                Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meReqdate"
                                                ControlToValidate="txtEnterDate" EmptyValueMessage="Please Enter date"
                                                InvalidValueMessage="Last Date of Recipt is Invalid (Enter dd/MM/yyyy Format)"
                                                Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Doctor"
                                                SetFocusOnError="True" ErrorMessage="mevIndDate" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Time</label>
                                        </div>
                                        <asp:TextBox ID="txtEnterTime" runat="server" CssClass="form-control" ToolTip="Enter Time"
                                            ValidationGroup="Doctor" TabIndex="2" />
                                        <ajaxToolKit:MaskedEditExtender ID="meeEnterTime" runat="server" TargetControlID="txtEnterTime"
                                            Mask="99:99:99" MaskType="Time" AcceptAMPM="True"
                                            ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevEnterTime" runat="server" ControlExtender="meeEnterTime"
                                            ControlToValidate="txtEnterTime" IsValidEmpty="False" EmptyValueMessage="Time is required"
                                            InvalidValueMessage="Time is invalid" Display="None" TooltipMessage="Input a time"
                                            EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="Doctor" ErrorMessage="mevEnterTime" />
                                        <label><span style="font-style: italic; font-size: smaller">Press 'A' or 'P' for AM & PM</span></label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Search</label>
                                        </div>
                                        <div class="input-group date">

                                            <asp:RadioButtonList ID="rdbSearchList" runat="server" RepeatDirection="Horizontal" ToolTip="Search Patient">
                                                <asp:ListItem Selected="True" Value="0">Staff ID</asp:ListItem>
                                                <asp:ListItem Value="1">Admission No.</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <div class="input-group-addon" id="Div3">
                                                <a href="#" title="Search Patient Details" data-toggle="modal" data-target="#divdemo2">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="1" />
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                    </div>
                                    <div class="form-group col-md-2">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblEmp" runat="server" Font-Bold="true" Text="Staff ID"></asp:Label>
                                            <asp:Label ID="lblDot" runat="server" Font-Bold="true" Text=":"></asp:Label>
                                        </div>
                                        <div class="col-md-5">
                                            <asp:Label ID="lblEmployeeCode" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <asp:Label ID="lblPatientCat" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/Images/nophoto.jpg" Height="110px" Width="110px" CssClass="form-control" ToolTip="Image" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Search Patient </label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtPatientName" runat="server" CssClass="form-control" MaxLength="100"
                                                TabIndex="3" ToolTip="Search Patient"></asp:TextBox>
                                            <%-- <asp:DropDownList ID="ddlEmployee" runat="server" AppendDataBoundItems="true" 
                                                                    Width="255px" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>--%>
                                            <asp:RequiredFieldValidator ID="rfvPatientName" runat="server" ControlToValidate="txtPatientName"
                                                Display="None" ErrorMessage="Please search patient name." SetFocusOnError="true" ValidationGroup="Doctor" />

                                            <div class="input-group-addon" id="Div4" style="border-bottom: 1px solid #fff;">
                                                <asp:Button ID="btnSearchOnForm" runat="server" CssClass="btn btn-primary" OnClick="btnSearchOnForm_Click" Text="Search" ToolTip="Search Patient" TabIndex="3" />
                                                <asp:HiddenField ID="hfPatientName" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Sex</label>
                                        </div>
                                        <asp:RadioButton ID="rdbMale" runat="server" Checked="true" Text="Male" GroupName="Sex" TabIndex="4" />&nbsp;&nbsp;                                                                        
                                               <asp:RadioButton ID="rdbFemale" runat="server" Text="Female" GroupName="Sex" TabIndex="5" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Age</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" MaxLength="3" onkeyup="validateNumeric(this);"
                                                TabIndex="4" ToolTip="Enter Age"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge" Display="None"
                                                ErrorMessage="Please enter age." SetFocusOnError="true" ValidationGroup="Doctor" />

                                            <div class="input-group-addon">
                                                <label>yrs</label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trDependent" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbPCList" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdbPCList_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">Self</asp:ListItem>
                                            <asp:ListItem Value="1">Dependent</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic" id="FirstDep" runat="server" visible="false">
                                            <sup>*</sup>
                                            <label>Dependents</label>
                                        </div>
                                        <div id="ThiDep" runat="server" visible="false">
                                            <asp:DropDownList ID="ddlDependent" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlDependent_SelectedIndexChanged" data-select2-enable="true"
                                                ValidationGroup="Doctor" CssClass="form-control" ToolTip="Select Dependents">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDependent" runat="server" ControlToValidate="ddlDependent" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select Dependent." ValidationGroup="Doctor" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-sm-1 form-group" id="Div1" runat="server" visible="false">
                                    </div>
                                    <div class="col-sm-1 form-group" id="SecDep" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Height</label>
                                            </div>
                                            <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" MaxLength="7"
                                                onkeyup="validateNumeric(this);" TabIndex="5" ToolTip="Enter Height"></asp:TextBox>
                                            <label>cm</label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Weight</label>
                                            </div>
                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" MaxLength="3" onkeyup="validateNumeric(this);"
                                                TabIndex="6" ToolTip="Enter Weight"></asp:TextBox>
                                            <label>kg</label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnBMIndex" runat="server" Text="Body Mass Index" OnClick="btnBMIndex_Click" TabIndex="9"
                                                CssClass="btn btn-primary" ToolTip="Click here to Calculate BMI" />
                                            <asp:Label ID="lblBMI" runat="server" Font-Bold="true" Text=""></asp:Label>
                                            <asp:Label ID="lblBMIUnit" runat="server" Font-Bold="true" Text="Kg/meter Square"></asp:Label>
                                        </div>

                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6" id="divRefBy" runat="server" visible="false">
                                                <div class="col-md-4">
                                                    <label>Reference By :</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" MaxLength="50"
                                                        TabIndex="10" ToolTip="Enter Reference By"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCReference" runat="server"
                                                        FilterType="Custom,LowerCaseLetters,UpperCaseLetters" TargetControlID="txtReference" ValidChars=".  ">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <div class="col-md-4">
                                                    <label>Blood Group:</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlBloodGrp" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                        ValidationGroup="Doctor" CssClass="form-control" ToolTip="Select Blood Group" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="trAdd2" runat="server">
                        <asp:Panel ID="pnlAdd2" runat="server">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Doctor Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Doctor's Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDoctor" runat="server" AppendDataBoundItems="true" TabIndex="8" ValidationGroup="Doctor"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" ToolTip="Select Doctor's Name"
                                            OnSelectedIndexChanged="ddlDoctor_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hfDoctorsName" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvDoctor" runat="server" ControlToValidate="ddlDoctor" InitialValue="0"
                                            Display="None" ErrorMessage="Please select doctor." ValidationGroup="Doctor" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Complaints</label>
                                        </div>
                                        <asp:TextBox ID="txtComplaints" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="9" ToolTip="Enter Complaints"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Diagnosis</label>
                                        </div>
                                        <asp:TextBox ID="txtDiagnosis" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="10" ToolTip="Enter Diagnosis"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Instructions</label>
                                        </div>
                                        <asp:TextBox ID="txtInstructions" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="11" ToolTip="Enter Instructions"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divChief" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Chief Complaints</label>
                                        </div>
                                        <asp:TextBox ID="txtChiefComplaints" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="14" ToolTip="Enter Chief Complaints"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Present history</label>
                                        </div>
                                        <asp:TextBox ID="txtFindings" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="15" ToolTip="Enter Present history"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Past history</label>
                                        </div>
                                        <asp:TextBox ID="txtPastHistory" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="12" ToolTip="Enter Past history"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Family history </label>
                                        </div>
                                        <asp:TextBox ID="txtFamilyHistory" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="13" ToolTip="Enter Family history"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>History of chronic disease/Accident</label>
                                        </div>
                                        <asp:TextBox ID="txtChronicDiesease" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="14" ToolTip="Enter History of chronic disease/Accident"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divMinor" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Minor surgical procedures/dressings</label>
                                        </div>
                                        <asp:TextBox ID="txtSurgicalProc" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="20" ToolTip="Enter Minor surgical procedures/dressings"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Referred to</label>
                                        </div>
                                        <asp:TextBox ID="txtReferred" TextMode="MultiLine" runat="server" CssClass="form-control"
                                            MaxLength="100" TabIndex="15" ToolTip="Enter Referred to"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Patient status (Admitted)</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdbAdmittedStatus" runat="server" RepeatDirection="Horizontal" TabIndex="23">
                                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>BP</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtBP" runat="server" CssClass="form-control" MaxLength="10"
                                                TabIndex="16" ToolTip="Enter Blood Pressure(BP)"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeBP" runat="server" FilterType="Custom, Numbers" TargetControlID="txtBP" ValidChars="/">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <div class="input-group-addon">
                                                <label>mmHg</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Temperature</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtTemprature" onkeyup="validateNumeric(this);" runat="server" CssClass="form-control"
                                                MaxLength="3" TabIndex="17" ToolTip="Enter Temperature"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <label>°F</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Pulse Rate</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtPulseRate" runat="server" CssClass="form-control" MaxLength="3"
                                                TabIndex="18" ToolTip="Enter Pulse Rate"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePulseRate" runat="server" FilterType="Numbers"
                                                TargetControlID="txtPulseRate" ValidChars=" ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <div class="input-group-addon">
                                                <label>beats/min</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Respiration</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtRespiration" runat="server" CssClass="form-control" MaxLength="3"
                                                TabIndex="19" ToolTip="Enter Respiration"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRespi" runat="server" FilterType="Numbers"
                                                TargetControlID="txtRespiration" ValidChars="">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                            <div class="input-group-addon">
                                                <label>breaths/min</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Random Sugar Level</label>
                                        </div>
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtRandomSugar" runat="server" CssClass="form-control" MaxLength="10"
                                                TabIndex="20" ToolTip="Enter Random Sugar Level"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRSL" runat="server" FilterType="Custom, Numbers"
                                                TargetControlID="txtRandomSugar" ValidChars="-">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <div class="input-group-addon">
                                                <label>mg/dL</label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="tr2" runat="server" visible="false">
                        <asp:Panel ID="Panel3" runat="server">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Certificate Issue</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Issue Certificates</label>
                                        </div>
                                        <asp:CheckBox ID="chkCerti" runat="server" Text="Issue Certificate" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="tr1" runat="server">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Laboratory Test</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Laboratory Test/ Investigations</label>
                                        </div>
                                        <asp:CheckBox ID="chkLab" runat="server" TabIndex="28" AutoPostBack="true"
                                            OnCheckedChanged="chkLab_CheckedChanged" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="trTest" runat="server" visible="false">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Test Title</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTitle" runat="server" AppendDataBoundItems="true" TabIndex="29" data-select2-enable="true"
                                            ValidationGroup="add" CssClass="form-control" ToolTip="Select Test Title" AutoPostBack="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="ddlTitle" InitialValue="0"
                                            Display="None" ErrorMessage="Please select  test title." ValidationGroup="add"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add Test" OnClick="btnAdd_Click" TabIndex="30"
                                            ValidationGroup="add" CssClass="btn btn-outline-primary" ToolTip="Click here to Add" />
                                        <asp:ValidationSummary ID="VSadd" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="add" />
                                    </div>
                                    <div class="form-group col-md-7">

                                        <div id="trChkBox" runat="server" visible="true" class="form-group col-md-12">
                                            <div class="col-md-4">
                                                <asp:CheckBox ID="chkTest" runat="server" AutoPostBack="true" Visible="false" OnCheckedChanged="chkTest_CheckedChanged" Text="Select All" Font-Bold="true" />
                                            </div>
                                            <div class="col-md-6">

                                                <asp:CheckBoxList ID="chkBList" runat="server" RepeatDirection="Horizontal" Visible="false" RepeatColumns="3" Width="370"></asp:CheckBoxList>

                                            </div>
                                            <div class="col-md-2">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="add" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-5">
                                        <asp:Panel ID="lvPanel" runat="server" Visible="false">
                                            <asp:ListView ID="lvTest" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>List Of Test Title</h5>
                                                        </div>
                                                        <table class="table table-bordered table-hover">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Delete </th>
                                                                    <th>TEST TITLE</th>
                                                                    <th>PRINT</th>

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
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("SRNO") %>'
                                                                ImageUrl="~/images/delete.png" OnClick="btnDeleteRec_Click" ToolTip="Delete Record" />
                                                        </td>
                                                        <td><%# Eval("TEST_TITLE") %></td>
                                                        <td>
                                                            <asp:Button ID="btnReport" runat="server" Text="Print" CommandArgument='<%# Eval("OBSERNO") %>'
                                                                OnClick="btnReport_Click" CssClass="btn btn-outline-info" ToolTip="Click here to Print"
                                                                Enabled='<%#Eval("OBSERNO").ToString() == "0" ? false : true %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="trGrid" runat="server" class="col-12">
                        <asp:Panel ID="pnlSGrid" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Prescription List</h5>
                                </div>

                                <asp:Panel ID="pnlSecondGridList" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="SecondGrid" runat="server" AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover"
                                        HeaderStyle-BackColor="ActiveBorder" AlternatingRowStyle-BackColor="#FFFFAA">
                                        <HeaderStyle CssClass="bg-light-blue" />
                                        <%--<AlternatingRowStyle BackColor="#FFFFD2" />--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="item">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDel" AlternateText="Delete Record" CommandArgument='<%# Eval("UNIQUE") %>'
                                                        ToolTip="Delete Record" runat="server" ImageUrl="~/images/delete.gif" OnClick="btnDelete_Click" />&nbsp;
                                                                        <asp:HiddenField ID="hdnINO" runat="server" Value='<%#Eval("INO")%>' />
                                                    <asp:HiddenField ID="hdnDosesID" runat="server" Value='<%# Eval("DOSES") %>' />
                                                    <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("UNIQUE") %>' />
                                                    <asp:HiddenField ID="hdnPStatusID" runat="server" Value='<%# Eval("PRESCRIPTION_STATUS_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="INO" HeaderText="ITEM NUMBER" Visible="false" />
                                            <asp:BoundField DataField="ITEMNAME" HeaderText="ITEM NAME" />
                                            <asp:BoundField DataField="NOOFDAYS" HeaderText="NOOFDAYS" />
                                            <asp:BoundField DataField="DOSES_ID" HeaderText="DOSES" />
                                            <asp:BoundField DataField="DOSES" HeaderText="DOSES_ID" Visible="false" />
                                            <asp:BoundField DataField="QTY" HeaderText="QUANTITY" />
                                            <%--<asp:BoundField DataField="SPINST" HeaderText="SPINST" />--%>
                                            <asp:BoundField DataField="UNIQUE" HeaderText="SRNO" Visible="false" />
                                            <asp:BoundField DataField="PRESCRIPTION_STATUS_ID" HeaderText="ISSUE STATUS" Visible="false" />
                                            <asp:BoundField DataField="PRESCRIPTION_STATUS" HeaderText="Issue Status" />

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </div>
                        </asp:Panel>
                    </div>
                    <div>
                        <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                    PopupControlID="Panel4" TargetControlID="btnPrescription" BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel">
                                </ajaxToolKit:ModalPopupExtender>--%>
                        <div id="divPrescription" class="modal fade" role="dialog">
                            <%--data-backdrop="static"--%>
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Add Prescription</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>
                                    <%--<div class="modal-body clearfix pb-0">--%>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="UpdatePrescription" runat="server">
                                            <ContentTemplate>
                                                <%--<asp:Panel ID="Panel4" runat="server" CssClass="modalPopup" Style="display: none; height: auto; width: 60%;">--%>
                                                <%--<asp:Panel ID="Panel4" runat="server" CssClass="PopupReg" align="center" Style="display: none; height: 70%; width: 60%;">--%>
                                                <%--<div class="panel panel-info">--%>

                                                <div class="col-md-12">
                                                    <asp:Button ID="btnCloseP" runat="server" Text="X" Style="font-family: Verdana; font-weight: bold; font-size: 10px"
                                                        CssClass="btnClosePop" Visible="false"
                                                        ToolTip="Close Prescription" OnClick="btnCloseP_Click" />
                                                    <%--</div>--%>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Item Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" ToolTip="Enter Item Name"></asp:TextBox>
                                                            <asp:HiddenField ID="hfItemName" runat="server" />
                                                            <ajaxToolKit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                TargetControlID="txtItemName"
                                                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                                                ServiceMethod="GetItemName" OnClientShowing="clientShowing" OnClientItemSelected="ItemName">
                                                            </ajaxToolKit:AutoCompleteExtender>
                                                            <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="txtItemName"
                                                                Display="None" ErrorMessage="Please enter item name" ValidationGroup="prescription"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>No. of Days</label>
                                                            </div>
                                                            <asp:TextBox ID="txtNumberofDays" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Number Of Days" MaxLength="3"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeNoOfDays" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtNumberofDays" ValidChars="">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvNoOfdays" runat="server" ControlToValidate="txtNumberofDays"
                                                                Display="None" ErrorMessage="Please enter number of days." ValidationGroup="prescription"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Issue Status</label>
                                                            </div>
                                                            <asp:RadioButton ID="rdbYes" runat="server" Checked="true" GroupName="Status" Text="Issued" />&nbsp;&nbsp;
                                                                    <asp:RadioButton ID="rdbNo" runat="server" GroupName="Status" Text="Prescribed" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Dosage</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDosage" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                                ToolTip="Select Dosage" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDosage" runat="server" ControlToValidate="ddlDosage" InitialValue="0"
                                                                Display="None" ErrorMessage="Please select dosages." ValidationGroup="prescription"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Quantity" MaxLength="5"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeQty" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtQuantity" ValidChars="">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtQuantity"
                                                                Display="None" ErrorMessage="Please enter quantity." ValidationGroup="prescription"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12" id="divSpeInst" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Special Instructions:</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecialInstructions" TextMode="MultiLine" runat="server" CssClass="form-control" ToolTip="Enter Special Instructions" MaxLength="30"></asp:TextBox>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <%--<p class="text-center">--%>
                                                            <asp:Button ID="btnAddPresc" runat="server" Text="Add Prescription" OnClick="btnAddPrescription_OnClick"
                                                                ValidationGroup="prescription" CssClass="btn btn-primary" ToolTip="Click here to Add Prescriptions" />
                                                            <asp:ValidationSummary ID="vsPriscription" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="prescription" />
                                                            <%--</div>--%>

                                                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_OnClick"
                                                                Visible="false" CssClass="btn btn-primary" ToolTip="Click here to Confirm" />&nbsp;&nbsp;  
                                                                <asp:Button ID="btnCancelP" runat="server" Text="Cancel" OnClick="btnCancelP_Click" Visible="false" CssClass="btn btn-warning" />
                                                            <%--</p>--%>
                                                        </div>

                                                        <div class="form-group col-md-12 text-center">
                                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                                <asp:GridView ID="gvPrisc" runat="server" AutoGenerateColumns="False"
                                                                    CssClass="table table-striped table-bordered table-hover"
                                                                    HeaderStyle-BackColor="ActiveBorder" AlternatingRowStyle-BackColor="#FFFFAA">
                                                                    <HeaderStyle CssClass="bg-light-blue" />

                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="item">
                                                                            <ItemTemplate>
                                                                                <asp:ImageButton ID="btnDelete" AlternateText="Delete Record"
                                                                                    CommandArgument='<%# Eval("UNIQUE") %>'
                                                                                    ToolTip="Delete Record" runat="server" ImageUrl="~/images/delete.gif"
                                                                                    OnClick="btnDelete_Click" />&nbsp;
                                                                                <asp:HiddenField ID="hdnINO" runat="server" Value='<%# Eval("INO") %>' />
                                                                                <asp:HiddenField ID="hdnDosesID" runat="server" Value='<%# Eval("DOSES") %>' />
                                                                                <asp:HiddenField ID="hdnSRNO" runat="server" Value='<%# Eval("UNIQUE") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="INO" HeaderText="ITEM NUMBER" Visible="false" />
                                                                        <asp:BoundField DataField="ITEMNAME" HeaderText="ITEM NAME" />
                                                                        <asp:BoundField DataField="NOOFDAYS" HeaderText="NOOFDAYS" />
                                                                        <asp:BoundField DataField="DOSES_ID" HeaderText="DOSES" />
                                                                        <asp:BoundField DataField="DOSES" HeaderText="DOSES_ID" Visible="false" />
                                                                        <asp:BoundField DataField="QTY" HeaderText="QUANTITY" />
                                                                        <%--<asp:BoundField DataField="SPINST" HeaderText="SPINST" />--%>
                                                                        <asp:BoundField DataField="UNIQUE" HeaderText="SRNO" Visible="false" />

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </div>

                                                    </div>
                                                </div>
                                                <%-- </asp:Panel>--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnPrescription" />
                                                <asp:PostBackTrigger ControlID="btnAddPresc" />
                                                <asp:PostBackTrigger ControlID="btnConfirm" />
                                                <asp:PostBackTrigger ControlID="btnCancelP" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>

                        </div>

                        <div class="box-footer">
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPrescription" runat="server" Text="Prescription" OnClick="btnPrescription_OnClick"
                                    ValidationGroup="Doctor" Visible="false" CssClass="btn btn-outline-primary" ToolTip="Click here for Prescription" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Doctor" OnClick="btnSubmit_OnClick"
                                    CssClass="btn btn-outline-primary" Visible="false" ToolTip="Click here to Submit" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" CssClass="btn btn-outline-danger"
                                    Visible="false" ToolTip="Click here to reset" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Doctor" />
                                <p>
                            </div>
                            <%--  <div class="col-md-12" id="trPatientHist" runat="server">
                                <asp:Panel ID="pnlPatientHist" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel panel-heading">Patient History</div>
                                        <div class="panel panel-body">
                                            <asp:Panel ID="pnlPatientHistDetail" runat="server" ScrollBars="Auto">
                                                <asp:GridView ID="lvPatientHist" runat="server" AutoGenerateColumns="False"
                                                    CssClass="vista-grid" HeaderStyle-BackColor="ActiveBorder">
                                                    <HeaderStyle CssClass="bg-light-blue" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-CssClass="item">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDetails" runat="server" AlternateText="Show Details" CommandArgument='<%# Eval("OPDID") %>'
                                                                    ImageUrl="~/IMAGES/edit.gif" OnClick="btnDetails_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print" ItemStyle-CssClass="item">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnPrint" AlternateText="Print Record" CommandArgument='<%# Eval("OPDID") %>'
                                                                    runat="server" ImageUrl="~/IMAGES/print.gif" OnClick="btnPrint_Click" ToolTip='<%# Eval("OPDID") %>' />&nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NAME" HeaderText="PATIENT NAME" />
                                                        <asp:BoundField DataField="DRNAME" HeaderText="DR.NAME" />
                                                        <asp:TemplateField HeaderText="OPD DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("OPDDATE","{0:d}") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="COMPLAINT" HeaderText="COMPLAINT" />
                                                        <asp:BoundField DataField="FINDING" HeaderText="FINDING" />
                                                        <asp:BoundField DataField="DIAGNOSIS" HeaderText="DIAGNOSIS" />
                                                        <asp:BoundField DataField="INSTRUCTION" HeaderText="INSTRUCTION" />
                                                        <asp:BoundField DataField="HEIGHT" HeaderText="HEIGHT(Inches)" />
                                                        <asp:BoundField DataField="WEIGHT" HeaderText="WEIGHT(KGs)" />
                                                        <asp:BoundField DataField="TEMP" HeaderText="TEMP" />
                                                        <asp:BoundField DataField="PULSE" HeaderText="PULSE" />
                                                        <asp:BoundField DataField="RESP" HeaderText="RESPIRATION" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>--%>
                            <div id="trPatientHist" runat="server">
                                <asp:UpdatePanel ID="lvUpdate" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPatientHist" runat="server">
                                            <div class="col-12">
                                                <asp:ListView ID="lvPatientHist" runat="server">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="ibler" runat="server" Text="No Record Found" CssClass="d-block text-center mt-3"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Patient History</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>View </th>
                                                                    <th>Print</th>
                                                                    <th>Patient Name </th>
                                                                    <th>Dr.Name </th>
                                                                    <th>OPD Date </th>
                                                                    <th>Diagnosis </th>
                                                                    <th>Instruction </th>
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
                                                                <asp:ImageButton ID="btnDetails" runat="server" AlternateText="Show Details" CommandArgument='<%# Eval("OPDID") %>' ImageUrl="~/IMAGES/edit.png" OnClick="btnDetails_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnPrint" runat="server" AlternateText="Print Record" CommandArgument='<%# Eval("OPDID") %>' ImageUrl="~/IMAGES/print.png" OnClick="btnPrint_Click" ToolTip='<%# Eval("OPDID") %>' />
                                                                <%--<asp:Button ID="btnPrint" runat="server" AlternateText="Print Record" CommandArgument='<%# Eval("OPDID") %>' OnClick="btnPrint_Click" ToolTip='<%# Eval("OPDID") %>' />--%>
                                                                        &nbsp; </td>
                                                            <td><%# Eval("NAME")%></td>
                                                            <td><%# Eval("DRNAME")%></td>
                                                            <td><%#Eval("OPDDATE","{0:d}") %></td>
                                                            <td><%# Eval("DIAGNOSIS")%></td>
                                                            <td><%# Eval("INSTRUCTION")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvPatientHist" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="vista-grid_datapager">
                                </div>
                            </div>
                            <%--<p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                        <p>
                                        </p>
                                    </p>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>--%>





    <div id="divdemo2" class="modal fade" role="dialog">
        <%--data-backdrop="static"--%>
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <%--<div class="modal-body clearfix pb-0">--%>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="form-group col-md-12">
                                    <label>Search Criteria:</label>
                                    <br />
                                    <asp:RadioButtonList ID="rbPatient" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Staff ID" Value="EmployeeCode" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Emp. Name" Value="EmployeeName"></asp:ListItem>
                                        <asp:ListItem Text="Student Name" Value="StudentName"></asp:ListItem>
                                        <asp:ListItem Text="Admission No." Value="StudentRegNo"></asp:ListItem>
                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Search String :</label>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" ToolTip="Enter Search String"></asp:TextBox>
                                </div>
                                <div class="form-group col-md-12">
                                    <p class="text-center">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-info"
                                            OnClientClick="return submitPopup(this.name);" ToolTip="Click here to Search" />
                                        <asp:Button ID="btnCanceModal" runat="server" Text="Cancel" CssClass="btn btn-warning"
                                            OnClientClick="return ClearSearchBox(this.name)" ToolTip="Click here to Reset" OnClick="btnCanceModal_Click" />
                                        <%--<asp:Button ID="Button1" runat="server" Text="Close" class="btn btn-default" data-dismiss="modal"
                                            AutoPostBack="True" CssClass="btn btn-primary" />--%>
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                    </p>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:UpdatePanel ID="updEdit" runat="server">
                                        <ContentTemplate>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px">--%>
                                            <%--<asp:Panel ID="pnlSearch" runat="server" ScrollBars="Auto" Height="300px" Width="520px">--%>
                                            <asp:ListView ID="lvEmp" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <%--<h4 class="box-title">Login Details
                                                        </h4>--%>
                                                        <asp:Panel ID="pnlSearch" runat="server" ScrollBars="Vertical" Height="300px">
                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                    <tr id="trHeader" runat="server" class="bg-light-blue">
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Employee Code
                                                                        </th>
                                                                        <th>Department
                                                                        </th>
                                                                        <th>Designation
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name")%>' CommandArgument='<%# Eval("IDNo")%>'
                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PFILENO")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDEPT")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SUBDESIG")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <%--</asp:Panel>--%>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSearch" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function submitPopup(btnSearch) {
            //debugger;
            var rbText;
            var searchtxt;

            if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_0').checked == true)
                rbText = "EmployeeCode";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_1').checked == true)
                rbText = "EmployeeName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_2').checked == true)
                rbText = "StudentName";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_3').checked == true)
                rbText = "StudentRegNo";
            else if (document.getElementById('ctl00_ContentPlaceHolder1_rbPatient_4').checked == true)
                rbText = "Other";

            searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
            __doPostBack('btnSearch', rbText + ',' + searchtxt);
            return true;
        }


        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
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

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

    </script>

    <script type="text/javascript">

        function ItemName(source, eventArgs) {
            var idno = eventArgs.get_value().split("*");
            //alert(idno);
            var Name = idno[0].split("-");
            // alert(Name);

            document.getElementById("<%= txtItemName.ClientID %>").value = idno[1];
            document.getElementById("<%= hfItemName.ClientID %>").value = Name[0];
        }
        //  keeps track of the delete button for the row

        //document.getElementById('ctl00_ContentPlaceHolder1_txtItemName').value = idno[1];
        //document.getElementById('ctl00_ContentPlaceHolder1_hfItemName').value = Name[0];

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

    </script>
    <script>

        function Close() {
            //$("#Details_Veiw").Show();
            //alert("Close");
            $("#myModal2").modal('hide');
        }
    </script>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

