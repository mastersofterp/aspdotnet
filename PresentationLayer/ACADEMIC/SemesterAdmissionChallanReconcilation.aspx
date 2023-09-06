<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SemesterAdmissionChallanReconcilation.aspx.cs" Inherits="ACADEMIC_SemesterAdmissionChallanReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <style>
        .select2-container .select2-selection--single .select2-selection__rendered {
    display: block;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    width: 120px;
}
    </style>
     <style>
        #ctl00_ContentPlaceHolder1_pnlChallan .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        /*.modal-body {
            height: calc(100vh - 126px);
            overflow-y: scroll;
        }

        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
            position: fixed;
        }

        .fixed-div {
            width: 100%;
            position: fixed;
            z-index: 1051; 
            background: #fff;
            box-shadow: 0 5px 20px 4px rgba(0,0,0,.1);
        }*/
        /*.dataTables_scrollHeadInner {
            width: max-content !important;
        }*/

        .modalPopup {
            background-color: #fff;
            border: 1px solid #eee;
            box-shadow: 0 3px 9px rgba(0,0,0,.5);
            padding: 10px;
        }

        .modal-dialog1 {
            width: 545px;
            /*margin: 30px auto;*/
            position: relative;
            /*width: auto;*/
            margin: 10px;
            transition: transform .3s ease-out,-webkit-transform .3s ease-out,-o-transform .3s ease-out;
            transform: translate(0,0);
        }

        .modal-body1 {
            position: relative;
        }

        .modal-content {
            width: 630px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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
                var table = $('#tableid').DataTable({
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
                                    return $('#tableid').DataTable().column(idx).visible();
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
                                                return $('#tableid').DataTable().column(idx).visible();
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
                                                return $('#tableid').DataTable().column(idx).visible();
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
                    var table = $('#tableid').DataTable({
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
                                        return $('#tableid').DataTable().column(idx).visible();
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
                                                   return $('#tableid').DataTable().column(idx).visible();
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
                                                   return $('#tableid').DataTable().column(idx).visible();
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">CHALLAN RECONCILIATION</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>School</label>
                                        </div>
                                        <asp:ListBox ID="lstbxSchool" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        <%--<asp:DropDownList ID="ddlSchool" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSchool_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="lstbxSchool"
                                            Display="None" ErrorMessage="Please Select School." SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." SetFocusOnError="true" ValidationGroup="Show"
                                            InitialValue="0" />--%>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label>Payment Mode</label>
                                        </div>
                                        <asp:DropDownList ID="ddlPaymode" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlPaymode_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="DD"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Cheque"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="NEFT/RTGS"></asp:ListItem>
                                            <asp:ListItem Value ="4" Text="Challan"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label>Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="True" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                           <%-- <asp:ListItem Value="1" Text="All"></asp:ListItem>--%>
                                            <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Pending"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                   
                                   
                                </div>
                            </div>
                            <%-- <div class="col-12">
                                <asp:ListView ID="lvSearchResults" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>Chalan Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>Challan No.
                                                        </th>
                                                        <th>Challan Date
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Pay Type
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input id="rdoSelectRecord" value='<%# Eval("DCR_NO") %>' name="Receipts" type="radio"
                                                    onclick="ShowRemark(this);" /><asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td>
                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TITLE") %>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAY_MODE_CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>--%>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show"
                                    TabIndex="10" ValidationGroup="Show" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="6" Text="Submit" ToolTip="Click to Submit" Visible="false"
                                    class="btn btn-primary" ValidationGroup="Submit" />                                
                                 <asp:Button ID="btnApproved" runat="server" Text="Submit" OnClick="btnApprove_Click" CssClass="btn btn-info" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" CssClass="btn btn-warning" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                            </div>


                            <div class="col-md-12">
                                <asp:Panel ID="pnlChallan" runat="server" Visible="false">
                                    <asp:ListView ID="lvChallan" runat="server" OnItemDataBound="lvChallan_ItemDataBound">

                                        <LayoutTemplate>
                                            <%-- <div class="tableFixHead">--%>
                                            <table class="table table-hover table-bordered table-striped nowrap" id="tableid" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" ToolTip="Select all" runat="server" AutoPostBack="false" onclick="totAllSubjects(this);" />Select All
                                                        </th>
                                                        <th>STUDNAME
                                                        </th>
                                                        <th>ENROLLMENT NO. 
                                                        </th>
                                                        <th>PROGRAM
                                                        </th>
                                                        <th>SEMESTER
                                                        </th>
                                                        <th>EMAILID
                                                        </th>
                                                        <th>MOBILENO
                                                        </th>
                                                        <th>RECIEPT TITLE
                                                        </th>
                                                        <th>RECIEPT NO
                                                        </th>
                                                        <th>RECIEPT DATE
                                                        </th>
                                                        <th>PAYMENT MODE
                                                        </th>
                                                        <th>BANK NAME
                                                        </th>
                                                        <th>
                                                            DDNO
                                                        </th>
                                                        <th>
                                                            CHEQUE NO
                                                        </th>
                                                        <th>
                                                            UTR No
                                                        </th>
                                                        <th>
                                                            INSTALLMENT NO
                                                        </th>
                                                        <th>
                                                            INSTALLMENT AMOUNT
                                                        </th>
                                                        <th>TOTAL AMOUNT
                                                        </th>
                                                        <th>RECEIVED AMOUNT
                                                        </th>
                                                        <th>PREVIEW
                                                        </th>
                                                        <th>ALLOT STATUS
                                                        </th>
                                                        <th>REMARK
                                                        </th>
                                                        <th>Print</th> 
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>

                                            </table>
                                            <%--</div>--%>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkapproved" runat="server" OnCheckedChanged="chkCourseno_CheckedChanged" />
                                                 <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%--asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("STUDNAME") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                                        OnClick="lnkId_Click"></asp:LinkButton>--%>
                                                    <%# Eval("STUDNAME") %>
                                                    
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PROGRAM")%>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENTMOBILE")%>
                                                </td>

                                                <td>
                                                    <%# Eval("RECIEPT_TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REC_NO")%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblRecDT" runat="server" Text='<%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>'></asp:Label>                                                   
                                                </td>
                                                <td>
                                                    <%# Eval("PAYMENT_MODE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("BANKNAME")%>
                                                </td>
                                                 <td>    
                                                    <%--<%# Eval("DDNO")%>--%>
                                                     <asp:Label ID="lblStatus" Text='<%# (Convert.ToInt32(Eval("DDNO") )== 0 ?  "NA" : Eval("DDNO") )%>' runat="server"></asp:Label>
                                                </td>
                                                 <td>
<%--                                                    <%# Eval("CHEQUENO")%>--%>
                                                      <asp:Label ID="Label1" Text='<%# (Convert.ToInt32(Eval("CHEQUENO") )== 0 ?  "NA" : Eval("CHEQUENO") )%>' runat="server"></asp:Label>
                                                </td>
                                                <td>
<%--                                                    <%# Eval("CHEQUENO")%>--%>
                                                      <asp:Label ID="Label2" Text='<%# ((Eval("TRANSACTIONID") )== "" ?  "NA" : Eval("TRANSACTIONID") )%>' runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("INSTALMENT_NO")%>
                                                    <asp:HiddenField ID="hdninstallmentno" runat="server" Value='<%# Eval("INSTALMENT_NO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("INSTALLMENT_AMOUNT")%>
                                                </td>

                                                <td>
                                                    <%# Eval("TOTAL_AMT")%>
                                                </td>
                                                <td>
                                                     <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("TOTAL_AMT")%>'/>
                                                    <asp:HiddenField ID="hdnTotalAmount" runat="server" Value='<%#Eval("TOTAL_AMT")%>' />
                                                </td>
                                                <td>
                                                    <asp:UpdatePanel ID="updPreview" runat="server">
                                                        <%--<img src="../Images/view2.png" />--%>
                                                        <ContentTemplate>
                                                            <asp:ImageButton ID="imgbtnPrevDoc" runat="server" CommandArgument='<%# Eval("FILENAME_SLIP") %>' data-target="#PassModel" data-toggle="modal" Height="20px" ImageUrl="../Images/search.png" Text="Preview" ToolTip='<%# Eval("FILENAME_SLIP") %>' OnClick="imgbtnPrevDoc_Click" Width="20px" Visible='<%# Convert.ToString(Eval("FILENAME_SLIP"))==string.Empty?false:true %>' />
                                                            <asp:Label ID="lblPreview" Text='<%# Convert.ToString(Eval("FILENAME_SLIP"))==string.Empty ?"Preview is not available":"" %>' runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                        </ContentTemplate>
                                                        <%--<Triggers>
                                                                <asp:PostBackTrigger ControlID="imgbtnPrevDoc"/>
                                                            </Triggers> --%>  
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatuslv" runat="server" AppendDataBoundItems="True" CssClass="form-control w-100px" data-select2-enable="true">
                                            <asp:ListItem Value="0" Text="Please Select"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Pending"></asp:ListItem>
                                        </asp:DropDownList>
                                                    <asp:HiddenField ID="hdntecher" runat="server" />
                                                    <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("APPROVED_STATUS")%>' />
                                                </td>
                                                <td>
                                                   <%-- <asp:TextBox ID="txtRemark" runat="server"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtRemark" Rows="2" TextMode="MultiLine" Width="250px" MaxLength="400"
                                        runat="server" />
                                                      <asp:HiddenField ID="hdnRemark" runat="server" Value='<%#Eval("REMARK")%>' />
                                                </td>
                                                <td>
                                            <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/Images/print.png" ToolTip='<%# Eval("IDNO")%>' CausesValidation="False"
                                                visible='<%# Eval("APPROVED_STATUS").ToString().Equals("APPROVED")?true:false %>' />
                                                    

                                                    <%--<asp:Label ID="label" Text='<%# Eval("APPROVED_STATUS")%>' visible='<%# Eval("APPROVED STATUS").ToString().Equals("APPROVED")?true:false %>' runat="server"></asp:Label>--%>
                                        </td>
                                            </tr>
                                        </ItemTemplate>
                                        <%--<FooterTemplate>
                                        </tbody></table>
                                    </FooterTemplate>--%>
                                    </asp:ListView>
                                </asp:Panel>

                            </div>

                             <div class="col-12">
                                <asp:ListView ID="lvSearchResults" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div class="sub-heading">
                                                <h5>Chalan Search Results</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblSearchResults">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Select
                                                        </th>
                                                        <th>Challan No.
                                                        </th>
                                                        <th>Challan Date
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>Pay Type
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input id="rdoSelectRecord" value='<%# Eval("DCR_NO") %>' name="Receipts" type="radio"
                                                    onclick="ShowRemark(this);" /><asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                                <asp:HiddenField ID="hidDcrNo" runat="server" Value='<%# Eval("DCR_NO") %>' />
                                                <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            </td>
                                            <td>
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td>
                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TITLE") %>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAY_MODE_CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
            <div id="divMsg" runat="server">
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lvChallan" />
        </Triggers>
    </asp:UpdatePanel>

    <ajaxToolKit:ModalPopupExtender ID="mpeViewDocument" BehaviorID="mpeViewDocument" runat="server" PopupControlID="pnlmpopup"
        TargetControlID="lnkPreview" CancelControlID="btnClose" BackgroundCssClass="modalBackground"
        OnOkScript="ResetSession()">
    </ajaxToolKit:ModalPopupExtender>
    <asp:LinkButton ID="lnkPreview" runat="server" CommandArgument='<%# Eval("FILENAME_SLIP") %>'></asp:LinkButton>

    <div class="modal fade" id="PassModel">
        <div class="modal-dialog">
            <div class="modal-content">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlmpopup" runat="server" CssClass="modalPopup">
                            <div class="modal-header">
                                <h4 class="modal-title">Document</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <iframe runat="server" width="550px" height="450px" id="iframeView"></iframe>
                                </div>
                            </div>

                            <!-- Modal footer -->
                            <div class="modal-footer" style="display: none">
                                <asp:Button ID="btncloseprev" runat="server" Text="Close" CssClass="btn btn-danger no" />
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btncloseprev" />
                          
                    </Triggers>
                     
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

   <%--  <script type="text/javascript">
         $(document).ready(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200
             });
         });
         var parameter = Sys.WebForms.PageRequestManager.getInstance();
         parameter.add_endRequest(function () {
             $('.multi-select-demo').multiselect({
                 includeSelectAllOption: true,
                 maxHeight: 200
             });
         });


    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true,
                    maxHeight: 200,
                    enableFiltering: true,
                    filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>
     <script>
         function selectAll(chk) {
             var totalCheckboxes = $('[id*=tableid] td input:checkbox').length;
             //alert(totalCheckboxes);
             for (var i = 0; i < totalCheckboxes; i++) {
                 if (chk.checked) {
                     document.getElementById("ctl00_ContentPlaceHolder1_lvChallan_ctrl" + i + "_chkapproved").checked = true;
                 }
                 else
                     document.getElementById("ctl00_ContentPlaceHolder1_lvChallan_ctrl" + i + "_chkapproved").checked = false;
             }
         }
    </script>
    <script>
    function totAllSubjects(headchk) {

                 var frm = document.forms[0]
                 for (i = 0; i < document.forms[0].elements.length; i++) {
                     var e = frm.elements[i];
                     if (e.type == 'checkbox') {
                         if (headchk.checked == true)
                         {
                             if( e.disabled==false){
                                 e.checked =true;
                             }
                         } 
                         else {
                             if( e.disabled==false)
                             {
                                 e.checked = false;
                                 headchk.checked = false;
                             }
                         }
                     }

                 }

    }
        </script>
</asp:Content>

