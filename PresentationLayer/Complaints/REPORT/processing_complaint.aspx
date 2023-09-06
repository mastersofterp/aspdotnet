<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="processing_complaint.aspx.cs"
    Inherits="Estate_processing_complaint" Title="" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">PROCESSING SERVICE</h3>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <asp:Panel ID="pnl" runat="server">
                            <div class="panel panel-info">
                                <div class="panel-heading">
                                    Processing Service
                                </div>
                                <div class="panel-body">
                                    <div class="form-group row">
                                        <div class="col-md-12">
                                            Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>Start Date  <span style="color: red;">*</span>:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgSDate" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" />
                                                </div>
                                                <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" Width="110px"
                                                    Style="text-align: justify" ValidationGroup="Pending" AutoPostBack="true" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtSDate" PopupButtonID="imgSDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate" Display="None" ErrorMessage="Start Date is Required" ValidationGroup="Pending"></asp:RequiredFieldValidator>
                                                <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSDate" />
                             <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                        ControlExtender="meeBillToDate" ControlToValidate="txtSDate" IsValidEmpty="true"
                                        InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                            <label>End Date  <span style="color: red;">*</span>:</label>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" />
                                                </div>
                                                <asp:TextBox ID="txtEndDate" runat="server" Width="110px" CssClass="form-control" AutoPostBack="true"
                                                    Style="text-align: justify" ValidationGroup="Pending" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtEndDate" PopupButtonID="imgEDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ControlToValidate="txtEndDate" ErrorMessage="End Date is Required" ValidationGroup="Pending" Display="None"></asp:RequiredFieldValidator>
                                                <%-- <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndDate" />
                             <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                        ControlExtender="meeBillToDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                        InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-10">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Show Report" TabIndex="3" CssClass="btn btn-primary" ValidationGroup="Pending" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" CssClass="btn btn-warning" CausesValidation="False" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <tr>
            <td colspan="2" style="text-align:left;padding:10px">
                <CR:CrystalReportViewer ID="crViewer" runat="server" AutoDataBind="true"  
                    DisplayGroupTree="False" EnableDrillDown="False" HasCrystalLogo="False" 
                    HasDrillUpButton="False" HasGotoPageButton="False" HasRefreshButton="True" 
                    HasSearchButton="False" HasToggleGroupTreeButton="False" 
                    HasViewList="False" 
                    HasZoomFactorList="False" BorderStyle="Solid" BorderColor="Black" BorderWidth="1" />
            </td>
        </tr>    --%>


    <div id="divMsg" runat="server"></div>
    <asp:UpdatePanel ID="updPnl" runat="server"></asp:UpdatePanel>
</asp:Content>
