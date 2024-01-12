<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="HostelAttendence.aspx.cs" Inherits="HOSTEL_HostelAttendence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Hostel Attendance</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel Session</label>
                                </div>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Hostel Session" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                    ControlToValidate="ddlSession" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"
                                    Display="None"></asp:RequiredFieldValidator>

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Hostel </label>
                                </div>
                                <asp:DropDownList ID="ddlHostel" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlHostel_SelectedIndexChanged" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ID="rfvHostel" runat="server" ControlToValidate="ddlHostel"
                                    Display="None" ErrorMessage="Please Select Hostel" ValidationGroup="Show" SetFocusOnError="True"
                                    InitialValue="0" />

                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Attendance Date </label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtAttendanceDate" runat="server" TabIndex="3" ToolTip="Enter Date" AutoPostBack="true" CssClass="form-control" OnTextChanged="txtAttendanceDate_TextChanged" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAttendanceDate"
                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    <%--<asp:Image ID="imgFromDate" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />--%>
                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtAttendanceDate" PopupButtonID="imgFromDate" Enabled="true" />
                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtAttendanceDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" ErrorTooltipEnabled="false" />
                                    <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please enter date."
                                        ControlExtender="MaskedEditExtender1" ControlToValidate="txtAttendanceDate" IsValidEmpty="false"
                                        InvalidValueMessage="From Date is invalid" Display="None" TooltipMessage="Input a date"
                                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                        ValidationGroup="submit" SetFocusOnError="true" />
                                </div>
                            </div>
                        </div>
                        <div class="row" ID="divAttenIncharge" runat="server">
                             <div class="form-group col-lg-3 col-md-6 col-12">
                                 <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Block Name</label>
                                </div>
                                <asp:DropDownList ID="ddlBlock" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                     ToolTip="Block Name" AutoPostBack="True" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" InitialValue="0" />
                                
                             </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>*</sup>--%>
                                    <label>Floor </label>
                                </div>
                                <asp:Panel ID="Panel2" runat="server" TabIndex="4">
                                    <div class="form-group col-md-12 checkbox-list-box">
                                        <asp:CheckBox ID="chkFloor" runat="server" Enabled="false" AutoPostBack="true" OnCheckedChanged="chkFloor_CheckedChanged" />
                                        Select All
                                            <br />

                                        <asp:CheckBoxList ID="cblstFloor" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style" RepeatColumns="5" 
                                        Checked="true" Enabled="false"    runat="server" ToolTip="Click to Select Floor">
                                        </asp:CheckBoxList>
                                
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>

                    <div class="col-12 btn-footer">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Remark" />

                        <asp:Button ID="btnShow" runat="server" Text="Show List" OnClick="btnShow_Click"
                            ValidationGroup="Show" CssClass="btn btn-primary" />

                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            ValidationGroup="Show" CssClass="btn btn-primary" />

                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                            ValidationGroup="Show" CssClass="btn btn-info" />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                    </div>

                    <div class="col-12 btn-footer">
                        <span style="color: Red">* Note : Please Select Remark For Present / Absent / Late.</span>
                        <%--<span style="color: Red">* Note : Please Uncheck student who are Absent</span>
                        (
                            <asp:CheckBox ID="chk1" runat="server" Checked="true" Enabled="false" />
                        Present
                     <asp:CheckBox ID="chk2" runat="server" Checked="false" Enabled="false" />
                        Absent )--%>
                    </div>

                    <div class="col-12">
                        <asp:ListView ID="lvDetails" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div id="demo-grid">
                                    <div class="sub-heading">
                                        <h5>Student List</h5> 
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>RRN No.
                                                </th>
                                                <th>Student Name
                                                </th>
                                                <th>Room Name
                                                </th>
                                                <th>Present/ Absent
                                                </th>
                                                <th>Remark
                                                </th>
                                                <th>Time
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
                                        <%# Eval("REGNO")%>
                                    </td>
                                    <td>
                                        <%# Eval("STUDNAME") %>
                                    </td>
                                    <td>
                                        <%# Eval("ROOM_NAME")%>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkIdno" runat="server" ToolTip='<%# Eval("IDNO") %>' Checked="true" onclick="preventCheckboxClick(event)" /> <%--preventCheckboxClick(event) Added By himanshu tamrakar for bug : 170496--%>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRemark" runat="server" AppendDataBoundItems="True" ToolTip="Please Select Remark for Late"
                                            CssClass="form-control" data-select2-enable="true" onchange="enableDisabled(this);">
                                            <asp:ListItem Selected="True" Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <ajaxToolKit:MaskedEditExtender ID="meeTime" runat="server" TargetControlID="txtTime" MessageValidatorTip="true" MaskType="Time" ErrorTooltipEnabled="true"
                                            OnInvalidCssClass="errordate" Mask="99:99" AcceptAMPM="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeTime"
                                            ControlToValidate="txtTime" EmptyValueMessage="Time is required"
                                            InvalidValueMessage="Time is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="12-Hour clock format Time Enter"
                                            Display="Dynamic" IsValidEmpty="True" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>

                     <div class="col-12">
                        <asp:Repeater ID="lvAdmin" runat="server" Visible="false">
                            <HeaderTemplate>
                                <div class="sub-heading">
                                    <h5>List of Submitted Attendance By Incharge</h5>
                                </div>
                                <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>View
                                            </th>
                                            <th>Hostel Name
                                            </th>
                                            <th>Block Name
                                            </th>
                                            <th>In-Charge Name
                                            </th>
                                            <th>Attendance Date
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ATT_NO") %>'
                                            AlternateText="Edit Record" ToolTip="View Record" OnClick="btnView_Click"  TabIndex="7" />&nbsp;

                                    </td>
                                    <td>
                                        <%# Eval("HOSTELNAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("BLOCK_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("UA_NAME")%>
                                    </td>
                                    <td>
                                        <%# Eval("ATT_DATE")%>
                                    </td>

                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <%-- <script type="text/javascript" language="javascript">
        function enableDisabled(crl) {
            var st = crl.id.split("ctrl");
            var a = st[1].split("_ddlRemark");
            var index = a[0];
            var id = document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_ddlRemark").value;
            if (id == "3") {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = true;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = true;

            }
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = true;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = false;

            }
        }
    </script>--%>
    <script type="text/javascript">  //Added By himanshu tamrakar 05/01/2024
        function preventCheckboxClick(event) {
            event.preventDefault();
        }
    </script>
    <script type="text/javascript" language="javascript">
        function enableDisabled(crl) {
            debugger;
            var st = crl.id.split("ctrl");
            var a = st[1].split("_ddlRemark");
            var index = a[0];
            var id = document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_ddlRemark");

            var ddlRemarkText = id.options[id.selectedIndex].text; //ddlRemarkText added by Saurabh L on 06/01/2023 Purpose: To get ddlRemark Text

            if (ddlRemarkText == "Present" || ddlRemarkText == "PRESENT" || ddlRemarkText == "present") {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = true;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = true;
                $(document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime")).val(""); //this line added by Saurabh L on 18/01/2023 Purpose: To clear Time 
            }
            else if (ddlRemarkText == "Absent" || ddlRemarkText == "ABSENT" || ddlRemarkText == "absent") {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = true;
                $(document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime")).val("");
            }
            else if (ddlRemarkText == "Late" || ddlRemarkText == "LATE" || ddlRemarkText == "late")  // for late
            {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = true;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = false;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = false;
            }
                //else if (id == "4") {
                //    document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = true;
                //    document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = true;
                //    document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime").disabled = false;
                //}
            else {
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").checked = true;
                document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_chkIdno").disabled = false;
                $(document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl" + index + "_txtTime")).val("");
                //document.getElementById("ctl00_ContentPlaceHolder1_lvDetails_ctrl"+index+"_txtTime").disabled=false; 
            }
        }
    </script>
</asp:Content>
