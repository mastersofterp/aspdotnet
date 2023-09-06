<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="leaves.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_leaves" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        function ShowHideDiv(chkLeaveSlot) {
            if (chkLeaveSlot.checked == true) {
                //alert("on");
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtNoslotDay').style.display = "block";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtafterCompletion').style.display = "block";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtstartDT').style.display = "block";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtcreditDT').style.display = "block";

            }
            else if (chkLeaveSlot.checked == false) {
                //alert("off");
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtNoslotDay').style.display = "none";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtafterCompletion').style.display = "none";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtstartDT').style.display = "none";
                document.getElementById('ctl00_ContentPlaceHolder1_divtxtcreditDT').style.display = "none";
            }
        }
    </script>



    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE DETAIL ENTRY</h3>
                        </div>
                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Leave Details</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Leave Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlleaveshrtname" runat="server" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="True" ToolTip="Select Leave Name"
                                                OnSelectedIndexChanged="ddlleaveshrtname_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvLeave" runat="server" ControlToValidate="ddlleaveshrtname"
                                                Display="None" ErrorMessage="Please Select Leave Name" ValidationGroup="Leave"
                                                SetFocusOnError="True" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Short Name</label>
                                            </div>
                                            <asp:TextBox ID="txtShortname" runat="server" MaxLength="10" CssClass="form-control" TabIndex="2"
                                                ToolTip="Enter Leave Short Name" Enabled="false" />
                                            <asp:RequiredFieldValidator ID="rfvShortname" runat="server" ControlToValidate="txtShortname"
                                                Display="None" ErrorMessage="Please Enter Short Name" ValidationGroup="Leave"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Max.Days</label>
                                            </div>
                                            <%--<asp:Label ID="txtMaxdays" runat="server" Text="" CssClass="form-control" TabIndex="3" ToolTip="Enter Maximum Leave Days" ></asp:Label>--%>
                                            <asp:TextBox ID="txtMaxdays" runat="server" MaxLength="5" CssClass="form-control" TabIndex="3" onkeypress="return CheckNumeric(event,this);"
                                                ToolTip="Enter Maximum Leave Days" />
                                            <asp:RangeValidator ID="rngMaxdays" runat="server" ControlToValidate="txtMaxdays"
                                                Display="None" ErrorMessage="Please Enter Max Days Between 0 to 999.99" ValidationGroup="Leave"
                                                SetFocusOnError="true" MinimumValue="0" MaximumValue="999.99" Type="Double"></asp:RangeValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FTBEmax" runat="server" TargetControlID="txtMaxdays" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Staff Type">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                                SetFocusOnError="true" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Period</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Leave Period">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ControlToValidate="ddlPeriod"
                                                Display="None" ErrorMessage="Please Select Period" ValidationGroup="Leave" SetFocusOnError="true"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Carry</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCarry" runat="server" CssClass="form-control" TabIndex="6" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Carry Yes or No" OnSelectedIndexChanged="ddlCarry_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">N</asp:ListItem>
                                                <asp:ListItem Value="1">Y</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCarry" visible="false">
                                            <div class="label-dynamic">
                                                <label>Maximum Days To Be Carried</label>
                                            </div>
                                            <asp:TextBox ID="txtDayCarry" runat="server" MaxLength="4" Text="" TabIndex="7"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RangeValidator ID="rvDayCarry" runat="server" ControlToValidate="txtDayCarry"
                                                Display="None" ErrorMessage="Please Enter Max Days Between 0 to 999.99" ValidationGroup="Leave"
                                                SetFocusOnError="true" MinimumValue="0" MaximumValue="999.99" Type="Double"></asp:RangeValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtDayCarry" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Gender</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSex" runat="server" CssClass="form-control" TabIndex="8" data-select2-enable="true"
                                                AppendDataBoundItems="true" ToolTip="Select Gender">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="A">ALL</asp:ListItem>
                                                <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                                <asp:ListItem Value="M">MALE</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSex" runat="server" ControlToValidate="ddlSex"
                                                Display="None" ErrorMessage="Please Select Gender" ValidationGroup="Leave" SetFocusOnError="true"
                                                InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Maximum Days</label>
                                            </div>
                                            <asp:TextBox ID="txtMaxDayApply" runat="server" MaxLength="4" Text="" TabIndex="9"
                                                CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtMaxDayApply" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Minimum Days</label>
                                            </div>
                                            <asp:TextBox ID="txtMinDaysApply" runat="server" MaxLength="4" Text="" TabIndex="10" onkeypress="return CheckFloat(event,this);"
                                                CssClass="form-control"></asp:TextBox>
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtMinDaysApply" FilterType="Custom" ></ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Maximum Time in Service</label>
                                            </div>
                                            <asp:TextBox ID="txtServiceLimit" runat="server" MaxLength="10" Text="" TabIndex="11"
                                                CssClass="form-control" ToolTip="Enter Days Allowed for Application"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtServiceLimit" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Service Period Complete</label>
                                            </div>
                                            <asp:TextBox ID="txtServiceComplete" runat="server" MaxLength="10" Text="" TabIndex="12"
                                                CssClass="form-control" ToolTip="Enter Days Allowed for Application"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtServiceComplete" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Calculate Holiday Or Not</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcalholiday" runat="server" CssClass="form-control" TabIndex="13" data-select2-enable="true"
                                                ToolTip="Select Calculate Holiday Or Not">
                                                <asp:ListItem>Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">N</asp:ListItem>
                                                <asp:ListItem Value="0">Y</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <div class="label-dynamic">
                                                <label>Allow Leave Submission Before(Capping)</label>
                                            </div>
                                            <asp:CheckBox ID="chkBeforeAfterCapping" runat="server" AutoPostBack="true" TabIndex="14"
                                                ToolTip="Check Mark to allow submission of leave after leave from date. And Uncheck, to allow leave submission before Joining date" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Days Allowed for Application</label>
                                            </div>
                                            <asp:TextBox ID="txtAllowdt" runat="server" MaxLength="10" Text="" TabIndex="15"
                                                CssClass="form-control" ToolTip="Enter Days Allowed for Application"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxtAllowdt" runat="server" ControlToValidate="txtAllowdt"
                                                Display="None" ErrorMessage="Please Enter Allowed days" ValidationGroup="Leave"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label for="txtLeaveSlot">Credit Leave on Month Basis</label>
                                            </div>
                                            <asp:CheckBox ID="chkLeaveSlot" runat="server" TabIndex="16" CssClass="form-control"
                                                ToolTip="Select to Credit Leave on Month Basic" OnCheckedChanged="ChckedChanged" AutoPostBack="true" />
                                        </div>

                                        <div id="divtxtNoslotDay" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>No. of Slot of Days</label>
                                            </div>
                                            <asp:TextBox ID="txtNoslotDay" MaxLength="3" runat="server" CssClass="form-control" ToolTip="Enter No of Slot Days" onkeypress="return CheckNumeric(event,this);" TabIndex="17"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNoslotDay" runat="server" ErrorMessage="Only Numbers allowed"
                                                ValidationGroup="Leave" ValidationExpression="\d+"> 
                                            </asp:RegularExpressionValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtNoslotDay" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtafterCompletion" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Total Credit Leaves after Completion of Slot</label>
                                            </div>
                                            <asp:TextBox ID="txtafterCompletion" TabIndex="18" MaxLength="5" runat="server" CssClass="form-control" ToolTip="Enter No of Days after Completion of slot"></asp:TextBox>
                                            <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtafterCompletion" runat="server"
                                                ErrorMessage="Only Numbers allowed" ValidationGroup="Leave" ValidationExpression="^[1-9]\d*(\.\d+)?$"> 
                                            </asp:RegularExpressionValidator>--%>
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtafterCompletion" FilterType="Numbers" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>--%>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtstartDT" runat="server">
                                            <div class="label-dynamic">
                                                <label>Starting Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgDt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>--%>
                                                <asp:TextBox ID="txtCreditDT" runat="server" MaxLength="10" CssClass="form-control"
                                                    TabIndex="19" ToolTip="Enter Credit Date " Style="z-index: 0;" />
                                                <%--<asp:RequiredFieldValidator ID="rfvCreditDt" runat="server" ControlToValidate="txtCreditDT"
                                                                    Display="None" ErrorMessage="Please Enter Date" ValidationGroup="Leave"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="ceCreditDt" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtCreditDT" PopupButtonID="imgDt" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeCreditDt" runat="server" TargetControlID="txtCreditDT"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevCreditDt" runat="server" ControlExtender="meeCreditDt"
                                                    ControlToValidate="txtCreditDT" EmptyValueMessage="Please Enter Date"
                                                    InvalidValueMessage="Enter Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <%--added new on 20-09-2022--%>
                                        <div id="divisdate" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>IS DOJ</label>
                                            </div>
                                            <asp:CheckBox ID="chkisdate" runat="server" TabIndex="20" CssClass="form-control"
                                                ToolTip="Credit Leave Joining Date wise" />
                                        </div>

                                        <%--<div class="form-group col-md-4">
                                                            <label for="txtLeaveSlot">Credit Leave on Month Basic :</label>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="15" onclick="ShowHideDiv(this)" CssClass="form-control" ToolTip="Select to Credit Leave on Month Basic" />
                                                        </div>--%>
                                        <%-- <div class="form-group col-md-4">
                                                            <label>Starting Date :</label>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" ToolTip="Enter starting date"></asp:TextBox>

                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtxtcreditDT" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Starting Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgDt2" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%-- <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>--%>
                                                <asp:TextBox ID="txtDate" runat="server" MaxLength="10" CssClass="form-control"
                                                    TabIndex="21" ToolTip="Enter Credit Date " Style="z-index: 0;" />
                                                <%--<asp:RequiredFieldValidator ID="rfvCreditDt" runat="server" ControlToValidate="txtCreditDT"
                                                                    Display="None" ErrorMessage="Please Enter Date" ValidationGroup="Leave"
                                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtCreditDT" PopupButtonID="imgDt2" Enabled="true"
                                                    EnableViewState="true">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtDate" EmptyValueMessage="Please Enter Date"
                                                    InvalidValueMessage="Enter Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                    ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <%--<div id="divtxtcreditDT" runat="server" class="form-group col-md-4">
                                                            <label>Credit Date :</label>
                                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Enter Credit Date"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtDate" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>--%>

                                        <%--<div class="form-group col-md-4">
                                                            <label>Is Credit leave Based</label>
                                                            <asp:DropDownList ID="ddlIsCredit" runat="server" CssClass="form-control" TabIndex="7"
                                                                AppendDataBoundItems="true" ToolTip="Select Gender">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="M">Monthly Basis</asp:ListItem>
                                                                <asp:ListItem Value="D">Day Basis</asp:ListItem>                                                            
                                                            </asp:DropDownList>
                                                        </div>--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trmul" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Multiple With</label>
                                            </div>
                                            <asp:TextBox ID="txtmutiplewith" runat="server" MaxLength="10" Text="1" TabIndex="22"
                                                CssClass="form-control" ToolTip="Enter Multiple With"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvmutiplewith" runat="server" ControlToValidate="txtmutiplewith"
                                                Display="None" ErrorMessage="Please Enter Mutiple With" ValidationGroup="Leave"
                                                SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="rngmutiplewith" runat="server" ControlToValidate="txtmutiplewith"
                                                Display="None" ErrorMessage="Please Enter Multiple with Between 0 to 999.99" ValidationGroup="Leave"
                                                SetFocusOnError="true" MinimumValue="0" MaximumValue="999.99" Type="Double"></asp:RangeValidator>
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                            <div class="label-dynamic">
                                                <label>Leave Apply Before Time</label>
                                            </div>
                                            <asp:TextBox ID="txttimeleave" runat="server" TabIndex="23" CssClass="form-control" ToolTip="Press A or P to switch between AM and PM"></asp:TextBox>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txttimeleave"
                                                Mask="99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                CultureTimePlaceholder="" Enabled="True" />
                                        </div>



                                        <div class="col-12" style="display: none">
                                            <br />
                                            <div class="form-group col-12">
                                                <p class="bg-success text-blue">
                                                    <%--<p>--%>
                                                    <label>PD LEAVE :</label>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="col-12" style="display: none">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Maximum Days</label>
                                                    </div>
                                                    <asp:TextBox ID="txtpdMaxDayApply" runat="server" MaxLength="2" Text="" TabIndex="24" autocomplete="off"
                                                        CssClass="form-control" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtpdMaxDayApply" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Days Allowed For PD Application</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPredated" runat="server" MaxLength="2" Text="" TabIndex="25" autocomplete="off"
                                                        CssClass="form-control" ToolTip="Enter Days Allowed for" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPredated" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12" hidden>
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                                    <div class="label-dynamic">
                                                        <label>Maximum Ocasions</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMaxOccPD" runat="server" MaxLength="10" Text="" TabIndex="26" autocomplete="off"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                                    <div class="label-dynamic">
                                                        <label>Occasion Period</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlOccPD" runat="server" CssClass="form-control" TabIndex="27" data-select2-enable="true"
                                                        AppendDataBoundItems="true" ToolTip="Select Gender">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="M">MONTHLY</asp:ListItem>
                                                        <asp:ListItem Value="Q">QUATERLY</asp:ListItem>
                                                        <asp:ListItem Value="H">HALF YEARLY</asp:ListItem>
                                                        <asp:ListItem Value="Y">YEARLY</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12" style="display: none">

                                            <div class="form-group col--12">
                                                <p class="bg-success text-blue">
                                                    <%--<p>--%>
                                                    <label>PDL LEAVE :</label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-12" style="display: none">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Maximum Days</label>
                                                    </div>
                                                    <asp:TextBox ID="txtpdlMaxDaysApply" runat="server" MaxLength="2" Text="" TabIndex="28" autocomplete="off"
                                                        CssClass="form-control" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtpdlMaxDaysApply" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Days Allowed For PDL Application</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPostdated" runat="server" MaxLength="2" Text="" TabIndex="29" autocomplete="off"
                                                        CssClass="form-control" ToolTip="Enter Maximum Times Post Dated Leave Allowed" onkeypress="return CheckNumeric(event,this);"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtPostdated" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12" hidden>
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                                    <div class="label-dynamic">
                                                        <label>Maximum Occasions</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMaxOccPDL" runat="server" MaxLength="2" Text="" TabIndex="30" autocomplete="off"
                                                        CssClass="form-control" ToolTip="Enter Maximum Times Post Dated Leave Allowed"></asp:TextBox>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label>:</label>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" hidden>
                                                    <div class="label-dynamic">
                                                        <label>Occasion Period</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlOccPDL" runat="server" CssClass="form-control" TabIndex="31" data-select2-enable="true"
                                                        AppendDataBoundItems="true" ToolTip="Select Gender">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="M">MONTHLY</asp:ListItem>
                                                        <asp:ListItem Value="Q">QUARTERLY</asp:ListItem>
                                                        <asp:ListItem Value="H">HALF YEARLY</asp:ListItem>
                                                        <asp:ListItem Value="Y">YEARLY</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <asp:Label ID="lblerror" SkinID="Errorlbl" runat="server"></asp:Label>
                                            <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label_dynamic">
                                                <label>Is ClassArrangeMent Required </label>
                                            </div>
                                            <asp:CheckBox ID="chkIsclassArragnment" runat="server" AutoPostBack="true" TabIndex="32" CssClass="form-control"
                                                ToolTip="Check mark to display Class Arrangment" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label_dynamic">
                                                <label>Is Class Arrangement Acceptance</label>
                                            </div>
                                            <asp:CheckBox ID="chkIsclassArrangmentAcceptance" runat="server" TabIndex="33" CssClass="form-control"
                                                ToolTip="Check mark for Class Arrangment Approval" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label_dynamic">
                                                <label>Is Required Load</label>
                                            </div>
                                            <asp:CheckBox ID="chkLoad" runat="server" TabIndex="34" CssClass="form-control"
                                                ToolTip="Check mark for Required Load" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label_dynamic">
                                                <label>Is Leave Validate Per Month</label>
                                            </div>
                                            <asp:CheckBox ID="chkValid" runat="server" TabIndex="35" CssClass="form-control" AutoPostBack="true"
                                                ToolTip="Check mark for Leave Valid Month" OnCheckedChanged="chkValid_CheckedChanged" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divLeaveValid" runat="server" visible="false">
                                            <div class="label_dynamic">
                                                <label>Leave Validate Per Month</label>
                                            </div>
                                            <asp:TextBox ID="txtLeaveMonth" runat="server" TabIndex="36" CssClass="form-control" MaxLength="3"
                                                onkeypress="return CheckNumeric(event,this);"  ToolTip="Enter Leave Valid Month" ></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" TabIndex="35"
                                    CssClass="btn btn-primary" ToolTip="Click here to Add New Leave Detail Entry"></asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leave" OnClick="btnSave_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" TabIndex="36" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" OnClick="btnBack_Click"
                                    CssClass="btn btn-primary" ToolTip="Click here to Return to Previous Menu" TabIndex="37" />
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    OnClick="btnShowReport_Click1" TabIndex="38" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ToolTip="Click here to Reset"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="39" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leave"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvLeave" runat="server">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Click Add New To Enter Leaves" CssClass="d-block text-center mt-3" />
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Leave Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action
                                                        </th>
                                                        <th>Leave Name
                                                        </th>
                                                        <th>Short Name
                                                        </th>
                                                        <th>Max.Days
                                                        </th>
                                                        <th>Carry
                                                        </th>
                                                        <th>Staff Type
                                                        </th>
                                                        <th>Period
                                                        </th>
                                                        <th>Gender
                                                        </th>
                                                        <%--  <th>
                                                                Multiple With
                                                               </th>--%>
                                                        <%--<th>Allowed Days
                                                            </th>
                                                            <th>Calculate Holidays
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("LNO") %>'
                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="20" />

                                                    <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("LNO") %>'
                                                                    AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                                    OnClientClick="showConfirmDel(this); return false;" />--%>
                                                </td>
                                                <td>
                                                    <%--<%# Eval("LEAVENAME") %>--%>
                                                    <%# Eval("Leave_Name") %>
                                                </td>
                                                <td>
                                                    <%# Eval("LEAVE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("MAX") %>
                                                </td>
                                                <td>
                                                    <%# Eval("CARRY") %>
                                                </td>
                                                <td>
                                                    <%# Eval("STAFFTYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PERIOD") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEX") %>
                                                </td>
                                                <%--   <td>
                                                                <%# Eval("CAL") %>
                                                            </td>--%>
                                                <%-- <td>
                                                    <%# Eval("ALLOWED_DAYS")%>
                                                </td>
                                                <td>
                                                    <%#Eval("CAL_HOLIDAYS")%>
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--<div class="vista-grid_datapager d-none">
                                            <div class="text-center">
                                                <asp:DataPager ID="dpPager" runat="server" PagedControlID="lvLeave" PageSize="10"
                                                    OnPreRender="dpPager_PreRender">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                            ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="Current" />
                                                        <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />

                                                    </Fields>
                                                </asp:DataPager>
                                            </div>
                                        </div>--%>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click"/>--%>
            <%--<asp:AsyncPostBackTrigger ControlID="btnShowReport" EventName="Click"/>--%>
        </Triggers>
    </asp:UpdatePanel>
    <%--BELOW CODE IS TO SHOW THE MODAL POPUP EXTENDER FOR DELETE CONFIRMATION--%>
    <%--DONT CHANGE THE CODE BELOW. USE AS IT IS--%>
    <%--<ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <div class="col-md-12">
        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
            <div class="text-center">
                <div class="modal-content">
                    <div class="modal-body">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" />
                        <td>&nbsp;&nbsp;Are you sure you want to delete this record..?</td>
                        <div class="text-center">
                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" CssClass="btn-primary" />
                            <asp:Button ID="btnNoDel" runat="server" Text="No" CssClass="btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>--%>

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

        function CheckFloat(key, object) {
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 9 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
            else {
                var parts = object.value.split('.');
                if (parts.length > 1 && keycode == 46) {
                    return false;
                }
                return true;
            }
        }

    </script>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
