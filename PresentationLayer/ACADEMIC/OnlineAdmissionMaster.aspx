<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineAdmissionMaster.aspx.cs" Inherits="ACADEMIC_OnlineAdmissionMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <style>
        .profile {
            display: none;
        }

        .fa-plus {
            background: #1acc1a;
            color: #fff;
            font-size: 15px;
            padding: 5px;
            border-radius: 2px;
            margin-right: 2px;
        }

        .nav-tabs-custom .nav-link {
            padding: 0.3rem 0.5rem;
        }

        /*#ctl00_ContentPlaceHolder1_pnlSubjectType .dataTables_scrollHeadInner {
            width: max-content !important;
        }*/
    </style>

    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 80px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            padding-left: 8px; /*new by 008*/
            padding-top: 3px; /*new by 008*/
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <%--===== Data Table Script added by gaurav =====--%>

    <script>
        // added by kajal jaiswal on 20-02-2024
        $(document).ready(function () {
           
            var ActiveTab = $('#ctl00_ContentPlaceHolder1_hdftab').val();
         
            $(".tab - pane").removeClass("active");
            $(".tab - pane").removeClass("fade");
            $(".tab - pane").addClass("fade");
            $("." + ActiveTab).addClass("active");
            $("." + ActiveTab).removeClass("fade");
        });
    </script>

    <script>
        $(document).ready(function () {
             var table = $('#divBoardGradelist').DataTable({
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
                                return $('#divBoardGradelist').DataTable().column(idx).visible();
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
                                            return $('#divBoardGradelist').DataTable().column(idx).visible();
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
                                            return $('#divBoardGradelist').DataTable().column(idx).visible();
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
                var table = $('#divBoardGradelist').DataTable({
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
                                    return $('#divBoardGradelist').DataTable().column(idx).visible();
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
                                                return $('#divBoardGradelist').DataTable().column(idx).visible();
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
                                                return $('#divBoardGradelist').DataTable().column(idx).visible();
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

     
    <script>
        $(document).ready(function () {
            var table = $('#divAddSubjectListlist').DataTable({
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
                                return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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
                                            return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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
                                            return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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
                var table = $('#divAddSubjectListlist').DataTable({
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
                                    return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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
                                                return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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
                                                return $('#divAddSubjectListlist').DataTable().column(idx).visible();
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

    <script lang="javascript" type="text/javascript">
        function allowAlphaNumericSpace(e) {
                var code = ('charCode' in e) ? e.charCode : e.keyCode;
                if (!(code == 32) && // space
                  !(code == 45) && // Dash
                  !(code > 47 && code < 58) && // numeric (0-9)
                  !(code > 64 && code < 91) && // upper alpha (A-Z)
                  !(code > 96 && code < 123)) { // lower alpha (a-z)
                    e.preventDefault();
                }
        }
    </script>
    
    

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                     <asp:HiddenField runat="server" id="hdftab" />
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">MASTERS DETAILS</h3>
                        </div>

                        <div id="Tabs" role="tabpanel">
                            <div id="divqualification">                               
                                <div class="col-12">
                                   
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="nav-item" id="tab1" runat="server" visible="false">
                                                <a class="nav-link tab1" data-toggle="tab" href="#tab_1">Board</a>
                                            </li>
                                            <li class="nav-item" id="tab2" runat="server" visible="false">
                                                <a class="nav-link tab2" data-toggle="tab" href="#tab_2">Subject</a>
                                            </li>
                                            <li class="nav-item" id="tab3" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_3">Group</a>
                                            </li>
                                            <li class="nav-item" id="tab4" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_4">Subject Type</a>
                                            </li>
                                            <li class="nav-item" id="tab5" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_5">Board Subject Configuration</a>
                                            </li>
                                            <li class="nav-item" id="tab6" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_6">Add Subject</a>
                                            </li>
                                            <li class="nav-item" id="tab7" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_7">Board Grade Scheme</a>
                                            </li>
                                            <li class="nav-item" id="tab8" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_8">Reservation Configuration</a>
                                            </li>
                                            <li class="nav-item" id="tab9" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_9">Qualifying Degree</a>
                                            </li>
                                            <li class="nav-item" id="tab10" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_10">Program</a>
                                            </li>
                                            <li class="nav-item" id="tab11" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_11">Test Score</a>
                                            </li>
                                            <li class="nav-item" id="tab12" runat="server" visible="false">
                                                <a class="nav-link" data-toggle="tab" href="#tab_12">Gate - Non Gate</a>
                                            </li>
                                        </ul>

                                        <div class="tab-content" id="my-tab-content">

                                            <%-- TAB:EXAMBOARD --%>
                                            <div class="tab-pane tab1" id="tab_1">
                                                <div class="mt-3">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>Country</label>
                                                            <asp:DropDownList runat="server" ID="ddlCountry" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvddlCountry" runat="server" ControlToValidate="ddlCountry"
                                                                Display="None" ValidationGroup="examboard" InitialValue="Please Select"
                                                                ErrorMessage="Please Select Country"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><%--<sup>*</sup>--%> State</label>
                                                            <asp:DropDownList runat="server" ID="ddlState" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlState" runat="server" ControlToValidate="ddlState"
                                                                    Display="None" ValidationGroup="examboard" InitialValue="Please Select"
                                                                    ErrorMessage="Please Select State"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Board Name</label>
                                                            <asp:TextBox ID="txtBoardName" runat="server" TabIndex="3" CssClass="form-control" MaxLength="200" onkeydown="return((event.keyCode >= 64 && event.keyCode <= 91) || (event.keyCode==32)|| (event.keyCode==8)|| (event.keyCode==9));"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtBoardName" runat="server" ControlToValidate="txtBoardName"
                                                                Display="None" ErrorMessage="Please Enter Board Name" ValidationGroup="examboard"
                                                                SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Qualification Level</label>
                                                            <asp:ListBox ID="lstbxQualificationLevel" runat="server" CssClass="form-control multi-select-demo"
                                                                AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7">
                                                                <asp:ListItem Value="1"> 10th</asp:ListItem>
                                                                <asp:ListItem Value="2"> 12th </asp:ListItem>
                                                                <asp:ListItem Value="3"> Graduation</asp:ListItem>
                                                                <asp:ListItem Value="4"> Post Graduation</asp:ListItem>
                                                            </asp:ListBox>
                                                            <asp:RequiredFieldValidator ID="rfvlstbxQualificationLevel" runat="server" ControlToValidate="lstbxQualificationLevel"
                                                                Display="None" ErrorMessage="Please Select Qualification Level" ValidationGroup="examboard"
                                                                SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitBoard" runat="server" Text="Submit" TabIndex="4" ValidationGroup="examboard" OnClick="btnSubmitBoard_Click" CssClass="btn btn-outline-info" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="examboard"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelBoard" runat="server" TabIndex="5" Text="Cancel" ToolTip="Click To Clear" OnClick="btnCancelBoard_Click" CssClass="btn btn-outline-danger" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlExamBoard" runat="server" Visible="false">
                                                        <asp:ListView ID="lvExamBoard" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divExamBoardlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Country</th>
                                                                            <th>State</th>
                                                                            <th>Board Name </th>
                                                                            <th>Qualification Level</th>
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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("BOARDNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditExamBoard_Click" TabIndex="6" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("COUNTRYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BOARDNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("QUALIFICATIONLEVEL") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <%-- TAB:SUBJECT --%>
                                            <div class="tab-pane tab2" id="tab_2">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Subject</label>
                                                            <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubject" runat="server" ControlToValidate="txtSubject" Display="None" ErrorMessage="Please Enter Subject Name" ValidationGroup="academic" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitSub" runat="server" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="academic" OnClick="btnSubmitSub_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="academic"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelSub" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelSub_Click" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlSubject" runat="server" Visible="false">
                                                        <asp:ListView ID="lvSubject" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divSubjectlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Subject Name</th>

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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("SUBJECTNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditSubject_Click" TabIndex="7" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SUBJECTNAME")%>
                                                                    </td>


                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <%-- TAB:GROUP --%>
                                            <div class="tab-pane tab3" id="tab_3">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Group</label>
                                                            <asp:TextBox ID="txtGroup" runat="server" MaxLength="100" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtGroup" runat="server" ControlToValidate="txtGroup" Display="None" ErrorMessage="Please Enter Group Name" ValidationGroup="Group" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitGroup" runat="server" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="Group" OnClick="btnSubmitGroup_Click" />
                                                    <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="Group"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelGroup" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelGroup_Click" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="PnlGroup" runat="server" Visible="false">
                                                        <asp:ListView ID="lvGroup" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divGrouplist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Group Name</th>

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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("GROUPID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditGroup_Click" TabIndex="7" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("GROUPNAME")%>
                                                                    </td>


                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <%-- TAB:SUBJECT TYPE --%>
                                            <div class="tab-pane tab4" id="tab_4">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Subject Type</label>
                                                            <asp:TextBox ID="txtSubjectType" runat="server" MaxLength="200" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rvftxtSubjectType" runat="server" ControlToValidate="txtSubjectType" Display="None" ErrorMessage="Please Enter Subject Type." ValidationGroup="SubjectType" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSubjectType" ValidationExpression="^[a-zA-Z0-9.@]{0,25}$" ValidationGroup="SubjectType" ErrorMessage="Allowed Only Alphanumeric"></asp:RegularExpressionValidator>--%>
                                                            <!-- Paper-I, Paper-II, Paper-III, Subject-I, Subject-II, Subject-III  -->
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Subject </label>
                                                            <asp:ListBox runat="server" ID="lstSubjectName" SelectionMode="Multiple" CssClass="form-control multi-select-demo "></asp:ListBox><%--CssClass="form-control multi-select-demo "--%>
                                                            <%--OnSelectedIndexChanged="lstSubjectName_SelectedIndexChanged"--%>
                                                            <asp:RequiredFieldValidator ID="rvflstSubjectName" runat="server" ControlToValidate="lstSubjectName"
                                                                Display="None" ValidationGroup="SubjectType" InitialValue=""
                                                                ErrorMessage="Please Select Subjects."></asp:RequiredFieldValidator>


                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnSubmitSubType" runat="server" Text="Submit" ValidationGroup="SubjectType" OnClick="btnSubmitSubjectType_Click" CssClass="btn btn-outline-info" />
                                                            <%--OnClick="btnSubmitSubjectType_Click"--%>
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="SubjectType"
                                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                            <asp:Button ID="btnCancelSubType" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelSubjectType_Click" />
                                                            <%--OnClick="btnCancelSubjectType_Click"--%>
                                                        </div>

                                                        <%--<div class="col-12 mt-3">
                                                    <div class="table-responsive">
                                                        <table class="table table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th>Edit/Delete</th>
                                                                    <th>Subject Type</th>
                                                                    <th>Subject Name</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>
                                                                        <img src="IMAGES/edit.png" alt="edit" />
                                                                        <img src="IMAGES/delete.png" alt="delete" />
                                                                    </td>
                                                                    <td>Paper-I</td>
                                                                    <td>Language Paper Name</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <img src="IMAGES/edit.png" alt="edit" />
                                                                        <img src="IMAGES/delete.png" alt="delete" />
                                                                    </td>
                                                                    <td>Subject-I</td>
                                                                    <td>All Subject Name</td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>

                                            </div>--%>

                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlSubjectType" runat="server" Visible="false">
                                                                <asp:ListView ID="lvSubjectType" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divSubjectType">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>Subject Type</th>
                                                                                    <th>Subject Name</th>

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
                                                                                <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                    CommandArgument='<%# Eval("SUBJECTTYPENO")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubjectType_Click"
                                                                                    TabIndex="6" />
                                                                                <%--OnClick="btnEditSubjectType_Click"--%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SUBJECTTYPE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("SUBJECTNAME")%>
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <%-- TAB:BOARD SUBJECT CONFIGURATION --%>
                                            <div class="tab-pane tab5" id="tab_5">
                                                <div class="mt-3">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Board</label>
                                                            <asp:DropDownList runat="server" ID="ddlBoardSubjConfig" TabIndex="3" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBoardSubjConfig_SelectedIndexChanged" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlddlBoardSubjConfig" runat="server" ControlToValidate="ddlBoardSubjConfig"
                                                                Display="None" ValidationGroup="BoardSubConfig" InitialValue="Please Select" ErrorMessage="Please Select Board"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBoardSubjConfig"
                                                                Display="None" ValidationGroup="BoardSubConfig" InitialValue="0" ErrorMessage="Please Select Board"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col col-lg-6 col-md-6 col-12">
                                                        <div class="row mt-4" id="Div3" runat="server" visible="true">

                                                            <div class="form-group col-12 text-right">
                                                                <asp:Button ID="btnAddDetail" runat="server" CssClass="btn btn-outline-info" OnClick="btnAddDetail_Click" ValidationGroup="BoardSubConfig" Text="Add Details" /><%--OnClick="btnAddDetail_Click"--%>
                                                                <%--  <asp:ValidationSummary ID="ValidationSummary10" runat="server" ValidationGroup="BoardSubConfig"
                                                                    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />--%>
                                                            </div>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="BoardConfigTable">
                                                            <tr>
                                                                <td>
                                                                    <div class="col-12 pl-0 pr-0">
                                                                        <asp:Panel ID="pnlBoardSubType" runat="server">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:ListView ID="lvBoardSubType" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <div class="sub-heading">
                                                                                                <h5>Enter SubjectType Subject Count Details</h5>
                                                                                            </div>
                                                                                            <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                                                <thead class="bg-light-blue">
                                                                                                    <tr>
                                                                                                        <th style="text-align: center">SrNo
                                                                                                        </th>
                                                                                                        <th style="text-align: center">SubjectType
                                                                                                        </th>
                                                                                                        <th style="text-align: center">Subject Count
                                                                                                        </th>
                                                                                                        <th style="text-align: center"></th>
                                                                                                        <%--<th style="text-align: center">Max. Grade Point
                                                                                                    </th>--%>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <tr>
                                                                                                <td style="text-align: center">
                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                    <asp:HiddenField ID="hfdBoConfSubType" runat="server" Value='<%# Eval("ID") %>' />
                                                                                                </td>
                                                                                                <td>

                                                                                                    <asp:DropDownList ID="ddlBoConfSubType" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="rfvddlBoConfSubType" runat="server" ControlToValidate="ddlBoConfSubType"
                                                                                                        Display="None" ValidationGroup="BoardSubConfig" InitialValue="Please Select" ErrorMessage="Please Select SubjectType"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtSubjectCount" runat="server" CssClass="form-control" onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" Text='<%# Eval("NO_OF_SUBJECTS") %>' onkeypress="return functionx(event);if(this.value.length==3) return false;" ToolTip="Please Enter Subject Count" type="number" placeholder="Subject Count"></asp:TextBox>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="linkbtnBoSubConfigDelete" runat="server" class="fa fa-close" ForeColor="Red" OnClick="btnBoSubConfigDeleteDetail_Click" CausesValidation="false" CommandArgument='<%#Eval("ID")%>' />
                                                                                                </td>

                                                                                            </tr>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnAddDetail" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitSubConfig" runat="server" Text="Submit" TabIndex="4" OnClick="btnSubmitSubConfig_Click" ValidationGroup="BoardSubConfig" CssClass="btn btn-outline-info" />
                                                    <%--OnClick="btnSubmitSubConfig_Click"--%>
                                                    <asp:ValidationSummary ID="ValidationSummary9" runat="server" ValidationGroup="BoardSubConfig"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelSubConfig" runat="server" Text="Cancel" TabIndex="5" OnClick="btnCancelSubConfig_Click" CssClass="btn btn-outline-danger" />
                                                    <%--OnClick="btnCancelSubConfig_Click"--%>
                                                </div>

                                            </div>

                                            <%-- TAB:ADD SUBJECT --%>
                                            <div class="tab-pane tab6" id="tab_6">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>Country</label>
                                                            <asp:DropDownList runat="server" ID="ddlCountrySub" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlCountrySub_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="rfvddlCountrySub" runat="server" ControlToValidate="ddlCountrySub"
                                                                Display="None" ValidationGroup="addsubject" InitialValue="0"
                                                                ErrorMessage="Please Select Country"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>State</label>
                                                            <asp:DropDownList runat="server" ID="ddlStateSub" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlStateSub_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlStateSub" runat="server" ControlToValidate="ddlStateSub"
                                                                Display="None" ValidationGroup="addsubject" InitialValue="Please Select"
                                                                ErrorMessage="Please Select State"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Qualification Level</label>
                                                            <asp:DropDownList runat="server" ID="ddlQualification" TabIndex="4" CssClass="form-control" OnSelectedIndexChanged="ddlQualification_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0"> Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1"> 10th</asp:ListItem>
                                                                <asp:ListItem Value="2"> 12th </asp:ListItem>
                                                                <asp:ListItem Value="3"> Graduation</asp:ListItem>
                                                                <asp:ListItem Value="4"> Post Graduation</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlQualification" runat="server" ControlToValidate="ddlQualification"
                                                                Display="None" ValidationGroup="addsubject" InitialValue="0"
                                                                ErrorMessage="Please Select Qualification Level"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Board</label>
                                                            <asp:DropDownList runat="server" ID="ddlBoard" TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlBoard_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlBoard" runat="server" ControlToValidate="ddlBoard"
                                                                Display="None" ValidationGroup="addsubject" InitialValue="0"
                                                                ErrorMessage="Please Select Board"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Subject Type</label>
                                                            <asp:DropDownList runat="server" ID="ddlSubType" TabIndex="5" CssClass="form-control" OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlSubType" runat="server" ControlToValidate="ddlSubType"
                                                                Display="None" ValidationGroup="addsubject" InitialValue="0"
                                                                ErrorMessage="Please Select Subject Type"></asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlAddSubjectMaxMarks" runat="server" Visible="false">
                                                        <asp:ListView ID="lvAddSubjectMaxMarks" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="table-responsive" style="height: 200px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divAddSubjectMaxMarkslist">
                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff!important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                            <tr>
                                                                                <th>SrNo</th>
                                                                                <th>IsActive</th>
                                                                                <th>Subject</th>
                                                                                <th>Max. Marks</th>
                                                                                <th>Group</th>
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
                                                                    <td style="text-align: center">
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                        <asp:HiddenField ID="hfdMaxMarksSubNo" runat="server" Value='<%# Eval("SUBJECTNO") %>' />
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox ID="chkIsActive" runat="server" CssClass="chkbox_addsubject" onchange="checkedActive_Change(this);" Checked='<%# Eval("ISSUBACTIVE") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%--<%# Eval("SUBJECTNAME")%>--%>
                                                                        <asp:Label ID="lblMaxMarksSubName" runat="server" Text='<%# Eval("SUBJECTNAME") %>' />
                                                                        <%--<asp:TextBox ID="txtMaxMarksSubName" runat="server" CssClass="form-control" MaxLength="15" min="0" Text='<%# Eval("SUBJECTNAME")%>' ReadOnly="true"></asp:TextBox>--%>

                                                                    </td>
                                                                    <td>
                                                                        <%--<asp:TextBox ID="txtSubjectMaxMark" runat="server" />--%>
                                                                        <asp:TextBox ID="txtMaxMarks" runat="server" onKeyUp="setMaxLength(this)" isMaxLength="3" CssClass="form-control" Width="200px" MaxLength="3" min="0" Text='<%# Eval("MAXMARKS") %>' onkeypress="return functionx(event)" ToolTip="Please Enter  Max Marks" type="number" placeholder="Max Marks"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ListBox runat="server" ID="lstGroup" SelectionMode="Multiple" CssClass="form-control multi-select-demo "></asp:ListBox>
                                                                    </td>
                                                                    <%--<td>
                                                                            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitAddSub" runat="server" Text="Submit" TabIndex="4" ValidationGroup="addsubject" OnClick="btnSubmitAddSub_Click" CssClass="btn btn-outline-info" />
                                                    <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="addsubject"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelAddSub" runat="server" Text="Cancel" TabIndex="5" OnClick="btnCancelAddSub_Click" CssClass="btn btn-outline-danger" />

                                                </div>


                                                <div class="col-12">
                                                    <asp:Panel ID="pnlAddSubjectList" runat="server" Visible="false">
                                                        <asp:ListView ID="lvAddSubjectList" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divAddSubjectListlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit</th>
                                                                            <th>Country</th>
                                                                            <th>State</th>
                                                                            <th>Qualification Level </th>
                                                                            <th>Board Name </th>
                                                                            <th>Subject Type</th>
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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("BOARDNO")+","+Eval("QUALIFICATION_LEVEL")+","+Eval("SUBJECTTYPENO")+","+Eval("COUNTRYNO")+","+Eval("STATENO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditAddSub_Click" TabIndex="6" />
                                                                        <%-- CommandArgument='<%# Eval("BOARDNO")+","+Eval("QUALIFICATION_LEVEL")+","+Eval("SUBJECTTYPENO")%>' AlternateText="Edit Record" ToolTip="Edit Record"--%>
                                                                        <%--<asp:HiddenField ID="hfdMaxMarksBoardNo" runat="server" Value='<%# Eval("BOARDNO") %>' />
                                                                        <asp:HiddenField ID="hfdMaxMarksQualificationLevel" runat="server" Value='<%# Eval("QUALIFICATION_LEVEL") %>' />
                                                                        <asp:HiddenField ID="hfdMaxMarksSubjectTypeNo" runat="server" Value='<%# Eval("SUBJECTTYPENO") %>' /> --%>                                                                      
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("COUNTRYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("QUALIFICATION_LEVEL_TEXT") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BOARDNAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SUBJECTTYPE") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <%-- TAB:BOARD GRADE --%>
                                            <div class="tab-pane tab7" id="tab_7">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>Country</label>

                                                            <asp:DropDownList runat="server" ID="ddlCountryGrade" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlCountryGrade_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--                                                            <asp:RequiredFieldValidator ID="rfvddlCountryGrade" runat="server" ControlToValidate="ddlCountryGrade"
                                                                Display="None" ValidationGroup="addgrade" InitialValue="0"
                                                                ErrorMessage="Please Select Country"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>State</label>
                                                            <asp:DropDownList runat="server" ID="ddlStateGrade" TabIndex="2" CssClass="form-control" OnSelectedIndexChanged="ddlStateGrade_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvddlStateGrade" runat="server" ControlToValidate="ddlStateGrade"
                                                                Display="None" ValidationGroup="addgrade" InitialValue="Please Select"
                                                                ErrorMessage="Please Select State"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Qualification Level</label>
                                                            <asp:DropDownList runat="server" ID="ddlQualificationGrade" TabIndex="4" CssClass="form-control" OnSelectedIndexChanged="ddlQualificationGrade_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0"> Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1"> 10th</asp:ListItem>
                                                                <asp:ListItem Value="2"> 12th </asp:ListItem>
                                                                <asp:ListItem Value="3"> Graduation</asp:ListItem>
                                                                <asp:ListItem Value="4"> Post Graduation</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlQualificationGrade" runat="server" ControlToValidate="ddlQualificationGrade"
                                                                Display="None" ValidationGroup="addgrade" InitialValue="0"
                                                                ErrorMessage="Please Select Qualification Level"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Board</label>
                                                            <asp:DropDownList runat="server" ID="ddlBoardGrade" TabIndex="3" CssClass="form-control" OnSelectedIndexChanged="ddlBoardGrade_SelectedIndexChanged" AutoPostBack="true" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlBoardGrade" runat="server" ControlToValidate="ddlBoardGrade"
                                                                Display="None" ValidationGroup="addgrade" InitialValue="0"
                                                                ErrorMessage="Please Select Board"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Max. Grade Point</label>
                                                            <asp:TextBox ID="txtMaxGradePoint" runat="server" CssClass="form-control" MaxLength="4" min="0" onkeypress="return functionx(event)" ToolTip="Please Enter Max Grade Point" type="number" placeholder="Max Grade Point"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtMaxGradePoint" runat="server" ControlToValidate="txtMaxGradePoint" Display="None" ErrorMessage="Please Enter Max Grade Point" ValidationGroup="addgrade" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>

                                                    <%-- Add Grade Scheme List  --%>
                                                    <div class="col col-lg-6 col-md-6 col-12">
                                                        <div class="row mt-4" id="DivAdd" runat="server" visible="true">

                                                            <div class="form-group col-12 text-right">
                                                                <asp:Button ID="btnAddGradeSlab" runat="server" CssClass="btn btn-outline-info" Text="Add Grade Details" OnClick="btnAddGradeSlab_Click" /><%--OnClick="btnAddGradeSlab_Click"--%>
                                                            </div>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="gradingTable">
                                                            <tr>
                                                                <td>
                                                                    <div class="col-12 pl-0 pr-0">
                                                                        <asp:Panel ID="pnlGradeSchemeList" runat="server">
                                                                            <asp:ListView ID="lvGradeSchemeList" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Enter Grade Scheme Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th style="text-align: center">SrNo
                                                                                                </th>
                                                                                                <th style="text-align: center">Grade Name
                                                                                                </th>
                                                                                                <th style="text-align: center">Grade Point
                                                                                                </th>
                                                                                                <%--<th style="text-align: center">Max. Grade Point
                                                                                                    </th>--%>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <%# Container.DataItemIndex + 1 %>
                                                                                            <asp:HiddenField ID="hfdvalue" runat="server" Value='<%# Eval("ID") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtGradeName" runat="server" CssClass="form-control" MaxLength="5" Text='<%# Eval("GRADENAME") %>' ToolTip="Please Enter Grade Name" placeholder="Grade Name"></asp:TextBox>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtGradePoint" runat="server" CssClass="form-control" onKeyUp="setMaxLength(this)" isMaxLength="3" MaxLength="3" min="0" Text='<%# Eval("GRADEPOINT") %>' onkeypress="return functionx(event)" ToolTip="Please Enter Grade Point" type="number" placeholder="Grade Point"></asp:TextBox>
                                                                                        </td>
                                                                                        <%--<td>
                                                                                                <asp:TextBox ID="txtMaxGradePoint" runat="server" CssClass="form-control" MaxLength="15" Text='<%# Eval("Column3") %>' onkeypress="return functionx(event)" ToolTip="Please Enter Max Grade Point" type="number" placeholder="Max Grade Point"></asp:TextBox>
                                                                                            </td> --%>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>



                                                    <%--<div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Grade Name</label>
                                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Grade Point</label>
                                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Max. Grade Point</label>
                                                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label></label>
                                                                <i class="fa fa-plus" aria-hidden="true"></i>
                                                            </div>
                                                        </div>--%>

                                                    <%--<div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Grade Name</label>
                                                                <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Grade Point</label>
                                                                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label><sup>*</sup> Max. Grade Point</label>
                                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <label></label>
                                                                <i class="fa fa-plus" aria-hidden="true"></i>
                                                            </div>
                                                        </div>--%>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitGrade" runat="server" Text="Submit" TabIndex="4" ValidationGroup="addgrade" OnClick="btnSubmitGrade_Click" CssClass="btn btn-outline-info" />
                                                    <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="addgrade"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelGrade" runat="server" Text="Cancel" TabIndex="5" OnClick="btnCancelGrade_Click" CssClass="btn btn-outline-danger" />
                                                </div>

                                                <%--<div class="col-12 mt-3">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Edit/Delete</th>
                                                                        <th>Country</th>
                                                                        <th>State</th>
                                                                        <th>Qualification Level </th>
                                                                        <th>Board Name </th>
                                                                        <th>Grade Name</th>
                                                                        <th>Grade Point</th>
                                                                        <th>Max. Grade Point</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img src="IMAGES/edit.png" alt="edit" />
                                                                            <img src="IMAGES/delete.png" alt="delete" />
                                                                        </td>
                                                                        <td>India</td>
                                                                        <td>Maharashtra</td>
                                                                        <td>10th</td>
                                                                        <td>Maharashtra State Board </td>
                                                                        <td>A</td>
                                                                        <td>9</td>
                                                                        <td>10</td>
                                                                    </tr>

                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>--%>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlBoardGradeList" runat="server" Visible="false">
                                                        <asp:ListView ID="lvBoardGradeList" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divBoardGradelist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit</th>
                                                                            <th>Country</th>
                                                                            <th>State</th>
                                                                            <th>Qualification Level </th>
                                                                            <th>Board Name </th>
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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("BOARDNO")+","+Eval("QUALIFICATION_LEVEL")+","+Eval("COUNTRYNO")+","+Eval("STATENO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEditBoardGradeScheme_Click" TabIndex="6" />
                                                                        <%--  CommandArgument='<%# Eval("BOARDNO")+","+Eval("QUALIFICATION_LEVEL")%>' AlternateText="Edit Record" ToolTip="Edit Record"--%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("COUNTRYNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STATENAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("QUALIFICATION_LEVEL_TEXT") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BOARDNAME") %>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                            <%-- TAB:Reservation Configuration --%>
                                            <div class="tab-pane tab8" id="tab_8">
                                                <div class="mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Degree</label>

                                                            <asp:DropDownList ID="ddlReservationDegree" runat="server" OnSelectedIndexChanged="ddlReservationDegree_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="1"
                                                                AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlReservationDegree" runat="server" ControlToValidate="ddlReservationDegree"
                                                                ErrorMessage="Please Select Degree." Display="None" SetFocusOnError="true" ValidationGroup="Reservation" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvResgDegree" runat="server" ControlToValidate="ddlReservationDegree" ErrorMessage="Please Select Degree." Display="None" SetFocusOnError="true" ValidationGroup="RegConfig" InitialValue="Please Select"></asp:RequiredFieldValidator>

                                                            <asp:Label ID="lblTooltipMsg" runat="server" Visible="false" Style="background-color: lightgray; font-family: Arial; font-style: italic; font-size: medium; color: darkgreen"></asp:Label>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Programme Code </label>
                                                            <asp:DropDownList ID="ddlReservationProgCode" runat="server" OnSelectedIndexChanged="ddlReservationProgCode_SelectedIndexChanged" AppendDataBoundItems="true" TabIndex="1"
                                                                AutoPostBack="true" ToolTip="Please Select Programme Code." CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvReservationProgCode" runat="server" ControlToValidate="ddlReservationProgCode"
                                                                ErrorMessage="Please Select Programme Code." Display="None" SetFocusOnError="true" ValidationGroup="Reservation" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvRegProg" runat="server" ControlToValidate="ddlReservationProgCode" ErrorMessage="Please Select Program Code." Display="None" SetFocusOnError="true" ValidationGroup="RegConfig" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Reservation </label>
                                                            <asp:ListBox runat="server" ID="lstReservation" SelectionMode="Multiple" CssClass="form-control multi-select-demo "></asp:ListBox><%--CssClass="form-control multi-select-demo "--%>
                                                            <%--OnSelectedIndexChanged="lstSubjectName_SelectedIndexChanged"--%>
                                                            <asp:RequiredFieldValidator ID="rfvlstReservation" runat="server" ControlToValidate="lstReservation"
                                                                Display="None" ValidationGroup="Reservation" InitialValue=""
                                                                ErrorMessage="Please Select Reservation."></asp:RequiredFieldValidator>
                                                        </div>

                                                        <%-- New Code From 15-12-2022 Start--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>* </sup>Start Date</label>
                                                            <asp:TextBox ID="txtReservationStartDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtReservationStartDate" runat="server" ControlToValidate="txtReservationStartDate"
                                                                Display="None" ErrorMessage="Please Select Start Date" SetFocusOnError="True"
                                                                ValidationGroup="Reservation" InitialValue=""></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>* </sup>End Date</label>
                                                            <asp:TextBox ID="txtReservationEndDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtReservationEndDate" runat="server" ControlToValidate="txtReservationEndDate"
                                                                Display="None" ErrorMessage="Please Select End Date" SetFocusOnError="True"
                                                                ValidationGroup="Academic" InitialValue=""></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Reservation" ForeColor="Red" runat="server"
                                                                ControlToValidate="txtReservationStartDate" ControlToCompare="txtReservationEndDate" Operator="LessThan" Type="Date"
                                                                Display="None" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                                        </div>
                                                        <%-- New Code From 15-12-2022 End--%>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnShowReservation" runat="server" Text="Show" ValidationGroup="RegConfig" CssClass="btn btn-outline-info" OnClick="btnShowReservation_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ValidationGroup="RegConfig"
                                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />

                                                            <asp:Button ID="btnSubmitReservation" runat="server" Text="Submit" ValidationGroup="Reservation" CssClass="btn btn-outline-info" OnClick="btnSubmitReservation_Click" />
                                                            <%--OnClick="btnSubmitReservation_Click"--%>
                                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="Reservation"
                                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                            <asp:Button ID="btnCancelReservation" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelReservation_Click" />
                                                            <%--OnClick="btnCancelReservation_Click"--%>
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:Panel ID="pnlReservation" runat="server" Visible="false">
                                                                <asp:ListView ID="lvReservation" runat="server">
                                                                    <LayoutTemplate>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divReservation">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <%--<th>Edit</th>--%>
                                                                                    <th>Degree</th>
                                                                                    <th>Programme Code</th>
                                                                                    <th>Reservation</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%--<td>
                                                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                                        CommandArgument='<%# Eval("DEGREE_NO")+","+Eval("BRANCH_NO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="6" OnClick="btnEditReservation_Click" />
                                                                                    
                                                                                </td>--%>
                                                                            <td>
                                                                                <%# Eval("DEGREE_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("BRANCH_NAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("RESERVATION_NAME")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <%--Qualifying Degree--%>
                                            <div class="tab-pane tab9" id="tab_9">
                                                <div class="mt-3">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Qualifying Degree</label>
                                                            <%--onkeypress="allowAlphaNumericSpace(event)"--%>
                                                            <asp:TextBox ID="txtQualDegreeName" runat="server" TabIndex="3" CssClass="form-control" MaxLength="200"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtQualDegree" runat="server" ControlToValidate="txtQualDegreeName"
                                                                Display="None" ErrorMessage="Please Enter Qualifying Degree" ValidationGroup="QualDegree" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="chkStatus" name="switch" checked />
                                                                <label data-on="Active" data-off="InActive" for="chkStatus"></label>
                                                            </div>
                                                        </div>

                                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup></sup>Status</label>
                                                            <div style="padding-top: 5px">
                                                                <asp:CheckBox ID="chkStatus" runat="server" AppendDataBoundItems="true" TabIndex="1" ToolTip="Check To Active." Text="&nbsp;Active"></asp:CheckBox>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <%--<asp:Button ID="btnSubmitQualDegree" runat="server" Text="Submit" TabIndex="4" OnClientClick="return validateActive();" ValidationGroup="QualDegree" OnClick="btnSubmitQualDegree_Click" CssClass="btn btn-outline-info" />--%>
                                                    <asp:LinkButton ID="btnSubmitQualDegree" runat="server" OnClientClick="return validateActive();" CssClass="btn btn-primary" ValidationGroup="QualDegree" OnClick="btnSubmitQualDegree_Click">Submit</asp:LinkButton>
                                                    <asp:ValidationSummary ID="vsQualDegree" runat="server" ValidationGroup="QualDegree"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelQualDegree" runat="server" TabIndex="5" Text="Cancel" ToolTip="Click To Clear" OnClick="btnCancelQualDegree_Click" CssClass="btn btn-outline-danger" />

                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlQualDegree" runat="server" Visible="false">
                                                        <asp:ListView ID="lvQualDegree" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divQualDegree">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Qualifying Degree</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <%--[QUALI_DEGREE_ID],[QUALI_DEGREE_NAME],[STATUS]--%>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("QUALI_DEGREE_ID ")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditQualDegree_Click" TabIndex="6" />
                                                                        <%--OnClick="btnEditQualDegree_Click"--%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("QUALI_DEGREE_NAME  ") %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                        <%--<%# Eval("STATUS")%>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>


                                            <%-- TAB:PROGRAM  --%>
                                            <div class="tab-pane tab10" id="tab_10">
                                                <div class="mt-3">
                                                    <div class="row">

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Program Type</label>
                                                            <asp:DropDownList ID="ddlProgramType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="9">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlProgramType" runat="server" ControlToValidate="ddlProgramType"
                                                                ErrorMessage="Please Select Programme Type" Display="None" ValidationGroup="Program" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Qualifying Degree</label>
                                                            <asp:DropDownList ID="ddlQualDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="9">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlQualDegree" runat="server" ControlToValidate="ddlQualDegree"
                                                                ErrorMessage="Please Select Qualifying Degree" Display="None" ValidationGroup="Program" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Program Code</label>
                                                            <asp:TextBox ID="txtProgramCode" runat="server" TabIndex="3" CssClass="form-control" MaxLength="10" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProgramCode" runat="server" ControlToValidate="txtProgramCode"
                                                                Display="None" ErrorMessage="Please Enter Program Code" ValidationGroup="Program" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup> Program Name</label>
                                                            <asp:TextBox ID="txtProgramName" runat="server" TabIndex="3" CssClass="form-control" MaxLength="100" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtProgramName" runat="server" ControlToValidate="txtProgramName"
                                                                Display="None" ErrorMessage="Please Enter Program Name" ValidationGroup="Program" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <asp:HiddenField ID="hfdPRG_Active" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Status</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="chkPRG_Status" name="switch" checked />
                                                                <label data-on="Active" data-off="InActive" for="chkPRG_Status"></label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <%--<asp:Button ID="btnSubmitProgrm" runat="server" Text="Submit" TabIndex="4" OnClientClick="return validatePRG_Active();" ValidationGroup="Program" OnClick="btnSubmitProgrm_Click" CssClass="btn btn-outline-info" />--%>
                                                    <asp:LinkButton ID="btnSubmitProgrm" runat="server" OnClientClick="return validatePRG_Active();" CssClass="btn btn-primary" ValidationGroup="Program" OnClick="btnSubmitProgrm_Click">Submit</asp:LinkButton>
                                                    <%--OnClick="btnSubmitProgrm_Click"--%>
                                                    <asp:ValidationSummary ID="vsProgram" runat="server" ValidationGroup="Program"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelProgrm" runat="server" TabIndex="5" Text="Cancel" ToolTip="Click To Clear" OnClick="btnCancelProgrm_Click" CssClass="btn btn-outline-danger" />
                                                    <%--OnClick="btnCancelProgrm_Click"--%>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlProgram" runat="server" Visible="false">
                                                        <asp:ListView ID="lvProgram" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divProgram">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Program Type</th>
                                                                            <th>Qualifying Degree</th>
                                                                            <th>Program Code</th>
                                                                            <th>Program Name </th>
                                                                            <th>Status</th>
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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("PROG_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditProgram_Click" TabIndex="6" />
                                                                        <%--OnClick="btnEditProgram_Click"--%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PROG_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("QUALI_DEGREE_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PROG_CODE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("PROG_NAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                        <%--<%# Eval("STATUS")%>--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                          
                                            <%-- TAB:TEST SCORE  --%>
                                            <div class="tab-pane tab11" id="tab_11">
                                                <div class="mt-3">

                                                    <div class="row">
                                                        <div class="col-12 sub-heading mb-3">
                                                            <h5>Create Test Master</h5>
                                                        </div>
                                                        <%--ddlAdmBatch;txtTestName;txtMaxScore;txtAverage;txtStdDev--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Admission Batch</label>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                                ErrorMessage="Please Select Admission Batch." Display="None" ValidationGroup="TestMaster" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvddlAdmBatchText" runat="server" ControlToValidate="ddlAdmBatch"
                                                                ErrorMessage="Please Select Admission Batch." Display="None" ValidationGroup="TestMaster" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Degree Type</label>

                                                            <asp:ListBox ID="lstbxdegreetype" runat="server" CssClass="form-control multi-select-demo"
                                                                AppendDataBoundItems="true" SelectionMode="Multiple" TabIndex="7"></asp:ListBox>

                                                            <asp:RequiredFieldValidator ID="rfvlstbxdegreetype" runat="server" ControlToValidate="lstbxdegreetype"
                                                                Display="None" ErrorMessage="Please Select Degree Type." SetFocusOnError="True"
                                                                ValidationGroup="TestMaster"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Test Name</label>
                                                            <asp:TextBox ID="txtTestName" runat="server" MaxLength="100" onkeypress="allowAlphaNumericSpace(event)" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtTestName" runat="server" ControlToValidate="txtTestName"
                                                                Display="None" ErrorMessage="Please Enter Test Name." ValidationGroup="TestMaster" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Max Score</label>
                                                            <asp:TextBox ID="txtMaxScore" runat="server" CssClass="form-control" MaxLength="5"
                                                                onkeypress="return (event.charCode >= 48 && event.charCode <= 57)"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtMaxScore" runat="server" ControlToValidate="txtMaxScore" Display="None" ErrorMessage="Please Enter Max Score" ValidationGroup="TestMaster" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Average</label>
                                                            <asp:TextBox ID="txtAverage" runat="server" CssClass="form-control" MaxLength="5"
                                                                onkeypress="return (event.charCode >= 48 && event.charCode <= 57)||event.charCode == 46"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtAverage" runat="server" ControlToValidate="txtAverage" Display="None" ErrorMessage="Please Enter Average" ValidationGroup="TestMaster" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>Std. Deviation</label>
                                                            <asp:TextBox ID="txtStdDev" runat="server" CssClass="form-control" MaxLength="5"
                                                                onkeypress="return (event.charCode >= 48 && event.charCode <= 57)||event.charCode == 46"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtStdDev" runat="server" ControlToValidate="txtStdDev" Display="None"
                                                                ErrorMessage="Please Enter Std. Deviation" ValidationGroup="TestMaster" SetFocusOnError="true">
                                                            </asp:RequiredFieldValidator>
                                                        </div>

                                                        <%-- New Code From 15-12-2022 Start--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>* </sup>Start Date</label>
                                                            <asp:TextBox ID="txtTestScoreStartDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtTestScoreStartDate" runat="server" ControlToValidate="txtTestScoreStartDate"
                                                                Display="None" ErrorMessage="Please Select Start Date" SetFocusOnError="True"
                                                                ValidationGroup="TestMaster" InitialValue=""></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>* </sup>End Date</label>
                                                            <asp:TextBox ID="txtTestScoreEndDate" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtTestScoreEndDate" runat="server" ControlToValidate="txtTestScoreEndDate"
                                                                Display="None" ErrorMessage="Please Select End Date" SetFocusOnError="True"
                                                                ValidationGroup="TestMaster" InitialValue=""></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator2" ValidationGroup="TestMaster" ForeColor="Red" runat="server"
                                                                ControlToValidate="txtTestScoreStartDate" ControlToCompare="txtTestScoreEndDate"
                                                                Operator="LessThan" Type="Date" Display="None" ErrorMessage="Start date must be less than End date."></asp:CompareValidator>
                                                        </div>

                                                        <asp:HiddenField ID="hfdTST_Active" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>is GATE qualify?</label>
                                                            </div>
                                                            <div class="switch form-inline">
                                                                <input type="checkbox" id="chkTST_Status" name="switch" checked />
                                                                <label data-on="Yes" data-off="No" for="chkTST_Status"></label>
                                                            </div>
                                                                                                               
                                                        </div>
   
                                                        <asp:HiddenField ID="hfdActiveStatus" runat="server" ClientIDMode="Static" />
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Active Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="chkActiveStatus" name="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="chkActiveStatus"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <label><sup>*</sup>No. Of Years</label>
                                                            <asp:DropDownList ID="ddlyears" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvyears" runat="server" ControlToValidate="ddlyears"
                                                                ErrorMessage="Please Select No. Of Years." Display="None" ValidationGroup="TestMaster" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvnoyears" runat="server" ControlToValidate="ddlyears"
                                                                ErrorMessage="Please Select No. Of Years." Display="None" ValidationGroup="TestMaster" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                        </div>
<%--Shrikant --%>
                                                        <div class="form-group col-lg-2 col-md-6 col-12" runat="server" visible="false">
                                                            <%--  --%>                                                           
                                                                    <label><sup>*</sup>Allow Test Score Subjects</label><br />
                                                                    <asp:CheckBox ID="chkAllowScoreSubject" runat="server" onclick="toggle(this)" Checked="true"/>                                                                  
                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-6 col-12" id="tableDiv" runat="server" visible="false">  
                                                             
                                                            <table class="table table-striped table-bordered nowrap" runat="server" id="idTable">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Additional Test Score Subject</th>
                                                                        <th>Max. Marks</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddSub1" runat="server" CssClass="form-control" MaxLength="50" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnAddSub1" runat="server" Value="0"/>
                                                                        </td>
                                                                         
                                                                        
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddMaxMarks1" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddSub2" runat="server" CssClass="form-control" MaxLength="50" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnAddSub2" runat="server" Value="0" />
                                                                        </td>
                                                                         
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddMaxMarks2" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddSub3" runat="server" CssClass="form-control" MaxLength="50" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnAddSub3" runat="server" Value="0" />
                                                                        </td>
                                                                         
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddMaxMarks3" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddSub4" runat="server" CssClass="form-control" MaxLength="50" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnAddSub4" runat="server" Value="0" />
                                                                        </td>
                                                                         
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddMaxMarks4" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddSub5" runat="server" CssClass="form-control" MaxLength="50" onkeypress="allowAlphaNumericSpace(event)"></asp:TextBox>
                                                                            <asp:HiddenField ID="hdnAddSub5" runat="server" Value="0" />
                                                                        </td>
                                                                         
                                                                        <td>
                                                                            <asp:TextBox ID="txtAddMaxMarks5" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)"></asp:TextBox></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            </div> 
                                                        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />                                                            
                                                        
                                                        <%-- New Code From 15-12-2022 End--%>
                                                    </div>

                                                </div>
                                        <div class="col-12 btn-footer">
<%--                                                   <asp:Button ID="btnSubmitTestScore" runat="server" Text="Submit" TabIndex="4" OnClientClick="return validateTST_Active();" ValidationGroup="TestMaster" OnClick="btnSubmitTestScore_Click" CssClass="btn btn-outline-info" />--%>
                                                    <asp:LinkButton ID="btnSubmitTestScore" runat="server" OnClientClick="return validateTST_Active(); " CssClass="btn btn-primary" ValidationGroup="TestMaster" OnClick="btnSubmitTestScore_Click">Submit</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary11" runat="server" ValidationGroup="TestMaster"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnCancelTestScore" runat="server" Text="Cancel" TabIndex="5" OnClick="btnCancelTestScore_Click" CssClass="btn btn-outline-danger" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlTestScore" runat="server" Visible="false">
                                                        <asp:ListView ID="lvTestScore" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divTestScore">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Admission Batch</th>
                                                                            <th>Degree Type</th>
                                                                            <th>Test Name</th>
                                                                            <th>Max Score</th>
                                                                            <th>Average</th>
                                                                            <th>Std. Deviation</th>
                                                                            <th>Start Date</th>
                                                                            <th>End Date</th>
                                                                            <th>is GATE qualify</th>
                                                                            <th>No Of Years</th>
                                                                            <th>Active Status</th>
                                                                            <%--<th>Additional Test Score Subject</th>
                                                                            <th>Max. Marks</th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <%--SCOREID ,BATCHNO,BATCHNAME,TESTNAME,MAXSCORE,AVERAGE,STDDEVIATION --%>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("SCOREID")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="6" OnClick="btnEditTestScore_Click" />
                                                                        <%--OnClick="btnEditProgram_Click"--%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("BATCHNAME")%>
                                                                        <asp:HiddenField ID="hfdBATCHNO" runat="server" Value='<%# Eval("BATCHNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEGREE_TYPE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TESTNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("MAXSCORE") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("AVERAGE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STDDEVIATION") %>
                                                                    </td>
                                                                    <td>
                                                                        <%#  Eval("STARTDATE") != DBNull.Value ? Convert.ToDateTime(Eval("STARTDATE")).ToString("dd-MM-yyyy") : "" %>
                                                                    </td>
                                                                    <td>
                                                                        <%#  Eval("ENDDATE") != DBNull.Value ? Convert.ToDateTime(Eval("ENDDATE")).ToString("dd-MM-yyyy") : "" %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" CssClass="badge" Text='<%# Eval("IS_GATE_QUALIFY") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("NO_OF_YEARS") %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblActiveStatus" runat="server" CssClass="badge" Text='<%# Eval("ACTIVE_STATUS") %>'></asp:Label>
                                                                    </td>
                                                                    <%--<td>
                                                                        <%# Eval("ALLOW_TESTSCORE_SUBJECT") %>
                                                                    </td>--%>
                                                                    
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <%--<div class="col-12 mt-3">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Edit/Delete</th>
                                                                        <th>Country</th>
                                                                        <th>State</th>
                                                                        <th>Qualification Level </th>
                                                                        <th>Board Name </th>
                                                                        <th>Grade Name</th>
                                                                        <th>Grade Point</th>
                                                                        <th>Max. Grade Point</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <img src="IMAGES/edit.png" alt="edit" />
                                                                            <img src="IMAGES/delete.png" alt="delete" />
                                                                        </td>
                                                                        <td>India</td>
                                                                        <td>Maharashtra</td>
                                                                        <td>10th</td>
                                                                        <td>Maharashtra State Board </td>
                                                                        <td>A</td>
                                                                        <td>9</td>
                                                                        <td>10</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>--%>
                                            </div>

                                            <%--TAB: GATE-NON GATE--%>
                                            <div class="tab-pane tab12" id="tab_12">
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                    <ContentTemplate>
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="col-md-4 col-md-4 col-12">
                                                                    <div class="row">

                                                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                                                            <div class="label-dynamic">
                                                                                <sup></sup>
                                                                                <%--<label>GATE - NON GATE</label>--%>
                                                                                <asp:RadioButtonList ID="chk_Gate_NonGate" runat="server" OnSelectedIndexChanged="chk_Gate_NonGate_Changed" AutoPostBack="true" RepeatDirection="Horizontal" CssClass="rdo-btn">
                                                                                    <asp:ListItem Value="1" Selected="True"> GATE &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                                    <asp:ListItem Value="0"> NON GATE</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <%--<label class="switch">
                                                                                <asp:CheckBox ID="chk_Gate_NonGate" runat="server" Checked="true" OnCheckedChanged="chk_Gate_NonGate_Changed" AutoPostBack="true" />
                                                                                <span class="slider round" id="span_Gate_NonGate" runat="server">GATE</span>
                                                                            </label>--%>
                                                                        </div>

                                                                        <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="div_Gate_SubjectCode" visible="true">
                                                                            <div class="label-dynamic">
                                                                                <sup>*</sup>
                                                                                <label>Subject Code</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlGateSubjectCode" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGateSubjectCode_SelectedIndexChanged" AppendDataBoundItems="true" data-select2-enable="true">
                                                                                <%----%>
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">EC</asp:ListItem>
                                                                                <asp:ListItem Value="2">EE</asp:ListItem>
                                                                                <asp:ListItem Value="3">CS</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvddlGateSubjectCode" runat="server" ControlToValidate="ddlGateSubjectCode"
                                                                                ErrorMessage="Please Select Subject Code" Display="None" ValidationGroup="GateNonGate" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                                        </div>

                                                                        <div class="form-group col-lg-12 col-md-12 col-12" runat="server" id="div_NonGate_DegreeBranch" visible="false">
                                                                            <div class="label-dynamic">
                                                                                <sup>*</sup>
                                                                                <label>Applied Degree-Branch</label>
                                                                            </div>
                                                                            <asp:DropDownList ID="ddlNonGateDegreMapping" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlNonGateDegreMapping_SelectedIndexChanged" AutoPostBack="true">
                                                                                <%----%>
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvddlNonGateDegreMapping" runat="server" ControlToValidate="ddlNonGateDegreMapping"
                                                                                ErrorMessage="Please Select Degree" Display="None" ValidationGroup="GateNonGate" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-8 col-md-8 col-12">

                                                                    <asp:Panel ID="pnlGateDegreeMapping" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvGateDegreeMapping" runat="server">
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divStageDegree">
                                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                            <tr>
                                                                                                <th id="thHead">
                                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select All" TabIndex="9" OnClick="checkBulkAllCheckbox_Gate(this)" />
                                                                                                </th>
                                                                                                <th>Applied Degree-Branch</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="UpdateLV">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <%--DEGREENO_BRANCHNO	DEGREENAME_BRANCHNAME	GateIsActive--%>
                                                                                            <td style="text-align: center">
                                                                                                <asp:HiddenField ID="hfdGateDegreeBranchId" runat="server" Value='<%# Eval("DEGREENO_BRANCHNO") %>' />
                                                                                                <asp:CheckBox ID="chkGateIsActive" runat="server" Checked='<%# Convert.ToBoolean(Eval("GateIsActive")) %>' CssClass="chkbox_addsubject" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("DEGREENAME_BRANCHNAME")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>


                                                                    <asp:Panel ID="pnlNonGateDegreeMapping" runat="server" Visible="false">
                                                                        <asp:ListView ID="lvNonGateDegreeMapping" runat="server">
                                                                            <LayoutTemplate>
                                                                                <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="divNonGateDegreeMapping">
                                                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                            <tr>
                                                                                                <th id="thHead">
                                                                                                    <asp:CheckBox ID="cbHead" runat="server" Text="Select All" TabIndex="9" OnClick="checkBulkAllCheckbox_NonGate(this)" />
                                                                                                </th>
                                                                                                <th>Qualifying Degree-Program</th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </LayoutTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:UpdatePanel runat="server" ID="UpdateLV">
                                                                                    <ContentTemplate>
                                                                                        <tr>
                                                                                            <%--DEGREENO	BRANCHNO	QUALI_DEGREE_ID_PROG_ID	QUALI_DEGREE_NAME_PROG_NAME	NonGateIsActive--%>
                                                                                            <td style="text-align: center">
                                                                                                <asp:HiddenField ID="hfdNonGateDegreeProgramId" runat="server" Value='<%# Eval("QUALI_DEGREE_ID_PROG_ID") %>' />
                                                                                                <asp:CheckBox ID="chkNonGateIsActive" runat="server" CssClass="chkbox_addsubject" Checked='<%# Convert.ToBoolean(Eval("NonGateIsActive")) %>' /><%--<%# Eval("NonGateIsActive") %>--%>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("QUALI_DEGREE_NAME_PROG_NAME")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </ItemTemplate>
                                                                        </asp:ListView>
                                                                    </asp:Panel>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitStageMapping" runat="server" CssClass="btn btn-primary" ValidationGroup="GateNonGate" OnClick="btnSubmitGate_NonGateMapping_Click">Submit</asp:LinkButton>
                                                            <%-- --%>
                                                            <asp:LinkButton ID="bntCancelStageMapping" runat="server" CssClass="btn btn-warning" OnClick="bntCancelGate_NonGateMapping_Click">Cancel</asp:LinkButton>
                                                            <%----%>
                                                            <asp:ValidationSummary ID="ValidationSummary10" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="GateNonGate" />
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnSubmitStageMapping" />
                                                        <asp:PostBackTrigger ControlID="bntCancelStageMapping" />

                                                        <asp:AsyncPostBackTrigger ControlID="chk_Gate_NonGate" EventName="SelectedIndexChanged" />
                                                        <%-- CheckedChanged" />--%>
                                                        <asp:AsyncPostBackTrigger ControlID="ddlGateSubjectCode" EventName="SelectedIndexChanged" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddlNonGateDegreMapping" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCountrySub" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlStateSub" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBoard" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlQualification" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSubType" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlCountryGrade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlStateGrade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBoardGrade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlQualificationGrade" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlReservationDegree" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlReservationProgCode" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmitBoard" />
            <asp:PostBackTrigger ControlID="btnCancelBoard" />
            <%--<asp:AsyncPostBackTrigger ControlID="btnSubmitSub" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelSub" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="btnSubmitSub" />
            <asp:PostBackTrigger ControlID="btnCancelSub" />
            <asp:PostBackTrigger ControlID="btnSubmitSubType" />
            <asp:PostBackTrigger ControlID="btnCancelSubType" />
            <asp:PostBackTrigger ControlID="btnSubmitAddSub" />
            <asp:PostBackTrigger ControlID="btnCancelAddSub" />

            <asp:PostBackTrigger ControlID="btnAddGradeSlab" />
            <asp:PostBackTrigger ControlID="btnSubmitGrade" />
            <asp:PostBackTrigger ControlID="btnCancelGrade" />

            <asp:PostBackTrigger ControlID="btnSubmitReservation" />
            <asp:PostBackTrigger ControlID="btnCancelReservation" />
            <asp:PostBackTrigger ControlID="btnShowReservation" />
            
            <asp:PostBackTrigger ControlID="btnSubmitGroup" />
            <asp:PostBackTrigger ControlID="btnCancelGroup" />

            <asp:PostBackTrigger ControlID="btnSubmitSubConfig" />
            <asp:PostBackTrigger ControlID="btnCancelSubConfig" />
            <asp:PostBackTrigger ControlID="btnAddDetail" />

            <asp:PostBackTrigger ControlID="btnSubmitQualDegree" />
            <asp:PostBackTrigger ControlID="btnCancelQualDegree" />

            <asp:PostBackTrigger ControlID="btnSubmitProgrm" />
            <asp:PostBackTrigger ControlID="btnCancelProgrm" />

            <asp:PostBackTrigger ControlID="btnSubmitTestScore" />
            <asp:PostBackTrigger ControlID="btnCancelTestScore" />
        </Triggers>
    </asp:UpdatePanel>


    <script>

        function TabShow(tabName) {
            //alert('hii')
            //var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
        
    <script>
        $(function () {
            $('input[name^="text"]').change(function () {

                var $current = $(this);

                $('input[name^="text"]').each(function () {
                    if ($(this).val() == $current.val() && $(this).attr('id') != $current.attr('id')) {
                        alert('duplicate found!');
                    }

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
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Enter Only Numeric Value ");
                return false;
            }
        }
        function allowAlphaNumericSpace(e)
        {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code == 45) && // Dash
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123))
            { // lower alpha (a-z)
                e.preventDefault();
            }
        }

        function setMaxLength(control) {
            //get the isMaxLength attribute
            var mLength = control.getAttribute ? parseInt(control.getAttribute("isMaxLength")) : ""

            //was the attribute found and the length is more than the max then trim it
            if (control.getAttribute && control.value.length > mLength) {
                control.value = control.value.substring(0, mLength);
                alert('Length exceeded');
            }

            //display the remaining characters
            var modid = control.getAttribute("id") + "_remain";
            if (document.getElementById(modid) != null) {
                document.getElementById(modid).innerHTML = mLength - control.value.length;
            }
        }

        function checkedActive_Change(chkIsActive) {      // Added function by Yogesh Kumbhar Date:15-11-2022

            var row = chkIsActive.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;//get current row index                      
            var row, id;
            row = $(chkIsActive).parent().parent();

            var checked = $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_chkIsActive").is(':checked');

            if (checked) {
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_txtMaxMarks").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_txtMaxMarks").attr("enabled", "enabled");

                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").multiselect('rebuild');
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").addClass("form-control multi-select-demo ");


            } else {
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_txtMaxMarks", row).val('');
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_txtMaxMarks").attr("disabled", "disabled");

                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").multiselect("clearSelection");
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").attr("disabled", "disabled");
                $("#ctl00_ContentPlaceHolder1_lvAddSubjectMaxMarks_ctrl" + rowIndex + "_lstGroup").closest('.multiselect-native-select').find('button').attr("disabled", "disabled");

            }


        };

    </script>

    <%-- STATUS : ACTIVE|INACTIVE IN TAB:9,10,11 --%>
    <script>
        function SetActive(val) {
            $('#chkStatus').prop('checked', val);

        }
        function validateActive() {

            $('#hfdActive').val($('#chkStatus').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitQualDegree').click(function () {
                    validateActive();
                });
            });
        });
    </script>

    <script>
        function SetPRG_Active(val) {
            $('#chkPRG_Status').prop('checked', val);

        }
        function validatePRG_Active() {

            $('#hfdPRG_Active').val($('#chkPRG_Status').prop('checked'));

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitProgrm').click(function () {
                    validatePRG_Active();
                });
            });
        });
    </script>

    <script>
        
        function SetTST_Active(val) {
            $('#chkTST_Status').prop('checked', val);
        }

        function Set_ActiveStatus(val) {
            $('#chkActiveStatus').prop('checked', val);
        }

        function validateTST_Active() {
                $('#hfdActiveStatus').val($('#chkActiveStatus').prop('checked'));
                $('#hfdTST_Active').val($('#chkTST_Status').prop('checked'));
        }
        
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitTestScore').click(function () {                    
                    validateTST_Active();
                });
            });
        });
    </script>

    <script>
        function checkBulkAllCheckbox_Gate(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvGateDegreeMapping$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvGateDegreeMapping$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

        function checkBulkAllCheckbox_NonGate(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvNonGateDegreeMapping$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvNonGateDegreeMapping$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

     <script type="text/javascript">
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200,
                 enableFiltering: true,
                 filterPlaceholder: 'Search',
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
                 });
             });
         });
    </script>

    <script>
        function toggle(ele) {
            var cont = document.getElementById('<%=tableDiv.ClientID%>');
            if (ele.checked) {
                cont.style.display = 'block';
            } else {
                cont.style.display = 'none';
            }
        }
    </script>  
    
    <script>
        $(function () {
            $("#tab_11").tabs({
                activate: function () {
                    var selectedTab = $('#tab_11').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
        },
        active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
    });
       });
         </script> 
</asp:Content>

<%--Finish--%>

