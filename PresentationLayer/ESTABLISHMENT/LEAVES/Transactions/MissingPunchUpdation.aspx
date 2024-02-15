<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="MissingPunchUpdation.aspx.cs" Inherits="ESTABLISHMENT_LEAVES_Transactions_MissingPunchUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">MISSING BIO METRIC PUNCH UPDATION REQUEST</h3>
                </div>
                <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div id="div1" runat="server">
                                        Note <b>:</b> <span style="color: #FF0000">Please Enter In Time and Out Time only on 24hrs format.</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                <div class="box-body mt-2">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Working Date</label>
                                </div>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                    </div>
                                    <asp:TextBox ID="txtWDate" runat="server" CssClass="form-control" SelectedDate="<%# DateTime.Today %>" 
                                        MaxLength="10" ToolTip="Enter Working Date" Style="z-index: 0;" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvWorkingDt" runat="server" ControlToValidate="txtWDate"
                                        Display="None" ErrorMessage="Please Enter Working Date" ValidationGroup="Holiday"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:CalendarExtender ID="ceWorkingDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtWDate"
                                        Enabled="true" EnableViewState="true" PopupButtonID="dvcal2">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meeWorkingDt" runat="server" TargetControlID="txtWDate"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevWorkingDt" runat="server" ControlExtender="meeWorkingDt"
                                        ControlToValidate="txtWDate" EmptyValueMessage="Please Enter Working Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                        Display="None" TooltipMessage="Please Enter Working Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Holiday" SetFocusOnError="true">
                                    </ajaxToolKit:MaskedEditValidator>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>In Time</label>
                                    </div>
                                    <asp:TextBox ID="txtInTime" runat="server" ToolTip="Enter In Time in 24hrs format only"
                                        CssClass="form-control" TabIndex="2" AutoPostBack="true" OnTextChanged="txtInTime_TextChanged"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="meeIn" runat="server" TargetControlID="txtInTime"
                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" UserTimeFormat="TwentyFourHour"
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="rfvInTime" runat="server" ControlToValidate="txtInTime"
                                        Display="None" ErrorMessage="Please Enter In Time" ValidationGroup="Holiday"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Out Time</label>
                                    </div>
                                    <asp:TextBox ID="txtOutTime" runat="server" ToolTip="Enter Out Time in 24hrs format only"
                                        CssClass="form-control" TabIndex="3" AutoPostBack="true" OnTextChanged="txtOutTime_TextChanged"></asp:TextBox>
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOutTime"
                                        Mask="99:99:99" MaskType="Time" AcceptAMPM="false" ErrorTooltipEnabled="True"
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" UserTimeFormat="TwentyFourHour"
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                        CultureTimePlaceholder="" Enabled="True" />

                                    <asp:RequiredFieldValidator ID="rfvOutTime" runat="server" ControlToValidate="txtOutTime"
                                        Display="None" ErrorMessage="Please Enter Out Time" ValidationGroup="Holiday"
                                        SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Reason</label>
                                </div>
                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine" TabIndex="4" />
                                <asp:RequiredFieldValidator ID="RFVReason" runat="server" ControlToValidate="txtReason"
                                    Display="None" ErrorMessage="Please Enter Valid Reason" ValidationGroup="Holiday"
                                    SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Holiday"
                            CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" TabIndex="5"/>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" TabIndex="6" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Holiday"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="PanelList" runat="server">
                            <asp:Repeater ID="RptMissPunch" runat="server">
                                <HeaderTemplate>
                                    <div class="sub-heading">
                                        <h5>Missing Punch Updation List</h5>
                                    </div>
                                    <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Working Date
                                                </th>
                                                <th>IN_TIME
                                                </th>
                                                <th>OUT_TIME
                                                </th>
                                                <th>Reason
                                                </th>
                                                <th>Status
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%# Eval("WRKDATE", "{0:dd/MM/yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("IN_TIME")%>
                                        </td>
                                        <td>
                                            <%# Eval("OUT_TIME")%>
                                        </td>
                                        <td>
                                            <%# Eval("REASON") %>
                                        </td>
                                        <td>
                                            <%# Eval("APR_STATUS") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody></table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <div id="DivNote" runat="server">
                                <div class="form-group col-sm-12">
                                    <div class="text-center">
                                        <p style="color: Red; font-weight: bold">
                                            No Record Found..!!                                                                
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
