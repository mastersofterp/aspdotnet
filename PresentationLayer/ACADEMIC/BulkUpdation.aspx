<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkUpdation.aspx.cs" Inherits="ACADEMIC_BulkUpdation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Searchfilter {
            font-size: 15px !important;
            padding: 0.375rem 0.75rem !important;
            display: block !important;
            width: 100% !important;
            height: 42px !important;
            background-color: transparent !important;
            border: 1px solid #ced4da !important;
            border-radius: 0.25rem !important;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out !important;
            margin-left: -15px !important;
            margin-bottom: 5px !important;
        }
    </style>
    <script type="text/javascript">
        function numeralsOnly(evt) {
            evt = (evt) ? evt : event;
            var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
            if (charCode > 31 && (charCode < 48 || charCode > 57 || charCode >= 65)) {
                alert(" Please Enter Numbers only");
                return false;
            }
            return true;
        }

        function checkEmail(txt) {

            var email = document.getElementById('txtemail');
            //var filter = /^([a-z-0-9_\.\-])+\@(([a-z-0-9\-])+\.)+([a-z-0-9]{2,4})+$/;
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            //alert(txt.value);
            if (!filter.test(txt.value)) {
                alert('Please Enter Valid Email Address');
                //email.focus;
                return false;
            }
        }

        function checkAddress(txt) {
            //var address = document.getElementById('txtusn');

            var specialChars = /[!@#$%^&*()_+\=\[\]{};':"\\|<>?]+/;

            if (specialChars.test(txt.value)) {
                alert('Special Symbols Are Not Allowed!!!');
                txt.value = txt.value.replace(specialChars, '');
                return false;
            }
        }
        //function isSpecialKey(evt) {
        //    var charCode = (evt.which) ? evt.which : event.keyCode
        //    if ((charCode >= 65 && charCode <= 91) || (charCode >= 97 && charCode <= 123) || (charCode >= 48 || charCode <= 57))
        //        alert("Please Enter  Email only");
        //        return true;

        //    return false;

        //}


        //function IsValidEmail(email) {
        //    //Check minimum valid length of an Email.
        //    if (email.length <= 2) {
        //        return false;
        //    }
        //    //If whether email has @ character.
        //    if (email.indexOf("@") == -1) {
        //        return false;
        //    }

        //    var parts = email.split("@");
        //    var dot = parts[1].indexOf(".");
        //    var len = parts[1].length;
        //    var dotSplits = parts[1].split(".");
        //    var dotCount = dotSplits.length - 1;


        //    //Check whether Dot is present, and that too minimum 1 character after @.
        //    if (dot == -1 || dot < 2 || dotCount > 2) {
        //        return false;
        //    }

        //    //Check whether Dot is not the last character and dots are not repeated.
        //    for (var i = 0; i < dotSplits.length; i++) {
        //        if (dotSplits[i].length == 0) {
        //            return false;
        //        }
        //    }

        //    return true;
        //};
    </script>

    <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>--%>

    <style>
        #table2_filter {
            display: none;
        }


        #table1_filter {
            display: none;
        }

        #table1_sorting {
            display: inline-table;
        }

        #table3_filter {
            display: none;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent" DynamicLayout="true" DisplayAfter="0">
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
    <%-- <script>
        $(document).ready(function () {
            var table = $('#table2').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,

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
                                return $('#table2').DataTable().column(idx).visible();
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
                                                return $('#table2').DataTable().column(idx).visible();
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
                                                return $('#table2').DataTable().column(idx).visible();
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
                                                return $('#table2').DataTable().column(idx).visible();
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
                var table = $('#table2').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,

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
                                    return $('#table2').DataTable().column(idx).visible();
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
                                                    return $('#table2').DataTable().column(idx).visible();
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
                                                    return $('#table2').DataTable().column(idx).visible();
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
                                                    return $('#table2').DataTable().column(idx).visible();
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
    </script>--%>

    <%--===== Data Table Script added by gaurav =====--%>



    <asp:UpdatePanel ID="updStudent" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="dvMain" runat="server">
                <div class="row">
                    <div class="col-md-12 col-sm-12 col-12">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <%--<h3 class="box-title">Bulk Updation of Fields</h3>--%>
                                <h3 class="box-title">
                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            </div>

                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">Bulk Updation</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Import Bulk Updation</a>
                                        </li>
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">

                                        <div class="tab-pane active" id="tab_1">
                                            <%-- <asp:UpdatePanel runat="server" ID="updp">
                                     <ContentTemplate>--%>
                                            <div class="col-12" id="divGeneralInfo" style="display: block;">
                                                <div class="sub-heading">
                                                    <h5>Selection Criteria</h5>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Batch</label>--%>
                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" 
                                                            OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" 
                                                            data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="None" ErrorMessage="please select batch" 
                                                            ControlToValidate="ddlAdmBatch" ValidationGroup="teacherallot" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDept1" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <%--<label>Department Name</label>--%>
                                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div id="divDept2" runat="server" visible="false">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" 
                                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" 
                                                                data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" 
                                                                ErrorMessage="please select Department" ControlToValidate="ddlDepartment" 
                                                                ValidationGroup="teacherallot" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Degree</label>--%>
                                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" 
                                                            CssClass="form-control" data-select2-enable="true"
                                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="None" ErrorMessage="please select Degree" 
                                                            ControlToValidate="ddlDegree" ValidationGroup="teacherallot" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Programme/Branch</label>--%>
                                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" 
                                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" 
                                                            data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="None" ErrorMessage="please select Programme/Branch" 
                                                            ControlToValidate="ddlBranch" ValidationGroup="teacherallot" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Semester</label>--%>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" 
                                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" 
                                                            data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="None" ErrorMessage="please select Semester" 
                                                            ControlToValidate="ddlSemester" ValidationGroup="teacherallot" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>



                                                    <%-- Added by Sakshi On Date : 01-12-2023--%>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblCat" runat="server" Font-Bold="true">Filter</asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCat" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control"
                                                            data-select2-enable="true" OnSelectedIndexChanged="ddlCat_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Blood Group</asp:ListItem>
                                                            <asp:ListItem Value="2">DOB</asp:ListItem>
                                                            <asp:ListItem Value="3">Category</asp:ListItem>
                                                            <asp:ListItem Value="4">Caste</asp:ListItem>
                                                            <asp:ListItem Value="5">Adhaar Card No.</asp:ListItem>
                                                            <asp:ListItem Value="6">Gender</asp:ListItem>
                                                            <asp:ListItem Value="7">Mobile</asp:ListItem>
                                                            <asp:ListItem Value="8">Email</asp:ListItem>
                                                            <asp:ListItem Value="9">Student Name</asp:ListItem>
                                                            <asp:ListItem Value="10">Father Name</asp:ListItem>
                                                            <asp:ListItem Value="11">Mother Name</asp:ListItem>
                                                            <asp:ListItem Value="12">Part Time/Full Time</asp:ListItem>
                                                            <asp:ListItem Value="13">Address</asp:ListItem>
                                                            <asp:ListItem Value="14">Medium Of Instruction</asp:ListItem>
                                                            <asp:ListItem Value="15">Parents Email Id</asp:ListItem>
                                                            <asp:ListItem Value="16">Merit Number</asp:ListItem>
                                                            <asp:ListItem Value="17">Father Mobile Number</asp:ListItem>
                                                            <asp:ListItem Value="18">Mother Mobile Number</asp:ListItem>
                                                            <asp:ListItem Value="19">ABC ID</asp:ListItem>
                                                            <asp:ListItem Value="20">DTE Application ID</asp:ListItem>  <%-- Added by Gunesh Mohane On Date : 19-03-2024--%>


                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="please select Filter" 
                                                            ControlToValidate="ddlCat" ValidationGroup="teacherallot" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </div>



                                                    <div class="form-group col-12" id="trFilter" runat="server" visible="false">
                                                        <div class="label-dynamic" id="Div1" runat="server">
                                                            <sup>* </sup>
                                                            <label>Filter</label>
                                                        </div>
                                                        <div id="Div2" runat="server" class="col-12">
                                                            <asp:RadioButtonList ID="rdbCat" runat="server" RepeatDirection="vertical" RepeatColumns="3"
                                                                OnSelectedIndexChanged="rdbCat_SelectedIndexChanged" AutoPostBack="true">
                                                                <%--<asp:ListItem Value="1">College Code</asp:ListItem>
                                                    <asp:ListItem Value="2">Student Type</asp:ListItem>
                                                    <asp:ListItem Value="3">KEA Status</asp:ListItem>
                                                    <asp:ListItem Value="4">Claim Category</asp:ListItem>
                                                    <asp:ListItem Value="5">Alloted Category</asp:ListItem>
                                                    <asp:ListItem Value="6">Admission Batch</asp:ListItem>--%>
                                                                <asp:ListItem Value="7">Blood Group</asp:ListItem>
                                                                <%-- <asp:ListItem Value="8">Admission Date</asp:ListItem>
                                                    <asp:ListItem Value="9">USN No</asp:ListItem>--%>
                                                                <asp:ListItem Value="10">DOB</asp:ListItem>
                                                                <%--<asp:ListItem Value="11">CSN No</asp:ListItem>--%>
                                                                <asp:ListItem Value="23">Category</asp:ListItem>
                                                                <asp:ListItem Value="12">Caste</asp:ListItem>
                                                                <%-- <asp:ListItem Value="13">Payment Type</asp:ListItem>--%>
                                                                <%--<asp:ListItem Value="14">Academic Year</asp:ListItem>
                                                    <asp:ListItem Value="15">State Exam Rank</asp:ListItem>
                                                    <asp:ListItem Value="16">Entrance Registration/Exam Roll No</asp:ListItem>
                                                    <asp:ListItem Value="17">Order No</asp:ListItem>
                                                    <asp:ListItem Value="18">Payment Group</asp:ListItem>--%>
                                                                <asp:ListItem Value="19">Adhaar Card No.</asp:ListItem>
                                                                <asp:ListItem Value="20">Gender</asp:ListItem>
                                                                <asp:ListItem Value="21">Mobile</asp:ListItem>
                                                                <asp:ListItem Value="22">Email</asp:ListItem>
                                                                <%--Added by Nikhil Lambe on 11/03/2021--%>
                                                                <asp:ListItem Value="24">Student Name</asp:ListItem>
                                                                <asp:ListItem Value="25">Father Name</asp:ListItem>
                                                                <asp:ListItem Value="26">Mother Name</asp:ListItem>
                                                                <asp:ListItem Value="27">Part Time/Full Time</asp:ListItem>
                                                                <asp:ListItem Value="28">Address</asp:ListItem>
                                                                <%--Added by Vinay Mishra on 17/08/2023 & 28/08/2023 & 14/09/2023--%>
                                                                <asp:ListItem Value="29">Medium Of Instruction</asp:ListItem>
                                                                <asp:ListItem Value="30">Parents Email Id</asp:ListItem>
                                                                <asp:ListItem Value="31">Merit Number</asp:ListItem>
                                                                <asp:ListItem Value="32">Father Mobile Number</asp:ListItem>
                                                                <asp:ListItem Value="33">Mother Mobile Number</asp:ListItem>
                                                                <%-----------------------------------------------%>
                                                            </asp:RadioButtonList>
                                                        </div>


                                                        <div style="margin-top: 4px">
                                                            <asp:Label ID="lblAddressNote" runat="server" ForeColor="Red" Font-Bold="true" CssClass="mt-2" 
                                                                Text="Note - Use only Comma(,), Hyphen(-) and Backslash(/) characters during entering the Address/Permanent Address. 
                                                                Other special character's are not acceptable." Visible="false"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" Visible="false" runat="server" OnClientClick="return numeralsOnly(event)" 
                                                        OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-primary" ValidationGroup="teacherallot" />
                                                    <asp:Button ID="btnClear" Visible="false" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-warning" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" 
                                                        ValidationGroup="teacherallot" />
                                                </div>



                                                <div class="col-12">
                                                    <asp:Panel ID="pnlStudent" runat="server">
                                                        <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student Details</h5>
                                                                </div>
                                                                <div class="col-lg-3 col-md-6">

                                                                    <%--// Added by Sakshi M--%>
                                                                    <div class="input-group sea-rch">
                                                                        <input type="text" id="FilterData1" onkeyup="SearchFunction1()" placeholder="Search" class="Searchfilter" />
                                                                    </div>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table1">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <%--<th style="width: 10%">ADMISSION NO.
                                                                </th>--%>
                                                                            <th><%--REGISTRATION NO.--%>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>STUDENT NAME
                                                                            </th>
                                                                            <th id="OtherFields" runat="server">
                                                                                <asp:Label ID="lblFields" runat="server" Text=""></asp:Label>
                                                                            </th>
                                                                            <th id="thDivPAddress" runat="server">
                                                                                <asp:Label ID="lblPAddress" runat="server"> Permanent Address</asp:Label>
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
                                                                    <td id="Td1" runat="server" visible="false">
                                                                        <%# Eval("IDNO")%>
                                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REGNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlcat1" runat="server" AppendDataBoundItems="true" ToolTip='<%# Eval("COLUMNID")%>' 
                                                                            CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <%--<asp:ListItem Value="1">Please Select1</asp:ListItem>--%>
                                                                        </asp:DropDownList>
                                                                        <asp:TextBox ID="txtusn" runat="server" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>
                                                                        <asp:TextBox ID="txtemail" onblur="return checkEmail(this)" runat="server" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>
                                                                        <%--<asp:TextBox ID="txtMotherName" runat="server" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>--%>
                                                                        <%-- <asp:RequiredFieldValidator ID ="rfvEmail" runat="server" ControlToValidate="txtemail" InitialValue="0" SetFocusOnError="true" ValidationGroup="teacherallot" ErrorMessage="Please Enter valid Email Address" Display="None"> </asp:RequiredFieldValidator>--%>
                                                                        <asp:TextBox ID="txtAdmDate" runat="server" Style="text-align: right" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>
                                                                        <asp:Image ID="imgFrmDt" runat="server" ImageUrl="~/images/calendar.png" Width="16px" />
                                                                        <asp:TextBox ID="txtLAdd" runat="server" Text='<%#Eval("COLUMNNAME")%>' onkeyup="return checkAddress(this)"></asp:TextBox>
                                                                        <%--<asp:Textbox id="txtpadd" runat="server" text='<%#Eval("PCOLUMNNAME")%>'></asp:Textbox>--%>
                                                                        <ajaxToolKit:CalendarExtender ID="ceFrmDt" runat="server" Enabled="true" CssClass="Calendar"
                                                                            EnableViewState="true" Format="dd/MM/yyyy" PopupButtonID="imgFrmDt" TargetControlID="txtAdmDate" />
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeFrmDt" runat="server" Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus"
                                                                            MessageValidatorTip="true" MaskType="Date" AcceptNegative="None" ErrorTooltipEnabled="true"
                                                                            TargetControlID="txtAdmDate" OnInvalidCssClass="errordate" ClearMaskOnLostFocus="true" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevFrmDt" runat="server" ControlExtender="meeFrmDt"
                                                                            ControlToValidate="txtAdmDate" EmptyValueMessage="Please Enter Admission Date."
                                                                            IsValidEmpty="true" ErrorMessage="Please Enter Valid Date In format" EmptyValueBlurredText="*"
                                                                            InvalidValueMessage="Please Enter Valid Date In format" Display="None" ValidationGroup="Show"
                                                                            SetFocusOnError="true" />
                                                                    </td>

                                                                    <td id="tdDivPAddress" runat="server">
                                                                        <asp:TextBox ID="txtpadd" runat="server" Text='<%#Eval("PCOLUMNNAME")%>' onkeyup="return checkAddress(this)"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlStudFather" runat="server" Visible="false">
                                                        <asp:ListView ID="lvStudFather" runat="server" OnItemDataBound="lvStudFather_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student Details</h5>

                                                                </div>
                                                                <%--<div class="row mb-1">
                                                        <div class="col-lg-2 col-md-6 offset-lg-7">
                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                        </div>


                                                        <div class="col-lg-3 col-md-6">
                                                            <div class="input-group sea-rch">
                                                                <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-search"></i>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>--%>

                                                                <%--<div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                       <%-- <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>--%>
                                                                <%-- <th style="width: 10%">ADMISSION NO.=======</div>--%>
                                                                <div class="col-lg-3 col-md-6">
                                                                    <div class="input-group sea-rch">
                                                                        <input type="text" id="FilterData2" onkeyup="SearchFunction2()" placeholder="Search" class="Searchfilter" />
                                                                    </div>

                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table2">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <%--<th style="width: 10%">ADMISSION NO.
>>>>>>> 568130c06637925b362696670cb66e2188674cd7
                                                                </th>--%>
                                                                            <th>Sr.No.</th>
                                                                            <th><%--REGISTRATION NO.--%>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>NAME
                                                                            </th>
                                                                            <th>FIRST NAME</th>
                                                                            <th>MIDDLE NAME</th>
                                                                            <th>LAST NAME</th>
                                                                            <%--  <th style="width: 25%" id="OtherFields" runat="server">
                                                                    <asp:Label ID="lblFields" runat="server" Text=""></asp:Label>
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
                                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                                    <td id="Td1" runat="server" visible="false">
                                                                        <%# Eval("IDNO")%>
                                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblREGNO_Father" runat="server"> <%# Eval("REGNO")%> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Style="display: none;"><%#Eval("COLUMNNAME")%></asp:Label>
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNFIRSTNAME")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMiddle" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNMIDDLENAME")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNLASTNAME")%>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="PnlStudParentEmail" runat="server" Visible="false">
                                                        <asp:ListView ID="lvStudParentEmail" runat="server" OnItemDataBound="lvStudParentEmail_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student Details</h5>
                                                                </div>

                                                                <%--<div class="row mb-1">
                                                        <div class="col-lg-2 col-md-6 offset-lg-7">
                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                        </div>


                                                        <div class="col-lg-3 col-md-6">
                                                            <div class="input-group sea-rch">
                                                                <input type="text" id="FilterDatas" class="form-control" placeholder="Search" />
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-search"></i>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>--%>

                                                                <%--<div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="MainLeadTables">
                                                            <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                <tr>--%>
                                                                <%--<th style="width: 10%">ADMISSION NO.=======</div>--%>
                                                                <div class="col-lg-3 col-md-6">
                                                                    <div class="input-group sea-rch">
                                                                        <input type="text" id="FilterData3" onkeyup="SearchFunction3()" placeholder="Search" class="Searchfilter" />
                                                                    </div>
                                                                </div>
                                                                </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="table3">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <%--<th style="width: 10%">ADMISSION NO.>>>>>>> 568130c06637925b362696670cb66e2188674cd7s </th>--%>
                                                                <th><%--REGISTRATION NO.--%>
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <th>STUDENT NAME</th>
                                                                <th>FATHER EMAIL</th>
                                                                <th>MOTHER EMAIL</th>
                                                                <%--<th>LAST NAME</th>--%>
                                                                <%--  <th style="width: 25%" id="OtherFields" runat="server">
                                                                    <asp:Label ID="lblFields" runat="server" Text=""></asp:Label>
                                                                </th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                                <%--</div>--%>
                                                            </LayoutTemplate>

                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td id="Td1" runat="server" visible="false">
                                                                        <%# Eval("IDNO")%>
                                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNO")%>'
                                                                            Visible="false" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REGNO")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("STUDNAME")%>
                                                                    </td>
                                                                    <%--<td>
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNNAME")%>'></asp:TextBox>
                                                        </td>--%>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFatherEmail" onblur="return checkEmail(this)" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNNAMEFATHEREMAIL")%>'></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMotherEmail" onblur="return checkEmail(this)" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNNAMEMOTHEREMAIL")%>'></asp:TextBox>
                                                                    </td>
                                                                    <%--<td>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" Text='<%#Eval("COLUMNLASTNAME")%>'></asp:TextBox>
                                                        </td>--%>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <%--</ContentTemplate>
                                </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="tab-pane fade" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateImport" runat="server" AssociatedUpdatePanelID="updpnlImportData"
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
                                            <asp:UpdatePanel ID="updpnlImportData" runat="server">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3">
                                                        <div class="sub-heading">
                                                            <h5>Import Excel</h5>
                                                        </div>

                                                        <%-- Added by Gunesh Mohane on 04-03-2024--%>
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-5 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true">Select Data</asp:Label>
                                                                </div>
                                                                <asp:ListBox ID="ddlfilter1" runat="server" SelectionMode="Multiple" AppendDataBoundItems="true"
                                                                    data-select2-enable="true" onchange="limitSelection()" AutoPostBack="true" TabIndex="1" 
                                                                    ToolTip="Please Select Data">
                                                                    <asp:ListItem Value="0" Disabled="true">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Blood Group</asp:ListItem>
                                                                    <asp:ListItem Value="2">DOB</asp:ListItem>
                                                                    <asp:ListItem Value="3">Category</asp:ListItem>
                                                                    <asp:ListItem Value="4">Caste</asp:ListItem>
                                                                    <asp:ListItem Value="5">Adhaar Card No.</asp:ListItem>
                                                                    <asp:ListItem Value="6">Gender</asp:ListItem>
                                                                    <asp:ListItem Value="7">Mobile</asp:ListItem>
                                                                    <asp:ListItem Value="8">Email</asp:ListItem>
                                                                    <asp:ListItem Value="9">Student Name</asp:ListItem>
                                                                    <asp:ListItem Value="10">Father Name</asp:ListItem>
                                                                    <asp:ListItem Value="11">Mother Name</asp:ListItem>
                                                                    <asp:ListItem Value="12">Part Time/Full Time</asp:ListItem>
                                                                    <asp:ListItem Value="13">Address</asp:ListItem>
                                                                    <asp:ListItem Value="14">Medium Of Instruction</asp:ListItem>
                                                                    <asp:ListItem Value="15">Parents Email Id</asp:ListItem>
                                                                    <asp:ListItem Value="16">Merit Number</asp:ListItem>
                                                                    <asp:ListItem Value="17">Father Mobile Number</asp:ListItem>
                                                                    <asp:ListItem Value="18">Mother Mobile Number</asp:ListItem>
                                                                    <asp:ListItem Value="19">ABC ID</asp:ListItem>
                                                                    <asp:ListItem Value="20">DTE Application ID</asp:ListItem>
                                                                </asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="rfvFilter1" runat="server" Display="None" ErrorMessage="Please Select Data"
                                                                    ControlToValidate="ddlfilter1" ValidationGroup="teacherallot1" />
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Batch</label>--%>
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true">Admission Batch</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdm2" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" TabIndex="2" 
                                                                     ToolTip="Please Select Admission Batch" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None" ErrorMessage="Please Select Batch" ControlToValidate="ddlAdm2" ValidationGroup="teacherallot1" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree</label>--%>
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true">Degree</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddldegree1" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" TabIndex="3" 
                                                                    ToolTip="Please Select Degree" data-select2-enable="true" OnSelectedIndexChanged="ddldegree1_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None" ErrorMessage="Please Select Degree" ControlToValidate="ddldegree1" ValidationGroup="teacherallot1" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <%--<label>Programme/Branch</label>--%>
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true">Branch</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlbranch1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" TabIndex="4" 
                                                                    ToolTip="Please Select Branch" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None" ErrorMessage="please select Programme/Branch" ControlToValidate="ddlbranch1" ValidationGroup="teacherallot1" InitialValue="0">
                                                                </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <%--<label>Semester</label>--%>
                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true">Semester</asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" TabIndex="5" 
                                                                    ToolTip="Please Select Semester" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None" ErrorMessage="please select Semester" ControlToValidate="ddlSemester1" ValidationGroup="teacherallot1" InitialValue="0">
                                                                </asp:RequiredFieldValidator>--%>
                                                            </div>



                                                            <%-- Added by Sakshi On Date : 01-12-2023--%>
                                                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <asp:Label ID="Label6" runat="server" Font-Bold="true">Filter</asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlfilter1" runat="server" AppendDataBoundItems="true"  AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlfilter1_SelectedIndexChanged" >
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Blood Group</asp:ListItem>
                                                <asp:ListItem Value="2">DOB</asp:ListItem>
                                                <asp:ListItem Value="3">Category</asp:ListItem>
                                                <asp:ListItem Value="4">Caste</asp:ListItem>
                                                <asp:ListItem Value="5">Adhaar Card No.</asp:ListItem>
                                                <asp:ListItem Value="6">Gender</asp:ListItem>
                                                <asp:ListItem Value="7">Mobile</asp:ListItem>
                                                <asp:ListItem Value="8">Email</asp:ListItem>
                                                <asp:ListItem Value="9">Student Name</asp:ListItem>
                                                <asp:ListItem Value="10">Father Name</asp:ListItem>
                                                <asp:ListItem Value="11">Mother Name</asp:ListItem>
                                                <asp:ListItem Value="12">Part Time/Full Time</asp:ListItem>
                                                <asp:ListItem Value="13">Address</asp:ListItem>
                                                <asp:ListItem Value="14">Medium Of Instruction</asp:ListItem>
                                                <asp:ListItem Value="15">Parents Email Id</asp:ListItem>
                                                <asp:ListItem Value="16">Merit Number</asp:ListItem>
                                                <asp:ListItem Value="17">Father Mobile Number</asp:ListItem>
                                                <asp:ListItem Value="18">Mother Mobile Number</asp:ListItem>
                                                <asp:ListItem Value="19">ABC ID</asp:ListItem>


                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None" ErrorMessage="please select Filter" ControlToValidate="ddlfilter1" ValidationGroup="teacherallot1" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>--%>



                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Attach Excel File</label>
                                                                </div>
                                                                <asp:FileUpload ID="FUFile" runat="server" ToolTip="Select file to upload" TabIndex="6" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divRecords" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Already Saved Records</label>
                                                                </div>
                                                                <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <%--  Added by Gunesh Mohane --%>
                                                    <div class="col-12 btn-footer">

                                                        <%--<asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-info" TabIndex="2"
                                                        Text="Download Excel Sheet" ToolTip="Click to download excel file" Enabled="true" Visible="false" ValidationGroup="teacherallot1" OnClick="btnExport_Click"><i class="fa fa-file-excel-o" ></i> Download Excel Sheet</asp:LinkButton>
                                                    <asp:LinkButton ID="btnUploadexcel" runat="server"  CssClass="btn btn-primary" TabIndex="3"
                                                        Text="Upload Excel Sheet" Visible="false" ToolTip="Click to Upload" Enabled="true" OnClick="btnUploadexcel_Click" ><i class="fa fa-upload" ></i> Upload Excel</asp:LinkButton>

                                                    <br />--%>

                                                        <%--  Added by Gunesh Mohane --%>
                                                        <asp:LinkButton ID="btnExport2" runat="server" CssClass="btn btn-info" TabIndex="7"
                                                            Text="Download Excel Sheet" ToolTip="Click to download excel file" Enabled="true" ValidationGroup="teacherallot1" OnClick="btnExport2_Click"><i class="fa fa-file-excel-o" ></i> Download Excel Sheet</asp:LinkButton>
                                                        <asp:LinkButton ID="btnUploadexcel2" runat="server" CssClass="btn btn-primary" TabIndex="8"
                                                            Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true" OnClientClick="return validateFileUpload();"
                                                            OnClick="btnUploadexcel2_Click"><i class="fa fa-upload"></i> Upload Excel
                                                        </asp:LinkButton>
                                                        <asp:Button ID="btnClear2" runat="server" Text="Clear" ToolTip="Click to Clear Fields" TabIndex="9" OnClick="btnClear2_Click" CssClass="btn btn-warning" />
                                                        <asp:HiddenField ID="TabName" runat="server" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                            ShowSummary="False" ValidationGroup="teacherallot1" Style="text-align: center" />
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnExport2" />
                                                    <asp:PostBackTrigger ControlID="btnUploadexcel2" />
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
        </ContentTemplate>

    </asp:UpdatePanel>

    <script>

        // function toggleSearch(searchBar, table) {
        //     var tableBody = table.querySelector('tbody');
        //     var allRows = tableBody.querySelectorAll('tr');
        //     var val = searchBar.value.toLowerCase();
        //     allRows.forEach((row, index) => {
        //         var insideSearch = row.innerHTML.trim().toLowerCase();
        //     //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
        //     if (insideSearch.includes(val)) {
        //         row.style.display = 'table-row';
        //     }
        //     else {
        //         row.style.display = 'none';
        //     }



        // });
        // }

        // function test6() {
        //     var searchBar6 = document.querySelector('#FilterDatas');
        //     var table6 = document.querySelector('#MainLeadTables');



        //     //console.log(allRows);
        //     searchBar6.addEventListener('focusout', () => {
        //         toggleSearch(searchBar6, table6);
        // });

        // $(".saveAsExcel").click(function () {

        //     //if (confirm('Do You Want To Apply for New Program?') == true) {
        //     // return false;
        //     //}
        //     var workbook = XLSX.utils.book_new();
        //     var allDataArray = [];
        //     allDataArray = makeTableArray(table6, allDataArray);
        //     var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
        //     workbook.SheetNames.push("Test");
        //     workbook.Sheets["Test"] = worksheet;
        //     XLSX.writeFile(workbook, "StudentLeadData.xlsx");
        // });
        // }

        // function makeTableArray(table, array) {
        //     var allTableRows = table.querySelectorAll('tr');
        //     allTableRows.forEach(row => {
        //         var rowArray = [];
        //     if (row.querySelector('td')) {
        //         var allTds = row.querySelectorAll('td');
        //         allTds.forEach(td => {
        //             if (td.querySelector('span')) {
        //         rowArray.push(td.querySelector('span').textContent);
        //     }
        //     else if (td.querySelector('input')) {
        //         rowArray.push(td.querySelector('input').value);
        //     }
        //     else if (td.querySelector('select')) {
        //         rowArray.push(td.querySelector('select').value);
        //     }
        //     else if (td.innerText) {
        //         rowArray.push(td.innerText);
        //     }
        //     else{
        //         rowArray.push('');
        //     }
        // });
        // }
        // if (row.querySelector('th')) {
        //     var allThs = row.querySelectorAll('th');
        //     allThs.forEach(th => {
        //         if (th.textContent) {
        //     rowArray.push(th.textContent);
        // }
        // else {
        //     rowArray.push('');
        // }
        // });
        // }
        // // console.log(allTds);



        // array.push(rowArray);
        // });
        // return array;
        //}
    </script>

    <script>

        <%--  Added by Gunesh Mohane on 08-03-2024 --%>
        function validateFileUpload() {
            var fuFile = document.getElementById('<%= FUFile.ClientID %>');
            var btnUploadexcel2 = document.getElementById('<%= btnUploadexcel2.ClientID %>');

            // Check if a file is selected
            if (fuFile.files.length === 0) {
                alert('Please select the Excel File to Upload');
                return false;
            }
            var fileName = fuFile.files[0].name;
            var fileExtension = fileName.split('.').pop().toLowerCase();
            if (fileExtension !== 'xlsx' && fileExtension !== 'xls') {
                alert('Please select a valid Excel file with extension .xlsx or .xls');
                fuFile.value = '';
                return false;
            }
            return true;
        }

        <%--  Added by Gunesh Mohane on 08-03-2024 --%>
        function limitSelection() {
            var selectedCount = $('#<%= ddlfilter1.ClientID %> option:selected').length;

            if (selectedCount > 5) {
                alert('Maximum 5 Selections are Allowed!');
                // Deselect the last selected item
                $('#<%= ddlfilter1.ClientID %> option:selected:last').prop('selected', false);
                }
            }



        function SearchFunction1() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;
            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;

            input = document.getElementById("FilterData1");
            filter = input.value.toLowerCase();
            table = document.getElementById("table1");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {
                td = trRow[i].getElementsByTagName("td")[1]; // 3- check name column
                td1 = trRow[i].getElementsByTagName("td")[0]; // 1- check rrn column
                //td2 = trRow[i].getElementsByTagName("td")[3]; // 2- check roll column

                if (td) {
                    //Name search
                    if (regnoflag == 0 && rollnoflag == 0) {
                        txtValue = td.textContent || td.innerText;
                        // alert(txtValue);
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            namefalg = 1;

                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hdfLeaveStatus");
                            //if (e != null) {
                            //    if (e.checked == true) 
                            //    {
                            //        Pcount++;
                            //    }
                            //    if (e.checked == false && e1.value == 1) 
                            //    {
                            //        ODcount++;
                            //    }
                            //}
                            trRow[i].style.display = "";
                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }
                    //Regno search
                    if (namefalg == 0 && rollnoflag == 0) {
                        txtValue = td1.textContent || td1.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            regnoflag = 1;

                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudents_ctrl" + i + "_hdfLeaveStatus");
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

                    ////Roll No search
                    //if (namefalg == 0 && regnoflag == 0) {
                    //    txtValue = td2.textContent || td2.innerText;
                    //    alert(td2.innerText);
                    //    if (txtValue.toLowerCase().indexOf(filter) > -1) {
                    //        rollnoflag = 1;
                    //        Tcount++;
                    //        var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                    //        var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                    //        //if (e != null) {
                    //        //    if (e.checked == true) {
                    //        //        Pcount++;
                    //        //    }
                    //        //    if (e.checked == false && e1.value == 1) {
                    //        //        ODcount++;
                    //        //    }
                    //        //}
                    //        trRow[i].style.display = "";
                    //    }
                    //    else {
                    //        trRow[i].style.display = "none";
                    //    }
                    //}

                }
            }

        }

        function SearchFunction2() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;

            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;
            var nameflags = 0;
            input = document.getElementById("FilterData2");
            filter = input.value.toLowerCase();
            table = document.getElementById("table2");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {
                td = trRow[i].getElementsByTagName("td")[1];  // 1- check rrn column 
                td1 = trRow[i].getElementsByTagName("td")[2]; // 3- check name column
                //td2 = trRow[i].getElementsByTagName("td")[4]; // 2- check roll column

                if (td) {
                    //Regno search    
                    if (namefalg == 0 && rollnoflag == 0) {
                        txtValue = td.textContent || td.innerText;

                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            regnoflag = 1;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_hdfLeaveStatus");
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
                    //Name search
                    if (regnoflag == 0 && rollnoflag == 0) {
                        txtValue = td1.textContent || td1.innerText;

                        if (txtValue.toLowerCase().indexOf(filter) > -1) {

                            namefalg = 1;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_hdfLeaveStatus");
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

                    //Roll No search
                    //if (namefalg == 0 && regnoflag == 0) {
                    //    txtValue = td2.textContent || td2.innerText;
                    //    alter(3);
                    //    if (txtValue.toLowerCase().indexOf(filter) > -1) {
                    //        rollnoflag = 1;
                    //        Tcount++;
                    //        var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_cbRow");
                    //        var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudFather_ctrl" + i + "_hdfLeaveStatus");
                    //        //if (e != null) {
                    //        //    if (e.checked == true) {
                    //        //        Pcount++;
                    //        //    }
                    //        //    if (e.checked == false && e1.value == 1) {
                    //        //        ODcount++;
                    //        //    }
                    //        //}
                    //        trRow[i].style.display = "";
                    //    }
                    //    else {
                    //        trRow[i].style.display = "none";
                    //    }
                    //}

                }
            }
        }

        function SearchFunction3() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;
            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;

            input = document.getElementById("FilterData3");
            filter = input.value.toLowerCase();
            table = document.getElementById("table3");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {
                td = trRow[i].getElementsByTagName("td")[0];  // 1- check rrn column 
                td1 = trRow[i].getElementsByTagName("td")[1]; // 3- check name column
                td2 = trRow[i].getElementsByTagName("td")[2]; // 2- check roll column

                if (td) {
                    //Regno search    
                    if (namefalg == 0 && rollnoflag == 0) {
                        txtValue = td.textContent || td.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            regnoflag = 1;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudParentEmail_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudParentEmail_ctrl" + i + "_hdfLeaveStatus");
                            trRow[i].style.display = "";
                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }
                    //Name search
                    if (regnoflag == 0 && rollnoflag == 0) {
                        txtValue = td1.textContent || td1.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {

                            namefalg = 1;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudParentEmail_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudParentEmail_ctrl" + i + "_hdfLeaveStatus");
                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                    ////Roll No search
                    //if (namefalg == 0 && regnoflag == 0) {
                    //    txtValue = td2.textContent || td2.innerText;
                    //    if (txtValue.toLowerCase().indexOf(filter) > -1) {
                    //        rollnoflag = 1;
                    //        Tcount++;
                    //        var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                    //        var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                    //        if (e != null) {
                    //            if (e.checked == true) {
                    //                Pcount++;
                    //            }
                    //            if (e.checked == false && e1.value == 1) {
                    //                ODcount++;
                    //            }
                    //        }
                    //        trRow[i].style.display = "";
                    //    }
                    //    else {
                    //        trRow[i].style.display = "none";
                    //    }
                    //}

                }
            }

        }

    </script>
</asp:Content>
