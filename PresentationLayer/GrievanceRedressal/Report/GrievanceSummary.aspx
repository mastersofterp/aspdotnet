<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GrievanceSummary.aspx.cs" Inherits="GrievanceRedressal_Report_GrievanceSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>
            <div class="col-md-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">GRIEVANCE SUMMARY</h3>
                        <p class="text-center">
                    </div>
                    <div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-12">
                                    Note <b>:</b> <span style="color: #FF0000">* Marked Is Mandatory !</span>
                                    <asp:Panel ID="pnlMain" runat="server">
                                        <div class="panel panel-info">
                                            <div class="panel panel-heading">Grievance Summary</div>
                                            <div class="panel panel-body">
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: red;"></span>From Date  :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="ImgBntCalc" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                                Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtSDate" runat="server" CssClass="form-control" ToolTip="Enter From Date"
                                                            ValidationGroup="Pending" AutoPostBack="true" TabIndex="1" Style="z-index: 0;" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight"
                                                            TargetControlID="txtSDate" PopupButtonID="ImgBntCalc">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <%--<asp:RequiredFieldValidator ID="rfvSDate" runat="server" ControlToValidate="txtSDate"
                                                        Display="None" ErrorMessage="From Date is Required" ValidationGroup="Pending"></asp:RequiredFieldValidator>--%>
                                                        <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtSDate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" 
                                                        ControlExtender="meeBillToDate" ControlToValidate="txtSDate" IsValidEmpty="true"
                                                        InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="Pending" SetFocusOnError="true" />--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-md-4">
                                                    <label><span style="color: red;"></span>To Date :</label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                                Style="cursor: pointer" />
                                                        </div>
                                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ToolTip="Enter To Date"
                                                            ValidationGroup="Pending" AutoPostBack="true" TabIndex="2" Style="z-index: 0;" />
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight"
                                                            TargetControlID="txtEndDate" PopupButtonID="Image1">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <%--  <asp:RequiredFieldValidator ID="rfvEDate" runat="server" ControlToValidate="txtEndDate"
                                                        ErrorMessage="To Date is Required" ValidationGroup="Pending" Display="None"></asp:RequiredFieldValidator>--%>
                                                        <%--<ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date"
                                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtEndDate" />
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" 
                                                        ControlExtender="meeBillToDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                        InvalidValueMessage="To Date is invalid" Display="None" TooltipMessage="Input a date"
                                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="Pending" SetFocusOnError="true" />--%>
                                                    </div>
                                                </div>


                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Grievance Type:</label>
                                                    <asp:DropDownList ID="ddlGrivType" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="true"
                                                        ToolTip="Select Grievance Type">
                                                        <asp:ListItem Value="0">Select Status</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvGrievanceType" runat="server" ControlToValidate="ddlGrivType" InitialValue="0"
                                                        Display="None" ValidationGroup="Pending" ErrorMessage="Please Select Grievance Type" SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-md-4">
                                                    <label><span style="color: #FF0000">*</span>Department:</label>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="true"
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
                                </div>
                                
                            </div>
                        </form>
                    </div>
                    <div class="box-footer">
                        <div class="col-md-12">
                            <p class="text-center">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show" TabIndex="5" ValidationGroup="Pending"
                                    CssClass="btn btn-info" ToolTip="Click here to Show Compeleted Grievance Report" OnClick="btnShowReport_Click" />&nbsp;                           
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6" CausesValidation="False"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Pending" ShowMessageBox="true"
                                    ShowSummary="false" DisplayMode="List" />
                            </p>
                        </div>

                        <div class="col-md-12">
                                    <asp:Panel ID="pnlGrievance" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvGrApplication" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <h4 class="box-title">Grievance Application Details
                                                    </h4>
                                                    <table class="table table-bordered table-hover">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="width: 15%;">Application No.
                                                                </th>
                                                                <th style="width: 15%;">Application Date.
                                                                </th>
                                                                <th style="width: 40%;">Grievance 
                                                                </th>
                                                                <th style="width: 10%;">Closing Date
                                                                </th>
                                                                <th style="width: 10%;">Status
                                                                </th>
                                                                <th style="width: 10%;">Print
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" style="width: 50%" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 15%;">
                                                        <%# Eval("GRIV_CODE")%>
                                                    </td>
                                                    <td style="width: 15%;">
                                                        <%# Eval("GR_APPLICATION_DATE", "{0:dd-MMM-yyyy}")%>                                                   
                                                    </td>
                                                    <td style="width: 40%;">
                                                        <%# Eval("GRIEVANCE")%>                                                   
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <%# Eval("GR_CLOSING_DATE", "{0:dd-MMM-yyyy}")%>                                                                                                  
                                                    </td>
                                                    <td style="width: 10%;">
                                                        <%# Eval("GRSTATUS")%>                                                    
                                                    </td>
                                                    <td style="width: 10%;">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

