<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MappingCreation.aspx.cs" Inherits="RFC_CONFIG_Masters_MappingCreation" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updDepart" runat="server" AssociatedUpdatePanelID="updDepartmentMapp"
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
            var table = $('#divlistDegree').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
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
                                return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
                                            }
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
                var table = $('#divlistDegree').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
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
                                    return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
                                                }
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


    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#divBranch').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

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
                                return $('#divBranch').DataTable().column(idx).visible();
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
                                                return $('#divBranch').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#divBranch').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#divBranch').DataTable().column(idx).visible();
                                            }
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
                var table = $('#divBranch').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

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
                                    return $('#divBranch').DataTable().column(idx).visible();
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
                                                    return $('#divBranch').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#divBranch').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#divBranch').DataTable().column(idx).visible();
                                                }
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


    <asp:UpdatePanel ID="updDepartmentMapp" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Mapping Structure</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">College Department Mapping</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">College Degree Mapping</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">College Degree Branch Mapping</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College</label>--%>
                                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollegeIdDepMap" OnSelectedIndexChanged="ddlCollegeIdDepMap_SelectedIndexChanged" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-9 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true">Department List</asp:Label>
                                                        </div>
                                                        <asp:Panel ID="pnldepMapping" runat="server">
                                                            <asp:ListView ID="lvDepMapping" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divCollist">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">
                                                                                    <asp:CheckBox ID="cbDep" TabIndex="5" class="newAddNew Tab" runat="server" onclick="SelectAll(this)" ToolTip="Select All" Text="Select All" />
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true">DEPARTMENT NAME</asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDeptShortName" runat="server" Font-Bold="true">DEPARTMENT SHORT NAME</asp:Label>
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
                                                                        <td style="text-align: center;">
                                                                            <asp:CheckBox ID="chkDepart" TabIndex="6" class="newAddNew Tab" runat="server" ToolTip='<%# Eval("DEPTNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblDepName" Text='<%# Eval("DEPTNAME") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DEPTCODE")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-6">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Uncheck If Inactive</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                                    <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitDepMapp" OnClick="btnSubmitDepMapp_Click" runat="server" ToolTip="Submit" OnClientClick="return validate();"
                                                    CssClass="btn btn-primary" TabIndex="7">Submit</asp:LinkButton>

                                                <asp:Button ID="btnCancelDepMap" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelDepMap_Click"
                                                    TabIndex="8" CssClass="btn btn-warning" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlMap" runat="server">
                                                    <asp:ListView ID="lvMappedDepartment" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Department Mapping List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>-<asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true">College - Department Name</asp:Label>
                                                                        </th>
                                                                        <th>Status
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updlstMapp">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEditDepMap" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("COLLEGE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" CommandName='<%# Eval("COLLEGE_ID")%>'
                                                                                OnClick="btnEditDepMap_Click" class="newAddNew Tab" TabIndex="9" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_DEPT_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACTIVESTATUS") %>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditDepMap" />
                                                                </Triggers>

                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_2">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College </label>--%>
                                                            <asp:Label ID="lblDYddlSchool_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollegeList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCollegeList_SelectedIndexChanged"
                                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-9 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Degree List</label>--%>
                                                            <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true">Degree List</asp:Label>
                                                        </div>
                                                        <asp:Panel ID="pnlDegreeMap" runat="server">
                                                            <asp:ListView ID="lstDegree" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divDegreeList">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">
                                                                                    <asp:CheckBox ID="cbDegHead" TabIndex="5" class="newAddNew Tab" runat="server" onclick="SelectAllDeg(this)" ToolTip="Select All" Text="Select All" />
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegShortName" runat="server" Font-Bold="true"></asp:Label>
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
                                                                        <td style="text-align: center;">
                                                                            <asp:CheckBox ID="chkDegree" TabIndex="6" class="newAddNew Tab" runat="server" ToolTip='<%# Eval("DEGREENO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblDegName" Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="lnkSaveDegreeMap" OnClick="lnkSaveDegreeMap_Click" runat="server" ToolTip="Submit" OnClientClick="return validateDegMapping();"
                                                    CssClass="btn btn-primary" TabIndex="7" Text="Submit" />

                                                <asp:Button ID="btnCancelDegreeMap" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelDegreeMap_Click"
                                                    TabIndex="8" CssClass="btn btn-warning" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="Panel2" runat="server">

                                                    <asp:ListView ID="lstDegreeMap" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>College Degree Mapping List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divlistDegree">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true">College Name</asp:Label></th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true">Degree Name</asp:Label></th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updDegMapp">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEditDegMap" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("COLLEGE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" CommandName='<%# Eval("COLLEGE_ID")%>'
                                                                                OnClick="btnEditDegMap_Click" class="newAddNew Tab" TabIndex="9" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DEGREENAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACTIVESTATUS") %>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditDegMap" />
                                                                </Triggers>

                                                            </asp:UpdatePanel>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="tab-pane" id="tab_3">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College Name</label>--%>
                                                            <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollegeBranchMap" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCollegeBranchMap_SelectedIndexChanged"
                                                            AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Degree</label>--%>
                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreeBrMap" AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeBrMap_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="5">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Department Name</label>--%>
                                                            <asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDeptBranchMap" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="6">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            <%--<label>Programme/Branch Name</label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranchMap" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <%--<div class="col-md-10">--%>
                                                        <asp:CheckBox ID="chkIsSpecialisation" OnCheckedChanged="chkIsSpecialisation_CheckedChanged" AutoPostBack="true" class="newAddNew Tab" Text="" runat="server" TabIndex="14" />
                                                        <label>Is Specialisation</label>
                                                        <%-- </div>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divcorebranch" visible="false">
                                                        <div class="label-dynamic">
                                                            <%-- <sup>* </sup>--%>
                                                            <%-- <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                            <label>Core Programme</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCoreBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Duration</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDurationBranchMap" placeholder="Enter Duration" runat="server" MaxLength="1" AutoComplete="off" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="afeDuration" runat="server" ValidChars="0123456789" TargetControlID="txtDurationBranchMap">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Programme / Branch Short Name</label>--%>
                                                            <asp:Label ID="lblDYtxtBranchShort" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtBranchShortName" placeholder="Enter Programme/Branch Short Name" runat="server" TabIndex="9" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%-- <sup>* </sup>--%>
                                                            <%--<label>Programme / Branch Code</label>--%>
                                                            <asp:Label ID="lblDYtxtBranchCode" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtBranchCode" placeholder="Enter Programme/Branch Code" MaxLength="10" runat="server" CssClass="form-control" TabIndex="10" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College Code</label>--%>
                                                            <asp:Label ID="lblDYtxtCollegeCode" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtCollegeCodeBranchMap" placeholder="Enter College Code" MaxLength="12" runat="server" TabIndex="11" CssClass="form-control" AutoComplete="off"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College Type</label>--%>
                                                            <asp:Label ID="lblDYtxtColgType" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollegeTypeBranchMap" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="12">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Programme Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlProgrammetypeBranchMap" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="13">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <%-- <sup>* </sup>--%>
                                                           <label>Faculty/Discipline</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlFcultyno" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="13">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <%--<div class="col-md-10">--%>
                                                        <asp:CheckBox ID="chkEng" class="newAddNew Tab" Text="" runat="server" TabIndex="14" />
                                                        <label>Check if Degree & Programme Name is Same</label>
                                                        <%-- </div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">

                                                <%--<legend>Intake Capacity</legend>--%>
                                                <div class="row" style="display:none">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Intake Capacity</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>I-Intake</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIntake1" runat="server" CssClass="form-control" TabIndex="15" AutoComplete="off" MaxLength="3"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftIntake1" runat="server" TargetControlID="txtIntake1" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>II-Intake</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIntake2" runat="server" CssClass="form-control" TabIndex="16" AutoComplete="off" MaxLength="3"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftIntake2" runat="server" TargetControlID="txtIntake2" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>III-Intake</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIntake3" runat="server" CssClass="form-control" TabIndex="17" AutoComplete="off" MaxLength="3"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftIntake3" runat="server" TargetControlID="txtIntake3" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>IV-Intake</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIntake4" runat="server" CssClass="form-control" TabIndex="18" AutoComplete="off" MaxLength="3"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftIntake4" runat="server" TargetControlID="txtIntake4" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>V-Intake</label>
                                                        </div>
                                                        <asp:TextBox ID="txtIntake5" runat="server" CssClass="form-control" TabIndex="19" AutoComplete="off" MaxLength="3"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftIntake5" runat="server" TargetControlID="txtIntake5" ValidChars="0123456789"></ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="form-group col-lg-9 col-md-6 col-12" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch List</label>
                                                </div>
                                                <asp:Panel ID="pnlBranch" runat="server">
                                                    <asp:ListView ID="lvBranchList" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divBranchList">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">
                                                                            <asp:CheckBox ID="cbBranchHead" TabIndex="5" class="newAddNew Tab" runat="server" onclick="SelectAllBranch(this);" ToolTip="Select All" Text="Select All" />
                                                                        </th>
                                                                        <th>BRANCH NAME
                                                                        </th>
                                                                        <th>BRANCH SHORT NAME
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
                                                                <td style="text-align: center;">
                                                                    <asp:CheckBox ID="chkBranch" TabIndex="6" class="newAddNew Tab" runat="server" ToolTip='<%# Eval("BRANCHNO") %>' />
                                                                    <%-- <asp:HiddenField ID="hdnDegree" Value='<%# Eval("DegreeDepartmentId") %>' runat="server" />--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label runat="server" ID="lblBranch" Text='<%# Eval("LONGNAME") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SHORTNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmitBranchMap" OnClick="btnSubmitBranchMap_Click" runat="server" ToolTip="Submit" OnClientClick="return validateDegBranchMapping();"
                                                    CssClass="btn btn-primary" TabIndex="20" Text="Submit"></asp:Button>

                                                <asp:Button ID="btnCancelBranchMap" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelBranchMap_Click"
                                                    TabIndex="21" CssClass="btn btn-warning" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlBranchMapp" runat="server">
                                                    <asp:ListView ID="lvBranchMapping" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Branch Mapping List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divBranch">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit</th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtBranchShort" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYtxtBranchCode" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label><asp:Label ID="lblDYtxtBranchName" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server" ID="updBranchMap">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEditBranchMap" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("CDBNO")%>' AlternateText="Edit Record" ToolTip="Edit Record" CommandName='<%# Eval("CDBNO")%>'
                                                                                OnClick="btnEditBranchMap_Click" class="newAddNew Tab" TabIndex="22" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BRANCH_CODE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DEGREEBRANCHNAME") %>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditBranchMap" />
                                                                </Triggers>

                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="TabName" runat="server" />
            <asp:HiddenField ID="hdncountBr" runat="server" />

            <asp:HiddenField ID="hdnFlag" runat="server" />
            <asp:HiddenField ID="hdncount1" runat="server" />
            <asp:HiddenField ID="hdnDegree" runat="server" />
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnSubmitDepMapp" />
            <asp:PostBackTrigger ControlID="ddlCollegeList" />
            <asp:PostBackTrigger ControlID="btnCancelDegreeMap" />

            <asp:PostBackTrigger ControlID="lnkSaveDegreeMap" />
            <asp:PostBackTrigger ControlID="btnSubmitBranchMap" />
            <asp:PostBackTrigger ControlID="btnCancelBranchMap" />
            <asp:PostBackTrigger ControlID="ddlDegreeBrMap" />
            <asp:PostBackTrigger ControlID="chkIsSpecialisation" />
            <asp:PostBackTrigger ControlID="ddlCollegeBranchMap" />

        </Triggers>
    </asp:UpdatePanel>
    <%-- Branch Mapping--%>
    <script type="text/javascript" language="javascript">
        function SelectAllBranch(headchk) {

            var frm = document.forms[0];
            var tbl = document.getElementById('divBranchList');

            var chkHead1 = document.getElementById('ctl00_ContentPlaceHolder1_lvBranchList_cbBranchHead');
            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvBranchList_ctrl' + i + '_chkBranch');
                if (headchk.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>
    <script>
        function validateBranch() {

            var idddlOrgMap = $("[id$=ddlDegreeBrMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Branch.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            else {
                //debugger;
                var count = 0;
                var hdnVal = document.getElementById('<%= hdncountBr.ClientID %>');
                //alert(hdnVal.value);

                for (i = 0; i < hdnVal.value; i++) {
                    // alert('aa');
                    var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvBranchList_ctrl' + i + '_chkBranch');
                    if (lst.type == 'checkbox') {
                        if (lst.checked == true) {
                            count++;
                        }
                    }
                }
                if (count == 0) {
                    //if (confirm('Are you sure?'))
                    //    return true;
                    //else
                    //{ return false; }
                    alert('Please Select atleast One Branch !!');
                    return false;
                }
                else {

                }
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitBranchMap').click(function () {
                    validateCol();
                });
            });
        });

    </script>

    <script>
        setTimeout(function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        }, 200);
    </script>
    <%-- End Branch Mapping--%>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                //alert("sunita " + $(tabName).attr("href"));
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //$("[id*=TabName]").val();
            });
        });
        //function pageLoad() {
        //    Tabs();
        //}
    </script>
    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('divCollist');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvDepMapping_cbDep');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvDepMapping_ctrl' + i + '_chkDepart');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }

        function SelectAllDeg(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('divDegreeList');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lstDegree_cbDegHead');

            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lstDegree_ctrl' + i + '_chkDegree');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>

    <script>

        function validateDegMapping() {

            var idddlCollegeList = $("[id$=ddlCollegeList]").attr("id");
            var ddlCollegeList = document.getElementById(idddlCollegeList);
            if (ddlCollegeList.value == 0) {
                //if ($('#ddlCollegeList').val() == 0 || $('#ddlCollegeList').val() == -1) {
                alert('Please Select College.', 'Warning!');
                $(ddlCollegeList).focus();
                return false;
            }

            {
                //debugger;
                var count = 0;
                var hdnVal = document.getElementById('<%= hdnDegree.ClientID %>');


                for (i = 0; i < hdnVal.value; i++) {
                    // alert('aa');
                    var lst = document.getElementById('ctl00_ContentPlaceHolder1_lstDegree_ctrl' + i + '_chkDegree');
                    if (lst.type == 'checkbox') {
                        if (lst.checked == true) {
                            count++;
                        }
                    }
                }
                if (count > 0) {
                    //if (confirm('Are you sure?'))
                    //    return true;
                    //else
                    //{ return false; }
                }
                else {
                    alert('Please Select Atleast One Degree !!');
                    return false;
                }
            }

        }
        function validate() {
            var idddlOrgMap = $("[id$=ddlCollegeIdDepMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select College.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }

            {
                //debugger;
                var count = 0;
                var hdnVal = document.getElementById('<%= hdncount1.ClientID %>');
                //alert(hdnVal.value);

                for (i = 0; i < hdnVal.value; i++) {
                    // alert('aa');
                    var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvDepMapping_ctrl' + i + '_chkDepart');
                    if (lst.type == 'checkbox') {
                        if (lst.checked == true) {
                            count++;
                        }
                    }
                }
                if (count > 0) {
                    //if (confirm('Are you sure?'))
                    //    return true;
                    //else
                    //{ return false; }
                }
                else {
                    alert('Please Select atleast One Department !!');
                    return false;
                }
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitDepMapp').click(function () {
                    validateCol();
                });
            });
        });

    </script>

    <script>

        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
        //setTimeout(function () {

        //}, 1800);
    </script>

    <script>
        function validateDegBranchMapping() {

            var idddlOrgMap = $("[id$=ddlCollegeBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select College Name.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlDegreeBrMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Degree.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlDeptBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Department Name.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Programme/Branch Name.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=txtDurationBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Enter Duration.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=txtBranchShortName]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Enter Programme/Branch Short Name.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=txtCollegeCodeBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Enter College Code.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlCollegeTypeBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select College Type.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlProgrammetypeBranchMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Programme Type.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitBranchMap').click(function () {
                    validateCol();
                });
            });
        });

    </script>
</asp:Content>
