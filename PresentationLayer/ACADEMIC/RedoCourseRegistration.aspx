<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" ViewStateEncryptionMode="Always" EnableViewStateMac="true"
     CodeFile="RedoCourseRegistration.aspx.cs" Inherits="ACADEMIC_RedoCourseRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        function totAllSubjects(headchk) {

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
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
                }

            }

        }




    </script>

    <%-- <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>
    <div id="dvMain" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-12">
                <div class="box box-primary">
                    <div id="div1" runat="server"></div>
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
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
                                        <asp:TextBox ID="txtEnrollno" runat="server" CssClass="form-control" ToolTip="Enter text to search." TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtEnrollno"
                                            Display="None" ErrorMessage="Please Enter RRN No." SetFocusOnError="true"
                                            ValidationGroup="search" />
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
                                        <li class="list-group-item"><b>Current Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="true" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Session :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSession" runat="server" Font-Bold="true" /></a>
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
                                            <asp:HiddenField ID="hdfDegreeno" runat="server" />
                                        </li>
                                        <li class="list-group-item"><b>Scheme :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="true" /></a>
                                        </li>

                                        <li id="liDemandAmount" runat="server" class="list-group-item"><b>Demand Amount :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDemandAmt" runat="server" Font-Bold="true" /></a>
                                        </li>
                                        <%-- <li class="list-group-item" style="display:none"><b>Backlog Semester :</b>
                                            <%--<a class="sub-label">
                                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList></a>
                                            <asp:DropDownList ID="ddlBackLogSem" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                ValidationGroup="backsem" AutoPostBack="True" OnSelectedIndexChanged="ddlBackLogSem_SelectedIndexChanged" data-select2-enable="true" TabIndex="5">
                                                <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>--%>
                                    </ul>
                                </div>
                                <%-- <div class="col-12 mt-3">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                            <div class="label-dynamic">
                                                <label>Backlog Semester</label>
                                            </div>
                                            <asp:DropDownList ID="ddlBackLogSem1" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                ValidationGroup="backsem" AutoPostBack="True" OnSelectedIndexChanged="ddlBackLogSem_SelectedIndexChanged" data-select2-enable="true" TabIndex="5" Visible="false">
                                            </asp:DropDownList>
                                            <%--       <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>
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
                                </div>--%>

                                <div class="col-12">
                                    <asp:Panel ID="pnlFailCourse" runat="server">
                                        <asp:HiddenField ID="hfdRedoCrsTbl" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hfdFeedetails" runat="server" ClientIDMode="Static" />
                                        <asp:ListView ID="lvFailCourse" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Offered Course List</h5>
                                                </div>
                                                <div style="width: 100%; overflow: auto">
                                                    <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap">
                                                        <thead>
                                                            <tr>
                                                                <th id="thselect">
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="totAllSubjects(this);" />
                                                                    Select All
                                                                </th>
                                                                <th>Course Code</th>
                                                                <th>Course Name</th>
                                                                <th style="text-align: center; display: none">Semester</th>
                                                                <th style="text-align: center">Course Type</th>
                                                                <th style="text-align: center">Credits</th>
                                                                <th>Amount</th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow" class="item">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" Checked='<%#(Convert.ToInt32(Eval("RE_REGISTER"))==1 ? true : false)%>' onclick="backlogLvChk();" />
                                                        <%-- Enabled='<%#(Convert.ToInt32(Eval("REGISTERED"))==1 ? false : true)%>'--%>
                                                         <asp:HiddenField ID="hfdSubID" runat="server" Value='<%# Eval("SUBID") %>'/>
                                                        <asp:HiddenField ID="hfdGrade" runat="server" Value='<%# Eval("GRADE") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                        <asp:HiddenField ID="hfdCredits" runat="server" Value='<%# Eval("CREDITS") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <asp:Label ID="lblsemester" runat="server" Text=' <%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SEMESTERNO")%>' />
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("SUBNAME") %>
                                                       
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("CREDITS") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDemandAmount" runat="server" Text='<%# Eval("DEMAND_AMT") %>' />
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
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnStudSubmit_Click" Text="Submit" CausesValidation="false" Visible="false"
                                        CssClass="btn btn-primary" TabIndex="8" />
                                    <asp:Button ID="btnStudSubmit" runat="server" Text="Save/Confirm" OnClick="btnStudSubmit_Click" CausesValidation="false"
                                        CssClass="btn btn-primary" TabIndex="9" />
                                    <asp:Button ID="btnPayment" runat="server" Text="Payment" Visible="false" TabIndex="9" OnClick="btnPayment_Click" CausesValidation="false"
                                        CssClass="btn btn-success" />
                                    <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click" CausesValidation="false"
                                        CssClass="btn btn-primary" TabIndex="9" Visible="false" />
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlOfferedCourse" runat="server">
                                        <asp:ListView ID="lvOfferedCourse" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Backlog Course</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th style="text-align: center">Semester
                                                            </th>
                                                            <th style="text-align: center">Course Type
                                                                 
                                                            </th>
                                                            <th style="text-align: center">Grade                                                                
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
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("SEMESTER") %>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("SUBNAME") %>
                                                    </td>
                                                    <td align="center">
                                                        <%# Eval("GRADE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
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
    <script type="text/javascript">
       
        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }
        function BacklogCount() {
            debugger;
            // alert("2")
            if (document.getElementById('tblBacklogSubjects') != null) {
                var backlogCounter = 0;
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
                if (dataRows != null) {
                    var backlogFineAmt = $("#ctl00_ContentPlaceHolder1_lblBacklogFine").text();
                    //alert(backlogFineAmt)
                    for (i = 0; i < (dataRows.length - 1) ; i++) {
                        //alert("In Backlog")
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + i + '_chkAccept').checked) {
                            //  alert("In Backlog")
                            backlogCounter = backlogCounter + 1;
                            //alert(backlogCounter)
                        }
                    }
                }
                return backlogCounter;
            }
        }
        function backlogLvChk() {
            debugger;
            //var count;
            if (document.getElementById('tblBacklogSubjects') != null) {
                dataRows = document.getElementById('tblBacklogSubjects').getElementsByTagName('tr');
                if (dataRows != null) {
                    var feeData = $("#hfdFeedetails").val();
                    var d = [];
                    if (feeData != '')
                        d = $.parseJSON(feeData);

                    //var clickedRow = chk.parentNode.parentNode;
                    //var subID = clickedRow.querySelector('#hfdSubID').value;
                    //var grade = clickedRow.querySelector('#hfdGrade').value;

                    var totfees = 0;
                    for (var k = 0; k < dataRows.length - 1; k++) {
                        //var chkAccept = $('#ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_chkAccept');
                        if (document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_chkAccept').checked) {
                            var subID = $('#ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_hfdSubID').val();
                            var grade = $('#ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_hfdGrade').val();

                            if (subID != '' && grade != '') {
                                if (d != null && d.length > 0) {
                                    for (var i = 0; i < d.length; i++) {
                                        if (d[i]["SUBID"] == subID) {
                                            var fees = d[i]["FEE"];

                                            if (grade == 'U')
                                                fees = fees / 2;

                                            if (fees > 0) {
                                                document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_lblDemandAmount').innerHTML = fees;
                                                totfees += fees;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                            document.getElementById('ctl00_ContentPlaceHolder1_lvFailCourse_ctrl' + k + '_lblDemandAmount').innerHTML = '';
                    }

                    if (totfees > 0)
                        document.getElementById('ctl00_ContentPlaceHolder1_lblDemandAmt').innerHTML = totfees;                  
                }
            }
        }
    </script>
</asp:Content>

