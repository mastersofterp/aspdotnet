<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelGatepassReport.aspx.cs" Inherits="HOSTEL_GATEPASS_HostelGatepassReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .custom-modal-dialog {
            max-width: 800px; /* Set the desired width here */
        }

        .btn-primary {
            margin-left: 21px;
        }

        .btn-danger {
            margin-left: 7px;
        }

        .btn-success {
        }
    </style>
    <asp:UpdatePanel ID="updmessAppl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <%--<div class="box-header with-border" style="display: flex;">
                    <h3 class="box-title">Hostel Gate Pass Report</h3>
                    <%--                    <asp:Button ID="btnAddGatepass" runat="server" Text="Add Gate Pass" Height="20%" />--%>
                    
            <div class="box-header with-border">                <%--lblDynamicPageTitle Added By Himanshu tamrakar 23-02-2024--%>
                <h3 class="box-title" style="text-transform: uppercase;">
                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
            </div>

                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>From Date </label>
                                        <b style="color: red;">*</b>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="5" ToolTip="Enter Out Date" AutoPostBack="true" CssClass="form-control" />
                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtFromDate" PopupButtonID="imgFromDate" Enabled="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" ErrorTooltipEnabled="false" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" EmptyValueMessage="Please enter To date."
                                            ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                            InvalidValueMessage="Out Date  is invalid" Display="None" TooltipMessage="Input a date"
                                            ErrorMessage="Please Select Out Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="submit" SetFocusOnError="true" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>To Date </label>
                                        <b style="color: red;">*</b>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" TabIndex="5" ToolTip="Enter Out Date" AutoPostBack="true" CssClass="form-control" />
                                        <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtToDate" PopupButtonID="imgFromDate" Enabled="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtToDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" ErrorTooltipEnabled="false" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please enter From Date."
                                            ControlExtender="MaskedEditExtender1" ControlToValidate="txtToDate" IsValidEmpty="false"
                                            InvalidValueMessage="In Date  is invalid" Display="None" TooltipMessage="Input a In Date"
                                            ErrorMessage="Please Select In Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="submit" SetFocusOnError="true" />
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Apply Date </label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtApplyDate" runat="server" TabIndex="1" ToolTip="Enter Date" AutoPostBack="true" CssClass="form-control" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtApplyDate" PopupButtonID="imgFromDate" Enabled="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtApplyDate"
                                            Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            MaskType="Date" ErrorTooltipEnabled="false" />

                                    </div>
                                </div>
                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    
                                    <label>InMate Code</label>
                                </div>
                                <asp:TextBox ID="txtInMateCode" runat="server" TabIndex="2" ToolTip="Enter InMate Code" AutoPostBack="true" CssClass="form-control" />
                            </div>--%>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Purpose </label>
                                    </div>
                                    <asp:DropDownList ID="ddlPurpose" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True" />
                                    <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlPurpose"
                                        Display="None" ErrorMessage="Please Select Purpose" ValidationGroup="Show" SetFocusOnError="True"
                                        InitialValue="0" />

                                </div>


                            </div>
                            <div id="Div2" class="row" runat="server">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">

                                        <label>Gate Pass Code</label>
                                    </div>
                                    <asp:TextBox ID="txtGatePassCode" runat="server" TabIndex="4" ToolTip="Enter Gate Pass Code" AutoPostBack="true" CssClass="form-control" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic ">
                                        <label>Status </label>
                                    </div>
                                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="3" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="True">
                                        <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        <asp:ListItem Value="A" Text="Approve"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                                        <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RfvStatus" runat="server" ControlToValidate="ddlStatus"
                                        Display="None" ValidationGroup="Show"
                                        InitialValue="0" />
                                </div>

                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row text-center" style="align-items: center;">
                                <div class="col-12">
                                    <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-primary" ValidationGroup="submit" OnClick="btnReport_Click" Width="68px" />
                                    <asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" CssClass="btn btn-success" ValidationGroup="submit" Width="100px" OnClick="btnExcelReport_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            </div>
    <div id="divMsg" runat="server"></div>





            <script src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
            <script src="https://cdn.datatables.net/1.10.16/js/dataTables.bootstrap.min.js"></script>
            <script src="https://cdn.datatables.net/responsive/2.2.0/js/dataTables.responsive.min.js"></script>
            <script src="https://cdn.datatables.net/responsive/2.2.0/js/responsive.bootstrap.min.js"></script>
            <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
            <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


