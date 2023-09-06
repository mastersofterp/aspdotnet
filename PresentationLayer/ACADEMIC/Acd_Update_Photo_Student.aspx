<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Acd_Update_Photo_Student.aspx.cs" Inherits="ACADEMIC_Acd_Update_Photo_Student" Title="" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBulkPhoto"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">BULK UPDATION OF PHOTO</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <asp:UpdatePanel runat="server" ID="updBulkPhoto">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Institute Name</label>--%>
                                            <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Institute" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Acd">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Degree</label>--%>
                                            <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="showstud">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree"
                                            Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Acd">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Branch</label>--%>
                                            <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlBranch" runat="server" ControlToValidate="ddlBranch" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Branch" ValidationGroup="Acd" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Admission Batch</label>--%>
                                            <asp:Label ID="lblDYddlAdmBatch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" AppendDataBoundItems="true" runat="server" OnSelectedIndexChanged="ddlAdmbatch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvddlAdmbatch" runat="server" ControlToValidate="ddlAdmbatch" Display="None"
                                            ErrorMessage="Please Select  Admission Batch" ValidationGroup="Acd" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Format</label>
                                        </div>
                                        <asp:RadioButtonList ID="rboStudent" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rboStudent_SelectedIndexChanged">
                                            <asp:ListItem Value="-1" style="display: none"> </asp:ListItem>
                                            <asp:ListItem Value="1">Photo &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">Signature</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvformat" runat="server" ControlToValidate="rboStudent"
                                            Display="None" ErrorMessage="Please Select Photo or Signature Format" ValidationGroup="Acd"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="butShow" runat="server" Text="Show" CssClass="btn btn-primary" OnClick="butShow_Click"
                            ValidationGroup="Acd" />
                        <asp:Button ID="butSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" ValidationGroup="Acd"
                            OnClick="butSubmit_Click" />
                        <asp:Button ID="butReport" CssClass="btn btn-info" runat="server" Text="Show Photo Report" Visible="false"
                            ValidationGroup="Acd" OnClick="butReport_Click1" />
                        <asp:Button ID="btnSignReport" CssClass="btn btn-info" runat="server" Text="Show Sign Report" Visible="false"
                            ValidationGroup="Acd" OnClick="btnSignReport_Click" />
                        <asp:Button ID="btnClear" CssClass="btn btn-warning" runat="server" OnClick="btnClear_Click"
                            Text="Clear" />

                        <asp:ValidationSummary ID="vsSelection" runat="server" ShowMessageBox="true" ShowSummary="false"
                            DisplayMode="List" CssClass="btn btn-primary" ValidationGroup="Acd" />
                    </div>

                    <div class="col-12">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <asp:Panel ID="pnlUpdatePhoto" runat="server" Visible="false">
                                    <asp:ListView ID="lvUpdatePhoto" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display-s" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th><%--Enrollment No.--%>
                                                            <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name</th>
                                                        <th>Photo</th>
                                                        <th>Update Photo</th>
                                                        <%--     <th>Signature</th>
                                                <th>Update Signature</th>--%>
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
                                                    <asp:Label runat="server" ID="lblRegno" Text='<%#Eval("REGNO")%>' ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <asp:Image ToolTip='<%#Eval("STUDNAME")%>' ID="ImgPhoto" Height="50px" Width="80px" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuStudPhoto" runat="server" Width="220px" />
                                                </td>
                                                <%--  <td>
                                            <asp:Image ID="ImgSign" Height="50px" Width="80px" runat="server" ToolTip='<%#Eval("STUDNAME")%>' />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuStudSign" runat="server" Width="220px" />
                                        </td>--%>
                                                <asp:HiddenField ID="hididno" Value='<%#Eval("IDNO")%>' runat="server" />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvUpdatePhoto" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-12">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                            <ContentTemplate>
                                <asp:Panel ID="pnlUpdateSign" runat="server" Visible="false">
                                    <asp:ListView ID="lvUpdateSign" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display-s" style="width: 100%" id="divsessionlist">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th><%--Enrollment No.--%>
                                                            <asp:Label ID="lblDYlvEnrollmentNo" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Student Name</th>
                                                        <%-- <th>Photo</th>
                                                <th>Update Photo</th>--%>
                                                        <th>Signature</th>
                                                        <th>Update Signature</th>
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
                                                    <asp:Label runat="server" ID="lblRegno" Text='<%#Eval("REGNO")%>' ToolTip='<%#Eval("IDNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%#Eval("STUDNAME")%>
                                                </td>
                                                <%--    <td>
                                            <asp:Image ToolTip='<%#Eval("STUDNAME")%>' ID="ImgPhoto" Height="50px" Width="80px" runat="server" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fuStudPhoto" runat="server" Width="220px" />
                                        </td>--%>
                                                <td>
                                                    <asp:Image ID="ImgSign" Height="50px" Width="80px" runat="server" ToolTip='<%#Eval("STUDNAME")%>' />
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuStudSign" runat="server" Width="220px" />
                                                </td>
                                                <asp:HiddenField ID="hididno1" Value='<%#Eval("IDNO")%>' runat="server" />
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lvUpdateSign" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--    <%#Eval("REGNO")%>--%>

    <div id="divMsg" runat="server"></div>

    <%--<script language="javascript" type="text/javascript">
        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            var myDT = $('#divsessionlist').DataTable({

            });
        }

    </script>--%>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('.display-s').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 400,
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
                                return $('.display-s').DataTable().column(idx).visible();
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
                                                return $('.display-s').DataTable().column(idx).visible();
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
                                                return $('.display-s').DataTable().column(idx).visible();
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
                                                return $('.display-s').DataTable().column(idx).visible();
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
                var table = $('.display-s').DataTable({
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
                                    return $('.display-s').DataTable().column(idx).visible();
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
                                                    return $('.display-s').DataTable().column(idx).visible();
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
                                                    return $('.display-s').DataTable().column(idx).visible();
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
                                                    return $('.display-s').DataTable().column(idx).visible();
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
</asp:Content>

