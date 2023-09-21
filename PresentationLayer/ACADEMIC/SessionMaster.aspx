<%@ page language="C#" masterpagefile="~/SiteMasterPage.master" autoeventwireup="true" viewstateencryptionmode="Always" enableviewstatemac="true"
    codefile="SessionMaster.aspx.cs" inherits="Academic_SessionCreate" title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <%-- <style>
        #ctl00_ContentPlaceHolder1_pnlSession .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
        #ctl00_ContentPlaceHolder1_pnlCollegeMap .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
        .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>--%>
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblStudents').DataTable({
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
                                return $('#tblStudents').DataTable().column(idx).visible();
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
                                            return $('#tblStudents').DataTable().column(idx).visible();
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
                                            return $('#tblStudents').DataTable().column(idx).visible();
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
                var table = $('#tblStudents').DataTable({
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
                                    return $('#tblStudents').DataTable().column(idx).visible();
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
                                                return $('#tblStudents').DataTable().column(idx).visible();
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
                                                return $('#tblStudents').DataTable().column(idx).visible();
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

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
    <%--  <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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
    </div>--%>
    <asp:UpdatePanel runat="server" ID="UPDROLE" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Session Master Creation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Session College Mapping</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdMCreation" runat="server" AssociatedUpdatePanelID="UPDMASTER"
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
                                        <asp:UpdatePanel ID="UPDMASTER" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session List</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session Long Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSLongName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="3"
                                                                ToolTip="Please Enter Session Long Name" placeholder="Enter Session Long Name" />
                                                            <asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                                                ErrorMessage="Please Enter Session Long Name." ControlToValidate="txtSLongName"
                                                                Display="None" ValidationGroup="submit" />
                                                        </div>

                                                        <!--===== added by gaurav on 21-05-2021=====-->
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session Start End Date</label>
                                                                <%--<asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>--%>
                                                            </div>
                                                            <div id="picker" class="form-control" tabindex="4">
                                                                <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                                                <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session Start Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                                                    ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                                                <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtStartDate" PopupButtonID="dvcal1" />
                                                                <%-- <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                ValidationGroup="submit" />--%>
                                                                <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                                    TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                                    ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                    TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                    ValidationGroup="submit" SetFocusOnError="True" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session End Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                                                </div>
                                                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                                                <%-- <asp:Image ID="imgEDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />--%>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                    TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                                                <%-- <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                ErrorMessage="Please Enter Session End Date" ControlToValidate="txtEndDate" Display="None"
                                                ValidationGroup="submit" />--%>
                                                                <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                                    TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                                    ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                    TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="submit" SetFocusOnError="True" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session Short Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSShortName" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                                ToolTip="Please Enter Session Short Name" placeholder="Enter Session Short Name" />
                                                            <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ErrorMessage="Please Enter Session Short Name."
                                            ControlToValidate="txtSShortName" Display="None" ValidationGroup="submit" />
                                                            <asp:HiddenField ID="hdnDate" runat="server" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <label>Provisional Certificate Session</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProvisionalCertificateSessionName" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="6"
                                                                ToolTip="Please Enter Provisional Certificate Session" placeholder="Enter Provisional Certificate Session" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Provisional Certificate Session Name" ControlToValidate="txtProvisionalCertificateSessionName"
                                            Display="None" InitialValue="" ValidationGroup="submit" />--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Session Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlOddEven" runat="server" TabIndex="6" ToolTip="Please Select Odd/Even" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <label>Session Name(Hindi)</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSessName_Hindi" runat="server" CssClass="form-control" MaxLength="25" ToolTip="Please Select Status"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Exam type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="7" ToolTip="Please Select Status" CssClass="form-control" data-select2-enable="true">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Academic Year</label>
                                                            </div>
                                                            <asp:TextBox runat="server" AutoComplete="off" ID="txtacadyear" placeholder="Enter Academic Year" ToolTip="Please Enter Academic Year" CssClass="form-control" TabIndex="8" MaxLength="25"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvacadyear" runat="server" ErrorMessage="Please Enter Academic Year"
                                            ControlToValidate="txtacadyear" Display="None" ValidationGroup="submit" />
                                                        </div>

                                                        <!--===== Added By Vinay Mishra on Dated 16/06/2023=====-->
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Sequence Number</label>
                                                            </div>
                                                            <asp:TextBox runat="server" AutoComplete="off" ID="txtSeqNo" placeholder="Enter Sequence Number" ToolTip="Please Enter Sequence Number" onkeyup="validateNumericAndNotZero(this);" CssClass="form-control" TabIndex="9" MaxLength="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvseqno" runat="server" ErrorMessage="Please Enter Sequence Number"
                                            ControlToValidate="txtSeqNo" Display="None" ValidationGroup="submit" />
                                                        </div>

                                                        <!--===== Added By Rishabh on Dated 28/10/2021=====-->
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="row">
                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Status</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdActive" name="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Is Start</label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="rdStart" name="switch" checked />
                                                                        <label data-on="Start" data-off="Stop" for="rdStart"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                        OnClick="btnSubmit_Click" TabIndex="9" CssClass="btn btn-primary"  /> 
                                                   <%-- OnClientClick="return validate();"--%>
                                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report"
                                                        CssClass="btn btn-info" Style="display: none;" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                        TabIndex="10" CssClass="btn btn-warning" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                                        <asp:ListView ID="lvSession" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <%--<th>
                                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>--%>
                                                                            <th>Session Long Name
                                                                            </th>
                                                                            <th>Session Short Name
                                                                            </th>
                                                                            <th>Start Date
                                                                            </th>
                                                                            <th>End Date
                                                                            </th>
                                                                            <th>Session Type
                                                                            </th>
                                                                            <th>Academic Year
                                                                            </th>
                                                                            <th>Sequence Number
                                                                            </th>
                                                                            <th>Is Active
                                                                            </th>
                                                                            <th>Is Start
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
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("SESSIONID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            OnClick="btnEdit_Click" TabIndex="11" />
                                                                    </td>
                                                                    <%-- <td>
                                                                        <%# Eval("COLLEGE_NAME") %>
                                                                    </td>--%>
                                                                    <td>
                                                                        <%# Eval("SESSION_PNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SESSION_NAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SESSION_STDATE","{0:dd-MMM-yyyy}") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SESSION_ENDDATE", "{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ODD_EVEN")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ACADEMIC_YEAR")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SEQUENCE_NO")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="divflock" runat="server" CssClass='<%# Eval("FLOCK")%>' Text='<%# Eval("FLOCK")%>' ForeColor='<%# Eval("FLOCK").ToString().Equals("Started")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                                <asp:PostBackTrigger ControlID="btnCancel" />
                                                <asp:PostBackTrigger ControlID="btnReport" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateImport" runat="server" AssociatedUpdatePanelID="UPDCOLLEGEMAP"
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
                                        <asp:UpdatePanel ID="UPDCOLLEGEMAP" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Session </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSessionCollege" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="submit2" ToolTip="Please Select Session." data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSessionCollege" runat="server" ValidationGroup="submit2" ControlToValidate="ddlSessionCollege" Display="None"
                                                                ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>College</label>--%>
                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <%--     <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>--%>

                                                            <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit2" runat="server" Text="Submit" ValidationGroup="submit2"
                                                        TabIndex="5" CssClass="btn btn-primary" OnClientClick="return validate2();" OnClick="btnSubmit2_Click" />
                                                    <asp:Button ID="btnReport2" runat="server" Text="Report"
                                                        CssClass="btn btn-info" Style="display: none;" />
                                                    <asp:Button ID="btnCancel2" runat="server" Text="Cancel"
                                                        TabIndex="6" CssClass="btn btn-warning" OnClick="btnCancel2_Click" />

                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit2" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlCollegeMap" runat="server" Visible="false">
                                                        <asp:ListView ID="lvCollegeMap" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display1" style="width: 100%">
                                                                    <thead class="bg-light-blue" style="top:-15px">
                                                                        <tr>
                                                                            <%-- <th style="text-align: center;">Edit
                                                                            </th>--%>
                                                                            <th style="width:20%">
                                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th style="width:10%">Start Date
                                                                            </th>
                                                                            <th style="width:10%">End Date
                                                                            </th>
                                                                            <th style="width:15%">Session Long Name
                                                                            </th>
                                                                            <th style="width:15%">Session Short Name
                                                                            </th>
                                                                            <th style="width:5%">Session Type
                                                                            </th>
                                                                            <th style="width:5%">Academic Year
                                                                            </th>
                                                                            <th style="width:5%">Is Active
                                                                            </th>
                                                                            <th style="width:5%">Is Start
                                                                            </th>
                                                                            <th style="width:10%">Active/In-Active
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
                                                                    <%-- <td style="text-align: center;">
                                                                        <asp:ImageButton ID="btnEdit2" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                                            CommandArgument='<%# Eval("SESSIONNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            TabIndex="5" />
                                                                    </td>--%>
                                                                    <td style="width:20%">
                                                                        <%# Eval("COLLEGE_NAME") %>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        <%# Eval("SESSION_STDATE","{0:dd-MMM-yyyy}") %>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        <%# Eval("SESSION_ENDDATE", "{0:dd-MMM-yyyy}")%>
                                                                    </td>
                                                                    <td style="width:15%">
                                                                        <%# Eval("SESSION_PNAME")%>
                                                                    </td>
                                                                    <td style="width:15%">
                                                                        <%# Eval("SESSION_NAME")%>
                                                                    </td>
                                                                    <td style="width:5%">
                                                                        <%# Eval("ODD_EVEN")%>
                                                                    </td>
                                                                    <td style="width:5%">
                                                                        <%# Eval("ACADEMIC_YEAR")%>
                                                                    </td>
                                                                    <td style="width:5%">
                                                                        <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("IS_ACTIVE")%>' Text='<%# Eval("IS_ACTIVE")%>' ForeColor='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width:5%">
                                                                        <asp:Label ID="divflock" runat="server" CssClass='<%# Eval("FLOCK")%>' Text='<%# Eval("FLOCK")%>' ForeColor='<%# Eval("FLOCK").ToString().Equals("Started")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width:10%">
                                                                        <asp:Button ID="btnDeActive" runat="server" Text='<%# Eval("IS_ACTIVE").ToString().Equals("Active")?"In-Active":"Active" %>' CssClass="btn btn-warning"
                                                                            OnClick="btnDeActive_Click" OnClientClick='<%# Eval("IS_ACTIVE", "return getConfirmation(\"{0}\")")%>' CommandArgument='<%# Eval("SESSIONNO")%>' ToolTip='<%# Eval("IS_ACTIVE")%>'/>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmit2" />
                                                <asp:PostBackTrigger ControlID="btnCancel2" />
                                                <asp:PostBackTrigger ControlID="btnReport2" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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
        function validateNumericAndNotZero(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return false;
            }
            if (txt.value == '0') {
                txt.value = '';
                alert('Please Enter Values Greater Than Zero!');
                txt.focus();
                return false;
            }

        }
    </script>

    <script language="javascript" type="text/javascript">

        function checkSessionList() {
            var ddl = document.getElementById('<%= ddlSession.ClientID %>');

            if (ddl.value == "0") {
                alert('Please Select Session from Session List for Modifying');
                return false;
            }
            else
                return true;
        }
    </script>
    <script>
        function getConfirmation(id) {
           // alert(id);
            var check = false;
            if (id == "Active") {
                check = confirm("Do you want to In-Active the record ?");
            }
            else {
                check = confirm("Do you want to Active the record ?");
            }
            if (check == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>

    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
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
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
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
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            //var ddlCollege = $("[id$=ddlCollege]").attr("id");
            //var ddlCollege = document.getElementById(ddlCollege);
            //if (ddlCollege.value == 0) {
            //    alert('Please Select School/Institute Name.', 'Warning!');
            //    $(ddlCollege).focus();
            //    return false;
            //}

            var idtxtweb = $("[id$=txtSLongName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Long Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtSShortName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Short Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtSeqNo]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Sequence Number', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtacadyear]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Academic Year', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                   // validate();
                });
            });
        });
    </script>

    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }

        function validate2() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));


            var rfvCollege = (document.getElementById("<%=lblDYddlSchool.ClientID%>").innerHTML);

            var ddlCollege = $("[id$=ddlCollege]").attr("id");
            var ddlCollege = document.getElementById(ddlCollege);
            if (ddlCollege.value == 0) {
                alert('Please Select ' + rfvCollege + ' .\n', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(ddlCollege).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit2').click(function () {
                    validate2();
                });
            });
        });
    </script>

    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            });

                });
            });
};
    </script>
    <script>
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
        });
    </script>

    <script>
        function tab() {
            $('#tab_2').tab('show')
        };
    </script>
</asp:Content>
