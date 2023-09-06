<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CreateDemand.aspx.cs" Inherits="Academic_CreateDemand" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_divSelectedStudents .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_lvStudent_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"


            });

        });
    </script>--%>

    <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tblStdList').DataTable({
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
                                    return $('#tblStdList').DataTable().column(idx).visible();
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
                                                return $('#tblStdList').DataTable().column(idx).visible();
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
                                                return $('#tblStdList').DataTable().column(idx).visible();
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
                    var table = $('#tblStdList').DataTable({
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
                                        return $('#tblStdList').DataTable().column(idx).visible();
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
                                                    return $('#tblStdList').DataTable().column(idx).visible();
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
                                                    return $('#tblStdList').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--  <h3 class="box-title">Create Demand <small style="text-transform: capitalize;">(Select criteria for demand creation)</small></h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label><small style="text-transform: capitalize;"></small></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Bulk Demand Creation</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Single Student Demand Creation</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
                                <asp:UpdatePanel ID="pnlFeeTable" runat="server">
                                    <ContentTemplate>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%-- <label>School/Institute Name</label>--%>
                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged" ToolTip="Please Select Institute." />
                                                    <asp:RequiredFieldValidator ID="rfvSchClg" runat="server" ControlToValidate="ddlSchClg" ValidationGroup="submit"
                                                        ErrorMessage="Please Select Institute Name" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Session</label>--%>
                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" ToolTip="Please Select Session">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="valSession" runat="server" ControlToValidate="ddlSession"
                                                        Display="None" ErrorMessage="Please Select Session." ValidationGroup="submit"
                                                        InitialValue="0" SetFocusOnError="true" />--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReceiptType" AppendDataBoundItems="true" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged1" ToolTip="Please Select Receipt Type">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                        Display="None" ErrorMessage="Please Select Receipt Type" ValidationGroup="submit"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Degree</label>--%>
                                                        <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control" TabIndex="4" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" ToolTip="Please Select Degree">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" ValidationGroup="submit"
                                                        ErrorMessage="Please Select Degree" Display="None" InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--<label>Programme/Branch</label>--%>
                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" ToolTip="Please Select Branch">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="valBranch" runat="server" ValidationGroup="submit" ControlToValidate="ddlBranch"
                                                        ErrorMessage="Please Select Branch" Display="None" InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            Select Students of
                                                <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label></label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSelectSemester" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="6" data-select2-enable="true"
                                                        CssClass="form-control" ToolTip="Please Select Students of Semester" OnSelectedIndexChanged="ddlSelectSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="valSelectSemester" runat="server" ControlToValidate="ddlSelectSemester"
                                                        Display="None" ErrorMessage="Please Select Semester For Selecting Students"
                                                        ValidationGroup="submit" InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>
                                                            Create Demand for
                                                <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label></label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlForSemester" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="7" data-select2-enable="true"
                                                        CssClass="form-control" ToolTip="Please Select Create Demand for Semester" OnSelectedIndexChanged="ddlForSemester_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="valForSemester" runat="server" ControlToValidate="ddlForSemester"
                                                        Display="None" ErrorMessage="Please Select Semester To Create Demand" ValidationGroup="submit"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Payment Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPaymentType" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" AutoPostBack="true"
                                                        CssClass="form-control" TabIndex="8" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                        Display="None" ErrorMessage="Please Select Payment Type" ValidationGroup="submit"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:CheckBox ID="chkOverwrite" Text="Overwrite existing demands" runat="server" TabIndex="9" Checked="true" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnShowStudents" runat="server" Text="Show students under selected criteria" TabIndex="10"
                                                ValidationGroup="submit" OnClick="btnShowStudents_Click" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" TabIndex="11"
                                                CssClass="btn btn-warning" />
                                            <asp:Button ID="btnShowReport" runat="server" Text="Show Report" TabIndex="14"
                                                ValidationGroup="submit" OnClick="btnShowReport_Click" CssClass="btn btn-info" />
                                            <%-- <asp:Button ID="btnCreateDemand" runat="server" Text="Create Demand" OnClick="btnCreateDemand_Click"
                                    ValidationGroup="submit" Visible="false" CssClass="btn btn-primary" />--%>
                                            <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="submit" />
                                        </div>

                                        <div class="col-12" style="display: none">
                                            <div class="sub-heading">
                                                <h5>Reports</h5>
                                            </div>
                                            <div class="row form-inline">
                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                    <asp:RadioButton ID="rdoDetailedReport" GroupName="report" runat="server" TabIndex="12"
                                                        Text="Detailed Report" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoSummaryReport" GroupName="report" runat="server" TabIndex="13"
                                            Text="Summary Report" />
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-12 text-center">
                                                    <asp:Button ID="btnShowReport1" runat="server" Text="Show Report" TabIndex="14"
                                                        ValidationGroup="submit" OnClick="btnShowReport_Click" CssClass="btn btn-info" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 pt-3" id="divSelectedStudents" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5>Demand Creation for selected students</h5>
                                            </div>
                                            <div class="btn-footer">
                                                <asp:Button ID="btnCreateDemandForSelStuds" runat="server" Text="Create Demand for Selected Students"
                                                    OnClick="btnCreateDemandForSelStuds_Click" ValidationGroup="submit" CssClass="btn btn-primary" />
                                            </div>

                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>List of Students</h5>
                                                </div>
                                                <asp:ListView ID="lvStudents" runat="server">
                                                    <LayoutTemplate>
                                                        <table id="tblSearchResults" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="selectAllStudents(this)" ToolTip="Select/Select all" />Select
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Name
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>
                                                                        <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        Student Semester
                                                                    </th>
                                                                    <th>
                                                                        <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        Demand Semester
                                                                    </th>
                                                                    <th>Demand Status
                                                                    </th>

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
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                                <asp:HiddenField ID="hidStudentNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            </td>
                                                            <td><%# Eval("REGNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("STUDNAME")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("BRANCH")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTER")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("DEMAND_SEMESTER")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("DSTATUS")%>' ForeColor='<%#Eval("DSTATUS").ToString().Equals("Pending")?System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnShowStudents" />
                                        <asp:PostBackTrigger ControlID="btnCancel" />
                                        <asp:PostBackTrigger ControlID="btnShowReport" />
                                        <asp:PostBackTrigger ControlID="btnCreateDemandForSelStuds" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

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
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup><label>Search Criteria</label>
                                                    </div>

                                                    <%--onchange=" return ddlSearch_change();"--%>
                                                    <asp:DropDownList runat="server" class="form-control" ID="ddlSearch" AutoPostBack="true" AppendDataBoundItems="true"
                                                        data-select2-enable="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divpanel">
                                                    <asp:Panel ID="pnltextbox" runat="server">
                                                        <div id="divtxt" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <sup>*</sup>
                                                                <label>Search String</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" onkeypress="return Validate()" MaxLength="20"></asp:TextBox>
                                                        </div>
                                                    </asp:Panel>

                                                    <asp:Panel ID="pnlDropdown" runat="server">
                                                        <div id="divDropDown" runat="server" style="display: block">
                                                            <div class="label-dynamic">
                                                                <%-- <label id="lblDropdown"></label>--%>
                                                                <sup>*</sup>
                                                                <asp:Label ID="lblDropdown" Style="font-weight: bold" runat="server"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList runat="server" class="form-control" ID="ddlDropdown" AppendDataBoundItems="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <%-- OnClientClick="return submitPopup(this.name);"--%>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-primary" />
                                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-warning" OnClick="btnClose_Click" OnClientClick="return CloseSearchBox(this.name)" data-dismiss="modal" />
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                        </div>


                                        <div class="col-12" runat="server" id="divSingleStudDetail" visible="false">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Student Full Name </label>
                                                    </div>

                                                    <asp:TextBox ID="txtStudFullname" CssClass="form-control" runat="server" ReadOnly="true" TabIndex="1" ToolTip="Please Enter Student Full Name" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                    <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                        FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudFullname" />
                                                    --%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>College Name </label>
                                                    </div>

                                                    <asp:TextBox ID="txtCollege" CssClass="form-control" runat="server" ReadOnly="true" TabIndex="1"
                                                        ToolTip="Please Enter Student Full Name" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true">Degree</asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtDegree" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true">Branch</asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true">Students Semester</asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtSemesterName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDegree" ValidationGroup="demand"
                                                        ErrorMessage="Please Enter Degree Name" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>

                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true">Semester For Demand </asp:Label>
                                                    </div>
                                                    <%-- <asp:DropDownList ID="ddlForSemesterN" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="7" data-select2-enable="true"
                                                        CssClass="form-control" ToolTip="Please Select Create Demand for Semester">
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>--%>
                                                    <asp:DropDownList ID="ddlForSemesterN" AppendDataBoundItems="true" runat="server" AutoPostBack="true" TabIndex="7" data-select2-enable="true"
                                                        CssClass="form-control" ToolTip="Please Select Create Demand for Semester" ValidationGroup="demand" OnSelectedIndexChanged="ddlForSemesterN_SelectedIndexChanged" >
                                                        <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlForSemester"
                                                        Display="None" ErrorMessage="Please Select Semester To Create Demand" ValidationGroup="demand"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true">Payment Type</asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtPaytype" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDegree" ValidationGroup="demand"
                                                        ErrorMessage="Please Enter Degree Name" Display="None" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    --%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Receipt Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRecType" AppendDataBoundItems="true" runat="server" TabIndex="2" AutoPostBack="true" CssClass="form-control"
                                                        data-select2-enable="true" ToolTip="Please Select Receipt Type" ValidationGroup="demand" OnSelectedIndexChanged="ddlRecType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReceiptType"
                                                        Display="None" ErrorMessage="Please Select Receipt Type" ValidationGroup="demand"
                                                        InitialValue="0" SetFocusOnError="true" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 05px">
                                                    <label>Demand Status : </label>
                                                    <asp:Label ID="lblStatus" runat="server" Style="font: bold"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 05px">
                                                    <label>Admission Status : </label>
                                                    <asp:Label ID="lblAdmStatus" runat="server" Style="font: bold"></asp:Label>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <asp:CheckBox ID="CheckBox1" Text="Overwrite existing demands" runat="server" TabIndex="9" Checked="true" />
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-12">
                                            <asp:Panel ID="pnlLV" runat="server">
                                                <asp:ListView ID="lvStudent" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="listViewGrid" class="vista-grid">
                                                            <div class="sub-heading">
                                                                <h5>Student List</h5>
                                                            </div>
                                                            <asp:Panel ID="Panel2" runat="server">
                                                                <table class="table table-striped table-bordered nowrap" id="tblStdList" style="width:100%;">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Name
                                                                            </th>
                                                                            <th>IdNo
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>
                                                                                <%--<asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                                Student Semester
                                                                            </th>
                                                                            <th>Father Name
                                                                            </th>
                                                                            <th>Mother Name
                                                                            </th>
                                                                            <th>Mobile No.
                                                                            </th>
                                                                           <%-- <th>Status
                                                                            </th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>

                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <%# Eval("idno")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNO")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FATHERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MOTHERNAME") %>
                                                            </td>
                                                            <td>
                                                                <%#Eval("STUDENTMOBILE") %>
                                                            </td>
                                                            <%--<td>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("DSTATUS")%>' ForeColor='<%#Eval("DSTATUS").ToString().Equals("Pending")?System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label>
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>


                                        <div class="col-12 pt-3" id="divDemandCreation" runat="server" visible="false">
                                            <div class="sub-heading">
                                                <h5></h5>
                                            </div>
                                            <div class="btn-footer">
                                                <asp:Button ID="btnCreateDemand" runat="server" Text="Create Student Demand " CausesValidation="false"
                                                    OnClick="btnCreateDemand_Click" ValidationGroup="demand" CssClass="btn btn-primary" />
                                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="demand" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnCreateDemand" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClose" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divMsg" runat="server">
                </div>
            </div>
        </div>
    </div>

    <script>
        function totAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('tblSearchResults');
            //alert(tbl)
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctl00_cbHead');
            //alert(chkHead)

            for (i = 1; i < tbl.rows.length; i++) {
                var chkRow
                if (i <= 9) {
                    chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctl0' + i + '_chkSelect');
                }
                else {
                    chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctl' + i + '_chkSelect');
                }
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }


        function Validate() {

            debugger

            var rbText;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;

            if (rbText == "IDNO" || rbText == "Mobile") {

                var char = (event.which) ? event.which : event.keyCode;
                if (char >= 48 && char <= 57) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else if (rbText == "NAME") {

                var char = (event.which) ? event.which : event.keyCode;

                if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122)) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

    </script>
    <%--<script>
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#tblSearchResults').DataTable({

            });
        }

    </script>--%>

    <script>
        function selectAllStudents(chk) {
            $("[id*=tblSearchResults] td").closest("tr").length;
            if (chk.checked) {
                $("[id*=tblSearchResults] td").closest("tr").find(':checkbox').attr('checked', 'checked');
            }
            else {
                $("[id*=tblSearchResults] td").closest("tr").find(':checkbox').removeAttr('checked');
            }
        }

        $(document).ready(function () {
            debugger
            // $("#<%= pnltextbox.ClientID %>").hide();

            $("#<%= pnlDropdown.ClientID %>").hide();
        });

        function submitPopup(btnsearch) {

            debugger
            var rbText;
            var searchtxt;

            var e = document.getElementById("<%=ddlSearch.ClientID%>");
            var rbText = e.options[e.selectedIndex].text;
            var ddlname = e.options[e.selectedIndex].text;
            if (rbText == "Please Select") {
                alert('Please Select Search Criteria.')
                $(e).focus();
                return false;
            }

            else {


                if (rbText == "ddl") {
                    var skillsSelect = document.getElementById("<%=ddlDropdown.ClientID%>").value;

                                var searchtxt = skillsSelect;
                                if (searchtxt == "0") {
                                    alert('Please Select ' + ddlname + '..!');
                                }
                                else {
                                    __doPostBack(btnsearch, rbText + ',' + searchtxt);
                                    return true;
                                    //$("#<%= divpanel.ClientID %>").hide();
                                    $("#<%= pnltextbox.ClientID %>").hide();

                                }
                            }
                            else if (rbText == "BRANCH") {

                                if (searchtxt == "Please Select") {
                                    alert('Please Select Branch..!');

                                }
                                else {
                                    __doPostBack(btnsearch, rbText + ',' + searchtxt);

                                    return true;
                                }

                            }
                            else {
                                searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;
                                if (searchtxt == "" || searchtxt == null) {
                                    alert('Please Enter Data to Search.');
                                    //$(searchtxt).focus();
                                    document.getElementById('<%=txtSearch.ClientID %>').focus();
                                    return false;
                                }
                                else {
                                    __doPostBack(btnsearch, rbText + ',' + searchtxt);
                                    //$("#<%= divpanel.ClientID %>").hide();
                                    //$("#<%= pnltextbox.ClientID %>").show();

                                    return true;
                                }
                            }
                    }
                }

                function ClearSearchBox(btncancelmodal) {
                    document.getElementById('<%=txtSearch.ClientID %>').value = '';
                    __doPostBack(btncancelmodal, '');
                    return true;
                }
                function CloseSearchBox(btnClose) {
                    document.getElementById('<%=txtSearch.ClientID %>').value = '';
                    __doPostBack(btnClose, '');
                    return true;
                }

                function Validate() {

                    debugger

                    var rbText;

                    var e = document.getElementById("<%=ddlSearch.ClientID%>");
                    var rbText = e.options[e.selectedIndex].text;

                    if (rbText == "IDNO" || rbText == "Mobile") {

                        var char = (event.which) ? event.which : event.keyCode;
                        if (char >= 48 && char <= 57) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else if (rbText == "NAME") {

                        var char = (event.which) ? event.which : event.keyCode;

                        if ((char >= 65 && char <= 90) || (char >= 97 && char <= 122) || (char = 49)) {
                            return true;
                        }
                        else {
                            return false;
                        }

                    }
                }

    </script>
</asp:Content>
