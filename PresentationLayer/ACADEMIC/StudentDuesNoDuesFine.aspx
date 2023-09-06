<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentDuesNoDuesFine.aspx.cs" Inherits="ACADEMIC_StudentDuesNoDuesFine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updresult"
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
    <script>
        $(document).ready(function () {
            var table = $('#divsessionlist').DataTable({
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
                            var arr = [0, 9];
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
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divsessionlist').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }
                                                }
                                                else if ($(node).find("textarea").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                        nodereturn += $(node).find("textarea").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }

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
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divsessionlist').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }
                                                }
                                                else if ($(node).find("textarea").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                        nodereturn += $(node).find("textarea").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }

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
                                            var arr = [0, 9];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#divsessionlist').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }
                                                }
                                                else if ($(node).find("textarea").length > 0) {
                                                    nodereturn = "";
                                                    if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                        nodereturn += $(node).find("textarea").eq(0).val();
                                                    } else {
                                                        nodereturn += "";
                                                    }

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
                var table = $('#divsessionlist').DataTable({
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
                                var arr = [0, 9];
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
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                            nodereturn += $(node).find("input:text").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }
                                                    }
                                                    else if ($(node).find("textarea").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                            nodereturn += $(node).find("textarea").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }

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
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                            nodereturn += $(node).find("input:text").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }
                                                    }
                                                    else if ($(node).find("textarea").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                            nodereturn += $(node).find("textarea").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }

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
                                                var arr = [0, 9];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#divsessionlist').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("input:text").eq(0).val() !== "Enter Amount") {
                                                            nodereturn += $(node).find("input:text").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }
                                                    }
                                                    else if ($(node).find("textarea").length > 0) {
                                                        nodereturn = "";
                                                        if ($(node).find("textarea").eq(0).val() !== "Enter Remarks") {
                                                            nodereturn += $(node).find("textarea").eq(0).val();
                                                        } else {
                                                            nodereturn += "";
                                                        }

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

    <asp:UpdatePanel ID="updresult" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT DUES/ NO DUES FINE ALLOTMENT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" TabIndex="1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                            Font-Bold="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                            </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Institute Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" TabIndex="2" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                             ToolTip="Please Select Institute" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcolg" runat="server" ControlToValidate="ddlColg"
                                            Display="None" ErrorMessage="Please Select Institute" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree </label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch </label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="4" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme </label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Scheme" ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" TabIndex="6" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="7" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--                                <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudentType" runat="server" AppendDataBoundItems="True" TabIndex="8" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Backlog</asp:ListItem>
                                            <asp:ListItem Value="2">Revaluation</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStudentType"
                                            Display="None" ErrorMessage="Please Select Student Type" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Type </label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Revaluation</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvExam" runat="server" ControlToValidate="ddlExamType"
                                            Display="None" ErrorMessage="Please Select Exam Type" InitialValue="-1"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudent" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStudent" runat="server" ControlToValidate="ddlStudent"
                                            Display="None" ErrorMessage="Please Select Student" SetFocusOnError="True">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" 
                                    Text="Show" ValidationGroup="report" CssClass="btn btn-primary" />
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                    Text="Submit" ValidationGroup="report" CssClass="btn btn-primary" Enabled="false" />
                                <asp:HiddenField runat="server" ID="hfval" Value="0" />
                                <%--    <asp:Button ID="btnLock" runat="server"
                                    Text="Lock" ValidationGroup="report" CssClass="btn btn-primary" OnClientClick="return ConfirmLock();" Enabled="false"  />--%>
                                <%--   <asp:Button ID="btnUnlock" runat="server"
                                    Text="Unpublish" ValidationGroup="report" CssClass="btn btn-primary" ToolTip="Unpublish" TabIndex="10" OnClientClick="return ConfirmUnLock();" Enabled="false"  />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" CausesValidation="False" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlLv" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Select Student</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <%--<asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" />&nbsp;Select--%>
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                        <th>Remarks
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
                                                    <asp:CheckBox ID="chkStudent" runat="server" ToolTip="Select to view tabulation chart" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudname" Font-Bold="true" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAmount" Style="text-align: center;" Text='<%# Eval("AMOUNT")%>' BorderStyle="Groove" runat="server" TextMode="SingleLine" Height="50px" class="form-control"></asp:TextBox>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="watersub" WatermarkText="Enter Amount" TargetControlID="txtAmount" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtAmount" runat="server" FilterType="Numbers, Custom"
                                                        ValidChars="." TargetControlID="txtAmount" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRemarks" BorderStyle="Groove" runat="server" Text='<%# Eval("REMARKS")%>' TextMode="MultiLine" Height="50px" class="form-control"></asp:TextBox>
                                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Enter Remarks" TargetControlID="txtRemarks" runat="server">
                                                    </ajaxToolKit:TextBoxWatermarkExtender>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblStatus"></asp:Label>
                                                </td>

                                                <%--  <td>
                                                    <asp:Label runat="server" ID="lblLockstatus" Font-Bold="true" Text='<%# (Eval("LOCK_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("LOCK") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>--%>
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
            <%--<asp:PostBackTrigger ControlID="btnShow" />--%>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">

        function ConfirmApproval(objMsg) {
            if (confirm(objMsg)) {
                document.getElementById("<%= hfval.ClientID %>").value = "1";
                //alert("execute code.");
                return true;
            }
            else
                return false;
        }

        //function Confirm() {
        //    var confirm_value = document.createElement("INPUT");
        //    confirm_value.type = "hidden";
        //    confirm_value.name = "confirm_value";
        //    if (confirm("It seems that some record(s) are already exist. Do you still want to continue?")) {
        //        confirm_value.value = "Yes";
        //    } else {
        //        confirm_value.value = "No";
        //    }
        //    document.forms[0].appendChild(confirm_value);
        //}
    </script>
</asp:Content>

