<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IrregularityApplication.aspx.cs"
    Inherits="ESTABLISHMENT_LEAVES_Transactions_IrregularityApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">IRREGULARITY APPLY</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnllist" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>IRREGULARITY APPLY</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlMonth" runat="server">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Month/Year</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgMonthYear" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtMonthYear" runat="server" onblur="return checkdate(this);"></asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceMonthYear" runat="server" Enabled="true" EnableViewState="true"
                                                Format="MM/yyyy" PopupButtonID="imgMonthYear" TargetControlID="txtMonthYear">
                                            </ajaxToolKit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ControlToValidate="txtMonthYear"
                                                Display="None" ErrorMessage="Please Enter Month & Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                ValidationGroup="Late">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="payroll" TabIndex="9"
                                            CssClass="btn btn-primary" ToolTip="Click here to Process Attendance" OnClick="btnShow_Click" />
                                        <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Late" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlLeaveCard" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvLateEarly" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No.
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Irregularity Status
                                                    </th>
                                                    <th>Irregularity Apply
                                                    </th>
                                                    <th>Status
                                                    </th>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("SERIAL_NO")%>
                                                <asp:HiddenField ID="hdnSrno" runat="server" Value='<%# Eval("SERIAL_NO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("ENTDATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("IrrugulaityStatus")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnApply" runat="server" Text="Apply" CommandName='<%# Eval("IDNO") %>'
                                                    CommandArgument='<%# Eval("SERIAL_NO") %>' CssClass="btn btn-primary" TabIndex="1"
                                                    OnClick="btnApply_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("FinalStatus")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlDetail" runat="server">
                            <div class="col-12">
                                <asp:ListView ID="lvInfo" runat="server">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lblErr" runat="server" Text="" CssClass="d-block text-center mt-3">
                                        </asp:Label>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Sr No.
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Irregularity Status
                                                    </th>
                                                    <th>Irregularity Apply
                                                    </th>
                                                    <th>Status
                                                    </th>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("SERIALNO")%>
                                                <asp:HiddenField ID="hdnSrno" runat="server" Value='<%# Eval("SERIALNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("ENTDATE","{0:dd/MM/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("IrrugulaityStatus")%>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnIrregularApply" runat="server" Text="Apply" CommandName='<%# Eval("IDNO") %>'
                                                    CommandArgument='<%# Eval("SERIALNO") %>' ToolTip=' <%# Eval("IrrugulaityStatus") %>'
                                                    CssClass="btn btn-primary" TabIndex="1" OnClick="btnIrregularApply_Click" />
                                            </td>
                                            <td>
                                                <%# Eval("FinalStatus")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlAdd" runat="server">
                            <%--<div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Add/Edit Employee Leave</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Employee Name</label>
                                        </div>
                                        <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control" ToolTip="Employee Name"
                                            ReadOnly="true" TabIndex="1">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:TextBox ID="txtDept" runat="server" CssClass="form-control" ToolTip="Department"
                                            ReadOnly="true" TabIndex="2">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Date</label>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Designation"
                                            ReadOnly="true" TabIndex="3">
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divInTime" runat="server">
                                        <div class="label-dynamic">
                                            <label>Actual In Time</label>
                                        </div>
                                        <asp:TextBox ID="txtIntime" runat="server" CssClass="form-control" ToolTip="In Time"
                                            ReadOnly="true" TabIndex="4" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divOutTime" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Out Time</label>
                                        </div>
                                        <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control" ToolTip="Out Time"
                                            ReadOnly="true" TabIndex="5" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Shift In Time</label>
                                        </div>
                                        <asp:TextBox ID="txtShiftIn" runat="server" CssClass="form-control" ToolTip="Shift In Time"
                                            ReadOnly="true" TabIndex="6" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Shift Out Time</label>
                                        </div>
                                        <asp:TextBox ID="txtShiftOut" runat="server" CssClass="form-control" ToolTip="Shift Out Time"
                                            ReadOnly="true" TabIndex="7" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Working Hours</label>
                                        </div>
                                        <asp:TextBox ID="txtWorkingHours" runat="server" CssClass="form-control" ToolTip="Shift Out Time"
                                            ReadOnly="true" TabIndex="8" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divrequiredhours" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Required Working Hours</label>
                                        </div>
                                        <asp:TextBox ID="txtrequiredhours" runat="server" CssClass="form-control" ToolTip="Required Working Hours"
                                            ReadOnly="true" TabIndex="9"/>
                                     </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status</label>
                                        </div>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control" ToolTip="Status"
                                            ReadOnly="true" TabIndex="10" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Path</label>
                                        </div>
                                        <asp:TextBox ID="txtPath" runat="server" Enabled="false" CssClass="form-control"
                                            ToolTip="Path" TabIndex="11" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Reason</label>
                                        </div>
                                        <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine"
                                            ToolTip="Enter Reason" TabIndex="12" MaxLength="200" onkeyDown="checkTextAreaMaxLength(this,event,'200');" onkeyup="textCounter(this, this.form.remLen, 200);"/>
                                        <asp:RequiredFieldValidator ID="rfvReason" runat="server"
                                            ControlToValidate="txtReason" Display="None" ErrorMessage="Please Enter Reason"
                                            ValidationGroup="Leaveapp"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <asp:Label ID="lblvalid" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="13"
                                    CssClass="btn btn-primary" ToolTip="Click here to Submit" OnClick="btnSave_Click" />
                                <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="14"
                                    CssClass="btn btn-primary" ToolTip="Click here to Go Back" OnClick="btnBack_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="15"
                                    CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Leaveapp"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event) {//IE
                        e.returnValue = false;
                    }
                    else {//Firefox
                        e.preventDefault();
                    }
                }
            }
        }
        function textCounter(field, countfield, maxlimit) {
            if (field.value.length > maxlimit)
                field.value = field.value.substring(0, maxlimit);
            else
                countfield.value = maxlimit - field.value.length;
        }

    </script>
    <script type="text/javascript" language="javascript">
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid Date Format. Please Enter in MM/YYYY Formate")
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
            }
            else {
                var monthfield = input.value.split("/")[0]

                if (monthfield > 12 || monthfield <= 0) {
                    alert("Month Should be greate than 0 and less than 13");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").value = "";
                    document.getElementById("ctl00_ContentPlaceHolder1_txtMonthYear").focus();
                }
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>


