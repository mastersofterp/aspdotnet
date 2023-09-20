<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="CourseAllotment_Bulk.aspx.cs" Inherits="ACADEMIC_CourseAllotment_Bulk" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_divCourses .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_div1 .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_div4 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .multiselect-container {
            position: absolute;
            transform: translate3d(0px, -46px, 0px);
            top: 0px;
            left: 0px;
            will-change: transform;
            height: 200px;
            overflow: auto;
        }
    </style>

    <%--  <link href="../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/bootstrap-multiselect.js"></script>--%>
    <link href="<%=Page.ResolveClientUrl("../plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("../plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <%--   <script src="../jquery/bootstrap-multiselect.js"></script>--%>
    <%-- <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect();
            });
        });
    </script>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }

            //function InitAutoCompl() {
            //    $('select[multiple]').val('').multiselect({
            //        columns: 2,     // how many columns should be use to show options            
            //        search: true, // include option search box
            //        searchOptions: {
            //            delay: 250,                         // time (in ms) between keystrokes until search happens
            //            clearSelection: true,
            //            showOptGroups: false,                // show option group titles if no options remaining

            //            searchText: true,                 // search within the text

            //            searchValue: true,                // search within the value

            //            onSearch: function (element) { } // fires on keyup before search on options happens
            //        },

            //        // plugin texts

            //        texts: {

            //            placeholder: 'Select Section', // text to use in dummy input

            //            search: 'Search',         // search input placeholder text

            //            selectedOptions: ' selected',      // selected suffix text

            //            selectAll: 'Select all sections',     // select all text

            //            unselectAll: 'Unselect all sections',   // unselect all text

            //            //noneSelected: 'None Selected'   // None selected text

            //        },

            //        // general options

            //        selectAll: true, // add select all option

            //        selectGroup: false, // select entire optgroup

            //        minHeight: 200,   // minimum height of option overlay

            //        maxHeight: null,  // maximum height of option overlay

            //        maxWidth: null,  // maximum width of option overlay (or selector)

            //        maxPlaceholderWidth: null, // maximum width of placeholder button

            //        maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

            //        showCheckbox: true,  // display the checkbox to the user

            //        optionAttributes: [],

            //    });
            //}

        });

    </script>

    <script>
        function getVal() {
            var array = []
            var sectionNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (sectionNo == undefined) {
                    sectionNo = checkboxes[i].value + ',';
                }
                else {
                    sectionNo += checkboxes[i].value + ',';
                }
            }
            //alert(sectionNo);
            $('#<%= hdnsectionno.ClientID %>').val(sectionNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>

    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>--%>
    <script>
        //var MulSel = $.noConflict();
        $(document).ready(function () {
            $('.multi-select-demo').multiselect();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                    enableHTML: true,
                    templates: {
                        filter: '<li class="multiselect-item multiselect-filter"><div class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text"><i class="fa fa-search"></i></span></div><input class="form-control multiselect-search" type="text" /></div></li>',
                        filterClearBtn: '<span class="input-group-btn"><button class="btn btn-default multiselect-clear-filter" style="height: 33px;" type="button"><i style="margin-right: 4px;" class="fa fa-eraser"></i></button></span>'
                    }
                    //dropRight: true,
                    //search: true,
                });

            });
        });
    </script>

    <style>
        .ms-options-wrap {
            position: relative;
        }

        .ms-options {
            /*position:absolute;
        bottom:10px;left:0;
        width:100%;*/
            min-height: inherit !important;
        }

        #example2_wrapper #example2_filter {
            /*display: none;*/
        }
    </style>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999;
            /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>

    <style type="text/css">
        .DefaultCheckBoxList li {
            float: left;
            list-style-type: none;
        }

        @media screen and (max-width: 300px) {
            .DefaultCheckBoxList li {
                float: none;
            }
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }

        });

    </script>

    <script>
        function getVal() {
            var array = []
            var sectionNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (sectionNo == undefined) {
                    sectionNo = checkboxes[i].value + ',';
                }
                else {
                    sectionNo += checkboxes[i].value + ',';
                }
            }
            //alert(sectionNo);
            $('#<%= hdnsectionno.ClientID %>').val(sectionNo);
            //document.getElementById(inpHide).value = "degreeNo";
        }
    </script>

    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>

    <%--<script src="../jquery/jquery.multiselect.js"></script>--%>
    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {

            var table = $('.display-s').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 400,
                //scrollX: true,
                //scrollCollapse: true,
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
                            return $('.display-s').DataTable().column(idx).visible();
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
                                    return $('.display-s').DataTable().column(idx).visible();
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
                                    return $('.display-s').DataTable().column(idx).visible();
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
                var table = $('.display-s').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 400,
                    //scrollX: true,
                    //scrollCollapse: true,
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
                                return $('.display-s').DataTable().column(idx).visible();
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
                                        return $('.display-s').DataTable().column(idx).visible();
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
                                        return $('.display-s').DataTable().column(idx).visible();
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
            var table = $('#tblCourse').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                //scrollX: true,
                //scrollCollapse: true,
                paging: false,
                searching: false,

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
                                return $('#tblCourse').DataTable().column(idx).visible();
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
                                            return $('#tblCourse').DataTable().column(idx).visible();
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
                                            return $('#tblCourse').DataTable().column(idx).visible();
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
                var table = $('#tblCourse').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    //scrollX: true,
                    //scrollCollapse: true,
                    paging: false,
                    searching: false,

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
                                    return $('#tblCourse').DataTable().column(idx).visible();
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
                                                return $('#tblCourse').DataTable().column(idx).visible();
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
                                                return $('#tblCourse').DataTable().column(idx).visible();
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
                <div class="box-header with-border">
                    <h3 class="box-title"><%--COURSE TEACHER ALLOTMENT BULK--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Main Teacher</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Additional Teacher</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Cancel Teacher Allotment</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updPanel1"
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
                                <asp:UpdatePanel ID="updPanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="divCourses" runat="server">
                                            <div class="col-12 mt-2">
                                                <div class="row">


                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>School/Institute Name</label>--%>
                                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchoolInstitute" TabIndex="2" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolInstitute_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchoolInstitute" runat="server" ControlToValidate="ddlSchoolInstitute" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituter" runat="server" ControlToValidate="ddlDegree" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%-- <label>Degree</label>--%>
                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" TabIndex="2" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvrddlDegree" runat="server" ControlToValidate="ddlDegree" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Programme/Branch</label>--%>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="3" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvrddlBranch" runat="server" ControlToValidate="ddlBranch" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/Branch" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Scheme</label>--%>
                                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlScheme" runat="server" ControlToValidate="ddlScheme"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvrddlScheme" runat="server" ControlToValidate="ddlScheme"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Session</label>--%>
                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionBulk" runat="server" TabIndex="2" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSessionBulk_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="rfvddlSessionBulk" runat="server" ControlToValidate="ddlSessionBulk"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvrddlSessionBulk" runat="server" ControlToValidate="ddlSessionBulk"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Semester</label>--%>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvrddlSemester" runat="server" ControlToValidate="ddlSemester"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Course</label>--%>
                                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="4"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_OnSelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSubject" runat="server" ControlToValidate="ddlSubject"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divTutorial" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Alloment For</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlTutorial" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="5"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTutorial_SelectedIndexChanged">
                                                            <%--<asp:ListItem Selected="True" Value="1">Theory</asp:ListItem>
                                                            <asp:ListItem Value="2">Tutorial</asp:ListItem>--%>
                                                        </asp:DropDownList>

                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <%--<label>Department</label>--%>
                                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddldepartment" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" TabIndex="6">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <%-- <asp:RequiredFieldValidator ID="rfvdepartment" runat="server" ControlToValidate="ddldepartment"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvdepartmentr" runat="server" ControlToValidate="ddldepartment"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>



                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="checkbox" id="chkCheck" runat="server" visible="false">
                                                            <label>
                                                                <b>Other Department</b><asp:CheckBox ID="chkDept" OnCheckedChanged="chkDept_OnCheckedChanged" runat="server" Style="margin-left: 24px;" AutoPostBack="true" Text="" />
                                                            </label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divOtherDepartment" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Other Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlOtherDepartment" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOtherDepartment_OnSelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="rfvOtherDepartment" runat="server" ControlToValidate="ddlOtherDepartment"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Other Department" ValidationGroup="teacherallot">
                                                        </asp:RequiredFieldValidator>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddldepartment"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallotr">
                                                        </asp:RequiredFieldValidator>--%>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnFilter" runat="server" TabIndex="7" ValidationGroup="teacherallot" OnClick="btnFilter_Click" CssClass="btn btn-primary"><%--<i class="fa fa-eye"></i>--%> Show </asp:LinkButton>
                                                <asp:LinkButton ID="btnSave" TabIndex="8" runat="server" CssClass="btn btn-primary" ValidationGroup="teacherallot" Visible="false"
                                                    OnClientClick="return getVal();" OnClick="btnSave_Click"><%--<i class="fa fa-save margin-r-5"></i>--%> Submit</asp:LinkButton>
                                                <asp:HiddenField ID="hdnsectionno" runat="server" />
                                                <asp:HiddenField ID="hdfCourseteach" runat="server" />

                                                <asp:LinkButton ID="btnReport" runat="server" TabIndex="9" Visible="false"
                                                    OnClick="btnReport_Click" ValidationGroup="teacherallotr" CssClass="btn btn-info">
                                                            Report
                                                </asp:LinkButton>
                                                <%--  <asp:LinkButton ID="btnReportExcelCT" runat="server" TabIndex="9" Visible="true"
                                                    OnClick="btnReportExcelCT_Click" ValidationGroup="teacherallotr" CssClass="btn btn-info">
                                                            Report
                                                </asp:LinkButton>--%>

                                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherallot"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="teacherallotr"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class=" col-12">

                                                <asp:ListView ID="lvCourseTeacher" runat="server" OnItemDataBound="lvCourseTeacher_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>LIST OF FACULTIES</h5>
                                                        </div>
                                                        <div class="col-lg-3 col-md-6">
                                                            <div class="input-group sea-rch">
                                                                <input type="text" id="FilterData" onkeyup="SearchFunction()" placeholder="Search" class="Searchfilter" />
                                                            </div>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblCourse">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" ToolTip="Select all" runat="server" AutoPostBack="false" onclick="selectAll(this);" />
                                                                        <%--onclick="SelectSelectAllCheckButton(this,ctl00_ContentPlaceHolder1_lvCourseTeacher)" />--%>
                                                                    </th>
                                                                    <%--    <th style="text-align: center">CCODE </th>
                                                                    <th style="text-align: center">Subject Name </th>--%>
                                                                    <th>Main Teacher</th>
                                                                    <th id="Section1">
                                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <%-- <th style="text-align: center">Room</th>--%>
                                                                    <th id="BatchTheory1">Section - Batch</th>
                                                                    <th>Is ADTeacher</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <td>
                                                            <%----%>
                                                            <asp:CheckBox ID="chkAccept" Enabled='<%# (Eval("Allot").ToString() == "1" ? false : true)%>' runat="server" ToolTip='<%# Eval("COURSENO")%>'
                                                                Checked='<%# Eval("ALLOT").ToString()=="1"?true:false%>' />
                                                            <asp:HiddenField ID="hdnAllo" runat="server" Value='<%# Eval("Allot")%>' />
                                                        </td>
                                                        <%--  <td><%# Eval("CCODE")%></td>
                                                        <td><%# Eval("COURSE_NAME")%></td>--%>
                                                        <td><%# Eval("UA_FULLNAME")%>
                                                            <asp:HiddenField ID="hdnTeacher" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                            <asp:HiddenField ID="hdndeptNo" runat="server" Value='<%# Eval("UA_DEPTNO")%>' />
                                                            <asp:HiddenField ID="hdnSubid" runat="server" Value='<%# Eval("SUBID")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" multiple="multiple" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnSection" runat="server" />

                                                            <asp:ListBox ID="lstbxSections" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                                                CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="false"
                                                                OnSelectedIndexChanged="lstbxSections_SelectedIndexChanged"></asp:ListBox>
                                                            <%--<asp:ListBox ID="lstbxSections" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>


                                                            <%--<div id="Layer1" class="dropdown mycheckbox-list">
                                                                <button class="btn btn-default dropdown-toggle" type="button" id="menu1" data-toggle="dropdown">
                                                                    Please Select
                                                                <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                                                                    <asp:CheckBoxList ID="chkbxSectionlist" runat="server"
                                                                        CssClass="DefaultCheckBoxList" DataTextField="SECTIONNAME" DataValueField="SECTIONNO" RepeatDirection="Vertical" CellSpacing="5" CellPadding="2">
                                                                    </asp:CheckBoxList>
                                                                </ul>
                                                            </div>--%>
                                                        </td>
                                                        <%-- <td>
                                                            <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:HiddenField ID="hdnRoom" runat="server" Value='<%# Eval("ROOMNO")%>' />
                                                        </td>--%>
                                                        <td>
                                                            <div class="col-sm-12">
                                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" Visible="false" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%-- <asp:HiddenField ID="hdnBatch" runat="server" Value='<%# Eval("BATCHNO")%>' />--%>
                                                                <span style="display: none" class="col-sm-1">
                                                                    <asp:LinkButton ID="lnkbatch" runat="server" ToolTip="Click here for batch" Visible="false" OnClick="lnkbatch_Click" OnClientClick="batchvisible(this);"> 
                                                            <i class="fa fa-eye" aria-hidden="true" style="margin-left: -32px;"></i>
                                                                    </asp:LinkButton>
                                                                </span>
                                                                <span>
                                                                    <asp:ListBox ID="lstbxBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                                                        OnSelectedIndexChanged="lstbxBatch_SelectedIndexChanged"
                                                                        CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                                                </span>
                                                            </div>
                                                        </td>

                                                        <td style="text-align: center">

                                                            <%-- <asp:CheckBox ID="chkAddTeacher" runat="server" ToolTip='<%# Eval("COURSENO")%>' Checked='<%# Eval("IS_ADTEACHER").ToString()=="1"?true:false%>'
                                                                Visible="true" OnCheckedChanged="chkAddTeacher_CheckedChanged" AutoPostBack="true" />--%>

                                                            <%--   <asp:HiddenField ID="hdnAddTeacher" runat="server" Value='<%# Eval("IS_ADTEACHER")%>' />--%>

                                                            <asp:ListBox ID="lstbxAdTeacher" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" TabIndex="6"
                                                                CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>

                                                            <%--<asp:ListBox ID="lstbxAdTeacher" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>


                                                            <%-- <div id="Div3" class="dropdown mycheckbox-list">
                                                                <button class="btn btn-default dropdown-toggle" type="button" id="Button1" data-toggle="dropdown">
                                                                    Please Select
                                                                <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                                                                    <asp:CheckBoxList ID="chkbxlstADTeacher" runat="server" OnSelectedIndexChanged="chkbxlstADTeacher_SelectedIndexChanged" AutoPostBack="true"
                                                                        CssClass="DefaultCheckBoxList" DataTextField="SECTIONNAME" DataValueField="SECTIONNO" RepeatDirection="Vertical" CellSpacing="5" CellPadding="2">
                                                                    </asp:CheckBoxList>
                                                                </ul>
                                                            </div>--%>

                                                        </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyItemTemplate>
                                                        <p>No record found! </p>
                                                    </EmptyItemTemplate>
                                                </asp:ListView>


                                            </div>

                                        </div>
                                        <div id="divMsg" runat="Server">
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnFilter" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
                                        <%--<asp:PostBackTrigger ControlID="btnReportExcelCT" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updPanel2"
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
                                <asp:UpdatePanel ID="updPanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="div1" runat="server">
                                            <div class="col-12 mt-2">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Scheme</label>--%>
                                                            <asp:Label ID="lblDYddlColgScheme_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchemeAT" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                            ValidationGroup="teacherallot" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchemeAT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSchemeAT" runat="server" ControlToValidate="ddlSchemeAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--  <label>Session</label>--%>
                                                            <asp:Label ID="lblDYddlSession_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionAT" TabIndex="2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSessionAT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfddlSessionAT" runat="server" ControlToValidate="ddlSessionAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>School/Institute Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchoolInstituteAT" TabIndex="2" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolInstituteAT_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituteAT" Enabled="false" runat="server" ControlToValidate="ddlSchoolInstituteAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Department</label>--%>
                                                            <asp:Label ID="lblDYddlDeptName_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDeptAT" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDeptAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlDeptAT" runat="server" ControlToValidate="ddlDeptAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreeAT" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDegreeAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlDegreeAT" runat="server" ControlToValidate="ddlDegreeAT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Program/Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranchAT" runat="server" TabIndex="3" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranchAT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBranchAT" runat="server" ControlToValidate="ddlBranchAT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/ Branch" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>



                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Semester</label>--%>
                                                            <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemesterAT" runat="server" TabIndex="4" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSemesterAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSemesterAT" runat="server" ControlToValidate="ddlSemesterAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%-- <label>Course</label>--%>
                                                            <asp:Label ID="lblDYddlCourse_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSubjectAT" runat="server" TabIndex="5" AppendDataBoundItems="true" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlSubjectAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSubjectAT" runat="server" ControlToValidate="ddlSubjectAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divTutorialAT" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Allot For</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlTutorialAT" runat="server" TabIndex="6" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlTutorialAT_SelectedIndexChanged">
                                                            <%--<asp:ListItem Selected="True" Value="1">Theory</asp:ListItem>
                                                            <asp:ListItem Value="2">Tutorial</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Enabled="false" ControlToValidate="ddlTutorialAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Course" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%-- <label>Section</label>--%>
                                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSectionAT" runat="server" TabIndex="7" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            OnSelectedIndexChanged="ddlSectionAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSectionAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="BatchAT" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Batch</label>
                                                            <%-- <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBatchesAT" runat="server" TabIndex="8" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            OnSelectedIndexChanged="ddlBatchesAT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBatchesAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Batches" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="checkbox" id="divChkDeptAT" runat="server" visible="false">
                                                            <label>
                                                                <b>Other Department</b><asp:CheckBox ID="chkDeptAT" OnCheckedChanged="chkDeptAT_OnCheckedChanged" runat="server" Style="margin-left: 24px;" AutoPostBack="true" Text="" />
                                                            </label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divDropOtherDepartmentAT" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Other Department</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlOtherDepartmentAT" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOtherDepartmentAT_OnSelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="rfvOtherDepartmentAT" runat="server" ControlToValidate="ddlOtherDepartmentAT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Other Department" ValidationGroup="teacherallotAT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnFilterAT" runat="server" TabIndex="9" ValidationGroup="teacherallotAT" CssClass="btn btn-primary" OnClick="btnFilterAT_Click"><i class="fa fa-eye"></i> Show </asp:LinkButton>
                                                <asp:LinkButton ID="btnSaveAT" runat="server" TabIndex="10" CssClass="btn btn-primary" OnClick="btnSaveAT_Click" ValidationGroup="teacherallotAT" Visible="false"><i class="fa fa-save margin-r-5"></i> Submit</asp:LinkButton>
                                                <asp:Button ID="btnClearAT" runat="server" TabIndex="11" Text="Clear" CssClass="btn btn-warning" OnClick="btnClearAT_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallotAT"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class=" col-12">
                                                <asp:ListView ID="lvCourseTeacherAT" runat="server" OnItemDataBound="lvCourseTeacherAT_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>ADDITIONAL TEACHER LIST</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example2">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHeadAT" ToolTip="Select all" runat="server" onclick="totAllSubjects(this)" />
                                                                    </th>
                                                                    <th>Add. Teacher</th>
                                                                    <th id="SectionAT">
                                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <%--  <th style="text-align: center" class="hidden">Room</th>--%>
                                                                    <th id="BatchTheoryAT">Batch</th>
                                                                    <%-- <th style="text-align: center">Main Teacher</th>--%>
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
                                                                <asp:CheckBox ID="chkAcceptAT" runat="server" Enabled='<%# (Eval("Allot_AT").ToString() == "1" ? false : true)%>' onclick="totSubjects(this)" ToolTip='<%# Eval("COURSENO")%>'
                                                                    Checked='<%# Eval("Allot_AT").ToString()=="1"?true:false%>' />
                                                                <asp:HiddenField ID="hdnAllotAT" runat="server" Value='<%# Eval("Allot_AT")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("UA_FULLNAME")%>
                                                                <asp:HiddenField ID="hdnTeacherAT" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlSectionAT" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblSectionAT" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnSectionAT" runat="server" />
                                                            </td>
                                                            <%-- <td style="text-align: center" class="hidden">
                                                                <asp:DropDownList ID="ddlRoomAT" runat="server" AppendDataBoundItems="true" CssClass="form-control" Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:HiddenField ID="hdnRoomAT" runat="server" Value='<%# Eval("ROOMNO")%>' />
                                                            </td>--%>
                                                            <td>
                                                                <asp:DropDownList ID="ddlBatchAT" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblBatchAT" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdnBatchAT" runat="server" />
                                                            </td>

                                                            <%--<td style="text-align: center">
                                                            <asp:Label ID="lblTeacherAT" BackColor="AliceBlue" runat="server"></asp:Label></td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyItemTemplate>
                                                        <p>No record found! </p>
                                                    </EmptyItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div id="div2" runat="Server">
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCancelCT"
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
                                <asp:UpdatePanel ID="updCancelCT" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div id="div4" runat="server">
                                            <div class="col-12 mt-2">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Scheme</label>--%>
                                                            <asp:Label ID="lblDYddlColgScheme_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchemeCT" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                            ValidationGroup="teacherallotCT" AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchemeCT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSchemeCT" runat="server" ControlToValidate="ddlSchemeCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfv1ddlSchemeCT" runat="server" ControlToValidate="ddlSchemeCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select College & Scheme" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Session</label>--%>
                                                            <asp:Label ID="lblDYddlSession_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSessionCT" TabIndex="2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSessionCT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlSessionCT" runat="server" ControlToValidate="ddlSessionCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfv1ddlSessionCT" runat="server" ControlToValidate="ddlSessionCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>School/Institute Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSchoolInstituteCT" TabIndex="2" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSchoolInstituteCT_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituteCT" runat="server" ControlToValidate="ddlSchoolInstituteCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvSchoolInstituterCT" runat="server" ControlToValidate="ddlSchoolInstituteCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select School/Institute Name" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Department</label>--%>
                                                            <asp:Label ID="lblDYddlDeptName_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDepartmentCT" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDepartmentCT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDepartmentCT" runat="server" ControlToValidate="ddlDepartmentCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfvDepartmentrCT" runat="server" ControlToValidate="ddlDepartmentCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Department" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Degree</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreeCT" runat="server" TabIndex="2" AppendDataBoundItems="true" ValidationGroup="teacherallotCT" OnSelectedIndexChanged="ddlDegreeCT_SelectedIndexChanged"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlDegreeCT" runat="server" ControlToValidate="ddlDegreeCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfv1ddlDegreeCT" runat="server" ControlToValidate="ddlDegreeCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Programme/ Branch</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranchCT" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallotCT" TabIndex="3"
                                                            AutoPostBack="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranchCT_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlBranchCT" runat="server" ControlToValidate="ddlBranchCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/ Branch" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfv1ddlBranchCT" runat="server" ControlToValidate="ddlBranchCT" Enabled="false"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Programme/ Branch" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>



                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Semester</label>--%>
                                                            <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlsemesterCT" runat="server" AppendDataBoundItems="true" TabIndex="5" ValidationGroup="ddlsemesterCT" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlsemesterCT" runat="server" ControlToValidate="ddlsemesterCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallotCT">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="rfv1ddlsemesterCT" runat="server" ControlToValidate="ddlsemesterCT"
                                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallotrCT">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnShowCT" TabIndex="6" runat="server" ValidationGroup="teacherallotCT" CssClass="btn btn-primary" OnClick="btnShowCT_Click"><%--<i class="fa fa-eye"></i> --%>Show </asp:LinkButton>
                                                <asp:LinkButton ID="btnSubmitCT" TabIndex="7" runat="server" OnClientClick="return confirm ('Do you really want to cancel the selected faculties subject allotment!');" CssClass="btn btn-primary" OnClick="btnSubmitCT_Click" ValidationGroup="teacherallotCT" Visible="false"><i class="fa fa-save margin-r-5"></i> Submit</asp:LinkButton>

                                                <asp:LinkButton ID="btnReportCT" runat="server" TabIndex="8"
                                                    OnClick="btnReportCT_Click" ValidationGroup="teacherallotrCT" CssClass="btn btn-info" Visible="false">
                                                    <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Report
                                                </asp:LinkButton>

                                                <asp:Button ID="btnCancelCT" runat="server" Text="Clear" CssClass="btn btn-warning" OnClick="btnCancelCT_Click" />
                                                <asp:ValidationSummary ID="vsfteacherallotCT" runat="server" ValidationGroup="teacherallotCT"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="teacherallotrCT"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class=" col-12">
                                                <asp:ListView ID="lvCourseTeacherCT" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>ALLOTTED TEACHER LIST</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example3">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Sr.No</th>
                                                                    <th style="text-align: center">
                                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th style="text-align: center">
                                                                        <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th style="text-align: center">Main Teacher</th>
                                                                    <th style="text-align: center">Add. Teacher</th>
                                                                    <th style="text-align: center">
                                                                        <asp:Label ID="lblDYddlSection_Tab" runat="server" Font-Bold="true"></asp:Label></th>
                                                                    <th style="text-align: center" class="hidden">Room</th>
                                                                    <th style="text-align: center">Batch</th>
                                                                    <th style="text-align: center">Cancel Status</th>
                                                                    <%-- <th>
                                                                        <asp:CheckBox ID="cbHeadAT" ToolTip="Select all" runat="server" onclick="totAllSubjects(this)" />
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
                                                            <td><%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("UA_NO")%>' />
                                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("CT_NO")%>' />
                                                                <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("SECTIONNO")%>' />
                                                                <asp:HiddenField ID="HiddenField5" runat="server" Value='<%# Eval("ROOMNO")%>' />
                                                                <asp:HiddenField ID="HiddenField6" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                            </td>
                                                            <td>
                                                                <%# Eval("COURSE_NAME")%>
                                                                
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbltdCourseType" Text='<%# Eval("SUBNAME")%>' runat="server" Font-Bold="true"></asp:Label>

                                                            </td>
                                                            <td>
                                                                <%# Eval("MAIN_TEACHER")%>
                                                                
                                                            </td>
                                                            <td>
                                                                <%# Eval("AD_TEACHER")%>
                                                                
                                                            </td>
                                                            <td>
                                                                <%# Eval("SECTION")%>
                                                                
                                                            </td>
                                                            <td class="hidden">
                                                                <%# Eval("ROOMNAME")%>
                                                                
                                                            </td>
                                                            <td>
                                                                <%# Eval("BATCH")%>
                                                                
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="chkCanAllotTea" runat="server" ToolTip='<%# Eval("CT_NO")%>' Checked='<%# Eval("CANCEL").ToString()=="1"?true:false%>'
                                                                    AutoPostBack="true" OnCheckedChanged="chkCanAllotTea_CheckedChanged" Enabled='<%# (Eval("CANCEL").ToString() == "1" ? false : true)%>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <EmptyItemTemplate>
                                                        <p>No record found! </p>
                                                    </EmptyItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                        <div id="div5" runat="Server">
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

    <%--    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchkAT) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchkAT.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }


        //function SelectSelectAllCheckButton(chk, ListViewID) {

        //    var ListViewID = document.getElementById(ListViewID);

        //    for (var i = 1; i < ListViewID.rows.length; i++) {

        //        var inputs = ListViewID.rows[i].getElementById(ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl9_chkAccept);

        //        if (inputs[0] != undefined) {

        //            if (inputs[0].getElementById() == "checkbox") {

        //                if (chk.checked == true) {

        //                    inputs[0].checked = true;

        //                }
        //                else {
        //                    inputs[0].checked = false;
        //                }

        //            }

        //        }

        //    }

        //}

        function SelectAll(obj) {

            // find the index of column
            var table = $(obj).closest('table'); // find the current table
            var th_s = table.find('th'); // find/get all the <th> of current table
            var current_th = $(obj).closest('th'); // get current <th>

            // the value of n is "1-indexed", meaning that the counting starts at 1
            var columnIndex = th_s.index(current_th) + 1; // find index of current <th> within list of <th>'s

            //  console.log('The Column is = ' + columnIndex); // print the index for testing

            // select all checkboxes from the same column index of the same table
            table.find('td:nth-child(' + (columnIndex) + ') input').prop("checked", obj.checked);
        }

        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                var chkTeacher = $("[id*=chkAddTeacher]", List);
                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.  doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                        chkTeacher.removeAttr("checked");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");
                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');
                        $("select", td).val('0');
                        chkTeacher.removeAttr("checked");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                    $("input[type=text]", td).val('');

                    $("select", td).attr("disabled", "disabled");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");
                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                var chkTeacher = $("[id*=chkAddTeacher]", List);
                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                        chkTeacher.removeAttr("checked");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");
                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');
                        $("select", td).val('0');
                        chkTeacher.removeAttr("checked");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");

                    $("input[type=text]", td).val('');
                    $("select", td).attr("disabled", "disabled");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");
                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });
    </script>--%>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchkAT) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchkAT.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

        function SelectAll(obj) {

            // find the index of column
            var table = $(obj).closest('table'); // find the current table
            var th_s = table.find('th'); // find/get all the <th> of current table
            var current_th = $(obj).closest('th'); // get current <th>

            // the value of n is "1-indexed", meaning that the counting starts at 1
            var columnIndex = th_s.index(current_th) + 1; // find index of current <th> within list of <th>'s

            //  console.log('The Column is = ' + columnIndex); // print the index for testing

            // select all checkboxes from the same column index of the same table
            table.find('td:nth-child(' + (columnIndex) + ') input').prop("checked", obj.checked);
        }

        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                var chkTeacher = $("[id*=chkAddTeacher]", List);
                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.  doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                        chkTeacher.removeAttr("checked");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");
                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');
                        $("select", td).val('0');
                        chkTeacher.removeAttr("checked");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");
                    $("input[type=text]", td).val('');

                    $("select", td).attr("disabled", "disabled");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");
                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                var chkTeacher = $("[id*=chkAddTeacher]", List);
                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");
                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                        chkTeacher.removeAttr("checked");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");
                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');
                        $("select", td).val('0');
                        chkTeacher.removeAttr("checked");
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=chkAccept]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in the Row
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");

                    $("input[type=text]", td).val('');
                    $("select", td).attr("disabled", "disabled");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");
                    $("select", td).removeAttr("disabled");
                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=chkAccept]", List).length == $("[id*=chkAccept]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });
    </script>

    <script type="text/javascript">
        //$(function () {
        //    $.ajax({
        //        type: "POST",
        //        url: "CourseAllotment.aspx/GetBatches",
        //        data: '{}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (r) {
        //            if (document.getElementById('ctl00_ContentPlaceHolder1_lvCourseTeacher_example2') != null)
        //                var dataRows = document.getElementById('ctl00_ContentPlaceHolder1_lvCourseTeacher_example2').getElementsByTagName('tr');

        //            var lstCustomers = $("[id*=lstCustomers]");
        //            lstCustomers.empty();
        //            $.each(r.d, function () {
        //                // lstCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
        //            });
        //        }
        //    });
        //});

        //function eye() {
        //    alert('hello');
        //}

        function eye(id) {
            var btnid = "" + id.id + "";
            var myArray = new Array();
            myArray = btnid.split('_');
            var index = myArray[3];
            var str = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxSections").value;

            if (str.length <= 0) {
                alert("Please select section.");
                btnid.disabled = true;
                return false
            }
            else
                return true;
            //if ($("#" + btnid + " i").hasClass("fa fa-eye-slash")) {
            //    $("#" + btnid + " i").addClass("fa fa-eye");
            //}
            //else if ($("#" + btnid + " i").hasClass("fa fa-eye")) {

            //}
            // id.att()
        }

        //function batchvisible(id) {
        //    debugger;
        //    var btnid = "" + id.id + "";
        //    var myArray = new Array();
        //    myArray = btnid.split('_');
        //    var index = myArray[3];
        //    var section = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxSections").value;

        //    var section1 = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxSections");
        //    var str = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxBatch").value;
        //    var str1 = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxBatch");
        //    var j;
        //    var s = new Array();
        //    for (j = 0; j < section1.length; j++) {
        //        if (section1.options[j].selected == true) {
        //            section = section1.options[j].value;

        //            var i;
        //            for (i = 0; i < str1.length; i++) 
        //            {
        //                // alert(str1.options[i].value);
        //                var mybatch = new Array();
        //                mybatch = (str1.options[i].value).split('-');
        //                var index = mybatch[0];
        //                var val1 = str1.options[i].value;
        //                s.push(mybatch[1]);
        //                if (section !== index && $.inArray(mybatch[1], s) != -1) {
        //                    //alert(val1);
        //                   // alert(index);
        //                    var list = ("ctl00_ContentPlaceHolder1_lvCourseTeacher_'" + index + "'_lstbxBatch");
        //                    str1.options[i].style.display = "none";
        //                }
        //                else {
        //                    str1.options[i].style.display = "block";
        //                }
        //            }
        //        }
        //    }




        //    //$("#ctl00_ContentPlaceHolder1_lvCourseTeacher_" + index + "_lstbxBatch").each(function () {
        //    //   alert($(this).val())
        //    //});

        //}

    </script>
    <%--  <script>
        function selectAll(chk) {
           // $("[id*=example2] td").closest("tr").length;
            var table = $('[id*=example2]').DataTable();
            table.destroy();
            if (chk.checked) {
                //$("[id*=example2] td").closest("tr").find(':checkbox').prop('checked', true);
                  $("[id*=example2] ").closest("td:nth-child(1)").find(':checkbox').prop('checked', true);
                //table.column(1).checkboxes.prop('checked', true);
            }
            else {
                //$("[id*=example2] ").closest("td:nth-child(1)").find(':checkbox').prop('checked', false);
                  table.column(1).checkboxes.prop('checked', false);
            }
        }
    </script>--%>
    <script>
        function selectAll(chk) {
            var totalCheckboxes = $('[id*=example2] td input:checkbox').length;
            for (var i = 0; i < totalCheckboxes; i++) {
                if (chk.checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_chkAccept").checked = true;
                }
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_chkAccept").checked = false;
            }
        }
    </script>


    <script>
        function selectAll(chk) {
            var totalCheckboxes = $('[id*=tblCourse] td input:checkbox').length;
            for (var i = 0; i < totalCheckboxes; i++) {
                if (chk.checked) {
                    document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_chkAccept").checked = true;
                }
                else
                    document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_chkAccept").checked = false;
            }
        }
    </script>
    <script>
        //New Search function added by Gopal M - 18/08/2023
        function SearchFunction() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;
            var Tcount = 0;
            var Pcount = 0;
            var ODcount = 0;
            var totalcount = 0;
            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;

            input = document.getElementById("FilterData");
            filter = input.value.toLowerCase();
            table = document.getElementById("tblCourse");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {
                // td = trRow[i].getElementsByTagName("td")[3]; // 3- check name column
                td1 = trRow[i].getElementsByTagName("td")[1]; // 1- check rrn column
                // td2 = trRow[i].getElementsByTagName("td")[2]; // 2- check roll column

                if (td1) {
                    //Name search                 
                    if (namefalg == 0 ) {
                        txtValue = td1.textContent || td1.innerText;

                        if (txtValue != "") {
                            if (txtValue.toLowerCase().indexOf(filter) > -1) {
                                regnoflag = 1;
                                Tcount++;
                                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_chkAccept");
                                var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvCourseTeacher_ctrl" + i + "_hdfCourseteach");
                                //if (e != null) {
                                //    if (e.checked == true) {
                                //        Pcount++;
                                //    }
                                //    if (e.checked == false && e1.value == 1) {
                                //        ODcount++;
                                //    }
                                //}

                                trRow[i].style.display = "";

                            }
                            else {
                                trRow[i].style.display = "none";
                            }
                        }
                    }


                }
            }

        }
        //end search filter code
    </script>

</asp:Content>

