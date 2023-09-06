<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="TPPlacementStatus.aspx.cs" Inherits="TRAININGANDPLACEMENT_Reports_TPPlacementStatus"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">PLACEMENT STATUS</h3>
                </div>

                <div class="box-body">
                    <div class="col-12" id="Div2">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Schedule Dates From</label>
                                </div>

                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <div class="fa fa-calendar"></div>
                                    </div>

                                    <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFromdt" runat="server" ControlToValidate="txtFromdt"
                                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="Schedule"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <%--<asp:Image ID="imgCalFromdt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                    <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromdt"
                                        PopupButtonID="txtFromdt" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server" TargetControlID="txtFromdt"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevFromdt" runat="server" ControlExtender="meeFromdt"
                                        ControlToValidate="txtFromdt" EmptyValueMessage="Please Enter From Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                        Display="None" TooltipMessage="Please Enter From Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Schedule Dates To</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <div class="fa fa-calendar"></div>
                                    </div>
                                    <asp:TextBox ID="txtTodt" runat="server" MaxLength="10" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtTodt"
                                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="Schedule"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <%-- <asp:Image ID="imgCalTodt" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CeTodt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodt"
                                        PopupButtonID="txtTodt" Enabled="true" EnableViewState="true">
                                    </ajaxToolKit:CalendarExtender>
                                    <ajaxToolKit:MaskedEditExtender ID="meeTodt" runat="server" TargetControlID="txtTodt"
                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                        AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevTodt" runat="server" ControlExtender="meeTodt"
                                        ControlToValidate="txtTodt" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To Date is Invalid (Enter dd/MM/yyyy Format)"
                                        Display="None" TooltipMessage="Please Enter To Date" EmptyValueBlurredText="Empty"
                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Schedule" SetFocusOnError="true"></ajaxToolKit:MaskedEditValidator>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Report" ValidationGroup="Schedule" OnClick="btnShow_Click" CssClass="btn btn-primary" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Schedule"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
