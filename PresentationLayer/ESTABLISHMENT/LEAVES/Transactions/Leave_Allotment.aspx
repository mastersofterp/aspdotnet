<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Leave_Allotment.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_Leave_Allotment"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function ReceiveServerData(arg) {

            if (arg == 0) {

                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirmvalue').value = confirm('Do yant to Proceed ?');
            }
        }

    </script>

    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)


        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkSelect')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE ALLOTMENT</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Leave Allotment Details</h5>
                                    </div>
                                    <div id="divnote" runat="server" visible="false">
                                        Note <b>:</b> <span style="color: #FF0000">Please Select College Name For Getting Employee Name.</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College" ValidationGroup="Leave1"
                                        SetFocusOnError="True" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Type Name</label>
                                    </div>
                                    <asp:RadioButtonList ID="radlTransfer" runat="server" RepeatDirection="Horizontal" TabIndex="2"
                                        AutoPostBack="true" OnSelectedIndexChanged="radlTransfer_OnSelectedIndexChanged">
                                        <asp:ListItem Value="M" Selected="True">Multiple&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="S">Single</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Staff Type">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                                        SetFocusOnError="true" InitialValue="0">
                                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStafftype"
                                        Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave1"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Employee</label>
                                    </div>
                                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Employee">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                        AppendDataBoundItems="true" ToolTip="Select Department">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Gender</label>
                                    </div>
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control" TabIndex="6" AutoPostBack="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlGender_SelectedIndexChanged" ToolTip="Select Gender">
                                        <asp:ListItem Value="A">ALL</asp:ListItem>
                                        <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                        <asp:ListItem Value="M">MALE</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlGender"
                                        Display="None" ErrorMessage="Please Select Gender" ValidationGroup="Leave" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Period</label>
                                    </div>
                                    <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" TabIndex="7" AppendDataBoundItems="true" data-select2-enable="true"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged" ToolTip="Select Period">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ControlToValidate="ddlPeriod"
                                        Display="None" ErrorMessage="Please Select Period" ValidationGroup="Leave1" SetFocusOnError="true"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Year</label>
                                    </div>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" TabIndex="8" AutoPostBack="true" data-select2-enable="true"
                                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" ToolTip="Select Year">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="Leave1"
                                        SetFocusOnError="true" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                    <%--<asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="ddlYear"
                                                        Display="None" ErrorMessage="Please Select Year" ValidationGroup="Leave1" SetFocusOnError="true"
                                                        InitialValue="0"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalholidayDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDt" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"
                                            TabIndex="9" ToolTip="Enter From Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="rfvholidayDt" runat="server" ControlToValidate="txtFromDt"
                                            Display="None" ErrorMessage="Please Enter From Date"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="ceholidayDt" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDt" Enabled="true"
                                            EnableViewState="true">
                                            <%--PopupButtonID="imgCalholidayDt"--%>
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meeholidayDt" runat="server" TargetControlID="txtFromDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevholidayDt" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtFromDt" EmptyValueMessage="Please Enter  From Date"
                                            InvalidValueMessage=" From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalToDt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="10" Enabled="false"
                                            ToolTip="Enter To Date" Style="z-index: 0;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtToDt"
                                            Display="None" ErrorMessage="Please Enter  To Date"
                                            SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDt" Enabled="true"
                                            EnableViewState="true">
                                            <%--PopupButtonID="imgCalToDt"--%>
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDt"
                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                            AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeholidayDt"
                                            ControlToValidate="txtToDt" EmptyValueMessage="Please Enter  To Date"
                                            InvalidValueMessage=" To Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter  To Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Leave" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                        <%--<asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDt"
                                                            CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date"
                                                            ValidationGroup="submit" ControlToCompare="txtFromDt" />--%>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Leave Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlLeavename" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                        TabIndex="11" ToolTip="Select Leave Name" OnSelectedIndexChanged="ddlLeavename_SelectedIndexChanged" AutoPostBack="true">
                                        <%--<asp:ListItem Value="0">ALL</asp:ListItem>--%>
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvLeavename" runat="server" ControlToValidate="ddlLeavename"
                                        Display="None" ErrorMessage="Please Select Leave Name" SetFocusOnError="true"
                                        ValidationGroup="Leave1" InitialValue="0">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12" id="trSingleLeaveToCredit" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Total Leave To Credit</label>
                                    </div>
                                    <asp:TextBox ID="txtLeaves" runat="server" MaxLength="5" CssClass="form-control"
                                        TabIndex="12" ToolTip="Enter Total Leave to Credit" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLeaves"
                                        Display="None" ErrorMessage="Please Enter Total Leave To Credit" SetFocusOnError="true"
                                        ValidationGroup="Leave1">
                                    </asp:RequiredFieldValidator>
                                    <%--<ajaxToolKit:FilteredTextBoxExtender ID="Ftbeleaves" runat="server" TargetControlID="txtLeaves" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="Leave1"
                            CssClass="btn btn-primary" ToolTip="Click here to Show" TabIndex="13" />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" ToolTip="Click here to Transfer"
                            Text="Transfer" ValidationGroup="Leave1" Visible="False" TabIndex="14" />
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" TabIndex="15"
                            OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Leave1"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlEmpList" runat="server">
                            <%--<asp:Panel ID="pnlEmpList" runat="server" Height="450px" ScrollBars="Vertical">--%>
                            <asp:ListView ID="lvEmployees" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Employees</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                    Select
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>Total Leaves
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCreditLeaves" runat="server" MaxLength="5" Text='<%# Eval("LEAVES")%>' />
                                            <%--<ajaxToolKit:FilteredTextBoxExtender ID="ftbecl" runat="server" TargetControlID="txtCreditLeaves" FilterType="Numbers,Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>--%>
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

</asp:Content>
