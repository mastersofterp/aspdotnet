<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="CourseRegActivityDefinition.aspx.cs" Inherits="ACTIVITY_CourseRegActivityDefinition" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%--<link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />--%>
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdShowStatus" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdPaymentApplicableForSemWise" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdEligibilityForCrsReg" runat="server" ClientIDMode="Static" />
    
    <script type="text/javascript" language="javascript">
        function SelectAllDegree() {
            debugger;

            var CHK = document.getElementById("<%=chkDegreeList.ClientID%>");
            //var CHK = document.getElementById('ctl00_ContentPlaceHolder1_chkDegreeList');;;
            var checkbox = CHK.getElementsByTagName("input");


            var chkDeg = document.getElementById('ctl00_ContentPlaceHolder1_chkDegree');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkDegreeList_' + i);
                if (chkDeg.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        function SelectAllBranch() {

            var CHK = document.getElementById("<%=chkBranchList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");


            var chkBranch = document.getElementById('ctl00_ContentPlaceHolder1_chkBranch');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkBranchList_' + i);
                if (chkBranch.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }


        function SelectAllSem() {
            var CHK = document.getElementById("<%=chkSemesterList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chksem = document.getElementById('ctl00_ContentPlaceHolder1_chkSemester');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkSemesterList_' + i);
                if (chksem.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        function SelectAllUsers() {
            var CHK = document.getElementById("<%=chkUserRightsList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRights');

            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_chkUserRightsList_' + i);
                if (chkUser.checked == true) {
                    chk.checked = true;
                }
                else {
                    chk.checked = false;
                }
            }
        }

        function SelectAllCoursePattern() {
            var CHK = document.getElementById("<%=ckhCoursePatternList.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");

            var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_ckhCoursePattern');
            for (var i = 0; i < checkbox.length; i++) {
                var chk = document.getElementById('ctl00_ContentPlaceHolder1_ckhCoursePatternList_' + i);
                if (chk != null) {
                    if (chkUser.checked == true) {
                        chk.checked = true;
                    }
                    else {
                        chk.checked = false;
                    }
                }
            }
        }

        //function ShowEligibleForCrsRegistration() {
        //    //  var chkUser = $('#ctl00_ContentPlaceHolder1_chkEligibleForCrsReg');
        //    var chkUser = document.getElementById('ctl00_ContentPlaceHolder1_chkEligibleForCrsReg');
        //    if (chkUser.checked == true) {
        //        $('#ctl00_ContentPlaceHolder1_txtEligiblityForCrsReg').show();
        //    }
        //    else {
        //        $('#ctl00_ContentPlaceHolder1_txtEligiblityForCrsReg').hide();
        //    }
        //}
    </script>

    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvDetail$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvDetail$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-1">
                    <%-- <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item"><a class="nav-link active" href="#tabLC" data-toggle="tab" aria-expanded="true">Activity Definition</a></li>
                        <li class="nav-item"><a class="nav-link" href="#idNotificationMaster" data-toggle="tab" aria-expanded="false">Notification Master</a></li>
                        <li class="nav-item"><a class="nav-link" href="#idNotificationDetails" data-toggle="tab" aria-expanded="false">Notification Details</a></li>
                    </ul>--%>
                    <div class="tab-content">
                        <div class="tab-pane active" id="tabLC">

                            <div class="box-header with-border">
                                <%--<h3 class="box-title">Define Activity for Session</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                                </h3>
                            </div>

                            <div class="box-body">

                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSesActivity"
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

                                <asp:UpdatePanel ID="updSesActivity" runat="server">
                                    <ContentTemplate>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Session </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSessionCollege" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Session." data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSessionCollege_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSessionCollege" runat="server" ValidationGroup="submit" ControlToValidate="ddlSessionCollege" Display="None"
                                                        ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>College</label>--%>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <%--<asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" ToolTip="Please Select School/Institute." AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true" ValidationGroup="submit" TabIndex="1">
                                                    </asp:DropDownList>--%>
                                                    <%--Added for Multiple Selection on 2022 Aug 30--%>

                                                    <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged1" AutoPostBack="true"></asp:ListBox>


                                                    <%--Added for Multiple Selection on 2022 Aug 30 End--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege" SetFocusOnError="true"
                                                        Display="None" ErrorMessage="Please Select School/Institute." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Activity Name </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlActivityName" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Activity." OnSelectedIndexChanged="ddlActivityName_SelectedIndexChanged" AutoPostBack="true">
                                                        <%--AutoPostBack="true" OnSelectedIndexChanged=""--%>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <%--  <asp:ListItem Value="1">Course Registration</asp:ListItem>
                                                        <asp:ListItem Value="2">Redo Course Registration</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="submit" ControlToValidate="ddlActivityName"
                                                        Display="None" ErrorMessage="Please Select Activity." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                    <%--add by maithili [07-09-2022] runat="server" visible="false"--%>
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Session</label>--%>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="ddlSession" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"
                                                        AppendDataBoundItems="true"></asp:ListBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalStartDate" runat="server" class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtStartDate" runat="server" TabIndex="3" CssClass="form-control" AutoComplete="off" />
                                                        <%-- <asp:Image ID="imgCalStartDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtStartDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                            EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtStartDate"
                                                            Display="None" ErrorMessage="Please enter start date." SetFocusOnError="true"
                                                            ValidationGroup="submit" />
                                                        <%--  <asp:CompareValidator ID="valStartDateType"
                                                            runat="server"
                                                            ControlToValidate="txtStartDate"
                                                            ControlToCompare="txtEndDate"
                                                            Display="Dynamic"
                                                            ErrorMessage="Please enter a valid date."
                                                            Operator="LessThan"
                                                            SetFocusOnError="true"
                                                            Type="Date"
                                                            CultureInvariantValues="true"
                                                            ValidationGroup="submit" />--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Start Time</label>
                                                    </div>

                                                    <asp:TextBox ID="txtfrom" runat="server" CssClass="form-control" ToolTip="Please Enter Time" OnTextChanged="txtfrom_TextChanged" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meStarttime" runat="server" TargetControlID="txtfrom"
                                                        Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                        MaskType="Time" />
                                                    <asp:RequiredFieldValidator ID="rfvTimeFrom" runat="server"
                                                        ControlToValidate="txtfrom" Display="None" SetFocusOnError="true"
                                                        ErrorMessage="Please Enter Start Time" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid Start Time"
                                                        ControlToValidate="txtfrom" Display="NONE" SetFocusOnError="true" ValidationGroup="submit"
                                                        ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>End Date</label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="imgCalEndDate" runat="server" class="fa fa-calendar"></i>
                                                        </div>
                                                        <%--onchange="checkdate();"--%>
                                                        <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4" OnTextChanged="txtEndDate_TextChanged" CssClass="form-control" AutoComplete="off" />
                                                        <%--<asp:Image ID="imgCalEndDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />--%>
                                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                            TargetControlID="txtEndDate" PopupButtonID="imgCalEndDate" Enabled="true" EnableViewState="true">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="valEndDate" runat="server" ControlToValidate="txtEndDate"
                                                            Display="None" ErrorMessage="Please enter end date." SetFocusOnError="true" ValidationGroup="submit" />
                                                        <%--<asp:CompareValidator ID="valEndDateType"
                                                            runat="server"
                                                            ControlToValidate="txtEndDate"
                                                            ControlToCompare="txtStartDate"
                                                            Display="Dynamic"
                                                            ErrorMessage="Please enter a valid date."
                                                            Operator="GreaterThan"
                                                            SetFocusOnError="true"
                                                            Type="Date"
                                                            ValidationGroup="submit"
                                                            CultureInvariantValues="true" />--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>End Time</label>
                                                    </div>

                                                    <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" ToolTip="Please Enter Time" OnTextChanged="txtTo_TextChanged" TabIndex="4" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:MaskedEditExtender ID="meEndtime" runat="server" TargetControlID="txtTo"
                                                        Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                        MaskType="Time" />
                                                    <asp:RequiredFieldValidator ID="rfvTimeTo" runat="server"
                                                        ControlToValidate="txtTo" Display="None" SetFocusOnError="true"
                                                        ErrorMessage="Please Enter End Time" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <%--    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please enter a valid End Time"
                                                        ControlToValidate="txtTo" Display="NONE" SetFocusOnError="true" ValidationGroup="submit"
                                                        ValidationExpression="((1[0-2]|0?[0-9]):([0-5][0-9]) ?([AaPp][Mm]))"></asp:RegularExpressionValidator>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-2 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Degree </label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkDegree" runat="server" Text="All Degree" AutoPostBack="true" onClick="SelectAllDegree()" CssClass="select-all-checkbox" TabIndex="6" />
                                                        <asp:CheckBoxList ID="chkDegreeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chkDegreeList_SelectedIndexChanged"
                                                            RepeatColumns="1" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Branch</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkBranch" runat="server" Text="All Branches" onClick="SelectAllBranch()" CssClass="select-all-checkbox" TabIndex="7" />
                                                        <asp:CheckBoxList ID="chkBranchList" runat="server" OnSelectedIndexChanged="chkBranchList_SelectedIndexChanged"
                                                            RepeatColumns="1" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkSemester" runat="server" Text="All Semesters" onclick="SelectAllSem()" CssClass="select-all-checkbox" TabIndex="8" />
                                                        <asp:CheckBoxList ID="chkSemesterList" runat="server" RepeatColumns="2" Width="100%" RepeatDirection="Horizontal" CssClass="checkbox-list-style"
                                                            OnSelectedIndexChanged="chkSemesterList_SelectedIndexChanged">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>User Rights</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="chkUserRights" runat="server" Text="All User" onclick="SelectAllUsers()" CssClass="select-all-checkbox" TabIndex="9" />
                                                        <asp:CheckBoxList ID="chkUserRightsList" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="chkUserRightsList_SelectedIndexChanged">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12 checkbox-list-column" id="dvCoursePattern" runat="server">
                                                    <div class="label-dynamic">
                                                        <label><sup>* </sup>Course Pattern</label>
                                                    </div>
                                                    <div class="checkbox-list-box">
                                                        <asp:CheckBox ID="ckhCoursePattern" runat="server" Text="All Course Pattern" AutoPostBack="true" onClick="SelectAllCoursePattern()"
                                                            OnCheckedChanged="ddlCoursePattern_SelectedIndexChanged" CssClass="select-all-checkbox" />
                                                        <asp:CheckBoxList ID="ckhCoursePatternList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCoursePattern_SelectedIndexChanged"
                                                            RepeatColumns="1" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1">Core Course</asp:ListItem>
                                                            <asp:ListItem Value="2">Elective Course</asp:ListItem>
                                                            <asp:ListItem Value="3">Global Elective Course</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                    <%-- <asp:DropDownList ID="ddlCoursePattern" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                        OnSelectedIndexChanged="ddlCoursePattern_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Core Course</asp:ListItem>
                                                        <asp:ListItem Value="2">Elective Course</asp:ListItem>
                                                        <asp:ListItem Value="3">Global Elective Course</asp:ListItem>                                                       
                                                    </asp:DropDownList>--%>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Activity Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                        <label data-on="Started" data-off="Stopped" for="rdActive"></label>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Show Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdStatus" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdStatus"></label>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Payment Applicable For Semester Wise</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="rdPaySemWiseYesNo" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="rdPaySemWiseYesNo"></label>
                                                    </div>
                                                </div>

                                                <div id="dvEligiblityForCrsReg" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--<asp:CheckBox ID="chkEligibleForCrsReg" runat="server" Text="Eligibility for Course Registration" onclick="ShowEligibleForCrsRegistration()" />--%>
                                                        <label>Eligibility for Course Registration</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="chkEligibleForCrsReg" name="switch" checked />
                                                        <label data-on="Yes" data-off="No" for="chkEligibleForCrsReg"></label>
                                                        <%--<asp:TextBox ID="txtEligiblityForCrsReg" runat="server" MaxLength="2" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"></asp:TextBox>--%>
                                                    </div>
                                                </div>

                                                <div id="dvStudIDType" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>Student Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStudentIDType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        ValidationGroup="submit" ToolTip="Please Select Student ID Type.">
                                                        <%--<asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>

                                                <div id="dvChoiseFor" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label id="lblAttempt" runat="server"></label>
                                                    </div>
                                                    <div>
                                                        <%-- <asp:TextBox ID="txtChoiseFor" runat="server" MaxLength="1" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"></asp:TextBox>--%>
                                                        <asp:TextBox ID="txtChoiseFor" runat="server" MaxLength="1" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"></asp:TextBox>
                                                    </div>
                                                </div>

                                                 <div id="dvNoOfPaperAllowed" runat="server" class="form-group col-lg-3 col-md-3 col-12" visible="false">
                                                    <div class="label-dynamic">
                                                       <label>Number of paper allowed</label>
                                                    </div>
                                                    <div>                                                      
                                                        <asp:TextBox ID="txtNoOfPprAllowed" runat="server" MaxLength="2" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106) && event.keyCode!=32);"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClientClick="return validate();"
                                                OnClick="btnSubmit_Click" TabIndex="12" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="13" OnClick="btnCancel_Click"
                                                CssClass="btn btn-warning" />
                                            <asp:Button ID="btnExcel" runat="server" Text="Excel Report" OnClick="btnExcel_Click" CssClass="btn btn-primary" ToolTip="Export to Excel" />
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvSessionActivities" runat="server" OnItemDataBound="lvSessionActivities_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Session Activities</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center; width: 3%">Edit </th>
                                                                    <th style="text-align: center; width: 4%">Show
                                                                    </th>
                                                                    <th style="text-align: center; width: 4%">Unique Group No</th>
                                                                    <th style="text-align: center; width: 10%">Activity Status </th>
                                                                    <th style="text-align: center; width: 10%">Session</th>
                                                                    <th style="width: 17%">Course Pattern</th>
                                                                    <%--   <th>College </th>
                                                                        <th>Degree </th>
                                                                        <th>Branch </th>--%>
                                                                    <%--   <th>Session </th>
                                                                        <th>Semester </th>--%>
                                                                    <th style="text-align: center; width: 12%">
                                                                    Start Date/Time </ths>
                                                                        <th style="text-align: center; width: 12%">End Date/Time </th>
                                                                    <th style="text-align: center; width: 10%">Show Status </th>
                                                                    <th style="text-align: center; width: 10%">Activity Name </th>
                                                                    <th style="text-align: center; width: 25%">Student Type </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <table class="table table-hover table-bordered mb-0">
                                                            <tr id="MAIN" runat="server" class="col-md-12">
                                                                <td>
                                                                    <tr>
                                                                        <td style="text-align: center; width: 3%">
                                                                            <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false"
                                                                                CommandArgument='<%# Eval("GROUPID") %>'
                                                                                ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                                        </td>
                                                                        <td style="text-align: center; width: 4%">
                                                                            <asp:Panel ID="pnlDetails" runat="server" Style="cursor: pointer; vertical-align: top; float: left">
                                                                                <asp:Image ID="imgExp" runat="server" ImageUrl="~/Images/action_down.png" />
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td style="text-align: center; width: 4%">
                                                                            <asp:Label ID="Label1" Text='<%# Eval("GROUPID")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center; width: 9%">
                                                                            <asp:Label ID="lblActive" Text='<%# Eval("STATUS")%>' ForeColor='<%# Eval("STATUS").ToString().Equals("Started")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center; width: 9%">
                                                                            <asp:Label ID="lbllvSession" Text='<%# Eval("SESSION_NAME")%>' runat="server"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 12%">
                                                                            <asp:Label ID="lblCoursePattern" runat="server"></asp:Label>
                                                                        </td>
                                                                        <%-- <td><%# Eval("COURSE_PATTERN")%></td>--%>
                                                                        <%--    <td><%# Eval("COLLEGE_NAME")%></td>
                                                                <td><%# Eval("CODE")%></td>
                                                                <td><%# Eval("SHORTNAME")%></td>
                                                                <td><%# Eval("SESSION_NAME")%></td><%# String.Format("{0}, {1}", Eval("PE_PERSON_ID"), Eval("SERVICE_PRIZE_ID")) %>'
                                                                <td><%# Eval("SEMESTER")%></td>--%>
                                                                        <%-- <td style="text-align: center; width: 10%"><%# (Eval("START_DATE").ToString() != string.Empty) ? (Eval("START_DATE","{0:dd-MMM-yyyy}")) : Eval("START_DATE" ,"{0:dd-MMM-yyyy}") %></td>
                                                                            <td style="text-align: center; width: 10%"><%# (Eval("END_DATE").ToString() != string.Empty) ? (Eval("END_DATE", "{0:dd-MMM-yyyy}")) : Eval("END_DATE", "{0:dd-MMM-yyyy}")%></td>--%>
                                                                        <td style="text-align: center; width: 10%"><%# (Eval("START_DATE").ToString() != string.Empty) ? (Eval("START_DATE")) : Eval("START_DATE") %></td>
                                                                        <td style="text-align: center; width: 10%"><%# (Eval("END_DATE").ToString() != string.Empty) ? (Eval("END_DATE")) : Eval("END_DATE" )%></td>

                                                                        <td style="text-align: center; width: 8%"><%# Eval("SHOWSTATUS")%></td>
                                                                        <%--<td style="text-align: center; width: 25%"><%# (Eval("CRS_ACTIVITY_NO").ToString() != string.Empty && Eval("CRS_ACTIVITY_NO").ToString()=="1") ? "Course Registration" :(Eval("CRS_ACTIVITY_NO").ToString() != string.Empty && Eval("CRS_ACTIVITY_NO").ToString()=="2") ?"Backlog / Redo Course Registration":"Improvement Course Registration"%></td>--%>
                                                                        <td style="text-align: center; width: 8%"><%# Eval("CRS_ACTIVITY_NAME")%></td>
                                                                        <td style="text-align: center; width: 8%"><%# Eval("IDTYPE_NAME")%></td>

                                                                        <ajaxToolKit:CollapsiblePanelExtender ID="cpeCourt2" runat="server" CollapseControlID="pnlDetails"
                                                                            Collapsed="true" CollapsedImage="~/Images/action_down.png" ExpandControlID="pnlDetails"
                                                                            ExpandedImage="~/Images/action_up.png" ImageControlID="imgExp" TargetControlID="pnlShowCDetails">
                                                                        </ajaxToolKit:CollapsiblePanelExtender>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlShowCDetails" runat="server" CssClass="collapsePanel">

                                                                <asp:ListView ID="lvDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive" style="width: 100%; overflow: scroll; height: 300px;">
                                                                            <table class="table table-striped table-bordered nowrap">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th>College
                                                                                        </th>
                                                                                        <th>Degree
                                                                                        </th>
                                                                                        <th>Branch
                                                                                        </th>
                                                                                        <th>Semester
                                                                                        </th>
                                                                                        <th>Payment Applicable</th>
                                                                                        <th>Eligibility for Course Registration </th>
                                                                                    </tr>
                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server">
                                                                                    </tr>
                                                                                </tbody>

                                                                            </table>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; font-family: Arial; font-size: medium">
                                                                            No Record Found
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <%# Eval("COLLEGE_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("DEGREENAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("LONGNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SEMESTERNAME")%>
                                                                            </td>
                                                                            <td><%# Eval("PAYMENT_APPLICABLE_FOR_SEM_WISE")%></td>
                                                                            <td><%# Eval("ELIGIBILITYFORCRSREG")%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmit" />
                                        <asp:PostBackTrigger ControlID="btnExcel" />
                                        <%--<asp:PostBackTrigger ControlID="lvSessionActivities" />--%>
                                    </Triggers>

                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatMandat(val) {
            $('#rdStatus').prop('checked', val);
        }
        function SetStatPaymentApplicableForSemWise(val) {
            $('#rdPaySemWiseYesNo').prop('checked', val);
        }

        function SetEligibleForCrsReg(val) {
            $('#chkEligibleForCrsReg').prop('checked', val);
        }

        function validate() {
            var msg = '';
            $('#hfdShowStatus').val($('#rdStatus').prop('checked'));
            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdPaymentApplicableForSemWise').val($('#rdPaySemWiseYesNo').prop('checked'));
            $('#hfdEligibilityForCrsReg').val($('#chkEligibleForCrsReg').prop('checked'));

            var rfvCollege = (document.getElementById("<%=lblDYddlSchool.ClientID%>").innerHTML);
            //var rfvSession = (document.getElementById("<%=lblDYddlSession.ClientID%>").innerHTML);
            var ddlSession = $("[id$=ddlSessionCollege]").attr("id");
            var ddlSession = document.getElementById(ddlSession);
            if (ddlSession.value == 0) {
                msg += 'Please Select Session \n';
                //alert('Please Select Session \n', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(ddlSession).focus();
                //return false;
            }

            var ddlCollege = $("[id$=ddlCollege]").attr("id");
            var ddlCollege = document.getElementById(ddlCollege);

            if (ddlCollege.value == 0) {
                msg += 'Please Select ' + rfvCollege + ' \n';
                //alert('Please Select ' + rfvCollege + ' \n', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(ddlCollege).focus();
                //return false;
            }

            var idtxtweb = $("[id$=txtStartDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                msg += 'Please Enter Start Date \n';
                //alert('Please Enter Start Date', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(txtweb).focus();
                //return false;
            }

            var idtxtwebst = $("[id$=txtfrom]").attr("id");
            var txtwebst = document.getElementById(idtxtwebst);
            if (txtwebst.value.length == 0) {
                msg += 'Please Enter Start Time \n';
                //alert('Please Enter Start Time', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(txtwebst).focus();
                //return false;
            }


            var idtxtweb = $("[id$=txtEndDate]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                msg += 'Please Enter End Date \n';
                //alert('Please Enter End Date', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(txtweb).focus();
                //return false;
            }

            var idtxtwebet = $("[id$=txtTo]").attr("id");
            var txtwebet = document.getElementById(idtxtwebet);
            if (txtwebet.value.length == 0) {
                msg += 'Please Enter End Time \n';
                //alert('Please Enter End Time', 'Warning!');
                ////$(txtweb).css('border-color', 'red');
                //$(txtwebet).focus();
                //return false;
            }

            if (parseInt($('#ctl00_ContentPlaceHolder1_ddlStudentIDType').val()) <= 0)
                msg += 'Please Select Student Type. \n';

            if (msg != '') {
                alert(msg, 'Warning!');
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });

        $("#ctl00_ContentPlaceHolder1_ddlActivityName").change(function () {
            var activityNo = $("#ctl00_ContentPlaceHolder1_ddlActivityName").val();
            if (activityNo > 1)
                $("#ctl00_ContentPlaceHolder1_dvCoursePattern").hide();
            else
                $("#ctl00_ContentPlaceHolder1_dvCoursePattern").show();
        });

        function DisplayStudentIDType(e) {
            //var activityNo = $("#rdPaySemWiseYesNo").prop('checked');
            //if ($("#rdPaySemWiseYesNo").prop('checked'))
            //    $("#ctl00_ContentPlaceHolder1_dvStudIDType").show();
            //else
            //    $("#ctl00_ContentPlaceHolder1_dvStudIDType").hide();
            ////dvStudIDType
        }
    </script>

    <script>
        $(document).ready(function () {
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                $($.fn.dataTable.tables(true)).DataTable()
                    .columns.adjust()
                    .responsive.recalc();
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <script>
        function checkdate() {

            var idtxtStartDate = $("[id$=txtStartDate]").attr("id");
            var txtSweb = document.getElementById(idtxtStartDate);
            var idtxtEndDate = $("[id$=txtEndDate]").attr("id");
            var txtEweb = document.getElementById(idtxtEndDate);
            //if (txtSweb.value.length == 0) {
            //    alert('Please Enter Start Date', 'Warning!');
            //    $('#<%= txtEndDate.ClientID %>').val('');
            //    $(txtSweb).focus();
            //    return false;
            //}

            //alert('in');
            var StartDate = document.getElementById("<%=txtStartDate.ClientID%>").value;
            var EndDate = document.getElementById("<%=txtEndDate.ClientID%>").value;

            var startrange = new Date(Date.parse(StartDate));
            var endrange = new Date(Date.parse(EndDate));
            //alert(startrange);
            //alert(endrange);
            if (endrange < startrange) {
                //alert('in true');
                alert("Please Select End date is greater than the Start date.");
                $('#<%= txtEndDate.ClientID %>').val('');
                return false;
            }
            return true;
        }


    </script>

    <script type="text/javascript">

        function checkEndDate(sender, args) {

            var startDate = new Date($("#txtStartDate").val());
            var endDate = new Date($("#txtEndDate").val());
            alert(startDate);
            alert(endDate);
            if (startDate > endDate) {
                alert("End date must be after start date");
                $("#txtEndDate").val("");
            }


            // I change the < operator to >

            //if (sender._selectedDate > new startrange) {
            //    alert("Please Select End date is greater than the Start date.");
            //    sender._selectedDate = new Date();
            //    // set the date back to the current date
            //    sender._textbox.set_Value('')
            //}

        }
    </script>

</asp:Content>
