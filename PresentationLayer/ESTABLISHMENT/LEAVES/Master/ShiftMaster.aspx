<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ShiftMaster.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Master_ShiftMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SHIFT MASTER </h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>College Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                        TabIndex="1" AutoPostBack="true" ToolTip="Select College Name">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                        Display="None" ErrorMessage="Please Select College Name" ValidationGroup="PAuthority"
                                        SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Shift Name</label>
                                    </div>
                                    <asp:TextBox ID="txtShiftName" runat="server" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvShiftName" runat="server" ControlToValidate="txtShiftName"
                                        Display="None" ErrorMessage="Please Enter Shift Name" ValidationGroup="Shift"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Shift In Time</label>
                                    </div>
                                    <asp:TextBox ID="txtShiftInTime" runat="server" ToolTip="Press A or P to switch between AM and PM "
                                        CssClass="form-control" TabIndex="2" AutoPostBack="true"
                                        OnTextChanged="txtShiftInTime_TextChanged"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="meeIn" runat="server" TargetControlID="txtShiftInTime"
                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShiftInTime"
                                        Display="None" ErrorMessage="Please Enter Shift In Time" ValidationGroup="Shift"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Shift Out Time</label>
                                    </div>
                                    <asp:TextBox ID="txtShiftOutTime" runat="server" ToolTip="Press A or P to switch between AM and PM "
                                        CssClass="form-control" TabIndex="3" AutoPostBack="true"
                                        OnTextChanged="txtShiftOutTime_TextChanged"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtShiftOutTime"
                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />

                                    <asp:RequiredFieldValidator ID="rfvShiftOutTime" runat="server" ControlToValidate="txtShiftOutTime"
                                        Display="None" ErrorMessage="Please Enter Shift Out Time" ValidationGroup="Shift"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Shift IN MidTime</label>
                                    </div>
                                    <asp:TextBox ID="txtShiftInTime_Mid" runat="server" ToolTip="Press A or P to switch between AM and PM "
                                        CssClass="form-control" TabIndex="4"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtShiftInTime_Mid"
                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShiftInTime_Mid"
                                        Display="None" ErrorMessage="Please Enter Shift In MidTime" ValidationGroup="Shift"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Shift OUT MidTime</label>
                                    </div>
                                    <asp:TextBox ID="txtShiftOutTime_Mid" runat="server"
                                        ToolTip="Press A or P to switch between AM and PM " CssClass="form-control" TabIndex="5"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                        AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                        ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time"
                                        TargetControlID="txtShiftOutTime_Mid" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtShiftOutTime_Mid" Display="None"
                                        ErrorMessage="Please Enter Shift OUT MidTime" SetFocusOnError="True"
                                        ValidationGroup="Shift">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Night Shift</label>
                                    </div>
                                    <asp:CheckBox ID="chkNightShift" runat="server" Checked="false" TabIndex="6" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Allow Comp-Off Leave</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsAllowCompOffLeave" runat="server" Checked="false" TabIndex="7" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <%--<sup>*</sup>--%>
                                        <label>Double Duty</label>
                                    </div>
                                    <asp:CheckBox ID="chkIsDoubleDuty" runat="server" Checked="false" TabIndex="8" />
                                </div>

                            </div>
                        </div>

                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnAdd" runat="server" SkinID="LinkAddNew" OnClick="btnAdd_Click" Text="Add New" TabIndex="9"
                            CssClass="btn btn-outline-primary" ToolTip="Click here to Add New Shift"></asp:LinkButton>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                            ValidationGroup="Shift" CssClass="btn btn-outline-primary" TabIndex="7" />
                        <asp:Button ID="btnback" CssClass="btn btn-outline-primary" TabIndex="9"
                            OnClick="btnback_Click" Text="Back" runat="server" />
                        <asp:Button ID="btnReport" runat="server" CausesValidation="false"
                            OnClick="btnReport_Click" Text="Report" CssClass="btn btn-outline-info" TabIndex="10" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                            ValidationGroup="Shift" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false"
                            OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-outline-danger" TabIndex="8" />
                    </div>
                    <div class="col-12 mt-3 mb-3">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvShiftMaster" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid">
                                        <div class="sub-heading">
                                            <h5>Shift Master</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Action
                                                    </th>
                                                    <th>SHIFT NAME
                                                    </th>
                                                    <th>COLLEGE NAME
                                                    </th>
                                                    <th>IN TIME
                                                    </th>
                                                    <th>OUT TIME
                                                    </th>
                                                    <th>SHIFT MID IN TIME
                                                    </th>
                                                    <th>SHIFT MID OUT TIME
                                                    </th>
                                                    <th>NIGHT SHIFT STATUS
                                                    </th>
                                                    <th>COMP-OFF ALLOW STATUS
                                                    </th>
                                                    <th>DOUBLE DUTY STATUS
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
                                    <tr class="item">
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SHIFTNO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            <asp:Label ID="lblSHIFTNO" runat="server" Text='<%# Eval("SHIFTNO") %>' Visible="false" />

                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("SHIFTNAME") %>' />
                                            <asp:HiddenField ID="hdncollege_no" runat="server" Value='<%# Eval("COLLEGE_NO") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("INTIME") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME") %>' />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblInTimeMid" runat="server" Text='<%# Eval("INTIME_MID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTimeMid" runat="server" Text='<%# Eval("OUTTIME_MID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNightShiftStatus" runat="server" Text='<%# Eval("NightShiftStatus") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAllowCompOffStatus" runat="server" Text='<%# Eval("AllowCompOffStatus") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIsDoubleDuty" runat="server" Text='<%# Eval("IsDoubleDutyNew") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem">
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SHIFTNO") %>'
                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            <asp:Label ID="lblSHIFTNO" runat="server" Text='<%# Eval("SHIFTNO") %>' Visible="false" />

                                        </td>
                                        <td>
                                            <asp:Label ID="lblShiftName" runat="server" Text='<%# Eval("SHIFTNAME") %>' />
                                            <asp:HiddenField ID="hdncollege_no" runat="server" Value='<%# Eval("COLLEGE_NO") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInTime" runat="server" Text='<%# Eval("INTIME") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTime" runat="server" Text='<%# Eval("OUTTIME") %>' />
                                        </td>

                                        <td>
                                            <asp:Label ID="lblInTimeMid" runat="server" Text='<%# Eval("INTIME_MID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOutTimeMid" runat="server" Text='<%# Eval("OUTTIME_MID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNightShiftStatus" runat="server" Text='<%# Eval("NightShiftStatus") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAllowCompOffStatus" runat="server" Text='<%# Eval("AllowCompOffStatus") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIsDoubleDuty" runat="server" Text='<%# Eval("IsDoubleDutyNew") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>

                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

