<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="complaint.aspx.cs" Inherits="Estt_complaint"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
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
    </div>

    <style type="text/css">
        #blur {
            width: 100%;
            background-color: black;
            moz-opacity: 0.5;
            khtml-opacity: .5;
            opacity: .5;
            filter: alpha(opacity=50);
            z-index: 120;
            height: 100%;
            position: absolute;
            top: 0;
            left: 0;
        }

        #progress {
            z-index: 200;
            background-color: White;
            position: absolute;
            top: 0pt;
            left: 0pt;
            border: solid 1px black;
            padding: 5px 5px 5px 5px;
            text-align: center;
        }
    </style>

    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CREATE SERVICE REQUEST</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Request Date</label>
                                                <asp:Label ID="lblsample" runat="server"></asp:Label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtComplaintDate" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                <%-- <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtComplaintDate" Enabled="true" EnableViewState="true" PopupButtonID="imgCal1">
                                                </ajaxToolKit:CalendarExtender>
                                                
                                                 <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtComplaintDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>--%>
                                                <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                    ControlToValidate="txtComplaintDate" Display="None"
                                                    ErrorMessage="Please Enter Complaint Date" SetFocusOnError="true"
                                                    ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                    EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1"
                                                    TargetControlID="txtComplaintDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                    Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    TargetControlID="txtComplaintDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server"
                                                    ControlExtender="meeFromdt" ControlToValidate="txtComplaintDate" Display="None"
                                                    EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Complaint Date"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    SetFocusOnError="true" TooltipMessage="Please Enter Complaint Date"
                                                    ValidationGroup="complaint">
                                                &#160;&#160;
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Name of Service Request</label>
                                            </div>
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="35" ReadOnly="true" />
                                            <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                                ControlToValidate="txtName" Display="None" ErrorMessage="Please Enter Name of Complaintee"
                                                ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRMDept" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlRMDept_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRMDept" runat="server" ControlToValidate="ddlRMDept" Display="None" ErrorMessage="Please Select Service Department."
                                                ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Request No.</label>
                                            </div>
                                            <asp:TextBox ID="txtComplaintNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCompNo" runat="server" ControlToValidate="txtComplaintNo" Display="None" ErrorMessage="Service Request No. Is Required."
                                                ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Request To</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRMCompTo" AppendDataBoundItems="true" runat="server" TabIndex="2"
                                                CssClass="form-control" data-select2-enable="true" Enabled="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvRMV" runat="server" ControlToValidate="ddlRMCompTo" Display="None" ErrorMessage="Please Assign Admin for selected Department."
                                                ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Request Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCompNature" AppendDataBoundItems="true" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCompNature_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCompNature" runat="server" ControlToValidate="ddlCompNature" Display="None" ErrorMessage="Please Select Service Request Nature Type."
                                                ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Service Request Details </label>
                                            </div>
                                            <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control" TabIndex="4" TextMode="MultiLine" MaxLength="400" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDetails" Display="None" ErrorMessage="Please Enter service Request Details."
                                                ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeCompDetail" runat="server" TargetControlID="txtDetails" FilterType="Custom,Numbers,UppercaseLetters, LowercaseLetters" ValidChars="-!.,() ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12" id="stuInfo" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Roll No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblRollNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Mobile No. :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblMobileNo" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Email ID :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEmailID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Location </label>
                                            </div>
                                            <asp:DropDownList ID="ddlArea" Enabled="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvArea" runat="server"
                                                ControlToValidate="ddlArea" Display="None" ErrorMessage="Please Select Location."
                                                ValidationGroup="complaint" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Location/Site of Service </label>
                                            </div>
                                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" MaxLength="150" TabIndex="6" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtLocation" Display="None" ErrorMessage="Please Enter Location of Service Request."
                                                ValidationGroup="complaint"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftebLocation" runat="server" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters"
                                                TargetControlID="txtLocation" ValidChars="/\-  ">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Contact Number </label>
                                            </div>
                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" MaxLength="10" TabIndex="7" onkeypress="return CheckNumeric(event,this);" />
                                            <asp:RequiredFieldValidator ID="rfvtxtphonenumber" runat="server" ControlToValidate="txtContactNo"
                                                ErrorMessage="Please Enter Contact Number." Display="None" ValidationGroup="complaint" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftxNumber" runat="server" FilterType="Numbers" TargetControlID="txtContactNo">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Alternate Number </label>
                                            </div>
                                            <asp:TextBox ID="txtContactNoOther" runat="server" CssClass="form-control" MaxLength="10" TabIndex="8" onkeypress="return CheckNumeric(event,this);" />
                                            <%-- <asp:RequiredFieldValidator ID="rfvContactNoOther" runat="server" ControlToValidate="txtContactNoOther"
                                                ErrorMessage="Please Enter Other Contact Number." Display="None" ValidationGroup="complaint" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeOContNo" runat="server" FilterType="Numbers" TargetControlID="txtContactNoOther">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-md-12 col-lg-9 col-12" id="divPre" runat="server" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-4 col-md-4 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Preferable Date for Visit/Contact </label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="ImaCalStartDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtEnterDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceLasteDateofReciptTime" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtEnterDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meReqdate" runat="server" TargetControlID="txtEnterDate"
                                                            Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevIndDate" runat="server" ControlExtender="meReqdate"
                                                            ControlToValidate="txtEnterDate" EmptyValueMessage="Please Enter Preferable date"
                                                            InvalidValueMessage="Preferable date is Invalid (Enter dd/MM/yyyy Format)"
                                                            Display="None" TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="complaint"
                                                            SetFocusOnError="True" ErrorMessage="mevIndDate" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-4 col-md-4 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Preferable Time From </label>
                                                    </div>
                                                    <asp:TextBox ID="txtEnterTime" runat="server" CssClass="form-control" />
                                                    <span style="font-style: italic; font-size: smaller"></span>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeEnterTime" runat="server" TargetControlID="txtEnterTime"
                                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="True"
                                                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevEnterTime" runat="server" ControlExtender="meeEnterTime"
                                                        ControlToValidate="txtEnterTime" IsValidEmpty="False" EmptyValueMessage="Please Enter Preferable Time."
                                                        InvalidValueMessage="Time is invalid" Display="None" TooltipMessage="Input a time"
                                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="complaint" ErrorMessage="mevEnterTime" />
                                                </div>
                                                <div class="form-group col-lg-4 col-md-4 col-12">
                                                    <div class="label-dynamic">
                                                        <label>To </label>
                                                    </div>
                                                    <asp:TextBox ID="txtEnterTimeTo" runat="server" CssClass="form-control" />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeEnterTimeTo" runat="server" TargetControlID="txtEnterTimeTo"
                                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="True"
                                                        ErrorTooltipEnabled="True" CultureAMPMPlaceholder=""
                                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevEnterTimeTo" runat="server" ControlExtender="meeEnterTimeTo"
                                                        ControlToValidate="txtEnterTimeTo" IsValidEmpty="False" EmptyValueMessage="Please Enter Preferable Time."
                                                        InvalidValueMessage="Time is invalid" Display="None" TooltipMessage="Input a time"
                                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="complaint" ErrorMessage="mevEnterTimeTo" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOTP" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>OTP </label>
                                            </div>
                                            <asp:TextBox ID="txtOPT" runat="server" MaxLength="10" placeholder="Enter OTP" ToolTip="Enter OTP"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeOTP" runat="server"
                                                TargetControlID="txtOPT"
                                                FilterType="Numbers,LowerCaseLetters,UpperCaseLetters">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                            <%--<asp:RequiredFieldValidator ID="rfvOPT" runat="server" ControlToValidate="txtOPT"
                                                Display="None" ErrorMessage="Please Enter OTP" ValidationGroup="complaint"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                            <asp:Label ID="lblOTP" runat="server" Visible="false" />
                                        </div>

                                        <div class="col-md-12 col-lg-6 col-12" id="trOTP" runat="server" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <asp:DropDownList ID="ddlLength" runat="server">
                                                        <asp:ListItem Text="5" Value="5" />
                                                        <asp:ListItem Text="8" Value="8" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Alphanumeric" Value="1" Selected="True" />
                                                        <asp:ListItem Text="Numeric" Value="2" />
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <label>
                                                <span style="color: #FF0000">Valid files : (.jpg, .bmp, .gif, .png, .pdf, .xls, .doc,.zip, .txt, .docx, .xlsx should be of 100 kb size.)</span>
                                            </label>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Attach File</label>
                                                <%--<sup> Format allowed (jpg,.pdf,.xls,.doc,.txt,.zip) of size 100 Kb.</sup>--%>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" ValidationGroup="complaint" ToolTip="Select file to upload" TabIndex="9" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                        ValidationGroup="complaint" CssClass="btn btn-primary"
                                                        CausesValidation="False" TabIndex="10" />
                                                    <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <div class="overlay">
                                                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                            <img alt="" src="loader.gif" />
                                                        </div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlFile" runat="server" Visible="false">
                                        <asp:ListView ID="lvfile" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <div class="sub-heading">
                                                        <h5>Download files</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th>File Name
                                                                </th>
                                                                <th>Service Request No
                                                                </th>
                                                                <th>Download
                                                                </th>
                                                                <%--<th>Creation Date 
                                                                </th>--%>
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
                                                        <asp:ImageButton ID="btnAttachDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#DataBinder.Eval(Container, "DataItem") %>'
                                                            AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnAttachDelete_Click"
                                                            OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" />
                                                    </td>
                                                    <td>
                                                        <%#GetFileName(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <%#GetFileNameCaseNo(DataBinder.Eval(Container, "DataItem")) %>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="imgFileDownload" runat="Server" ImageUrl="~/images/action_down.gif"
                                                                    AlternateText='<%#DataBinder.Eval(Container, "DataItem") %>' ToolTip='<%#DataBinder.Eval(Container, "DataItem")%>'
                                                                    OnClick="imgFileDownload_Click" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="imgFileDownload" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <%--<td>
                                                        <%#GetFileDate(DataBinder.Eval(Container, "DataItem"))%>
                                                    </td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-md-12 col-lg-9 col-12" id="divOpen" runat="server" visible="false">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status :</label>
                                                        <asp:RadioButton ID="rdStatus" runat="server" GroupName="rdStatus" Checked="false" Text="Reopen" TabIndex="9" />

                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Remark </label>
                                                    </div>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="50" TabIndex="10" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnGenerateOTP" runat="server" CssClass="btn btn-primary" Text="Generate OTP" ToolTip="Click here to Generate OTP" OnClick="btnGenerateOTP_Click" Visible="false" />
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="complaint" OnClick="btnSave_Click" CssClass="btn btn-primary" TabIndex="11" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="12" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="complaint" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>


                                <div class="col-12">
                                    <asp:ListView ID="lvComplaint" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading">
                                                    <h5>Service Entry List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>EDIT / DELETE</th>
                                                            <th>SERVICE REQUEST NO.                                                                                                                                            
                                                            </th>
                                                            <%-- & DATE   --%>
                                                            <th>DATE
                                                            </th>
                                                            <%--<th style="width: 25%">SERVICE REQUEST ID</th>--%>
                                                            <%--<th style="width: 25%">SERVICE REQUEST DATE</th>--%>
                                                            <%--<th>SERVICE REQUEST DETAILS</th>--%> <%-- by sonal--%>
                                                            <%--<th style="width: 15%;">COMPLAINER NAME</th>--%>
                                                            <th>LOCATION /           
                                                                                    SITE
                                                            </th>
                                                            <%--<th style="width: 10%">SITE</th>--%>
                                                            <th>SERVICE DEPARTMENT</th>
                                                            <%--<th>ACTION TAKEN</th>--%>
                                                            <th>STATUS</th>
                                                            <%--<th style="width: 5%">REPORT</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit1.gif" CommandArgument='<%# Eval("COMPLAINTID")%>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />

                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("COMPLAINTID")%>'
                                                        AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                        OnClientClick="return DeleteItem()" />
                                                </td>
                                                <td><%# Eval("COMPLAINTNO")%></td>
                                                <td><%# Eval("COMPLAINTDATE", "{0:d}")%></td>
                                                <%-- <td style="width: 25%"><%# Eval("COMPLAINTID")%></td>--%>
                                                <%-- <td style="width: 25%"><%# Eval("COMPLAINTDATE", "{0:d}")%></td>--%>
                                                <%-- <td><%# Eval("COMPLAINT")%></td>--%>
                                                <%--<td style="width: 15%;"><%# Eval("COMPLAINTEE_NAME")%></td>--%><%--showConfirmDel(this); return false;--%>
                                                <td><%# Eval("AREANAME")%>
                                                    <%# Eval("COMPLAINTEE_ADDRESS")%>
                                                </td>
                                                <%--<td style="width: 10%"><%# Eval("COMPLAINTEE_ADDRESS")%></td> --%>
                                                <td><%# Eval("DEPTNAME")%></td>
                                                <%-- <td><%# Eval("WORKOUT")%></td>--%>
                                                <td><%# Eval("COMPLAINTSTATUS")%></td>
                                                <td id="tdPrint" runat="server" visible="false">
                                                    <asp:Button ID="btnPrint" runat="server" CausesValidation="false" CommandName="Print"
                                                        Text="Print" CommandArgument='<%# Eval("COMPLAINTID") %>' OnClick="btnPrint_Click" CssClass="btn btn-primary" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                                <div class="vista-grid_datapager d-none">
                                    <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvComplaint" PageSize="10"
                                        OnPreRender="dpPager_PreRender">
                                        <Fields>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                            <asp:NumericPagerField ButtonCount="10" ButtonType="Link" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowPreviousPageButton="false" ShowNextPageButton="true" ShowLastPageButton="false" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>




    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />

    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <div>
                <div>
                    <div class="text-center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                    </div>
                    <div>
                        &nbsp;&nbsp;Are you sure you want to delete this record?
                    </div>
                </div>
                <div>
                    <div class="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn btn-primary" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn btn-danger" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

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

    <script type="text/javascript">
        function DeleteItem() {
            if (confirm("Are you sure you want to delete ...?")) {
                return true;
            }
            return false;
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
    </script>



    <%--END MODAL POPUP EXTENDER FOR DELETE CONFIRMATION --%>
</asp:Content>
