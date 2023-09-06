<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="createexamname.aspx.cs" Inherits="Academic_createexamname" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExam"
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
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
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
                                return $('#mytable').DataTable().column(idx).visible();
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
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
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
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                var table = $('#mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
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
                                    return $('#mytable').DataTable().column(idx).visible();
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
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
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
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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


    <%--<script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            debugger;
            var frm = document.forms[0]

            alert(frm);
            alert(headchk.idx);
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
            if (headchk.type == 'checkbox') {

                if (headchk.checked == true) {

                    headchk.checked = true;
                }
                else {
                    headchk.checked = false;
                }
            }
            }

        }

    </script>--%>


    <script>
        function settimeslot(val) {

            $('#rdActivetimeslot').prop('checked', val);
            // $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));
        }

        function validateDegTyp() {
            //alert("he");
            $('#hftimeslot').val($('#rdActivetimeslot').prop('checked'));

            //var Slotname = $("[id$=txtSlotName]").attr("id");
            //var Slotname = document.getElementById(Slotname);
            //if (Slotname.value == 0) {
            //    alert('Please Enter Slot Name', 'Warning!');
            //    $(Slotname).focus();
            //    return false;
            //}

            //var fromtime = $("[id$=txtTimeFrom]").attr("id");
            //var fromtime = document.getElementById(fromtime);
            //if (fromtime.value == 0) {
            //    alert('Please Enter From Time', 'Warning!');
            //    $(fromtime).focus();
            //    return false;
            //}

            //var totime = $("[id$=txtTimeTo]").attr("id");
            //var totime = document.getElementById(totime);
            //if (totime.value.length == 0) {
            //    alert('Please Enter to Time', 'Warning!');
            //    //$(txtweb).css('border-color', 'red');
            //    $(totime).focus();
            //    return false;
            //}
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    // alert("hi");
                    validateDegTyp();
                });
            });
        });

    </script>

    <asp:UpdatePanel runat="server" ID="updExam">
        <ContentTemplate>
            <asp:HiddenField ID="hftimeslot" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">EXAM NAME CREATION</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Pattern</label>
                                        </div>
                                        <asp:DropDownList ID="ddlExamPattern" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" OnSelectedIndexChanged="ddlExamPattern_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-2" style="display: none;">
                                        <label>Status</label>

                                        <div class="switch form-inline">
                                            <input type="checkbox" id="rdActivetimeslot" name="switch" checked />
                                            <label data-on="Active" tabindex="4" class="newAddNew Tab" data-off="Inactive" for="rdActivetimeslot"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSeqNum" runat="server">
                                    <asp:ListView ID="lvExamHead" runat="server" OnItemDataBound="lvExamHead_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Sr No.</th>
                                                            <th>Short Name</th>
                                                            <th>Exam Name</th>
                                                            <th>Exam Type</th>
                                                            <th>Pattern Name</th>
                                                            <th>Status</th>

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
                                                    <asp:Label ID="lblExamNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%#Eval("EXAMNO")%>' Font-Bold="true" /></td>
                                                <td>
                                                    <asp:Label ID="lblFldName" runat="server" Text='<%#Eval("FLDNAME")%>' Font-Bold="true" /></td>
                                                <td>
                                                    <asp:TextBox ID="txtExamName" runat="server" Text='<%#Eval("EXAMNAME")%>' Width="80%" /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExamType" runat="server" data-select2-enable="true">
                                                        <asp:ListItem Value="0"> Please Select </asp:ListItem>
                                                        <asp:ListItem Text="Internal" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="External" Value="2"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hfdIntEx" runat="server" Value='<%# Eval("EXAMTYPE")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPatternName" runat="server" Text='<%#Eval("PATTERN_NAME")%>' Font-Bold="true" /></td>



                                                <td>
                                                    <%--  <input type="checkbox" id="rdActivetimeslot" name="switch" checked onchange="SelectAll(this);" />                                                    
                                                     <label data-on="Active" class="newAddNew Tab" data-off="Inactive" for="rdActivetimeslot"></label>   --%>
                                                    <asp:CheckBox runat="server" ID="Chkstatus" ToolTip='<%# Eval("STATUS")%>' />
                                                    <asp:Label ID="lblActive1" Text='<%# Eval("ACTIVESTATUS")%>' ForeColor='<%# Eval("ACTIVESTATUS").ToString().Equals("Active")?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>

                                                </td>
                                                <%-- <td class="switch form-inline">
                                                    <input type="checkbox" id="rdActivetimeslot" name="switch" checked onchange="SelectAll(this);" />

                                                    <label data-on="Active" class="newAddNew Tab" data-off="Inactive" for="rdActivetimeslot"></label>
                                                                                     
                                                </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12 btn-footer mt-4" runat="server" id="Divbttn">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" OnClientClick="return validateDegTyp();"
                                    OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancel_Click"
                                    CssClass="btn btn-warning" />
                            </div>


                            <div>
                            </div>
                            <div class="col-12">
                                <asp:Label ID="lblerror" runat="server" SkinID="Errorlbl" />
                                <asp:Label ID="lblmsg" runat="server" SkinID="lblmsg" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
