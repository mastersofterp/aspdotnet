<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Leave_Approval_Pending_Status.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Reports_Leave_Approval_Pending_Status" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LEAVE STATUS</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Criteria for Leave Approval/Pending Status</h5>
                                            </div>
                                            <div id="divnote" runat="server" visible="false">
                                                Note <b>:</b> <span style="color: #FF0000">Please Select The College Name To Get The Employee List.</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select College" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trddldept" runat="server">
                                            <div class="label-dynamic">
                                                <label>Select Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="2" AutoPostBack="True" ToolTip="Select Department"
                                                OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trddlstaff" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstafftype" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="3" AutoPostBack="True" ToolTip="Select Staff"
                                                OnSelectedIndexChanged="ddlstafftype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvstafftype" runat="server" ControlToValidate="ddlstafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                                <asp:RadioButtonList ID="rblAllParticular" runat="server"
                                                    RepeatDirection="Horizontal" TabIndex="4"
                                                    OnSelectedIndexChanged="rblAllParticular_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Enabled="true" Selected="True" Text="&nbsp&nbsp All Employees&nbsp&nbsp" Value="0"></asp:ListItem>
                                                    <asp:ListItem Enabled="true" Text="&nbsp&nbsp Particular Employee" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tremp" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="5" data-select2-enable="true"
                                                ToolTip="Select Employee">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmp" runat="server" ControlToValidate="ddlEmp"
                                                Display="None" ErrorMessage="Please select Employee" SetFocusOnError="true" ValidationGroup="Leaveapp" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromdt" runat="server" onblur="return checkdate(this);" Style="z-index: 0;"
                                                    CssClass="form-control" TabIndex="6" ToolTip="Enter Leave/Approval Pending Status From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtFromdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <%--<ajaxToolKit:MaskedEditExtender ID="mefrmdt" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                                    MessageValidatorTip="true" TargetControlID="txtFromdt" />--%>
                                                <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromdt"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromdt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromdt" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
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
                                                <asp:TextBox ID="txtTodt" runat="server" MaxLength="7" Style="z-index: 0;" AutoPostBack="true"
                                                    CssClass="form-control" TabIndex="7" ToolTip="Enter Leave/Approval Pending Status To Date" OnTextChanged="txtTodt_TextChanged1" />
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Leaveapp">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgCalTodt" TargetControlID="txtTodt">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" TargetControlID="txtTodt" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" ControlExtender="meeTodt"
                                                    ControlToValidate="txtTodt" InvalidValueMessage="To Date is Invalid (Enter -dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Leaveapp" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="tr1" runat="server">
                                            <div class="label-dynamic">
                                                <label>Leave Status</label>
                                            </div>
                                            <asp:RadioButtonList ID="rdbleavestatus" runat="server"
                                                RepeatDirection="Horizontal" TabIndex="8">
                                                <asp:ListItem Enabled="true" Selected="True" Text="&nbsp&nbsp Approved &nbsp&nbsp&nbsp&nbsp" Value="0"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="&nbsp&nbsp Pending" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="&nbsp&nbsp Rejected" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>

                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="Leaveapp" TabIndex="9"
                                    CssClass="btn btn-info" OnClick="btnReport_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnReportnew" runat="server" Text="Authority Status Report" ValidationGroup="Leaveapp" TabIndex="10"
                                    CssClass="btn btn-info" OnClick="btnReportnew_Click" ToolTip="Click here to Show Report" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <%--<div class="col-12">
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
                        </div>
                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnReportnew" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

