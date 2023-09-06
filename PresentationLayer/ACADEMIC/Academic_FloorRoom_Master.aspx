<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Academic_FloorRoom_Master.aspx.cs" Inherits="ACADEMIC_Academic_FloorRoom_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <%--<script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();

        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });

        }
    </script>--%>
    <%-- <script src="../../Content/jquery.js" type="text/javascript"></script>
    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>
    <%-- <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
    <%--<style>
        #ctl00_ContentPlaceHolder1_pnlConfigRef .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>


    <%--===== Data Table Script added by gaurav =====--%>
   <%-- <script>
        $(document).ready(function () {
            var table = $('#tab-le').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

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
                                return $('#tab-le').DataTable().column(idx).visible();
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
                                                return $('#tab-le').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#tab-le').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tab-le').DataTable().column(idx).visible();
                                            }
                                        }
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
                var table = $('#tab-le').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

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
                                    return $('#tab-le').DataTable().column(idx).visible();
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#tab-le').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tab-le').DataTable().column(idx).visible();
                                                }
                                            }
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
            var table = $('#table_config').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

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
                                return $('#table_config').DataTable().column(idx).visible();
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
                                                return $('#table_config').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#table_config').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#table_config').DataTable().column(idx).visible();
                                            }
                                        }
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
                var table = $('#table_config').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

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
                                    return $('#table_config').DataTable().column(idx).visible();
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
                                                    return $('#table_config').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#table_config').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#table_config').DataTable().column(idx).visible();
                                                }
                                            }
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
            var table = $('#tab_uni').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,

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
                                return $('#tab_uni').DataTable().column(idx).visible();
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
                                                return $('#tab_uni').DataTable().column(idx).visible();
                                            }
                                        }
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
                                                return $('#tab_uni').DataTable().column(idx).visible();
                                            }
                                        }
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tab_uni').DataTable().column(idx).visible();
                                            }
                                        }
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
                var table = $('#tab_uni').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

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
                                    return $('#tab_uni').DataTable().column(idx).visible();
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
                                                    return $('#tab_uni').DataTable().column(idx).visible();
                                                }
                                            }
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
                                                    return $('#tab_uni').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#tab_uni').DataTable().column(idx).visible();
                                                }
                                            }
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

--%>


    <asp:UpdatePanel runat="server" ID="UpdRole"   UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Floor Master</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">Room Master</a>
                                    </li>
                                    
                                </ul>
                                <div class="box-tools pull-right">
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                </div>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdproOwner" runat="server" AssociatedUpdatePanelID="upfloor"
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
                                        <asp:UpdatePanel ID="upfloor" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <%--<div class="box-header with-border">
                                                                <h3 class="box-title" style="margin-left: 15px">Ownership Status</h3>
                                                            </div>--%>

                                                        <div id="divMsg" runat="server">
                                                        </div>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-md-3">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Floor Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtFloorName"  runat="server" TabIndex="3" MaxLength="50"
                                                                            ToolTip="Please Enter Floor Name" placeholder="Enter Floor Name" />
                                                                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Floor Name" ControlToValidate="txtFloorName"
                                            Display="None" ValidationGroup="submit" />
                                                                        <%--onkeyup="return ValidateTextbox(this);"--%>
                                                                      <%--  <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtFloorName" />--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Active Status</label>
                                                                        </div>
                                                                         <asp:CheckBox ID="chkActive" runat="server"  ForeColor="#005500" TabIndex="4" onclick="return show(chkActive);" Checked="true"/>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="box-footer">
                                                            <p class="text-center">
                                                                <asp:Button ID="btnSave" runat="server" Text="Submit" 
                                                                    CssClass="btn btn-primary"  TabIndex="5" OnClick="btnSave_Click" ValidationGroup="submit" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" 
                                                                    CssClass="btn btn-warning"  TabIndex="6" OnClick="btnCancel_Click" />
                                                                   <asp:ValidationSummary ID="valfloorsummary" runat="server" DisplayMode="List"
                                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                                            </p>

                                                            <div class="col-12">
                                                                <asp:Panel ID="Pnlfloor" runat="server">
                                                                    <asp:ListView ID="lvfloor" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Floor Master List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Edit
                                                                                        </th>
                                                                                        <th>Floor Name
                                                                                        </th>
                                                                                        <th>Active Status
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
                                                                                <td style="text-align: center;">
                                                                                      <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("FLOORNO")%>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                                                  <%--  <asp:ImageButton ID="btnEdit"  runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("FLOORNO") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" TabIndex="7" OnClick="btnEdit_Click1"/>--%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("FLOORNAME")%>
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("ACTIVESTATUS")%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <%--  <Triggers>
                                                  <asp:PostBackTrigger ControlID="btnSave" />
                                             </Triggers>--%>
                                        </asp:UpdatePanel>
                                    </div>
                                
                                    <div class="tab-pane" id="tab2">
                                        <div>
                                            <asp:UpdateProgress ID="updProgAff" runat="server" AssociatedUpdatePanelID="updROOM"
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
                                        <asp:HiddenField ID="hfdStatAff" runat="server" ClientIDMode="Static" />
                                        <asp:UpdatePanel ID="updROOM" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div id="div1" runat="server"></div>
                                                        <%--<div class="box-header with-border">
                                                                <h3 class="box-title" style="margin-left: 15px">Define Affiliation Type</h3>
                                                            </div>--%>
                                                        <div class="box-body">
                                                            <div id="div2" runat="server">
                                                            </div>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Room Name</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRoomName" AutoComplete="off" placeholder="Enter Room Name" runat="server" TabIndex="1" MaxLength="50"
                                                                            ToolTip="Please Enter Room Name." />
                                                                           <asp:RequiredFieldValidator ID="rfvrrom" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Room Name" ControlToValidate="txtRoomName"
                                            Display="None" ValidationGroup="submitroom" />
                                                                        <%--onkeyup="return ValidateTextbox(this);"--%>
                                                                        <%--   <asp:RequiredFieldValidator ID="rfvExamName" runat="server" ControlToValidate="txtAffilationName"
                                                            Display="None" ErrorMessage="Please Enter Affilation Type Name." ValidationGroup="submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

<%--                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltAffiliation" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtRoomName" />--%>
                                                                    </div>
                                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Floor Name </label>
                                              <%--  <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </div>
                                            <asp:DropDownList ID="ddlfloor" runat="server" AppendDataBoundItems="true"  TabIndex="2"
                                                CssClass="form-control" data-select2-enable="true" ToolTip="Please Select Floor Name">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                           <asp:RequiredFieldValidator ID="rfvddlfloor" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Select Floor Name" InitialValue="0" ControlToValidate="ddlfloor"
                                            Display="None" ValidationGroup="submitroom" />
                                        </div>
                                                                      <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Room Intake</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtintake" AutoComplete="off" placeholder="Enter Inatke" runat="server" TabIndex="3" MaxLength="50"
                                                                            ToolTip="Please Enter Inatke." onkeyup="IsNumeric(this)" />

                                                                        <asp:RequiredFieldValidator ID="rfvintake" runat="server" SetFocusOnError="True"
                                            ErrorMessage="Please Enter Intake" ControlToValidate="txtintake"
                                            Display="None" ValidationGroup="submitroom" />

<%--                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtRoomName" />--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label> Active Status</label>
                                                                        </div>
                                                                             <%-- <asp:CheckBox ID="chkActive" runat="server" TabIndex="3" Checked="true"/>--%>
                                            <asp:CheckBox ID="chkroom" runat="server"  ForeColor="#005500" TabIndex="4" Checked="true"/>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnSaveRoom" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submitroom" 
                                                                    CssClass="btn btn-primary" OnClick="btnSaveRoom_Click"  TabIndex="5" />
                                                                <asp:Button ID="btnCancelRoom" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-warning" OnClick="btnCancelRoom_Click" TabIndex="6" />
                                                                        <asp:ValidationSummary ID="valsummaryroom" runat="server" ValidationGroup="submitroom"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            </div>

                                                            <div class="col-12">
                                                                <asp:Panel ID="Pnlroom" runat="server">
                                                                    <asp:ListView ID="lvroomintake" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Room Master List</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100% !important" id="tab-le">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr>
                                                                                        <th style="text-align: center;">Edit
                                                                                        </th>
                                                                                        <th>Room Name
                                                                                        </th>
                                                                                        <th>Room Intake
                                                                                        </th>
                                                                                        <th>FloorName
                                                                                        </th>
                                                                                        <th>
                                                                                            Active Status
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
                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btneditRoomIntake" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("ROOMNO") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btneditRoomIntake_Click"  TabIndex="7" CausesValidation="false" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("ROOMNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                   <%# Eval("INTAKE")%> 
                                                                                </td>
                                                                                <td>
                                                                                      <%# Eval("FLOORNAME")%> 
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("ACTIVE_STATUS")%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            <%--    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />--%>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnSaveAffilation" />--%>
                                                <%--<asp:PostBackTrigger ControlID="btnCancelAffilation" />--%>
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
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                    alert("Please Enter Numeric value only");
                }
            }
        }

    </script>
       </asp:Content>
    

