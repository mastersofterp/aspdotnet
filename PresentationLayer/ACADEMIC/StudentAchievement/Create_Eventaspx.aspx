<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" Culture="en-GB" CodeFile="Create_Eventaspx.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Create_Eventaspx" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        $(document).ready(function () {
            var table = $('#tblEvent').DataTable({
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
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblEvent').DataTable().column(idx).visible();
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
                return $('#tblEvent').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
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
            var arr = [0, 9];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#tblEvent').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
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
                var table = $('#tblEvent').DataTable({
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
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblEvent').DataTable().column(idx).visible();
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
                    return $('#tblEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
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
                var arr = [0, 9];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#tblEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
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
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script src="jquery/jquery-1.10.2.js"></script>
    <script src="jquery/jquery-1.10.2.min.js"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 3px 0;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });
    </script>
    <script>
        function onlyNumericKeys(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                return false;
            }
        }

    </script>

    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnCreateevent_id" runat="server" />


            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Academic Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcademicYear" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Height="16px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlAcademicYear" runat="server" ControlToValidate="ddlAcademicYear"
                                            Display="None" ErrorMessage="Please Select Academic Year" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Category</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEventCategory" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlEvent" runat="server" ControlToValidate="ddlEventCategory"
                                            Display="None" ErrorMessage="Please Select Eevent Category" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Activity Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlActivityType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlActivity" runat="server" ControlToValidate="ddlActivityType"
                                            Display="None" ErrorMessage="Please Select Activity Type" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Title</label>
                                        </div>
                                        <asp:TextBox ID="txtEventTitle" runat="server" AutoComplete="off" MaxLength="500" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender
                                            ID="txtCharacters_FilteredTextBoxExtender"
                                            runat="server"
                                            Enabled="True" TargetControlID="txtEventTitle" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789(),- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="rfvtxtEventTitle" runat="server" ControlToValidate="txtEventTitle"
                                            Display="None" ErrorMessage="Please Enter Event Title" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Select Club</label>
                                        </div>
                                        <asp:ListBox ID="lstSelectClub" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Academic"
                                            ControlToValidate="lstSelectClub" runat="server"
                                            Display="None" ErrorMessage="Please select Club"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Conduct By</label>
                                        </div>
                                        <asp:TextBox ID="txtConductBy" runat="server" AutoComplete="off" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfctxtConductBy" runat="server" ControlToValidate="txtConductBy"
                                            Display="None" ErrorMessage="Please Enter Conduct By" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextender1" runat="server" Enabled="True" TargetControlID="txtConductBy" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789(),- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Level</label>
                                        </div>
                                        <asp:DropDownList ID="ddlEventLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlEventLevel" runat="server" ControlToValidate="ddlEventLevel"
                                            Display="None" ErrorMessage="Please Select  Event Level" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Organize By</label>
                                        </div>
                                        <asp:TextBox ID="txtOrganizeBy" runat="server" AutoComplete="off" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtOrganizeBy" runat="server" ControlToValidate="txtOrganizeBy"
                                            Display="None" ErrorMessage="Please Enter Organized by" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="Filteredtextboxextenderff" runat="server" Enabled="True" TargetControlID="txtOrganizeBy" FilterType="Custom,LowercaseLetters,UppercaseLetters" FilterMode="ValidChars" ValidChars="0123456789(),- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty Co-Ordinator</label>
                                        </div>
                                        <asp:ListBox ID="lstbxFacultyCoordinator" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Academic"
                                            ControlToValidate="lstbxFacultyCoordinator" runat="server"
                                            Display="None" ErrorMessage="Please select FacultyCoordinator"></asp:RequiredFieldValidator>
                                    </div>



                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Venue</label>
                                        </div>
                                        <asp:TextBox ID="txtVenue" runat="server" AutoComplete="off" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfctxtVenue" runat="server" ControlToValidate="txtVenue"
                                            Display="None" ErrorMessage="Please Enter Venue" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender
                                            ID="Filteredtextboxextender4"
                                            runat="server"
                                            Enabled="True"
                                            TargetControlID="txtVenue"
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789(),- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Mode</label>
                                        </div>
                                        <asp:DropDownList ID="ddlMode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">OnLine</asp:ListItem>
                                            <asp:ListItem Value="2">OffLine</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlMode" runat="server" ControlToValidate="ddlMode"
                                            Display="None" ErrorMessage="Please Select Event Mode" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Duration</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDuration" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcddlDuration" runat="server" ControlToValidate="ddlDuration"
                                            Display="None" ErrorMessage="Please Select Duration" SetFocusOnError="True"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start End Date</label>
                                        </div>
                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                        <div id="picker" class="form-control">
                                            <i class="fa fa-calendar"></i>&nbsp;
                                        <span id="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
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
                                                <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="Academic" onpaste="return false;"
                                                    TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
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
                                                    ValidationGroup="Academic" SetFocusOnError="True" />
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
                                                <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="Academic" TabIndex="4"
                                                    ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                    ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="Academic" SetFocusOnError="True" />
                                            </div>
                                        </div>

                                    </div>

                               <%--     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Event Start Time</label>
                                        </div>
                                        <div class="input-group time">
                                            <div class="input-group-addon">
                                            </div>
                                            <asp:TextBox ID="txtActivitytime" runat="server" ValidationGroup="Academic"
                                                ToolTip="Please Enter Activity Time" TextMode="Time" CssClass="form-control" Style="z-index: 0;" />
                                            <asp:RequiredFieldValidator ID="rfvtxtActivitytime" ValidationGroup="Academic"
                                                ControlToValidate="txtActivitytime" runat="server"
                                                Display="None" ErrorMessage="Please select Activity Time"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>--%>


                                         <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Event Start Time</label>
                                                            </div>
                                                            <asp:TextBox ID="txtActivitytime" runat="server"  ValidationGroup="Academic" TabIndex ="8" CssClass="form-control" ToolTip="Please Enter Activity Time."></asp:TextBox>

                                                            <ajaxToolKit:MaskedEditExtender ID="msActivitytime" runat="server" TargetControlID="txtActivitytime"
                                                                Mask="99:99" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" AcceptAMPM="true"
                                                                MaskType="Time" />
                                                            <%-- <ajaxToolKit:MaskedEditValidator ID="mevStarttime" runat="server" EmptyValueMessage="Please Enter Start Time"
                                            ControlExtender="meEndtime" ControlToValidate="txtStartTime" IsValidEmpty="false"
                                            InvalidValueMessage="Start Time is invalid" Display="None" ErrorMessage="Please Enter Start Time"
                                            InvalidValueBlurredMessage="*" SetFocusOnError="true" ValidationGroup="Submit" />--%>
                                                            <asp:RequiredFieldValidator ID="rfvtxtActivitytime" runat="server" ControlToValidate="txtActivitytime"
                                                                Display="None" ValidationGroup="Academic" ErrorMessage="Please Enter Activity Time"></asp:RequiredFieldValidator>
                                                        </div>





                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Registration Last Date</label>
                                        </div>
                                        <asp:HiddenField ID="hdnRegDate" runat="server" />
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="I1" runat="server" class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtRegistrationDate" runat="server" ValidationGroup="Academic"
                                                ToolTip="Please Enter Registration Last Date" CssClass="form-control" Style="z-index: 0;" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtRegistrationDate" PopupButtonID="I1" />

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Academic" Format="dd/MM/yyyy"
                                                ControlToValidate="txtRegistrationDate" runat="server"
                                                Display="None" ErrorMessage="Please select Registration Last Date"></asp:RequiredFieldValidator>

                                            <%--                                     <asp:CompareValidator ID="CompareValidator1" ValidationGroup = "Academic" ForeColor = "Red" runat="server" 
                                        ControlToValidate = "txtRegistrationDate" ControlToCompare = "txtStartDate" Operator="GreaterThanEqual" Type = "Date" 
                                        ErrorMessage="Registration date must be Equal to Start date."></asp:CompareValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Registration Capacity</label>
                                        </div>
                                        <asp:TextBox ID="txtRegistrationCapacity" runat="server" AutoComplete="off" MaxLength="4"
                                            CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtRegistrationCapacity" runat="server"
                                            ControlToValidate="txtEventTitle" Display="None"
                                            ErrorMessage="Please Enter Registration Capacity" SetFocusOnError="True"
                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" onchange="valiCreateEvent();" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Height="16px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" SetFocusOnError="True"
                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:ListBox ID="lstDegree" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AutoPostBack="true" onchange="valiCreateEvent();" OnSelectedIndexChanged="lstDegree_SelectedIndexChanged"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstDegree" ValidationGroup="ClubMapping" ControlToValidate="lstDegree" runat="server"
                                            Display="None" ErrorMessage="Please select Degree"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:ListBox ID="lstBranch" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" onchange="valiCreateEvent();" AutoPostBack="true" OnSelectedIndexChanged="lstBranch_SelectedIndexChanged"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstBranch" ValidationGroup="ClubMapping" ControlToValidate="lstBranch" runat="server"
                                            Display="None" ErrorMessage="Please select Branch"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Houses</label>
                                        </div>
                                        <asp:ListBox ID="lstHouses" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Academic"
                                            ControlToValidate="lstHouses" runat="server"
                                            Display="None" ErrorMessage="Please select Houses"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Funded By</label>
                                        </div>
                                        <asp:TextBox ID="txtFundedBy" runat="server" AutoComplete="off" MaxLength="1000" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender
                                            ID="Filteredtextboxextender3"
                                            runat="server"
                                            Enabled="True"
                                            TargetControlID="txtFundedBy"
                                            FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789(),- ">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>




                                    <%--   <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Registration Date</label>
                                </div>
                                <asp:HiddenField ID="hdnRegDate" runat="server" />
                                  <div id="DatePicker1" class="form-control">
                                    <i class="fa fa-calendar"></i>&nbsp;
                                        <span id="DATE"></span>
                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label></label>
                                    </div>
                                     <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i id="I1" runat="server" class="fa fa-calendar text-blue"></i>
                                        </div>
                                        <asp:TextBox ID="txtRegistrationDate" TextMode="Date" runat="server" ValidationGroup="Academic" onpaste="return false;"
                                            TabIndex="3" ToolTip="Please Enter Registration Date" CssClass="form-control" Style="z-index: 0;" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtRegistrationDate" PopupButtonID="I1" />
                                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtRegistrationDate"
                                            Display="None" ErrorMessage="Please Enter Registration Date" SetFocusOnError="True"
                                            ValidationGroup="submit" />
                                        <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtRegistrationDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                            DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeStartDate"
                                            ControlToValidate="txtRegistrationDate" EmptyValueMessage="Please Enter Start Date"
                                            InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                            TooltipMessage="Please Enter Registration Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Academic" SetFocusOnError="True" />

                                        <asp:CompareValidator ID="cvtxtStartDate" runat="server"
                                            ControlToCompare="txtRegistrationDate" CultureInvariantValues="true"
                                            Display="Dynamic" EnableClientScript="true" ValidationGroup="Academic" 
                                            ControlToValidate="txtStartDate" 
                                            
                                            ErrorMessage="Start date must be earlier than finish date"
                                            Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual"
                                            Text="Start date must be earlier than finish date"></asp:CompareValidator>
                                    </div>

                                    
                                </div>

                            </div>--%>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="InActive" for="rdActive"></label>

                                        </div>
                                    </div>


                                </div>


                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label></label>
                                    </div>
                                    <asp:CheckBox ID="chkPrizes" OnCheckedChanged="chkPrizes_CheckedChanged" AutoPostBack="true" runat="server" Text="Prizes" />
                                </div>

                                <div id="Prizes" class="row" runat="server">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label id="lblw">Winner(Amount in Rs)</label>
                                        </div>
                                        <asp:TextBox ID="txtWinner" runat="server" Enabled="false" MaxLength="10" onkeypress="return isNumber(event)" CssClass="form-control"></asp:TextBox>

                                        <ajaxToolKit:FilteredTextBoxExtender
                                            ID="Filteredtextboxextender2"
                                            runat="server"
                                            Enabled="True"
                                            TargetControlID="txtWinner"
                                            FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Runner Up(Amount in Rs)</label>
                                        </div>
                                        <asp:TextBox ID="txtRunnerUp" runat="server" AutoComplete="off" Enabled="false" MaxLength="10" onkeypress="return isNumber(event)" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Third Place(Amount in Rs)</label>
                                        </div>
                                        <asp:TextBox ID="txtThirdPlace" runat="server" AutoComplete="off" Enabled="false" MaxLength="10" onkeypress="return isNumber(event)" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Event Brochure <small style="color: red;">(Choose only PDF file)</small></label>
                                        </div>
                                        <div id="Div1" class="logoContainer" runat="server">
                                            <img src="../../Images/default-fileupload.png" alt="upload image" runat="server" id="imgUpFile" />
                                        </div>
                                        <div class="fileContainer sprite pl-1">
                                            <span runat="server" id="ufFile"
                                                cssclass="form-control" tabindex="7">Upload File</span>
                                            <%--    <asp:FileUpload ID="fuEventfile" runat="server" TabIndex="1" />--%>
                                            <asp:FileUpload ID="fuEventfile" runat="server" ToolTip="Select file to upload"
                                                CssClass="form-control" onkeypress="" onchange="return CheckFileSize(this)" />
                                              <asp:Label ID="lblfile" runat="server" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmitCreateEvent" runat="server" CssClass="btn btn-primary" ValidationGroup="Academic" OnClientClick="return valiCreateEvent();" OnClick="btnSubmitCreateEvent_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancelAcademicYear" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancelAcademicYear_Click">Cancel</asp:LinkButton>
                                <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-info" CausesValidation="false" OnClick="btnReport_Click">Excel Report</asp:LinkButton>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="Academic" />

                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Create Event List</h5>
                                </div>
                                <asp:Panel ID="pnlEvent" runat="server" Visible="false">
                                    <asp:ListView ID="lvCreateEvent" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvCreateEvent_ItemEditing">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblEvent">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th style="width: 10%;">Academic Year</th>
                                                        <th style="width: 10%;">Event Category</th>
                                                        <th style="width: 20%;">Event Title</th>
                                                        <th style="width: 10%;">Start Date</th>
                                                        <th style="width: 10%;">End Date</th>
                                                        <th style="width: 10%;">Registration Date</th>
                                                        <th style="width: 10%;">Venue</th>
                                                        <th style="width: 10%;">Organize By</th>
                                                        <th style="width: 10%;">Conduct By</th>
                                                        <th style="width: 10%;">Download</th>
                                                        <th style="width: 5%;">Activity Type</th>
                                                        <th style="width: 10%;">Club Type</th>
                                                        <%--<th style="width: 10%;">Select Club Type</th>
                                                <th style="width: 10%;">Select Houses</th>--%>
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
                                                    <asp:LinkButton ID="btnEditCreateEvent" runat="server" OnClick="btnEditCreateEvent_Click" CssClass="fas fa-edit" CausesValidation="false" CommandArgument='<%#Eval("CREATE_EVENT_ID") %>' CommandName="Edit"></asp:LinkButton>

                                                </td>
                                                <td><%# Eval("ACADMIC_YEAR_NAME") %></td>
                                                <td><%# Eval("EVENT_CATEGORY_NAME") %></td>
                                                <td><%# Eval("EVENT_TITLE") %></td>
                                                <td><%# Convert.ToDateTime (Eval("STDATE")).ToString("d") %></td>
                                                <td><%# Convert.ToDateTime (Eval("ENDDATE")).ToString("d") %></td>
                                                <td><%# Convert.ToDateTime (Eval("REGISTRATION_DATE")).ToString("d") %></td>
                                                <td><%# Eval("VENUE") %></td>
                                                <td><%# Eval("ORGANIZE_BY") %></td>
                                                <td><%# Eval("CONDUCT_BY") %></td>
                                                <td>
                                                    <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-primary" CausesValidation="false" OnClick="btnDownload_Click" CommandArgument='<%# Eval("FILE_NAME") %>' ToolTip='<%# Eval("FILE_NAME") %>' />

                                                </td>
                                                
                                                <td><%# Eval("ACTIVITY_NAME") %></td>
                                                <td><%# Eval("CLUB_ACTIVITY_TYPE") %></td>


                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCollege" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lstDegree" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lstBranch" EventName="SelectedIndexChanged" />

            <asp:PostBackTrigger ControlID="btnSubmitCreateEvent" />
            <asp:PostBackTrigger ControlID="btnCancelAcademicYear" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="lvCreateEvent" />

        </Triggers>
    </asp:UpdatePanel>


  <script>
      debugger
      $("input:file").change(function () {
          var fileName = $(this).val();
          //alert(fileName)
          newText = fileName.replace(/fakepath/g, '');
          var newtext1 = newText.replace(/C:/, '');
          //newtext2 = newtext1.replace('//', ''); 
          var result = newtext1.substring(2, newtext1.length);


          if (result.length > 0) {
              $(this).parent().children('span').html(result);
          }
          else {
              $(this).parent().children('span').html("Choose file");
          }
      });
      var parameter = Sys.WebForms.PageRequestManager.getInstance();
      parameter.add_endRequest(function () {
          $("input:file").change(function () {
              var fileName = $(this).val();
              //alert(fileName)
              newText = fileName.replace(/fakepath/g, '');
              var newtext1 = newText.replace(/C:/, '');
              //newtext2 = newtext1.replace('//', ''); 
              var result = newtext1.substring(2, newtext1.length);


              if (result.length > 0) {
                  $(this).parent().children('span').html(result);
              }
              else {
                  $(this).parent().children('span').html("Choose file");
              }
          });
      });
      //file input preview
      function readURL(input) {
          if (input.files && input.files[0]) {
              var reader = new FileReader();
              reader.onload = function (e) {
                  //$('.logoContainer img').attr('src', e.target.result);
              }
              reader.readAsDataURL(input.files[0]);
          }
      }
      $("input:file").change(function () {
          readURL(this);
      });
    </script>
        
       
 

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

    <script type="text/javascript">
        $(document).ready(function () {
         
            $('#picker').daterangepicker({
                
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {

            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });
    </script>



    <%--    For Registration Date--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#DatePicker1').daterangepicker({
                startRegDate: moment().subtract(00, 'days'),
                //endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY',
                },
                ranges: {
                },
            },
        function (start) {
            $('#DATE').html(start.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnRegDate.ClientID%>').value = (start.format('DD MMM, YYYY'))
        });

            $('#DATE').html(moment().subtract(00, 'days').format('DD MMM, YYYY'));
            document.getElementById('<%=hdnRegDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY'))
        });
    </script>


    <script>
        function SetRegistrationDate(date) {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            $(document).ready(function () {

                var startRegDate1 = moment(date.split('-')[0], "MMM DD, YYYY");

                //var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");

                $('#DATE').html(startDate.format("MMM DD, YYYY"));
                document.getElementById('<%=hdnRegDate.ClientID%>').value = date;

                $('#DatePicker1').daterangepicker({
                    startRegDate1: startRegDate1.format("MM/DD/YYYY"),
                    format: {
                    },
                    ranges: {
                    },
                },
        function (start) {
            alert(start);

            $('#DATE').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            document.getElementById('<%=hdnRegDate.ClientID%>').value = (start.format('MMM DD, YYYY'))
        });

            });
};
    </script>

    <script>
        function Setdate(date) {

            var prm = Sys.WebForms.PageRequestManager.getInstance();

            $(document).ready(function () {

                var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");

                $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                document.getElementById('<%=hdnDate.ClientID%>').value = date;

                $('#picker').daterangepicker({
                    startDate: startDate.format("MM/DD/YYYY"),
                    endDate: endtDate.format("MM/DD/YYYY"),
                    ranges: {
                    },
                },
        function (start, end) {
            alert(start);

            $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
        });

            });

        };

    </script>
    <!-- pdf upload Script -->
    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_fuEventfile").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_fuEventfile").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {

            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

      
  
    


    <script>
        function SetCreateEvent(val) {

            $('#rdActive').prop('checked', val);
        }

        function valiCreateEvent() {
            $('#hfdActive').val($('#rdActive').prop('checked'));
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCreateEvent').click(function () {
                    validate();
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



     <script type="text/javascript">
         function CheckFileSize(chk) {

             var maxFileSize = 500000;
             var fi = document.getElementById(chk.id);
             for (var i = 0; i <= fi.files.length - 1; i++) {

                 var fsize = fi.files.item(i).size;

                 if (fsize >= maxFileSize) {
                     alert("File size should not be greater than 500kb");
                     $(chk).val("");
                     return false;

                 }
             }
             var fileExtension = ['pdf'];
             if ($.inArray($(chk).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                 alert("Only formats are allowed : " + fileExtension.join(', '));
                 $(chk).val("");
             }
         }

    </script>
</asp:Content>
