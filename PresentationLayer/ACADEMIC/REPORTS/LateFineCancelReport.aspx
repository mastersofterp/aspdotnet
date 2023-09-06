<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LateFineCancelReport.aspx.cs"
    Inherits="ACADEMIC_REPORTS_LateFineCancelReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Late Fee Cancel Student's Report</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="date1" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" ValidationGroup="Report" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="date1" Enabled="true" EnableViewState="true" />
                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please enter From date." SetFocusOnError="true"
                                                ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MessageValidatorTip="true" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" MaskType="Date" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar" id="date" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" ValidationGroup="Report" />
                                            <%-- <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />--%>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="date" Enabled="true" EnableViewState="true" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please enter To date." SetFocusOnError="true"
                                                ValidationGroup="Report" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" MessageValidatorTip="true" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" MaskType="Date" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 mt-3">
                                        <asp:Button ID="btnExcelReport" runat="server" OnClick="btnExcelReport_Click" Text="Excel Report"
                                            ValidationGroup="Report" CssClass="btn-primary btn"></asp:Button>&nbsp;&nbsp;
                                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            TabIndex="10" CssClass="btn btn-warning" />                                      
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Report" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvlatefineRecords" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>Late Fee Cancel Student's Details</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Enrollnment No</th>
                                                        <th>Student Name</th>
                                                        <th>SESSION_NAME</th>
                                                        <th>RECIEPT TITLE</th>
                                                        <th>TOTAL DEMAND</th>
                                                        <th>ACTUAL LATE FINE</th>
                                                        <th>WAVE OFF LATE FINE</th>
                                                        <th>CANCELLED LATE FINE</th>
                                                        <th>LATE FINE AFTER CANCEL/WAVE OFF</th>
                                                        <th>DEMAND AFTER CANCEL/WAVE OFF LATE FINE</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                            <td><%# Eval("SEMESTERNAME")%></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

