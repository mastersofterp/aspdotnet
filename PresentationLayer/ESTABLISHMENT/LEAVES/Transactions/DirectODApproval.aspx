<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectODApproval.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Transactions_DirectODApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">


        function parseDate(str) {
            var date = str.split('/');
            return new Date(date[2], date[1], date[0] - 1);
        }

        function GetDaysBetweenDates(date1, date2) {
            return (date2 - date1) / (1000 * 60 * 60 * 24)
        }


        function caldiff() {

            if ((document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value != null) && (document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value != null)) {

                var d = GetDaysBetweenDates(parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtFromdt').value), parseDate(document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').value));
                {
                    document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = (parseInt(d) + 1);
                    if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value == "NaN") {
                        document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                    }
                }

            }
            if (document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value <= 0) {
                alert("No. of Days can not be 0 or less than 0 ");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            if (parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value) > parseInt(document.getElementById('ctl00_ContentPlaceHolder1_txtLeavebal').value)) {

                alert("No. of Days not more than Balance Days");
                document.getElementById('ctl00_ContentPlaceHolder1_txtNodays').value = "";
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodt').focus();
            }
            return false;
        }

        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.name.endsWith('chkSelect')) {
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true)
                            e.checked = true;
                        else
                            e.checked = false;
                    }
                }
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">DIRECT APPROVAL OF OD</h3>
                </div>
                <div class="box-body">
                    <asp:Panel ID="pnlAdd" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Direct OD Approval Details</h5>
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
                                        Display="None" ErrorMessage="Please Select College Name " ValidationGroup="Leaveapp"
                                        SetFocusOnError="true">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trddldept" runat="server">
                                    <div class="label-dynamic">
                                        <label>Select Department</label>
                                    </div>
                                    <asp:DropDownList ID="ddldept" runat="server" AppendDataBoundItems="true" TabIndex="2" data-select2-enable="true"
                                        CssClass="form-control" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" id="trStaff" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Staff Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" data-select2-enable="true"
                                        CssClass="form-control" OnSelectedIndexChanged="ddlStaff_SelectedIndexChanged" ToolTip="Select Staff Type">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                        Display="None" ErrorMessage="Please select Staff Type" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <%--<div class="form-group col-lg-3 col-md-6 col-12" id="trleave" runat="server">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Select Leave Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddlLeaveType" runat="server" AppendDataBoundItems="true" TabIndex="4" data-select2-enable="true"
                                        ToolTip="Select Leave Type" CssClass="form-control" OnSelectedIndexChanged="ddlLeaveType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Enabled="true" Selected="True" Text="Please Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlLeaveType" runat="server" ControlToValidate="ddlLeaveType"
                                        Display="None" ErrorMessage="Please select Leave Type" SetFocusOnError="true" ValidationGroup="Leaveapp"
                                        InitialValue="0"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Select Date</label>
                                    </div>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="imgCalFromdt" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromdt" runat="server" MaxLength="10" CssClass="form-control" TabIndex="5"
                                            OnTextChanged="txtFromdt_TextChanged" ToolTip="Enter Date" Style="z-index: 0;"></asp:TextBox>
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgCalFromdt" TargetControlID="txtFromdt">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" AcceptNegative="Left"
                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                            MessageValidatorTip="true" TargetControlID="txtFromdt" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedEditExtender5"
                                            ControlToValidate="txtFromdt" InvalidValueMessage="Select Date is Invalid (Enter dd/mm/yyyy Format)"
                                            Display="None" TooltipMessage="Please Enter Select Date" EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Select Date" ValidationGroup="Leaveapp" SetFocusOnError="True" IsValidEmpty="false"
                                            InitialValue="__/__/____" /><%--EmptyValueMessage="Please Enter From Date"--%>
                                    </div>
                                </div>
                                <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="Div1" runat="server">
                                     <label>OD</label>
                                     <asp:RadioButton ID="Rb_OD"  TabIndex="6" GroupName ="0" Checked="true" runat="server" />&nbsp&nbsp
                                     <label>CompOff</label>
                                     <asp:RadioButton ID="Rb_CompOff" TabIndex="7" GroupName ="0" runat="server" />
                                </div>--%>
                                <div class="form-group col-lg-5 col-md-12 col-12">
                                    <div class=" note-div">
                                        <h5 class="heading">Note</h5>
                                        <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: #FF0000">Please Check Mark Checkbox Option for Bulk OD Approval. </span></p>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Leaveapp" TabIndex="7"
                            OnClick="btnShow_Click" ToolTip="Click here to Show" CssClass="btn btn-primary" />
                        <asp:Button ID="btnCancelAdd" runat="server" Text="Cancel" ValidationGroup="Leaveapp" TabIndex="8"
                            OnClick="btnCancelAdd_Click" ToolTip="Click here to Reset" CssClass="btn btn-primary" CausesValidation="false" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            ValidationGroup="Leaveapp" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        <asp:Button ID="btnBulkApprove" runat="server" Text="Bulk OD Approve" TabIndex="9"
                            OnClick="btnBulkApprove_Click" ToolTip="Click here to Show" CssClass="btn btn-primary" />
                    </div>
                    <asp:Panel ID="pnlList" runat="server">
                        <div class="col-12 table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                            <asp:ListView ID="lvPendingList" runat="server">
                                <EmptyDataTemplate>
                                    <p class="text-center text-bold">
                                        <asp:Label ID="lblErr" runat="server" Text=" No more Pending List of OD for Approval">
                                        </asp:Label>
                                    </p>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Pending List of OD for Approval</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" tabindex="10">
                                        <%--<thead class="bg-light-blue">--%>
                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                            <tr>
                                                <th>Action
                                                    <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                </th>
                                                <th>Sr.No.
                                                </th>
                                                <th>Name
                                               <%-- </th>
                                                <th>Leave Name
                                                </th>--%>
                                                <th>From Date
                                                </th>
                                                <th>To Date
                                                </th>
                                                <th>No. of days
                                                </th>
                                                <th>Joining date
                                                </th>
                                                <th>Approve/Reject
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
                                            <asp:Panel ID="pnlShowQA" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                            </asp:Panel>
                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("ODTRNO") %>' />
                                            <asp:HiddenField ID="hidEmployeeNo" runat="server" Value='<%# Eval("ODTRNO") %>' />
                                        </td>
                                        <td>
                                            <%# Eval("sno")%>
                                        </td>
                                        <td>
                                            <%# Eval("EmpName")%>
                                        </td>
                                        <%-- <td>
                                            <%# Eval("LName")%>
                                        </td>--%>
                                        <td>
                                            <%# Eval("From_date")%>
                                        </td>
                                        <td>
                                            <%# Eval("TO_DATE") %>
                                        </td>
                                        <td>
                                            <%# Eval("NO_OF_DAYS") %>
                                        </td>
                                        <td>
                                            <%# Eval("JOINDT") %>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnApproval" runat="server" Text="Select" CommandArgument='<%# Eval("ODTRNO")%>'
                                                ToolTip="Select to Approve/Reject" OnClick="btnApproval_Click" CssClass="btn btn-primary" TabIndex="11" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="9" style="text-align: left; padding-left: 10px; padding-right: 10px">
                                            <asp:Panel ID="pnlQues" runat="server" CssClass="collapsePanel" TabIndex="12">
                                                <table class="table table-bordered table-hover">
                                                    <tr>
                                                        <%--<th align="left">--%>
                                                        <th>Purpose Of Visit
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("PURPOSE") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <ajaxToolKit:CollapsiblePanelExtender ID="cpeQA" runat="server" ExpandDirection="Vertical"
                                        TargetControlID="pnlQues" ExpandControlID="pnlShowQA" CollapseControlID="pnlShowQA"
                                        ExpandedText="Hide Reason" CollapsedText="Show Reason" CollapsedImage="~/Images/action_down.png"
                                        ExpandedImage="~/images/action_up.png" ImageControlID="imgExp" Collapsed="true">
                                    </ajaxToolKit:CollapsiblePanelExtender>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlApprove" runat="server">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>OD Approval Details</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="sub-heading">
                                        <h5>Select Criteria for OD Approval</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Employee Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEmpName" ToolTip="Employee Name" Enabled="false" runat="server"></asp:Label></a>
                                        </li>

                                        <%--<li class="list-group-item"><b>Leave Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblLeaveName" ToolTip="Leave Name" Enabled="false" runat="server"></asp:Label></a>
                                        </li>--%>

                                        <li class="list-group-item"><b>From Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFromdt" ToolTip="From Date" Enabled="false" runat="server"></asp:Label></a>
                                        </li>

                                        <li class="list-group-item"><b>To Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTodt" ToolTip="To Date" Enabled="false" runat="server"></asp:Label></a>
                                        </li>

                                        <li class="list-group-item"><b>No.of Days :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblNodays" ToolTip="Number of Days" Enabled="false" runat="server"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Place of Visit :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblPlace" runat="server" TabIndex="12" ToolTip="Place Of Visit"></asp:Label></a>
                                        </li>

                                    </ul>
                                </div>
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Purpose of Visit :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblReason" runat="server" ToolTip="Reason" Enabled="false"></asp:Label></a>
                                        </li>


                                        <li class="list-group-item"><b>Joining Date :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblJoindt" runat="server" ToolTip="Joining Date" Enabled="false"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>OD Criteria :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblOdCriteria" runat="server" ToolTip="OD Criteria" Enabled="false"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Event Type :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEvent" runat="server" TabIndex="10" ToolTip="Event Type"></asp:Label>
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Topic :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTopic" runat="server" TabIndex="11" ToolTip="Topic"></asp:Label>
                                            </a>
                                        </li>

                                        <%-- <li class="list-group-item"><b>&nbsp;</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblNoon" runat="server" ToolTip="Joining Date Noon" Enabled="false"></asp:Label></a>
                                        </li>--%>
                                        <%-- <li class="list-group-item"><b>FN/AN :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblNoon" runat="server" ToolTip="FN/AN" Enabled="false"></asp:Label></a>
                                        </li>

                                        <li class="list-group-item" style="display: none"><b>Total no.of OD :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lbltot" runat="server" ToolTip="Total Number of Leaves" Enabled="false"></asp:Label></a>
                                        </li>

                                        <li class="list-group-item"><b>Balance OD :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblbal" runat="server" ToolTip="Balance Leaves" Enabled="false"></asp:Label></a>
                                        </li>--%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <div class="row">
                                <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>
                                <div class="col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Select</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSelect" runat="server" ToolTip="Select Approve or Reject" CssClass="form-control" TabIndex="11" data-select2-enable="true"
                                        AppendDataBoundItems="true">
                                        <asp:ListItem Value="A">Approve</asp:ListItem>
                                        <asp:ListItem Value="R">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <%--<div class="form-group col-lg-3 col-md-6 col-12">--%>
                                <div class="col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Remarks</label>
                                    </div>
                                    <%-- Width="250px" Height="50px" --%>
                                    <asp:TextBox ID="txtRemarks" runat="server" ToolTip="Enter Remarks" TabIndex="12" TextMode="MultiLine" CssClass="form-control"
                                        MaxLength="180" onkeyDown="checkTextAreaMaxLength(this,event,'180');" onkeyup="textCounter(this, this.form.remLen, 180);" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer mt-2 mb-2">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ValidationGroup="Leaveapp" TabIndex="13"
                                CssClass="btn btn-primary" ToolTip="Clic here to Submit" OnClick="btnSave_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="false" TabIndex="14"
                                OnClick="btnBack_Click" CssClass="btn btn-primary" ToolTip="Click here to Go Back" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" TabIndex="15"
                                CssClass="btn btn-warning" ToolTip="Click here to Reset" OnClick="btnCancel_Click" />
                        </div>

                        <div class="col-12 mt-2 mb-3">
                            <asp:ListView ID="lvStatus" runat="server">
                                <EmptyDataTemplate>
                                    <div class="text-center">
                                        <asp:Label ID="ibler" runat="server" Text="No More OD Approval List"></asp:Label>
                                    </div>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Approval Status</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No.
                                                </th>
                                                <th>Authority Name
                                                </th>
                                                <th>User Name
                                                </th>
                                                <th>Status
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%# Eval("sno")%>
                                        </td>
                                        <td>
                                            <%# Eval("PANAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAusername")%>
                                        </td>
                                        <td>
                                            <%# Eval("STATUS")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="altitem">
                                        <td>
                                            <%# Eval("sno")%>
                                        </td>
                                        <td>
                                            <%# Eval("PANAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("PAusername")%>
                                        </td>
                                        <td>
                                            <%# Eval("STATUS")%>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
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
</asp:Content>
