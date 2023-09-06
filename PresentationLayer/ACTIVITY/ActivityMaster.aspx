<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ActivityMaster.aspx.cs" Inherits="Activity_ActivityMaster" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updActivity"
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
            var table = $('#tab-le').DataTable({
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

    <asp:HiddenField ID="hfdActive" runat="server" ClientIDMode="Static" />
    <%-- <asp:UpdatePanel ID="updActivity" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Add/Edit Activity Master</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">

                    <div class="col-12 mb-2">
                        <%-- <div class="sub-heading">
                                    <h5>Add/Edit Activity Master</h5>
                                </div>--%>
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Activity Code</label>
                                </div>
                                <asp:TextBox ID="txtActivityCode" runat="server" TabIndex="1" MaxLength="10" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="valCode" runat="server" ControlToValidate="txtActivityCode"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Activity Code."
                                    SetFocusOnError="true" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtActivityCode"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Activity Name</label>
                                </div>
                                <asp:TextBox ID="txtActivity" runat="server" TabIndex="2" MaxLength="200" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="valActivity" runat="server" ControlToValidate="txtActivity"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Activity Name."
                                    SetFocusOnError="true" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtActivity"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>

                                   <%-- ddlassign added on 28-2-23 by Injamam--%>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                     <sup>* </sup>
                                    <label>Assign To</label>
                                </div>
                                <asp:DropDownList ID="ddlassign" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="3"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged">
                                    <asp:ListItem Value="">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Academic</asp:ListItem>
                                    <asp:ListItem Value="1">Examination</asp:ListItem>
                                </asp:DropDownList>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server" ErrorMessage="Please Select Assign to" ControlToValidate="ddlassign"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <asp:UpdatePanel ID="updActivity" runat="server">
                                    <ContentTemplate>
                                        <%-- <UpdatePanel id="updActivity" runat="server">
                                        <ContentTemplate>--%>
                                        <div class="col-12 pl-0 pr-0">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Exam Pattern</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamPattern" runat="server" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="3"
                                                        CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged" AutoPostBack="True">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Exam No.</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamNo" runat="server" CssClass="form-control" TabIndex="3" AppendDataBoundItems="True" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlExamNo_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Sub-Exam No.</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSubExamNo" runat="server" CssClass="form-control" TabIndex="4" AppendDataBoundItems="True" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <%-- </ContentTemplate>
                                    </UpdatePanel>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12 h-100">
                                <div class="label-dynamic">
                                    <label>User Type</label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:CheckBoxList ID="chkListUserTypes" runat="server" RepeatColumns="2" TabIndex="5"
                                        RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12 h-100">
                                <div class="label-dynamic">
                                    <label>Page Link</label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:Panel ID="pnl" runat="server">
                                        <asp:CheckBoxList ID="chkPageLink" runat="server" TabIndex="6" RepeatColumns="2"
                                            RepeatDirection="Horizontal"
                                            Width="100%" CssClass="checkbox-list-style">
                                        </asp:CheckBoxList>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12 h-100">
                                <div class="label-dynamic">
                                    <label>PreRequiste Activity</label>
                                </div>
                                <div class="form-group col-md-12 checkbox-list-box">
                                    <asp:CheckBoxList ID="chkPreActNo" runat="server" TabIndex="7" RepeatColumns="2"
                                        RepeatDirection="Horizontal" Width="100%" CssClass="checkbox-list-style">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Activity Details</label>
                                </div>
                                <asp:TextBox ID="txtActivityTemplate" runat="server" TabIndex="8" MaxLength="10" CssClass="form-control" TextMode="MultiLine" />
                                <asp:RequiredFieldValidator ID="rfvActivityTemplate" runat="server" ControlToValidate="txtActivityTemplate"
                                    ValidationGroup="Submit" Display="None" ErrorMessage="Please Enter Activity Details."
                                    SetFocusOnError="true" />
                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtActivityTemplate"
                                    FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*+|=\{}[]:;<>">
                                </ajaxToolKit:FilteredTextBoxExtender>
                            </div>
                            <div class="col-lg-4 col-md-6 col-12">
                                <div class="row">
                                    <div class="form-group col-6">
                                        <div class="label-dynamic">
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                            OnClick="btnSubmit_Click" TabIndex="9" CssClass="btn btn-primary" OnClientClick="return validate();" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="10" OnClick="btnCancel_Click" 
                            CssClass="btn btn-warning" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Submit" />
                    </div>
                    <div class="col-12 mt-3">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvActivities" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Activities</h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tab-le">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Edit
                                                </th>
                                                <th>Activity Code
                                                </th>
                                                <th>Activity
                                                </th> 
                                                <th>Assign To  <%--//Added by Injamam 28-2-23--%>
                                                </th>
                                                <th>Exam Pattern
                                                </th>
                                                <th>Exam Name
                                                </th>
                                                <th>Sub Exam Name
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
                                            <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/Images/edit.png"
                                                CommandArgument='<%# Eval("ACTIVITY_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                OnClick="btnEdit_Click" TabIndex="10" />
                                        </td>
                                        <td>
                                            <%# Eval("ACTIVITY_CODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("ACTIVITY_NAME")%>
                                        </td>
                                        <td>     <%--//Added by Injamam 28-2-23--%>
                                            <%--<asp:Label ID="Label1" Text='<%#Eval("ASSIGN_TO")%>' runat="server"></asp:Label>--%>
                                            <%# Eval("ASSIGN_TO")%>
                                        </td>
                                        <td>
                                            <%# Eval("PATTERN_NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("EXAMNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SUBEXAMNAME")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblActive" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdActive').val($('#rdActive').prop('checked'));
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>
</asp:Content>
