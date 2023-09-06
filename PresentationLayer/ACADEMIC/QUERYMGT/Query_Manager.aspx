<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Query_Manager.aspx.cs" Inherits="Query_Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        /*#My_Table_wrapper .dataTables_scrollHeadInner {
            width: max-content!important;
        }*/
        /*.dataTables_scrollHeadInner {
    width: max-content!important;
}*/
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {

            var table = $('#My_Table').DataTable({
                responsive: true,
                lengthChange: true,
               // scrollY: 320,
                //scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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
                                                return $('#My_Table').DataTable().column(idx).visible();
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

                var table = $('#My_Table').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 320,
                    //scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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
                                                    return $('#My_Table').DataTable().column(idx).visible();
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


    <script>
        $(document).ready(function () {

            var table = $('#tab4_mytable').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 320,
                //scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab4_mytable').DataTable().column(idx).visible();
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

                var table = $('#tab4_mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 320,
                    //scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab4_mytable').DataTable().column(idx).visible();
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


    <script>
        $(document).ready(function () {

            var table = $('#tab5_mytable').DataTable({
                responsive: true,
                lengthChange: true,
                //scrollY: 320,
                //scrollX: true,
                scrollCollapse: true,

                dom: 'lBfrtip',

                //Export functionality
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                return $('#tab5_mytable').DataTable().column(idx).visible();
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

                var table = $('#tab5_mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    //scrollY: 320,
                    //scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab5_mytable').DataTable().column(idx).visible();
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
                                                    return $('#tab5_mytable').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>Query Manager</span></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <asp:HyperLink ID="HyperLink1" runat="server" class="nav-link active" data-toggle="tab" href="#tab_1">Service Department</asp:HyperLink>
                                <%--<a class="nav-link active" data-toggle="tab" href="#tab_1">Service Department</a>--%>
                            </li>
                            <li class="nav-item">
                                <asp:HyperLink ID="HyperLink2" runat="server" class="nav-link" data-toggle="tab" href="#tab_2">Request Type</asp:HyperLink>
                                <%--<a class="nav-link" data-toggle="tab" href="#tab_2">Request Type</a>--%>
                            </li>
                            <li class="nav-item">
                                <asp:HyperLink ID="HyperLink3" runat="server" class="nav-link" data-toggle="tab" href="#tab_3">Request Category</asp:HyperLink>
                                <%--<a class="nav-link" data-toggle="tab" href="#tab_3">Request Category</a>--%>
                            </li>
                            <li class="nav-item">
                                <asp:HyperLink ID="HyperLink4" runat="server" class="nav-link" data-toggle="tab" href="#tab_4">Request Sub-Category</asp:HyperLink>
                                <%--<a class="nav-link" data-toggle="tab" href="#tab_4">Request Sub-Category</a>--%>
                            </li>
                            <li class="nav-item">
                                <asp:HyperLink ID="HyperLink5" runat="server" class="nav-link" data-toggle="tab" href="#tab_5">User Allocation</asp:HyperLink>
                                <%--<a class="nav-link" data-toggle="tab" href="#tab_5">User Allocation</a>--%>
                            </li>
                            <%--<li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Service Department</a>
                            </li>--%>
                            <%--<li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Request Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Request Category</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4">Request Sub-Category</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_5">User Allocation</a>
                            </li>--%>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__ball"></div>
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
                                                        <sup>* </sup>
                                                        <label>Department Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDepartmentName"
                                                        Display="None" ErrorMessage="Please Enter Department Name"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Short Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtShortssName" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShortssName"
                                                        Display="None" ErrorMessage="Please Enter Department Short Name"
                                                        ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="switch" name="switch" class="switch" checked>
                                                        <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:HiddenField ID="hdnchkSDept" runat="server" ClientIDMode="Static" />
                                            <asp:LinkButton ID="btnSubmitServiceDept" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit1" OnClick="btnSubmitServiceDept_Click" OnClientClick="return submitsave();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelServiceDept" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelServiceDept_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit1" />

                                        </div>

                                        <div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <asp:Repeater ID="lvServiceDept" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>Department Name</th>
                                                                <th>Short Name</th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditServiceDept" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("QMDepartmentID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditServiceDept_Click" />
                                                            </td>
                                                            <td><%# Eval("QMDepartmentName")%></td>
                                                            <td><%# Eval("QMDepartmentShortname")%></td>
                                                            <td><%--<span class="badge badge-success"><%# Eval("IsActive")%></span>--%>
                                                                <asp:Label ID="lablstatus" runat="server" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updtab_2"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__ball"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="updtab_2" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceDepartment" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlServiceDepartment"
                                                        Display="None" ErrorMessage="Please Enter Department" InitialValue="0"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Type Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtTypeName" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTypeName"
                                                        Display="None" ErrorMessage="Please Enter Type Name"
                                                        ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="switch1" name="switch" class="switch" checked />
                                                        <label data-on="Active" data-off="Inactive" for="switch1"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:HiddenField ID="hdnRequestType" runat="server" ClientIDMode="Static" />
                                            <asp:LinkButton ID="btnSubmitRequestType" runat="server" ValidationGroup="submit2" CssClass="btn btn-outline-info" OnClick="btnSubmitRequestType_Click" OnClientClick="return submitsavetabtwo();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelRequestType" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelRequestType_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit2" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="My_Table">
                                                <asp:Repeater ID="lvRequestType" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>Service Department</th>
                                                                <th>Type Name</th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditRequestType" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("QMRequestTypeID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditRequestType_Click" /></td>
                                                            <td><%# Eval("QMDepartmentName")%></td>
                                                            <td><%# Eval("QMRequestTypeName")%></td>
                                                            <td><%--<span class="badge badge-success"><%# Eval("IsActive")%></span>--%>
                                                                <asp:Label ID="lablstatus" runat="server" Text='<%# Eval("IsActive")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updTab3"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__ball"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="updTab3" runat="server">
                                    <ContentTemplate>


                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSDept" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AutoPostBack="true"
                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSDept_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSDept"
                                                        Display="None" ErrorMessage="Please Enter Department" InitialValue="0"
                                                        ValidationGroup="submit3"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRequestType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlRequestType"
                                                        Display="None" ErrorMessage="Please Enter Request Type" InitialValue="0"
                                                        ValidationGroup="submit3"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Category Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRequestCategory" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRequestCategory"
                                                        Display="None" ErrorMessage="Please Enter Request Category"
                                                        ValidationGroup="submit3"></asp:RequiredFieldValidator>
                                                </div>


                                                <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>General Instruction</label>
                                            </div>
                                            <asp:TextBox ID="txtGeneralInstruction" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                        </div>--%>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">

                                            <asp:LinkButton ID="btnSubmitRequestCategory" runat="server" ValidationGroup="submit3" CssClass="btn btn-outline-info" OnClick="btnSubmitRequestCategory_Click" OnClientClick="return submitsave();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelRequestCategory" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelRequestCategory_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit3" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <asp:Repeater ID="lvRequestCategory" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>Service Department</th>
                                                                <th>Request Type</th>
                                                                <th>Request Category Name</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditCategoryType" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("QMRequestCategoryID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditCategoryType_Click" />
                                                            </td>
                                                            <td><%# Eval("QMDepartmentName")%></td>
                                                            <td><%# Eval("QMRequestTypeName")%></td>
                                                            <td><%# Eval("QMRequestCategoryName")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </table>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_4">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updtab_4"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__ball"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="updtab_4" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AutoPostBack="true"
                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlDept"
                                                        Display="None" ErrorMessage="Please Enter Department" InitialValue="0"
                                                        ValidationGroup="submit4"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlReqstType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlReqstType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlReqstType"
                                                        Display="None" ErrorMessage="Please Enter Request Type" InitialValue="0"
                                                        ValidationGroup="submit4"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Category</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRequestCategory" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlRequestCategory"
                                                        Display="None" ErrorMessage="Please Enter Request Category" InitialValue="0"
                                                        ValidationGroup="submit4"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Sub-Category</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSubCategory" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSubCategory"
                                                        Display="None" ErrorMessage="Please Enter Sub Category"
                                                        ValidationGroup="submit4"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>General Instruction</label>
                                                    </div>
                                                    <asp:TextBox ID="txtInstruction" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-12 col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Is Paid Service</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-3 col-12">
                                                            <div class="label-dynamic">
                                                                <label>If Yes</label>
                                                            </div>
                                                            <div class="form-check pl-0">
                                                                <label class="form-check-label">
                                                                    <asp:CheckBox ID="chkYes" runat="server" Text="Yes" OnCheckedChanged="chkYes_CheckedChanged" AutoPostBack="true" Checked="false" />
                                                                    <%--<input type="checkbox" class="form-check-input" value="" style="margin-top: .2rem;margin-left: -1.25rem;">Yes--%>
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="paidservice" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" />

                                                            <%--add 28-04-2022 amount validation--%>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" runat="server" ControlToValidate="txtAmount"
                                                                Display="None" ErrorMessage="Please Enter Is Paid Service Amount"
                                                                ValidationGroup="submit4"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-12 col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Provide Emergency Support</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-3 col-12">
                                                            <div class="label-dynamic">
                                                                <label>If Yes</label>
                                                            </div>
                                                            <div class="form-check pl-0">
                                                                <label class="form-check-label">
                                                                    <asp:CheckBox ID="chkYess" runat="server" Text="Yes" OnCheckedChanged="chkYess_CheckedChanged" AutoPostBack="true" Checked="false" />
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="emergencyservice" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Hours</label>
                                                            </div>
                                                            <asp:TextBox ID="txtHours" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" />
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="emergencyservice1" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" />
                                                            <%--add 28-04-2022 amount validation--%>

                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtAmt"
                                                                Display="None" ErrorMessage="Please Enter Provide Emergency Support Amount"
                                                                ValidationGroup="submit4"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitsubcategory" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit4" OnClick="btnSubmitsubcategory_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelsubcategory" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelsubcategory_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit4" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab4_mytable">
                                                <asp:Repeater ID="lvsubcategory" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action</th>
                                                                <th>Request Type</th>
                                                                <th>Request Category</th>
                                                                <th>Request Sub-Category</th>
                                                                <th>General Instruction </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="btnEditsubCategory" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("QMRequestSubCategoryID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditsubCategory_Click" />
                                                            </td>
                                                            <td><%# Eval("QMRequestTypeName")%></td>
                                                            <td><%# Eval("QMRequestCategoryName")%></td>
                                                            <td><%# Eval("QMRequestSubCategoryName")%></td>
                                                            <td><%# Eval("GeneralInstruction")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade" id="tab_5">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updtab_5"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__ball"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>


                                <asp:UpdatePanel ID="updtab_5" runat="server">

                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Department</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceDepart" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        OnSelectedIndexChanged="ddlServiceDepart_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlServiceDepart"
                                                        Display="None" ErrorMessage="Please Enter Department" InitialValue="0"
                                                        ValidationGroup="submit5"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Request Type</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxRequestType" runat="server" CssClass="form-control " SelectionMode="Multiple" data-select2-enable="true" AppendDataBoundItems="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="lstbxRequestType"
                                                        Display="None" ErrorMessage="Please Select Request Type"
                                                        ValidationGroup="submit5"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Incharge</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlIncharge" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                                        OnSelectedIndexChanged="ddlIncharge_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlIncharge"
                                                        Display="None" ErrorMessage="Please Enter Select Incharge" InitialValue="0"
                                                        ValidationGroup="submit5"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Add Team</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxAddTeam" runat="server" CssClass="form-control " SelectionMode="Multiple" data-select2-enable="true" AppendDataBoundItems="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="lstbxAddTeam"
                                                        Display="None" ErrorMessage="Please Enter Select Team"
                                                        ValidationGroup="submit5"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitUser" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit5" OnClick="btnSubmitUser_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelUser" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelUser_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="submit5" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <div class="col-12 mt-3">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab5_mytable">
                                                    <asp:Repeater ID="lvUser" runat="server">
                                                        <HeaderTemplate>
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Action</th>
                                                                    <th>Service Department</th>
                                                                    <th>Request Type</th>
                                                                    <th>Incharge</th>
                                                                    <th>Team </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEditUserAllocation" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("QMUserAllocationID") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditUserAllocation_Click" />
                                                                </td>

                                                                <td><%# Eval("QMDepartmentName")%></td>
                                                                <td><%# Eval("QMRequestTypeName")%></td>
                                                                <td><%# Eval("InchargeName")%></td>
                                                                <td><%# Eval("Member")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                </table>
                                            </div>
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

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
    <script type="text/javascript" language="javascript">
        function SetStat1(val) {
            debugger;
            $('#switch').prop('checked', val)
            $('#switch1').prop('checked', val)

        }

        function submitsave() {
            debugger;
            {

                $('#hdnchkSDept').val($('#switch').prop('checked'));
                $('#hdnRequestType').val($('#switch1').prop('checked'));


            }
        }


        //added by amol 09/03/2022
        function submitsavetabtwo() {
            debugger;
            {

                $('#hdnchkSDept').val($('#switch1').prop('checked'));
                $('#hdnRequestType').val($('#switch1').prop('checked'));


            }
        }
    </script>
    <script lang="javascript" type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }
    </script>

</asp:Content>

