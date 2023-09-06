<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Activity_Registration.aspx.cs" Inherits="ACADEMIC_StudentAchievement_Activity_Registration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>

        $(document).ready(function () {
            var table = $('#tblActiveActivityEvent').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                //width: "100%",
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                   return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                           nodereturn += $(this).html();
                       });
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
               var arr = [0, 9];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                           nodereturn += $(this).html();
                       });
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
                var table = $('#tblActiveActivityEvent').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    //width: "100%",
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                    return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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
                var arr = [0, 9];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#tblActiveActivityEvent').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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

    <div class="row" runat="server" id="divactivity">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <%--<asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>--%>
                        <label>ACTIVITY REGISTRATION</label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12 mt-3">
                        <div class="sub-heading">
                            <h5>Active Activity List</h5>
                        </div>
                        <asp:Panel ID="pnlEvent" runat="server">
                            <asp:ListView ID="lvActivityList" runat="server" ItemPlaceholderID="itemPlaceholder">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" id="tblActiveActivityEvent">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>
                                                    <%--<asp:CheckBox ID="chkAll" runat="server" onclick="ChecktotAll(this);" />--%>
                                                    Select <%--All--%>
                                                </th>
                                                <%-- <th style="width: 10%;">Select</th>--%>
                                                <th>Club Name</th>
                                                <th>Activity Type</th>
                                                <th>Activity Title</th>
                                                <th>Activity Start Date</th>
                                                <th>Activity End Date</th>
                                                <th>Time</th>
                                                <th>Registration Last Date</th>
                                                <th>Registered/Total Capacity</th>
                                                <%-- <th>Club Name</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkAccept" runat="server" />
                                            </td>
                                            <%--   <td>
                                                <asp:HiddenField ID="hdnCreateEventId" runat="server" Text='<%# Eval("CREATE_EVENT_ID") %>' />
                                            </td>--%>
                                            <td>
                                                <asp:Label ID="lblClubName" runat="server" Text='<%# Eval("CLUB_NAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnCreateEventId" runat="server" Value='<%# Eval(" CREATE_EVENT_ID") %>' />
                                                <asp:HiddenField ID="hdnClubNo" runat="server" Value='<%# Eval(" CLUB_ACTIVITY_NO") %>' />
                                            </td>


                                            <td>
                                                <asp:Label ID="lblActivitName" runat="server" Text='<%# Eval("ACTIVITY_NAME") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblActivityTitle" runat="server" Text='<%# Eval("ACTIVITY_TITLE") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblSdate" runat="server" Text='<%# Convert.ToDateTime(Eval("ACTIVITY_START_DATE")).ToString("d")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblEdate" runat="server" Text='<%#Convert.ToDateTime (Eval("ACTIVITY_END_DATE")).ToString("d")%>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblTime" runat="server" Text='<%# Eval("TIME") %>'></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblRegistrationDate" runat="server" Text='<%#Convert.ToDateTime(Eval("REGISTRATION_LAST_DATE")).ToString("d")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("REGISTERED_CAPACITY") +"/"+Eval("TOTAL_CAPACITY")%>'></asp:Label>
                                            </td>

                                        </tr>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <asp:Panel ID="pnlEventNoDataMsg" runat="server" Visible="false">
                            <h5 style="text-align: left; font-size: 15px; font-weigth: bold">No records to display...</h5>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-primary" ValidationGroup="Academic" OnClientClick="return ChecktotAll();" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-warning" CausesValidation="false" OnClientClick="retuen valiCancelbutton(); " OnClick="btnCancel_Click">Cancel</asp:LinkButton>

                    </div>

                    <div class="col-12 mt-3" id="divRegActList" runat="server">
                        <div class="sub-heading">
                            <h5>Registerd Activity List</h5>
                        </div>
                        <asp:Panel ID="pnlRegstlst" runat="server">
                            <asp:ListView ID="ListView2" runat="server" ItemPlaceholderID="itemPlaceholder">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" id="tblActivity1">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <%-- <th style="width: 10%;">Select</th>--%>
                                                <th>Action</th>
                                                <th>Club Name</th>
                                                <th>Activity Type</th>
                                                <th>Activity Title</th>
                                                <th>Activity Start Date</th>
                                                <th>Activity End Date</th>
                                                <th>Activity Time</th>
                                                <th>Registration Last Date</th>
                                                <th>Registration Date</th>
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
                                            <%--<asp:ImageButton ID="btnDeleteActvity" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#Eval("ACT_ID") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteActvity_Click" />--%>

                                            <asp:ImageButton ID="btnDeleteActvity" runat="server" ImageUrl="~/Images/delete.png" CommandArgument='<%#Eval("ACT_ID")+","+Eval("ACTIVITY_START_DATE") %>'
                                                AlternateText="Delete Record" ToolTip="Delete Record" OnClick="btnDeleteActvity_Click" />

                                        </td>
                                        <td>
                                            <%# Eval("CLUB_NAME") %>

                                        </td>

                                        <td>
                                            <%# Eval("ACTIVITY_TYPE") %>
                                        </td>

                                        <td>
                                            <%# Eval("ACTIVITY_TITLE") %>
                                        </td>

                                        <td>
                                            <%# Convert.ToDateTime(Eval("ACTIVITY_START_DATE")).ToString("d")%>
                                        </td>

                                        <td>
                                            <%#Convert.ToDateTime (Eval("ACTIVITY_END_DATE")).ToString("d")%>
                                        </td>

                                        <td>
                                            <%# Eval("ACTIVITY_TIME") %>
                                        </td>

                                        <td>
                                            <%#Convert.ToDateTime(Eval("REGISTRATION_LAST_DATE")).ToString("d")%>
                                        </td>

                                        <td>
                                            <%#Convert.ToDateTime(Eval("REGISTERED_DATE")).ToString("d")%>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>

                        <asp:Panel ID="pnlRegstlstNoDataMsg" runat="server" Visible="false">
                            <h6 style="text-align: left; font-size: 15px; font-weigth: bold">No records to display...</h6>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--CheckBox Script--%>
    <script>

        function ChecktotAll(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            // SumTotal();
                            // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                            // sum += parseFloat(j);
                            if (e.disabled == false) {
                                e.checked = true;
                            }
                        }
                        else {
                            if (e.disabled == false) {
                                e.checked = false;
                                headchk.checked = false;
                            }
                        }

                        // x = sum.toString();
                    }

                }
                if (headchk.checked == true) {
                    // SumTotal();
                }
                else {
                    // SumTotal();
                }
            }
            catch (err) {
                //alert("Please Select Event");
            }
        }
    </script>



    <%--    Cancel Button Check box Script--%>
    <%--  <script>
        function valiCancelbutton() {
            var chk1 = $('#chkAll').val('checked');
            var chk2 = $('#chkAccept').val('checked');
            if (chk1.checked == true) {
                chk1.checked == false;
            }
            else if (chk2.checked == true) {
                chk2.checked == false;
            }
        }
    </script>--%>



    <script>
        $(document).ready(function () {
            var table = $('#tblActivity1').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                width: "100%",
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [0, 9];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblActivity1').DataTable().column(idx).visible();
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
                   return $('#tblActivity1').DataTable().column(idx).visible();
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
                           nodereturn += $(this).html();
                       });
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
               var arr = [0, 9];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#tblActivity1').DataTable().column(idx).visible();
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
                           nodereturn += $(this).html();
                       });
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
                var table = $('#tblActivity1').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    width: "100%",
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0, 9];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblEvent').DataTable().column(idx).visible();
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
                    return $('#tblActivity1').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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
                var arr = [0, 9];
                if (arr.indexOf(idx) !== -1) {
                    return false;
                } else {
                    return $('#tblActivity1').DataTable().column(idx).visible();
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
                            nodereturn += $(this).html();
                        });
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
        $(document).on('click', 'input[type="checkbox"]', function () {
            $('input[type="checkbox"]').not(this).prop('checked', false);
        });
    </script>
</asp:Content>

