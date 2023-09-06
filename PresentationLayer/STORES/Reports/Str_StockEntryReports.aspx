<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_StockEntryReports.aspx.cs" Inherits="STORES_Reports_Str_StockEntryReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--    <script src="../Scripts/jquery.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-impromptu.2.6.min.js" type="text/javascript"></script>

    <link href="../Scripts/impromptu.css" rel="stylesheet" type="text/css" />--%>

    <script language="javascript" type="text/javascript">
        function confirmDeleteResult(v, m, f) {
            if (v) //user clicked OK 
                $('#' + f.hidID).click();
        }

    </script>

    <asp:UpdatePanel ID="updpnlMain" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Stock Entry Reports</h3>
                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                   <%-- <div class="sub-heading">
                                        <h5>Stock Entry Reports</h5>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-12">
                                            <asp:Panel ID="Panel_Confirm" runat="server" CssClass="Panel_Confirm" EnableViewState="false"
                                                Visible="false">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Label ID="Label_ConfirmMessage" runat="server" Style="font-family: Verdana; font-size: 11px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Report Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" runat="server" data-select2-enable="true" CssClass="form-control" TabIndex="4" ToolTip="Select Report">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Security Pass Report" Value="StrSecurityPassReport.rpt"></asp:ListItem>
                                                <asp:ListItem Text="GRN Report" Value="StrGRNReport.rpt"></asp:ListItem>
                                                <asp:ListItem Text="Invoice Report" Value="StrInvoiceReport.rpt"></asp:ListItem>
                                                <asp:ListItem Text="Vendor Payment Report" Value="StrVendorPaymentReport.rpt"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgFromDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ToolTip="Enter From Date" TabIndex="1" Text=""></asp:TextBox>


                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>

                                                  <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meFromDate"
                                                    ControlToValidate="txtFromDate" InvalidValueMessage="From Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None"  EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="From Date Is Invalid (Enter In dd/MM/yyyy Format)" ValidationGroup="Store"
                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true" />

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>To Date:</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="imgToDate">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" Text="" CssClass="form-control" ToolTip="Enter To Date"></asp:TextBox>

                                                <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtToDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgToDate"
                                                    TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meToDate"
                                                    ControlToValidate="txtToDate" InvalidValueMessage="To Date Is Invalid (Enter In dd/MM/yyyy Format)"
                                                    Display="None"  EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="To Date Is Invalid (Enter In dd/MM/yyyy Format)" ValidationGroup="Store"
                                                    SetFocusOnError="True" ErrorMessage="MaskedEditValidator2" IsValidEmpty="true" />

                                                <asp:CompareValidator ID="cmpvDate" runat="server" ErrorMessage="End Date Should Be Greater Than Or Equal To  From Date"
                                                    ControlToCompare="txtFromDate" ControlToValidate="txtToDate" Display="None" ValueToCompare="Date"
                                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="Store"></asp:CompareValidator>
                                            </div>
                                        </div>


                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnRpt" runat="server" Text="Show Report" OnClick="btnReport_Click"
                                            CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show Report" ValidationGroup="Store" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />
                                    </div>
                                </div>


                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

