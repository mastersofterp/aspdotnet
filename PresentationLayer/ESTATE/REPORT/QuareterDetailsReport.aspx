<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="QuareterDetailsReport.aspx.cs"
    Inherits="ESTATE_Report_QuareterAllotmentReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updQuarterReport" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">QUARTER ALLOTMENT AND VACANT REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Quarter Allotment and Vacant Report
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <%--Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>From Date <%--<span style="color: red;">*</span>--%>:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtFromdate" runat="server" TabIndex="12" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calextenderdatebirth" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgCal"
                                                            TargetControlID="txtFromdate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="msedatebirth" runat="server"
                                                            AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                            TargetControlID="txtFromdate" />
                                                        <div class="input-group-addon">
                                                            <%--<asp:Image ID="imgCal" runat="server" Height="18px"
                                                                ImageUrl="~/images/calendar.png" Style="cursor: pointer" Width="16px" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgCal" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                    <label>To Date <%--<span style="color: red;">*</span>--%>:</label>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtTodate" runat="server" Style="margin-left: 0px"
                                                            TabIndex="13" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Enabled="True" Format="dd/MM/yyyy"
                                                            PopupButtonID="imgCal1" TargetControlID="txtTodate" />
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                            AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                                                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                                                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                                                            TargetControlID="txtTodate" />
                                                        <div class="input-group-addon">
                                                           <%-- <asp:Image ID="imgCal1" runat="server" ImageUrl="~/images/calendar.png"
                                                                Style="cursor: pointer" />--%>
                                                            <asp:ImageButton runat="Server" ID="imgCal1" ImageUrl="~/images/calendar.png" AlternateText="Click to show calendar" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-2">
                                                </div>
                                                <div class="col-md-10">
                                                    <asp:Button ID="btnQrtAllotment" runat="server" OnClick="btnQrtAllotment_Click" Text="Quarter Allotment" CssClass="btn btn-primary" TabIndex="14" ToolTip="Please From Date and TO Date" />
                                                    <asp:Button ID="btnQrtVacant" runat="server" OnClick="btnQrtVacant_Click" ToolTip="Please From Date and TO Date" Text="Vacated Quarter" TabIndex="15" CssClass="btn btn-primary" />
                                                    <asp:Button ID="btnUnoccupied" runat="server" Text="Un Occupied Quarter" CssClass="btn btn-primary" ToolTip="Please Do not Select Date" TabIndex="16" OnClick="btnUnoccupied_Click" />
                                                    <asp:Button ID="btnreset" runat="server" OnClick="btnreset_Click" Text="Reset" CssClass="btn btn-warning" TabIndex="16" />
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
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

