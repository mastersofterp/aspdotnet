<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="AttendanceEntry_Modified.aspx.cs" Inherits="ACADEMIC_TIMETABLE_AttendanceEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%#Page.ResolveClientUrl("~/fullcalendar/fullcalendar.css")%>" rel="stylesheet" />

    <%--<style>
        #ctl00_ContentPlaceHolder1_pnlAttendenceEntry .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>--%>

  <style>
        .Searchfilter {
            font-size: 15px !important;
            padding: 0.375rem 0.75rem !important;
            display: block !important;
            width: 100% !important;
            height: 42px !important;
            background-color: transparent !important;
            border: 1px solid #ced4da !important;
            border-radius: 0.25rem !important;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out !important;
            margin-left: -15px !important;
            margin-bottom: 5px !important;
        }
    </style>

 <style>
        #ctl00_ContentPlaceHolder1_pnlAttendenceEntry .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .attendancestudent .dataTables_filter {
            display: none !important;
        }

        .attendancestudent .dt-buttons {
            display: none !important;
        }
    </style>

    <style type="text/css">
        .modal-backdrop {
            display: none;
        }

        body {
            padding: 0 !important;
            margin: 0 !important;
        }

        .mytable.table {
            margin-bottom: 0;
            font-family: 'PT Sans', sans-serif;
        }

        .mytable td {
            position: relative;
            padding: 0px;
        }

        .mytable.table td {
            background-color: transparent !important;
            text-decoration: none !important;
            border-color: #ccc;
        }

            .mytable.table td span.fa.fa-calendar-check-o {
                position: absolute;
                left: 12.5%;
                top: 11.5%;
                transform: translateX(-30%);
                font-size: 120%;
                display: table;
                margin: 0;
            }

            .mytable.table td a span:nth-child(2) {
            }

            .mytable.table td span:nth-child(1) {
                position: static;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                font-weight: 500;
                font-size: small;
                /*text-decoration: line-through;*/
            }

            .mytable.table td .fa {
                position: absolute;
                top: 5.5%;
                right: 14%;
            }

            .mytable.table td span + span.fa.fa-unlock {
                right: 8%;
            }

            .mytable.table td a {
                display: block;
                position: absolute;
                top: 3px;
                bottom: 0;
                left: 0;
                right: 0;
                z-index: 0;
            }

        .mytable.table tr:nth-child(1) td a::before {
            display: none !important;
        }

        .mytable.table td a::before {
            content: '';
            display: block;
            position: absolute;
            height: 27px;
            width: 27px;
            top: -3%;
            border-radius: 50%;
            left: 39%;
            transform: scale(0);
            background-color: rgba(0,0,0,0.08);
            padding: 5px;
            z-index: -1;
        }

        .mytable.table td:hover a::before {
            background-color: rgba(0,0,0,0.15);
            transform: scale(1);
            transition: .2s;
        }

        .mytable.table td a + span {
            position: static;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            font-size: small;
            font-weight: 600;
        }

        .mytable.table td span:nth-child(1), .mytable.table td .fa, .mytable.table td a + span {
            color: #555;
        }

        .mytable.table > tbody > tr:nth-child(1) > td {
            background-color: #255282 !important;
            font-size: 125%;
            font-weight: 600;
        }

            .mytable.table > tbody > tr:nth-child(1) > td, .mytable.table > tbody > tr:nth-child(1) > td a {
                color: #FFF !important;
                font-weight: 300;
            }

        .table-bordered {
            /*border: 1px solid #ddd !important;*/
            border-color: #ccc;
        }

        .box {
            box-shadow: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            /*border: 1px solid #ccc;*/
        }

        table.mytable {
            border-collapse: collapse;
            /*border-radius: 1em;*/
            overflow: hidden;
            box-shadow: 0px 2px 3px rgba(0,0,0,0.3);
        }

        .mytable.table td span:nth-child(1).fa.fa-lock {
            display: none;
        }

        .mytable.table td span:nth-child(1).fa.fa-unlock {
            display: none;
        }

        .lbltop {
            margin-top: 0px;
            margin-bottom: 10px;
        }

        .txtTopClass {
            font-size: inherit;
            font-family: inherit;
            padding: 5px 12px;
            letter-spacing: normal;
            background: #fff !important;
            color: #3c4551;
            border-radius: 5px;
            font-weight: 400;
            border-left: 6px solid #25CD7F !important;
        }
    </style>

    <style type="text/css">
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 3px;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
        }

        .dataTables_filter {
            display: none;
        }

        .mytable.table td.shifted-lecture {
            border-color: #ccc !important;
        }

            .mytable.table td.shifted-lecture::before {
                content: '';
                display: block;
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background-color: rgba(255, 204, 102,0.4);
                height: 100%;
                z-index: 0;
            }

        .mytable.table td.today-date {
            background-color: rgba(46, 170, 8,.2) !important;
            border-color: #ccc !important;
        }
    </style>

    <style>
        .notify-table .fa.fa-unlock {
            color: #36c60a;
            opacity: .5;
        }

        .notify-table .fa.fa-lock {
            color: #d11e1e;
            opacity: .2;
        }

        .notify-table .fa.fa-calendar-check-o {
            color: #FF0000;
            opacity: .5;
        }

        .notify-table .fa.fa-square {
            color: rgba(255, 204, 102,0.4);
        }

            .notify-table .fa.fa-square.green {
                color: rgba(46, 170, 8,.4);
            }

        .notify-table .blue.fa.fa-calendar-check-o {
            color: #03A9F3;
        }

        .notify-table .fa-2x {
            font-size: 18px;
        }

        .notify-table .table-bordered > tbody > tr > td {
            font-size: 12px !important;
            font-weight: 500;
        }

        .notify-table .table-bordered > tbody > tr {
            cursor: pointer;
        }

        .notify-table .r-lecture.fa.fa-font {
            color: magenta;
        }
    </style>
    <style>
        /*#preloader {
            display: none;
        }*/

        .list-group .list-group-item .sub-label {
            float: initial;
        }
    </style>
    <%--  -----------loader-----------------------%>
    <%-- <script type="text/javascript">
        $(window).on("load", function () {
            $("#preloader").hide();
        });
    </script>--%>
    <script type="text/javascript">
        function showModal() {
            //alert('hii');
            // $.noConflict();
            $("#myModal1").modal('show');
            $('body').addClass('modal-open');
            // $('.modal-backdrop').css({ "position": "fixed" }).addClass("fade in");
        }

        function hideModal() {
            //alert("hello");
            // $.noConflict();
            $("#myModal1").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').css({ "position": "static" });
            $('.modal-backdrop').removeClass(".fade .in").removeClass("fade in");
            return false;
        }
    </script>


    <script type="text/javascript">
        function showModalATT() {
            //alert('hii');
            // $.noConflict();
            $("#idModal").modal('show');
            $('body').addClass('modal-open');
            // $('.modal-backdrop').css({ "position": "fixed" }).addClass("fade in");
        }

        function hideModalATT() {
            //alert("hello");
            // $.noConflict();
            $("#idModal").modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').css({ "position": "static" });
            $('.modal-backdrop').removeClass(".fade .in").removeClass("fade in");
            return false;
        }
    </script>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeachingPlan"
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

    <asp:UpdatePanel ID="updTeachingPlan" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <%--  Attendance Entry--%>
                                <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="">
                                    <div id="divFaculty" class="row" runat="server" visible="false">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Faculty</label>
                                            </div>
                                            <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" AutoPostBack="true"
                                                data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="dvAtt" runat="server">
                                        <div id="pnlAttendenceEntry" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-12 col-md-12 col-lg-9">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Date :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="txtLectDate" runat="server" Font-Bold="true"></asp:Label>
                                                            </a>
                                                        </li>
                                                        <li class="list-group-item"><b>
                                                            <asp:Label ID="lblDYddlCourse" runat="server" Font-Bold="true"></asp:Label>
                                                            :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="txtCourse" Font-Bold="true" runat="server"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                    <div class="row mt-3">
                                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Class Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlClassType" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                Font-Bold="True">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Attendance Status</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                                Font-Bold="True" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-4 col-12">
                                                            <div class="label-dynamic">
                                                                <sup id="lblAttend" runat="server" visible="true">* </sup>
                                                                <label>Topic Covered</label>
                                                            </div>
                                                            <asp:TextBox ID="txtTopcDesc" placeholder="Enter Topic Covered" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvtopcv" runat="server" ControlToValidate="txtTopcDesc" Display="None" ValidationGroup="Submit" ErrorMessage="Please Enter Topic Covered" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <asp:DropDownList ID="ddlTopicCovered" runat="server" data-select2-enable="true" AppendDataBoundItems="true" Visible="false">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divTopicCoveredStatus" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup id="Sup1" runat="server">* </sup>
                                                                <label>Topic Covered Status</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddltopicstatus" runat="server" data-select2-enable="true" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Started</asp:ListItem>
                                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                                <asp:ListItem Value="3">Continued</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfctopicstatus" runat="server" ControlToValidate="ddltopicstatus" InitialValue="0" Display="None" ValidationGroup="Submit" ErrorMessage="Please Select Topic Covered Status" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 col-md-6 col-lg-3">
                                                    <table class="table table-bordered nowrap">
                                                        <tbody>
                                                            <tr>
                                                                <td>Total </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalStudent" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Present </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPresentStudent" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Absent </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAbsentStudent" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td>OD </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtODStudent" runat="server" Enabled="False" CssClass="form-control"></asp:TextBox></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>CO Number</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCONumber" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">CO-1</asp:ListItem>
                                                        <asp:ListItem Value="2">CO-2</asp:ListItem>
                                                        <asp:ListItem Value="3">CO-3</asp:ListItem>
                                                        <asp:ListItem Value="4">CO-4</asp:ListItem>
                                                        <asp:ListItem Value="5">CO-5</asp:ListItem>
                                                        <asp:ListItem Value="6">CO-6</asp:ListItem>
                                                        <asp:ListItem Value="7">CO-7</asp:ListItem>
                                                        <asp:ListItem Value="8">CO-8</asp:ListItem>
                                                        <asp:ListItem Value="9">All-CO</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>


                                            <!-- /.box-body -->
                                            <div class="row">
                                                <div class="col-12 btn-footer">
                                                    <asp:HiddenField ID="hdnSlotVal" runat="server" />
                                                    <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return checkTC(this);" Text="Submit" ValidationGroup="Submit" CssClass="btn btn-primary"> Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnRegister" runat="server" CausesValidation="False" CssClass="btn btn-primary" OnClick="btnRegister_Click">Attendance Register</asp:LinkButton>

                                                    <asp:LinkButton ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-info"><span class="glyphicon glyphicon-step-backward">Back</span></asp:LinkButton>
                                                    <%-- <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn btn-info  glyphicon-step-backward" />--%>
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-warning" />
                                                    <input type="button" onclick="return ShowExistingSlot()" value="Copy Attendance" class="btn btn-primary" id="btnCopyAtt" style="display: none;" />
                                                    <asp:ValidationSummary runat="server" ID="vsummery1" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-12 attendancestudent" >
                                                    <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudent_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <%-- Search Textbox Modified by Gopal M - 18/08/2023--%>

                                                            <div class="col-lg-3 col-md-6">
                                                                <div class="input-group sea-rch">
                                                                    <%-- <div class="label-dynamic">
                                                                           <label>Search</label>
                                                                      </div>--%>
                                                                    <input type="text" id="FilterData" onkeyup="SearchFunction()" placeholder="Search" class="Searchfilter" />
                                                                    <%--  <div class="input-group-addon">
                                                                           <i class="fa fa-search"></i>
                                                                     </div>--%>
                                                                </div>
                                                            </div>
                                                            <%-- Search end--%>
                                                            <table class="table table-striped table-bordered nowrap display" id="tblStudentRecordsnew" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Checked="true" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblDYRRNo" runat="server" Font-Bold="true"></asp:Label>
                                                                        </th>
                                                                        <th>Roll No.
                                                                        </th>
                                                                        <th>Name
                                                                        </th>
                                                                        <th>Late by Time (MM:SS)
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead1" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead2" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead3" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead4" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead5" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead6" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                        <th>
                                                                            <asp:Label ID="lblhead7" runat="server" Text='' ToolTip=''
                                                                                Font-Size="9pt" />
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tblStudentRecords" style="overflow: auto">
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:CheckBox ID="cbRow" runat="server" Checked='<%#Eval("AttStatus").ToString() == "1" ? true : false %>' onclick="CheckSelectionCount(this)" ToolTip='<%# Eval("IDNO")%>' />
                                                                    <asp:HiddenField ID="hdfIdNo" runat="server" Value='<%# Eval("IDNO")%>' />
                                                                    <asp:HiddenField ID="hdfLeaveStatus" runat="server" Value='<%# Eval("LeaveStatus")%>' />
                                                                    <asp:HiddenField ID="hdfAttStatus" runat="server" Value='<%# Eval("AttStatus")%>' />
                                                                    <asp:HiddenField ID="hdfAttdone" runat="server" Value='<%# Eval("attDone")%>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblRollNo" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ROLLNO") %>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLTime" runat="server" Enabled='<%#Eval("AttStatus").ToString() == "1" ? true : false %>' Text='<%# Eval("LMARK_TIME")%>' placeholder="MM:SS" CssClass="form-control" onblur="checkTime(this)" Height="20px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbl1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>

                                            </div>

                                            <div class="row mt-3">
                                                <div class="form-group col-lg-9 col-md-12 col-12">
                                                    <div class=" note-div">
                                                        <h5 class="heading">Note</h5>
                                                        <p runat="server" id="spanNote"><i class="fa fa-star" aria-hidden="true"></i><span>[Note : Please click on date to mark attendance]</span></p>
                                                        <p>
                                                            <i class="fa fa-star" aria-hidden="true"></i><span>[Checked = Present, Unchecked = Absent,
                                                            <asp:Label ID="lblDYtxtRegNo" runat="server"></asp:Label>
                                                                (Green)= OD Approved,
                                                            <asp:Label ID="lblDYRNo" runat="server"></asp:Label>
                                                                (Red)= Absent] </span>
                                                        </p>
                                                        <p><i class="fa fa-star" aria-hidden="true"></i><span>Attendance Status (Holiday) - Attendance will not calculated.</span></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row" id="attpanel" runat="server" visible="false">
                                            <div class="col-12" runat="server" id="divCalendar">
                                                <asp:UpdatePanel runat="server" ID="updatePanelClass">
                                                    <ContentTemplate>

                                                        <div id="calDiv">
                                                            <%--Added by Swapnil For Global Elective--%>
                                                            <div class="col-12 form-group">
                                                                <div class="label-dynamic">
                                                                    <label>Select Course Type</label>
                                                                </div>
                                                                <asp:RadioButton ID="rdoCore" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="rdoCore_CheckedChanged" Text="Core/Elective" GroupName="coursetype" />
                                                                <asp:RadioButton ID="rdoGlobalElective" runat="server" AutoPostBack="true" OnCheckedChanged="rdoGlobalElective_CheckedChanged" Text="Open/Global Elective" GroupName="coursetype" />
                                                            </div>
                                                            <%--End Global Elective--%>
                                                            <div class="row" id="divClgSession" runat="server">
                                                                <div class="form-group col-lg-5 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYddlCollegeSession" runat="server" Font-Bold="true"></asp:Label>

                                                                    </div>
                                                                    <asp:DropDownList ID="ddlColgSession" runat="server" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                                        ToolTip="Please select School - Session" OnSelectedIndexChanged="ddlColgSession_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True" data-select2-enable="true">
                                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvColgSession" runat="server" ControlToValidate="ddlColgSession"
                                                                        Display="None" ErrorMessage="Please Select School - Session" ValidationGroup="Course"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <%-- <div class="form-inline">--%>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <%--<label>Scheme Type</label>--%>
                                                                        <asp:Label ID="lblDYrdoSchemeType" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>

                                                                    <asp:RadioButton ID="rbnOld" runat="server" GroupName="ST" Text="Non CBCS" Style="display: none;"
                                                                        TabIndex="2" AutoPostBack="true" OnCheckedChanged="rbnOld_CheckedChanged" />
                                                                    &nbsp;
                                                                 <asp:RadioButton ID="rbnNew" runat="server" GroupName="ST" Text="Regular" Checked="true"
                                                                     TabIndex="1" AutoPostBack="true"
                                                                     OnCheckedChanged="rbnNew_CheckedChanged" />

                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="divTutorial">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Attendance for</label>
                                                                    </div>
                                                                    <asp:RadioButton ID="rdoRegular" runat="server" Text="Regular" OnCheckedChanged="rdoRegular_CheckedChanged" GroupName="rdoattendancefor" Checked="true"
                                                                        TabIndex="2" AutoPostBack="true" />
                                                                    <asp:RadioButton ID="rdoTutorial" runat="server" Text="Tutorial" OnCheckedChanged="rdoTutorial_CheckedChanged" GroupName="rdoattendancefor"
                                                                        TabIndex="2" AutoPostBack="true" />
                                                                </div>
                                                            </div>
                                                            <%--Global Elective Date 21-01-2023--%>
                                                            <div class="row" id="divGlobalSession" runat="server" visible="false">
                                                                <div class="form-group col-lg-5 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>

                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSessionGlobal" runat="server" AutoPostBack="True" CssClass="form-control" TabIndex="1"
                                                                        ToolTip="Please select School - Session" OnSelectedIndexChanged="ddlSessionGlobal_SelectedIndexChanged"
                                                                        AppendDataBoundItems="True" data-select2-enable="true">
                                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSessionGlobal"
                                                                        Display="None" ErrorMessage="Please Select Session" ValidationGroup="Course"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <%--Global Elective END--%>

                                                            <div id="Cal" runat="server">
                                                                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" NextPrevFormat="FullMonth"
                                                                    Width="100%" CellSpacing="7" OnDayRender="Calendar1_DayRender" DayStyle-Height="70px"
                                                                    OnSelectionChanged="Calendar1_SelectionChanged" CssClass="mytable table table-striped table-bordered nowrap display">
                                                                    <SelectedDayStyle BackColor="#CCFFCC" ForeColor="#003300" />
                                                                    <TodayDayStyle BackColor="#ffffff" ForeColor="green" Font-Bold="true" BorderColor="green"
                                                                        BorderStyle="solid" BorderWidth="1px" />
                                                                    <OtherMonthDayStyle ForeColor="#999999" />
                                                                    <NextPrevStyle Font-Bold="True" Font-Size="10pt" Font-Underline="false" />
                                                                    <DayHeaderStyle Font-Bold="True" Font-Size="10pt" ForeColor="#333333" Height="10pt" CssClass="text-center" />
                                                                    <TitleStyle Height="12pt" CssClass="" Font-Underline="false" />
                                                                    <DayStyle BackColor="White" />
                                                                </asp:Calendar>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="notify-table mt-4">
                                                            <table class="table table-striped table-bordered nowrap display">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Notation</th>
                                                                        <th>Description</th>
                                                                        <th>Notation</th>
                                                                        <th>Description</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td class="text-center">
                                                                            <i class="fa fa-unlock fa-2x" aria-hidden="true"></i>
                                                                        </td>
                                                                        <td>Unlocked Lecture Day</td>
                                                                        <td class="text-center">
                                                                            <i class="fa fa-lock fa-2x" aria-hidden="true"></i>
                                                                        </td>
                                                                        <td>Locked Lecture Day</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="text-center">
                                                                            <i class="fa fa-lock fa-2x" aria-hidden="true"></i>
                                                                        </td>
                                                                        <td>Holiday</td>

                                                                        <td class="text-center">
                                                                            <i class="fa fa-font fa-2x" aria-hidden="true" style="color: lightgreen"></i>
                                                                        </td>
                                                                        <td>Attendance Marked</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="text-center">
                                                                            <i class="fa fa-font fa-2x" aria-hidden="true"></i>
                                                                        </td>
                                                                        <td>Shifted Lectures</td>
                                                                        <td class="text-center">
                                                                            <i class="green fa fa-square fa-2x" aria-hidden="true"></i>
                                                                        </td>
                                                                        <td>Current Date</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="text-center">
                                                                            <i class="fa fa-font fa-2x" aria-hidden="true" style="color: orange"></i>
                                                                        </td>
                                                                        <td>Regular Lecture</td>
                                                                        <td class="text-center">
                                                                            <i class="r-lecture fa fa-font fa-2x" aria-hidden="true"></i>
                                                                            <asp:Label ID="lbltest" runat="server" Text="[EC]"></asp:Label>
                                                                        </td>
                                                                        <td>Extra Class</td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="dvRegister" runat="server" visible="false">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">STUDENT DAILY ATTENDANCE</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div style="color: Red; font-weight: bold">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <div class="box-body">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red;">*</span> From Date </label>
                                        <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ValidationGroup="submit" CssClass="form-control" />
                                        <ajaxToolKit:CalendarExtender ID="cetxtFromDate" runat="server" Format="dd/MM/yyyy"
                                            PopupButtonID="imgfrm" TargetControlID="txtFromDate" Animated="true" Enabled="true" EnableViewState="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtFromDate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                            ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                            ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                        <asp:RequiredFieldValidator ID="rfvFromdate" runat="server" ControlToValidate="txtFromDate"
                                            Display="None" ErrorMessage="Please Enter From Date." ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <label><span style="color: red;">*</span> To Date </label>
                                        <asp:TextBox ID="txtTodate" runat="server" TabIndex="2" ValidationGroup="submit" />
                                        <ajaxToolKit:CalendarExtender ID="ceTodate" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgto"
                                            TargetControlID="txtTodate" Animated="true" Enabled="true" EnableViewState="true" />
                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                            OnFocusCssClass="MaskedEditFocus" TargetControlID="txtTodate"
                                            OnInvalidCssClass="errordate" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                            ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                            ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                        <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtTodate"
                                            Display="None" ErrorMessage="Please Enter To Date." ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12"></div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnDayWise" runat="server" Text="Daily Attendance Report"
                                        TabIndex="3" ValidationGroup="Report" OnClick="btnDayWise_Click" CssClass="btn btn-success"><i class="fa fa-file-excel-o" aria-hidden="true"> Daily Attendance Report</i></asp:LinkButton>
                                    <asp:Button ID="btnReturn" runat="server" Text="Back" OnClick="btnReturn_Click"
                                        TabIndex="4" CssClass="btn btn-info" />
                                    <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Report" />
                                </div>
                            </div>
                        </div>

                        <!-- The Modal -->
                        <div class="modal fade" id="myModal1">
                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 163%; margin-left: -24%; padding-right: 2%">

                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">Subjects details for Attendance</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>

                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <asp:ListView ID="lvSubjectList" runat="server">
                                            <EmptyDataTemplate>
                                                <div class="box text-center" style="width: 95%; margin-left: 1%;">
                                                    <asp:Label ID="lblEmpty" runat="server" Style="color: #f59d31;" Text="No Subject for Attendance for the Day"></asp:Label>
                                                </div>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>

                                                <table class="table table-bordered" style="width: 95%; margin-left: 1%;">
                                                    <tr class="bg-info">

                                                        <th>Subject
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYddlCourseType" runat="server" Font-Bold="true"></asp:Label>
                                                        </th>
                                                        <th>
                                                            <asp:Label ID="lblDYtxtSection" runat="server" Font-Bold="true"></asp:Label>
                                                            <%--Section--%>
                                                        </th>
                                                        <th>LectureType
                                                        </th>
                                                        <th>Status
                                                        </th>
                                                        <th>Submit By
                                                        </th>
                                                        <th>Submitted Date
                                                        </th>
                                                        <th>Lock/Unlock
                                                        </th>

                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkbtnCourse" CssClass="Gethiddenvalue" runat="server" CommandArgument='<%# Eval("TP_NO") %>'
                                                            OnClick="lnkbtnCourse_Click" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("COURSENO") %>'
                                                            Enabled='<%#Eval("LOCKSTATUS").ToString()=="1" ? false :true%>'></asp:LinkButton>
                                                        <asp:HiddenField ID="hfvSchme" runat="server" Value='<%#Eval("SCHEMENO") %>' />
                                                        <asp:HiddenField ID="hfvSem" runat="server" Value='<%#Eval("SEMESTERNO") %>' />
                                                        <asp:HiddenField ID="hfvSection" runat="server" Value='<%#Eval("SECTIONNO") %>' />
                                                        <asp:HiddenField ID="hfvbatch" runat="server" Value='<%#Eval("BATCHNO") %>' />
                                                        <asp:HiddenField ID="hfvslotNo" runat="server" Value='<%#Eval("SLOTNO") %>' />
                                                        <asp:HiddenField ID="hfvAttType" runat="server" Value='<%#Eval("AttType") %>' />
                                                        <asp:HiddenField ID="hfvAltCourseno" runat="server" Value='<%#Eval("AltCourseno") %>' />
                                                        <asp:HiddenField ID="hfvsubID" runat="server" Value='<%#Eval("SUBID") %>' />
                                                        <asp:HiddenField ID="hdnAttendanceNo" runat="server" Value='<%#Eval("ATT_TPNO") %>' />
                                                        <asp:HiddenField ID="hdnExtraClass" runat="server" Value='<%#Eval("EXTRACLASS") %>' />
                                                        <asp:HiddenField ID="hdnTopicCoveredStatus" runat="server" Value='<%#Eval("TC_STATUS") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("COURSE_TYPE")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("SECTIONNAME")%>
                                                        <asp:HiddenField ID="hdnSectinname" runat="server" Value='<%#Eval("SECTIONNAME") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("LectType")%>
                                                        <asp:HiddenField ID="hdnLectTypeNO" runat="server" Value='<%#Eval("LectTypeNo") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("STATUS")%>
                                                        <%--<asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("STATUS") %>' />--%>
                                                    <td style="text-align: center">
                                                        <%#Eval("TAKEN_BY")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%#Eval("ATT_DATE")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblLockStatus" runat="server" Text='<%#Eval("LOCKSTATUS").ToString()=="1" ? "Lock" :"Unlock"%>' Font-Bold="true"
                                                            ForeColor='<%#Eval("LOCKSTATUS").ToString()=="1" ? System.Drawing.Color.Red:System.Drawing.Color.Green%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                </div>
                            </div>
                        </div>



                        <div class="modal fade" id="idModal">
                            <div class="modal-dialog">
                                <div class="modal-content" style="width: 70%; margin-left: 63%; padding-right: 2%; height: 600%">
                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">Copy Attendance to Next Slot</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>
                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="form-group col-lg-8 col-md-8 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Copy From :</label>
                                                </div>
                                                <asp:Label ID="lblCopyAttFrom" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:HiddenField ID="hdnSlotno" Value='<%# Eval("SLOTNO") %>' runat="server" />
                                            </div>


                                            <div class="form-group col-lg-8 col-md-8 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Copy To :</label>
                                                </div>
                                                <asp:DropDownList ID="ddlCopytoSlot" runat="server" Font-Bold="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-6">
                                            <input id="btnCopyAttendance" runat="server" onclick="return InsertCopyAttendance()" text="Copy" value="Copy Attendance" class="btn btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttNo" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnDayWise" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $.noConflict();
            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });
        function bindDataTable() {
          
            var myDT = $('#examplex').DataTable({
                "paging": false,
                // "ordering": false,
                "info": false,
                "scrollY": "400px",
                "columns": [
                   { "width": "5%" },
                   { "width": "5%" },
                   null,
                   { "width": "5%" }
                ]
            });
        }
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function () {
            if ($('.mytable > table > td').children().length > 0) {
                $(this).find('.fa').not('span + span').remove();
            }
        })
    </script>

    <script type="text/javascript" language="javascript">

        function checkTime(field) {
            var errorMsg = "";
            var txtSlotVal = document.getElementById('<%=hdnSlotVal.ClientID %>').value;
            // regular expression to match required time format
            re = /^(\d{1,2}):(\d{2})(:00)?([ap]m)?$/;
            regs1 = txtSlotVal.match(re);

            if (field.value != "") {
                if (regs = field.value.match(re)) {
                    if (regs[4]) {
                        // 12-hour time format with am/pm
                        if (regs[1] < 1 || regs[1] > 60) {
                            errorMsg = "Please Enter ' Time' in MM:SS format(eg. 10:00 or 5:00).";
                        }
                    } else {
                        // 24-hour time format
                        if (regs[1] > 60) {
                            errorMsg = "Please Enter ' Time' in MM:SS format(eg. 10:00 or 5:00)";
                        }
                    }
                    if (!errorMsg && regs[2] > 59) {
                        errorMsg = "Please Enter ' Time' in MM:SS format(eg. 10:00 or 5:00).";
                    }
                } else {
                    errorMsg = "Please Enter ' Time' in MM:SS format(eg. 10:00 or 5:00).";
                }
            }
            //------------------------------------------------------------------//
            if (errorMsg == "" && field.value != "") {
                if (regs[4] == regs1[4]) {
                    // 12-hour time format with am/pm
                    if (regs[1] != "0") {
                        if (regs[1] != "00") {
                            if (regs[1] > regs1[1]) {
                                errorMsg = "Late by time duration exceeded.";
                            }
                        }
                    }
                }
                if (!errorMsg && regs[2] > regs1[2]) {
                    errorMsg = "Late by time duration exceeded.";
                }
            }
            //------------------------------------------------------------------//
            if (errorMsg != "") {
                field.focus();
                alert(errorMsg);
                field.value = "";
                field.focus();
                return false;
            }
            return true;
        }

        function checkTC() {
            var AttStatusNo = $('#ctl00_ContentPlaceHolder1_ddlStatus').val();
            var txtCONo = $('#ctl00_ContentPlaceHolder1_ddlCONumber').val();

            var txtTCode = $('#ctl00_ContentPlaceHolder1_ddlTopicCovered').val();//$('#ctl00_ContentPlaceHolder1_txtTopcDesc').val();
            if (AttStatusNo == "1" || AttStatusNo == "2") {
                if (txtTCode == '0') {
                    alert("Please Select Topic Covered");
                    return false;
                }
                //if (txtCONo == "0") {
                //    alert("Please Select CO Number");
                //    return false;
                //}
            }
        }

        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (e.disabled == false)
                            e.checked = true;
                    }
                    else
                        e.checked = false;
                }
            }
            CheckSelectionCount(this);
        }

        function CheckSelectionCount(chk) {

            var Tcount = 0;
            var Pcount = 0;
            var ODcount = 0;

            var txtT = document.getElementById('<%=txtTotalStudent.ClientID %>');
            var txtP = document.getElementById('<%=txtPresentStudent.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsentStudent.ClientID %>');
            var txtOD = document.getElementById('<%=txtODStudent.ClientID %>');
            var frm = document.forms[0]

            var rows = document.getElementById("tblStudentRecords").getElementsByTagName("tr").length;
            Tcount = rows;

            for (i = 0; i < rows; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                if (e.checked == true) {
                    Pcount++;
                }
                if (e.checked == false && e1.value == 1) {
                    ODcount++;
                }
            }
            txtT.value = Tcount;
            txtP.value = Pcount;
            txtA.value = (Tcount - Pcount - ODcount);
            txtOD.value = (ODcount);

            var str = chk.id;
            var start = str.indexOf("_ctrl") + 5;
            var end = str.indexOf("_cbRow");
            var eindex = str.substring(start, end);

            //ctl00_ContentPlaceHolder1_lvStudent_ctrl0_txtLTime
            var extChk = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + eindex + "_txtLTime");
            if (chk.checked == true) {
                extChk.checked = true;
                extChk.disabled = false;
            }
            else {
                extChk.disabled = true;
            }
        }
        function CheckSelectionCount_Temp(chk) {
            var Pcount = 0;
            var list = document.getElementById("<%=lvStudent.ClientID %>");
            var txtP = document.getElementById('<%=txtPresentStudent.ClientID %>');
            var txtT = document.getElementById('<%=txtTotalStudent.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsentStudent.ClientID %>');
            var frm = document.forms[0]
            var Tcount = list.rows.length;
            alert(Tcount);
            if (list.rows.length > 0) {
                for (Row = 1; Row < GridView.rows.length; Row++) {
                    if (chk.checked == true)
                        Pcount++;
                }
            }
            txtT.value = Tcount;
            txtP.value = Pcount;
            txtA.value = (Tcount - Pcount);
        }

        function ConfirmToDelete(me) {
            if (me != null) {
                var ret = confirm('Are you sure to Delete this Attendance Entry?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }

        function SetUniqueRadioButton(current) {
            var tbl = document.getElementById('tblAudit');
            if (tbl != null && tbl.rows && tbl.rows.length > 0) {
                for (i = 0; i < tbl.rows.length - 1; i++) {
                    var elm = document.getElementById('ctl00_ContentPlaceHolder1_ctrl' + i + '_rdoSelect');
                    if (elm.type == 'radio')
                        elm.checked = false;
                }
            }
            current.checked = true;
            return;
        }

        //$(document).ready(function () { if ($('table td').children().length > 0) { $(this).find('.fa').not('span + span').remove(); } });
    </script>

    <script type="text/javascript">
        function ShowExistingSlot() {
            var lectureDate = document.getElementById('<%=txtLectDate.ClientID%>').innerText;
            var courseno = document.getElementById("<%=txtCourse.ClientID%>").title;

            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/AttendanceEntry_Modified.aspx/GetExistingSlotDetails")%>',//"AttendanceEntry_Modified.aspx/GetExistingSlotDetails",
                data: '{LectureDate: ' + JSON.stringify(lectureDate) + ',courseno:' + courseno + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    if (JSON.parse(r.d) == '' || JSON.parse(r.d) == null) {
                        alert('Next slot not available. Please mark the attendance first.')
                        return false;
                    }
                    showModalATT();
                    //var data = JSON.stringify(r.d);
                    var x = JSON.parse(r.d);

                    //x = x.replace(/&lt;/g, "<");
                    //x = x.replace(/&gt;/g, ">");
                    //x = x.replace(/&amp;/g, "&")
                    //alert(x.data)
                    $.each(x, function (a, b) {
                        document.getElementById('<%= lblCopyAttFrom.ClientID %>').innerHTML = b.LECTURESLOT;
                        var slotno = b.SLOTNO;
                        hdnAttNo = b.ATT_NO;
                        FillTimeSlot(slotno);
                    });
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (e) {
                    console.log(e);
                }
            });
            }
            function OnSuccess(response) {
                alert(response.d);
            }
    </script>

    <script type="text/javascript">
        function FillTimeSlot(slotno) {
            $.ajax({
                type: "POST",
                //url: "Default.aspx/GetTimeSlots",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/AttendanceEntry_Modified.aspx/GetTimeSlots")%>',
                data: '{slotno: ' + slotno + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var ddlCopytoSlot = $("[id*=ddlCopytoSlot]");
                    ddlCopytoSlot.empty().append('<option selected="selected" value="0">Please select</option>');
                    $.each(r.d, function () {
                        ddlCopytoSlot.append($("<option></option>").val(this['Value']).html(this['Text']));
                    });
                }
            });
        };
    </script>

    <script type="text/javascript" language="javascript">

        function ShowHideCopy() {
            $("#btnCopyAtt").show();
        }
    </script>

    <script type="text/javascript">
        function InsertCopyAttendance() {

            var ddlCopytoSlot = $("[id*=ctl00_ContentPlaceHolder1_ddlCopytoSlot]").val();

            var classType = $("[id*=ctl00_ContentPlaceHolder1_ddlClassType]").val();
            var att_status = $("[id*=ctl00_ContentPlaceHolder1_ddlStatus]").val();
            var hdnTeachingPlanStatus = document.getElementById('<%=hdnTeachingPlanStatus.ClientID %>').value;
            // hdnTeachingPlanStatus=1 dropdown
            if (hdnTeachingPlanStatus == "1") {
                var tpno = $("[id*=ctl00_ContentPlaceHolder1_ddlTopicCovered]").val();
                if (tpno == 0) {
                    alert('Please Select Topic Covered', 'Warning!');
                    $("[id*=ctl00_ContentPlaceHolder1_ddlTopicCovered]").focus();
                    return false;
                }
            }
            else {
                tpno = "0";

                var txtTopicDesc = document.getElementById("<%=txtTopcDesc.ClientID%>").value;
            }


            if (txtTopicDesc == null || txtTopicDesc == "") {
                txtTopicDesc = "";
            }

            if (ddlCopytoSlot == 0) {
                alert('Please Select Slot', 'Warning!');
                $("[id*=ctl00_ContentPlaceHolder1_ddlCopytoSlot]").focus();
                return false;
            }

            $.ajax({
                type: "POST",
                url: '<%=Page.ResolveUrl("~/ACADEMIC/TimeTable/AttendanceEntry_Modified.aspx/CopyAttendance")%>',
                //url: "AttendanceEntry_Modified.aspx/CopyAttendance",
                data: '{slotno: ' + JSON.parse(ddlCopytoSlot) + ',att_no: ' + JSON.parse(hdnAttNo) + ',class_type: ' + JSON.parse(classType) + ',att_status: ' + JSON.parse(att_status) + ',topic_desc: ' + JSON.stringify(txtTopicDesc) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    result = $.parseJSON(result);

                    if (result == 1) {
                        alert('Attendance copied successfully');
                        hideModalATT();
                    }
                    else {
                        alert('Something went wrong');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (e) {
                    console.log(e);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>
    <script>


        //Search filter Modified by Rohit M - 03/10/2023 
        function CheckSelectionCount(chk) {

            var Tcount = 0;
            var Pcount = 0;
            var ODcount = 0;

            var txtT = document.getElementById('<%=txtTotalStudent.ClientID %>');
            var txtP = document.getElementById('<%=txtPresentStudent.ClientID %>');
            var txtA = document.getElementById('<%=txtAbsentStudent.ClientID %>');
            var txtOD = document.getElementById('<%=txtODStudent.ClientID %>');
            var frm = document.forms[0]

            var rows = document.getElementById("tblStudentRecordsnew").getElementsByTagName("tr").length;
            Tcount = rows - 1;

            for (i = 0; i < rows - 1 ; i++) {
                var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                if (e != null) {
                    if (e.checked == true) {
                        Pcount++;
                    }
                    if (e.checked == false && e1.value == 1) {
                        ODcount++;
                    }
                }
            }

            txtT.value = Tcount;
            txtP.value = Pcount;
            txtA.value = (Tcount - Pcount - ODcount);
            txtOD.value = (ODcount);
        }

        //New function added by Rohit M - 03/10/2023
        function SearchFunction() {
            var input, filter, table, tr, td, i, txtValue, td1, td2;
            var Tcount = 0;
            var Pcount = 0;
            var ODcount = 0;
            var totalcount = 0;
            var regnoflag = 0;
            var rollnoflag = 0;
            var namefalg = 0;

            //var txtT = document.getElementById('<%=txtTotalStudent.ClientID %>');
            //var txtP = document.getElementById('<%=txtPresentStudent.ClientID %>');
            //var txtA = document.getElementById('<%=txtAbsentStudent.ClientID %>');
            //var txtOD = document.getElementById('<%=txtODStudent.ClientID %>');

            input = document.getElementById("FilterData");
            filter = input.value.toLowerCase();
            table = document.getElementById("tblStudentRecordsnew");
            trRow = table.getElementsByTagName("tr");

            for (i = 0; i < trRow.length; i++) {
                td = trRow[i].getElementsByTagName("td")[3]; // 3- check name column
                td1 = trRow[i].getElementsByTagName("td")[1]; // 1- check rrm column
                td2 = trRow[i].getElementsByTagName("td")[2]; // 2- check roll column
                if (td) {

                    //Name search
                    if (regnoflag == 0 && rollnoflag == 0) {
                        txtValue = td.textContent || td.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            namefalg = 1;
                            Tcount++;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                            if (e != null) {
                                if (e.checked == true) {
                                    Pcount++;
                                }
                                if (e.checked == false && e1.value == 1) {
                                    ODcount++;
                                }
                            }

                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                    //Regno search
                    if (namefalg == 0 && rollnoflag == 0) {
                        txtValue = td1.textContent || td1.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            regnoflag = 1;
                            Tcount++;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                            if (e != null) {
                                if (e.checked == true) {
                                    Pcount++;
                                }
                                if (e.checked == false && e1.value == 1) {
                                    ODcount++;
                                }
                            }

                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                    //Roll No search
                    if (namefalg == 0 && regnoflag == 0) {
                        txtValue = td2.textContent || td2.innerText;
                        if (txtValue.toLowerCase().indexOf(filter) > -1) {
                            rollnoflag = 1;
                            Tcount++;
                            var e = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_cbRow");
                            var e1 = document.getElementById("ctl00_ContentPlaceHolder1_lvStudent_ctrl" + i + "_hdfLeaveStatus");
                            if (e != null) {
                                if (e.checked == true) {
                                    Pcount++;
                                }
                                if (e.checked == false && e1.value == 1) {
                                    ODcount++;
                                }
                            }

                            trRow[i].style.display = "";

                        }
                        else {
                            trRow[i].style.display = "none";
                        }
                    }

                }
            }

        }
        //end search filter code



    </script>

</asp:Content>
