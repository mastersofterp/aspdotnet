<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Minor_creation.aspx.cs" Inherits="ACADEMIC_Minor_creation" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
      <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tbladdcourse').DataTable({
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
                                    return $('#tbladdcourse').DataTable().column(idx).visible();
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
                                                return $('#tbladdcourse').DataTable().column(idx).visible();
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
                                                return $('#tbladdcourse').DataTable().column(idx).visible();
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
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).ready(function () {
                    var table = $('#tbladdcourse').DataTable({
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
                                        return $('#tbladdcourse').DataTable().column(idx).visible();
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
                                                    return $('#tbladdcourse').DataTable().column(idx).visible();
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
                                                    return $('#tbladdcourse').DataTable().column(idx).visible();
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
                    });
                });
            });

        </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updNotify"
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
        <%--AssociatedUpdatePanelID="updBulkReg"--%>
    </div>

    <asp:UpdatePanel ID="updNotify" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Minor Creation</h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom ml-md-2">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Minor Creation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Add Course</a>
                                    </li>
                                </ul>

                                <div class="tab-content" id="my-tab-content">
                                    <div id="tab_1" class="tab-pane active">
                                        <div>
                                             <asp:UpdatePanel ID="updMinor" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-12 mt-3">
                                                            <div class="row">
                                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>College & Program</label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" Display="None" ControlToValidate="ddlCollege" ErrorMessage="Please Select College & Program!!!" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Minor</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtminor" runat="server" onkeyup="validateAlphabet(this);" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtminor" runat="server" Display="None" ControlToValidate="txtminor" ErrorMessage="Please Enter Minor!!!" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group col-lg-4 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>*</sup>
                                                                        <label>Credit</label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCredit" runat="server" MaxLength="2" onkeyup="validateNumeric(this);" CssClass="form-control" />
                                                                    <asp:RequiredFieldValidator ID="rfvtxtCredit" runat="server" Display="None" ControlToValidate="txtCredit" ErrorMessage="Please Enter Credit!!!" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-12 btn-footer mt-3">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" CssClass="btn btn-primary"/>
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-warning" />
                                                            <asp:ValidationSummary ID="validationMinor" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="submit" DisplayMode="List" />
                                                        </div>
                                                        <div class="col-12">
                                                            <asp:HiddenField ID="hdfsub" runat="server" />
                                                            <asp:Panel ID="pnlMinor" runat="server" Visible="true">
                                                                <asp:ListView ID="lvMinor" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="table-responsive">
                                                                            <table id="example" class="table table-striped table-bordered display" style="width: 100%;">
                                                                                <thead>
                                                                                    <tr>
                                                                                        <th>Edit</th>
                                                                                        <th>College & Program</th>
                                                                                        <th>Minor</th>
                                                                                        <th>Credits</th>
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
                                                                            <td><asp:ImageButton ID="imgbtn" runat="server" CommandArgument='<%#Eval("MNR_GRP_NO")%>' ToolTip="Edit Record" ImageUrl="~/Images/edit.png" OnClick="imgbtn_Click"/></td>
                                                                            <td><asp:Label ID="lblClgPrg" runat="server" Text='<%# Eval("COLLEGE_PROGRAM") %>' ToolTip='<%#Eval("CDBNO")%>'></asp:Label></td>
                                                                            <td><asp:Label ID="lblMinor" runat="server" Text='<%# Eval("MNR_GRP_NAME") %>' ToolTip='<%#Eval("COLLEGE_ID")%>'></asp:Label></td>
                                                                            <td><asp:Label ID="lblCredit" runat="server" Text='<%# Eval("CREDIT") %>' ToolTip='<%#Eval("BRANCHNO")%>'></asp:Label></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </ContentTemplate>
                                             </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div id="tab_2" class="tab-pane">
                                        <div>
                                            <asp:UpdatePanel ID="updCourse" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="col-12 mt-3 ">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>College & Program</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddldegreeBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddldegreeBranch_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddldegreeBranch" runat="server" Display="None" ControlToValidate="ddldegreeBranch" ErrorMessage="Please Select College & Program!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Minor</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCourseMinor" runat="server" AppendDataBoundItems="true" TabIndex="3" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:ListBox ID="ddlCourseMinor" runat="server" TabIndex="3" AppendDataBoundItems="true" SelectionMode="Multiple" CssClass="form-control multi-select-demo" ></asp:ListBox>
                                                                <%--<asp:ListBox ID="ddlCourseMinor" runat="server" TabIndex="3" AppendDataBoundItems="true" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>--%>
                                                                <asp:RequiredFieldValidator ID="rfvddlCourseMinor" runat="server" Display="None" ControlToValidate="ddlCourseMinor" ErrorMessage="Please Select Minor!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Scheme</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemesterminor" runat="server" AppendDataBoundItems="true" TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlSemesterminor_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvddlSemesterminor" runat="server" Display="None" ControlToValidate="ddlSemesterminor" ErrorMessage="Please Select Scheme!!!" InitialValue="0" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>*</sup>
                                                                    <label>Courses</label>
                                                                </div>
                                                                <%--<asp:DropDownList ID="ddlCourses" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                                <asp:ListBox ID="ddlCourses" runat="server" AppendDataBoundItems="true" TabIndex="4" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                                                <asp:RequiredFieldValidator ID="rfvddlCourses" runat="server" Display="None" ControlToValidate="ddlCourses" ErrorMessage="Please Select Course!!!" ValidationGroup="submit2"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitCourse" runat="server" Text="Submit" OnClick="btnSubmitCourse_Click" ValidationGroup="submit2" CssClass="btn btn-primary"/>
                                                        <asp:Button ID="btnCancelCourse" runat="server" Text="Cancel" OnClick="btnCancelCourse_Click" CssClass="btn btn-warning"/>
                                                        <asp:ValidationSummary ID="ValidateCourse" runat="server" ShowMessageBox="true" ShowSummary="false" ShowValidationErrors="true" ValidationGroup="submit2" DisplayMode="List" />
                                                    </div>
                                                    <div class="col-12 mt-3">
                                                        <asp:HiddenField ID="hdfcourse" runat="server" />
                                                        <asp:Panel ID="pnlCourse" runat="server" Visible="true">
                                                            <asp:ListView ID="lvCourse" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="table-responsive">
                                                                        <table id="tbladdcourse" class="table table-striped table-bordered display" style="width: 100%;">
                                                                            <thead>
                                                                                <tr>
                                                                                    <th>Edit</th>
                                                                                    <th>College & Program</th>
                                                                                    <th>Minor</th>
                                                                                    <th>Scheme</th>
                                                                                    <th>Course</th>
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
                                                                        <td><asp:ImageButton ID="imgbtnCourse" runat="server" CommandArgument='<%#Eval("MNR_OFFERED_COURSE_NO")%>' ToolTip="Edit Record" ImageUrl="~/Images/edit.png" OnClick="imgbtnCourse_Click"/></td>
                                                                        <td><asp:Label ID="lblClgProg" runat="server" Text='<%# Eval("COLLEGE_PROGRAM") %>' ToolTip='<%#Eval("CDBNO")%>'></asp:Label></td>
                                                                        <td><asp:Label ID="lblMinorN" runat="server" Text='<%# Eval("MNR_GRP_NAME") %>' ToolTip='<%#Eval("COLLEGE_ID")%>'></asp:Label></td>
                                                                        <td><asp:Label ID="lblScheme" runat="server" Text='<%# Eval("SCHEMENAME") %>' ToolTip='<%#Eval("SCHEMENO")%>'></asp:Label></td>
                                                                        <td><asp:Label ID="lblCourse" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%#Eval("COURSENO")%>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>

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
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitCourse" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('example');
                list = 'lvMinor';
            }
            //else if (headid == 2) {
            //    tbl = document.getElementById('exampleCourse');
            //    list = 'lvCourse';
            //}

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
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
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Numeric Characters allowed !");
                return false;
            }
            else
                return true;
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z ]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.value = '';
                txt.focus = true;
                alert("Only Alphabets allowed!");
                return false;
            }
            //else
            //    return true;

            var textbox = document.getElementById('<%= txtminor.ClientID %>');
            var text = textbox.value;
            
            // Remove extra spaces
            text = text.replace(/\s+/g, " ");

            // Restrict to only one space
            //if (text.indexOf(" ") !== -1 && text.lastIndexOf(" ") !== text.indexOf(" ")) {
            //    var firstSpaceIndex = text.indexOf(" ");
            //    text = text.substring(0, firstSpaceIndex) + text.substring(firstSpaceIndex + 1);
            //}

            // Update the textbox value
            textbox.value = text;

            return true;
        }

</script>
</asp:Content>

