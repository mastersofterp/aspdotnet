<%@ Page Language="C#" MasterPageFile="~/ServiceBookMaster.master" AutoEventWireup="true" CodeFile="Pay_SB_Current_Appointment_Status.aspx.cs" Inherits="ESTABLISHMENT_ServiceBook_Pay_SB_Current_Appointment_Status" %>

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
                            <h3 class="box-title">Current Appointment Status </h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12" id="divdoc1">
                                    <div class="row">

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Appointment Details</h5>
                                                    </div>
                                                </div>
                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Appointment Details : </label>
                                                    </div>
                                                    <asp:TextBox ID="txtAppointment" runat="server" CssClass="form-control" TabIndex="1"
                                                        ToolTip="Enter Appointment Details " onkeypress="return CheckAlphabet(event,this);" MaxLength="120"></asp:TextBox>
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Appointment Mode : </label>
                                                    </div>
                                                    <%--  <asp:TextBox ID="txtAppMode" runat="server" CssClass="form-control" TabIndex="2"
                                                        ToolTip="Enter Appointment Details " onkeypress="return CheckAlphabet(event,this);" MaxLength="120"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlAppMode" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Select Appointment Mode" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Advertise</asp:ListItem>
                                                        <asp:ListItem Value="2">Internet </asp:ListItem>
                                                        <asp:ListItem Value="3">College Website</asp:ListItem>
                                                        <asp:ListItem Value="4">Reference</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Selection Committee Details</h5>
                                                    </div>
                                                </div>
                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Selection Committee Details : </label>
                                                    </div>
                                                    <asp:TextBox ID="txtSelectionDetail" runat="server" CssClass="form-control" TabIndex="3"
                                                        ToolTip="Enter Selection Committee Details" onkeypress="return CheckAlphabet(event,this);" MaxLength="120"></asp:TextBox>
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Selection Committee Member : </label>
                                                    </div>
                                                    <asp:TextBox ID="txtCommitteeMembers" runat="server" CssClass="form-control" TabIndex="2"
                                                        ToolTip="Enter Selection Committee Member" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Advertisement</h5>
                                                    </div>
                                                </div>

                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Advertisement :</label>
                                                    <asp:TextBox ID="txtAdvt" runat="server" CssClass="form-control" TabIndex="5"
                                                        ToolTip="Enter Advertisement Details" MaxLength="100"></asp:TextBox>
                                                </div>--%>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Newspaper Names :</label>
                                                    <asp:TextBox ID="txtNewspaper" runat="server" CssClass="form-control" TabIndex="3"
                                                        ToolTip="Enter Newspaper Name" MaxLength="100"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Date of Newspaper :</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgnews" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtNewsDt" runat="server" CssClass="form-control" TabIndex="4" AutoPostBack="true"
                                                            ToolTip="Enter Date of Newspaper" Style="z-index: 0;"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calexnew" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtNewsDt" PopupButtonID="imgnews" Enabled="true" EnableViewState="true"
                                                            PopupPosition="BottomLeft">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meeNDt" runat="server" TargetControlID="txtNewsDt"
                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                            AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="mevNDt" runat="server" ControlExtender="meeNDt"
                                                            ControlToValidate="txtNewsDt" InvalidValueMessage="Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                            Display="None" TooltipMessage="Please Enter Date of Newspaper" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Reference :</label>
                                                    <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" TabIndex="5"
                                                        ToolTip="Enter Reference Details" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <label>Name of Authority :</label>
                                                    <asp:TextBox ID="txtNameAuthority" runat="server" CssClass="form-control" TabIndex="9"
                                                        ToolTip="Enter Name of Authority" MaxLength="100" onkeypress="return CheckAlphabet(event,this);"></asp:TextBox>
                                                </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Appointment Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtApppDate" runat="server" CssClass="form-control" TabIndex="6" AutoPostBack="true"
                                                    ToolTip="Enter Appointment Date" Style="z-index: 0;"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="calexDt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtApppDate" PopupButtonID="imDt" Enabled="true" EnableViewState="true"
                                                    PopupPosition="BottomLeft">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeDt" runat="server" TargetControlID="txtApppDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevAppDt" runat="server" ControlExtender="meeDt"
                                                    ControlToValidate="txtApppDate" InvalidValueMessage="Appointment Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Appointment Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Appointment Order Number : </label>
                                            </div>
                                            <asp:TextBox ID="txtAppNo" runat="server" CssClass="form-control" TabIndex="7"
                                                ToolTip="Enter Appointment Order Number" MaxLength="20"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">

                                                <label>Designation as per Appointment :</label>
                                            </div>
                                            <asp:TextBox ID="txtPostName" runat="server" CssClass="form-control" ToolTip="Enter Post Name"
                                                onkeypress="return CheckAlphabet(event,this);" MaxLength="100" TabIndex="8"></asp:TextBox>

                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Appointment Status :</label>
                                            </div>
                                            <asp:TextBox ID="txtAppStatus" runat="server" CssClass="form-control" ToolTip="Enter Post Name"
                                                onkeypress="return CheckAlphabet(event,this);" MaxLength="100" TabIndex="9"></asp:TextBox>

                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Pay Scale :</label>
                                            <asp:TextBox ID="txtpayscale" runat="server" CssClass="form-control" TabIndex="10" onkeypress="return isNumberKey(event);"
                                                ToolTip="Enter Pay Scale" MaxLength="20"></asp:TextBox>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Department :</label>
                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" TabIndex="11" MaxLength="100"
                                                onkeypress="return CheckAlphabet(event,this);" ToolTip="Enter Department"></asp:TextBox>
                                        </div>



                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Valid Experience consider at time of Appointment</h5>
                                                    </div>
                                                </div>


                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%-- <sup>* </sup>--%>
                                                                <label>From Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="imgCal" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="12" AutoPostBack="true"
                                                                    ToolTip="Enter From Date" Style="z-index: 0;" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>

                                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtFromDate" PopupButtonID="imgCal" Enabled="true" EnableViewState="true"
                                                                    PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date"
                                                    ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
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




                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%-- <sup>* </sup>--%>
                                                                <label>To Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtToDate" runat="server" ToolTip="Enter From Date" Style="z-index: 0;" 
                                                                    CssClass="form-control" TabIndex="13" onBlur="CalDuration();" onChange="CalDuration();"></asp:TextBox>

                                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                                    PopupButtonID="Image1" Enabled="true" EnableViewState="true" PopupPosition="BottomLeft">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" ValidationGroup="ServiceBook"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>--%>
                                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate"
                                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                                    Display="None" TooltipMessage="Please Enter to Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ServiceBook" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                                                <asp:CompareValidator ID="cvToDate" runat="server" Display="None" ErrorMessage="To Date Should be Greater than or equal to From Date"
                                                                    ValidationGroup="ServiceBook" SetFocusOnError="True" ControlToCompare="txtFromDate"
                                                                    ControlToValidate="txtToDate" Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
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
                                                            <asp:TextBox ID="txtExperience" Enabled="true" runat="server" CssClass="form-control"
                                                                ToolTip="Total Experience" TabIndex="14" onkeydown="return EditControl(event,this);" onkeypress="return EditControl(event,this);"
                                                                onclick="return EditControl(event,this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtotalexperience" runat="server"
                                                                ControlToValidate="txtExperience" Display="None" ErrorMessage="Please Enter Total Experience"
                                                                ValidationGroup="ServiceBook" SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <label>Experience Type:</label>
                                            <asp:DropDownList ID="ddlExpTyp" runat="server" CssClass="form-control" TabIndex="15" ToolTip="Enter Experience Type" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Industrial</asp:ListItem>
                                                <asp:ListItem Value="2">Teaching </asp:ListItem>
                                                <asp:ListItem Value="3">Other </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>University Approval Status :</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdbStatus" runat="server" AutoPostBack="true" TabIndex="16"
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
                                                    <asp:TextBox ID="txtApprovalno" runat="server" CssClass="form-control" TabIndex="17" ToolTip="University Approval no "
                                                        MaxLength="15"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divdate" runat="server" visible="false">
                                                    <div class="label-dynamic"></div>
                                                    <label>Date :</label>

                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="idDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TabIndex="18" AutoPostBack="true"
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

                                                <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divdoc" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <label>Upload Document :</label>
                                                    </div>
                                                    <asp:FileUpload ID="flupuniv" runat="server" ToolTip="Click here to Upload Document" TabIndex="12" />
                                                      <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                                </div>--%>
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
                                            <asp:RadioButtonList ID="rdbTeacher" runat="server" AutoPostBack="true" TabIndex="19"
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
                                            <asp:TextBox ID="txtteachno" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Enter Approval Number"
                                                MaxLength="15"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divpgdt" runat="server" visible="false">
                                            <div class="label-dynamic"></div>
                                            <label>Date :</label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgappdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtappdt" runat="server" CssClass="form-control" TabIndex="21" AutoPostBack="true"
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

                                        <%--  <div class="form-group col-md-3 col-12" id="divpgdoc" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Upload Document :</label>
                                                </div>
                                                <asp:FileUpload ID="flupteach" runat="server" ToolTip="Click here to Upload Document" TabIndex="12" />
                                                <span style="color: red">Upload File Maximum Size 10 Mb</span>
                                            </div>--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </asp:Panel>


                            <%--</div>--%>



                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ServiceBook" TabIndex="22"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="23"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ServiceBook"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>



                        </div>

                        <div class="col-12">
                            <asp:Panel ID="pnlList" runat="server">
                                <asp:ListView ID="lvPrvService" runat="server">
                                    <EmptyDataTemplate>
                                        <br />
                                        <p class="text-center text-bold">
                                            <asp:Label ID="lblErrMsg" runat="server" SkinID="Errorlbl" Text="No Rows In Current Appointment Status Details"></asp:Label>
                                        </p>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Current Appointment Status Details</h5>
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
                                                    <th>Designation                                                        
                                                    </th>
                                                    <th>Appointment Date
                                                    </th>

                                                    <%-- <th>Attachment
                                                        </th>--%>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("cano")%>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%# Eval("cano") %>'
                                                    AlternateText="Delete Record" ToolTip="Delete Record"
                                                    OnClientClick="showConfirmDel(this); return false;" OnClick="btnDelete_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("fdt","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("tdt","{0:dd/MM/yyyy}")%>
                                            </td>
                                            
                                            <%--<td>
                                                <%# Eval("APPOINTMENTMODE")%>
                                            </td>--%>
                                            <td>
                                                <%# Eval("POST") %>
                                            </td>
                                            <td>
                                                <%# Eval("APPOINTMENTDDATE" , "{0:dd/MM/yyyy}") %>
                                            </td>

                                            <%--<td>
                                                    <asp:HyperLink ID="lnkDownload" runat="server" Target="_blank" NavigateUrl='<%# GetFileNamePath(Eval("ATTACHMENT"),Eval("PSNO"),Eval("IDNO"))%>'><%# Eval("ATTACHMENT")%></asp:HyperLink>
                                                </td>--%>
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

            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rdbStatus" />
            <asp:PostBackTrigger ControlID="rdbTeacher" />

            <asp:PostBackTrigger ControlID="btnSubmit" />
            <%--         <asp:PostBackTrigger ControlID="btnAdd" />--%>
        </Triggers>
    </asp:UpdatePanel>


    <%--  <script type="text/javascript">
                                            function fun1() {
                                                alert('SONAL');
                                                if (document.getElementById("ctl00_ctl00_ContentPlaceHolder1_sbctp_rdbStatus").checked == true) {
                                                    alert('TANU')
                                                    document.getElementById("ctl00_ctl00_ContentPlaceHolder1_sbctp_divapp").visible = true;

                                                }
                                            }
                                        </script>--%>

    <script type="text/javascript">
        function fun1() {
            alert('SONAL');
            if (document.getElementById("rdbStatus").checked == true) {
                alert('TANU')
                document.getElementById("PanelUni").visible = true;

            }
        }
    </script>

    <%--<script>
        function ShowPanel1() {
            document.getElementById('<%= rdbStatus.ClientID %>').checked = true;
            document.getElementById('<%= PanelUni.ClientID %>').style.display = 'block';
            document.getElementById('<%= PanelPG.ClientID %>').style.display = 'none';
        }

        function ShowPanel2() {
            document.getElementById('<%= rdbTeacher.ClientID %>').checked = false;
                                                document.getElementById('<%= PanelUni.ClientID %>').style.display = 'block';
                                                document.getElementById('<%= PanelPG.ClientID %>').style.display = 'none';
                                            }

    </script>--%>
</asp:Content>



