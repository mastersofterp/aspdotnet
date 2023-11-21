<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Str_Dead_Stock_Entry.aspx.cs" Inherits="STORES_Transactions_StockEntry_Str_Dead_Stock_Entry" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script type="text/javascript">
        //------------05/05/2022--start--------------
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                maxWidth: '100%',
                enableFiltering: true,
                filterPlaceholder: 'Search',
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    maxWidth: '100%',
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                });
            });
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {


            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)


            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                //   InitAutoCompl();
            }

        });




    </script>
    <script type="text/javascript">
        function AddItem() {

            document.getElementById('<%=PnlItem.ClientID%>').style.display = 'block';
            document.getElementById('<%=divAddItem.ClientID%>').style.display = 'none';
            // alert('a');
        }
    </script>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>
    <%-- <style type="text/css">
        #load {
            width: 100%;
            height: 100%;
            position: fixed;
            z-index: 9999; /*background: url("/images/loading_icon.gif") no-repeat center center rgba(0,0,0,0.25);*/
        }
    </style>--%>
    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }

    </script>

    <%--<asp:UpdatePanel ID="pnlFeeTable" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">STOCK ENTRY</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" />
                    </div>

                    <div id="divGRNEtry" runat="server" visible="false">
                        <asp:Panel ID="PnlSecurityPass" runat="server" Visible="true">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Stock Entry</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdednumber" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Dead Stock Number</label>
                                        </div>
                                        <asp:TextBox ID="txtDStoNumber" runat="server" CssClass="form-control" ToolTip="" ReadOnly="true"></asp:TextBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Stock Entry Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="Image1">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <%--  <div class="input-group-addon">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                                                </div>--%>
                                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control" ToolTip="Select Date" /><%--ValidationGroup="Store"--%>

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="true" EnableViewState="true"
                                                Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtIssueDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtIssueDate" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" EmptyValueMessage="Please Enter Valid Event Issue Date"
                                                ControlExtender="MaskedEditExtender5" ControlToValidate="txtIssueDate" IsValidEmpty="true"
                                                InvalidValueMessage="Issue Date is invalid [Enter In dd/MM/yyyy Format]" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Store" SetFocusOnError="true" />


                                        </div>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" ToolTip="Enter Remark"></asp:TextBox>
                                        <%-- <asp:HiddenField ID="hdnOthEdit" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnrowcount" runat="server" />--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPONum" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <%--<sup>*</sup>--%>
                                            <label>Selected PO's:<span style="color: #FF0000">*</span> </label>
                                        </div>
                                        <asp:TextBox ID="txtPONum" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" ToolTip="Enter Remark"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer" id="divAddItem" runat="server" visible="false">
                                <asp:Button ID="btnAddItem" runat="server" CssClass="btn btn-info" Text="Add Item" CausesValidation="true" OnClick="btnAddItem_Click" Visible="false" />
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PnlItem" runat="server" Visible="false">
                            <div class="col-12">
                                <div class="sub-heading">
                                    <h5>Add/Edit Item Details</h5>
                                </div>
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Item Name :</label>
                                        </div>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlItem"
                                            Display="None" ErrorMessage="Please Select Item Name." InitialValue="0" ValidationGroup="AddItem"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Qty :</label>
                                        </div>
                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" MaxLength="10" ToolTip="Enter Qty"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemQty"
                                            Display="None" ErrorMessage="Please Enter Qty." ValidationGroup="AddItem"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbtxtContNo" runat="server" ValidChars="0123456789"
                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtItemQty">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Rate</label>
                                        </div>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="form-control" MaxLength="10" ToolTip="Enter Rate"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="0123456789."
                                            FilterType="Custom" FilterMode="ValidChars" TargetControlID="txtRate">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divItemRemark" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtItemRemark" runat="server" CssClass="form-control" ToolTip="Enter Remark"></asp:TextBox>

                                    </div>

                                </div>
                            </div>
                            <div class="col-12 btn-footer">

                                <asp:Button ID="btnSaveItem" runat="server" CssClass="btn btn-info" Text="Add Item" ValidationGroup="AddItem" OnClick="btnSaveItem_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait..';" />
                                <%--OnClientClick="return GetPO()"--%>
                                <asp:Button ID="btnCancelItem" runat="server" Visible="false" CssClass="btn btn-warning" Text="Cancel" CausesValidation="true" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddItem" />

                            </div>

                        </asp:Panel>
                        <div class="form-group col-lg-3 col-md-6 col-12" id="divauto" runat="server" visible="false">
                            <div class="label-dynamic">
                                <sup></sup>
                                <label>Check Auto Serial Number</label>
                            </div>
                            <asp:CheckBox ID="chkAutoSerial" runat="server" CssClass="form-control" Checked="false" TabIndex="5" AutoPostBack="true" OnCheckedChanged="chkAutoSerial_CheckedChanged" />

                        </div>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvitem" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Item List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit </th>
                                                        <th>Delete </th>
                                                        <th>Item Name</th>
                                                        <th>Qty. </th>
                                                        <th>Rate</th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("ITEM_NO") %>'
                                                    ImageUrl="~/images/edit.png" OnClick="btnEdit_Click2" ToolTip="Edit Record" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ITEM_NO") %>'
                                                    ImageUrl="~/images/delete.png" ToolTip="Delete Record" OnClick="btnDelete_Click1" />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblItemName" Text='<%# Eval("ITEM_NAME")%>' CssClass="form-control"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdItemNo" Value='<%# Eval("ITEM_NO")%>' />

                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblItemQty" CssClass="form-control" Text='<%# Eval("QTY")%>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblItemRate" Text='<%# Eval("ITEM_RATE")%>' CssClass="form-control"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlitems" runat="server">
                            <div class="col-12 mt-5">
                                <asp:ListView ID="lvitems" runat="server" OnItemDataBound="lvitems_DataBound">
                                    <LayoutTemplate>
                                        <div>
                                            <div class="sub-heading">
                                                <h5>Item Serial Number</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="item_serial">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Sr.No.</th>
                                                        <th>Item Name </th>
                                                        <th>Qty.  </th>
                                                        <th>Rate</th>
                                                        <th id="Th2" runat="server">Serial Number</th>
                                                        <th>Technical Specification</th>
                                                        <th runat="server" id="thQty" visible="false">Quality & Qty Details</th>
                                                        <th id="Th3" runat="server">Department</th>
                                                        <th>Location </th>
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
                                                <asp:Label ID="lblitemSrNo" runat="server" Text='<%# Container.DataItemIndex + 1%>' CssClass="form-control"></asp:Label>
                                                <asp:HiddenField runat="server" ID="hdnDSTK_ENTRY_ID" Value='<%# Eval("DSTK_ENTRY_ID")%>' />
                                                <asp:HiddenField runat="server" ID="hditemSrNo" Value='<%# Eval("ITEM_SRNO")%>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="lblItemName" Text='<%# Eval("ITEM_NAME")%>' CssClass="form-control" ReadOnly="true" MaxLength="100"></asp:TextBox>
                                                <asp:HiddenField runat="server" ID="hdItemNo" Value='<%# Eval("ITEM_NO")%>' />
                                                <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("DSTK_ENTRY_ID")%>' />
                                                <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("ITEM_SRNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblItemQty" CssClass="form-control" Text='<%# Eval("QTY")%>'></asp:Label>

                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblItemRate" Text='<%# Eval("ITEM_RATE")%>' CssClass="form-control"></asp:Label>


                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSerialNo" runat="server" Text='<%# Eval("DSR_NUMBER")%>'
                                                    CssClass="form-control" MaxLength="64" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecification" runat="server" Text='<%# Eval("TECH_SPEC")%>'
                                                    CssClass="form-control" MaxLength="150" />

                                            </td>
                                            <td runat="server" id="thQty1" visible="false">
                                                <asp:TextBox ID="txtQtySpec" runat="server" Text='<%# Eval("QUALITY_QTY_SPEC")%>'
                                                    CssClass="form-control" MaxLength="150" />

                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Text="Please select Issue Slip No." Value="0"></asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddllocation" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Text="Please select Issue Slip No." Value="0"></asp:ListItem>
                                                </asp:DropDownList>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </asp:Panel>

                        <div class=" col-12 btn-footer" id="divbtn" runat="server" style="text-align: center">
                            <asp:Button ID="btnAddNew2" runat="server" Text="Add New" CssClass="btn btn-primary" OnClick="btnAdNew_Click" Visible="false" />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" ValidationGroup="Store" OnClick="btnSubmit_Click" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait..';" />
                            <asp:Button ID="btngenerateBarcode" runat="server" CssClass="btn btn-info" Text="Generate Barcode" OnClick="btngenerateBarcode_Click" />
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="valiSummary" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Store" DisplayMode="List" />
                        </div>
                    </div>

                    <asp:Panel ID="PnlitemDetail" runat="server" Visible="false">
                        <div class="col-12">
                            <asp:ListView ID="listitemDetail" runat="server">
                                <LayoutTemplate>
                                    <div>
                                        <div class="sub-heading">
                                            <h5>Stock Details</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                            <thead class="bg-light-blue">
                                                <th>Action </th>
                                                <th>Stock Number</th>
                                                <th>Creation Date</th>
                                                <th>Issue Date</th>
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
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit.png" ToolTip="Edit" CommandArgument='<%# Eval("DSTK_ENTRY_ID")%>'
                                                OnClick="btnEdit_Click1" />
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandArgument='<%# Eval("DSTK_ENTRY_ID") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDelete_Click"
                                                OnClientClick="javascript:return confirm('Are You Sure You Want To Delete This Record?')" Visible="false" />
                                        </td>
                                        <td>

                                            <asp:Label ID="lblstocknumber" runat="server" Text='<%# Eval("DEAD_STOCK_NO")%>'></asp:Label>
                                        </td>
                                        <td>

                                            <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CREATED_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label>
                                        </td>
                                        <td>

                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("ISSUE_DATE", "{0: dd-MM-yyyy}")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </asp:Panel>
                </div>

            </div>
        </div>
    </div>


    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">
        function Validate(crl) {
            if (document.getElementById('<%= txtIssueDate.ClientID %>').value != '') {
                var date_regex = /^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/;
                if (!(date_regex.test(document.getElementById('<%= txtIssueDate.ClientID %>').value))) {
                    alert("Issue Date Is Invalid (Enter In [dd/MM/yyyy] Format).");
                    return false;
                }
            }
        }
    </script>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#item_serial').DataTable({
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
                                return $('#item_serial').DataTable().column(idx).visible();
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
                                            return $('#item_serial').DataTable().column(idx).visible();
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
                                            return $('#item_serial').DataTable().column(idx).visible();
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
                var table = $('#item_serial').DataTable({
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
                                    return $('#item_serial').DataTable().column(idx).visible();
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
                                               return $('#item_serial').DataTable().column(idx).visible();
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
                                               return $('#item_serial').DataTable().column(idx).visible();
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


</asp:Content>
