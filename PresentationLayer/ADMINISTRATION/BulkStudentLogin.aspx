<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BulkStudentLogin.aspx.cs" Inherits="ADMINISTRATION_BulkStudentLogin" Title="" %>

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

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <script>
        $(document).ready(function () {
            var table = $('#tblParent').DataTable({
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
                                return $('#tblParent').DataTable().column(idx).visible();
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
                                            return $('#tblParent').DataTable().column(idx).visible();
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
                                            return $('#tblParent').DataTable().column(idx).visible();
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
                var table = $('#tblParent').DataTable({
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
                                    return $('#tblParent').DataTable().column(idx).visible();
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
                                                return $('#tblParent').DataTable().column(idx).visible();
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
                                                return $('#tblParent').DataTable().column(idx).visible();
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
                        <div class="box-body">

                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tabUploadStudent">Upload Student</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tabCreateLogin">Create Student Login</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tabCreateParentLogin">Create Parent Login</a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tabUploadStudent">
                                        <asp:UpdatePanel ID="updpnl" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnl" runat="server">
                                                    <div class="col-12 mt-3">
                                                        <div class="sub-heading">
                                                            <h5>Upload Admission Data</h5>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 mt-2">
                                                        <div class="row">
                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note</h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Before import excel, kindly ensure that School/College, Degree, Branch available in ERP master. If not available then do the Master entry in ERP then upload excel.</span>  </p>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Before import excel, kindly ensure that Column Names And Data Format is same as available in blank Excel Sheet.</span></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12 mt-3">
                                                                <div class="sub-heading">
                                                                    <h5>Student Admission Data Import</h5>
                                                                </div>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Admission Batch</label>
                                                                    <%--    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                </div>
                                                                <asp:DropDownList ID="ddlStudAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                                    TabIndex="1">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudAdmBatch"
                                                                    ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Attach Excel File</label>
                                                                </div>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" ToolTip="Select file to upload" TabIndex="2" />
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

                                                        <asp:LinkButton ID="btnExport" runat="server" CssClass="btn btn-primary" TabIndex="3" OnClick="btnExport_Click"
                                                            Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" Enabled="true"> Download Blank Excel Sheet Template</asp:LinkButton>

                                                        <asp:LinkButton ID="btnExportUploadLog" runat="server" CssClass="btn btn-info" TabIndex="5" Text="Export to Excel" ToolTip="Export to Excel" Enabled="true" OnClick="btnExportUploadLog_Click">Export to Excel</asp:LinkButton>
                                                        <asp:LinkButton ID="btnCancelUpload" runat="server" CssClass="btn btn-warning" TabIndex="5" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelUpload_Click" Enabled="true">Cancel</asp:LinkButton>


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

                                                    <div class="col-12 mt-3 d-none">
                                                        <asp:ListView ID="lvStudent" runat="server" OnItemCommand="lvStudent_ItemCommand" Visible="false">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Student List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Row No.</th>
                                                                            <th><%--Reg. No.--%>
                                                                                <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Roll No. 
                                                                            </th>
                                                                            <th>Gender
                                                                            </th>
                                                                            <th><%--Admission Type --%>
                                                                                <asp:Label ID="blDYddlAdmType" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Sem--%>
                                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Year
                                                                            </th>
                                                                            <%--  <th>Adm. Batch
                                                        </th>--%>
                                                                            <th>Student Name
                                                                            </th>
                                                                            <th>Admission Date
                                                                            </th>
                                                                            <th>DOB
                                                                            </th>
                                                                            <th>Father's Name
                                                                            </th>
                                                                            <th>Category
                                                                            </th>
                                                                            <th>Phy. Hand.
                                                                            </th>
                                                                            <th>Mobile Number
                                                                            </th>
                                                                            <th>Email Id
                                                                            </th>
                                                                            <th>College Name
                                                                            </th>
                                                                            <th><%--Degree--%>
                                                                                <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th><%--Branch--%>
                                                                                <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                            </th>
                                                                            <th>Address1
                                                                            </th>
                                                                            <th>Address2
                                                                            </th>
                                                                            <th>Address3
                                                                            </th>
                                                                            <th>Country
                                                                            </th>
                                                                            <th>State
                                                                            </th>
                                                                            <th>District
                                                                            </th>
                                                                            <th>Pin Code
                                                                            </th>
                                                                            <th>Board
                                                                            </th>
                                                                            <th>YR_12
                                                                            </th>
                                                                            <th>PR_12
                                                                            </th>
                                                                            <th>Blood Grp
                                                                            </th>
                                                                            <th>Payment Type
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
                                                                    <td><%# Container.DataItemIndex +1 %></td>
                                                                    <td>
                                                                        <asp:Label ID="lblRegNo" runat="server" Text='<%# Eval("REGISTRATIONNO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("ROLLNO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("GENDER")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIdType" runat="server" Text='<%# Eval("ADMISSIONTYPE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR")%>'></asp:Label>
                                                                    </td>
                                                                    <%--<td>
                                                    <asp:Label ID="lblAdmBatch"  runat="server" Text='<%# Eval("ADMBATCH")%>'></asp:Label>
                                                </td>--%>
                                                                    <td>
                                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDENTNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAdmissionDate" runat="server" Text='<%# Eval("ADMISSION_DATE") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFatherName" runat="server" Text='<%# Eval("FATHERNAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CATEGORY")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPH" runat="server" Text='<%# Eval("PHYSICALLY_HANDICAPPED")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGENAME")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAdd1" runat="server" Text='<%# Eval("ADDRESS1")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAdd2" runat="server" Text='<%# Eval("ADDRESS2")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblAdd3" runat="server" Text='<%# Eval("ADDRESS3")%>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("COUNTRYNAME")%>'></asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDistrict" runat="server" Text='<%# Eval("DISTRICT")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPin" runat="server" Text='<%# Eval("PINCODE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBoard" runat="server" Text='<%# Eval("BOARD")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblYR12" runat="server" Text='<%# Eval("YR_12")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPR12" runat="server" Text='<%# Eval("PR_12")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBloogGrp" runat="server" Text='<%# Eval("BLOODGROUP")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PAYMENT_TYPE")%>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSection" runat="server" Text='<%# Eval("SECTIONNAME")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
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
                                                <asp:AsyncPostBackTrigger ControlID="ddlColg" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlDegree" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlBranch" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane fade" id="tabCreateLogin">
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
                                                    <div class="sub-heading">
                                                        <h5>Bulk Student User Creation</h5>
                                                    </div>
                                                </div>
                                                <div class="col-12 mt-2">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Admission Batch</label>--%>
                                                                <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="false">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                                Display="None" ErrorMessage="Please Select Admission batch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Institute Name</label>--%>
                                                                <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlColg" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged"
                                                                ToolTip="Please Select Institute">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlColg"
                                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="SubPercentage"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Degree</label>--%>
                                                                <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <%--<label>Branch</label>--%>
                                                                <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="4">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <%--<label>Semester</label>--%>
                                                                <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddSemester"
                                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <%--<div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                            --%>
                                                            <%-- <asp:RadioButtonList ID="rdobtn"  OnSelectedIndexChanged="rdobtn_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text=" "> Password Same As Registration Number &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2"> Genarate Random Password &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="3"> Custom Password </asp:ListItem>
                                                    </asp:RadioButtonList>  --%>
                                                            <asp:RadioButton ID="rdRegPass" GroupName="Student" runat="server" AutoPostBack="true" OnCheckedChanged="rdRegPass_CheckedChanged" Text="Password Same As Registration No." Checked="true" />
                                                            <br />
                                                            <asp:RadioButton ID="rdGeneratepass" GroupName="Student" runat="server" AutoPostBack="true" OnCheckedChanged="rdGeneratepass_CheckedChanged" Text=" Generate Random Password" /><br />
                                                            <asp:RadioButton ID="rdoCustomPass" GroupName="Student" runat="server" AutoPostBack="true" OnCheckedChanged="rdoCustomPass_CheckedChanged" Text=" Custom Password" />


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divPassWord">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Enter Password</label>

                                                            </div>

                                                            <asp:TextBox ID="txtEnterPassword" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEnterPassword"
                                                                ErrorMessage="Please Enter Custom password" ValidationGroup="report" Display="None"></asp:RequiredFieldValidator>
                                                        </div>


                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="SubPercentage"
                                                        CssClass="btn btn-primary" TabIndex="6" />
                                                    <%-- <asp:Button ID="btnUpdate" runat="server" Enabled="false" Text="Create Users" OnClick="btnUpdate_Click"
                                                CssClass="btn btn-primary" TabIndex="7" />--%>
                                                    <%--    <asp:Button ID="btnCreateLogin" runat="server" Enabled="false" Text="Create Login"  CssClass="btn btn-primary"  AutoPostBack="true" OnClick="btnCreateLogin_Click" />--%>
                                                    <asp:Button ID="btnCreateLogin" runat="server" Text="Create Login" ValidationGroup="SubPercentage" CssClass="btn btn-primary" OnClick="btnCreateLogin_Click" />

                                                    <asp:Button ID="btnSendEmail" runat="server" Enabled="true" Text="Send Email" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" />
                                                    <%-- <asp:Button ID="btnSendSMS" runat="server" Enabled="true" Text="Send SMS" CssClass="btn btn-primary" Visible="false" OnClick="btnSendSMS_Click" TabIndex="8" />--%>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="9"
                                                        CausesValidation="False" CssClass="btn btn-warning" />
                                                    <%--   <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="SubPercentage" OnClick="btnCreateLogin_Click" />--%>


                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="SubPercentage"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>

                                                <div class="col-12">
                                                    <div id="dvListView">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvStudents" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <div class=" note-div">
                                                                            <h5 class="heading">Note</h5>
                                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Email will send only those students whose user is created and marked as "Created".</span></p>
                                                                        </div>
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <table id="tblStudents" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                                        Select </th>
                                                                                    <th>Login Status</th>
                                                                                    <th>Enrollment No. </th>
                                                                                    <th>Student Name </th>
                                                                                    <th>Mobile No </th>
                                                                                    <th>Email ID </th>
                                                                                    <th>Branch</th>
                                                                                    <th>Semester</th>
                                                                                    <%--   <th>DOB </th>
                                                                                    --%>
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
                                                                            <asp:CheckBox ID="chkRow" runat="server" Checked='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 0 ?  false : true )%>' Font-Bold="true" ForeColor="Green" onclick="CountSelection();" Text='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  "CREATED" : "" )%>' />
                                                                            <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLogin" runat="server" Text='<%# Eval("UA_FIRSTLOG") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblreg" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("ADMCAN")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblstud" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("DOBNEW")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("STUDENTMOBILE")%>' ToolTip='<%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNAME")%>' ToolTip='<%# Eval("BRANCHNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <%--   <td>
                                                                    <asp:Label ID="lblDob" runat="server" Text='<%# Eval("DOB")%>' ToolTip='<%# Eval("DOB")%>'></asp:Label>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                        <div id="div2" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%-- Added for parent login --%>
                                    <div class="tab-pane fade" id="tabCreateParentLogin">
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
                                                    <div class="sub-heading">
                                                        <h5>Bulk Parent User Creation</h5>
                                                    </div>
                                                </div>
                                                <div class="col-12 mt-2">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Admission Batch</label>
                                                                <asp:Label ID="lblPAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlParentAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="false">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlParentAdmBatch"
                                                                Display="None" ErrorMessage="Please Select Admission batch" InitialValue="0" ValidationGroup="PLogin"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Institute Name</label>
                                                                <asp:Label ID="lblPInstituteName" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlParentColg" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                                AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlParentColg_SelectedIndexChanged"
                                                                ToolTip="Please Select Institute">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlParentColg"
                                                                Display="None" ErrorMessage="Please Select Institute" ValidationGroup="PLogin"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Degree</label>
                                                                <asp:Label ID="lblDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlParentDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlParentDegree_SelectedIndexChanged" TabIndex="3">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlParentDegree"
                                                                Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="PLogin"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>Branch</label>
                                                                <asp:Label ID="lblParentBranch" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlParentBranch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlParentBranch_SelectedIndexChanged" AutoPostBack="true" TabIndex="4">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <label>Semester</label>
                                                                <asp:Label ID="lblParentSemester" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlParentSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddSemester"
                                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="SubPercentage"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <%--<div class="label-dynamic">
                                                        <label></label>
                                                    </div>
                                                            --%>
                                                            <%-- <asp:RadioButtonList ID="rdobtn"  OnSelectedIndexChanged="rdobtn_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text=" "> Password Same As Registration Number &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="2"> Genarate Random Password &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="3"> Custom Password </asp:ListItem>
                                                    </asp:RadioButtonList>  --%>
                                                            <asp:RadioButton ID="rdoParentRegpd" GroupName="Parent" runat="server" AutoPostBack="true" OnCheckedChanged="rdoParentRegpd_CheckedChanged" Text="Password Same As Registration No." Checked="true" />
                                                            <br />
                                                            <asp:RadioButton ID="rdoParentGeneratepd" GroupName="Parent" runat="server" AutoPostBack="true" OnCheckedChanged="rdoParentGeneratepd_CheckedChanged" Text=" Generate Random Password" /><br />
                                                            <asp:RadioButton ID="rdoParentCustompd" GroupName="Parent" runat="server" AutoPostBack="true" OnCheckedChanged="rdoParentCustompd_CheckedChanged" Text=" Custom Password" />


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divParentPassword">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Enter Password</label>

                                                            </div>

                                                            <asp:TextBox ID="txtPEnterPass" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPEnterPass"
                                                                ErrorMessage="Please Enter Custom password" ValidationGroup="PLogin" Display="None"></asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="BtnParentShow" runat="server" Text="Show" ValidationGroup="PLogin"
                                                        CssClass="btn btn-primary" OnClick="BtnParentShow_Click" />
                                                    <%-- <asp:Button ID="btnUpdate" runat="server" Enabled="false" Text="Create Users" OnClick="btnUpdate_Click"
                                                CssClass="btn btn-primary" TabIndex="7" />--%>
                                                    <%--    <asp:Button ID="btnCreateLogin" runat="server" Enabled="false" Text="Create Login"  CssClass="btn btn-primary"  AutoPostBack="true" OnClick="btnCreateLogin_Click" />--%>
                                                    <asp:Button ID="BtnCreateParentLogin" CssClass="btn btn-primary" OnClick="BtnCreateParentLogin_Click" runat="server" Text="Create Login" ValidationGroup="PLogin" />

                                                    <asp:Button ID="btnSendParentEmail" runat="server" Enabled="true" Text="Send Email" OnClick="btnSendParentEmail_Click" CssClass="btn btn-primary" />
                                                    <%-- <asp:Button ID="btnSendSMS" runat="server" Enabled="true" Text="Send SMS" CssClass="btn btn-primary" Visible="false" OnClick="btnSendSMS_Click" TabIndex="8" />--%>
                                                    <asp:Button ID="btnPCancel" runat="server" Text="Cancel" OnClick="btnPCancel_Click"
                                                        CausesValidation="False" CssClass="btn btn-warning" />
                                                    <%--   <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-warning" ValidationGroup="SubPercentage" OnClick="btnCreateLogin_Click" />--%>


                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="PLogin"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>


                                                <%-- <div>
                                                 <span style="font-size: small; color: green;">(Note :Record saving Parent mobile number are Displayed)</span>
                                                 </div>--%>
                                                <div class="col-12">
                                                    <div id="Div4">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <asp:ListView ID="lvParent" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class=" note-div">
                                                                        <h5 class="heading">Note</h5>
                                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Record having parent mobile number and father Name are displayed.</span>  </p>
                                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Email will send only those parents whose user is created and marked as "Created".</span></p>
                                                                    </div>
                                                                    <div id="demo-grid">
                                                                        <div class="sub-heading">
                                                                            <h5>Parent List</h5>
                                                                        </div>
                                                                        <table id="tblParent" class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>
                                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" />
                                                                                        Select </th>
                                                                                    <th>Login Status</th>
                                                                                    <th>Registration No. </th>
                                                                                    <th>Student Name </th>
                                                                                    <th>Father Name</th>
                                                                                    <th>Father Email ID</th>
                                                                                    <th>Father Mobile No</th>
                                                                                    <%--<th>Mobile No </th>
                                                                                    <th>Email ID </th>
                                                                                    <th>Branch</th>
                                                                                    <th>Semester</th>--%>
                                                                                    <%--   <th>DOB </th>
                                                                                    --%>
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
                                                                            <asp:CheckBox ID="chkRowParent" runat="server" Checked='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 0 ?  false : true )%>' Font-Bold="true" ForeColor="Green" onclick="CountSelection();" Text='<%# (Convert.ToInt32(Eval("LOGIN_STATUS") )== 1 ?  "CREATED" : "" )%>' />
                                                                            <asp:HiddenField ID="hidStudentId" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPLogin" runat="server" Text='<%# Eval("UA_FIRSTLOG") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPreg" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("ADMCAN")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblPstudent" runat="server" Text='<%# Eval("STUDNAME")%>' ToolTip='<%# Eval("DOBNEW")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblFathername" runat="server" Text='<%# Eval("FATHERNAME")%>' ToolTip='<%# Eval("FATHERNAME")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblFatherEmailID" runat="server" Text='<%# Eval("FATHER_EMAIL")%>' ToolTip='<%# Eval("FATHER_EMAIL")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblFatherMobile" runat="server" Text='<%# Eval("FATHERMOBILE")%>' ToolTip='<%# Eval("FATHERMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <%--<td>
                                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("STUDENTMOBILE")%>' ToolTip='<%# Eval("STUDENTMOBILE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EMAILID")%>' ToolTip='<%# Eval("EMAILID")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCHNAME")%>' ToolTip='<%# Eval("BRANCHNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNAME")%>'></asp:Label>
                                                                        </td>--%>
                                                                        <%--   <td>
                                                                    <asp:Label ID="lblDob" runat="server" Text='<%# Eval("DOB")%>' ToolTip='<%# Eval("DOB")%>'></asp:Label>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                        <div id="div5" runat="server">
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>

                                                <%--                                                 <asp:PostBackTrigger ControlID="btnParentShow" />
                                                --%>
                                                <asp:AsyncPostBackTrigger ControlID="ddlParentColg" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlParentDegree" />
                                                <asp:AsyncPostBackTrigger ControlID="ddlParentBranch" />
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
    <div id="divMsg" runat="server"></div>

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

