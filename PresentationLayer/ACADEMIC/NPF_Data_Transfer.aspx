<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NPF_Data_Transfer.aspx.cs" Inherits="ACADEMIC_NPF_Data_Transfer" ViewStateEncryptionMode="Always" EnableViewStateMac="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content!important;
        }

        #exceltodt .dataTables_scrollHeadInner {
            width: max-content!important;
        }
    </style>
    <asp:UpdatePanel ID="updpnUploadExcel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">--%>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>NPF Excel Upload</label>
                                        </div>
                                        <asp:FileUpload ID="fuexelUpload" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">

                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnUpload" runat="server" Text="Upload" TabIndex="1" ToolTip="Please Upload File" CssClass="btn btn-primary" OnClick="btnUpload_Click1" />
                                    </div>
                                   <div class="form-group col-lg-7 col-md-6 col-12">
                                        <div class=" note-div">
                                            <h5 class="heading">Note </h5>
                                            <p><i class="fa fa-star" aria-hidden="true"></i><span><span style="color: green; font-weight: bold">Only .xls or .xlsx extention is allowed</span></span></p>                                           
                                        </div>
                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSampleDownload" runat="server" Text="Sample Excel Download" ToolTip="Download Sample Excel" TabIndex="2" CssClass="btn btn-primary" OnClick="btnSampleDownload_Click" />
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="3" ToolTip="Submit" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnShow" runat="server" TabIndex="4" ToolTip="Show" Text="Show Existing Record" CssClass="btn btn-info" OnClick="btnShow_Click" />
                                     <asp:Button ID="btnShowDataDownload" runat="server" TabIndex="5" ToolTip="Show" Text="Existing Record Excel Download" CssClass="btn btn-info" OnClick="btnShowDataDownload_Click" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="6" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-warning" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                        <div>
                            <asp:UpdatePanel ID="updList" runat="server">
                                <ContentTemplate>

                                    <div class="col-12 mt-2">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvBinddata" runat="server" EnableModelValidation="True">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" id="tbl_updlist">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>SR.No</th>
                                                                <th>NPF_Discipline</th>
                                                                <th>NPF_Programme</th>
                                                                <th>NPF_Specialization</th>
                                                                <th>Discipline</th>
                                                                <th>Programme</th>
                                                                <th>SpecializationID</th>
                                                                <th>Specialization</th>
                                                                <th>School Name</th>
                                                                <th>Degree</th>
                                                                <th>Programme/Branch Name</th>
                                                                <th>Student Type</th>


                                                                <%-- <th style="text-align: center;">COURSE_TYPE</th>   --%>
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
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </td>
                                                        <td><%# Eval("NPF_Discipline")%></td>
                                                        <td><%# Eval("NPF_Programme")%></td>
                                                        <td><%# Eval("NPF_Specialization")%></td>
                                                        <td><%# Eval("[School Name]")%></td>
                                                        <td><%# Eval("Degree")%></td>
                                                        <td><%# Eval("Programme/Branch Name")%></td>
                                                        <td><%# Eval("COURSE_TYPE")%></td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>

                        <div id="exceltodt">
                            <asp:UpdatePanel ID="updpnlExceldata" runat="server">
                                <ContentTemplate>
                                        <div class="col-12">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvDatatableDisplay" runat="server" EnableModelValidation="True">
                                                    <LayoutTemplate>
                                                        <table class="table table-striped table-bordered nowrap display">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>SR.No</th>
                                                                    <th>NPF_Discipline</th>
                                                                    <th>NPF_Programme</th>
                                                                    <th>NPF_SpecializationID</th>
                                                                    <th>NPF_Specialization</th>
                                                                    <th>College_ID</th>
                                                                    <th>School Name</th>
                                                                    <th>DegreeNo</th>
                                                                    <th>Degree</th>
                                                                    <th>Branchno</th>
                                                                    <th>Programme/Branch Name</th>
                                                                    <th>Student Type</th>
                                                                    <th>IDTYPE</th>


                                                                    <%-- <th style="text-align: center;">COURSE_TYPE</th>   --%>
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
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </td>
                                                            <td><%# Eval("NPF_Discipline")%></td>
                                                            <td><%# Eval("NPF_Programme")%></td>
                                                            <td><%# Eval("NPF_SpecializationID")%></td>
                                                            <td><%# Eval("NPF_Specialization")%></td>
                                                            <td><%# Eval("College_ID")%></td>
                                                            <td><%# Eval("School Name")%></td>
                                                            <td><%# Eval("DegreeNo")%></td>
                                                            <td><%# Eval("Degree")%></td>
                                                            <td><%# Eval("Branchno")%></td>
                                                            <td><%# Eval("Programme/Branch Name")%></td>
                                                            <td><%# Eval("STUDENT_TYPE")%></td>
                                                            <td><%# Eval("IDTYPE")%></td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lvDatatableDisplay" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnSampleDownload" />
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnShowDataDownload" />
        </Triggers>
    </asp:UpdatePanel>


     <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tbl_updlist').DataTable({
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
                                    return $('#tbl_updlist').DataTable().column(idx).visible();
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
                                                return $('#tbl_updlist').DataTable().column(idx).visible();
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
                                                return $('#tbl_updlist').DataTable().column(idx).visible();
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
                    var table = $('#tbl_updlist').DataTable({
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
                                        return $('#tbl_updlist').DataTable().column(idx).visible();
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
                                                   return $('#tbl_updlist').DataTable().column(idx).visible();
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
                                                   return $('#tbl_updlist').DataTable().column(idx).visible();
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

