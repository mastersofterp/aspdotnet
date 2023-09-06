<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="dailyworkout.aspx.cs" Inherits="Estate_dailyworkout" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">DAILY WORKOUT DETAILS</h3>
                </div>

                <div class="box-body">
                    <asp:Panel ID="pnlMain" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Start Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="ImgBntCalc" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                        
                                        <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" ValidationGroup="Pending" AutoPostBack="true" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtSDate" PopupButtonID="ImgBntCalc">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate"
                                            Display="None" ErrorMessage="Start Date is Required" ValidationGroup="Pending"></asp:RequiredFieldValidator>
                                        <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                                    ControlExtender="meeBillToDate" ControlToValidate="txtSDate" IsValidEmpty="true"
                                                    InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Pending" SetFocusOnError="true" />--%>
                                    </div>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Start Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ValidationGroup="Pending" AutoPostBack="true" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtEndDate" PopupButtonID="Image1">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ControlToValidate="txtEndDate"
                                            ErrorMessage="End Date is Required" ValidationGroup="Pending" Display="None"></asp:RequiredFieldValidator>
                                        <%--  <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" EmptyValueMessage="Please Enter Valid Event Start Date"
                                                    ControlExtender="meeBillToDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                    InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Pending" SetFocusOnError="true" />--%>
                                    </div>
                                </div>
                            </div>    
                        </div>                              
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnFeedback" runat="server" Text="Complaint Feedback Report" TabIndex="3" ValidationGroup="Pending" CssClass="btn btn-primary" OnClick="btnFeedback_Click" Visible="false"/>
                            <asp:Button ID="btnSubmit" runat="server" Text="Show Report" TabIndex="3" ValidationGroup="Pending" CssClass="btn btn-info" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Consolidate Workout Details" TabIndex="3" ValidationGroup="Pending" CssClass="btn btn-info" OnClick="btnReport_Click"  Visible="true"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />                  
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server"></div>
    <asp:UpdatePanel ID="updPnl" runat="server"></asp:UpdatePanel>
</asp:Content>
