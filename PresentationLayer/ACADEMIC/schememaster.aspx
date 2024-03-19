<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SchemeMaster.aspx.cs" Inherits="ACADEMIC_SchemeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_pnlSchemeAllot .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>


    <div>
        <asp:UpdateProgress ID="updDepart" runat="server" AssociatedUpdatePanelID="updMaster"
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
            var table = $('#tblStudentList').DataTable({
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
                                return $('#tblStudentList').DataTable().column(idx).visible();
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
                                            return $('#tblStudentList').DataTable().column(idx).visible();
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
                                            return $('#tblStudentList').DataTable().column(idx).visible();
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
                var table = $('#tblStudentList').DataTable({
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
                                    return $('#tblStudentList').DataTable().column(idx).visible();
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
                                                return $('#tblStudentList').DataTable().column(idx).visible();
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
                                                return $('#tblStudentList').DataTable().column(idx).visible();
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


    <asp:UpdatePanel ID="updMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Scheme Type</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Scheme Creation</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="1">College Scheme Configuration</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">Scheme Allotment</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="updprogupdSchemeType" runat="server" AssociatedUpdatePanelID="updSchemeType"
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
                                        <asp:UpdatePanel ID="updSchemeType" runat="server">
                                            <ContentTemplate>
                                                <div id="divMsg" runat="server">
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Scheme Type Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                    <label>Type Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtScheme" AutoComplete="off" TabIndex="2" placeholder="Enter Scheme Type Name" runat="server" MaxLength="50" CssClass="form-control"
                                                                    ToolTip="Please Enter Degree Type Name" />
                                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" TargetControlID="txtScheme" />--%>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Code </label>
                                                                </div>
                                                                <asp:TextBox ID="txtCode" AutoComplete="off" TabIndex="2" placeholder="Enter Scheme Code Here" runat="server" MaxLength="10" CssClass="form-control"
                                                                    ToolTip="Please Enter Scheme Code Here" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitSchemeType" TabIndex="3" OnClick="btnSubmitSchemeType_Click" OnClientClick="return SchemeTypeVal();"
                                                            CssClass="btn btn-primary" runat="server" Text="Submit" />
                                                        <asp:Button ID="btnCancelSchemeType" TabIndex="4" OnClick="btnCancelSchemeType_Click" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlSchemeType" runat="server" Visible="false">
                                                            <asp:ListView ID="lvSchemeType" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Scheme Type List</h5>
                                                                    </div>
                                                                    <%-- <div class="row mb-1">
                                                                        <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                        </div>

                                                                        <div class="col-lg-3 col-md-6">
                                                                            <div class="input-group sea-rch">
                                                                                <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-search"></i>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>--%>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%;" id="divlistType">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center;">Edit
                                                                                    </th>
                                                                                    <th>ID
                                                                                    </th>
                                                                                    <th style="text-transform: uppercase">
                                                                                        <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                                        TYPE NAME
                                                                                    </th>
                                                                                    <th>CODE
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
                                                                    <asp:UpdatePanel runat="server" ID="updDegreeType">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btnEditSchemeType" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" OnClick="btnEditSchemeType_Click"
                                                                                        CommandArgument='<%# Eval("SCHEMETYPENO")%>' AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="5" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SCHEMETYPENO")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SCHEMETYPE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive4" Text='<%# Eval("SCHEMETYPE_CODE")%>' ForeColor='<%# Eval("SCHEMETYPE_CODE").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:PostBackTrigger ControlID="btnEditSchemeType" />--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdprogupdSchemeCreation" runat="server" AssociatedUpdatePanelID="updSchemeCreation"
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
                                        <asp:UpdatePanel ID="updSchemeCreation" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree</label>--%>
                                                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegreeNo" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlDegreeNo_SelectedIndexChanged" CssClass="form-control" TabIndex="1"
                                                                    ToolTip="Please Select Degree">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegreeNo"
                                                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Department</label>--%>
                                                                    <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control" TabIndex="2"
                                                                    ToolTip="Please Select Department">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                                                                    Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Branch</label>--%>
                                                                    <asp:Label ID="lblDYtxtBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="3"
                                                                    ToolTip="Please Select Branch">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch"
                                                                    ControlToValidate="ddlBranch" Display="None" ValidationGroup="submit" InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Semester</label>--%>
                                                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" data-select2-enable="true"
                                                                    CssClass="form-control" ToolTip="Please Select Semester" TabIndex="4">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit" />--%>
                                                            </div>

                                                            <%--For Specialization Start--%>
                                                            <%-- <div class="form-group col-lg-3 col-md-6 col-12" id="divSpecialization" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                 
                                                                    <asp:Label ID="lblDYddlSpecialization" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSpecialization" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="3"
                                                                    ToolTip="Please Select Specialisation">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                          
                                                            </div>--%>
                                                            <%--  End Specialization--%>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Admission Batch</label>--%>
                                                                    <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBatchNo" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                                                    ToolTip="Please Select Admission Batch" CssClass="form-control" TabIndex="4">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBatchNo" runat="server" ErrorMessage="Please Select Batch"
                                                                    ControlToValidate="ddlBatchNo" Display="None" ValidationGroup="submit" InitialValue="0" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Scheme Type</label>--%>
                                                                    <asp:Label ID="lblDYrdoSchemeType" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSchemeType" runat="server" AppendDataBoundItems="true" AutoPostBack="false" data-select2-enable="true"
                                                                    ToolTip="Please Select Scheme Type" CssClass="form-control" TabIndex="5">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSchemeType" runat="server" ControlToValidate="ddlSchemeType"
                                                                    Display="None" ErrorMessage="Please Select Scheme Type" InitialValue="0"
                                                                    ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Grade/Marks</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlgrademarks" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" AutoPostBack="false" ToolTip="Please Select Pattern name">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="G">Grade</asp:ListItem>
                                                                    <asp:ListItem Value="M">Marks</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvgrademarks" runat="server" ControlToValidate="ddlgrademarks" Display="None" ErrorMessage="Please Select Grade/Marks" InitialValue="0" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Pattern Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPatternName" runat="server" AppendDataBoundItems="true" AutoPostBack="false" TabIndex="7"
                                                                    ToolTip="Please Select Pattern name" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPatternName" runat="server" ControlToValidate="ddlPatternName"
                                                                    Display="None" ErrorMessage="Please Select Pattern Name" InitialValue="0"
                                                                    ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                     <sup>* </sup>
                                                                    <label>Study Pattern</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlStudyPattern" runat="server" AppendDataBoundItems="true" TabIndex="7"
                                                                    ToolTip="Please Select Study name" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="1">Semester</asp:ListItem>
                                                                    <asp:ListItem Value="2">Trimester</asp:ListItem>
                                                                    <asp:ListItem Value="3">Yearly </asp:ListItem>
                                                                </asp:DropDownList>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudyPattern"
                                                                    Display="None" ErrorMessage="Please Select Study Pattern" InitialValue="0"
                                                                    ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Minimum No. of Credits</label>
                                                                </div>
                                                                <asp:TextBox ID="txtCredits" runat="server" TabIndex="7" ToolTip="Please Enter Minimum No. of Credits" MaxLength="3" placeholder="Enter Minimum No. of Credits"
                                                                    onkeyup="validateNumeric(this);"></asp:TextBox>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Abolish Attempts</label>
                                                                </div>
                                                                <asp:TextBox ID="txtAbolishAttmp" runat="server" TabIndex="7" ToolTip="Please Enter Abolish Attempts" placeholder="Enter Abolish Attempts"
                                                                    onkeyup="validateNumeric(this);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                                            OnClick="btnSubmit_Click" TabIndex="8" />
                                                        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="9" Visible="false"
                                                            Text="Report" CssClass="btn btn-info" Enabled="False" />
                                                        <asp:Button ID="btnExcelReport" runat="server" OnClick="btnExcelReport_Click" TabIndex="10" OnClientClick="
                                                             ExcelReport()" Text="Excel Report" CssClass="btn btn-info" Enabled="False" Visible="false" />
                                                        <asp:Button ID="btnCheckListReport" runat="server" OnClick="btnCheckListReport_Click" TabIndex="11"
                                                            Text="Check List Report" CssClass="btn btn-info" OnClientClick="return ReportVal();" Visible="false" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="11" />

                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                    </p>
                                                    <div class="col-12">
                                                        <asp:Label ID="lblStatus" runat="server" SkinID="Msglbl"></asp:Label>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvScheme" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>SCHEME LIST</h5>
                                                                    </div>
                                                                    <div class="row mb-1">
                                                                        <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                        </div>


                                                                        <div class="col-lg-3 col-md-6">
                                                                            <div class="input-group sea-rch">
                                                                                <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-search"></i>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>



                                                                    <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblSchemeList">
                                                                            <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th>Action
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                                        Name
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>Pattern Name
                                                                                    </th>
                                                                                    <th>Study Pattern
                                                                                    </th>
                                                                                    <th>Minimum No.of Credits
                                                                                    </th>
                                                                                    <th>Abolish Attempts
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
                                                                    <%--<asp:UpdatePanel ID="updschemelist" runat="server">
                                                                        <ContentTemplate>--%>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("SchemeNo") %>'
                                                                                ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                        </td>
                                                                        <td>

                                                                            <%--<asp:Label ID="lblscheme" runat="server" Text='<%#string.Concat(Eval("SCHEMENAME")," (",Eval("SCHEMETYPE"),")")%>'></asp:Label>--%>
                                                                            <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEMENAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbllname" runat="server" Text='<%# Eval("LONGNAME")%>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblbatch" runat="server" Text='<%# Eval("BATCH")%>'></asp:Label>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblpattname" runat="server" Text='<%# Eval("PATTERN_NAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblStudyPattern" runat="server" Text='<%# Eval("STUDY_PATTERN_NAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MIMIMUM_CREDITS_DEGREE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ABOLISH_ATTEMPTS")%>
                                                                        </td>
                                                                    </tr>
                                                                    <%--</ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="btnEdit" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>--%>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="updProgSchemeAllot" runat="server" AssociatedUpdatePanelID="updSchemeAllot"
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
                                        <asp:UpdatePanel ID="updSchemeAllot" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="trstype" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Scheme Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSType" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Institute Name</label>--%>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select School/Institute" ControlToValidate="ddlCollege" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%-- <label>Degree</label>--%>
                                                                    <asp:Label ID="lblDYlvDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="showstud" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="showstud"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Batch Year</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBatchYear" runat="server" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="True" CssClass="form-control" data-select2-enable="true"
                                                                    OnSelectedIndexChanged="ddlBatchYear_SelectedIndexChanged" ValidationGroup="showstud"
                                                                    Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Branch</label>--%>
                                                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBranch1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                    ValidationGroup="showstud" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBranch1" runat="server" ControlToValidate="ddlBranch1" SetFocusOnError="true"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Branch/Programme" ValidationGroup="showstud">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <%--Start Specialisation--%>
                                                            <%--   <div class="form-group col-lg-3 col-md-6 col-12" id="divSASpecilaization" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    
                                                                    <label>Specialisation</label>
                                                                    
                                                                </div>
                                                                <asp:DropDownList ID="ddlSASpecialisation" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSASpecialisation_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" TabIndex="3"
                                                                    ToolTip="Please Select Specialisation">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                              
                                                            </div>--%>
                                                            <%-- End Specialisation--%>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYlvAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    ValidationGroup="showstud" TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlAdmBatch" SetFocusOnError="true"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch" ValidationGroup="showstud">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYddlSemester_Tab2" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                                    ValidationGroup="showstud">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester1" SetFocusOnError="true"
                                                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="showstud">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnShowStudent" runat="server" Text="Show Student" ValidationGroup="showstud" TabIndex="1"
                                                            OnClick="btnShowStudent_Click" CssClass="btn btn-primary" />
                                                       
                                                        <asp:Button ID="btnPrintAllotment" runat="server" Text="Scheme Allotment/Pending Report" TabIndex="2"  OnClick="btnPrintAllotment_Click" CssClass="btn btn-primary" />
                                                         <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" TabIndex="3" CssClass="btn btn-warning" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="showstud"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                    <div class="col-12 btn-footer text-center">
                                                        <asp:Label ID="lblStatus1" runat="server" Font-Bold="true" ForeColor="Red" SkinID="lblmsg" />
                                                        <asp:Label ID="lblSch" runat="server" Font-Bold="true" ForeColor="Red" SkinID="lblmsg" Text="Select Scheme from following list and assign to Selected Students" Visible="false" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Total Selected Student</label>
                                                        </div>
                                                        <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" TabIndex="1" Enabled="false" />
                                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                      <div class="label-dynamic">
                                                        <sup>* </sup>
                                                           <label>Select Branch for Scheme</label>
                                                          <%-- <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>--%>
                                                       </div>
                                                       <asp:DropDownList ID="ddlBranchForScheme" runat="server" Enabled="False" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1"
                                                           ValidationGroup="showstud" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchForScheme_SelectedIndexChanged">
                                                           <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                       </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>Scheme</label>--%>
                                                            <asp:Label ID="lblDYddlScheme" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" Enabled="False" CssClass="form-control" TabIndex="1" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="assignsch">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                        </div>
                                                        </div>

                                                    <div class="col-lg-3 col-md-12 col-12 btn-footer mt-lg-3">
                                                        <asp:Button ID="btnAssignSch" runat="server" CssClass="btn btn-primary" OnClick="btnAssignSch_Click" TabIndex="1" OnClientClick="return validateAssign();" Text="Allot Scheme" ValidationGroup="assignsch" />
                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="assignsch" />
                                                    </div>
                                                    <div class="col-12 btn-footer text-center">
                                                        <asp:Label ID="lblStatus2" runat="server" Font-Bold="true" ForeColor="Red" SkinID="Errorlbl" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlSchemeAllot" runat="server">
                                                            <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound"
                                                                Visible="false">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblStudentList">
                                                                        <div class="sub-heading">
                                                                            <h5>Student List</h5>
                                                                        </div>
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:CheckBox ID="cbHead" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Name
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlSemester_Tab" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtAdmYear" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Cur.
                                                                            <asp:Label ID="lblDYtxtScheme" runat="server" Font-Bold="true"></asp:Label>
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
                                                                            <asp:CheckBox ID="cbRow" runat="server" TabIndex="1" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("ENROLLNO")%>
                                                                        </td>

                                                                        <td>
                                                                            <%# Eval("NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("BatchName")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SchemeName")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_4">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSchemeCreation"
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
                <asp:UpdatePanel ID="updPnl" runat="server">
                    <ContentTemplate>
                         <div id="div3" runat="server">
                                                </div>
                        <div class="box-body">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label> School</label>
                                   <%-- <asp:Label ID="lblDYddlSchoo" Font-Bold="true" runat="server"></asp:Label>--%>
                                </div>
                                <asp:DropDownList ID="ddlCollege1" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCollege1_SelectedIndexChanged"/>
                                <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege1" ValidationGroup="Submit1" Display="None"
                                    ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="true" />
                            </div>


                            <div class="form-group col-md-12" id="divDegree" runat="server" visible="false">
                                <%--<legend class="legendPay" style="font-size: 16px"><span style="color: red;">* </span><b>Degree</b></legend>--%>
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Degree</label>
                                </div>
                                <asp:CheckBoxList ID="cblDegree" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                   CssClass="cbl" CellPadding="5" RepeatLayout="Table" AutoPostBack="true" OnSelectedIndexChanged="cblDegree_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>

                            <div class="form-group col-md-12" id="divBranch" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Branch</label>
                                </div>
                                <asp:CheckBoxList ID="cblBranch" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                   CssClass="cbl" OnSelectedIndexChanged="cblBranch_SelectedIndexChanged" CellPadding="5" RepeatLayout="Table" RepeatColumns="4"
                                    AutoPostBack="true">
                                </asp:CheckBoxList>
                            </div>

                            <div class="form-group col-md-12" id="divScheme" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Regulation/Scheme</label>
                                </div>
                                <asp:CheckBoxList ID="cblScheme" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal" Style="margin-right: 5px"
                                    CssClass="cbl" CellPadding="5" OnSelectedIndexChanged="cblScheme_SelectedIndexChanged" RepeatLayout="Table" RepeatColumns="3"
                                    AutoPostBack="true">
                                </asp:CheckBoxList>
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit1" runat="server" Text="Submit" ValidationGroup="Submit1" Enabled="false" OnClick="btnSubmit1_Click" CssClass="btn btn-primary" OnClientClick="return UserConfirmation();" />
                                <asp:Button ID="btnCancel1" runat="server" Text="Clear"  CssClass="btn btn-warning" OnClick="btnCancel1_Click" />

                                <asp:ValidationSummary ID="vsReport" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit1" />
                                <div id="div2" runat="server"></div>
                                <p>
                                </p>
                            </p>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcelReport" />
            <asp:PostBackTrigger ControlID="btnPrintAllotment" />
        </Triggers>
    </asp:UpdatePanel>

        <script type="text/javascript" language="javascript">
            function UserConfirmation() {
                if (confirm("Are you sure you want to Submit?"))
                    return true;
                else
                    return false;
            }
    </script>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {

            var txtTo = document.getElementById('<%=ddlScheme.FindControl( "ddlScheme").ClientID%>');
            if (txtTo.value == 0) {
                alert('Please Select Scheme/Path.', 'Warning!');
                $(txtTo).focus();
                return false;
            }


            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

            if (txtTot == 0 || document.getElementById('<%= ddlScheme.ClientID %>').selectedIndex == 0) {
                alert('Please Select Atleast One Scheme/Student from Scheme/Student List');
                return false;
            }
            else
                return true;
        }

    </script>


    <script>
        function SchemeTypeVal() {

            var txtSch1 = document.getElementById('<%=txtScheme.FindControl( "txtScheme").ClientID%>');
            if (txtSch1.value.length == 0) {
                alert('Please Enter Scheme Type Name', 'Warning!');
                $(txtSch1).focus();
                return false;
            }

            var txtcod = document.getElementById('<%=txtCode.FindControl( "txtCode").ClientID%>');
            if (txtcod.value.length == 0) {
                alert('Please Enter Code', 'Warning!');
                $(txtcod).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitSchemeType').click(function () {
                    SchemeTypeVal();
                });
            });
        });

    </script>

    <script>
        function ReportVal() {

            var ddlDegreeNo = $("[id$=ddlDegreeNo]").attr("id");
            var ddlDegreeNo = document.getElementById(ddlDegreeNo);
            if (ddlDegreeNo.value == 0) {
                alert('Please Select Degree', 'Warning!');
                $(ddlDegreeNo).focus();
                return false;
            }

            var ddlDept = $("[id$=ddlDept]").attr("id");
            var ddlDept = document.getElementById(ddlDept);
            if (ddlDept.value == 0) {
                alert('Please Select Department', 'Warning!');
                $(ddlDept).focus();
                return false;
            }

            var ddlBranch = $("[id$=ddlBranch]").attr("id");
            var ddlBranch = document.getElementById(ddlBranch);
            if (ddlBranch.value == 0) {
                alert('Please Select Branch', 'Warning!');
                $(ddlBranch).focus();
                return false;
            }

            var ddlSchemeType = $("[id$=ddlSchemeType]").attr("id");
            var ddlSchemeType = document.getElementById(ddlSchemeType);
            if (ddlSchemeType.value == 0) {
                alert('Please Select Scheme Type', 'Warning!');
                $(ddlSchemeType).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnCheckListReport').click(function () {
                    ReportVal();
                });
            });
        });

    </script>



    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#tblSchemeList');

            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "SchemeType.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>

    <script>
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>
  <script type="text/javascript">
      function ValidateCheckBoxListdegree(sender, args) {
          var checkBoxList = document.getElementById("<%=cblDegree.ClientID %>");
        var checkboxes = checkBoxList.getElementsByTagName("input");
        var isValid = false;
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked) {
                isValid = true;
                break;
            }
        }
        args.IsValid = isValid;
      }
      function ValidateCheckBoxListBranch(sender, args) {
          var checkBoxList = document.getElementById("<%=cblBranch.ClientID %>");
              var checkboxes = checkBoxList.getElementsByTagName("input");
              var isValid = false;
              for (var i = 0; i < checkboxes.length; i++) {
                  if (checkboxes[i].checked) {
                      isValid = true;
                      break;
                  }
              }
              args.IsValid = isValid;
      }
      function ValidateCheckBoxListScheme(sender, args) {
          var checkBoxList = document.getElementById("<%=cblScheme.ClientID %>");
               var checkboxes = checkBoxList.getElementsByTagName("input");
               var isValid = false;
               for (var i = 0; i < checkboxes.length; i++) {
                   if (checkboxes[i].checked) {
                       isValid = true;
                       break;
                   }
               }
               args.IsValid = isValid;
           }
      
</script>

</asp:Content>




