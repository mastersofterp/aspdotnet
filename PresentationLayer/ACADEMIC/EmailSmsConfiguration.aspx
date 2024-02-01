<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EmailSmsConfiguration.aspx.cs" Inherits="ACADEMIC_EmailSmsConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .badge {
            display: inline-block;
            padding: .25em .4em;
            font-size: 95%;
            font-weight: 700;
            line-height: 1;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: .25rem;
            transition: color .15s ease-in-out,background-color .15s ease-in-out,border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
    </style>

    <script>
        $(document).ready(function () {
            var table = $('#tblwhatsapp').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
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
                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                var table = $('#tblwhatsapp').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
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
                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
            var table = $('#tblwhatsapp').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
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
                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                var table = $('#tblwhatsapp').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
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
                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
                                                    return $('#tblwhatsapp').DataTable().column(idx).visible();
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
            var table = $('#mytablesLink').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 300,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
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
                                return $('#mytables').DataTable().column(idx).visible();
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
                                                return $('#mytables').DataTable().column(idx).visible();
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
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytablesLink').DataTable().column(idx).visible();
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
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytablesLink').DataTable().column(idx).visible();
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
                var table = $('#mytablesLink').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 300,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
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
                                    return $('#mytables').DataTable().column(idx).visible();
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
                                                    return $('#mytablesLink').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytablesLink').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytablesLink').DataTable().column(idx).visible();
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
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <%-- <h3 class="box-title"><span>Email SMS Configuration </span></h3>--%>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Service Provider</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Email Configuration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">SMS Service Providers</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_7" tabindex="1">SMS Template Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_6" tabindex="1">SMS Configuration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="1">Whats App Configuration</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link " data-toggle="tab" href="#tab_8" tabindex="1">WhatsApp Template Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_9" tabindex="1">WhatsApp Template</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_5" tabindex="1">Link Assign</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:HiddenField ID="hfdProv" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updRule"
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

                                <asp:UpdatePanel ID="updRule" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Configuration</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlConfiguration" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlConfiguration_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Email</asp:ListItem>
                                                        <asp:ListItem Value="2">SMS</asp:ListItem>
                                                        <asp:ListItem Value="3">Whats App</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlConfiguration"
                                                        Display="None" ErrorMessage="Please Select Configuration." InitialValue="0" ValidationGroup="Service" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Provider Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceProviderName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2" OnSelectedIndexChanged="ddlConfiguration_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlServiceProviderName"
                                                        Display="None" ErrorMessage="Please Select Service Provider Name." InitialValue="0" ValidationGroup="Service" />


                                                    <%--   <asp:TextBox ID="txtServiceProvideName" runat="server" CssClass="form-control" TabIndex="3" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServiceProvideName"
                                                        Display="None" ErrorMessage="Please Enter Service Provider Name" InitialValue="" ValidationGroup="Service" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Parameter</label>
                                                    </div>
                                                    <asp:ListBox ID="lstParameter" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                        CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="lstParameter"
                                                        Display="None" ErrorMessage="Please Select Parameter." InitialValue="" ValidationGroup="Service" />
                                                </div>
                                                <%--Added by Nikhil  Lambe--%>
                                                <div style="display:none">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divPriority" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Priority</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control" AppendDataBoundItems="true" ToolTip="Please select priority" data-select2-enable="true" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ControlToValidate="ddlPriority" ErrorMessage="Please Select Priority."
                                                        InitialValue="0" Display="None" SetFocusOnError="true" ValidationGroup="Service"></asp:RequiredFieldValidator>
                                                </div>
                                                    </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                       <input type="checkbox" id="rdActiveProvider" name="switch" checked />
                                                            <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActiveProvider"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnService" runat="server" CssClass="btn btn-outline-info" OnClick="btnService_Click" TabIndex="5" OnClientClick="return validateProvider();">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnServiceCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnServiceCancel_Click" TabIndex="6">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Service"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                        </div>
                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:ListView ID="lvServiceProvider" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action</th>
                                                                        <th>Configuration</th>
                                                                        <th>Service Provider Name</th>
                                                                        <th>Parameter</th>
                                                                        <th>Active Status</th>
                                                                        <%--<th>Priority</th>--%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%#Eval("SERVICE_NO") %>' OnClick="btnEdit_Click" />

                                                            </td>
                                                            <td><%#Eval("CONFIG_NAME") %></td>
                                                            <td><%#Eval("SERVICE_PROVIDER_NAME") %></td>
                                                            <td><%#Eval("LABEL") %></td>
                                                            <%--<td>
                                                                <asp:Label ID="lblPriority" runat="server" Text='<%#Eval("PRIORITY") %>' ForeColor='<%#Eval("PRIORITY").Equals("Not Set") ? System.Drawing.Color.Red : System.Drawing.Color.Black%>'></asp:Label>

                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblStatusProv" runat="server" Text='<%#Eval("ACTIVE_STATUS") %>'></asp:Label>                                                                
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <script>
                                            function SetStateProvider(val) {
                                                $('#rdActiveProvider').prop('checked', val);
                                            }
                                            function validateProvider() {
                                                var config=document.getElementById('<%=ddlConfiguration.ClientID%>').value;
                                                var param=document.getElementById('<%=lstParameter.ClientID%>').value;
                                                var provName=document.getElementById('<%=ddlServiceProviderName.ClientID%>').value;
                                                var alertMsg="";
                                                $('#hfdProv').val($('#rdActiveProvider').prop('checked'));
                                                if(config==0)
                                                {
                                                    alertMsg+="Please select configuration.\n";
                                                }
                                                if(provName==0)
                                                {
                                                    alertMsg+="Please select service provider name.\n";
                                                }
                                                if(param=="")
                                                {
                                                    alertMsg+="Please select atleast one parameter.";
                                                }
                                                if(alertMsg!="")
                                                {
                                                    alert(alertMsg);
                                                    return false;
                                                }
                                                else
                                                {
                                                    return true;
                                                }
                                                //alert(param);
                                            }
                                        </script>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="tab_2">
                                <div>
                                    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />

                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                                        <label>Service Provider</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceProvider" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceProvider_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlServiceProvider"
                                                        Display="None" ErrorMessage="Please Select Service Provider." InitialValue="0" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSMTPServer">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMTP Server</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMTPServer" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSMTPServer"
                                                        Display="None" ErrorMessage="Please Enter SMTP Server." InitialValue="" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="SMTPServerPort">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMTP Server Port</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMTPServerPort" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSMTPServerPort"
                                                        Display="None" ErrorMessage="Please Enter SMTP Server Port." InitialValue="" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivCKey">
                                                    <div class="label-dynamic">
                                                        <sup>*</sup>
                                                        <label>CKey/UserId</label>
                                                    </div>
                                                    <asp:TextBox ID="txtCKey" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCKey"
                                                        Display="None" ErrorMessage="Please Enter CKey/UserId." InitialValue="" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivEmailID">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Email ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmailID"
                                                        Display="None" ErrorMessage="Please Enter Email ID." InitialValue="" ValidationGroup="ServiceEmail" />
                                                    <asp:RegularExpressionValidator ID="rfvuserEmail" runat="server" ControlToValidate="txtEmailID"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="ServiceEmail"></asp:RegularExpressionValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbxemailid" runat="server" TargetControlID="txtEmailID"
                                                        InvalidChars="ABCDEFGHIJKLNMOPQRSTUVWXYZ" FilterMode="InvalidChars" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivPassword">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Password</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPassword"
                                                        Display="None" ErrorMessage="Please Enter Password." InitialValue="" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divAPI">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>API Key</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAPI" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAPI"
                                                        Display="None" ErrorMessage="Please Enter API Key." InitialValue="" ValidationGroup="ServiceEmail" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="switchEmail" name="switchEmail" class="switchEmail" checked tabindex="8" />
                                                        <label data-on="Active" data-off="Inactive" for="switchEmail"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="ServiceEmail" ClientIDMode="Static" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ServiceEmail"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvEmailServices" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action</th>
                                                                        <th>Service Provider</th>
                                                                        <th>SMTP Server</th>
                                                                        <th>SMTP Server Port</th>
                                                                        <th>CKey/UserId</th>
                                                                        <th>Email ID</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="btnEditEmail" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%#Eval("EMAILSMS_NO") %>' OnClick="btnEditEmail_Click" />
                                                            </td>
                                                            <td><%#Eval("SERVICE_PROVIDER_NAME") %></td>
                                                            <td><%#Eval("SMTP_SERVER") %></td>
                                                             <td><%#Eval("SMTP_PORT") %></td>
                                                            <td><%#Eval("CKEY_USERID") %></td>
                                                            <td><%#Eval("EMAILID") %></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblEmailStatus" Text='<%# Eval("STATUS") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:HiddenField ID="hdnSms" runat="server" ClientIDMode="Static" />

                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Provider</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceProviderSMS" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceProviderSMS_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlServiceProviderSMS"
                                                        Display="None" ErrorMessage="Please Select Service Provider." InitialValue="0" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSMSAPI">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS API</label>
                                                    </div>
                                                    <asp:TextBox ID="TxtSMSAPI" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtSMSAPI"
                                                        Display="None" ErrorMessage="Please Enter SMS API." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSMSUserID">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS User ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMSUserID" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSMSUserID"
                                                        Display="None" ErrorMessage="Please Enter SMS User ID." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivPasswordSMS">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Password</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPasswordSMS" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtPasswordSMS"
                                                        Display="None" ErrorMessage="Please Enter Password." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivEmailSMS">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Email ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmailSMS" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEmailSMS"
                                                        Display="None" ErrorMessage="Please Enter Email ID." InitialValue="" ValidationGroup="ServiceSMS" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailSMS"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID." ValidationGroup="ServiceSMS"></asp:RegularExpressionValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtEmailSMS"
                                                        InvalidChars="ABCDEFGHIJKLNMOPQRSTUVWXYZ" FilterMode="InvalidChars" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSMSParameterI">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS Parameter I</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMSParameterI" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtSMSParameterI"
                                                        Display="None" ErrorMessage="Please Enter SMS Parameter I." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivSMSParameterII">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS Parameter II</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMSParameterII" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtSMSParameterII"
                                                        Display="None" ErrorMessage="Please Enter SMS Parameter II." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divSMSProvider">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS Provider</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMSProvider" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtSMSProvider"
                                                        Display="None" ErrorMessage="Please Enter SMS Provider." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divSMSUrl">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>SMS Url</label>
                                                    </div>
                                                    <asp:TextBox ID="txtSMSUrl" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtSMSUrl"
                                                        Display="None" ErrorMessage="Please Enter SMS Url." InitialValue="" ValidationGroup="ServiceSMS" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="switchSms" name="switchSms" class="switchSms" checked tabindex="8" />
                                                        <label data-on="Active" data-off="Inactive" for="switchSms"></label>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitSMS" runat="server" CssClass="btn btn-outline-info" ValidationGroup="ServiceSMS" ClientIDMode="Static" OnClick="btnSubmitSMS_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelSMS" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelSMS_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="ServiceSMS"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvsmsData" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action</th>
                                                                        <th>Service Provider</th>
                                                                        <th>SMS API</th>
                                                                        <th>SMS User ID</th>
                                                                        <th>Email ID</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="btnEditSms" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%#Eval("EMAILSMS_NO") %>' OnClick="btnEditSms_Click" />
                                                            </td>


                                                            <td><%#Eval("SERVICE_PROVIDER_NAME") %></td>
                                                            <td><%#Eval("SMS_API") %></td>
                                                            <td><%#Eval("SMS_User_ID") %></td>
                                                            <td><%#Eval("EMAILID") %></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblSMSStatus" Text='<%#Eval("STATUS") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_7">
                                <div>
                                    <asp:HiddenField ID="hfdStatTempType" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="UpdprogDepartment" runat="server" AssociatedUpdatePanelID="updtmptyp"
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
                                <asp:UpdatePanel ID="updtmptyp" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template Type</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTemplateType" runat="server" CssClass="form-control" TabIndex="2"
                                                            ToolTip="Please Enter Template Type." AutoComplete="OFF" placeholder="Template Type" ValidationGroup="submit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtTemplateType" />
                                                    </div>


                                                    <div class="form-group col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActiveTemType" name="switch" checked />

                                                            <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActiveTemType"></label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSave" TabIndex="4" ToolTip="Submit" ValidationGroup="Submit" OnClientClick="return validateTempType();"
                                                CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnUpdate" TabIndex="5" ToolTip="Update" OnClick="btnUpdate_Click" OnClientClick="return validateTempType();" ValidationGroup="Submit"
                                                CssClass="btn btn-primary" runat="server" Text="Update" Visible="false" />
                                            <asp:Button ID="btnCancelTempType" TabIndex="6" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancelTempType_Click" />
                                            <asp:ValidationSummary ID="vlsummary" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                        </div>

                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvTempType" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Template Type List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center;">Edit
                                                                    </th>
                                                                    <th>Template Type
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
                                                        <asp:UpdatePanel runat="server" ID="updTemplate">

                                                            <ContentTemplate>

                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btnEditTempType" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("TEMPLATE_ID") %>'
                                                                            AlternateText="Edit Record" class="newAddNew Tab" ToolTip="Edit Record" OnClick="btnEditTempType_Click" TabIndex="7" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TEMPLATE_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Active" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>
                                        </div>
                                        <script>
                                            function SetStatTemType(val) {
                                                $('#rdActiveTemType').prop('checked', val);
                                            }
                                            function validateTempType() {
                                                $('#hfdStatTempType').val($('#rdActiveTemType').prop('checked'));
                                                var tempType="";
                                                tempType=document.getElementById('<%= txtTemplateType.ClientID%>').value;
                                                if(tempType=="")
                                                {
                                                    alert('Please enter template type.');
                                                    return false;
                                                }
                                                else
                                                {
                                                    return true;
                                                }
                                            }
                                        </script>
                                    </ContentTemplate>

                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade" id="tab_6">
                                <div>
                                    <asp:HiddenField ID="hfSmsStatus" runat="server" ClientIDMode="Static" />

                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updsms"
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
                                <asp:UpdatePanel ID="updsms" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlTemplateType" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem>Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTemplateName" AutoComplete="off" TabIndex="3" placeholder="Enter Template Name" runat="server" CssClass="form-control"
                                                            ToolTip="Please Enter Template Name." />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=". ()" TargetControlID="txtTemplateName" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Page Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPageName" runat="server" TabIndex="4" multiple="multiple" Visible="false" ToolTip="Please Select Page Name">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnpageno" runat="server" />
                                                        <asp:ListBox ID="lstbxPageName" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTemplateId" AutoComplete="off" TabIndex="5" placeholder="Enter Template Id" runat="server" CssClass="form-control"
                                                            ToolTip="Please Enter Template Id." onkeypress="return (event.charCode>47 && event.charCode<58)" MaxLength="25" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTemplate" runat="server" TabIndex="6" TextMode="MultiLine" Height="60" Width="240" ToolTip="Please Enter Template"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup> </sup>
                                                            <label>Var Count</label>
                                                        </div>
                                                        <asp:TextBox ID="txtVarCount" runat="server" TabIndex="7" ToolTip="Please Enter Variable Count " onkeypress="return (event.charCode>47 && event.charCode<58)" MaxLength="2" ></asp:TextBox>
                                                    </div>
                                                    <div class="form-group  col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActiveSmsTemType" name="switch" checked />

                                                            <label data-on="Active" tabindex="8" class="newAddNew Tab" data-off="Inactive" for="rdActiveSmsTemType"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit_Temp" TabIndex="9" ToolTip="Submit" ValidationGroup="Submit" OnClick="btnSubmit_Temp_Click"
                                                    CssClass="btn btn-primary" OnClientClick="return validateTemplate();" runat="server" Text="Submit" />
                                                <asp:Button ID="btnUpdateSms" TabIndex="10" ToolTip="Update" OnClientClick="return validateTemplate();" ValidationGroup="Submit" OnClick="btnUpdateSms_Click"
                                                    CssClass="btn btn-primary" runat="server" Text="Update" Visible="false" />
                                                <asp:Button ID="btnCancelSmsTemp" TabIndex="11" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancelSmsTemp_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlSmsTeplate" runat="server" Visible="true">
                                                    <asp:ListView ID="lvSmsTemplate" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>SMS Template List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="tblSms" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>Template Type
                                                                        </th>
                                                                        <th>Template Name
                                                                        </th>
                                                                        <%--  <th>Page Name
                                                                                    </th>--%>
                                                                        <th>Template id
                                                                        </th>
                                                                        <th>Template 
                                                                        </th>
                                                                        <th>Variable Count
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
                                                            <tr>
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEditSmsType" class="newAddNew Tab" TabIndex="12" runat="server" ImageUrl="~/images/edit.png" OnClick="btnEditSmsType_Click"
                                                                        CommandArgument='<%# Eval("SMS_TEMPLATE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" />

                                                                </td>
                                                                <td>
                                                                    <%# Eval("TEMPLATE_TYPE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TEMPLATE_NAME")%>
                                                                </td>
                                                                <%--<td>
                                                                                <%# Eval("AL_Link")%>
                                                                            </td>--%>
                                                                <td>
                                                                    <%# Eval("TEM_ID")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TEMPLATE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("VARIABLE_COUNT")%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblActiveStatus" Text='<%# Eval("ACTIVE_STATUS")%>' runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <script>
                                    function SetSmsStatTemType(val) {
                                        $('#rdActiveSmsTemType').prop('checked', val);
                                    }
                                    function validateTemplate() {
                                        $('#hfSmsStatus').val($('#rdActiveSmsTemType').prop('checked'));
                                        var tempId="";
                                        var tempName="";
                                        var template="";
                                        var templateType="";
                                        tempId=document.getElementById('<%=txtTemplateId.ClientID%>').value;
                                        tempName=document.getElementById('<%=txtTemplateName.ClientID%>').value;
                                        template=document.getElementById('<%=txtTemplate.ClientID%>').value;
                                        templateType=document.getElementById('<%=ddlTemplateType.ClientID%>').value;
                                        var alertMsg="";
                                        if(tempId=="" || tempName=="" || template=="" || templateType==0)
                                        {
                                            if(templateType==0)
                                            {
                                                alertMsg+="Please enter template type.\n";
                                            }
                                            if(tempName=="")
                                            {
                                                alertMsg+="Please enter template name.\n";
                                            }
                                            if(tempId=="")
                                            {
                                                alertMsg+="Please enter template id.\n";
                                            }
                                            
                                            if(template=="")
                                            {
                                                alertMsg+="Please enter template.\n";
                                            }
                                            alert(alertMsg);
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                        
                                    } 

                                    function clearTemp() {
                                        document.getElementById('<%=txtTemplateId.ClientID%>').value="";
                                        document.getElementById('<%=txtTemplateName.ClientID%>').value="";
                                        document.getElementById('<%=txtTemplate.ClientID%>').value="";
                                    }
                                </script>
                            </div>

                            <div class="tab-pane fade" id="tab_4">
                                <div>
                                    <asp:HiddenField ID="hdnWhatsaap" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
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
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Service Provider</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlServiceProviderWhat" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlServiceProviderWhat_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlServiceProviderWhat"
                                                        Display="None" ErrorMessage="Please Select Service Provider." InitialValue="0" ValidationGroup="ServiceWhatsaap" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivURL">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>API URL</label>
                                                    </div>
                                                    <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtURL"
                                                        Display="None" ErrorMessage="Please Enter API URL." InitialValue="" ValidationGroup="ServiceWhatsaap" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivToken">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Token</label>
                                                    </div>
                                                    <asp:TextBox ID="txtToken" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtToken"
                                                        Display="None" ErrorMessage="Please Enter Token." InitialValue="" ValidationGroup="ServiceWhatsaap" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivAccountID">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Account SID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAccountID" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtAccountID"
                                                        Display="None" ErrorMessage="Please Enter Account SID." InitialValue="" ValidationGroup="ServiceWhatsaap" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivMobileNo">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Mobile No.</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" TabIndex="1" onkeypress="return (event.charCode>47 && event.charCode<58)" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtMobileNo"
                                                        Display="None" ErrorMessage="Please Enter Mobile No." InitialValue="" ValidationGroup="ServiceWhatsaap" />
                                                    <%--<asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid"
                                                        ID="revMobile" ControlToValidate="txtMobileNo" ValidationExpression=".{10}.*"
                                                        Display="None" ValidationGroup="Register"></asp:RegularExpressionValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divUserWhatsApp">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>User Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" TabIndex="1" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtUserName"
                                                        Display="None" ErrorMessage="Please Enter User Name." InitialValue="" ValidationGroup="ServiceWhatsaap" />

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divAPI_Key">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>API Key</label>
                                                    </div>
                                                    <asp:TextBox ID="txtAPIKey" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtAPIKey"
                                                        Display="None" ErrorMessage="Please Enter API Key." InitialValue="" ValidationGroup="ServiceWhatsaap" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Status</label>
                                                    </div>
                                                    <div class="switch form-inline">
                                                        <input type="checkbox" id="switchWhatsaap" name="switchWhatsaap" class="switchWhatsaap" checked tabindex="8" />
                                                        <label data-on="Active" data-off="Inactive" for="switchWhatsaap"></label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitWhatsApp" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitWhatsApp_Click" ClientIDMode="Static" ValidationGroup="ServiceWhatsaap">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCnacelWhatsApp" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCnacelWhatsApp_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ServiceWhatsaap"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <asp:ListView ID="lvWhatsapp" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblwhatsapp">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action</th>
                                                                        <th>Service Provider</th>
                                                                        <th>API URL</th>
                                                                        <th>API Key</th>
                                                                         <th>Account ID</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Status</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton runat="server" ID="btnEditWhatsapp" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%#Eval("EMAILSMS_NO") %>' OnClick="btnEditWhatsapp_Click" />
                                                            </td>
                                                            <td><%#Eval("SERVICE_PROVIDER_NAME") %></td>
                                                            <td><%#Eval("WHATSAAP_API_URL") %></td>
                                                            <td><%#Eval("API_KEY") %></td>
                                                            <td><%#Eval("WHATSAAP_ACCOUNT_SID") %></td>
                                                            <td><%#Eval("WHATSAAP_MOBILE") %></td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblWhatstatus" Text='<%#Eval("STATUS") %>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane" id="tab_5">
                                <div>

                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel4"
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

                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Domain</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDomain" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDomain_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlDomain"
                                                        Display="None" ErrorMessage="Please Select Domain" InitialValue="0"/>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <asp:ListView ID="lvALinks" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytablesLink">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center">Select
                                                      <%--  <asp:CheckBox runat="server" ID="chkall" onclick="return totAll(this);" />--%>
                                                                    </th>
                                                                    <th style="text-align: center">Domain
                                                                    </th>
                                                                    <th style="text-align: center">Page Name
                                                                    </th>
                                                                    <th style="text-align: center">Email
                                                                    </th>
                                                                    <th style="text-align: center">Sms
                                                                    </th>
                                                                    <th style="text-align: center">WhatsApp
                                                                    </th>
                                                                    <th style="text-align: center; display: none">Multiple
                                                                    </th>

                                                                </tr>
                                                            </thead>
                                                            <tbody class="test">
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>

                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="check" />
                                                                <asp:Label runat="server" ID="lblalno" Text='<%# Eval("al_no")%>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("as_title")%>
                                                            </td>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblPageName" Text='<%# Eval("al_link")%>'></asp:Label>

                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="ckhEmail" Checked='<%# (Eval("EMAIL").ToString() == "1") ? true  : false %>' />
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="ChkSms" Checked='<%# (Eval("SMS").ToString() == "1") ? true  : false  %>' />
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="ChkWhatssp" Checked='<%# (Eval("WHATSAAP").ToString() == "1") ? true  : false  %>' />
                                                            </td>
                                                            <td style="text-align: center; display: none">
                                                                <asp:CheckBox runat="server" ID="chkMultiple" Checked='<%# (Eval("MULTIPLE_STATUS").ToString() == "1") ? true  : false  %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>

                                        </div>
                                        <div class="col-12 btn-footer" runat="server" visible="false" id="DivLinkButton">
                                            <asp:LinkButton ID="btnLinkAssin" runat="server" CssClass="btn btn-outline-info" OnClick="btnLinkAssin_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelLink" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelLink_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="ServiceSMS"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_8">
                                <div>
                                    <asp:HiddenField ID="hfdWhatsappType" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="Updprogress2" runat="server" AssociatedUpdatePanelID="updwhatsApp"
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
                                <asp:UpdatePanel ID="updwhatsApp" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Whatsapp Template Type</label>
                                                        </div>
                                                        <asp:TextBox ID="txtWAtemp" runat="server" CssClass="form-control" TabIndex="2"
                                                            ToolTip="Please Enter Whatsapp Template Type." AutoComplete="OFF" placeholder="Template Type" ValidationGroup="whatsappsubmit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtWAtemp" />
                                                    </div>


                                                    <div class="form-group col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActiveTemType2" name="switch" checked />

                                                            <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActiveTemType2"></label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">

                                            <asp:Button ID="btnWhatsAppSubmit" TabIndex="4" ToolTip="Submit" ValidationGroup="Submit" OnClientClick="return validatewhatsapp();"
                                                CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnWhatsAppSubmit_Click" />
                                            <asp:Button ID="btnWhatsAppUpdate" TabIndex="5" ToolTip="Update" OnClick="btnWhatsAppUpdate_Click" OnClientClick="return validatewhatsapp();" ValidationGroup="Submit"
                                                CssClass="btn btn-primary" runat="server" Text="Update" Visible="false" />
                                            <asp:Button ID="btnCancel1" TabIndex="6" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel1_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="whatsappsubmit" />

                                        </div>

                                        <div class="col-md-12">

                                            <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto">
                                                <asp:ListView ID="lvWhatsAppTemplate" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Template Type List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th style="text-align: center;">Edit
                                                                    </th>
                                                                    <th>Template Type
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
                                                        <asp:UpdatePanel runat="server" ID="updWaTemplate">

                                                            <ContentTemplate>

                                                                <tr>
                                                                    <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btnEdit1" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("WHATSAPP_TEMPLATE_ID") %>'
                                                                            AlternateText="Edit Record" class="newAddNew Tab" ToolTip="Edit Record" OnClick="btnEdit1_Click" TabIndex="7" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TEMPLATE_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblWhatsAppActive" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_9">
                                <div>
                                    <asp:HiddenField ID="hfWhatsappstatus" runat="server" ClientIDMode="Static" />

                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="upnlwhatsapptemp"
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
                                <asp:UpdatePanel ID="upnlwhatsapptemp" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Whatsapp Template Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlwhatsapptemp" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Whatsapp Template Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtwhatsapp" AutoComplete="off" TabIndex="3" placeholder="Enter Whatsapp Template Name" runat="server" CssClass="form-control"
                                                            ToolTip="Please Enter Template Name." />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=". ()" TargetControlID="txtwhatsapp" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Page Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlwhatsapppage" runat="server" TabIndex="4" multiple="multiple" Visible="false" ToolTip="Please Select Page Name">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        <asp:ListBox ID="lvlistwhatsapp" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                            CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"></asp:ListBox>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <%-- <sup>* </sup>--%>
                                                            <label>Template Id</label>
                                                        </div>
                                                        <asp:TextBox ID="txtwhatsapptempid" AutoComplete="off" TabIndex="5" placeholder="Enter Template Id" runat="server" CssClass="form-control"
                                                            ToolTip="Please Enter Template Id" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Template</label>
                                                        </div>
                                                        <asp:TextBox ID="txtwhatsapptemp" runat="server" TabIndex="6" TextMode="MultiLine" Height="60" Width="240" MaxLength="1490" ToolTip="Please Enter Whatsapp Template"></asp:TextBox>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Var Count</label>
                                                        </div>
                                                        <asp:TextBox ID="txtwhatsappcount" runat="server" TabIndex="7" ToolTip="Please Enter Variable Count " onkeyup="return whatsappNumbers();"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group  col-md-6 col-12">
                                                        <div class="label-dynamic">

                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActivewhatsappTem" name="switch" checked />

                                                            <label data-on="Active" tabindex="8" class="newAddNew Tab" data-off="Inactive" for="rdActivewhatsappTem"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnwhatsapptemp" TabIndex="9" ToolTip="Submit" ValidationGroup="Submit" OnClick="btnwhatsapptemp_Click"
                                                    CssClass="btn btn-primary" OnClientClick="return whatsappvalidate();" runat="server" Text="Submit" />
                                                <asp:Button ID="btnwhatsapptemplateupdate" TabIndex="10" ToolTip="Update" OnClientClick="return whatsappvalidate();" ValidationGroup="Submit" OnClick="btnwhatsapptemplateupdate_Click"
                                                    CssClass="btn btn-primary" runat="server" Text="Update" Visible="false" />
                                                <asp:Button ID="btnwhatsappCancel" TabIndex="11" ToolTip="Cancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnwhatsappCancel_Click" />

                                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="Panel8" runat="server" Visible="true">
                                                    <asp:ListView ID="lvwhatsapptempnew" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Whatsapp Template List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="tblSms" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>Template Type
                                                                        </th>
                                                                        <th>Template Name
                                                                        </th>
                                                                      <%--  <th>Page Name
                                                                        </th>
                                                                        <th>Template Id
                                                                        </th>--%>
                                                                        <th>Template 
                                                                        </th>
                                                                      <%--  <th>Variable Count
                                                                        </th>--%>
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
                                                            <tr>
                                                                <td style="text-align: center;">
                                                                    <asp:ImageButton ID="btnEditwhatsappType" class="newAddNew Tab" TabIndex="12" runat="server" ImageUrl="~/images/edit.png" OnClick="btnEditwhatsappType_Click"
                                                                        CommandArgument='<%# Eval("WHATSAPP_TEMPLATE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" />

                                                                </td>
                                                                <td>
                                                                    <%# Eval("TEMPLATE_TYPE")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TEMPLATE_NAME")%>
                                                                </td>
                                                               <%-- <td>
                                                                    <%# Eval("AL_Link")%>
                                                                </td>--%>
                                                               <%-- <td>
                                                                    <%# Eval("TEM_ID")%>
                                                                </td>--%>
                                                                <td>
                                                                    <%# Eval("TEMPLATE")%>
                                                                </td>
                                                               <%-- <td>
                                                                    <%# Eval("VARIABLE_COUNT")%>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblActiveStatus" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("ACTIVE")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
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

    <script>
        const phoneNo = document.querySelector('.phoneNo');
        const phoneNoDropdown = document.querySelector(".phone-no-dropdown");
        //initialize telephone input
        const iti = window.intlTelInput(phoneNo, ({
            allowDropdown: true,
            dropdownContainer: phoneNoDropdown,
            autoPlaceholder: "aggressive",
            initialCountry: "in",
            placeholderNumberType: "MOBILE",
            utilsScript: "./node_modules/intl-tel-input/build/js/utils.js"
        }));
    </script>

    <script>
        function SetEmail(val) {
            $('#switchEmail').prop('checked', val);
        }

       
        var summary = "";
        $(function () {
            
            $('#btnSubmit').click(function () {
                localStorage.setItem("currentId", "#btnSubmit,Submit");
                debugger;
                //   ShowLoader('#btnSubmit');
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switchEmail').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    localStorage.setItem("currentId", "#btnSubmit,Submit");
                    //      ShowLoader('#btnSubmit');
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switchEmail').prop('checked'));
                });
            });
        });
    </script>
    <script>
        function SetSMS(val) {
            $('#switchSms').prop('checked', val);
        }

       
        var summary = "";
        $(function () {
            
            $('#btnSubmitSMS').click(function () {
                localStorage.setItem("currentId", "#btnSubmitSMS,Submit");
                debugger;
                //  ShowLoader('#btnSubmitSMS');
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
                $('#hdnSms').val($('#switchSms').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitSMS').click(function () {
                    localStorage.setItem("currentId", "#btnSubmitSMS,Submit");
                    //    ShowLoader('#btnSubmitSMS');
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hdnSms').val($('#switchSms').prop('checked'));
                });
            });
        });
    </script>
    <script>
        function SetWhats(val) {
            $('#switchWhatsaap').prop('checked', val);
        }

       
        var summary = "";
        $(function () {
            
            $('#btnSubmitWhatsApp').click(function () {
                localStorage.setItem("currentId", "#btnSubmitWhatsApp,Submit");
                debugger;
                //   ShowLoader('#btnSubmitWhatsApp');
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
                $('#hdnWhatsaap').val($('#switchWhatsaap').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitWhatsApp').click(function () {
                    localStorage.setItem("currentId", "#btnSubmitWhatsApp,Submit");
                    // ShowLoader('#btnSubmitWhatsApp');
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hdnWhatsaap').val($('#switchWhatsaap').prop('checked'));
                });
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
        function SetTemplate(val) {
            $('#switchTemplate').prop('checked', val);
        }       
        var summary = "";
        $(function () {            
            $('#btnSaveTemp').click(function () {
                localStorage.setItem("currentId", "#btnSaveTemp,Submit");
                debugger;
                if (summary != "") {
                    customAlert(summary); 
                    summary = "";
                    return false
                }
                $('#hdnSMSTemplate').val($('#switchTemplate').prop('checked'));
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSaveTemp').click(function () {
                    localStorage.setItem("currentId", "#btnSaveTemp,Submit");
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hdnSMSTemplate').val($('#switchTemplate').prop('checked'));
                });
            });
        });
    </script>
    <script>
        function SetwhatsappTemType(val) {
            $('#rdActiveTemType2').prop('checked', val);
        }
        function validatewhatsapp() {
            $('#hfdWhatsappType').val($('#rdActiveTemType2').prop('checked'));
            var idtxtweb = $("[id$=txtWAtemp]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Whatsapp Template Type.', 'Warning!');
                $(txtweb).focus();
                return false;
            }
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnWhastappSubmit').click(function () {
                        validatewhatsapp();
                    });
                });
            });
        }
    </script>
    <script>
        function SetWhatsappStatTemType(val) {
            $('#rdActivewhatsappTem').prop('checked', val);
        }


        function whatsappvalidate() {

            $('#hfWhatsappstatus').val($('#rdActivewhatsappTem').prop('checked'));
            var whatsTempType="";
            var whatsTempName="";
            var whatsTemplate="";
            var alertMessage="";
            whatsTempType=document.getElementById('<%=ddlwhatsapptemp.ClientID%>').value;
            whatsTempName=document.getElementById('<%=txtwhatsapp.ClientID%>').value;
            whatsTemplate=document.getElementById('<%=txtwhatsapptemp.ClientID%>').value;

            if(whatsTempType==0)
            {
                alertMessage+="Please select whatsapp template type.\n";
            }
            if(whatsTempName==0)
            {
                alertMessage+="Please select whatsapp template name.\n";
            } 
            if(whatsTemplate==0)
            {
                alertMessage+="Please select template.\n";
            }
            if(alertMessage!="")
            {
                alert(alertMessage);
                return false;
            }
            else
            {
                return true;
            }           
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnwhatsapptemp').click(function () {
                });
            });
        });
    </script>
</asp:Content>

