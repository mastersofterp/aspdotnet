<%@ Page Title="" Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_Sb_PreviousService.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_Sb_PreviousService" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="sbhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sbctp" runat="Server">


    <link href="../Css/master.css" rel="stylesheet" type="text/css" />
    <link href="../Css/Theme1.css" rel="stylesheet" type="text/css" />


    <asp:UpdatePanel ID="updImage" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Previous Experience</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Post Name/Designation</label>
                                            </div>
                                            <asp:TextBox ID="txtPostName" runat="server" CssClass="form-control" ToolTip="Enter Post Name"
                                                onkeyup="validateAlphabet(this);" MaxLength="50" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtPostName" Display="None" ErrorMessage="Please Enter Post Name"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="2" AutoPostBack="true"
                                                    ToolTip="Enter From Date" Style="z-index: 0;" onBlur="CalDuration();" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                                <%-- OnTextChanged="txtFromDate_TextChanged"--%>

                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date"
                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>

                                        <%--  <div class="form-group col-md-4">
                                                <label><span style="color: #FF0000">*</span> From Date :  </label>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <asp:Image ID="imgCalholidayDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                    </div>
                                                    <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="5"
                                                        ToolTip="Enter Holiday Starting Date" />
                                                    <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                                        Display="None" ErrorMessage="Please Enter Holiday From Date" ValidationGroup="Holiday"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFromDt" PopupButtonID="imgCalholidayDt" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true"/>
                                                    <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                                        ControlToValidate="txtFromDt" 
                                                        InvalidValueMessage="Holiday From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                        TooltipMessage="Please Enter Holiday Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                        ValidationGroup="Holiday" SetFocusOnError="true" IsValidEmpty="false" InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" ToolTip="Enter To Date" Style="z-index: 0;" AutoPostBack="true"
                                                    OnTextChanged="txtToDate_TextChanged" CssClass="form-control" TabIndex="3" onChange="CalDuration();"></asp:TextBox>
                                                <%--onBlur="CalDuration();" onChange="CalDuration();"--%>

                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter to Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                <%-- <asp:CompareValidator ID--%>

                                                <%-- <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                            Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" />--%>

                                                <%--                                                       <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than  or equal to From Date"
                                                            ValidationGroup="ServiceBook" SetFocusOnError="true" ControlToCompare="txtFromDate" 
                                                            ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date" ></asp:CompareValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Total Experience</label>
                                            </div>
                                            <asp:TextBox ID="txtExperience" Enabled="false" runat="server" CssClass="form-control"
                                                ToolTip="Total Experience" TabIndex="4"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtotalexperience" runat="server"
                                                ControlToValidate="txtExperience" Display="None" ErrorMessage="Please Enter Total Experience"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Experience Type:</label>
                                            <asp:DropDownList ID="ddlexptype" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Enter Experience Type" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Academic</asp:ListItem>
                                                <asp:ListItem Value="2">Industry </asp:ListItem>
                                                <asp:ListItem Value="3">Research </asp:ListItem>
                                                <asp:ListItem Value="4">Professional </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>




                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Institute/Organization Name</label>
                                            </div>
                                            <asp:TextBox ID="txtInstitute" runat="server" CssClass="form-control" TabIndex="6"
                                                ToolTip="Enter Institute Name" MaxLength="120"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvNameofInistitutionaAndPostHeld" runat="server"
                                                ControlToValidate="txtInstitute" Display="None" ErrorMessage="Please Enter Institute Name"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>University Name :  </label>

                                            <asp:TextBox ID="txtuniversity" runat="server" CssClass="form-control" TabIndex="7" MaxLength="100"
                                                ToolTip="Enter University Name" onkeyup="validateAlphabet(this);"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Organization Type :</label>
                                            <asp:DropDownList ID="ddlnaturework" runat="server" CssClass="form-control" TabIndex="8" ToolTip="Enter Organization Type" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Government</asp:ListItem>
                                                <asp:ListItem Value="2">Private </asp:ListItem>
                                                <asp:ListItem Value="3">Semi-Government</asp:ListItem>
                                                <asp:ListItem Value="4">Govt Aided</asp:ListItem>
                                                <asp:ListItem Value="5">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Nature of work & Responsibilities :</label>
                                            <asp:TextBox ID="txtnaturework" runat="server" CssClass="form-control" TabIndex="9" MaxLength="100"
                                                onkeyup="validateAlphabet(this);" ToolTip="Enter Nature Of Work"></asp:TextBox>
                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Department :</label>
                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" TabIndex="10" MaxLength="100"
                                                onkeyup="validateAlphabet(this);" ToolTip="Enter Department"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Pay Scale :</label>
                                            <asp:TextBox ID="txtpayscale" runat="server" CssClass="form-control" TabIndex="11" onkeypress="return isNumberKey(event);"
                                                ToolTip="Enter Pay Scale" MaxLength="20"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbescale" runat="server" TargetControlID="txtpayscale" FilterType="Numbers,Custom"
                                                ValidChars="-">
                                            </ajaxToolKit:FilteredTextBoxExtender>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Last Gross Salary: </label>
                                            <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control" TabIndex="12" ToolTip="Enter Last Gross Salary"
                                                onkeypress="return CheckNumeric(event,this);" MaxLength="10">
                                            </asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbelastsal" runat="server" TargetControlID="txtsalary" FilterType="Custom,Numbers"
                                                ValidChars=".">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Reason For Leaving Service</label>
                                            </div>
                                            <asp:TextBox ID="txtReasonForTerminationOfService" runat="server" CssClass="form-control" MaxLength="100"
                                                ToolTip="Enter Reason For Leaving Service" TabIndex="13" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvReasonForTerminationOfService" runat="server"
                                                ControlToValidate="txtReasonForTerminationOfService" Display="None"
                                                ErrorMessage="Please Enter Reason For Termination Of Service"
                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Reference of Senior Person</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Name :</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRefName" runat="server" CssClass="form-control" MaxLength="80"
                                                        ToolTip="Enter Name" TabIndex="14" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Designation :</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDes" runat="server" CssClass="form-control" MaxLength="80"
                                                        ToolTip="Enter Designation" TabIndex="15" onkeyup="validateAlphabet(this);"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Email Id :</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50"
                                                        ToolTip="Enter EmailId" TabIndex="16"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                        Display="Dynamic" ErrorMessage="Invalid email address" />


                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Mobile no :</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMob" runat="server" CssClass="form-control" MaxLength="10"
                                                        ToolTip="Enter Mobile Number" TabIndex="17" onkeyup="validateNumeric(this);"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <br />

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trOff" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Desig.of Attest.Officer</label>
                                            </div>
                                            <asp:TextBox ID="txtDesignationOfAttestingOfficer" runat="server" CssClass="form-control"
                                                ToolTip="Enter Desig.of Attest.Officer" TabIndex="18"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks</label>
                                            </div>
                                            <asp:TextBox ID="txtReMarks" runat="server" CssClass="form-control" ToolTip="Enter Remarks" MaxLength="120"
                                                 TextMode="MultiLine" TabIndex="19"></asp:TextBox>
                                            <%--onkeypress="return CheckAlphabet(event,this);"--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Address Of Organization Worked :</label>
                                            </div>
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" ToolTip="Enter Address of organization worked"
                                                MaxLength="500" TextMode="MultiLine" TabIndex="20"></asp:TextBox>
                                        </div>




                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Upload Document</label>
                                            </div>
                                            <asp:FileUpload ID="flupld" runat="server" ToolTip="Click here to Upload Document" TabIndex="21" />
                                            <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                        </div>


                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>University Approval Status :</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="true" TabIndex="22"
                                                        RepeatDirection="Horizontal" ToolTip="Select University Approval Status Yes/NO" OnSelectedIndexChanged="rdbStatus_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Text="Yes" Value="0"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Selected="True" Text="No" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <%--<div class="form-group col-lg-3 col-md-6 col-12" id="appstatus" runat="server" visible="false">--%>
                                                <%-- <div class="row">--%>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divapp" runat="server" visible="false">
                                                    <div class="label-dynamic"></div>
                                                    <label>University Approval no :</label>
                                                    <asp:TextBox ID="txtApprovalno" runat="server" CssClass="form-control" TabIndex="23" ToolTip="University Approval no "
                                                        onkeyup="validateNumeric(this);" MaxLength="10"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divdate" runat="server" visible="false">
                                                    <div class="label-dynamic"></div>
                                                    <label>Date :</label>

                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="idDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="24" 
                                                            ToolTip="Enter Date" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDate" PopupButtonID="idDate" Enabled="true" EnableViewState="true"
                                                            PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>

                                                        <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtDate"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="meeDate"
                                                            ControlToValidate="txtDate" InvalidValueMessage="Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                            Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divdoc" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>Upload Document :</label>
                                                    </div>
                                                    <asp:FileUpload ID="flupuniv" runat="server" ToolTip="Click here to Upload Document" TabIndex="25" />
                                                    <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </div>
                                        </div>--%>

                                        <%--                                        <div class="col-12">
                                            <div class="row">--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>PG Teacher Status :</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbTeacher" runat="server" AutoPostBack="true" TabIndex="26"
                                                RepeatDirection="Horizontal" ToolTip="Select PG Teacher Status Yes/NO" OnSelectedIndexChanged="rdbTeacher_SelectedIndexChanged">
                                                <asp:ListItem Enabled="true" Text="Yes" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Selected="True" Text="No" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>

                                        <%--<div class="form-group col-lg-6 col-md-6 col-12" id="divpgteacher" runat="server" visible="false">--%>
                                        <%--<div class="row">--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divpg" runat="server" visible="false">
                                            <div class="label-dynamic"></div>
                                            <label>Approval no :</label>
                                            <asp:TextBox ID="txtteachno" runat="server" CssClass="form-control" TabIndex="27" ToolTip="Enter Approval Number"
                                                onkeyup="validateNumeric(this);" MaxLength="10"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divpgdt" runat="server" visible="false">
                                            <div class="label-dynamic"></div>
                                            <label>Date :</label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgappdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtappdt" runat="server" CssClass="form-control" TabIndex="28" AutoPostBack="true"
                                                    ToolTip="Enter Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cedt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtappdt" PopupButtonID="imgappdt" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" TargetControlID="txtappdt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevdt" runat="server" ControlExtender="meeDate"
                                                    ControlToValidate="txtappdt" InvalidValueMessage="Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>
                                        <%-- </div>--%>

                                        <div class="form-group col-md-3 col-12" id="divpgdoc" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Upload Document :</label>
                                            </div>
                                            <asp:FileUpload ID="flupteach" runat="server" ToolTip="Click here to Upload Document" TabIndex="29" />
                                            <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                        </div>
                                        <%--</div>--%>
                                    </div>

                                    <%--</div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                                        </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="30"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="31"
                                            OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <%--</asp:Panel>--%>
                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvPrvService" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Previous Experience Of Employee"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Previous Quali.Service Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>From Date
                                                    </th>
                                                    <th>To Date
                                                    </th>
                                                    <th>Institution
                                                    </th>
                                                    <th>Reason
                                                    </th>
                                                    <%--<th>Attest.officer
                                                    </th>--%>
                                                    <th id="divFolder" runat="server">Attachment
                                                    </th>
                                                    <th id="divBlob" runat="server">Attachment
                                                    </th>
                                                    <th id="divFolder1" runat="server">University Attachment
                                                    </th>
                                                    <th id="divBlob1" runat="server">University Attachment
                                                    </th>
                                                    <th id="divFolder2" runat="server">PG Attachment
                                                    </th>
                                                    <th id="divBlob2" runat="server">PG Attachment
                                                    </th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("psno")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("psno") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                    OnClientClick="showConfirmDel(this); return false;" />
                                            </td>
                                            <td>
                                                <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("inst")%>
                                            </td>
                                            <td>
                                                <%# Eval("termination")%>
                                            </td>
                                            <%--<td>
                                            <%# Eval("officer")%>
                                        </td>--%>
                                            <td id="tdFolder" runat="server">
                                                <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                            </td>

                                            <td style="text-align: center" id="tdBlob" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("ATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("ATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("ATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </td>

                                            <td id="tdFolder1" runat="server">
                                                <asp:HyperLink ID="lnkDownload1" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("UNIVERSITYATACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("UNIVERSITYATACHMENT")%></asp:HyperLink>
                                            </td>
                                            <td style="text-align: center" id="tdBlob1" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview1" runat="server" OnClick="imgbtnPreview1_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("UNIVERSITYATACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("UNIVERSITYATACHMENT") %>' Visible='<%# Convert.ToString(Eval("UNIVERSITYATACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </td>

                                            <td id="tdFolder2" runat="server">
                                                <asp:HyperLink ID="lnkDownload2" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("PGTATTACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("PGTATTACHMENT")%></asp:HyperLink>
                                            </td>

                                            <td style="text-align: center" id="tdBlob2" runat="server" visible="false">
                                                    <asp:UpdatePanel ID="updPreview2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPreview2" runat="server" OnClick="imgbtnPreview2_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("PGTATTACHMENT") %>'
                                                                data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("PGTATTACHMENT") %>' Visible='<%# Convert.ToString(Eval("PGTATTACHMENT"))==string.Empty?false:true %>'></asp:ImageButton>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

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

            <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
            <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
                OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
                BackgroundCssClass="modalBackground" />
            <div class="col-md-12">
                <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                    <div class="text-center">
                        <div class="modal-content">
                            <div class="modal-body">
                                <asp:Image ID="imgWarning" runat="server" ImageUrl="~/Images/warning.png" />
                                <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                                <div class="text-center">
                                    <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                                    <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
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
                    if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                        obj.style.backgroundColor = "White";
                        return true;

                    }
                    else {
                        alert('Please Enter Alphabets Only!');
                        obj.focus();
                    }
                    return false;
                }
                function CalDuration() {

                    var datejoing = document.getElementById('<%=txtFromDate.ClientID%>').value;
                    var dateleaving = document.getElementById('<%=txtToDate.ClientID%>').value;
                    if (datejoing != '' && dateleaving != '') {

                        var dateElements = datejoing.split("/");
                        var outputDateString = dateElements[2] + "/" + dateElements[1] + "/" + dateElements[0];
                        var dateElementsnew = dateleaving.split("/");
                        var outputDateStringnew = dateElementsnew[2] + "/" + dateElementsnew[1] + "/" + dateElementsnew[0];

                        var date1 = new Date(outputDateString);
                        var date2 = new Date(outputDateStringnew);

                        if (Object.prototype.toString.call(date2) === "[object Date]") {
                            // it is a date
                            if (isNaN(date2.getTime())) {  // d.valueOf() could also work
                                document.getElementById('<%=txtExperience.ClientID%>').value = '';
                            } else {
                                // date is valid
                            }
                        } else {
                            // not a date
                        }

                        if (Object.prototype.toString.call(date1) === "[object Date]") {
                            // it is a date
                            if (isNaN(date1.getTime())) {  // d.valueOf() could also work
                                document.getElementById('<%=txtExperience.ClientID%>').value = '';
                                return;
                            } else {
                                // date is valid
                            }
                        } else {
                            // not a date
                        }


                        if (date1 > date2) {
                            alert("To date should be greater than equal to from date.");
                            document.getElementById('<%=txtExperience.ClientID%>').value = '';
                             document.getElementById('<%=txtToDate.ClientID%>').value = '';
                             return;
                         }
                         else if (date1 > new Date() || date2 > new Date()) {
                             alert("Future date should not be accepted.");
                             document.getElementById('<%=txtExperience.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    return;
                }
            dateDiff(date1, date2);
                        //var timeDiff = Math.abs(parseInt(date1.getTime()) - parseInt(date2.getTime()));
                        //var diffDays = Math.round(timeDiff / (1000 * 60 * 60 * 24));

                        //var totalYears = Math.trunc(diffDays / 365);
                        //var totalMonths = Math.trunc((diffDays % 365) / 30);
                        //var totalDays = Math.trunc((diffDays % 365) % 30)
                        //document.getElementById('<%=txtExperience.ClientID%>').value = totalYears + ' ' + 'Years' + ' ' + totalMonths + ' ' + 'Months' + ' ' + totalDays + ' ' + 'Days';
                    }
                    else
                        document.getElementById('<%=txtExperience.ClientID%>').value = '';
                }

                function dateDiff(startingDate, endingDate) {
                    var startDate = new Date(new Date(startingDate).toISOString().substr(0, 10));
                    if (!endingDate) {
                        endingDate = new Date().toISOString().substr(0, 10);    // need date in YYYY-MM-DD format
                    }
                    var endDate = new Date(endingDate);
                    if (startDate > endDate) {
                        var swap = startDate;
                        startDate = endDate;
                        endDate = swap;
                    }
                    var startYear = startDate.getFullYear();
                    var february = (startYear % 4 === 0 && startYear % 100 !== 0) || startYear % 400 === 0 ? 29 : 28;
                    var daysInMonth = [31, february, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

                    var yearDiff = endDate.getFullYear() - startYear;
                    var monthDiff = endDate.getMonth() - startDate.getMonth();
                    if (monthDiff < 0) {
                        yearDiff--;
                        monthDiff += 12;
                    }
                    var dayDiff = endDate.getDate() - startDate.getDate();
                    if (dayDiff < 0) {
                        if (monthDiff > 0) {
                            monthDiff--;
                        } else {
                            yearDiff--;
                            monthDiff = 11;
                        }
                        dayDiff += daysInMonth[startDate.getMonth()];
                    }
                    document.getElementById('<%=txtExperience.ClientID%>').value = yearDiff + ' ' + 'Years' + ' ' + monthDiff + ' ' + 'Months' + ' ' + dayDiff + ' ' + 'Days';
                    return yearDiff + 'Y ' + monthDiff + 'M ' + dayDiff + 'D';
                }

                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    console.log(charCode);
                    if (charCode != 46 && charCode != 45 && charCode > 31
                    && (charCode < 48 || charCode > 57)) {
                        alert("Only Hiphen(-) And Numeric Characters Should Accepted ");
                        return false;
                    }
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
            </script>


        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnUpload" />--%>

            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

