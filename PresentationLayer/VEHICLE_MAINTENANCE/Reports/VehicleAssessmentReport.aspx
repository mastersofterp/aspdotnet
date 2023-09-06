<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="VehicleAssessmentReport.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Reports_VehicleAssessmentReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">VEHICLE ASSESSMENT REPORT</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlPatientReport" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="ImgBntCalc">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFDate" TabIndex="1" runat="server" CssClass="form-control"
                                                    ToolTip="Enter From Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server"
                                                    Format="dd/MM/yyyy" PopupButtonID="ImgBntCalc" TargetControlID="txtFDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtFDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlToValidate="txtFDate"
                                                    ControlExtender="medt" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    IsValidEmpty="true" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit">                                                                       
                                                </ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvFDate" ControlToValidate="txtFDate" Display="None"
                                                    ErrorMessage="Please Enter From Date " ValidationGroup="Submit" />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image1456">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTDate" runat="server" TabIndex="2" CssClass="form-control"
                                                    ToolTip="Enter TO Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTDate" PopupButtonID="Image1456">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtTDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtTDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                    Display="None" Text="*" ValidationGroup="Submit"> </ajaxToolKit:MaskedEditValidator>
                                                <asp:RequiredFieldValidator runat="server" ID="rfvTDate" ControlToValidate="txtTDate" Display="None"
                                                    ErrorMessage="Please Enter To Date " ValidationGroup="Submit" />
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnRport" runat="server" Text="Report" TabIndex="3" ValidationGroup="Submit"
                                        CssClass="btn btn-info" ToolTip="Click here to Show Report" OnClick="btnRport_Click" />                             
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="4"
                                                                CssClass="btn btn-warning" ToolTip="Click here to Reset" CausesValidation="false" />

                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Submit" />

                                </div>

                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


