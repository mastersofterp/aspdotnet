<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="PaperSetterStaffEntry.aspx.cs" MaintainScrollPositionOnPostback="false" Inherits="Academic_Masters_Staff" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <%--<asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPS"
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
        </asp:UpdateProgress>--%>
    </div>
    <%-- <asp:UpdatePanel ID="updPS" runat="server">
        <ContentTemplate>--%>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <%--  CssClass="form-control multi-select-demo" --%>
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
        $(document).ready(function () {
            var table = $('#tblpref').DataTable({
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
                                return $('#tblpref').DataTable().column(idx).visible();
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
                                            return $('#tblpref').DataTable().column(idx).visible();
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
                                            return $('#tblpref').DataTable().column(idx).visible();
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
                var table = $('#tblpref').DataTable({
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
                                    return $('#tblpref').DataTable().column(idx).visible();
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
                                                return $('#tblpref').DataTable().column(idx).visible();
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
                                                return $('#tblpref').DataTable().column(idx).visible();
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

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblstafflist').DataTable({
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
                                return $('#tblstafflist').DataTable().column(idx).visible();
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
                                            return $('#tblstafflist').DataTable().column(idx).visible();
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
                                            return $('#tblstafflist').DataTable().column(idx).visible();
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
                var table = $('#tblstafflist').DataTable({
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
                                    return $('#tblstafflist').DataTable().column(idx).visible();
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
                                                return $('#tblstafflist').DataTable().column(idx).visible();
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
                                                return $('#tblstafflist').DataTable().column(idx).visible();
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


    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tbllast').DataTable({
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
                                return $('#tbllast').DataTable().column(idx).visible();
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
                                            return $('#tbllast').DataTable().column(idx).visible();
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
                                            return $('#tbllast').DataTable().column(idx).visible();
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
                var table = $('#tbllast').DataTable({
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
                                    return $('#tbllast').DataTable().column(idx).visible();
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
                                                return $('#tbllast').DataTable().column(idx).visible();
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
                                                return $('#tbllast').DataTable().column(idx).visible();
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
                    <h3 class="box-title">STAFF MANAGEMENT</h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">INTERNAL</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">EXTERNAL</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">ADD PREFRENCE</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel1"
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
                                <asp:UpdatePanel ID="updatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-4">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <span style="color: red;">*</span>
                                                        <asp:Label ID="lblDYddlCollegeScheme" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlcollege" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlcollege_SelectedIndexChanged"
                                                        AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="ddlcollege" Display="None"
                                                        ErrorMessage="Please Select School/College" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <span style="color: red;">*</span>
                                                        <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDepartment" ToolTip="Please Select Department" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                        ControlToValidate="ddlDepartment" Display="None"
                                                        ErrorMessage="Please Select Department Name" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                        ControlToValidate="ddlDepartment" Display="None"
                                                        ErrorMessage="Please Select Department Name" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <span style="color: red;">*</span>
                                                        <asp:Label ID="lblDYStafflist" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="ddlStaff" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="1"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfStaff" runat="server" ControlToValidate="ddlStaff" Display="None"
                                                        ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                        ControlToValidate="ddlStaff" Display="None"
                                                        ErrorMessage="Please Select Staff" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="BtnAddStaff" runat="server" Text="Add Staff" ValidationGroup="Add"
                                                ToolTip="Select to save" CssClass="btn btn-primary" OnClick="BtnAddStaff_Click" />
                                            <asp:Button ID="btnIClear" runat="server" Text="Clear" TabIndex="14"
                                                CssClass="btn btn-warning" OnClick="btnIClear_Click" />
                                            <asp:ValidationSummary ID="ValidationSummaryShow" runat="server" DisplayMode="List"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="Add" />
                                        </div>
                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <%--<div class="sub-heading">
                                                    <h5>Staff List </h5>
                                                </div>--%>
                                                <asp:ListView ID="lvintrn" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5>Staff List </h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <%--<th>ADD</th>--%>
                                                                        <th>Staff Name</th>
                                                                        <th>Contact No.</th>
                                                                        <th>Department</th>
                                                                        <th>Email</th>
                                                                        <th>I/E </th>
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
                                                            <%--<td>
                                                                <asp:Button ID="BtnAddStaff" runat="server" Text="Add Staff" CssClass="btn btn-primary" ToolTip='<%# Eval("UA_NO")%>' OnClick="BtnAddStaff_Click" /></td>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("STAFF_NAME") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblContact" runat="server" Text='<%# Eval("CONTACTNO") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DEPARTMENT") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAIL_ID") %>' />
                                                                <%--<asp:HiddenField ID="HdnAdd" runat="server" Value='<%# Eval("PADDRESS") %>' />--%>
                                                            </td>
                                                            <td><%# Eval("INTERNAL_EXTERNAL")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatePanel2"
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
                                <asp:UpdatePanel ID="updatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="PnlDetailForStaff" runat="server">
                                            <div class="col-12 mt-4">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblStaffname" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ToolTip="Please enter staff name" ID="txtIName" MaxLength="50" CssClass="form-control" runat="server" TabIndex="1" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIName"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter Staff Name"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblDYMobilenumber" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtMobile" runat="server" ToolTip="Please Enter Mobile Number" CssClass="form-control" TabIndex="1" MaxLength="12" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtMobile">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMobile"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter Mobile Number"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <%--<span style="color: red;">*</span>--%>
                                                            <asp:Label ID="lblDYEmail" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtIEmail" runat="server" CssClass="form-control" ToolTip="Please Enter Email Address" MaxLength="50" TabIndex="1" />
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIEmail"
                                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid EmailId" ValidationGroup="Submit"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <%--  <span style="color: red;">*</span>--%>
                                                            <asp:Label ID="lblDYQualification" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtIQualify" ToolTip="Please Enter Qualification" CssClass="form-control" MaxLength="50" runat="server" TabIndex="1" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijk lmnopqrstuvwxyz."
                                                            TargetControlID="txtIQualify">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <%-- <span style="color: red;">*</span>--%>
                                                            <asp:Label ID="lblDYTeachExp" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtITeach" MaxLength="10" ToolTip="Please Enter Experience" CssClass="form-control"
                                                            runat="server" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijk lmnopqrstuvwxyz1234567890."
                                                            TargetControlID="txtITeach">
                                                        </ajaxToolKit:FilteredTextBoxExtender>

                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <%--<span style="color: red;">*</span>--%>
                                                            <asp:Label ID="lblDYAddress" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtIAdd" TextMode="MultiLine" runat="server" CssClass="form-control" ToolTip="Please Enter Local address"
                                                            MaxLength="500" TabIndex="1" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblNameofInst" runat="server" Font-Bold="true" Text="Name of Institute"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtNameOfInst" runat="server" CssClass="form-control" ToolTip="Please Enter Name of Institute" MaxLength="99" TabIndex="1" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNameOfInst"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter Name of Institute"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                              <span style="color: red;">*</span>
                                                            <asp:Label ID="lblBank" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                         <asp:DropDownList ID="ddlBnkName" ToolTip="Please Select Bank" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlBnkName"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Select Bank Name"
                                                            SetFocusOnError="true" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                        ControlToValidate="ddlBnkName" Display="None"
                                                        ErrorMessage="Please SelectSelect Bank" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="SubmitE"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                             <span style="color: red;">*</span>
                                                            <asp:Label ID="lblAccNo" runat="server" Font-Bold="true" Text="Account Number"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtAccNo" MaxLength="15" ToolTip="Please Enter Account Number" CssClass="form-control"
                                                            runat="server" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom" ValidChars="0123456789"
                                                            TargetControlID="txtAccNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtAccNo"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter Account Number"
                                                            SetFocusOnError="true" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblIFSC" runat="server" Font-Bold="true" Text="IFSC Code"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" ToolTip="Please Enter IFSC Code" MaxLength="99" TabIndex="1" onkeyup="convertToUppercase(this)" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom" ValidChars="0123456789qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtIFSC">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtIFSC"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter IFSC Code"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblPan" runat="server" Font-Bold="true" Text="PAN No."></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtPanNo" runat="server" CssClass="form-control" ToolTip="Please Enter Pan No." MaxLength="99" TabIndex="1" onkeyup="convertToUppercase(this)" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom" ValidChars="0123456789qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM"
                                                            TargetControlID="txtPanNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtPanNo"
                                                            ValidationGroup="SubmitE" Display="None" ErrorMessage="Please Enter Pan No."
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                              <span style="color: red;">*</span>
                                                            <asp:Label ID="lblDYddlDept" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDeptl" ToolTip="Please Select Department" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                        ControlToValidate="ddlDeptl" Display="None"
                                                        ErrorMessage="Please Select Department Name" InitialValue="0"
                                                        SetFocusOnError="True" ValidationGroup="SubmitE"></asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Add Staff" ValidationGroup="SubmitE"
                                                    ToolTip="Select to save" OnClick="btnSubmit_Click" TabIndex="1"
                                                    CssClass="btn btn-primary" />
                                                <asp:Button ID="btnEClear" runat="server" Text="Clear" TabIndex="1"
                                                    CssClass="btn btn-warning" OnClick="btnEClear_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ToolTip="Select to cancel" ShowSummary="false" ValidationGroup="SubmitE" />
                                            </div>
                                        </asp:Panel>
                                        <div class="col-12 mt-3">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvElist" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5>Staff List </h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblstafflist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <%--<th>ADD</th>--%>
                                                                        <th>Staff Name</th>
                                                                        <th>Contact No.</th>
                                                                        <th>Department</th>
                                                                        <th>I/E </th>
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
                                                            <td><%# Eval("STAFF_NAME")%></td>
                                                            <td><%# Eval("CONTACTNO")%></td>
                                                            <td><%# Eval("DEPARTMENT")%></td>
                                                            <td><%# Eval("INTERNAL_EXTERNAL")%></td>
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
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                <asp:UpdatePanel ID="updatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div id="div_preference" runat="server" visible="true">
                                            <div class="col-12 mt-4">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Name</label>
                                                        </div>
                                                        <asp:TextBox ToolTip="Please enter staff name" ID="txtStaff" MaxLength="50" CssClass="form-control" runat="server" TabIndex="1" />
                                                        <asp:RequiredFieldValidator ID="valStaff" runat="server" ControlToValidate="txtStaff"
                                                            ValidationGroup="AddPS" Display="None" ErrorMessage="Please Enter Staff Name"
                                                            SetFocusOnError="true" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactNo" runat="server" ToolTip="Please Enter Contact no." CssClass="form-control" TabIndex="1" MaxLength="12" />
                                                        <%-- <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" ControlToValidate="txtContactNo"
                                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Contact Nos."
                                                SetFocusOnError="true" />--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtContactNo" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtContactNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ToolTip="Please Enter Email Address" MaxLength="50" TabIndex="1" />
                                                        <asp:RegularExpressionValidator ID="rfvLocalEmail" runat="server" ControlToValidate="txtEmail"
                                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid EmailId" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Qualification</label>
                                                        </div>
                                                        <asp:TextBox ID="txtQualification" ToolTip="Please Enter Qualification" CssClass="form-control" MaxLength="50" runat="server" TabIndex="1" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtQualification" runat="server" FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijk lmnopqrstuvwxyz."
                                                            TargetControlID="txtQualification">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Teaching Experience</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTeachExp" MaxLength="10" ToolTip="Please Enter Experience" CssClass="form-control"
                                                            runat="server" TabIndex="1"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890."
                                                            TargetControlID="txtContactNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" CssClass="form-control" ToolTip="Please Enter Local address"
                                                            MaxLength="500" TabIndex="1" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Preference for Paper Setting</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                                            ValidationGroup="offered" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true" TabIndex="1">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlClgname"
                                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="AddPS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <span style="color: red;">*</span>
                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                                            TabIndex="1" ToolTip="Please Select Department">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="AddPS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label><span style="color: red;">*</span> BOS Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDept" TabIndex="1" ToolTip="Please Select Department" CssClass="form-control" data-select2-enable="true"
                                                            runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlDeptPS_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlDept"
                                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <%-- ValidationGroup="AddPS"--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label><span style="color: red;">*</span> Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true"
                                                            AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1"
                                                            ToolTip="Please Select Semester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlSemester"
                                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="AddPS"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label><span style="color: red;">*</span> Course</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCourse" TabIndex="1" ToolTip="Please Select Course" CssClass="form-control" data-select2-enable="true"
                                                            runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCoursePS" runat="server" ControlToValidate="ddlCourse"
                                                            Display="None" ErrorMessage="Please Select Course" InitialValue="0" SetFocusOnError="True"
                                                            ValidationGroup="AddPS"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12 mt-3">
                                                <%-- <asp:Panel ID="pnlPSCourse" runat="server">--%>
                                                <asp:ListView ID="lvPSCourse" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Course Name
                                                                        </th>
                                                                        <th>Semster
                                                                        </th>
                                                                        <th>Degree
                                                                        </th>
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
                                                                <asp:ImageButton ID="btnDeletePS" TabIndex="12" runat="server" CausesValidation="false" ImageUrl="~/images/delete.png"
                                                                    CommandArgument='<%# Eval("CCODE") %>' AlternateText='<%# Eval("CCODE") %>' ToolTip="Select to Delete Record"
                                                                    OnClick="btnDeletePS_Click" />
                                                            </td>
                                                            <td>
                                                                <span>
                                                                    <%# Eval("COURSE_NAME")%></span>
                                                                <asp:HiddenField ID="hfCcode" runat="server" Value='<%# Eval("CCODE") %>' />
                                                            </td>
                                                            <td>
                                                                <span>
                                                                    <%# Eval("SEMESTERNAME")%></span>

                                                            </td>
                                                            <td>
                                                                <span>
                                                                    <%# Eval("DEGREENAME")%></span>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%-- </asp:Panel>--%>
                                            </div>


                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAddPS" TabIndex="1" runat="server" Text="Add Preference" ValidationGroup="AddPS"
                                                    ToolTip="Select to allot course" OnClick="btnAddPS_Click" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnPsCancel" Font-Bold="true" runat="server" Text="Cancel" TabIndex="1"
                                                    CssClass="btn btn-warning" OnClick="btnPsCancel_Click" ValidationGroup="Cancel" />
                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ToolTip="Select to cancel" ShowSummary="false" ValidationGroup="Submit" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddPS" />
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ToolTip="Select to cancel" ShowSummary="false" ValidationGroup="Cancel" />

                                            </div>

                                        </div>
                                        <div class="col-12 mt-3">
                                            <%--<div class="table-responsive">--%>
                                            <%--<table class="table table-striped table-bordered nowrap" style="width: 100%" id="tbllast">
                                                    <asp:Repeater ID="lvlinks" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Staff List </h5>
                                                            </div>
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>
                                                                    <th>Edit </th>
                                                                    <th>Delete </th>
                                                                    <th>Staff Name </th>
                                                                    <th>Contact No. </th>
                                                                    <th>Department </th>
                                                                    <th>I/E </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("STAFFNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="14" ToolTip="Select to Edit" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" CausesValidation="false" CommandArgument='<%# Eval("STAFFNO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" OnClientClick="return showConfirm();" TabIndex="14" ToolTip="Delete Record" />
                                                                </td>
                                                                <td><%# Eval("STAFF_NAME")%></td>
                                                                <td><%# Eval("CONTACTNO")%></td>
                                                                <td><%# Eval("DEPARTMENT")%></td>
                                                                <td><%# Eval("INTERNAL_EXTERNAL")%></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                              
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>--%>
                                            <%--</asp:Panel>--%>
                                            <asp:Panel ID="pnl3" runat="server">
                                                <asp:ListView ID="lvlinks" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h5>Staff List </h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tbllast">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <%--<th>Action</th>--%>
                                                                        <th>Action</th>
                                                                        <th>Staff Name</th>
                                                                        <th>Contact No.</th>
                                                                        <th>Department</th>
                                                                        <th>I/E</th>
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
                                                            <%--<td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("STAFFNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" TabIndex="14" ToolTip="Select to Edit" />
                                                            </td>--%>
                                                            <td>
                                                                <asp:ImageButton ID="btnDelete" runat="server" AlternateText="Delete Record" CausesValidation="false" CommandArgument='<%# Eval("STAFFNO") %>' ImageUrl="~/Images/delete.png" OnClick="btnDelete_Click" OnClientClick="return showConfirm();" TabIndex="14" ToolTip="Delete Record" />
                                                            </td>
                                                            <%--<td><%# Eval("STAFF_NAME")%></td>--%>
                                                            <td><asp:LinkButton ID="lnkStaffName" runat="server" Text='<%# Eval("STAFF_NAME") %>' CausesValidation="false"  CommandArgument='<%# Eval("STAFFNO") %>' OnClick="lnkStaffName_Click"
                                                    ></asp:LinkButton></td>
                                                            <td><%# Eval("CONTACTNO")%></td>
                                                            <td><%# Eval("DEPARTMENT")%></td>
                                                            <td><%# Eval("INTERNAL_EXTERNAL")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--</div>--%>
                                            </asp:Panel>
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

    <div class="col-12 d-none">
        <div class="row">
            <div class="form-group col-lg-6 col-md-12 col-12">
                <div class=" note-div">
                    <h5 class="heading">Note </h5>
                    <p>
                        <asp:Label ID="lblUserMsg" runat="server"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="form-group col-lg-2 col-md-6 col-12">
                <div class="label-dynamic">
                    <sup></sup>
                    <label></label>
                </div>
                <asp:RadioButton ID="rbInternal" runat="server" Text="Internal" GroupName="IntExt"
                    TabIndex="1" Checked="false" OnCheckedChanged="rbInternal_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbExternal" runat="server" Checked="true" Text="External"
                                                GroupName="IntExt" TabIndex="7" />
            </div>
            <div class="form-group col-lg-4 col-md-6 col-12">
                <asp:Panel ID="PnlForEnterCode" runat="server">
                    <div class="row">
                        <div class=" col-md-10">
                            <div class="label-dynamic">
                                <sup></sup>
                                <label></label>
                            </div>
                            <asp:TextBox ID="TextBox1" TextMode="SingleLine" runat="server" ToolTip="Please Enter Code" CssClass="form-control" placeholder="Enter Employee Code"
                                TabIndex="1" Width="100%" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Employee Code!" SetFocusOnError="true" ControlToValidate="TextBox1" ValidationGroup="Go" Display="None"></asp:RequiredFieldValidator>
                        </div>
                        <div class=" col-md-4">
                            <div class="label-dynamic">
                                <sup></sup>
                                <label></label>
                            </div>
                            <asp:Button ID="Button1" Font-Bold="true" runat="server" Text="GO" TabIndex="1"
                                CssClass="btn btn-primary" OnClick="Button1_Click" Width="50px" ValidationGroup="Go" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Go" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" />
                        </div>

                    </div>

                </asp:Panel>
            </div>
        </div>
    </div>

    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <%--  Enable the button so it can be played again --%>    <%# Eval("COURSE_NAME")%>
    <%--    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel"
        runat="server" TargetControlID="div" PopupControlID="div" OkControlID="btnOkDel"
        OnOkScript="okDelClick();" CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();"
        BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <img align="middle" src="../../Images/warning.png" alt="" />
                    </td>
                    <td>&nbsp;&nbsp;Are you sure you want to cancel this staff?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>--%>

    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
    <script type="text/javascript">
        function showConfirm() {
            var ret = confirm('Are You Sure You Want To Delete ? After Done You Never Make Any Changes');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }

        function convertToUppercase(element) {
            element.value = element.value.toUpperCase();
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
</asp:Content>

