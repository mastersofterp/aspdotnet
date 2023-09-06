<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_ITReports.aspx.cs" Inherits="Pay_ITReports" %>

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
                            <h3 class="box-title">INCOME TAX REPORTS</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Employee Income Tax Reports</h5>
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
                                        <td style="width: 97%">
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
                                        <td style="width: 97%">
                                            <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAll" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From</label>
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
                                                <label>To</label>
                                            </div>
                                            <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalToDate" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter To Date" />
                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                                                    PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meeToData" runat="server" TargetControlID="txtToDate" MaskType="Date" Mask="99/99/9999"></ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">                          
                                        <asp:RadioButtonList ID="rblCalculationBy" runat="server" AutoPostBack="true"
                                                RepeatDirection="Horizontal" TabIndex="3" ToolTip="College Wise"
                                                OnSelectedIndexChanged="rblCalculationBy_SelectedIndexChanged" Visible="false">
                                                <asp:ListItem Enabled="true" Selected="True" Text="College Wise" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>                                           
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="trcalculationby" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>
                                                    <asp:Label runat="server" ID="lblCalculationBy" Text="Select Staff" /></label>
                                            </div>
                                            <asp:DropDownList ID="ddlCalculationBy" runat="server" CssClass="form-control" ToolTip="Select Staff Type" TabIndex="4" AppendDataBoundItems="true" data-select2-enable="true">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCalculationBy" ControlToValidate="ddlCalculationBy" runat="server" SetFocusOnError="True" ValidationGroup="submit"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select College Type"></asp:RequiredFieldValidator>

                                            <asp:CheckBox ID="chkStaffWise" runat="server" Checked="false" Text="Staff Wise" AutoPostBack="true"
                                                Visible="true" OnCheckedChanged="chkStaffWise_CheckedChanged" TabIndex="5" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Order By</label>
                                            </div>
                                            <asp:DropDownList ID="rblOrderBy" runat="server" AutoPostBack="True" TabIndex="6" ToolTip="Select Order by" data-select2-enable="true"
                                                OnSelectedIndexChanged="rblOrderBy_SelectedIndexChanged">
                                                <asp:ListItem Text="ID No." Value="IDNO"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Employee Name" Value="FName"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Sequence Number" Value="SEQ_NO"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Value="PFILENO">Employee Number</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowCollege" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="7" ToolTip="Select College"
                                                AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" ControlToValidate="ddlCollege" runat="server" SetFocusOnError="True" ValidationGroup="submit"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select College"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="rowStaffWise" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStaff" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="true" TabIndex="9" ToolTip="Select Staff Type"
                                                OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvStaff" ControlToValidate="ddlStaff" runat="server" SetFocusOnError="True" ValidationGroup="submit"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Staff"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="lblemp" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmp" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="9" ToolTip="Select Employee">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmp" ControlToValidate="ddlEmp"
                                                runat="server" SetFocusOnError="True" ValidationGroup="submit1"
                                                InitialValue="0" Display="None" ErrorMessage="Please Select Employee Name"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divchkamt" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>
                                                    <asp:CheckBox ID="chkDontShowNegative" TabIndex="10" ToolTip="Don't Show Negative Amounts" runat="server" Text="Don't Show Negative Amounts" Checked="false" />
                                                    <br />
                                                    <asp:CheckBox ID="chkDontShowAmount" runat="server" TabIndex="11" ToolTip="Don't Show Amounts" Text="Don't Show Amounts" Checked="false" />
                                                    <br />
                                                    <asp:CheckBox ID="chkDontShowZero" runat="server" TabIndex="12" ToolTip="Don't Show Zeros" Text="Don't Show Zeros" Checked="false" />
                                                </label>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divchklstopt" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <label>Options for List</label>
                                            </div>
                                            <asp:RadioButtonList ID="rblOptions" runat="server" RepeatDirection="Vertical"
                                                RepeatColumns="2">
                                                <asp:ListItem Enabled="true" Selected="True" Text="None" Value="TAXAMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="88 A (Relief)" Value="REBATEAMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Surcharge" Value="SURCHRG"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="HRA" Value="HRA"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="88 C (Ladies)" Value="LADDISC"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Tax Deducted" Value="TAXDED"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Other Income" Value="OTH_INC"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="88 D" Value="D88AMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="House Loan" Value="HOUSEAMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="VI A" Value="VIAMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Under 89" Value="U89AMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="NSC Int." Value="NSCAMT"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Testing And Consultancy" Value="EXPAMT"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowTax" runat="server" Text="Show Tax" ValidationGroup="submit"
                                    ToolTip="Click To Show Tax" TabIndex="26" OnClick="btnShowTax_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnTaxColumns" runat="server" Text="Tax Columns" ValidationGroup="submit"
                                    ToolTip="Click for Tax Columns" TabIndex="27" OnClick="btnTaxColumns_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnForm16" runat="server" Text="FORM 16" ValidationGroup="submit"
                                    ToolTip="Click To Generate FORM 16" TabIndex="28" OnClick="btnForm16_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnForm16partB" runat="server" Text="FORM 16 Part B" ValidationGroup="submit"
                                    ToolTip="Click To Generate FORM 16 Part B" TabIndex="29" OnClick="btnForm16partB_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btn24Q" runat="server" Text="24 Q" ValidationGroup="submit"
                                    ToolTip="Click To Generate FORM 24Q" TabIndex="30" OnClick="btn24Q_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnTaxDetails" runat="server" Text="Tax Details" ValidationGroup="submit"
                                    ToolTip="Click To Show Tax Details" TabIndex="31" OnClick="btnTaxDetails_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnRemainingIT" runat="server"
                                    Text="IT Remaining Month Amount" ValidationGroup="submit"
                                    ToolTip="Click To Show Ramaining Month IT" TabIndex="32" OnClick="btnRemainingIT_Click" CssClass="btn btn-info" />

                                <asp:Button ID="btnForm16RemAmt" runat="server"
                                    Text="IT Schedule Deduction" ValidationGroup="submit"
                                    ToolTip="Click To Show Form 16" OnClick="btnForm16RemAmt_Click" TabIndex="33" CssClass="btn btn-info" />

                                <asp:Button ID="btn26Q" runat="server"
                                    Text="26Q" ValidationGroup="submit"
                                    ToolTip="Click To Show 26Q" OnClick="btn26Q_Click" TabIndex="33" CssClass="btn btn-info" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="34" ToolTip="Click To Reset"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server"></div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
