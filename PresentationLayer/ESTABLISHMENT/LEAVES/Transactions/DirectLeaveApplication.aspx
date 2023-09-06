<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DirectLeaveApplication.aspx.cs" Inherits="DirectLeaveApplication"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script language="javascript" type="text/javascript">

        function ReceiveServerData(arg) {

            if (arg == 0) {

                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirmvalue').value = confirm('Do yant to Proceed ?');
            }
        }

    </script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('#table2').dataTable();
                }
            });
        };

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
    </script>

    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">DIRECT LEAVE APPLICATION</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Direct Leave Application Details</h5>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Leave Type</label>
                                                </div>
                                                <asp:RadioButtonList ID="rblleavetype" runat="server" TabIndex="1" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblleavetype_SelectedIndexChanged">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="Full Day" Value="0">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="Half Day" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>College Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                                    Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Staff Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" ToolTip="Select Staff Type">
                                                </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStafftype" InitialValue="0"
                                                    Display="None" ErrorMessage="Please Select Staff Type" ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Employee Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="2" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" ToolTip="Select Employee Name">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                    Display="None" ErrorMessage="Please Select Employee" ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true" InitialValue="0" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Leave Name</label>
                                                </div>
                                                <asp:DropDownList ID="ddlLName" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlLName_SelectedIndexChanged" ToolTip="Select Leave Name">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLName" runat="server" ControlToValidate="ddlLName"
                                                    Display="None" ErrorMessage="Please Select Leave " ValidationGroup="Leaveapp"
                                                    SetFocusOnError="true" InitialValue="0" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Balance Leave</label>
                                                </div>
                                                <asp:TextBox ID="txtLeavebal" TabIndex="4" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromdt" runat="server" CssClass="form-control" TabIndex="5"
                                                        MaxLength="10" ToolTip="Enter Leave Application From Date" Style="z-index: 0;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtFromdt"
                                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Leaveapp"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdt"
                                                        PopupButtonID="imgCalFromdt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                                        ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true">
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>

                                                    <asp:TextBox ID="txtTodt" runat="server" CssClass="form-control" TabIndex="6" MaxLength="10"
                                                        OnTextChanged="txtTodt_TextChanged" AutoPostBack="true" Style="z-index: 0;" />
                                                    <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Leaveapp"
                                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                                        PopupButtonID="imgCalTodt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                                        ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true">
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>No. of Days</label>
                                                </div>
                                                <asp:TextBox ID="txtNodays" runat="server" MaxLength="5" CssClass="form-control" Enabled="false" OnTextChanged="txtNodays_TextChanged"
                                                    AutoPostBack="true" TabIndex="7" ToolTip="Enter Number of Days" />
                                                <asp:RequiredFieldValidator ID="rfvNodays" runat="server" ControlToValidate="txtNodays"
                                                    Display="None" ErrorMessage="Please Enter No. of Days" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <%--<asp:RangeValidator ID="rngNodays" runat="server" ControlToValidate="txtNodays" Display="None"
                                                        ErrorMessage="Please Enter No of Days Between 1 to 999" ValidationGroup="Leaveapp"
                                                        SetFocusOnError="true" MinimumValue="1" MaximumValue="999.99" Type="Double"></asp:RangeValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Joining Date</label>
                                                </div>
                                                <div class="input-group date">
                                                   <%-- <div class="input-group-addon">
                                                        <i id="imgCalJoindt" runat="server" class="fa fa-calendar text-blue"></i>
                                                    </div>--%>

                                                    <asp:TextBox ID="txtJoindt" runat="server" CssClass="form-control" ToolTip="Enter Joining Date"
                                                        MaxLength="10" TabIndex="8" Style="z-index: 0;" Enabled="false" />
                                                    <ajaxToolKit:CalendarExtender ID="CeJoindt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtJoindt"
                                                        PopupButtonID="imgCalJoindt" Enabled="true" EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeJoindt" runat="server" TargetControlID="txtJoindt"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevJoindt" runat="server" ControlExtender="meeJoindt"
                                                        ControlToValidate="txtJoindt" EmptyValueMessage="Please Enter Joining Date" InvalidValueMessage="Joining Date is Invalid (Enter dd/MM/yyyy Format)"
                                                        Display="None" TooltipMessage="Please Enter Joining Date" EmptyValueBlurredText="Empty"
                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divJoiningCriteria" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Joining Criteria</label>
                                                </div>
                                                <asp:DropDownList ID="ddlNoon" runat="server" CssClass="form-control" TabIndex="9" data-select2-enable="true"
                                                    AppendDataBoundItems="true" ToolTip="Select FN/AN" AutoPostBack="true" OnSelectedIndexChanged="ddlNoon_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">First Half</asp:ListItem>
                                                    <asp:ListItem Value="1">Second Half</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                <div class="label-dynamic">
                                                    <label>Certificates</label>
                                                </div>
                                                <asp:CheckBox ID="chkUFit" TabIndex="10" Text="Unfit Certificate" runat="Server" />
                                                <asp:CheckBox ID="ChkFit" Text="Fit Certificate" runat="Server" TabIndex="11" />
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                <div class="label-dynamic">
                                                    <label>Charge</label>
                                                </div>
                                                <asp:DropDownList ID="ddlChargeHanded" AppendDataBoundItems="true" runat="server" TabIndex="12" data-select2-enable="true"
                                                    ToolTip="Select Charge" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlChargeHanded"
                                                    Display="None" ErrorMessage="Please Enter Reason" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Reason</label>
                                                </div>
                                                <%--Width="350px" Height="50px TextMode="MultiLine" --%>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TabIndex="13"
                                                    ToolTip="Enter Reason" onkeypress="return CheckAlphabet(event,this);" MaxLength="200" />
                                                <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason"
                                                    Display="None" ErrorMessage="Please Enter Reason" ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divPath" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Path</label>
                                                </div>
                                                 <asp:TextBox ID="txtPath" runat="server" Enabled="false" CssClass="form-control"
                                        ToolTip="Path" TabIndex="25" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp"
                                        CssClass="btn btn-primary" TabIndex="14" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnBack" runat="server" CausesValidation="false" Visible="false"
                                        Text="Back" CssClass="btn btn-primary" TabIndex="15" ToolTip="Click here to Go back to Previous Menu" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        CssClass="btn btn-warning" TabIndex="16" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </ContentTemplate>

        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>--%>
    </asp:UpdatePanel>
</asp:Content>
