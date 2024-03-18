<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="DefineAcademicYear.aspx.cs" Inherits="ACADEMIC_DefineAcademicYear" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            var table = $('#divsessionlist').DataTable({
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
                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                var table = $('#divsessionlist').DataTable({
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
                                    return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
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
            var table = $('#idlvAcdBatch').DataTable({
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
                                return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                var table = $('#idlvAcdBatch').DataTable({
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
                                    return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                    return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                    return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
                                                    return $('#idlvAcdBatch').DataTable().column(idx).visible();
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
    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStart" runat="server" ClientIDMode="Static" />
    <%--    <asp:UpdatePanel runat="server" ID="udpBatch">
        <ContentTemplate>--%>
    <%-- <asp:HiddenField ID="hidTAB" runat="server" ClientIDMode="Static" />--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Course Creation </h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1" id="tab1">Admission Batch</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Define Academic Year</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3" id="tab3">Academic Batch</a>
                            </li>
                        </ul>
                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updAdm" runat="server" AssociatedUpdatePanelID="updAdmBatch"
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

                                <asp:UpdatePanel ID="updAdmBatch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYddlAdmBatch" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:TextBox ID="txtAdmBatch" runat="server" MaxLength="30" AutoComplete="off" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Admission Batch" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*_+=,./:;<>?'{}[]\|&&quot;'" TargetControlID="txtAdmBatch" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtAdmBatch" runat="server" ControlToValidate="txtAdmBatch"
                                                            Display="None" ErrorMessage="Please Enter Admission Batch" SetFocusOnError="True"
                                                            ValidationGroup="AdmBatch"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="chkrdActive" name="switch" checked />
                                                            <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="chkrdActive"></label>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:CheckBox ID="chkIsAdmissionFlag" runat="server" CssClass="checkbox" />
                                                        <label>Is Admission Flag</label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit"
                                                    CssClass="btn btn-primary" ValidationGroup="AdmBatch" TabIndex="3" OnClientClick="return validation();" OnClick="btnSave_Click" />

                                                <asp:Button ID="AdmBatchCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                    CssClass="btn btn-warning" TabIndex="4" OnClick="AdmBatchCancel_Click" />

                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="AdmBatch"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <%--<panel ID="Panellv" runat="server" Visible="false">--%>
                                            <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Admission Batch List</h5>
                                                </div>
                                                <div class="table-responsive">
                                                    <asp:Panel ID="PanelAdmBatch" runat="server">
                                                        <asp:ListView ID="lvAdmBatch" runat="server" ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvAdmBatch_ItemEditing">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Admission Batch
                                                                            </th>
                                                                            <th>Status
                                                                            </th>
                                                                            <th>Admission Flag
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
                                                                        <asp:ImageButton ID="btnEditAdmBatch" runat="server" ImageUrl="~/Images/edit.png" OnClick="btnEditAdmBatch_Click"
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("BATCHNO")%>' TabIndex="5" />
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("BATCHNAME")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIsadmissionflag" runat="server" Text='<%# Eval("IS_ADMISSION_FLAG")%>' ForeColor='<%# Eval("IS_ADMISSION_FLAG").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>

                                                </div>
                                            </div>
                                            <%--</panel>--%>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="tab_2">
                                <div>
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
                                </div>

                                <asp:UpdatePanel ID="updSession" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">


                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtSLongName" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="2"
                                                            ToolTip="Please Enter Session Long Name" placeholder="Enter Academic Year Name" />
                                                        <%--<asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Academic Year Name" ControlToValidate="txtSLongName"
                                            Display="None" ValidationGroup="Submit" />--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Acedemic Year Start End Date</label>
                                                            <%--<asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        </div>

                                                        <div id="picker" class="form-control" tabindex="3">
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year Start Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="dvcal1" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="submit" onpaste="return false;"
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
                                                                ValidationGroup="submit" SetFocusOnError="True" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Year End Date</label>
                                                        </div>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i id="dvcal2" runat="server" class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="submit" TabIndex="4"
                                                                ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
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



                                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                                        </div>
                                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                                    </div>

                                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:CheckBox ID="chkIsCurrFinacialYear" runat="server" CssClass="checkbox" />
                                                        <label>Is Current Financial Year</label>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                                        TabIndex="11" ValidationGroup="Submit" CssClass="btn btn-primary" OnClientClick="return validate();" />

                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                        TabIndex="13" CssClass="btn btn-warning" />

                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel ID="pnlAcademicYear" runat="server">
                                                        <asp:ListView ID="lvAcademicYear" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divsessionlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <th>Academic Year
                                                                            </th>
                                                                            <th>Start Date
                                                                            </th>
                                                                            <th>End Date
                                                                            </th>
                                                                            <th>Is Active
                                                                            </th>
                                                                            <th>Current Financial Year
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
                                                                            OnClick="btnEdit_Click" CommandArgument='<%# Eval("ACADEMIC_YEAR_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            TabIndex="14" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ACADEMIC_YEAR_NAME")%>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("ACADEMIC_YEAR_STARTDATE","{0:dd-MMM-yyyy}") %>
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("ACADEMIC_YEAR_ENDDATE","{0:dd-MMM-yyyy}")%>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>' ForeColor='<%# Eval("ACTIVE_STATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIsFinancialYear" runat="server" Text='<%# Eval("IS_CURRENT_FY")%>' ForeColor='<%# Eval("IS_CURRENT_FY").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UdpAcdBatch" runat="server" AssociatedUpdatePanelID="UdpAcademicBatch"
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

                                <asp:UpdatePanel ID="UdpAcademicBatch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Academic Batch</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAcdBatch" runat="server" MaxLength="9" AutoComplete="off" CssClass="form-control" TabIndex="1" ToolTip="Please Enter Academic Batch" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            FilterMode="ValidChars" FilterType="Custom" ValidChars="0123456987-" TargetControlID="txtAcdBatch" />
                                                        <asp:RequiredFieldValidator ID="rfvtxtAcdBatch" runat="server" ControlToValidate="txtAcdBatch"
                                                            Display="None" ErrorMessage="Please Enter Academic Batch" SetFocusOnError="True"
                                                            ValidationGroup="AcdBatch"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Status</label>
                                                        </div>
                                                        <div class="switch form-inline">
                                                            <input type="checkbox" id="AcdBatchrdActive" name="switch" checked />
                                                            <label data-on="Active" tabindex="2" class="newAddNew Tab" data-off="Inactive" for="AcdBatchrdActive"></label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnAcdBatchSubmit" runat="server" Text="Submit" ToolTip="Submit"
                                                    CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="3" OnClientClick="return AcademicBatchvalidation();" OnClick="btnAcdBatchSubmit_Click" />

                                                <asp:Button ID="btnAcdBatchCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                    CssClass="btn btn-warning" TabIndex="4" OnClick="btnAcdBatchCancel_Click" />

                                            </div>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="AcdBatch"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            <div class="col-12 mt-3">
                                                <div class="sub-heading">
                                                    <h5>Academic Batch List</h5>
                                                </div>
                                                <div class="table-responsive">
                                                    <asp:Panel ID="PanelAcdBatch" runat="server">
                                                        <asp:ListView ID="lvAcdBatch" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="idlvAcdBatch">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Academic Batch
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
                                                                    <td>
                                                                        <asp:ImageButton ID="btnAcdBatchEdit" runat="server" ImageUrl="~/Images/edit.png"
                                                                            AlternateText="Edit Record" ToolTip="Edit Record" CommandArgument='<%# Eval("ACADEMICBATCHNO")%>' TabIndex="5" OnClick="btnAcdBatchEdit_Click" />
                                                                    </td>

                                                                    <td>
                                                                        <%# Eval("ACADEMICBATCH")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAcdStatus" runat="server" Text='<%# Eval("ACTIVESTATUS") %>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>

                                                                    </td>
                                                                </tr>

                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>

                                                </div>
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



    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
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

                    ranges: {

                    },

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

            var idtxtweb = $("[id$=txtSLongName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Academic Year Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
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
        function SetSetSubjecttype(val) {

            $('#chkrdActive').prop('checked', val);
        }
        function validation() {
            var alertMsg = "";
            var facuilty = document.getElementById('<%=txtAdmBatch.ClientID%>').value;
            if (facuilty == 0) {
                if (facuilty == "") {
                    alertMsg = alertMsg + 'Please Enter Admission Batch\n';
                }
                alert(alertMsg);
                return false;
            }
            else {

                $('#hfdActive').val($('#chkrdActive').prop('checked'));
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validation();
                });
            });
        });

    </script>
    <script>
        function SetSetAcademicBatch(val) {

            $('#AcdBatchrdActive').prop('checked', val);
        }
        function AcademicBatchvalidation() {
            var alertMsg = "";
            var batch = document.getElementById('<%=txtAcdBatch.ClientID%>').value;
            if (batch == 0) {
                if (batch == "") {
                    alertMsg = alertMsg + 'Please Enter Academic Batch\n';
                }
                alert(alertMsg);
                return false;
            }
            else {

                $('#hfdActive').val($('#AcdBatchrdActive').prop('checked'));
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnAcdBatchSubmit').click(function () {
                    validation();
                });
            });
        });

    </script>

</asp:Content>

