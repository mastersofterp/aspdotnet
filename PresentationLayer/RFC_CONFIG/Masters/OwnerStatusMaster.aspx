<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OwnerStatusMaster.aspx.cs" Inherits="RFC_CONFIG_Masters_OwnerStatusMaster"
    MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>--%>
    <%-- <script src="../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <%--<style>
        #ctl00_ContentPlaceHolder1_pnlConfigRef .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>


    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tab-le').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging:false,

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
                                return $('#tab-le').DataTable().column(idx).visible();
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
                                                return $('#tab-le').DataTable().column(idx).visible();
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
                                                return $('#tab-le').DataTable().column(idx).visible();
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
                                                return $('#tab-le').DataTable().column(idx).visible();
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
                var table = $('#tab-le').DataTable({
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
                                    return $('#tab-le').DataTable().column(idx).visible();
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
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
            var table = $('#table_config').DataTable({
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
                                return $('#table_config').DataTable().column(idx).visible();
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
                                                return $('#table_config').DataTable().column(idx).visible();
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
                                                return $('#table_config').DataTable().column(idx).visible();
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
                                                return $('#table_config').DataTable().column(idx).visible();
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
                var table = $('#table_config').DataTable({
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
                                    return $('#table_config').DataTable().column(idx).visible();
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
                                                    return $('#table_config').DataTable().column(idx).visible();
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
                                                    return $('#table_config').DataTable().column(idx).visible();
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
                                                    return $('#table_config').DataTable().column(idx).visible();
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
            var table = $('#tab_uni').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

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
                                return $('#tab_uni').DataTable().column(idx).visible();
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
                                                return $('#tab_uni').DataTable().column(idx).visible();
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
                                                return $('#tab_uni').DataTable().column(idx).visible();
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
                                                return $('#tab_uni').DataTable().column(idx).visible();
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
                var table = $('#tab_uni').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

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
                                    return $('#tab_uni').DataTable().column(idx).visible();
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
                                                    return $('#tab_uni').DataTable().column(idx).visible();
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
                                                    return $('#tab_uni').DataTable().column(idx).visible();
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
                                                    return $('#tab_uni').DataTable().column(idx).visible();
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




    <asp:UpdatePanel runat="server" ID="UpdRole" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Ownership Status Master</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">Define Affiliation Type</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="3" href="#tab3">Define University</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="4" href="#tab4">Define College Type</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="5" href="#tab5" style="display: none">Config Reference Details</a>
                                    </li>
                                </ul>
                                <div class="box-tools pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdproOwner" runat="server" AssociatedUpdatePanelID="updBatch"
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
                                        <asp:UpdatePanel ID="updBatch" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <%--<div class="box-header with-border">
                                                                <h3 class="box-title" style="margin-left: 15px">Ownership Status</h3>
                                                            </div>--%>

                                                        <div id="divMsg" runat="server">
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Ownership Status Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtOwnershipStatusName" AutoComplete="off" runat="server" TabIndex="6" MaxLength="50"
                                                                            ToolTip="Please Enter Ownership Status Name" placeholder="Enter Ownership Status Name" />
                                                                        <%--onkeyup="return ValidateTextbox(this);"--%>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtOwnershipStatusName" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                                                            <label data-on="Active" data-off="Inactive" tabindex="7" class="newAddNew Tab" for="rdActive"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="box-footer">
                                                            <p class="text-center">
                                                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="return validateOwner();"
                                                                    CssClass="btn btn-primary" OnClick="btnSave_Click" TabIndex="8" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" />
                                                            </p>

                                                            <div class="col-12">
                                                                <asp:Panel ID="Panel1" runat="server">
                                                                    <asp:ListView ID="lvOwnership" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Ownership Status List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Edit
                                                                                        </th>
                                                                                        <th>Ownership Status Name
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
                                                                                    <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("OwnershipStatusId") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="10" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("OwnershipStatusName")%>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblActive" Text='<%# Eval("IsActive")%>' ForeColor='<%# Eval("IsActive").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <%--  <Triggers>
                                                  <asp:PostBackTrigger ControlID="btnSave" />
                                             </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab2">
                                        <div>
                                            <asp:UpdateProgress ID="updProgAff" runat="server" AssociatedUpdatePanelID="updAffilation"
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
                                        <asp:HiddenField ID="hfdStatAff" runat="server" ClientIDMode="Static" />
                                        <asp:UpdatePanel ID="updAffilation" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div id="div1" runat="server"></div>
                                                        <%--<div class="box-header with-border">
                                                                <h3 class="box-title" style="margin-left: 15px">Define Affiliation Type</h3>
                                                            </div>--%>
                                                        <div class="box-body">
                                                            <div id="div2" runat="server">
                                                            </div>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Affiliation Type Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAffilationName" AutoComplete="off" placeholder="Enter Affiliation Type Name" runat="server" TabIndex="6" MaxLength="50"
                                                                            ToolTip="Please Enter Affiliation Type Name." />
                                                                        <%--onkeyup="return ValidateTextbox(this);"--%>
                                                                        <%--   <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="txtAffilationName"
                                                            Display="None" ErrorMessage="Please Enter Affilation Type Name." ValidationGroup="submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltAffiliation" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtAffilationName" />
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActiveAffi" name="switch" checked />
                                                                            <label data-on="Active" class="newAddNew Tab" tabindex="7" data-off="Inactive" for="rdActiveAffi"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnSaveAffilation" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validateAffilation();"
                                                                    CssClass="btn btn-primary" OnClick="btnSaveAffilation_Click" TabIndex="8" />
                                                                <asp:Button ID="btnCancelAffilation" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-warning" OnClick="btnCancelAffilation_Click" TabIndex="9" />
                                                            </div>

                                                            <div class="col-12">
                                                                <asp:Panel ID="Panel2" runat="server">
                                                                    <asp:ListView ID="lvAffilation" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Affiliation Type List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100% !important" id="tab-le">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Edit
                                                                                        </th>
                                                                                        <th>Affiliation Type Name
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
                                                                                    <asp:ImageButton ID="btnEditAffilation" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("AffilationTypeId") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditAffilation_Click" TabIndex="10" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("AffilationName")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive" Text='<%# Eval("IsActive")%>' ForeColor='<%# Eval("IsActive").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnSaveAffilation" />--%>
                                                <%--<asp:PostBackTrigger ControlID="btnCancelAffilation" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab3">
                                        <div>
                                            <asp:UpdateProgress ID="updProUniversity" runat="server" AssociatedUpdatePanelID="updUniversity"
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
                                        <asp:HiddenField ID="hfdStatUniv" runat="server" ClientIDMode="Static" />
                                        <asp:UpdatePanel ID="updUniversity" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <%-- <div class="box-header with-border">
                                                                <h3 class="box-title" style="margin-left: 15px">Define University</h3>
                                                            </div>--%>
                                                        <div id="div3" runat="server">
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>University Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtUniversityName" runat="server" CssClass="form-control" TabIndex="6"
                                                                            ToolTip="Please Enter University Name" AutoComplete="OFF" placeholder="University Name" ValidationGroup="submit" />
                                                                        <asp:RequiredFieldValidator ID="rfvUniversityname" runat="server" ControlToValidate="txtUniversityName"
                                                                            Display="None" ErrorMessage="Please Enter University Name." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                        <%--  onkeyup="return ValidTextbox();"--%>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltUniversity" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtUniversityName" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>State</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" ValidationGroup="submit"
                                                                            TabIndex="7" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Please Select State"
                                                                            ControlToValidate="ddlState" ValidationGroup="submit" InitialValue="0" Display="None">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActiveUniv" name="switch" checked />
                                                                            <label data-on="Active" tabindex="8" class="newAddNew Tab" data-off="Inactive" for="rdActiveUniv"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <br />
                                                        <div class="col-12 btn-footer">
                                                            <%--<p class="text-center">--%>
                                                            <asp:Button ID="btnSaveUniversity" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                                CssClass="btn btn-primary" TabIndex="9" OnClick="btnSaveUniversity_Click" OnClientClick="return validateUniv();" />
                                                            <asp:Button ID="btnCancelUniv" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="10" OnClick="btnCancelUniv_Click" />

                                                            <asp:ValidationSummary ID="vlsummary" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <%-- </p>--%>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="Panel3" runat="server">
                                                                <asp:ListView ID="lvUniversity" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>University List </h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%; table-layout: fixed;" id="tab_uni">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center; width: 10%;">Edit
                                                                                    </th>
                                                                                    <th>University Name
                                                                                    </th>
                                                                                    <th>State
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
                                                                                <asp:ImageButton ID="btnEditUniv" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("UNIVERSITYID") %>'
                                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditUniv_Click" TabIndex="11" />
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("UNIVERSITYNAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("STATENAME")%>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblActive" Text='<%# Eval("IsActive")%>' ForeColor='<%# Eval("IsActive").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>

                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnSaveUniversity" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab4">
                                        <div>
                                            <asp:UpdateProgress ID="updProInstitute" runat="server" AssociatedUpdatePanelID="updInstitute"
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
                                        <asp:HiddenField ID="hfdStatInsti" runat="server" ClientIDMode="Static" />
                                        <asp:UpdatePanel ID="updInstitute" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <%--  <div class="box-header with-border">
                                                            <h3 class="box-title" style="margin-left: 15px">Define College Type</h3>
                                                        </div>--%>
                                                        <div id="div4" runat="server">
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <%--<label>College Type</label>--%>
                                                                            <asp:Label ID="lblDYtxtColgType" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtInstituteTypeName" runat="server" TabIndex="6" MaxLength="50" AutoComplete="off"
                                                                            ToolTip="Please Enter College Type Name." placeholder="Enter College Type" />
                                                                        <%--onkeyup="return ValidateTextbox(this);" --%>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltInst" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtInstituteTypeName" />
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Status</label>
                                                                        </div>
                                                                        <div class="switch form-inline">
                                                                            <input type="checkbox" id="rdActiveInst" name="switch" checked />
                                                                            <label data-on="Active" class="newAddNew Tab" tabindex="7" data-off="Inactive" for="rdActiveInst"></label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="box-footer">
                                                            <p class="text-center">
                                                                <asp:Button ID="btnSaveInstitute" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return validateInstitute();"
                                                                    CssClass="btn btn-primary" OnClick="btnSaveInstitute_Click" TabIndex="8" />
                                                                <asp:Button ID="btnCancelInstitute" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-warning" OnClick="btnCancelInstitute_Click" TabIndex="9" />
                                                            </p>

                                                            <div class="col-md-12">
                                                                <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                                                    <asp:ListView ID="lvInstituteType" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>College Type List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display"  style="table-layout:fixed">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Edit
                                                                                        </th>
                                                                                        <th><asp:Label ID="lblDYtxtColgType" runat="server" Font-Bold="true"></asp:Label>
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
                                                                                    <asp:ImageButton ID="btnEditInstitute" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("InstitutionTypeId") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditInstitute_Click" TabIndex="10" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("InstitutionTypeName")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive" Text='<%# Eval("IsActive")%>' ForeColor='<%# Eval("IsActive").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnSaveInstitute" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab5">
                                        <div>
                                            <asp:UpdateProgress ID="updRef" runat="server" AssociatedUpdatePanelID="updConfigRef"
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
                                        <asp:UpdatePanel ID="updConfigRef" runat="server">
                                            <ContentTemplate>

                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Organization</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlOrganization" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                TabIndex="6">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Project Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProjectName" Placeholder="Please Enter Project Name" CssClass="form-control"
                                                                TabIndex="7" ToolTip="Enter Project Name" AutoComplete="off" runat="server"></asp:TextBox>
                                                            <%--  onkeypress="return alphaOnly(event);"--%>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteProname" runat="server" TargetControlID="txtProjectName" FilterType="Custom" FilterMode="InvalidChars"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Server Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtServerName" Placeholder="Please Enter Server Name" CssClass="form-control"
                                                                TabIndex="8" ToolTip="Enter Server Name" AutoComplete="off" runat="server"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>User ID</label>
                                                            </div>
                                                            <asp:TextBox ID="txtUserID" Placeholder="Please Enter User ID" CssClass="form-control"
                                                                TabIndex="9" ToolTip="Enter User ID" AutoComplete="off" runat="server"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Password</label>
                                                            </div>
                                                            <asp:TextBox ID="txtPassword" Placeholder="Please Enter Password" CssClass="form-control"
                                                                TabIndex="10" ToolTip="Enter Password" AutoComplete="off" runat="server"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Database Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDatabaseName" Placeholder="Please Enter Database Name" CssClass="form-control" onkeypress="return alphaOnly(event);"
                                                                TabIndex="11" ToolTip="Enter Database Name" AutoComplete="off" runat="server"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Organization URL</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOrgUrl" Style="text-transform: lowercase" Placeholder="Please Enter Organization URL" CssClass="form-control"
                                                                TabIndex="12" ToolTip="Enter Organization URL" AutoComplete="off" runat="server"></asp:TextBox>
                                                            <%-- <asp:RegularExpressionValidator ID="RegExUrl" runat="server" ForeColor="Red" ErrorMessage="Please Enter URL in Correct Format" ControlToValidate="txtOrgUrl" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" Display="Dynamic"></asp:RegularExpressionValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Default Page Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDefaultpage" Placeholder="Please Enter Default Page Name" CssClass="form-control"
                                                                TabIndex="13" ToolTip="Enter Default Page Name" AutoComplete="off" runat="server"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmitConfgRef" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmitConfgRef_Click" OnClientClick="return validateConfigRef();"
                                                        TabIndex="14" CssClass="btn btn-primary" />

                                                    <asp:Button ID="btnCancelConfgRef" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelConfgRef_Click"
                                                        TabIndex="15" CssClass="btn btn-warning" />

                                                    <%-- <asp:Button ID="btnGoBack" Visible="false" runat="server" Text="Go Back To List" ToolTip="Go Back To List" OnClick="btnGoBack_Click"
                                        TabIndex="8" CssClass="btn btn-info" />--%>
                                                </div>


                                                <div class="col-12">
                                                    <asp:Panel ID="pnlConfigRef" runat="server" Visible="false">
                                                        <asp:ListView ID="lvConfigRef" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Config Reference Details List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table_config">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit
                                                                            </th>
                                                                            <th>Organization
                                                                            </th>
                                                                            <th>Project Name
                                                                            </th>
                                                                            <th>Server Name
                                                                            </th>
                                                                            <th>User ID
                                                                            </th>
                                                                            <th>Password
                                                                            </th>
                                                                            <th>Database Name
                                                                            </th>
                                                                            <th>Organization URL
                                                                            </th>
                                                                            <th>Default Page Name
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
                                                                        <asp:ImageButton ID="btnEditConfgRef" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                            CommandArgument='<%# Eval("ReferenceDetailsId")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                            TabIndex="16" OnClick="btnEditConfgRef_Click" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("OrgName") %>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ProjectName")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("ServerName")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("UserId")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("Password")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DBName")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("URL_LINK")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("DEFAULT_PAGE")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                            </ContentTemplate>
                                            <Triggers>
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


    <%--Ownerhip status master--%>
    <script>
        function SetStatOwner(val) {
            $('#rdActive').prop('checked', val);
        }

        function validateOwner() {

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var idtxtOwnershipStatusName = $("[id$=txtOwnershipStatusName]").attr("id");
            var txtOwnershipStatusName = document.getElementById(idtxtOwnershipStatusName);
            if (txtOwnershipStatusName.value.length == 0) {
                alert('Please Enter Ownership Status Name', 'Warning!');
                $(txtOwnershipStatusName).focus();
                return false;
            }
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {

            $(function () {

                $('#btnSave').click(function () {
                    alert('a');
                    validateOwner();
                });
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateTextbox(txtid) {
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = "";
                return false;
            }
        }
        $(document).ready(function () {
            $('#txtOwnershipStatusName').keypress(function (e) {
                var k = e.which;
                var ok = k == 127 || k == 8 || k == 9 || k == 13 || k == 37 || k == 38 || k == 39 || k == 40;
                ok = ok ||
                   k >= 65 && k <= 90 || // A-Z
                   k >= 97 && k <= 122 || // a-z
                   k == 44; // ,

                if (!ok) {
                    e.preventDefault();
                }
            });
        });
        function isNumber(evt) {
            var theEvent = evt || window.event;
            var key = theEvent.keyCode || theEvent.which;

            key = String.fromCharCode(key);
            if (key.length == 0) return;
            var regex = /^[a-zA-Z.,\b]+$/;
            //alert(!regex.test(key))
            if (!regex.test(key)) {
                alert("if")
                theEvent.returnValue = false;
                if (theEvent.preventDefault) theEvent.preventDefault();
            }
            else { alert("else") }
        }
    </script>
    <%--end--%>

    <%--Affilation Master--%>
    <script>
        function SetStatAffilation(val) {
            $('#rdActiveAffi').prop('checked', val);
        }

        function validateAffilation() {

            $('#hfdStatAff').val($('#rdActiveAffi').prop('checked'));

            var idtxtAffilationName = $("[id$=txtAffilationName]").attr("id");
            var txtAffilationName = document.getElementById(idtxtAffilationName);
            // alert(txtAffilationName.value.length)
            if (txtAffilationName.value.length == 0) {
                alert('Please Enter Affilation Type Name.', 'Warning!');
                //$(txtAffilationName).css('border-color', 'red');
                $(txtAffilationName).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSaveAffilation').click(function () {
                    validateAffilation();
                });
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateTextbox(txtid) {
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = "";
                return false;
            }
        }
    </script>
    <%--end--%>

    <%--UniversityMaster--%>
    <script>
        function SetStatUniv(val) {
            $('#rdActiveUniv').prop('checked', val);
        }

        function validateUniv() {

            $('#hfdStatUniv').val($('#rdActiveUniv').prop('checked'));

            var idUN = $("[id$=txtUniversityName]").attr("id");
            var txtUniversityName = document.getElementById(idUN);
            if (txtUniversityName.value.length == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Enter University Name.', 'Warning!');
                $(txtUniversityName).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlState]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select State.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSaveUniversity').click(function () {
                    validateUniv();
                });
            });
        });
    </script>

    <script type="text/javascript">
        function ValidTextbox() {
            var charactersOnly = document.getElementById('ctl00_ContentPlaceHolder1_txtUniversityName').value;
            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById('ctl00_ContentPlaceHolder1_txtUniversityName').value = "";
                return false;
            }
        }
    </script>
    <%--end--%>

    <%--College Type--%>
    <script>
        function SetStatInstitute(val) {
            debugger;
            $('#rdActiveInst').prop('checked', val);
        }

        function validateInstitute() {

            $('#hfdStatInsti').val($('#rdActiveInst').prop('checked'));

            var idtxtInstituteTypeName = $("[id$=txtInstituteTypeName]").attr("id");
            var txtInstituteTypeName = document.getElementById(idtxtInstituteTypeName);
            // alert(txtInstituteTypeName.value.length)
            if (txtInstituteTypeName.value.length == 0) {
                alert('Please Enter College Type Name.', 'Warning!');
                //$(txtInstituteTypeName).css('border-color', 'red');
                $(txtInstituteTypeName).focus();
                return false;
            }

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSaveInstitute').click(function () {
                    validateInstitute();
                });
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateTextbox(txtid) {
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = "";
                return false;
            }
        }
    </script>
    <%--end--%>

    <%--ConfigReferenceDetails--%>
    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    <script>
        function validateConfigRef() {

            var ddlO = $("[id$=ddlOrganization]").attr("id");
            var ddlO = document.getElementById(ddlO);
            if (ddlO.value == 0) {
                alert('Please Select Organization', 'Warning!');
                $(ddlO).focus();
                return false;
            }

            var txtProject = $("[id$=txtProjectName]").attr("id");
            var txtProject = document.getElementById(txtProject);
            if (txtProject.value == 0) {
                alert('Please Enter Project Name', 'Warning!');
                $(txtProject).focus();
                return false;
            }

            var txtServer = $("[id$=txtServerName]").attr("id");
            var txtServer = document.getElementById(txtServer);
            if (txtServer.value == 0) {
                alert('Please Enter Server Name', 'Warning!');
                $(txtServer).focus();
                return false;
            }

            var txtU = $("[id$=txtUserID]").attr("id");
            var txtU = document.getElementById(txtU);
            if (txtU.value == 0) {
                alert('Please Enter User ID', 'Warning!');
                $(txtU).focus();
                return false;
            }

            var txtPass = $("[id$=txtPassword]").attr("id");
            var txtPass = document.getElementById(txtPass);
            if (txtPass.value == 0) {
                alert('Please Enter Password', 'Warning!');
                $(txtPass).focus();
                return false;
            }

            var txtDb = $("[id$=txtDatabaseName]").attr("id");
            var txtDb = document.getElementById(txtDb);
            if (txtDb.value == 0) {
                alert('Please Enter Database Name', 'Warning!');
                $(txtDb).focus();
                return false;
            }

            var txtO = $("[id$=txtOrgUrl]").attr("id");
            var txtO = document.getElementById(txtO);
            if (txtO.value == 0) {
                alert('Please Enter Organization Url', 'Warning!');
                $(txtO).focus();
                return false;
            }

            var txtpage = $("[id$=txtDefaultpage]").attr("id");
            var txtpage = document.getElementById(txtpage);
            if (txtpage.value == 0) {
                alert('Please Enter Default Page Name', 'Warning!');
                $(txtpage).focus();
                return false;
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitConfgRef').click(function () {
                    validateConfigRef();
                });
            });
        });

    </script>
    <%--END--%>
</asp:Content>
