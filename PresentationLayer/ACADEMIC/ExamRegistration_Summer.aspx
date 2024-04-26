<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExamRegistration_Summer.aspx.cs" Inherits="ACADEMIC_ExamRegistration_Summer" %>

<%--<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamRegistration_CC.aspx.cs" Inherits="Academic_ExamRegistration" Title="" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>






<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>


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
                        //var x = document.getElementById("<%= lblTotalExamFee.ClientID%>");
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
                alert("Error : " + err.message);
            }
        }


        function SumTotal() {
            debugger;

            try {
                var length = $("[id*=tblBacklogSubjects] td").closest("tr").length;
                var Duration = document.getElementById('ctl00_ContentPlaceHolder1_hdfDuration').value;
                var IsFinalSem = document.getElementById('ctl00_ContentPlaceHolder1_hdfSemester').value;

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

                if (Duration == IsFinalSem) {
                    total = total + 1400;
                }
                else {
                    total = total;
                }


                // LATE FEE PATCH ON DT 18062022

                var hdfExamLastDate = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLastDate').value;
                var hdfExamLateFee = document.getElementById('ctl00_ContentPlaceHolder1_hdfExamLateFee').value;
                var today = new Date();
                var yyyy = today.getFullYear();
                var mm = today.getMonth() + 1; // Months start at 0!
                var dd = today.getDate();

                if (dd < 10) dd = '0' + dd;
                if (mm < 10) mm = '0' + mm;

                today = dd + '/' + mm + '/' + yyyy;

                Todate = new Date(today);
                Lastdate = new Date(hdfExamLastDate);


                //Now you can Compare these dates 
                //if (today >= hdfExamLastDate) {
                if (Todate >= Lastdate) {
                    var latefee = parseFloat(hdfExamLateFee);
                } else {
                    var latefee = 0;
                    // alert('no late fee');
                }

                // LATE FEE PATCH ON DT 18062022
                total = parseFloat(total + latefee);
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

    <%-- <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>
    <div id="dvMain" runat="server">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div2" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">SESSION CREATION</h3>--%>
                    <h3 class="box-title">
                         <asp:Label ID="lblDynamicPageTitle" runat="server" style="text-transform: uppercase;"></asp:Label>

                    </h3>
                </div>

               <%-- <-----------------------------------------Added By Rohit--------------------------------------------------->--%>
                   <div id="pnlSearch" runat="server" visible="False">
                            <div class="col-12">
                                <div class="row">
                                    <div id="divenroll" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>PRN No</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="1" MaxLength="20"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers, UppercaseLetters, LowercaseLetters" TargetControlID="txtEnrollno" />
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                            Display="None" ErrorMessage="Please Enter PRN No." SetFocusOnError="true"
                                            ValidationGroup="search" />

                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="search" />
                          <%--  <asp:Button ID="btnSearch" runat="server" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" TabIndex="1" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" ValidationGroup="Show" TabIndex="1" OnClick="btnClear_Click" CssClass="btn btn-warning" />--%>
                                  <asp:Button ID="btnSearch" runat="server" Text="Show" CssClass="btn btn-primary" TabIndex="1" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" TabIndex="1" CssClass="btn btn-warning" OnClick="btnClear_Click" />
                            </div>
                        </div>
                <%--<---------------------------------------------------------------------------------------------------------->--%>

                <div class="box-body">
                    <asp:UpdatePanel ID="updatepnl" runat="server">
                        <ContentTemplate>


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
                                                    <asp:Label ID="lblCurrentSemester" runat="server" Font-Bold="true" /></a>
                                            </li>
                                            <li class="list-group-item" style="display: none;"><b>Admission Batch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Degree / Branch :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="true" />

                                                </a>
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
                                             <li class="list-group-item"><b>Paid Fee :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="PaidTotal" runat="server" Font-Bold="true" /></a>
                                                    <asp:HiddenField ID="hdfPaidTotal" runat="server" />
                                            </li>                                           

                                        </ul>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12 d-none">
                                        <ul class="list-group list-group-unbordered" runat="server" id="Allfeesitems" style="height: 200px; overflow: scroll;">

                                            <%--<li class="list-group-item" runat="server" id="lblH2"><b>Is Processing Fee:</b>--%>
                                            <%--<asp:Label ID="lblDYddlColgScheme" runat="server" Font-Bold="true"></asp:Label>--%>

                                            <li class="list-group-item" runat="server" id="lvitemH1">

                                                <b>
                                                    <%--Subjects Fee :--%>
                                                    <asp:Label ID="lblH1" runat="server" Font-Bold="true"></asp:Label>

                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalExamFee1" runat="server" Font-Bold="true" /></a>
                                                <asp:Label ID="lblOrderID1" runat="server" CssClass="data_label" Visible="false">0.00</asp:Label>

                                            </li>





                                            <li class="list-group-item" runat="server" id="lvitemH2">
                                                <b>
                                                    <asp:Label ID="lblH2" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblfessapplicable1" runat="server" Font-Bold="true" />
                                                </a>

                                            </li>

                                            <%--<li class="list-group-item"><b>Is Certificate Fee:</b>--%>
                                            <li class="list-group-item" runat="server" id="lvitemH3"><b>
                                                <asp:Label ID="lblH3" runat="server" Font-Bold="true"></asp:Label>
                                            </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblCertificateFee1" runat="server" Font-Bold="true" /></a>
                                            </li>

                                            <%--<li class="list-group-item" runat="server" id="lvitemH1">
                                                
                                                <b>--%>
                                            <%--Subjects Fee :--%>
                                            <%--   <asp:Label ID="lblH1" runat="server" Font-Bold="true"></asp:Label>

                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblTotalExamFee" runat="server" Font-Bold="true" /></a>
                                                <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false">0.00</asp:Label>

                                            </li>--%>




                                            <li class="list-group-item" runat="server" id="lvitemH4">
                                                <b>
                                                    <asp:Label ID="lblH4" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead4amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" runat="server" id="lvitemH5">
                                                <b>
                                                    <asp:Label ID="lblH5" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead5amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" runat="server" id="lvitemH6">
                                                <b>
                                                    <asp:Label ID="lblH6" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead6amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>

                                            <li class="list-group-item" runat="server" id="lvitemH7">
                                                <b>
                                                    <asp:Label ID="lblH7" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead7amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>

                                            <li class="list-group-item" runat="server" id="lvitemH8">
                                                <b>
                                                    <asp:Label ID="lblH8" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead8amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" runat="server" id="lvitemH9">
                                                <b>
                                                    <asp:Label ID="lblH9" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead9amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" runat="server" id="lvitemH10">
                                                <b>
                                                    <asp:Label ID="lblH10" runat="server" Font-Bold="true"></asp:Label>
                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblhead10amount" runat="server" Font-Bold="true" />
                                                </a>
                                            </li>
                                            <li class="list-group-item" runat="server" id="lvLatefeesHead">
                                                <b>
                                                    <%--Late Fee :--%>
                                                    <asp:Label ID="lblLateFeeHead" runat="server" Font-Bold="true"></asp:Label>

                                                </b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblLateFee1" runat="server" Font-Bold="true" /></a>
                                            </li>


                                        </ul>
                                        <ul class="list-group list-group-unbordered d-none" runat="server" id="lvTotalAmount">

                                            <li class="list-group-item"><b>Total Fee :</b>
                                               <a class="sub-label">
                                                    <asp:Label ID="FinalTotal1" runat="server" Font-Bold="true" /></a>

                                                
                                                   <%-- <asp:Label ID="totamtpay" runat="server" Font-Bold="true" BackColor="#ffcc99" />--%>
<%--                                                  <asp:TextBox ID="totamtpay" runat="server" CssClass="form-control" ToolTip="0" TabIndex="1" MaxLength="20"></asp:TextBox>--%>
                                                
                                                <asp:HiddenField ID="hdfCreditTotal1" runat="server" />
                                            </li>
 
                                        </ul>
                                    </div>
                                    <asp:HiddenField ID="hdfCategory" runat="server" />
                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                </div>
                            </div>

                            <div class="col-12 mt-4">
                                <asp:Panel ID="pnlFailCourse" runat="server">
                                    <%--<span style="color:red" class="pull-right">Note: For Final Year Students Rs.1400/- is added for Certificate Purpose  </span>--%>


                                    <asp:ListView ID="lvFailCourse" runat="server" OnItemDataBound="lvFailCourse_ItemDataBound" >
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Courses List</h5>
                                            </div>
                                            <div>
                                                <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap ">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                               <asp:CheckBox ID="chkAll" Checked="false" runat="server" OnCheckedChanged="chkAll_CheckedChanged"  AutoPostBack="true" />
                                                                All
                                                            </th>
                                                            <th>COURSE CODE
                                                            </th>
                                                            <th>COURSE NAME
                                                            </th>
                                                            <%----%>
                                                            <th>CREDIT
                                                            </th>
                                                            <th>COURSE TYPE                                                                   
                                                            </th>
                                                            <th>EXAM TYPE                                                                  
                                                            </th>
                                                            <th>SEMESTER   
                                                            </th>
                                                            <th id="BatchTheory1">EXAM FEES
                                                                   
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>


                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow" class="item">
                                                <td>
                                                    <%--<asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "0" ? true : false %>' />--%>
                                                    <%--<asp:CheckBox ID="CheckBox1" runat="server" ToolTip="Click to select this subject for registration" onclick="backlogLvChk(this);" />--%>



                                                    <%--<asp:CheckBox ID="chkAccept" runat="server" Enabled='<%#Eval("STUD_EXAM_REGISTERED").ToString()=="1" ? false : true%>' Checked='<%# Eval("STUD_EXAM_REGISTERED").ToString() == "1" ? true : false %>'/>--%>
                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="chkaccept_checkedchanged" />

                                                  <%-- <asp:checkbox id="chkAccept" runat="server" checked="false" autopostback="true" onclick="exefunction(this);" />--%>

                                                      <asp:HiddenField ID="hdfee" runat="server" Value='<%# (Eval("FEE").ToString())==""?"0":Eval("FEE") %>' />


                                                    <%--OnCheckedChanged="chkAccept_CheckedChanged"--%>  <%--Checked='<%#(Convert.ToInt32(Eval("STUD_EXAM_REGISTERED"))==1 ? true : false)%>' --%>
                                                    <%-- Enabled='<%# Eval("REGISTERED").ToString()=="1" && Eval("EXAMTYPENO").ToString()=="1" && Eval("EXAM_REGISTERED").ToString()=="1"? false : true%>'--%>
                                                    <%--Enabled='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? false : true)%>' onclick="backlogLvChk(this);" />--%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    <asp:HiddenField ID="hdfExamRegistered" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                    <asp:HiddenField ID="hdfStudRegistered" runat="server" Value='<%# Eval("STUD_EXAM_REGISTERED") %>' />
                                                     <asp:HiddenField ID="hdfextind" runat="server" Value='<%# Eval("EXT_IND") %>' /> 
                                                </td>
                                            <%--    --%>
                                                <td>
                                                    <asp:Label ID="lblcredits" runat="server" Text=' <%# Eval("CREDITS") %>' />

                                                </td>
                                                <td>

                                                    <%# Eval("SUBNAME") %>
                                                </td>

                                                <td>

                                                    <asp:Label ID="lblExamType" runat="server" Text='<%# Eval("EXAMTYPE") %>' ToolTip='<%# Eval("EXAMTYPENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblsemester" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmt" runat="server" Text=' <%# Eval("FEE") %>' />

                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </asp:Panel>

                            </div>
                        </ContentTemplate>
                        <%--<Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>--%>


                    </asp:UpdatePanel>

                    <div class="col-12 btn-footer" id="divbtn" runat="server">
                        <asp:Button ID="btnPay" runat="server" Text="Submit & Pay Online" CausesValidation="false"
                            Font-Bold="true" OnClick="btnPay_Click" CssClass="btn btn-primary" TabIndex="7" OnClientClick="return showConfirm();" />
                        <asp:Button ID="btnSubmit_WithDemand" runat="server" OnClick="btnSubmit_WithDemand_Click" Text="Submit With Offline Fee" CausesValidation="false"
                            Font-Bold="true" CssClass="btn btn-primary" TabIndex="9" OnClientClick="return showConfirm1();" />
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CausesValidation="false"
                            Font-Bold="true" CssClass="btn btn-primary" TabIndex="8" />
                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Report" OnClick="btnPrintRegSlip_Click" CausesValidation="false"
                            Font-Bold="true" CssClass="btn btn-info" TabIndex="9" Visible="true" />
                        &nbsp;
                        
                    </div>
                    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>



 <%--   <script>

        function exefunction(chkAccept) {

            debugger;
            var fee = 0;
            var feesamt = 0;
            var tempamt = 0
            var count = 0;
            list = 'lvFailCourse';


            var dataRows = document.getElementsByTagName('tr');


            if (dataRows != null) {
                debugger;


                for (i = 0; i < dataRows.length - 1; i++) {

                    if (document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value == 'NaN' || document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value == '') {

                        fee = 0;
                    }
                    else {
                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = fee;
                    }
                    var cbrow = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept");


                    if (document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept").checked == true && !cbrow.disabled) {

                        count++;
                        fee += parseInt(document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfee").value);

                        document.getElementById("ctl00_ContentPlaceHolder1_totamtpay").value = fee;

                    }

                }
            }
        }

    </script>--%>

    <script>
        function exefunction(chkAccept) {

            debugger
            var fee = 0;
            var count = 0;
            list = 'lvFailCourse';
            var totamtpay = document.getElementById("ctl00_ContentPlaceHolder1_totamtpay");

            for (var i = 0; i < list.length; i++) {
                var cbrow = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_chkAccept");
                var hdfee = document.getElementById("ctl00_ContentPlaceHolder1_lvFailCourse_ctrl" + i + "_hdfee");

                if (cbrow && hdfee) {
                    if (cbrow.checked && !cbrow.disabled) {
                        count++;
                        fee += parseInt(hdfee.value);
                        totamtpay.value = fee;
                    }
                }
            }

            // Update the total amount field
            if (!isNaN(fee)) {
                totamtpay.value = fee;
            } else {
                totamtpay.value = '';
            }
        }
</script>


    <%--    -------------------------------------------fees calculation ----------------------------------------------------------------------------------------------------%>
    <%-- <script type="text/javascript">

        function calculateExamFees() {

            debugger;

            var table = document.getElementById('<%= lvFailCourse.ClientID %>'); // Get the ListView table by its ClientID
            var rows = table.getElementsByTagName('tr'); // Get all the rows in the table
            var sum = 0; // Initialize the sum to zero

            for (var i = 1; i < rows.length; i++) { // Start from 1 to skip the header row
                var row = rows[i];
                var checkBox = row.getElementsByTagName('input')[0]; // Assuming the checkbox is the first input element in each row

                if (checkBox && checkBox.type === 'checkbox' && checkBox.checked) {
                    // The checkbox is checked, so add the corresponding Exam Fees to the sum
                    var examFeesCell = row.cells[row.cells.length - 1]; // Assuming Exam Fees is the last cell in each row
                    var examFees = parseFloat(examFeesCell.textContent.trim()); // Parse the Exam Fees as a float

                    if (!isNaN(examFees)) {
                        sum += examFees; // Add the Exam Fees to the sum
                    }
                }
            }

            alert('Total Exam Fees for checked rows: ' + sum.toFixed(2)); // Display the total with two decimal places
        }
    </script>--%>

    <%--    -----------------------------------------------------------------------------------------------------------------------------------------------%>

    <%-- </ContentTemplate>
          <Triggers>
            <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
        </Triggers>
     </asp:UpdatePanel>--%>
    <script type="text/javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });
        //function SelectAll(headchk) {

        //    var frm = document.forms[0]

        //    for (i = 0; i < document.forms[0].elements.length; i++) {

        //        var e = frm.elements[i];
        //        if (e.type == 'checkbox') {
        //            if (headchk.checked == true) {
        //                //var table = $('#tblBacklogSubjects').DataTable()
        //                //alert(table.column[8].val().sum());
        //                e.checked = true;
        //            }

        //            else
        //                e.checked = false;
        //        }
        //    }
        //}
        //function CheckSelectionCount(chk) {
        //    var count = -1;
        //    var frm = document.forms[0]
        //    for (i = 0; i < document.forms[0].elements.length; i++) {
        //        var e = frm.elements[i];
        //        if (count == 2) {
        //            chk.checked = false;
        //            alert("You have reached maximum limit!");
        //            return;
        //        }
        //        else if (count < 2) {
        //            if (e.checked == true) {
        //                count += 1;
        //            }
        //        }
        //        else {
        //            return;
        //        }
        //    }
        //}
        //function BacklogCount() {
        //    debugger;
        //    // alert("2")
        //    if (document.getElementById('tblBacklogSubjects') != null) {
        //        var backlogCounter = 0;
        //        dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
        //        if (dataRows != null) {
        //            var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
        //            //alert(backlogFineAmt)
        //            for (i = 0; i < (dataRows.length - 1) ; i++) {
        //                //alert("In Backlog")
        //                if (document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_chkAccept').checked) {
        //                    //  alert("In Backlog")
        //                    backlogCounter = backlogCounter + 1;
        //                    //alert(backlogCounter)
        //                }
        //            }
        //        }
        //        return backlogCounter;
        //    }
        //}
        //function backlogLvChk(chk) {
        //    debugger;
        //    var count;
        //    if (document.getElementById('tblBacklogSubjects') != null) {
        //        dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
        //        if (dataRows != null) {
        //            //alert("1")
        //            count = BacklogCount();

        //            //if (!isBacklogGreaterThan2) {
        //            if (count <= 2) {
        //                var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
        //                //alert(backlogFineAmt)
        //                if (chk.checked) {
        //                    $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
        //                    $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
        //                }
        //                else {
        //                    $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
        //                    $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
        //                }
        //                var newbacklogFineAmt = $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val();
        //                // var selectedCourseAmt = $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val();
        //                var lateFineAmt = $("#ctl00_ContentPlaceHolder1_hdnLateFine").val();

        //                // var totalFinalAmt = Number(selectedCourseAmt) + Number(newbacklogFineAmt) + Number(lateFineAmt);
        //                var totalFinalAmt = Number(newbacklogFineAmt);
        //                $("#ctl00_ContentPlaceHolder1_lblTotalFee").text(Number(totalFinalAmt));
        //                $("#ctl00_ContentPlaceHolder1_hdnTotalFee").val(Number(totalFinalAmt));
        //            }
        //                //}
        //            else {
        //                if (chk.checked) {
        //                    $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(500));
        //                    $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(500));
        //                }
        //                else {
        //                    var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
        //                    if (backlogFineAmt == 500) {
        //                        if (count <= 2) {
        //                            $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(count) * 200);
        //                            $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(count) * 200);
        //                        }
        //                        else {
        //                            $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text(Number(500));
        //                            $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val(Number(500));
        //                        }
        //                    }
        //                }
        //                var newbacklogFineAmt = $("#ctl00_ContentPlaceHolder1_hdnBacklogFine").val();
        //                //  var selectedCourseAmt = $("#ctl00_ContentPlaceHolder1_hdnSelectedCourseFee").val();
        //                //  var lateFineAmt = $("#ctl00_ContentPlaceHolder1_hdnLateFine").val();

        //                //  var totalFinalAmt = Number(selectedCourseAmt) + Number(newbacklogFineAmt) + Number(lateFineAmt);
        //                var totalFinalAmt = Number(newbacklogFineAmt);
        //                $("#ctl00_ContentPlaceHolder1_lblTotalFee").text(Number(totalFinalAmt));
        //                $("#ctl00_ContentPlaceHolder1_hdnTotalFee").val(Number(totalFinalAmt));
        //            }
        //        }
        //    }
        //}
    </script>
    <script type="text/javascript">
        function showConfirm() {
            var ret = confirm('Do you want to Pay this Subjects for Exam Registration?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
        function showConfirm1() {
            var ret = confirm('Do you want to Create Demand for this Subjects for Exam Registration?');
            if (ret == true) {
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>


    <%-- <script src="../JAVASCRIPTS/Inspect.js"></script>--%>
    <%--<script>--%>
    <%--// ADDED BY GAURAV SONPAROTE 16_08_2023

        $(document).ready(function () {
            var getSystemOS = getMobileOperatingSystem();
            if (getSystemOS !== "iOS") {
                console.log('Is DevTools open:', window.devtools.isOpen);
                console.log('DevTools orientation:', window.devtools.orientation);
                if (window.devtools.isOpen == true && window.devtools.orientation != undefined) {
                    alert("Please Close The Inspector Window.");
                    location.reload();
                }
                // Get notified when it's opened/closed or orientation changes
                window.addEventListener('devtoolschange', function (event) {
                    // window.addEventListener('devtoolschange', event => {
                    console.log('Is DevTools open:', event.detail.isOpen);
                    console.log('DevTools orientation:', event.detail.orientation);
                    console.log(event);
                    if (event.detail.isOpen == true && event.detail.orientation != undefined) {
                        alert("Please Close The Inspector Window.");
                        location.reload();
                    }
                });
            }
            function getMobileOperatingSystem() {
                var userAgent = navigator.userAgent || navigator.vendor || window.opera;

                // Windows Phone must come first because its UA also contains "Android"
                if (/windows phone/i.test(userAgent)) {
                    return "Windows Phone";
                }

                if (/android/i.test(userAgent)) {
                    return "Android";
                }

                // iOS detection from: http://stackoverflow.com/a/9039885/177710
                if (/iPad|iPhone|iPod/.test(userAgent) && !window.MSStream) {
                    return "iOS";
                }

                return "unknown";
            }
        });

        $(document).ready(function () {

            $(document)[0].oncontextmenu = function () { return true; }

            $(document).mousedown(function (e) {
                if (e.button == 2) {
                    return true;
                } else {
                    return true;
                }
            });
            // FOR right click off ---------------START  
            //$(document).ready(function () {

            //    $(document)[0].oncontextmenu = function () { return false; }

            //    $(document).mousedown(function (e) {
            //        if (e.button == 2) {
            //            return false;
            //        } else {
            //            return true;
            //        }
            //    });

            // --------------------------------------END 
            document.onkeydown = function (e) {
                if (event.keyCode == 123) {
                    return false;
                }
                if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                    return false;
                }
                if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                    return false;
                }
                if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                    return false;
                }
                if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                    return false;
                }
            }
        });
    </script>--%>



    

</asp:Content>


