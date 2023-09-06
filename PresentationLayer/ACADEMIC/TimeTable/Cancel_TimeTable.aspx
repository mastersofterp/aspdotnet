<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Cancel_TimeTable.aspx.cs" Inherits="ACADEMIC_TIMETABLE_Cancel_TimeTable" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlTimeTable .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
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
    <asp:UpdatePanel ID="updTimeTable" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true">School/Institute Name</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchoolInstitute" TabIndex="1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSchoolInstitute"
                                            Display="None" ErrorMessage="Please Select  College & Scheme" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true">Session</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true" runat="server"
                                            TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" TabIndex="3" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select ddlDepartment" InitialValue="0" ValidationGroup="course">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="4" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/ Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"
                                            TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/ Branch" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                            TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator  ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true">Semester</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged" AutoPostBack="True"
                                            TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true">Section</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            TabIndex="8" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlSlotType" runat="server" Font-Bold="true">Slot Type</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSlotType" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            TabIndex="9" OnSelectedIndexChanged="ddlSlotType_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSlotType"
                                            Display="None" ErrorMessage="Please Select Slot Type" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDate" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table Start Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgStartDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged" TabIndex="10" data-mask="" Style="z-index: 0" onblur="return checkDateValidation();" />
                                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Time Table Start Date." ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="ceDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="imgStartDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeDate" runat="server" TargetControlID="txtStartDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" EmptyValueMessage="Please Enter Time Table Start Date"
                                                ControlExtender="meeDate" ControlToValidate="txtStartDate" IsValidEmpty="true"
                                                InvalidValueMessage="Date is Invalid!" Display="None" TooltipMessage="Input a Start Date"
                                                ErrorMessage="Please Enter Time Table Start Date." EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />


                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Time Table End Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="imgEndDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" data-inputmask="'alias': 'dd/mm/yyyy'"
                                                AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged" TabIndex="11" data-mask="" Style="z-index: 0" onblur="return checkDateValidation();" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndDate"
                                                Display="None" ErrorMessage="Please Enter Time Table End Date" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="imgEndDate" Enabled="true"
                                                EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEndDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" EmptyValueMessage="Please Enter Time Table End Date"
                                                ControlExtender="meeDate" ControlToValidate="txtEndDate" IsValidEmpty="true"
                                                InvalidValueMessage="Date is Invalid!" Display="None" TooltipMessage="Input a Date"
                                                ErrorMessage="Please Enter Time Table End Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                SetFocusOnError="true" />



                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Cancellation Type</label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoCancelType" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="rdoCancelType_SelectedIndexChanged" RepeatDirection="Horizontal" TabIndex="12">
                                            <asp:ListItem Selected="True" Value="0">Time Table &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">Attendance &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Both &nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>



                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" class="btn btn-primary" OnClick="btnShow_Click"
                                    TabIndex="13" ValidationGroup="show" />

                                <asp:Button ID="btnCancel" runat="server" Text="Submit" class="btn btn-primary" OnClick="btnCancel_Click" Visible="false"
                                    TabIndex="14" OnClientClick="return AlertMsg();" />

                                <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-warning"
                                    TabIndex="15" OnClientClick="return true;"
                                    OnClick="btnClear_Click" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="show" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-5 col-md-6 col-12" id="divDateDetails" runat="server" visible="false">
                                        <br />
                                        <p class="text-center" style="border-style: double; font-size: 14px; font-weight: bold; color: #3c8dbc;">
                                            <asp:Label ID="lblTitleDate" runat="server" Text="Selected Session Start & End Date :"></asp:Label>
                                        </p>
                                    </div>
                                    <div class="form-group col-lg-7 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note</h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>You Can't Cancel Time Table if  Attendance Already Marked Or Alternate Faculty Assigned.</span></p>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>REGULAR_TT : REGULAR TIME TABLE & SHIFT_TT : SPECIAL TIME TABLE.</span></p>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlTimeTable" runat="server" Visible="false">
                                <div class="col-12 ">
                                    <%--<table id="example" class="table table-bordered table-hover table-fixed small" style="margin-bottom: 0px;width: 100%" >--%>
                                    <asp:ListView ID="lvTimeTable" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Datewise Time Table / Attendance</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" id="tablehead" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th style="text-align: center;">Select All
                                                                                <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" TabIndex="13" />
                                                        </th>
                                                        <th>Sr.No.</th>
                                                        <%-- <th>Session</th>
                                                           <th>Degree</th>--%>
                                                        <th>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <%--<th>Regulation</th>--%>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>Time Table Date</th>
                                                        <th>Day</th>
                                                        <th>Faculty</th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlCourse_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                        <th>SlotName</th>
                                                        <th id="Timetable">Time Table</th>
                                                        <th>Att. Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>

                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkTTNO" runat="server" TabIndex="14" ToolTip="Select to Cancel" />
                                                </td>
                                                <td><%# Container.DataItemIndex+1 %></td>
                                                <asp:HiddenField ID="hfTTNO" runat="server" Value='<%# Eval("TTNO") %>' />
                                                <asp:HiddenField ID="hfTempAttStatus" runat="server" Value='<%# Eval("TEMP_ATT_STATUS") %>' />
                                                <asp:HiddenField ID="hfIsAlternate" runat="server" Value='<%# Eval("ISALTERNATE") %>' />

                                                <asp:HiddenField ID="hfATT_NO" runat="server" Value='<%# Eval("ATT_NO") %>' />
                                                <td><%# Eval("SHORTNAME")%></td>
                                                <%--<td><%# Eval("SCHEMENAME")%></td>--%>
                                                <td><%# Eval("SEMESTERNAME")%></td>
                                                <td><%# Eval("SECTIONNAME")%></td>
                                                <td><%# Eval("BATCHNAME")%></td>
                                                <td>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("TIME_TABLE_DATE")%>'></asp:Label></td>
                                                <td><%# Eval("DAY_NAME")%></td>
                                                <td><%# Eval("FACULTYNAME")%></td>
                                                <td><%# Eval("FULL_COURSE_NAME")%></td>
                                                <td><%# Eval("SLOTNAME")%></td>
                                                <td>
                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblAtt_status" runat="server" Text='<%# Eval("ATT_STATUS")%>'></asp:Label></td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <%--</table>--%>
                                    <%-- </asp:Panel>--%>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>

            <script language="javascript" type="text/javascript">
                function totAll(headchk) {
                    var frm = document.forms[0]
                    for (i = 0; i < document.forms[0].elements.length; i++) {
                        var e = frm.elements[i];
                        if (e.type == 'checkbox') {
                            if (headchk.checked == true) {
                                if (e.disabled == false)
                                    e.checked = true;
                            }
                            else
                                e.checked = false;
                        }
                    }
                }


                function AlertMsg() {

                    var tt = $("#ctl00_ContentPlaceHolder1_rdoCancelType_0").is(':checked');
                    var att = $("#ctl00_ContentPlaceHolder1_rdoCancelType_1").is(':checked');
                    var both = $("#ctl00_ContentPlaceHolder1_rdoCancelType_2").is(':checked');
                    if (tt == true && att == false && both == false) {
                        var result = confirm('Do you really want to Cancel the Time Table ?');
                        if (result == true)
                            return true;
                        else
                            return false;
                    }
                    else if (tt == false && att == true && both == false) {
                        var result = confirm('Do you really want to Cancel the Attendance ?');
                        if (result == true)
                            return true;
                        else
                            return false;
                    }
                    else if (tt == false && att == false && both == true) {
                        var result = confirm('Do you really want to Cancel Both Time Table And Attendance ?');
                        if (result == true)
                            return true;
                        else
                            return false;
                    }

                }

                function checkDateValidation() {
                    var toindate = document.getElementById('<%=txtStartDate.ClientID %>').value;
                    var infromdate = document.getElementById('<%=txtEndDate .ClientID %>').value;
                    var date = infromdate.split("/");
                    var day = date[0];
                    var month = date[1] - 1;
                    var year = date[2];
                    var FromDate = new Date(year, month, day);
                    var tdate = todate.split("/");
                    var tday = tdate[0];
                    var tmonth = tdate[1] - 1;
                    var tyear = tdate[2];
                    var TDate = new Date(tyear, tmonth, tday);
                    alert(FromDate);
                    alert(TDate);
                    if (toindate != "" && infromdate != "")
                        if (FromDate > TDate) {
                            document.getElementById('<%=txtStartDate.ClientID %>').value = '';
                        document.getElementById('<%=txtEndDate.ClientID %>').focus();
                        alert('To Date should not be less than From Date');
                    }
            }

            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

