<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoomMaster.aspx.cs"
    Inherits="ACADEMIC_MASTERS_RoomMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hfdMandatory" runat="server" ClientIDMode="Static" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRoom"
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

    <%--===== Data Table Script =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#divsessionlist').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: true, // Added by Gaurav for Hide pagination
                lengthMenu: [
                [50, 100, 150, 500,1000,2000],
                 [50, 100, 150, 500,1000,2000,'All'],
                ],
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
                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                                            return $('#divsessionlist').DataTable().column(idx).visible();
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
                                            return $('#divsessionlist').DataTable().column(idx).visible();
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
                var table = $('#divsessionlist').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: true, // Added by Gaurav for Hide pagination
                    lengthMenu: [
            [50, 100, 150, 500,1000,2000],
              [50, 100, 150, 500,1000,2000,'All'],
                    ],
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
                                    return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                return $('#divsessionlist').DataTable().column(idx).visible();
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
                                                return $('#divsessionlist').DataTable().column(idx).visible();
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
    <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatMandat(val) {
            $('#rdMandatory').prop('checked', val);
        }

        function validate() {
            if (Page_ClientValidate()) {
                $('#hfdMandatory').val($('#rdMandatory').prop('checked'));
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
    <asp:UpdatePanel ID="updRoom" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div2" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="Room" runat="server">Create Room</asp:Label>
                            </h3>

                            <%--  <h5>
                                <asp:Label ID="lblDYtxtRoomCreate" runat="server"></asp:Label>
                            </h5>--%>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYtxtSchoolname" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1">
                                        </asp:DropDownList>
                                        <%--   <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>--%>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlCollege"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select College/School/Institute Name"
                                            SetFocusOnError="true" InitialValue="0" />


                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <%-- <label>Department</label>--%>
                                            <asp:Label ID="lblDYddlDeptName" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDept" runat="server" ControlToValidate="ddlDept"
                                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <%--<label>Floor</label>--%>
                                            <asp:Label ID="lblDYtxtFloor" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlFloorNo" runat="server" AppendDataBoundItems="true"
                                            TabIndex="3" data-select2-enable="true" OnSelectedIndexChanged="ddlFloorNo_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0"> Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvFloorNo" runat="server" ControlToValidate="ddlFloorNo"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Block."
                                            SetFocusOnError="true" InitialValue="-1" />--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFloorNo"
                                            Display="None" ErrorMessage="Please Select Floor" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Block Name</label>
                                            <asp:Label ID="lblBlock" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlBlockNo" runat="server" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="True"
                                            TabIndex="3" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBlockNo"
                                            Display="None" ErrorMessage="Please Select Block" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>

                                </div>

                            </div>
                            <div>
                                <div class="col-12  mt-3">
                                    <asp:Panel ID="pnlAssessment" runat="server">
                                            <asp:ListView ID="lvAssessment" runat="server">
                                                <LayoutTemplate>
                                                    <div class="table-responsive" style="height: 250px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                        <table class="table table-striped table-bordered nowrap " style="width: 100%;" id="MainLeadTable">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Sr No</th>
                                                                    <th>Room Name</th>
                                                                    <th>Room Capacity</th>
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
                                                    <tbody>
                                                        <tr>
                                                            <td style="text-align: center">
                                                                <%# Container.DataItemIndex + 1 %>
                                                                <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRoomName" runat="server" CssClass="form-control" Width="300px" placeholder="Please Enter" TabIndex="4"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="form-control"  Width="200px" placeholder="Please Enter" TabIndex="5"></asp:TextBox></td>
                                                             <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRoomCapacity"
                                                              ValidChars="1234567890" FilterMode="ValidChars" />
                                                             <td>
                                                                <asp:CheckBox ID="chkStatus" runat="server" TabIndex="6" /></td>


                                                            <%--placeholder="25"--%>
                                                        </tr>

                                                    </tbody>
                                                </ItemTemplate>
                                            </asp:ListView>
                                      
                                    </asp:Panel>
                                </div>


                            </div>
                            <div class="col-12 btn-footer">
                                 <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn btn-primary" Onclick="btnadd_Click" TabIndex="7" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                    TabIndex="8" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" />

                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvRoomMaster" runat="server" OnItemDataBound="lvRoomMaster_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Present Block/Room Details</h5>
                                            </div>
                                            <div>
                                                <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <%--                                                <table class="table table-striped table-bordered">--%>

                                                        <%-- <thead>--%>
                                                        <tr class="bg-light-blue">
                                                            <th>Edit </th>
                                                            <th>College</th>
                                                            <th>Department </th>
                                                            <th>Floor</th>
                                                            <th>Block Name</th>
                                                            <th>Room Name </th>
                                                            <th>Room Capacity</th>
                                                            <th>ActiveStatus</th>
                                                            <%-- <th>Sequence</th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>

                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("ROOMNO") %>' ImageUrl="~/Images/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>

                                                <td><%# Eval("COLLEGE")%></td>
                                                <%--<td><%# Eval("DEPARTMENT")%></td>  --%>
                                                <td><%# Eval("DEPARTMENT")%></td>
                                                <td><%# Eval("FLOORNAME")%></td>
                                                <td><%# Eval("BLOCKNAME")%></td>
                                                <td><%# Eval("ROOMNAME")%></td>
                                                <td><%# Eval("ROOMCAPACITY")%></td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblLockstatus" Font-Bold="true" Text='<%#Convert.ToString(Eval("ACTIVESTATUS"))=="1" ? "Active" : "Inactive"  %>' ForeColor='<%# (Convert.ToInt32(Eval("ACTIVESTATUS") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>


                        <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                            TargetControlID="div" PopupControlID="div"
                            OkControlID="btnOkDel" OnOkScript="okDelClick();"
                            CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
                        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
                            <div style="text-align: center; background-color: darkgray;">
                                <table>
                                    <tr>
                                        <td align="center">
                                            <img align="middle" src="images/warning.gif" alt="" /></td>
                                        <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                                            <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="Server">
    </div>



    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>




</asp:Content>
