<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OD_Category.aspx.cs" Inherits="ACADEMIC_ODTracker_OD_Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatEvent" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatSubEvent" runat="server" ClientIDMode="Static" />

    <style>
        .nav-tabs-custom .nav-link {
            background: #fff;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblSubEventDetails').DataTable({
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
                                return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
                                            return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
                var table = $('#tblSubEventDetails').DataTable({
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
                                    return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
                                                return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblSubEventDetails').DataTable().column(idx).visible();
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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updEventCategory"
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
    <asp:UpdatePanel ID="updEventCategory" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">OD Category</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">Event Category</a>--%>
                                            <asp:Button Text="Event Category" ID="btnTabEventsCategory" CssClass="nav-link active" runat="server" OnClick="btnTabEventsCategory_Click" />
                                        </li>
                                        <li class="nav-item">
                                            <%--<a class="nav-link" data-toggle="tab" href="#tab_2">Sub Event Category</a>--%>
                                            <asp:Button Text="Sub Event Category" ID="btnTabSubEventCategory" CssClass="nav-link" runat="server" OnClick="btnTabSubEventCategory_Click" />
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">
                                        <asp:MultiView ID="MainView" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <%--<div class="tab-pane active" id="tab_1">--%>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Event </label>
                                                            </div>
                                                            <asp:TextBox ID="txtEventCategory" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvEventCategory" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Enter Event Category" ControlToValidate="txtEventCategory"
                                                                Display="None" ValidationGroup="EventSubmit" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdActiveEvent" name="switch" checked />
                                                                <label data-on="Active" data-off="Inactive" for="rdActiveEvent"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitEvent" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="7" OnClick="btnSubmitEvent_Click" ValidationGroup="EventSubmit" CausesValidation="true" OnClientClick="validateEvent();" />
                                                    <asp:Button ID="btnCancelSubmitEvent" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancelSubmitEvent_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="EventSubmit" />
                                                    <%--<asp:Button ID="Button2" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" />--%>
                                                </div>

                                                <div class="col-12">
                                                    <div class="table-responsive">
                                                        <asp:ListView ID="lvEventDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="listViewGrid" class="vista-grid">
                                                                    <div class="sub-heading">
                                                                        <h5>Events</h5>
                                                                    </div>
                                                                    <table id="tblEventsDetails" class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>EVENT ID
                                                                                </th>
                                                                                <th>EVENT NAME
                                                                                </th>
                                                                                <th>Is Active
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </LayoutTemplate>
                                                            <EmptyDataTemplate>
                                                            </EmptyDataTemplate>
                                                            <ItemTemplate>
                                                                <tr class="item">
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btnEditEvent" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("EVENT_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditEvent_Click" TabIndex="14" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("EVENT_ID")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("EVENTNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                                <%--</div>--%>
                                            </asp:View>
                                            <asp:View ID="View2" runat="server">
                                                <%--<div class="tab-pane fade" id="tab_2">--%>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Event Category </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlEventCategory" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Select Event Category" ControlToValidate="ddlEventCategory" InitialValue="0"
                                                                Display="None" ValidationGroup="SubEventsubmit" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sub Event </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubEventCategory" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSubEventCategory" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Enter Sub Event Category" ControlToValidate="txtSubEventCategory"
                                                                Display="None" ValidationGroup="SubEventsubmit" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="rdActiveSubEvent" name="switch" checked />
                                                                <label data-on="Active" data-off="Inactive" for="rdActiveSubEvent"></label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubEventSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" TabIndex="7" OnClick="btnSubEventSubmit_Click" ValidationGroup="SubEventsubmit" CausesValidation="true" OnClientClick="validateSubEvent();" />
                                                    <asp:Button ID="btnCancelSubEventSubmit" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="8" OnClick="btnCancelSubEventSubmit_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="SubEventsubmit" />
                                                    <%--<asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-warning" TabIndex="8" />--%>
                                                </div>
                                                <%--</div>--%>

                                                <div class="col-12">
                                                    <asp:ListView ID="lvSubEventDetails" runat="server">
                                                        <LayoutTemplate>
                                                            <div id="listViewGrid" class="vista-grid">
                                                                <div class="sub-heading">
                                                                    <h5>Sub Events</h5>
                                                                </div>
                                                                <table id="tblSubEventDetails" class="table table-striped table-bordered nowrap" style="width: 100% !important;">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <th>SUB EVENT ID
                                                                            </th>
                                                                            <th>EVENT 
                                                                            </th>
                                                                            <th>SUB EVENT NAME
                                                                            </th>
                                                                            <th>Is Active
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <EmptyDataTemplate>
                                                        </EmptyDataTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEditSubEvent" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                        CommandArgument='<%# Eval("SUB_EVENT_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEditSubEvent_Click" TabIndex="14" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUB_EVENT_ID")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("EVENTNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SUB_EVENTNAME")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
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
            <!-- The Modal -->
            <div class="modal fade" id="myModal_Create">
                <div class="modal-dialog modal-xl">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Create College Event</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select Category</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select Sub Category</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Name </label>
                                        </div>
                                        <asp:TextBox ID="txtEventName" runat="server" CssClass="form-control" TabIndex="3" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start End Date</label>
                                        </div>
                                        <div id="picker" class="form-control" tabindex="4">
                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Request as a Special Event </label>
                                        </div>
                                        <asp:CheckBox ID="chkSpecialEvent" runat="server" TabIndex="5" />
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Publish </label>
                                        </div>
                                        <asp:CheckBox ID="CheckBox1" runat="server" TabIndex="6" />
                                    </div>

                                    <div class="form-group col-lg-5 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Comment </label>
                                        </div>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" TabIndex="7" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Create" CssClass="btn btn-primary" TabIndex="8" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" TabIndex="9" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function SetEvent(val) {
            $('#rdActiveEvent').prop('checked', val);
        }
        function SetSubEvent(val) {
            $('#rdActiveSubEvent').prop('checked', val);
        }

        function validateEvent() {
            $('#hfdStatEvent').val($('#rdActiveEvent').prop('checked'));
        }
        function validateSubEvent() {
            $('#hfdStatSubEvent').val($('#rdActiveSubEvent').prop('checked'));
        }
    </script>

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

    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });
    </script>
</asp:Content>
