<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Estb_Biometric_Consolidate_report.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_Estb_Biometric_Consolidate_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">BIOMETRIC CONSOLIDATE REPORT</h3>
                </div>

                <div class="box-body">
                    <asp:UpdatePanel ID="updAdd" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlcollege" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Select College" data-select2-enable="true"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFVdept" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaffType" runat="server" AppendDataBoundItems="true" TabIndex="2" ToolTip="Select Staff Type" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlStaffType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaffType"
                                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" TabIndex="3" ToolTip="Select Department" data-select2-enable="true"
                                                CssClass="form-control" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rblAllParticular" runat="server" TabIndex="5" ToolTip="Employees"
                                                RepeatDirection="Horizontal" OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Enabled="true" Selected="True" Text="All Employees" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Particular Employee" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="6" ToolTip="Select Employees" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                 <asp:TextBox ID="txtFdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFdate"
                                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgToDate" TargetControlID="txtFdate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meFDate" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                    TargetControlID="txtFdate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mvFdate" runat="server" ControlExtender="meFDate"
                                                    ControlToValidate="txtFdate" Display="None" 
                                                    EmptyValueBlurredText="Empty" InvalidValueMessage="From Date is invalid"
                                                    IsValidEmpty="False" SetFocusOnError="True" ValidationGroup="Leaveapp"  InitialValue="__/__/____"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                 <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:CalendarExtender ID="calDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgDate" TargetControlID="txtDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meDate" runat="server" CultureAMPMPlaceholder=""
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                                                    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                                                    OnInvalidCssClass="errordate" Enabled="True" Mask="99/99/9999" MaskType="Date"
                                                    TargetControlID="txtDate" >
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mvDate" runat="server" ControlExtender="meDate"
                                                    ControlToValidate="txtDate" Display="None"
                                                     EmptyValueBlurredText="Empty" InvalidValueMessage="Date is invalid"
                                                    IsValidEmpty="False" InitialValue="__/__/____" SetFocusOnError="True" ValidationGroup="Leaveapp"></ajaxToolKit:MaskedEditValidator>
                                                <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtDate"
                                                    CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                    ValidationGroup="Holiday" ControlToCompare="txtFdate" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp" TabIndex="9" ToolTip="Click to show the Report"
                                        CssClass="btn btn-info" OnClick="btnReport_Click" />
                                    <asp:Button ID="btnReport1" runat="server" Text="In Out Report" ValidationGroup="Leaveapp" TabIndex="10" ToolTip="Click to show the Report"
                                        CssClass="btn btn-info" OnClick="btnReport1_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" ToolTip="Click To Reset"
                                        CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnReport" />
                            <asp:PostBackTrigger ControlID="btnReport1" />
                            <%--<asp:PostBackTrigger ControlID="btnCancel" />--%>

                            <%--<asp:PostBackTrigger ControlID="btnExport" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


    <div id="divMsg" runat="server">
    </div>


</asp:Content>

