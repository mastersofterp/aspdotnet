<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="OnlinePaymentLog.aspx.cs" Inherits="ACADEMIC_OnlinePaymentLog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
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
    </div>--%>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        /*=========File Upload CSS==========*/
        .logoContainer img {
            width: 50px;
            height: 50px;
        }

            .logoContainer img:focus {
                color: #495057;
                background-color: #fff;
                border-color: #80bdff;
                outline: 0;
                box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
            }

        .fileContainer {
            position: relative;
            cursor: pointer;
        }

            .fileContainer span {
                overflow: hidden;
                font-weight: bold;
                display: block;
                white-space: nowrap;
                text-overflow: ellipsis;
                cursor: pointer;
            }

            .fileContainer input[type="file"] {
                opacity: 0;
                margin: 0;
                padding: 0;
                width: 100%;
                height: 100%;
                left: 0;
                top: 0;
                position: absolute;
                cursor: pointer;
                color: #495057;
            }
        /*=========File Upload end==========*/
    </style>
    <script>
        function BindTable() {
            var table = $('#BindDynamicTable').DataTable({
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
                                return $('#BindDynamicTable').DataTable().column(idx).visible();
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
                                           return $('#BindDynamicTable').DataTable().column(idx).visible();
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
                                           return $('#BindDynamicTable').DataTable().column(idx).visible();
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
        }
    </script>
    <%--<asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title"></h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Institute Name</label>--%>
                                    <asp:Label ID="lblDYddlCollege" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlClg" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1" OnSelectedIndexChanged="ddlClg_SelectedIndexChanged" AutoPostBack="true" ToolTip="Please Select College.">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvClg" runat="server" ControlToValidate="ddlClg"
                                    Display="None" ErrorMessage="Please Select College" SetFocusOnError="true" ValidationGroup="show" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <%--<label>Degree</label>--%>
                                    <asp:Label ID="lblDYddlDegree" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" TabIndex="2" ToolTip="Please Select Degree. ">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" SetFocusOnError="true"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="show" Visible="false"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <%-- <label>Branch</label>--%>
                                    <asp:Label ID="lblDYddlBranch" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="3" ToolTip="Please Select Branch.">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="" SetFocusOnError="true"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <%--<sup>* </sup>--%>
                                    <%--<label>Semester</label>--%>
                                    <asp:Label ID="lblDYddlSemester" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="4" ToolTip="Please Select Semester.">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Receipt Type</label>
                                    <asp:Label ID="lblReciptType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="5">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClg" 
                                            Display="None" ErrorMessage="Please Select Institute" SetFocusOnError="true" ValidationGroup="show" InitialValue="0" />--%>
                            </div>
                        </div>
                    </div>


                    <div class="col-12 btn-footer">
                        <%-- <asp:Button ID="btnShow" runat="server" Text="Show Student" CssClass="btn btn-primary"
                                    ToolTip="Shows Students under Selected Criteria." ValidationGroup="show" />--%>

                        <input id="btnShow" type="button" title="Show Student" value="Show Student" class="btn btn-primary" validationgroup="show" tabindex="6"/>

                        <asp:Button ID="btnExcelReport" runat="server" Text="Excel Report" CssClass="btn btn-info d-none" OnClick="btnExcelReport_Click"
                            ValidationGroup="show" />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CssClass="btn btn-warning" ToolTip="Cancel Selected under Selected Criteria." TabIndex="7" />

                        <asp:ValidationSummary ID="ValidationsUM" ValidationGroup="show" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" runat="server" />
                    </div>
                </div>
                <%--       </ContentTemplate>
    </asp:UpdatePanel>--%>

                <div class="col-12" id="divstudent">
                    <table class="table table-striped table-bordered nowrap" id="BindDynamicTable">
                        <thead class="bg-light-blue">
                            <tr>
                                <th>Registration No</th>
                                <th>Student Name</th>
                                <th>Degree</th>
                                <th>Branch</th>
                                <th>Semester</th>
                                <th>Order ID</th>
                                <th>Transaction ID</th>
                                <th>Payment Date</th>
                                <th>Fees Amount</th>
                                <th>Payment Status</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>


    <div id="divMsg" runat="server">
    </div>

    <script>

        $(document).ready(function () {
            $("#divstudent").hide();

        });
    </script>
    <script type="text/javascript" language="javascript">


        $('#btnShow').on('click', function () {

            var Obj = {}; var str = '';

            debugger;
            // alert('okk');
            Obj.CollegeID = $("#ctl00_ContentPlaceHolder1_ddlClg").val();
            //document.getElementById("<%=ddlClg.ClientID%>").value;
            Obj.DegreeNo = $("#ctl00_ContentPlaceHolder1_ddlDegree").val();
            Obj.BranchNo = $("#ctl00_ContentPlaceHolder1_ddlBranch").val();
            Obj.Semesterno = $("#ctl00_ContentPlaceHolder1_ddlSemester").val();
            Obj.ReceiptType = $("#ctl00_ContentPlaceHolder1_ddlReceiptType").val();

            if (Obj.CollegeID == 0) {
                alert("Please select College !!");
                return false;
            }
            if (Obj.DegreeNo == 0) {
                Obj.DegreeNo == 0;
   
            }
            if (Obj.BranchNo == 0) {
                Obj.BranchNo == 0;
            }
            if (Obj.Semesterno == 0) {
                Obj.Semesterno == 0;
            }
            if (Obj.ReceiptType == 0) {
                alert("Please select Receipt Type !!");
                return false;
            }
            // alert('rr');
            $.ajax({
            type: "POST",
            url: '<%=Page.ResolveUrl("~/ACADEMIC/OnlinePaymentLog.aspx/BindList")%>',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(Obj),
            success: function (data) {

                debugger;
                // Alert("ok");
                if (data == '') {

                    //alert('Data Not found for Selection.');

                    return false;
                }
                else {
                    $("#BindDynamicTable").find("tbody").remove();
                    //str.empty();
                    // str = '<thead><tr><th>Registration No</th><th>Student Name</th><th>Degree</th><th>Branch</th><th>Semester</th><th>Order ID</th><th>Transaction ID</th><th>Payment Date</th><th>Fees Amount</th><th>Payment Status</th></tr></thead><tbody>';
                    str = '<tbody>';
                    //str.empty();
                    $.each(data.d, function (index, GetValue) {
                        str = str + '<tr>'
                        str = str + '<td>' + GetValue.Registrationno + '</td>'
                        str = str + '<td>' + GetValue.StudentName + '</td>'
                        str = str + '<td>' + GetValue.Degreeno + '</td>'
                        str = str + '<td>' + GetValue.Branchno + '</td>'
                        str = str + '<td>' + GetValue.SemesterName + '</td>'
                        str = str + '<td>' + GetValue.OrderID + '</td>'
                        str = str + '<td>' + GetValue.transactionID + '</td>'
                        str = str + '<td>' + GetValue.PaymentDate + '</td>'
                        str = str + '<td>' + GetValue.Amount + '</td>'
                        str = str + '<td>' + GetValue.PaymentStatus + '</td>'
                        str = str + '</tr>'

                    });
                    str = str + '</tbody>';

                    $("#BindDynamicTable").append(str);

                    $("#divstudent").show();
                      BindTable();
                }
            },


            error: function (error) {
                console.log(error);
            }
        });

        });
    </script>


</asp:Content>
