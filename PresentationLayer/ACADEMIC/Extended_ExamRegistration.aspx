<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Extended_ExamRegistration.aspx.cs" Inherits="Academic_Extended_ExamRegistration" Title="" %>

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
                <%--<-------------------------------------------------------------------------------------------------------------->--%>

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
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                               
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
                                             <li class="list-group-item"><b>Session :</b>
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
                                    <asp:HiddenField ID="hdfCategory" runat="server" />
                                    <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                </div>
                            </div>
                           
                            <div class="col-12 mt-4">
                                <asp:Panel ID="pnlFailCourse" runat="server">
                                    <%--<span style="color:red" class="pull-right">Note: For Final Year Students Rs.1400/- is added for Certificate Purpose  </span>--%>


                                    <asp:ListView ID="lvFailCourse" runat="server" OnItemDataBound="lvFailCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Courses List</h5>
                                            </div>
                                            <div>
                                                <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap ">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                <%--<asp:CheckBox ID="chkAll" Checked="true" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />--%>
                                                                All
                                                            </th>
                                                            <th>COURSE CODE
                                                            </th>
                                                            <th>COURSE NAME
                                                            </th>
                                                            <th>CREDIT
                                                            </th>
                                                            <th>COURSE TYPE                                                                   
                                                            <%--</th>
                                                            <th>EXAM TYPE --%>                                                                 
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
                                                   
                                                    <%--<asp:CheckBox ID="chkAccept" runat="server" Enabled='<%#Eval("STUD_EXAM_REGISTERED").ToString()=="1" ? false : true%>' Checked='<%# Eval("STUD_EXAM_REGISTERED").ToString() == "1" ? true : false %>'/>--%>
                                                    
                                                    <%--<asp:CheckBox ID="chkAccept" runat="server" Checked="false" AutoPostBack="true" onclick="CheckSelectionCountgrp1(this);"  OnCheckedChanged="chkAccept_CheckedChanged"/>--%>

                                                    <asp:CheckBox ID="chkAccept" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged"/>


                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    <asp:HiddenField ID="hdfExamRegistered" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' />
                                                    <asp:HiddenField ID="hdfStudRegistered" runat="server" Value='<%# Eval("EXT_SEM") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblcredits" runat="server" Text=' <%# Eval("CREDITS") %>' />

                                                </td>
                                                <td>

                                                    <%# Eval("SUBNAME") %>
                                                </td>

                                               <%-- <td>

                                                    <asp:Label ID="lblExamType" runat="server" Text='<%# Eval("EXAMTYPE") %>' ToolTip='<%# Eval("EXAMTYPENO")%>' />
                                                </td>--%>
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
                            Font-Bold="true" CssClass="btn btn-primary" TabIndex="9" OnClientClick="return showConfirm1();;" />
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CausesValidation="false"
                            Font-Bold="true" CssClass="btn btn-primary" TabIndex="8" />
                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Report" OnClick="btnPrintRegSlip_Click" CausesValidation="false"
                           Font-Bold="true" CssClass="btn btn-info" TabIndex="9" Visible="true" />
                    </div>
                    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                    <div id="divMsg" runat="server">
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

    <%---------------------------------checkbox validation -----------------------------------------%>
  <script>
        //function validateCheckboxes() {
        //    // Get all checkboxes
        //    var checkboxes = document.querySelectorAll('input[name="checkbox"]');
  
        //    // Count the number of checked checkboxes
        //    var checkedCount = 0;
        //    for (var i = 0; i < checkboxes.length; i++) {
        //        if (checkboxes[i].checked) {
        //            checkedCount++;
        //        }
        //    }

        //    // Check if the number of checked checkboxes is not more than 5
        //    if (checkedCount > 5) {
        //        alert('Only 5 checkboxes are allowed to be checked.');
        //        // Uncheck the last checked checkbox
        //        checkboxes[checkboxes.length - 1].checked = false;
        //    }
        //}

      function CheckSelectionCountgrp1(chk) {
          debugger;
          var count = 0;
          var checkboxes = document.querySelectorAll('[id*=chkAccept]'); // Use a more flexible selector

          for (var i = 0; i <= checkboxes.length; i++) {
              if (count>=4) {
                  chk.checked = false;
                  alert("Only 8 Courses was Allowed for single semester..! ");
                  return;
              } else if (checkboxes[i].checked) {
                  count += 1;
              }
          }
          return;

      }
      
  </script>

</asp:Content>
