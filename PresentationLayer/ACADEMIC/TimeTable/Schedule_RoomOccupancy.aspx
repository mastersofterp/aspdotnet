<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Schedule_RoomOccupancy.aspx.cs" Inherits="ACADEMIC_TimeTable_Schedule_RoomOccupancy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
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

    <style>
        .badge-pill {
            padding-top: 0.4rem;
            padding-bottom: 0.3rem;
        }

        .badge-warning {
            color: #fff;
            background-color: #d9763d;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

      <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('.tbl-display').DataTable({
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
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('.tbl-display').DataTable().column(idx).visible();
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
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('.tbl-display').DataTable().column(idx).visible();
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
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('.tbl-display').DataTable().column(idx).visible();
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
                    var table = $('.tbl-display').DataTable({
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
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('.tbl-display').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('.tbl-display').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('.tbl-display').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="updtime" runat="server">
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

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />
                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select Date" SetFocusOnError="true"
                                                ValidationGroup="submit" />
                                        </div>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Please Select Type" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Class Schedule</asp:ListItem>
                                            <asp:ListItem Value="2">Room Occupancy</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Type"
                                            ControlToValidate="ddlType" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12" id="divAttendanceStatus" visible="false" runat="server">
                                        <div class="label-dynamic">
                                            <label>Attendance Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAttendanceStatus" AutoPostBack="true" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Please Select Attendance Status" OnSelectedIndexChanged="ddlAttendanceStatus_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Done</asp:ListItem>
                                            <asp:ListItem Value="2">Not Done</asp:ListItem>
                                        </asp:DropDownList>

                                    
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="submit" OnClick="btnShow_Click"
                                    TabIndex="3" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSendEmail" Visible="false" runat="server" OnClick="btnSendEmail_Click" CssClass="btn btn-info" Text="Send Email" ValidationGroup="show"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                                <asp:Button ID="btnExcel" runat="server" CssClass="btn btn-outline-primary d-none" Text="Report (Excel)" ValidationGroup="submit" OnClick="btnExcel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ValidationGroup="submit"
                                    ShowSummary="false" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">

                                    <asp:ListView ID="lvClassSchedule" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Class Schedule</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap tbl-display" style="width: 100%" id="tblStudent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Session                                                       
                                                        </th>
                                                        <th>School
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Slot
                                                        </th>
                                                        <th>Time From 
                                                        </th>
                                                        <th>Time To
                                                        </th>
                                                        <th>Day
                                                        </th>
                                                        <th>Room
                                                        </th>
                                                        <th>Room Capacity
                                                        </th>
                                                        <th>Floor
                                                        </th>
                                                        <th>Duration
                                                        </th>
                                                        <th>Course Type</th>
                                                        <th>Course Code</th>
                                                        <th>Course Name</th>
                                                        <th>Staff</th>
                                                        <th>Section</th>
                                                        <th>Semester</th>
                                                        <th>Attendance Status</th>
                                                        <th>Total Student</th>
                                                        <th>Present Student</th>
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
                                                    <%# Eval("SESSION_NAME")%>  
                                                </td>
                                                <td>
                                                    <%# Eval("COLLEGE_NAME")%>                                       
                                                </td>
                                                <td><%# Eval("TIME_TABLE_DATE")%>                                              
                                                </td>
                                                <td><%# Eval("SLOTNAME")%> 
                                                                                                 
                                                </td>
                                                <td><%# Eval("TIMEFROM")%>                                                   
                                                </td>
                                                <td><%# Eval("TIMETO")%>                                                  
                                                </td>
                                                <td><%# Eval("DAY_NAME")%> </td>
                                                <td><%# Eval("ROOMNAME")%> </td>
                                                <td><%# Eval("INTAKE")%> </td>

                                                <td><%# Eval("FLOORNAME")%> </td>
                                                <td><%# Eval("Duration")%> </td>
                                                <td><%# Eval("SUBNAME")%> </td>
                                                <td><%# Eval("CCODE")%> </td>
                                                <td><%# Eval("COURSE_NAME")%> </td>
                                                <td><%# Eval("UA_FULLNAME")%> </td>
                                                <td><%# Eval("SECTIONNAME")%> </td>
                                                <td><%# Eval("SEMESTERNO")%> </td>
                                                <td><%# Eval("Attendence_Status")%></td>
                                                <td><%# Eval("TOTAL_STUDENT")%></td>
                                                <td><%# Eval("Present")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvRoomOccupancy" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Room Occupancy</h5>
                                            </div>
                                            <%--<div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="">
                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>--%>
                                            <table class="table table-striped table-bordered nowrap tbl-display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                            <th>Floor                                                       
                                                            </th>
                                                            <th>Room
                                                            </th>
                                                            <th>Room Capacity
                                                            </th>
                                                            <th>Hours Booked
                                                            </th>
                                                            <th>Utilization %
                                                            </th>
                                                            <th>Tag
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                       <%--     </div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("FLOOR")%>  
                                                </td>
                                                <td>
                                                    <%# Eval("ROOM")%>                                       
                                                </td>
                                                <td><%# Eval("ROOMCAPACITY")%>                                              
                                                </td>
                                                <td><%# Eval("HOURSBOOKED")%> 
                                                                                                 
                                                </td>
                                                <td><%# Eval("UTILIZATION")%>                                                   
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTag" runat="server" Text='<%# Eval("TAG")%>'></asp:Label>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:PostBackTrigger ControlID="btnSendEmail" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

