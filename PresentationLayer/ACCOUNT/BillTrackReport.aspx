<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BillTrackReport.aspx.cs" Inherits="ACCOUNT_BillTrackReport" %>

<%@ Register Assembly="AutoSuggestBox" Namespace="ASB" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <%--  <script type="text/javascript" charset="utf8" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script type="text/javascript" charset="utf8" src="https://cdn.datatables.net/1.10.25/js/jquery.dataTables.min.js"></script>--%>

    <%--  <script src="../JAVASCRIPTS/JScriptAdmin_Module.js"></script>    
    <script src="../plugins/datatables/dataTables.bootstrap.min.js"></script>
    <script src="../plugins/datatables/jquery.dataTables.min.js"></script>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery.min.js"></script>
    <script src="../bootstrap/js/bootstrap.min.js"></script>--%>



    <%-- <asp:UpdatePanel ID="updPanel" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Track Bill Report</h3>
                </div>
                <div class="box-body">
                    <div id="divCompName" runat="server" style="text-align: center!important; font-size: x-large"></div>

                    <asp:Panel ID="pnlBillList" runat="server">
                        <div class="col-12 mb-3">
                            <%--  <div class="panel-heading">Track Bill Report</div>--%>
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Bill Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlBillStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="ALL">All</asp:ListItem>
                                        <asp:ListItem Value="P">Pending Bill</asp:ListItem>
                                        <asp:ListItem Value="A">Approved Bill</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="ddlBillStatus" Display="None"
                                        InitialValue="0" ValidationGroup="Report" ErrorMessage="Please Select Bill Type"></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label>From Date</label>
                                    </div>
                                    <div class="input-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="ImgtxtInvDt">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                                Format="dd/MM/yyyy" PopupButtonID="ImgtxtInvDt" TargetControlID="txtFromDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate"
                                                ClearMaskOnLostFocus="true">
                                            </ajaxToolKit:MaskedEditExtender>

                                            <ajaxToolKit:MaskedEditValidator ID="meInvDate" runat="server"
                                                ControlExtender="MaskedEditExtender6" ControlToValidate="txtFromDate" IsValidEmpty="true"
                                                InvalidValueMessage="From Date is Invalid (Enter In dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Report" SetFocusOnError="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>To Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon" id="Image1">
                                            <i class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>

                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                            Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtToDate">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate"
                                            ClearMaskOnLostFocus="true">
                                        </ajaxToolKit:MaskedEditExtender>

                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                            ControlExtender="MaskedEditExtender6" ControlToValidate="txtToDate" IsValidEmpty="true"
                                            InvalidValueMessage="To Date is Invalid (Enter In dd/MM/yyyy Format)" Display="None" TooltipMessage="Input a date"
                                            ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                            ValidationGroup="Report" SetFocusOnError="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer mt-3">
                            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" ValidationGroup="Report" OnClick="btnShow_Click" />
                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-info" ValidationGroup="Report" OnClick="btnReport_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                        </div>

                        <div class="co-12 mt-3">
                            <asp:ListView ID="lvBillList" runat="server">
                                <LayoutTemplate>
                                    <div id="lgv1">
                                        <div class="sub-heading">
                                            <h5>Uploaded Bill List</h5>
                                        </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Bill Date
                                                        </th>
                                                        <th>Tran. No.
                                                        </th>
                                                        <th>Created By
                                                        </th>
                                                        <th>Supplier Name
                                                        </th>
                                                        <th>Approval Status
                                                        </th>
                                                        <th>Approval Date
                                                        </th>
                                                        <th>Voucher Number
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
                                            <%# Eval("BILL_DATE","{0:dd-MM-yyyy}") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("SERIAL_NO") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("UA_FULLNAME") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("SUPPLIER_NAME") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("BILL_STATUS") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("APPROVAL_DATE","{0:dd-MM-yyyy}") %>                                                              
                                        </td>
                                        <td>
                                            <%# Eval("VOUCHER_NO") %>                                                              
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>

                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
   
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>



