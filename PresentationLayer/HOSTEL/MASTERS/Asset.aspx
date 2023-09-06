<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Asset.aspx.cs" Inherits="Hostel_Masters_Asset" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript">
    //On Page Load
    $(document).ready(function () {
        $('#table2').DataTable();
    });
</script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //if (prm != null) {
        //    prm.add_endRequest(function (sender, e) {
        //        if (sender._postBackSettings.panelsToUpdate != null) {
        //            $('#table2').dataTable();
        //        }
        //    });
        //};

        function CheckInteger(event, element) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 8) return true;
            if ((charCode < 48 || charCode > 57))
                return false;
            return true;
        };

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>
    <<%--script type="text/javascript">
        //On Page Load
        $(document).ready(function () {
            $('#table3').DataTable();
        });
</script>--%>

    <script type="text/javascript">
        //On UpdatePanel Refresh
        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //if (prm != null) {
        //    prm.add_endRequest(function (sender, e) {
        //        if (sender._postBackSettings.panelsToUpdate != null) {
        //            $('#table3').dataTable();
        //        }
        //    });
        //};

        function CheckNumeric(event, obj) {
            var k = (window.event) ? event.keyCode : event.which;
            //alert(k);
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0) {
                obj.style.backgroundColor = "White";
                return true;
            }
            if (k > 45 && k < 58) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter numeric Value');
                obj.focus();
            }
            return false;
        }
        onkeypress = "return CheckAlphabet(event,this);"
        function CheckAlphabet(event, obj) {

            var k = (window.event) ? event.keyCode : event.which;
            if (k == 8 || k == 9 || k == 43 || k == 95 || k == 0 || k == 32 || k == 46 || k == 13) {
                obj.style.backgroundColor = "White";
                return true;

            }
            if (k >= 65 && k <= 90 || k >= 97 && k <= 122) {
                obj.style.backgroundColor = "White";
                return true;

            }
            else {
                alert('Please Enter Alphabets Only!');
                obj.focus();
            }
            return false;
        }
    </script>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#table3').DataTable({
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
                                return $('#table3').DataTable().column(idx).visible();
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
                                            return $('#table3').DataTable().column(idx).visible();
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
                                            return $('#table3').DataTable().column(idx).visible();
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
                var table = $('#table3').DataTable({
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
                                    return $('#table3').DataTable().column(idx).visible();
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
                                                return $('#table3').DataTable().column(idx).visible();
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
                                                return $('#table3').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">ASSET MASTER MANAGEMENT</h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#AssetType">Add/Edit Asset Type</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#Assets">Add/Edit Assets</a>
                            </li>
                        </ul>

                        <div class="tab-content" id="my-tab-content">
                            <div class="tab-pane active" id="AssetType">
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
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="col-12 col-md-12 col-lg-4">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Asset Type </label>
                                                            </div>
                                                            <asp:TextBox ID="txtAssetType" MaxLength="100" runat="server" CssClass="form-control" TabIndex="1" onkeypress="return isAlphabetKey(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvddlItemGroupName" runat="server" ControlToValidate="txtAssetType"
                                                                Display="None" ErrorMessage="Please Enter Asset Type" ValidationGroup="storesub"
                                                                 SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Asset Type Code </label>
                                                            </div>
                                                            <asp:TextBox ID="txtAssetCode" MaxLength="100" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtxtItemSubGroupName" runat="server" ControlToValidate="txtAssetCode"
                                                                Display="None" ErrorMessage="Please Enter Asset Type Code" ValidationGroup="storesub"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator runat="server" ID="regtxtItemSubGroupName" Display="None"
                                                                ValidationExpression="^[a-z|A-Z|]+[a-z|A-Z|0-9|\s]*" ControlToValidate="txtAssetCode"
                                                                ErrorMessage="Enterd Only Alphanumeric Characters For Sub Group Name " ValidationGroup="storesub"></asp:RegularExpressionValidator>

                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="storesub" />

                                                        <asp:Button ID="btnAssetSubmit" runat="server" OnClick="btnAssetSubmit_Click"
                                                            Text="Submit" ValidationGroup="storesub" CssClass="btn btn-primary" TabIndex="3" ToolTip="Click To Submit" />
                                                        <asp:Button ID="btnAssetcancel" Text="Cancel" runat="server" OnClick="btnAssetcancel_Click"
                                                            CssClass="btn btn-warning" TabIndex="4" ToolTip="Click To Reset" />
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-12 col-lg-8">
                                                    <asp:Repeater ID="lvItemAssetMaster" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Asset Type List</h5>
                                                            </div>
                                                            <table id="table2" class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit
                                                                        </th>
                                                                        <th>Asset Type
                                                                        </th>
                                                                        <th>Asset Code
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="btnAssetEdit" runat="server" ImageUrl="~/images/edit.png" CommandArgument='<%# Eval("ASSET_TYPE_NO") %>'
                                                                        AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnAssetEdit_Click" />&nbsp;
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ASSET_TYPE_NAME")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ASSET_TYPE_CODE")%>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody></table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="Assets">
                                <div>
                                    <asp:UpdateProgress ID="UpdprogReprint" runat="server" AssociatedUpdatePanelID="updBlock"
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
                                <asp:UpdatePanel ID="updBlock" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="col-12 col-md-12 col-lg-4">
                                                    <div class="row">
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Asset Type </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlAssetType" runat="server" TabIndex="1" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="valAssetType" runat="server" ControlToValidate="ddlAssetType"
                                                                Display="None" ErrorMessage="Please Select Asset Type." ValidationGroup="submit" SetFocusOnError="True" InitialValue="0" />
                                                        </div>

                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Asset Name </label>
                                                            </div>
                                                            <asp:TextBox ID="txtAssetName" runat="server" MaxLength="50" TabIndex="2" CssClass="form-control" onkeypress="return isAlphabetKey(event);" />
                                                            <asp:RequiredFieldValidator ID="valAssetName" runat="server" ControlToValidate="txtAssetName"
                                                                Display="None" ErrorMessage="Please Enter Asset Name." ValidationGroup="submit" SetFocusOnError="True" />
                                                            <asp:RegularExpressionValidator ID="revAssetName" runat="server"
                                                                ControlToValidate="txtAssetName" ErrorMessage="Enter Valid Asset Name" ValidationExpression="^([a-zA-Z ()\s]*)$"
                                                                ValidationGroup="submit" SetFocusOnError="true" Display="None"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Asset Quantity </label>
                                                            </div>
                                                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="3" TabIndex="3" CssClass="form-control" onkeypress="return CheckInteger(event,this);" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity"
                                                                Display="None" ErrorMessage="Please Enter Asset Quantity" ValidationGroup="submit" SetFocusOnError="True" />
                                                        </div>
                                                        <div class="form-group col-lg-12 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Is Available </label>
                                                            </div>
                                                            <asp:CheckBox runat="server" ID="chkAvail" />
                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" TabIndex="4" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-warning" TabIndex="5" />
                                                        <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-12 col-lg-8">

                                                    <asp:Panel ID="pnllist" runat="server">
                                                        <div class="col-md-12">
                                                            <asp:Repeater ID="lvAssets" runat="server">
                                                                <HeaderTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>List of Define Assets</h5>
                                                                    </div>
                                                                    <table id="table3" class="table table-striped table-bordered nowrap " style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Asset Type
                                                                                </th>
                                                                                <th>Asset Name
                                                                                </th>
                                                                                <th>Quantity</th>
                                                                                <th>Available</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>

                                                                        <td>
                                                                            <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("ASSET_NO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                OnClick="btnEdit_Click" TabIndex="5" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ASSET_TYPE_NAME") %>
                                            
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ASSET_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ASSET_QUANTITY") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("IS_AVAILABLE") %>
                                                                        </td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody></table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
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

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))

                return false;

            return true;
        }
    </script>
</asp:Content>

