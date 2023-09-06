<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AlternetAttendance.aspx.cs" Inherits="ACADEMIC_TimeTable_AlternetAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnlLv .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_pnlMutual .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%-- <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>

    <script src="http://code.jquery.com/ui/1.9.1/jquery-ui.js"></script>

    <script>
        function UserDeleteConfirmation() {
            return  confirm("Are you sure you want to submit?");
        }
    </script>

    <script language="javascript" type="text/javascript">
        function SelectSingleCheckbox(Chkid){ 
            var chkbid = document.getElementById(Chkid);
            var chkList = document.getElementsByTagName("input");
            for (i = 0; i < chkList.length; i++) {
                if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                    chkList[i].checked = false;
                }
            }
        }
    </script>--%>

    <%-- <script type="text/javascript">
        function SelectSingleCheckboxFacCourse(Chkid) {
            var tblFruits = document.getElementById("ctl00_ContentPlaceHolder1_lvFacultyCourse_tblFacCourseList");
            var chks = tblFruits.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                chks[i].onclick = function () {
                    for (var i = 0; i < chks.length; i++) {
                        if (chks[i] != this && this.checked) {
                            chks[i].checked = false;
                        }
                    }
                };
            }
        };
    </script>--%>

    <%-- <style type="text/css">
        @media (min-width: 1200px)
        {
            .content-wrapper .container
            {
                width: 1300px;
            }
        }
    </style>--%>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#mytable').DataTable().column(idx).visible();
                            }
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
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
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
                var table = $('#mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#mytable').DataTable().column(idx).visible();
                                }
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
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });


        function SelectSingleCheckbox(Chkid) {
            var chkbid = document.getElementById(Chkid);
            var chkList = document.getElementsByTagName("input");
            for (i = 0; i < chkList.length; i++) {
                if (chkList[i].type == "checkbox" && chkList[i].id != chkbid.id) {
                    chkList[i].checked = false;
                }
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Alternate Attendance</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Cancel Alternate Attendance</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <%--Attendance Tab--%>
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
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
                                <asp:UpdatePanel ID="updatt" runat="server">
                                    <ContentTemplate>

                                        <div id="divCourses" runat="server">
                                            <div class="container-fluid">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Attendance Date </label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <asp:LinkButton ID="imgCal2" runat="server" Enabled="false">
                                                                <%--<span class="fa fa-calendar" aria-disabled="true"  id="imgCal1"></span>--%>
                                                                 <%--<span class="fa fa-calendar"   id="Span1"></span>--%>
                                                                        </asp:LinkButton>
                                                                        <i class="fa fa-calendar" id="imgCal1"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAttDate" runat="server" TabIndex="1" ValidationGroup="Submit" AutoPostBack="true" CssClass="form-control" placeholder="DD/MM/YYYY" OnTextChanged="txtAttDate_TextChanged" />
                                                                    <%--<ajaxtoolkit:CalendarExtender OnClientShown="changeCalenderDateColor" ID="ceExamDate" runat="server" Format="dd/MM/yyyy" OnClientDateSelectionChanged="detect_sunday" PopupButtonID="imgCal1" TargetControlID="txtAttDate" />--%>
                                                                    <ajaxtoolkit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgCal1" TargetControlID="txtAttDate" />
                                                                    <ajaxtoolkit:MaskedEditExtender ID="meExamDate" runat="server" Mask="99/99/9999" MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtAttDate" />
                                                                    <ajaxtoolkit:MaskedEditValidator ID="mvExamDate" runat="server" ControlExtender="meExamDate" ControlToValidate="txtAttDate" Display="None" EmptyValueMessage="Please Enter Exam Date" ErrorMessage="Please Enter Exam Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Exam Date is invalid" IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Submit" />
                                                                    <asp:RequiredFieldValidator ID="rfvExamDate" runat="server" ControlToValidate="txtAttDate" Display="None" ErrorMessage="Please Select Attendance Date" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-control" TabIndex="2" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInstitute" ValidationGroup="Show"
                                                                    Display="None" ErrorMessage="Please Select School/Instittue." SetFocusOnError="true" InitialValue="0" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-control" TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" ValidationGroup="Show"
                                                                    Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="true" InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Attendance Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAttType" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-control" OnSelectedIndexChanged="ddlAttType_SelectedIndexChanged" TabIndex="2" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlAttType" ValidationGroup="Show"
                                                                    Display="None" ErrorMessage="Please Select Attendance Type" SetFocusOnError="true" InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divddldept">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Faculty Department</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-contro" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" TabIndex="3" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDept" ValidationGroup="Show"
                                                                    Display="None" ErrorMessage="Please Select Department" SetFocusOnError="true" InitialValue="0" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--Schedule Faculty List View--%>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlLv" runat="server" >
                                                            <asp:ListView ID="lvCourse" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>SUBJECT LIST</h5>
                                                                        <span class="pull-right">
                                                                            <label>Note :</label><b><span style="color: #FF0000"> Disabled checkbox reflects that the attendance is already marked!</span></b></span>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <th>Select</th>
                                                                                <th>Schedule Subject</th>
                                                                                <th>Att. Date</th>
                                                                                <th class="text-center">Schedule Slot</th>
                                                                                <%-- <th class="text-center">Schedule Faculty</th>--%>
                                                                                <th>Taken Subject</th>
                                                                                <th id="takenfacultyhead" runat="server">Taken Faculty</th>
                                                                                <th>Taken Slot</th>
                                                                                <th>Sub. Date</th>
                                                                                <th>Transfer Type</th>
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
                                                                            <asp:CheckBox ID="chkCourse" runat="server" ToolTip='<%# Eval("COURSENO")%>' TabIndex="4" AutoPostBack="true" OnClick="javascript:SelectSingleCheckbox(this.id)" OnCheckedChanged="chkCourse_CheckedChanged" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" ToolTip='<%# Eval("AANO")%>' Text='<%# Eval("COURSE")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnSem1" runat="server" Value='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblDate" runat="server" ToolTip='<%# Eval("SESSIONNO")%>' Text='<%# (String.IsNullOrEmpty(Eval("ATTENDANCE_DATE").ToString())?"-":Eval("ATTENDANCE_DATE"))%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnSection" runat="server" Value='<%# Eval("SECTIONNO")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnTTNO" runat="server" Value='<%# Eval("TTNO")%>' Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSlot" runat="server" ToolTip='<%# Eval("SLOTNO")%>' Text='<%# Eval("SLOT")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnBatch" runat="server" Value='<%# Eval("BATCHNO")%>' Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <%-- <asp:Label ID="lblScheduleName" runat="server" ToolTip='<%# Eval("SCHEDULE_UANO")%>' Text='<%# (String.IsNullOrEmpty(Eval("SCHEDULE_NAME").ToString())?"-":Eval("SCHEDULE_NAME"))%>'></asp:Label>--%>
                                                                            <asp:Label ID="lblScheduleName" runat="server" ToolTip='<%# Eval("SCHEDULE_UANO")%>' Text='<%# (String.IsNullOrEmpty(Eval("TAKEN_COURSE").ToString())?"-":Eval("TAKEN_COURSE"))%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnScheme" runat="server" Value='<%# Eval("SCHEMENO")%>' Visible="false" />
                                                                        </td>
                                                                        <td id="takenfaculty" runat="server">
                                                                            <asp:DropDownList ID="ddlFaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" Enabled="false" TabIndex="5">
                                                                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:HiddenField ID="hdnTakenUano" runat="server" Value='<%# Eval("TAKEN_UANO1")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnTakenUano1" runat="server" Value='<%# Eval("TAKEN_UANO")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnDayno" runat="server" Value='<%# Eval("DAYNO")%>' Visible="false" />
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblTakenSlot" runat="server" Text='<%# (String.IsNullOrEmpty(Eval("TAKENSLOT").ToString())?"-":Eval("TAKENSLOT"))%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblSubmissionDate" runat="server" ToolTip='<%# Eval("TRANS_DATE")%>' Text='<%# (String.IsNullOrEmpty(Eval("TRANS_DATE").ToString())?"-":Eval("TRANS_DATE"))%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnSemester" runat="server" Value='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblTransferType" runat="server" ToolTip='<%# Eval("TRANSFER_TYPE")%>' Text='<%# (String.IsNullOrEmpty(Eval("CLASSTYPE").ToString())?"-":Eval("CLASSTYPE"))%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnTakenCourse" runat="server" Value='<%# Eval("TAKEN_COURSENO")%>' Visible="false" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                    <%--Taken Faculty List View--%>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlMutual" runat="server">
                                                            <asp:ListView ID="lvFacultyCourseMutual" runat="server" Visible="false">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div>
                                                                            <h5>
                                                                                <label class="label label-default">SUBJECT OF SELECTED FACULTY</label>
                                                                            </h5>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFacCourseList" runat="server">
                                                                                <thead>
                                                                                    <tr class="bg-light-blue">
                                                                                        <th>Select</th>
                                                                                        <th>Subject</th>
                                                                                        <th style="display: none">Main Slot</th>
                                                                                        <th style="display: none">Slot To Be Taken</th>
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
                                                                            <asp:CheckBox ID="chkFacCourse" runat="server" ToolTip='<%# Eval("COURSENO")%>' TabIndex="6" OnClick="javascript:SelectSingleCheckboxFacCourse(this.id)" />
                                                                        </td>
                                                                        <td>
                                                                            <%-- <asp:Label ID="lblFacCourseName" runat="server" Text='<%# Eval("COURSE")%>' ToolTip='<%# Eval("SLOTNO")%>'></asp:Label>--%>
                                                                            <asp:Label ID="lblFacCourseName" runat="server" Text='<%# Eval("COURSE")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnScheduleUano" runat="server" Value='<%# Eval("SCHEDULE_UANO")%>' Visible="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:HiddenField ID="hdnSem" runat="server" Value='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnBatch" runat="server" Value='<%# Eval("BATCHNO")%>' Visible="false" />
                                                                            <%--<asp:Label ID="lblFacSlot" runat="server" ToolTip='<%# Eval("SLOTNO")%>' Text='<%# Eval("MAIN_SLOT")%>'></asp:Label>--%>
                                                                        </td>
                                                                        <td style="display: none">
                                                                            <%--<asp:Label ID="lblTakenSlot" runat="server" ToolTip='<%# Eval("TAKEN_SLOTNO")%>' Text='<%# (String.IsNullOrEmpty(Eval("TAKEN_SLOT").ToString())?"-":Eval("TAKEN_SLOT"))%>'></asp:Label>--%>
                                                                            <asp:HiddenField ID="hdnSection" runat="server" Value='<%# Eval("SECTIONNO")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnScheme" runat="server" Value='<%# Eval("SCHEMENO")%>' Visible="false" />
                                                                            <asp:HiddenField ID="hdnSession" runat="server" Value='<%# Eval("SESSIONNO")%>' Visible="false" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

                                                        <asp:ListView ID="lvFacultyCourse" runat="server" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h3>Course of Selected Faculty</h3>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblFacCourseList" runat="server">
                                                                    <thead>
                                                                        <tr class="bg-light-blue">
                                                                            <th>Select</th>
                                                                            <th>Subject</th>
                                                                            <th>Main Slot</th>
                                                                            <th>Slot To Be Taken</th>
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
                                                                        <asp:CheckBox ID="chkFacCourse" runat="server" ToolTip='<%# Eval("COURSENO")%>' TabIndex="6" OnClick="javascript:SelectSingleCheckboxFacCourse(this.id)" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFacCourseName" runat="server" ToolTip='<%# Eval("MAIN_SLOTNO")%>' Text='<%# Eval("COURSE")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnScheduleUano" runat="server" Value='<%# Eval("SCHEDULE_UANO")%>' Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFacSlot" runat="server" ToolTip='<%# Eval("MAIN_SLOTNO")%>' Text='<%# Eval("MAIN_SLOT")%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnSem" runat="server" Value='<%# Eval("SEMESTERNO")%>' Visible="false" />
                                                                        <asp:HiddenField ID="hdnBatch" runat="server" Value='<%# Eval("BATCHNO")%>' Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTakenSlot" runat="server" ToolTip='<%# Eval("TAKEN_SLOTNO")%>' Text='<%# (String.IsNullOrEmpty(Eval("TAKEN_SLOT").ToString())?"-":Eval("TAKEN_SLOT"))%>'></asp:Label>
                                                                        <asp:HiddenField ID="hdnSection" runat="server" Value='<%# Eval("SECTIONNO")%>' Visible="false" />
                                                                        <asp:HiddenField ID="hdnScheme" runat="server" Value='<%# Eval("SCHEMENO")%>' Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <%--Buttons--%>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" TabIndex="7">
                                              <i class="fa fa-eye"></i> Show
                                            </asp:LinkButton>
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" Text="Submit" TabIndex="8"
                                                ValidationGroup="Submit" Enabled="false" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                                            <asp:Button ID="btnDetailReport" runat="server" Text="Alternate Attendance Report" TabIndex="9" CssClass="btn btn-info" ValidationGroup="AttDate"
                                                OnClick="btnDetailReport_Click" Visible="false" />
                                            <asp:Button ID="Button1" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="10" />
                                            <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AttDate" />
                                        </div>

                                        <div id="divMsg" runat="Server">
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnShow" />
                                        <asp:PostBackTrigger ControlID="btnDetailReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <%--Cancel Tab--%>
                            <div class="tab-pane" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
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
                                        <div id="div7" runat="server">
                                            <div class="container-fluid">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlInstitute1" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlInstitute1_SelectedIndexChanged" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlInstitute1" ValidationGroup="ShowCan"
                                                                    Display="None" ErrorMessage="Please Select School/Instittue" SetFocusOnError="true" InitialValue="0" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree1" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                                    CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree1" ValidationGroup="ShowCan"
                                                                    Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="true" InitialValue="0" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12 ">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Cancel Attendance Date</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon" id="Div2" runat="server">
                                                                        <i id="imgCal" class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAttDateCan" runat="server" ValidationGroup="ShowCan" CssClass="form-control"
                                                                        placeholder="DD/MM/YYYY" TabIndex="1" AutoPostBack="true" />
                                                                    <ajaxtoolkit:CalendarExtender ID="calExtCan" runat="server" Format="dd/MM/yyyy" TargetControlID="txtAttDateCan"
                                                                        PopupButtonID="imgCal" />
                                                                    <ajaxtoolkit:MaskedEditExtender ID="meetxtAttDateCan" runat="server" OnInvalidCssClass="errordate"
                                                                        TargetControlID="txtAttDateCan" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                    <ajaxtoolkit:MaskedEditValidator ID="mevtxtAttDateCan" runat="server" ControlExtender="meetxtAttDateCan"
                                                                        ControlToValidate="txtAttDate" EmptyValueMessage="Please Enter Attendance Date"
                                                                        InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                        TooltipMessage="Please Enter Cancel Attendance Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                        ValidationGroup="ShowCan" SetFocusOnError="True" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAttDateCan"
                                                                        Display="None" ErrorMessage="Please Enter Cancel Attendance Date" ValidationGroup="ShowCan" SetFocusOnError="true">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 mt-4">
                                                                <asp:Button ID="btnShowAtt" runat="server" Text="Show" OnClick="btnShowAtt_Click" CssClass="btn btn-primary" TabIndex="1" ValidationGroup="ShowCan" />
                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ShowCan" />
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:ListView ID="lvCanAtt" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div>
                                                                            <div>
                                                                                <h5>
                                                                                    <label class="label label-default">ALTERNATE ATTENDANCE LIST</label>
                                                                                </h5>
                                                                                <table class="table table-bordered table-hover table-fixed display" id="mytable">
                                                                                    <thead>
                                                                                        <tr class="bg-light-blue">
                                                                                            <th class="text-center">Select</th>
                                                                                            <th class="text-center">Subject</th>
                                                                                            <th class="text-center">Schedule Faculty</th>
                                                                                            <th class="text-center">Taken Faculty</th>
                                                                                            <th class="text-center">Transfer Date</th>
                                                                                            <th class="text-center">Slot To Be Taken</th>
                                                                                            <th class="text-center">Transfer Type</th>
                                                                                        </tr>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </thead>
                                                                                </table>
                                                                            </div>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkCanCourse" runat="server" ToolTip='<%# Eval("AANO")%>' TabIndex="1" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCanCourseName" runat="server" ToolTip='<%# Eval("SCHEDULE_COURSENO")%>' Text='<%# Eval("SUBJECT")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCanScheduleFac" runat="server" ToolTip='<%# Eval("SCHEDULE_UANO")%>' Text='<%# Eval("SCHEDULE_FACULTY")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCanTakenFac" runat="server" ToolTip='<%# Eval("TAKEN_UANO")%>' Text='<%# Eval("TAKEN_FACULTY")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblTranDate" runat="server" ToolTip='<%# Eval("SLOTNO")%>' Text='<%# Eval("TRANS_DATE")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCanSlot" runat="server" ToolTip='<%# Eval("SLOTNO")%>' Text='<%# Eval("SLOT")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblCanAttType" runat="server" ToolTip='<%# Eval("TRANSFER_TYPE")%>' Text='<%# Eval("CLASSTYPE")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnCanAtt" runat="server" Text="Cancel Attendance" OnClick="btnCanAtt_Click" CssClass="btn btn-primary" Enabled="false" TabIndex="1" />
                                                                <asp:Button ID="btnAttCanReport" runat="server" Text="Cancellation Report" OnClick="btnAttCanReport_Click" CssClass="btn btn-info" Visible="false" TabIndex="1" Enabled="false" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click1" CssClass="btn btn-warning" TabIndex="1" />
                                                            </div>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="div8" runat="Server">
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

