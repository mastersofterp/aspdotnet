<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualBiometricinsert.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ESTABLISHMENT_LEAVES_Transactions_ManualBiometricinsert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MANUAL BIO METRIC ENTRY</h3>
                        </div>

                        <div class="box-body">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>College Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" TabIndex="2" AppendDataBoundItems="true" data-select2-enable="true"
                                                 ToolTip="Select College Name" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege" InitialValue="0"
                                                Display="None" ErrorMessage="Please Select College Name " ValidationGroup="payroll"
                                                SetFocusOnError="true">
                                            </asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Staff Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlStafftype" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlStafftype_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlStafftype" runat="server" ControlToValidate="ddlStafftype"
                                                Display="None" ErrorMessage="Select Staff Type" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Department</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartment"
                                                Display="None" ErrorMessage="Please Select Department" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>--%>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Month &amp; Year </label>
                                            </div>
                                            <div class='input-group date' id="myDatepicker1">
                                                <div class="input-group-addon" id="ImaCalStartDate" runat="server">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtMonthYear" runat="server"
                                                    ToolTip="Enter date in MM/yyyy format"
                                                    CssClass="form-control datepickerinput" data-inputmask="'mask': '99/9999'" onchange="return checkdate(this);" TabIndex="3"></asp:TextBox>


                                                <ajaxToolKit:CalendarExtender ID="cetxtStartDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="MM/yyyy" PopupButtonID="ImaCalStartDate" TargetControlID="txtMonthYear">
                                                </ajaxToolKit:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="rfvtxtStartDate" runat="server" ControlToValidate="txtMonthYear"
                                                    Display="None" ErrorMessage="Please Enter Month &amp; Year in (MM/YYYY Format)" SetFocusOnError="True"
                                                    ValidationGroup="payroll">
                                                </asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Employee</label>
                                            </div>
                                            <asp:DropDownList ID="ddlEmployee" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="ddlEmployee"
                                                Display="None" ErrorMessage="Please Select Employee" ValidationGroup="payroll" InitialValue="0"></asp:RequiredFieldValidator>

                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" ValidationGroup="payroll" TabIndex="5" OnClick="btnshow_Click"
                                    CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="payroll" TabIndex="6" OnClick="btnSubmit_Click"
                                    CssClass="btn btn-primary " />
                                <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="payroll" OnClick="btnReport_Click" TabIndex="7"
                                    CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="payroll"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlAttendance" runat="server">
                                    <asp:ListView ID="lvAttendance" runat="server">
                                        <EmptyDataTemplate>
                                            <p class="text-center">
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                            </p>
                                        </EmptyDataTemplate>
                                        <LayoutTemplate>
                                            <div class="vista-grid">
                                                <div class="sub-heading" style="display: none">
                                                    <h5>MANUAL BIO METRIC ENTRY</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <asp:CheckBox ID="cbAl" runat="server" onclick="totAllSubjects(this)" />
                                                                Select
                                                            </th>
                                                            <th>Date
                                                            </th>
                                                            <th>In Time
                                                            </th>
                                                            <th>Out Time
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
                                            <tr class="item">
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("USERID") %>' />
                                                </td>
                                                <td>
                                                    <%-- <%#Eval("ENDDATE")%>--%>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Date" TabIndex="9" ReadOnly="true" Text='<%# Eval("ENDDATE")%>' />
                                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("USERID")%>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInTime" runat="server" CssClass="form-control" ToolTip="Enter  In-Time" TabIndex="10" Text='<%# Eval("INTIME")%>' />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="payroll"
                                        ControlToValidate="txtInTime" ErrorMessage="Please Enter the Time" Display="None" />--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeInTime" runat="server" TargetControlID="txtInTime"
                                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                    <%--<ajaxToolKit:MaskedEditValidator ID="mevInTime" ControlToValidate="txtInTime" ControlExtender="meeInTime"
                                        SetFocusOnError="true" ValidationGroup="payroll" MaximumValue="23:59:59" runat="server"
                                        ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control" ToolTip="Enter  Out-Time" TabIndex="11" Text='<%# Eval("OUTTIME")%>' />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeOutTime" runat="server" TargetControlID="txtOutTime"
                                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                    <%-- <ajaxToolKit:MaskedEditValidator ID="mevOuttime" ControlToValidate="txtOutTime" ControlExtender="meeOutTime"
                                        SetFocusOnError="true" ValidationGroup="payroll" MaximumValue="23:59:59" runat="server"
                                        ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                        <AlternatingItemTemplate>
                                            <tr class="altitem">
                                                <td>
                                                    <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("USERID") %>' />
                                                </td>
                                                <td>
                                                    <%--<%#Eval("ENDDATE")%>--%>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ToolTip="Date" TabIndex="9" ReadOnly="true" Text='<%# Eval("ENDDATE")%>' />
                                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("USERID")%>' />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtInTime" runat="server" CssClass="form-control" ToolTip="Enter  In-Time" TabIndex="10" Text='<%# Eval("INTIME")%>' />
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="payroll"
                                        ControlToValidate="txtInTime" ErrorMessage="Please Enter the Time" Display="None" />--%>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeInTime" runat="server" TargetControlID="txtInTime"
                                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                    <%--<ajaxToolKit:MaskedEditValidator ID="mevInTime" ControlToValidate="txtInTime" ControlExtender="meeInTime"
                                        SetFocusOnError="true" ValidationGroup="payroll" MaximumValue="23:59:59" runat="server"
                                        ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOutTime" runat="server" CssClass="form-control" ToolTip="Enter  Out-Time" TabIndex="11" Text='<%# Eval("OUTTIME")%>' />
                                                    <ajaxToolKit:MaskedEditExtender ID="meeOutTime" runat="server" TargetControlID="txtOutTime"
                                                        ClearMaskOnLostFocus="false" MaskType="Time" Mask="99:99:99" />
                                                    <%--<ajaxToolKit:MaskedEditValidator ID="mevOuttime" ControlToValidate="txtOutTime" ControlExtender="meeOutTime"
                                        SetFocusOnError="true" ValidationGroup="payroll" MaximumValue="23:59:59" runat="server"
                                        ErrorMessage="Invalid Time" MinimumValue="00:00:00" InvalidValueBlurredMessage="Invalid Time" />--%>
                                                </td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnshow" />--%>
            <asp:PostBackTrigger ControlID="btnReport" />
            <%--<asp:PostBackTrigger ControlID="txtMonthYear" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        // Move an element directly on top of another element (and optionally
        // make it the same size)
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + 'px';
            top.style.left = location.x + 'px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">
        function checkdate(input) {
            var validformat = /^\d{2}\/\d{4}$/ //Basic check for format validity
            var returnval = false
            if (!validformat.test(input.value)) {
                alert("Invalid date format. Please enter date in MM/YYYY format")
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
    <div id="divMsg" runat="server"></div>
</asp:Content>


