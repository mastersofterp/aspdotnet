<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Leave_Cancel.aspx.cs" Inherits="Leave_Cancel"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">

        function ReceiveServerData(arg) {

            if (arg == 0) {

                document.getElementById('ctl00_ContentPlaceHolder1_hdnConfirmvalue').value = confirm('Do yant to Proceed ?');
            }
        }

    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">LEAVE CANCELLATION</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="panel panel-info">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Leave Cancellation Details</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>College Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Select College Name">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Leave"
                                            SetFocusOnError="true">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" TabIndex="2" data-select2-enable="true"
                                            AppendDataBoundItems="true" AutoPostBack="true" ToolTip="Select Department"
                                            OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Staff Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStafftype" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="3" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Staff Type">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStafftype" runat="server" ControlToValidate="ddlStafftype"
                                            Display="None" ErrorMessage="Please Select Staff Type " ValidationGroup="Leave"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Leave Date From</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="4"
                                                OnTextChanged="txtFromdt_TextChanged" AutoPostBack="true" Text="Enter Leave Date" Style="z-index: 0;" />
                                            <asp:RequiredFieldValidator ID="rfvFromdt" runat="server"
                                                ControlToValidate="txtFromdt" Display="None"
                                                ErrorMessage="Please Enter Leave Date" SetFocusOnError="true"
                                                ValidationGroup="Leave"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdt" runat="server" Enabled="true"
                                                EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt"
                                                TargetControlID="txtFromdt">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromdt" runat="server"
                                                AcceptNegative="Left" DisplayMoney="Left" ErrorTooltipEnabled="true"
                                                Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                TargetControlID="txtFromdt" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" CssClass="d-block w-100"
                                                ControlExtender="meeFromdt" ControlToValidate="txtFromdt" Display="Static"
                                                EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter From Date"
                                                InvalidValueBlurredMessage="Invalid Date"
                                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                SetFocusOnError="true" TooltipMessage="Please Enter Leave Date"
                                                ValidationGroup="Leave"> <%-- &#160;&#160;  --%>                                            
                                            </ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Employee</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEmp" runat="server" TabIndex="5" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Employee">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEmp"
                                            Display="None" ErrorMessage="Please Select Employee" ValidationGroup="Leave"
                                            SetFocusOnError="true" InitialValue="0">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnRestore" runat="server" OnClick="btnRestore_Click" ValidationGroup="Leave" CssClass="btn btn-primary"
                            Text="Click To Restore Leave" ToolTip="Click To Cancel the Approved Leave" Enabled="true" TabIndex="6" />
                        <asp:Button ID="btnShowRpt" runat="server" CssClass="btn btn-info" ToolTip="Click here to show Report"
                            Text="Show Report" OnClick="btnShowRpt_Click" ValidationGroup="Leave" TabIndex="7" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" ToolTip="Click here to Reset"
                            Text="Cancel" OnClick="btnCancel_Click" TabIndex="8" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Leave" />
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="pnlEmpList" runat="server">
                            <%--<asp:Panel ID="pnlEmpList" runat="server" Height="90%" ScrollBars="Vertical">--%>   <%--Style="height: 250px;"--%>
                            <asp:ListView ID="lvEmployees" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>List of Leaves</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No
                                                </th>
                                                <th>
                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="checkAllEmployees(this)" ToolTip="Check to Select All Leaves" Checked="false"/>
                                                    Select
                                                </th>
                                                <th>Employee Name
                                                </th>
                                                <th>Leave Name
                                                </th>
                                                <th>From Date
                                                </th>
                                                <th>To Date
                                                </th>
                                                <th>No. of Days
                                                </th>
                                                <th>Remark
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
                                            <%#Container.DataItemIndex+1 %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("LETRNO") %>' />
                                            <asp:HiddenField ID="hdnLeaveTrNo" runat="server" Value='<%# Eval("LETRNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("EMPFULLNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("LEAVENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("FROM_DATE")%>
                                            <asp:HiddenField ID="hdnFdt" runat="server" Value='<%# Eval("FROM_DATE") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("TO_DATE")%>
                                            <asp:HiddenField ID="hdnTdt" runat="server" Value='<%# Eval("TO_DATE") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("NO_OF_DAYS")%>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("NO_OF_DAYS") %>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtlvcancelRemark" runat="server" Text='<%#Eval("LVCANCEL_REMARK")%>'></asp:TextBox>
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

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;


        function checkAllEmployees(chkcomplaint) {
            var frm = document.forms[0];
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (chkcomplaint.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

            </script>
<div id="divMsg" runat="server">
</div>
</asp:Content>
