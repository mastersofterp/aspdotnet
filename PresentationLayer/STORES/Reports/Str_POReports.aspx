<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_POReports.aspx.cs" Inherits="STORES_Reports_Str_POReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--  <script src="../Scripts/jquery.js" type="text/javascript"></script>

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
                            <h3 class="box-title">PO STOCK REPORT</h3>

                        </div>


                        <div class="box-body">
                            <asp:Panel ID="pnlDSRDetails" runat="server" Visible="True">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                            <%-- <div class="sub-heading">
                                                <h5>PO Stock Report</h5>
                                            </div>--%>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>PO Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlReport" runat="server" data-select2-enable="true" CssClass="form-control" TabIndex="4" ToolTip="Select Report" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged">
                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Delivered" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Not Delivered" Value="3"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDelivered" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rblDelivered" runat="server" TabIndex="1" RepeatDirection="Horizontal" AutoPostBack="true">

                                                <asp:ListItem Enabled="true" Selected="True" Text="Delivered All &nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Late Delivered &nbsp;" Value="2"></asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-4 col-md-6 col-12" id="divNotDelivered" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label></label>
                                            </div>
                                            <asp:RadioButtonList ID="rblNotDelivered" runat="server" TabIndex="1" RepeatDirection="Horizontal" AutoPostBack="true">

                                                <asp:ListItem Enabled="true" Selected="True" Text="Not Delivered All &nbsp;" Value="1"></asp:ListItem>
                                                <asp:ListItem Enabled="true" Text="Delivered Date Crossed &nbsp;" Value="2"></asp:ListItem>

                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="popupImgFromDt">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Report"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="ceFromDate" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="popupImgFromDt" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>


                                                <ajaxToolKit:MaskedEditValidator ID="mevFrom" runat="server"
                                                    ControlExtender="meFromDate" ControlToValidate="txtFromDate" Display="None"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter In dd/MM/yyyy Format)"
                                                    ValidationGroup="Store">
                                                </ajaxToolKit:MaskedEditValidator>
                                            </div>


                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="popupImgToDt">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Select Report"></asp:TextBox>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" DisplayMoney="Left"
                                                    Enabled="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    PopupButtonID="popupImgToDt" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>


                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender1" ControlToValidate="txtToDate" Display="None"
                                                    InvalidValueBlurredMessage="Invalid Date"
                                                    InvalidValueMessage="To Date is Invalid (Enter In dd/MM/yyyy Format)"
                                                    ValidationGroup="Store">
                                                </ajaxToolKit:MaskedEditValidator>
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

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRpt" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <div>
        <asp:ValidationSummary runat="server" ID="vdReqField" DisplayMode="List" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="Store" />
    </div>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

