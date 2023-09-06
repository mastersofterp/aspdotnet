<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Placement_Head_OD_Dashboard.aspx.cs" Inherits="ACADEMIC_ODTracker_Placement_Head_OD_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .card {
            position: relative;
            background-color: #fff;
            box-shadow: 3px 3px 10px rgb(0 0 0 / 10%);
        }

        .card-body {
            padding: 1rem 1.25rem;
        }

            .card-body h6 {
                font-weight: 600;
            }

            .card-body i {
                font-size: 28px;
                margin-top: 10px;
                margin-bottom: 6px;
            }

        .count {
            position: absolute;
            right: 20px;
            top: 35%;
            font-size: 36px;
        }

        .viewbtn {
            position: absolute;
            right: 20px;
            bottom: 10%;
        }

        #hidden {
            display: none;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #MyEvent.table-bordered > thead > tr > th {
            border-top: 1px solid #e5e5e5;
        }

        .nav-tabs-custom .nav-link {
            background: #fff;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblEventReqOD').DataTable({
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
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblEventReqOD').DataTable().column(idx).visible();
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
                                        var arr = [0, 9, 10];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblEventReqOD').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
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
                                        var arr = [0, 9, 10];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblEventReqOD').DataTable().column(idx).visible();
                                        }
                                    },
                                    format: {
                                        body: function (data, column, row, node) {
                                            var nodereturn;
                                            if ($(node).find("input:text").length > 0) {
                                                nodereturn = "";
                                                nodereturn += $(node).find("input:text").eq(0).val();
                                            }
                                            else if ($(node).find("input:checkbox").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:checkbox").each(function () {
                                                    if ($(this).is(':checked')) {
                                                        nodereturn += "On";
                                                    } else {
                                                        nodereturn += "Off";
                                                    }
                                                });
                                            }
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
                                            else if ($(node).find("img").length > 0) {
                                                nodereturn = "";
                                            }
                                            else if ($(node).find("input:hidden").length > 0) {
                                                nodereturn = "";
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
                var table = $('#tblEventReqOD').DataTable({
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
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblEventReqOD').DataTable().column(idx).visible();
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
                                            var arr = [0, 9, 10];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblEventReqOD').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
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
                                            var arr = [0, 9, 10];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblEventReqOD').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
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

    </script>
    <asp:UpdatePanel ID="updPlacementHead" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Placement Head Dashboard</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                        <div class="card">
                                            <div class="card-body">
                                                <h6>Student Placement OD</h6>
                                                <i class="fas fa-user-graduate" style="color: #cf8ee2;"></i>
                                                <div class="info">
                                                    <span>Pending Request</span>
                                                </div>
                                                <span class="count">
                                                    <asp:Label ID="lblDashboardStudODCnt" runat="server" Text="0"></asp:Label></span>
                                                <%--<asp:Button ID="btnStdOD" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View E-ODs" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                        <div class="card">
                                            <div class="card-body">
                                                <h6>College Event</h6>
                                                <i class="fas fa-calendar-alt" style="color: #d677a3;"></i>
                                                <div class="info">
                                                    <span>Pending Request</span>
                                                </div>
                                                <span class="count">
                                                    <asp:Label ID="lblDashboardClgEventCnt" runat="server" Text="0"></asp:Label></span>
                                                <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                        <div class="card">
                                            <div class="card-body">
                                                <h6>Placement Event</h6>
                                                <i class="fas fa-book-reader" style="color: #a16edc;"></i>
                                                <div class="info">
                                                    <span>Pending Request</span>
                                                </div>
                                                <span class="count">
                                                    <asp:Label ID="lblDashboardPlacementEventCnt" runat="server" Text="0"></asp:Label></span>
                                                <%--<asp:Button ID="Button2" runat="server" CssClass="btn btn-outline-primary viewbtn btn-sm" Text="View All" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-12">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">Event OD Approval</a>--%>
                                            <asp:Button Text="Placement OD Approval" ID="btnTabPlacementODApproval" CssClass="nav-link active" runat="server" OnClick="btnTabPlacementODApproval_Click" />
                                        </li>
                                        <li class="nav-item">
                                            <%--<a class="nav-link" data-toggle="tab" href="#tab_2">Student OD Approval </a>--%>
                                            <asp:Button Text="Student Placement OD Approval" ID="btnTabStudentODApproval" CssClass="nav-link" runat="server" OnClick="btnTabStudentODApproval_Click" />
                                        </li>
                                    </ul>

                                    <div class="tab-content" id="my-tab-content">
                                        <asp:MultiView ID="MainView" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <%--<div class="tab-pane active" id="tab_1">--%>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Placement OD Request</h5>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPlacementODStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlPlacementODStatus_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select Status</asp:ListItem>
                                                            <asp:ListItem Value="1">Pending for Approval</asp:ListItem>
                                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                            <asp:ListItem Value="4">All </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnPlacementODApprove" runat="server" Text="Approve" CssClass="btn btn-primary" TabIndex="7" OnClick="btnPlacementODApproval_Click" />
                                                    <asp:Button ID="btnPlacementODReject" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" OnClick="btnPlacementODReject_Click" />
                                                </div>

                                                <div class="col-12 table-responsive mt-4">
                                                    <asp:ListView ID="lvPlacementOD" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="divlvFeeItems" class="vista-grid">
                                                                <div class="table-responsive" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                <th>Select</th>
                                                                                <th>Event ID</th>
                                                                                <th>Event Name</th>
                                                                                <th>From Date</th>
                                                                                <th>To Date</th>
                                                                                <th>Category</th>
                                                                                <th>Status</th>
                                                                                <th>Publish</th>
                                                                                <th>Comments</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td>
                                                                    <asp:CheckBox ID="chkPlacement" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PLACEMENT_ID")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PLACEMENT_NAME")%>
                                                                    <asp:Label ID="lblPlacement_SRNO" runat="server" Visible="false" Text='<%#Eval("PLACEMENT_SRNO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("START_DATE","{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("END_DATE","{0:dd/MM/yyyy}")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PLACEMENT_NAME")%>                                                             
                                                                </td>
                                                                <td>
                                                                    <%--<%# Eval("REQUEST_STATUS")%>--%>
                                                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("REQUEST_STATUS")%>' Font-Bold="true"
                                                                    ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("REQUEST_STATUS_COLOR").ToString())%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("IS_PUBLISH")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("COMMENT")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="View2" runat="server">
                                                <%--<div class="tab-pane fade" id="tab_2">--%>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Student OD Request</h5>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Status </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStudentODStatus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlStudentODStatus_SelectedIndexChanged">
                                                            <%--onChange--%>
                                                            <asp:ListItem Value="0">Please Select Status</asp:ListItem>
                                                            <asp:ListItem Value="1">Pending For Approval</asp:ListItem>
                                                            <asp:ListItem Value="2">Approved</asp:ListItem>
                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                            <asp:ListItem Value="4">All </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnStudentODApprove" runat="server" Text="Approve" CssClass="btn btn-primary" TabIndex="7" OnClick="btnStudentODApprove_Click" />
                                                    <asp:Button ID="btnStudentODReject" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" OnClick="btnStudentODReject_Click" />
                                                </div>
                                                <div class="col-12 mt-4">
                                                    <asp:ListView ID="lvStudentOD" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="divlvFeeItems1" class="vista-grid">
                                                                <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblStudent">
                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                <th>Select</th>
                                                                                <th>Reg No</th>
                                                                                <th>Student Name</th>
                                                                                <th>Placement Name</th>
                                                                                <th>Placement Coordinator Status</th>
                                                                                <th>Coordinator Head Status</th>
                                                                                <th>Comments</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td>
                                                                    <asp:CheckBox ID="chkStudPlacementSelect" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REGNO")%>
                                                                    <asp:Label ID="lblStudPlacementNo" runat="server" Visible="false" Text='<%#Eval("STUD_PLACEMENT_NO")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STUDNAME")%>                                                             
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPlacementNo" runat="server" Visible="false" Text='<%#Eval("PLACEMENT_NO")%>'></asp:Label>
                                                                    <%# Eval("PLACEMENT_NAME")%>                                                             
                                                                </td>
                                                                <td>
                                                                    <%--<%# Eval("STATUS")%>--%>
                                                                    <asp:Label ID="lblRequestStatus" runat="server" Text='<%# Eval("STATUS")%>' Font-Bold="true"
                                                                    ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("STATUS_COLOR").ToString())%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%--<%# Eval("PLACEMENT_COORDINATOR_STATUS")%>--%>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("PLACEMENT_COORDINATOR_STATUS")%>' Font-Bold="true"
                                                                    ForeColor='<%#System.Drawing.ColorTranslator.FromHtml(Eval("PLACEMENT_COORDINATOR_STATUS_COLOR").ToString())%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("STUDENT_COMMENT")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function collapse(cell) {
            var row = cell.parentElement;
            var target_row = row.parentElement.children[row.rowIndex + 1];
            if (target_row.style.display == 'table-row') {
                cell.innerHTML = '<i class="fas fa-plus" style="color: #fff; background: green; padding:5px; border-radius:50%; "></i>';
                target_row.style.display = 'none';
            } else {
                cell.innerHTML = '<i class="fas fa-minus" style="color: #fff; background: red; padding:5px; border-radius:50%; "></i>';
                target_row.style.display = 'table-row';
            }
        }
    </script>
    <script>
        function TabShow() {
            debugger;
            var tabName = "tab_2";
            alert(tabName);
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
</asp:Content>

