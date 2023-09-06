<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Membermaster.aspx.cs" Inherits="MEETING_MANAGEMENT_MASTERS_Membermaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" type="text/javascript"></script>--%>

    <%-- <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MEMBER DETAILS</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdblistMemberType" runat="server" TabIndex="1" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rdblistMemberType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="1">Employee &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Student</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <%--   <div class="form-group col-lg-4 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>If Member belongs to Institute then Select that faculty from above list. </span></p>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>

                            <asp:Panel ID="pnlSelectMember" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Member</h5>
                                    </div>
                                </div>
                                <div class="form-group col-lg-5 col-md-12 col-12 mt-2 mb-2">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>If Member belongs to Institute then Select that faculty from above list. </span></p>
                                    </div>
                                </div>

                                <div class="col-12" id="divStudPanel" runat="server" visible="false">
                                    <div class="row">

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegree" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="1" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                ErrorMessage="Select atleast one Degree !" InitialValue="0" Display="None" ValidationGroup="SHOW"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                ErrorMessage="Select atleast one Branch !" Display="None" InitialValue="0" ValidationGroup="SHOW"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsemester"
                                                ErrorMessage="Select atleast one Semester !" Display="None" InitialValue="0" ValidationGroup="SHOW"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer" id="divShow" runat="server" visible="false">

                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ToolTip="Click here to Show" OnClick="btnShow_Click" CausesValidation="true"  ValidationGroup="SHOW" />
                                      <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SHOW" /> <%--SHAIKH JUNED (30/03/2022)--%>

                                </div>
                                <div class="col-12" id="divEmpPanel" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Select Member </label>
                                            </div>
                                            <asp:DropDownList ID="ddlMember" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ToolTip="Select Member" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged"></asp:DropDownList>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>

                                            <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/Images/nophoto.jpg" Height="80px" Width="80px" ToolTip="Image" />

                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>

                            <div class="col-12">
                                <asp:Panel ID="pnlMemberDetails" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Member Details</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Title</label>
                                            </div>
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" ToolTip="Enter Title" onkeypress="return CheckAlphabet(event,this);"
                                                MaxLength="5" TabIndex="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" Display="None" ErrorMessage="Please Enter Title" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>First Name </label>
                                            </div>
                                            <asp:TextBox ID="txtFName" runat="server" CssClass="form-control" ToolTip="Enter First Name" onkeypress="return CheckAlphabet(event,this);"
                                                MaxLength="60" TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="rfvFName" runat="server" ControlToValidate="txtFName"
                                                Display="None" ErrorMessage="Please Enter First Name" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Middle Name</label>
                                            </div>
                                            <asp:TextBox ID="txtMName" runat="server" CssClass="form-control" ToolTip="Enter Middle Name" onkeypress="return CheckAlphabet(event,this);" TabIndex="5" MaxLength="60" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Last Name </label>
                                            </div>
                                            <asp:TextBox ID="txtLName" runat="server" CssClass="form-control" ToolTip="Enter Last Name" onkeypress="return CheckAlphabet(event,this);" TabIndex="6" MaxLength="60" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvLName" runat="server" ControlToValidate="txtLName"
                                                        Display="None" ErrorMessage="Please Enter Last Name" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Date Of Birth </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgbirthday" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDOB" runat="server" TabIndex="7" CssClass="form-control" ToolTip="Select Date Of Birth"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDOB" PopupButtonID="imgbirthday" Enabled="True">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server" TargetControlID="txtDOB"
                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                    CultureTimePlaceholder="" Enabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="msedatebirth"
                                                    ControlToValidate="txtDOB" EmptyValueMessage="Please Enter DOB." IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid DOB  In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid DOB In [dd/MM/yyyy] format"
                                                    Display="None" Text="*"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Occupation</label>
                                            </div>
                                            <asp:TextBox ID="txtProfession" runat="server" CssClass="form-control" ToolTip="Enter Occupation" TabIndex="8" MaxLength="60" />
                                            <%--<asp:RequiredFieldValidator ID="rfvProf" runat="server" ControlToValidate="txtProfession" Display="None" ErrorMessage="Please Enter profession." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeProfession" runat="server" TargetControlID="txtProfession" FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=" "></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trConstituency" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Constituency</label>
                                            </div>
                                            <asp:TextBox ID="txtConstituency" runat="server" CssClass="form-control" ToolTip="Enter Constituency" MaxLength="40" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trAdharNo" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Adhar No.</label>
                                            </div>
                                            <asp:TextBox ID="txtAdharNo" runat="server" CssClass="form-control" ToolTip="Enter Adhar No."></asp:TextBox>

                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlOtherDetails" runat="server">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Other Details</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Academic Qualification</label>
                                            </div>
                                            <asp:TextBox ID="txtAcadQuali" runat="server" CssClass="form-control" ToolTip="Enter Academic Qualification" TabIndex="9" MaxLength="60" />
                                            <asp:RequiredFieldValidator ID="rfvAcadQuali" runat="server" ControlToValidate="txtAcadQuali" Display="None" ErrorMessage="Please Enter Academic Qualification." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAcadQuali" runat="server" TargetControlID="txtAcadQuali" FilterType="Custom, UppercaseLetters, LowercaseLetters" ValidChars=". "></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Member (From Date)</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="img3" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDt" runat="server" CssClass="form-control" ToolTip="Select From Date" TabIndex="10" ValidationGroup="SUBMIT"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="img3" TargetControlID="txtFrmDt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFrmDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                    ControlToValidate="txtFrmDt" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid From Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" Display="None"
                                                    Text="*" ValidationGroup="SUBMIT"></ajaxToolKit:MaskedEditValidator>
                                            </div>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Member(To Date) </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDt" runat="server" CssClass="form-control" ToolTip="Enter To Date" TabIndex="11" ValidationGroup="SUBMIT"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtToDt">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDt" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtToDt" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                    ErrorMessage="Please Enter Valid To Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid To Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="SUBMIT"></ajaxToolKit:MaskedEditValidator>
                                                <asp:CompareValidator ID="CompareValidator1"
                                                    ControlToCompare="txtFrmDt" ControlToValidate="txtToDt" ErrorMessage="To date should be greater than from date" runat="server"
                                                    Operator="GreaterThan" SetFocusOnError="True" Type="Date" ValidationGroup="SUBMIT" Display="None"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>PAN Number </label>
                                            </div>
                                            <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control" ToolTip="Enter PAN Number" TabIndex="12" MaxLength="10" Style="text-transform: uppercase;" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvPAN" runat="server" ControlToValidate="txtPAN" Display="None" ErrorMessage="Please Enter PAN Number." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePAN" runat="server" TargetControlID="txtPAN" 
                                                ValidChars="0123456789" FilterType="Custom, UppercaseLetters, LowercaseLetters, Numbers"></ajaxToolKit:FilteredTextBoxExtender>

                                            <%--  <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPinPerm"
                                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Representation</label>
                                            </div>
                                            <asp:TextBox ID="txtRepresentation" runat="server" CssClass="form-control" ToolTip="Enter Representation" onkeypress="return CheckAlphabet(event,this);"
                                                TabIndex="13" MaxLength="40" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Designation</label>
                                            </div>
                                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ToolTip="Enter Designation" onkeypress="return CheckAlphabet(event,this);"
                                                TabIndex="14" MaxLength="40" />
                                              <asp:RequiredFieldValidator ID="rfvPAN" runat="server" ControlToValidate="txtDesignation" Display="None" ErrorMessage="Please Enter Designation." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Joining Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgJD" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtJoiningDate" runat="server" TabIndex="15" CssClass="form-control" ToolTip="Enter Joining Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtJoiningDate" PopupButtonID="imgJD" Enabled="True">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtJoiningDate"
                                                    Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    Enabled="True" />
                                          <%--      <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                        ControlToValidate="txtJoiningDate" EmptyValueMessage="Please Enter Joining Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid Joining Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Joining Date In [dd/MM/yyyy] format"
                                                        Display="None" Text="*" ValidationGroup="SUBMIT"></ajaxToolKit:MaskedEditValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="pnlOfficialAddress" runat="server">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Address Details</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Official Address</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanAddress" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Address Line 1 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddOfficial" runat="server" CssClass="form-control" ToolTip="Enter Address Line1" TabIndex="16" MaxLength="200"
                                                onkeypress="return CheckAlphaNumeric(event,this);" />
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddOfficial"
                                                Display="None" ErrorMessage="Please Enter Address Line 1" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Address Line 2 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddLineOfficial" runat="server" CssClass="form-control" ToolTip="Enter Address Line2" TabIndex="17" MaxLength="200" onkeypress="return CheckAlphaNumeric(event,this);" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>City</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCityTemp" runat="server" TabIndex="18" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select City"></asp:DropDownList>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCityTemp"
                                                Display="None" ErrorMessage="Please Select City" InitialValue="0" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>State</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStateTemp" runat="server" TabIndex="19" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select State"></asp:DropDownList>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStateTemp"
                                                Display="None" ErrorMessage="Please Select State" InitialValue="0" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>PinCode </label>
                                            </div>
                                            <asp:TextBox ID="txtPinOffci" runat="server" CssClass="form-control" ToolTip="Enter Pin Code" onkeypress="return CheckNumeric(event,this);" MaxLength="6" TabIndex="20" />
                                          <%--  <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txtPinOffci"
                                                ValidationExpression="^[0-9]{6}$" ErrorMessage="Please enter a 6 digit Pin number!"
                                                Display="None" ValidationGroup="SUBMIT" />--%>
                                             <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCatagoryShort" runat="server" TargetControlID="txtPinOffci"
                                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPinOffci"
                                                Display="None" ErrorMessage="Please Enter Pincode" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Country</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCountryTemp" runat="server" AppendDataBoundItems="true" TabIndex="21" CssClass="form-control" ToolTip="Select Country" data-select2-enable="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCountryTemp"
                                                Display="None" ErrorMessage="Please Select Country" InitialValue="0" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Land Line No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPhoneOffc" runat="server" CssClass="form-control" ToolTip="Enter Land Line No." TabIndex="22" MaxLength="12" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeOPhone" runat="server" FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtPhoneOffc"></ajaxToolKit:FilteredTextBoxExtender>


                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Mobile No. </label>
                                            </div>
                                            <asp:TextBox ID="txtMobileTemp" runat="server" CssClass="form-control" ToolTip="Enter Mobile No." TabIndex="23" MaxLength="10" />
                                            <asp:RequiredFieldValidator ID="rfvMobile" runat="server" ControlToValidate="txtMobileTemp"
                                                Display="None" ErrorMessage="Please Enter Mobile No." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMobileTemp" runat="server" FilterType="Numbers" TargetControlID="txtMobileTemp"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Email ID </label>
                                            </div>
                                            <asp:TextBox ID="txtoffEmail" runat="server" CssClass="form-control" ToolTip="Enter Email ID" TabIndex="24" MaxLength="200" />
                                            <asp:RequiredFieldValidator ID="rfvOEmail" runat="server" ControlToValidate="txtoffEmail"
                                                Display="None" ErrorMessage="Please Enter Email ID" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revOEmail" runat="server" ErrorMessage="Please Enter Valid Official Email ID" Display="None"
                                                ValidationGroup="SUBMIT" ControlToValidate="txtoffEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>
                                              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtoffEmail"
                                                Display="None" ErrorMessage="Please Enter Email ID." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Permanent Address</h5>
                                            </div>
                                            <asp:CheckBox ID="Chksame" runat="server" AutoPostBack="True" Text="Same as Official" OnCheckedChanged="Chksame_CheckedChanged" TabIndex="25" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Address Line 1 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddPerm" runat="server" CssClass="form-control" ToolTip="Enter Address Line1" MaxLength="200" TabIndex="26" onkeypress="return CheckAlphaNumeric(event,this);" />
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAddPerm"
                                                Display="None" ErrorMessage="Please Enter Address Line 1." ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Address Line 2 </label>
                                            </div>
                                            <asp:TextBox ID="txtAddLinePerm" runat="server" CssClass="form-control" ToolTip="Enter Address Line2" MaxLength="200" TabIndex="27" onkeypress="return CheckAlphaNumeric(event,this);" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>City</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCityPerm" runat="server" TabIndex="28" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select City"></asp:DropDownList>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlCityPerm"
                                                Display="None" ErrorMessage="Please Select City" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>State</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStatePerm" runat="server" TabIndex="29" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ToolTip="Select State"></asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlStatePerm"
                                                Display="None" ErrorMessage="Please Select State" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>PinCode</label>
                                            </div>
                                            <asp:TextBox ID="txtPinPerm" runat="server" CssClass="form-control" ToolTip="Enter Pin Code" TabIndex="30" onkeypress="return CheckNumeric(event,this);"
                                                MaxLength="6" />
                                             <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPinPerm"
                                                                        ValidChars="0123456789" FilterMode="ValidChars">
                                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                                <%--  <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtPinPerm"
                                                ValidationExpression="^[0-9]{6}$" ErrorMessage="Please enter a 6 digit Pin number!"
                                                Display="None" ValidationGroup="SUBMIT" />--%>
                                   <%--             <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPinPerm"
                                                Display="None" ErrorMessage="Please Enter Pincode" ValidationGroup="SUBMIT"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Country</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCountryPerm" runat="server" AppendDataBoundItems="true" TabIndex="31" CssClass="form-control" data-select2-enable="true" ToolTip="Select Country"></asp:DropDownList>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Land Line No.</label>
                                            </div>
                                            <asp:TextBox ID="txtPhonePerm" runat="server" CssClass="form-control" ToolTip="Enter Land Line No." TabIndex="32" MaxLength="15" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbePPhone" runat="server" FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtPhonePerm"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Mobile No. </label>
                                            </div>
                                            <asp:TextBox ID="txtMobilePerm" runat="server" CssClass="form-control" ToolTip="Enter Mobile No." TabIndex="33" MaxLength="12" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeMobilePerm" runat="server" FilterType="Custom, Numbers" ValidChars="+- " TargetControlID="txtMobilePerm"></ajaxToolKit:FilteredTextBoxExtender>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Email ID </label>
                                            </div>
                                            <asp:TextBox ID="txtperemail" runat="server" CssClass="form-control" ToolTip="Enter Email ID" TabIndex="34" MaxLength="200" />
                                            <asp:RegularExpressionValidator ID="revPEmail" runat="server" ErrorMessage="Please Enter Valid Email ID" Display="None"
                                                ValidationGroup="SUBMIT" ControlToValidate="txtperemail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                            </asp:RegularExpressionValidator>

                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" ValidationGroup="SUBMIT" CausesValidation="true" TabIndex="35" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="36" />
                                 <asp:Button ID="back" runat="server" Text="Back" CssClass="btn btn-warning" ToolTip="Click here to Cancel" OnClick="back_Click" CausesValidation="false" TabIndex="36" Visible="false" />
                                <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SUBMIT" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvMRBills" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>LIST OF MEMBERS CREATED</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit </th>
                                                            <th>Member Id </th>
                                                            <th>Member Name </th>
                                                            <th>Member Wise Report </th>
                                                            <th>Member Type</th>
                                                            <th>User Type</th>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("PK_CMEMBER") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("PK_CMEMBER")%></td>
                                                <td><%# Eval("NAME")%>
                                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("USERID") %>' />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkMemberDetailsbtn" runat="server" CausesValidation="false" CommandArgument='<%# Eval("PK_CMEMBER") %>' OnClick="lnkMemberDetailsbtn_Click"> Member Report </asp:LinkButton>
                                                </td>
                                                <td><%# Eval("MEMBER_TYPE")%></td>
                                                <td><%# Eval("EMPSTUD_TYPE")%></td>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
