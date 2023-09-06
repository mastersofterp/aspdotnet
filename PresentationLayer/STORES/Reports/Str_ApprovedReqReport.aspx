<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_ApprovedReqReport.aspx.cs" Inherits="STORES_Reports_Str_ApprovedReqReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

   
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">APPROVED REQUISITIONS DETAIL REPORT</h3>
                        </div>
                        <div class="box-body">
                           <%-- <asp:UpdatePanel runat="server">
                                <ContentTemplate>--%>
                                    <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>From Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="CalImage1">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calFromDt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" PopupButtonID="CalImage1"></ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtFromDate"
                                                            Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender4"
                                                            ControlToValidate="txtFromDate" InvalidValueMessage="From Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                            Display="None" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date Of Sending " ValidationGroup="submit"
                                                            SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true"></ajaxToolKit:MaskedEditValidator>
                                                    </div>



                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>To Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" id="calImage2">
                                                            <i class="fa fa-calendar text-blue"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" PopupButtonID="calImage2">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                                            Mask="99/99/9999" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True"
                                                            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                            CultureTimePlaceholder="" Enabled="True">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtToDate" InvalidValueMessage="To Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                            Display="None" EmptyValueBlurredText="Empty"
                                                            InvalidValueBlurredMessage="Invalid Date Of Sending " ValidationGroup="submit"
                                                            SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true"></ajaxToolKit:MaskedEditValidator>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" ToolTip="Please Select main department name"
                                                        AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Requisition Number</label>
                                                    </div>
                                                    <asp:DropDownList CssClass="form-control" data-select2-enable="true" ToolTip="Select Requisition Number" runat="server" ID="ddlRequisitionNo" AppendDataBoundItems="true"
                                                        TabIndex="2" OnSelectedIndexChanged="ddlReqNo_OnSelectedChanged" AutoPostBack="true">
                                                    </asp:DropDownList>

                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnPrint" runat="server" Text="Show Report" CssClass="btn btn-info" ToolTip="Click To Print" ValidationGroup="submit" TabIndex="3" OnClick="btnPrint_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click To Reset" TabIndex="4" />
                                            <asp:ValidationSummary ID="valsum" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </asp:Panel>
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlDept" />
                                    <asp:PostBackTrigger ControlID="btnPrint" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>

                    </div>

                </div>

            </div>
     
        <%-- <Triggers>                           
                            <asp:AsyncPostBackTrigger ControlID="ddlDept" />
                            <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                        </Triggers>--%>

     <asp:UpdatePanel ID="updpnlMain" runat="server">
           
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>


