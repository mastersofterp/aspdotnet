<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserRolemaster_LinkAssign.aspx.cs" Inherits="UserRolemaster_LinkAssign" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" language="javascript">
        $(function () {
            $("[id*=tvLinks] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {

                            $(this).attr("checked", "checked");
                        } else {

                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {

                        ////                   
                        if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
                        else {
                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        }
                    }
                }
            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $("[id*=tvLinks] input[type=checkbox]").bind("click", function () {
                    var table = $(this).closest("table");
                    if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                        //Is Parent CheckBox
                        var childDiv = table.next();
                        var isChecked = $(this).is(":checked");
                        $("input[type=checkbox]", childDiv).each(function () {
                            if (isChecked) {

                                $(this).attr("checked", "checked");
                            } else {

                                $(this).removeAttr("checked");
                            }
                        });
                    } else {
                        //Is Child CheckBox
                        var parentDIV = $(this).closest("DIV");
                        if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        } else {

                            ////                   
                            if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                                $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                            }
                            else {
                                $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                            }
                        }
                    }
                });
            });

        });

    </script>--%>
    <style>
    .custom-dropdown {
    /* Reset the form-control class or remove it */
    padding: 0;
    /* Other CSS properties as needed */
}

    </style>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
        #ctl00_ContentPlaceHolder1_DataPager1 a {
            background-color: #fff;
            border: 1px solid;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
        }

        #ctl00_ContentPlaceHolder1_NumberDropDown {
            padding-top: 0.25rem;
            padding-bottom: 0.25rem;
            padding-left: 0.5rem;
            font-size: .845rem;
            border-radius: 0.25rem;
            border: 1px solid #ccc;
        }
        /*.default-c .paginate_button.page-item.active a {
        }*/

        #ctl00_ContentPlaceHolder1_DataPager1 span {
            background-color: #0d70fd;
            border: 1px solid #fff;
            padding: 6px 10px;
            border-radius: 19px;
            text-decoration: none;
            color: #fff;
        }
    </style>
    <script type="text/javascript" language="javascript">
        $(function () {
            $("[id*=tvLinks] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {

                            $(this).attr("checked", "checked");
                        } else {

                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {



                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {

                        ////                   
                        if (($("input[type=checkbox]:checked", parentDIV).length == 0)) {

                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
                        else {
                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        }
                    }
                }
            });
        })

    </script>

    <style>
        .nav-tabs-custom > .nav-tabs > li.active:hover > a {
            background-color: #fff;
            color: #3c8dbc;
        }
    </style>

    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#tblHead').DataTable({
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
                                return $('#tblHead').DataTable().column(idx).visible();
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
                                            return $('#tblHead').DataTable().column(idx).visible();
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
                                            return $('#tblHead').DataTable().column(idx).visible();
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
                var table = $('#tblHead').DataTable({
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
                                    return $('#tblHead').DataTable().column(idx).visible();
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
                                                return $('#tblHead').DataTable().column(idx).visible();
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
                                                return $('#tblHead').DataTable().column(idx).visible();
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


    <asp:UpdatePanel runat="server" ID="UpdRole" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tabLC">User Role</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tabBC">User Role Menu</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="3" href="#tabBD">Bulk Link Assign Department Wise</a>
                                    </li>
                                </ul>
                                <div class="box-tools pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                                <div class="tab-content" id="my-tab-content">

                                    <div class="tab-pane active" id="tabLC">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Upduserrole"
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

                                        <asp:UpdatePanel runat="server" ID="Upduserrole" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="div1" runat="server"></div>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>User Role</h5>
                                                    </div>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>User Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlUserType" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="req_usertype" runat="server" ControlToValidate="ddlUserType"
                                                                    ErrorMessage="Please Select User Type" Display="None" SetFocusOnError="true" InitialValue="0" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>User Role Name</label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtUserRole" TabIndex="5" CssClass="form-control" placeholder="Enter User Role" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserRole" SetFocusOnError="true"
                                                                    ErrorMessage="Please Enter User Role" Display="None" ValidationGroup="submit" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Role Description</label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtroledescript" TabIndex="6" CssClass="form-control" placeholder="Enter Description" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                                            </div>

                                                            <%--<div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Active Status</label>
                                                                </div>
                                                                <asp:CheckBox ID="chkFlock" TabIndex="7" runat="server" />
                                                            </div>--%>

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

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note (Please Select)</h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Checked: Status- <span style="color: green; font-weight: bold">Active </span></span></p>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>UnChecked: Status- <span style="color: red; font-weight: bold">De-Active </span></span></p>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validate();" Text="Submit" CssClass="btn btn-primary" ValidationGroup="submit" TabIndex="8"
                                                            OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" TabIndex="9" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <asp:Repeater ID="lvrolelist" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>User Role List</h5>
                                                                        </div>
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>User Type
                                                                                </th>
                                                                                <th>Role Name
                                                                                </th>
                                                                                <th>Description
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnEdit" class="newAddNew Tab" TabIndex="10" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ROLE_NO")+","+Eval("ROLE_NAME") %>'
                                                                                    ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblscheme" runat="server" Text='<%#Eval("USERDESC")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbllname" runat="server" Text='<%# Eval("ROLE_NAME")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbldescrip" runat="server" Text='<%# Eval("ROLE_DES")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblpattname" runat="server" Text='<%# Eval("ACTIVE_STATUS")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tabBC">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updrolemenu"
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

                                        <asp:UpdatePanel ID="updrolemenu" runat="server">
                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>User Role Menu</h5>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>User Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlMusertype" runat="server" AppendDataBoundItems="True" TabIndex="4" CssClass="form-control"
                                                                    data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlMusertype_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlMusertype" SetFocusOnError="true"
                                                                    ErrorMessage="Please Select User Type" Display="None" InitialValue="0" ValidationGroup="Menusubmit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>User Role</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlMuserrole" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMuserrole" SetFocusOnError="true"
                                                                    ErrorMessage="Please Select User Role" Display="None" InitialValue="0" ValidationGroup="Menusubmit" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnMSubmit" runat="server" Text="Submit" TabIndex="6" CssClass="btn btn-primary" ValidationGroup="Menusubmit"
                                                            OnClick="btnMSubmit_Click" />
                                                        <asp:Button ID="btnMcancel" runat="server" Text="Cancel" TabIndex="7" CssClass="btn btn-warning" OnClick="btnMcancel_Click" />

                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="Menusubmit" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <asp:Repeater ID="lvrolemenulist" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>User Role Menu List</h5>
                                                                        </div>
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Action
                                                                                </th>
                                                                                <th>User Type
                                                                                </th>
                                                                                <th>Role Name
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnRmenuEdit" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ROLE_NO") %>'
                                                                                    CommandName='<%# Eval("UA_TYPE") %>' ToolTip="Edit Record" OnClick="btnRmenuEdit_Click" TabIndex="8" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblscheme" runat="server" Text='<%#Eval("USERDESC")%>'></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lbllname" runat="server" Text='<%# Eval("ROLE_NAME")%>'></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </tbody>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </table>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 mt-4">
                                                        <div class="row">

                                                            <div class="col-6">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Access Link</h5>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                                    <asp:UpdatePanel ID="Upd_treepnl" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlTree" runat="server" ScrollBars="Auto" Height="250px">
                                                                                <asp:TreeView ID="tvLinks" runat="server" ExpandDepth="0">
                                                                                </asp:TreeView>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="btnCheckID" EventName="Click" />--%>
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                            <div class="col-6" id="div_tvOriginalLinks" runat="server" visible="false">
                                                                <div class="col-12">
                                                                    <div class="sub-heading">
                                                                        <h5>Original Access Links</h5>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group col-lg-12 col-md-12 col-12">
                                                                    <asp:UpdatePanel ID="Upd_treeOriginalpnl" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Panel ID="pnlOriginalTree" runat="server" ScrollBars="Auto" Height="250px">
                                                                                <asp:TreeView ID="tvOriginalLinks" runat="server" ExpandDepth="0">
                                                                                </asp:TreeView>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="btnCheckID" EventName="Click" />--%>
                                                                            <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />--%>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tabBD">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updbulkassign"
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
                                        <asp:UpdatePanel ID="updbulkassign" runat="server">
                                            <ContentTemplate>
                                                <div id="div3" runat="server"></div>
                                                <div class="col-12 mt-3">
                                                    <div class="sub-heading">
                                                        <h5>Bulk Link Assign Department Wise</h5>
                                                    </div>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12 col-lg-6 col-md-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>User Details</h5>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>User Type</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBusertype" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                            AutoPostBack="True" onchange="ResetCheckBoxValue();" OnSelectedIndexChanged="ddlBusertype_SelectedIndexChanged" TabIndex="4">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select User Type."
                                                                            ControlToValidate="ddlBusertype" Display="None" SetFocusOnError="True" ValidationGroup="BulkShow"
                                                                            InitialValue="0">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="trBDept" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <%--<label>Department</label>--%>
                                                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBDept" runat="server" AppendDataBoundItems="True" TabIndex="5" CssClass="form-control"
                                                                            data-select2-enable="true" onchange="ResetCheckBoxValue();">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12 h-100">
                                                                <div class="sub-heading">
                                                                    <h5>Access Link</h5>
                                                                </div>
                                                                <div class="form-group col-md-12 checkbox-list-box">
                                                                    <asp:CheckBoxList ID="chkListRole" runat="server" AppendDataBoundItems="true"
                                                                        TabIndex="1" RepeatColumns="2" RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                                                    </asp:CheckBoxList>
                                                                    <asp:HiddenField runat="server" ID="hfsemno" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="pnlBStudent" runat="server" visible="false">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Degree</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBdegree" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" onchange="ResetCheckBoxValue();" OnSelectedIndexChanged="ddlBdegree_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Branch</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBbranch" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                                                    data-select2-enable="true" onchange="ResetCheckBoxValue();">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlBSemester" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                                                    data-select2-enable="true" onchange="ResetCheckBoxValue();">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnBShow" runat="server" Text="Show" OnClick="btnBShow_Click" ValidationGroup="BulkShow" CssClass="btn btn-primary" TabIndex="6" />
                                                        <asp:Button ID="btnBSubmit" runat="server" Text="Submit" OnClick="btnBSubmit_Click"
                                                            Enabled="false" CssClass="btn btn-primary" TabIndex="7" />
                                                        <asp:Button ID="btnBcancel" runat="server" Text="Cancel" OnClick="btnBcancel_Click" CssClass="btn btn-warning" TabIndex="8" />
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="BulkShow" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlBulkDetail" runat="server" Visible="false">
                                                            <div class="col-12">
                                                                <asp:CheckBox ID="cbAllowSingleSelection" runat="server" Text="check here to assign role for single user" TabIndex="9" OnClick="setCheckSingleCB(this)" />
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Label ID="lblBtotal" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                            </div>

                                                            <%--Number Dropdown Added By Shrikant W. on 07-09-2023--%>
                                                            <div class="row">
                                                                <div class="col-lg-9 col-md-9 col-sm-9 col-12 d-flex mb-0 pb-0 mt-3">
                                                                    <div class="form-group d-flex mb-0 pb-0">
                                                                        <label class="mt-1 mr-1">Show</label>
                                                                        <asp:DropDownList ID="NumberDropDown" runat="server" CssClass="custom-dropdown" AutoPostBack="true" OnSelectedIndexChanged="NumberDropDown_SelectedIndexChanged">
                                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                                                                            <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                                            <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                                            <asp:ListItem Text="1500" Value="1500"></asp:ListItem>
                                                                            <asp:ListItem Text="2000" Value="2000"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <label class="mt-1 ml-1">Entries</label>
                                                                    </div>
                                                                </div>
                                                                <div></div>
                                                                <div></div>
                                                                <div class="col-lg-3 col-md-3 col-sm-3 col-12">
                                                                    <div class="form-group" style="text-align: right;">
                                                                        <label for="FilterData"></label>
                                                                        <input type="text" id="FilterData" class="form-control sfilter" placeholder="Search" />
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <asp:ListView ID="lvBulkDetail" runat="server" OnPagePropertiesChanging="lvBulkDetail_PagePropertiesChanging">
                                                                <LayoutTemplate>
                                                                    <%--<div class="sub-heading">
                                                                        <h5>Details</h5>
                                                                    </div>--%>
                                                                    <div class="table-responsive" style="height: 400px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap stble" id="tbltest">
                                                                            <thead class="bg-light-blue" style="vertical-align: top">
                                                                                <tr id="trRow">
                                                                                    <th id="thHead">
                                                                                        <asp:CheckBox ID="cbHead" runat="server" Text="Select All" TabIndex="9" OnClick="checkBulkAllCheckbox(this)" />
                                                                                    </th>
                                                                                    <th data-sortable="true">User Name <span class="sort-icon">&#8593;</span></th>
                                                                                    <th data-sortable="true">Full Name <span class="sort-icon">&#8593;</span></th>
                                                                                    <th data-sortable="true">Role <span class="sort-icon">&#8593;</span></th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody class="stbody">
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>

                                                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    
                                                                                </ContentTemplate>
                                                                                <Triggers>
                                                                                    <asp:AsyncPostBackTrigger ControlID="chkuano" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>--%>

                                                                            <asp:HiddenField ID="hfUserNo" runat="server" Value='<%# Eval("UA_NO") %>' />
                                                                            <%--OnCheckedChanged="chkuano_CheckedChanged" AutoPostBack="true"--%>
                                                                            <asp:CheckBox ID="chkuano" TabIndex="10" runat="server" ToolTip='<%# Eval("UA_NO") %>' OnClick="restrictListViewCheckboxSelection(this);" />
                                                                            <%--<asp:Button ID="btnAssignLinks" runat="server" ToolTip='<%# Eval("UA_NO") %>' OnClick="btn_Click" Height="0" Width="0" CssClass="hidden" />--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NAME")%>' ToolTip='<%# Eval("UA_NO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblname" runat="server" Text='<%# Eval("UA_FULLNAME")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("ROLE_NAME")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                            
                                                           <%-- DataPager Added By Shrikant W. on 07-09-2023--%>
                                                            <div style="text-align: left; margin-top: 0px;">
                                                                <asp:DataPager ID="DataPager2" runat="server" PagedControlID="lvBulkDetail" PageSize="200">
                                                                    <Fields>
                                                                        <asp:TemplatePagerField>
                                                                            <PagerTemplate>
                                                                                <b>Showing
                                                                <asp:Label runat="server" ID="CurrentPageLabel"
                                                                    Text="<%# Container.StartRowIndex+1 %>" />
                                                                                    to
                                                                <asp:Label runat="server" ID="TotalPagesLabel"
                                                                    Text="<%# Convert.ToInt32(Container.StartRowIndex + Container.PageSize) > Convert.ToInt32(Container.TotalRowCount) ? Convert.ToInt32(Container.TotalRowCount):Convert.ToInt32(Container.StartRowIndex+ Container.PageSize) %>" />
                                                                                    ( of
                                                                <asp:Label runat="server" ID="TotalItemsLabel"
                                                                    Text="<%# Container.TotalRowCount%>" />
                                                                                    records)
                                                                <br />
                                                                                </b>
                                                                            </PagerTemplate>
                                                                        </asp:TemplatePagerField>
                                                                    </Fields>
                                                                </asp:DataPager>
                                                            </div>
                                                            <div style="text-align: right; margin-top: 0px;">
                                                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvBulkDetail" PageSize="1000">
                                                                    <Fields>
                                                                        <asp:NumericPagerField />
                                                                    </Fields>
                                                                </asp:DataPager>
                                                            </div>
                                                        </asp:Panel>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- added by Kajal Jaiswal--%>

    <script type="text/javascript" language="javascript">

        var isSingleCheck = false;
        function ResetCheckBoxValue() {
            isSingleCheck = false;
            $('#ctl00_ContentPlaceHolder1_cbAllowSingleSelection').prop('checked', false);

            $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").removeAttr("disabled");
            $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").attr("enabled", "enabled");
        }
        function setListViewCheckBoxHeader() {
            var checked = $("#ctl00_ContentPlaceHolder1_cbAllowSingleSelection").is(':checked');
            if (checked) {
                $('#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead').prop('checked', false);
                $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").attr("disabled", "disabled");
            }

        }

        function setCheckSingleCB(chk) {

            if (chk.checked == true) {

                $('#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead').prop('checked', false);
                $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").attr("disabled", "disabled");

                isSingleCheck = true;

            } else {

                $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").removeAttr("disabled");
                $("#ctl00_ContentPlaceHolder1_lvBulkDetail_cbHead").attr("enabled", "enabled");


                //$('#ctl00_ContentPlaceHolder1_chkListRole').prop('checked', false);
                //$("[id*=ctl00_ContentPlaceHolder1_chkListRole] input").removeAttr("checked");

                var list = $('#<%=chkListRole.ClientID%> input');//#ctl00_ContentPlaceHolder1_chkListRole           
                list.each(function (index) {
                    item = $(this);
                    item.attr('checked', false);
                });

                /*var chkBoxList = document.getElementById("<=chkListRole.ClientID>");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }*/

                isSingleCheck = false;

            }

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    e.checked = false;
                }
            }

            /*if (isSingleCheck == false) {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    var s = e.name.split("ctl00$ContentPlaceHolder1$chkListRole$ctrl");
                    var b = 'ctl00$ContentPlaceHolder1$chkListRole$ctrl';
                    var g = b + s[1];
                    if (e.name == g) {
                        e.checked = false;
                    } ctl00_ContentPlaceHolder1_chkListRole
                    ctl00_ContentPlaceHolder1_chkListRole_0
                }
            }*/


        }

        function callWebMethod(user_no) {

            //alert('hello Sir...');
            //var formData = { UserNo: user_no };
            //alert(JSON.stringify(formData));
            var list = $('#<%=chkListRole.ClientID%> input');//#ctl00_ContentPlaceHolder1_chkListRole           
            list.each(function (index) {
                item = $(this);
                item.attr('checked', false);
            });

            $.ajax({
                type: "POST",
                //url: "homeFaculty.aspx/ShowEmpTasks",                
                ////url: "http://localhost:55403/PresentationLayer/UserRolemaster_LinkAssign.aspx/getUserRoles",
                url: '<%= ResolveUrl("UserRolemaster_LinkAssign.aspx/SetUserAccessLinks") %>',
                data: "{UserNo:'" + user_no + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                ////success: OnSuccessEmpTask,
                success: function (d) {
                    var data = JSON.parse(d.d);
                    ////alert(d.d.UA_ROLENO);
                    var strRoleNo = data.UA_ROLENO;
                    ////alert(strRoleNo);
                    var list = $('#<%=chkListRole.ClientID%> input');//#ctl00_ContentPlaceHolder1_chkListRole
                    ////alert(list);
                    var rolesArr = strRoleNo.split(',');
                    /*list.each(function (index) {
                        item = $(this);
                        item.attr('checked', false);
                    });*/

                    list.each(function (index) {
                        item = $(this);
                        //alert(item.val());
                        for (i = 0; i < rolesArr.length; i++) {
                            //if (strRoleNo.indexOf(item.val()) != -1) {
                            //alert(rolesArr[i] + '---' + item.val());
                            if (rolesArr[i] == item.val()) {
                                ////alert(item.val());
                                ////alert(isSingleCheck);
                                item.attr('checked', true);
                                ////yourCheckBoxVariable.click();
                                ////item.setAttribute("checked", "checked");
                                ////item.checked = true;
                            }
                        }
                    });

                },
                failure: function (response) {
                    alert('failure');
                },
                error: function (response) {
                    //debugger
                    alert("error");
                    alert(response.responseText);
                }
            });
            function OnSuccessEmpTask(response) {
                alert('hi:' + json.stringify(response));
                //success block
                var data = JSON.parse(response.d);
            };
        }

        function restrictListViewCheckboxSelection(headchk) {

            var row = headchk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;//get current row index                      
            var row;
            row = $(headchk).parent().parent();
            ////getIds(row);
            var row_parent_Object = row[0];
            var rowNumber_obj = row_parent_Object._DT_CellIndex;
            var rownumber = rowNumber_obj.row;
            ////alert(rownumber);
            var isChecked = false;

            if (isSingleCheck == true) {
                var frm = document.forms[0]
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    var s = e.name.split("ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl");
                    var b = 'ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl';
                    var g = b + s[1];
                    if (e.name == g) {
                        if (headchk.checked == true && headchk == e) {
                            e.checked = true;

                            //alert(row);, row
                            var us_No = $("#ctl00_ContentPlaceHolder1_lvBulkDetail_ctrl" + rownumber + "_hfUserNo").val();
                            //alert(us_No);
                            callWebMethod(us_No);


                            ////document.getElementById("ctl00_ContentPlaceHolder1_lvBulkDetail_ctrl" + rownumber + "_btnAssignLinks").click();
                            /////__doPostBack(headchk, '');
                        } else {
                            e.checked = false;
                            ////return false;
                        }
                    }
                }
            }
        }

        function checkBulkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvBulkDetail$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>


    <script language="javascript" type="text/javascript">    //Added by Sachin Lohakare 08-12-2022 
        function OnTreeClick(evt) {

            var src = window.event != window.undefined ? window.event.srcElement : evt.target;

            var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
            if (isChkBoxClick) {
                //alert("src");
                var parentTable = GetParentByTagName("table", src);
                var nxtSibling = parentTable.nextSibling;
                if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
                {
                    if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                    {
                        //check or uncheck children at all levels
                        CheckUncheckChildren(parentTable.nextSibling, src.checked);
                    }
                }
                //check or uncheck parents at all levels
                CheckUncheckParents(src, src.checked);
            }
        }

        function CheckUncheckChildren(childContainer, check) {
            var childChkBoxes = childContainer.getElementsByTagName("input");
            var childChkBoxCount = childChkBoxes.length;
            for (var i = 0; i < childChkBoxCount; i++) {
                childChkBoxes[i].checked = check;
            }
        }

        function CheckUncheckParents(srcChild, check) {
            var parentDiv = GetParentByTagName("div", srcChild);
            var parentNodeTable = parentDiv.previousSibling;

            if (parentNodeTable) {
                var checkUncheckSwitch;

                if (check) //checkbox checked
                {
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);

                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        return; //do not need to check parent if any(one or more) child not checked
                }
                else //checkbox unchecked
                {
                    //checkUncheckSwitch = false;

                    //# Prashant
                    var isAllSiblingsChecked = AreAllSiblingsChecked(srcChild);

                    if (isAllSiblingsChecked)
                        checkUncheckSwitch = true;
                    else
                        checkUncheckSwitch = false;

                }

                var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
                if (inpElemsInParentTable.length > 0) {
                    var parentNodeChkBox = inpElemsInParentTable[0];
                    parentNodeChkBox.checked = checkUncheckSwitch;
                    //do the same recursively
                    CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
                }
            }
        }

        function AreAllSiblingsChecked(chkBox) {
            var parentDiv = GetParentByTagName("div", chkBox);
            var childCount = parentDiv.childNodes.length;
            for (var i = 0; i < childCount; i++) {
                if (parentDiv.childNodes[i].nodeType == 1) //check if the child node is an element node
                {
                    if (parentDiv.childNodes[i].tagName.toLowerCase() == "table") {
                        var prevChkBox = parentDiv.childNodes[i].getElementsByTagName("input")[0];
                        //if any of sibling nodes are not checked, return false
                        if (prevChkBox.checked) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        //utility function to get the container of an element by tagname
        function GetParentByTagName(parentTagName, childElementObj) {
            var parent = childElementObj.parentNode;
            while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
                parent = parent.parentNode;
            }
            return parent;
        }
    </script>



    <script language="javascript" type="text/javascript">

        // Called via a startup script created in Code Behind.
        // Disables all treeview checkboxes that have a text with a class=disabledTreeviewNode.
        // treeviewID is the ClientID of the treeView
        function DisableCheckBoxes() {

            //, select, button, textarea
            $("#ctl00_ContentPlaceHolder1_tvOriginalLinks").find("input, a").prop("disabled", true);//.removeAttr('href').removeAttr('onclick');
            ////var node = $('.treeNode:eq(0)'); //get the node element
            ////var nodeLink = $('input', node).attr("disabled", "disabled");

            //TREEVIEW_ID = treeviewID;

            /*var treeView = document.getElementById("#ctl00_ContentPlaceHolder1_tvOriginalLinks");
            //.attr("disabled", "disabled");
            if (treeView) {
                alert('Hi');
                var childCheckBoxes = treeView.getElementsByTagName("input");
                for (var i = 0; i < childCheckBoxes.length; i++) {
                    var textSpan = GetCheckBoxTextSpan(childCheckBoxes[i]);

                    if (textSpan.firstChild)
                        if (textSpan.firstChild.className == "disabledTreeviewNode")
                            childCheckBoxes[i].disabled = true;
                }
            }*/
        }

        function GetCheckBoxTextSpan(checkBox) {
            // Set label text to node name
            var parentDiv = checkBox.parentNode;
            var nodeSpan = parentDiv.getElementsByTagName("span");

            return nodeSpan[0];
        }
    </script>


    <script>

        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            if (Page_ClientValidate('submit')) {

                $('#hfdStat').val($('#rdActive').prop('checked'));

                //alert('hi');
                var theString = $("#ctl00_ContentPlaceHolder1_txtUserRole").val();//document.getElementById('#txtUserRole').value;
                // Match a string that ends with abc, similar to LIKE '%abc'
                //if (theString.match(/^.*abc$/)) {
                /*Match found */
                //}

                // Match a string that starts with abc, similar to LIKE 'abc%'
                if (theString.trim().toLowerCase().match(/^default.*$/)) {
                    //alert(theString);
                    alert('You are not allow to generate or modify default roles.');
                    return false;
                }

                //var idtxtOwnershipStatusName = $("[id$=txtOwnershipStatusName]").attr("id");
                //var txtOwnershipStatusName = document.getElementById(idtxtOwnershipStatusName);
                //if (txtOwnershipStatusName.value.length == 0) {
                //    alert('Please Enter Ownership Status Name', 'Warning!');
                //    $(txtOwnershipStatusName).focus();
                //    return false;
                //}
            }
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {

            $(function () {

                $('#btnSubmit').click(function () {
                    alert('a');
                    validate();
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

   <%-- Added By Shrikant W. on 08-09-2023 for Searching and Sorting the Items in the ListView of Bulk Link Assign Department Wise Tab --%>
    <script>
        var currentSortColumn = null;
        var isAscending = true;

        function sortListView() {
            var $table = $('.stbody');
            var rows = $table.find('tr').get();
            var columnIndex = $(this).index();

            if (currentSortColumn === columnIndex) {
                isAscending = !isAscending;
            } else {
                isAscending = true;
            }

            currentSortColumn = columnIndex;

            rows.sort(function (a, b) {
                var keyA = $(a).find('td').eq(columnIndex).text().toLowerCase();
                var keyB = $(b).find('td').eq(columnIndex).text().toLowerCase();

                if (isAscending) {
                    return keyA.localeCompare(keyB);
                } else {
                    return keyB.localeCompare(keyA);
                }
            });

            $.each(rows, function (index, row) {
                $table.append(row);
            });
        }

        function filterListView() {
            var searchText = $('#FilterData').val().toLowerCase();
            $('.stbody tr').each(function () {
                var rowText = $(this).text().toLowerCase();
                if (rowText.includes(searchText)) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        }

        $(document).on('keyup', '#FilterData', function () {
            filterListView();
            sortListView();
        });

        $(document).on('click', 'th[data-sortable="true"]', function () {
            sortListView();
            filterListView();
        });


        $(document).ready(function () {
            sortListView();
            filterListView();
        });
    </script>




</asp:Content>

