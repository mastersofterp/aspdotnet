<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgStructure.aspx.cs" Inherits="RFC_CONFIG_Masters_OrgStructure" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .Tab:focus {
            outline: none;
            box-shadow: 0px 0px 5px 2px #61C5FA !important;
        }

        .college-lists .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updOrg"
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
    <asp:HiddenField ID="hfdStatMap" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnFlag" runat="server" />
    <asp:HiddenField ID="hdnLogo" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatColg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnLogoColg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfdStatOrg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnFlagOrg" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnCoeSign" runat="server" ClientIDMode="Static" />

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#divcolgglist').DataTable({
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
                                return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                return $('#divcolgglist').DataTable().column(idx).visible();
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
                var table = $('#divcolgglist').DataTable({
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
                                    return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
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
                                                    return $('#divcolgglist').DataTable().column(idx).visible();
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


    <asp:UpdatePanel ID="updOrg" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnLogoOrg" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Organization Structure</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom" id="Tabs">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Define Organization</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Define College</a>
                                    </li>
                                    <li class="nav-item" style="display: none;">
                                        <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Organization-College Mapping</a>
                                    </li>
                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab_1">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Organization / Institute Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtorgName" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="4"
                                                            ToolTip="Enter Organization / Institute Name" Placeholder="Enter Organization / Institute Name" />
                                                        <asp:RequiredFieldValidator ID="rfvSessionLName" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Enter Organization / Institute Name" ControlToValidate="txtorgName"
                                                            Display="None" ValidationGroup="submit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fltname" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Custom" ValidChars="., " TargetControlID="txtorgName" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Email</label>
                                                        </div>
                                                        <asp:TextBox ID="txtemailadd" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                            ToolTip="Enter Email Address" Placeholder="Enter Email Address" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="True"
                                                            ErrorMessage="Enter Email Address" ControlToValidate="txtemailadd"
                                                            Display="None" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Web Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtwebOrg" runat="server" AutoComplete="off" CssClass="form-control" MaxLength="100" TabIndex="6"
                                                            ToolTip="Enter Web Address" Placeholder="Enter Web Address" />
                                                        <asp:RequiredFieldValidator ID="rfvShortName" runat="server" ErrorMessage="Enter Web Address"
                                                            ControlToValidate="txtwebOrg" Display="None" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Contact Person Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactName" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="7" onkeypress="return alphaOnly(event);"
                                                            ToolTip="Enter Contact Name" Placeholder="Enter Contact Name" onkeyup="return ValidateTextbox(this);" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Contact Name"
                                                            ControlToValidate="txtContactName" Display="None" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Contact Person Mob Number</label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactNo" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="10" TabIndex="8"
                                                            ToolTip="Enter Contact Person Mob Number." Placeholder="Enter Contact Person Mob Number" onkeyup="return ValidateNumericTextbox(this);" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Contact Person Mob Number."
                                                            ControlToValidate="txtContactNo" Display="None" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Contact Person Designation</label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactDesign" AutoComplete="off" runat="server" CssClass="form-control" onkeypress="return alphaOnly(event);" MaxLength="25" TabIndex="9"
                                                            ToolTip="Enter Designation" Placeholder="Enter Designation" onkeyup="return ValidateTextbox(this);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Designation."
                                                            ControlToValidate="txtContactDesign" Display="None" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Contact Person Email ID</label>
                                                        </div>
                                                        <asp:TextBox ID="txtContactEmail" AutoComplete="off" runat="server" CssClass="form-control" MaxLength="100" TabIndex="10"
                                                            ToolTip="Enter Contact Person Email ID" Placeholder="Enter Contact Person Email ID"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Contact Person Email ID"
                                                            ControlToValidate="txtContactEmail" Display="None" ValidationGroup="submit" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <%--<label>College Type</label>--%>
                                                            <asp:Label ID="lblDYtxtColgType" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlInstTypeIdOrg" ToolTip="Please Select College Type" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="11">
                                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Ownership Status </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlOwnershipStatusIdOrg" ToolTip="Please Select Ownership Status" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="12">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <%--<span style="color: red;" id="fromDSpan" runat="server" visible="true">*</span> MIS Order Date--%>
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>MIS Order Date </label>
                                                        </div>
                                                        <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtMISOrderDate"
                                                            IsValidEmpty="False" EmptyValueMessage="MIS Order Date is required" InvalidValueMessage="From date is invalid"
                                                            InvalidValueBlurredMessage="Invalid MIS Order Date" Display="Dynamic" ValidationGroup="report" Enabled="true" />


                                                        <div class="input-group date" style="margin-top: 5px;">
                                                            <div class="input-group-addon" id="MisDate">
                                                                <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" /> --%>
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtMISOrderDate" Placeholder="Enter/Select MIS Order Date" ToolTip="Enter/Select MIS Order Date" runat="server" TabIndex="13" CssClass="form-control" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtMISOrderDate" PopupButtonID="MisDate" Enabled="true" EnableViewState="true" />
                                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtMISOrderDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <%--<span style="color: red;" id="Span1" runat="server" visible="true">*</span>Establishment Date--%>
                                                        <div class="label-dynamic">
                                                            <label>Establishment Date </label>
                                                        </div>
                                                        <%--<ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtEstdate"
                                                            IsValidEmpty="False" EmptyValueMessage="Establishment Date is required" InvalidValueMessage="From date is invalid"
                                                            InvalidValueBlurredMessage="Invalid Establishment Date" Display="Dynamic" ValidationGroup="report" Enabled="true" />--%>


                                                        <div class="input-group date" style="margin-top: 5px;">
                                                            <div class="input-group-addon" id="EstDate">
                                                                <%-- <asp:Image ID="imgCalFromDate" runat="server" src="../images/calendar.png" Style="cursor: pointer" /> --%>
                                                                <i class="fa fa-calendar text-blue"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtEstdate" runat="server" Placeholder="Enter/Select Establishment Date" ToolTip="Enter/Select Establishment Date" TabIndex="14" CssClass="form-control" />

                                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtEstdate" PopupButtonID="EstDate" Enabled="true" EnableViewState="true" />
                                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEstdate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Address </label>
                                                        </div>
                                                        <asp:TextBox runat="server" ID="txtAddress" ToolTip="Enter Address" TextMode="MultiLine" CssClass="form-control" Placeholder="Enter Address" TabIndex="15"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Address"
                                                            ControlToValidate="txtAddress" Display="None" ValidationGroup="submit" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <%-- <label><span style="color: red;">*</span> Organization Logo</label>--%>
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Organization Logo </label>
                                                        </div>
                                                        <div class="image">
                                                            <asp:Image ID="imgCollegeLogoOrg" runat="server" ImageUrl="~/Images/nophoto.jpg" BorderColor="#0099FF"
                                                                BorderStyle="Solid" BorderWidth="1px" Height="120px" Width="120px" />
                                                        </div>
                                                        <br />
                                                        <asp:FileUpload ID="fuCollegeLogoOrg" class="newAddNew Tab" TabIndex="17" runat="server" onchange="LoadImageOrg();" />
                                                        <div style="margin-top: 0px;">
                                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Note : (Upload image with .png, .jpg, .jpeg format only and maximum size should be upto 250 KB.)"></asp:Label><br />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-6">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveOrg" name="switch" checked />
                                                                    <label data-on="Active" class="newAddNew Tab" tabindex="18" data-off="Inactive" for="rdActiveOrg"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-6">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Display Organization Logo On Report</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdYesOrg" name="switch2" checked />
                                                                    <label data-on="Yes" class="newAddNew Tab" tabindex="19" data-off="No" for="rdYesOrg"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="form-group col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitOrg" runat="server" ToolTip="Submit" OnClientClick="return validateOrgField();"
                                                    CssClass="btn btn-primary" OnClick="btnSubmitOrg_Click" TabIndex="20">Submit</asp:LinkButton>

                                                <asp:Button ID="btnCancelOrg" runat="server" ToolTip="Cancel" Text="Cancel" OnClick="btnCancelOrg_Click"
                                                    TabIndex="21" CssClass="btn btn-warning" />

                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                                    <asp:ListView ID="lvSessionOrg" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Organization List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>Org Name
                                                                        </th>
                                                                        <th>Email ID
                                                                        </th>
                                                                        <th>Web Address
                                                                        </th>
                                                                        <th>Contact Name
                                                                        </th>
                                                                        <th>Contact No
                                                                        </th>
                                                                        <th>Designation
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
                                                                    <asp:ImageButton ID="btnEditOrg" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png" TabIndex="22"
                                                                        class="newAddNew Tab" CommandArgument='<%# Eval("OrganizationId")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                        OnClick="btnEditOrg_Click" />
                                                                </td>
                                                                <td>
                                                                    <%# Eval("OrgName") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("Email")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("Website")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ContactName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ContactNo")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ContactDesignation")%>
                                                                </td>
                                                                <%--<td>
                                                                    <asp:Label ID="lblIsActive" runat="server" CssClass='<%# Eval("ActiveStatus")%>' Text='<%# Eval("ActiveStatus")%>'></asp:Label>
                                                                </td>--%>
                                                                <td>
                                                                    <asp:Label ID="lblActive" Text='<%# Eval("ActiveStatus")%>' ForeColor='<%# Eval("ActiveStatus").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_2">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Organization</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlOrgToMap" ToolTip="Please Select Organization" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtColgNameColg" Placeholder="Please Enter College Name" CssClass="form-control" onkeypress="return alphaOnlyCol(event);"
                                                            TabIndex="4" ToolTip="Enter College Name" AutoComplete="off" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvColgName" runat="server" ControlToValidate="txtColgNameColg"
                                                            Display="None" ErrorMessage="Please Enter College Name" SetFocusOnError="true"
                                                            ValidationGroup="Submit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteColgname" runat="server" TargetControlID="txtColgNameColg" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlInstituteTypeColg" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="4" ToolTip="Select College Type" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInstituteTypeColg" Display="None" SetFocusOnError="true"
                                                            ErrorMessage="Please Select College Type" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>University</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlUniversityColg" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="5" ToolTip="Select University" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUni" runat="server" ControlToValidate="ddlUniversityColg" Display="None" SetFocusOnError="true"
                                                            ErrorMessage="Please Select University" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Code</label>
                                                        </div>
                                                        <asp:TextBox ID="txtCode" Placeholder="Please Enter Code" ToolTip="Enter Code" CssClass="form-control"
                                                            TabIndex="6" AutoComplete="off" runat="server"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode"
                                                            Display="None" ErrorMessage="Please Enter Code" SetFocusOnError="true"
                                                            ValidationGroup="Submit" />--%>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCode" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Short Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtShortName" Placeholder="Please Enter Short Name" ToolTip="Enter Short Name"
                                                            TabIndex="7" AutoComplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSShort" runat="server" ControlToValidate="txtShortName"
                                                            Display="None" ErrorMessage="Please Enter Short Name" SetFocusOnError="true"
                                                            ValidationGroup="Submit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteShortName" runat="server" TargetControlID="txtShortName" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Location</label>
                                                        </div>
                                                        <asp:TextBox ID="txtLocation" Placeholder="Please Enter Location" onkeypress="return alphaOnlyCol(event);" ToolTip="Enter Location"
                                                            TabIndex="8" AutoComplete="off" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtLocation"
                                                            Display="None" ErrorMessage="Please Enter Location" SetFocusOnError="true"
                                                            ValidationGroup="Submit" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtLocation" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>State</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlStateColg" data-select2-enable="true" AppendDataBoundItems="true" ToolTip="Select State" TabIndex="9" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlStateColg" Display="None" SetFocusOnError="true"
                                                            ErrorMessage="Please Select State" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Address</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAddressColg" TextMode="MultiLine" Placeholder="Please Enter Address" ToolTip="Enter Address" CssClass="form-control"
                                                            TabIndex="10" AutoComplete="off" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddressColg"
                                                            Display="None" ErrorMessage="Please Enter Address" SetFocusOnError="true"
                                                            ValidationGroup="Submit" />
                                                    </div>

                                                    <div class="form-group col-md-3">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Upload Logo</label>
                                                        </div>
                                                        <div class="image">
                                                            <asp:Image ID="btnImage" runat="server" BorderColor="#0099FF"
                                                                BorderStyle="Solid" BorderWidth="1px" Height="120px" Width="120px" ImageUrl="~/IMAGES/nophoto.jpg" />
                                                        </div>
                                                        <br />
                                                        <asp:FileUpload ID="fuLogoColg" class="newAddNew Tab" TabIndex="11" runat="server" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG," ToolTip="Select file to Upload" onchange="LoadImage();" />

                                                        <br />
                                                        <div style="margin-top: 0px;">
                                                            <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="Note : (Upload image with .png, .jpg, .jpeg format only and maximum size should be upto 250 KB.)"></asp:Label><br />
                                                        </div>
                                                    </div>
                                                    <%-- coe sign upload --%>
                                                    <div class="form-group col-md-3">
                                                        <div class="label-dynamic">
                                                            <label>Upload Sign</label>
                                                        </div>
                                                        <div class="">
                                                            <asp:Image ID="ImgSign" runat="server" Height="100px" Width="100px" ImageUrl="~/Images/default-fileupload.png" />
                                                        </div>
                                                        <br />
                                                        <asp:FileUpload ID="fuCoeSign" class="newAddNew Tab" TabIndex="12" runat="server" accept=".jpg,.jpeg,.JPG,.JPEG,.PNG,.gif" ToolTip="Select Sign file to Upload" onchange="LoadSign();" />

                                                        <br />
                                                        <%--<div style="margin-top: 0px;">
                                                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="Note : (Upload image with .png, .jpg, .jpeg format only and maximum size should be upto 250 KB.)"></asp:Label><br />
                                                        </div>--%>
                                                    </div>
                                                    <%-- ----------------------%>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <br />
                                                        <div class="row">
                                                            <div class="form-group col-6">
                                                                <div class="label-dynamic" style="margin-top: -22px">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveColg" name="switch" checked />
                                                                    <label data-on="Active" tabindex="12" class="newAddNew Tab" data-off="Inactive" for="rdActiveColg"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmitColg" TabIndex="13" ToolTip="Submit" OnClick="btnSubmitColg_Click" ValidationGroup="Submit" OnClientClick="return validateCol();"
                                                    CssClass="btn btn-primary" runat="server" Text="Submit" />
                                                <asp:Button ID="btnCancelColg" TabIndex="14" ToolTip="Cancel" OnClick="btnCancelColg_Click" runat="server" CssClass="btn btn-warning" Text="Cancel" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="Submit" />
                                            </div>
                                            <div class="col-12">
                                                <asp:Panel ID="pnlColgMaster" runat="server" Visible="false">
                                                    <asp:ListView ID="lvColgMaster" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>College List</h5>
                                                            </div>
                                                            <div class="college-lists">
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divcolgglist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center;">Edit
                                                                            </th>
                                                                            <th style="text-align: center; width: 5%">College Name
                                                                            </th>
                                                                            <th>College Type
                                                                            </th>
                                                                            <th>University
                                                                            </th>
                                                                            <th>Code
                                                                            </th>
                                                                            <th>Short Name
                                                                            </th>
                                                                            <th>Location
                                                                            </th>
                                                                            <th>Address
                                                                            </th>
                                                                            <th>State
                                                                            </th>
                                                                            <th>Logo
                                                                            </th>
                                                                            <th>
                                                                                COE Sign
                                                                                </th>
                                                                            <th>Status
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel runat="server">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEditColg" class="newAddNew Tab" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                Height="20px" Width="20px" CommandArgument='<%# Eval("COLLEGE_ID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                TabIndex="15" OnClick="btnEditColg_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("InstitutionTypeName")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("UniversityName") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SHORT_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("LOCATION")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_ADDRESS")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("STATENAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image2" Height="40" Width="40" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("Logo")) ) %>' />
                                                                            <%-- <%# Eval("Logo")%>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Image ID="Image1" Height="40" Width="40" runat="server" ImageUrl='<%# String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String((byte[])Eval("COE_Sign"))) %>' />
                                                                           
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblIsActiveCol" runat="server" CssClass='<%# Eval("ActiveStatus")%>' Text='<%# Eval("ActiveStatus")%>' ForeColor='<%# Eval("ActiveStatus").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditColg" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_3" style="display: none;">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Organization</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlOrgMap" ToolTip="Please Select Organization" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4" OnSelectedIndexChanged="ddlOrgMap_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-9 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>College List</label>
                                                        </div>
                                                        <asp:ListView ID="lvCollegeMap" runat="server">
                                                            <LayoutTemplate>
                                                                <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divCollist">--%>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divCollist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th style="text-align: center; padding: 5px;">
                                                                                <asp:CheckBox ID="cbHead" runat="server" TabIndex="4" class="newAddNew Tab" onclick="SelectAll(this)" ToolTip="Select All" Text="Select All" />
                                                                            </th>
                                                                            <th>COLLEGE NAME
                                                                            </th>
                                                                            <th>COLLEGE CODE
                                                                            </th>
                                                                            <th>SHORT NAME
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
                                                                        <asp:CheckBox ID="cbRow" TabIndex="4" class="newAddNew Tab" runat="server" ToolTip='<%# Eval("COLLEGE_ID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="lblColName" Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CODE")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("SHORT_NAME")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="row">
                                                            <div class="form-group col-6">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Status</label>
                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="rdActiveMap" name="switch" checked />
                                                                    <label data-on="Active" data-off="Inactive" for="rdActiveMap"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>

                                                    <%--<div class="form-group col-12">
                                        <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                    </div>--%>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSaveOrgColMapping" runat="server" ToolTip="Submit" OnClientClick="return validateOrgColMapping();"
                                                    CssClass="btn btn-primary" OnClick="btnSaveOrgColMapping_Click" TabIndex="4" Text="Submit"></asp:Button>

                                                <%-- <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                                     OnClick="btnSubmit_Click" TabIndex="11" CssClass="btn btn-primary" ClientIDMode="Static"/>--%>
                                                <asp:Button ID="btnCancelMap" runat="server" ToolTip="Cancel" Text="Cancel" OnClick="btnCancelMap_Click"
                                                    TabIndex="4" CssClass="btn btn-warning" />

                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                            </div>

                                            <div class="col-12">
                                                <asp:Panel ID="pnlMap" runat="server">
                                                    <asp:ListView ID="lvMapping" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Organization College Mapping List</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center;">Edit
                                                                        </th>
                                                                        <th>Organization-College Name
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
                                                            <asp:UpdatePanel runat="server" ID="updlstMapp">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center;">
                                                                            <asp:ImageButton ID="btnEditOrgMap" runat="server" class="newAddNew Tab" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("OrganizationId")%>' AlternateText="Edit Record" ToolTip="Edit Record" CommandName='<%# Eval("OrganizationId")%>'
                                                                                OnClick="btnEditOrgMap_Click" TabIndex="4" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("OrganizationCollegeName") %>
                                                                        </td>
                                                                        <%-- <td>
                                                                            <%# Eval("ActiveStatus") %>
                                                                        </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblActive" Text='<%# Eval("ActiveStatus")%>' ForeColor='<%# Eval("ActiveStatus").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:PostBackTrigger ControlID="btnEditOrgMap" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <%--   <asp:DropDownList runat="server" ID="ddlState">
                                                <asp:ListItem Value="0">Maharashtra</asp:ListItem>
                                            </asp:DropDownList>
                                              <asp:DropDownList ID="ddlCity" runat="server">
                                            </asp:DropDownList>--%>
                                        </div>
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
            <asp:PostBackTrigger ControlID="btnSubmitColg" />
            <asp:PostBackTrigger ControlID="btnCancelColg" />
            <asp:PostBackTrigger ControlID="btnSaveOrgColMapping" />
            <asp:PostBackTrigger ControlID="ddlOrgMap" />
            <asp:PostBackTrigger ControlID="btnSubmitOrg" />
            <asp:PostBackTrigger ControlID="btnCancelOrg" />
            <asp:PostBackTrigger ControlID="btnCancelMap" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))

            //Tabs();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
    <script>
        function SetStatMap(val) {
            $('#rdActiveMap').prop('checked', val);
        }
        function SetFlag(val) {
            $('#rdYes').prop('checked', val);
        }

        function validateOrgColMapping() {
            $('#hfdStatMap').val($('#rdActiveMap').prop('checked'));

            var idddlOrgMap = $("[id$=ddlOrgMap]").attr("id");
            var ddlOrgMap = document.getElementById(idddlOrgMap);
            if (ddlOrgMap.value == 0) {
                //if ($('#ddlOrgMap').val() == 0 || $('#ddlOrgMap').val() == -1) {
                alert('Please Select Organization.', 'Warning!');
                $(ddlOrgMap).focus();
                return false;
            }

            var frm = document.forms[0];
            var tbl = document.getElementById('divCollist');
            var count = 0;
            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvCollegeMap_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkRow.checked == true) {
                    count = count + 1;
                }
            }
            if (count == 0) {
                alert('Please Select Atleast One College.', 'Warning!');
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function ValidateTextbox(txtid) {
            //var charactersOnly = txtid.id;
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = " ";
                return false;
            }
        }

        function ValidateNumericTextbox(txtid) {
            var res = document.getElementById(txtid.id).value;
            if (res != '') {
                if (isNaN(res)) {
                    document.getElementById(txtid.id).value = "";
                    alert("Enter Numbers Only");
                    return false;
                } else {
                    return true
                }
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('divCollist');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvCollegeMap_cbHead');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvCollegeMap_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>
    <script>
        function SetStatColg(val) {
            $('#rdActiveColg').prop('checked', val);
        }

        function validateCol() {

            $('#hfdStatColg').val($('#rdActiveColg').prop('checked'));

            var ddlOrgToMap = $("[id$=ddlOrgToMap]").attr("id");
            var ddlOrgToMap = document.getElementById(ddlOrgToMap);
            if (ddlOrgToMap.value == 0) {
                alert('Please Select Organization', 'Warning!');
                //$(ddlUniversity).css('border-color', 'red');
                $(ddlOrgToMap).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtColgNameColg]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter College Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var ddlUniversity = $("[id$=ddlInstituteTypeColg]").attr("id");
            var ddlUniversity = document.getElementById(ddlUniversity);
            if (ddlUniversity.value == 0) {
                alert('Please Select College Type', 'Warning!');
                //$(ddlUniversity).css('border-color', 'red');
                $(ddlUniversity).focus();
                return false;
            }

            var ddlUniversity = $("[id$=ddlUniversityColg]").attr("id");
            var ddlUniversity = document.getElementById(ddlUniversity);
            if (ddlUniversity.value == 0) {
                alert('Please Select University', 'Warning!');
                //$(ddlUniversity).css('border-color', 'red');
                $(ddlUniversity).focus();
                return false;
            }

            //var idtxtweb = $("[id$=txtCode]").attr("id");
            //var txtweb = document.getElementById(idtxtweb);
            //if (txtweb.value.length == 0) {
            //    alert('Please Enter College Code', 'Warning!');
            //    //$(txtweb).css('border-color', 'red');
            //    $(txtweb).focus();
            //    return false;
            //}

            var idtxtweb = $("[id$=txtShortName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Short Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtLocation]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Location', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var ddlState = $("[id$=ddlStateColg]").attr("id");
            var ddlState = document.getElementById(ddlState);
            if (ddlState.value == 0) {
                alert('Please Select State', 'Warning!');
                //$(ddlState).css('border-color', 'red');
                $(ddlState).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtAddressColg]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Address', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmitColg').click(function () {
                    validateCol();
                });
            });
        });

    </script>
    <script type="text/javascript">
        function alphaOnlyCol(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }

        function LoadImage() {
            var preview = document.querySelector('#<%=btnImage.ClientID %>');
            var file = document.querySelector('#<%=fuLogoColg.ClientID %>').files[0];
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
    </script>
    <script>
        function LoadSign() {
            var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuCoeSign.ClientID %>').files[0];
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
    </script>
    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "tab_1";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
                //location.reload();
            });
        });
        //function pageLoad() {
        //    Tabs();
        //}
    </script>
    <script>
        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>
    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var frm = document.forms[0];
            var tbl = document.getElementById('divCollist');
            var chkHead = document.getElementById('ctl00_ContentPlaceHolder1_lvCollegeMap_cbHead');


            for (i = 0; i < tbl.rows.length - 1; i++) {
                var chkRow = document.getElementById('ctl00_ContentPlaceHolder1_lvCollegeMap_ctrl' + i + '_cbRow');
                //alert(chkRow)
                if (chkHead.checked == true)
                    chkRow.checked = true;
                else
                    chkRow.checked = false;
            }
        }
    </script>
    <script>
        function SetStatOrg(val) {
            $('#rdActiveOrg').prop('checked', val);
        }
        function SetFlagOrg(val1) {
            $('#rdYesOrg').prop('checked', val1);
        }

        function LoadImageOrg() {
            var preview = document.querySelector('#<%=imgCollegeLogoOrg.ClientID %>');
            var file = document.querySelector('#<%=fuCollegeLogoOrg.ClientID %>').files[0];
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

        function validateOrgField() {

            $('#hfdStatOrg').val($('#rdActiveOrg').prop('checked'));
            $('#hdnFlagOrg').val($('#rdYesOrg').prop('checked'));

            var idtxtorgName = $("[id$=txtorgName]").attr("id");
            var txtorgName = document.getElementById(idtxtorgName);
            // alert(txtorgName.value.length)
            if (txtorgName.value.length == 0) {
                alert('Please Enter Organization / Institute Name', 'Warning!');
                //$(txtorgName).css('border-color', 'red');
                $(txtorgName).focus();
                return false;
            }
            var idtxtemailadd = $("[id$=txtemailadd]").attr("id");
            var txtemailadd = document.getElementById(idtxtemailadd);
            if (txtemailadd.value.length == 0) {
                alert('Please Enter Email', 'Warning!');
                //$(txtemailadd).css('border-color', 'red');
                $(txtemailadd).focus();
                return false;
            }

            var emailVal = txtemailadd.value;
            if (emailVal == "") {
                $(txtemailadd).css('border-color', '');
            }
            else {
                var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                if (filter.test(emailVal)) {
                }
                else {
                    alert('Please Enter Email in Correct Format.', 'Warning!');
                    //$(txtemailadd).css('border-color', 'red');
                    $(txtemailadd).focus();
                    return false;
                }
            }

            var idtxtwebOrg = $("[id$=txtwebOrg]").attr("id");
            var txtwebOrg = document.getElementById(idtxtwebOrg);
            if (txtwebOrg.value.length == 0) {
                alert('Please Enter Web Address', 'Warning!');
                //$(txtwebOrg).css('border-color', 'red');
                $(txtwebOrg).focus();
                return false;
            }

            var url = txtwebOrg.value;
            url_validate = /(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
            if (!url_validate.test(url)) {
                alert('Please Enter Valid Web Address', 'Warning!');
                //$(txtwebOrg).css('border-color', 'red');
                $(txtwebOrg).focus();
                return false;
            }

            var idtxtContactEmail = $("[id$=txtContactEmail]").attr("id");
            var txtContactEmail = document.getElementById(idtxtContactEmail);
            //if (txtContactEmail.value.length == 0) {
            //    alert('Please Enter Contact Person Email Address', 'Warning!');
            //    $(txtContactEmail).css('border-color', 'red');
            //    $(txtContactEmail).focus();
            //    return false;
            //}
            var emailVal1 = txtContactEmail.value;
            if (emailVal1 == "") {
                $(txtContactEmail).css('border-color', '');
            }
            else {
                var filter1 = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
                if (filter1.test(emailVal1)) {
                }
                else {
                    alert('Please Enter Contact Person Email in Correct Format', 'Warning!');
                    //$(txtContactEmail).css('border-color', 'red');
                    $(txtContactEmail).focus();
                    return false;
                }
            }

            var idddlInstTypeId = $("[id$=ddlInstTypeIdOrg]").attr("id");
            var ddlInstTypeId = document.getElementById(idddlInstTypeId);
            if (ddlInstTypeId.value == 0) {
                //if ($('#ddlInstTypeId').val() == 0 || $('#ddlInstTypeId').val() == -1) {
                alert('Please Select College Type', 'Warning!');
                //$(ddlInstTypeId).css('border-color', 'red');
                $(ddlInstTypeId).focus();
                return false;
            }

            var idddlOwnershipStatusId = $("[id$=ddlOwnershipStatusIdOrg]").attr("id");
            var ddlOwnershipStatusId = document.getElementById(idddlOwnershipStatusId);
            if (ddlOwnershipStatusId.value == 0) {
                //if ($('#ddlOwnershipStatusId').val() == 0 || $('#ddlOwnershipStatusId').val() == -1) {
                alert('Please Select Ownership Status', 'Warning!');
                $(ddlOwnershipStatusId).focus();
                return false;
            }

            var idtxtMISOrderDate = $("[id$=txtMISOrderDate]").attr("id");
            var txtMISOrderDate = document.getElementById(idtxtMISOrderDate);
            if (txtMISOrderDate.value.length == 0) {
                alert('Please Enter MIS Order Date', 'Warning!');
                //$(txtMISOrderDate).css('border-color', 'red');
                $(txtMISOrderDate).focus();
                return false;
            }

            //var idtxtEstdate = $("[id$=txtEstdate]").attr("id");
            //var txtEstdate = document.getElementById(idtxtEstdate);
            //if (txtEstdate.value.length == 0) {
            //    alert('Please Enter Estabishment Date', 'Warning!')
            //    //$(txtEstdate).css('border-color', 'red');
            //    $(txtEstdate).focus();
            //    return false;
            //}

            var idtxtAddress = $("[id$=txtAddress]").attr("id");
            var txtAddress = document.getElementById(idtxtAddress);
            if (txtAddress.value.length == 0) {
                alert('Please Enter Address', 'Warning!');
                //$(txtAddress).css('border-color', 'red');
                $(txtAddress).focus();
                return false;
            }

        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validateOrgField();
                });
            });
        });
    </script>
    <script type="text/javascript">
        function ValidateTextbox(txtid) {
            //var charactersOnly = txtid.id;
            var charactersOnly = document.getElementById(txtid.id).value;

            if (!/^[a-zA-Z ]*$/g.test(charactersOnly)) {
                alert("Enter characters Only");
                document.getElementById(txtid.id).value = "";
                return false;
            }
        }

        function ValidateNumericTextbox(txtid) {
            var res = document.getElementById(txtid.id).value;
            if (res != '') {
                if (isNaN(res)) {
                    document.getElementById(txtid.id).value = "";
                    alert("Enter Numbers Only");
                    return false;
                } else {
                    return true
                }
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

        $("#ctl00_ContentPlaceHolder1_pnlColgMaster").click(function () {
            var tab2 = $("[id*=TabName]").val("tab_2");//document.getElementById('<%= TabName.ClientID%>').value;
            //$('#Tabs a[href="' + tab1 + '"]').tab('show');
            //alert(tab2.val());

        });
        $("#ctl00_ContentPlaceHolder1_pnlMap").click(function () {
            var tab3 = $("[id*=TabName]").val("tab_3");//document.getElementById('<%= TabName.ClientID%>').value;
            //alert(tab3.val());
            //$('#Tabs a[href="' + tab2 + '"]').tab('show');

        });
        $('.nav-tabs a').on('shown.bs.tab', function () {
            $($.fn.dataTable.tables(true)).DataTable()
                   .columns.adjust();
        });
    </script>
</asp:Content>

