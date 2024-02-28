<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="FacultyAttendanceNotFilled.aspx.cs" Inherits="ACADEMIC_REPORTS_FacultyAttendanceNotFilled" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--===== Data Table Script added by gaurav =====--%>
    <link href="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multi-select/bootstrap-multiselect.js")%>"></script>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .table {
            width: auto !important;
        }
    </style>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAtt"
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
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#example2').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            //var arr = [0];
                            //if (arr.indexOf(idx) !== -1) {
                            //    return false;
                            //} else {
                            return $('#example2').DataTable().column(idx).visible();
                            //}
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            //var arr = [0];
                                            //if (arr.indexOf(idx) !== -1) {
                                            //    return false;
                                            //} else {
                                            return $('#example2').DataTable().column(idx).visible();
                                            //}
                                        }
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            //var arr = [0];
                                            //if (arr.indexOf(idx) !== -1) {
                                            //    return false;
                                            //} else {
                                            return $('#example2').DataTable().column(idx).visible();
                                            //}
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            //var arr = [0];
                                            //if (arr.indexOf(idx) !== -1) {
                                            //    return false;
                                            //} else {
                                            return $('#example2').DataTable().column(idx).visible();
                                            //}
                                        }
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#example2').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                //var arr = [0];
                                //if (arr.indexOf(idx) !== -1) {
                                //    return false;
                                //} else {
                                return $('#example2').DataTable().column(idx).visible();
                                //}
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                //var arr = [0];
                                                //if (arr.indexOf(idx) !== -1) {
                                                //    return false;
                                                //} else {
                                                return $('#example2').DataTable().column(idx).visible();
                                                //}
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                //var arr = [0];
                                                //if (arr.indexOf(idx) !== -1) {
                                                //    return false;
                                                //} else {
                                                return $('#example2').DataTable().column(idx).visible();
                                                //}
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                //var arr = [0];
                                                //if (arr.indexOf(idx) !== -1) {
                                                //    return false;
                                                //} else {
                                                return $('#example2').DataTable().column(idx).visible();
                                                //}
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });

    </script>
    <asp:UpdatePanel ID="updAtt" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                        <asp:RadioButtonList ID="rdbReports" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" OnSelectedIndexChanged="rdbReports_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Selected="True" Value="1">&nbsp;Attendance Tracker&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Attendance Marked & Not-Marked &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">&nbsp;Attendance register & Consolidated Attendance&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="4">&nbsp;Class Attendance Entry Report Of Faculty&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--Attendance register & Consolidated Attendance--%>
                                    </div>
                                    <%-- dvSession Added By Vipul T on dated 22-12-2023 as per Tno:- --%>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="dvSession" visible="false">
                                        <div class="">
                                            <sup>*</sup>
                                            <asp:Label runat="server" >Session</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSessionn" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divsession" visible="false" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlSession1" runat="server" SelectionMode="Multiple" AutoPostBack="true" CssClass="form-control multi-select-demo" AppendDataBoundItems="true" TabIndex="1"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divCollege" visible="false">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSchool" SetFocusOnError="true"
                                            ErrorMessage="Please Select School/Institute Name" InitialValue="0" Display="None" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divDegree" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            ErrorMessage="Select atleast one Degree" InitialValue="0" Display="None" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divBranch" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Programme/Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch1" runat="server" ControlToValidate="ddlBranch"
                                            ErrorMessage="Select atleast one Branch" Display="None" InitialValue="0" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSem" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem1" runat="server" ControlToValidate="ddlsemester"
                                            ErrorMessage="Select atleast one Semester " Display="None" InitialValue="0" ValidationGroup="ShowStudent"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divClg" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:DropDownList ID="ddlClgname" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlClgname"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme." ValidationGroup="teacherallot">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="DivSession1" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div id="divTeacher" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" style="display: none">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Teacher</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divcourse" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <%--<label>Course</label>--%>
                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divSection" visible="false">
                                        <div class="label-dynamic">
                                            <%--     <sup>* </sup>--%>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" TabIndex="4" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="calender">
                                        <div class="label-dynamic" runat="server" id="DivLabel" visible="false">
                                            <sup>*</sup>
                                            <label>Attendance Start Date</label>
                                        </div>
                                          <div class="label-dynamic" runat="server" id="DivLabel2"  visible="false">
                                            <sup>*</sup>
                                            <label>Attendance Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtStartDate1" runat="server">
                                                <i class="fa fa-calendar" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" TabIndex="6" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtStartDate" PopupButtonID="txtStartDate1" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Attendance Start Date" IsValidEmpty="false"
                                                InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" ErrorMessage="Start Date is Invalid (Enter dd/mm/yyyy Format)"
                                                TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                ValidationGroup="submit" SetFocusOnError="True" />
                                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" SetFocusOnError="True"
                                                ValidationGroup="submit" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartDate" Display="None" SetFocusOnError="True" ValidationGroup="ShowStudent" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="calender1">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Attendance End Date</label>
                                        </div>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="txtEndDate1" runat="server">
                                                <i class="fa fa-calendar" runat="server"></i>
                                            </div>
                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="7" CssClass="form-control" placeholder="DD/MM/YYYY" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtEndDate" PopupButtonID="txtEndDate1" />

                                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" OnFocusCssClass="MaskedEditFocus"
                                                DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Attendance End Date"
                                                InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)" Display="None" IsValidEmpty="false"
                                                TooltipMessage="Please Enter Attendance End Date" EmptyValueBlurredText="Empty"
                                                InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ControlToValidate="txtEndDate" Display="None"
                                                ValidationGroup="submit" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtEndDate" Display="None" ValidationGroup="ShowStudent" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnAttReport" runat="server" OnClick="btnAttReport_Click" TabIndex="8" OnClientClick="return validate();"
                                    Text="Attendance Marked-Not Marked Report" ValidationGroup="show" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnAttTracker" runat="server" OnClick="btnAttTracker_Click" TabIndex="9" OnClientClick="return validate1();"
                                    Text="Attendance Tracker Report" CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnAttRegister" runat="server" TabIndex="5" Text="Attendance Register Report" OnClientClick="return validate2();"
                                    CssClass="btn btn-info" Visible="false" OnClick="btnAttRegister_Click" />
                                <asp:Button ID="btnConAtt" runat="server" TabIndex="6" Text="Consolidated Attendance Report" OnClientClick="return validate2();"
                                    CssClass="btn btn-info" Visible="false" OnClick="btnConAtt_Click" />

                                <%-- btnExcel Added By Vipul T on dated 22-12-2023 as per Tno:- --%>
                                <asp:Button ID="btnExcel" runat="server" TabIndex="6" Text="Report (Excel)" OnClick="btnExcel_Click" 
                                    CssClass="btn btn-info" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="10"
                                    Text="Cancel" CssClass="btn btn-warning" />

                                <asp:ValidationSummary ID="vsShow" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="ShowStudent" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAttReport" />
            <asp:PostBackTrigger ControlID="btnAttTracker" />
            <asp:PostBackTrigger ControlID="btnAttRegister" />
            <asp:PostBackTrigger ControlID="btnConAtt" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
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
        function validate() {
            var collegeid = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchool").value;
            var session = document.getElementById("ctl00_ContentPlaceHolder1_ddlSession").value;
            var AttStartDate = $("#ctl00_ContentPlaceHolder1_txtStartDate").val();
            var AttEndDtae = document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value;

            if (collegeid == "0") {
                alert("Please Select School/Institute.");
                return false;
            }
            if (session == "0") {
                alert("Please Select Session.");
                return false;
            }
            if (AttStartDate == "" || AttStartDate == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (AttEndDtae == "" || AttEndDtae == "DD/MM/YYYY") {
                alert("Please Enter Attendance End Date.");
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnAttReport').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script>
        function validate2() {
            var college = document.getElementById("ctl00_ContentPlaceHolder1_ddlClgname").value;
            var session = document.getElementById("ctl00_ContentPlaceHolder1_ddlSession").value;
            var AttStartDate = $("#ctl00_ContentPlaceHolder1_txtStartDate").val();
            var AttEndDtae = document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value;

            if (college == "0") {
                alert("Please Select College & Scheme");
                return false;
            }
            if (session == "0") {
                alert("Please Select Session.");
                return false;
            }
            if (AttStartDate == "" || AttStartDate == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (AttEndDtae == "" || AttEndDtae == "DD/MM/YYYY") {
                alert("Please Enter Attendance End Date.");
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnAttReport').click(function () {
                    validate();
                });
            });
        });
    </script>
    <script>
        function validate1() {
            //var school = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchoolInstitute").value;
            var school = document.getElementById("ctl00_ContentPlaceHolder1_ddlSchool").value;
            var degree = document.getElementById("ctl00_ContentPlaceHolder1_ddlDegree").value;
            var branch = document.getElementById("ctl00_ContentPlaceHolder1_ddlBranch").value;
            var semester = document.getElementById("ctl00_ContentPlaceHolder1_ddlsemester").value;
            var AttStartDate = $("#ctl00_ContentPlaceHolder1_txtStartDate").val();
            var AttEndDtae = document.getElementById("ctl00_ContentPlaceHolder1_txtEndDate").value;
            //if (school == "0") {
            //    alert("Please Select School/Institute Name.");
            //    return false;
            //}
            if (school == "0") {
                alert("Please Select School/Institute Name");
                return false;
            }
            if (degree == "0") {
                alert("Please Select Degree");
                return false;
            }
            if (branch == "0") {
                alert("Please Select Branch.");
                return false;
            }
            if (semester == "0") {
                alert("Please Select Semester.");
                return false;
            }
            if (AttStartDate == "" || AttStartDate == "DD/MM/YYYY") {
                alert("Please Enter Attendance Start Date.");
                return false;
            }
            if (AttEndDtae == "" || AttEndDtae == "DD/MM/YYYY") {
                alert("Please Enter Attendance End Date.");
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnAttReport').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
