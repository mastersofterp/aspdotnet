﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RetestExamRegistration_All.aspx.cs" Inherits="Academic_Examination_ExamRegistration" Title="" %>

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
                             SumTotal();
                             var j = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_lblAmt').innerText);
                            //// alert(j);
                             sum += parseFloat(j);
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
                    SumTotal();
                }
                else {
                    SumTotal();
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
              //  var lbl = document.getElementById('<%=lblTotalExamFee.ClientID %>');
               // $("[id*=ctl00_ContentPlaceHolder1_lblTotalExamFee]").text(total + '.00');
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
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">SUBSTITUTE EXAM REGISTRATION</h3>
                        <br />
                        <%--<span style="color:red;font-weight:400;font-size:medium;">Note: For Final Semester Students Rs.1400 is added for the following:<br />
                                Provisional Certificate - Rs. 400/-
                               <br /> Consolidated grade Sheet - Rs. 400/-
                               <br /> Degree Certificate - Rs. 600/- </span>--%>
                    </div>
                    <div class="box-body">
                        <div id="pnlSearch" runat="server">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlclgname" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlclgname_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvclg" runat="server" ErrorMessage="Please Select College"
                                            ControlToValidate="ddlclgname" Display="None" InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" InitialValue="0" />
                                    </div>
                                    <div id="divenroll" class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>RRN No</label>
                                        </div>
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="2" OnTextChanged="txtEnroll_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                            Display="None" ErrorMessage="Please Enter RRN No." SetFocusOnError="true"
                                            ValidationGroup="search" />
                                    </div>

                                    
                                       <%--  <div>
                                        <asp:DropDownList ID="ddlsessionforabsent" OnSelectedIndexChanged="ddlsessionforabsent_SelectedIndexChanged" runat="server"
                                            AppendDataBoundItems="true" data-select2-enable="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                             </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsessionforabsent"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                --%>



                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsessionnew" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" InitialValue="0" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnProceed_Click" Text="Show" ValidationGroup="search" CssClass="btn btn-primary" TabIndex="3" />
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" ValidationGroup="Show" OnClick="btnCancel_Click" TabIndex="4"
                                    CssClass="btn btn-warning" />
                            </div>
                        </div>
                        <div class="col-12" id="divCourses" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
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
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Admission Batch :</b>
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
                                        <li class="list-group-item"><b>Total Substitute Exam Fee :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTotalExamFee" runat="server" Font-Bold="true" /></a>
                                            <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Font-Bold="true"  Visible="false">0</asp:Label>
                                            <asp:HiddenField ID="hdfExamLastDate" runat="server" />
                                             <asp:HiddenField ID="hdfDuration" runat="server" />
                                             <asp:HiddenField ID="hdfSemester" runat="server" />
                                            <asp:HiddenField ID="hdfExamLateFee" runat="server" />
                                        </li>

                                        <%-- <li class="list-group-item">
                                            <a class="sub-label">
                                                <asp:Label ID="Stuatus" runat="server" Font-Bold="true" Visible="true"/>Pending</a>
                                            <asp:Label ID="statusforapprov" runat="server" CssClass="data_label" Font-Bold="true"  Visible="true">Status :</asp:Label>
                                           
                                        </li>--%>
                                    </ul>
                                </div>
                                <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label>Backlog Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBackLogSem" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                ValidationGroup="backsem" AutoPostBack="True" OnSelectedIndexChanged="ddlBackLogSem_SelectedIndexChanged" data-select2-enable="true" TabIndex="5" Visible="false">
                                            </asp:DropDownList>
                                            <%--       <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>--%>
                                            <asp:HiddenField ID="hdfCategory" runat="server" />
                                            <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
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
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click1" Text="Show Courses" ValidationGroup="backsem" TabIndex="6"
                                        CssClass="btn btn-primary" Visible="false" />
                                    <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Print Reciept" TabIndex="7"
                                        ValidationGroup="backsem" CssClass="btn btn-primary" Visible="false" Style="display: none;" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="backsem"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlFailCourse" runat="server">
                                        <%--<span style="color:red" class="pull-right">Note: For Final Year Students Rs.1400/- is added for Certificate Purpose  </span>--%>

                                       <%-- <span id="notefinal" runat="server" visible="false" style="color: red; font-weight: 700; font-size: medium;">Note: For Final Semester Students Rs.1400 is added for the following:<br />
                                            Provisional Certificate - Rs. 400/-
                               <br />
                                            Consolidated grade Sheet - Rs. 400/-
                               <br />
                                        <%--    Degree Certificate - Rs. 600/- </span>--%>
                                       <%-- <div  style= "text-align: center;">
                                        <asp:Button ID="Button1" runat="server"  Text="Online Pay" CausesValidation="false" OnClick="btnSubmit_Click"
                                        Font-Bold="true" CssClass="btn btn-success" TabIndex="8" visible="false"/>
                                                </div>--%>
                                        <div class="row">
                                            
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlexamnameabsentstudent" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True"  data-select2-enable="true" OnSelectedIndexChanged="ddlexamnameabsentstudent_SelectedIndexChanged"> <%--OnSelectedIndexChanged="ddlexamnameabsentstudent_SelectedIndexChanged"--%>
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlexamnameabsentstudent"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sub Exam Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlsubexamname" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True"  data-select2-enable="true" OnSelectedIndexChanged="ddlsubexamname_SelectedIndexChanged"> 
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlsubexamname"
                                            Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                            </div>
                                        <asp:ListView ID="lvFailCourse" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Courses List</h5>
                                                </div>
                                                <div style="width: 100%; overflow: auto">
                                                    <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                    <%--<asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" />--%>
                                                                    Select 

                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th style="text-align: center">Semester
                                                                </th>
                                                                <th style="text-align: center">Course Type
                                                                    <%--Theory/Prac--%>
                                                                </th>
                                                               <%-- <th style="text-align: center">Grade
                                                                    <%--Theory/Prac--%>
                                                                <%--</th>--%>
                                                               <%-- <th style="text-align: center">SUBEXAM NAME
                                                                   
                                                                </th>--%>
                                                                <th style="text-align: center">Exam Fees
                                                                    <%--Theory/Prac--%>
                                                                <%--</th>--%>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow" class="item">
                                                    <td>
                                                        <%--<asp:CheckBox ID="chkAccept" runat="server" Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' Enabled='<%# Eval("ACCEPTED").ToString() == "0" ? true : false %>' />--%>
                                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" ToolTip="Click to select this subject for registration" onclick="backlogLvChk(this);" />--%>
                                                       
                                                        
                                                         <%--<asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" 
                                                              Checked='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? true:false %>' Enabled='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? false:true %>'
                                                                    OnCheckedChanged="ChckedTotal_change"/> --%>

                                                        <asp:CheckBox ID="chkAccept" runat="server" AutoPostBack="true" 
                                                             Checked='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? true:false %>' Enabled='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? false:true %>'
                                                                    OnCheckedChanged="ChckedTotal_change"/> 
                                                        <%-- Checked='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? true:false %>' Enabled='<%# Eval("STUDENT_REQUEST").ToString()=="1" ? false:true %>'--%>
                                                        
                                                        <%--OnCheckedChanged="chkAccept_CheckedChanged"--%><%--Checked='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? true : false)%>' --%>
                                                        <%-- Enabled='<%# Eval("REGISTERED").ToString()=="1" && Eval("EXAMTYPENO").ToString()=="1" && Eval("EXAM_REGISTERED").ToString()=="1"? false : true%>'--%>
                                                        <%--Enabled='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? false : true)%>' onclick="backlogLvChk(this);" OnCheckedChanged="ChckedTotal_change" />--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />

                                                    </td>
                                                    <td>
                                                       <%-- <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("STUDENT_REQUEST")%>'/>--%>
                                                          <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' ToolTip='<%# Eval("ADMIN_APPROVE")%>'/>
                                                       <asp:HiddenField ID="hdfstudapplied" Value='<%# Eval("STUDENT_REQUEST") %>' runat="server" />
                                                        <asp:HiddenField ID="hdfexistMarks" Value='<%# Eval("EXISTS_MARK") %>' runat="server" />
                                                        <asp:HiddenField ID="hdfExamRegistered" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                        <asp:HiddenField ID="hdfStudRegistered" runat="server" Value='<%# Eval("STUD_EXAM_REGISTERED") %>' />
                                                       
                                                     
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblsemester" runat="server" Text=' <%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                        <%-- <%# Eval("SEMESTER") %>--%>
                                                    </td>
                                                    <td align="center">
                                                        <%-- <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical" %>--%>
                                                        <%# Eval("SUBNAME") %>
                                                    </td>
                                                  <%--  <td align="center">
                                                        <%# Eval("SUBEXAMNAME") %>
                                                    </td>--%>
                                                   <%-- <td align="center">
                                                        <%--  <%# Eval("EXAMTYPE") %>--%>
                                                       <%-- <asp:Label ID="lblExamType" runat="server" Text='<%# Eval("EXAMTYPE") %>' ToolTip='<%# Eval("EXAMTYPENO")%>' />
                                                    </td>--%>
                                                    <td align="center">
                                                        <asp:Label ID="lblAmt" runat="server" Text='<%#Eval("AMOUNT") %>' ToolTip='<%# Eval("AMOUNT")%>'/>
                                                   <%--  <%# Eval("AMOUNT") %>--%>
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
                                    <asp:Panel ID="pnlFailInaggre" runat="server" Visible="true">
                                        <asp:ListView ID="lvFailInaggre" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Improve Fail In Aggregate</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>
                                                                <%--<asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />--%>
                                                                    Select
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Theory/Prac
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </thead>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow" class="item">
                                                    <td>
                                                        <asp:CheckBox ID="chkAcceptSub" runat="server" /><%--onclick="CheckSelectionCount(this)" --%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTER") %>
                                                    </td>
                                                    <td style="font-weight: bold" align="center">
                                                        <%--    <%# (Eval("SUBID").ToString()) == "1" ? "Theory" : "Practical"%>--%>
                                                        <%# Eval("SUBNAME") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnsave" runat="server"  Text="Submit" CausesValidation="false"
                                        Font-Bold="true" CssClass="btn btn-success" TabIndex="7" OnClick="btnsave_Click"/> <%--OnClick="btnsave_Click"--%>
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Online Pay" CausesValidation="false"
                                        Font-Bold="true" CssClass="btn btn-success" TabIndex="8" visible="false"/>
                                    <asp:Button ID="btnPrintRegSlip" runat="server" Text="Print Receipt" OnClick="btnPrintRegSlip_Click" CausesValidation="false"
                                        CssClass="btn btn-primary" TabIndex="9" Visible="false" />
                                </div>
                                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                                <div id="divMsg" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
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
       // function backlogLvChk(chk) {
       //     debugger;
       //     var count;
       //     if (document.getElementById('tblBacklogSubjects') != null) {
       //        dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
       //         if (dataRows != null) {
       //             //alert("1")
       //             count = BacklogCount();
       //
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
    <script>
        function myFunction() {
            debugger
            //confirm("Are you sure do you want to Lock Mark");
            var check
            var msg = "Are you sure do you want to Lock Mark.";

            if (confirm(msg) == true) {
                return true;
            }
            else {

                return false;
            }

        }
</script>
</asp:Content>
