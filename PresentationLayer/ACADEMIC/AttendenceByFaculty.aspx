<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AttendenceByFaculty.aspx.cs" Inherits="ACADEMIC_AttendenceByFaculty" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementById("i");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>


    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <style>
        #ctl00_ContentPlaceHolder1_updpnl
        {
            height: auto;
        }
    </style>

    <script type="text/javascript">
        $(document).bind("contextmenu", function (e) {
            e.preventDefault();
        });
        $(document).keydown(function (e) {
            if (e.which === 123) {
                return false;
            }
            else if ((event.ctrlKey && event.shiftKey && event.keyCode == 73) || (event.ctrlKey && event.shiftKey && event.keyCode == 74)) {
                return false;
            }
        });

    </script>

    <script>
        $(document).ready(function () {
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#aaa').DataTable({
                "paging": false,
                "ordering": false,
                "info": false
            });
        }
    </script>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="dvSelection" runat="server">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">STUDENT DAILY ATTENDANCE</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">Selection Criteria for Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form-group col-md-1"></div>
                                    <div class="form-group col-md-2" id="trTeacher" runat="server">
                                        <label><span style="color: red;">*</span> Teacher</label>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="True" TabIndex="2" ValidationGroup="course"
                                            CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvteacher" runat="server" ControlToValidate="ddlTeacher"
                                            Display="None" ErrorMessage="Please Select Course teacher." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><span style="color: red;">*</span> Session</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="2"
                                            ValidationGroup="course" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><span style="color: red;">*</span> Section</label>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" TabIndex="2"
                                            ValidationGroup="course" CssClass="form-control" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><span style="color: red;">*</span> Subject Type</label>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged"
                                            ValidationGroup="course" CssClass="form-control" TabIndex="2"
                                            AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <%-- <asp:ListItem Value="1">Theory</asp:ListItem>
                                <asp:ListItem Value="2">Practical</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            ErrorMessage="Please Select Subject Type." InitialValue="0" ValidationGroup="course"
                                            Display="None"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label><span style="color: red;">*</span> Attendance for</label>
                                        <asp:DropDownList ID="ddlAttFor" runat="server"
                                            TabIndex="2" ValidationGroup="course" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlAttFor_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAttFor" runat="server" ControlToValidate="ddlAttFor"
                                            Display="None" ErrorMessage="Please Select Attendance for." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-1"></div>
                                    <div class="form-group col-md-12">
                                        <p class="text-center">
                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
                                        </p>
                                    </div>

                                </div>
                                <div class="box-footer">
                                    <div class="col-md-12">
                                        <asp:ListView ID="lvCourse" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Course List</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Course Name
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
                                                        <asp:LinkButton ID="lnkbtnCourse" runat="server" CommandArgument='<%# Eval("COURSENO")%>'
                                                            OnClick="lnkbtnCourse_Click" Text='<%# GetCourseName(Eval("COURSENAME"),Eval("SCHEMENAME"),Eval("SECTION"),Eval("SUBJECTTYPE"),Eval("BATCHNAME")) %>'
                                                            ToolTip='<%# Eval("SECTIONNO")%>' CommandName='<%# Eval("BATCH")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div id="dvNext" runat="server" visible="false">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">STUDENT DAILY ATTENDANCE</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>

                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form-group col-md-12">
                                        <label>Course </label>
                                        <asp:Label ID="lblCourse" runat="server" Font-Bold="True"></asp:Label>
                                        <asp:Label ID="lblSection" runat="server" Visible="False"></asp:Label>
                                    </div>

                                    <div class="form-group col-md-12" style="text-align: center; padding-left: 150px;">
                                        <div class="form-group col-md-3">
                                            <label><span style="color: red;">*</span> Attendance Date</label>
                                            <asp:TextBox ID="txtAttendanceDate" runat="server" TabIndex="1" ValidationGroup="submit" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtAttendanceDate_TextChanged" />
                                            <asp:TextBox ID="txtAttendanceDate1" runat="server" CssClass="form-control" Style="display: none" />

                                            <%--<asp:Image ID="imgAttendanceDate" runat="server" ImageUrl="~/images/calendar.png"
                                     Width="16px"  />--%>
                                            <ajaxToolKit:CalendarExtender ID="ceAttendanceDate" runat="server" Format="dd/MM/yyyy"
                                                PopupButtonID="imgAttendanceDate" TargetControlID="txtAttendanceDate" OnClientDateSelectionChanged="datechanged"/>
                                            <ajaxToolKit:MaskedEditExtender ID="meAttendanceDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtAttendanceDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvAttendanceDate" runat="server" ControlExtender="meAttendanceDate"
                                                ControlToValidate="txtAttendanceDate" Display="None" EmptyValueMessage="Please enter Attendance Date"
                                                ErrorMessage="Please Select Attendance Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><span style="color: red;">*</span> Class Type</label>
                                            <asp:DropDownList ID="ddlClassType" runat="server" TabIndex="6" ValidationGroup="Submit"
                                                CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlClassType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Regular Class</asp:ListItem>
                                                <asp:ListItem Value="2">Extra Class</asp:ListItem>
                                                <asp:ListItem Value="3">Alternate Class</asp:ListItem>
                                                <%-- <asp:ListItem Value="4">Off Campus Work</asp:ListItem>--%>
                                                <%-- <asp:ListItem Value="5">Special Class 4</asp:ListItem>
                                                 <asp:ListItem Value="6">Special Class 5</asp:ListItem>
                                                 <asp:ListItem Value="7">Special Class 6</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvClassType" runat="server" ControlToValidate="ddlClassType"
                                                Display="None" ErrorMessage="Please Select Class Type" InitialValue="0" ValidationGroup="Submit">
                                            </asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvPeriodShow" runat="server" ControlToValidate="ddlClassType"
                                                Display="None" ErrorMessage="Please Select Class Type" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label><span style="color: red;">*</span> Topic Covered</label>
                                            <asp:TextBox ID="txtTopic" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false" />
                                            <asp:DropDownList ID="ddlTopics" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="6">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:RequiredFieldValidator ID="rfvTopic" runat="server" ControlToValidate="ddlTopics" Display="None"
                                            ErrorMessage="Please Select Topic Covered" InitialValue="0"  ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>--%>

                                            <%--  added by sumit on 24-01-2020--%>

                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTopics"
                                                Display="None" ErrorMessage="Please Select Topic Covered" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <%--OnSelectedIndexChanged="chkPeriod_SelectedIndexChanged"--%>
                                    <div class="form-group col-md-12" id="dvPeriod" runat="server" style="padding-left: 160px;" visible="false">
                                        <label><span style="color: red;">*</span> Period </label>
                                        <asp:CheckBoxList ID="chkPeriod" runat="server" OnSelectedIndexChanged="chkPeriod_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="form-group col-md-12" id="divExtraClass" runat="server" style="padding-left: 160px;">
                                        <div class="form-group col-md-6">
                                            <label><span style="color: red;"></span>Remark / Extra Topic Covered </label>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12" style="text-align: center; padding-left: 150px;">
                                        <div class="form-group col-md-3" id="trAttby" runat="server" visible="false">
                                            <label><span style="color: red;">*</span> Engaged in place of</label>
                                            <asp:DropDownList ID="ddlAttby" runat="server" AppendDataBoundItems="True" CssClass="form-control" OnSelectedIndexChanged="ddlAttby_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3" id="trCourse" runat="server" visible="false">
                                            <label><span style="color: red;">*</span> Course</label>
                                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <%-- <div class="form-group col-md-3" id="Div1" runat="server">--%>
                                        <div class="form-group col-md-3" id="trSwap" runat="server" visible="false">
                                            <label><span style="color: red;">*</span> Course Taken</label>
                                            <asp:CheckBox ID="chkSwap" runat="server" Text="Yes/No" />
                                        </div>
                                        <%-- </div>--%>
                                    </div>
                                    <div class="box-footer">
                                        <p class="text-center">
                                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show Students"
                                                ValidationGroup="Show" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" TabIndex="9"
                                                Text="Submit" ValidationGroup="Submit" CssClass="btn btn-success" Enabled="False" />
                                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" TabIndex="10"
                                                Text="Clear" CausesValidation="false" CssClass="btn btn-info" Visible="false" />
                                            <asp:Button ID="btnBack" runat="server" TabIndex="11" Text="Back" CausesValidation="false"
                                                OnClick="btnBack_Click" CssClass="btn btn-info" />
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                            <p>
                                                &nbsp;<asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                                <br />
                                                <p id="pmsg" runat="server" class="text-center" visible="false">
                                                    <asp:Label ID="lblMsgw" runat="server" Font-Bold="true" ForeColor="Red" />
                                                </p>
                                                <br />
                                                <div class="col-md-12" style="text-align: center;">
                                                    <div class="form-group col-md-3">
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <label>
                                                            Total Student
                                                        </label>
                                                        <asp:TextBox ID="txtTotStud" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <label>
                                                            Present
                                                        </label>
                                                        <asp:TextBox ID="txtPresent" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-md-2">
                                                        <label>
                                                            Absent
                                                        </label>
                                                        <asp:TextBox ID="txtAbsent" runat="server" CssClass="form-control" Enabled="false" />
                                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <b style="color: red;">Note : </b>&nbsp;Checked = Student Present; Unchecked = Student Absent&nbsp;
                                                </div>
                                                <div class="col-md-12" style="padding-top: 20px">
                                                    <p class="text-center">
                                                        <asp:LinkButton ID="btnDetails" runat="server" CausesValidation="False" CssClass="btn btn-info" Font-Bold="True" OnClick="btnDetails_Click">Show Attendance Details</asp:LinkButton>
                                                        <asp:LinkButton ID="btnRegister" runat="server" CausesValidation="False" CssClass="btn btn-info" Font-Bold="True" OnClick="btnRegister_Click">Attendance Register</asp:LinkButton>
                                                    </p>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <asp:Panel ID="pnlTimeTable" runat="server">
                                                        <div class="table table-responsive">
                                                            <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <%-- <div>--%>
                                                                    <h4>Student List</h4>
                                                                    <table id="aaa" class="table table-hover table-bordered">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" Visible="true" />
                                                                                    Select </th>
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbExtraHead" runat="server" Checked="false" Visible="false" />
                                                                                    Extra Curricular/ Events</th>
                                                                                <th>Reg. No. </th>
                                                                                <th>Name </th>
                                                                                <th>Section </th>
                                                                                <th>Batch </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                    <%--</div>--%>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <%--<td>
                                                                <asp:CheckBox ID="chkRow" runat="server" onclick="CountSelection();" Checked='<%# (Eval("ATT_STATUS").ToString() == "1" ? true : false) %>' ToolTip='<%# (Eval("ATT_STATUS").ToString() == "1" ? true : false) %>' />
                                                                <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                            </td>--%>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkRow" runat="server" onclick="CheckSelectionCount(this)" ToolTip='<%# (Eval("ATT_STATUS").ToString() == "1" ? true : false) %>' />
                                                                            <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                            <asp:HiddenField ID="hidDiscplnrInfo" runat="server" Value='<%# Eval("DISCIDNO")%>' />
                                                                            <asp:HiddenField ID="hidDiscFromDt" runat="server" Value='<%# Eval("FROMDATE")%>' />
                                                                            <asp:HiddenField ID="hidDiscToDt" runat="server" Value='<%# Eval("TODATE")%>' />
                                                                        </td>

                                                                        <td>
                                                                            <asp:CheckBox ID="chkExtraCrclr" runat="server" Enabled="true" onclick="CheckSelectionCount(this)" ToolTip='<%# (Eval("EXTRA_CURR_STATUS").ToString() == "1" ? true : false) %>' />
                                                                        </td>
                                                                        <td><%# Eval("REGNO")%></td>
                                                                        <td><%# Eval("STUDNAME")%></td>
                                                                        <td><%# Eval("SECTIONNAME")%></td>
                                                                        <td><%# Eval("BATCH")%>
                                                                            <asp:HiddenField ID="hidBatchId" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </p>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="dvDates" runat="server" visible="false">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">STUDENT DAILY ATTENDANCE</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <p class="text-center">
                                            <asp:Button ID="btnBackToAtt" runat="server" Text="Back" OnClick="btnBackToAtt_Click" CssClass="btn btn-info" />
                                            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CausesValidation="False" CssClass="btn btn-primary" />
                                        </p>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Height="400px">
                                            <div class="table table-responsive">
                                                <asp:ListView ID="lvAttDone" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <h4>Attendance Done</h4>
                                                            <table class="table table-hover table-bordered">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>Attendance Date
                                                                        </th>
                                                                        <th>Period No
                                                                        </th>
                                                                        <th>Class Type
                                                                        </th>
                                                                        <th>Attendance For
                                                                        </th>
                                                                        <th>Batch
                                                                        </th>
                                                                        <th>Topic Covered
                                                                        </th>
                                                                        <th>Teacher
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
                                                                <asp:Label ID="lblAttDate" runat="server" Text='<%# Eval("ATT_DATE","{0:dd-MM-yyyy}")%> '>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("PERIOD")%>'>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblClassType" runat="server" Text='<%# Eval("CLASS_TYPE")%>'>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAttenFor" runat="server" Text='<%# Eval("COURSE_TYPE")%>'>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("BATCH")%>'>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTopicCov" runat="server" Text='<%# Eval("TOPIC_COVERED")%>'>
                                                                </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTeacher" runat="server" Text='<%# Eval("TEACHER")%>'>
                                                                </asp:Label>
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


                        <div id="dvRegister" runat="server" visible="false">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">STUDENT DAILY ATTENDANCE</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>


                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="form-group col-md-3"></div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span> From Date </label>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ValidationGroup="submit" CssClass="form-control" />
                                        <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgfrm" TargetControlID="txtFromDate" Animated="true" Enabled="true" EnableViewState="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtFromDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                            ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                            ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                        <asp:RequiredFieldValidator ID="rfvFromdate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label><span style="color: red;">*</span> To Date </label>
                                        <asp:TextBox ID="txtTodate" runat="server" TabIndex="2" ValidationGroup="submit" />
                                        <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgto"
                                            TargetControlID="txtTodate" Animated="true" Enabled="true" EnableViewState="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                            OnFocusCssClass="MaskedEditFocus" TargetControlID="txtTodate"
                                            OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                            ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                            ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                        <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtTodate"
                                            Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-3"></div>
                                </div>
                                <div class="box-footer">
                                    <p class="text-center">
                                        <asp:LinkButton ID="btnDayWise" runat="server" Text="Daily Attendance Report"
                                            TabIndex="3" ValidationGroup="Report" OnClick="btnDayWise_Click" CssClass="btn btn-success"><i class="fa fa-file-excel-o" aria-hidden="true"> Daily Attendance Report</i></asp:LinkButton>
                                        <asp:Button ID="btnReturn" runat="server" Text="Back" OnClick="btnBackToAtt_Click"
                                            TabIndex="4" CssClass="btn btn-info" />
                                        <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Report" />
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div id="divMsg" runat="server" />
                        <div id="divScript" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnDayWise" />
            <asp:PostBackTrigger ControlID="ddlTopics" />
        </Triggers>
    </asp:UpdatePanel>



    <script>
        function datechanged(sender, args) {
            var dlClassType = document.getElementById('<%= ddlClassType.ClientID %>');
            dlClassType.selectedIndex = 0;
            var ddlTopics = document.getElementById('<%= ddlTopics.ClientID %>');
            ddlTopics.selectedIndex = 0;
            var dd = document.getElementById('ctl00_ContentPlaceHolder1_txtAttendanceDate').value;
            var res = dd.split("/");
            document.getElementById('ctl00_ContentPlaceHolder1_txtAttendanceDate1').value = res[1] + '/' + res[0] + '/' + res[2];
            __doPostBack("<%=updpnl.UniqueID %>", "");

            document.getElementById('ctl00_ContentPlaceHolder1_chkPeriod').checked = false;
            document.getElementById('ctl00_ContentPlaceHolder1_txtTotStud').value = "";
            document.getElementById('ctl00_ContentPlaceHolder1_txtPresent').value = "";
            document.getElementById('ctl00_ContentPlaceHolder1_txtAbsent').value = "";
            document.getElementById('ctl00_ContentPlaceHolder1_txtTopic').value = "";
            var chkBoxList = document.getElementById('ctl00_ContentPlaceHolder1_chkPeriod');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = false;
            }
        }
    </script>

    <script type="text/javascript" lang="javascript">

        function CheckSelectionCount(chk) {
            debugger;
            var Pcount = 0;
            var extrPCount = 0;
            var Tcount = 0;
            var txtP = document.getElementById('<%=txtPresent.ClientID %>');
            var txtT = document.getElementById('<%=hdfTot.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsent.ClientID %>');
            var frm = document.forms[0]


            ExtraSelectionCount(chk);
            var rows = document.getElementById("aaa").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            Tcount = rows;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");
                var ee = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr");
                if (e.checked == true) {
                    Pcount++;
                }
                if (ee.checked == true) {
                    extrPCount++;
                }
                if (ee.enabled == false) {
                    extrPCount = extrPCount - 1;
                }
            }

            txtT.value = Tcount;
            txtP.value = (Pcount + extrPCount);
            txtA.value = (Tcount - (Pcount + extrPCount));


        }

        function CountSelection() {
            var chkExtraHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbExtraHead');
            var extChk = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkExtraCrclr');

            try {
                var tbl = document.getElementById('aaa');

                if (extChk.checked == true)
                { }
                else {
                    chkExtraHead.checked = false;
                }
            }
            catch (e) {
            }
        }

        function CountSelection1() {
            var selectedItem = 0;
            var totalStudent = 0;

            try {
                //var tbl = document.getElementById('tblStudents');

                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    totalStudent = (tbl.rows.length - 1);

                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;

                        var chk = dataCell.firstChild;

                        if (chk.checked) {
                            selectedItem++;
                        }
                    }
                }
                document.getElementById('<%= txtTotStud.ClientID %>').value = totalStudent;
                document.getElementById('<%= txtPresent.ClientID %>').value = selectedItem;
                document.getElementById('<%= txtAbsent.ClientID %>').value = (totalStudent - selectedItem);

            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }





        function SelectAllExtra() {
            debugger;
            var chkExtraHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbExtraHead');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_cbHead');
            document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkExtraCrclr').disabled = false;

            var tbl = document.getElementById('aaa');

            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkExtra = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_chkExtraCrclr');

                if (chkExtraHead.checked == true) {
                    chkExtra.checked = true;
                    chkHead.checked = false;
                }
                else {
                    chkExtra.checked = false;
                }
            }

        }

        function ExtraSelectionCount(chk) {
            debugger
            var extrPCount = 0;
            var Pcount = 0;
            var Tcount = 0;
            var txtP = document.getElementById('<%=txtPresent.ClientID %>');
            var txtT = document.getElementById('<%=hdfTot.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsent.ClientID %>');
            var frm = document.forms[0]

            var rows = document.getElementById("aaa").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            Tcount = rows;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");
                var ee = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr");

                if (e.checked == true) {
                    Pcount++;
                    //alert("pcount= "+Pcount);
                }
                if (ee.checked == true) {
                    extrPCount++;
                    //alert("extra= "+extrPCount);
                }
            }

            txtT.value = Tcount;
            txtP.value = (Pcount + extrPCount);
            txtA.value = (Tcount - (Pcount + extrPCount));
            var str = chk.id;
            //alert("hello");
            var start = str.indexOf("_ctrl") + 5;
            var end = str.indexOf("_chkRow");
            var eindex = str.substring(start, end);

            var chkRowAll = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_cbHead");
            var extChk = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkExtraCrclr");
            if (chk.checked == true) {
                extChk.checked = false;
                extChk.disabled = true;
                //alert("hi");
            }
            else {
                //New code
                var DiscplnIDNO = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_hidDiscplnrInfo").value;
                if (DiscplnIDNO != "") {
                    var attDate1 = document.getElementById("ctl00_ContentPlaceHolder1_txtAttendanceDate1").value;
                    var fromDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_hidDiscFromDt").value;
                    var toDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_hidDiscToDt").value;

                    var attDate = new Date(attDate1);
                    yearAtt = attDate.getFullYear();
                    monthAtt = attDate.getMonth();
                    dateAtt = attDate.getDate();

                    var fromDate = new Date(fromDate1);
                    yearfrm = fromDate.getFullYear();
                    monthfrm = fromDate.getMonth();
                    datefrm = fromDate.getDate();

                    var toDate = new Date(toDate1);
                    yearto = toDate.getFullYear();
                    monthto = toDate.getMonth();
                    dateto = toDate.getDate();

                    var attDateNew = new Date(yearAtt, monthAtt, dateAtt).getTime();
                    var frmDateNew = new Date(yearfrm, monthfrm, datefrm).getTime();
                    var toDateNew = new Date(yearto, monthto, dateto).getTime();
                    if (attDateNew >= frmDateNew && attDateNew <= toDateNew) {
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkRow").disabled = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkRow").checked = false;

                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkExtraCrclr").disabled = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkExtraCrclr").checked = false;
                    }
                    else {
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkRow").disabled = false;
                        //document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkRow").checked = true;

                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkExtraCrclr").disabled = false;
                    }
                }
                else {
                    extChk.disabled = false;
                }
                //
                //
            }

            if (extChk.checked == true) {
                chkRowAll.checked = false;
            }

            if (chk.checked == false) {
                //    //New Code This code is for headCheck select when list content disciplinary action student and we want to check head checked then user following loop
                var Disablecount = 0;
                var EnableCount = 0;
                var CheckCount = 0
                var UnCheckCount = 0

                for (i = 0; i < rows; i++) {
                    var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");

                    if (e.disabled == true) {
                        Disablecount++;
                    }
                    if (e.disabled == false) {
                        EnableCount++;
                    }

                    if (e.checked == true) {
                        CheckCount++;
                    }
                    if (e.checked == false) {
                        UnCheckCount++;
                    }
                }
                if (CheckCount == EnableCount) {
                    chkRowAll.checked = true;
                }
                if (CheckCount != EnableCount) {
                    chkRowAll.checked = false;
                }
            }
            if (chk.checked == true) {
                //    //New Code This code is for headCheck select when list content disciplinary action student and we want to check head checked then user following loop
                var Disablecount = 0;
                var EnableCount = 0;
                var CheckCount = 0
                var UnCheckCount = 0

                for (i = 0; i < rows; i++) {
                    var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");

                    if (e.disabled == true) {
                        Disablecount++;
                    }
                    if (e.disabled == false) {
                        EnableCount++;
                    }

                    if (e.checked == true) {
                        CheckCount++;
                    }
                    if (e.checked == false) {
                        UnCheckCount++;
                    }
                }
                if (CheckCount == EnableCount) {
                    chkRowAll.checked = true;
                }
                if (CheckCount != EnableCount) {
                    chkRowAll.checked = false;
                }
            }
        }
        function SelectAll(headchk) {
            debugger;
            var rows = document.getElementById("aaa").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");
                //New Code By Amit K 
                var DiscplnIDNO = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscplnrInfo").value;
                if (DiscplnIDNO != "") {
                    //alert(DiscplnIDNO);
                    var attDate1 = document.getElementById("ctl00_ContentPlaceHolder1_txtAttendanceDate1").value;
                    var fromDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscFromDt").value;
                    var toDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscToDt").value;

                    var attDate = new Date(attDate1);
                    yearAtt = attDate.getFullYear();
                    monthAtt = attDate.getMonth();
                    dateAtt = attDate.getDate();

                    var fromDate = new Date(fromDate1);
                    yearfrm = fromDate.getFullYear();
                    monthfrm = fromDate.getMonth();
                    datefrm = fromDate.getDate();

                    var toDate = new Date(toDate1);
                    yearto = toDate.getFullYear();
                    monthto = toDate.getMonth();
                    dateto = toDate.getDate();

                    var attDateNew = new Date(yearAtt, monthAtt, dateAtt).getTime();
                    var frmDateNew = new Date(yearfrm, monthfrm, datefrm).getTime();
                    var toDateNew = new Date(yearto, monthto, dateto).getTime();

                    //alert("att date = " + attDateNew + " from date = " + frmDateNew + "to date = " + toDateNew);

                    if (attDateNew >= frmDateNew && attDateNew <= toDateNew) {
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").disabled = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").checked = false;

                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").checked = false;
                    }
                    else {
                        //alert("else block");
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").disabled = false;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").checked = true;

                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = true;
                    }

                }
                //New Code By Amit K end
                //alert(headchk.checked);
                if (headchk.checked == true) {
                    if (e.disabled == false) {
                        e.checked = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = true;
                    }
                }
                else {
                    //alert("else");
                    //New Code By Amit K 
                    var DiscplnIDNO = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscplnrInfo").value;
                    if (DiscplnIDNO != "") {
                        var attDate1 = document.getElementById("ctl00_ContentPlaceHolder1_txtAttendanceDate1").value;
                        var fromDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscFromDt").value;
                        var toDate1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hidDiscToDt").value;

                        var attDate = new Date(attDate1);
                        yearAtt = attDate.getFullYear();
                        monthAtt = attDate.getMonth();
                        dateAtt = attDate.getDate();

                        var fromDate = new Date(fromDate1);
                        yearfrm = fromDate.getFullYear();
                        monthfrm = fromDate.getMonth();
                        datefrm = fromDate.getDate();

                        var toDate = new Date(toDate1);
                        yearto = toDate.getFullYear();
                        monthto = toDate.getMonth();
                        dateto = toDate.getDate();

                        var attDateNew = new Date(yearAtt, monthAtt, dateAtt).getTime();
                        var frmDateNew = new Date(yearfrm, monthfrm, datefrm).getTime();
                        var toDateNew = new Date(yearto, monthto, dateto).getTime();

                        //alert("att date = " + attDateNew + " from date = " + frmDateNew + "to date = " + toDateNew);

                        if (attDateNew >= frmDateNew && attDateNew <= toDateNew) {
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").disabled = true;
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").checked = false;

                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = true;
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").checked = false;
                        }
                        else {
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").disabled = false;
                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow").checked = false;

                            document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = false;
                        }

                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = false;
                        e.checked = false;
                    }
                    //New Code By Amit K end
                    //document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = false;
                    //e.checked = false;
                }

                document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").checked = false;
            }
            CheckSelectionCount(document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkRow"));

            //document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkExtraCrclr").disabled = true;
        }
        function SelectAll1(mainChk) {
            try {
                var tbl = document.getElementById('tblStudents');
                if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                    for (i = 1; i < tbl.rows.length; i++) {
                        var dataRow = tbl.rows[i];
                        var dataCell = dataRow.firstChild;
                        var chk = dataCell.firstChild;
                        chk.checked = mainChk.checked;
                    }
                }
                CountSelection();
            }
            catch (e) {

                alert("Error: " + e.description);
            }
        }

    </script>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Attendance Entry?"))
                return true;
            else
                return false;
        }
    </script>

    <script>
        function chekHolidayDate() {
            var Date = document.getElementById('<%= txtAttendanceDate.ClientID%>').value;
            __doPostBack("CheckHolidayDate", "false");
        }
    </script>

</asp:Content>

<%--function ExtraSelectionCount(chk) {
            debugger
            var extrPCount = 0;
            var Pcount = 0;
            var Tcount = 0;
            var txtP = document.getElementById('<%=txtPresent.ClientID %>');
            var txtT = document.getElementById('<%=hdfTot.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsent.ClientID %>');
            var frm = document.forms[0]

            var rows = document.getElementById("aaa").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            Tcount = rows;
            
            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");
                var ee = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr");
                
                if (e.checked == true) {
                    Pcount++;
                    //alert("pcount= "+Pcount);
                }
                if (ee.checked == true) {
                    extrPCount++;
                   //alert("extra= "+extrPCount);
                }
            }
          
            txtT.value = Tcount;
            txtP.value = (Pcount + extrPCount);
            txtA.value = (Tcount - (Pcount + extrPCount));
            var str = chk.id;
            //alert("hello");
            var start = str.indexOf("_ctrl") + 5;
            var end = str.indexOf("_chkRow");
            var eindex = str.substring(start, end);
            
            var chkRowAll = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_cbHead");
            var extChk = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + eindex + "_chkExtraCrclr");
            if (chk.checked == true) {
                extChk.checked = false;
                extChk.disabled = true;
                //alert("hi");
            }
            else {
                extChk.disabled = false;
            }

            if (extChk.checked == true) {
                chkRowAll.checked = false;
            }

            if (chk.checked == false) {
                chkRowAll.checked = false;
            }
        }--%>

<%--function SelectAll(headchk) {
            debugger;
            var rows = document.getElementById("aaa").getElementsByTagName("tbody")[0].getElementsByTagName("tr").length;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkRow");
                //var extChk = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr");

                if (headchk.checked == true) {
                    if (e.disabled == false) {
                        e.checked = true;
                        document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = true;
                    }
                }
                else {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").disabled = false;
                    e.checked = false;
                }
                //}

                document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_chkExtraCrclr").checked = false;
            }
            CheckSelectionCount(document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkRow"));

            //document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl0_chkExtraCrclr").disabled = true;
        }--%>