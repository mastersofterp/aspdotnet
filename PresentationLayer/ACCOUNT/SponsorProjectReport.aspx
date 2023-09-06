<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SponsorProjectReport.aspx.cs" Inherits="ACCOUNT_SponsorProjectReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <style type="text/css">
        .account_compname {
            font-weight: bold;
            margin-left: 200px;
        }
    </style>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div4" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Sponsor Project Report</h3>
                    <%--  <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />--%>
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-12 ">
                        <div class="form-group col-12">
                            <div id="divCompName" runat="server" style="font-size: x-large; text-align: center;">
                            </div>
                        </div>
                        <div id="div1" runat="server" class="account_compname">
                        </div>
                    </div>
                    <div class="col-12">
                        <div id="fldBillFile" runat="server">
                            <div class="row mt-3">
                                <div class="form-group col-lg-3 col-md-6 col-12 ">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCal">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFrmDate" runat="server"
                                            AutoPostBack="True" />
                                        <AjaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                            Format="dd/MM/yyyy" PopupButtonID="imgCal" PopupPosition="BottomLeft" TargetControlID="txtFrmDate">
                                        </AjaxToolKit:CalendarExtender>
                                        <AjaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFrmDate">
                                        </AjaxToolKit:MaskedEditExtender>
                                        <%--  <AjaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="metxtDepDate"
                                            ControlToValidate="txtFrmDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Date is Invalid (Enter mm/dd/yyyy Format)"
                                            SetFocusOnError="True" TooltipMessage="Please Enter From Date" ValidationGroup="submit"
                                            ErrorMessage="Please Enter Date in dd/MM/yyyy format" />--%>
                                        <input id="Hidden1" runat="server" type="hidden" />
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="imgCal1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>

                                        <asp:TextBox ID="txtUptoDate" runat="server"
                                            AutoPostBack="True" />

                                        <AjaxToolKit:CalendarExtender ID="txtUptoDate_CalendarExtender" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCal1" PopupPosition="BottomLeft"
                                            TargetControlID="txtUptoDate">
                                        </AjaxToolKit:CalendarExtender>
                                        <AjaxToolKit:MaskedEditExtender ID="txtUptoDate_MaskedEditExtender" runat="server"
                                            AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtUptoDate">
                                        </AjaxToolKit:MaskedEditExtender>
                                        <AjaxToolKit:MaskedEditValidator ID="mevtxtToDate" runat="server" ControlExtender="txtUptoDate_MaskedEditExtender"
                                            ControlToValidate="txtUptoDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Date"
                                            InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Date is Invalid (Enter mm/dd/yyyy Format)"
                                            SetFocusOnError="True" TooltipMessage="Please Enter To Date" ValidationGroup="submit"
                                            ErrorMessage="Please Enter Date in dd/MM/yyyy format" />
                                        <input id="hdnBal" runat="server" type="hidden" />
                                        <input id="hdnMode" runat="server" type="hidden" />
                                    </div>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Project Name </label>
                                    </div>
                                    <asp:DropDownList ID="ddlProjName" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        data-select2-enable="true">
                                        <asp:ListItem Value="0" Selected="True">--Please Select--</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDDLprojName" runat="server" InitialValue="0"
                                        ControlToValidate="ddlProjName" ValidationGroup="sponsor" ErrorMessage="Please select project name" Display="None"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:UpdatePanel ID="UPDLedger" runat="server"></asp:UpdatePanel>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnReport" CssClass="btn btn-info" runat="server" Text="Report" OnClick="btnReport_Click" ValidationGroup="sponsor" />
                                    <asp:ValidationSummary ID="vsSponsor" runat="server" ValidationGroup="sponsor" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

