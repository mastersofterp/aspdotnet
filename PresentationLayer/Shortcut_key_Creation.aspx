<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Shortcut_key_Creation.aspx.cs" Inherits="Shortcut_key_Creation" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Content/jquery.js"></script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updlink"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>

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
    <asp:UpdatePanel ID="updlink" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Shortcut Key Creation For Link</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--  <div class="pull-right" id="markfield" visible="false">
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                            </div>--%>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <div class="form-group col-md-12">
                                    <asp:Panel ID="pnlAdd" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label>Link Title</label>
                                                <asp:TextBox ID="txtLinkTitle" runat="server" CssClass="form-control" MaxLength="50" />
                                                <%-- <asp:RequiredFieldValidator ID="rfvLinkTitle" runat="server" ControlToValidate="txtLinkTitle"
                                        Display="None" ErrorMessage="Link Title Required" ValidationGroup="submit" ></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label>Link URL </label>
                                                <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="form-control" MaxLength="150" />

                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <label><span style="color: red">*</span>Shortcut Key</label>
                                                <asp:TextBox ID="txtShortcutkey" runat="server" CssClass="form-control" onkeypress="return validchar();" />
                                                <asp:RequiredFieldValidator ID="rfvShortcutkey" runat="server" ControlToValidate="txtShortcutkey"
                                                    Display="None" ErrorMessage="Please Enter Shortcut Key." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>

                                        <div class="box-footer col-md-12" id="btnid" visible="false">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="submit" />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    CausesValidation="False" CssClass="btn btn-danger" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ValidationGroup="submit"
                                                    ShowMessageBox="True" ShowSummary="False" />
                                            </p>
                                        </div>

                                        <div>
                                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlList" runat="server" Visible="false">
                                        <div class="col-md-2"></div>
                                        <div class="form-group col-md-4">
                                            <label>Domain</label>
                                            <asp:DropDownList ID="ddlModule" runat="server" AppendDataBoundItems="true" AutoPostBack="true" data-select2-enable="true"
                                                OnSelectedIndexChanged="ddlModule_SelectedIndexChanged" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>



                                        <div class="col-md-12 table table-responsive">
                                            <asp:UpdatePanel ID="updLinks" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel1" runat="server" Style="height: 350px; overflow: auto;" Visible="false">
                                                        <asp:ListView ID="lvALinks" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Access Link List </h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tab-le">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Action
                                                                            </th>
                                                                            <th>Domain
                                                                            </th>
                                                                            <th>Link Title
                                                                            </th>
                                                                            <th>Link URL
                                                                            </th>
                                                                            <th>Shortcut Key
                                                                            </th>
                                                                            <th>Link Sr.No.
                                                                            </th>
                                                                            <th>Level No.
                                                                            </th>
                                                                            <th>Status
                                                                            </th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ImageButton ID="btnEdit" class="newAddNew Tab" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("al_no")%>'
                                                                            OnClick="btnEdit_Click" AlternateText="Edit Record" ToolTip="Edit Record" />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("as_title")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("al_link")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("al_url")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SHORTCUT_KEY")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SrNo")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("LevelNo")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblactinestatus" runat="server" Text='<%# Eval("Active_Status")%>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </asp:Panel>


                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        //function ValidTextbox() {
        //    var charactersOnly = document.getElementById('ctl00$ContentPlaceHolder1$txtShortcutkey').value;
        //    if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
        //        alert("Enter Characters Only");
        //        document.getElementById('ctl00$ContentPlaceHolder1$txtShortcutkey').value = " ";
        //        return false;
        //    }
        //}
        //function submit()
        //{
        //    alert("Link Updated Successfully");
        //}

        function validchar() {

            if ((event.keyCode > 64 && event.keyCode < 91) || (event.keyCode > 96 && event.keyCode < 123) || event.keyCode == 8)
                return true;
            else {
                alert("Enter Characters Only");
                return false;
            }

        }
    </script>
</asp:Content>

