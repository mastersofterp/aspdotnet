<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Backlog_ExamRegistration_CC.aspx.cs" Inherits="Academic_Backlog_ExamRegistration_CC" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            var table = $('#my-table').DataTable({
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
                                return $('#my-table').DataTable().column(idx).visible();
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
                                                return $('#my-table').DataTable().column(idx).visible();
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
                                                return $('#my-table').DataTable().column(idx).visible();
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
                                                return $('#my-table').DataTable().column(idx).visible();
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
                var table = $('#my-table').DataTable({
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
                                    return $('#my-table').DataTable().column(idx).visible();
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
                                                    return $('#my-table').DataTable().column(idx).visible();
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
                                                    return $('#my-table').DataTable().column(idx).visible();
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
                                                    return $('#my-table').DataTable().column(idx).visible();
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

        // backup
        //function totAllSubjects(headchk) {
        //    var frm = document.forms[0]
        //    for (i = 0; i < document.forms[0].elements.length; i++) {
        //        var e = frm.elements[i];
        //        if (e.type == 'checkbox') {
        //            if (headchk.checked == true)
        //                e.checked = true;
        //            else {
        //                e.checked = false;
        //                headchk.checked = false;
        //            }
        //        }

        //    }

        //}

        // ends 

        //function totAllSubjects(headchk) {
        //    headchk.checked = true;
        //    var frm = document.forms[0]
        //    for (i = 0; i < document.forms[0].elements.length; i++) {
        //        var e = frm.elements[i];
        //        if (e.type == 'checkbox') {
        //            if (headchk.checked == true) {
        //                e.checked = true;
        //            }
        //            else {
        //                e.checked = false;
        //                headchk.checked = false;
        //            }
        //        }

        //    }

        //}


        // TEST BACKUP WITHOUT NEW REQUIREMENT CHANGES ON DT 01072022
        //      function totAllSubjects(headchk) {
        //          debugger;
        //          var sum = 0;
        //          var frm = document.forms[0]
        //          try {
        //              for (i = 0; i < document.forms[0].elements.length; i++) {
        //                  var e = frm.elements[i];
        //                  if (e.type == 'checkbox') {
        //                      if (headchk.checked == true) {
        //                          // SumTotal();
        //                          // var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
        //                          //// alert(j);
        //                          // sum += parseFloat(j);
        //                          if (e.disabled == false) {
        //                              e.checked = true;
        //                          }
        //                      }
        //                      else {
        //                          if (e.disabled == false) {
        //                              e.checked = false;
        //                              headchk.checked = false;
        //                          }
        //                      }
        //                      //var x = document.getElementById("<%= lblTotalExamFee.ClientID%>");
        //                      // x = sum.toString();
        //                  }
        //
        //              }
        //              if (headchk.checked == true) {
        //                  SumTotal();
        //              }
        //              else {
        //                  SumTotal();
        //              }
        //          }
        //          catch (err) {
        //              alert("Error : " + err.message);
        //          }
        //      }
        //
        //
        //      function SumTotal() {
        //          debugger;
        //
        //          try{
        //              var length = $("[id*=tblBacklogSubjects] td").closest("tr").length;
        //              var Duration = document.getElementById('ctl00_ContentPlaceHolder1_hdfDuration').value;
        //              var IsFinalSem = document.getElementById('ctl00_ContentPlaceHolder1_hdfSemester').value;          
        //          
        //              // dataRowsmark = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
        //              var total = 0;
        //              for (var i = 0; i < length; i++) {
        //                  //    MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
        //                  var hdfExamRegistered = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfExamRegistered").value;
        //                  if (document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked && hdfExamRegistered !=1) {
        //                      var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
        //                      //  alert(j);
        //                      total += parseFloat(j);
        //                  }
        //              }
        //              // alert(total);
        //              debugger;
        //
        //              if (Duration == IsFinalSem) {
        //                  total = total + 1400;
        //              }
        //              else {
        //                  total = total;
        //              }
        //          
        //
        //              // LATE FEE PATCH ON DT 18062022
        //          
        //              var hdfExamLastDate = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLastDate').value;
        //              var hdfExamLateFee = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLateFee').value;
        //              var today = new Date();
        //              var yyyy = today.getFullYear();
        //              var mm = today.getMonth() + 1; // Months start at 0!
        //              var dd = today.getDate();
        //
        //              if (dd < 10) dd = '0' + dd;
        //              if (mm < 10) mm = '0' + mm;
        //
        //              today = dd + '/' + mm + '/' + yyyy;          
        //
        //              //Now you can Compare these dates 
        //              if (today >= hdfExamLastDate) {
        //                  //alert('late fee');
        //                  var latefee = parseFloat(hdfExamLateFee);
        //              } else {
        //                  var latefee = 0;
        //                  // alert('no late fee');
        //              }
        //
        //              // LATE FEE PATCH ON DT 18062022
        //              total = parseFloat(total + latefee);
        //             // alert(total);
        //              var lbl = document.getElementById('<%=lblTotalExamFee.ClientID %>');
        //              $("[id*=ctl00_ContentPlaceHolder1_lblTotalExamFee]").text(total + '.00');
        //          }
        //          catch (error) {
        //              alert('Error:-' + error.message);
        //          }
        //      }

        // ENDS HERE TEST BACKUP WITHOUT NEW REQUIREMENT CHANGES ON DT 01072022

        // ADDED NEW SCRIPTS BY NARESH BEERLA ON DT 01/07/2022 AS PER NEW REQUIREMENT 

        function totAllSubjects(headchk) {
            debugger;
            var sum = 0;
            var frm = document.forms[0]
            try {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    var e = frm.elements[i];
                    if (e.type == 'checkbox') {
                        if (headchk.checked == true) {
                            //SumTotal();
                            /// var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            // alert(j);
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
                        // var x = document.getElementById("<%= lblTotalExamFee.ClientID%>");
                        //  x = sum.toString();
                    }

                }
                if (headchk.checked == true) {
                    // SumTotal();
                }
                else {
                    //  SumTotal();
                }
            }
            catch (err) {
                alert("Error : " + err.message);
            }
        }


        function SumTotal() {
            debugger;

            try {
                var length = $("[id*=tblBacklogSubjects] td").closest("tr").length;
                //var Duration = document.getElementById('ctl00_ContentPlaceHolder1_hdfDuration').value;
                //var IsFinalSem = document.getElementById('ctl00_ContentPlaceHolder1_hdfSemester').value;

                // dataRowsmark = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
                var total = 0;
                for (var i = 0; i < length; i++) {
                    //    MaxMark = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvGrades_ctrl' + i + '_txtMax').value.trim());
                    //    var hdfExamRegistered = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfExamRegistered").value;
                    var hdfStudRegistered = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfStudRegistered").value;
                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked && hdfStudRegistered != 1) {
                        var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                        //  alert(j);
                        total += parseFloat(j);
                    }
                }
                // alert(total);
                debugger;

                //if (Duration == IsFinalSem) {
                //      total = total + 1400;
                //  }
                //  else {
                //      total = total;
                // }


                // LATE FEE PATCH ON DT 18062022

                // var hdfExamLastDate = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLastDate').value;
                // var hdfExamLateFee = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLateFee').value;
                // var today = new Date();
                // var yyyy = today.getFullYear();
                // var mm = today.getMonth() + 1; // Months start at 0!
                // var dd = today.getDate();

                //  if (dd < 10) dd = '0' + dd;
                //  if (mm < 10) mm = '0' + mm;
                //
                //  today = dd + '/' + mm + '/' + yyyy;
                //
                //  Todate = new Date(today);
                //  Lastdate = new Date(hdfExamLastDate);


                //Now you can Compare these dates 
                //if (today >= hdfExamLastDate) {
                //  if (Todate >= Lastdate) {
                //      var latefee = parseFloat(hdfExamLateFee);
                //  } else {
                //      var latefee = 0;
                //      // alert('no late fee');
                //  }

                // LATE FEE PATCH ON DT 18062022
                // total = parseFloat(total + latefee);
                total = parseFloat(total)// + latefee);
                // alert(total);
                var lbl = document.getElementById('<%=lblTotalExamFee.ClientID %>');
                $("[id*=ctl00_ContentPlaceHolder1_lblTotalExamFee]").text(total + '.00');
            }
            catch (error) {
                alert('Error:-' + error.message);
            }
        }


        // ENDS HERE NEW SCRIPTS BY NARESH BEERLA ON DT 01/07/2022 AS PER NEW REQUIREMENT 


    </script>
<%--      <script src="alert/dist/sweetalert.min.js"></script>
 <link rel="stylesheet" href="alert/dist/sweetalert.css">--%>
    <%-- <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>
    <div id="dvMain" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">BACKLOG EXAM REGISTRATION</h3>
                    </div>

                    <asp:UpdatePanel ID="updatepnl" runat="server">
                        <ContentTemplate>
                            <div class="box-body">
                                <div class="col-12" id="divCourses" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-lg-7 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblName" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>RRN No :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester/Trimester :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item" style="display: none;"><b>Admission Batch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                                    </a>
                                                </li>

                                                <li class="list-group-item"><b>Degree / Branch :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Scheme :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                 <li class="list-group-item"><b>Session Name :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblsessionno" runat="server" Font-Bold="true" /></a>
                                                </li>
                                            </ul>
                                        </div>

                                        <div class="col-lg-5 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <%-- <li class="list-group-item"><b>Admission Batch :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                            </a>
                                        </li>--%>

                                                <li class="list-group-item"><b>Is Processing Fee:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblfessapplicable" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Is Certificate Fee:</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblCertificateFee" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Subjects Fee :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblTotalExamFee" runat="server" Font-Bold="true" /></a>
                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0.00</asp:Label>
                                                    <asp:HiddenField ID="hdfExamLastDate" runat="server" />
                                                    <asp:HiddenField ID="hdfDuration" runat="server" />
                                                    <asp:HiddenField ID="hdfSemester" runat="server" />
                                                    <asp:HiddenField ID="hdfExamLateFee" runat="server" />
                                                </li>

                                                <li class="list-group-item"><b>Late Fee :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblLateFee" runat="server" Font-Bold="true" /></a>


                                                </li>

                                                <li class="list-group-item"><b>Total Fee :</b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="FinalTotal" runat="server" Font-Bold="true" /></a>
                                                      <asp:HiddenField ID="hdfCreditTotal" runat="server" />
                                                </li>


                                            </ul>
                                        </div>
                                        <div class="col-12 mt-3">
                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Backlog Semester/Trimester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlBackLogSem" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                        ValidationGroup="backsem" AutoPostBack="True" OnSelectedIndexChanged="ddlBackLogSem_SelectedIndexChanged" data-select2-enable="true" TabIndex="5" Visible="true" Height="16px">
                                                    </asp:DropDownList>
                                                   <asp:HiddenField ID="hdfCategory" runat="server" />
                                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />

                                                </div>
                                                <div class="form-group col-lg-5 col-md-6 col-12">
                                                    <div class=" note-div" >
                                                        <h3 class="heading">Note </h3>
                                                       
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>This Is One Time Activity, Kindly Verify Data Before Final Registration</span>  </p>

                                                    </div>

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">

                                                    <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                    <div class="label-dynamic">
                                                        <label>Photo</label>
                                                    </div>
                                                    <asp:Image ID="imgPhoto" runat="server" Width="50%" Height="80%" />
                                                </div>
                                                <div class="form-group offset-lg-3 col-lg-6 col-md-6 col-12" style="display: none">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;">
                                                            <tr class="bg-light-blue">
                                                                <th>Details 
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td>Comman Fee ((Grade Card + CAP) * No of Semesters)
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">
                                                                    <asp:Label ID="lblCommanFee" runat="server" Text="0"></asp:Label>
                                                                    <asp:HiddenField ID="hdnDefaultCommanFee" runat="server" Value="0"></asp:HiddenField>
                                                                    <asp:HiddenField ID="hdnCommanFee" runat="server" Value="0"></asp:HiddenField>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td>Selected Course Fee
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">
                                                                    <asp:Label ID="lblSelectedCourseFee" runat="server"></asp:Label>
                                                                    <asp:HiddenField ID="hdnSelectedCourseFee" runat="server"></asp:HiddenField>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: none">
                                                                <td>Late Fine
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">
                                                                    <asp:Label ID="lblLateFine" runat="server"></asp:Label>
                                                                    <asp:HiddenField ID="hdnLateFine" runat="server"></asp:HiddenField>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Backlog Fine
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">
                                                                    <asp:Label ID="lblBacklogFine" runat="server"></asp:Label>
                                                                    <asp:HiddenField ID="hdnBacklogFine" runat="server"></asp:HiddenField>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="font-weight: bold;">Total Fee
                                                                </td>
                                                                <td style="font-weight: bold; text-align: center;">
                                                                    <asp:Label ID="lblTotalFee" runat="server"></asp:Label>
                                                                    <asp:HiddenField ID="hdnTotalFee" runat="server"></asp:HiddenField>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            
                                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Print Reciept" TabIndex="7"
                                                ValidationGroup="backsem" CssClass="btn btn-primary" Visible="false" Style="display: none;" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-12"  >
                                            <asp:Panel ID="pnlFailCourse" runat="server">
                                                <%--<span style="color:red" class="pull-right">Note: For Final Year Students Rs.1400/- is added for Certificate Purpose  </span>--%>
                                                <asp:ListView ID="lvFailCourse" runat="server" OnItemDataBound="lvFailCourse_ItemDataBound">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Courses List</h5>
                                                        </div>
                                                        <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                            <thead>
                                                                <tr>
                                                                    <th>

                                                                        <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                        <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" /><%--Checked="true"--%> 
                                                                    Select All

                                                                    </th>
                                                                    <th>COURSE CODE
                                                                    </th>
                                                                    <th>COURSE NAME
                                                                    </th>
                                                                    <th>CREDIT
                                                                   </th>
                                                                    <th>SEMESTER
                                                                    </th>
                                                                    <th>
                                                                        COURSE TYPE                                                                
                                                                    </th> 

                                                                       <th>
                                                                        INTERNAL                                                               
                                                                    </th> 

                                                                    <th>
                                                                        EXTERNAL                                                               
                                                                    </th> 

                                                                      <th>
                                                                        TOTAL                                                               
                                                                    </th>

                                                                                                                                    
                                                                    <th>GRADE
                                                                 
                                                                    </th>
                                                                    <th id="BatchTheory1" style="text-align: center">EXAM FEES
                                                                  
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>

                                                        </table>

                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow" class="item">
                                                            <td>
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "0" ? true : false %>' />--%>

                                                                <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />
                                                                <%--Checked="true"--%>

                                                                <%--                                                        ForeColor='<%#Convert.ToInt32(Eval("ACTIVE_STATUS"))==1?System.Drawing.Color.Green:System.Drawing.Color.Red %>'--%>
                                                                <%--Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' 
                                                            Enabled='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? false : true %>'--%>
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" Enabled='<%#Eval("STUD_EXAM_REGISTERED").ToString()=="1" ? false : true%>'  OnCheckedChanged="chkAccept_CheckedChanged" AutoPostBack="true" Checked='<%# Eval("STUD_EXAM_REGISTERED").ToString() == "1" ? true : false %>'/>--%><%--OnCheckedChanged="chkAccept_CheckedChanged"--%>  <%--Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' --%>
                                                                <%-- Enabled='<%# Eval("REGISTERED").ToString()=="1" && Eval("EXAMTYPENO").ToString()=="1" && Eval("EXAM_REGISTERED").ToString()=="1"? false : true%>'--%>
                                                                <%--Enabled='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? false : true)%>' onclick="backlogLvChk(this);" />--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                                <%--  <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("CPS") %>' />--%>
                                                                <asp:HiddenField ID="hdfExamRegistered" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                                <asp:HiddenField ID="hdfextind" runat="server" Value='<%# Eval("EXT_IND") %>' />                                                              
                                                            </td>

                                                               <td>
                                                    <asp:Label ID="lblcredits" runat="server" Text=' <%# Eval("CREDITS") %>' />

                                                </td>
                                                            <td>

                                                                <asp:Label ID="lblsem" runat="server" Text=' <%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTER")%>' />

                                                            </td>
                                                            <td>
                                                                <%-- <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical" %>--%>
                                                                <%# Eval("SUBNAME") %>
                                                                <%--<asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />--%>
                                                            </td>
                                                           <td>
                                                                <%# Eval("INTERMARK") %>
                                                            </td>
                                                              <td>
                                                                <%# Eval("EXTERMARK") %>
                                                            </td>
                                                              <td>
                                                                <%# Eval("MARKTOT") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("GRADE") %>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblAmt" runat="server" Text=' <%# Eval("FEE") %>' />
                                                                <%--  <%# Eval("FEE") %>--%>
                                                         
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <%--<EmptyDataTemplate>
                                            <span style="background-color: #00E171; font-size:large; font-style:normal; border:1px solid #000000;">
                                                    No Backlog Courses.
                                            </span>
                                            </EmptyDataTemplate>--%>
                                                </asp:ListView>
                                            </asp:Panel>

                                        </div>
                                        <div id="divbtn" runat="server" class="col-12 btn-footer">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit & Pay Online" CausesValidation="false"
                                                Font-Bold="true" CssClass="btn btn-primary" TabIndex="8"  OnClientClick="return showConfirm();" />
                                              <asp:Button ID="btnSubmit_WithDemand" runat="server" OnClick="btnSubmit_WithDemand_Click" Text="Submit With Offline Fee" CausesValidation="false"
                                                Font-Bold="true" CssClass="btn btn-primary" TabIndex="9"  OnClientClick="return showConfirm1();;" />
                                            <asp:Button ID="btnPay" runat="server" Text="Submit" CausesValidation="false" OnClick="btnPay_Click"
                                                Font-Bold="true" CssClass="btn btn-primary" TabIndex="10" Visible="false" />                                      
                                              <asp:Button ID="btnPrintRegSlip" runat="server" Text="Report" OnClick="btnPrintRegSlip_Click" CausesValidation="false"
                            CssClass="btn btn-info" TabIndex="9" Visible="true" />


                                        </div>
                                        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                        <div id="divMsg" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                         

                          
                        </ContentTemplate>
                         <Triggers>
                                  <asp:PostBackTrigger ControlID="btnPrintRegSlip" />                        
                                  <asp:PostBackTrigger ControlID="btnSubmit" />   
                                  <asp:PostBackTrigger ControlID="btnPay" />                       
                             </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
        </Triggers>
     </asp:UpdatePanel>--%>
   <script type="text/javascript">
       function showConfirm() {
           var ret = confirm('Do you want to Pay this Backlog Subjects for Exam Registration?');
           if (ret == true) {
               validate = true;
           }
           else
               validate = false;
           return validate;
       }
       function showConfirm1() {
           var ret = confirm('Do you want to Create Demand for this Backlog Subjects for Exam Registration?');
           if (ret == true) {
               validate = true;
           }
           else
               validate = false;
           return validate;
       }
    </script>
    
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
    .form-control {}
</style>
</asp:Content>

