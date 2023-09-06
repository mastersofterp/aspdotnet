<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GRDailyWorkout.aspx.cs" Inherits="GrievanceRedressal_Report_GRDailyWorkout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <%--<script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>

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
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE DETAILS REPORT</h3>
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
                                                <%--<asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                    Style="cursor: pointer" />--%>
                                                <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" ToolTip="Enter Start Date"
                                                    ValidationGroup="Pending" AutoPostBack="true" TabIndex="1" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" 
                                                    TargetControlID="txtSDate" PopupButtonID="ImgBntCalc">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate"
                                                    Display="None" ErrorMessage="Start Date is Required" ValidationGroup="Pending" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                   
                                                <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
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
                                                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                        Style="cursor: pointer" />--%>
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ToolTip="Enter End Date"
                                                    ValidationGroup="Pending" AutoPostBack="true" TabIndex="2"  />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtEndDate" PopupButtonID="Image1">
                                                </ajaxToolKit:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ControlToValidate="txtEndDate"
                                                    ErrorMessage="End Date is Required" ValidationGroup="Pending" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndDate" />
                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" 
                                                    ControlExtender="meeBillToDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                    InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                    ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                    ValidationGroup="Pending" SetFocusOnError="true" />--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Select Status of Grievance</label>
                                            </div>
                                            <asp:DropDownList ID="ddlstatusG" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true"
                                                ToolTip="Select Grievance Type">
                                                 <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                   <asp:ListItem Value="1">Completed</asp:ListItem>
                                                   <asp:ListItem Value="2">In Process</asp:ListItem>
                                                   <asp:ListItem Value="3">Pending</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvGrievanceType" runat="server" ControlToValidate="ddlstatusG" InitialValue="0"
                                                Display="None" ValidationGroup="Pending" ErrorMessage="Please Select Status of Grievance Type "  SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <%--<asp:Button ID="btnShowReport" runat="server" Text="Show  Report" TabIndex="3" ValidationGroup="Pending"
                                    CssClass="btn btn-info" ToolTip="Click here to get report" OnClick="btnShowReport_Click" /> --%>    
                                <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="4" ValidationGroup="Pending"
                                   CssClass="btn btn-info" ToolTip="Click to get report" OnClick="btnReport_Click" />                
                                
                                <asp:Button ID="btnConsolidated" runat="server" Text="Consolidated Grievance Report" TabIndex="5" 
                                    CssClass="btn btn-info" ToolTip="Click here to Show Compeleted Grievance Report" OnClick="btnConsolidated_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" CausesValidation="False"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>
             <asp:PostBackTrigger ControlID="btnReport" />
         </Triggers>--%>
    </asp:UpdatePanel>  
</asp:Content>

