<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrivSum.aspx.cs" Inherits="GrievanceRedressal_Report_GrivSum" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


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
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">GRIEVANCE SUMMARY</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlMain" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="ImgBntCalc" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--<asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                        Style="cursor: pointer" />--%>
                                                <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                    ValidationGroup="Pending" AutoPostBack="true" TabIndex="1" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                    TargetControlID="txtSDate" PopupButtonID="ImgBntCalc">
                                                </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate"
                                                    Display="None" ErrorMessage="From Date is Required" ValidationGroup="Pending"></asp:RequiredFieldValidator>
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
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i id="Image1" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                    Style="cursor: pointer" />--%>
                                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                    ValidationGroup="Pending" AutoPostBack="true" TabIndex="2" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" 
                                                    TargetControlID="txtEndDate" PopupButtonID="Image1">
                                                </ajaxToolKit:CalendarExtender>
                                                 <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ControlToValidate="txtEndDate"
                                                ErrorMessage="To Date is Required" ValidationGroup="Pending" Display="None"></asp:RequiredFieldValidator>
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
                                                <label>Grievance Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlGrivType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="3" AppendDataBoundItems="true"
                                                ToolTip="Select Grievance Type">
                                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvGrievanceType" runat="server" ControlToValidate="ddlGrivType" InitialValue="0"
                                                Display="None" ValidationGroup="Pending" ErrorMessage="Please Select Grievance Type" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true"
                                                ToolTip="Select Department">
                                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDepartment" InitialValue="0"
                                                Display="None" ValidationGroup="Pending" ErrorMessage="Please Select Department" SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                              
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show" TabIndex="5" ValidationGroup="Pending"
                                    CssClass="btn btn-info" ToolTip="Click here to Show Compeleted Grievance Report" OnClick="btnShowReport_Click" />                           
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" CausesValidation="False"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlGrievance" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvGrApplication" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>Grievance Application Details</h5></div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Application No.
                                                            </th>
                                                            <th>Application Date.
                                                            </th>
                                                            <th>Grievance 
                                                            </th>
                                                            <th>Closing Date
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Print
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server"/>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("GRIV_CODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("GR_APPLICATION_DATE", "{0:dd-MMM-yyyy}")%>                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("GRIEVANCE")%>                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("GR_CLOSING_DATE", "{0:dd-MMM-yyyy}")%>                                                                                                  
                                                </td>
                                                <td>
                                                    <%# Eval("GRSTATUS")%>                                                    
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" class="btn btn-primary"
                                                        CommandName='<%# Eval("GAID") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

