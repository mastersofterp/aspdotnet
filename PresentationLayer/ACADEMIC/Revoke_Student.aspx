<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Revoke_Student.aspx.cs" Inherits="ACADEMIC_Revoke_Student"
    ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAddressDetails"
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
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updEdcutationalDetails"
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
        #ctl00_ContentPlaceHolder1_pnlCourse .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }

        .Background
        {
            background-color: black;
            opacity: 0.9;
        }

        .modalPopup
        {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
        }

        .dataTables_scrollHeadInner
        {
            width: max-content !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblstudDetails').DataTable({
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
                                return $('#tblstudDetails').DataTable().column(idx).visible();
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
                                            return $('#tblstudDetails').DataTable().column(idx).visible();
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
                                            return $('#tblstudDetails').DataTable().column(idx).visible();
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
                var table = $('#tblstudDetails').DataTable({
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
                                    return $('#tblstudDetails').DataTable().column(idx).visible();
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
                                                return $('#tblstudDetails').DataTable().column(idx).visible();
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
                                                return $('#tblstudDetails').DataTable().column(idx).visible();
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


    <asp:UpdatePanel ID="updCollege1" runat="server"></asp:UpdatePanel>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Revoke Students</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Admission Batch</label>
                                </div>
                                <asp:DropDownList ID="ddlAdmbatch" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAdmBatch"
                                    Display="None" ValidationGroup="stud" InitialValue="0"
                                    ErrorMessage="Please Select Admission Batch" Visible="true"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlAdmBatch"
                                    Display="None" ValidationGroup="summary" InitialValue="0"
                                    ErrorMessage="Please Select Admission Batch." Visible="true"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">

                                <div class="label-dynamic">
                                    <%-- <sup>* </sup>--%>
                                    <label>College</label>
                                </div>
                                <asp:DropDownList ID="ddlProgrammeType" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlProgrammeType_SelectedIndexChanged1" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Degree</label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="form-group col-lg-4 col-md-4 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Branch</label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <%--<div class="col-12 btn-footer" style="display: none">
                        <asp:Button ID="btnShowStudent" runat="server" TabIndex="12" Text="Show Student" CssClass="btn btn-primary" ToolTip="Show Student" OnClick="btnShowStudent_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnExcel" runat="server" TabIndex="12" Text="Export In Excel" CssClass="btn btn-primary" ToolTip="Export In Excel" OnClick="btnExcel_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnExcel2" runat="server" TabIndex="13" Text="Export Student list(SVET)" CssClass="btn btn-primary" ToolTip="Export Student list" OnClick="btnExcel2_Click" ValidationGroup="stud" Visible="false" />
                        <asp:Button ID="btnExcel3" runat="server" TabIndex="14" Text="Fee Paid Student Details" CssClass="btn btn-primary" ToolTip="Export Student list Payment Details" OnClick="btnExcel3_Click" ValidationGroup="stud" />

                        <asp:ValidationSummary ID="vsStud" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="stud" />

                        <asp:Button ID="btnPendingAmt" runat="server" TabIndex="15" Text="Applicant Fill Up Status" CssClass="btn btn-primary" ToolTip="Pending Registration Fee Student List" OnClick="btnPendingAmt_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnBranchPref" runat="server" TabIndex="15" Text="Branch Pref and Entrance Details" CssClass="btn btn-primary" ToolTip="Applicant Branch Preference with Entrance Details" OnClick="btnBranchPref_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnShowReport" runat="server" TabIndex="16" Text="Show Report" CssClass="btn btn-info" ToolTip="Show Report" OnClick="btnShowReport_Click" ValidationGroup="stud" />
                    </div>--%>
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnShowStud" runat="server" TabIndex="4" Text="Show Student List" CssClass="btn btn-primary " ToolTip="Show Student" OnClick="btnShowStud_Click" ValidationGroup="stud" />
                        <asp:Button ID="btnRevoke" runat="server" TabIndex="5" Text="Revoke Student" CssClass="btn btn-danger" Enabled="false" ValidationGroup="studData" OnClick="btnRevoke_Click" />
                        <asp:Button ID="btnCancel" runat="server" TabIndex="5" Text="Cancel" CssClass="btn btn-warning"  OnClick="btnCancel_Click1" />

                        <asp:ValidationSummary ID="vsStud" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="stud" DisplayMode="List" />
                       
                    </div>
                    <div class="col-12">
                        <asp:Panel ID="updCollege" runat="server">
                            <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Students List</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblstudDetails">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" onclick="totAllSubjects(this)" />Select
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>College
                                                </th>
                                                <th>Degree
                                                </th>
                                                <th>Branch
                                                </th>
                                                <th>Mobile No
                                                </th>
                                                <th>Email Id
                                                </th>
                                                <th>Application ID
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <%--<%# Container.DataItemIndex + 1%>--%>
                                            <asp:CheckBox ID="chkSelect" TabIndex="1" runat="server" ToolTip='<%# Eval("IDNO") %>' />
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("IDNO")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                             <%# Eval("STUDNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("COLLEGE_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCHNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("STUDENTMOBILE")%>
                                        </td>
                                        <td>
                                            <%# Eval("EMAILID")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAppStatus" runat="server" Text='<%# Eval("APPLICATIONID")%>'></asp:Label>

                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:LinkButton ID="lnkPopup" runat="server"></asp:LinkButton>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <%--  </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="btnExcel" />
            <asp:AsyncPostBackTrigger ControlID="lvStudent" />
            <asp:AsyncPostBackTrigger ControlID="btnShowStudent" />
            <asp:PostBackTrigger ControlID="btnBranchPref" />
            <asp:AsyncPostBackTrigger ControlID="btnShowReport" />
            <asp:PostBackTrigger ControlID="btnExcel2" />
            <asp:PostBackTrigger ControlID="btnExcel3" />
            <asp:PostBackTrigger ControlID="btnPendingAmt" />
        </Triggers>--%>

    <%--</asp:UpdatePanel>--%>
    <div id="divMsg" runat="server">
    </div>

    <asp:Panel ID="pnlPopup" runat="server">
        <asp:UpdatePanel ID="updMaindiv" runat="server">
            <ContentTemplate>
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Applicant Details</h4>
                            <button type="button" id="btnCloseX" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body" style="overflow: scroll; height: 450px;">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Applied Programme</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Admission Type</label>
                                        <asp:DropDownList ID="ddlAdmissionType" runat="server" AppendDataBoundItems="true" CssClass="form-control" ToolTip="Please Select Admission Type." data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Regular</asp:ListItem>
                                            <asp:ListItem Value="2">Lateral</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Programme Type</label>

                                        <asp:DropDownList ID="ddlpopProgrammeType" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlProgrammeType_SelectedIndexChanged" ToolTip="Please Select Programme Type.">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Degree</label>
                                        <asp:DropDownList ID="ddlpopDegree" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlpopDegree_SelectedIndexChanged" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlpopDegree"
                                            ErrorMessage="Please Select Degree." Display="None" SetFocusOnError="true" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>

                                        <asp:Label ID="lblTooltipMsg" runat="server" Visible="false" Style="background-color: lightgray; font-family: Arial; font-style: italic; font-size: medium; color: darkgreen"></asp:Label>

                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divDegree" runat="server" style="display: none">
                                        <label><sup>*</sup>Programme Code </label>
                                        <asp:DropDownList ID="ddlCodePref" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="2" ToolTip="Please Select Programme Code."
                                            data-select2-enable="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlCodePref"
                                            ErrorMessage="Please Select Programme Code." Display="None" SetFocusOnError="true" ValidationGroup="Submit" InitialValue="0" Visible="false"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                        <label><sup>*</sup>Programme/Branch </label>
                                        <asp:DropDownList ID="ddlpopBranch" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            CssClass="form-control" ToolTip="Please Select Programme/Branch." AutoPostBack="true" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpopBranch"
                                            ErrorMessage="Please Select Programme/Branch." Display="None" SetFocusOnError="true"
                                            ValidationGroup="Submit" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label>Total Applied Programme </label>
                                        <asp:TextBox runat="server" ID="txtTotBranches" CssClass="form-control" Text="" ToolTip="Total Applied Programme"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Total Application / Form Fee </label>
                                        <asp:TextBox runat="server" ID="txtTotFees" CssClass="form-control" Text="" ToolTip="Form Fee"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Candidate’s Details</h5>
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Candidate's Full Name</label>
                                        </div>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" ToolTip="Please Enter Candidate's Full Name" placeholder="Enter Name "></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>DOB</label>
                                        </div>
                                        <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="4" AutoComplete="off" CssClass="form-control" ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        (As per 10th Certificate)
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Email</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Email" AutoComplete="off" />
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email"
                                            ControlToValidate="txtEmail" Display="None" SetFocusOnError="true" ValidationGroup="Submit" />
                                        <br />
                                        <asp:RegularExpressionValidator ID="revEmailId1" runat="server" ControlToValidate="txtEmail"
                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ErrorMessage="Please Enter Valid Email ID" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label>Alternate Email ID</label>
                                        <asp:TextBox ID="txtAlternateEmailId" runat="server" ToolTip="Please Enter Alternate Email ID"
                                            TabIndex="6" CssClass="form-control" Placeholder="Please Enter Email ID" AutoComplete="off" />
                                        <br />
                                        <asp:RegularExpressionValidator ID="revPermEmailId" runat="server" ControlToValidate="txtAlternateEmailId"
                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ErrorMessage="Please Enter Valid Email ID" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Gender </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoGender" runat="server" TextAlign="Right" RepeatDirection="Horizontal" ValidationGroup="Submit" TabIndex="7" ToolTip="Please Select Gender.">
                                            <asp:ListItem Value="1">Male</asp:ListItem>
                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                            <asp:ListItem Value="3">Transgender</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rdoGender" ErrorMessage="Please Select Gender." Display="None" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Mobile</label><br />
                                        <div class="input-group">
                                            <div class="input-group-addon" style="width: 25%; padding: 0px 3px; border-bottom: 0px">
                                                <i>
                                                    <asp:TextBox ID="txtmobilecode" runat="server" Text="+91" CssClass="form-control"
                                                        MaxLength="3" AutoComplete="off" />
                                                </i>
                                            </div>
                                            <asp:TextBox ID="txtMob" runat="server" CssClass="form-control" ToolTip="Please Enter Mobile Number." placeholder="Enter Mobile Number" AutoComplete="off" MaxLength="10" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Religion </label>
                                        </div>
                                        <asp:DropDownList ID="ddlReligion" runat="server" TabIndex="8" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Religion." AppendDataBoundItems="true" OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReligion" runat="server" ValidationGroup="Submit" ControlToValidate="ddlReligion" Display="None"
                                            ErrorMessage="Please Select Religion." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divPopReligion" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Others , Please Specify </label>

                                        </div>
                                        <asp:TextBox ID="txtOtherReligion" runat="server" CssClass="form-control" ValidationGroup="Submit" TabIndex="9" ToolTip="Please Enter Other Religion." MaxLength="32" placeholder="Enter Other Religion"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvPopotherreligion" runat="server" ControlToValidate="txtOtherReligion" Display="None"
                                            ErrorMessage="Please Enter Other Religion." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server" TargetControlID="txtOtherReligion" FilterMode="InvalidChars"
                                            InvalidChars="0123456789~`!@#$%^&*()_-+={[}]|\:;'<,>?">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Category </label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="9" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Category." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ValidationGroup="Submit" ControlToValidate="ddlCategory" Display="None"
                                            ErrorMessage="Please Select Category." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Nationality </label>
                                        </div>
                                        <asp:DropDownList ID="ddlNationality" runat="server" TabIndex="10" CssClass="form-control" ValidationGroup="Submit" ToolTip="Please Select Nationality." AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvNational" runat="server" ValidationGroup="Submit" ControlToValidate="ddlNationality" Display="None"
                                            ErrorMessage="Please Select Nationality." InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label>Blood Group</label>
                                        <asp:DropDownList runat="server" ID="ddlBloodGroup" AppendDataBoundItems="true"
                                            TabIndex="11" ToolTip="Please Select Blood Group" CssClass="form-control" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label>Identification Mark</label>
                                        <asp:TextBox ID="txtIdentificationMark" runat="server" TabIndex="12" AutoComplete="off"
                                            CssClass="form-control" ToolTip="Please Enter Identification Mark" placeholder="Please Enter Identification Mark" MaxLength="50" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtIdentificationMark"
                                            InvalidChars="1234567890~`!@#$%^&*_-+={[}]|\:;'<,>.?" FilterMode="InvalidChars" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Aadhaar Card Number </label>
                                        </div>
                                        <asp:TextBox ID="txtAdhaarNo" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter Aadhar card Number." MaxLength="12" placeholder="Enter Aadhar card Number" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAdhar" runat="server" ValidationGroup="Submit" ControlToValidate="txtAdhaarNo" Display="None"
                                            ErrorMessage="Please Enter Aadhaar Card Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajaxToolkit1" runat="server" TargetControlID="txtAdhaarNo" FilterMode="ValidChars"
                                            ValidChars="0123456789">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtAdhaarNo"
                                            Display="None" SetFocusOnError="True" ValidationExpression="^[100000000000-999999999999]{12}$"
                                            ErrorMessage="Please Enter Valid Aadhaar Card Number" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Marital Status</label>
                                        <asp:DropDownList runat="server" ID="ddlMarital" AppendDataBoundItems="true"
                                            TabIndex="14" ToolTip="Please Select Marital Status" CssClass="form-control" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Married</asp:ListItem>
                                            <asp:ListItem Value="2">Unmarried</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfMarital" runat="server" ErrorMessage="Please Select Marital Status"
                                            ControlToValidate="ddlMarital" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Are You Differently Abled?</label>
                                        <asp:DropDownList runat="server" ID="ddlDiffAbility" AppendDataBoundItems="true" AutoPostBack="true"
                                            TabIndex="15" ToolTip="Please Select Are You Differently Abled" CssClass="form-control" OnSelectedIndexChanged="ddlDiffAbility_SelectedIndexChanged" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfAbility" runat="server" ErrorMessage="Please Select Are You Differently Abled"
                                            ControlToValidate="ddlDiffAbility" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="Abilility" runat="server">
                                        <label><sup>*</sup>Nature of Disability</label>
                                        <asp:TextBox ID="txtNatureOfDisability" runat="server" TabIndex="16" CssClass="form-control"
                                            ToolTip="Please Nature of Disability" AutoComplete="off" />
                                        <asp:RequiredFieldValidator ID="rvfNatureOfDisability" runat="server" ErrorMessage="Please Nature of Disability"
                                            ControlToValidate="txtNatureOfDisability" Display="None" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajaxDisable" runat="server" TargetControlID="txtNatureOfDisability" FilterMode="InvalidChars"
                                            InvalidChars="`~!@#$%^&*-_+={[}]|\:;'<,>.?1234567890">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="Abilility1" runat="server">
                                        <label><sup>*</sup>% of Disability (Certificate Required from Competent Authority)</label>
                                        <asp:TextBox ID="txtPercentageOfDisability" runat="server" TabIndex="16" CssClass="form-control"
                                            ToolTip="Please Enter Percentage of Disability" MaxLength="3" AutoComplete="off" OnTextChanged="txtPercentageOfDisability_TextChanged" AutoPostBack="true" />
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server" TargetControlID="txtPercentageOfDisability"
                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                        <asp:RequiredFieldValidator ID="rvfPercentageOfDisability" runat="server" ErrorMessage="Please Enter Percentage of Disability"
                                            ControlToValidate="txtPercentageOfDisability" Display="None" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>State of Domicile</label>
                                        <asp:DropDownList runat="server" ID="ddlStateDomicile" AppendDataBoundItems="true"
                                            TabIndex="16" ToolTip="Please Select State of Domicile" CssClass="form-control" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtNRIstate" runat="server" Visible="false" CssClass="form-control" PlaceHolder="Please Enter State" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rvflDomicile" runat="server" ErrorMessage="Please Select State of Domicile"
                                            ControlToValidate="ddlStateDomicile" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter State of Domicile"
                                            ControlToValidate="txtNRIstate" Display="None" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup>*</sup>Sports Person ?</label>
                                        <asp:DropDownList runat="server" ID="ddlSports" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSports_SelectedIndexChanged"
                                            TabIndex="17" ToolTip="Please Select Sports Person" CssClass="form-control" AutoPostBack="true" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfSports" runat="server" ErrorMessage="Please Select Sports Person"
                                            ControlToValidate="ddlSports" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="LevelOfSports" runat="server" visible="false">
                                        <label><sup>*</sup>Level of Sports Represented</label>
                                        <asp:DropDownList runat="server" ID="ddlLevelOfSports" AppendDataBoundItems="true"
                                            TabIndex="18" ToolTip="Please Select Level of Sports Represented" CssClass="form-control" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1"> State Level</asp:ListItem>
                                            <asp:ListItem Value="2"> National Level</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rvfLevelOfSports" runat="server" ErrorMessage="Please Select Level of Sports Represented"
                                            ControlToValidate="ddlLevelOfSports" Display="None" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="sportsName" runat="server" visible="false">
                                        <label><sup>*</sup>Sports Name</label>
                                        <asp:TextBox ID="txtSportsName" runat="server" ToolTip="Please Enter Sports Name" MaxLength="64" CssClass="form-control" AutoComplete="off" TabIndex="18"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSportsName" runat="server" ControlToValidate="txtSportsName" ErrorMessage="Please Enter Sports Name"
                                            Display="None" SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbe1" runat="server" InvalidChars="~`!@#$%^&*()_-+=|\}]{[:;'<,>.?/0123456789" FilterMode="InvalidChars" TargetControlID="txtSportsName"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12" id="sportsDoc" runat="server" visible="false">
                                        <label><sup>*</sup>Document Upload <span style="color: red">(Upload file with .pdf format only and maximum size of each document upto 500 KB.)</span></label>
                                        <asp:FileUpload ID="sportUpload" runat="server" TabIndex="18" ToolTip="Please Upload Document" />
                                        <asp:Label ID="lblFileName" runat="server" Visible="false"></asp:Label>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDoc" runat="server" ControlToValidate="sportUpload" Display="None"
                                            ErrorMessage="Please Select Document to upload" ValidationGroup="Submit" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <label><sup></sup>Hosteller/Transport</label>
                                        <asp:DropDownList ID="ddlHost" runat="server" ToolTip="Please Select Hosteller/Transport." TabIndex="19" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Hosteller</asp:ListItem>
                                            <asp:ListItem Value="2">Transport</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>



                                </div>
                            </div>
                            <asp:UpdatePanel ID="updPraentsPar" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Particulars of Parents </h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father's Full Name</label>
                                                </div>
                                                <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control" TabIndex="20" ToolTip="Please Enter Father Name." placeholder="Enter Father Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFather" runat="server" ValidationGroup="Submit" ControlToValidate="txtFatherName" Display="None"
                                                    ErrorMessage="Please Enter Father Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFatherName" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Tel. Number (with STD code)</label>
                                                <asp:TextBox ID="txtFTelNo" runat="server" TabIndex="21" ToolTip="Please Enter Tel. Number" AutoComplete="off"
                                                    MaxLength="12" CssClass="form-control" Placeholder="Please Enter Tel. Number" onkeyup="validateNumeric(this)" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server" TargetControlID="txtFTelNo"
                                                    ValidChars="1234567890" FilterMode="ValidChars" />
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Tel. Number is Invalid." ID="RegularExpressionValidator1" ControlToValidate="txtFTelNo" ValidationExpression=".{10}.*"
                                                    Display="None" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Father's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtFMobile" runat="server" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Father's Mobile Number." placeholder="Enter Father's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFmob" runat="server" ValidationGroup="Submit" ControlToValidate="txtFMobile" Display="None"
                                                    ErrorMessage="Please Enter Father's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtFMobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="revFmob" runat="server" ControlToValidate="txtFMobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Father's Mobile Number." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Occupation of Father </label>
                                                </div>
                                                <asp:DropDownList ID="ddlFOccupation" runat="server" TabIndex="23" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit" ToolTip="Please Select Occupation of Father." AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divFoccupation" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtOccFather" runat="server" CssClass="form-control" TabIndex="24" ToolTip="Please Enter Other Occupation." placeholder="Enter Father Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFathOcc" runat="server" ControlToValidate="txtOccFather" Display="None"
                                                    ErrorMessage="Please Enter Father's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender41" runat="server" TargetControlID="txtPopFoccOther" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Designation</label>
                                                <asp:TextBox ID="txtFDesignation" runat="server" TabIndex="24" ToolTip="Please Enter Designation"
                                                    MaxLength="50" CssClass="form-control" Placeholder="Please Enter Designation" AutoComplete="off" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server" TargetControlID="txtFDesignation"
                                                    InvalidChars="0123456789~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Email</label>
                                                <asp:TextBox ID="txtFEmail" runat="server" TabIndex="25" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter Valid Email ID" />
                                                <br />
                                                <asp:RegularExpressionValidator ID="revEmail2" runat="server" ControlToValidate="txtFEmail"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Email ID" ValidationGroup="Submit"></asp:RegularExpressionValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server" TargetControlID="txtFEmail"
                                                    InvalidChars="~`!#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother's Full Name</label>
                                                </div>
                                                <asp:TextBox ID="txtMothersName" runat="server" CssClass="form-control" TabIndex="26" ToolTip="Please Enter Mother Name." placeholder="Enter Mother Name" MaxLength="150" Style="text-transform: uppercase" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMother" runat="server" ValidationGroup="Submit" ControlToValidate="txtMothersName" Display="None"
                                                    ErrorMessage="Please Enter Mother Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMothersName" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Tel. Number (with STD code)</label>
                                                <asp:TextBox ID="txtMTelNo" runat="server" TabIndex="27" ToolTip="Please Enter Tel. Number" AutoComplete="off"
                                                    MaxLength="12" CssClass="form-control" Placeholder="Please Enter Tel. Number" onkeyup="validateNumeric(this)" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server" TargetControlID="txtMTelNo"
                                                    ValidChars="1234567890" FilterMode="ValidChars" />
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Tel. Number is Invalid." ID="RegularExpressionValidator3" ControlToValidate="txtMTelNo" ValidationExpression=".{10}.*"
                                                    Display="None" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Mother's Mobile Number </label>
                                                </div>
                                                <asp:TextBox ID="txtMMobile" runat="server" CssClass="form-control" TabIndex="28" ToolTip="Please Enter Mother's Mobile Number." placeholder="Enter Mother's Mobile Number" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMMobile" runat="server" ValidationGroup="Submit" ControlToValidate="txtMMobile" Display="None"
                                                    ErrorMessage="Please Enter Mother's Mobile Number." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMMobile" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMMobile"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{10}$"
                                                    ErrorMessage="Please Enter Valid Mother's Mobile Number." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <label>Occupation of Mother </label>
                                                </div>
                                                <asp:DropDownList ID="ddlMOccupation" runat="server" TabIndex="29" CssClass="form-control" data-select2-enable="false" ValidationGroup="Submit" ToolTip="Please Select Occupation of Mother." AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divMoccupation" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Others , Please Specify</label>

                                                </div>
                                                <asp:TextBox ID="txtOccMother" runat="server" CssClass="form-control" TabIndex="30" ToolTip="Please Enter Other Occupation." placeholder="Enter Mother Other Occupation" MaxLength="150"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMOcc" runat="server" ControlToValidate="txtOccMother" Display="None"
                                                    ErrorMessage="Please Enter Mother's Other Occupation." SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender42" runat="server" TargetControlID="txtOccMother" FilterMode="InvalidChars"
                                                    InvalidChars="0123456789~`!@#$%^&*()_-+{[}]:;'<,>.?/|\">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Designation</label>
                                                <asp:TextBox ID="txtMDesignation" runat="server" TabIndex="30" ToolTip="Please Enter Designation"
                                                    MaxLength="50" CssClass="form-control" Placeholder="Please Enter Designation" AutoComplete="off" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender40" runat="server" TargetControlID="txtMDesignation"
                                                    InvalidChars="0123456789~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label>Email</label>
                                                <asp:TextBox ID="txtMEmail" runat="server" TabIndex="31" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter Valid Email ID" />
                                                <br />
                                                <asp:RegularExpressionValidator ID="revEmailId3" runat="server" ControlToValidate="txtMEmail"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Email ID" ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender43" runat="server" TargetControlID="txtMEmail"
                                                    InvalidChars="~`!#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Annual income of parents (Includes both parents)</label>
                                                </div>
                                                <asp:DropDownList ID="ddlParentsIncome" runat="server" CssClass="form-control" ToolTip="Please Select Annual Income of Parents." AppendDataBoundItems="true" data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">BPL</asp:ListItem>
                                                    <asp:ListItem Value="2">Less than 2 Lac</asp:ListItem>
                                                    <asp:ListItem Value="3">2 Lac-4 Lac</asp:ListItem>
                                                    <asp:ListItem Value="4">4 Lac-6 Lac</asp:ListItem>
                                                    <asp:ListItem Value="5">Above 6 Lac</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:UpdatePanel ID="updAddressDetails" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Address Details </h5>
                                                </div>
                                                <div class="sub-heading">
                                                    <h5>Correspondence Address</h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>Address</label>

                                                <asp:TextBox ID="txtCorresAddress" TextMode="MultiLine" runat="server"
                                                    MaxLength="100" ToolTip="Please Enter Local Address" TabIndex="32" CssClass="form-control" placeholder="Please Enter Local Address" AutoComplete="off" />
                                                <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtCorresAddress"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Local Address.">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender44" runat="server" InvalidChars="<>?|\~`!@#$%^&*_+={[}]:;&quot;'"
                                                    TargetControlID="txtCorresAddress" FilterMode="InvalidChars" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>Country </label>

                                                <asp:DropDownList ID="ddlLCon" runat="server" TabIndex="33" CssClass="form-control" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlLCon_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select Country.">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlLCon" runat="server" ControlToValidate="ddlLCon"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Select country." InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtLcountry" Visible="false" PlaceHolder="Please Enter Country" CssClass="form-control" Width="55%" ToolTip="Please Enter Country" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please Enter Country"
                                                    ControlToValidate="txtLcountry" Display="None" SetFocusOnError="true" ValidationGroup="Address" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender45" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                    TargetControlID="txtLcountry" />

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>State </label>
                                                <asp:DropDownList ID="ddlLSta" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="35" OnSelectedIndexChanged="ddlLSta_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select State.">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlLSta" runat="server" ControlToValidate="ddlLSta"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Select state." InitialValue="0">
                                                </asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtLState" Visible="false" PlaceHolder="Please Enter State" CssClass="form-control" Width="55%" ToolTip="Please Enter State" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter State"
                                                    ControlToValidate="txtLState" Display="None" SetFocusOnError="true" ValidationGroup="Address" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender46" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                    TargetControlID="txtLState" />

                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>City</label>
                                                <div class="d-flex">
                                                    <asp:DropDownList ID="ddlCorrCity" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="36" OnSelectedIndexChanged="ddlCorrCity_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select City.">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlCorrCity" runat="server" ControlToValidate="ddlCorrCity"
                                                        ValidationGroup="Address" Display="none" SetFocusOnError="true" ErrorMessage="Please Select city." InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                    <%-- <asp:RegularExpressionValidator ID="ReqValContactPerson_SpecialChars" runat="server" CssClass="changecolor"
                                                            ControlToValidate="ddlCorrCity" Display="Dynamic"
                                                            ErrorMessage="Only numbers are allowed" SetFocusOnError="True"
                                                            ValidationExpression="[\%\/\\\&\?\,\'\;\:\!\-]+123456789"></asp:RegularExpressionValidator>--%>

                                                    <asp:TextBox runat="server" ID="txtLcity" Visible="false" PlaceHolder="Please Enter City" CssClass="form-control" AutoComplete="off" ToolTip="Please Enter City."></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtOtherCity" Visible="false" PlaceHolder="Please Enter Other City" CssClass="form-control"
                                                        ToolTip="Please Enter Other City" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter City"
                                                        ControlToValidate="txtLcity" Display="None" SetFocusOnError="true" ValidationGroup="Address" />
                                                    <asp:RequiredFieldValidator ID="rfvothercity" runat="server" ErrorMessage="Please Enter City"
                                                        ControlToValidate="txtOtherCity" Display="None" SetFocusOnError="true" ValidationGroup="Address" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender47" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                        TargetControlID="txtLcity" />
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>PIN Code/ZIP</label>
                                                </div>
                                                <asp:TextBox ID="txtLocalPIN" runat="server" CssClass="form-control" TabIndex="37" ToolTip="Please Enter Pincode." MaxLength="6" placeholder="Enter Pincode" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPincode" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Pincode." Display="None" ControlToValidate="txtLocalPIN" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtLocalPIN" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtLocalPIN"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{6}$"
                                                    ErrorMessage="Please Enter Valid Pincode." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Permanent Address</h5>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>Copy Correspondence Address</label>
                                                <asp:HiddenField runat="server" ID="hdncopy" />
                                                <asp:ImageButton ID="imgbToCopyLocalAddress" runat="server" ImageUrl="~/images/copy.png"
                                                    OnClientClick="copyLocalAddr(this)" TabIndex="38" ToolTip="Copy Local Address."
                                                    OnClick="imgbToCopyLocalAddress_Click" Height="25px" />
                                                <p>&nbsp;&nbsp; (If same as permanent address) </p>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>Address</label>

                                                <asp:TextBox ID="txtPermAddress" runat="server" TextMode="MultiLine"
                                                    MaxLength="100" ToolTip="Please Enter Permenant Address" TabIndex="39" CssClass="form-control" placeholder="Please Enter Permenant Address" AutoComplete="off" />
                                                <asp:TextBox ID="txtPdistrict" runat="server" Visible="False" ToolTip="Please Enter District" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPermAddress"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Permanent Address.">
                                                </asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>Country </label>
                                                <asp:DropDownList ID="ddlPCon" runat="server" CssClass="form-control" TabIndex="40" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlPCon_SelectedIndexChanged" ToolTip="Please Select Country.">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="txtCCountry" Visible="false" PlaceHolder="Please Enter Country" CssClass="form-control" Width="250px" ToolTip="Please Enter Country" AutoComplete="off"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                    TargetControlID="txtCCountry" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPCon" InitialValue="0"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Permanent Country.">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>State </label>
                                                <asp:DropDownList ID="ddlPermanentState" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="41" OnSelectedIndexChanged="ddlPermanentState_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select State.">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="txtCState" Visible="false" PlaceHolder="Please Enter State" CssClass="form-control" Width="55%"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                    TargetControlID="txtCState" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlPermanentState" InitialValue="0"
                                                    ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Permanent State.">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <label><sup>*</sup>City</label>
                                                <div class="d-flex">
                                                    <asp:DropDownList ID="ddlPermanentCity" runat="server" CssClass="form-control" AppendDataBoundItems="True" TabIndex="42" OnSelectedIndexChanged="ddlPermanentCity_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select City.">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox runat="server" ID="txtCCity" Visible="false" PlaceHolder="Please Enter City" CssClass="form-control" ToolTip="Please Enter City" AutoComplete="off"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="txtPerOtherCity" Visible="false" PlaceHolder="Please Enter Other City" CssClass="form-control" ToolTip="Please Enter Other City" AutoComplete="off"></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                        TargetControlID="txtCCity" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPermanentCity" InitialValue="0"
                                                        ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Select Permanent City.">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>PIN Code/ZIP</label>
                                                </div>
                                                <asp:TextBox ID="txtPermPIN" runat="server" CssClass="form-control" TabIndex="43" ToolTip="Please Enter Pincode." MaxLength="6" placeholder="Enter Pincode" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="Submit" ErrorMessage="Please Enter Pincode." Display="None" ControlToValidate="txtPermPIN" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender48" runat="server" TargetControlID="txtPermPIN" FilterMode="ValidChars"
                                                    ValidChars="0123456789">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtPermPIN"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="^[0-9]{6}$"
                                                    ErrorMessage="Please Enter Valid Pincode." ValidationGroup="Submit"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Educational Details</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updEdcutationalDetails" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row" id="EditEdudetail" runat="server" visible="false">

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <asp:Label ID="Label1" runat="server" Font-Size="12px"><sup>*</sup><b>Degree</b></asp:Label>
                                                <asp:DropDownList ID="ddlEduDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlEduDegree_SelectedIndexChanged" TabIndex="44" ToolTip="Please Select Degree." data-select2-enable="false" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlEduDegree" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" style="display: none">
                                                <asp:Label ID="Label2" runat="server" Font-Size="12px"><sup>*</sup><b>Programme Code</b></asp:Label>
                                                <asp:DropDownList ID="ddlCode" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" TabIndex="45" ToolTip="Please Select Programme Code." data-select2-enable="false" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCode" ErrorMessage="Please Select Programme Code." InitialValue="0" ValidationGroup="Submit" Display="None" Visible="false"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="div2" runat="server">
                                                <asp:Label ID="Label3" runat="server" Font-Size="12px"><sup>*</sup><b>Programme/Branch</b></asp:Label>
                                                <asp:DropDownList ID="ddlEduBranch" OnSelectedIndexChanged="ddlEduBranch_SelectedIndexChanged" runat="server" AppendDataBoundItems="true" TabIndex="46" AutoPostBack="true"
                                                    CssClass="form-control" ToolTip="Please Select Programme/Branch." data-select2-enable="false" Enabled="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlEduBranch"
                                                    ErrorMessage="Please Select Programme/Branch." Display="None" SetFocusOnError="true"
                                                    ValidationGroup="Submit" InitialValue="0" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <asp:Label ID="lblQual" runat="server" Font-Size="12px"><sup>*</sup><b>Qualification Type</b></asp:Label>
                                                <asp:DropDownList ID="ddlQual" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlQual_SelectedIndexChanged" TabIndex="47" ToolTip="Please Select Qualification Type." data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Qualify</asp:ListItem>
                                                    <asp:ListItem Value="2">Entrance</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlQual" ErrorMessage="Please Select Qualification Type." InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <asp:Label ID="lblQualExam" runat="server" Font-Size="12px"><sup>*</sup><b>Qualifying Exam/Subject</b></asp:Label>
                                                <asp:DropDownList ID="ddlExam" runat="server" AppendDataBoundItems="True" CssClass="form-control" TabIndex="48" ToolTip="Please Select Examination." data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlExam" ErrorMessage="Please Select Qualifying Exam/Subject." InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divSubject" runat="server" visible="false">
                                                <label><sup>*</sup>Name of the Subject</label>
                                                <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control" ToolTip="Please Select Compulsory Subject." OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlSubject" ErrorMessage="Please Select Compulsory Subject." InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divOther" runat="server" visible="false">
                                                <label><sup>*</sup>Other Subject (If Any)</label>
                                                <asp:TextBox ID="txtOther" runat="server" CssClass="form-control" ToolTip="Enter Other Subject" MaxLength="64" AutoComplete="off"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fbeOther" runat="server" TargetControlID="txtOther" InvalidChars="~`!@#$%^&*+=|}}{{:;?<>,_" FilterMode="InvalidChars"></ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvOther" runat="server" ControlToValidate="txtOther" Display="None" ErrorMessage="Please Enter Other Subject"
                                                    SetFocusOnError="true" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divStatus1" runat="server" style="display: none">
                                                <label><sup>*</sup>Pass Status</label><br />
                                                <asp:RadioButton ID="rdoPass" runat="server" AutoPostBack="true" Checked="true" GroupName="PassApear" Text="Passed" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="rdoAppear" runat="server" AutoPostBack="true" GroupName="PassApear" Text="Appearing" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <%--OnTextChanged="txtMarksObtained_TextChanged"--%>
                                                <sup>*</sup><asp:Label ID="lblMarksObt" runat="server" Font-Size="12px"><b>Marks Obtained in Qualifying Subject</b></asp:Label>
                                                <asp:TextBox ID="txtMarksObtained" Autocomplete="Off" runat="server" CssClass="form-control" MaxLength="5" placeholder="Enter Obtained Marks" TabIndex="49" ToolTip="Please Enter Marks Obtained in Qualifying Exam/Subject." />
                                                <asp:RequiredFieldValidator ID="rfvObtMarks" runat="server" ControlToValidate="txtMarksObtained" Display="None" ErrorMessage="Please Enter Marks Obtained in Qualifying Exam/Subject." SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True" TargetControlID="txtMarksObtained" ValidChars="0123456789." FilterMode="ValidChars" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <sup>*</sup><asp:Label ID="lblFull" runat="server" Font-Size="12px"><b>Full Mark of Paper (Atleast 100)</b></asp:Label>
                                                <asp:TextBox ID="txtMaxMark" Autocomplete="Off" runat="server" AutoPostBack="true" CssClass="form-control" MaxLength="5" OnTextChanged="txtMaxMark_TextChanged" placeholder="Enter Maximum Marks" TabIndex="50" ToolTip="Please Enter Maximum Marks." />
                                                <asp:RequiredFieldValidator ID="rfvMaxMarks" runat="server" ControlToValidate="txtMaxMark" Display="None" ErrorMessage="Please Enter Maximum Marks." SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" Enabled="True" TargetControlID="txtMaxMark" ValidChars="0123456789." />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <%-- TabIndex="6"--%>
                                                <asp:Label ID="lblpercent" runat="server" Font-Size="12px"><sup>*</sup><b>Percentage</b></asp:Label>&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdoPer" runat="server" AutoPostBack="true" Checked="true" GroupName="check" Text="Percentage" Visible="false" />
                                                &nbsp;&nbsp;
                                                <asp:RadioButton ID="rdoCGPA" runat="server" AutoPostBack="true" GroupName="check" Text="CGPA" Visible="false" />
                                                <asp:TextBox ID="txtPer" runat="server" CssClass="form-control" MaxLength="5" TabIndex="51" onkeyup="validateNumeric(this);" placeholder="Enter Percentage" ToolTip="Please Enter Percentage." AutoComplete="off" Enabled="false" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True" TargetControlID="txtPer" ValidChars="0123456789." FilterMode="ValidChars" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divYear" runat="server" style="display: none" visible="false">
                                                <asp:Label ID="lblYrPass" runat="server" Font-Size="12px"><sup>*</sup><b>Year of Passing</b></asp:Label>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" TabIndex="52" ToolTip="Please Enter Year Of Passing." AppendDataBoundItems="true" data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlYear" Display="None" ErrorMessage="Please Select Year Of Passing." SetFocusOnError="True" ValidationGroup="Submit" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="Div3" visible="false">
                                                <label><sup>*</sup>Major Subjects</label>
                                                <asp:TextBox ID="txtSubj" runat="server" CssClass="form-control" MaxLength="100" ToolTip="Please Enter Subjects." />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtSubj" Display="None" ErrorMessage="Please Enter Major Subjects." SetFocusOnError="True" ValidationGroup="Submit" Visible="false"></asp:RequiredFieldValidator>

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" Enabled="True" FilterMode="InvalidChars" InvalidChars="~!@#$%^&amp;*()_+|?&gt;&lt;/?" TargetControlID="txtSubj" />
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" id="divBoard" runat="server" visible="false">
                                                <asp:Label ID="lblBoardUni" runat="server" Font-Size="12px"><sup>*</sup><b>Board/University</b></asp:Label>
                                                <asp:DropDownList ID="ddlBoard" runat="server" AppendDataBoundItems="True" CssClass="form-control" ToolTip="Please Select Universty/Board ." data-select2-enable="false">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlBoard" Display="None" ErrorMessage="Please Select Universty/Board." InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-4 col-md-6 col-12" runat="server" id="Div4" visible="false">
                                                <label><sup></sup>Upload Marksheet</label>
                                                <asp:FileUpload ID="fucontrol" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="fucontrol" Display="None" ErrorMessage="Please Select File TO Upload." InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblUpload" runat="server" ForeColor="Red" Text="(Upload .pdf file only)"></asp:Label>
                                                <asp:Label ID="Label4" runat="server" ForeColor="Green" Visible="false"></asp:Label>
                                            </div>
                                            <asp:Label ID="lblFlagEdu" runat="server" Text="" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:ListView ID="lvEntrance" runat="server" EnableModelValidation="True" Visible="false">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="table table-responsive">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <%--<th>Edit </th>--%>
                                                                    <th>Edit</th>
                                                                    <th>Branch</th>
                                                                    <%--<th>Qualification Type</th>--%>
                                                                    <th>Qualifying Exam/Subject</th>
                                                                    <%--<th>Compulsory Subject</th>--%>
                                                                    <%--<th>Other Subject</th>--%>
                                                                    <th>Year of Passing </th>
                                                                    <%-- <th>Year/Sem</th>--%>
                                                                    <%--<th hidden>Pass Status</th>--%>
                                                                    <th>Marks Obtained in Qualifying Exam/Subject </th>
                                                                    <th>Total Marks in Qualifying Exam/Subject</th>
                                                                    <th>Percentage</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item">
                                                    <td>
                                                        <asp:ImageButton ID="btnEditlv" CausesValidation="false" runat="server" ImageUrl="~/images/edit.png" AlternateText='<%#Eval("STLQNO") %>' CommandArgument='<%# Eval("STLQNO") %>' ToolTip='<%# Container.DataItemIndex +1%>' TabIndex="53" OnClick="btnEditlv_Click" />

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label>
                                                    </td>
                                                    <td id="tdQualyType" runat="server" style="display: none">
                                                        <asp:Label ID="lblQualType" runat="server" Text='<%# Eval("QUALIFY_TYPE") %>'></asp:Label>
                                                    </td>
                                                    <td id="qualifyno" runat="server">
                                                        <asp:Label ID="lblExam" runat="server" Text='<%# Eval("QUALIFYEXAMNAME")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField ID="lblExamNo" runat="server" Value='<%# Eval("QUALIFYNO") %>' />
                                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR_OF_PASSING")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblObtMarks" runat="server" Text='<%# Eval("OBTAINED_MARKS")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMaxMarks" runat="server" Text='<%# Eval("OUT_OF_MARKS")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPercent" runat="server" Text='<%# Eval("PERCENTAGE")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Photo and Signature Details</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" ToolTip="Please Upload Photo" ImageUrl="~/IMAGES/nophoto.jpg" Style="margin-bottom: 5px;" />
                                        <asp:FileUpload ID="fuPhoto" runat="server" TabIndex="54" onchange="previewFilePhoto()" accept=".jpg, .jpeg" />
                                        <asp:RegularExpressionValidator ID="rfvPhoto" ControlToValidate="fuPhoto" runat="Server"
                                            ErrorMessage="Only jpg or jpeg files are allowed" ValidationExpression="^.*\.(jpeg|JPEG|jpg|JPG|jpe|JPE)$"
                                            ValidationGroup="Submit" Display="None" />
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <asp:Image ID="ImgSign" runat="server" Width="125px" Height="40px" ToolTip="Please Upload Photo" ImageUrl="~/IMAGES/nophoto.jpg" Style="margin-bottom: 5px;" />
                                        <asp:FileUpload ID="fuSignature" runat="server" TabIndex="55" onchange="previewFileSignature()" accept=".jpg, .jpeg, .png" />
                                        <asp:RegularExpressionValidator ID="rfvSignature" ControlToValidate="fuSignature" runat="Server"
                                            ErrorMessage="Only jpg or jpeg files are allowed" ValidationExpression="^.*\.(jpeg|JPEG|jpg|JPG|jpe|JPE)$"
                                            ValidationGroup="Submit" Display="None" />
                                    </div>

                                    <div class="form-group col-lg-10 col-md-12 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading"><span style="color: red">Note</span></h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span style="color: red">jpg or jpeg file required for Photo and Signature upto 150KB only.</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updDocuments" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Document List</h5>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <asp:ListView ID="lvDocument" runat="server" OnItemDataBound="lvDocument_ItemDataBound">
                                                    <%--OnItemDataBound="lvFourthSem_ItemDataBound"--%>
                                                    <LayoutTemplate>
                                                        <div id="demo-grid" class="vista-grid">
                                                            <div class="table table-responsive">
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No
                                                                            </th>
                                                                            <th>Document Name
                                                                            </th>
                                                                            <th>File Name
                                                                            </th>
                                                                            <th>Choose File</th>
                                                                            <%-- <th>Upload</th>--%>
                                                                            <th>Document Upload Status</th>
                                                                            <th>Preview</th>
                                                                            <%-- <th></th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <%--  </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td>
                                                                <asp:Label ID="lblSRNO" runat="server" Text=' <%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCUMENTNAME") %> ' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbldoc" runat="server" ToolTip='<%# Eval("DOCUMENTNO") %>'
                                                                    CommandArgument='<%#Eval("DOCUMENTNO")%>' Text='<%# Eval("FILENAME") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fuDocument1" runat="server" TabIndex="56" ToolTip='<%# Eval("DOCUMENTNO") %>'
                                                                    ValidationGroup="submit" />
                                                            </td>

                                                            <%--<td>
                                                                <asp:UpdatePanel ID="updUpload" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Button ID="btnUpload" runat="server" TabIndex="3" Text="Upload" ToolTip="Click to Upload"
                                                                            OnClick="btnUpload_Click" CssClass="btn btn-outline-info" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnUpload" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblStatus" runat="server" ToolTip="Document Status" Text='<%# Convert.ToInt32(Eval("DOCUMENT_STATUS"))==1 ? "Completed":"Pending"   %>' ForeColor='<%# (Convert.ToInt32(Eval("DOCUMENT_STATUS") )==1 ? System.Drawing.Color.Green: System.Drawing.Color.Red)%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:UpdatePanel ID="updPreview" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="imgbtnPrevDoc" runat="server" TabIndex="57" CommandArgument='<%# Eval("PREVIEW_PATH") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="~/IMAGES/search.png" OnClick="imgbtnPrevDoc_Click" Text="Preview" ToolTip='<%# Eval("FILENAME") %>' Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME"))==string.Empty?false:true %>' />
                                                                        <asp:Label ID="lblPreview" Text='<%# Convert.ToString(Eval("FILENAME"))==string.Empty ?"Preview not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="imgbtnPrevDoc" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <%--<Triggers>
                                    <asp:PostBackTrigger ControlID="imgbtnPrevDoc" />
                                </Triggers>--%>
                            </asp:UpdatePanel>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmitPopup" runat="server" Font-Bold="true" Text="Submit" CssClass="btn btn-outline-success" TabIndex="58" data-dismiss="modal" ValidationGroup="Submit" OnClick="btnSubmitPopup_Click" />
                                    <asp:Button ID="btnClose" runat="server" Font-Bold="true" Text="Close" CssClass="btn btn-outline-danger" data-dismiss="modal" TabIndex="59" ValidationGroup="b" OnClick="btnClose_Click" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmitPopup" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:ValidationSummary ID="vfSubmit" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                    </div>
                </div>
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <ajaxToolKit:ModalPopupExtender ID="Mp1" runat="server" TargetControlID="lnkPopup" PopupControlID="pnlPopup" BackgroundCssClass="Background" CancelControlID="btnCloseX"></ajaxToolKit:ModalPopupExtender>


    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlPopupdoc"
        TargetControlID="lnkPreview" CancelControlID="btnClosePopdoc" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("PREVIEW_PATH") %>'></asp:LinkButton>
    <asp:Panel ID="pnlPopupdoc" runat="server" CssClass="modalPopup">
        <div class="header">
            Document
                 
        </div>
        <div class="body">
            <iframe runat="server" width="680px" height="550px" id="iframeView"></iframe>
        </div>
        <div class="footer" align="right">
            <asp:Button ID="btnClosePopdoc" runat="server" ForeColor="Red" Font-Bold="true" TabIndex="1" Font-Size="Larger" Text="Close" CssClass="no" />
        </div>
    </asp:Panel>

    <script>
        function validateProgrammeCode() {
            var selectedValue = document.getElementById('<%= ddlDegree.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Degree.');
                return false;
            }
            else {
                return true;
            }
        }

        function validateProgrammeType() {
            var selectedValue = document.getElementById('<%= ddlProgrammeType.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Programme Type.');
                return false;
            }
            else {
                return true;
            }
        }


        function validateControl() {

            var selectedValue = document.getElementById('<%= ddlProgrammeType.ClientID%>').value;
            if (selectedValue == "0") {
                alert('Please Select Programme Type.');
                return false;
            }
            else {
                return true;
            }
        }
        function validateAdmissionBatch() {
            var admBatch = document.getElementById('<%=ddlAdmbatch.ClientID%>');
            if (admBatch.value == 0) {
                alert('Please Select Admission Batch.');
                return false;
            }
        }
    </script>
    <script>
        function OtherOcc() {
            //alert("in");
            var fOcc = document.getElementById('<%=ddlFOccupation.ClientID%>').value;
            var mOcc = document.getElementById('<%=ddlMOccupation.ClientID%>').value;
            //alert(fOcc);
            //alert(mOcc);
            if (fOcc == "7") {
                $("#divFoccupation").show();
                //var FatherOcc = document.getElementById('<%=txtOccFather.ClientID%>').value;
            }
            else {
                $("#divFoccupation").hide();
            }
            if (mOcc == "7") {
                //alert("Show M");
                $("#divMoccupation").show();
            }
            else {
                $("#divMoccupation").hide();
            }
        }
    </script>
    <script type="text/javascript">
        function previewFilePhoto() {
            if (validateFileSize()) {

                var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhoto.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {

                    reader.readAsDataURL(file);

                } else {
                    preview.src = "";
                }
            }
            else {
                alert('File size should be less than or equal to 150KB.');
                $imgPhotoNoCrop.val('');

                return;
            }
        }

        function totAllSubjects(headchk) {
            debugger;
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

        function previewFileSignature() {
            if (validateFileSizeSign()) {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignature.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }

            }
            else {
                alert('File size should be less than or equal to 150 KB.');

                return;
            }
        }


        function validateFileSize() {
            var uploadControl = document.getElementById('<%= fuPhoto.ClientID %>');
            if (uploadControl.files[0].size > 153600) {
                return false;
            }
            else {
                return true;
            }
        }
        function validateFileSizeSign() {
            var uploadControl = document.getElementById('<%= fuSignature.ClientID %>');
            if (uploadControl.files[0].size > 153600) {
                return false;
            }
            else {
                return true;
            }
        }

        //***********VALIDATION FOR OBTAINED AND MAX MARKS*******************
        function ValidateMaxMarksSelectedRow(obj) {
            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var obtm = 0;
            var maxm = 0;
            obtm = parseFloat(row.cells[2].getElementsByTagName("input")[0].value);
            maxm = parseFloat(row.cells[3].getElementsByTagName("input")[0].value);
            if (obtm > maxm) {
                alert('Max Marks should be greater than Obtained Marks.');
                //row.cells[3].focus();
            }
            return false;
        }
    </script>
</asp:Content>



