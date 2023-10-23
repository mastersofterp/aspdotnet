<%@ Page Title="" Language="C#" MasterPageFile="~/RFC_CONFIG/ConfigSiteMasterPage.master" AutoEventWireup="true" CodeFile="BasicMasterCreation.aspx.cs" Inherits="RFC_CONFIG_Masters_BasicMasterCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />


    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }
    </style>
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatDegTyp" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatDegMaster" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdbranch" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfIsCore" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hidTAB" runat="server" Value="tab_1" />
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
            var table = $('#divlistDegree').DataTable({
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
                                return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                return $('#divlistDegree').DataTable().column(idx).visible();
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
                var table = $('#divlistDegree').DataTable({
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
                                    return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
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
                                                    return $('#divlistDegree').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="updMaster" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnLogo" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--<h3 class="box-title">BASIC MASTER CREATION</h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Define Department</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="1">Degree Type</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="1">Define Degree</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_4" tabindex="1">Define Branch</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdprogDepartment" runat="server" AssociatedUpdatePanelID="updDepartment"
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
                                        <asp:UpdatePanel ID="updDepartment" runat="server">
                                            <ContentTemplate>
                                                <div id="divMsg" runat="server">
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Department Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" TabIndex="5"
                                                                    ToolTip="Please Enter Department Name" AutoComplete="OFF" placeholder="Department Name" ValidationGroup="submit" />
                                                                <%--    <asp:RequiredFieldValidator ID="rfvDepartmentname" runat="server" ControlToValidate="txtDepartment"
                                                        Display="None" ErrorMessage="Please Enter Department Name." ValidationGroup="submit"></asp:RequiredFieldValidator> onkeyup="return ValidateTextbox(this);"--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".,&0123456789 " TargetControlID="txtDepartment" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Department Short Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtDeptShortName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtDeptShort" runat="server" CssClass="form-control" TabIndex="6"
                                                                    ToolTip="Please Enter Department Short Name" AutoComplete="OFF" placeholder="Department Short Name" ValidationGroup="submit" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="&.-0123456789 " TargetControlID="txtDeptShort" />
                                                            </div>
                                                            <div class="form-group col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActive" name="switch" checked />
                                                                    <label data-on="Active" tabindex="7" class="newAddNew Tab" data-off="Inactive" for="rdActive"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <br />
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                            CssClass="btn btn-primary" TabIndex="8" OnClick="btnSave_Click" OnClientClick="return validate();" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            CssClass="btn btn-warning" TabIndex="9" OnClick="btnCancel_Click" />

                                                        <asp:ValidationSummary ID="vlsummary" runat="server" ValidationGroup="submit"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <div class="col-md-12">
                                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                                <asp:ListView ID="lvDepartment" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Department List</h5>
                                                                        </div>
                                                                        <table id="divsessionlist" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th style="text-align: center;">Edit </th>
                                                                                    <th>ID </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtDeptName" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>
                                                                                        <asp:Label ID="lblDYtxtDeptShortName" runat="server" Font-Bold="true"></asp:Label>
                                                                                    </th>
                                                                                    <th>Status </th>
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
                                                                                <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" class="newAddNew Tab" CommandArgument='<%# Eval("DEPTNO") %>' ImageUrl="~/images/edit.png" OnClick="btnEdit_Click" TabIndex="10" ToolTip="Edit Record" />
                                                                            </td>
                                                                            <td><%# Eval("DEPTNO")%></td>
                                                                            <td><%# Eval("DEPTNAME")%></td>
                                                                            <td><%# Eval("DEPTCODE")%></td>
                                                                            <td>
                                                                                <asp:Label ID="lblActive3" runat="server" ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' Text='<%# Eval("ACTIVESTATUS")%>'></asp:Label>
                                                                                <%-- <%# Eval("ACTIVESTATUS")%>--%></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                    </p>

                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div class="tab-pane" id="tab_2">
                                        <div>
                                            <asp:UpdateProgress ID="UpdprogDegreeType" runat="server" AssociatedUpdatePanelID="updDegreeType"
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
                                        <asp:UpdatePanel ID="updDegreeType" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree Type Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtDegTypName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtDegreeType" AutoComplete="off" TabIndex="2" placeholder="Enter Degree Type Name" onkeypress="return alphaOnlyDegType(event);" runat="server" MaxLength="50" CssClass="form-control"
                                                                    ToolTip="Please Enter Degree Type Name" />
                                                                <%--                  <ajaxToolKit:FilteredTextBoxExtender ID="fteDegree" runat="server" TargetControlID="txtDegreeType" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=". " TargetControlID="txtDegreeType" />
                                                            </div>

                                                            <div class="form-group col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveDegType" name="switch" checked />
                                                                    <label data-on="Active" tabindex="3" class="newAddNew Tab" data-off="Inactive" for="rdActiveDegType"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitDegType" TabIndex="3" OnClick="btnSubmitDegType_Click" ValidationGroup="Submit" OnClientClick="return validateDegTyp();"
                                                            CssClass="btn btn-primary" runat="server" Text="Submit" />
                                                        <asp:Button ID="btnCancelDegType" TabIndex="4" OnClick="btnCancelDegType_Click" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlDegreeType" runat="server" Visible="false">
                                                            <asp:ListView ID="lvDegreeType" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Degree Type List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divlistType">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>ID
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegTypName" runat="server" Font-Bold="true"></asp:Label>
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
                                                                    <asp:UpdatePanel runat="server" ID="updDegreeType">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btnEditDegType" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" OnClick="btnEditDegType_Click"
                                                                                        CommandArgument='<%# Eval("UA_SECTION")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="5" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_SECTION")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_SECTIONNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive4" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                                                    <%-- <%# Eval("ACTIVESTATUS")%>--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:PostBackTrigger ControlID="btnEditDegType" />--%>
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

                                    <div class="tab-pane" id="tab_3">
                                        <div>
                                            <asp:UpdateProgress ID="updProgDegree" runat="server" AssociatedUpdatePanelID="updDegreeMaster"
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
                                        <asp:UpdatePanel ID="updDegreeMaster" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>

                                                                <asp:TextBox ID="txtDegreeName" AutoComplete="off" TabIndex="2" placeholder="Enter Degree Name" runat="server" MaxLength="128" CssClass="form-control"
                                                                    ToolTip="Please Enter Degree Name" OnTextChanged="txtDegreeName_TextChanged" AutoPostBack="true" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".,0123456789+-&() " TargetControlID="txtDegreeName" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree Short Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtDegShortName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtDegreeShortName" AutoComplete="off" TabIndex="3" placeholder="Enter Degree Short Name" runat="server" MaxLength="50" CssClass="form-control"
                                                                    ToolTip="Please Enter Degree Short Name" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".,0123456789+-&() " TargetControlID="txtDegreeShortName" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <%--<label>Degree Type</label>--%>
                                                                    <asp:Label ID="lblDegreeNameHindi" runat="server" Font-Bold="true">Degree Name Hindi</asp:Label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtDegreeNameHindi" CssClass="form-control" placeholder="Degree Name Hindi"></asp:TextBox>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree Code</label>--%>
                                                                    <asp:Label ID="lblDYtxtDegCode" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtDegreeCode" AutoComplete="off" TabIndex="4" placeholder="Enter Degree Code" runat="server" MaxLength="50" CssClass="form-control"
                                                                    ToolTip="Please Enter Degree Code" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".,0123456789+-&() " TargetControlID="txtDegreeCode" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Degree Type</label>--%>
                                                                    <asp:Label ID="lblDYddlDegreeType" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlDegreeType" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveDegMas" name="switch" checked />
                                                                    <label data-on="Active" tabindex="5" class="newAddNew Tab" data-off="Inactive" for="rdActiveDegMas"></label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitDegMaster" TabIndex="5" OnClick="btnSubmitDegMaster_Click" ToolTip="Submit" ValidationGroup="Submit" OnClientClick="return validateDegMaster1();"
                                                            CssClass="btn btn-primary" runat="server" Text="Submit" />
                                                        <asp:Button ID="btnCancelDegMaster" TabIndex="6" ToolTip="Cancel" OnClick="btnCancelDegMaster_Click" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlDegreeMaster" runat="server" Visible="false">
                                                            <asp:ListView ID="lvDegreeMaster" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Degree List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divlistDegree">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>ID
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lbltxtDegreeNameHindi" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegShortName" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtDegCode" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYddlDegreeType" runat="server" Font-Bold="true"></asp:Label>
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
                                                                    <asp:UpdatePanel runat="server" ID="updDegreeMaster">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center;">

                                                                                    <asp:ImageButton ID="btnEditDegMaster" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" OnClick="btnEditDegMaster_Click"
                                                                                        CommandArgument='<%# Eval("DEGREENO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                        TabIndex="7" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("DEGREENO")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("DEGREENAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("DEGREENAMEHINDI")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("CODE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("DEGREE_CODE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_SECTIONNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive1" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                                                    <%--  <%# Eval("ACTIVESTATUS")%>--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:PostBackTrigger ControlID="btnEditDegMaster" />--%>
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

                                    <div class="tab-pane" id="tab_4">
                                        <div>
                                            <asp:UpdateProgress ID="updprogBranch" runat="server" AssociatedUpdatePanelID="updBranchMaster"
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
                                        <asp:UpdatePanel ID="updBranchMaster" runat="server">
                                            <ContentTemplate>
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Branch Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtBranchName" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <%--    onkeyup="return ValidTxtbranchname();"--%>
                                                                <asp:TextBox ID="txtBranchname" runat="server" AutoPostBack="true" CssClass="form-control" TabIndex="2"
                                                                    ToolTip="Please Enter Branch Name" AutoComplete="OFF" placeholder="Branch Name" OnTextChanged="txtBranchname_TextChanged" ValidationGroup="submit" />
                                                                <asp:RequiredFieldValidator ID="rfvBranchname" runat="server" ControlToValidate="txtBranchname"
                                                                    Display="None" ErrorMessage="Please Enter Branch Name." ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".,+-&() " TargetControlID="txtBranchname" />

                                                            </div>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Branch Short Name</label>--%>
                                                                    <asp:Label ID="lblDYtxtBranchShort" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <asp:TextBox ID="txtBranchshortname" runat="server" CssClass="form-control" TabIndex="3"
                                                                    ToolTip="Please Enter Branch Short Name" AutoComplete="OFF" placeholder="Branch Short Name" ValidationGroup="submit" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars=".-123456789+-&() " TargetControlID="txtBranchshortname" />

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <%--<label>Degree Type</label>--%>
                                                                    <asp:Label ID="lblBranchNameHindi" runat="server" Font-Bold="true">Branch Name Hindi</asp:Label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtBranchNameHindi" CssClass="form-control" placeholder="Branch Name Hindi"></asp:TextBox>
                                                            </div>

                                                            <asp:RequiredFieldValidator ID="rfvbranchshortname" runat="server" ErrorMessage="Please Enter Branch Short Name"
                                                                ControlToValidate="txtBranchshortname" ValidationGroup="submit" Display="None">
                                                            </asp:RequiredFieldValidator>
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <%--<sup>* </sup>--%>
                                                                    <label>Knowledge Partner</label>

                                                                </div>
                                                                <asp:DropDownList ID="ddlKnowledgePartner" runat="server" TabIndex="4" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">

                                                                    <label>Is Core</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdisCore" name="switch" checked />
                                                                    <label data-on="Yes" tabindex="4" class="newAddNew Tab" data-off="No" for="rdisCore"></label>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActivebranch" name="switch" checked />
                                                                    <label data-on="Active" tabindex="4" class="newAddNew Tab" data-off="Inactive" for="rdActivebranch"></label>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="box-footer">
                                                        <p class="text-center">
                                                            <asp:Button ID="btnSavebranch" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit"
                                                                CssClass="btn btn-primary" TabIndex="4" OnClick="btnSavebranch_Click" OnClientClick="return validatebranch();" />
                                                            <asp:Button ID="btnCancelbranch" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                CssClass="btn btn-warning" TabIndex="5" OnClick="btnCancelbranch_Click" />

                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </p>
                                                    </div>

                                                    <div class="col-md-12">
                                                        <asp:Panel ID="Panelbranch" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvBranch" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Branch List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divbranchlist">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th style="text-align: center;">Edit
                                                                                </th>
                                                                                <th>ID
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBranchName" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>
                                                                                    <asp:Label ID="lblDYtxtBranchShort" runat="server" Font-Bold="true"></asp:Label>
                                                                                </th>
                                                                                <th>Branch in Hindi
                                                                                </th>
                                                                                <th>Is Core
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
                                                                    <asp:UpdatePanel runat="server" ID="updBranchMaster">
                                                                        <ContentTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center;">
                                                                                    <asp:ImageButton ID="btnEditbranch" class="newAddNew Tab" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("BRANCHNO") %>'
                                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditbranch_Click" TabIndex="7" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("BRANCHNO")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("LONGNAME")%>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("SHORTNAME")%>
                                                                                </td>

                                                                                <td>
                                                                                    <%# Eval("BRANCHNAMEINLOCALLANGUAGE")%>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Eval("ISCORESTATUS")%>' ForeColor='<%# Eval("ISCORESTATUS").ToString().Equals("Yes")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblActive2" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                                                    <%-- <%# Eval("ACTIVESTATUS")%>--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <%--<asp:PostBackTrigger ControlID="btnEditbranch" />--%>
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
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="TabName" runat="server" />
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                //alert("sunita " + $(tabName).attr("href"));
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //$("[id*=TabName]").val();
            });
        });

    </script>
    <%-- Department Master--%>
    <script>
        function SetStat(val) {
            $('#rdActive').prop('checked', val);
        }

        function validate() {

            $('#hfdStat').val($('#rdActive').prop('checked'));

            var idtxtDepartment = $("[id$=txtDepartment]").attr("id");
            var txtDepartment = document.getElementById(idtxtDepartment);
            // alert(txtOwnershipStatusName.value.length)
            if (txtDepartment.value.length == 0) {
                alert('Please Enter Department Name', 'Warning!');
                //$(txtDepartment).css('border-color', 'red');
                $(txtDepartment).focus();
                return false;
            }

            var idtxtDeptShort = $("[id$=txtDeptShort]").attr("id");
            var txtDeptShort = document.getElementById(idtxtDeptShort);
            // alert(txtOwnershipStatusName.value.length)
            if (txtDeptShort.value.length == 0) {
                alert('Please Enter Department Short Name', 'Warning!');
                //$(txtDeptShort).css('border-color', 'red');
                $(txtDeptShort).focus();
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
        function ValidateTextbox(txtid) {
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = "";
                return false;
            }
        }
    </script>
    <script type="text/javascript">
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
    </script>

    <%-- Degree Type Master--%>

    <script>
        function SetStatDegTyp(val) {
            $('#rdActiveDegType').prop('checked', val);
        }

        function validateDegTyp() {

            $('#hfdStatDegTyp').val($('#rdActiveDegType').prop('checked'));

            var idtxtweb = $("[id$=txtDegreeType]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Degree Type Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitDegType').click(function () {
                    validateDegTyp();
                });
            });
        });

    </script>
    <script type="text/javascript">
        function alphaOnlyDegType(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <%-- Degree Master--%>
    <script>
        function SetStatDegMaster(val) {
            $('#rdActiveDegMas').prop('checked', val);
        }

        function validateDegMaster() {

            $('#hfdStatDegMaster').val($('#rdActiveDegMas').prop('checked'));

            var degName = $("[id$=txtDegreeName]").attr("id");
            var degName = document.getElementById(degName);
            if (degName.value == 0) {
                alert('Please Enter Degree Name', 'Warning!');
                $(degName).focus();
                return false;
            }

            var ShrtN = $("[id$=txtDegreeShortName]").attr("id");
            var ShrtN = document.getElementById(ShrtN);
            if (ShrtN.value == 0) {
                alert('Please Enter Short Name', 'Warning!');
                $(ShrtN).focus();
                return false;
            }

            var Code = $("[id$=txtDegreeCode]").attr("id");
            var Code = document.getElementById(Code);
            if (Code.value.length == 0) {
                alert('Please Enter Degree Code', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(Code).focus();
                return false;
            }

            var DegTypeM = $("[id*=ctl00_ContentPlaceHolder1_ddlDegreeType]").val();
            // var DegTypeM = document.getElementById(DegTypeM);
            if (DegTypeM == 0) {
                alert('Please Select Degree Type', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $("[id*=ctl00_ContentPlaceHolder1_ddlDegreeType]").focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitDegMaster').click(function () {
                    validateDegMaster();
                });
            });
        });

    </script>
    <script type="text/javascript">
        function alphaOnlyDegMaster(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

    <%-- Branch Master--%>
    <script>
        function SetStatBranch(val) {
            //alert('b');
            $('#rdActivebranch').prop('checked', val);
        }

        function SetStatIsCore(val1) {
            //alert('a');
            $('#rdisCore').prop('checked', val1);
        }

        function validatebranch() {

            $('#hfdbranch').val($('#rdActivebranch').prop('checked'));
            $('#hdfIsCore').val($('#rdisCore').prop('checked'));


            var BrName = $("[id$=txtBranchname]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Branch Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(BrName).focus();
                return false;
            }

            var BrName = $("[id$=txtBranchshortname]").attr("id");
            var BrName = document.getElementById(BrName);
            if (BrName.value.length == 0) {
                alert('Please Enter Branch Short Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(BrName).focus();
                return false;
            }

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSavebranch').click(function () {
                    validatebranch();
                });
            });
        });
    </script>

    <script type="text/javascript">
        function ValidTxtbranchname() {
            var charactersOnly = document.getElementById('ctl00_ContentPlaceHolder1_txtBranchname').value;
            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter Characters Only");
                document.getElementById('ctl00_ContentPlaceHolder1_txtBranchname').value = "";
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    <script>

        $("#ctl00_ContentPlaceHolder1_pnlDegreeType").click(function () {
            var tab2 = $("[id*=TabName]").val("tab_2");//document.getElementById('<%= TabName.ClientID%>').value;
            //$('#Tabs a[href="' + tab1 + '"]').tab('show');
            //alert(tab2.val());

        });
        $("#ctl00_ContentPlaceHolder1_pnlDegreeMaster").click(function () {
            var tab3 = $("[id*=TabName]").val("tab_3");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab3.val());
            //$('#Tabs a[href="' + tab2 + '"]').tab('show');

        });
        $("#ctl00_ContentPlaceHolder1_Panelbranch").click(function () {
            var tab4 = $("[id*=TabName]").val("tab_4");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab4.val());
            //$('#Tabs a[href="' + tab3 + '"]').tab('show');

        });
        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>







    <%--<script>
        function translateToHindi() {
            // Get the value entered in the first textbox
            var englishText = document.getElementById('<%= txtDegreeName.ClientID %>').value;

            // Use an API or translation service to translate the text to Hindi
            // Replace this with the actual translation code or API request
            var hindiTranslation = translateTextToHindi(englishText);

            // Update the second textbox with the translated text
            document.getElementById('<%= txtDegreeNameHindi.ClientID %>').value = hindiTranslation;
}

// Function to translate text to Hindi using an API or other methods
function translateTextToHindi(text) {
    // Replace this with your translation logic or API call
    // For example, you can use Google Translate API, Microsoft Translator, etc.
    // Be sure to handle the translation and error handling accordingly
    // This is a placeholder and does not perform actual translation
    return "Hindi Translation: " + text;
}
    </script>--%>
</asp:Content>

