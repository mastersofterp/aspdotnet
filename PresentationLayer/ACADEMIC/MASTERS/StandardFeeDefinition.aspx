<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    Title="" CodeFile="StandardFeeDefinition.aspx.cs" Inherits="Payments_StandardFeeDefinition" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multi-select/bootstrap-multiselect.js")%>"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <%--===== Data Table Script added by gaurav =====--%>
    <%--<script>
        $(document).ready(function () {
            var table = $('#tblFeeEntryGrid').DataTable({
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
                                return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                var table = $('#tblFeeEntryGrid').DataTable({
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
                                    return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                    return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                    return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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
                                                    return $('#tblFeeEntryGrid').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">Select Criteria for Fee Definition</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Receipt Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlReceiptType" runat="server" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                    AutoPostBack="true" />
                                                <asp:RequiredFieldValidator ID="valReceiptType" runat="server" ControlToValidate="ddlReceiptType"
                                                    Display="None" ErrorMessage="Please select receipt type." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Institute Name</label>--%>
                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSchClg" runat="server" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Institute">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSchClg" runat="server" ControlToValidate="ddlSchClg"
                                                    Display="None" ErrorMessage="Please select Institute." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Degree</label>--%>
                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valDegree" runat="server" ControlToValidate="ddlDegree"
                                                    Display="None" ErrorMessage="Please select degree." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Branch</label>--%>
                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" ErrorMessage="Please select branch." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <%--<label>Batch</label>--%>
                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="5" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valBatch" runat="server" ControlToValidate="ddlBatch"
                                                    Display="None" ErrorMessage="Please select batch." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Type</label>
                                                </div>
                                                <asp:DropDownList ID="ddlPaymentType" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged" TabIndex="6"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="valPaymentType" runat="server" ControlToValidate="ddlPaymentType"
                                                    Display="None" ErrorMessage="Please select payment type." InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="show" />
                                            </div>

                                            <div id="idFeesName" class="form-group col-lg-12 col-md-12 col-12" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <label>Fees Name</label>
                                                </div>
                                                <asp:Label ID="lblFeeName" runat="server" Text="" Font-Size="Small" />
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnShow" runat="server" Text="Show Fee Definition" ValidationGroup="show"
                                                    TabIndex="7" OnClick="btnShow_Click" CssClass=" btn btn-primary" />
                                                <asp:Button ID="btnCopy" runat="server" OnClick="btnCopy_Click" Text="Copy Standard Fees " ValidationGroup="copy" CssClass="btn btn-primary" />
                                                <asp:ValidationSummary ID="valSummery" runat="server" TabIndex="8" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="show" CssClass=" btn btn-primary" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" TabIndex="8" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="copy" CssClass=" btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading pb-0">
                                                    <h5>Search Fee Item By Name</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-12 mb-1">
                                                <%-- <asp:TextBox ID="txtSearchBox" runat="server" onkeyup="javascript:GetMachingListboxItem(this);"
                                                    CssClass="form-control" TabIndex="8" />--%>
                                                <asp:TextBox ID="txtSearchBox" runat="server" OnTextChanged="txtSearchBox_TextChanged" AutoPostBack="true"
                                                    CssClass="form-control" TabIndex="8" />
                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                <asp:ListBox ID="lstFeesItems" runat="server" AutoPostBack="true" TabIndex="9" Style="min-height: 200px; max-height: 200px; overflow: auto;" CssClass="form-control"
                                                    OnSelectedIndexChanged="lstFeesItems_SelectedIndexChanged"></asp:ListBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <%--ScrollBars="auto" Height="400px"--%>
                                            <asp:ListView ID="lv" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Standard Fee Definition</h5>
                                                    </div>
                                                    <table id="tblFeeEntryGrid" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Fee Items
                                                                </th>
                                                                <th>Currency
                                                                </th>
                                                                <th>Sem-1
                                                                </th>
                                                                <th>Sem-2
                                                                </th>
                                                                <th>Sem-3
                                                                </th>
                                                                <th>Sem-4
                                                                </th>
                                                                <th>Sem-5
                                                                </th>
                                                                <th>Sem-6
                                                                </th>
                                                                <th>Sem-7
                                                                </th>
                                                                <th>Sem-8
                                                                </th>
                                                                <th>Sem-9
                                                                </th>
                                                                <th>Sem-10
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                        <tbody>
                                                            <tr>
                                                                <td>TOTAL:
                                                                </td>
                                                                <td>INR</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem1TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem2TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem3TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem4TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem5TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem6TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem7TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem8TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem9TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSem10TotalAmt" onkeydown="javascript:return false;" runat="server"
                                                                        AutoCompleteType="Disabled" CssClass="form-control" Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item">
                                                        <td>
                                                            <asp:Label ID="lblFeeHead" runat="server" Text='<%# Eval("FEE_LONGNAME") %>' />
                                                            <asp:HiddenField ID="hidFeeCatNo" runat="server" Value='<%# Bind("FEE_CAT_NO") %>' />
                                                            <asp:HiddenField ID="hidFeeHead" runat="server" Value='<%# Bind("FEE_HEAD") %>' />
                                                            <asp:HiddenField ID="hidFeeDesc" runat="server" Value='<%# Bind("FEE_DESCRIPTION") %>' />
                                                            <asp:HiddenField ID="hidSrNo" runat="server" Value='<%# Bind("SRNO") %>' />
                                                            <asp:HiddenField ID="hidRecieptCode" runat="server" Value='<%# Bind("RECIEPT_CODE") %>' />
                                                            <asp:HiddenField ID="hidDegreeNo" runat="server" Value='<%# Bind("DEGREENO") %>' />
                                                            <asp:HiddenField ID="hidBranchNo" runat="server" Value='<%# Bind("BRANCHNO") %>' />
                                                            <asp:HiddenField ID="hidBatchNo" runat="server" Value='<%# Bind("BATCHNO") %>' />
                                                            <asp:HiddenField ID="hidPaymentNo" runat="server" Value='<%# Bind("PAYTYPENO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("CURRENCY") %>' />
                                                            <%-- <asp:HiddenField ID="hidCurrency" runat="server" Value='<%# Eval("CUR_NO") %>' />--%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem1" runat="server" Text='<%# Eval("SEMESTER1") %>' TaEvalex="8"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem2" runat="server" Text='<%# Eval("SEMESTER2") %>' TaEvalex="9"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem3" runat="server" Text='<%# Eval("SEMESTER3") %>' TaEvalex="10"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem4" runat="server" Text='<%# Eval("SEMESTER4") %>' TaEvalex="11"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem5" runat="server" Text='<%# Eval("SEMESTER5") %>' TaEvalex="12"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem6" runat="server" Text='<%# Eval("SEMESTER6") %>' TaEvalex="13"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem7" runat="server" Text='<%# Eval("SEMESTER7") %>' TaEvalex="14"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem8" runat="server" Text='<%# Eval("SEMESTER8") %>' TaEvalex="15"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem9" runat="server" Text='<%# Eval("SEMESTER9") %>' TaEvalex="16"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSem10" runat="server" Text='<%# Eval("SEMESTER10") %>' TaEvalex="17"
                                                                AutoCompleteType="Disabled" onblur="UpdateTotalAmounts();" onkeyup="IsNumeric(this);"
                                                                CssClass="form-control" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>


                                </div>
                            </div>

                            <asp:Panel ID="pnlCopy" runat="server" Visible="false">

                                <div class="col-12 mb-4">
                                    <div class="form-group col-lg-12 col-md-12 col-12 ">
                                        <div class="sub-heading">
                                            <h5>Copy Standard Fees</h5>
                                        </div>
                                        <div class="form-group col-lg-5 col-md-6 col-12">
                                            <asp:RadioButtonList ID="rdbCopyStanderdFees" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbCopyStanderdFees_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">Admission Batch Wise &nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">Branch Wise</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>

                                    <div class="row" id="divAdmBatch" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Receipt Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlRecieptCopy" OnSelectedIndexChanged="ddlRecieptCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                AutoPostBack="true" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch From</label>
                                            </div>
                                            <asp:DropDownList ID="ddlColgCopy" OnSelectedIndexChanged="ddlColgCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="false" ToolTip="Please select Admission Batch From" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch From</label>
                                            </div>
                                            <asp:DropDownList ID="ddlAdmissionBatchFrom" runat="server" AppendDataBoundItems="true"
                                                TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                AutoPostBack="true" ToolTip="Please select Admission Batch From" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none ">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Degree</label>
                                            </div>
                                            <asp:DropDownList ID="ddlDegreeCopy" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegreeCopy_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Branch</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBranchCopy" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlBranchCopy_SelectedIndexChanged" AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Admission Batch To</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBatchCopy" runat="server" OnSelectedIndexChanged="ddlBatchCopy_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                TabIndex="5" AutoPostBack="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Payment Type</label>
                                            </div>
                                            <asp:DropDownList ID="ddlPaymentCopy" OnSelectedIndexChanged="ddlPaymentCopy_SelectedIndexChanged" runat="server" AppendDataBoundItems="true"
                                                CssClass="form-control" data-select2-enable="true" TabIndex="6"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Fees Name</label>
                                            </div>
                                            <asp:Label ID="lblFeeSNameCopy" runat="server" Text="" Font-Size="Small" />
                                        </div>

                                    </div>
                                </div>

                                <div class="col-9" id="divBranch" runat="server" visible="false">
                                    <div class="form-group col-lg-6 col-md-6 col-12 ">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Receipt Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptTypeRbd" runat="server" AppendDataBoundItems="true"
                                            TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true" ToolTip="Please select Receipt Type ">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-12 col-md-12 col-12 ">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label></label>
                                            <asp:Label ID="LblBranchOverride" CssClass="form-control" Font-Bold="true" Style="color: red; font-size: 14px" runat="server" Text="After updating any standard fees, please ensure that you check the associated demands that have already been created." />
                                        </div>

                                    </div>

                                    <div class="row">

                                        <div class="form-group col-lg-6 col-md-6 col-12 ">
                                            <div class="form-group col-lg-12 col-md-12 col-12 ">
                                                <div class="sub-heading">
                                                    <h5>Copy From</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Admission Batch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmBatchFrom" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdmBatchFrom_SelectedIndexChanged"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Admission Batch ">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-12 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>School/Institute</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollegeFrom" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollegeFrom_SelectedIndexChanged"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Admission Batch ">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-6 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegreeFrom" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegreeFrom_SelectedIndexChanged"
                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-6 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranchFrom" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-6 col-md-6 col-12 ">
                                            <div class="form-group col-lg-12 col-md-12 col-12 ">
                                                <div class="sub-heading">
                                                    <h5>Copy To</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-12 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Admission Batch</label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmBatchTo" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlAdmBatchTo_SelectedIndexChanged"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Admission Batch ">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-12 col-md-6 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>School/Institute</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollegeTo" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollegeTo_SelectedIndexChanged"
                                                    TabIndex="2" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" ToolTip="Please select Admission Batch ">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-6 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Degree</label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegreeTo" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegreeTo_SelectedIndexChanged"
                                                    CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-12 col-md-6 col-12 ">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Branch</label>
                                                </div>
                                                <%--<asp:DropDownList ID="ddlBranchTo" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>--%>

                                                <asp:ListBox ID="lboBranchTo" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>

                                            </div>

                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12 ml-3">
                                            <asp:CheckBox ID="ChkBranchCopyOverride" CssClass="form-control" Font-Bold="true"   runat="server" Text="If you check checkbox ,Standard Fees Will be Override" />
                                        </div>
                                        </div>
                                    </div>
                            </asp:Panel>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmitCopy" runat="server" Visible="false" Text="Submit to Copy Standard Fees" OnClientClick="return Validation();"
                                    TabIndex="7" OnClick="btnSubmitCopy_Click" CssClass="btn btn-primary" />

                                <asp:Button ID="btnSubmitCopyDiv" runat="server" Visible="false" Text="Submit to Copy Standard Fees" OnClientClick="return ValidationRBD();"
                                    TabIndex="8" OnClick="btnSubmitCopyDiv_Click" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancelCopy" runat="server" Visible="false" Text="Cancel" CausesValidation="false"
                                    TabIndex="9" OnClick="btnCancelCopy_Click" CssClass="btn btn-warning" />

                                <asp:Button ID="btnSubmit" Visible="false" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                                    ValidationGroup="submit" CssClass="btn btn-primary" />

                                <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" CssClass="btn btn-warning" />

                                <asp:Button ID="btnExcel" runat="server" Text="Overall Fee Definition Report(Excel)" OnClick="btnExcel_Click"
                                    TabIndex="24" Enabled="true" CssClass="btn btn-info" OnClientClick="return validatereport();" />
                                <asp:ValidationSummary ID="vsSubmit" runat="server" ValidationGroup="Submit" ShowMessageBox="true" DisplayMode="List" ShowSummary="false" CssClass=" btn btn-primary" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function GetMachingListboxItem(searchTextbox) {
            if (searchTextbox != null) {
                var listbox = document.getElementById("ctl00_ContentPlaceHolder1_lstFeesItems");
                if (listbox != null) {
                    var strTxt = searchTextbox.value;
                    if (strTxt != "") {
                        for (var index = 0; index < listbox.length; index++) {
                            var strLst = listbox.options[index].innerHTML;
                            if (strLst.toUpperCase().substring(0, strTxt.length) == strTxt.toUpperCase()) {
                                listbox.selectedIndex = index;
                                return;
                            }
                            else {
                                listbox.selectedIndex = -1;
                            }
                        }
                    }
                }
            }
            return false;
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = 0;
                }
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function UpdateTotalAmounts() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                if (document.getElementById('tblFeeEntryGrid') != null)
                    dataRows = document.getElementById('tblFeeEntryGrid').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (sem = 2; sem <= 11; sem++) {

                        totalFeeAmt = 0.00;
                        for (i = 1; i < (dataRows.length - 1) ; i++) {

                            var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                            var dataCell = dataCellCollection.item(sem);

                            var controls = dataCell.getElementsByTagName('input');

                            var txtAmt = controls.item(0).value;
                            //                              alert(txtAmt)
                            if (txtAmt != '')
                                totalFeeAmt += parseFloat(txtAmt);
                            //                              alert(totalFeeAmt)
                            if ((i + 2) == dataRows.length) {
                                var semcnt = sem - 1;
                                document.getElementById('ctl00_ContentPlaceHolder1_lv_txtSem' + semcnt + 'TotalAmt').value = totalFeeAmt.toString();
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function UpdateTotalAmounts() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                if (document.getElementById('tblFeeEntryGrid') != null)
                    dataRows = document.getElementById('tblFeeEntryGrid').getElementsByTagName('tr');

                if (dataRows != null) {
                    for (sem = 2; sem <= 11; sem++) {

                        totalFeeAmt = 0.00;
                        for (i = 1; i < (dataRows.length - 1) ; i++) {

                            var dataCellCollection = dataRows.item(i).getElementsByTagName('td');

                            var dataCell = dataCellCollection.item(sem);

                            var controls = dataCell.getElementsByTagName('input');

                            var txtAmt = controls.item(0).value;
                            //                              alert(txtAmt)
                            if (txtAmt != '')
                                totalFeeAmt += parseFloat(txtAmt);
                            //                              alert(totalFeeAmt)
                            if ((i + 2) == dataRows.length) {
                                var semcnt = sem - 1;
                                document.getElementById('ctl00_ContentPlaceHolder1_lv_txtSem' + semcnt + 'TotalAmt').value = totalFeeAmt.toString();
                            }
                        }
                    }
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        //$("#column_select").change(function () {
        //    $("#layout_select").children('option').hide();
        //    $("#layout_select").children("option[value^=" + $(this).val() + "]").show()
        //})

        $(function () {
            // When your textbox is changed (i.e. a date of birth is set)
            $("#<%=ddlAdmissionBatchFrom.ClientID%>").change(function () {
                alert('hai');
                //$("#<%--= txtAge.ClientID --%>").val(_calculateAge($(this).val()));
            });
        });

        //$(function () {
        // alert('hai');
        // $("#<%--=ddlAdmissionBatchFrom.ClientID--%>").change(function () {
        //    alert('hai');
        // $("#<%--=ddlBatchCopy.ClientID--%>").children('option').hide();
        // $("#<%--=ddlBatchCopy.ClientID--%>").children("option[value^=" + $(this).val() + "]").show()
        //  });
        //  });

        function value(ddl) {
            var ddlmainValue = ddl.options[ddl.value].value;
            var drop = document.getElementById('<%= ddlBatchCopy.ClientID%>');
            var val = document.getElementById("<%= ddlBatchCopy.ClientID%>").length;
            for (var i = 0 ; i < val ; i++) {
                if (i == ddlmainValue) {
                    drop.options[ddlmainValue].style.display = "none";
                }
                else {
                    drop.options[i].style.display = "block";
                }
            }
        }

        function ValidationRBD() {

            var ddlReceiptTypeRbd = $("[id$=ddlReceiptTypeRbd]").attr("id");
            var ddlReceiptTypeRbd = document.getElementById(ddlReceiptTypeRbd);
            if (ddlReceiptTypeRbd.value == 0) {
                alert('Please Select Receipt Type for Copy.', 'Warning!');
                $(ddlReceiptTypeRbd).focus();
                return false;
            }

            var ddlAdmissionBatchFrom = $("[id$=ddlAdmBatchFrom]").attr("id");
            var ddlAdmissionBatchFrom = document.getElementById(ddlAdmissionBatchFrom);
            if (ddlAdmissionBatchFrom.value == 0) {
                alert('Please Select Admission Batch From.', 'Warning!');
                $(ddlAdmissionBatchFrom).focus();
                return false;
            }

            var ddlBatchCopy = $("[id$=ddlAdmBatchTo]").attr("id");
            var ddlBatchCopy = document.getElementById(ddlBatchCopy);
            if (ddlBatchCopy.value == 0) {
                alert('Please Select Admission Batch To.', 'Warning!');
                $(ddlBatchCopy).focus();
                return false;
            }

            var ddlCollegeFrom = $("[id$=ddlCollegeFrom]").attr("id");
            var ddlCollegeFrom = document.getElementById(ddlCollegeFrom);
            if (ddlCollegeFrom.value == 0) {
                alert('Please Select School/Institute  CopyFrom.', 'Warning!');
                $(ddlCollegeFrom).focus();
                return false;
            }

            var ddlCollegeTo = $("[id$=ddlCollegeTo]").attr("id");
            var ddlCollegeTo = document.getElementById(ddlCollegeTo);
            if (ddlCollegeTo.value == 0) {
                alert('Please Select School/Institute CopyTo.', 'Warning!');
                $(ddlCollegeTo).focus();
                return false;
            }

            var ddlDegreeFrom = $("[id$=ddlDegreeFrom]").attr("id");
            var ddlDegreeFrom = document.getElementById(ddlDegreeFrom);
            if (ddlDegreeFrom.value == 0) {
                alert('Please Select  Degree  CopyFrom.', 'Warning!');
                $(ddlDegreeFrom).focus();
                return false;
            }

            var ddlDegreeTo = $("[id$=ddlDegreeTo]").attr("id");
            var ddlDegreeTo = document.getElementById(ddlDegreeTo);
            if (ddlDegreeTo.value == 0) {
                alert('Please Select  Degree CopyTo.', 'Warning!');
                $(ddlDegreeTo).focus();
                return false;
            }

            var ddlBranchFrom = $("[id$=ddlBranchFrom]").attr("id");
            var ddlBranchFrom = document.getElementById(ddlCollegeFrom);
            if (ddlBranchFrom.value == 0) {
                alert('Please Select Branch  CopyFrom.', 'Warning!');
                $(ddlBranchFrom).focus();
                return false;
            }

            var ddlBranchTo = $("[id$=ddlBranchTo]").attr("id");
            var ddlBranchTo = document.getElementById(ddlBranchTo);
            if (ddlBranchTo.value == 0) {
                alert('Please Select Branch CopyTo.', 'Warning!');
                $(ddlBranchTo).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCopyDiv').click(function () {
                    ValidationRBD();
                });
            });
        });

        function Validation() {

            var ddlRecieptCopy = $("[id$=ddlRecieptCopy]").attr("id");
            var ddlRecieptCopy = document.getElementById(ddlRecieptCopy);
            if (ddlRecieptCopy.value == 0) {
                alert('Please Select Receipt Type for Copy.', 'Warning!');
                $(ddlRecieptCopy).focus();
                return false;
            }

            var ddlAdmissionBatchFrom = $("[id$=ddlAdmissionBatchFrom]").attr("id");
            var ddlAdmissionBatchFrom = document.getElementById(ddlAdmissionBatchFrom);
            if (ddlAdmissionBatchFrom.value == 0) {
                alert('Please Select Admission Batch From.', 'Warning!');
                $(ddlAdmissionBatchFrom).focus();
                return false;
            }

            var ddlBatchCopy = $("[id$=ddlBatchCopy]").attr("id");
            var ddlBatchCopy = document.getElementById(ddlBatchCopy);
            if (ddlBatchCopy.value == 0) {
                alert('Please Select Admission Batch To.', 'Warning!');
                $(ddlBatchCopy).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitCopy').click(function () {
                    Validation();
                });
            });
        });
        function validatereport() {

            var ddlBatch = $("[id$=ddlBatch]").attr("id");
            var ddlBatch = document.getElementById(ddlBatch);
            if (ddlBatch.value == 0) {
                alert('Please Select Admission Batch.', 'Warning!');
                $(ddlBatch).focus();
                return false;
            }
            else
                return true;
        }
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
</asp:Content>
