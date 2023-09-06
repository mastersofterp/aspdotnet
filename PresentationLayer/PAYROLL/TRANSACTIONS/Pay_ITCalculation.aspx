<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Pay_ITCalculation.aspx.cs" Inherits="Pay_ITCalculation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpanel"
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
    <asp:UpdatePanel ID="updpanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">INCOME TAX CALCULATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Employee Income Tax Calculation</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="Panel_Error" runat="server" CssClass="Panel_Error" EnableViewState="false"
                                Visible="false">
                                <table class="table table-bordered table-hover table-responsive">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/error.gif" align="absmiddle" alt="Error" />
                                        </td>
                                        <td>
                                            <font style="font-family: Verdana; font-family: 11px; font-weight: bold; color: #CD0A0A">
                                        </font>
                                            <asp:Label ID="Label_ErrorMessage" runat="server" Style="font-family: Verdana; font-size: 11px; color: #CD0A0A"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                Visible="false">
                                <table class="table table-bordered table-hover table-responsive">
                                    <tr>
                                        <td style="width: 3%; vertical-align: top">
                                            <img src="../../../images/confirm.gif" align="absmiddle" alt="confirm" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalFromDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter From Date" />
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                    Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="True"
                                                    ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter To Date" />
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Tr1" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select College" data-select2-enable="true"
                                                AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="True"
                                                ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowStaffWise" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Select Staff"
                                                AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStaff"
                                                Display="None" ErrorMessage="Please Select Staff" SetFocusOnError="True"
                                                ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    <asp:RadioButtonList ID="rblCalculationBy" runat="server" AutoPostBack="true"
                                                        RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rblCalculationBy_SelectedIndexChanged">
                                                        <asp:ListItem Enabled="true" Selected="True" Text="All Employees" Value="0" TabIndex="6"></asp:ListItem>
                                                        <asp:ListItem Enabled="true" Text="Particular Employee" Value="1" TabIndex="7"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trcalulationby" runat="server">
                                            <div class="label-dynamic">
                                                <label>
                                                    <asp:Label runat="server" ID="lblCalculationBy" Text="Select Employee:" Font-Bold="true" /></label>
                                            </div>
                                            <asp:DropDownList ID="ddlCalculationBy" runat="server" CssClass="form-control" ToolTip="Select Employee" TabIndex="8" AppendDataBoundItems="true" data-select2-enable="true">
                                            </asp:DropDownList>

                                            <asp:CheckBox ID="chkStaffWise" runat="server" Checked="false" Text="Staff Wise" AutoPostBack="true"
                                                Visible="false" OnCheckedChanged="chkStaffWise_CheckedChanged" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>
                                                    <asp:CheckBox ID="chkProposedSalary" TabIndex="2" ToolTip="Consider Proposed Salary" runat="server" Text="Consider Proposed Salary" /></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnCalculate" runat="server" Text="Calculate Income Tax"
                                    CssClass="btn btn-primary" ValidationGroup="submit"
                                    TabIndex="8" ToolTip="Click To Calculate Income Tax"
                                    OnClick="btnCalculate_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" ToolTip="Click To Reset"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
