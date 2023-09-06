<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="UniversityMaster.aspx.cs"
    Inherits="RFC_CONFIG_Masters_UniversityMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus
        {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <%--    <script type="text/javascript">
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
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>
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

    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:UpdatePanel ID="updBatch" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="margin-left: 15px">Define University</h3>
                        </div>
                        <div id="divMsg" runat="server">
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>University Name</label>
                                        </div>
                                        <asp:TextBox ID="txtUniversityName" runat="server" CssClass="form-control" TabIndex="1" onkeyup="return ValidTextbox();"
                                            ToolTip="Please Enter University Name" AutoComplete="OFF" placeholder="University Name" ValidationGroup="submit" />
                                        <asp:RequiredFieldValidator ID="rfvUniversityname" runat="server" ControlToValidate="txtUniversityName"
                                            Display="None" ErrorMessage="Please Enter University Name." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>State</label>
                                        </div>
                                        <asp:DropDownList ID="ddlState" runat="server" AppendDataBoundItems="true" CssClass="form-control" ValidationGroup="submit"
                                            TabIndex="2" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Please Select State"
                                            ControlToValidate="ddlState" ValidationGroup="submit" InitialValue="0" Display="None">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status</label>
                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActive" name="switch" checked />
                                            <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="col-12 btn-footer">
                            <%--<p class="text-center">--%>
                            <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                CssClass="btn btn-primary" TabIndex="4" OnClick="btnSave_Click" OnClientClick="return validate();" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                CssClass="btn btn-warning" TabIndex="5" OnClick="btnCancel_Click" />

                            <asp:ValidationSummary ID="vlsummary" runat="server" ValidationGroup="submit"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <%-- </p>--%>
                        </div>
                        <div class="col-md-12">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvUniversity" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>University List </h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tab-le">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">Edit
                                                    </th>
                                                    <th>University Name
                                                    </th>
                                                    <th>State
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
                                            <td style="text-align: center;">
                                                <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("UNIVERSITYID") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" TabIndex="6" />
                                            </td>
                                            <td>
                                                <%# Eval("UNIVERSITYNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("STATENAME")%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblActive" Text='<%# Eval("IsActive")%>' ForeColor='<%# Eval("IsActive").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>



    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var idUN = $("[id$=txtUniversityName]").attr("id");
            var txtUniversityName = document.getElementById(idUN);
            if (txtUniversityName.value.length == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Enter University Name.', 'Warning!');
                $(txtUniversityName).focus();
                return false;
            }
            var idddlOrgMap = $("[id$=ddlState]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select State.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    validate();
                });
            });
        });
    </script>

    <script type="text/javascript">
        function ValidTextbox() {
            var charactersOnly = document.getElementById('ctl00_ContentPlaceHolder1_txtUniversityName').value;
            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById('ctl00_ContentPlaceHolder1_txtUniversityName').value = "";
                return false;
            }
        }
    </script>
</asp:Content>


