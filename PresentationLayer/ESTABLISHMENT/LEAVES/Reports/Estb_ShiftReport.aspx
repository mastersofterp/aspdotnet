<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Estb_ShiftReport.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Reports_Estb_ShiftReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updAll" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SHIFT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>Select Criteria for Shift Report</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server">
                                            <div class="label-dynamic">
                                                <%--<sup>*</sup>--%>
                                                <label>College</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" data-select2-enable="true"
                                                CssClass="form-control" ToolTip="Select College" AutoPostBack="true">
                                            </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="Permission" InitialValue="0">
                                            </asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divstafftype" runat="server">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstafftype" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="3" AutoPostBack="True" ToolTip="Select Staff">
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlstafftype"
                                                Display="None" ErrorMessage="Please Select Staff Type" SetFocusOnError="true" ValidationGroup="shiftreport" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                   <%--     <div class="form-group col-lg-3 col-md-6 col-12" id="trddldept" runat="server">
                                            <div class="label-dynamic">
                                                <label>Select Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                CssClass="form-control" TabIndex="2" AutoPostBack="True" ToolTip="Select Department">
                                            </asp:DropDownList>
                                        </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divfromdate" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImaCalStartDate" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromdt" runat="server" onblur="return checkdate(this);" Style="z-index: 0;"
                                                    CssClass="form-control" TabIndex="6" ToolTip="Enter Shift From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtFromdt">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvfdate" runat="server" ControlToValidate="txtFromdt"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                                    ValidationGroup="shiftreport"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromdt"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromdt" InvalidValueMessage="From Date is Invalid (Enter -dd/mm/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="shiftreport" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="imgCalTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodt" runat="server" MaxLength="7" Style="z-index: 0;"
                                                    CssClass="form-control" TabIndex="7" ToolTip="Enter Shift To Date" AutoPostBack="true" OnTextChanged="txtTodt_TextChanged" />
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="shiftreport">
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
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="shiftreport" SetFocusOnError="True" IsValidEmpty="false" InitialValue="__/__/____" />
                                            </div>
                                        </div>



                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="shiftreport" TabIndex="9"
                                    CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnReport_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="shiftreport"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>