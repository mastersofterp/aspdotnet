<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MonthlyFuelExpenseEntry.aspx.cs" Inherits="VEHICLE_MAINTENANCE_Transaction_MonthlyFuelExpenseEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../../JAVASCRIPTS/JScriptAdmin_Module.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            else
                return true;
        }

        function Clear() {
            document.getElementById('<%=txtAmount.ClientID%>').value = '';
            document.getElementById('<%=txtFromDate.ClientID%>').value = '';
            document.getElementById('<%=txtToDate.ClientID%>').value = '';
            document.getElementById('<%=ddlSupplier.ClientID%>').value = '0';

        }
        function check_date(a) {
            var Fdate = document.getElementById('<%=txtFromDate.ClientID%>').value.split("/");
            var Tdate = document.getElementById('<%=txtToDate.ClientID%>').value.split("/");
            var FromDate = new Date(Fdate[2], Fdate[1] - 1, Fdate[0]);
            var ToDate = new Date(Tdate[2], Tdate[1] - 1, Tdate[0]);
            var PresentDate = new Date();
            if (a == 0) {
                if (PresentDate < ToDate) {
                    alert('Future date is not allowed.');
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').value.focus();
                    return;
                }
                else {

                    if (ToDate < FromDate) {
                        alert('To Date should not be less than From Date');
                        document.getElementById('<%=txtToDate.ClientID%>').value = '';
                        document.getElementById('<%=txtToDate.ClientID%>').value.focus();
                    }
                }

            }
            else {
                if (PresentDate < FromDate) {
                    alert('Future date is not allowed.');
                    document.getElementById('<%=txtFromDate.ClientID%>').value = '';
                    document.getElementById('<%=txtFromDate.ClientID%>').value.focus();
                }
                document.getElementById('<%=txtToDate.ClientID%>').value = '';
            }
        }
        function check_rdate(a) {
            if (a == 0) {
                var Fdate = document.getElementById('<%=txtRFdate.ClientID%>').value.split("/");
                var Tdate = document.getElementById('<%=txtRTdate.ClientID%>').value.split("/");
                var FromDate = new Date(Fdate[2], Fdate[1] - 1, Fdate[0]);
                var ToDate = new Date(Tdate[2], Tdate[1] - 1, Tdate[0]);
                if (ToDate < FromDate) {
                    alert("To date Cannot Be Less Than From Date.");
                    var Tdate = document.getElementById('<%=txtRTdate.ClientID%>').value = '';
                }
            } else {
                document.getElementById('<%=txtRTdate.ClientID%>').value = '';
            }

        }

        function ClearRptCon() {
            document.getElementById('<%=txtRFdate.ClientID%>').value = '';
            document.getElementById('<%=txtRTdate.ClientID%>').value = '';
        }
    </script>
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
                            <h3 class="box-title">MONTHLY FUEL EXPENSE ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="Panel1" runat="server">
                                <div id="DivControl" runat="server">
                                    <div class="col-12">
                                        <%--  <div class=" sub-heading">Add/Edit Monthly Fuel Expense Entry</div>--%>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtFromDate23">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtFromDate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="1"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(1);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="ceePdDt" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtFromDate23" TargetControlID="txtFromDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtFromDate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtFromDate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="From date invalid Enter[dd/MM/yyyy] format" ValidationGroup="Submit">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtToDate23">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtToDate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="2"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_date(0);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtToDate23" TargetControlID="txtToDate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtToDate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtToDate" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter To Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage=" To date invalid Enter [dd/MM/yyyy] format" ValidationGroup="Submit">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Fuel Supplier</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSupplier" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="3"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="supplier" runat="server" ControlToValidate="ddlSupplier" ErrorMessage="Please Select Fuel Supplier" Display="None" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TabIndex="4" MaxLength="12"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeAmount" TargetControlID="txtAmount" runat="server" FilterType="Numbers,Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter Amount" Display="None" ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Click"
                                            CssClass="btn btn-primary" ToolTip="Click here to Submit" CausesValidation="true" TabIndex="5" />
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />
                                        <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-info" OnClick="btnreport_Click" TabIndex="7" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="6"
                                            CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />

                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="pnlList" runat="server">
                                            <asp:ListView ID="lvSuppiler" runat="server">
                                                <LayoutTemplate>
                                                    <div id="lgv1">
                                                        <div class="sub-heading">
                                                            <h5>MONTHLY FUEL EXPENSE ENTRY</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>From Date
                                                                    </th>
                                                                    <th>To Date
                                                                    </th>
                                                                    <th>Fuel Supplier
                                                                    </th>
                                                                    <th>Amount
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                CommandArgument='<%# Eval("MFE_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("FROM_DATE","{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TO_DATE","{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FUEL_SUPPILER_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("AMOUNT")%>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div id="DivReport" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class=" sub-heading">
                                            <h5>Monthly Fuel Expense Report</h5>
                                        </div>
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>From Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtRFdate2">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtRFdate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="8"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_rdate(1);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtRFdate2" TargetControlID="txtRFdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtRFdate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtRFdate" EmptyValueMessage="Please Enter From Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid From Date In [dd/MM/yyyy] format" ValidationGroup="btnreport">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>To Date</label>
                                                </div>
                                                <div class="input-group date">
                                                    <div class="input-group-addon" id="txtRTdate3">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtRTdate" CssClass="form-control" ToolTip="Enter Complaint Date" TabIndex="9"
                                                        runat="server" Style="z-index: 0;" autocomplete="off" onchange="check_rdate(0);"></asp:TextBox>
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" EnableViewState="true"
                                                        Format="dd/MM/yyyy" PopupButtonID="txtRTdate3" TargetControlID="txtRTdate">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
                                                        AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true" MaskType="Date"
                                                        MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtRTdate"
                                                        ClearMaskOnLostFocus="true">
                                                    </ajaxToolKit:MaskedEditExtender>
                                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
                                                        ControlToValidate="txtRTdate" EmptyValueMessage="Please Enter To Date" IsValidEmpty="false"
                                                        ErrorMessage="Please Enter Valid To Date In [dd/MM/yyyy] format" Display="None" Text="*"
                                                        InvalidValueMessage="Please Enter Valid To Date In [dd/MM/yyyy] format" ValidationGroup="btnreport">                                                            
                                                    </ajaxToolKit:MaskedEditValidator>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowRpt" runat="server" Text="ShowReport" OnClick="btnShowRpt_Click" CssClass="btn btn-info" ValidationGroup="btnreport" TabIndex="10" />
                                        <asp:ValidationSummary ID="vsReport" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="btnreport" />
                                        <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnBack_Click" OnClientClick="ClearRptCon();" TabIndex="12" />
                                        <asp:Button ID="btnRptClear" runat="server" CssClass="btn btn-warning" Text="Cancel" TabIndex="11" OnClientClick="ClearRptCon();" />

                                    </div>

                                </div>

                            </asp:Panel>

                        </div>
                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

