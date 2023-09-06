<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ConsolidatedDSRReport.aspx.cs" Inherits="STORES_Reports_ConsolidatedDSRReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="updDSR" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">CONSOLIDATED DSR REPORT</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlReport" runat="server">
                                    <div class="panel panel-info">
                                        <div class="panel-heading">
                                            Consolidated DSR Report
                                        </div>
                                        <div class="panel-body">
                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <%--Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>--%>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group col-md-4">
                                                    <label>Department</label>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label>From Date <%--<span style="color: red;">*</span>--%>:</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFromDate" TargetControlID="txtFromDate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            OnInvalidCssClass="errordate" TargetControlID="txtFromDate" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender2"
                                                            ControlToValidate="txtFromDate" IsValidEmpty="False" EmptyValueMessage="From Date is required" InvalidValueMessage="From Date is Invalid (Enter In dd/MM/yyyy Format)"
                                                            Display="None" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" ValidationGroup="Chk_Date" />

                                                    </div>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label>To Date <%--<span style="color: red;">*</span>--%>:</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtTodate">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                            ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                            OnInvalidCssClass="errordate" TargetControlID="txtTodate" ClearMaskOnLostFocus="true">
                                                        </ajaxToolKit:MaskedEditExtender>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender1"
                                                            ControlToValidate="txtTodate" IsValidEmpty="False" EmptyValueMessage="From Date is required" InvalidValueMessage="To Date is Invalid (Enter In dd/MM/yyyy Format)"
                                                            Display="None" TooltipMessage="Input a date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" ValidationGroup="Chk_Date" />

                                                    </div>
                                                </div>
                                            </div>


                                            <div class="form-group col-md-12">
                                               <p style="text-align:center">
                                                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-primary" TabIndex="14" OnClick="btnreport_Click"/>
                                                    <asp:Button ID="btnCancel" runat="server" ToolTip="Clear Fields" Text="Cancel" TabIndex="15" CssClass="btn btn-warning" OnClick="btnCancel_Click"/>
                                                </p>
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


