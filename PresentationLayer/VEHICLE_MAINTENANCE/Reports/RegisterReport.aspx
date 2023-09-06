<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RegisterReport.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Report_RegisterReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">REPAIR / SERVICING REGISTER</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlCourtName_Top" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkConsolidated" runat="server" TabIndex="1" ToolTip="Check for Consolidate Report" AutoPostBack="true"
                                        Text="Vehicle Consolidate Report" OnCheckedChanged="chkConsolidated_CheckedChanged" />

                                </div>
                            </div>
                        </div>

                        <asp:Panel ID="panConsolidate" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Select Vehicle</label>
                                        </div>
                                        <asp:DropDownList ID="ddlVehicleCons" runat="server" ValidationGroup="Consolidate"
                                            CssClass="form-control" data-select2-enable="true" ToolTip="Select Vehicle" TabIndex="2" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="Please Select Vehicle" ControlToValidate="ddlVehicleCons" ValidationGroup="Consolidate"
                                            InitialValue="0" Display="None" Text="*">                                                                
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Consolidate"
                                            ShowMessageBox="true" ShowSummary="false" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnConsReport" runat="server" ValidationGroup="Consolidate" TabIndex="3"
                                    Text="Report" OnClick="btnConsReport_Click" CssClass="btn btn-info" ToolTip="Click here for Consolidate Report" />
                                <asp:Button ID="btnConsCancel" runat="server" Text="Cancel" TabIndex="4"
                                    OnClick="btnConsCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="panServicing" runat="server" Visible="true">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Vehicle Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlvehical" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Vehicle Name" TabIndex="5" Enabled="true" ValidationGroup="Submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Workshop</label>
                                        </div>
                                        <asp:DropDownList ID="ddlworkshop" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Workshop Name" TabIndex="6" Enabled="true" ValidationGroup="Submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Bill</label>
                                        </div>
                                        <asp:DropDownList ID="ddlbillno" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                            ToolTip="Select Bill Number" TabIndex="7" Enabled="true" ValidationGroup="Submit">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImgBntCalc">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control" ToolTip="Select From Date"
                                                TabIndex="8" ValidationGroup="Submit" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtfrmDate" PopupButtonID="ImgBntCalc">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txtfrmDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server" ControlExtender="medt"
                                                ControlToValidate="txtfrmDate" EmptyValueMessage="Please Enter Date" IsValidEmpty="true"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                Display="None" Text="*" ValidationGroup="Submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>To Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control" ToolTip="Select To Date"
                                                TabIndex="9" ValidationGroup="Submit" Style="z-index: 0;" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txttoDate" PopupButtonID="Image1">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="med2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                OnInvalidCssClass="errordate" TargetControlID="txttoDate" ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="med2"
                                                ControlToValidate="txttoDate" EmptyValueMessage="Please Enter Date" IsValidEmpty="true"
                                                ErrorMessage="Please Enter Valid Date In [dd/MM/yyyy] format" InvalidValueMessage="Please Enter Valid Date In [dd/MM/yyyy] format"
                                                Display="None" Text="*" ValidationGroup="Submit">
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>




                            <div class="form-group col-12 btn-footer">
                                <asp:Button ID="btnReport" Text="Report" CssClass="btn btn-info" ToolTip="Click here to Show Report"
                                    runat="server" OnClick="btnReport_Click" ValidationGroup="Submit" TabIndex="10" />
                                <asp:Button ID="btnCancel" Text="Cancel" CssClass="btn btn-warning" ToolTip="Click here to Reset"
                                    runat="server" OnClick="btnCancel_Click" TabIndex="11" />
                                <asp:Label ID="lblReq" runat="server" SkinID="Errorlbl" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" />

                            </div>
                        </asp:Panel>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>


    <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl"></asp:Label>
    <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg"></asp:Label>



</asp:Content>
