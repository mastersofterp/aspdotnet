<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Invoice_Entry.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="Stores_Transactions_Stock_Entry_Str_Invoice_Entry"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%--<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<link href="<%#Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%#Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>--%>
    <link href="../../../plugins/multi-select/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../plugins/multi-select/bootstrap-multiselect.js"></script>

    <%-- <style>
        .multiselect-container.dropdown-menu.show {
            height:200px;
            overflow:scroll;
        }
    </style>--%>

    <%--  <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>

    <script>
        $(document).ready(function () {
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 3];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#mytable').DataTable().column(idx).visible();
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
                                            var arr = [0, 3];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
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
                                            var arr = [0, 3];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
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
                                            var arr = [0, 3];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
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
                var table = $('#mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 3];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                var arr = [0, 3];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                var arr = [0, 3];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                var arr = [0, 3];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
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

    <script>
        //var MulSel = $.noConflict();


        //------------05/05/2022-start---------------

        //$(document).ready(function () {
        //    $('.multi-select-demo').multiselect();
        //    $('.multiselect').css("width", "100%");
        //    $(".multiselect-container").css("width", "110%");
        //    $(".multiselect-container dropdown-menu").css("width", "110% ! important");
        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    prm.add_endRequest(function () {
        //        $('.multi-select-demo').multiselect({
        //            allSelectedText: 'All',
        //            maxHeight: 200,
        //            maxWidth: '100%',
        //            includeSelectAllOption: true
        //        });
        //        $('.multiselect').css("width", "100%");
        //        $(".multiselect-container").css("width", "110%")
        //        $(".multiselect.ul").css("width", "110%");
        //        $(".multiselect-container dropdown-menu").css("width", "110%");
        //    });
        //});
        //------------05/05/2022--end--------------







        //------------05/05/2022--start--------------
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                maxWidth: '100%',
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
                    maxWidth: '100%',
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });


        //------------05/05/2022--start--------------
    </script>


    <script type="text/javascript">

        function fnAllowNumeric() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8) {
                event.keyCode = 0;
                alert("Only Numeric Value Allowed..!");
                return false;
            }
        }

        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


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
                //   InitAutoCompl();
            }

        });

        function GetPO() {

            var POarray = []
            var PO;

            var checkboxes = document.querySelectorAll("#Pordno input[type=checkbox]:checked")

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (checkboxes[i].value != 'multiselect-all') {
                    if (PO == undefined) {
                        PO = checkboxes[i].value + '$';
                    }
                    else {
                        PO += checkboxes[i].value + '$';
                    }
                }
            }

            $('#<%= hdnPO.ClientID %>').val(degreeNo);

        }


    </script>
    <script type="text/javascript">
        function AddItem() {

            document.getElementById('<%=PnlItem.ClientID%>').style.display = 'block';
            document.getElementById('<%=divAddItem.ClientID%>').style.display = 'none';
            // alert('a');
        }
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
    <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999; /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>
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



    <%-- <asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Invoice Entry</h3>
                </div>
                <asp:Panel runat="server" ID="InvPanel">
                    <div class="box-body">
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                        </div>


                        <div id="divInvoiceEtry" runat="server" visible="false">
                            <asp:Panel ID="PnlSecurityPass" runat="server" HorizontalAlign="left" Visible="true">
                                <div class="col-12">
                                    <div class="row">
                                        <div class=" col-lg-12 col-md-6 col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Invoicing Entry</h5>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divInvNumber" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice Number</label>
                                            </div>
                                            <asp:TextBox ID="txtInvoiceNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice Date</label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image12345">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--  <div class="input-group-addon">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </div>--%>
                                                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control"
                                                    ToolTip="Select Date" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInvoiceDate"
                                                    Display="None" ErrorMessage="Please Select Invoice Date" ValidationGroup="Store"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image12345" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtInvoiceDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                            </div>
                                        </div>
                                        <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>GRN Date </label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image2">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>--%>
                                        <%--   <div class="input-group-addon">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </div>--%>
                                        <%--<asp:TextBox ID="txtGRNDate" runat="server" CssClass="form-control"
                                                ToolTip="Select Date" />

                                            <ajaxToolKit:CalendarExtender ID="cetxtDepDate1" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="Image2" PopupPosition="BottomLeft" TargetControlID="txtGRNDate">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="metxtDepDate1" runat="server" AcceptNegative="Left"
                                                DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtGRNDate">
                                            </ajaxToolKit:MaskedEditExtender>
                                            <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="metxtDepDate1" ControlToValidate="txtGRNDate"
                                                IsValidEmpty="false" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select GRN Date"
                                                InvalidValueMessage="GRN Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                        </div>
                                    </div>--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>GRN Date </label>
                                            </div>


                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Div3">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--<div class="input-group-addon">
                                                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                                <%--     <asp:TextBox ID="txtGRNDate" runat="server" CssClass="form-control"
                                                    ToolTip="Select Date" />--%>

                                                <asp:TextBox ID="txtGRNDate" runat="server" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGRNDate"
                                                Display="None" ErrorMessage="Please Select GRN Date" ValidationGroup="Store"></asp:RequiredFieldValidator>--%>  <%--(29/03/2022)--%>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Div3" PopupPosition="BottomLeft" TargetControlID="txtGRNDate">
                                                </ajaxToolKit:CalendarExtender>
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtGRNDate">
                                                </ajaxToolKit:MaskedEditExtender>
                                                <%--   <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtGRNDate"
                                                IsValidEmpty="true" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select GRN Date"
                                                InvalidValueMessage="GRN Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>DM Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="Image3">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <%--   <div class="input-group-addon">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        </div>--%>
                                                <asp:TextBox ID="txtDMDate" runat="server" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="cetxtDepDate" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="Image3" PopupPosition="BottomLeft" TargetControlID="txtDMDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="metxtDepDate" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtDMDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="metxtDepDate" ControlToValidate="txtDMDate"
                                                    IsValidEmpty="false" ErrorMessage="Please Enter Valid GRN Date In [dd/MM/yyyy] format" EmptyValueMessage="Please Select DM Date"
                                                    InvalidValueMessage="DM Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                    Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>DM No. </label>
                                            </div>
                                            <asp:TextBox ID="txtDMNo" runat="server" CssClass="form-control" ValidationGroup="stores" MaxLength="95" ToolTip="Enter DM No."></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDmno" runat="server" ControlToValidate="txtDMNo"
                                                Display="None" ErrorMessage="Please Enter DM No." ValidationGroup="Store"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnListCount" runat="server" />
                                            <asp:HiddenField ID="hdnTaxableAmt" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPO" runat="server" />
                                            <asp:HiddenField ID="hdnDiscAmt" runat="server" />
                                            <asp:HiddenField ID="hdnBillAmt" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnTaxAmt" runat="server" Value="0" />

                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGRNNum" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>GRN Number</label>
                                            </div>
                                            <asp:ListBox ID="ddlGRNNumber" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                                SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlGRNNumber_SelectedIndexChanged"></asp:ListBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div2" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Vendor Name </label>
                                            </div>
                                            <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0" ControlToValidate="ddlVendor"
                                                Display="None" ErrorMessage="Please Select Vendor Name" ValidationGroup="Store"></asp:RequiredFieldValidator>
                                            <asp:HiddenField ID="hdnIndex" runat="server" />
                                            <asp:HiddenField ID="hdnBasicAmt" runat="server" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPO" runat="server" visible="true">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>PO No. </label>
                                            </div>
                                            <asp:ListBox ID="ddlPO" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control multi-select-demo"
                                                SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged"></asp:ListBox>

                                        </div>

                                        <%--//------------------------------------------------------//--%>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item Expiry Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="div4">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtItemExpiryDate" runat="server" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="div4" PopupPosition="BottomLeft" TargetControlID="txtItemExpiryDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtItemExpiryDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender3" ControlToValidate="txtItemExpiryDate"
                                                    ErrorMessage="Please Enter Valid Item Expiry Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Item Expiry Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                    Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                                <%--EmptyValueMessage="Please Select Item Expiry Date" IsValidEmpty="true"--%>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Item Warranty Date </label>
                                            </div>
                                            <div class="input-group date">
                                                <div class="input-group-addon" id="div5">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>

                                                <asp:TextBox ID="txtItemWarrentyDate" runat="server" CssClass="form-control" ToolTip="Select Date"></asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="true" EnableViewState="true"
                                                    Format="dd/MM/yyyy" PopupButtonID="div5" PopupPosition="BottomLeft" TargetControlID="txtItemWarrentyDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AcceptNegative="Left"
                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                                                    MessageValidatorTip="true" OnInvalidCssClass="errordate" TargetControlID="txtItemWarrentyDate">
                                                </ajaxToolKit:MaskedEditExtender>

                                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender4" ControlToValidate="txtItemExpiryDate"
                                                    IsValidEmpty="true" ErrorMessage="Please Enter Valid Item Warranty Date In [dd/MM/yyyy] format"
                                                    InvalidValueMessage="Item Warranty Date Is Invalid  [Enter In dd/MM/yyyy Format]" Display="None" SetFocusOnError="true"
                                                    Text="*" ValidationGroup="Store"></ajaxToolKit:MaskedEditValidator>
                                                <%--EmptyValueMessage="Please Select Item Expiry Date"--%>
                                            </div>
                                        </div>

                                        <%--//--------------------------------------------------------//--%>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" ToolTip="Enter Remark"></asp:TextBox>
                                            <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnrowcount" runat="server" />

                                        </div>


                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divPONum" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Selected PO's </label>
                                            </div>
                                            <asp:TextBox ID="txtPONum" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divGrnNumtxt" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Selected GRN Numbers</label>
                                            </div>
                                            <asp:TextBox ID="txtGrnNumbers" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>

                                        </div>
                                        <%-- //----start----29-08-2023--%>
                                        <%--   <div class="col-lg-6 col-md-6 col-12 template-btn-move-up">
                                                        <div class="form-group">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Upload Invoice<code></code></label>
                                                            </div>
                                                            <label class="filelabel ">
                                                                 <asp:FileUpload ID="Uploadinvoice" runat="server" EnableViewState="true" TabIndex="5"
                                                                 ToolTip="Click here to Attach File" />

                                                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            </label>
                                                        </div>
                                                    </div>--%>



                                        <%-- //----end----29-08-2023--%>
                                    </div>


                                </div>
                                <div class="col-12 btn-footer" id="divAddItem" runat="server" visible="true">

                                    <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-info" Text="Add Item" CausesValidation="true" OnClick="btnAddItem_Click" />



                                </div>
                            </asp:Panel>

                            <asp:Panel ID="PnlItem" runat="server" Visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                            <div class="sub-heading">
                                                <h5>Add/Edit Item Details</h5>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Item Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlItem" runat="server" data-select2-enable="true" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlItem"
                                                Display="None" ErrorMessage="Please Select Item Name." InitialValue="0" ValidationGroup="AddItem"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>*</sup>
                                                <label>Invoice Qty </label>
                                            </div>
                                            <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" ToolTip="Enter Received Qty" onkeypress="return fnAllowNumeric()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQty"
                                                Display="None" ErrorMessage="Please Enter Invoice Qty." ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="Range1" ControlToValidate="txtItemQty" MinimumValue="1" MaximumValue="2147483647" Type="Integer" runat="server" ValidationGroup="AddItem" ErrorMessage="Invoice Quantity Must Be Greater Than Zero" Display="None" />

                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divItemRemark" runat="server" visible="false">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Remark</label>
                                            </div>
                                            <asp:TextBox ID="txtItemRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSaveItem" runat="server" CssClass="btn btn-primary" Text="Save Item" ValidationGroup="AddItem" OnClick="btnSaveItem_Click" />
                                        <%--OnClientClick="return GetPO()"--%>
                                        <asp:Button ID="btnCancelItem" runat="server" Visible="false" CssClass="btn btn-warning" Text="Cancel" CausesValidation="true" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddItem" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="col-12 mb-4">
                                <asp:ListView ID="lvItem" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <div>

                                            <div class="sub-heading">
                                                <h5>Item List</h5>
                                            </div>
                                            <div class="form-group col-lg-5 col-md-6 col-12">
                                                <div class=" note-div">
                                                    <h5 class="heading">Note </h5>
                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Enter Rate And Discount Before Adding Tax</span> </p>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th></th>
                                                        <th>PO Number</th>
                                                        <th>Item Name</th>
                                                        <th>PO Qty</th>
                                                        <th>Rec. Qty</th>
                                                        <th>Invoice Qty</th>
                                                        <th>Bal Qty</th>
                                                        <th>Rate</th>
                                                        <th>Disc%</th>
                                                        <th>Disc Amt</th>
                                                        <th>Taxable Amt</th>
                                                        <th>Tax Info</th>
                                                        <th>Tax Amt</th>
                                                        <th>Bill Amt</th>
                                                        <th>Oth Info</th>
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
                                                <%--     <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/Images/delete.png"
                                                CommandArgument='<%#Eval("ITEM_SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" OnClientClick="return Confirm('Are You Sure You Want To Delete this Item?');" />
                                            <asp:HiddenField ID="hdnItemSrNo" runat="server" Value='<%# Eval("ITEM_SRNO")%>' />--%>

                                                <asp:ImageButton ID="btnDeleteItem" runat="server" CausesValidation="false" ImageUrl="~/images/delete.png"
                                                    CommandArgument='<%#Eval("ITEM_SRNO")%>' AlternateText="Delete Record" OnClick="btnDeleteItem_Click" OnClientClick="return confirm('Are You Sure You Want To Delete this Item?');" />
                                                <asp:HiddenField ID="hdnItemSrNo" runat="server" Value='<%# Eval("ITEM_SRNO")%>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblRefno" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                <asp:HiddenField ID="hdnPordno" runat="server" Value='<%# Eval("PORDNO")%>' />
                                                <asp:HiddenField ID="hdnGrnId" runat="server" Value='<%# Eval("GRNID")%>' />
                                            </td>


                                            <td>
                                                <asp:TextBox ID="lblPOQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("PO_QTY")%>'></asp:TextBox>
                                                <asp:HiddenField ID="hdnItemno" runat="server" Value='<%# Eval("ITEM_NO")%>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblReceivedQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("RECEIVED_QTY")%>'></asp:TextBox>

                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                            TargetControlID="lblReceivedQty" ValidChars=".RECEIVED_QTY">
                                                        </ajaxToolKit:FilteredTextBoxExtender>--%>


                                                <asp:HiddenField ID="hdnTechSpec" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblInvoiceQty" runat="server" CssClass="form-control" Text='<%# Eval("INV_QTY")%>' onchange="return readListViewTextBoxes();" onblur="return CalOnGRNQty(this);"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                    TargetControlID="lblInvoiceQty" ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <%--  <asp:RequiredFieldValidator ID="rfvInv" runat="server" InitialValue="0" ControlToValidate="lblInvoiceQty"
                                                            Display="None" ErrorMessage="Please Enter Invoice Quantity." ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                                <asp:HiddenField ID="hdnQualityQtySpec" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblBalQty" runat="server" CssClass="form-control" Enabled="false" Text='<%# Eval("BAL_QTY")%>'></asp:TextBox>
                                                <asp:HiddenField ID="hdnOthItemRemark" runat="server" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblRate" runat="server" CssClass="form-control" Text='<%# Eval("RATE")%>' onblur="return CalOnRate(this);"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbeRate" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                    TargetControlID="lblRate" ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <%--   <asp:RequiredFieldValidator ID="rfvRate" runat="server" InitialValue="0" ControlToValidate="lblRate"
                                                            Display="None" ErrorMessage="Please Enter Rate Amount." ValidationGroup="Store"></asp:RequiredFieldValidator>--%>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblDiscPer" runat="server" CssClass="form-control" Enabled="true" Text='<%# Eval("DISC_PER")%>' onblur="return CalOnDiscPer(this);"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftDiscper" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                    TargetControlID="lblDiscPer" ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblDiscAmt" runat="server" CssClass="form-control" Enabled="true" Text='<%# Eval("DISC_AMT")%>' onblur="return CalOnDiscAmount(this);"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftdiscamt" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars"
                                                    TargetControlID="lblDiscAmt" ValidChars=".">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblTaxableAmt" runat="server" CssClass="form-control" Text='<%# Eval("TAXABLE_AMT")%>' Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%-- <asp:Button ID="btnAddTax" runat="server" CommandArgument='<%#Eval("ITEM_NO")%>' CssClass="btn btn-primary" Text="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />--%>
                                                <asp:ImageButton runat="server" ID="btnAddTax" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add" OnClientClick="return GetTaxableAmt(this);" OnClick="btnAddTax_Click" />
                                                <asp:HiddenField ID="hdnIsTaxInclusive" runat="server" Value='<%#Eval("IsTaxInclusive") %>' />
                                                <%--30/12/2023--%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblTaxAmount" runat="server" Enabled="false" Text='<%# Eval("TAX_AMT")%>' CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="hdnIsTax" runat="server" Value='<%# Eval("IS_TAX")%>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblBillAmt" runat="server" Text='<%# Eval("BILL_AMT")%>' Enabled="false" CssClass="form-control"></asp:TextBox>
                                                <asp:HiddenField ID="hdnItemPOQty" runat="server" Value='<%# Eval("PO_QTY")%>' />
                                                <asp:HiddenField ID="hdnItemRecQty" runat="server" Value='<%# Eval("RECEIVED_QTY")%>' />
                                                <asp:HiddenField ID="hdnItemBalQty" runat="server" Value='<%# Eval("BAL_QTY")%>' />
                                                <asp:HiddenField ID="hdnItemDiscPer" runat="server" Value='<%# Eval("DISC_PER")%>' />
                                                <asp:HiddenField ID="hdnItemDiscAmt" runat="server" Value='<%# Eval("DISC_AMT")%>' />
                                                <asp:HiddenField ID="hdnItemTaxableAmt" runat="server" Value='<%# Eval("TAXABLE_AMT")%>' />
                                                <asp:HiddenField ID="hdnItemTaxAmt" runat="server" Value='<%# Eval("TAX_AMT")%>' />
                                                <asp:HiddenField ID="hdnItemBillAmt" runat="server" Value='<%# Eval("BILL_AMT")%>' />
                                            </td>
                                            <%-- <td>
                                                        <asp:TextBox ID="lblItemRemark" runat="server" CssClass="form-control" Text='<%# Eval("ITEM_REMARK")%>'></asp:TextBox>
                                                    </td>--%>
                                            <td>
                                                <asp:ImageButton runat="server" ID="btnAddOthInfo" ImageUrl="~/IMAGES/Addblue.PNG" Width="22PX" Height="22PX" CommandArgument='<%#Eval("ITEM_NO")%>' AlternateText="Add Oth Info" OnClientClick="return GetOthInfoIndex(this);" OnClick="btnAddOthInfo_Click" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>

                            </div>
                            <div class="col-12" id="divItemCount" runat="server" visible="false">
                                <div class="row">
                                    <%-----11/11/2022 Shaikh Juned - Start--%>
                                    <div class="col-lg-2 col-md-3 col-6">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Net Amount :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblNetAmtCount" runat="server"></asp:Label></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <%-----11/11/2022 Shaikh Juned - end--%>

                                    <div id="divItems" runat="server" visible="false">
                                        <div class="col-lg-2 col-md-3 col-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Number Of Items :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblItemCount" runat="server"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-2 col-md-3 col-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Total Invoice Qty  :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblItemQtyCount" runat="server"> </asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup></sup>
                                    <label>Attach File</label>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group date">
                                            <asp:FileUpload ID="Uploadinvoice" runat="server" ValidationGroup="complaint" ToolTip="Select file to upload" TabIndex="9" />
                                            <div class="input-group-addon">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                                                    CssClass="btn btn-primary"
                                                    CausesValidation="False" TabIndex="10" ToolTip="Add Attach File" />
                                                <asp:Label ID="lblResult" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnAdd" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                                <img alt="" src="loader.gif" />
                                            </div>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                            <div class="form-group col-12">
                                <div id="divAttch" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <asp:Panel ID="pnlAttachmentList" runat="server" ScrollBars="Auto" Visible="false">
                                                <asp:ListView ID="lvCompAttach" runat="server">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Delete</th>
                                                                    <th>Attachments  
                                                                    </th>
                                                                    <th>Download
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
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/delete.png"
                                                                    CommandArgument=' <%#Eval("FILENAME") %>' ToolTip="Delete Record"
                                                                    OnClientClick="javascript:return confirm('Are you sure you want to delete this file?')" OnClick="ImageButton1_Click" />
                                                            </td>

                                                            <td>
                                                                <%# Eval("DisplayFileName")%></a>
                                                            </td>

                                                            <td style="text-align: center">
                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnPreview" runat="server" OnClick="imgbtnPreview_Click" Text="Preview" ImageUrl="~/Images/action_down.png" ToolTip='<%# Eval("FILENAME") %>'
                                                                            data-toggle="modal" data-target="#preview" CommandArgument='<%# Eval("FILENAME") %>' Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>'></asp:ImageButton>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPreview" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>

                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnAddNew2" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                                <%--(29/03/2022) btnsubmit add OnClientClick="return Validate(this);" --%>
                                <%--                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Store" CausesValidation="true" OnClick="btnSubmit_Click" OnClientClick="return Validate(this);" />--%>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Store" OnClick="btnSubmit_Click" OnClientClick="return Validate(this);" />
                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnBack_Click" />
                                 <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info" TabIndex="47" Text="Report" OnClick="btnReport_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                               
                                <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" />
                            </div>
                        </div>

                        <div class="col-12">

                            <ajaxToolKit:ModalPopupExtender ID="MdlTax" runat="server" PopupControlID="pnlTaxDetail" TargetControlID="lblTax"
                                BackgroundCssClass="modalBackground" BehaviorID="mdlPopupDel" CancelControlID="ImgTax">
                            </ajaxToolKit:ModalPopupExtender>

                            <asp:Label ID="lblTax" runat="server"></asp:Label>

                            <asp:Panel ID="pnlTaxDetail" runat="server" CssClass="PopupReg" Style="display: none; height: auto; width: 50%; background: #fff; z-index: 333; box-shadow: rgba(0, 0, 0, 0.16) 0px 10px 36px 0px, rgba(0, 0, 0, 0.06) 0px 0px 0px 1px;">
                                <div class="col-12">
                                    <div class="sub-heading mt-3 mb-3">
                                        <h5>Add Details</h5>
                                        <div class="box-tools pull-right">
                                            <asp:ImageButton ID="ImgTax" runat="server" ImageUrl="~/IMAGES/delete.png" ToolTip="Close" />
                                        </div>
                                    </div>

                                    <div class="col-12" id="divTaxPopup" runat="server" visible="false">

                                        <asp:ListView ID="lvTax" runat="server">
                                            <LayoutTemplate>
                                                <div id="lgv1">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                        <thead>
                                                            <tr class="bg-light-blue">

                                                                <th>Tax Name                                                                              
                                                                </th>
                                                                <th>Tax Amount
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
                                                        <asp:Label ID="lblTaxName" runat="server" Text='<%#Eval("TAX_NAME") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdnTaxId" runat="server" Value='<%#Eval("TAXID") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="lblTaxAmount" runat="server" CssClass="form-control" Text='<%#Eval("TAX_AMOUNT") %>' onblur="CalTotTaxAmt(this)"></asp:TextBox>
                                                    </td>


                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Total Tax Amount</label>
                                                        <asp:TextBox ID="txtTotTaxAmt" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>

                                                </div>


                                                <%-- //=================================================================30/12/2023--%>
                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:CheckBox ID="chkTaxInclusive" runat="server" Checked="false" />
                                                    <label>Is Tax Inclusive</label>
                                                </div>
                                                <%-- //================================================================30/12/2023--%>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label></label>
                                                    </div>
                                                    <asp:Button ID="btnTaxSubmit" runat="server" CssClass="btn btn-primary" Text="Save Tax" OnClientClick="return GetTotTaxAmt();" OnClick="btnTaxSubmit_Click" />

                                                </div>


                                            </div>

                                        </div>

                                    </div>

                                    <div class="col-12" id="divOthPopup" runat="server" visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Technical Specification</label>
                                                </div>
                                                <asp:TextBox ID="txtTechSpec" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Quality&Qty Specification</label>
                                                </div>
                                                <asp:TextBox ID="txtQualityQtySpec" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Item Remark</label>
                                                </div>
                                                <asp:TextBox ID="txtItemRemarkOth" runat="server" CssClass="form-control" TextMode="MultiLine" />

                                            </div>


                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnSaveOthInfo" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnSaveOthInfo_Click" OnClientClick="return SaveOthInfo();" />
                                        </div>

                                    </div>

                                </div>

                            </asp:Panel>

                        </div>

                        <div class="col-12">
                            <asp:ListView ID="lvInvoiceEntry" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div>
                                        <div class="sub-heading">
                                            <h5>Invoice Entry List</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Action</th>
                                                    <th>Invoice Number</th>
                                                    <th>Invoice Date</th>
                                                    <th>GRN Date</th>
                                                    <th>Vendor Name</th>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%#Eval("INVTRNO")%>' AlternateText="Edit Record" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("INVNO")%>                                                       
                                        </td>
                                        <td>
                                            <%# Eval("INVDATE","{0:dd-MM-yyyy}")%>                                                    
                                        </td>
                                        <td>
                                            <%# Eval("GRNDATE","{0:dd-MM-yyyy}")%>
                                        </td>
                                        <td>
                                            <%# Eval("PNAME")%>
                                        </td>

                                    </tr>
                                </ItemTemplate>

                            </asp:ListView>

                        </div>

                        <div class="form-group col-lg-3 col-md-6 col-12" id="divBlob" runat="server" visible="false">
                            <asp:Label ID="lblBlobConnectiontring" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnBlobCon" runat="server" />
                            <asp:Label ID="lblBlobContainer" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hdnBlobContainer" runat="server" />
                        </div>
                    </div>
                </asp:Panel>
                <%--    //------start  08-12-2023--%>
                <asp:Panel ID="pnlReport" runat="server" Visible="false">
                    <div class="panel panel-info">
                        <%--<div class="panel-heading">Delivery Note/ Invoice Number</div>--%>

                        <div class="panel-body">
                            <div class="col-md-12">
                                <div class="col-md-5">
                                    <div class="sub-heading">
                                        <h5>Delivery Note/ Invoice Number</h5>
                                    </div>
                                    <br />
                                    <asp:DropDownList ID="ddlInv" runat="server" TabIndex="48" CssClass="form-control" AppendDataBoundItems="true">
                                        <%--AutoPostBack="false" OnSelectedIndexChanged="ddlInv_SelectedIndexChanged" --%>
                                        <asp:ListItem Enabled="true" Selected="True" Value="0" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlInv" runat="server" ControlToValidate="ddlInv"
                                        Display="None" ErrorMessage="Please Select Invoice" InitialValue="0" ValidationGroup="StoreReport"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnRpt" runat="server" Text=" Show Report" TabIndex="49" ToolTip="Click To Show Report" CssClass="btn btn-info" OnClick="btnRpt_Click" ValidationGroup="StoreReport" />
                        <asp:Button ID="Button1" runat="server" Text="Back" OnClick="Button1_Click" TabIndex="50" ToolTip="Click To Go Back" CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="vsReport" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="StoreReport" />
                    </div>
                </asp:Panel>
                <%--    //------end  08-12-2023--%>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
        <Triggers>--%>
    <%--<asp:AsyncPostBackTrigger ControlID="ddlPO" />--%>
    <%-- <asp:PostBackTrigger ControlID="btnSubmit" />--%>
    <%--  </Triggers>
    </asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function CalOnGRNQty(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");

            //vishwas

            var i = st[1].split("_lblInvoiceQty");
            // var i = st[1].split("_lblReceivedQty");      

            var index = i[0];

            //calculate Bal Qty             
            var POQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblPOQty').value;
            var RecQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblReceivedQty').value;


            var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
            var BalQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBalQty').value;
            //var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblReceivedQty').value;           //
            //if (BalQty > 0) {
            //if (GRNQty > BalQty) {
            //    //alert('Invoice Quantity Should Be Less Than or Equal to Balance Quantity ');
            //    document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value = 0;
            //    return false;
            //}
            //}  
            //if (GRNQty > POQty) {
            //    alert('Invoice Quantity Should Be Less Than or Equal to PO Quantity ');
            //    document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value = 0;
            //    return false;
            //}




            if (GRNQty == 0 || GRNQty == "") {

                //BalAmt = Number(Number(BillAmount) - Number(TotalPaid)).toFixed(2);
                //alert(BalAmt);

                // alert('Please Enter in Invoice Quantity.');
                return false;
            }

            if (Number(POQty) > 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnItemBalQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBalQty').value = (Number(POQty) - (Number(RecQty) + Number(GRNQty))).toFixed(2);
            }


            //
            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var totamount = grossAmount;// + taxamt;

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = discamt.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_btnAddTax').disabled = true;

            var score = 0;
            var ROWS = document.getElementById('<%=hdnrowcount.ClientID%>').value;
            var i = 0;

            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;

        }

        function CalOnRate(crl) {
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblRate");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;
            if (rate == 0 || rate == "") {
                // alert('Please Enter Rate Amount.');
            }
            var Discountamt = 0;
            if (Number(discount) == 0 || Number(discount) < 1)
                Discountamt = Number(discamt);
            else
                Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;

            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            var totamount = Number(grossAmount) + Number(taxamt);

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_btnAddTax').disabled = true;
        }

        function CalOnDiscPer(crl) {


            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscPer");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discount = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;

            var Discountamt = (Number(rate).toFixed(2) * Number(discount).toFixed(2) * qty) / 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - Discountamt;
            //var totamount = grossAmount;
            var totamount = Number(grossAmount) + Number(taxamt);

            if (discount == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = false;
            }
            else {

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').disabled = true;
            }
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value = Discountamt.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
        }

        function CalOnDiscAmount(crl) {

            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_lblDiscAmt");
            var index = i[0];

            var qty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
            var rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var discamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            var taxamt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;

            var Discper = (Number(discamt).toFixed(2) / (Number(rate).toFixed(2) * qty)) * 100;
            var grossAmount = (Number(rate).toFixed(2) * qty) - discamt;
            // var totamount = grossAmount;// + taxamt;  
            var totamount = Number(grossAmount) + Number(taxamt);
            var Discountamt = (Number(discamt).toFixed(2) * 1) / 1;

            if (discamt == 0) {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = false;
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscPer').disabled = true;
            }

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value = grossAmount.toFixed(2);
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTaxableAmt').value = totamount.toFixed(2);
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value = totamount.toFixed(2);
        }

        function CalTotTaxAmt(crl) {
            var TotAmount = 0;
            var ROWS = document.getElementById('<%=hdnListCount.ClientID%>').value;
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                TotAmount += Number(document.getElementById("ctl00_ContentPlaceHolder1_lvTax_ctrl" + i + "_lblTaxAmount").value);
            }
            document.getElementById('<%= txtTotTaxAmt.ClientID %>').value = TotAmount;
        }

        function GetTaxableAmt(crl) {

            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_btnAddTax");
            var index = i[0];
            if (document.getElementById('<%=ddlVendor.ClientID%>').value == 0) {
                alert("Please Select Vendor.");
                return false;
            }

            var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
            var INVQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;

            if (Number(Rate) > 0 && Number(INVQty) > 0) {
                //document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
                //document.getElementById('<%=hdnIndex.ClientID%>').value = index;
                //document.getElementById('<%=hdnBasicAmt.ClientID%>').value = (Number(Rate) * Number(INVQty)).toFixed(2);

                document.getElementById('<%=hdnDiscAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
                document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
                document.getElementById('<%=hdnTaxAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;
                document.getElementById('<%=hdnBillAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value;

                document.getElementById('<%=hdnIndex.ClientID%>').value = index;
                document.getElementById('<%=hdnBasicAmt.ClientID%>').value = (Number(Rate) * Number(INVQty)).toFixed(2);

            }
            else {
                //alert("Please Enter Invoice Qty,Rate And Discount Before Adding Taxes.");
                alert("Please Enter Invoice Qty or Rate Amount.");
                return false;
            }
        }

        function GetOthInfoIndex(crl) {
            debugger;
            var st = crl.id.split("ctl00_ContentPlaceHolder1_lvItem_ctrl");
            var i = st[1].split("_btnAddOthInfo");
            var index = i[0];
            document.getElementById('<%=hdnIndex.ClientID%>').value = index;
            document.getElementById('<%=hdnDiscAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblDiscAmt').value;
            document.getElementById('<%=txtTechSpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnTechSpec').value;
            document.getElementById('<%=txtQualityQtySpec.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnQualityQtySpec').value;
            document.getElementById('<%=txtItemRemarkOth.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_hdnOthItemRemark').value;

            document.getElementById('<%=hdnTaxableAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxableAmt').value;
            document.getElementById('<%=hdnTaxAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblTaxAmount').value;
            document.getElementById('<%=hdnBillAmt.ClientID%>').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBillAmt').value;
        }
        function SaveOthInfo() {

            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnTechSpec').value = document.getElementById('<%=txtTechSpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnQualityQtySpec').value = document.getElementById('<%=txtQualityQtySpec.ClientID%>').value;
            document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnOthItemRemark').value = document.getElementById('<%=txtItemRemarkOth.ClientID%>').value;
            document.getElementById('<%=hdnOthEdit.ClientID%>').value = '1';
        }

        function GetTotTaxAmt() {
            debugger;

            // document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
            // document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
            // var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
            // var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value
            // document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);


            //===========================30/12/2023==========================//
            if (document.getElementById('<%=chkTaxInclusive.ClientID%>').checked == false) {

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
                var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
                var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value = 0;   //30/12/2023



                //alert(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value);
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
                // var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
                var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
                var TaxableAmt = document.getElementById('<%=hdnBasicAmt.ClientID%>').value - document.getElementById('<%=hdnDiscAmt.ClientID%>').value

                var deductAmt = Number(TaxableAmt) - Number(TotTaxAmt);
                // alert('TaxableAmt=' + TaxableAmt);
                // alert('TotTaxAmt=' + TotTaxAmt);
                // alert('deductAmt=' + deductAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value = Number(deductAmt);
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(deductAmt) + Number(TotTaxAmt); //30/12/2023
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTaxInclusive').value = 1;   //30/12/2023
                //  alert(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value);



                //alert(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnItemTaxableAmt').value);

            }














            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblInvoiceQty').disabled = false;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblRate').disabled = false;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + 0 + '_lblDiscPer').disabled = true;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblDiscAmt').disabled = true;


            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxAmount').value = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value;
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_hdnIsTax').value = 1;
            //var TaxableAmt = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblTaxableAmt').value;
            //var TotTaxAmt = document.getElementById('<%=txtTotTaxAmt.ClientID%>').value
            //document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + document.getElementById('<%=hdnIndex.ClientID%>').value + '_lblBillAmt').value = Number(TaxableAmt) + Number(TotTaxAmt);

        }
        function readListViewTextBoxes() {

            var score = 0;

            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                score += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value);
            }
            document.getElementById('<%= lblItemQtyCount.ClientID %>').innerHTML = score;

        }

        function VAlidationPAyment(crl) {
            debugger

            var RowCount = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);

            var i = 0;
            for (i = 0; i < RowCount; i++) {


                var POQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblPOQty').value;
                var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblRate').value;
                var GRNQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblInvoiceQty').value;
                var BalQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + index + '_lblBalQty').value;

                if (GRNQty > POQty) {
                    alert('Invoice Qty should not be Greater Than PO Qty. ');
                    return false;
                } else if (GRNQty == 0 || GRNQty == "") {


                    alert('Please Enter In Invoice Qty.');
                    return false;
                }
                else if (GRNQty > BalQty) {
                    alert('Invoice Qty should not be Greater Than Balance Qty. ');
                    return false;
                }
                else if (Rate == 0 || Rate == "") {


                    alert('Please Enter In Rate Amount.');
                    return false;
                }

            }
        }

        //(29/03/2022)
        function Validate(crl) {


            if (document.getElementById('<%= txtDMDate.ClientID %>').value == '') {
                alert("Please Enter DM Date.");
                return false;
            }
            var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
            if (!(date_regex.test(document.getElementById('<%= txtDMDate.ClientID %>').value))) {
                alert("DM Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                return false;
            }
            if (document.getElementById('<%= txtDMNo.ClientID %>').value == '') {
                alert("Please Enter DM No.");
                return false;
            }
            if (document.getElementById('<%= ddlVendor.ClientID %>').value == 0) {
                alert("Please Select Vendor Name.");
                return false;
            }

            if ((document.getElementById('<%= txtItemExpiryDate.ClientID %>').value) == "99/99/9999") {
                alert("Item Expiry Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                return false;
            }
            if ((document.getElementById('<%= txtItemWarrentyDate.ClientID %>').value) == "99/99/9999") {
                alert("Item Warrenty Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                return false;
            }
            if ((document.getElementById('<%= txtItemWarrentyDate.ClientID %>').value) > (document.getElementById('<%= txtItemExpiryDate.ClientID %>').value))

                //var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                //  if (!(date_regex.test(document.getElementById('<%= txtItemExpiryDate.ClientID %>').value))) {
                //    alert("Item Expiry Date Is Invalidddd (Enter In [dd/MM/yyyy] Format).");
                //    return false;
                //}
                //var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                //  if (!(date_regex.test(document.getElementById('<%= txtItemWarrentyDate.ClientID %>').value))) {
                //    alert("Item Warranty Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                //    return false;
                //} 






                var ItemQtyCount = 0;
            var ROWS = Number(document.getElementById('<%=hdnrowcount.ClientID%>').value);
            var i = 0;
            for (i = 0; i < ROWS; i++) {
                debugger;
                ItemQtyCount += Number(document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value);

                var Rate = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblRate').value;
                var InvQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblInvoiceQty').value;
                var ItemName = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblItemName').innerHTML;
                var BalQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBalQty').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBalQty').value;
                var RecQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblReceivedQty').value == '' ? 0 : document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblReceivedQty').value;

                var POQty = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblPOQty').value;

                if (InvQty == '0' || InvQty == '') {
                    alert("Please Enter Invoice Qty For The Item Name : " + ItemName);
                    return false;
                }
                else if (Rate == '0' || Rate == '') {
                    alert("Please Enter Rate For The Item Name : " + ItemName);
                    return false;
                }
                else if (BalQty < 0) {
                    alert("Bal Qty Should Not Be Less Than Zero, Check For Item : " + ItemName);
                    return false;
                }

                var PoNumber = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblRefno').innerHTML;
                var RemainQty = Number(POQty) - Number(RecQty);
                if (PoNumber != '') {
                    if (Number(InvQty) > Number(POQty)) {
                        alert("Invoice Qty Should Not Be Greater Than PO Qty For Item Name " + ItemName);
                        return false;
                    }
                    if (Number(InvQty) > Number(RemainQty)) {
                        alert("Invoice Qty Should Not Be Greater Than PO Qty For Item Name " + ItemName);
                        return false;
                    }
                }

                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemPOQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblPOQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemRecQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblReceivedQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemBalQty').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBalQty').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemDiscAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblDiscAmt').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemDiscPer').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblDiscPer').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemTaxableAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblTaxableAmt').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemTaxAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblTaxAmount').value;
                document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_hdnItemBillAmt').value = document.getElementById('ctl00_ContentPlaceHolder1_lvItem_ctrl' + i + '_lblBillAmt').value;
            }

            document.getElementById('<%= lblItemQtyCount.ClientID %>').value = ItemQtyCount;

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
</asp:Content>
