<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Str_Item_Master.aspx.cs" Inherits="Stores_Masters_Str_Item_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="RControl" Namespace="RControl" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>--%>

    <%--<script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#divsessionlist').DataTable({
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
                                return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                return $('#divsessionlis').DataTable().column(idx).visible();
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
                var table = $('#divsessionlist').DataTable({
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
                                    return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlis').DataTable().column(idx).visible();
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
                                                    return $('#divsessionlis').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">ITEM MASTER</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#ItmMas">Item Master</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#SubbGrp">Item Sub Group Master</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#MaiGrp" id="A1" runat="server" visible="false">Item Group Master</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#SuGrpDep">Sub Group Depreciation Entry</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="ItmMas">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updatePanel1"
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

                                <asp:UpdatePanel ID="updatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="pnl" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Item Master</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDept" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Department</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Department" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" Enabled="false">
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepartment"
                                                                Display="None" ErrorMessage="Please Select Department." ValidationGroup="store"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span5" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Sub Group</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItemSubGroup" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Item Sub Group"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlItemSubGroup_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlItemSubGroup" runat="server" ControlToValidate="ddlItemSubGroup"
                                                                Display="None" ErrorMessage="Please Select Item Sub Group" ValidationGroup="store"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span6" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Item Name" MaxLength="100" TabIndex="2"
                                                                onKeyUp="LowercaseToUppercase(this)"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbetxtName" runat="server" TargetControlID="txtItemName"
                                                                FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-!@#$%^&*()/?:. ">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemName" runat="server" ControlToValidate="txtItemName"
                                                                Display="None" ErrorMessage="Please Enter Item Name" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span7" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Item Code" MaxLength="30" TabIndex="3"
                                                                onKeyUp="LowercaseToUppercase(this)"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbeItemCode" runat="server" TargetControlID="txtItemCode"
                                                                FilterMode="ValidChars" ValidChars="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890/">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemCode" runat="server" ControlToValidate="txtItemCode"
                                                                Display="None" ErrorMessage="Please Enter Item Code" ValidationGroup="store"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Unit</label>
                                                            </div>
                                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Unit" MaxLength="100" TabIndex="4"
                                                                onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtUnitFilter" runat="server" FilterType="LowercaseLetters, UppercaseLetters"
                                                                FilterMode="ValidChars" TargetControlID="txtUnit">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Re-order Level</label>
                                                            </div>
                                                            <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Re-order Level" MaxLength="5" TabIndex="5"
                                                                Text="0"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtReorderLevelFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtReorderLevel" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Specification Of Item</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCommonDescriptionOfItem" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Specification Of Item" MaxLength="2000"
                                                                TabIndex="6"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Budget Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtBudgetQuantity" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Budget Quantity" MaxLength="6" TabIndex="10"
                                                                Text="0"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtBudgetQuantityFilter" runat="server"
                                                                FilterType="Numbers,custom" FilterMode="ValidChars" TargetControlID="txtBudgetQuantity"
                                                                ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Max Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMaxQuantity" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Max Quantity" MaxLength="6" TabIndex="11"
                                                                Text="0"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtMaxQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtMaxQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Min Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtMinQuantity" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Min Quantity" MaxLength="6" TabIndex="12"
                                                                Text="0"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtMinQuantityFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtMinQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOB" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Ope.Bal Quantity</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOpeningBalanceQuantity" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Ope.Bal Quantity" Text="0"
                                                                MaxLength="6" TabIndex="13"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtOpBalQtyFilter" runat="server" FilterType="Numbers,custom"
                                                                FilterMode="ValidChars" TargetControlID="txtOpeningBalanceQuantity" ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divOBValue" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Ope.Bal Value</label>
                                                            </div>
                                                            <asp:TextBox ID="txtOpeningBalanceValue" runat="server" CssClass="form-control"
                                                                ToolTip="Enter Ope.Bal Value" MaxLength="6"
                                                                Text="0" TabIndex="14"></asp:TextBox>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="txtOpeningBalanceValueFilter" runat="server"
                                                                FilterType="Numbers,custom" FilterMode="ValidChars" TargetControlID="txtOpeningBalanceValue"
                                                                ValidChars=".">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Is Tax Applicable</label>
                                                            </div>
                                                            <asp:CheckBox ID="chkTax" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="ChkTaxChckedChanged" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItmType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                AppendDataBoundItems="True" TabIndex="7" Visible="false">
                                                                <asp:ListItem Value="0">Please Select
                                                                </asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12">
                                                    <asp:Panel runat="server" ID="pnlTaxFields">
                                                        <asp:ListView ID="lvTaxFields" runat="server">
                                                            <LayoutTemplate>
                                                                <div id="lgv1">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Select </th>
                                                                                <th>Tax Name </th>
                                                                                <th>Tax Per </th>
                                                                                <th>Tax Type </th>
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
                                                                        <asp:CheckBox ID="chkTAXID" runat="server" Checked="false" />
                                                                        <asp:HiddenField ID="hdTAXID" runat="server" Value='<%# Eval("TAXID") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTAXNAME" runat="server" Text='<%# Eval("TAX_NAME")%>' />
                                                                        <asp:HiddenField ID="HDTAXSRNO" runat="server" Value='<%# Eval("TAX_SRNO") %>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTAXPER" runat="server" Text='<%# Eval("TAX_PER")%>' />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSTATETAX" runat="server" Text='<%# Eval("IS_STATE_TAX")%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer mt-3 mb-3">
                                                <asp:Button ID="butItemMasterSubmit" ValidationGroup="store" Text="Submit" runat="server"
                                                    OnClick="butItemMasterSubmit_Click" TabIndex="17" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnshowrptItems" Text="Report" Visible="false" runat="server" CssClass="btn btn-info"
                                                    TabIndex="18" OnClick="btnshowrptItems_Click" />
                                                <asp:Button ID="Button1" Text="Report" runat="server" CssClass="btn btn-info"
                                                    TabIndex="18" OnClick="Button1_Click" />
                                                <asp:Button ID="butItemMasterCancel" Text="Cancel" runat="server" CssClass="btn btn-warning"
                                                    OnClick="butItemMasterCancel_Click" TabIndex="19" />
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="store"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:HiddenField ID="hidLastTab" Value="0" runat="server" />
                                            </div>

                                            <asp:Panel ID="pnlItemMaster" runat="server" Visible="false">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Item Master Entry List</h5>
                                                    </div>
                                                </div>
                                                <div class="col-12">
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Action
                                                                </th>
                                                                <th id="thDept" runat="server" visible="false">Department
                                                                </th>
                                                                <th>Group Name
                                                                </th>
                                                                <th>Sub Group Name
                                                                </th>
                                                                <th>Item Name
                                                                </th>
                                                                <th>Unit
                                                                </th>
                                                                <%--<th>OP Bal Qty
                                                                </th>--%>
                                                                <th>Reorder Level
                                                                </th>
                                                                <%-- <th>Approval
                                                                            </th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:ListView ID="lvItemMaster" runat="server" OnItemDataBound="lvItemMaster_ItemDataBound">
                                                                <EmptyDataTemplate>
                                                                    <br />
                                                                    <div class="text-center">
                                                                        <asp:Label ID="lblerr" runat="server" SkinID="Errorlbl" Text="No Records Found" />
                                                                    </div>
                                                                </EmptyDataTemplate>

                                                                <LayoutTemplate>
                                                                    <div id="demo-grid" class="">
                                                                        <div class="lgv1">
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </div>

                                                                    </div>

                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEditItemMaster" runat="server" AlternateText="Edit Record"
                                                                                CommandArgument='<%# Eval("ITEM_NO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEditItemMaster_Click"
                                                                                ToolTip="Edit Record" TabIndex="20" />
                                                                            &#160;
                                                                        </td>
                                                                        <td id="tdDept" runat="server" visible="false">
                                                                            <%# Eval("MDNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MIGNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MISGNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ITEM_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ITEM_UNIT")%>
                                                                        </td>
                                                                        <%--<td>
                                                                            <%# Eval("ITEM_MIN_QTY")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ITEM_MAX_QTY")%>
                                                                        </td>--%>
                                                                        <%-- <td>
                                                                                    <%# Eval("ITEM_OB_QTY")%>
                                                                                </td>--%>
                                                                        <%--<td>
                                                                            <%# Eval("ITEM_OB_VALUE")%>
                                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("ITEM_REORDER_QTY")%>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:ListView>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="SubbGrp">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updatePanel2"
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
                                <asp:UpdatePanel ID="updatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Item Sub Group Name</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="spanDept" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Group Name</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItemGroupName" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Select Item Group Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvddlItemGroupName" runat="server" ControlToValidate="ddlItemGroupName"
                                                                Display="None" ErrorMessage="Please Select Item Group Name" ValidationGroup="storesub"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span1" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Sub Group Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemSubGroupName" MaxLength="100" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Item Sub Group Name"
                                                                onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemSubGroupName" runat="server" ControlToValidate="txtItemSubGroupName"
                                                                Display="None" ErrorMessage="Please Enter Item Sub Group Name" ValidationGroup="storesub"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="regtxtItemSubGroupName" Display="None"
                                                                ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*" ControlToValidate="txtItemSubGroupName"
                                                                ErrorMessage="Enterd Only Alphanumeric Characters For Sub Group Name " ValidationGroup="storesub"></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="span2" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Short Name/Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSubShortname" runat="server" CssClass="form-control" TabIndex="3" ToolTip="Enter Short Name/Code" MaxLength="100" onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtSubShortname" runat="server" ControlToValidate="txtSubShortname"
                                                                Display="None" ErrorMessage="Please Enter Short Name/Code" ValidationGroup="storesub"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" Display="None"
                                                                ValidationGroup="storesub" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                ControlToValidate="txtSubShortname" ErrorMessage="Enterd Only Alphanumeric Characters Short sub Group Name "></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer">
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="storesub" />

                                                <asp:Button ID="butSubItemSubmit" runat="server" OnClick="butSubItemSubmit_Click"
                                                    Text="Submit" ValidationGroup="storesub" CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Submit" />

                                                <asp:Button ID="btnshowsubgrprpt" Text="Report" runat="server" Visible="false" CssClass="btn btn-info" TabIndex="5" ToolTip="Click To Show Report" OnClick="btnshowsubgrprpt_Click" />
                                                <asp:Button ID="Button2" Text="Report" runat="server" CssClass="btn btn-info" TabIndex="5" ToolTip="Click To Show Report" OnClick="Button2_Click" />
                                                <asp:Button ID="butSubItemcancel" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="6" ToolTip="Click To Reset"
                                                    OnClick="butSubItemcancel_Click" />
                                            </div>

                                            <asp:Panel ID="pnlItemSubGroupMaster" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvItemSubGroupMaster" runat="server">
                                                        <EmptyDataTemplate>
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Item Sub Group Master</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="divsessionlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Group Name
                                                                        </th>
                                                                        <th>Sub Group Name
                                                                        </th>
                                                                        <th>Short Name
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
                                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("MISGNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSubGroup_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MIGNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MISGNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SNAME")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                    <%--<div class="vista-grid_datapager text-center">
                                                        <asp:DataPager ID="dpPagerSubGroupMaster" runat="server" PagedControlID="lvItemSubGroupMaster"
                                                            PageSize="10" OnPreRender="dpPagerSubGroupMaster_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>--%>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="MaiGrp">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updatePanel3"
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
                                <asp:UpdatePanel ID="updatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel5" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Item Group Name</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Type</label>
                                                            </div>
                                                            <asp:RadioButtonList ID="rdbItemType" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="F" Selected="True">Fixed Assets&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="C">Consumable</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Group Name</label>
                                                            </div>
                                                            <asp:TextBox ID="txtItemGroupName" runat="server" CssClass="form-control" TabIndex="1" ToolTip="Enter  Item Group Name" MaxLength="100" onKeyUp="LowercaseToUppercase(this);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemGroupName" runat="server" ControlToValidate="txtItemGroupName"
                                                                Display="None" ErrorMessage="Please Enter Item Group Name" ValidationGroup="storegrp"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="regtxtItemGroupName" Display="None"
                                                                ValidationGroup="storegrp" ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*"
                                                                ControlToValidate="txtItemGroupName" ErrorMessage="Enterd Only Alphanumeric Characters For  Group Name "></asp:RegularExpressionValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Short Name/Code</label>
                                                            </div>
                                                            <asp:TextBox ID="txtShortName" runat="server" MaxLength="25" onKeyUp="LowercaseToUppercase(this);" CssClass="form-control" TabIndex="2" ToolTip="Enter Short Name/Code"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtShortName" runat="server" ControlToValidate="txtShortName" Display="None" ErrorMessage="Please Enter Item Group Short Name" SetFocusOnError="True" ValidationGroup="storegrp"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <div class="col-12 btn-footer">
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="storegrp" />

                                                <asp:Button ID="butGroupSubmit" runat="server" OnClick="butGroupSubmit_Click" Text="Submit" ValidationGroup="storegrp" ToolTip="Click To Submit" TabIndex="3" CssClass="btn btn-primary" />
                                                <asp:Button ID="btnshorptitemgrp" Visible="false" runat="server" Text="Report" ToolTip="Click To Show Report" TabIndex="4" CssClass="btn btn-info" OnClick="btnshorptitemgrp_Click" />
                                                <asp:Button ID="Button3" runat="server" Text="Report" ToolTip="Click To Show Report" TabIndex="4" CssClass="btn btn-info" OnClick="Button3_Click" />
                                                <asp:Button ID="butGroupCancel" runat="server" OnClick="butGroupCancel_Click" Text="Cancel" ToolTip="Click To Reset" TabIndex="5" CssClass="btn btn-warning" />

                                            </div>

                                            <asp:Panel ID="pnlItemGroupMaster" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvItemGroupMaster" runat="server">
                                                        <EmptyDataTemplate>
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Item Group Master</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Group Name
                                                                        </th>
                                                                        <th>Short Name
                                                                        </th>
                                                                        <th>Type
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
                                                                    <asp:ImageButton ID="btnEditGroup" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("MIGNO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditGroup_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MIGNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ITEM_TYPE")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                    <%--<div class="vista-grid_datapager text-center">
                                                        <asp:DataPager ID="dpPagerGroupMaster" runat="server" PagedControlID="lvItemGroupMaster"
                                                            PageSize="10" OnPreRender="dpPagerGroupMaster_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>--%>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="SuGrpDep">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updatePanel4"
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
                                <asp:UpdatePanel ID="updatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div class="box-body">
                                            <asp:Panel ID="Panel7" runat="server">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Add/Edit Sub Group Depreciation Entry</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div6" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Item Sub Group</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlItemSubgroupdep" AppendDataBoundItems="true" runat="server"
                                                                CssClass="form-control" TabIndex="1" ToolTip="Select Item Sub Group Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlItemSubgroupdep"
                                                                Display="None" ErrorMessage="Please Select Item Sub Group Name" ValidationGroup="storedepre"
                                                                InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div7" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Depreciation %</label>
                                                            </div>
                                                            <asp:TextBox ID="txtDepper" MaxLength="3" runat="server" CssClass="form-control" TabIndex="2" ToolTip="Enter Depreciation Percentage"
                                                                onkeypress="return jsDecimals(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDepper"
                                                                Display="None" ErrorMessage="Please Enter Depreciation Percentage" ValidationGroup="storedepre"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftpde" runat="server" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtDepper">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div8" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Depreciation From Date</label>
                                                            </div>
                                                            <div class="input-group date">
                                                                <div class="input-group-addon">
                                                                    <i class="fa fa-calendar text-blue" id="Image1"></i>
                                                                </div>
                                                                <%--<div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                                                <asp:TextBox ID="txtDepdate" runat="server" CssClass="form-control" ToolTip="Enter Depreciation From Date "></asp:TextBox>
                                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true"
                                                                    EnableViewState="true" PopupButtonID="Image1" TargetControlID="txtDepdate" Format="dd/MM/yyyy">
                                                                </ajaxToolKit:CalendarExtender>
                                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptNegative="Left"
                                                                    DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true"
                                                                    OnInvalidCssClass="errordate" TargetControlID="txtDepdate" ClearMaskOnLostFocus="true">
                                                                </ajaxToolKit:MaskedEditExtender>
                                                                <%-- <asp:RequiredFieldValidator runat="server" ID="rfvDOfBirth" ControlToValidate="txtDepdate" Display="None"
                                                                    ErrorMessage="Please Enter Depreciation From Date." ValidationGroup="storedepre" />--%>
                                                                <ajaxToolKit:MaskedEditValidator ID="mevDate" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtDepdate"
                                                                    IsValidEmpty="false" ErrorMessage="Please Enter Valid Depreciation From Date [dd/MM/yyyy] format"
                                                                    Display="None" SetFocusOnError="true" EmptyValueMessage="Please Enter Depreciation From Date"
                                                                    Text="*" ValidationGroup="storedepre" InvalidValueMessage="Depreciation From Date Is Invalid (Enter In dd/MM/yyyy Format)"></ajaxToolKit:MaskedEditValidator>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                            <div class="col-12 btn-footer">
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List"
                                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="storedepre" />

                                                <asp:Button ID="btnSubmititemsubgroup" runat="server" OnClick="btnSubmititemsubgroup_Click"
                                                    Text="Submit" ValidationGroup="storedepre" CssClass="btn btn-primary" TabIndex="4" ToolTip="Click To Submit" />
                                                <asp:Button ID="btnCancelitemsubgroup" Text="Cancel" runat="server" CssClass="btn btn-warning" TabIndex="5" ToolTip="Click To Reset"
                                                    OnClick="btnCancelitemsubgroup_Click" />
                                            </div>

                                            <asp:Panel ID="Panel8" runat="server">
                                                <div class="col-12">
                                                    <asp:ListView ID="lvDepreciation" runat="server">
                                                        <EmptyDataTemplate>
                                                            <div class="text-center">
                                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" />
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Sub Group Depreciation Entry</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Action
                                                                        </th>
                                                                        <th>Item Sub Group 
                                                                        </th>
                                                                        <th>Depreciation %
                                                                        </th>
                                                                        <th>Depreciation Date
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
                                                                    <asp:ImageButton ID="btnEditDepreciarion" runat="server" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("DEP_MAP_ID") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditDepreciarion_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MISGNAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DEPR_PER ")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DEPR_FROM_DATE")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>

                                                    </asp:ListView>
                                                    <%--<div class="vista-grid_datapager text-center">
                                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvItemSubGroupMaster"
                                                            PageSize="10" OnPreRender="DataPager1_PreRender">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                                <asp:NumericPagerField ButtonType="Link" ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                                <asp:NextPreviousPagerField LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </div>--%>
                                                </div>
                                            </asp:Panel>
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

    <div id="divMsg" runat="server"></div>
    <script type="text/javascript">
        function LowercaseToUppercase(txt) {
            var textValue = txt.value;
            txt.value = textValue.toUpperCase();

        }

        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    //Here is where respond with 0 o 1.
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        alert("Please Enter Numeric Value");
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }

    </script>

    <script type="text/javascript">
        var selected_tab = 1;
        debugger;
        $(function () {
            var tabs = $("#tabs").tabs({
                select: function (e, i) {
                    selected_tab = i.index;
                }
            });
            selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
            tabs.tabs('select', selected_tab);
            $("form").submit(function () {
                $("[id$=selected_tab]").val(selected_tab);
            });
        });

    </script>

</asp:Content>
