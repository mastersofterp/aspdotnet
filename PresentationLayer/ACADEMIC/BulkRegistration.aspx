<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
    CodeFile="BulkRegistration.aspx.cs" Inherits="ACADEMIC_BulkRegistration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblHeadstdlist').DataTable({
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
                                return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                            return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                            else if ($(node).find("input:submit").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:submit").each(function () {
                                                    nodereturn += $(this).val();
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
                                            return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                            else if ($(node).find("input:submit").length > 0) {
                                                nodereturn = "";
                                                $(node).find("input:submit").each(function () {
                                                    nodereturn += $(this).val();
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
                var table = $('#tblHeadstdlist').DataTable({
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
                                    return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                                return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                                else if ($(node).find("input:submit").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:submit").each(function () {
                                                        nodereturn += $(this).val();
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
                                                return $('#tblHeadstdlist').DataTable().column(idx).visible();
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
                                                else if ($(node).find("input:submit").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:submit").each(function () {
                                                        nodereturn += $(this).val();
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

    <script>
        $(document).ready(function () {


            $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').on('click', function () {
                // Get all rows with search applied

                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
                var count = 0;

                for (i = 0; i <= rows.length; i++) {
                    if (document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_cbHead').checked == true) {
                        document.getElementById('<%= txtTotStud.ClientID %>').value = count++;
                    }
                    else {
                        document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
                    }
                }

            });



            // Handle click on checkbox to set state of "Select all" control
            $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {

                // If checkbox is not checked
                //if (!this.checked) {

                var el = $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').get(0);
                var rows = table.rows({ 'search': 'applied' }).nodes();

                // Check/uncheck checkboxes for all rows in the table
                if (el && el.checked && ('indeterminate' in el)) {
                    // Set visual state of "Select all" control
                    // as 'indeterminate'
                    el.indeterminate = true;
                }
                var tot = document.getElementById('<%= txtTotStud.ClientID %>');
                //alert(this.checked)
                //for (i = 0; i <= rows.length; i++) {
                if (this.checked == true) {
                    tot.value = Number(tot.value) + 1;
                }
                else {
                    tot.value = Number(tot.value) - 1;
                    //document.getElementById('<%= txtTotStud.ClientID %>').value = count--;
                }
                //}
                // If "Select all" control is checked and has 'indeterminate' property

                //}
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').on('click', function () {

                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);


                });

                // Handle click on checkbox to set state of "Select all" control
                $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {

                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;

                        }
                    }
                });
            });
        });
    </script>


    <asp:UpdatePanel ID="updBulkReg" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="divMsg" runat="server">
                        </div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">BULK COURSE REGISTRATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">

                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 ">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--  <label>College & Scheme</label>--%>
                                            <asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                            ValidationGroup="submit" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%----%>
                                        <%-- <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="True" TabIndex="1" onchange="fn();"  CausesValidation="False"
                                            ToolTip="Please Select College & Scheme" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                        </asp:DropDownList>--%>
                                        <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlClgname" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select College & Scheme" InitialValue="0" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Session</label>--%>
                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true" ValidationGroup="submit"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" data-select2-enable="true" CssClass="form-control" CausesValidation="False">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--data-select2-enable="true"--%>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged"
                                            AutoPostBack="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <%-- <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="submit" SetFocusOnError="true"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>School/Institute Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select School/Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select School/Institute Name" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programme/Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Programme/Branch"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Scheme</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ErrorMessage="Please Select Scheme"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Semester</label>--%>
                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True" SetFocusOnError="true"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" ValidationGroup="submit" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Student Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Section</label>--%>
                                            <asp:Label ID="lblDYddlSection" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:HiddenField ID="hftot" runat="server" />
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Total Students Selected</label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                            ForeColor="#000066"></asp:TextBox>
                                        <%--meta:resourcekey="txtTotStudResource1"--%>
                                        <%--<ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Total Credit Limit :</label>
                                        </div>
                                        <asp:TextBox ID="lblTotalCredit" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                            ForeColor="#000066"></asp:TextBox>
                                        <%--meta:resourcekey="txtTotStudResource1"--%>
                                        <%--<ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                            WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />--%>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" OnClientClick="return validateAssign();"
                                    Enabled="false" />
                                <asp:Button ID="btnReport" runat="server" Text="Registration Slip Report"
                                    CssClass="btn btn-info" OnClick="btnReport_Click" Visible="false"
                                    ValidationGroup="submit" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                    CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ValidationGroup="submit" ShowSummary="false" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-md-6 col-12">
                                        <asp:Panel ID="pnlStudents" runat="server" Visible="true">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <%--OnLayoutCreated="lvStudent_LayoutCreated">--%>
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHeadstdlist">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" Text="Select All" runat="server"  ToolTip="Select/Select all" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblDYtxtRegNo" runat="server" Font-Bold="true"></asp:Label>
                                                                </th>
                                                                <%--<th style="text-align: left; width: 15%">
                                                                                Enroll. No.
                                                                            </th>--%>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Print Report</th>
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
                                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>'  Onclick="totStudents(this)"/>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <%--<td style="text-align: left; width: 25%">
                                                                        <asp:LinkButton ID="lblRegNo" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip="Click here to Display Registered Courses"
                                                                            CommandArgument='<%# Eval("IDNO") %>' OnClick="btnPrint_Click" ValidationGroup="submit"
                                                                            Font-Underline="false" ForeColor="Black" />
                                                                    </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="lnkPrintRegReport" runat="server" ToolTip='<%# Eval("IDNO") %>' CssClass="btn btn-info"
                                                                CausesValidation="False"
                                                                OnClick="lnkPrintRegReport_Click"></asp:Button>
                                                            <%--OnClientClick="return confirm('Do you want to See the Report ? ');"   OnClientClick="fn();return false;" --%> <%--PostBackUrl="~/academic/BulkRegistration.aspx"--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <%--<AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" onClick="totStudents(this);" GroupName="BoxChk" />
                                                        </td>
                                                        <td >
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                              
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>--%>
                                            </asp:ListView>
                                           <asp:HiddenField ID="hdStudntCount" runat="server" />
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-6 col-12">
                                        <asp:Panel ID="pnlCourses" runat="server" Visible="true">
                                            <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="divlvPaidReceipts">
                                                        <div class="sub-heading">
                                                            <h5>Offered Courses</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" Text="Select All" runat="server" onclick="return SelectAllCourse(this)" ToolTip="Select/Select all" />
                                                                    </th>
                                                                    <th>
                                                                        <asp:Label ID="lblDYtxtCourseCode" runat="server" Font-Bold="true"></asp:Label>
                                                                        -
                                                                        <asp:Label ID="lblDYtxtCourseName" runat="server" Font-Bold="true"></asp:Label>
                                                                    </th>
                                                                    <th>Credit </th>
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
                                                            <asp:CheckBox ID="cbRow" runat="server" OnCheckedChanged="cbRow_CheckedChanged" AutoPostBack="true" ToolTip='<%# Eval("CREDITS") %>' Checked="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                            &nbsp;
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("CREDITS") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="altitem">
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" OnCheckedChanged="cbRow_CheckedChanged" AutoPostBack="true" ToolTip='<%# Eval("CREDITS") %>' Checked="true" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' ToolTip='<%# Eval("CREDITS") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                            <asp:HiddenField ID="hfdCourseCount" runat="server" />
                                        </asp:Panel>
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlStudentsReamin" runat="server">
                                            <asp:ListView ID="lvStudentsRemain" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List (Demand Not Found)</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblHead">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th>HT No.
                                                                </th>
                                                                <th>Student Name
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
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <%--<AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                        <td >
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' />
                                                        </td>
                                                        <td >
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>--%>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <%--<div id="divMsg" runat="server">
                    </div>--%>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlClgname" />
            <asp:PostBackTrigger ControlID="ddlSemester" />
            <asp:PostBackTrigger ControlID="ddlsection" />
            <asp:PostBackTrigger ControlID="ddlSession" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="ddlAdmBatch" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
            }
            //		var frm = document.forms[0]
            //		for (i=0; i < document.forms[0].elements.length; i++) {
            //			var e = frm.elements[i];
            //			if (e.type == 'checkbox') {
            //			    if (headchk.checked == true) {
            //			        if (e.disabled == false) { e.checked = true; }
            //			    }
            //			    else
            //			        e.checked = false;
            //			}
            //        }
            //        if (headchk.checked == true) {
            //            txtTot.value = hftot.value;
            //            }
            //        else {
            //            txtTot.value = 0;
            //        }
        }

        function SelectAllCourse(headchk) {
            var i = 0;
           // var hfdCourseCount = document.getElementById('<%= hfdCourseCount.ClientID %>');
            var hfdCourseCount = document.getElementById('<%= hfdCourseCount.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hfdCourseCount) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvCourse_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox')
                    lst.checked = (headchk.checked == true) ? true : false;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
             
                        alert('Please Check atleast one student ');
                        return false;         
            }
            else
                return true;
        }

   
        function totStudents(chk) {
  
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var currentCount = Number(txtTot.value); // Added By Vipul T on date 17-02-2024 as per Tno:-
          
            if (chk.checked) {             
                txtTot.value = currentCount + 1;
            } else {                          
                if (currentCount > 0) {
                    txtTot.value = currentCount - 1;
                }
            }          
        }

      
  


      

    </script>

</asp:Content>
