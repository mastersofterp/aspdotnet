<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RoomInvigilator.aspx.cs"
    Inherits="ACADEMIC_MASTERS_Roominvigilator" %>

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
                [50, 100, 150, 500, 1000, 2000],
                 [50, 100, 150, 500, 1000, 2000, 'All'],
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
            [50, 100, 150, 500, 1000, 2000],
              [50, 100, 150, 500, 1000, 2000, 'All'],
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
                                <asp:Label ID="Room" runat="server">Room Invigilator Duty</asp:Label>
                            </h3>


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
                                            TabIndex="1">
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
                                            TabIndex="1" data-select2-enable="true" OnSelectedIndexChanged="ddlFloorNo_SelectedIndexChanged" AutoPostBack="true">
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
                                            TabIndex="1" OnSelectedIndexChanged="ddlBlockNo_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBlockNo"
                                            Display="None" ErrorMessage="Please Select Block" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>

                                </div>

                            </div>
                            <div>
                            </div>
                            <br />
                            <br />

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvRoomMaster" runat="server">
                                        <LayoutTemplate>

                                            <div>
                                                <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divsessionlist">
                                                    <thead class="bg-light-blue">
                                                        <%--<table class="table table-striped table-bordered">--%>

                                                        <%-- <thead>--%>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center">Sr No. </th>

                                                            <th style="text-align: center">
                                                                <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" TabIndex="1" />
                                                                Check
                                                        
                                                            <%--<th style="text-align: center">Check </th>--%>
                                                            <th style="text-align: center">Room No</th>
                                                            <th style="text-align: center">Room Name </th>
                                                            <th style="text-align: center">Room Capacity</th>
                                                            <th style="text-align: center">Required Invigilator</th>
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
                                                <td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:CheckBox ID="chk" runat="server" AutoPostBack="true" onclick="return chld(this);" />
                                                    <%--Enabled='<%# Eval("DEAN_LOCK").ToString().ToLower() == "true" ? false : true %>'--%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblRoomno" runat="server" Text='<%# Eval("ROOMNO")%>' ToolTip='<%# Eval("ROOMNO")%>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblRoomname" runat="server" Text='<%# Eval("ROOMNAME")%>' ToolTip='<%# Eval("ROOMNAME")%>' />
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:Label ID="lblRomCpt" runat="server" Text='<%# Eval("ROOMCAPACITY")%>' ToolTip='<%# Eval("ROOMCAPACITY")%>' />

                                                <td style="text-align: center">
                                                    <asp:TextBox ID="txtRequiredInvigilator" runat="server" TabIndex="1" placeholder="Please Enter" MaxLength="5" Text='<%# Eval("REQD_INVIGILATORS")%>'></asp:TextBox>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRequiredInvigilator"
                                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                                </td>


                                            </tr>

                                        </ItemTemplate>

                                    </asp:ListView>

                                    <br />
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" ValidationGroup="Submit"
                                            TabIndex="1" OnClick="btnSubmit_Click" />

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-warning" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Report" />

                                    </div>
                                </asp:Panel>
                            </div>
                        </div>


                        <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
                            TargetControlID="div" PopupControlID="div"
                            OkControlID="btnOkDel" OnOkScript="okDelClick();"
                            CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
                        <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
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
            //find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

        function enforceMaxLength(textbox, maxLength) {
            if (textbox.value.length > maxLength) {
                textbox.value = textbox.value.slice(0, maxLength);
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {


            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                    }
                    else if (e.checked == false) {
                        headchk.checked == false;
                    }
                    else {
                        e.checked = false;
                    }
                }
            }
        }
    </script>


       <script language="javascript" type="text/javascript">
       function chld(ths) {
         //  var hdchk = document.getElementById("ctl00_ContentPlaceHolder1_lvRoomMaster_chkheader"); 
 
           {
               if (ths.checked == false) {
                   document.getElementById("ctl00_ContentPlaceHolder1_lvRoomMaster_chkheader").checked = false;

                   //alert('Hii');
               }
               if (ths.checked == true) {

                   var updateButtons = $('#divsessionlist input[type=checkbox]');
                   var count =0, chkcount=0;
                   $('#divsessionlist tbody tr').each(function () {
                       count +=1;
                       var ck =  $(this).find('input[type=checkbox]').prop('checked');
                       if(ck)
                       {
                           chkcount +=1;
                       }
                   });
                   if(chkcount==count)
                   {
                       document.getElementById("ctl00_ContentPlaceHolder1_lvRoomMaster_chkheader").checked = true;
                   }
                   

               }
           }
       }

    </script>

    



   <%-- <script language="javascript" type="text/javascript">
        function chld(ths) {

        var checkboxes = $('#divsessionlist input[type=checkbox]');

        // Check if the clicked checkbox is the header checkbox
        if (ths === headerCheckbox) {
            checkboxes.prop('checked', ths.checked);
        } else {
            // Check the status of all checkboxes
            var allChecked = true;
            checkboxes.each(function () {
                if (!this.checked) {
                    allChecked = false;
                    return false; // exit the loop early if any checkbox is unchecked
                }
            });

            // Update the state of the header checkbox
            headerCheckbox.checked = allChecked;
        }
    }
</script>--%>


<%--    <script language="javascript" type="text/javascript">
        function chld(ths) {
            
      
        // Assuming all child checkboxes share a common parent container
        var columnCheckboxes = $('#divsessionlist input[type=checkbox]');

        // Iterate through each checkbox in the column
        for (var i = 0; i < columnCheckboxes.length; i++) {
            var checkbox = columnCheckboxes[i];

            // If any checkbox in the column is unchecked, uncheck the header checkbox
            if (checkbox.checked === false) {
                headerCheckbox.checked = false;
                return; // exit the loop early
            }
        }

        // If all checkboxes in the column are checked, check the header checkbox
        headerCheckbox.checked = true;
    }
    </script>--%>




</asp:Content>
