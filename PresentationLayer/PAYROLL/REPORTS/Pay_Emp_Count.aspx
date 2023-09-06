<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Pay_Emp_Count.aspx.cs" Inherits="PAYROLL_REPORTS_Pay_Emp_Count" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EMPLOYEE COUNT REPORT</h3>
                        </div>
                        <div class="box-body">
                                <div class="col-12">
	                                    <div class="row">
		                                    <div class="col-12">
		                                    <div class="sub-heading">
				                                    <h5>Employee Count Report</h5>
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    <asp:Panel ID="pnl" runat="server">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="imgCalFromDate1" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ToolTip="Enter From Date" CssClass="form-Control" />
                                                            
                                                               <%-- <asp:Image ID="imgCalFromDate" runat="server"
                                                                    Style="cursor: hand" ImageUrl="~/IMAGES/calendar.png" />--%>
                                                            

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate1" Enabled="true" EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                Display="None" ErrorMessage="Please enter initial date for report." SetFocusOnError="true"
                                                                ValidationGroup="Payroll" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" ToolTip="Select College" data-select2-enable="true"
                                                            AppendDataBoundItems="true" TabIndex="2">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rqfCollege" runat="server" ControlToValidate="ddlCollege"
                                                            ValidationGroup="Payroll" ErrorMessage="Please select College" SetFocusOnError="true"
                                                            InitialValue="0" Display="None"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Payroll"
                                        DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                                    <asp:Button ID="btnShowReport" runat="server" Text="Show Report"
                                        OnClick="btnShowReport_Click" ValidationGroup="Payroll" CssClass="btn btn-info" TabIndex="3" ToolTip="Click To Show the Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" TabIndex="4" ToolTip="Click To Reset" CssClass="btn btn-warning" />
                                </div>                      
                            </div>
                        </div>
                    </div>
                </div>
            </div>                     
            <div id="divMsg" runat="server"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

