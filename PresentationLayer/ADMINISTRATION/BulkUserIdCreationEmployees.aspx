<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkUserIdCreationEmployees.aspx.cs" Inherits="ADMINISTRATION_Bulk_User_Id_Creation_Employees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upduser"
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
    <%--add prashant sir login messages show--%>
    <script type="text/javascript" language="javascript">
        $("#lblmessage").delay(500).fadeOut(100);
    </script>
    <%-- <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>

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

    <asp:UpdatePanel ID="upduser" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">

                                <div class="nav-tabs-custom">
                                    <div class="nav-tabs-custom">
                                        <ul class="nav nav-tabs" role="tablist">

                                            <li class="nav-item">
                                                <a class="nav-link active" data-toggle="tab" href="#tab_2">Bulk Employee User Creation</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link " data-toggle="tab" href="#tab_1">Email Send </a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link " data-toggle="tab" href="#tab_3">Import Employee </a>
                                            </li>
                                        </ul>
                                        <%-- <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tabUploadStudent">Upload Student</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tabCreateLogin">Create Login</a>
                                    </li>
                                </ul>--%>
                                        <div class="tab-content" id="my-tab-content">
                                            <div class="tab-pane fade" id="tab_1">
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
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Employee Send Email</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>School/Institute</label>
                                                                        <%--<asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCollege1" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCollege1" runat="server" ControlToValidate="ddlCollege1"
                                                                        Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Staff</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlStaff2" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvStaff2" runat="server" ControlToValidate="ddlStaff2"
                                                                        Display="None" ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>User Type</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlEmployeeType3" runat="server" AppendDataBoundItems="True" data-select2-enable="true" AutoPostBack="true"
                                                                        CssClass="form-control">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlEmployeeType3" runat="server" ControlToValidate="ddlEmployeeType3"
                                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee User Type"
                                                                        InitialValue="0" ValidationGroup="Admin1"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnShow1" runat="server" Text="Show" ValidationGroup="Admin1"
                                                                CssClass="btn btn-primary" OnClick="btnShow1_Click" />
                                                            <%----%>
                                                            <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" ValidationGroup="Admin1" Enabled="True"
                                                                CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                                                            <asp:Button ID="btnCancel1" runat="server" Text="Cancel"
                                                                CausesValidation="False" CssClass="btn btn-warning" OnClick="btnCancel1_Click" />
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="Admin1" />
                                                        </div>
                                               <%--         --Updated on 14092023--%>
                                                    <div id="Div3" class="col-12">
                                                        <asp:Panel ID="Panel3" runat="server">
                                                            <asp:ListView ID="ListView1" runat="server" Visible="false">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Employee List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100% !important;" id="tblStudents">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />Select
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblEmpCode1" runat="server"></asp:Label>
                                                                                </th>
                                                                                <th>Employee Name</th>
                                                                                <th>User Name</th>
                                                                                <th>Mobile No </th>
                                                                                <th>Email ID </th>
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
                                                                            <asp:CheckBox ID="chkRow" runat="server" Font-Bold="true" ForeColor="Green" />

                                                                            <asp:HiddenField ID="hidStudentId" runat="server"
                                                                                Value='<%# Eval("IDNO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblreg" Text='<%# Eval("PFILENO")%>' ToolTip='<%# Eval("PFILENO")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblNAME" Text='<%# Eval("NAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lbluaname" Text='<%# Eval("UA_NAME")%>'></asp:Label>
                                                                            <asp:HiddenField ID="hdnUaNo" runat="server"
                                                                                Value='<%# Eval("UA_NO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("PHONENO")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("UA_EMAIL")%>'></asp:Label>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="tab-pane active " id="tab_2">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBatch"
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
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Bulk Employee User Creation</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>School/Institute</label>
<%--                                                                        <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                                        Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <label>Staff</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlStaff" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvStaff" runat="server" ControlToValidate="ddlStaff"
                                                                        Display="None" ErrorMessage="Please Select Staff" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>User Type</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="True" data-select2-enable="true"
                                                                        CssClass="form-control">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlEmployeeType" runat="server" ControlToValidate="ddlEmployeeType"
                                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee User Type"
                                                                        InitialValue="0" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <asp:Label runat="server" ID="lblmessage" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Admin"
                                                                OnClick="btnShow_Click" CssClass="btn btn-primary" />
                                                            <%----%>
                                                            <asp:Button ID="btnUpdate" runat="server" Text="Create Users" ValidationGroup="Admin" Enabled="true"
                                                                OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel3_Click"
                                                                CausesValidation="False" CssClass="btn btn-warning" />
                                                            <asp:ValidationSummary ID="vsSelection" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="Admin" />
                                                        </div>

                                                        <div id="dvListView" class="col-12">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvStudents" runat="server" Visible="false">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Employee List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100% !important;" id="">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />Select
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                                                                                    </th>
                                                                                    <th>Employee Name
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
                                                                                <asp:CheckBox ID="chkRow" runat="server" onclick="CountSelection();" Font-Bold="true" ForeColor="Green"
                                                                                    Enabled='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  false : true )%>' Text='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  "CREATED" : "" )%>'
                                                                                    Checked='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 0 ?  false : true )%>' />
                                                                                <asp:HiddenField ID="hidStudentId" runat="server"
                                                                                    Value='<%# Eval("IDNO")%>' />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblreg" Text='<%# Eval("PFILENO")%>' ToolTip='<%# Eval("PFILENO")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label runat="server" ID="lblstud" Text='<%# Eval("NAME")%>'></asp:Label>

                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="tab-pane fade " id="tab_3">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updBatch"
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
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-md-12 col-sm-12 col-12">
                                                                <%--<div class="box box-primary">--%>
                                                                <%--<div class="box-body">--%>

                                                                <div class="nav-tabs-custom">

                                                                    <div class="tab-content">
                                                                        <div class="tab-pane active" id="tabUploadStudent">
                                                                            <asp:UpdatePanel ID="updpnl" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:Panel ID="pnl" runat="server">
                                                                                        <%-- <div class="box-header with-border">
                                                                                                <h3 class="box-title">Upload Employee Data</h3>
                                                                                            </div>--%>
                                                                                        <div class="col-12 mt-2">
                                                                                            <div class="row">
                                                                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-12">
                                                                                            <div class="row">
                                                                                                <div class="col-12">
                                                                                                    <div class="sub-heading">
                                                                                                        <h5>Employee Data Import</h5>
                                                                                                    </div>
                                                                                                </div>


                                                                                                <div class="col-md-3">
                                                                                                    <label><span style="color: red;">*</span> User Type</label>
                                                                                                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True"
                                                                                                        CssClass="form-control">
                                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmployeeType"
                                                                                                        Display="None" SetFocusOnError="true" ErrorMessage="Please Select Employee User Type"
                                                                                                        InitialValue="0" ValidationGroup="Admin"></asp:RequiredFieldValidator>
                                                                                                </div>

                                                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                                                    <div class="label-dynamic">
                                                                                                        <sup>* </sup>
                                                                                                        <label>Attach Excel File</label>
                                                                                                    </div>
                                                                                                    <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="Select file to upload" TabIndex="2" />
                                                                                                </div>
                                                                                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                                                                                    <div class="label-dynamic">
                                                                                                        <sup> </sup>
                                                                                                        <label>With Master</label>
                                                                                                    </div>
                                                                                                    <asp:CheckBox ID="chismaster" runat="server" ToolTip="Check if no need to master data" TabIndex="3" />
                                                                                                </div>

                                                                                                <div class="form-group col-lg-3 col-md-6 col-12 d-none" id="divRecords" runat="server" visible="false">
                                                                                                    <div class="label-dynamic">
                                                                                                        <sup>* </sup>
                                                                                                        <label>Already Saved Records</label>
                                                                                                    </div>
                                                                                                    <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                                                                </div>

                                                                                                <div class="col-lg-3 col-md-6 col-12" runat="server" id="divCount" visible="false">
                                                                                                    <ul class="list-group list-group-unbordered">
                                                                                                        <li class="list-group-item" id="divrecexist" runat="server" visible="false"><b>Total Record Already Exist :</b>
                                                                                                            <a class="sub-label">
                                                                                                                <asp:Label ID="lblTotalAlreadyExistsCount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                                                        </li>

                                                                                                        <li class="list-group-item" id="divrecupload" runat="server" visible="false"><b>Total Record Uploaded :</b>
                                                                                                            <a class="sub-label">
                                                                                                                <asp:Label ID="lblTotalRecordUploadCount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                                                        </li>

                                                                                                        <li class="list-group-item" id="divRecwitherror" runat="server" visible="false"><b>Total Record With Error :</b>
                                                                                                            <a class="sub-label">
                                                                                                                <asp:Label ID="lblTotalRecordErrorCount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                                                        </li>

                                                                                                        <li class="list-group-item" id="divtotcount" runat="server" visible="false"><b>TotalCount :</b>
                                                                                                            <a class="sub-label">
                                                                                                                <asp:Label ID="lblTotalRecordCount" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                                            </a>
                                                                                                        </li>
                                                                                                        <li class="list-group-item" id="divErrorNote" runat="server" visible="false"><b>Note :</b>
                                                                                                            <a class="sub-label">
                                                                                                                <asp:Label ID="lblErrorNote" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                                            </a>
                                                                                                        </li>
                                                                                                    </ul>
                                                                                                </div>

                                                                                            </div>
                                                                                        </div>

                                                                                        <div class="col-12 btn-footer">
                                                                                            <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                                                                                        </div>

                                                                                        <div class="col-12 btn-footer">
                                                                                            <asp:LinkButton ID="btnUpload" runat="server" ValidationGroup="report" CssClass="btn btn-primary" TabIndex="4"
                                                                                                Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true" AutoPostBack="false" OnClick="btnUpload_Click">Upload Excel</asp:LinkButton>


                                                                                            <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click" Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" CssClass="btn btn-primary" />


                                                                                            <asp:LinkButton ID="btnExportUploadLog" runat="server" CssClass="btn btn-primary" TabIndex="5" Text="Export to Excel" ToolTip="Export to Excel" Enabled="true" OnClick="btnExportUploadLog_Click" Visible="false">Export to Excel</asp:LinkButton>
                                                                                            <asp:LinkButton ID="btnCancelUpload" runat="server" CssClass="btn btn-primary" TabIndex="5" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelUpload_Click" Enabled="true">Cancel</asp:LinkButton>


                                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                                                                ShowSummary="False" ValidationGroup="report" Style="text-align: center" />
                                                                                        </div>

                                                                                        <div class="form-group col-12 text-center" id="divNote" runat="server" visible="false">
                                                                                            <label><span style="color: red">Note: Excel Sheet Data is not imported, Please correct following data and upload the Excel again.</span></label>
                                                                                        </div>
                                                                                        <div class="col-12">
                                                                                            <asp:ListView ID="LvDescription" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <div class="sub-heading">
                                                                                                        <h5>Data Import Log</h5>
                                                                                                    </div>
                                                                                                    <div class="" style="height: 200px; overflow: scroll;">
                                                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                                                            <thead class="bg-light-blue">
                                                                                                                <tr>
                                                                                                                    <th>Row No</th>
                                                                                                                    <th>Description</th>
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
                                                                                                            <%--<asp:HiddenField ID="hfdvalue" runat="server" Value='<%# Eval("RowId") %>' />--%>
                                                                                                            <%--<%# Eval("RowId") %>--%>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%--<asp: ID="txtGradeName" runat="server" CssClass="form-control" MaxLength="25" Text='<%# Eval("Column1") %>' ToolTip="Please Enter Grade Name" placeholder="Grade Name"></asp:>--%>
                                                                                                            <%# Eval("Description") %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>
                                                                                            </asp:ListView>
                                                                                        </div>


                                                                                        <div id="Div1" class="col-md-12">
                                                                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                                                                <asp:ListView ID="lvEmployee" runat="server" Visible="false">
                                                                                                    <LayoutTemplate>
                                                                                                        <div id="demo-grid">

                                                                                                            <h4>Employee Data List</h4>

                                                                                                            <table id="tblEmployee" class="table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                                                                <thead>
                                                                                                                    <tr class="bg-light-blue">
                                                                                                                        <th>Row No.</th>
                                                                                                                       <%-- <th>Society Name</th>--%>
                                                                                                                        <th>College Name </th>
                                                                                                                        <th>Employee Id</th>
                                                                                                                        <th>RFIDNO</th>
                                                                                                                        <th>Title</th>
                                                                                                                        <th>First Name</th>
                                                                                                                        <th>Middle Name</th>
                                                                                                                        <th>Last Name</th>
                                                                                                                        <th>Gender</th>
                                                                                                                        <th>Fathers Name
                                                                                                                        </th>
                                                                                                                        <th>Mothers Name
                                                                                                                        </th>
                                                                                                                        <th>Husbands Name
                                                                                                                        </th>
                                                                                                                        <th>Date of Birth
                                                                                                                        </th>
                                                                                                                        <th>Date of Joining
                                                                                                                        </th>
                                                                                                                        <th>Date of Retirement
                                                                                                                        </th>
                                                                                                                        <th>Date of Increment
                                                                                                                        </th>
                                                                                                                        <th>UID No
                                                                                                                        </th>
                                                                                                                        <th>Actual Basic
                                                                                                                        </th>
                                                                                                                        <th>Grade Pay
                                                                                                                        </th>
                                                                                                                        <th>Department
                                                                                                                        </th>
                                                                                                                        <th>Designation
                                                                                                                        </th>
                                                                                                                        <th>Staff Name
                                                                                                                        </th>
                                                                                                                        <th>Mobile No
                                                                                                                        </th>
                                                                                                                        <th>E-mail ID
                                                                                                                        </th>
                                                                                                                        <th>Pay rule
                                                                                                                        </th>
                                                                                                                        <th>Pay Scale
                                                                                                                        </th>
                                                                                                                        <th>NATURE OF APPONIMENT
                                                                                                                        </th>
                                                                                                                        <th>CONSOLIDATED EMPLOYEE AMOUNT
                                                                                                                        </th>
                                                                                                                        <th>Employee Type
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
                                                                                                            <td><%# Container.DataItemIndex +1 %></td>
                                                                                                       <%--     <td>
                                                                                                                <asp:Label ID="lblSocietyName" runat="server" Text='<%# Eval("Society Name")%>'></asp:Label>
                                                                                                            </td>--%>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblSchoolName" runat="server" Text='<%# Eval("College Name")%>'></asp:Label>
                                                                                                                <asp:HiddenField runat="server" ID="hdsocity" Value=""/>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Eval("Employee Id")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblRFIDNO" runat="server" Text='<%# Eval("RFIDNO")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("First Name")%>'></asp:Label>
                                                                                                            </td>

                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMiddleName" runat="server" Text='<%# Eval("Middle Name")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("Last Name") %>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblFathersName" runat="server" Text='<%# Eval("Fathers Name")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMothersName" runat="server" Text='<%# Eval("Mothers Name")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblHusbandsName" runat="server" Text='<%# Eval("Husbands Name")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDateofBirth" runat="server" Text='<%# Eval("Date of Birth", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDateofJoining" runat="server" Text='<%# Eval("Date of Joining", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDateofRetirement" runat="server" Text='<%# Eval("Date of Retirement", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDateofIncrement" runat="server" Text='<%# Eval("Date of Increment", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblUIDNo" runat="server" Text='<%# Eval("UID No")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblActualBasic" runat="server" Text='<%# Eval("Actual Basic")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblGradePay" runat="server" Text='<%# Eval("Grade Pay")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblStaffName" runat="server" Text='<%# Eval("Staff Name")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("Mobile No")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblEmailID" runat="server" Text='<%# Eval("E-mail ID")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPayrule" runat="server" Text='<%# Eval("Pay rule")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblPayScale" runat="server" Text='<%# Eval("Pay Scale")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblNATUREOFAPPONIMENT" runat="server" Text='<%# Eval("NATURE OF APPONIMENT")%>'></asp:Label>
                                                                                                            </td>
                                                                                                            <td>
                                                                                                                <asp:Label ID="lblCONSOLIDATEDEMPLOYEEAMOUNT" runat="server" Text='<%# Eval("CONSOLIDATED EMPLOYEE AMOUNT")%>'></asp:Label>
                                                                                                            </td>
                                                                                                             <td>
                                                                                                                <asp:Label ID="lblEmployeeType" runat="server" Text='<%# Eval("Employee Type")%>'></asp:Label>
                                                                                                            </td>

                                                                                                        </tr>
                                                                                                    </ItemTemplate>
                                                                                                </asp:ListView>
                                                                                            </asp:Panel>
                                                                                        </div>


                                                                                    </asp:Panel>

                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                                                                    <asp:PostBackTrigger ControlID="btnExport" />
                                                                                    <asp:PostBackTrigger ControlID="btnExportUploadLog" />
                                                                                    <%--                                                <asp:PostBackTrigger ControlID="btnCreateLogin" />--%>
                                                                                    <%--  <asp:AsyncPostBackTrigger ControlID="btnUpload" />  --%>
                                                                                    <%--                                                <asp:AsyncPostBackTrigger ControlID="btnExport" />    --%>
                                                                                    <%-- <asp:AsyncPostBackTrigger ControlID="ddlColg" />--%>
                                                                                    <%--  <asp:AsyncPostBackTrigger ControlID="ddlDegree" />--%>
                                                                                    <%-- <asp:AsyncPostBackTrigger ControlID="ddlBranch" />--%>
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </div>

                                                                        <div class="tab-pane fade" id="tabCreateLogin">
                                                                            <div>
                                                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBatch"
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

                                                                        </div>



                                                                    </div>
                                                                </div>

                                                                <%--</div>--%>
                                                                <%--</div>--%>
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
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpdate" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server"></div>

    <script>
        function TabShow(tabid) {
            var tabName = tabid;
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
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
</asp:Content>
