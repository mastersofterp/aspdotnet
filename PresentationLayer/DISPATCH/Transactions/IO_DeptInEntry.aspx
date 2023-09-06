<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IO_DeptInEntry.aspx.cs" Inherits="Dispatch_Transactions_IO_DeptInEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
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
                            <h3 class="box-title">DEPARTMENT INWARD ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnl" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Search Criteria</h5></div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divfrmdate" visible="true" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>From Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <%--<asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                        <i id="imgFrmDt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFrmDate" runat="server" MaxLength="100" CssClass="form-control" ToolTip="Select From Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFrmDt" runat="server" ControlToValidate="txtFrmDate"
                                                    Display="None" ErrorMessage="Please Enter From Date" SetFocusOnError="true" ValidationGroup="Date"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txtFrmDate">
                                                </ajaxToolKit:CalendarExtender>

       <%-- GAYATRI RODE 13-01-2021  --%>       <ajaxToolKit:MaskedEditExtender ID="medt" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtFrmDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MEVDate" runat="server"  ControlExtender="medt" ControlToValidate="txtFrmDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>
                                          

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divtodate" visible="true" runat="server">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>To Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <%--   <asp:Image ID="imgTodt" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False" Style="cursor: pointer" />--%>
                                                        <i id="imgTodt" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="18" CssClass="form-control"  ToolTip="Select To Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTodt" runat="server" ControlToValidate="txtToDate"
                                                    Display="None" ErrorMessage="Please Enter To Date" SetFocusOnError="true" ValidationGroup="Date"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:CalendarExtender ID="ceTodt" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="imgTodt" TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>

          <%-- GAYATRI RODE 13-01-2021  --%>      <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtToDate" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender1"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] " ControlToValidate="txtToDate"
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*" ValidationGroup="Date">
                                                </ajaxToolKit:MaskedEditValidator>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        
                            <div class="col-12 btn-footer" id="button" visible="true">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="Date" CssClass="btn btn-primary" ToolTip="Click here to Show List" />
                                <asp:Button ID="btnreport" runat="server" Text="Report" ValidationGroup="Date"
                                    OnClick="btnreport_Click" Visible="false" CssClass="btn btn-info" ToolTip="Click here to Get Report" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Date" />

       <%-- GAYATRI RODE 13-01-2021  --%>    <asp:CompareValidator ID="CampCalExtDate" runat="server" ControlToValidate="txtToDate" 
CultureInvariantValues="true" Display="None" ErrorMessage="To Date Must Be Equal To Or Greater Than From Date." Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" 
ValidationGroup="Date" ControlToCompare="txtFrmDate" />

                            </div>
                          <%--  Modified by Saahil Trivedi 17-02-2022--%>
                             <asp:Panel ID="pnlAdd" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading"><h5>Add Inward Entry</h5></div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Sender Name</label>
                                            </div>
                                            <asp:Label ID="lblFrom" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Subject</label>
                                            </div>
                                            <asp:Label ID="lblSubject" runat="server" Font-Bold="true"></asp:Label>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Received Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <%--<asp:Image ID="imgReceivedDT" runat="server" ImageUrl="~/images/calendar.png" CausesValidation="False"
                                                        Style="cursor: pointer" />--%>
                                                        <i id="imgReceivedDT" runat="server" class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtReceivedDT" runat="server" MaxLength="10" CssClass="form-control" ToolTip="Enter Received Date"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvReceivedDT" runat="server" ControlToValidate="txtReceivedDT"
                                                    Display="None" ErrorMessage="Please enter valid Received Date." SetFocusOnError="true" ValidationGroup="Submit" />
                                                <ajaxToolKit:CalendarExtender ID="CeReceivedDT" runat="server" Enabled="true" EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgReceivedDT"
                                                    TargetControlID="txtReceivedDT">
                                                </ajaxToolKit:CalendarExtender>
                                                  <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left" DisplayMoney="Left"
                                                    ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                    OnInvalidCssClass="errordate" TargetControlID="txtReceivedDT" ClearMaskOnLostFocus="true">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"  ControlExtender="medt" ControlToValidate="txtReceivedDT"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Date Format [dd/MM/yyyy] "
                                                    InvalidValueMessage="Please Enter Valid Date Format [dd/MM/yyyy] " Display="None" Text="*"
                                                    ValidationGroup="Date"></ajaxToolKit:MaskedEditValidator>
                                            </div>

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Remarks</label>
                                            </div>
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Received" ValidationGroup="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ToolTip="CLick here to Received" />
                                    <asp:Button ID="btnnotreceive" runat="server" Text="Not Received" ValidationGroup="Submit" OnClick="btnnotreceive_Click" CssClass="btn btn-info" ToolTip="CLick here to Not Received" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" ToolTip="Click here to Reset" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                </div>
                            </asp:Panel>
                            <div class="col-12">
                                <asp:Panel ID="pnlList" runat="server">
                                    <asp:ListView ID="lvInward" runat="server">
                                        <LayoutTemplate>
                                            <div id="lgv1">
                                                <div class="sub-heading"><h5>Department Inward Entry List</h5></div>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>POST TYPE
                                                        </th>
                                                        <th>SENDER NAME
                                                        </th>
                                                        <th>RECEIVED DATE
                                                        </th>
                                                        <th>SUBJECT
                                                        </th>
                                                        <th>ACTION
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("POSTTYPENAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("IOFROM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CENTRALRECSENTDT")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SUBJECT")%>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnReceived" runat="server" CommandArgument='<%# Eval("IOTRANNO") %>'
                                                        Text="Select" CssClass="btn btn-primary" ToolTip="Click to enter Received details" OnClick="btnReceived_Click" />
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
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

