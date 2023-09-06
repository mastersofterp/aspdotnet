<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ComplaintStatusReport.aspx.cs" Inherits="Complaints_REPORT_ComplaintStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">SERVICE REQUEST STATUS REPORTS</h3>
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
                                        <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" ValidationGroup="Pending" AutoPostBack="true" TabIndex="1"/>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtSDate" PopupButtonID="ImgBntCalc">
                                        </ajaxToolKit:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate"
                                            Display="None" ErrorMessage="Start Date is Required" ValidationGroup="Pending"></asp:RequiredFieldValidator>
                                                <%--   <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
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
                                        <label>End Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ValidationGroup="Pending" AutoPostBack="true" TabIndex="2"/>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" 
                                            TargetControlID="txtEndDate" PopupButtonID="Image1" >
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
                                
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Officer-In-Charge</label>
                                    </div>
                                    <asp:DropDownList ID="ddlOfficer" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Service Request Status</label>
                                    </div>
                                    <asp:DropDownList ID="ddlComplaintStatus" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">Pending</asp:ListItem>
                                        <asp:ListItem Value="2">In-Process</asp:ListItem>
                                        <asp:ListItem Value="3">Complete</asp:ListItem>
                                        <asp:ListItem Value="4">Allotted</asp:ListItem>
                                        <asp:ListItem Value="5">Declined</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                                    
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnSubmit" runat="server" Text="Show Report" TabIndex="4" ValidationGroup="Pending" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnFeedback" runat="server" Text="Complaint Feedback Report" TabIndex="4" ValidationGroup="Pending" CssClass="btn btn-primary" OnClick="btnFeedback_Click" Visible="false" />
                            
                            <asp:Button ID="btnExport" runat="server" Text="Export To Excel" TabIndex="5" ValidationGroup="Pending" CssClass="btn btn-info" OnClick="btnExport_Click" Visible="false" />
                            <asp:Button ID="btnReport" runat="server" Text="Consolidate Workout Details" TabIndex="5" ValidationGroup="Pending" CssClass="btn btn-info" OnClick="btnReport_Click" Visible="false" />
                            
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                        </div>
                        <%--<div class="form-group row">
                                        <div class="form-group col-md-12">
                            <div class="col-md-12 table-responsive" id="div2" runat="server" visible="false">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="500px">
                                    <table class="table table-bordered table-hover">
                                        <tr>
                                            <td>
                                                <asp:ListView ID="grdPFMSReport" runat="server">
                                                   
                                                    <LayoutTemplate>
                                                        <div id="lgv1">

                                                            <table class="table table-bordered table-hover">
                                                                <thead>
                                                                    <tr class="bg-light-blue">

                                                                        <th>Receiving Party Code
                                                                        </th>
                                                                     
                                                                        <th>Receiving Party Name    
                                                                        </th>
                                                                        <th>Transaction Code
                                                                        </th>
                                                                        <th>Transaction Key
                                                                        </th>
                                                                        <th>Component Code
                                                                        </th>
                                                                        <th>Expense Type
                                                                        </th>
                                                                        <th>Amount
                                                                        </th>
                                                                         <th>Remarks
                                                                        </th>
                                                                        <th>Action Type
                                                                        </th>
                                                                        <th>Account Number
                                                                        </th>
                                                                        <th>Payment Method
                                                                        </th>
                                                                        <th>NarrationForPassBook
                                                                        </th>
                                                                       
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            
                                                            <td>
                                                                <%# Eval("[Receiving Party Code]")%>
                                                            </td>
                                                             <td>
                                                                <%# Eval("Receiving Party Name ")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Transaction Code")%>
                                                            </td>

                                                           
                                                            <td>
                                                                <%# Eval("Transaction Key")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("Component Code")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("Expense Type")%>
                                                            </td>
                                                           <td>
                                                                <%# Eval("Amount")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Remarks")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("Action Type")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Account Number")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("Payment Method")%>
                                                            </td>

                                                            <td>
                                                                <%# Eval("NarrationForPassBook")%>
                                                            </td>
                                                           
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                               
                            </div>
                        </div>--%>
                          
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div id="divMsg" runat="server"></div>
    <asp:UpdatePanel ID="updPnl" runat="server"></asp:UpdatePanel>
</asp:Content>

