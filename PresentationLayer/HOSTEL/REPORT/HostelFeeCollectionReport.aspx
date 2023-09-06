<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HostelFeeCollectionReport.aspx.cs" Inherits="HOSTEL_REPORT_HostelFeeCollectionReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel fee collection</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session No. </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelSessionNo" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                    TabIndex="1" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvHostelSessionNo" runat="server" ErrorMessage="Please Select Hostel Session"
                                    ControlToValidate="ddlHostelSessionNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Name </label>
                                </div>
                                <asp:DropDownList ID="ddlHostelNo" runat="server" CssClass="form-control"
                                    AppendDataBoundItems="True" AutoPostBack="True" data-select2-enable="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Hostel Name"
                                    ControlToValidate="ddlHostelNo" Display="None" InitialValue="0" ValidationGroup="Submit" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>From Date  </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromdate" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Enter From Date" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromdate"
                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromdate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromdate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please enter date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtFromdate" IsValidEmpty="false"
                                        InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />
                                </div>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>To Date  </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtTodate" runat="server" TabIndex="4" CssClass="form-control" ToolTip="Enter To Date" AutoPostBack="true" OnTextChanged="txtTodate_TextChanged" />
                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtTodate"
                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtTodate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtTodate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please enter date."
                                        ControlExtender="meFromDate" ControlToValidate="txtTodate" IsValidEmpty="false"
                                        InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                   
                                    <label>Payment Type </label>
                                </div>       
                                      <asp:DropDownList ID="ddlpaymenttype" runat="server" CssClass="form-control"
                                    TabIndex="1" data-select2-enable="true">
                                          <asp:ListItem Value="">Please Select</asp:ListItem>
                                          <asp:ListItem Value="C">Counter</asp:ListItem>
                                          <asp:ListItem Value="O">Online</asp:ListItem>
                                </asp:DropDownList>          
                                 <%--<select name="" class='form-control select2 toggle-exam-type' id="ddlTestType" tabindex="1" ranat="server">
                                    <option value="0">Please Select</option>
                                    <option value="1">Counter</option>
                                    <option value="2">Online</option>
                                  </select>  --%>                         
                            </div>

                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnHostelwiseFee" runat="server" Text="Hostel Wise Fee Collection" ToolTip="Report Hostel wise student"
                            TabIndex="8" ValidationGroup="Submit" OnClick="btnHostelwiseFee_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnHostelWiseExcelReport" runat="server" Text="Hostel Wise Excel Report" ToolTip="Report Hostel wise student"
                            TabIndex="9" ValidationGroup="Submit" OnClick="btnHostelWiseExcelReport_Click"  CssClass="btn btn-info" />
                        <asp:Button ID="btnDatewiseFee" runat="server" Text="Date Wise Fee Collection" ToolTip="Report Date wise fee collection"
                            TabIndex="10" ValidationGroup="Submit" OnClick="btnDatewiseFee_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnDateWiseExcelReport" runat="server" Text="Date Wise Excel Report" ToolTip="Report Date wise fee collection"
                            TabIndex="11" ValidationGroup="Submit" OnClick="btnDateWiseExcelReport_Click"  CssClass="btn btn-info" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel" TabIndex="9" OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                        <asp:ValidationSummary ID="vsStudent" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    
</asp:Content>

