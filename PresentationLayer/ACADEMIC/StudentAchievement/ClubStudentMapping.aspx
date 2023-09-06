<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" Culture="en-GB" CodeFile="ClubStudentMapping.aspx.cs" Inherits="ACADEMIC_StudentAchievement_ClubStudentMapping" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        $(document).ready(function () {
            var table = $('#tblClubMapping').DataTable({
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
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblClubMapping').DataTable().column(idx).visible();
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
                return $('#tblClubMapping').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
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
            var arr = [0, 9];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#tblClubMapping').DataTable().column(idx).visible();
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
                        nodereturn += $(this).html();
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
                var table = $('#tblClubMapping').DataTable({
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
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblClubMapping').DataTable().column(idx).visible();
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
                    return $('#tblClubMapping').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
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
                var arr = [0, 9];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#tblClubMapping').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
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
    <script src="https://cdn.datatables.net/1.10.4/js/jquery.dataTables.min.js"></script>

    <script src="jquery/jquery-1.10.2.js"></script>
    <script src="jquery/jquery-1.10.2.min.js"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        input[type=checkbox], input[type=radio] {
            margin: 0px 3px 0;
        }

        .form-control {
        }
    </style>
    <asp:UpdatePanel ID="updSession" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <%--OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            ErrorMessage="Please Select College." Display="None" SetFocusOnError="true" ValidationGroup="ClubMapping" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            ErrorMessage="Please Select College." Display="None" SetFocusOnError="true" ValidationGroup="ClubMapping" InitialValue="Please Select"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:ListBox ID="lstDegree" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstDegree_SelectedIndexChanged"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstDegree" ValidationGroup="ClubMapping" ControlToValidate="lstDegree" runat="server"
                                            Display="None" ErrorMessage="Please select Degree"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:ListBox ID="lstBranch" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="rfvlstBranch" ValidationGroup="ClubMapping" ControlToValidate="lstBranch" runat="server"
                                            Display="None" ErrorMessage="Please select Branch"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="ClubMapping" CssClass="btn btn-outline-info" OnClick="btnShow_Click" />
                                <%--OnClick="btnShow_Click"--%>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <%--OnClick="btnCancel_Click"--%>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="ClubMapping" />

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <label>Club</label>
                                </div>
                                <asp:DropDownList ID="ddlClub" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                    AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                    <%--OnSelectedIndexChanged="ddlClub_SelectedIndexChanged"--%>
                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlClub" runat="server" ControlToValidate="ddlClub"
                                    ErrorMessage="Please Select Club." Display="None" SetFocusOnError="true" ValidationGroup="ClubMappingSub" InitialValue="0"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlClub"
                                    ErrorMessage="Please Select Club." Display="None" SetFocusOnError="true" ValidationGroup="ClubMappingSub" InitialValue="Please Select"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="sub-heading">
                                    <h5>Club Student Mapping List</h5>
                                </div>
                                <asp:Panel ID="pnlClubMapping" runat="server" Visible="false">
                                    <asp:ListView ID="lvClubMapping" runat="server">
                                        <%--ItemPlaceholderID="itemPlaceholder" OnItemEditing="lvClubMapping_ItemEditing"--%>
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblClubMapping">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="width: 5%;"></th>
                                                        <th style="width: 10%;">RegNo</th>
                                                        <th style="width: 20%;">Student Name</th>
                                                        <th style="width: 10%;">Mobile No</th>
                                                        <th style="width: 10%;">Email</th>
                                                        <th style="width: 10%;">Degree-Branch</th>
                                                        <th style="width: 10%;">Club Name</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CssClass="fas fa-edit" CausesValidation="false" CommandArgument='<%#Eval("CLUB_ACTIVITY_NO") %>' CommandName="Edit"></asp:LinkButton>
                                                    <%--OnClick="btnEditCreateEvent_Click"
                                                </td>--%>
                                                <td style="text-align:center">
                                                    <asp:HiddenField ID="hfdIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    <asp:CheckBox ID="chkIsActive" runat="server" CssClass="chkbox_addsubject"/>
                                                </td>

                                                <td><%# Eval("REGNO1") %></td>
                                                <td><%# Eval("STUDNAME") %></td>
                                                <td><%# Eval("STUDENTMOBILE") %></td>
                                                <td><%# Eval("EMAILID") %></td>
                                                <td><%# Eval("DEGREEBRANCH") %></td>
                                                <td> 
                                                    <%--<%# Eval("CLUB_ACTIVITY_TYPE") %>--%>
                                                    <%--Eval("CLUB_ACTIVITY_NO")  Eval("CLUB_ACTIVITY_TYPE")--%>
                                                    <asp:HiddenField ID="hfdClubActivityNo" runat="server" Value='<%# (Eval("CLUB_ACTIVITY_NO") == null ? "0" : Eval("CLUB_ACTIVITY_NO"))  %>'/>
                                                    <asp:Label ID="lblClubActivityType" runat="server" Text='<%# (Eval("CLUB_ACTIVITY_TYPE") == null ? "" : Eval("CLUB_ACTIVITY_TYPE"))  %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" ValidationGroup="ClubMappingSub" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                    <%--OnClientClick="return valiCreateEvent();" OnClick="btnSubmitCreateEvent_Click"--%>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="false" ValidationGroup="ClubMappingSub" />
                                    <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-info" CausesValidation="false" OnClick="btnReport_Click">Excel Report</asp:LinkButton>
                                    <%--OnClick="btnReport_Click"--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCollege" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lstDegree" EventName="SelectedIndexChanged" />
            
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

        <script type="text/javascript">
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200
                });
            });

        </script>
</asp:Content>
